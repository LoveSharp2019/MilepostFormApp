using Basler.Pylon;
using Cell.DataModel;
using Cell.Interface;
using Cell.Tools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Org.ICamera
{
    [MyVersion("1.0.0.0")]
    [MyDisplayName("Basler 相机")]
    public class IPlatCamera_Basler : IPlatDevice_Camera, IPlatRealtimeUIProvider
    {
        string _cmrIP = null; // 输入参数 相机IP
        Camera myCamera = null;//相机对象
        internal IPlatCamera_Basler()
        {
            IsInitOK = false;
        }

        ~IPlatCamera_Basler()
        {
            Dispose(false);
        }

        enum ErrorDef
        {
            Success = 0,//操作成功，无错误
            Unsupported = -1,//设备不支持此功能
            ParamError = -2,//参数错误（不支持的参数）
            InvokeFailed = -3,//库函数调用出错
            Allowed = 1,//调用成功，但不是所有的参数都支持
            InitFailedWhenOpen = -4,//
            NotOpen = -5, //卡未打开
            Timeout = -6, //操作超时
            DevUnExisted = -7,//设备不存在
            TrigModeUnMatch = -8, //触发模式不匹配
            NotGrabbing = -9,
            RegistCBFailed = -10,//注册回调函数失败
            Grabbing = -11, //当前正在抓图，不能设置某些参数
            noDevice = -12
        }

        #region IPlatInitializable 接口
        /// <summary>获取初始化需要的所有参数的名称 </summary>
        public string[] InitParamNames { get { return new string[] { "IP" }; } }


        void _CheckInitName(string initName, string funcName)
        {
            if (null == initName)
                throw new ArgumentNullException(string.Format("{0} failed By: name = null! ", funcName));
            if (!InitParamNames.Contains(initName))
                throw new ArgumentException(string.Format("{0} failed By: name = {1} is not included by InitParamNames:{2}", funcName, initName, string.Join("|", InitParamNames)));
        }

        /// <summary>
        /// 获取指定名称的初始化参数的信息
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public CosParams GetInitParamDescribe(string name)
        {
            _CheckInitName(name, "GetInitParamDescribe");
            CosParams ret = null;
            if (name == "IP")
                ret = CosParams.Create(name, typeof(string), cValueLimit.Non, null, false, "相机IP地址");

            return ret;
        }

        /// <summary>
        /// 获取指定名称的初始化参数的当前值
        /// </summary>
        /// <param name="name">参数名称，如果参数名称不在InitParamNames中，将会抛出一个ArgumentException异常</param>
        /// <returns>参数值</returns>
        public object GetInitParamValue(string name)
        {
            _CheckInitName(name, "GetInitParamValue");
            if ("IP" == name)
                return _cmrIP;
            return null;
        }

        string _initErrorInfo = "NO-OPS";

        /// <summary>
        ///设置取指定名称的初始化参数的当前值
        /// </summary>
        /// <param name="name">参数名称，如果参数名称不在InitParamNames中，将会抛出一个ArgumentException异常</param>
        /// <param name="value">参数值</param>
        /// <returns>操作成功返回True，失败返回false，可通过GetInitErrorInfo()获取错误信息</returns>
        public bool SetInitParamValue(string name, object value)
        {
            _CheckInitName(name, "SetInitParamValue(name, value)");
            if (null == value)
                throw new Exception("SetInitParamValue(name, value) failed By: value = null");

            if (!Type.GetType(GetInitParamDescribe(name).ptype).IsAssignableFrom(value.GetType()))
                throw new ArgumentException(string.Format("SetInitParamValue(name = {0}, value) faile By: value's Type = {1} can't Assignable to InitParam's Type:{2}", name, value.GetType(), Type.GetType(GetInitParamDescribe(name).ptype).Name));
            if (name == "IP")
            {
                string tmp = value as string;
                if (!ToolsFun.IsIPAddress(tmp))
                {
                    _initErrorInfo = string.Format("SetInitParamValue(string name = {0},...) failed By: name isnot Legal ip address", name);
                    return false;
                }
                _cmrIP = tmp;
                _initErrorInfo = "Success";
                return true;
            }
            _initErrorInfo = string.Format("SetInitParamValue(string name = {0},...) failed By: name  is not included by InitParamNames:{1}", name, string.Join("|", InitParamNames));
            return false;
        }

        /// <summary>
        /// 对象初始化
        /// </summary>
        /// <returns>操作成功返回True，失败返回false，可通过GetInitErrorInfo()获取错误信息</returns>
        public bool Initialize()
        {
            if (!ToolsFun.IsIPAddress(_cmrIP))
            {
                _initErrorInfo = "Initialize()failed by IP = \"" + _cmrIP + "\" is not legal ip address";
                IsInitOK = false;
                return false;
            }
            IsInitOK = true;
            _initErrorInfo = "Success";
            return true;
        }


        /// <summary>获取初始化状态，如果对象已初始化成功，返回True</summary>
        public bool IsInitOK { get; private set; }

        /// <summary>获取初始化错误的描述信息</summary>
        public string GetInitErrorInfo() { return _initErrorInfo; }
        #endregion//Initor接口

        #region InterfaceDevice 接口
        public string DeviceModel { get { return "Basler Camera"; } }

        public string DeviceStatus { get { return IsDeviceOpen ? "打开" : "关闭"; } }

        /// <summary>
        /// 打开设备
        /// </summary>
        public int OpenDevice()
        {
            if (!IsInitOK)
            {
                return (int)ErrorDef.InitFailedWhenOpen;
            }

            if (IsDeviceOpen)
                return (int)ErrorDef.Success;

            List<ICameraInfo> deviceList = IpConfigurator.EnumerateAllDevices();
            foreach (var device in deviceList)
            {
                if (device[CameraInfoKey.DeviceIpAddress] == _cmrIP)
                {
                    //如果当前相机信息中序列号是指定的序列号，则实例化相机类
                    myCamera = new Camera(device);
                    myCamera.Open();//打开相机

                    IsDeviceOpen = true;

                    return (int)ErrorDef.Success;
                }
            }
            return (int)ErrorDef.noDevice;
        }

        /// <summary>
        /// 关闭设备
        /// </summary>
        public int CloseDevice()
        {
            try
            {
                if (!IsDeviceOpen)
                    return (int)ErrorDef.Success;

                myCamera.Close();
            }
            catch
            {
                return (int)ErrorDef.InvokeFailed;
            }
            IsDeviceOpen = false;

            return (int)ErrorDef.Success;
        }

        /// <summary>
        /// 设备是否已经打开
        /// </summary>
        public bool IsDeviceOpen { get; private set; }
        #endregion//IJFDevice接口

        #region ErrorInfo接口
        public string GetErrorInfo(int errorCode)
        {
            string ret = "Unknown ErrorCode: " + errorCode;
            switch (errorCode)
            {
                case (int)ErrorDef.Success:
                    ret = "Success";
                    break;
                case (int)ErrorDef.Unsupported:
                    ret = "Unsupported";
                    break;
                case (int)ErrorDef.ParamError:
                    ret = "Param Error";
                    break;
                case (int)ErrorDef.InvokeFailed:
                    ret = "Invoke Failed";
                    break;
                case (int)ErrorDef.Allowed:
                    ret = "Allowed,Not all param are supported";
                    break;
                case (int)ErrorDef.InitFailedWhenOpen:
                    ret = "Camera is uninitialised When open";
                    break;
                case (int)ErrorDef.NotOpen:
                    ret = "Camera is not open";
                    break;
                case (int)ErrorDef.Timeout:
                    ret = "Failed by timeout";
                    break;
                case (int)ErrorDef.DevUnExisted:
                    ret = "Camera is unexist with current ip";
                    break;
                case (int)ErrorDef.TrigModeUnMatch:
                    ret = "Trig-Mode is not Match";
                    break;
                case (int)ErrorDef.NotGrabbing:
                    ret = "Not Grabbing";
                    break;
                case (int)ErrorDef.RegistCBFailed:
                    ret = "Regist Frame Callback failed!";
                    break;
                case (int)ErrorDef.Grabbing:
                    ret = "Failed When Grabbing!";
                    break;
                default:
                    break;
            }
            return ret;
        }
        #endregion

        #region 相机接口
        /// <summary>相机是否处于触发模式</summary>  
        public int GetTrigMode(out cCmrTrigMode tm)
        {
            tm = cCmrTrigMode.disable;

            IEnumParameter triggerSelector = myCamera.Parameters[PLCamera.TriggerSelector];
            IEnumParameter triggerMode = myCamera.Parameters[PLCamera.TriggerMode];
            IEnumParameter triggerSource = myCamera.Parameters[PLCamera.TriggerSource];

            if (triggerMode.GetValueOrDefault(PLCamera.TriggerMode.Off) == PLCamera.TriggerMode.Off)
            {
                tm = cCmrTrigMode.disable;
            }

            if (triggerMode.GetValueOrDefault(PLCamera.TriggerMode.On) == PLCamera.TriggerMode.On)
            {
                if (triggerSource.GetValueOrDefault(PLCamera.TriggerSource.Software) == PLCamera.TriggerSource.Software)
                {
                    tm = cCmrTrigMode.software;
                }
                else if (triggerSource.GetValueOrDefault(PLCamera.TriggerSource.Line1) == PLCamera.TriggerSource.Line1)
                {
                    tm = cCmrTrigMode.hardware_line0;
                }
                else if (triggerSource.GetValueOrDefault(PLCamera.TriggerSource.Line2) == PLCamera.TriggerSource.Line2)
                {
                    tm = cCmrTrigMode.hardware_line1;
                }
                else if (triggerSource.GetValueOrDefault(PLCamera.TriggerSource.Line3) == PLCamera.TriggerSource.Line3)
                {
                    tm = cCmrTrigMode.hardware_line2;
                }
                else if (triggerSource.GetValueOrDefault(PLCamera.TriggerSource.Line4) == PLCamera.TriggerSource.Line4)
                {
                    tm = cCmrTrigMode.hardware_line3;
                }
                else
                    return (int)ErrorDef.Unsupported;
            }

            return (int)ErrorDef.Success;
        }
        public int SetTrigMode(cCmrTrigMode tm)
        {
            try
            {
                if (!IsDeviceOpen)
                    return (int)ErrorDef.NotOpen;
                if (IsGrabbing)
                {
                    cCmrTrigMode currMode;
                    if (0 == GetTrigMode(out currMode) && currMode == tm)
                        return (int)ErrorDef.Success;
                    return (int)ErrorDef.Grabbing;
                }
                IEnumParameter triggerSelector = myCamera.Parameters[PLCamera.TriggerSelector];
                IEnumParameter triggerMode = myCamera.Parameters[PLCamera.TriggerMode];
                IEnumParameter triggerSource = myCamera.Parameters[PLCamera.TriggerSource];

                switch (tm)
                {
                    case cCmrTrigMode.disable:

                        triggerMode.SetValue(PLCamera.TriggerMode.Off);
                        break;
                    case cCmrTrigMode.software:
                        //if (myCamera.WaitForFrameTriggerReady(1000, TimeoutHandling.ThrowException))
                        //{
                        //    myCamera.ExecuteSoftwareTrigger();
                        //}                    
                        triggerMode.SetValue(PLCamera.TriggerMode.On);
                        triggerSource.SetValue(PLCamera.TriggerSource.Software);
                        break;
                    case cCmrTrigMode.hardware_line0:
                        triggerMode.SetValue(PLCamera.TriggerMode.On);
                        triggerSource.SetValue(PLCamera.TriggerSource.Line1);
                        break;
                    case cCmrTrigMode.hardware_line1:
                        triggerMode.SetValue(PLCamera.TriggerMode.On);
                        triggerSource.SetValue(PLCamera.TriggerSource.Line2);
                        break;
                    case cCmrTrigMode.hardware_line2:
                        triggerMode.SetValue(PLCamera.TriggerMode.On);
                        triggerSource.SetValue(PLCamera.TriggerSource.Line3);
                        break;
                    case cCmrTrigMode.hardware_line3:
                        triggerMode.SetValue(PLCamera.TriggerMode.On);
                        triggerSource.SetValue(PLCamera.TriggerSource.Line4);
                        break;
                    default:
                        return (int)ErrorDef.TrigModeUnMatch;
                }

                return (int)ErrorDef.Success;
            }
            catch
            {
                return (int)ErrorDef.InvokeFailed;
            }
        }

        public bool IsGrabbing { get; private set; }

        /// <summary>开始图像采集</summary>
        /// <returns></returns>
        public int StartGrab()
        {
            if (!IsDeviceOpen)
                return (int)ErrorDef.NotOpen;
            if (IsGrabbing)
                return (int)ErrorDef.Success;
            try
            {
                myCamera.StreamGrabber.Start();
            }
            catch
            {
                return (int)ErrorDef.InvokeFailed;
            }

            IsGrabbing = true;
            return (int)ErrorDef.Success;
        }
        /// <summary>停止图像采集</summary>
        public int StopGrab()
        {
            if (!IsDeviceOpen || !IsGrabbing)
                return (int)ErrorDef.Success;
            try
            {
                myCamera.StreamGrabber.Stop();
            }
            catch
            {
                return (int)ErrorDef.InvokeFailed;
            }
            IsGrabbing = false;
            return (int)ErrorDef.Success;

        }

        /// <summary>实时抓拍一张图片</summary>
        public int GrabOne(out IPlat_Image img, int timeoutMilSeconds = -1)
        {
            img = null;
            if (!IsDeviceOpen)
                return (int)ErrorDef.NotOpen;

            StartGrab();

            if (!IsGrabbing)
                return (int)ErrorDef.NotGrabbing;
            cCmrTrigMode currTM = cCmrTrigMode.disable;
            GetTrigMode(out currTM);
            if (currTM != cCmrTrigMode.software)
                return (int)ErrorDef.TrigModeUnMatch;


            //软触发一次
            SoftwareTrig();

            IGrabResult grabResult = myCamera.StreamGrabber.RetrieveResult(4000, TimeoutHandling.ThrowException);//读取buffer，超时时间为4000ms

            using (grabResult)
            {
                if (grabResult.GrabSucceeded)
                {
                    if (IsMonoData(grabResult))
                    {
                        byte[] buffer = grabResult.PixelData as byte[];
                        img = new IPlatImage_Basler(buffer, grabResult.Height, grabResult.Width, IPlatImgPixFormat.Mono8);
                        //如果是黑白图像，则利用GenImage1算子生成黑白图像

                        //IntPtr p = Marshal.UnsafeAddrOfPinnedArrayElement(buffer, 0);
                        //image.GenImage1("byte", grabResult.Width, grabResult.Height, p);
                    }
                    else
                    {
                        if (grabResult.PixelTypeValue != PixelType.RGB8packed)
                        {
                            //如果图像不是RGB8格式，则将图像转换为RGB8，然后生成彩色图像
                            //因为GenImageInterleaved算子能够生成的图像的数据，常见的格式只有RGB8
                            //如果采集的图像是RGB8则不需转换，具体生成图像方法请自行测试编写。
                            byte[] buffer_rgb = new byte[grabResult.Width * grabResult.Height * 3];
                            Basler.Pylon.PixelDataConverter convert = new PixelDataConverter();
                            convert.OutputPixelFormat = PixelType.RGB8packed;
                            convert.Convert(buffer_rgb, grabResult);

                            img = new IPlatImage_Basler(buffer_rgb, grabResult.Height, grabResult.Width, IPlatImgPixFormat.RGB24);
                            //IntPtr p = Marshal.UnsafeAddrOfPinnedArrayElement(buffer_rgb, 0);
                            //image.GenImageInterleaved(p, "rgb", grabResult.Width, grabResult.Height, 0, "byte", grabResult.Width, grabResult.Height, 0, 0, -1, 0);
                        }
                    }
                }
                else
                {
                    return (int)ErrorDef.InvokeFailed;
                }
            }
            StopGrab();
            return (int)ErrorDef.Success;
        }

        private Boolean IsMonoData(IGrabResult iGrabResult)//判断图像是否为黑白格式
        {
            switch (iGrabResult.PixelTypeValue)
            {
                case PixelType.Mono1packed:
                case PixelType.Mono2packed:
                case PixelType.Mono4packed:
                case PixelType.Mono8:
                case PixelType.Mono8signed:
                case PixelType.Mono10:
                case PixelType.Mono10p:
                case PixelType.Mono10packed:
                case PixelType.Mono12:
                case PixelType.Mono12p:
                case PixelType.Mono12packed:
                case PixelType.Mono16:
                    return true;
                default:
                    return false;
            }
        }

        public int SoftwareTrig()
        {

            if (!IsDeviceOpen || !IsGrabbing)
                return (int)ErrorDef.NotOpen;
            try
            {
                myCamera.ExecuteSoftwareTrigger();
            }
            catch
            {
                return (int)ErrorDef.InvokeFailed;
            }

            return (int)ErrorDef.Success;
        }

        public int SetExposureTime(double ExposureTimeNum)//设置曝光时间us
        {
            try
            {
                myCamera.Parameters[PLCamera.ExposureTimeAbs].SetValue(ExposureTimeNum);
            }
            catch
            {
                return (int)ErrorDef.InvokeFailed;
            }
            return (int)ErrorDef.Success;
        }

        public int GetExposureTime(out double ExposureTimeNum)//获取曝光时间us
        {
            ExposureTimeNum = 0;
            try
            {
                ExposureTimeNum = myCamera.Parameters[PLCamera.ExposureTimeAbs].GetValue();
            }
            catch
            {
                return (int)ErrorDef.InvokeFailed;
            }
            return (int)ErrorDef.Success;
        }

        public int SetBalanceAuto(bool enable)//关闭自动白平衡
        {
            try
            {
                if (enable)
                    myCamera.Parameters[PLCamera.BalanceWhiteAuto].TrySetValue("On");
                else
                    myCamera.Parameters[PLCamera.BalanceWhiteAuto].TrySetValue("Off");
            }
            catch
            {
                return (int)ErrorDef.InvokeFailed;
            }
            return (int)ErrorDef.Success;
        }


        /// <summary>X方向镜像</summary>
        //bool ReverseX { get; set; }
        public int GetReverseX(out bool enabled)
        {
            enabled = false;
            return (int)ErrorDef.Unsupported;
        }
        public int SetReverseX(bool enabled)//暂不实现
        {
            return (int)ErrorDef.Unsupported;
        }
        /// <summary>Y方向镜像</summary>      
        public int GetReverseY(out bool enabled)//暂不实现 
        {
            enabled = false;
            return (int)ErrorDef.Unsupported;
        }
        public int SetReverseY(bool enabled) //暂不实现
        {
            return (int)ErrorDef.Unsupported;
        }

        /// <summary>设置相机增益参数</summary>
        public int SetGain(double value)
        {
            return (int)ErrorDef.Unsupported;
        }
        /// <summary>获取相机增益参数</summary>
        public int GetGain(out double value)
        {
            value = 0;
            return (int)ErrorDef.Unsupported;
        }

        /// <summary>内部图片缓存的最大数量</summary>
        public int GetBuffSize(out int maxNum)
        {
            maxNum = 0;
            return (int)ErrorDef.Success;

        }

        public int SetBuffSize(int maxNum)
        {
            return (int)ErrorDef.Success;
        }

        event CmrAcqFrameDelegate AcqFrameEvent;

        /// <summary>图像采集回调函数</summary>

        public bool IsRegistAcqFrameCallback
        {
            get { return null != AcqFrameEvent; }
        }

        public int RegistAcqFrameCallback(CmrAcqFrameDelegate callback)
        {
            return (int)ErrorDef.Unsupported;
        }
        public void RemoveAcqFrameCallback(CmrAcqFrameDelegate callback)
        {

        }
        public void ClearAcqFrameCallback()
        {

        }


        /// <summary>当前已缓存的帧数</summary>
        public int CurrBuffCount() { return -1; }
        public int ClearBuff() { return (int)ErrorDef.Unsupported; }

        /// <summary>
        /// 从队列中取出指定数量的图片
        /// </summary>
        /// <param name="images"></param>
        /// <param name="framecount"></param>
        /// <returns></returns>
        public int DeqFrames(out IPlat_Image[] images, int framecount, int timeoutMilSec)
        {
            throw new NotImplementedException();
        }

        #endregion//相机接口

        /// <summary>实时调试界面接口</summary>
        public UcRealTimeUI GetRealtimeUI()
        {
            Uc_RealTimeCmr ui = new Uc_RealTimeCmr();
            ui.SetCamera(this);
            return ui;
        }

        public void Dispose()
        {
            Dispose(true);
            System.GC.SuppressFinalize(this);
        }

        void Dispose(bool disposing)
        {
            ////////////释放非托管资源
            CloseDevice();
            if (disposing)//////////////释放其他托管资源
            {

            }
        }
    }
}
