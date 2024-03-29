using Cell.DataModel;
using Cell.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Org.ILineScan
{
    public abstract class IPlatLineScan_Base : IPlatDevice_LineScan, IPlatRealtimeUIProvider
    {
        #region ErrorInfo接口
        public enum ErrorDef
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

        #region IPlatInitializable 接口
        /// <summary>获取初始化需要的所有参数的名称 </summary>
        public abstract string[] InitParamNames { get; }


        internal void _CheckInitName(string initName, string funcName)
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
        public abstract CosParams GetInitParamDescribe(string name);

        /// <summary>
        /// 获取指定名称的初始化参数的当前值
        /// </summary>
        /// <param name="name">参数名称，如果参数名称不在InitParamNames中，将会抛出一个ArgumentException异常</param>
        /// <returns>参数值</returns>
        public abstract object GetInitParamValue(string name);


        string _initErrorInfo = "NO-OPS";

        /// <summary>
        ///设置取指定名称的初始化参数的当前值
        /// </summary>
        /// <param name="name">参数名称，如果参数名称不在InitParamNames中，将会抛出一个ArgumentException异常</param>
        /// <param name="value">参数值</param>
        /// <returns>操作成功返回True，失败返回false，可通过GetInitErrorInfo()获取错误信息</returns>
        public abstract bool SetInitParamValue(string name, object value);

        /// <summary>
        /// 对象初始化
        /// </summary>
        /// <returns>操作成功返回True，失败返回false，可通过GetInitErrorInfo()获取错误信息</returns>
        public abstract bool Initialize();



        /// <summary>获取初始化状态，如果对象已初始化成功，返回True</summary>
        public bool IsInitOK { get; internal set; }

        /// <summary>获取初始化错误的描述信息</summary>
        public string GetInitErrorInfo() { return _initErrorInfo; }
        #endregion//Initor接口

        #region  IPlatDevice'APIs

        public abstract string DeviceModel { get; }

        /// <summary>
        /// 设备状态
        /// </summary>
        public string DeviceStatus { get; }
        /// <summary>
        /// 打开设备
        /// </summary>
        public abstract int OpenDevice();

        /// <summary>
        /// 关闭设备
        /// </summary>
        public abstract int CloseDevice();

        /// <summary>
        /// 设备是否已经打开
        /// </summary>
        public bool IsDeviceOpen { get;  set; }

        #endregion

        #region IPlatLineScan

        public  bool IsGrabbing { get; set; }

        /// <summary>设置传感器job</summary>
        public abstract int GetSeneorJob(out string job);
        public abstract int SetSeneorJob(string tm);

        /// <summary>开始图像采集</summary>
        /// <returns></returns>
        public abstract int StartGrab();
        /// <summary>停止图像采集</summary>
        public abstract int StopGrab();

        public abstract int GetOneImg(out IPlat_Image img, int timeoutMilSeconds = -1);
        #endregion

        /// <summary>实时调试界面接口</summary>
        public UcRealTimeUI GetRealtimeUI()
        {
            Uc_RealTimeLineScan ui = new Uc_RealTimeLineScan();
            ui.SetLineScan(this);
            return ui;
        }


        public abstract void Dispose();
    }
}
