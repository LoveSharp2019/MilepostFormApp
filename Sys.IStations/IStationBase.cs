using Cell.DataModel;
using Cell.Interface;
using Cell.Tools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Sys.IStations
{
    public abstract class IStationBase : ICmdWorkBase, IPlatStation
    {
        /// <summary>
        /// 工站状态转 string
        /// </summary>
        /// <param name="ws"></param>
        /// <returns></returns>
        public static string WorkStatusName(IWorkStatus ws)
        {
            string wsName = ws.ToString();
            switch (ws)
            {
                case IWorkStatus.UnStart:// = 0,    //线程未开始运行
                    wsName = "未开始";
                    break;
                case IWorkStatus.Running://,        //线程正在运行，未退出
                    wsName = "运行中";
                    break;
                case IWorkStatus.Pausing://,        //线程暂停中
                    wsName = "已暂停";
                    break;
                case IWorkStatus.Interactiving://,  //人机交互 ， 等待人工干预指令
                    wsName = "人工干预";
                    break;
                case IWorkStatus.NormalEnd://     //线程正常完成后退出
                    wsName = "正常结束";
                    break;
                case IWorkStatus.CommandExit://,    //收到退出指令
                    wsName = "指令结束";
                    break;
                case IWorkStatus.ErrorExit://,      //发生错误退出，（重启或人工消除错误后可恢复）
                    wsName = "错误退出";
                    break;
                case IWorkStatus.ExceptionExit://,  //发生异常退出 ,  (不可恢复的错误)
                    wsName = "异常退出";
                    break;
                case IWorkStatus.AbortExit://,      //由调用者强制退出
                    wsName = "强制终止";
                    break;
                default:

                    break;
            }
            return wsName;
        }

        public IStationBase()
        {
            DeclearCfgParam(CosParams.Create("轴运动到位稳定时间(毫秒)", typeof(int), cValueLimit.Min, new object[] { 0 }, false), "工站基础配置");

            _cfgFilePath = null;
            _cfg = new AppCfgFromXml();
            IsInitOK = false;
        }
        string[] _resetModes = new string[] { "每次运行前必须归零", "程序启动后只运行一次", "运行前不检查是否归零" };

        int _axisMotionDoneDelay = 0; //轴运动到位后的延时

        /// <summary> 产品完成回调 </summary>
        public Action<object, int, string[], int, string[], string[]> ActProductFinished;

        /// <summary> 定制消息回调 </summary>
        public Action<object, string, object[]> ActStationCustomizeMsg;

        string _initErrorInfo = "No-Ops"; //初始化动作失败信息

        string _cfgFilePath; //工站配置文件名称
        AppCfgFromXml _cfg = null;
        /// <summary>
        /// 用于保存工站私有配置数据
        /// </summary>
        public AppCfgFromXml Config { get { return _cfg; } }


        string _stationName = null;
        public override string Name 
        {
            get
            {
                string nameInCfg = AppHubCenter.Instance.InitorManager.GetIDByInitor(this);
                return nameInCfg != null ? nameInCfg : _stationName;
            }
            set
            {
                if (null != AppHubCenter.Instance.InitorManager.GetIDByInitor(this)) //对已经存在于Initor管理器中的工站，不能修改名称
                    return;
                _stationName = value;
            }
        }

        string IPlatOrder.Name
        {
            get { return Name; }
            set { Name = value; }
        }



        #region IPlatInitializable's API



        string[] _stationBaseInitParams = new string[] {"配置文件"//配置文件路径
                                                       
                                                        };
        /// <summary>获取初始化需要的所有参数的名称 </summary>
        public virtual string[] InitParamNames { get { return _stationBaseInitParams; } }

        /// <summary>
        /// 检查初始化参数名称是否合法
        /// 如果InitParamNames未包含initParamName 会抛出异常
        /// </summary>
        /// <param name="initParamName"></param>
        protected void CheckInitParamName(string initParamName, string function = null)
        {
            if (string.IsNullOrEmpty(initParamName))
                throw new ArgumentNullException("initParamName is null or empty" + function == null ? "" : (" in StationName=" + Name + " 's function()"));
            if (!InitParamNames.Contains(initParamName))
                throw new ArgumentNullException("initParamName is not contained by ParamName's List, " + function == null ? "" : ("StationName=" + Name + " 's function()"));
        }
        /// <summary>
        /// 获取指定名称的初始化参数的信息
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public virtual CosParams GetInitParamDescribe(string name)
        {
            CheckInitParamName(name, "GetInitParamDescribe");
            if (name == _stationBaseInitParams[0])
                return CosParams.Create(_stationBaseInitParams[0], typeof(string), cValueLimit.File, null, false);
            throw new Exception();//不可能运行到这一步
        }

        /// <summary>
        /// 获取指定名称的初始化参数的当前值
        /// </summary>
        /// <param name="name">参数名称，如果参数名称不在InitParamNames中，将会抛出一个ArgumentException异常</param>
        /// <returns>参数值</returns>
        public virtual object GetInitParamValue(string name)
        {
            CheckInitParamName(name, "GetInitParamValue");
            if (name == _stationBaseInitParams[0])
                if (_cfgFilePath == null)
                    return "AppConfig\\stations\\Hip采图工站.cfg";
                else
                    return _cfgFilePath;
            throw new Exception();//不可能运行到这一步
        }

        /// <summary>
        ///设置取指定名称的初始化参数的当前值
        /// </summary>
        /// <param name="name">参数名称，如果参数名称不在InitParamNames中，将会抛出一个ArgumentException异常</param>
        /// <param name="value">参数值</param>
        /// <returns>操作成功返回True，失败返回false，可通过GetInitErrorInfo()获取错误信息</returns>
        public virtual bool SetInitParamValue(string name, object value)
        {
            CheckInitParamName(name, "SetInitParamValue");
            if (name == _stationBaseInitParams[0])
            {
                if (null == value)
                {
                    _initErrorInfo = "SetInitParamValue(name = " + "\"" + name + "\",value) falied by: value = null";
                    return false;
                }
                if (!typeof(string).IsAssignableFrom(value.GetType()))
                {
                    _initErrorInfo = "SetInitParamValue(name = " + "\"" + name + "\",value) falied by: value's type = " + value.GetType().Name + " is not Assignable to string";
                    return false;
                }
                _cfgFilePath = value as string;
                _initErrorInfo = "Success";
                return true;
            }
            throw new Exception();
        }

        /// <summary>
        /// 对象初始化
        /// </summary>
        /// <returns>操作成功返回True，失败返回false，可通过GetInitErrorInfo()获取错误信息</returns>
        public virtual bool Initialize()
        {

            if (string.IsNullOrEmpty(_cfgFilePath))
            {
                _initErrorInfo = "Initialize Failed by: Station's CfgFilePath Is Null Or Empty";
                IsInitOK = false;
                return false;
            }

            try
            {
                _cfg.Load(_cfgFilePath, true);
                LoadCfg(); //加载工站配置
            }
            catch (Exception ex)
            {
                _cfg = null;
                _initErrorInfo = "Load Station's cfg failed!path = " + _cfgFilePath + ",Error:" + ex.Message;
                IsInitOK = false;
                return false;
            }

            _initErrorInfo = "Success";

            IsInitOK = true;
            return true;
        }


        /// <summary>获取初始化状态，如果对象已初始化成功，返回True</summary>
        public virtual bool IsInitOK { get; private set; }

        /// <summary>获取初始化错误的描述信息</summary>
        public virtual string GetInitErrorInfo()
        {
            return _initErrorInfo;
        }
        #endregion


        #region 申明工站使用的不可删除对象（包括设备/配置/方法流） ， 建议（只）在继承类的构造函数中调用
        internal DictionaryEx<NamedChnType, List<List<string>>> DeclearedDevChnMapping
        {
            get
            {
                DictionaryEx<string, object> baseCfg = _cfg.GetItemValue("StationBasePrivateConfig") as DictionaryEx<string, object>;
                if (null == baseCfg)
                    return null;
                if (!baseCfg.ContainsKey("LocalDevChannelMap"))
                    return null;
                DictionaryEx<NamedChnType, List<List<string>>> dictDevChnMap = baseCfg["LocalDevChannelMap"] as DictionaryEx<NamedChnType, List<List<string>>>; //配置中已经保存的映射表
                return dictDevChnMap;
            }
        }

        /// <summary>
        /// 获取一个声明的（工站固有的）设备通道的真正（全局）名称
        /// </summary>
        /// <param name="chnType"></param>
        /// <param name="locName"></param>
        /// <returns></returns>
        protected string GetDecChnGlobName(NamedChnType devType, string locName)
        {
            if (!IsDevChnDecleared(devType, locName))
                return null;
            DictionaryEx<NamedChnType, List<List<string>>> namesMap = DeclearedDevChnMapping;
            if (null == namesMap)
                return null;
            if (!namesMap.ContainsKey(devType))
                return null;
            List<List<string>> lgMaps = namesMap[devType];
            foreach (List<string> lg in lgMaps)
                if (lg[0] == locName)
                {
                    if (lg.Count > 1)
                        return lg[1];
                    else
                        return null;
                }

            return null;
        }


        /// <summary>
        /// 获取轴的替身名 （如果轴未指定替身名，则返回 axisGlobName）
        /// </summary>
        /// <param name="axisGlobName"></param>
        /// <returns></returns>
        public string GetDecChnAliasName(NamedChnType devType, string axisGlobName)
        {
            DictionaryEx<NamedChnType, List<List<string>>> namesMap = DeclearedDevChnMapping;
            if (null == namesMap)
                return axisGlobName;
            if (!namesMap.ContainsKey(devType))
                return null;
            List<List<string>> lgMaps = namesMap[devType];
            foreach (List<string> lg in lgMaps)
                if (lg.Count > 1 && lg[1] == axisGlobName)
                {
                    return lg[0];

                }

            return axisGlobName;
        }


        /// <summary>
        ///  获取工站声明的（固有的）所有设备通道的名称
        ///  主要用于在可选界面中作为参数列表 等...
        /// </summary>
        /// <param name="devType">设备类型</param>
        /// <returns></returns>
        public string[] AllDecDevChnNames(NamedChnType devType)
        {
            if (!_dictDeclearedDevChns.ContainsKey(devType))
                return null;
            return _dictDeclearedDevChns[devType].ToArray();

        }

        DictionaryEx<NamedChnType, List<string>> _dictDeclearedDevChns = new DictionaryEx<NamedChnType, List<string>>();

        /// <summary>
        /// 声明工站使用的设备/通道（名称）
        /// </summary>
        /// <param name="devType">设备类型</param>
        /// <param name="locName">设备在工站中的名称</param>
        /// <param name="globalName">设备全局名称</param>
        protected void DeclearDevChn(NamedChnType devType, string locName/*, string globalName = null*/)
        {
            if (devType == NamedChnType.None)
                throw new ArgumentException("DeclearDevChn(devType,...) failed by devType == NamedChnType.None");
            if (string.IsNullOrEmpty(locName))
                throw new ArgumentException("DeclearDevChn(devType,locName,...) failed by locName is null or empty");
            if (!_dictDeclearedDevChns.ContainsKey(devType))
                _dictDeclearedDevChns.Add(devType, new List<string>());
            if (_dictDeclearedDevChns[devType].Contains(locName))
                throw new ArgumentException("DeclearDevChn failed: locName = " + locName + " has been decleared!");
            _dictDeclearedDevChns[devType].Add(locName);

        }

        /// <summary>
        /// 设备通道是否为声明（固有）属性
        /// </summary>
        /// <param name="devType"></param>
        /// <param name="locName"></param>
        /// <returns></returns>
        public bool IsDevChnDecleared(NamedChnType devType, string locName)
        {
            if (string.IsNullOrEmpty(locName))
                return false;
            if (!_dictDeclearedDevChns.ContainsKey(devType))
                return false;
            return _dictDeclearedDevChns[devType].Contains(locName);
        }



        List<string> _lstWorkPosDecleared = new List<string>();
        /// <summary>
        /// 声明一个工作点位
        /// </summary>
        /// <param name="wpName"></param>
        /// <param name="pos"></param>
        protected void DeclearWorkPosition(string wpName)
        {
            if (string.IsNullOrEmpty(wpName))
                throw new ArgumentNullException("DeclearWorkPosition(wpName) failed by: wpName is null or empty!");
            if (IsWorkPosDecleared(wpName))
                throw new ArgumentException("DeclearWorkPosition(wpName) failed by: wpName = " + wpName + " is already decleared");
            _lstWorkPosDecleared.Add(wpName);

        }


        public bool IsWorkPosDecleared(string wpName)
        {
            return _lstWorkPosDecleared.Contains(wpName);
        }


        List<string> _lstMethodFlowDecleared = new List<string>();

        /// <summary>
        /// 申明一个动作流（不可删除的）
        /// </summary>
        /// <param name="mfName"></param>
        /// <param name="mf"></param>
        protected void DeclearMethodFlow(string mfName)
        {
            if (string.IsNullOrEmpty(mfName))
                throw new ArgumentNullException("DeclearMethodFlow(string mfName) failed by: mfName is null or empty");
            if (IsMethodFlowDecleared(mfName))
                throw new Exception("DeclearMethodFlow(string mfName) failed by: mfName =" + mfName + " is already decleared!");
            _lstMethodFlowDecleared.Add(mfName);
        }

        public bool IsMethodFlowDecleared(string mfName)
        {
            return _lstMethodFlowDecleared.Contains(mfName);
        }

        #endregion


        /// <summary>
        /// 工作线程当前执行的任务模式
        /// </summary>
        protected enum StationThreadWorkMode
        {
            Normal = 0,//正常工作模式
            Resetting, //工站归零任务
        }

        StationThreadWorkMode _stationThreadWorkMode = StationThreadWorkMode.Normal;

        /// <summary>
        /// 工站线程的运行模式 
        /// </summary>
        protected StationThreadWorkMode STWorkMode { get { return _stationThreadWorkMode; } }
        public override ICmdResult Start()
        {
            lock (accessLocker)
            {
                if (_stationThreadWorkMode == StationThreadWorkMode.Resetting) //当前正在进行归零动作
                {
                    if (CurrWorkStatus == IWorkStatus.Running ||
                           CurrWorkStatus == IWorkStatus.Pausing ||
                           CurrWorkStatus == IWorkStatus.Interactiving
                           )
                        return ICmdResult.StatusError;
                }
                _stationThreadWorkMode = StationThreadWorkMode.Normal;
                return base.Start();
            }
        }


        /// <summary>
        /// 向工站发送复位指令（各轴归零等动作）
        /// </summary>
        /// <returns></returns>
        public ICmdResult Reset()
        {
            lock (accessLocker)
            {
                if (_stationThreadWorkMode == StationThreadWorkMode.Normal)
                {
                    if (CurrWorkStatus == IWorkStatus.Pausing ||
                        CurrWorkStatus == IWorkStatus.Interactiving ||
                        CurrWorkStatus == IWorkStatus.Pausing) // 正在运行时不能执行归零动作
                        return ICmdResult.StatusError;
                }
                _stationThreadWorkMode = StationThreadWorkMode.Resetting; //将线程工作模式置为归零模式
                ICmdResult ret = base.Start();
                return ret;
            }
        }



        /// <summary>
        /// 执行归零动作，由继承类实现
        /// 函数如果执行失败 可通过ExitWork返回错误信息
        /// </summary>
        protected abstract void ExecuteReset();

        /// <summary>
        /// 打开程序后是否执行过一次归零动作
        /// </summary>
        bool _isExecuteResetOnce = false;


        /// <summary>
        /// 工站启动后是否需要归零
        /// </summary>
        /// <returns></returns>
        protected bool IsNeedResetWhenStart()
        {
            if (_stationThreadWorkMode == StationThreadWorkMode.Resetting)
                return true;

            string resetMode = GetSysCfgValue("工站归零模式") as string;
            if (resetMode == _resetModes[0]) //每次运行前都归零
                return true;
            else if (resetMode == _resetModes[1] && !_isExecuteResetOnce)//程序开启后只运行一次
                return true;
            return false;
        }


        /// <summary>
        /// 为了支持结批/归零动作，重写线程函数
        /// </summary>
        protected override void ThreadFunc()
        {
            long cmdWaited = CmdUnknown;
            WorkExitCode exitCode = WorkExitCode.Normal;

            try
            {
                cmdEvent.WaitOne();
                if (command != CmdStart)
                    ExitWork(WorkExitCode.Exception, "WorkThread receive first command is not CommandStart,command = " + command);
                RespCmd(ICmdResult.Success);
                ChangeWorkStatus(IWorkStatus.Running);

                PrepareWhenWorkStart();

                if (_stationThreadWorkMode == StationThreadWorkMode.Resetting)
                {
                    SendMsg2Outter("工站开始复位归零...");
                    ExecuteReset();
                    _isExecuteResetOnce = true;
                    ExitWork(WorkExitCode.Normal, "工站复位完成");
                }


                string resetMode = GetSysCfgValue("工站归零模式") as string;
                if (resetMode == _resetModes[0] //每次运行前都归零
                    || (resetMode == _resetModes[1] && !_isExecuteResetOnce))//程序开启后只运行一次
                {
                    SendMsg2Outter("任务运行前开始复位...");
                    try
                    {
                        ExecuteReset();
                    }
                    catch (IWorkExitException wee)
                    {
                        if (wee.ExitCode != WorkExitCode.Normal)
                            throw wee;
                    }
                    SendMsg2Outter("复位完成，开始运行任务");
                    _isExecuteResetOnce = true;
                }
                while (true)
                {
                    CheckCmd(CycleMilliseconds < 0 ? -1 : CycleMilliseconds);
                    RunLoopInWork();
                }
            }
            catch (IWorkExitException wee) //工作线程退出流程
            {
                exitCode = wee.ExitCode;
                SendMsg2Outter("任务即将退出:" + exitCode + " 信息:" + wee.ExitInfo);

            }
            catch (Exception ex)
            {
                exitCode = WorkExitCode.Exception;
                SendMsg2Outter("任务发生未知的程序异常:" + ex.Message);
                ChangeWorkStatus(IWorkStatus.ExceptionExit);
            }
            finally
            {
                SendMsg2Outter("任务开始退出前清理");
                try
                {
                    CleanupWhenWorkExit();
                    SendMsg2Outter("任务清理完成，退出运行");
                }
                catch (IWorkExitException exCleanWEE) //CleanupWhenWorkExit()中发生可异常
                {
                    switch (exCleanWEE.ExitCode)
                    {
                        case WorkExitCode.Command://此时不会再接受指令                        
                            break;
                        case WorkExitCode.Normal:
                            SendMsg2Outter("任务清理完成，退出运行");
                            break;
                        case WorkExitCode.Error:
                            SendMsg2Outter("清理流程发生错误:" + exCleanWEE.Message);
                            break;
                        case WorkExitCode.Exception:
                            SendMsg2Outter("清理流程发生异常:" + exCleanWEE.Message);
                            break;

                    }
                }
                catch (Exception exCleanup)
                {
                    AppHubCenter.Instance.StationMgr.OnStationLog(this, "清理流程发生未定义程序异常:" + exCleanup.Message, 0, LogMode.ShowRecord);
                }




                switch (exitCode)
                {
                    case WorkExitCode.Command:
                        ChangeWorkStatus(IWorkStatus.CommandExit);
                        break;
                    case WorkExitCode.Error:
                        ChangeWorkStatus(IWorkStatus.ErrorExit);
                        break;
                    case WorkExitCode.Exception:
                        ChangeWorkStatus(IWorkStatus.ExceptionExit);
                        break;
                    case WorkExitCode.Normal:
                        ChangeWorkStatus(IWorkStatus.NormalEnd);
                        break;

                }
                _stationThreadWorkMode = StationThreadWorkMode.Normal;
            }

        }


        #region
        UcStationRealtimeUI _ui = null;
        public virtual UcRealTimeUI GetRealtimeUI()
        {
            return null;
        }
        #endregion

        #region
        public virtual void ShowCfgDialog()
        {
            FormStationBaseCfg fm = new FormStationBaseCfg();
            fm.SetStation(this);
            fm.Text = "工站参数配置-" + Name;
            fm.ShowDialog();
        }

        public void Dispose()
        {
            try
            {
                if (Stop(1000) != ICmdResult.Success)
                    Abort();
            }
            catch
            {
                Abort();
            }
        }
        #endregion

        static object DefaultValueFromType(Type t)
        {
            if (t.IsValueType)
                return Activator.CreateInstance(t);

            ConstructorInfo[] ctors = t.GetConstructors(System.Reflection.BindingFlags.Instance
                                                          | System.Reflection.BindingFlags.NonPublic
                                                          | System.Reflection.BindingFlags.Public);
            if (null == ctors)
                throw new Exception("CreateInstance(Type t) failed By: Not found t-Instance's Constructor");
            foreach (ConstructorInfo ctor in ctors)
            {
                ParameterInfo[] ps = ctor.GetParameters();
                if (ps == null || ps.Length == 0)
                    return ctor.Invoke(null) as IPlatInitializable;
            }

            return null;
        }

        internal static bool IsNullableType(Type type)
        {
            return !type.IsValueType;//return (type.IsGenericType && type.GetGenericTypeDefinition() == typeof(Nullable<>));
        }
        bool _isFirstLoadCfg = true;
        public virtual void LoadCfg()
        {
            _cfg.Load();

            if (!_cfg.ContainsItem("StationBasePrivateConfig"))
                _cfg.AddItem("StationBasePrivateConfig", new DictionaryEx<string, object>());
            DictionaryEx<string, object> baseCfg = _cfg.GetItemValue("StationBasePrivateConfig") as DictionaryEx<string, object>; //StationBase 专用配置


            if (!baseCfg.ContainsKey("DiNames"))
                baseCfg.Add("DiNames", new List<string>());
            if (!baseCfg.ContainsKey("DoNames"))
                baseCfg.Add("DoNames", new List<string>());
            if (!baseCfg.ContainsKey(nodeName.LineScanNames.ToString()))
                baseCfg.Add(nodeName.LineScanNames.ToString(), new List<string>());
            if (!baseCfg.ContainsKey("AiNames"))
                baseCfg.Add("AiNames", new List<string>());
            if (!baseCfg.ContainsKey("AoNames"))
                baseCfg.Add("AoNames", new List<string>());

            if (!baseCfg.ContainsKey("AxisNames"))
                baseCfg.Add("AxisNames", new List<string>());

            if (!baseCfg.ContainsKey("CmpTrigNames")) //比较触发器
                baseCfg.Add("CmpTrigNames", new List<string>());

            if (!baseCfg.ContainsKey("CameraNames"))
                baseCfg.Add("CameraNames", new List<string>());

            if (!baseCfg.ContainsKey("WorkPositions"))//工站的工作点位
                baseCfg.Add("WorkPositions", new List<IMultiAxisProPos>());


            if (!baseCfg.ContainsKey("LightChannelNames"))//工站的光源通道名称
                baseCfg.Add("LightChannelNames", new List<string>());


            if (!baseCfg.ContainsKey("TrigChannelNames"))//工站的触发通道名称
                baseCfg.Add("TrigChannelNames", new DictionaryEx<string, string>());

            if (!baseCfg.ContainsKey("SystemDataPoolAliasNameMapping")) //系统数据池变量名称映射表
                baseCfg.Add("SystemDataPoolAliasNameMapping", new DictionaryEx<string, string>());

            ///工站声明的设备通道名称与全局的设备通道名称的映射关系表
            if (!baseCfg.ContainsKey("LocalDevChannelMap"))
                baseCfg.Add("LocalDevChannelMap", new DictionaryEx<NamedChnType, List<List<string>>>());
            DictionaryEx<NamedChnType, List<List<string>>> dictDevChnMap = baseCfg["LocalDevChannelMap"] as DictionaryEx<NamedChnType, List<List<string>>>; //配置中已经保存的映射表
            AdjustDevChnMap(dictDevChnMap);
            _cfg.Save();




            ///加载继承类申明的配置项
            foreach (KeyValuePair<string, object[]> kv in dictCfgParamDecleared)
            {
                string cfgName = kv.Key;
                string cfgCategory = (kv.Value as object[])[0] as string;
                Type cfgType = Type.GetType((kv.Value[1] as CosParams).ptype);
                object defaultValue = DefaultValueFromType(cfgType);
                if (!_cfg.ContainsItem(cfgName))
                {
                    _cfg.AddItem(cfgName, defaultValue, cfgCategory);
                    if (_isFirstLoadCfg && kv.Value.Length > 2) //用户在构造函数中改变了值
                        _cfg.SetItemValue(cfgName, kv.Value[2]);
                }
                else //cfg中已包含声明参数项 ，检查是否合法
                {
                    string currCategory = _cfg.GetItemTag(cfgName);
                    if (currCategory != cfgCategory) //检查类别名称是否合法
                    {
                        _cfg.RemoveItem(cfgName);
                        _cfg.AddItem(cfgName, defaultValue, cfgCategory);
                        if (_isFirstLoadCfg && kv.Value.Length > 2) //用户在构造函数中改变了值
                            _cfg.SetItemValue(cfgName, kv.Value[2]);
                    }
                    else //检查当前值类型是否合法
                    {
                        object currValue = _cfg.GetItemValue(cfgName);
                        if (null != currValue)
                        {
                            if (cfgType != currValue.GetType())
                            {
                                _cfg.RemoveItem(cfgName);
                                _cfg.AddItem(cfgName, defaultValue, cfgCategory);
                                if (_isFirstLoadCfg && kv.Value.Length > 2) //用户在构造函数中改变了值
                                    _cfg.SetItemValue(cfgName, kv.Value[2]);
                            }
                        }
                        else
                        {
                            if (!IsNullableType(cfgType))
                            {
                                _cfg.RemoveItem(cfgName);
                                _cfg.AddItem(cfgName, defaultValue, cfgCategory);
                                if (_isFirstLoadCfg && kv.Value.Length > 2) //用户在构造函数中改变了值
                                    _cfg.SetItemValue(cfgName, kv.Value[2]);
                            }
                        }
                    }
                }
            }

            ///加载声明的工作点位配置
            List<IMultiAxisProPos> wokPosInCfg = baseCfg["WorkPositions"] as List<IMultiAxisProPos>;
            foreach (string wpName in _lstWorkPosDecleared)
            {
                bool isExistedWP = false;
                foreach (IMultiAxisProPos wp in wokPosInCfg)
                    if (wp.Name == wpName)
                    {
                        isExistedWP = true;
                        break;
                    }
                if (!isExistedWP)
                {
                    IMultiAxisProPos ap = new IMultiAxisProPos();
                    ap.Name = wpName;
                    wokPosInCfg.Add(ap);
                    _cfg.Save();
                }
            }

            ///加载系统数据映射表
            _dictSysPoolItemNameMapping = baseCfg["SystemDataPoolAliasNameMapping"] as DictionaryEx<string, string>;
            foreach (string aliasName in _lstSysPoolItemAliasNames)
            {
                if (!_dictSysPoolItemNameMapping.ContainsKey(aliasName))
                    _dictSysPoolItemNameMapping.Add(aliasName, Name + ":" + aliasName);//添加一个默认的全局名称

                string realName = _dictSysPoolItemNameMapping[aliasName];
                if (!AppHubCenter.Instance.DataPool.ContainItem(realName)) //如果系统数据池中没有值项，则添加初始值
                    AppHubCenter.Instance.DataPool.RegistItem(realName, _dictSysPoolItemDecleared[aliasName][0] as Type, _dictSysPoolItemDecleared[aliasName][1]);
            }


            _isFirstLoadCfg = false;
        }


        /// <summary>
        /// 整理注册的通道映射表 ，剔除多余的部分，增加（未加入的已声明通道）
        /// </summary>
        /// <param name="devChnMapInCfg">已保存在配置文件中的映射表</param>
        void AdjustDevChnMap(DictionaryEx<NamedChnType, List<List<string>>> devChnMapInCfg)
        {
            if (_dictDeclearedDevChns.Keys.Count == 0)
            {
                devChnMapInCfg.Clear();
                return;
            }

            //先删除配置中多余的Key
            int keyCount = devChnMapInCfg.Keys.Count;
            while (keyCount > 0)
            {
                int index = 0;
                foreach (NamedChnType k in devChnMapInCfg.Keys)
                {
                    if (!_dictDeclearedDevChns.Keys.Contains(k))
                    {
                        devChnMapInCfg.Remove(k);
                        break;
                    }
                    else
                        index++;
                }
                if (keyCount == index)
                    break;
                keyCount = devChnMapInCfg.Keys.Count;
            }


            foreach (NamedChnType k in _dictDeclearedDevChns.Keys)
            {
                if (!devChnMapInCfg.ContainsKey(k))
                {
                    devChnMapInCfg.Add(k, new List<List<string>>());
                    List<string> devChnDecleared = _dictDeclearedDevChns[k];
                    foreach (string locDevChnName in devChnDecleared)
                    {
                        List<string> locAndGlobName = new List<string>();
                        locAndGlobName.Add(locDevChnName);
                        locAndGlobName.Add("");
                        devChnMapInCfg[k].Add(locAndGlobName);
                    }
                }
                else
                {
                    List<string> devChnDecleared = _dictDeclearedDevChns[k];
                    List<List<string>> devChnMap = devChnMapInCfg[k];

                    ///先删除多余的部分
                    int dcCount = devChnMap.Count;
                    while (dcCount > 0)
                    {
                        int index = 0;
                        foreach (List<string> dcm in devChnMap)
                        {
                            if (!devChnDecleared.Contains(dcm[0]))
                            {
                                devChnMap.Remove(dcm);
                                break;
                            }
                            else
                                index++;
                        }
                        if (index == dcCount)
                            break;
                        dcCount = devChnMap.Count;
                    }


                    //再添加缺少的部分
                    foreach (string locDevChnName in devChnDecleared)
                    {
                        bool isFound = false;
                        foreach (List<string> kv in devChnMap)
                            if (kv[0] == locDevChnName)
                            {
                                isFound = true;
                                break;
                            }
                        if (!isFound)
                        {
                            List<string> locAndGlobNames = new List<string>();
                            locAndGlobNames.Add(locDevChnName);
                            locAndGlobNames.Add("");
                            devChnMap.Add(locAndGlobNames);
                        }

                    }

                }

            }

        }

        public virtual void SaveCfg()
        {
            (_cfg.GetItemValue("StationBasePrivateConfig") as DictionaryEx<string, object>)["SystemDataPoolAliasNameMapping"] = _dictSysPoolItemNameMapping;
            _cfg.Save();
        }

        List<string> _SBCfg(string sbCfgName)
        {
            if (null == _cfg)
                return new List<string>();
            if (!_cfg.ContainsItem("StationBasePrivateConfig")) 
                return new List<string>();
            DictionaryEx<string, object> cfgItm = _cfg.GetItemValue("StationBasePrivateConfig") as DictionaryEx<string, object>;
            if (!cfgItm.ContainsKey(sbCfgName))
                return new List<string>();
            return cfgItm[sbCfgName] as List<string>;

        }

        #region 工站所使用的设备（通道）

        public enum nodeName
        {
            DiNames, DoNames, AiNames, AoNames, AxisNames
        , CmpTrigNames, CameraNames, LineScanNames
        }

        /// <summary>
        /// 获取工站内线扫相机
        /// </summary>
        public string[] LineScanNames
        {
            get
            {
                return _SBCfg(nodeName.LineScanNames.ToString()).ToArray();
            }
        }

        public void AddnamebyNodeName(string diName, nodeName NodeName)
        {
            if (string.IsNullOrEmpty(diName))
                throw new ArgumentNullException("AddNodeName(" + NodeName.ToString() + ") failed by:diName is null or empty");
            List<string> diNames = _SBCfg(NodeName.ToString());
            if (diNames.Contains(diName))
                return;
            diNames.Add(diName);
        }

        public void RemoveNamebyNodeName(string diName, nodeName NodeName)
        {
            List<string> diNames = _SBCfg(NodeName.ToString());
            if (!diNames.Contains(diName))
                return;
            diNames.Remove(diName);
        }

        public void ClearNamebyNodeName(nodeName NodeName)
        {
            _SBCfg(NodeName.ToString()).Clear();
        }

        public bool ContainAxis(string axisName, nodeName NodeName)
        {
            return _SBCfg(NodeName.ToString()).Contains(axisName);
        }
        /// <summary>
        ///  获取工站所用的数字量输入（通道）
        /// </summary>
        public string[] DINames
        {
            get
            {
                return _SBCfg(nodeName.DiNames.ToString()).ToArray();
            }
        }

        public string[] DONames
        {
            get
            {
                return _SBCfg(nodeName.DoNames.ToString()).ToArray();
            }
        }

        public string[] AINames
        {
            get
            {
                return _SBCfg(nodeName.AiNames.ToString()).ToArray();
            }
        }
        public string[] AONames
        {
            get
            {
                return _SBCfg(nodeName.AoNames.ToString()).ToArray();
            }
        }

        public string[] AxisNames
        {
            get
            {
                return _SBCfg(nodeName.AxisNames.ToString()).ToArray();
            }
        }

        public string[] CmpTrigNames
        {
            get
            {
                return _SBCfg(nodeName.CmpTrigNames.ToString()).ToArray();
            }
        }

        public string[] CameraNames
        {
            get
            {
                return _SBCfg(nodeName.CameraNames.ToString()).ToArray();
            }
        }

        #endregion

        public string[] WorkPositionNames
        {
            get
            {
                List<string> ret = new List<string>();
                List<IMultiAxisProPos> allPos = WorkPositions;
                foreach (IMultiAxisProPos pos in allPos)
                    ret.Add(pos.Name);
                return ret.ToArray();
            }
        }

        public bool ContianPositionName(string posName)
        {
            foreach (IMultiAxisProPos pos in WorkPositions)
                if (pos.Name == posName)
                    return true;
            return false;
        }

        public IMultiAxisProPos GetWorkPosition(string name)
        {
            List<IMultiAxisProPos> allPos = WorkPositions;
            foreach (IMultiAxisProPos pos in allPos)
                if (pos.Name == name)
                    return pos;
            return null;
        }

        public void AddWorkPosition(IMultiAxisProPos maPos)
        {
            if (null == maPos || string.IsNullOrEmpty(maPos.Name))
                throw new ArgumentNullException("SetWorkPosition(maPos) failed by:maPos == null or maPos.Name is null or empty");
            List<IMultiAxisProPos> allPos = WorkPositions;
            for (int i = 0; i < allPos.Count; i++)
                if (allPos[i].Name == maPos.Name)
                {
                    allPos[i] = maPos;
                    return;
                }
            allPos.Add(maPos);
        }

        /// <summary>
        /// 移除工作点位
        /// </summary>
        /// <param name="name"></param>
        public void RemoveWorkPosition(string name)
        {
            if (string.IsNullOrEmpty(name))
                return;
            List<IMultiAxisProPos> allPos = WorkPositions;
            for (int i = 0; i < allPos.Count; i++)
                if (allPos[i].Name == name)
                {
                    allPos.RemoveAt(i);
                    return;
                }
        }

        /// <summary>
        /// 移动到工作点位posName （非插补模式）
        /// </summary>
        /// <param name="posName"></param>
        /// <param name="errorInfo"></param>
        /// <returns></returns>
        public bool MoveToWorkPosition(string posName, out string errorInfo)
        {
            errorInfo = "Success";
            string[] workPosNames = WorkPositionNames;
            if (WorkPositionNames == null)
            {
                errorInfo = "无工作点位Name = " + posName;
                return false;
            }
            bool isExisted = false;
            foreach (string workPosName in workPosNames)
                if (workPosName == posName)
                {
                    isExisted = true;
                    break;
                }
            if (!isExisted)
            {
                errorInfo = " 工作点位Name = \"" + posName + "\"不存在";
                return false;
            }

            IMultiAxisProPos pos = GetWorkPosition(posName);
            return MoveToPosition(pos, out errorInfo);

        }

        public bool MoveWorkPosAndWait(string workPosName, out string errorInfo, int timeoutSeconds = -1)
        {
            errorInfo = "Success";
            string[] workPosNames = WorkPositionNames;
            if (WorkPositionNames == null)
            {
                errorInfo = "无工作点位Name = " + workPosName;
                return false;
            }
            bool isExisted = false;
            foreach (string workPosNameTmp in workPosNames)
                if (workPosNameTmp == workPosName)
                {
                    isExisted = true;
                    break;
                }
            if (!isExisted)
            {
                errorInfo = " 工作点位Name = \"" + workPosName + "\"不存在";
                return false;
            }

            IMultiAxisProPos pos = GetWorkPosition(workPosName);
            return MovePosAndWait(pos, out errorInfo, timeoutSeconds * 1000);
        }


        public bool MovePosAndWait(IMultiAxisProPos pos, out string errorInfo, int timeoutSeconds = -1)
        {
            if (!MoveToPosition(pos, out errorInfo))
                return false;
            return WaitToPosition(pos, out errorInfo, timeoutSeconds * 1000);
        }




        /// <summary>
        /// 检查轴通道(设备)是否存在(可用)
        /// </summary>
        /// <param name="axisName"></param>
        /// <returns></returns>
        IDevCellInfo CheckAxisDevInfo(string axisName, out string errorInfo)
        {
            if (!ContainAxis(axisName, nodeName.AxisNames))
            {
                errorInfo = "工站不包含轴，Name = \"" + axisName + "\"";
                return null;
            }
            IDevCellInfo ci = AppHubCenter.Instance.MDCellNameMgr.GetAxisCellInfo(axisName); //在命名表中的通道信息
            if (null == ci)
            {
                errorInfo = "未找到轴:\"" + axisName + "\"设备信息";
                return null;
            }
            IPlatDevice_MotionDaq dev = AppHubCenter.Instance.InitorManager.GetInitor(ci.DeviceID) as IPlatDevice_MotionDaq;
            if (null == dev)
            {
                errorInfo = "未找到轴:\"" + axisName + "\"所属设备:\"" + ci.DeviceID + "\"";
                return null;
            }
            if (!dev.IsDeviceOpen)
            {
                errorInfo = "轴:\"" + axisName + "\"所属设备:\"" + ci.DeviceID + "\"未打开";
                return null;
            }
            if (ci.ModuleIndex >= dev.McMCount)
            {
                errorInfo = "轴:\"" + axisName + "\"模块序号:\"" + ci.ModuleIndex + "\"超出限制!";
                return null;
            }
            IPlatModule_Motion md = dev.GetMc(ci.ModuleIndex);
            if (ci.ChannelIndex >= md.AxisCount)
            {
                errorInfo = "轴:\"" + axisName + "\"通道序号:\"" + ci.ChannelIndex + "\"超出限制!";
                return null;
            }

            errorInfo = "";
            return ci;
        }

        bool CheckAxisCanMove(string axisName, out string errorInfo)
        {
            IDevCellInfo ci = CheckAxisDevInfo(axisName, out errorInfo);
            if (null == ci)
                return false;

            IPlatModule_Motion md = (AppHubCenter.Instance.InitorManager.GetInitor(ci.DeviceID) as IPlatDevice_MotionDaq).GetMc(ci.ModuleIndex);
            return CheckAxisCanMove(md, ci.ChannelIndex, out errorInfo);
        }

        /// <summary>
        /// 清楚轴报警
        /// </summary>
        /// <param name="axisName"></param>
        /// <param name="errorInfo"></param>
        /// <returns></returns>
        public bool AxisClearAlarm(string axisName, out string errorInfo)
        {
            IDevCellInfo ci = CheckAxisDevInfo(axisName, out errorInfo);
            if (null == ci)
            {
                errorInfo = "轴:\"" + axisName + "\" 清除报警失败,ErrorInfo:" + errorInfo;
                return false;
            }
            int errCode = (AppHubCenter.Instance.InitorManager.GetInitor(ci.DeviceID) as IPlatDevice_MotionDaq).GetMc(ci.ModuleIndex).ClearAlarm(ci.ChannelIndex);
            if (errCode != 0)
            {
                errorInfo = "轴:\"" + axisName + "\"清除报警失败,ErrorInfo:" + (AppHubCenter.Instance.InitorManager.GetInitor(ci.DeviceID) as IPlatDevice_MotionDaq).GetMc(ci.ModuleIndex).GetErrorInfo(errCode);
                return false;
            }
            errorInfo = "Success";
            return true;
        }

        /// <summary>
        /// 轴清除报警 （替身名）
        /// </summary>
        /// <param name="aliasName"></param>
        /// <param name="errorInfo"></param>
        /// <returns></returns>
        public bool AxisClearAlarmAlias(string aliasName, out string errorInfo)
        {
            string gAxisName = GetDecChnGlobName(NamedChnType.Axis, aliasName);
            if (string.IsNullOrEmpty(gAxisName))
            {
                errorInfo = "清除轴报警失败，替身名:\"" + aliasName + "\"  未指定轴通道名称";
                return false;
            }

            bool ret = AxisClearAlarm(gAxisName, out errorInfo);
            if (!ret)
                errorInfo = "轴替身:\"" + aliasName + "\" " + errorInfo;
            return ret;
        }



        /// <summary>
        /// 打开所有轴设备,消除报警,伺服使能
        /// </summary>
        /// <param name="errorInfo"></param>
        /// <returns></returns>
        public bool EnableAllAxis(out string errorInfo)
        {
            string[] allAxisNames = AxisNames;//工站包含的所有电机
            if (null == allAxisNames || 0 == allAxisNames.Length)
            {
                errorInfo = "Success:工站不包含电机轴";
                return true;
            }
            foreach (string axisName in allAxisNames)
            {
                //打开电机所属设备
                if (!OpenChnDevice(NamedChnType.Axis, axisName, out errorInfo))
                {
                    errorInfo = "使能工站所有轴电机失败:" + errorInfo;
                    return false;
                }
                IPlatDevice dev = null;
                IDevCellInfo ci = null;
                if (!AppDevChannel.CheckChannel(IDevCellType.Axis, axisName, out dev, out ci, out errorInfo))
                {
                    errorInfo = "使能工站所有轴电机失败:" + errorInfo;
                    return false;
                }
                IPlatModule_Motion md = (dev as IPlatDevice_MotionDaq).GetMc(ci.ModuleIndex);
                //电机消除警报
                int nRet = md.ClearAlarm(ci.ChannelIndex);
                if (0 != nRet)
                {
                    errorInfo = "使能工站所有轴电机失败:未能消除电机报警:innerError = " + md.GetErrorInfo(nRet);
                    return false;
                }

                //电机伺服上电
                nRet = md.ServoOn(ci.ChannelIndex);
                if (0 != nRet)
                {
                    errorInfo = "使能工站所有轴电机失败:电机伺服使能失败:innerError = " + md.GetErrorInfo(nRet);
                    return false;
                }
                //上电之后再消除一次报警
                nRet = md.ClearAlarm(ci.ChannelIndex);
                if (0 != nRet)
                {
                    errorInfo = "使能工站所有轴电机失败:未能消除电机报警:innerError = " + md.GetErrorInfo(nRet);
                    return false;
                }
            }

            errorInfo = "Success";
            return true;
        }



        /// <summary>
        /// 打开工站中所有命名通道所属的设备
        /// </summary>
        /// <param name="errorInfo"></param>
        /// <returns></returns>
        public bool OpenAllDevices(out string errorInfo)
        {
            StringBuilder sbErrorInfo = new StringBuilder();
            bool ret = true;
            string optErrorInfo;

            //先检查所有设备通道的替身名都绑定
            foreach (NamedChnType nct in _dictDeclearedDevChns.Keys)
            {
                List<string> allAliasNames = _dictDeclearedDevChns[nct];
                foreach (string s in allAliasNames)
                {
                    string gName = GetDecChnGlobName(nct, s);
                    if (string.IsNullOrEmpty(gName))
                    {
                        ret = false;
                        sbErrorInfo.AppendLine(nct.ToString() + " 替身名:\"" + s + "\"未绑定全局通道名称");
                        errorInfo = sbErrorInfo.ToString();
                        return ret;
                    }
                }
            }

            //打开所有di设备
            string[] allChnNames = DINames;
            if (null != allChnNames)
                foreach (string s in allChnNames)
                    if (!OpenChnDevice(NamedChnType.Di, s, out optErrorInfo))
                    {
                        sbErrorInfo.AppendLine(s + "open fail" + optErrorInfo);
                        ret = false;
                        errorInfo = sbErrorInfo.ToString();
                        return ret;
                    }

            //打开所有do设备
            allChnNames = DONames;
            if (null != allChnNames)
                foreach (string s in allChnNames)
                    if (!OpenChnDevice(NamedChnType.Do, s, out optErrorInfo))
                    {
                        sbErrorInfo.AppendLine(s + "open fail" + optErrorInfo);
                        ret = false;
                        errorInfo = sbErrorInfo.ToString();
                        return ret;
                    }

            //打开所有轴设备
            allChnNames = AxisNames;
            if (null != allChnNames)
                foreach (string s in allChnNames)
                    if (!OpenChnDevice(NamedChnType.Axis, s, out optErrorInfo))
                    {
                        sbErrorInfo.AppendLine(s + "open fail" + optErrorInfo);
                        ret = false;
                        errorInfo = sbErrorInfo.ToString();
                        return ret;
                    }


            //打开所有相机设备
            allChnNames = CameraNames;
            if (null != allChnNames)
                foreach (string s in allChnNames)
                    if (!OpenChnDevice(NamedChnType.Camera, s, out optErrorInfo))
                    {
                        sbErrorInfo.AppendLine(s + "open fail" + optErrorInfo);
                        ret = false;
                        errorInfo = sbErrorInfo.ToString();
                        return ret;
                    }

            //打开所有AI设备
            allChnNames = AINames;
            if (null != allChnNames)
                foreach (string s in allChnNames)
                    if (!OpenChnDevice(NamedChnType.Ai, s, out optErrorInfo))
                    {
                        sbErrorInfo.AppendLine(s + "open fail" + optErrorInfo);
                        ret = false;
                        errorInfo = sbErrorInfo.ToString();
                        return ret;
                    }

            //打开所有AO设备
            allChnNames = AONames;
            if (null != allChnNames)
                foreach (string s in allChnNames)
                    if (!OpenChnDevice(NamedChnType.Ao, s, out optErrorInfo))
                    {
                        sbErrorInfo.AppendLine(s + "open fail" + optErrorInfo);
                        ret = false;
                        errorInfo = sbErrorInfo.ToString();
                        return ret;
                    }


            if (ret)
                errorInfo = "Success";
            else
                errorInfo = sbErrorInfo.ToString();
            return ret;
        }





        #region  相机相关操作
        /// <summary>
        /// 打开/关闭所有相机设备
        /// </summary>
        /// <param name="errorInfo"></param>
        /// <returns></returns>
        public bool EnableAllCmrDev(bool isOpen, out string errorInfo)
        {
            string[] allCmrNames = CameraNames;
            if (null == allCmrNames || 0 == allCmrNames.Length)
            {
                errorInfo = "Success:工站不包含相机设备";
                return true;
            }
            AppInitorManager mgr = AppHubCenter.Instance.InitorManager;
            foreach (string s in allCmrNames)
            {
                IPlatInitializable initor = mgr.GetInitor(s);
                if (null == initor)
                {
                    errorInfo = "设备列表中未包含相机:" + s;
                    return false;
                }

                IPlatDevice_Camera cmr = initor as IPlatDevice_Camera;
                if (null == cmr)
                {
                    errorInfo = "设备列表中未包含相机:" + s + ",InitorName = \"" + s + "\" realType = " + initor.GetType().Name;
                    return false;
                }
                int ret = 0;
                if (isOpen)
                    ret = cmr.OpenDevice();
                else
                    ret = cmr.CloseDevice();
                if (ret != 0)
                {
                    errorInfo = "相机:\"" + s + "\"" + (isOpen ? "打开" : "关闭") + "失败，ErrorInfo:" + cmr.GetErrorInfo(ret);
                    return false;
                }
            }
            errorInfo = "Success";
            return true;

        }

        /// <summary>
        /// 打开/关闭所有相机设备
        /// </summary>
        /// <param name="errorInfo"></param>
        /// <returns></returns>
        public bool EnableAllLineScanDev(bool isOpen, out string errorInfo)
        {
            string[] allCmrNames = CameraNames;
            if (null == allCmrNames || 0 == allCmrNames.Length)
            {
                errorInfo = "Success:工站不包含相机设备";
                return true;
            }
            AppInitorManager mgr = AppHubCenter.Instance.InitorManager;
            foreach (string s in allCmrNames)
            {
                IPlatInitializable initor = mgr.GetInitor(s);
                if (null == initor)
                {
                    errorInfo = "设备列表中未包含相机:" + s;
                    return false;
                }

                IPlatDevice_LineScan cmr = initor as IPlatDevice_LineScan;
                if (null == cmr)
                {
                    errorInfo = "设备列表中未包含线扫激光:" + s + ",InitorName = \"" + s + "\" realType = " + initor.GetType().Name;
                    return false;
                }
                int ret = 0;
                if (isOpen)
                    ret = cmr.OpenDevice();
                else
                    ret = cmr.CloseDevice();
                if (ret != 0)
                {
                    errorInfo = "线扫激光:\"" + s + "\"" + (isOpen ? "打开" : "关闭") + "失败，ErrorInfo:" + cmr.GetErrorInfo(ret);
                    return false;
                }
            }
            errorInfo = "Success";
            return true;

        }

        /// <summary>
        /// 设置相机触发模式
        /// </summary>
        /// <param name="cmrName"></param>
        /// <param name="mode"></param>
        /// <param name="errorInfo"></param>
        /// <returns></returns>
        public bool SetCmrTrigMode(string cmrName, cCmrTrigMode mode, out string errorInfo)
        {
            AppInitorManager mgr = AppHubCenter.Instance.InitorManager;
            IPlatInitializable initor = mgr.GetInitor(cmrName);
            if (null == initor)
            {
                errorInfo = "设置相机触发模式失败,设备列表中未包含相机:" + cmrName;
                return false;
            }

            IPlatDevice_Camera cmr = initor as IPlatDevice_Camera;
            if (null == cmr)
            {
                errorInfo = "设置相机触发模式失败,设备列表中未包含相机:" + cmrName + ",Initor's realType = " + initor.GetType().Name;
                return false;
            }

            int ret = cmr.SetTrigMode(mode);
            if (ret != 0)
            {
                errorInfo = "设置相机触发模式失败:Cmr = \"" + cmrName + "\",ErrorInfo = " + cmr.GetErrorInfo(ret);
                return false;
            }
            errorInfo = "Success";
            return true;
        }

        /// <summary>
        /// 设置相机触发模式（替身名）
        /// </summary>
        /// <param name="aliasName"></param>
        /// <param name="mode"></param>
        /// <param name="errorInfo"></param>
        /// <returns></returns>
        public bool SetCmrTrigModeAlias(string aliasName, cCmrTrigMode mode, out string errorInfo)
        {
            if (!IsDevChnDecleared(NamedChnType.Camera, aliasName))
            {
                errorInfo = "设置相机触发模式失败，Alias:\"" + aliasName + "\"不存在！";
                return false;
            }
            string globName = GetDecChnGlobName(NamedChnType.Camera, aliasName);
            if (string.IsNullOrEmpty(globName))
            {
                errorInfo = "设置相机触发模式失败，Alias:\"" + aliasName + "\"" + " 未绑定全局设备名";
                return false;
            }

            if (!SetCmrTrigMode(globName, mode, out errorInfo))
            {
                errorInfo = "设置相机触发模式失败，Alias:\"" + aliasName + "\"->" + errorInfo;
                return false;
            }

            return true;
        }

        /// <summary>
        /// 打开/关闭 相机图像采集
        /// </summary>
        /// <param name="cmrName"></param>
        /// <param name="isStart"></param>
        /// <param name="errorInfo"></param>
        /// <returns></returns>
        public bool EnableCmrGrab(string cmrName, bool isStart, out string errorInfo)
        {
            AppInitorManager mgr = AppHubCenter.Instance.InitorManager;
            IPlatInitializable initor = mgr.GetInitor(cmrName);
            if (null == initor)
            {
                errorInfo = (isStart ? "打开" : "关闭") + "相机采集失败,设备列表中未包含相机:" + cmrName;
                return false;
            }

            IPlatDevice_Camera cmr = initor as IPlatDevice_Camera;
            if (null == cmr)
            {
                errorInfo = (isStart ? "打开" : "关闭") + "相机采集失败,设备列表中未包含相机:" + cmrName + ",Initor's realType = " + initor.GetType().Name;
                return false;
            }
            int ret = 0;
            if (isStart)
                ret = cmr.StartGrab();
            else
                ret = cmr.StopGrab();
            if (ret != 0)
            {
                errorInfo = (isStart ? "打开" : "关闭") + "相机采集失败:Cmr = \"" + cmrName + "\",ErrorInfo = " + cmr.GetErrorInfo(ret);
                return false;
            }
            errorInfo = "Success";
            return true;
        }

        public bool EnableCmrGrabAlias(string aliasName, bool isStart, out string errorInfo)
        {
            if (!IsDevChnDecleared(NamedChnType.Camera, aliasName))
            {
                errorInfo = (isStart ? "打开" : "关闭") + "相机采集失败，Alias:\"" + aliasName + "\"不存在！";
                return false;
            }
            string globName = GetDecChnGlobName(NamedChnType.Camera, aliasName);
            if (string.IsNullOrEmpty(globName))
            {
                errorInfo = (isStart ? "打开" : "关闭") + "相机采集失败，Alias:\"" + aliasName + "\"" + " 未绑定全局设备名";
                return false;
            }

            if (!EnableCmrGrab(globName, isStart, out errorInfo))
            {
                errorInfo = (isStart ? "打开" : "关闭") + "相机采集失败，Alias:\"" + aliasName + "\"->" + errorInfo;
                return false;
            }

            return true;
        }

        /// <summary>
        ///  打开/关闭 线扫相机图像采集
        /// </summary>
        /// <param name="aliasName"></param>
        /// <param name="isStart"></param>
        /// <param name="errorInfo"></param>
        /// <returns></returns>
        public bool EnableLineScanGrabAlias(string aliasName, bool isStart, out string errorInfo)
        {
            if (!IsDevChnDecleared(NamedChnType.LineScan, aliasName))
            {
                errorInfo = (isStart ? "打开" : "关闭") + "线扫激光采集失败，Alias:\"" + aliasName + "\"不存在！";
                return false;
            }
            string globName = GetDecChnGlobName(NamedChnType.LineScan, aliasName);
            if (string.IsNullOrEmpty(globName))
            {
                errorInfo = (isStart ? "打开" : "关闭") + "线扫激光采集失败，Alias:\"" + aliasName + "\"" + " 未绑定全局设备名";
                return false;
            }

            if (!EnableLineScanGrab(globName, isStart, out errorInfo))
            {
                errorInfo = (isStart ? "打开" : "关闭") + "线扫激光采集失败，Alias:\"" + aliasName + "\"->" + errorInfo;
                return false;
            }

            return true;
        }

        /// <summary>
        /// 打开/关闭 线扫相机图像采集
        /// </summary>
        /// <param name="cmrName"></param>
        /// <param name="isStart"></param>
        /// <param name="errorInfo"></param>
        /// <returns></returns>
        public bool EnableLineScanGrab(string cmrName, bool isStart, out string errorInfo)
        {
            AppInitorManager mgr = AppHubCenter.Instance.InitorManager;
            IPlatInitializable initor = mgr.GetInitor(cmrName);
            if (null == initor)
            {
                errorInfo = (isStart ? "打开" : "关闭") + "线扫激光采集失败,设备列表中未包含相机:" + cmrName;
                return false;
            }

            IPlatDevice_LineScan cmr = initor as IPlatDevice_LineScan;
            if (null == cmr)
            {
                errorInfo = (isStart ? "打开" : "关闭") + "线扫激光采集失败,设备列表中未包含相机:" + cmrName + ",Initor's realType = " + initor.GetType().Name;
                return false;
            }
            int ret = 0;
            if (isStart)
                ret = cmr.StartGrab();
            else
                ret = cmr.StopGrab();
            if (ret != 0)
            {
                errorInfo = (isStart ? "打开" : "关闭") + "线扫激光采集失败:Cmr = \"" + cmrName + "\",ErrorInfo = " + cmr.GetErrorInfo(ret);
                return false;
            }
            errorInfo = "Success";
            return true;
        }
        /// <summary>
        /// 设置相机曝光值
        /// </summary>
        /// <param name="cmrName"></param>
        /// <param name="microSeconds"></param>
        /// <param name="errorInfo"></param>
        /// <returns></returns>
        public bool SetCmrExposure(string cmrName, double microSeconds, out string errorInfo)
        {
            AppInitorManager mgr = AppHubCenter.Instance.InitorManager;
            IPlatInitializable initor = mgr.GetInitor(cmrName);
            if (null == initor)
            {
                errorInfo = "设置相机曝光参数失败,设备列表中未包含相机:" + cmrName;
                return false;
            }

            IPlatDevice_Camera cmr = initor as IPlatDevice_Camera;
            if (null == cmr)
            {
                errorInfo = "设置相机曝光参数失败,设备列表中未包含相机:" + cmrName + ",Initor's realType = " + initor.GetType().Name;
                return false;
            }

            int ret = cmr.SetExposureTime(microSeconds);
            if (ret != 0)
            {
                errorInfo = "设置相机曝光参数失败:Cmr = \"" + cmrName + "\",ErrorInfo = " + cmr.GetErrorInfo(ret);
                return false;
            }
            errorInfo = "Success";
            return true;
        }

        /// <summary>
        /// 设置相机曝光值（替身名）
        /// </summary>
        /// <param name="aliasName"></param>
        /// <param name="microSeconds"></param>
        /// <param name="errorInfo"></param>
        /// <returns></returns>
        public bool SetCmrExposureAlias(string aliasName, double microSeconds, out string errorInfo)
        {
            if (!IsDevChnDecleared(NamedChnType.Camera, aliasName))
            {
                errorInfo = "设置相机曝光参数失败，Alias:\"" + aliasName + "\"不存在！";
                return false;
            }
            string globName = GetDecChnGlobName(NamedChnType.Camera, aliasName);
            if (string.IsNullOrEmpty(globName))
            {
                errorInfo = "设置相机曝光参数失败，Alias:\"" + aliasName + "\"" + " 未绑定全局设备名";
                return false;
            }

            if (!SetCmrExposure(globName, microSeconds, out errorInfo))
            {
                errorInfo = "设置相机曝光参数失败，Alias:\"" + aliasName + "\"->" + errorInfo;
                return false;
            }

            return true;
        }

        /// <summary>
        /// 获取相机曝光值
        /// </summary>
        /// <param name="cmrName"></param>
        /// <param name="microSeconds"></param>
        /// <param name="errorInfo"></param>
        /// <returns></returns>
        public bool GetCmrExposure(string cmrName, out double microSeconds, out string errorInfo)
        {
            AppInitorManager mgr = AppHubCenter.Instance.InitorManager;
            IPlatInitializable initor = mgr.GetInitor(cmrName);

            microSeconds = 0;
            if (null == initor)
            {
                errorInfo = "获取相机曝光参数失败,设备列表中未包含相机:" + cmrName;
                return false;
            }

            IPlatDevice_Camera cmr = initor as IPlatDevice_Camera;
            if (null == cmr)
            {
                errorInfo = "获取相机曝光参数失败,设备列表中未包含相机:" + cmrName + ",Initor's realType = " + initor.GetType().Name;
                return false;
            }

            int ret = cmr.GetExposureTime(out microSeconds);
            if (ret != 0)
            {
                errorInfo = "获取相机曝光参数失败:Cmr = \"" + cmrName + "\",ErrorInfo = " + cmr.GetErrorInfo(ret);
                return false;
            }
            errorInfo = "Success";
            return true;
        }

        /// <summary>
        /// 获取相机曝光值（替身名）
        /// </summary>
        /// <param name="aliasName"></param>
        /// <param name="microSeconds"></param>
        /// <param name="errorInfo"></param>
        /// <returns></returns>
        public bool GetCmrExposureAlias(string aliasName, out double microSeconds, out string errorInfo)
        {
            microSeconds = 0;

            if (!IsDevChnDecleared(NamedChnType.Camera, aliasName))
            {
                errorInfo = "获取相机曝光参数失败，Alias:\"" + aliasName + "\"不存在！";
                return false;
            }
            string globName = GetDecChnGlobName(NamedChnType.Camera, aliasName);
            if (string.IsNullOrEmpty(globName))
            {
                errorInfo = "获取相机曝光参数失败，Alias:\"" + aliasName + "\"" + " 未绑定全局设备名";
                return false;
            }

            if (!GetCmrExposure(globName, out microSeconds, out errorInfo))
            {
                errorInfo = "获取相机曝光参数失败，Alias:\"" + aliasName + "\"->" + errorInfo;
                return false;
            }

            return true;
        }

        /// <summary>
        /// 设置相机增益参数
        /// </summary>
        /// <param name="cmrName"></param>
        /// <param name="gain"></param>
        /// <param name="errorInfo"></param>
        /// <returns></returns>
        public bool SetCmrGain(string cmrName, double gain, out string errorInfo)
        {
            AppInitorManager mgr = AppHubCenter.Instance.InitorManager;
            IPlatInitializable initor = mgr.GetInitor(cmrName);
            if (null == initor)
            {
                errorInfo = "设置相机增益参数失败,设备列表中未包含相机:" + cmrName;
                return false;
            }

            IPlatDevice_Camera cmr = initor as IPlatDevice_Camera;
            if (null == cmr)
            {
                errorInfo = "设置相机增益参数失败,设备列表中未包含相机:" + cmrName + ",Initor's realType = " + initor.GetType().Name;
                return false;
            }

            int ret = cmr.SetGain(gain);
            if (ret != 0)
            {
                errorInfo = "设置相机增益参数失败:Cmr = \"" + cmrName + "\",ErrorInfo = " + cmr.GetErrorInfo(ret);
                return false;
            }
            errorInfo = "Success";
            return true;
        }


        /// <summary>
        /// 设置相机增益参数（替身名）
        /// </summary>
        /// <param name="aliasName"></param>
        /// <param name="gan"></param>
        /// <param name="errorInfo"></param>
        /// <returns></returns>
        public bool SetCmrGainAlias(string aliasName, double gain, out string errorInfo)
        {
            if (!IsDevChnDecleared(NamedChnType.Camera, aliasName))
            {
                errorInfo = "设置相机增益参数失败，Alias:\"" + aliasName + "\"不存在！";
                return false;
            }
            string globName = GetDecChnGlobName(NamedChnType.Camera, aliasName);
            if (string.IsNullOrEmpty(globName))
            {
                errorInfo = "设置相机增益参数失败，Alias:\"" + aliasName + "\"" + " 未绑定全局设备名";
                return false;
            }

            if (!SetCmrGain(globName, gain, out errorInfo))
            {
                errorInfo = "设置相机增益参数失败，Alias:\"" + aliasName + "\"->" + errorInfo;
                return false;
            }

            return true;
        }

        /// <summary>
        /// 获取相机增益参数
        /// </summary>
        /// <param name="cmrName"></param>
        /// <param name="gain"></param>
        /// <param name="errorInfo"></param>
        /// <returns></returns>
        public bool GetCmrGain(string cmrName, out double gain, out string errorInfo)
        {
            AppInitorManager mgr = AppHubCenter.Instance.InitorManager;
            IPlatInitializable initor = mgr.GetInitor(cmrName);

            gain = 0;

            if (null == initor)
            {
                errorInfo = "获取相机增益参数失败,设备列表中未包含相机:" + cmrName;
                return false;
            }

            IPlatDevice_Camera cmr = initor as IPlatDevice_Camera;
            if (null == cmr)
            {
                errorInfo = "获取相机增益参数失败,设备列表中未包含相机:" + cmrName + ",Initor's realType = " + initor.GetType().Name;
                return false;
            }

            int ret = cmr.GetGain(out gain);
            if (ret != 0)
            {
                errorInfo = "获取相机增益参数失败:Cmr = \"" + cmrName + "\",ErrorInfo = " + cmr.GetErrorInfo(ret);
                return false;
            }
            errorInfo = "Success";
            return true;
        }


        /// <summary>
        /// 获取相机增益参数（替身名）
        /// </summary>
        /// <param name="aliasName"></param>
        /// <param name="gan"></param>
        /// <param name="errorInfo"></param>
        /// <returns></returns>
        public bool GetCmrGainAlias(string aliasName, out double gain, out string errorInfo)
        {
            gain = 0;

            if (!IsDevChnDecleared(NamedChnType.Camera, aliasName))
            {
                errorInfo = "获取相机增益参数失败，Alias:\"" + aliasName + "\"不存在！";
                return false;
            }
            string globName = GetDecChnGlobName(NamedChnType.Camera, aliasName);
            if (string.IsNullOrEmpty(globName))
            {
                errorInfo = "获取相机增益参数失败，Alias:\"" + aliasName + "\"" + " 未绑定全局设备名";
                return false;
            }

            if (!GetCmrGain(globName, out gain, out errorInfo))
            {
                errorInfo = "获取相机增益参数失败，Alias:\"" + aliasName + "\"->" + errorInfo;
                return false;
            }

            return true;
        }

        /// <summary>
        /// 注册相机采图回调函数
        /// </summary>
        /// <param name="cmrName"></param>
        /// <param name="callback"></param>
        /// <param name="errorInfo"></param>
        /// <returns></returns>
        public bool RegistCmrAcqFrameCallback(string cmrName, CmrAcqFrameDelegate callback, out string errorInfo)
        {
            AppInitorManager mgr = AppHubCenter.Instance.InitorManager;
            IPlatInitializable initor = mgr.GetInitor(cmrName);

            if (null == initor)
            {
                errorInfo = "注册相机回调函数失败,设备列表中未包含相机:" + cmrName;
                return false;
            }

            IPlatDevice_Camera cmr = initor as IPlatDevice_Camera;
            if (null == cmr)
            {
                errorInfo = "注册相机回调函数失败,设备列表中未包含相机:" + cmrName + ",Initor's realType = " + initor.GetType().Name;
                return false;
            }

            if (cmr.IsRegistAcqFrameCallback) //当前相机正在使用回调模式
            {
                if (cmr.IsGrabbing)
                {
                    cmr.StopGrab();
                }
                cmr.ClearAcqFrameCallback(); //改为取图模式
            }

            int ret = cmr.RegistAcqFrameCallback(callback);
            if (ret != 0)
            {
                errorInfo = "注册相机回调函数失败:Cmr = \"" + cmrName + "\",ErrorInfo = " + cmr.GetErrorInfo(ret);
                return false;
            }
            errorInfo = "Success";
            return true;
        }

        /// <summary>
        /// 注册相机采图回调函数（替身名）
        /// </summary>
        /// <param name="aliasName"></param>
        /// <param name="callback"></param>
        /// <param name="errorInfo"></param>
        /// <returns></returns>
        public bool RegistCmrAcqFrameCallbackAlias(string aliasName, CmrAcqFrameDelegate callback, out string errorInfo)
        {
            if (!IsDevChnDecleared(NamedChnType.Camera, aliasName))
            {
                errorInfo = "注册相机回调函数失败，Alias:\"" + aliasName + "\"不存在！";
                return false;
            }
            string globName = GetDecChnGlobName(NamedChnType.Camera, aliasName);
            if (string.IsNullOrEmpty(globName))
            {
                errorInfo = "注册相机回调函数失败，Alias:\"" + aliasName + "\"" + " 未绑定全局设备名";
                return false;
            }

            if (!RegistCmrAcqFrameCallback(globName, callback, out errorInfo))
            {
                errorInfo = "注册相机回调函数失败，Alias:\"" + aliasName + "\"->" + errorInfo;
                return false;
            }

            return true;
        }

       

        /// <summary>
        /// 软件采集一幅相机图片
        /// </summary>
        /// <returns></returns>
        public bool SnapCmrImage(string cmrName, out IPlat_Image img, out string errorInfo)
        {
            AppInitorManager mgr = AppHubCenter.Instance.InitorManager;
            IPlatInitializable initor = mgr.GetInitor(cmrName);
            if (null == initor)
            {
                img = null;
                errorInfo = "采图失败,设备列表中未包含相机:" + cmrName;
                return false;
            }

            IPlatDevice_Camera cmr = initor as IPlatDevice_Camera;
           
            if (null == cmr)
            {
                img = null;
                errorInfo = "采图失败,设备列表中未包含相机:" + cmrName + ",Initor's realType = " + initor.GetType().Name;
                return false;
            }

            int ret = cmr.GrabOne(out img, 2000);
            if (ret != 0)
            {
                errorInfo = "采图失败,Cmr = \"" + cmrName + "\", ErrorInfo:" + cmr.GetErrorInfo(ret);
                return false;
            }

            errorInfo = "Success";
            return true;
        }

        /// <summary>
        /// 通过站内名 拍一张图片
        /// </summary>
        /// <param name="aliasName"></param>
        /// <param name="img"></param>
        /// <param name="errorInfo"></param>
        /// <returns></returns>
        public bool SnapCmrImageAlias(string aliasName, out IPlat_Image img, out string errorInfo)
        {
            if (!IsDevChnDecleared(NamedChnType.Camera, aliasName))
            {
                img = null;
                errorInfo = "相机采图失败，Alias:\"" + aliasName + "\"不存在！";
                return false;
            }
            string globName = GetDecChnGlobName(NamedChnType.Camera, aliasName);
            if (string.IsNullOrEmpty(globName))
            {
                img = null;
                errorInfo = "相机采图失败，Alias:\"" + aliasName + "\"" + " 未绑定全局设备名";
                return false;
            }

            if (!SnapCmrImage(globName, out img, out errorInfo))
            {
                errorInfo = "相机采图失败，Alias:\"" + aliasName + "\"->" + errorInfo;
                return false;
            }

            return true;
        }

        #endregion


        /// <summary>
        /// 打开所有DIO设备
        /// </summary>
        /// <param name="errorInfo"></param>
        /// <returns></returns>
        public bool OpenAllDioDev(out string errorInfo)
        {
            string[] allDoNames = DONames;//工站包含的所有电机
            if (null != allDoNames)
                foreach (string doName in allDoNames)
                {
                    //打开电机所属设备
                    if (!OpenChnDevice(NamedChnType.Do, doName, out errorInfo))
                    {
                        errorInfo = "使能工站所有DIO失败:" + errorInfo;
                        return false;
                    }
                }
            string[] allDINames = DINames;
            if (null != allDINames)
                foreach (string diName in allDINames)
                {
                    //打开电机所属设备
                    if (!OpenChnDevice(NamedChnType.Di, diName, out errorInfo))
                    {
                        errorInfo = "使能工站所有DIO失败:" + errorInfo;
                        return false;
                    }
                }

            errorInfo = "Success";
            return true;
        }


        /// <summary>
        /// 轴 使能
        /// </summary>
        /// <param name="axisName"></param>
        /// <param name="isServOn"></param>
        /// <param name="errorInfo"></param>
        /// <returns></returns>
        public bool AxisServo(string axisName, bool isServOn, out string errorInfo)
        {
            IDevCellInfo ci = CheckAxisDevInfo(axisName, out errorInfo);
            if (null == ci)
            {
                errorInfo = "轴:\"" + axisName + "\"伺服" + (isServOn ? "使能" : "去使能") + "失败,ErrorInfo:" + errorInfo;
                return false;
            }
            int errCode = 0;
            if (isServOn)
                errCode = (AppHubCenter.Instance.InitorManager.GetInitor(ci.DeviceID) as IPlatDevice_MotionDaq).GetMc(ci.ModuleIndex).ServoOn(ci.ChannelIndex);
            else
                errCode = (AppHubCenter.Instance.InitorManager.GetInitor(ci.DeviceID) as IPlatDevice_MotionDaq).GetMc(ci.ModuleIndex).ServoOff(ci.ChannelIndex);
            if (errCode != 0)
            {
                errorInfo = "轴:\"" + axisName + "\"伺服" + (isServOn ? "使能" : "去使能") + "失败,ErrorInfo:" + (AppHubCenter.Instance.InitorManager.GetInitor(ci.DeviceID) as IPlatDevice_MotionDaq).GetMc(ci.ModuleIndex).GetErrorInfo(errCode);
                return false;
            }
            errorInfo = "Success";
            return true;
        }


        /// <summary>
        /// 工站方法，操作（站内声明的）轴伺服上电/断电
        /// </summary>
        /// <param name="axisLocName">站内声明的轴ID</param>
        /// <param name="isServOn"></param>
        /// <param name="errorInfo"></param>
        /// <returns></returns>
        public bool AxisServoByAlias(string aliasName, bool isServOn, out string errorInfo)
        {
            errorInfo = "Unknown-Error";
            if (string.IsNullOrEmpty(aliasName))
            {
                errorInfo = "站内轴名称参数为空";
                return false;
            }
            if (!IsDevChnDecleared(NamedChnType.Axis, aliasName))
            {
                errorInfo = "站内轴名称 =\"" + aliasName + "\"不是工站固有轴";
                return false;
            }

            string globAxisID = GetDecChnGlobName(NamedChnType.Axis, aliasName);
            if (string.IsNullOrEmpty(globAxisID))
            {
                errorInfo = "站内轴名称 =\"" + aliasName + "\"未绑定全局轴ID";
                return false;
            }

            string innerErrorInfo;
            if (!AxisServo(globAxisID, isServOn, out innerErrorInfo))
            {
                errorInfo = "站内轴:\"" + aliasName + "\"->" + innerErrorInfo;
                return false;
            }

            errorInfo = "Success";
            return true;
        }


        /// <summary>
        /// 轴电机归零
        /// </summary>
        /// <param name="axisName"></param>
        /// <param name="errorInfo"></param>
        /// <returns></returns>
        public bool AxisHome(string axisName, out string errorInfo)
        {
            errorInfo = "Success";
            IDevCellInfo ci = CheckAxisDevInfo(axisName, out errorInfo);
            if (null == ci)
            {
                errorInfo = "轴归零失败，Name = \"" + axisName + "\",ErrorInfo:" + errorInfo;
                return false;
            }

            if (!CheckAxisCanMove(axisName, out errorInfo))
            {
                errorInfo = "轴归零失败，Name = \"" + axisName + "\",ErrorInfo:" + errorInfo;
                return false;
            }
            int errCode = (AppHubCenter.Instance.InitorManager.GetInitor(ci.DeviceID) as IPlatDevice_MotionDaq).GetMc(ci.ModuleIndex).Home(ci.ChannelIndex);

            if (errCode != 0)
            {
                errorInfo = "轴归零失败，Name = \"" + axisName + "\",ErrorInfo:" + (AppHubCenter.Instance.InitorManager.GetInitor(ci.DeviceID) as IPlatDevice_MotionDaq).GetMc(ci.ModuleIndex).GetErrorInfo(errCode);
                return false;
            }
            return true;
        }


        public bool AxisHomeByAlias(string aliasName, out string errorInfo)
        {
            string axisName = GetDecChnGlobName(NamedChnType.Axis, aliasName);
            if (string.IsNullOrEmpty(axisName))
            {
                errorInfo = "轴归零运动失败，AliasName = \"" + aliasName + "\",未指定轴ID";
            }
            bool ret = AxisHome(axisName, out errorInfo);
            if (!ret)
                errorInfo = "AliasName:\"" + aliasName + "\"" + errorInfo;
            return ret;
        }


        public ICmdResult WaitAxisHomeDone(string axisName, int timeoutMilliSeconds = -1)
        {
            string errorInfo = "";
            IDevCellInfo ci = CheckAxisDevInfo(axisName, out errorInfo);
            if (null == ci)
                return ICmdResult.UnknownError;
            IPlatModule_Motion md = (AppHubCenter.Instance.InitorManager.GetInitor(ci.DeviceID) as IPlatDevice_MotionDaq).GetMc(ci.ModuleIndex);
            DateTime startTime = DateTime.Now;
            bool isDone = false;
            while (true)
            {

                if (0 != md.IsHomeDone(ci.ChannelIndex, out isDone))
                    return ICmdResult.UnknownError;
                if (isDone)
                    return ICmdResult.Success;
                if (timeoutMilliSeconds >= 0)
                {
                    TimeSpan ts = DateTime.Now - startTime;
                    if (ts.TotalMilliseconds >= timeoutMilliSeconds)
                        return ICmdResult.Timeout;
                }
                if (IsInWorkThread())
                {
                    if (CheckCmd(CycleMilliseconds) == CCRet.Resume)
                        startTime = DateTime.Now;
                }
                else
                    Thread.Sleep(CycleMilliseconds);//响应退出指令
            }

        }

        public ICmdResult WaitAxisHomeDoneByAlias(string aliasName, int timeoutMilliSeconds = -1)
        {
            string axisName = GetDecChnGlobName(NamedChnType.Axis, aliasName);
            if (string.IsNullOrEmpty(axisName))
            {
                return ICmdResult.UnknownError;
            }
            return WaitAxisHomeDone(axisName, timeoutMilliSeconds);
        }



        bool CheckAxisCanMove(IPlatModule_Motion md, int axisIndex, out string errorInfo)
        {
            bool[] axisStatus;
            int errRet = md.GetMotionStatus(axisIndex, out axisStatus);
            if (0 != errRet)
            {
                errorInfo = "获取轴状态失败";
                return false;
            }
            bool isServOn = md.IsSVO(axisIndex); //测试查看
            if (!axisStatus[md.MSID_SVO])
            {
                errorInfo = "轴伺服未使能";
                return false;
            }
            if (axisStatus[md.MSID_ALM])
            {
                errorInfo = "轴已报警";
                return false;
            }

            errorInfo = "";
            return true;
        }

        /// <summary>
        /// 以速度模式移动指定轴（全局ID）
        /// </summary>
        /// <param name="axisName"></param>
        /// <param name="isPositive"></param>
        /// <param name="errorInfo"></param>
        /// <returns></returns>
        public bool MoveJogAxis(string axisName, bool isPositive, out string errorInfo)
        {

            IDevCellInfo ci = CheckAxisDevInfo(axisName, out errorInfo);
            if (null == ci)
            {
                errorInfo = "轴速度运动失败，Name = \"" + axisName + "\",ErrorInfo:" + errorInfo;
                return false;
            }

            if (!CheckAxisCanMove(axisName, out errorInfo))
            {
                errorInfo = "轴速度运动失败，Name = \"" + axisName + "\",ErrorInfo:" + errorInfo;
                return false;
            }
            IPlatModule_Motion md = (AppHubCenter.Instance.InitorManager.GetInitor(ci.DeviceID) as IPlatDevice_MotionDaq).GetMc(ci.ModuleIndex);
            bool[] mcStatus = null;
            int nRet = md.GetMotionStatus(ci.ChannelIndex, out mcStatus);
            if (0 != nRet)
            {
                errorInfo = "轴速度运动失败，Name = \"" + axisName + "\",未能获取轴当前运动状态:";
                return false;
            }
            if (isPositive)
            {
                //if(mcStatus[md.MSID_PL])
                //{
                //    errorInfo = "Success";
                //    return true;
                //}

                //if(md.MSID_SPL >-1 && mcStatus[md.MSID_SPL]) //轴电机支持软正限位
                //{
                //    errorInfo = "Success:软正限位被触发";
                //    return true;
                //}


            }
            else
            {
                //if (mcStatus[md.MSID_NL])
                //{
                //    errorInfo = "Success";
                //    return true;
                //}

                //if (md.MSID_SNL > -1 && mcStatus[md.MSID_SNL]) //轴电机支持软负限位
                //{
                //    errorInfo = "Success:软负限位被触发";
                //    return true;
                //}
            }


            MotionParam mp;

            int errorCode = md.GetMotionParam(ci.ChannelIndex, out mp);
            if (errorCode != 0)
            {
                errorInfo = "轴速移动度失败，Name = \"" + axisName + "\",ErrorInfo:" + md.GetErrorInfo(errorCode);
                return false;
            }


            errorCode = md.Jog(ci.ChannelIndex, mp.vm, isPositive);

            if (errorCode != 0)
            {
                errorInfo = "轴速移动度失败，Name = \"" + axisName + "\",ErrorInfo:" + md.GetErrorInfo(errorCode);
                return false;
            }
            errorInfo = "Success";
            return true;
        }


        /// <summary>
        /// 以速度模式移动指定轴（替身ID）
        /// </summary>
        /// <param name="aliasName"></param>
        /// <param name="isPositive"></param>
        /// <param name="errorInfo"></param>
        /// <returns></returns>
        public bool MoveVelAxisByAlias(string aliasName, bool isPositive, out string errorInfo)
        {
            string axisName = GetDecChnGlobName(NamedChnType.Axis, aliasName);
            if (string.IsNullOrEmpty(axisName))
            {
                errorInfo = "轴速度运动失败，AliasName = \"" + aliasName + "\",未指定轴ID";
            }
            bool ret = MoveJogAxis(axisName, isPositive, out errorInfo);
            if (!ret)
                errorInfo = "AliasName:\"" + aliasName + "\"" + errorInfo;
            return ret;
        }



        public bool MoveToPosition(IMultiAxisProPos pos, out string errorInfo)
        {
            errorInfo = "Success";
            string[] axisNamesInPos = pos.AxisNames;
            if (null == axisNamesInPos || 0 == axisNamesInPos.Length)
            {
                errorInfo = "点位中不包含轴/电机";
                return false;
            }

            string[] AxisNamesInStation = AxisNames;
            if (AxisNamesInStation == null || AxisNamesInStation.Length == 0)
            {
                errorInfo = "工站配置中没有轴/电机";
                return false;
            }
            List<IDevCellInfo> lstAxisCIs = new List<IDevCellInfo>();
            foreach (string axisNameInPos in axisNamesInPos)
            {
                IDevCellInfo ci = CheckAxisDevInfo(axisNameInPos, out errorInfo);
                if (null == ci)
                {
                    errorInfo = errorInfo = "轴移动失败 Name = \"" + axisNameInPos + "\",ErrorInfo :" + errorInfo;
                    return false;
                }
                lstAxisCIs.Add(ci);
            }

            bool isAllAxisOK = true;
            int i = 0;
            for (i = 0; i < lstAxisCIs.Count; i++)
            {
                IPlatModule_Motion md = (AppHubCenter.Instance.InitorManager.GetInitor(lstAxisCIs[i].DeviceID) as IPlatDevice_MotionDaq).GetMc(lstAxisCIs[i].ModuleIndex);
                if (!CheckAxisCanMove(md, lstAxisCIs[i].ChannelIndex, out errorInfo))
                {
                    errorInfo = "轴移动失败 Name = \"" + axisNamesInPos[i] + "\",ErrorInfo :" + errorInfo;
                    isAllAxisOK = false;
                    break;
                }

                int errCode = md.AbsMove(lstAxisCIs[i].ChannelIndex, pos.GetAxisPos(axisNamesInPos[i]));
                if (0 != errCode)
                {
                    errorInfo = "轴移动失败 Name = \"" + axisNamesInPos[i] + "\",ErrorInfo :" + md.GetErrorInfo(errCode);
                    isAllAxisOK = false;
                    break;
                }
            }
            if (!isAllAxisOK)
            {
                for (int j = 0; j < i; j++)
                    (AppHubCenter.Instance.InitorManager.GetInitor(lstAxisCIs[j].DeviceID) as IPlatDevice_MotionDaq).GetMc(lstAxisCIs[j].ModuleIndex).StopAxis(lstAxisCIs[j].ChannelIndex);
                return false;
            }
            return true;

        }


        public bool WaitToPosition(IMultiAxisProPos pos, out string errorInfo, int timeoutMilliSeconds = -1)
        {
            errorInfo = "Success";
            string[] axisNamesInPos = pos.AxisNames;
            if (null == axisNamesInPos || 0 == axisNamesInPos.Length)
            {
                errorInfo = "点位中不包含轴/电机";
                return false;
            }

            _axisMotionDoneDelay = (int)GetCfgParamValue("轴运动到位稳定时间(毫秒)");
            if (_axisMotionDoneDelay > 0 && IsInWorkThread())
                CheckCmd(_axisMotionDoneDelay);
            else
                Thread.Sleep(_axisMotionDoneDelay);

            string[] AxisNamesInStation = AxisNames;
            if (AxisNamesInStation == null || AxisNamesInStation.Length == 0)
            {
                errorInfo = "工站配置中没有轴/电机";
                return false;
            }
            List<IDevCellInfo> lstAxisCIs = new List<IDevCellInfo>();
            List<IPlatModule_Motion> lstMD = new List<IPlatModule_Motion>();
            foreach (string axisNameInPos in axisNamesInPos)
            {
                IDevCellInfo ci = CheckAxisDevInfo(axisNameInPos, out errorInfo);
                if (null == ci)
                {
                    errorInfo = errorInfo = "等待轴运动到点位失败,轴检测错误: Name = \"" + axisNameInPos + "\",ErrorInfo :" + errorInfo;
                    return false;
                }
                lstAxisCIs.Add(ci);
                lstMD.Add((AppHubCenter.Instance.InitorManager.GetInitor(ci.DeviceID) as IPlatDevice_MotionDaq).GetMc(ci.ModuleIndex));
            }

            DateTime startTime = DateTime.Now;
            while (true)
            {
                bool isAllAxisDone = true;
                for (int i = 0; i < lstAxisCIs.Count; i++)
                {
                    bool[] mcStatus = null; //轴所有运动状态
                    int nOpt = lstMD[i].GetMotionStatus(lstAxisCIs[i].ChannelIndex, out mcStatus);
                    if (nOpt != 0)
                    {
                        errorInfo = "等待轴运动到点位失败,获取轴状态失败:" + lstMD[i].GetErrorInfo(nOpt);
                        return false;
                    }

                    if (lstMD[i].MSID_ALM >= 0 && mcStatus[lstMD[i].MSID_ALM]) //轴电机报警
                    {
                        errorInfo = "等待轴运动到点位失败,轴电机:\"" + axisNamesInPos[i] + "\"报警!";
                        return false;
                    }

                    if (lstMD[i].MSID_SVO >= 0 && !mcStatus[lstMD[i].MSID_SVO]) //轴电机失电
                    {
                        errorInfo = "等待轴运动到点位失败,轴电机:\"" + axisNamesInPos[i] + "\"伺服失电!";
                        return false;
                    }

                    if (lstMD[i].MSID_EMG >= 0)//轴急停被触发
                    {
                        if (mcStatus[lstMD[i].MSID_EMG])
                        {
                            errorInfo = "等待轴运动到点位失败,轴电机:\"" + axisNamesInPos[i] + "\"急停被触发!";
                            return false;
                        }
                    }                  

                    if (!mcStatus[lstMD[i].MSID_MDN] || !mcStatus[lstMD[i].MSID_INP])
                    {
                        isAllAxisDone = false;
                        errorInfo = "等待轴运动到点位失败,轴电机:\"" + i + mcStatus[lstMD[i].MSID_MDN] + mcStatus[lstMD[i].MSID_INP] + "\"急停被触发!";
                        break;
                    }

                }

                if (isAllAxisDone)
                {
                    errorInfo = "Success";
                    return true;
                }

                if (timeoutMilliSeconds >= 0)
                {
                    TimeSpan ts = DateTime.Now - startTime;
                    if (ts.TotalMilliseconds >= timeoutMilliSeconds)
                    {
                        errorInfo += "等待轴运动到点位失败,超时未完成";
                        return false;
                    }
                }


                if (CheckCmd(CycleMilliseconds) == CCRet.Resume)
                    startTime = DateTime.Now;
            }
        }

        public ICmdResult WaitMotionDone(string axisName, int milliSenconds = -1)
        {
            string errorInfo = "";
            IDevCellInfo ci = CheckAxisDevInfo(axisName, out errorInfo);
            if (null == ci)
                return ICmdResult.UnknownError;

            _axisMotionDoneDelay = (int)GetCfgParamValue("轴运动到位稳定时间(毫秒)");
            if (_axisMotionDoneDelay > 0 && IsInWorkThread())
                CheckCmd(_axisMotionDoneDelay);
            else
                Thread.Sleep(_axisMotionDoneDelay);

            DateTime startTime = DateTime.Now;
            IPlatModule_Motion md = (AppHubCenter.Instance.InitorManager.GetInitor(ci.DeviceID) as IPlatDevice_MotionDaq).GetMc(ci.ModuleIndex);
            while (true)
            {
                if (md.IsMDN(ci.ChannelIndex) && md.IsINP(ci.ChannelIndex))
                {
                    errorInfo = "Success";
                    return ICmdResult.Success;
                }
                if (IsInWorkThread())
                {
                    if (CheckCmd(CycleMilliseconds) == CCRet.Resume)
                        startTime = DateTime.Now;
                }
                if (milliSenconds >= 0)
                {
                    TimeSpan ts = DateTime.Now - startTime;
                    if (milliSenconds >= 0 && ts.TotalMilliseconds >= milliSenconds)
                        return ICmdResult.Timeout;
                }

            }
        }


        public ICmdResult WaitMotionDoneByAlias(string axisAliasName, int milliSenconds = -1)
        {
            string gName = GetDecChnGlobName(NamedChnType.Axis, axisAliasName);
            return WaitMotionDone(gName, milliSenconds);
        }

        /// <summary>
        /// 同时等待多个轴运动完成信号
        /// </summary>
        /// <param name="axisNames"></param>
        /// <param name="milliSeconds"></param>
        /// <returns></returns>
        public ICmdResult WaitMotionDones(string[] axisNames, int milliSeconds = -1)
        {
            if (null == axisNames || 0 == axisNames.Length)
                throw new ArgumentNullException();
            string errorInfo = "";
            IDevCellInfo[] cis = new IDevCellInfo[axisNames.Length];//
            IPlatModule_Motion[] mds = new IPlatModule_Motion[axisNames.Length];
            for (int i = 0; i < axisNames.Length; i++)
            {
                cis[i] = CheckAxisDevInfo(axisNames[i], out errorInfo);
                if (null == cis)
                    return ICmdResult.UnknownError;
                mds[i] = (AppHubCenter.Instance.InitorManager.GetInitor(cis[i].DeviceID) as IPlatDevice_MotionDaq).GetMc(cis[i].ModuleIndex);
            }
            DateTime startTime = DateTime.Now;
            while (true)
            {
                if (IsInWorkThread())
                {
                    if (CheckCmd(CycleMilliseconds) == CCRet.Resume)
                        startTime = DateTime.Now;
                }
                else
                    Thread.Sleep(CycleMilliseconds);

                bool isAllDone = true;
                for (int i = 0; i < cis.Length; i++)
                {
                    if (!mds[i].IsMDN(cis[i].ChannelIndex) || !mds[i].IsINP(cis[i].ChannelIndex))
                    {
                        isAllDone = false;
                        break;
                    }
                }

                if (isAllDone)
                    return ICmdResult.Success;


                if (milliSeconds >= 0)
                {
                    TimeSpan ts = DateTime.Now - startTime;
                    if (ts.TotalMilliseconds >= milliSeconds)
                        return ICmdResult.Timeout;
                }

            }
        }

        /// <summary>
        /// 同时等待多个轴运动完成信号（替身名）
        /// </summary>
        /// <returns></returns>
        public ICmdResult WaitMotionDonesByAlias(string[] aliasNames, int milliSeconds = -1)
        {
            if (null == aliasNames || 0 == aliasNames.Length)
                return ICmdResult.UnknownError;
            string[] gns = new string[aliasNames.Length];
            for (int i = 0; i < aliasNames.Length; i++)
            {
                gns[i] = GetDecChnGlobName(NamedChnType.Axis, aliasNames[i]);
                if (string.IsNullOrEmpty(gns[i]))
                    return ICmdResult.UnknownError;
            }
            return WaitMotionDones(gns, milliSeconds);
        }

        public bool WaitToWorkPosition(string posName, out string errorInfo, int timeoutMilliSeconds = -1)
        {
            string[] workPosNames = WorkPositionNames;
            if (WorkPositionNames == null || !WorkPositionNames.Contains(posName))
            {
                errorInfo = "等待到工作点位失败:无工作点位Name = " + posName;
                return false;
            }

            IMultiAxisProPos pos = GetWorkPosition(posName);
            bool ret = WaitToPosition(pos, out errorInfo, timeoutMilliSeconds);
            if (!ret)
            {
                errorInfo = "等待到运动点位失败，点位名称:\"" + posName + "\",Error:" + errorInfo;
                return false;
            }

            errorInfo = "Success";
            return true;
        }



        /// <summary>
        /// 单轴运动(PTP)
        /// </summary>
        /// <param name="axisName"></param>
        /// <param name="pos"></param>
        /// <param name=""></param>
        /// <returns></returns>
        public bool MoveAxis(string axisName, double pos, bool isAbs, out string errorInfo)
        {
            errorInfo = "Success";
            IDevCellInfo ci = CheckAxisDevInfo(axisName, out errorInfo);
            if (null == ci)
            {
                errorInfo = "轴移动失败，Name = \"" + axisName + "\",ErrorInfo:" + errorInfo;
                return false;
            }

            if (!CheckAxisCanMove(axisName, out errorInfo))
            {
                errorInfo = "轴移动失败，Name = \"" + axisName + "\",ErrorInfo:" + errorInfo;
                return false;
            }
            int errCode = 0;
            if (isAbs)
                errCode = (AppHubCenter.Instance.InitorManager.GetInitor(ci.DeviceID) as IPlatDevice_MotionDaq).GetMc(ci.ModuleIndex).AbsMove(ci.ChannelIndex, pos);
            else
                errCode = (AppHubCenter.Instance.InitorManager.GetInitor(ci.DeviceID) as IPlatDevice_MotionDaq).GetMc(ci.ModuleIndex).RelMove(ci.ChannelIndex, pos);

            if (errCode != 0)
            {
                errorInfo = "轴移动失败，Name = \"" + axisName + "\",ErrorInfo:" + (AppHubCenter.Instance.InitorManager.GetInitor(ci.DeviceID) as IPlatDevice_MotionDaq).GetMc(ci.ModuleIndex).GetErrorInfo(errCode);
                return false;
            }
            return true;
        }

        /// <summary>
        /// 单轴运动(PTP)
        /// </summary>
        /// <param name="axisName"></param>
        /// <param name="pos"></param>
        /// <param name=""></param>
        /// <returns></returns>
        public bool MoveAxisAndWait(string axisName, double pos, bool isAbs, out string errorInfo)
        {
            errorInfo = "Success";
            IDevCellInfo ci = CheckAxisDevInfo(axisName, out errorInfo);
            if (null == ci)
            {
                errorInfo = "轴移动失败，Name = \"" + axisName + "\",ErrorInfo:" + errorInfo;
                return false;
            }

            if (!CheckAxisCanMove(axisName, out errorInfo))
            {
                errorInfo = "轴移动失败，Name = \"" + axisName + "\",ErrorInfo:" + errorInfo;
                return false;
            }
            int errCode = 0;
            if (isAbs)
                errCode = (AppHubCenter.Instance.InitorManager.GetInitor(ci.DeviceID) as IPlatDevice_MotionDaq).GetMc(ci.ModuleIndex).AbsMove(ci.ChannelIndex, pos);
            else
                errCode = (AppHubCenter.Instance.InitorManager.GetInitor(ci.DeviceID) as IPlatDevice_MotionDaq).GetMc(ci.ModuleIndex).RelMove(ci.ChannelIndex, pos);

            if (errCode != 0)
            {
                errorInfo = "轴移动失败，Name = \"" + axisName + "\",ErrorInfo:" + (AppHubCenter.Instance.InitorManager.GetInitor(ci.DeviceID) as IPlatDevice_MotionDaq).GetMc(ci.ModuleIndex).GetErrorInfo(errCode);
                return false;
            }
            if (ICmdResult.Success != WaitMotionDone(axisName))
                return false;
            return true;
        }


        public bool MoveAxisByAlias(string axisAliasName, double pos, bool isAbs, out string errorInfo)
        {
            if (string.IsNullOrEmpty(axisAliasName))
            {
                errorInfo = "MoveAxisByAlias(axisAliasName...) failed by axisAliasName is null or empty";
                return false;
            }

            string gName = GetDecChnGlobName(NamedChnType.Axis, axisAliasName);
            if (string.IsNullOrEmpty(gName))
            {
                errorInfo = "MoveAxisByAlias(axisAliasName = \"" + axisAliasName + "\" failed by UnBind Global Channel";
                return false;
            }
            return MoveAxis(gName, pos, isAbs, out errorInfo);
        }

        public bool MoveAxisAndWaitByAlias(string axisAliasName, double pos, bool isAbs, out string errorInfo)
        {
            if (string.IsNullOrEmpty(axisAliasName))
            {
                errorInfo = "MoveAxisByAlias(axisAliasName...) failed by axisAliasName is null or empty";
                return false;
            }

            string gName = GetDecChnGlobName(NamedChnType.Axis, axisAliasName);
            if (string.IsNullOrEmpty(gName))
            {
                errorInfo = "MoveAxisByAlias(axisAliasName = \"" + axisAliasName + "\" failed by UnBind Global Channel";
                return false;
            }
            return MoveAxisAndWait(gName, pos, isAbs, out errorInfo);
        }



        public List<IMultiAxisProPos> WorkPositions { get { return (_cfg.GetItemValue("StationBasePrivateConfig") as DictionaryEx<string, object>)["WorkPositions"] as List<IMultiAxisProPos>; } }



        /// <summary>
        /// 获取轴的运动参数:脉冲当量
        /// </summary>
        /// <param name="axisName"></param>
        /// <param name="factor"></param>
        /// <param name="errorInfo"></param>
        /// <returns></returns>
        public bool GetAxisPulseFactor(string axisName, out double factor, out string errorInfo)
        {
            factor = 0;
            errorInfo = "Unknown-Error";
            IDevCellInfo axisCellInfo = AppHubCenter.Instance.MDCellNameMgr.GetAxisCellInfo(axisName);
            if (null == axisCellInfo)
            {
                errorInfo = string.Format("轴名称:\"{0}\"在配置表中不存在！", axisName);
                return false;
            }

            if (!AppHubCenter.Instance.InitorManager.ContainID(axisCellInfo.DeviceID))
            {
                errorInfo = string.Format("轴:\"{0}\"所属设备\"{1}\"在设备管理器中不存在!", axisName, axisCellInfo.DeviceID);
                return false;
            }
            IPlatDevice_MotionDaq dev = AppHubCenter.Instance.InitorManager.GetInitor(axisCellInfo.DeviceID) as IPlatDevice_MotionDaq;
            if (!dev.IsDeviceOpen)
            {
                errorInfo = string.Format("轴:\"{0}\"所属设备\"{1}\"未打开!", axisName, axisCellInfo.DeviceID);
                return false;
            }
            if (axisCellInfo.ModuleIndex >= dev.McMCount)
            {
                errorInfo = string.Format("轴:\"{0}\"所属模块序号\"{1}超出限制:0~{2}\"", axisName, axisCellInfo.ModuleIndex, dev.McMCount - 1);
                return false;
            }

            IPlatModule_Motion motion = dev.GetMc(axisCellInfo.ModuleIndex);
            if (axisCellInfo.ChannelIndex >= motion.AxisCount)
            {
                errorInfo = string.Format("轴:\"{0}\"通道序号\"{1}超出限制:0~{2}\"", axisName, axisCellInfo.ChannelIndex, motion.AxisCount - 1);
                return false;
            }

            int errorCode = motion.GetPulseFactor(axisCellInfo.ChannelIndex, out factor);
            if (0 == errorCode)
            {
                errorInfo = "Success";
                return true;
            }

            errorInfo = string.Format("获取轴:\"{0}\"脉冲当量失败：ErrorInfo:{1}", axisName, motion.GetErrorInfo(errorCode));
            return false;
        }

        /// <summary>
        /// 获取轴的运动参数:脉冲当量（使用替身名）
        /// </summary>
        /// <param name="axisName"></param>
        /// <param name="factor"></param>
        /// <param name="errorInfo"></param>
        /// <returns></returns>
        public bool GetAxisPulseFactorByAlias(string aliasName, out double factor, out string errorInfo)
        {
            errorInfo = "Unknown error";
            factor = 0;
            string gName = GetDecChnGlobName(NamedChnType.Axis, aliasName);
            if (string.IsNullOrEmpty(gName))
            {
                errorInfo = "替身名：" + aliasName + "未绑定通道设备";
                return false;
            }
            return GetAxisPulseFactor(gName, out factor, out errorInfo);

        }

        /// <summary>
        /// 获取轴的运动参数:速度
        /// </summary>
        /// <param name="axisName"></param>
        /// <param name="spd"></param>
        /// <param name="errorInfo"></param>
        /// <returns></returns>
        public bool GetAxisMPSpeed(string axisName, out double spd, out string errorInfo)
        {
            spd = 0;
            errorInfo = "Unknown-Error";
            IDevCellInfo axisCellInfo = AppHubCenter.Instance.MDCellNameMgr.GetAxisCellInfo(axisName);
            if (null == axisCellInfo)
            {
                errorInfo = string.Format("轴名称:\"{0}\"在配置表中不存在！", axisName);
                return false;
            }

            if (!AppHubCenter.Instance.InitorManager.ContainID(axisCellInfo.DeviceID))
            {
                errorInfo = string.Format("轴:\"{0}\"所属设备\"{1}\"在设备管理器中不存在!", axisName, axisCellInfo.DeviceID);
                return false;
            }
            IPlatDevice_MotionDaq dev = AppHubCenter.Instance.InitorManager.GetInitor(axisCellInfo.DeviceID) as IPlatDevice_MotionDaq;
            if (!dev.IsDeviceOpen)
            {
                errorInfo = string.Format("轴:\"{0}\"所属设备\"{1}\"未打开!", axisName, axisCellInfo.DeviceID);
                return false;
            }
            if (axisCellInfo.ModuleIndex >= dev.McMCount)
            {
                errorInfo = string.Format("轴:\"{0}\"所属模块序号\"{1}超出限制:0~{2}\"", axisName, axisCellInfo.ModuleIndex, dev.McMCount - 1);
                return false;
            }

            IPlatModule_Motion motion = dev.GetMc(axisCellInfo.ModuleIndex);
            if (axisCellInfo.ChannelIndex >= motion.AxisCount)
            {
                errorInfo = string.Format("轴:\"{0}\"通道序号\"{1}超出限制:0~{2}\"", axisName, axisCellInfo.ChannelIndex, motion.AxisCount - 1);
                return false;
            }


            MotionParam mp;

            int errorCode = motion.GetMotionParam(axisCellInfo.ChannelIndex, out mp);
            if (0 == errorCode)
            {
                errorInfo = "Success";
                spd = mp.vm;
                return true;
            }

            errorInfo = string.Format("获取轴:\"{0}\"速度失败：ErrorInfo:{1}", axisName, motion.GetErrorInfo(errorCode));
            return false;
        }

        /// <summary>
        /// 获取轴的运动参数:速度（使用替身名）
        /// </summary>
        /// <param name="axisName"></param>
        /// <param name="spd"></param>
        /// <param name="errorInfo"></param>
        /// <returns></returns>
        public bool GetAxisMPSpeedByAlias(string aliasName, out double spd, out string errorInfo)
        {
            errorInfo = "Unknown error";
            spd = 0;
            string gName = GetDecChnGlobName(NamedChnType.Axis, aliasName);
            if (string.IsNullOrEmpty(gName))
            {
                errorInfo = "替身名：" + aliasName + "未绑定通道设备";
                return false;
            }
            return GetAxisMPSpeed(gName, out spd, out errorInfo);

        }

        /// <summary>
        /// 设置轴运行速度
        /// </summary>
        /// <param name="axisName"></param>
        /// <param name="spd"></param>
        /// <param name="errorInfo"></param>
        /// <returns></returns>
        public bool SetAxisMPSpeed(string axisName, double spd, out string errorInfo)
        {
            //spd = 0;
            errorInfo = "Unknown-Error";
            IDevCellInfo axisCellInfo = AppHubCenter.Instance.MDCellNameMgr.GetAxisCellInfo(axisName);
            if (null == axisCellInfo)
            {
                errorInfo = string.Format("轴名称:\"{0}\"在配置表中不存在！", axisName);
                return false;
            }

            if (!AppHubCenter.Instance.InitorManager.ContainID(axisCellInfo.DeviceID))
            {
                errorInfo = string.Format("轴:\"{0}\"所属设备\"{1}\"在设备管理器中不存在!", axisName, axisCellInfo.DeviceID);
                return false;
            }
            IPlatDevice_MotionDaq dev = AppHubCenter.Instance.InitorManager.GetInitor(axisCellInfo.DeviceID) as IPlatDevice_MotionDaq;
            if (!dev.IsDeviceOpen)
            {
                errorInfo = string.Format("轴:\"{0}\"所属设备\"{1}\"未打开!", axisName, axisCellInfo.DeviceID);
                return false;
            }
            if (axisCellInfo.ModuleIndex >= dev.McMCount)
            {
                errorInfo = string.Format("轴:\"{0}\"所属模块序号\"{1}超出限制:0~{2}\"", axisName, axisCellInfo.ModuleIndex, dev.McMCount - 1);
                return false;
            }

            IPlatModule_Motion motion = dev.GetMc(axisCellInfo.ModuleIndex);
            if (axisCellInfo.ChannelIndex >= motion.AxisCount)
            {
                errorInfo = string.Format("轴:\"{0}\"通道序号\"{1}超出限制:0~{2}\"", axisName, axisCellInfo.ChannelIndex, motion.AxisCount - 1);
                return false;
            }


            MotionParam mp;

            int errorCode = motion.GetMotionParam(axisCellInfo.ChannelIndex, out mp);
            if (0 != errorCode)
            {
                errorInfo = string.Format("获取轴:\"{0}\"运动参数失败：ErrorInfo:{1}", axisName, motion.GetErrorInfo(errorCode));
                return false;
            }
            mp.vm = spd;
            errorCode = motion.SetMotionParam(axisCellInfo.ChannelIndex, mp);
            if (0 != errorCode)
            {
                errorInfo = string.Format("设置轴:\"{0}\"运动参数失败：ErrorInfo:{1}", axisName, motion.GetErrorInfo(errorCode));
                return false;
            }
            errorInfo = "Success";
            return true;

        }

        /// <summary>
        /// 设置轴运行速度（替身名）
        /// </summary>
        /// <param name="aliasName"></param>
        /// <param name="spd"></param>
        /// <param name="errorInfo"></param>
        /// <returns></returns>
        public bool SetAxisMPSpeedByAlias(string aliasName, double spd, out string errorInfo)
        {
            string gName = GetDecChnGlobName(NamedChnType.Axis, aliasName);
            if (string.IsNullOrEmpty(gName))
            {
                errorInfo = "替身名：" + aliasName + "未绑定通道设备";
                return false;
            }
            return SetAxisMPSpeed(gName, spd, out errorInfo);
        }






        /// <summary>
        /// 获取轴(电机)当前的实际位置
        /// </summary>
        public bool GetAxisPosition(string axisName, out double dPos, out string errorInfo)
        {
            dPos = 0;
            errorInfo = "Unknown-Error";
            IDevCellInfo axisCellInfo = AppHubCenter.Instance.MDCellNameMgr.GetAxisCellInfo(axisName);
            if (null == axisCellInfo)
            {
                errorInfo = string.Format("轴名称:\"{0}\"在配置表中不存在！", axisName);
                return false;
            }

            if (!AppHubCenter.Instance.InitorManager.ContainID(axisCellInfo.DeviceID))
            {
                errorInfo = string.Format("轴:\"{0}\"所属设备\"{1}\"在设备管理器中不存在!", axisName, axisCellInfo.DeviceID);
                return false;
            }
            IPlatDevice_MotionDaq dev = AppHubCenter.Instance.InitorManager.GetInitor(axisCellInfo.DeviceID) as IPlatDevice_MotionDaq;
            if (!dev.IsDeviceOpen)
            {
                errorInfo = string.Format("轴:\"{0}\"所属设备\"{1}\"未打开!", axisName, axisCellInfo.DeviceID);
                return false;
            }
            if (axisCellInfo.ModuleIndex >= dev.McMCount)
            {
                errorInfo = string.Format("轴:\"{0}\"所属模块序号\"{1}超出限制:0~{2}\"", axisName, axisCellInfo.ModuleIndex, dev.McMCount - 1);
                return false;
            }

            IPlatModule_Motion motion = dev.GetMc(axisCellInfo.ModuleIndex);
            if (axisCellInfo.ChannelIndex >= motion.AxisCount)
            {
                errorInfo = string.Format("轴:\"{0}\"通道序号\"{1}超出限制:0~{2}\"", axisName, axisCellInfo.ChannelIndex, motion.AxisCount - 1);
                return false;
            }

            int errorCode = motion.GetEncPos(axisCellInfo.ChannelIndex, out dPos);
            if (0 == errorCode)
            {
                errorInfo = "Success";
                return true;
            }

            errorInfo = string.Format("获取轴:\"{0}\"位置失败：ErrorInfo:{1}", axisName, motion.GetErrorInfo(errorCode));
            return false;
        }


        //public abstract string[] InternalMethodFlowNames();

        //FormStationBase _stationForm = new FormStationBase();

        /// <summary>
        /// 用于单独调试工站的窗口界面(不建议以Dialog模式显示)
        /// </summary>
        /// <returns></returns>
        public virtual Form GenForm()
        {
            //FormStationBaseDebug fm = new FormStationBaseDebug();
            //fm.SetStation(this);
            //return fm;

            return null;
        }





        /// <summary>
        /// 存放继承类中声明的参数配置 ， 
        /// Key = ItemName
        /// Value = <"参数类别",参数类型> 
        /// </summary>
        Dictionary<string, object[]> dictCfgParamDecleared = new Dictionary<string, object[]>();
        /// <summary>
        /// 供继承类申明需要的序列化参数
        /// 只在继承类的构造函数中使用
        /// </summary>
        /// <param name="paramName"></param>
        /// <param name="paramType"></param>
        public void DeclearCfgParam(string cfgName, Type cfgType, string category = "StationConfig")
        {
            if (string.IsNullOrEmpty(cfgName))
                throw new ArgumentNullException("声明的配置参数名称为空 in DeclearCfgParam()");
            if (cfgName == "StationBasePrivateConfig")
                throw new ArgumentException("声明的配置参数名称不能为:\"StationBasePrivateConfig\" in DeclearCfgParam()");
            if (string.IsNullOrEmpty(category))
                throw new ArgumentException("声明的配置参数类别名不能为空 in DeclearCfgParam()");
            if (dictCfgParamDecleared.ContainsKey(cfgName))
                throw new ArgumentException("重复申明配置参数,Name = " + cfgName);
            dictCfgParamDecleared.Add(cfgName, new object[] { category, CosParams.Create(cfgName, cfgType, cValueLimit.Non, null, false) });

        }

        /// <summary>
        /// 获取所有已声明配置参数的类别名称
        /// </summary>
        /// <returns></returns>
        public List<string> AllCfgParamCategories()
        {
            List<string> ret = new List<string>();
            foreach (KeyValuePair<string, object[]> kv in dictCfgParamDecleared)
                if (!ret.Contains(kv.Value[0] as string))
                    ret.Add(kv.Value[0] as string);
            return ret;
        }

        /// <summary>
        /// 获取某一个类别下的所有配置参数名
        /// </summary>
        /// <param name="category"></param>
        /// <returns></returns>
        public List<string> AllCfgParamNamesInCategory(string category)
        {
            List<string> ret = new List<string>();
            foreach (KeyValuePair<string, object[]> kv in dictCfgParamDecleared)
                if (kv.Value[0] as string == category)
                    ret.Add(kv.Key);
            return ret;
        }



        public void DeclearCfgParam(CosParams paramDescribe, string category)
        {
            if (string.IsNullOrEmpty(paramDescribe.pName))
                throw new ArgumentNullException("声明的配置参数名称为空 in DeclearCfgParam()");
            if (paramDescribe.pName == "StationBasePrivateConfig")
                throw new ArgumentException("声明的配置参数名称不能为:\"StationBasePrivateConfig\" in DeclearCfgParam()");
            if (string.IsNullOrEmpty(category))
                throw new ArgumentException("声明的配置参数类别名不能为空 in DeclearCfgParam()");
            if (dictCfgParamDecleared.ContainsKey(paramDescribe.pName))
                throw new ArgumentException("重复申明配置参数,Name = " + paramDescribe.pName);
            dictCfgParamDecleared.Add(paramDescribe.pName, new object[] { category, paramDescribe });
        }


        /// <summary>
        /// 配置项是否为工站内注册（不可删除）
        /// </summary>
        /// <param name="cfgName"></param>
        /// <returns></returns>
        public bool IsCfgDecleared(string cfgName)
        {
            return dictCfgParamDecleared.ContainsKey(cfgName);
        }

        /// <summary>
        /// 获取配置项的类型描述信息
        /// </summary>
        /// <param name="cfgName"></param>
        /// <returns></returns>
        public CosParams GetCfgParamDescribe(string cfgName)
        {
            if (dictCfgParamDecleared.ContainsKey(cfgName)) //工站(声明的)固有配置属性
                return dictCfgParamDecleared[cfgName][1] as CosParams;
            else // 从配置界面上动态添加的配置
            {
                if (!_cfg.ContainsItem(cfgName))
                    throw new ArgumentException(string.Format("GetCfgParamDescribe(cfgName,...) failed by cfgName = \"{0}\" is not contained in StationName = \"{1}\"", cfgName, Name));
                return CosParams.Create(cfgName, _cfg.GetItemValue(cfgName).GetType(), cValueLimit.Non, null, false);
            }

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="cfgName"></param>
        /// <param name="cfgValue"></param>
        public void SetCfgParamValue(string cfgName, object cfgValue)
        {
            if (IsInitOK) //已经初始化完成
                _cfg.SetItemValue(cfgName, cfgValue);
            else //调用时未初始化（构造函数中）
            {
                if (dictCfgParamDecleared.ContainsKey(cfgName))
                {
                    object[] oa = dictCfgParamDecleared[cfgName];
                    dictCfgParamDecleared[cfgName] = new object[] { oa[0], oa[1], cfgValue };
                }
                else
                    _cfg.SetItemValue(cfgName, cfgValue);
            }
        }


        public object GetCfgParamValue(string cfgName)
        {
            if (!IsInitOK)
            {
                if (_cfg.ContainsItem(cfgName)) //如果配置中已包含
                    return _cfg.GetItemValue(cfgName);

                if (dictCfgParamDecleared.ContainsKey(cfgName)) //如果是声明的（固有）参数项
                {
                    object[] oa = dictCfgParamDecleared[cfgName];
                    if (oa.Length > 2)
                        return oa[2];
                    else
                        return DefaultValueFromType(Type.GetType((oa[1] as CosParams).ptype));
                }

                return null;
            }
            return _cfg.GetItemValue(cfgName);
        }

        /// <summary>
        /// 获取系统配置项的值（包括：本工站声明的 和非本工站声明的）
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        protected object GetSysCfgValue(string name)
        {
            if (string.IsNullOrEmpty(name))
                throw new ArgumentNullException("IStationBase.GetSysCfgValue failed by name is null empty");
            if (AppHubCenter.Instance.SystemCfg.ContainsItem(name))
                return AppHubCenter.Instance.SystemCfg.GetItemValue(name);

            return null;
        }



        protected virtual void NotifyProductFinished(int passCount, string[] passIDs, int NGCount, string[] ngIDs, string[] NGInfo)
        {
            ActProductFinished?.Invoke(this, passCount, passIDs, NGCount, ngIDs, NGInfo);
        }

        protected virtual void NotifyCustomizeMsg(string msgCategory, object[] msgParams)
        {
            ActStationCustomizeMsg?.Invoke(this, msgCategory, msgParams);
        }



        #region 通用的工站方法
        /// <summary>
        /// 打开通道单元所属的设备
        /// </summary>
        /// <param name="chnType"></param>
        /// <param name="chnName"></param>
        /// <returns></returns>
        public bool OpenChnDevice(NamedChnType devChnType, string chnName, out string errorInfo)
        {
            if (NamedChnType.None == devChnType)
            {
                errorInfo = "未指定设备类型";
                return false;
            }
            if (string.IsNullOrEmpty(chnName))
            {
                errorInfo = "未指定设备通道名称";
                return false;
            }

            int errCode = 0;

            if (devChnType == NamedChnType.Camera) //相机设备未使用多级通道
            {
                string[] allCmrNames = AppHubCenter.Instance.InitorManager.GetIDs(typeof(IPlatDevice_Camera));
                if (null == allCmrNames)
                {
                    errorInfo = "设备表中不存在相机设备！";
                    return false;
                }
                if (!allCmrNames.Contains(chnName))
                {
                    errorInfo = "设备表中未包含相机设备:\"" + chnName + "\"";
                    return false;
                }

                IPlatDevice_Camera cmr = AppHubCenter.Instance.InitorManager.GetInitor(chnName) as IPlatDevice_Camera;
                if (cmr.IsDeviceOpen)
                {
                    errorInfo = "Success";
                    return true;
                }

                errCode = cmr.OpenDevice();
                if (errCode != 0)
                {
                    errorInfo = cmr.GetErrorInfo(errCode);
                    return false;
                }
                errorInfo = "Success";
                return true;

            }
            else
            {
                IDevCellInfo ci = null;

                switch (devChnType)
                {
                    case NamedChnType.Ai:
                        ci = AppHubCenter.Instance.MDCellNameMgr.GetAiCellInfo(chnName);
                        break;
                    case NamedChnType.Ao:
                        ci = AppHubCenter.Instance.MDCellNameMgr.GetAoCellInfo(chnName);
                        break;
                    case NamedChnType.Axis:
                        ci = AppHubCenter.Instance.MDCellNameMgr.GetAxisCellInfo(chnName);
                        break;
                    case NamedChnType.Di:
                        ci = AppHubCenter.Instance.MDCellNameMgr.GetDiCellInfo(chnName);
                        break;
                    case NamedChnType.Do:
                        ci = AppHubCenter.Instance.MDCellNameMgr.GetDoCellInfo(chnName);
                        break;

                }

                if (ci == null)
                {
                    errorInfo = "设备通道ID:\"" + chnName + "\" 在设备命名表中不存在";
                    return false;
                }
                IPlatDevice dev = AppHubCenter.Instance.InitorManager.GetInitor(ci.DeviceID) as IPlatDevice;
                if (null == dev)
                {
                    errorInfo = "设备通道ID:\"" + chnName + "\"所属设备:\"" + ci.DeviceID + "\"不存在";
                    return false;
                }
                if (dev.IsDeviceOpen)
                {
                    errorInfo = "Success";
                    return true;
                }
                else
                {
                    errCode = dev.OpenDevice();
                    if (errCode != 0)
                    {
                        errorInfo = "设备通道ID:\"" + chnName + "\"所属设备:\"" + ci.DeviceID + "\"打开失败:" + dev.GetErrorInfo(errCode);
                        return false;
                    }

                    errorInfo = "Success";
                    return true;
                }

            }





        }


        /// <summary>
        /// 打开通道单元所属的设备
        /// </summary>
        /// <param name="chnType"></param>
        /// <param name="chnAliasName">通道的替身名</param>
        /// <param name="errorInfo"></param>
        /// <returns></returns>
        public bool OpenChnDeviceAlias(NamedChnType devChnType, string chnAliasName, out string errorInfo)
        {
            if (NamedChnType.None == devChnType)
            {
                errorInfo = "未指定设备类型";
                return false;
            }
            if (string.IsNullOrEmpty(chnAliasName))
            {
                errorInfo = "未指定设备通道名称";
                return false;
            }
            string gName = GetDecChnGlobName(devChnType, chnAliasName);
            if (string.IsNullOrEmpty(gName))
            {
                errorInfo = "OpenChnDeviceAlias(chnAliasName ...) failed by:  chnAliasName = \"" + chnAliasName + "\" is not bingding to global";
                return false;
            }
            string innerError;
            bool isOK = OpenChnDevice(devChnType, gName, out innerError);
            if (!isOK)
                errorInfo = "OpenChnDeviceAlias(chnAliasName ...) failed by:  chnAliasName = \"" + chnAliasName + "\"," + innerError;
            else
                errorInfo = "Success";
            return isOK;

        }




        /// <summary>
        ///  设置DO状态
        /// </summary>
        /// <param name="doName">工站内配置的DO</param>
        /// <param name="isTurnOn"></param>
        /// <param name="errorInfo"></param>
        /// <returns></returns>
        public bool SetDO(string doName, bool isTurnOn, out string errorInfo)
        {
            errorInfo = "Unknown error";
            if (string.IsNullOrEmpty(doName))
            {
                errorInfo = "SetDO(string doName...)falied by doName is null or empty ";
                return false;
            }
            //  AppDevChannel chn = new AppDevChannel(IDevCellType.DO, doName);
            string err;
            IPlatDevice dev;
            IDevCellInfo ci;
            if (!AppDevChannel.CheckChannel(IDevCellType.DO, doName, out dev, out ci, out err))
            {
                errorInfo = "SetDO(...) fialed by:" + err;
                return false;
            }

            int errCode = (dev as IPlatDevice_MotionDaq).GetDio(ci.ModuleIndex).SetDO(ci.ChannelIndex, isTurnOn);
            if (errCode != 0)
            {
                errorInfo = "SetDO(...) fialed by :" + (dev as IPlatDevice_MotionDaq).GetDio(ci.ModuleIndex).GetErrorInfo(errCode);
                return false;
            }

            errorInfo = "Success";
            return true;

        }


        public ICmdResult WaitDO(string doName, bool isTurnOn, out string errorInfo, int timeoutMilSeconds = -1)
        {
            errorInfo = "Unknown error";
            if (string.IsNullOrEmpty(doName))
            {
                errorInfo = "WaitDO(string doName...)falied by doName is null or empty ";
                return ICmdResult.UnknownError;
            }
            // AppDevChannel chn = new AppDevChannel(IDevCellType.DO, doName);
            string err;
            IPlatDevice dev;
            IDevCellInfo ci;
            if (!AppDevChannel.CheckChannel(IDevCellType.DO, doName, out dev, out ci, out err))
            {
                errorInfo = "WaitDO(...) fialed by:" + err;
                return ICmdResult.UnknownError;
            }

            IPlatModule_DIO md = (dev as IPlatDevice_MotionDaq).GetDio(ci.ModuleIndex);

            DateTime startTime = DateTime.Now;
            bool isON = false;
            int errCode;
            while (true)
            {
                errCode = md.GetDO(ci.ChannelIndex, out isON);
                if (errCode != 0)
                {
                    errorInfo = "WaitDO(string doName...) faled by:" + md.GetErrorInfo(errCode);
                    return ICmdResult.ActionError;
                }
                if (isON == isTurnOn)
                {
                    errorInfo = "Success";
                    return ICmdResult.Success;
                }
                if (IsInWorkThread())
                {
                    if (CheckCmd(CycleMilliseconds) == CCRet.Resume)
                        startTime = DateTime.Now;
                }
                else
                    Thread.Sleep(CycleMilliseconds);

                if (timeoutMilSeconds >= 0)
                {
                    TimeSpan ts = (DateTime.Now - startTime);
                    if (ts.TotalMilliseconds >= timeoutMilSeconds)
                    {
                        errorInfo = "Timeout";
                        return ICmdResult.Timeout;
                    }
                }

            }


        }


        public bool GetDI(string diName, out bool isON, out string errorInfo)
        {
            isON = false;
            errorInfo = "Unknown error";
            if (string.IsNullOrEmpty(diName))
            {
                errorInfo = "GetDI(string diName...)falied by diName is null or empty ";
                return false;
            }
            //AppDevChannel chn = new AppDevChannel(IDevCellType.DI, diName);
            string err;
            IPlatDevice dev;
            IDevCellInfo ci;
            if (!AppDevChannel.CheckChannel(IDevCellType.DI, diName, out dev, out ci, out err))
            {
                errorInfo = "GetDI() fialed by:" + err;
                return false;
            }

            IPlatModule_DIO md = (dev as IPlatDevice_MotionDaq).GetDio(ci.ModuleIndex);
            int errCode = md.GetDI(ci.ChannelIndex, out isON);
            if (errCode != 0)
            {
                errorInfo = "WaitDI(string diName...) faled by:" + md.GetErrorInfo(errCode);
                return false;
            }

            return true;
        }

        public bool GetDIByAlias(string diAlias, out bool isON, out string errorInfo)
        {
            isON = false;
            if (string.IsNullOrEmpty(diAlias))
            {
                errorInfo = "GetDIByAlias failed by diAliasName is null or empty";
                return false;
            }
            string gName = GetDecChnGlobName(NamedChnType.Di, diAlias);
            if (string.IsNullOrEmpty(gName))
            {
                errorInfo = "GetDIByAlias(diAlias,..) faied by: diAlias = \"" + diAlias + "\" is not binding to global name";
                return false;
            }

            return GetDI(gName, out isON, out errorInfo);
        }



        public bool GetDO(string doName, out bool isON, out string errorInfo)
        {
            isON = false;
            errorInfo = "Unknown error";
            if (string.IsNullOrEmpty(doName))
            {
                errorInfo = "GetDO(string doName...)falied by doName is null or empty ";
                return false;
            }
            //AppDevChannel chn = new AppDevChannel(IDevCellType.DI, diName);
            string err;
            IPlatDevice dev;
            IDevCellInfo ci;
            if (!AppDevChannel.CheckChannel(IDevCellType.DO, doName, out dev, out ci, out err))
            {
                errorInfo = "GetDO() fialed by:" + err;
                return false;
            }

            IPlatModule_DIO md = (dev as IPlatDevice_MotionDaq).GetDio(ci.ModuleIndex);
            int errCode = md.GetDO(ci.ChannelIndex, out isON);
            if (errCode != 0)
            {
                errorInfo = "GetDO(string diName...) faled by:" + md.GetErrorInfo(errCode);
                return false;
            }

            return true;
        }

        public bool GetDOByAlias(string doAlias, out bool isON, out string errorInfo)
        {
            isON = false;
            if (string.IsNullOrEmpty(doAlias))
            {
                errorInfo = "GetDOByAlias failed by doAliasName is null or empty";
                return false;
            }
            string gName = GetDecChnGlobName(NamedChnType.Di, doAlias);
            if (string.IsNullOrEmpty(gName))
            {
                errorInfo = "GetDOByAlias(diAlias,..) faied by: diAlias = \"" + doAlias + "\" is not binding to global name";
                return false;
            }

            return GetDO(gName, out isON, out errorInfo);
        }





        public ICmdResult WaitDI(string diName, bool isTurnOn, out string errorInfo, int timeoutMilSeconds = -1)
        {
            errorInfo = "Unknown error";
            if (string.IsNullOrEmpty(diName))
            {
                errorInfo = "WaitDI(string diName...)falied by diName is null or empty ";
                return ICmdResult.UnknownError;
            }
            //AppDevChannel chn = new AppDevChannel(IDevCellType.DI, diName);
            string err;
            IPlatDevice dev;
            IDevCellInfo ci;
            if (!AppDevChannel.CheckChannel(IDevCellType.DI, diName, out dev, out ci, out err))
            {
                errorInfo = "WaitDI(...) fialed by:" + err;
                return ICmdResult.UnknownError;
            }

            IPlatModule_DIO md = (dev as IPlatDevice_MotionDaq).GetDio(ci.ModuleIndex);

            DateTime startTime = DateTime.Now;
            bool isON = false;
            int errCode;
            while (true)
            {
                errCode = md.GetDI(ci.ChannelIndex, out isON);
                if (errCode != 0)
                {
                    errorInfo = "WaitDI(string diName...) faled by:" + md.GetErrorInfo(errCode);
                    return ICmdResult.ActionError;
                }
                if (isON == isTurnOn)
                {
                    errorInfo = "Success";
                    return ICmdResult.Success;
                }
                if (IsInWorkThread())
                {
                    if (CheckCmd(CycleMilliseconds) == CCRet.Resume)
                        startTime = DateTime.Now;
                }
                else
                    Thread.Sleep(CycleMilliseconds);

                if (timeoutMilSeconds >= 0)
                {
                    TimeSpan ts = (DateTime.Now - startTime);
                    if (ts.TotalMilliseconds >= timeoutMilSeconds)
                    {
                        errorInfo = "Timeout";
                        return ICmdResult.Timeout;
                    }
                }

            }
        }


        /// <summary>
        /// 等待多个DI为指定的状态（相同的）
        /// </summary>
        /// <param name="diNames"></param>
        /// <param name="isTurnOn"></param>
        /// <param name="errorInfo"></param>
        /// <param name="timeoutMilSeconds"></param>
        /// <returns></returns>
        public ICmdResult WaitDIs(string[] diNames, bool isTurnOn, out string errorInfo, int timeoutMilSeconds = -1)
        {
            errorInfo = "Unknown error";
            if (null == diNames || 0 == diNames.Length)
            {
                errorInfo = "WaitDIs(string[] diNames...)falied by diNames is null or empty ";
                return ICmdResult.UnknownError;
            }
            string err;
            IPlatDevice[] devs = new IPlatDevice[diNames.Length];
            IDevCellInfo[] cis = new IDevCellInfo[diNames.Length];
            IPlatModule_DIO[] mds = new IPlatModule_DIO[diNames.Length];
            for (int i = 0; i < diNames.Length; i++)
            {
                if (!AppDevChannel.CheckChannel(IDevCellType.DI, diNames[i], out devs[i], out cis[i], out err))
                {
                    errorInfo = "WaitDIs(...) fialed by:" + err;
                    return ICmdResult.UnknownError;
                }
                mds[i] = (devs[i] as IPlatDevice_MotionDaq).GetDio(cis[i].ModuleIndex);
            }



            DateTime startTime = DateTime.Now;
            bool isON = false;
            bool isWaitedAll = false;
            int errCode;
            while (true)
            {
                isWaitedAll = true;
                for (int i = 0; i < diNames.Length; i++)
                {
                    errCode = mds[i].GetDI(cis[i].ChannelIndex, out isON);
                    if (errCode != 0)
                    {
                        errorInfo = "WaitDIs(string[] diNames...) faled by:" + mds[i].GetErrorInfo(errCode);
                        return ICmdResult.ActionError;
                    }
                    if (isON != isTurnOn)
                    {
                        isWaitedAll = false;
                        break;
                    }

                }
                if (isWaitedAll)
                    return ICmdResult.Success;

                if (IsInWorkThread())
                {
                    if (CheckCmd(CycleMilliseconds) == CCRet.Resume)
                        startTime = DateTime.Now;
                }
                else
                    Thread.Sleep(CycleMilliseconds);

                if (timeoutMilSeconds >= 0)
                {
                    TimeSpan ts = (DateTime.Now - startTime);
                    if (ts.TotalMilliseconds >= timeoutMilSeconds)
                    {
                        errorInfo = "Timeout";
                        return ICmdResult.Timeout;
                    }
                }

            }
        }


        /// <summary>
        /// 等待多个DI为指定的状态（使用替身名）
        /// </summary>
        /// <param name="diNames"></param>
        /// <param name="isTurnOn"></param>
        /// <param name="errorInfo"></param>
        /// <param name="timeoutMilSeconds"></param>
        /// <returns></returns>
        public ICmdResult WaitDIsByAlias(string[] diAliasNames, bool isTurnOn, out string errorInfo, int timeoutMilSeconds = -1)
        {
            if (null == diAliasNames || 0 == diAliasNames.Length)
            {
                errorInfo = "WaitDIsByAlias(string[] diAliasNames,...) failed by diAliasNames is null or empty";
                return ICmdResult.UnknownError;
            }

            string[] diNames = new string[diAliasNames.Length];
            for (int i = 0; i < diAliasNames.Length; i++)
            {
                diNames[i] = GetDecChnGlobName(NamedChnType.Di, diAliasNames[i]);
                if (string.IsNullOrEmpty(diNames[i]))
                {
                    errorInfo = "WaitDIsByAlias(string[] diAliasNames,...) failed by AliasName = \"" + diAliasNames[i] + "\"未绑定全局通道名 ";
                    return ICmdResult.UnknownError;
                }
            }

            return WaitDIs(diNames, isTurnOn, out errorInfo, timeoutMilSeconds);
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="doName"></param>
        /// <param name="isTurnOn"></param>
        /// <param name="errorInfo"></param>
        /// <returns></returns>
        public bool SetDOAlias(string doAliasName, bool isTurnOn, out string errorInfo)
        {
            errorInfo = "Unknown error";
            if (string.IsNullOrEmpty(doAliasName))
            {
                errorInfo = "SetDOAlias(... faled by doAliasName is null or empty";
                return false;
            }
            string gName = GetDecChnGlobName(NamedChnType.Do, doAliasName);
            if (string.IsNullOrEmpty(gName))
            {
                errorInfo = "SetDOAlias(doAliasName,..) faied by: doAliasName = \"" + doAliasName + "\" is not binding to global name";
                return false;
            }
            string innerError;
            bool isOK = SetDO(gName, isTurnOn, out innerError);
            isOK = ICmdResult.Success == WaitDO(gName, isTurnOn, out innerError);
            return isOK;
        }

        public bool SetDOAliass(string[] doAliasName, bool[] isTurnOn, out string errorInfo)
        {
            if (null == doAliasName || doAliasName.Length == 0 || null == isTurnOn || doAliasName.Length != isTurnOn.Length)
                throw new ArgumentException("IStationBase.SetDOAliass() failed by Argument error!");
            for (int i = 0; i < doAliasName.Length; i++)
            {

                if (!SetDOAlias(doAliasName[i], isTurnOn[i], out errorInfo))
                {
                    errorInfo = (isTurnOn[i] ? "打开" : "关闭") + " DO:\"" + doAliasName[i] + "\" 失败:" + errorInfo;
                    return false;
                }
            }
            errorInfo = "Success";
            return true;
        }

        public ICmdResult WaitDOAlias(string doAliasName, bool isTurnOn, out string errorInfo, int timeoutMiliSeconds = -1)
        {
            errorInfo = "Unknown error";
            if (string.IsNullOrEmpty(doAliasName))
            {
                errorInfo = "WaitDOAlias(... )faled by doAliasName is null or empty";
                return ICmdResult.UnknownError;
            }
            string gName = GetDecChnGlobName(NamedChnType.Do, doAliasName);
            if (string.IsNullOrEmpty(gName))
            {
                errorInfo = "doAliasName = \"" + doAliasName + "\" is not binding to global name";
                return ICmdResult.UnknownError;
            }
            string innerError;
            ICmdResult ret = WaitDO(gName, isTurnOn, out innerError, timeoutMiliSeconds);
            if (ret != ICmdResult.Success)
                errorInfo = "WetDOAlias(doAliasName,..) faied by: doAliasName = \"" + doAliasName + "\" innerError = " + innerError;

            return ret;
        }

        public ICmdResult WaitDIAlias(string diAliasName, bool isTurnOn, out string errorInfo, int timeoutMiliSeconds = -1)
        {
            errorInfo = "Unknown error";
            if (string.IsNullOrEmpty(diAliasName))
            {
                errorInfo = "WaitDIAlias(... )faled by diAliasName is null or empty";
                return ICmdResult.UnknownError;
            }
            string gName = GetDecChnGlobName(NamedChnType.Di, diAliasName);
            if (string.IsNullOrEmpty(gName))
            {
                errorInfo = "diAliasName = \"" + diAliasName + "\" is not binding to global name";
                return ICmdResult.UnknownError;
            }
            string innerError;
            ICmdResult ret = WaitDI(gName, isTurnOn, out innerError, timeoutMiliSeconds);
            if (ret != ICmdResult.Success)
                errorInfo = "WaitDIAlias(diAliasName,..) faied by: diAliasName = \"" + diAliasName + "\" innerError = " + innerError;

            return ret;
        }



        public void StopAxis(string axisName)
        {
            if (string.IsNullOrEmpty(axisName))
                return;
            string err;
            IPlatDevice dev;
            IDevCellInfo ci;
            if (!AppDevChannel.CheckChannel(IDevCellType.Axis, axisName, out dev, out ci, out err))
                return;

            (dev as IPlatDevice_MotionDaq).GetMc(ci.ModuleIndex).StopAxis(ci.ChannelIndex);
        }


        public void StopAxisAlias(string axisAliasName)
        {
            if (string.IsNullOrEmpty(axisAliasName))
                return;
            string gAxisName = GetDecChnGlobName(NamedChnType.Axis, axisAliasName);
            StopAxis(gAxisName);
        }


        #endregion


        #region  数据池相关操作


        /// <summary>
        /// 保存所有声明的系统变量假名(只是为了以申明顺序访问)
        /// </summary>
        List<string> _lstSysPoolItemAliasNames = new List<string>();

        /// <summary>
        /// 保存所有声明的系统变量假名/类型/初始值
        /// </summary>
        Dictionary<string, object[]> _dictSysPoolItemDecleared = new Dictionary<string, object[]>();

        /// <summary>
        /// 全局变量名称
        /// </summary>
        DictionaryEx<string, string> _dictSysPoolItemNameMapping = new DictionaryEx<string, string>();



        /// <summary>
        /// 声明一个变量到系统数据池中
        /// 本函数在继承类的构造函数中使用
        /// 真正的注册动作在Station的Init函数中
        /// 声明的变量名称为站内替身名（非实际的系统变量名称）
        /// </summary>
        /// <param name="aliasName"></param>
        /// <param name="itemType"></param>
        /// <param name="itemInitValue"></param>
        protected void DeclearSPItemAlias(string aliasName, Type itemType, object itemInitValue) //SP = system pool ,系统数据池 
        {
            if (string.IsNullOrEmpty(aliasName))
                throw new ArgumentNullException("DeclearSystemPoolItem failed by :aliasName is null or empty ");
            if (_dictSysPoolItemDecleared.ContainsKey(aliasName))
                throw new ArgumentException("DeclearSystemPoolItem failed by :aliasName = \"" + aliasName + "\" is decleared!");
            ///检查初始值和类型是否匹配
            if (itemType.IsValueType && itemInitValue == null)
                throw new ArgumentException("DeclearSystemPoolItem failed by:itemType = " + itemType.Name + " InitValue = null");
            object itemVal = itemInitValue;

            _lstSysPoolItemAliasNames.Add(aliasName);
            _dictSysPoolItemDecleared.Add(aliasName, new object[] { itemType, itemVal });

        }



        /// <summary>
        /// 获取工站内所有系统变量假名（不带StationName + ":"前缀）
        /// </summary>
        public string[] AllSPAliasNames
        {
            get { return _lstSysPoolItemAliasNames.ToArray(); }
        }



        /// <summary>
        /// 获取系统变量的真正名称
        /// </summary>
        /// <param name="locName"></param>
        /// <returns></returns>
        public string GetSPAliasRealName(string aliasName)
        {
            if (!_dictSysPoolItemNameMapping.ContainsKey(aliasName))
                return null;
            return _dictSysPoolItemNameMapping[aliasName];
        }

        /// <summary>
        /// 获取系统变量(替身)的类型
        /// </summary>
        /// <param name="aliasName"></param>
        /// <returns></returns>
        public Type GetSPAliasType(string aliasName)
        {
            if (!_dictSysPoolItemDecleared.ContainsKey(aliasName))
                return null;
            return _dictSysPoolItemDecleared[aliasName][0] as Type;
        }


        public bool SetSPAliasRealName(string aliasName, string realName)
        {
            if (string.IsNullOrEmpty(realName))
                throw new ArgumentNullException("SetSPAliasRealName(aliasName,realName) failed by realName is null or empty");
            if (!_dictSysPoolItemDecleared.ContainsKey(aliasName))
                return false;
            string oldRealName = null;
            if (_dictSysPoolItemNameMapping.ContainsKey(aliasName))
            {
                oldRealName = _dictSysPoolItemNameMapping[aliasName];
                if (oldRealName == realName)
                    return true;
                else
                    AppHubCenter.Instance.DataPool.RemoveItem(oldRealName);
                _dictSysPoolItemNameMapping.Remove(aliasName);
            }
            _dictSysPoolItemNameMapping.Add(aliasName, realName);
            AppHubCenter.Instance.DataPool.RegistItem(realName, _dictSysPoolItemDecleared[aliasName][0] as Type, _dictSysPoolItemDecleared[aliasName][1]);

            return true;
        }


        /// <summary>
        /// 根据站内名获取一个系统变量的值
        /// 
        /// </summary>
        /// <param name="aliasName"></param>
        /// <returns></returns>
        public bool GetSPAliasValue(string aliasName, out object value)
        {
            value = null;
            if (!_dictSysPoolItemDecleared.ContainsKey(aliasName))
                throw new ArgumentException("GetSPAliasValue(aliasName) failed by: aliasName = " + aliasName + "is not decleared");
            string spRealName = GetSPAliasRealName(aliasName);
            if (string.IsNullOrEmpty(spRealName))
                return false;//throw new Exception("获取系统变量值失败，替身名称:\"" + aliasName + "\"未绑定系统变量");
            return AppHubCenter.Instance.DataPool.GetItemValue(spRealName, out value);
        }

        /// <summary>
        /// 根据站内名设置一个系统变量值
        /// </summary>
        /// <param name="locName"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public bool SetSPAliasValue(string aliasName, object value)
        {

            if (!_dictSysPoolItemDecleared.ContainsKey(aliasName))
                throw new ArgumentException("SetSPAliasValue(aliasName) failed by: aliasName = " + aliasName + "is not decleared");
            string spRealName = GetSPAliasRealName(aliasName);
            if (string.IsNullOrEmpty(spRealName))
                return false;
            return AppHubCenter.Instance.DataPool.SetItemValue(spRealName, value);
        }

        /// <summary>
        /// 等待一个系统bool变量的值
        /// </summary>
        /// <param name="sysPoolItemName">系统变量名称</param>
        /// <param name="targetValue">系统变量目标值</param>
        /// <param name="timeoutMilliSeconds">超时毫秒数</param>
        /// <returns></returns>
        protected bool WaitSPBool(string sysPoolItemName, bool targetValue, out string errorInfo, int timeoutMilliSeconds = -1)
        {
            errorInfo = "Success";
            if (!AppHubCenter.Instance.DataPool.ContainItem(sysPoolItemName))
            {
                errorInfo = "等待的系统Bool变量名称:\"" + sysPoolItemName + "\"不存在";
                return false;
            }

            if (AppHubCenter.Instance.DataPool.GetItemType(sysPoolItemName) != typeof(bool))
            {
                errorInfo = "等待的系统Bool变量名称:\"" + sysPoolItemName + "\"真实类型为:" + AppHubCenter.Instance.DataPool.GetItemType(sysPoolItemName).Name;
                return false;
            }
            DateTime startTime = DateTime.Now;
            object currVal;
            while (true)
            {
                if (IsInWorkThread())
                {
                    if (CheckCmd(CycleMilliseconds) == CCRet.Resume)
                        startTime = DateTime.Now;
                }
                else
                    Thread.Sleep(CycleMilliseconds);
                bool isOK = AppHubCenter.Instance.DataPool.GetItemValue(sysPoolItemName, out currVal);
                if (!isOK)
                {
                    errorInfo = "未能获取系统变量:\"" + sysPoolItemName + "\"的值";
                    return false;
                }

                if (Convert.ToBoolean(currVal) == targetValue)
                    return true;

                if (timeoutMilliSeconds >= 0)
                {
                    TimeSpan ts = DateTime.Now - startTime;
                    if (ts.TotalMilliseconds >= timeoutMilliSeconds)
                    {
                        errorInfo = "等待超时！系统Bool变量名称:\"" + sysPoolItemName + "\" 目标值:" + targetValue;
                        return false;
                    }
                }


            }

        }

        protected bool WaitSPBoolByAliasName(string aliasName, bool targetValue, out string errorInfo, int timeoutMilliSeconds = -1)
        {
            string realName = GetSPAliasRealName(aliasName);
            if (string.IsNullOrEmpty(realName))
            {
                errorInfo = "等待系统Bool变量失败，AliasName = " + aliasName + "未绑定系统名称";
                return false;
            }

            bool ret = WaitSPBool(realName, targetValue, out errorInfo, timeoutMilliSeconds);
            if (!ret)
                errorInfo = "AliasName:\"" + aliasName + "\"->" + errorInfo;
            return ret;
        }


        IStationRunMode _runMode = IStationRunMode.Auto;
        public virtual IStationRunMode RunMode { get { return _runMode; } }
        public virtual bool SetRunMode(IStationRunMode runMode)
        {
            if (IsWorking())
                return false;
            _runMode = runMode;
            return true;
        }



        #endregion


        /// <summary>
        /// 将多个工作点位组合成一个点位,如果各工作点位中包含相同的轴，后面的轴位置会覆盖前面的点
        /// </summary>
        /// <param name="workPosNames"></param>
        /// <returns></returns>
        protected IMultiAxisProPos UnionWorkPos(string[] workPosNames)
        {
            if (null == workPosNames || 0 == workPosNames.Length)
                return null;
            IMultiAxisProPos ret = new IMultiAxisProPos();
            foreach (string pn in workPosNames)
            {
                IMultiAxisProPos pos = GetWorkPosition(pn);
                if (null == pos)
                    return null;
                foreach (var ap in pos.Positions)
                {
                    if (!ret.ContainAxis(ap.Name))
                        ret.Positions.Add(Items.Create(ap.Name, ap.Value));
                    else
                        ret.SetAxisPos(ap.Name, ap.Value);
                }
            }
            return ret;
        }



        protected void LogAndExitWork(WorkExitCode wec, string info)
        {
            // Log((int)wec, info);
            ExitWork(wec, info);
        }
    }
}
