using Cell.DataModel;
using Cell.Interface;
using Cell.Tools;
using Lmi3d.GoSdk;
using Lmi3d.GoSdk.Messages;
using Lmi3d.Zen;
using Lmi3d.Zen.Io;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Org.ILineScan
{
    [MyVersion("1.0.0.0")]
    [MyDisplayName("LMI 25系列")]
    public class IPlatLineScan_Lmi25x : IPlatLineScan_Base
    {
        string _cmrIP = null; // 输入参数 相机IP

        static GoSystem system;
        static GoSensor sensor;

        //  static double xResolution;
        static double zResolution;
        //  static double xOffset;
        static double zOffset;

        /// <summary>
        /// 接受回调的 轮廓数据
        /// </summary>
        public static List<short[]> ProfileData = new List<short[]>();

        internal IPlatLineScan_Lmi25x()
        {
            IsInitOK = false;
        }
        ~IPlatLineScan_Lmi25x()
        {
            Dispose(false);
        }

        #region IPlatInitializable 接口
        /// <summary>获取初始化需要的所有参数的名称 </summary>
        public override string[] InitParamNames { get { return new string[] { "IP" }; } }

        /// <summary>
        /// 获取指定名称的初始化参数的信息
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public override CosParams GetInitParamDescribe(string name)
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
        public override object GetInitParamValue(string name)
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
        public override bool SetInitParamValue(string name, object value)
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
        public override bool Initialize()
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


        #endregion//Initor接口

        #region InterfaceDevice 接口
        public override string DeviceModel { get { return "LMI 25系列"; } }

        /// <summary>
        /// 打开设备
        /// </summary>
        public override int OpenDevice()
        {
            if (!IsInitOK)
            {
                return (int)ErrorDef.InitFailedWhenOpen;
            }

            if (IsDeviceOpen)
                return (int)ErrorDef.Success;

            try
            {

                KApiLib.Construct();
                GoSdkLib.Construct();
                system = new GoSystem();

                KIpAddress ipAddress = KIpAddress.Parse(_cmrIP);
                sensor = system.FindSensorByIpAddress(ipAddress);

                sensor.Connect();
                GoSetup setup = sensor.Setup;
                system.EnableData(true);
                system.SetDataHandler(onData);

                IsDeviceOpen = true;
            }
            catch
            {
                return (int)ErrorDef.InvokeFailed;
            }

            return (int)ErrorDef.Success;
        }

        /// <summary>
        /// 关闭设备
        /// </summary>
        public override int CloseDevice()
        {
            try
            {
                if (!IsDeviceOpen)
                    return (int)ErrorDef.Success;
                sensor.Disconnect();
            }
            catch
            {
                return (int)ErrorDef.InvokeFailed;
            }
            IsDeviceOpen = false;

            return (int)ErrorDef.Success;
        }

        private static void onData(KObject data)
        {
            GoDataSet dataSet = (GoDataSet)data;
            for (UInt32 i = 0; i < dataSet.Count; i++)
            {
                GoDataMsg dataObj = (GoDataMsg)dataSet.Get(i);
                switch (dataObj.MessageType)
                {
                    case GoDataMessageType.Stamp:
                        {
                            GoStampMsg stampMsg = (GoStampMsg)dataObj;
                            for (UInt32 j = 0; j < stampMsg.Count; j++)
                            {
                                GoStamp stamp = stampMsg.Get(j);
                            }
                        }
                        break;
                    //主要接受profile
                    case GoDataMessageType.UniformProfile:
                        {
                            GoUniformProfileMsg profileMsg = (GoUniformProfileMsg)dataObj;
                            for (UInt32 k = 0; k < profileMsg.Count; ++k)
                            {
                                int profilePointCount = profileMsg.Width;
                                // xResolution = (double)profileMsg.XResolution / 1000000;
                                zResolution = (double)profileMsg.ZResolution / 1000000;
                                // xOffset = (double)profileMsg.XOffset / 1000;
                                zOffset = (double)profileMsg.ZOffset / 1000;

                                short[] points = new short[profilePointCount];
                                ProfilePoint[] profileBuffer = new ProfilePoint[profilePointCount];
                                IntPtr pointsPtr = profileMsg.Data;
                                Marshal.Copy(pointsPtr, points, 0, points.Length);
                                ProfileData.Add(points);    //profile增加到list列表中

                            }
                        }
                        break;

                    case GoDataMessageType.UniformSurface:
                        {
                            GoUniformSurfaceMsg surfaceMsg = (GoUniformSurfaceMsg)dataObj;
                            int width = (int)surfaceMsg.Width;
                            int length = (int)surfaceMsg.Length;
                            long bufferSize = width * length;
                            IntPtr bufferPointer = surfaceMsg.Data;


                            short[] ranges = new short[bufferSize];
                            Marshal.Copy(bufferPointer, ranges, 0, ranges.Length);
                            short[] aLine = new short[width];
                            for (int j = 0; j < length; j++)
                            {
                                Buffer.BlockCopy(ranges, j * width * sizeof(short), aLine, 0, width * sizeof(short));
                                ProfileData.Add(aLine);
                            }
                        }
                        break;
                }
            }

            // Refer to example ReceiveRange, ReceiveProfile, ReceiveMeasurement and ReceiveWholePart on how to receive data
        }

        struct ProfilePoint
        {
            public double x;
            public double z;
            byte intensity;
        }
        #endregion

        #region IPlatLineScan 接口

        /// <summary>设置传感器job</summary>
        public override int GetSeneorJob(out string job)
        {
            job = "";
            try
            {
                job = sensor.DefaultJob;
            }
            catch
            {
                return (int)ErrorDef.InvokeFailed; //显示传感器内部Job
            }

            return (int)ErrorDef.Success; //显示传感器内部Job
        }

        public override int SetSeneorJob(string tm)
        {
            try
            {
                sensor.CopyFile(tm, "_live.job");
                sensor.DefaultJob = tm;
            }
            catch
            {
                return (int)ErrorDef.InvokeFailed; //显示传感器内部Job
            }

            return (int)ErrorDef.Success; //显示传感器内部Job
        }

        /// <summary>开始图像采集</summary>
        /// <returns></returns>
        public override int StartGrab()
        {
            if (!IsDeviceOpen)
                return (int)ErrorDef.NotOpen;
            if (IsGrabbing)
                return (int)ErrorDef.Success;
            try
            {
                ProfileData.Clear();
                Thread.Sleep(10);
                system.Start();
            }
            catch
            {
                return (int)ErrorDef.InvokeFailed;
            }

            IsGrabbing = true;
            return (int)ErrorDef.Success;
        }
        /// <summary>停止图像采集</summary>
        public override int StopGrab()
        {
            if (!IsDeviceOpen || !IsGrabbing)
                return (int)ErrorDef.Success;
            try
            {
                system.Stop();
                system.ClearData();
            }
            catch
            {
                return (int)ErrorDef.InvokeFailed;
            }
            IsGrabbing = false;
            return (int)ErrorDef.Success;
        }

        public override int GetOneImg(out IPlat_Image img, int timeoutMilSeconds = -1)
        {
            img = null;

            int row = ProfileData[0].Length;
            int col = ProfileData.Count;
            if (row == 0 || col == 0)
            {
                return (int)ErrorDef.NotGrabbing;
            }

            double[] buffer = new double[col * row];

            for (int i = 0; i < col; i++)     //行数据
            {
                int ProfileIndex = 0;
                foreach (var points in ProfileData[i])     //列数据
                {
                    if (points != -32768)
                    {
                        buffer[i * row + ProfileIndex] = points * zResolution + zOffset;   // 10000.0;   //数据换算zResolution+zOffset;
                    }
                    else
                    {
                        buffer[i * row + ProfileIndex] = -10;   //无效位置赋值
                    }
                    ProfileIndex++;
                }
            }

            img = new IPlatImage_Lmi25x(buffer, row, col, IPlatImgPixFormat.Mono8);

            return (int)ErrorDef.Success;
        }
        #endregion

        public override void Dispose()
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
                sensor?.Dispose();
                system?.Dispose();
            }
        }
    }
}
