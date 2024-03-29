using Cell.DataModel;
using Cell.Interface;
using Cell.Tools;
using Sys.IStations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Body.IMainStation
{
    public class IMainStationBase : IPlatMainStation
    {
        string _cfgPath = AppDomain.CurrentDomain.BaseDirectory + "AppConfig\\AppMainStation.cfg";

        //主工站配置文档
        internal AppCfgFromXml _cfg { get; private set; } = new AppCfgFromXml(); //用于存储配置

        public virtual string AppName { get { return "MainStationVM"; } }

        /// <summary>
        /// 设备是否成功归零过至少一次
        /// </summary>
        public bool IsReseted { get; private set; } = false;
        /// <summary>
        /// 获取当前工作状态
        /// </summary>
        public virtual IWorkStatus WorkStatus { get; protected set; }

        #region  报警信息
        public virtual bool IsAlarming { get; protected set; }

        string _alarmInfo = "No-Alarm";
        public virtual string GetAlarmInfo()
        {
            return _alarmInfo;
        }

        /// <summary>
        /// 重置（消除）报警信号
        /// </summary>
        public virtual bool ClearAlarming(out string errorInfo)
        {
            errorInfo = "Success";
            IsAlarming = false;
            SetFixDoSig(FixedDO.红灯, false);
            SetFixDoSig(FixedDO.蜂鸣器, false);
            SetFixDoSig(FixedDO.绿灯, false);
            SetFixDoSig(FixedDO.黄灯, false);
            WorkStatus = IWorkStatus.UnStart;
            if (UIPanel is UcMainStationBasePanel)
            {
                (UIPanel as UcMainStationBasePanel).OnMSWorkStatus(WorkStatus);
                (UIPanel as UcMainStationBasePanel).OnAlarm(false, null);
            }
            return true;

        }

        #endregion

        public IMainStationBase()
        {
            _cfg.Load(_cfgPath, true);

            string[] fixDiNames = Enum.GetNames(typeof(FixedDI));
            for (int i = 0; i < fixDiNames.Length; i++)
            {
                if (!_cfg.ContainsItem(fixDiNames[i]))
                    _cfg.AddItem(fixDiNames[i], new Cell.DataModel.DioInfo() { GlobalChnName = "", Enabled = false });//全局通道名称,是否启用
                else
                {
                    if (_cfg.GetItemValue(fixDiNames[i]).GetType() != typeof(DioInfo))
                    {
                        _cfg.RemoveItem(fixDiNames[i]);
                        _cfg.AddItem(fixDiNames[i], new DioInfo() { GlobalChnName = "", Enabled = false });
                    }
                }
            }

            string[] fixedDoNames = Enum.GetNames(typeof(FixedDO));
            for (int i = 0; i < fixedDoNames.Length; i++)
            {
                if (!_cfg.ContainsItem(fixedDoNames[i]))
                    _cfg.AddItem(fixedDoNames[i], new Cell.DataModel.DioInfo() { GlobalChnName = "", Enabled = false }); //全局通道名称,是否启用
                else
                {
                    if (_cfg.GetItemValue(fixedDoNames[i]).GetType() != typeof(DioInfo))
                    {
                        _cfg.RemoveItem(fixedDoNames[i]);
                        _cfg.AddItem(fixedDoNames[i], new DioInfo() { GlobalChnName = "", Enabled = false });
                    }
                }
            }
            if (!_cfg.ContainsItem("DeclearedDI"))
                _cfg.AddItem("DeclearedDI", new DictionaryEx<string, DioInfo>());
            else
            {
                if (_cfg.GetItemValue("DeclearedDI").GetType() != typeof(DictionaryEx<string, DioInfo>))
                {
                    _cfg.RemoveItem("DeclearedDI");
                    _cfg.AddItem("DeclearedDI", new DictionaryEx<string, DioInfo>());
                }
            }
            _dctDeclearedDI = _cfg.GetItemValue("DeclearedDI") as DictionaryEx<string, DioInfo>;


            if (!_cfg.ContainsItem("DeclearedDO"))
                _cfg.AddItem("DeclearedDO", new DictionaryEx<string, Cell.DataModel.DioInfo>());
            else
            {
                if (_cfg.GetItemValue("DeclearedDO").GetType() != typeof(DictionaryEx<string, DioInfo>))
                {
                    _cfg.RemoveItem("DeclearedDO");
                    _cfg.AddItem("DeclearedDO", new DictionaryEx<string, DioInfo>());
                }
            }
            _dctDeclearedDO = _cfg.GetItemValue("DeclearedDO") as DictionaryEx<string, DioInfo>;


            WorkStatus = IWorkStatus.UnStart;
            IsAlarming = false;
            SetFixDoSig(FixedDO.红灯, false);
            SetFixDoSig(FixedDO.蜂鸣器, false);
            SetFixDoSig(FixedDO.绿灯, false);
            SetFixDoSig(FixedDO.黄灯, false);
        }

        #region  子工站控制  

        public virtual bool Start(out string errorInfo)//开始运行
        {
            errorInfo = "Unknown Error";
            if (IsAlarming)
            {
                errorInfo = "当前处于报警状态";
                return false;
            }
            if (IsStationRunning(WorkStatus))
            {
                errorInfo = "Success";
                return true;
            }
            AppStationManager stationMgr = AppHubCenter.Instance.StationMgr;


            string[] allEnableStationNames = stationMgr.AllEnabledStationNames();
            if (null == allEnableStationNames || 0 == allEnableStationNames.Length)
            {
                errorInfo = "不存在使能的工站";
                return false;
            }

            foreach (string stationName in allEnableStationNames) // 先检查有没有正在运行的工站
            {
                IPlatStation station = stationMgr.GetStation(stationName);
                if (IsStationRunning(station.CurrWorkStatus))
                {
                    errorInfo = "启动失败，工站:" + station.Name + " 当前状态:" + station.CurrWorkStatus.ToString();
                    return false;
                }
            }

            int failedIndex = -1; //启动失败的工站号
            foreach (string stationName in allEnableStationNames)
            {
                IPlatStation station = stationMgr.GetStation(stationName);
                ICmdResult ret = station.Start();
                if (ret != ICmdResult.Success)
                {
                    errorInfo = "工站:" + station.Name + " 启动失败,Error:" + ret.ToString();
                    break;
                }
            }

            if (failedIndex > -1)
            {
                for (int i = 0; i < failedIndex + 1; i++)
                {
                    IPlatStation station = stationMgr.GetStation(allEnableStationNames[i]);
                    if (ICmdResult.Success != station.Stop(100))
                        station.Abort();
                }
                return false;
            }

            //if (UIPanel is UcMainStationBasePanel)
            //    (UIPanel as UcMainStationBasePanel).StartCount();

            SetFixDoSig(FixedDO.停止按钮灯, false);
            SetFixDoSig(FixedDO.复位按钮灯, false);
            SetFixDoSig(FixedDO.开始按钮灯, true);
            SetFixDoSig(FixedDO.急停按钮灯, false);
            SetFixDoSig(FixedDO.暂停按钮灯, false);
            SetFixDoSig(FixedDO.红灯, false);
            SetFixDoSig(FixedDO.绿灯, true);
            SetFixDoSig(FixedDO.蜂鸣器, false);
            SetFixDoSig(FixedDO.黄灯, false);
            WorkStatus = IWorkStatus.Running;

            if (UIPanel is UcMainStationBasePanel)
                (UIPanel as UcMainStationBasePanel).OnMSWorkStatus(WorkStatus);

            errorInfo = "Success";
            return true;
        }

        /// <summary>停止运行</summary>
        public virtual bool Stop(out string errorInfo)
        {
            errorInfo = "Unknown Error";
            if (!IsStationRunning(WorkStatus))
            {
                errorInfo = "Success";
                return true;
            }
            AppStationManager stationMgr = AppHubCenter.Instance.StationMgr;


            string[] allEnableStationNames = stationMgr.AllEnabledStationNames();
            if (null == allEnableStationNames || 0 == allEnableStationNames.Length)
            {
                errorInfo = "Success";
                return true;
            }

            foreach (string stationName in allEnableStationNames) // 先检查有没有正在运行的工站
            {
                IPlatStation station = stationMgr.GetStation(stationName);
                if (IsStationRunning(station.CurrWorkStatus))
                {
                    ICmdResult ret = station.Stop(1000);
                    if (ret != ICmdResult.Success)
                        station.Abort();
                }
            }

            SetFixDoSig(FixedDO.停止按钮灯, false);
            SetFixDoSig(FixedDO.复位按钮灯, false);
            SetFixDoSig(FixedDO.开始按钮灯, false);
            SetFixDoSig(FixedDO.急停按钮灯, false);
            SetFixDoSig(FixedDO.暂停按钮灯, false);
            SetFixDoSig(FixedDO.红灯, true);
            SetFixDoSig(FixedDO.绿灯, false);
            SetFixDoSig(FixedDO.蜂鸣器, false);
            SetFixDoSig(FixedDO.黄灯, false);
            WorkStatus = IWorkStatus.CommandExit;

            if (UIPanel is UcMainStationBasePanel)
                (UIPanel as UcMainStationBasePanel).OnMSWorkStatus(WorkStatus);

            errorInfo = "Success";
            return true;
        }

        /// <summary>暂停</summary>
        public virtual bool Pause(out string errorInfo)
        {
            errorInfo = "Unknown Error";
            if (WorkStatus != IWorkStatus.Running)
            {
                errorInfo = "设备当前状态:" + WorkStatus.ToString();
                return false;
            }
            if (WorkStatus == IWorkStatus.Pausing)
            {
                errorInfo = "Success";
                return true;
            }
            AppStationManager stationMgr = AppHubCenter.Instance.StationMgr;
            string[] allEnableStationNames = stationMgr.AllEnabledStationNames();
            if (null == allEnableStationNames || 0 == allEnableStationNames.Length)
            {
                errorInfo = "无使能工站";
                return false;
            }

            foreach (string sn in allEnableStationNames)
            {
                IPlatStation station = stationMgr.GetStation(sn);
                ICmdResult ret = station.Pause(-1);
                if (ret != ICmdResult.Success)
                {
                    errorInfo = "工站:" + station.Name + " 暂停失败:" + ret.ToString();
                    return false;
                }
            }

            SetFixDoSig(FixedDO.停止按钮灯, false);
            SetFixDoSig(FixedDO.复位按钮灯, false);
            SetFixDoSig(FixedDO.开始按钮灯, false);
            SetFixDoSig(FixedDO.急停按钮灯, false);
            SetFixDoSig(FixedDO.暂停按钮灯, false);
            SetFixDoSig(FixedDO.红灯, true);
            SetFixDoSig(FixedDO.绿灯, false);
            SetFixDoSig(FixedDO.蜂鸣器, false);
            SetFixDoSig(FixedDO.黄灯, false);

            WorkStatus = IWorkStatus.Pausing;
            if (UIPanel is UcMainStationBasePanel)
                (UIPanel as UcMainStationBasePanel).OnMSWorkStatus(WorkStatus);

            errorInfo = "Success";
            return true;
        }

        /// <summary>从暂停中恢复运行</summary>
        public virtual bool Resume(out string errorInfo)
        {
            errorInfo = "Unknown Error";
            if (WorkStatus == IWorkStatus.Running)
            {
                errorInfo = "当前正在运行！恢复运行指令将被忽略";
                return true;
            }
            if (WorkStatus != IWorkStatus.Pausing)
            {
                errorInfo = "当前状态 = " + WorkStatus + ",不能响应恢复运行指令";
                return false;
            }

            AppStationManager stationMgr = AppHubCenter.Instance.StationMgr;

            string[] allEnableStationNames = stationMgr.AllEnabledStationNames();
            if (null == allEnableStationNames || 0 == allEnableStationNames.Length)
            {
                errorInfo = "无使能工站";
                return false;
            }

            foreach (string sn in allEnableStationNames)
            {
                IPlatStation station = stationMgr.GetStation(sn);
                ICmdResult ret = station.Resume(1000);
                if (ret != ICmdResult.Success)
                {
                    errorInfo = "工站:" + station.Name + "恢复运行失败:" + ret.ToString();
                    return false;
                }
            }

            SetFixDoSig(FixedDO.停止按钮灯, false);
            SetFixDoSig(FixedDO.复位按钮灯, false);
            SetFixDoSig(FixedDO.开始按钮灯, true);
            SetFixDoSig(FixedDO.急停按钮灯, false);
            SetFixDoSig(FixedDO.暂停按钮灯, false);
            SetFixDoSig(FixedDO.红灯, false);
            SetFixDoSig(FixedDO.绿灯, true);
            SetFixDoSig(FixedDO.蜂鸣器, false);
            SetFixDoSig(FixedDO.黄灯, false);

            errorInfo = "Success";
            WorkStatus = IWorkStatus.Running;
            return true;
        }

        bool isReseting = false; //当前是否正在归零
        public virtual bool Reset(out string errorInfo)
        {
            errorInfo = "Unknown Error";
            if (IsStationRunning(WorkStatus))
            {
                errorInfo = "复位失败：当前正在运行中！";
                return false;
            }

            AppStationManager stationMgr = AppHubCenter.Instance.StationMgr;

            string[] allEnableStationNames = stationMgr.AllEnabledStationNames();
            if (null == allEnableStationNames || 0 == allEnableStationNames.Length)
            {
                errorInfo = "复位失败：无使能工站";
                return false;
            }

            foreach (string sn in allEnableStationNames)
            {
                IPlatStation station = stationMgr.GetStation(sn);
                ICmdResult ret = station.Reset();
                if (ret != ICmdResult.Success)
                {
                    errorInfo = "工站:" + station.Name + "复位失败:" + ret.ToString();
                    return false;
                }
            }

            errorInfo = "Success";
            WorkStatus = IWorkStatus.Running;

            if (UIPanel is UcMainStationBasePanel)
                (UIPanel as UcMainStationBasePanel).OnMSWorkStatus(WorkStatus);

            SetFixDoSig(FixedDO.停止按钮灯, false);
            SetFixDoSig(FixedDO.复位按钮灯, true);
            SetFixDoSig(FixedDO.开始按钮灯, false);
            SetFixDoSig(FixedDO.急停按钮灯, false);
            SetFixDoSig(FixedDO.暂停按钮灯, false);
            SetFixDoSig(FixedDO.红灯, false);
            SetFixDoSig(FixedDO.绿灯, true);
            SetFixDoSig(FixedDO.蜂鸣器, false);
            SetFixDoSig(FixedDO.黄灯, false);
            isReseting = true;
            return true;
        }


        protected bool IsStationRunning(IWorkStatus ws)
        {
            return ws == IWorkStatus.Running || ws == IWorkStatus.Pausing || ws == IWorkStatus.Interactiving;
        }
        #endregion

        #region  Callback

        /// <summary>
        /// 处理工站状态改变
        /// </summary>
        /// <param name="station"></param>
        /// <param name="currWorkStatus"></param>
        public virtual void OnStationWorkStatusChanged(IPlatStation station, IWorkStatus currWorkStatus)
        {
            if (!IsStationRunning(WorkStatus))
                return;
            AppStationManager stationMgr = AppHubCenter.Instance.StationMgr;

            string[] allEnableStationNames = stationMgr.AllEnabledStationNames();
            if (null == allEnableStationNames || 0 == allEnableStationNames.Length)
                return;

            if (IsAlarming)
                return;


            if (IsStationRunning(WorkStatus)) //当前正处于工作状态
            {
                if (currWorkStatus == IWorkStatus.ErrorExit || currWorkStatus == IWorkStatus.ExceptionExit || currWorkStatus == IWorkStatus.AbortExit)//有工站错误退出
                {
                    IsAlarming = true;
                    isReseting = false;
                    SetFixDoSig(FixedDO.停止按钮灯, false);
                    SetFixDoSig(FixedDO.复位按钮灯, false);
                    SetFixDoSig(FixedDO.开始按钮灯, false);
                    SetFixDoSig(FixedDO.急停按钮灯, false);
                    SetFixDoSig(FixedDO.暂停按钮灯, false);
                    SetFixDoSig(FixedDO.红灯, true);
                    SetFixDoSig(FixedDO.绿灯, false);
                    SetFixDoSig(FixedDO.蜂鸣器, false);
                    SetFixDoSig(FixedDO.黄灯, false);
                    _alarmInfo = "工站:\"" + station.Name + "\"退出:Code:" + currWorkStatus;
                    if (UIPanel is UcMainStationBasePanel)
                        (UIPanel as UcMainStationBasePanel).OnAlarm(true, _alarmInfo);
                    WorkStatus = IWorkStatus.ErrorExit;
                    if (UIPanel is UcMainStationBasePanel)
                    {
                        //  (UIPanel as UcMainStationBasePanel).StopCount();
                        (UIPanel as UcMainStationBasePanel).OnMSWorkStatus(WorkStatus);
                    }
                }

            }

            int runningCount = 0;
            foreach (string sn in allEnableStationNames)
            {
                IPlatStation st = stationMgr.GetStation(sn);
                if (IsStationRunning(st.CurrWorkStatus))
                    runningCount++;
            }

            if (currWorkStatus == IWorkStatus.NormalEnd || currWorkStatus == IWorkStatus.CommandExit)
            {
                if (runningCount == 0)
                {
                    WorkStatus = IWorkStatus.NormalEnd;
                    if (UIPanel is UcMainStationBasePanel)
                    {
                        // (UIPanel as UcMainStationBasePanel).StopCount();

                        (UIPanel as UcMainStationBasePanel).OnMSWorkStatus(WorkStatus);

                    }
                    SetFixDoSig(FixedDO.停止按钮灯, false);
                    SetFixDoSig(FixedDO.复位按钮灯, false);
                    SetFixDoSig(FixedDO.开始按钮灯, false);
                    SetFixDoSig(FixedDO.急停按钮灯, false);
                    SetFixDoSig(FixedDO.暂停按钮灯, false);
                    SetFixDoSig(FixedDO.红灯, false);
                    SetFixDoSig(FixedDO.绿灯, false);
                    SetFixDoSig(FixedDO.蜂鸣器, false);
                    SetFixDoSig(FixedDO.黄灯, false);
                    if (currWorkStatus == IWorkStatus.NormalEnd && isReseting)
                    {
                        isReseting = false;
                        IsReseted = true;
                    }
                }
            }
        }

        /// <summary>
        ///  处理工站的业务状态发生改变
        /// </summary>
        /// <param name="station"></param>
        /// <param name="currCustomStatus"></param>
        public virtual void OnStationCustomStatusChanged(IPlatStation station, int currCustomStatus)
        {
            string txt = string.Format("工站:{0,-10} 业务状态：{1} ", station.Name, station.GetCustomStatusName(currCustomStatus));
            ShowTxtInBasePanel(txt);
        }

        /// <summary>
        /// 产品加工完成消息
        /// </summary>
        /// <param name="station">消息发送者</param>
        /// <param name="PassCount">本次生产完成的成品数量</param>
        /// <param name="NGCount">本次生产的次品数量</param>
        /// <param name="NGInfo">次品信息</param>
        public virtual void OnStationProductFinished(IPlatStation station, int passCount, string[] passIDs, int ngCount, string[] ngIDs, string[] ngInfo)
        {
            string txt = string.Format("工站:{0,-10}加工完成：Pass = {1} Fail = {2}", station.Name, passCount, ngCount);

            ShowTxtInBasePanel(txt);
        }

        /// <summary>
        /// 处理工站发来的其他定制化的消息
        /// </summary>
        /// <param name="station"></param>
        /// <param name="msg"></param>
        public virtual void OnStationCustomizeMsg(IPlatStation station, string msgCategory, object[] msgParams)
        {
            string txt = string.Format("工站:{0,-10}定制消息：{1} 参数数量:{2}", station.Name, msgCategory, msgParams == null ? "null" : msgParams.Length.ToString());
            ShowTxtInBasePanel(txt);
        }

        public virtual void OnStationTxtMsg(IPlatStation station, string msgInfo)
        {
            string txt = string.Format("工站:{0,-10}文本消息：{1}", station.Name, msgInfo);
            ShowTxtInBasePanel(txt);
        }

        #endregion

        #region  参数配置

        /// <summary>
        /// 获取一个参数项的值
        /// </summary>
        /// <param name="name"></param>
        /// <param name="isSuccess"></param>
        /// <param name="value"></param>
        public delegate void dgGetParamCfg(string name, out object value, out bool isSuccess);

        /// <summary>
        /// 设置一个参数的值
        /// </summary>
        /// <param name="name"></param>
        /// <param name="value"></param>
        /// <param name="isSuccess"></param>
        public delegate void dgSetParamCfg(string name, object value, out bool isSuccess);

        internal struct PDI
        {
            internal CosParams Describe { get; set; }
            internal string Categoty { get; set; }

            internal object InitValue { get; set; }

            internal dgGetParamCfg FuncGetParam { get; set; }
            internal dgSetParamCfg FuncSetParam { get; set; }
        }

        List<PDI> DeclearedParamDescribes = new List<PDI>(); //所有声明的主工站参数
        List<PDI> DeclearedSysCfgDescribes = new List<PDI>(); //所有声明的系统参数


        /// <summary>
        /// 所有声明的参数类别
        /// 供界面分类使用
        /// </summary>
        internal string[] AllParamCategotys
        {
            get
            {
                List<string> ret = new List<string>();
                foreach (PDI pdi in DeclearedParamDescribes)
                    if (!ret.Contains(pdi.Categoty))
                        ret.Add(pdi.Categoty);
                return ret.ToArray();
            }
        }
        /// <summary>
        /// 获取一个参数类别下的所有参数描述
        /// </summary>
        /// <param name="categoty"></param>
        /// <returns></returns>
        internal CosParams[] GetParamDescribs(string categoty)
        {
            if (!AllParamCategotys.Contains(categoty))
                return null;
            List<CosParams> ret = new List<CosParams>();
            foreach (PDI pdi in DeclearedParamDescribes)
                if (pdi.Categoty == categoty)
                    ret.Add(pdi.Describe);

            return ret.ToArray();
        }


        public CosParams[] SysCfgParamDescribes
        {
            get
            {
                List<CosParams> ret = new List<CosParams>();
                foreach (PDI pdi in DeclearedSysCfgDescribes)
                    ret.Add(pdi.Describe);
                return ret.ToArray();
            }
        }

        /// <summary>
        /// 获取所有系统配置的类别 （供界面排序使用）
        /// </summary>
        internal string[] AllSysCfgCategotys
        {
            get
            {
                List<string> ret = new List<string>();
                foreach (PDI pdi in DeclearedSysCfgDescribes)
                    if (!ret.Contains(pdi.Categoty))
                        ret.Add(pdi.Categoty);
                return ret.ToArray();
            }
        }


        /// <summary>
        /// 获取一个类别下的系统配置项的描述（供界面排序）
        /// </summary>
        /// <param name="categoty"></param>
        /// <returns></returns>
        internal CosParams[] GetSysCfgDescribs(string categoty)
        {
            if (!AllParamCategotys.Contains(categoty))
                return null;
            List<CosParams> ret = new List<CosParams>();
            foreach (PDI pdi in DeclearedSysCfgDescribes)
                if (pdi.Categoty == categoty)
                    ret.Add(pdi.Describe);

            return ret.ToArray();
        }

        /// <summary>
        /// 所有声明的配置参数
        /// </summary>
        public string[] DeclearCfgNames
        {
            get
            {
                List<string> ret = new List<string>();
                foreach (PDI pdi in DeclearedParamDescribes)
                    ret.Add(pdi.Describe.pName);
                return ret.ToArray();
            }
        }

        /// <summary>
        /// 所有由主站声明的系统配置项
        /// </summary>
        public string[] DeclearSysCfgNames
        {
            get
            {
                List<string> ret = new List<string>();
                foreach (PDI pdi in DeclearedSysCfgDescribes)
                    ret.Add(pdi.Describe.pName);
                return ret.ToArray();
            }
        }

        #endregion 

        #region  DIO

        DictionaryEx<string, DioInfo> _dctDeclearedDI = null; //用户（继承类中）声明的DI
        DictionaryEx<string, DioInfo> _dctDeclearedDO = null;

        /// <summary>注册过的Di列表 </summary>
        List<string> _lstDeclearedDINames = new List<string>();
        /// <summary>保存声明时的DI初始值 </summary>
        Dictionary<string, string> _dctDINameChns = new Dictionary<string, string>();
        /// <summary>注册过的DO列表 </summary>
        List<string> _lstDeclearedDONames = new List<string>();
        /// <summary>保存声明时的Do初始值 </summary>
        Dictionary<string, string> _dctDONameChns = new Dictionary<string, string>();

        /// <summary>
        /// 获取所有声明的DI名称
        /// </summary>
        public string[] DeclearedDiNames { get { return _lstDeclearedDINames.ToArray(); } }
        /// <summary>
        /// 获取所有声明的DO名称
        /// </summary>
        public string[] DeclearedDoNames { get { return _lstDeclearedDONames.ToArray(); } }

        enum ResultCode
        {
            Success = 0,
            Unknown,
            未定义的配置项,
            IO未绑定全局通道,
            全局通道名称无效,
            打开设备通道失败,
            获取IO状态失败,
            设置DO状态失败,
            IO未启用,
            参数非法
        }

        public string GetErrorInfo(int errorCode)
        {
            Array ar = Enum.GetValues(typeof(ResultCode));
            foreach (int i in ar)
                if (i == errorCode)
                    return ((ResultCode)errorCode).ToString();

            return "Undefined ErrorCode:" + errorCode;
        }

        /// <summary>
        /// 获取声明的DI的信息
        /// </summary>
        /// <param name="diName"></param>
        /// <returns></returns>
        public DioInfo GetDeclearedDiInfo(string diName)
        {
            if (string.IsNullOrEmpty(diName))
                throw new ArgumentNullException("JFMainStationBase.GetDeclearedDiInfo(string diName) failed by diName is empty string");
            if (!DeclearedDiNames.Contains(diName))
                throw new ArgumentNullException("JFMainStationBase.GetDeclearedDiInfo(string diName) failed by diName is not decleared");
            return _dctDeclearedDI[diName];
        }

        public DioInfo GetDeclearedDoInfo(string doName)
        {
            if (string.IsNullOrEmpty(doName))
                throw new ArgumentNullException("JFMainStationBase.GetDeclearedDoInfo(string doName) failed by doName is empty string");
            if (!DeclearedDoNames.Contains(doName))
                throw new ArgumentNullException("JFMainStationBase.GetDeclearedDoInfo(string doName) failed by doName is not decleared");
            return _dctDeclearedDO[doName];
        }

        /// <summary>
        /// 获取固定DI（开始按钮、停止按钮等）的状态
        /// </summary>
        /// <param name="di"></param>
        /// <param name="isON"></param>
        /// <returns></returns>
        public int GetFixDiSig(FixedDI di, out bool isON)
        {
            DioInfo ioInfo = (DioInfo)_cfg.GetItemValue(di.ToString());
            if (string.IsNullOrEmpty(ioInfo.GlobalChnName))
            {
                isON = false;
                return (int)ResultCode.IO未绑定全局通道;
            }



            IPlatDevice dev = null;
            IDevCellInfo ci = null;
            if (!AppDevChannel.CheckChannel(IDevCellType.DI, ioInfo.GlobalChnName, out dev, out ci, out string errorInfo))
            {
                isON = false;
                return (int)ResultCode.全局通道名称无效;
            }
            if (!dev.IsDeviceOpen && dev.OpenDevice() != 0)
            {
                isON = false;
                return (int)ResultCode.打开设备通道失败;
            }

            if (0 != (dev as IPlatDevice_MotionDaq).GetDio(ci.ModuleIndex).GetDI(ci.ChannelIndex, out isON))
            {
                return (int)ResultCode.获取IO状态失败;
            }

            return (int)ResultCode.Success;
        }


        public int GetFixDoSig(FixedDO dO, out bool isON)
        {
            DioInfo ioInfo = (DioInfo)_cfg.GetItemValue(dO.ToString());
            if (string.IsNullOrEmpty(ioInfo.GlobalChnName))
            {
                isON = false;
                return (int)ResultCode.IO未绑定全局通道;
            }

            IPlatDevice dev = null;
            IDevCellInfo ci = null;
            if (!AppDevChannel.CheckChannel(IDevCellType.DO, ioInfo.GlobalChnName, out dev, out ci, out string errorInfo))
            {
                isON = false;
                return (int)ResultCode.全局通道名称无效;
            }
            if (!dev.IsDeviceOpen && dev.OpenDevice() != 0)
            {
                isON = false;
                return (int)ResultCode.打开设备通道失败;
            }

            if (0 != (dev as IPlatDevice_MotionDaq).GetDio(ci.ModuleIndex).GetDO(ci.ChannelIndex, out isON))
            {
                return (int)ResultCode.获取IO状态失败;
            }

            return (int)ResultCode.Success;

        }

        /// <summary>
        /// 设置DO开（常量）关
        /// </summary>
        /// <param name="dO"></param>
        /// <param name="isON"></param>
        /// <returns></returns>
        public int SetFixDoSig(FixedDO dO, bool isON)
        {
            DioInfo ioInfo = (DioInfo)_cfg.GetItemValue(dO.ToString());
            if (string.IsNullOrEmpty(ioInfo.GlobalChnName))
                return (int)ResultCode.IO未绑定全局通道;


            IPlatDevice dev = null;
            IDevCellInfo ci = null;
            if (!AppDevChannel.CheckChannel(IDevCellType.DO, ioInfo.GlobalChnName, out dev, out ci, out string errorInfo))
                return (int)ResultCode.全局通道名称无效;

            if (!dev.IsDeviceOpen && dev.OpenDevice() != 0)
                return (int)ResultCode.打开设备通道失败;


            if (0 != (dev as IPlatDevice_MotionDaq).GetDio(ci.ModuleIndex).SetDO(ci.ChannelIndex, isON))
                return (int)ResultCode.获取IO状态失败;


            return (int)ResultCode.Success;
        }

        public bool SetFixDoSigs(FixedDO[] dOs, bool[] isONs, out string errorInfo)
        {
            if (null == dOs || 0 == dOs.Length || null == isONs || dOs.Length != isONs.Length)
                throw new ArgumentException("JFMainStationBase.SetFixDoSigs failed by argument error");
            for (int i = 0; i < dOs.Length; i++)
            {
                int ret = SetFixDoSig(dOs[i], isONs[i]);
                if (ret != 0)
                {
                    errorInfo = (isONs[i] ? "打开" : "关闭") + "DO:\"" + dOs[i] + "\"失败：" + GetErrorInfo(ret);
                    return false;
                }
            }

            errorInfo = "Success";
            return true;


        }

        public void ValidatedDeclearedItems()
        {
            foreach (string diName in DeclearedDiNames)
            {
                if (!_dctDeclearedDI.ContainsKey(diName))
                    _dctDeclearedDI.Add(diName, new DioInfo() { GlobalChnName = _dctDINameChns[diName], Enabled = false });

            }
            foreach (string doName in DeclearedDoNames)
            {
                if (!_dctDeclearedDO.ContainsKey(doName))
                    _dctDeclearedDO.Add(doName, new DioInfo() { GlobalChnName = _dctDONameChns[doName], Enabled = false });
            }

            ///主工站配置项
            foreach (PDI pdi in DeclearedParamDescribes)
            {
                if (pdi.FuncGetParam != null) //通过回调函数设置/获取的配置项
                    continue;

                string name = pdi.Describe.pName;
                Type type = Type.GetType(pdi.Describe.ptype);
                object initVal = pdi.InitValue;
                string tag = pdi.Categoty;

                if (!_cfg.ContainsItem(name)) //配置中未包含声明项
                    _cfg.AddItem(name, initVal, tag);
                else
                {
                    if (_cfg.GetItemTag(name) != tag)
                    {
                        _cfg.RemoveItem(name);
                        _cfg.AddItem(name, initVal, tag);
                    }
                    else //标签相同
                    {
                        object val = _cfg.GetItemValue(name);
                        if (null == val)
                        {
                            if (!ToolsFun.IsNullableType(type))
                                _cfg.SetItemValue(name, pdi.InitValue);
                        }
                        else//检查类型是否匹配 , 如果不匹配，赋初始值
                        {
                            if (val.GetType() != type)
                                _cfg.SetItemValue(name, initVal);
                        }
                    }

                }

            }
            _cfg.Save();


            AppCfgFromXml sysCfg = AppHubCenter.Instance.SystemCfg;
            //系统配置项
            for (int i = 0; i < DeclearedSysCfgDescribes.Count; i++)
            {
                string name = DeclearedSysCfgDescribes[i].Describe.pName;
                object initVal = DeclearedSysCfgDescribes[i].InitValue;
                Type type = Type.GetType(DeclearedSysCfgDescribes[i].Describe.ptype);
                string tag = DeclearedSysCfgDescribes[i].Categoty;
                if (!sysCfg.ContainsItem(name))
                    sysCfg.AddItem(name, initVal, tag);
                else //配置项中已存在
                {
                    string tagExisted = sysCfg.GetItemTag(name);
                    if (tag != tagExisted) //标签名称不对
                    {
                        sysCfg.RemoveItem(name);
                        sysCfg.AddItem(name, initVal, tag);
                    }
                    else
                    {
                        object val = sysCfg.GetItemValue(name);
                        if (null == val)
                        {
                            if (!ToolsFun.IsNullableType(type))
                                sysCfg.SetItemValue(name, initVal);
                        }
                        else
                        {
                            if (val.GetType() != type)
                            {
                                sysCfg.RemoveItem(name);
                                sysCfg.AddItem(name, initVal, tag);
                            }
                        }
                    }
                }
            }
            sysCfg.Save();
            UpdateWatchDioList();
            StartWatchDio(); //开始监听DIO
        }

        bool _isWatchDioRunFlag = false;
        Thread threadWatchDio = null;
        bool[] _isNeedWatchFixedDis = null;
        bool[] _isNeedWatchDeclearDis = null;
        bool[] _lastFixedDiSigs = null;//最后一次FixDi们的状态
        bool[] _lastDeclearedDiSigs = null;

        void StartWatchDio()
        {
            _isWatchDioRunFlag = true;
            threadWatchDio = new Thread(WatchDioFunc);
            threadWatchDio.IsBackground = true;
            threadWatchDio.Start();
        }

        /// <summary>
        /// 停止监视DIO线程
        /// 程序退出前调用一次
        /// </summary>
        internal void StopWatchDio()
        {
            _isWatchDioRunFlag = false;
            if (!threadWatchDio.Join(500))
                threadWatchDio.Abort();
        }


        /// <summary>
        /// 监控DIO线程函数
        /// </summary>
        void WatchDioFunc()
        {
            Array arr = Enum.GetValues(typeof(FixedDI));
            FixedDI[] fixedDis = new FixedDI[arr.Length];
            int cnt = 0;
            foreach (int i in arr)
            {
                fixedDis[cnt] = (FixedDI)i;
                cnt++;
            }
            string[] declearedDis = DeclearedDiNames;


            bool isCurrSigOn = false;
            while (_isWatchDioRunFlag)
            {
                //轮询FixedDI
                for (int i = 0; i < _isNeedWatchFixedDis.Length; i++)
                {
                    if (!_isNeedWatchFixedDis[i])
                        continue;
                    else
                    {
                        int nRet = GetFixDiSig(fixedDis[i], out isCurrSigOn);
                        if (0 == nRet && isCurrSigOn != _lastFixedDiSigs[i])
                        {
                            OnFixedDISignal(fixedDis[i], isCurrSigOn);
                            _lastFixedDiSigs[i] = isCurrSigOn;
                        }
                    }
                }
                if (null != _isNeedWatchDeclearDis)//轮询DeclearedDi
                    for (int i = 0; i < _isNeedWatchDeclearDis.Length; i++)
                    {
                        if (!_isNeedWatchDeclearDis[i])
                            continue;
                        int nRet = GetDiSig(declearedDis[i], out isCurrSigOn);
                        if (0 == nRet && isCurrSigOn != _lastDeclearedDiSigs[i])
                        {
                            OnDeclearedDISignal(declearedDis[i], isCurrSigOn);
                            _lastDeclearedDiSigs[i] = isCurrSigOn;
                        }

                    }

                Thread.Sleep(100);
            }
        }
        /// <summary>
        /// 获取输入状态
        /// </summary>
        /// <param name="diName">由DeclearDI声明的输入设备</param>
        /// <param name="isON"></param>
        /// <returns></returns>
        public int GetDiSig(string diName, out bool isON)
        {
            if (string.IsNullOrWhiteSpace(diName))
                throw new ArgumentNullException("JFMainStationBase.GetDiSig(diName) failed by diName is empty string");
            if (!_lstDeclearedDINames.Contains(diName))
                throw new ArgumentException("JFMainStationBase.GetDiSig(diName) failed by diName is not decleared");

            if (string.IsNullOrEmpty(_dctDeclearedDI[diName].GlobalChnName))
            {
                isON = false;
                return (int)ResultCode.IO未绑定全局通道;
            }

            IPlatDevice dev = null;
            IDevCellInfo ci = null;
            if (!AppDevChannel.CheckChannel(IDevCellType.DI, _dctDeclearedDI[diName].GlobalChnName, out dev, out ci, out string errorInfo))
            {
                isON = false;
                return (int)ResultCode.全局通道名称无效;
            }
            if (!dev.IsDeviceOpen && dev.OpenDevice() != 0)
            {
                isON = false;
                return (int)ResultCode.打开设备通道失败;
            }

            if (0 != (dev as IPlatDevice_MotionDaq).GetDio(ci.ModuleIndex).GetDI(ci.ChannelIndex, out isON))
            {
                return (int)ResultCode.获取IO状态失败;
            }

            return (int)ResultCode.Success;
        }


        /// <summary>
        /// 获取输出状态
        /// </summary>
        /// <param name="doName">由DeclearDO声明的输出设备</param>
        /// <param name="isON"></param>
        /// <returns></returns>
        public int GetDoSig(string doName, out bool isON)
        {
            if (string.IsNullOrWhiteSpace(doName))
                throw new ArgumentNullException("IMainStationBase.GetDoSig(doName) failed by doName is empty string");
            if (!_lstDeclearedDONames.Contains(doName))
                throw new ArgumentException("IMainStationBase.GetDoSig(doName) failed by doName is not decleared");

            if (string.IsNullOrEmpty(_dctDeclearedDO[doName].GlobalChnName))
            {
                isON = false;
                return (int)ResultCode.IO未绑定全局通道;
            }

            IPlatDevice dev = null;
            IDevCellInfo ci = null;
            if (!AppDevChannel.CheckChannel(IDevCellType.DO, _dctDeclearedDO[doName].GlobalChnName, out dev, out ci, out string errorInfo))
            {
                isON = false;
                return (int)ResultCode.全局通道名称无效;
            }
            if (!dev.IsDeviceOpen && dev.OpenDevice() != 0)
            {
                isON = false;
                return (int)ResultCode.打开设备通道失败;
            }

            if (0 != (dev as IPlatDevice_MotionDaq).GetDio(ci.ModuleIndex).GetDO(ci.ChannelIndex, out isON))
            {
                return (int)ResultCode.获取IO状态失败;
            }

            return (int)ResultCode.Success;
        }

        /// <summary>
        /// 更新Dio监控列表
        /// </summary>
        internal void UpdateWatchDioList()
        {
            string[] arFixedDINames = Enum.GetNames(typeof(FixedDI));
            string[] arDeclearedDis = DeclearedDiNames;
            if (null == _isNeedWatchFixedDis)
            {
                _isNeedWatchFixedDis = new bool[arFixedDINames.Length];
                _lastFixedDiSigs = new bool[arFixedDINames.Length];
            }
            if (null == _isNeedWatchDeclearDis)
            {
                if (null != arDeclearedDis && arDeclearedDis.Length > 0)
                {
                    _isNeedWatchDeclearDis = new bool[arDeclearedDis.Length];
                    _lastDeclearedDiSigs = new bool[arDeclearedDis.Length];
                }
            }


            for (int i = 0; i < arFixedDINames.Length; i++)
            {
                string diName = arFixedDINames[i];
                DioInfo ioinfo = (DioInfo)_cfg.GetItemValue(diName);
                if (ioinfo == null || string.IsNullOrEmpty(ioinfo.GlobalChnName))
                    _isNeedWatchFixedDis[i] = false;
                else
                {
                    if (!ioinfo.Enabled)
                        _isNeedWatchFixedDis[i] = false;
                    else
                        _isNeedWatchFixedDis[i] = true;
                }
            }
            if (_isNeedWatchDeclearDis != null)
                for (int i = 0; i < arDeclearedDis.Length; i++)
                {
                    DioInfo ioInfo = GetDeclearedDiInfo(arDeclearedDis[i]);
                    if (string.IsNullOrEmpty(ioInfo.GlobalChnName))
                        _isNeedWatchDeclearDis[i] = false;
                    else
                    {
                        if (!ioInfo.Enabled)
                            _isNeedWatchDeclearDis[i] = false;
                        else
                            _isNeedWatchDeclearDis[i] = true;
                    }
                }


        }

        /// <summary>
        /// 收到控制面板上的开始/停止等按钮(固定配置的DI)的状态改变消息的处理函数
        /// </summary>
        protected virtual void OnFixedDISignal(FixedDI di, bool isSigON)
        {
            ShowTxtInBasePanel(di.ToString() + (isSigON ? "按下" : "抬起"));
            int ret = 0;
            string errorInfo;
            string showInfo;
            switch (di)
            {
                case FixedDI.复位按钮:
                    if (IsFixedDoEnabled(FixedDO.复位按钮灯))
                    {
                        ret = SetFixDoSig(FixedDO.复位按钮灯, isSigON);
                        if (ret != 0)
                            ShowTxtInBasePanel((isSigON ? "打开" : "关闭") + "复位按钮灯失败，Error:" + GetErrorInfo(ret));

                    }
                    if (isSigON)
                    {
                        if (!Reset(out errorInfo))
                            showInfo = "复位失败，Error:" + GetErrorInfo(ret);
                        else
                            showInfo = "设备开始复位";
                        ShowTxtInBasePanel(showInfo);

                    }
                    break;
                case FixedDI.开始按钮:
                    if (IsFixedDoEnabled(FixedDO.开始按钮灯))
                    {
                        ret = SetFixDoSig(FixedDO.开始按钮灯, isSigON);
                        if (ret != 0)
                            ShowTxtInBasePanel((isSigON ? "打开" : "关闭") + "开始按钮灯失败，Error:" + GetErrorInfo(ret));

                    }
                    if (isSigON)
                    {
                        if (WorkStatus == IWorkStatus.Pausing)
                        {
                            if (!Resume(out errorInfo))
                                showInfo = "恢复运行失败，Error:" + GetErrorInfo(ret);
                            else
                                showInfo = "设备恢复运行";
                        }
                        else
                        {
                            if (!Start(out errorInfo))
                                showInfo = "开始运行失败，Error:" + GetErrorInfo(ret);
                            else
                                showInfo = "设备开始运行";
                        }
                        ShowTxtInBasePanel(showInfo);
                    }
                    break;
                case FixedDI.暂停按钮:
                    if (IsFixedDoEnabled(FixedDO.暂停按钮灯))
                    {
                        ret = SetFixDoSig(FixedDO.暂停按钮灯, isSigON);
                        if (ret != 0)
                            ShowTxtInBasePanel((isSigON ? "打开" : "关闭") + "暂停按钮灯失败，Error:" + GetErrorInfo(ret));

                    }
                    if (isSigON)
                    {
                        if (!Pause(out errorInfo))
                            showInfo = "暂停失败，Error:" + GetErrorInfo(ret);
                        else
                            showInfo = "设备已暂停";
                        ShowTxtInBasePanel(showInfo);
                    }
                    break;
                case FixedDI.停止按钮:
                    if (IsFixedDoEnabled(FixedDO.停止按钮灯))
                    {
                        ret = SetFixDoSig(FixedDO.停止按钮灯, isSigON);
                        if (ret != 0)
                            ShowTxtInBasePanel((isSigON ? "打开" : "关闭") + "停止按钮灯失败，Error:" + GetErrorInfo(ret));

                    }
                    if (isSigON)
                    {
                        if (!Stop(out errorInfo))
                            showInfo = "停止失败，Error:" + GetErrorInfo(ret);
                        else
                            showInfo = "设备已停止";
                        ShowTxtInBasePanel(showInfo);
                    }
                    break;
                case FixedDI.急停按钮:
                    if (IsFixedDoEnabled(FixedDO.急停按钮灯))
                    {
                        ret = SetFixDoSig(FixedDO.急停按钮灯, isSigON);
                        if (ret != 0)
                            ShowTxtInBasePanel((isSigON ? "打开" : "关闭") + "急停按钮灯失败，Error:" + GetErrorInfo(ret));

                    }
                    if (isSigON)
                    {
                        if (!Stop(out errorInfo))
                            showInfo = "停止失败，Error:" + GetErrorInfo(ret);
                        else
                            showInfo = "设备已停止";
                        ShowTxtInBasePanel(showInfo);
                    }
                    break;
            }
        }


        public bool IsFixedDoEnabled(FixedDO fd)
        {
            DioInfo ioinfo = (DioInfo)_cfg.GetItemValue(fd.ToString());
            if (ioinfo == null || string.IsNullOrEmpty(ioinfo.GlobalChnName))
                return false;
            else
            {
                if (!ioinfo.Enabled)
                    return false;
                else
                    return true;
            }
        }


        /// <summary>
        /// 设置输出状态
        /// </summary>
        /// <param name="doName">由DeclearDO声明的输出设备</param>
        /// <param name="isON"></param>
        /// <returns></returns>
        public int SetDoSig(string doName, bool isON)
        {
            if (string.IsNullOrWhiteSpace(doName))
                throw new ArgumentNullException("IMainStationBase.SetDoSig(doName) failed by doName is empty string");
            if (!_lstDeclearedDONames.Contains(doName))
                throw new ArgumentException("IMainStationBase.SetDoSig(doName) failed by doName is not decleared");

            if (string.IsNullOrEmpty(_dctDeclearedDO[doName].GlobalChnName))
                return (int)ResultCode.IO未绑定全局通道;


            IPlatDevice dev = null;
            IDevCellInfo ci = null;
            if (!AppDevChannel.CheckChannel(IDevCellType.DO, _dctDeclearedDO[doName].GlobalChnName, out dev, out ci, out string errorInfo))
                return (int)ResultCode.全局通道名称无效;

            if (!dev.IsDeviceOpen && dev.OpenDevice() != 0)
                return (int)ResultCode.打开设备通道失败;


            if (0 != (dev as IPlatDevice_MotionDaq).GetDio(ci.ModuleIndex).SetDO(ci.ChannelIndex, isON))
                return (int)ResultCode.获取IO状态失败;

            return (int)ResultCode.Success;
        }

        /// <summary>
        /// 收到自定义DI 状态改变消息
        /// </summary>
        /// <param name="diName"></param>
        /// <param name="isSigON"></param>
        protected virtual void OnDeclearedDISignal(string diName, bool isSigON)
        {

        }



        #endregion

        #region  UI 面板

        ///主工站UI面板，      
        Control _uiPanel = null;
        /// <summary>
        /// 人机交互面板，用于在主窗口中显示 
        /// </summary>
        /// <returns></returns>
        public virtual Control UIPanel
        {
            get
            {
                if (null == _uiPanel)
                {
                    lock (this)
                    {
                        if (null == _uiPanel)
                        {
                            UcMainStationBasePanel pn = new UcMainStationBasePanel();
                            pn.SetMainStation(this);
                            _uiPanel = pn;

                        }
                    }
                }
                return _uiPanel;
            }

        }


        RichTextBox _briefPanel = null;
        //显示联系人等帮助信息
        public virtual Control BriefPanel
        {
            get
            {
                return _briefPanel;
            }
        }

        ///主工站参数配置面板
        UcMainStationBaseCfg _cfgUI = null;// new UcMainStationBaseCfg();

        public virtual Control ConfigPanel
        {
            get
            {
                if (null == _cfgUI)
                {
                    lock (this)
                    {
                        if (null == _cfgUI)
                        {
                            UcMainStationBaseCfg ctrl = new UcMainStationBaseCfg();
                            ctrl.SetMainStation(this);
                            _cfgUI = ctrl;
                        }
                    }
                }
                return _cfgUI;
            }

        }

        /// <summary>
        /// 用户测试界面
        /// </summary>
        public virtual Control TestPanel
        {
            get; private set;
        } = null;



        void ShowTxtInBasePanel(string txt)
        {
            //if (UIPanel is UcMainStationBasePanel)
            //    (UIPanel as UcMainStationBasePanel).ShowTxt(txt);
        }
        #endregion
    }
}
