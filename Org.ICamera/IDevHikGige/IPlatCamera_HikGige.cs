using Cell.DataModel;
using Cell.Interface;
using Cell.Tools;
using MvCamCtrl.NET;
using Org.ICamera.IDevHikGige;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using static MvCamCtrl.NET.MyCamera;


namespace Org.ICamera
{
    /// <summary>
    /// 海康GIGE相机类
    /// </summary>
    [MyVersion("1.0.0.0")]
    [MyDisplayName("海康GIGE系列工业相机")]
    public class IPlatCamera_HikGige : IPlatDevice_Camera, IPlatRealtimeUIProvider
    {
        internal IPlatCamera_HikGige()
        {
            IsInitOK = false;
            IsDeviceOpen = false;
            IsGrabbing = false;
            _hikCmr = new MyCamera();
        }

        ~IPlatCamera_HikGige()
        {
            Dispose(false);
        }

        string linkMode = null;   //0：IP    1：使用SN连接
        string linkParam = null; // IP 或者SN

        MyCamera _hikCmr = null;
        int _payloadSize = 0; //图像帧数据字节数

        #region Initor接口
        /// <summary>获取初始化需要的所有参数的名称 </summary>     
        public string[] InitParamNames => new string[] { "连接方式", "连接参数" };
        object[] linkModeParamRange = new object[] { "IP", "序列号" };


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
            if (name == "连接方式")
                ret = CosParams.Create("连接方式", typeof(string), cValueLimit.Range, linkModeParamRange, "工控机与相机的连接方式",true);
            if (name == "连接参数")
                ret = CosParams.Create("连接参数", typeof(string), cValueLimit.Non, null, "在指定的连接方式下，使用此参数与相机建立连接");
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
            if (name == "连接方式")
                return linkMode;
            else if (name == "连接参数")
                return linkParam;
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
                throw new ArgumentException(string.Format("SetInitParamValue(name = {0}, value) faile By: value's Type = {1} can't Assignable to InitParam's Type:{2}", name, value.GetType(), GetInitParamDescribe(name).ptype));

            if (name == "连接方式")
            {
                if (null == value)
                {
                    linkMode = null;
                    _initErrorInfo = "SetInitParamValue(name = \"连接方式\",value) failed by value = null";
                    return false;
                }
                string tmp = value as string;
                if (!linkModeParamRange.Contains(tmp))
                {
                    linkMode = null;
                    _initErrorInfo = string.Format("SetInitParamValue(name = \"连接方式\",value = {0}) failed by value isnot included by legal params:\"{1}\"|\"{2}\"|\"{3}\"", tmp, linkModeParamRange[0], linkModeParamRange[1], linkModeParamRange[2]);
                    return false;
                }
                linkMode = tmp;
                _initErrorInfo = "Success";
                return true;
            }
            else if (name == "连接参数")
            {
                if (string.IsNullOrEmpty((string)value))
                {
                    _initErrorInfo = string.Format("SetInitParamValue(name = \"连接参数\",value) failed By:value is null or empty string");
                    return false;
                }
                if (linkModeParamRange[0].ToString() == linkMode)
                {
                    string tmp = value as string;
                    if (!ToolsFun.IsIPAddress(tmp))
                    {
                        _initErrorInfo = string.Format("SetInitParamValue(string name = {0},...) failed By: name isnot Legal ip address", name);
                        return false;
                    }
                }

                linkParam = (string)value;
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
            if (linkModeParamRange[0].ToString() == linkMode)
                if (!ToolsFun.IsIPAddress(linkParam))
                {
                    _initErrorInfo = "Initialize()failed by IP = \"" + linkParam + "\" is not legal ip address";
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


        #region IJFDevice接口
        public string DeviceModel { get { return "海康GIGE相机"; } }

        public string DeviceStatus { get { return IsDeviceOpen ? "打开" : "关闭"; } }

        /// <summary>
        /// 打开设备
        /// </summary>
        public int OpenDevice()
        {
            int ret = (int)ErrorDef.Success;
            do
            {
                if (!IsInitOK)
                {
                    ret = (int)ErrorDef.InitFailedWhenOpen;
                    break;
                }
                if (IsDeviceOpen)
                    return (int)ErrorDef.Success;
                ///枚举当前所有GIGE设备
                MyCamera.MV_CC_DEVICE_INFO_LIST stDevList = new MyCamera.MV_CC_DEVICE_INFO_LIST();

                // int nRet = MyCamera.MV_CC_EnumDevices_NET(MyCamera.MV_GIGE_DEVICE | MyCamera.MV_USB_DEVICE, ref m_stDeviceList);

                MyCamera.MV_CC_EnumDevices_NET((uint)MyCamera.MV_GIGE_DEVICE, ref stDevList);
                if (stDevList.nDeviceNum == 0)
                {
                    ret = (int)ErrorDef.DevUnExisted;
                    break;
                }
                bool isFindDev = false;
                MyCamera.MV_CC_DEVICE_INFO devCmr = new MyCamera.MV_CC_DEVICE_INFO(); ;
                for (int i = 0; i < stDevList.nDeviceNum; i++)
                {
                    MyCamera.MV_CC_DEVICE_INFO devInfo = (MyCamera.MV_CC_DEVICE_INFO)Marshal.PtrToStructure(stDevList.pDeviceInfo[i], typeof(MyCamera.MV_CC_DEVICE_INFO));

                    IntPtr gigeBuff = Marshal.UnsafeAddrOfPinnedArrayElement(devInfo.SpecialInfo.stGigEInfo, 0);
                    MyCamera.MV_GIGE_DEVICE_INFO gigeInfo = (MyCamera.MV_GIGE_DEVICE_INFO)Marshal.PtrToStructure(gigeBuff, typeof(MyCamera.MV_GIGE_DEVICE_INFO));

                    //    MyCamera.MV_GIGE_DEVICE_INFO_EX gigeInfo = (MyCamera.MV_GIGE_DEVICE_INFO_EX)MyCamera.ByteToStruct(device.SpecialInfo.stGigEInfo, typeof(MyCamera.MV_GIGE_DEVICE_INFO_EX));


                    if (linkModeParamRange[0].ToString() == linkMode)
                    {
                        uint nIp1 = ((gigeInfo.nCurrentIp & 0xff000000) >> 24);
                        uint nIp2 = ((gigeInfo.nCurrentIp & 0x00ff0000) >> 16);
                        uint nIp3 = ((gigeInfo.nCurrentIp & 0x0000ff00) >> 8);
                        uint nIp4 = (gigeInfo.nCurrentIp & 0x000000ff);
                        string devIP = nIp1 + "." + nIp2 + "." + nIp3 + "." + nIp4;
                        if (devIP == linkParam)
                        {
                            isFindDev = true;
                            devCmr = devInfo;
                            break;
                        }
                    }
                    else if (linkModeParamRange[1].ToString() == linkMode)
                    {
                        // 序列号
                        if (gigeInfo.chSerialNumber == linkParam)
                        {
                            isFindDev = true;
                            devCmr = devInfo;
                            break;
                        }
                    }

                }
                if (!isFindDev)
                {
                    ret = (int)ErrorDef.DevUnExisted;
                    break;
                }
                _hikCmr = new MyCamera();

                int err = _hikCmr.MV_CC_CreateDevice_NET(ref devCmr);
                if (MyCamera.MV_OK != err)
                {
                    ret = (int)ErrorDef.InvokeFailed;
                    break;
                }

                err = _hikCmr.MV_CC_OpenDevice_NET();
                if (MyCamera.MV_OK != err)
                {
                    ret = (int)ErrorDef.InvokeFailed;
                    break;
                }
                MyCamera.MVCC_INTVALUE stVal = new MyCamera.MVCC_INTVALUE();
                int nRet = _hikCmr.MV_CC_GetIntValue_NET("PayloadSize", ref stVal);
                if (MyCamera.MV_OK != nRet)
                    _payloadSize = 0;
                else
                    _payloadSize = (int)stVal.nCurValue;

                IsDeviceOpen = true;

                err = _hikCmr.MV_CC_SetHeartBeatTimeout_NET(1000);//设置相机连接心跳时间，防止异常断开后需要较长时间才可以重新连接
                if (MyCamera.MV_OK != err)
                {
                    ret = (int)ErrorDef.InvokeFailed;
                    break;
                }

                break;
            } while (false);
            return ret;
        }

        /// <summary>
        /// 关闭设备
        /// </summary>
        public int CloseDevice()
        {
            if (!IsDeviceOpen)
                return (int)ErrorDef.Success;
            _hikCmr.MV_CC_StopGrabbing_NET();
            //_hikCmr.MV_CC_ClearImageBuffer_NET();
            _hikCmr.MV_CC_CloseDevice_NET();
            _hikCmr.MV_CC_DestroyDevice_NET();
            IsDeviceOpen = false;
            return (int)ErrorDef.Success;
        }

        /// <summary>
        /// 设备是否已经打开
        /// </summary>
        public bool IsDeviceOpen { get; private set; }
        #endregion//IJFDevice接口


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
        }

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
        #endregion//ErrorInfo接口

        #region 相机接口
        /// <summary>相机是否处于触发模式</summary>
        public int GetTrigMode(out cCmrTrigMode tm)
        {
            tm = cCmrTrigMode.disable;
            if (!IsDeviceOpen)
                return (int)ErrorDef.NotOpen;
            MyCamera.MVCC_ENUMVALUE eval = new MyCamera.MVCC_ENUMVALUE();
            int err = _hikCmr.MV_CC_GetEnumValue_NET("TriggerMode", ref eval);
            //MyCamera.MVCC_INTVALUE val = new MyCamera.MVCC_INTVALUE();
            //int err = _hikCmr.MV_CC_GetIntValue_NET("TriggerMode", ref val);
            if (MyCamera.MV_OK != err)
                return (int)ErrorDef.InvokeFailed;
            if (eval.nCurValue == (uint)MyCamera.MV_CAM_TRIGGER_MODE.MV_TRIGGER_MODE_OFF)
            {
                tm = cCmrTrigMode.disable;
                return (int)ErrorDef.Success;
            }

            err = _hikCmr.MV_CC_GetEnumValue_NET("TriggerSource", ref eval);
            if (MyCamera.MV_OK != err)
                return (int)ErrorDef.InvokeFailed;

            if (eval.nCurValue == (uint)MyCamera.MV_CAM_TRIGGER_SOURCE.MV_TRIGGER_SOURCE_SOFTWARE)
            {
                tm = cCmrTrigMode.software;
                return (int)ErrorDef.Success;
            }
            else if (eval.nCurValue == (uint)MyCamera.MV_CAM_TRIGGER_SOURCE.MV_TRIGGER_SOURCE_LINE0 ||
                eval.nCurValue == (uint)MyCamera.MV_CAM_TRIGGER_SOURCE.MV_TRIGGER_SOURCE_LINE1 ||
                eval.nCurValue == (uint)MyCamera.MV_CAM_TRIGGER_SOURCE.MV_TRIGGER_SOURCE_LINE2 ||
                eval.nCurValue == (uint)MyCamera.MV_CAM_TRIGGER_SOURCE.MV_TRIGGER_SOURCE_LINE3)
            {
                tm = cCmrTrigMode.hardware_line0 + ((int)eval.nCurValue - (int)MyCamera.MV_CAM_TRIGGER_SOURCE.MV_TRIGGER_SOURCE_LINE0);
                return (int)ErrorDef.Success;
            }

            return (int)ErrorDef.TrigModeUnMatch;

        }
        public int SetTrigMode(cCmrTrigMode tm)
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
            int err = 0;
            uint eVal = 0;
            do
            {
                switch (tm)
                {
                    case cCmrTrigMode.disable:
                        err = _hikCmr.MV_CC_SetEnumValue_NET("TriggerMode", (uint)MyCamera.MV_CAM_TRIGGER_MODE.MV_TRIGGER_MODE_OFF);
                        if (err != 0)
                            break;
                        //设置相机内部帧缓存数量
                        err = _hikCmr.MV_CC_SetImageNodeNum_NET(1);
                        if (MyCamera.MV_OK == err)
                            _FrameBuffSize = 1;
                        else
                            _FrameBuffSize = 0;
                        err = _hikCmr.MV_CC_SetGrabStrategy_NET(MV_GRAB_STRATEGY.MV_GrabStrategy_LatestImagesOnly);
                        if (err != MyCamera.MV_OK)
                            break;
                        break;
                    case cCmrTrigMode.software:
                        err = _hikCmr.MV_CC_SetEnumValue_NET("TriggerMode", (uint)MyCamera.MV_CAM_TRIGGER_MODE.MV_TRIGGER_MODE_ON);
                        if (err != MyCamera.MV_OK)
                            break;
                        eVal = (uint)MyCamera.MV_CAM_TRIGGER_SOURCE.MV_TRIGGER_SOURCE_SOFTWARE;
                        err = _hikCmr.MV_CC_SetEnumValue_NET("TriggerSource", eVal);
                        if (err != MyCamera.MV_OK)
                            break;
                        //设置相机内部帧缓存数量
                        err = _hikCmr.MV_CC_SetImageNodeNum_NET(1);
                        if (MyCamera.MV_OK == err)
                            _FrameBuffSize = 1;
                        else
                            _FrameBuffSize = 0;
                        err = _hikCmr.MV_CC_SetGrabStrategy_NET(MV_GRAB_STRATEGY.MV_GrabStrategy_LatestImagesOnly);
                        if (err != MyCamera.MV_OK)
                            break;
                        break;
                    case cCmrTrigMode.hardware_line0:
                    case cCmrTrigMode.hardware_line1:
                    case cCmrTrigMode.hardware_line2:
                    case cCmrTrigMode.hardware_line3:
                        err = _hikCmr.MV_CC_SetEnumValue_NET("TriggerMode", (uint)MyCamera.MV_CAM_TRIGGER_MODE.MV_TRIGGER_MODE_ON);
                        if (err != MyCamera.MV_OK)
                            break;
                        eVal = (uint)(MyCamera.MV_CAM_TRIGGER_SOURCE.MV_TRIGGER_SOURCE_LINE0 + ((int)tm - (int)cCmrTrigMode.hardware_line0));
                        err = _hikCmr.MV_CC_SetEnumValue_NET("TriggerSource", eVal);
                        if (err != MyCamera.MV_OK)
                            break;
                        //设置相机内部帧缓存数量
                        err = _hikCmr.MV_CC_SetImageNodeNum_NET(1);
                        if (MyCamera.MV_OK == err)
                            _FrameBuffSize = 1;
                        else
                            _FrameBuffSize = 0;
                        break;
                    default:
                        err = (int)ErrorDef.TrigModeUnMatch;
                        break;
                }
            } while (false);
            if (err == MyCamera.MV_OK)
                return (int)ErrorDef.Success;
            return (int)ErrorDef.InvokeFailed;
        }


        /// <summary>X方向镜像</summary>
        public int GetReverseX(out bool enabled)
        {
            enabled = false;
            if (!IsDeviceOpen)
                return (int)ErrorDef.NotOpen;

            bool bValue = false;
            int nRet = _hikCmr.MV_CC_GetBoolValue_NET("ReverseX", ref bValue);
            if (MyCamera.MV_OK != nRet)
                return (int)ErrorDef.InvokeFailed;
            enabled = bValue;
            return (int)ErrorDef.Success;
        }
        public int SetReverseX(bool enabled)//暂不实现
        {
            if (!IsDeviceOpen)
                return (int)ErrorDef.NotOpen;

            int nRet = _hikCmr.MV_CC_SetBoolValue_NET("ReverseX", enabled);
            if (MyCamera.MV_OK != nRet)
                return (int)ErrorDef.InvokeFailed;
            return (int)ErrorDef.Success;
        }
        /// <summary>Y方向镜像</summary>
        public int GetReverseY(out bool enabled)//暂不实现 
        {
            enabled = false;
            if (!IsDeviceOpen)
                return (int)ErrorDef.NotOpen;

            bool bValue = false;
            int nRet = _hikCmr.MV_CC_GetBoolValue_NET("ReverseY", ref bValue);
            if (MyCamera.MV_OK != nRet)
                return (int)ErrorDef.InvokeFailed;
            enabled = bValue;
            return (int)ErrorDef.Success;
        }
        public int SetReverseY(bool enabled) //暂不实现
        {
            if (!IsDeviceOpen)
                return (int)ErrorDef.NotOpen;

            int nRet = _hikCmr.MV_CC_SetBoolValue_NET("ReverseY", enabled);
            if (MyCamera.MV_OK != nRet)
                return (int)ErrorDef.InvokeFailed;
            return (int)ErrorDef.Success;
        }

        /// <summary>设置相机增益参数</summary>
        public int SetGain(double value)
        {
            if (!IsDeviceOpen)
                return (int)ErrorDef.NotOpen;
            MyCamera.MVCC_FLOATVALUE stParam = new MyCamera.MVCC_FLOATVALUE();
            int nRet = _hikCmr.MV_CC_GetFloatValue_NET("Gain", ref stParam);
            if (MyCamera.MV_OK != nRet)
                return (int)ErrorDef.InvokeFailed;
            if (value < stParam.fMin || value > stParam.fMax)
                return (int)ErrorDef.ParamError;
            int err = _hikCmr.MV_CC_SetFloatValue_NET("Gain", (float)value);
            if (MyCamera.MV_OK != err)
                return (int)ErrorDef.InvokeFailed;
            return (int)ErrorDef.Success;
        }
        /// <summary>获取相机增益参数</summary>
        public int GetGain(out double value)
        {
            value = 0;
            if (!IsDeviceOpen)
                return (int)ErrorDef.NotOpen;
            MyCamera.MVCC_FLOATVALUE stParam = new MyCamera.MVCC_FLOATVALUE();
            int nRet = _hikCmr.MV_CC_GetFloatValue_NET("Gain", ref stParam);
            if (MyCamera.MV_OK != nRet)
                return (int)ErrorDef.InvokeFailed;
            value = (double)stParam.fCurValue;
            return (int)ErrorDef.Success;
        }

        /// <summary>设置相机曝光时间 </summary>
        public int SetExposureTime(double microSeconds)
        {
            if (!IsDeviceOpen)
                return (int)ErrorDef.NotOpen;
            MyCamera.MVCC_FLOATVALUE stParam = new MyCamera.MVCC_FLOATVALUE();
            int nRet = _hikCmr.MV_CC_GetFloatValue_NET("ExposureTime", ref stParam);
            if (MyCamera.MV_OK != nRet)
                return (int)ErrorDef.InvokeFailed;
            if (microSeconds < stParam.fMin || microSeconds > stParam.fMax)
                return (int)ErrorDef.ParamError;
            int err = _hikCmr.MV_CC_SetFloatValue_NET("ExposureTime", (float)microSeconds);
            if (MyCamera.MV_OK != err)
                return (int)ErrorDef.InvokeFailed;
            return (int)ErrorDef.Success;
        }
        /// <summary>获取相机曝光时间</summary>
        public int GetExposureTime(out double microSeconds)
        {
            microSeconds = 0;
            if (!IsDeviceOpen)
                return (int)ErrorDef.NotOpen;
            MyCamera.MVCC_FLOATVALUE stParam = new MyCamera.MVCC_FLOATVALUE();
            int nRet = _hikCmr.MV_CC_GetFloatValue_NET("ExposureTime", ref stParam);
            if (MyCamera.MV_OK != nRet)
                return (int)ErrorDef.InvokeFailed;
            microSeconds = (double)stParam.fCurValue;
            return (int)ErrorDef.Success;
        }

        int _FrameBuffSize = 0;
        /// <summary>内部图片缓存的最大数量</summary>
        public int GetBuffSize(out int maxNum)
        {
            maxNum = 0;
            if (!IsDeviceOpen)
                return (int)ErrorDef.NotOpen;
            maxNum = _FrameBuffSize;
            return (int)ErrorDef.Success;


        }
        public int SetBuffSize(int maxNum)
        {
            if (maxNum < 1 || maxNum > 30)
                return (int)ErrorDef.ParamError;
            if (!IsDeviceOpen)
                return (int)ErrorDef.NotOpen;
            if (IsGrabbing)
                return (int)ErrorDef.Grabbing;

            int err = _hikCmr.MV_CC_SetImageNodeNum_NET((uint)maxNum);
            if (MyCamera.MV_OK != err)
                return (int)ErrorDef.InvokeFailed;
            _FrameBuffSize = maxNum;
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
            //防止重复注册事件
            if (null != AcqFrameEvent)
            {
                Delegate[] delegates = AcqFrameEvent.GetInvocationList();
                for (int i = 0; i < delegates.Length; i++)
                    if (delegates[i].GetMethodInfo().Name == callback.GetMethodInfo().Name)
                        return (int)ErrorDef.Success;
            }
            AcqFrameEvent += callback;
            return (int)ErrorDef.Success;
        }
        public void RemoveAcqFrameCallback(CmrAcqFrameDelegate callback)
        {
            AcqFrameEvent -= callback;
        }
        public void ClearAcqFrameCallback()
        {
            AcqFrameEvent = null;
        }

        public bool IsGrabbing { get; private set; }

        cbOutputExdelegate dgHikFrameCallback = new cbOutputExdelegate(HikFrameCallback);

        /// <summary>
        /// 用于向Hik相机句柄注册的回调函数,必须
        /// </summary>
        /// <param name="pData"></param>
        /// <param name="pFrameInfo"></param>
        /// <param name="pUser"></param>
        static void HikFrameCallback(IntPtr pData, ref MV_FRAME_OUT_INFO_EX pFrameInfo, IntPtr pUser)
        {
            GCHandle hd = GCHandle.FromIntPtr(pUser);
            IPlatCamera_HikGige cmr = hd.Target as IPlatCamera_HikGige;
            cmr._HikFrameCallback(pData, ref pFrameInfo);
        }


        void _HikFrameCallback(IntPtr pData, ref MV_FRAME_OUT_INFO_EX pFrameInfo)
        {
            MVCC_INTVALUE stVal = new MVCC_INTVALUE();
            int err = _hikCmr.MV_CC_GetIntValue_NET("PayloadSize", ref stVal);
            if (MyCamera.MV_OK != err)
                throw new Exception("HikFrameCallback failed bt MV_CC_GetIntValue_NET(\"PayloadSize\") return errorCode = " + err);
            _payloadSize = (int)stVal.nCurValue;
            byte[] bytes = new byte[_payloadSize];
            Marshal.Copy(pData, bytes, 0, _payloadSize);
            IPlatImage_Hik img = new IPlatImage_Hik(bytes, pFrameInfo, _hikCmr);
            if (null != AcqFrameEvent)
            {
                AcqFrameEvent(this, img);
            }
        }



        /// <summary>开始图像采集</summary>
        /// <returns></returns>
        public int StartGrab()
        {
            if (!IsDeviceOpen)
                return (int)ErrorDef.NotOpen;
            if (IsGrabbing)
                return (int)ErrorDef.Success;
            int err = 0;
            if (null != AcqFrameEvent)
            {
                GCHandle handleThis = GCHandle.Alloc(this);
                IntPtr ptr = GCHandle.ToIntPtr(handleThis);

                err = _hikCmr.MV_CC_RegisterImageCallBackEx_NET(dgHikFrameCallback, ptr);
                if (err != MyCamera.MV_OK)
                    return (int)ErrorDef.RegistCBFailed;

            }

            err = _hikCmr.MV_CC_StartGrabbing_NET();
            if (err != MyCamera.MV_OK)
                return (int)ErrorDef.InvokeFailed;

            IsGrabbing = true;
            return (int)ErrorDef.Success;
        }
        /// <summary>停止图像采集</summary>
        public int StopGrab()
        {
            if (!IsDeviceOpen || !IsGrabbing)
                return (int)ErrorDef.Success;
            int err = _hikCmr.MV_CC_StopGrabbing_NET();
            if (err != MyCamera.MV_OK)
                return (int)ErrorDef.InvokeFailed;
            IsGrabbing = false;
            return (int)ErrorDef.Success;

        }


        /// <summary>实时抓拍一张图片</summary>
        public int GrabOne(out IPlat_Image img, int timeoutMilSeconds = -1)
        {

            img = null;
            if (!IsDeviceOpen)
                return (int)ErrorDef.NotOpen;
            if (!IsGrabbing)
                return (int)ErrorDef.NotGrabbing;

            //JFCmrTrigMode currTM = JFCmrTrigMode.disable;
            //GetTrigMode(out currTM);
            //if (currTM != JFCmrTrigMode.disable)
            //    return (int)ErrorDef.TrigModeUnMatch;

            int err = 0;

            //err = _hikCmr.MV_CC_ClearImageBuffer_NET();
            //if (MyCamera.MV_OK != err)
            //    return (int)ErrorDef.InvokeFailed;

            if (_payloadSize <= 0)
            {
                MyCamera.MVCC_INTVALUE stVal = new MyCamera.MVCC_INTVALUE();
                err = _hikCmr.MV_CC_GetIntValue_NET("PayloadSize", ref stVal);
                if (MyCamera.MV_OK != err)
                    return (int)ErrorDef.InvokeFailed;
                else
                    _payloadSize = (int)stVal.nCurValue;
            }

            byte[] dataBytes = new byte[_payloadSize];
            IntPtr ptr = Marshal.UnsafeAddrOfPinnedArrayElement(dataBytes, 0);
            MyCamera.MV_FRAME_OUT_INFO_EX frameInfo = new MyCamera.MV_FRAME_OUT_INFO_EX();
            err = _hikCmr.MV_CC_GetOneFrameTimeout_NET(ptr, (uint)_payloadSize, ref frameInfo, timeoutMilSeconds);
            if (err != MyCamera.MV_OK)
                return (int)ErrorDef.InvokeFailed;
            img = new IPlatImage_Hik(dataBytes, frameInfo, _hikCmr);
            return (int)ErrorDef.Success;
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

        /// <summary>软触发一次使相机拍照</summary>
        public int SoftwareTrig()
        {
            if (!IsDeviceOpen)
                return (int)ErrorDef.NotOpen;
            cCmrTrigMode tm;
            GetTrigMode(out tm);
            if (tm != cCmrTrigMode.software)
                return (int)ErrorDef.TrigModeUnMatch;
            int err = _hikCmr.MV_CC_SetCommandValue_NET("TriggerSoftware");
            if (MyCamera.MV_OK != err)
                return (int)ErrorDef.InvokeFailed;
            return (int)ErrorDef.Success;

        }

        public int SetBalanceAuto(bool enable)//关闭自动白平衡
        {
            try
            {
                if (enable)
                    // _hikCmr.MV_CC_SetBalanceWhiteAuto_NET((uint)MyCamera.MV_CAM_BALANCEWHITE_AUTO.MV_BALANCEWHITE_AUTO_CONTINUOUS):
                    _hikCmr.MV_CC_SetEnumValue_NET("BalanceWhite", (uint)MyCamera.MV_CAM_BALANCEWHITE_AUTO.MV_BALANCEWHITE_AUTO_CONTINUOUS);
                else
                    //  // _hikCmr.MV_CC_SetBalanceWhiteAuto_NET((uint)MyCamera.MV_CAM_BALANCEWHITE_AUTO.MV_BALANCEWHITE_AUTO_OFF):
                    _hikCmr.MV_CC_SetEnumValue_NET("BalanceWhite", (uint)MyCamera.MV_CAM_BALANCEWHITE_AUTO.MV_BALANCEWHITE_AUTO_OFF);
            }
            catch
            {
                return (int)ErrorDef.InvokeFailed;
            }
            return (int)ErrorDef.Success;
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
