using Cell.DataModel;
using Cell.Interface;
using Cell.Tools;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Org.IMotionDaq
{
    [MyVersion("1.0.0.0")]
    [MyDisplayName("固高GTS-VB系列")]
    public class MotionDaq_Gugao : IPlatDevice_MotionDaq
    {

        public string DeviceStatus { get { return "不支持此功能"; } }
        /// <summary>
        /// 景焱运动控制卡类
        /// </summary>
        /// <param name="cfgFile">配置文件(paras.db)路径</param>
        /// <param name="axisCount">轴数量</param>
        /// <param name="ioCount">DIO数量</param>
        /// <param name="mode">模式 1-脱机模式，0-在线模式(初始化板卡+配置)，2-仅初始化板卡不配置参数 </param>
        internal MotionDaq_Gugao()
        {
            IsInitOK = false;
            CreateInitParamDescribes();
            McMCount = 1;
            DioMCount = 1;
            AioMCount = 1;
        }

        public void Dispose()
        {
            Dispose(true);
            System.GC.SuppressFinalize(this);
        }

        void Dispose(bool disposing)
        {
            ////////////释放非托管资源
            if (disposing)//////////////释放其他托管资源
            {
                CloseDevice();
                dio = null;
                mc = null;
            }
        }

        ~MotionDaq_Gugao()
        {
            Dispose(false);
        }


        public string[] InitParamNames
        {
            get
            {
                if (null == initParamDescribes)
                    return null;
                return initParamDescribes.Keys.ToArray();
            }
        }

        /// <summary>
        /// 配置文件
        /// </summary>
        string config_file = null;
        /// <summary>
        /// 板卡拨号
        /// </summary>
        int use_card_no = 0;
        /// <summary>
        /// 板卡拥有的轴数
        /// </summary>
        int use_axis_num = 0;

        #region  init api's
        /// 初始化参数描述信息
        SortedDictionary<string, CosParams> initParamDescribes = null;

        /// <summary>
        ///  创建 Init 参数
        /// </summary>
        void CreateInitParamDescribes()
        {
            if (null == initParamDescribes)
                initParamDescribes = new SortedDictionary<string, CosParams>();
            initParamDescribes.Clear();

            object[] minValueZero = new object[] { 0 };
            CosParams pd = CosParams.Create("板卡拨号", typeof(int), cValueLimit.Min, minValueZero, false);
            initParamDescribes.Add("板卡拨号", pd);
            pd = CosParams.Create("配置文件", typeof(string), cValueLimit.File, null, false, "固高配置文件");
            initParamDescribes.Add("配置文件", pd);
            pd = CosParams.Create("板卡轴数", typeof(int), cValueLimit.Range, new object[] { 4, 8 }, false, "当前板卡轴数量");
            initParamDescribes.Add("板卡轴数", pd);
        }

        public bool IsInitOK { get; private set; }

        /// <summary>
        /// 检测参数名称
        /// </summary>
        /// <param name="initName">参数名</param>
        /// <param name="funcName">方法名 自定义</param>
        void _CheckInitName(string initName, string funcName)
        {
            if (null == initName)
                throw new ArgumentNullException(string.Format("{0} failed By: name = null! ", funcName));
            if (!InitParamNames.Contains(initName))
                throw new ArgumentException(string.Format("{0} failed By: name = {1} is not included by InitParamNames:{2}", funcName, initName, string.Join("|", InitParamNames)));
        }

        /// <summary>
        /// 获取参数类型结构
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public CosParams GetInitParamDescribe(string name)
        {
            _CheckInitName(name, "GetInitParamDescribe(name)");
            return initParamDescribes[name];
        }

        /// <summary>
        /// 获取指定名称的初始化参数的当前值
        /// </summary>
        /// <param name="name">参数名称，如果参数名称不在InitParamNames中，将会抛出一个ArgumentException异常</param>
        /// <returns>参数值</returns>
        public object GetInitParamValue(string name)
        {
            _CheckInitName(name, "GetInitParamDescribe(name)");
            if (name == "配置文件")
                return config_file;
            else if (name == "板卡拨号")
                return use_card_no;
            else if (name == "板卡轴数")
                return use_axis_num;
            else
                throw new Exception("GetInitParamValue(name) failed By: name = " + name);
        }

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

            if (name == "配置文件")
            {
                string tmp = value as string;
                if (!File.Exists(tmp))
                {
                    _initErrorInfo = string.Format("设置初始化参数\"配置文件\"失败，文件{0}不存在！", tmp);
                    config_file = null;
                    return false;
                }
                config_file = tmp;
                _initErrorInfo = "Success";
                return true;
            }
            else if (name == "板卡拨号")
            {
                int tmp = (int)value;
                if (tmp < 0)
                {
                    _initErrorInfo = "板卡拨号不能为负数";
                    use_card_no = 0;
                    return false;
                }
                use_card_no = tmp;
                _initErrorInfo = "Success";
                return true;
            }
            else if (name == "板卡轴数")
            {
                int tmp = (int)value;
                if (tmp < 0)
                {
                    _initErrorInfo = "板卡拨号不能为负数";
                    use_axis_num = 0;
                    return false;
                }
                use_axis_num = tmp;
                _initErrorInfo = "Success";
                return true;
            }
            else
                throw new Exception("GetInitParamValue(name) failed By: name = " + name);
        }

        #endregion

        public AppCfgFromXml mdCfg = new AppCfgFromXml();

        /// <summary>
        /// 初始化动作，并不打开卡，只是检查参数的合法性，打开卡的动作在Open（）函数中
        /// </summary>
        /// <returns></returns>
        public bool Initialize()
        {
            bool ret = true;
            _initErrorInfo = "Success";
            if (null == config_file)
            {
                _initErrorInfo = "初始化参数:\"配置文件\"未设置！";
                ret = false;
            }
            if (use_card_no < 0)
            {
                _initErrorInfo = "初始化参数:\"板卡卡号\"未设置！"; ;
                ret = false;
            }
            if (use_axis_num < 0)
            {
                _initErrorInfo = "初始化参数:\"板卡轴数\"未设置！"; ;
                ret = false;
            }

            string _cfgFilePath = "AppConfig/CardCfg/" + DeviceModel + "/" + use_card_no + ".cfg";
            mdCfg = new AppCfgFromXml();
            try
            {
                mdCfg.Load(_cfgFilePath, true);
            }
            catch (Exception ex)
            {
                _initErrorInfo = "\"控制器参数配置文件\" 加载失败：Path = " + _cfgFilePath + "ErrorInfo:" + ex.Message;
                return false;
            }

            IsInitOK = ret;
            return ret;

        }


        public string GetInitErrorInfo() //已实现，未测试
        {
            return _initErrorInfo;
        }

        string _initErrorInfo = "NO-OPS";

        GTDio dio;
        GTMC mc;
        GTAio aio;

        GTCompareTrigger cmpTrig;
        List<GTCompareTrigger> cmpTrigs;


        #region IMcDaq's API Begin
        public int OpenDevice()
        {
            int rtn = 0;
            //打开运动控制器。参数必须为（0,1），不能修改。
            rtn += GugaoCardHelper.InitCard(use_card_no, config_file);
            rtn += GugaoCardHelper.ClrSts(use_card_no, 1, 8);
            if (rtn == 0)
            {
                dio = new GTDio(use_card_no);
                aio = new GTAio(use_card_no);
                mc = new GTMC(use_card_no, use_axis_num, mdCfg);

                dio.Open();
                aio.Open();
                //  cmpTrigs = new List<HtmCompareTrigger>();

                IsDeviceOpen = true;
                return (int)ErrorDef.Success;
            }
            else
            {
                IsDeviceOpen = false;
                return (int)ErrorDef.InvokeFailed;
            }
        }

        public int CloseDevice()
        {
            if (GugaoCardHelper.GTClose(use_card_no) == 0)
            {
                IsDeviceOpen = false;
                return (int)ErrorDef.Success;
            }
            else
            {
                return (int)ErrorDef.InvokeFailed;
            }
        }

        public bool IsDeviceOpen { get; private set; }

        /// <summary>设备类型/// </summary>
        public string DeviceModel { get { return "固高GTS-VB系列"; } }

        /// <summary>
        /// 设备上连接的MotionCtrl（运动控制模块）的数量
        /// </summary>
        public int McMCount { get; private set; }

        /// <summary>
        /// 设备上连接的DIO（数字IO模块）的数量
        /// </summary>
        public int DioMCount { get; private set; }

        public int AioMCount { get; private set; }

        public int CompareTriggerMCount { get { return cmpTrigs.Count; } }


        /// <summary>
        /// 获取运动控制器模块
        /// </summary>
        /// <param name="index">模块序号序号，从0开始</param>
        /// <returns></returns>
        public IPlatModule_Motion GetMc(int Modelindex) //编写OK，未测试
        {
            if (Modelindex != 0)
                throw new ArgumentOutOfRangeException(string.Format("GetMc failed by index ={0} (MC's Count = {1})", Modelindex, McMCount));

            return mc;
        }

        /// <summary>
        /// 获取数字IO控制器模块
        /// </summary>
        /// <param name="index">序号，从0开始</param>
        /// <returns></returns>
        public IPlatModule_DIO GetDio(int Modelindex) //编写OK，未测试
        {
            if (Modelindex != 0)
                throw new ArgumentOutOfRangeException(string.Format("IJFMotionCtrl.GetDio failed by index ={0} (Dio's Count = {1})", Modelindex, DioMCount));

            return dio;
        }

        public IPlatModule_AIO GetAio(int Modelindex)
        {
            if (Modelindex != 0)
                throw new ArgumentOutOfRangeException(string.Format("IJFMotionCtrl.GetAio failed by index ={0} (Aio's Count = {1})", Modelindex, AioMCount));

            return aio;
        }

        public IPlatModule_CmprTrg GetCompareTrigger(int index) //编写OK，未测试
        {
            //if (index < 0 || index >= CompareTriggerCount)
            //    throw new ArgumentOutOfRangeException(string.Format("IJFMotionCtrl.GetCompareTrigger failed by index ={0} (CompareTrigger's Count = {1})", index, CompareTriggerCount));

            //return cmpTrigs[index];
            return null;
        }
        #endregion IMcDaq's API End



        public UcRealTimeUI GetRealtimeUI()
        {
            UcRealTimeUI ret = new UcRealTimeUI();
            if (!IsDeviceOpen)
                return ret;
            ret.AutoScroll = true;

            UcMotionDaq ui = new UcMotionDaq();
            ui.AutoScroll = true;
            ui.SetDevice(this, DeviceModel + use_card_no.ToString());
            return ret;

        }


        /// <summary>显示一个对话框窗口</summary>
        public void ShowCfgDialog()
        {
            if (!IsDeviceOpen)
            {
                MessageBox.Show("打开配置界面失败，Error：运动控制卡未打开！");
                return;
            }

            throw new Exception("ShowCfgDialog() failed By:HTM.LoadUI(true) return Errorcode = ");
            return;
        }

        public string GetErrorInfo(int errorCode)
        {
            string ret = "ErrorCode:" + errorCode + " Undefined";
            switch (errorCode)
            {
                case (int)ErrorDef.Success:
                    ret = "Success";
                    break;
                case (int)ErrorDef.InvokeFailed:
                    ret = "Inner API invoke failed";
                    break;
                case (int)ErrorDef.InitFailedWhenOpenCard:
                    ret = "Init Failed When Open";
                    break;
                case (int)ErrorDef.NotOpen:
                    ret = "Device not open";
                    break;
                default:
                    break;
            }
            return ret;
        }


    }
}
