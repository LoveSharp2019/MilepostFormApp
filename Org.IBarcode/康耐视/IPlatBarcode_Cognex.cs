using Cell.DataModel;
using Cell.Interface;
using Cell.Tools;
using Cell.UI;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Org.IBarcode
{
    [MyVersion("1.0.0.0")]
    [MyDisplayName("康耐视读码器")]
    public class IPlatBarcode_Cognex : IPlatDevice_Barcode, IPlatRealtimeUIProvider
    {
        private Socket _clientSocket;
        int _timeoutMS = 1000; //通讯超时（单位：毫秒）
        string _ip = null;
        int _port = 0;
        public event ucBarcodeDelegate ScanCallBack;



        enum ResultCode
        {
            Success = 0,
            未初始化,
            设备未打开,
            串口名称不存在,
            串口打开失败,
            Tcp连接失败,
            Tcp连接断开,
            通讯超时,
            设备返回内容错误,
            通讯异常,
            参数错误,
            工作模式错误,
            启动监听任务失败,
            写数据失败,
            读数据失败,
            读数据超时,
            设备扫码失败,
            [Description("设备不支持此功能")]
            设备不支持此功能//
        }

        #region InterfaceInitializable 接口
        /// <summary>获取初始化需要的所有参数的名称 </summary>
        public string[] InitParamNames { get { return new string[] { "IP", "Port", "通讯超时(毫秒)" }; } }

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
                return CosParams.Create(name, typeof(string), cValueLimit.Non, null, false,"Tcp通讯 ip");
            else if (name == "Port")
                return CosParams.Create(name, typeof(int), cValueLimit.Range, new object[] { 600, 1200, 2400, 4800, 9600, 19200, 38400 }, true, "Tcp通讯 port");
            else if (name == "通讯超时(毫秒)")
                return CosParams.Create(name, typeof(int), cValueLimit.Non, null, false, "Tcp通讯 TimeOut");
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
            if (name == "IP")
                return _ip;
            else if (name == "Port")
                return _port;
            else if (name == "通讯超时(毫秒)")
                return _timeoutMS;
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
                string ip = value as string;
                if (!ToolsFun.IsIPAddress(ip))
                {
                    _initErrorInfo = "ip = " + ip + "不是有效的IP地址";
                    return false;
                }
                _ip = ip;
            }
            else if (name == "Port")
                _port = (int)value;
            else if (name == "通讯超时(毫秒)")
                _timeoutMS = (int)value;
            else
                throw new ArgumentOutOfRangeException("name=" + name + " is unknow");
            _initErrorInfo = "Success";
            return true;
        }

        /// <summary>
        /// 对象初始化
        /// </summary>
        /// <returns>操作成功返回True，失败返回false，可通过GetInitErrorInfo()获取错误信息</returns>
        public bool Initialize()
        {
            if (!ToolsFun.IsIPAddress(_ip))
            {
                _initErrorInfo = "IP地址无效:" + _ip;
                IsInitOK = false;
                return false;
            }
            if (_port == 0)
            {
                _initErrorInfo = "无效的Tcp端口:" + _port;
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
        #endregion

        #region InterfaceDevice 接口
        public string DeviceModel { get { return "康耐视 扫码器"; } }

        // 目前暂未使用
        public string DeviceStatus { get { return "不支持此功能"; } }

        /// <summary>
        /// 打开设备
        /// </summary>
        public int OpenDevice()
        {
            if (!IsInitOK)
                return (int)ResultCode.未初始化;
            if (IsDeviceOpen)
                return (int)ResultCode.Success;
            try
            {
                IPEndPoint ip = new IPEndPoint(IPAddress.Parse(_ip), _port);
                _clientSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                IAsyncResult asyncResult = _clientSocket.BeginConnect(ip, new AsyncCallback(ConnectCallBack), _clientSocket);
                _clientSocket.Connect(_ip, _port);

                if (!_clientSocket.Connected)
                    return (int)ResultCode.Tcp连接失败;

                _clientSocket.ReceiveTimeout = _timeoutMS;
                _clientSocket.SendTimeout = _timeoutMS;
            }
            catch
            {
                return (int)ResultCode.Tcp连接失败;
            }

            IsDeviceOpen = true;
            return (int)ResultCode.Success;
        }

        private void ConnectCallBack(IAsyncResult iar)
        {
            try
            {
                Socket client = (Socket)iar.AsyncState;

                client.EndConnect(iar);
                StateObject state = new StateObject() { workSocket = client };
                client.BeginReceive(state.buffer, 0, StateObject.BufferSize, 0,
                    new AsyncCallback(ReceiveCallback), state);
            }
            catch
            {

            }

        }

        private void ReceiveCallback(IAsyncResult ar)
        {
            // Retrieve the state object and the client socket 
            // from the asynchronous state object.
            StateObject state = (StateObject)ar.AsyncState;
            Socket client = state.workSocket;
            try
            {
                // Read data from the remote device.
                int bytesLen = client.EndReceive(ar);
                // There might be more data, so store the data received so far.
                byte[] Recdata = state.buffer.Skip(0).Take(bytesLen).ToArray();
                // Get the rest of the data.
                state = new StateObject() { workSocket = client };
                client.BeginReceive(state.buffer, 0, StateObject.BufferSize, 0,
                    new AsyncCallback(ReceiveCallback), state);

                string SN = System.Text.Encoding.UTF8.GetString(Recdata).Split('\r')[0];

                string str0_On = "||>set output.action 0 0\r\n";//0路打开
                string str0_Off = "||>set output.action 0 1\r\n";//0路关闭

                ScanCallBack?.Invoke(this, 0, SN);

                client.Send(System.Text.Encoding.UTF8.GetBytes(str0_On)); //0路打开
                System.Threading.Thread.Sleep(500);
                client.Send(System.Text.Encoding.UTF8.GetBytes(str0_Off)); //0路打开

            }
            catch (Exception e)
            {
                try
                {
                    state = new StateObject() { workSocket = client };
                    client.BeginReceive(state.buffer, 0, StateObject.BufferSize, 0,
                   new AsyncCallback(ReceiveCallback), state);
                }
                catch
                { }
            }
        }


        /// <summary>
        /// 关闭设备
        /// </summary>
        public int CloseDevice()
        {
            if (!IsDeviceOpen)
                return (int)ResultCode.Success;
            _clientSocket?.Close();
            _clientSocket?.Dispose();
            _clientSocket = null;

            IsDeviceOpen = false;
            return (int)ResultCode.Success;
        }

        /// <summary>
        /// 设备是否已经打开
        /// </summary>
        public bool IsDeviceOpen { get; private set; }
        #endregion//IJFDevice接口

        public string GetErrorInfo(int errorCode)
        {
            if ((Enum.GetValues(typeof(ResultCode)) as int[]).Contains(errorCode))
                return ((ResultCode)errorCode).ToString();
            return "Undefined ErrorCode:" + errorCode;
        }

        cBarcodeSanMode _wm = cBarcodeSanMode.Passive;

        public cBarcodeSanMode GetWorkMode()
        {
            return _wm;
        }

        public int SetWorkMode(cBarcodeSanMode mode)
        {
            return (int)ResultCode.设备不支持此功能;
        }

        public int ClearBuff()
        {
            // strCurrentSN = string.Empty;
            return (int)ResultCode.设备不支持此功能;
        }

        /// <summary>
        /// 向设备发送扫码指令
        /// </summary>
        /// <param name="barcode"></param>
        /// <returns></returns>
        public int Scan(out string barcode)
        {
            barcode = string.Empty;
            return (int)ResultCode.设备不支持此功能;
        }

        private bool disposedValue;
        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    // TODO: 释放托管状态(托管对象)
                    CloseDevice();
                }

                // TODO: 释放未托管的资源(未托管的对象)并替代终结器
                // TODO: 将大型字段设置为 null
                disposedValue = true;
            }
        }

        public void Dispose()
        {
            // 不要更改此代码。请将清理代码放入“Dispose(bool disposing)”方法中
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }

        public UcRealTimeUI GetRealtimeUI()
        {
            UcBarcodeScan ui = new UcBarcodeScan();
            ui.SetDevice(this);
            return ui;
        }
    }
}
