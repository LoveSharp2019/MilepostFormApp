using Cell.DataModel;
using Cell.Interface;
using Cell.Tools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Sys.IStations
{

    /// <summary>
    /// 工站管理
    /// </summary>
    public class AppStationManager
    {
        /// <summary>
        /// 工站运行状态改变事件
        /// Run/Stop/Pause
        /// </summary>
        public event WorkStatusChange EventStationWorkStatusChanged;
        /// <summary>
        /// 工站自定义状态改变
        /// 正在上料/正在加工 等等...
        /// </summary>
        public event CustomStatusChange EventStationCustomStatusChanged;
        /// <summary>
        /// 工站产品加工完成事件
        /// </summary>
        public Action<object, int, string[], int, string[], string[]> ActStationProductFinished;

        /// <summary>
        /// 工站自定义消息
        /// </summary>
        public Action<object, string, object[]> ActStationCustomizeMsg;

        /// <summary>
        /// 文本消息，
        /// </summary>
        public event WorkMsgInfo EventStationTxtMsg;

        //本对象内部 向外部对象发送消息的事件
        delegate void _DlgStationWsChange(IPlatStation station, IWorkStatus workStatus);
        delegate void _DlgStationCsChange(IPlatStation station, int customStatus);
        delegate void _DlgStationTxtMsg(IPlatStation station, string msg);
        delegate void _DlgStationProductFinish(IPlatStation station, int passCount, string[] passIDs, int ngCount, string[] ngIDs, string[] ngInfo);
        delegate void _DlgCustomizeMsg(IPlatStation station, string msgCategory, object[] msgParams);


        internal AppStationManager(string cfgPath)
        {
            _cfg.Load(cfgPath, true);
            if (!_cfg.ContainsItem("StationEnabled"))
            {
                _dictStationEnabled = new DictionaryEx<string, bool>();
                _cfg.AddItem("StationEnabled", _dictStationEnabled);
            }
            else
                _dictStationEnabled = _cfg.GetItemValue("StationEnabled") as DictionaryEx<string, bool>;

            List<string> existedStationNames = _initorStationNames();
            List<string> stationNamesInCfg = _dictStationEnabled.Keys.ToList();
            if (null == existedStationNames)
                _dictStationEnabled.Clear();
            else
            {
                foreach (string cfgName in stationNamesInCfg) //去除多余的项
                    if (!existedStationNames.Contains(cfgName))
                        _dictStationEnabled.Remove(cfgName);
                foreach (string exsitedName in existedStationNames) //添加缺少的项
                    if (!_dictStationEnabled.ContainsKey(exsitedName))
                    {
                        _dictStationEnabled.Add(exsitedName, true);
                    }
            }

            ///添加默认的消息回调
            foreach (string stationName in _dictStationEnabled.Keys)
            {
                IPlatStation station = GetStation(stationName);
             
                station.WorkStatusChanged += StationWorkStatusChanged;
                station.CustomStatusChanged += StationCustomStatusChanged;
                if (station is ICmdWorkBase)
                {
                    (station as ICmdWorkBase).WorkMsg2Outter += StationTxtMsg;
                    if (station is IStationBase)
                    {
                        (station as IStationBase).ActStationCustomizeMsg += StationCustomizeMsg;
                        (station as IStationBase).ActProductFinished += StationProductFinished;
                    }
                }                       
            }

            _cfg.Save();
            DeclearedStationNames = new List<string>();
        }

        AppCfgFromXml _cfg = new AppCfgFromXml();

        DictionaryEx<string, bool> _dictStationEnabled = null;



        IPlatMainStation _mainStation = null; //主工站
        /// <summary>
        /// 为应用程序注册一个主工站，在Application.Run()运行之前调用
        /// </summary>
        /// <param name=""></param>
        public void DeclearMainStation(IPlatMainStation mainStation)
        {
            if (_mainStation != null)
                throw new Exception("Main Station is already Registed!");
            if (null == mainStation)
                throw new ArgumentNullException("Main Station is null object");
            if (mainStation == _mainStation)
                return;
            _mainStation = mainStation;

            // 监听 主工站绑定的IO
            //if (_mainStation is IMainStationBase)
            //    (_mainStation as IMainStationBase).ValidatedDeclearedItems();
        }

        public IPlatMainStation MainStation { get { return _mainStation; } }

        internal List<string> DeclearedStationNames { get; private set; }

        /// <summary>
        /// 接收一条工站日志记录,保存
        /// 如果工站自身未提供UI，则会在架构附加的StationUI上显示
        /// 如果工站自身有UI，则忽略显示功能
        /// </summary>
        /// <param name="info"></param>
        public void OnStationLog(IPlatStation station, string info, int level, LogMode mode)
        {

        }

        /// <summary>
        /// 注册一个工站（不可删除）,
        /// 在Application.Run()运行之前调用
        /// </summary>
        /// <param name="station"></param>
        public void DeclearStation(IPlatStation station)
        {
            if (station == null)
                throw new ArgumentNullException("AppStationManager.DeclearStation(IPlatStation station) failed by station = null");
            string name = station.Name;
            if (string.IsNullOrEmpty(name))
                throw new ArgumentException("AppStationManager.DeclearStation(IPlatStation station) failed by station's Name null or empty");
            if (_initorStationNames().Contains(name))
            {
                IPlatInitializable existedStation = AppHubCenter.Instance.InitorManager.GetInitor(name);
                if (existedStation.GetType() != station.GetType())
                {
                    throw new Exception("AppStationManager.DeclearStation(IPlatStation station) failed by:Exist a station with same name and type unmatched: Decleared Type = " + station.GetType() + " ; Existed Type = " + existedStation.GetType());
                }
                (existedStation as IPlatStation).Name = name;
                DeclearedStationNames.Add(station.Name);
                return;
            }

            AppHubCenter.Instance.InitorManager.Add(station.Name, station);
            DeclearedStationNames.Add(station.Name);
        }

        public void StationTxtMsg(object sender, string msgInfo)
        {
            _DlgStationTxtMsg txtMsgCaller = new _DlgStationTxtMsg(_StationTxtMsg);
            txtMsgCaller.BeginInvoke(sender as IPlatStation, msgInfo, null, null);
        }

        void _MsgInvokeEndCallback(IAsyncResult ar)
        {
            _DlgStationTxtMsg txtMsgCaller = ar.AsyncState as _DlgStationTxtMsg;
            txtMsgCaller.EndInvoke(ar);
        }


        public void _StationTxtMsg(IPlatStation station, string msgInfo)
        {
            if (_StationMsgReciever.ContainsKey(station))
            {
                List<IStationMsgReceiver> uis = _StationMsgReciever[station];
                foreach (IStationMsgReceiver ui in uis)
                    ui.OnTxtMsg(msgInfo);
            }
            ///将消息发送到MainStaion处理
            MainStation.OnStationTxtMsg(station, msgInfo);
            EventStationTxtMsg?.Invoke(station, msgInfo);

        }


        /// <summary>
        /// 站点发出 UI界面回调
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="workStatus"></param>
        void StationWorkStatusChanged(object sender, IWorkStatus workStatus)
        {
            _DlgStationWsChange dlgWSChanged = new _DlgStationWsChange(_StationWorkStatusChanged);
            //dlgWSChanged.BeginInvoke(sender as IPlatStation, workStatus, _WSInvokeEndCallback, dlgWSChanged);
            dlgWSChanged.BeginInvoke(sender as IPlatStation, workStatus, null, null);
        }

        void _WSInvokeEndCallback(IAsyncResult ar)
        {
            _DlgStationWsChange dlgWSChanged = ar.AsyncState as _DlgStationWsChange;
            dlgWSChanged.EndInvoke(ar);
        }

        void StationCustomStatusChanged(object sender, int currCustomStatus)
        {
            _DlgStationCsChange csInvoker = new _DlgStationCsChange(_StationCustomStatusChanged);
            //csInvoker.BeginInvoke(sender as IPlatStation, currCustomStatus, _CSInvokeEndCallback, csInvoker);
            csInvoker.BeginInvoke(sender as IPlatStation, currCustomStatus, null, null);
        }

        void _CSInvokeEndCallback(IAsyncResult ar)
        {
            _DlgStationCsChange csInvoker = ar.AsyncState as _DlgStationCsChange;
            csInvoker.EndInvoke(ar);
        }

        void StationCustomizeMsg(object station, string msgCategory, object[] msgParams)
        {
            _DlgCustomizeMsg invoker = new _DlgCustomizeMsg(_StationCustomizeMsg);
            //invoker.BeginInvoke(station as IPlatStation, msgCategory, msgParams, _CMInvokeEndCallback, invoker);
            invoker.BeginInvoke(station as IPlatStation, msgCategory, msgParams, null, null);
        }

        void _CMInvokeEndCallback(IAsyncResult ar)
        {
            _DlgCustomizeMsg invoker = ar.AsyncState as _DlgCustomizeMsg;
            invoker.EndInvoke(ar);
        }



        void StationProductFinished(object station, int passCount, string[] passIDs, int ngCount, string[] ngIDs, string[] ngInfo)
        {
            _DlgStationProductFinish invoker = new _DlgStationProductFinish(_StationProductFinished);
            invoker.BeginInvoke(station as IPlatStation, passCount, passIDs, ngCount, ngIDs, ngInfo, _PFInvokeEndCallback, invoker);
            //invoker
        }

        void _PFInvokeEndCallback(IAsyncResult ar)
        {
            _DlgStationProductFinish invoker = ar.AsyncState as _DlgStationProductFinish;
            invoker.EndInvoke(ar);
        }




        /// <summary>
        ///  通过配置界面添加的所有工站名称
        /// </summary>
        List<string> _initorStationNames()
        {
            List<string> ret = new List<string>();
            string[] initStationNames = AppHubCenter.Instance.InitorManager.GetIDs(typeof(IPlatStation));
            ret.AddRange(initStationNames);
            return ret;
        }


        /// <summary>
        /// 获取程序中所有可用的工站
        /// </summary>
        /// <returns></returns>
        public string[] AllStationNames()
        {
            List<string> ret = new List<string>();
            foreach (string declearedStationName in DeclearedStationNames)
                ret.Add(declearedStationName);
            List<string> allStationNames = _initorStationNames();
            foreach (string sn in allStationNames)
                if (!DeclearedStationNames.Contains(sn))
                    ret.Add(sn);

            return ret.ToArray();
        }

        public IPlatStation GetStation(string stationName)
        {
            AppInitorManager initorMgr = AppHubCenter.Instance.InitorManager;
            string[] allInitorStation = initorMgr.GetIDs(typeof(IPlatStation)); //通过架构UI创建的工站对象
            if (null == allInitorStation)
                return null;
            foreach (string stName in allInitorStation)
                if (stationName == stName)
                    return initorMgr.GetInitor(stationName) as IPlatStation;
            return null;
        }


        /// <summary>
        /// 所有工站的消息接收者
        /// </summary>
        Dictionary<IPlatStation, List<IStationMsgReceiver>> _StationMsgReciever = new Dictionary<IPlatStation, List<IStationMsgReceiver>>();

        /// <summary>
        /// 为工站附加一个ui ， 一般情况下由App中的架构功能调用
        /// 如：不提供RealtimeUI的工站，系统会自动指派一个ui 
        /// </summary>
        /// <param name="station"></param>
        /// <param name="ui"></param>
        public void AppendStationMsgReceiver(IPlatStation station, IStationMsgReceiver rcver)
        {
            if (!_StationMsgReciever.ContainsKey(station))
                _StationMsgReciever.Add(station, new List<IStationMsgReceiver>());

            List<IStationMsgReceiver> uis = _StationMsgReciever[station];
            if (!uis.Contains(rcver))
                uis.Add(rcver);
        }

        /// <summary>
        /// 将一个ui移除
        /// </summary>
        /// <param name="ui"></param>
        public void RemoveStationMsgReciever(IStationMsgReceiver ui)
        {
            foreach (List<IStationMsgReceiver> stationUIS in _StationMsgReciever.Values)
                if (stationUIS.Contains(ui))
                    stationUIS.Remove(ui);
        }


        /// <summary>
        /// 处理工站状态改变
        /// 未提供JustRealTimeUI的工站通过此函数刷新界面
        /// 以提供JustRealTimeUI的工站 只是通过此函数向MainStation发送消息，相关界面更新功能由工站自身维护
        /// </summary>
        /// <param name="station"></param>
        /// <param name="currWorkStatus"></param>
        void _StationWorkStatusChanged(IPlatStation station, IWorkStatus currWorkStatus)
        {
            if (_StationMsgReciever.ContainsKey(station))
            {
                List<IStationMsgReceiver> uis = _StationMsgReciever[station];
                foreach (IStationMsgReceiver ui in uis)
                    ui.OnWorkStatusChanged(currWorkStatus);
            }
            ///将消息发送到MainStaion处理
            MainStation.OnStationWorkStatusChanged(station, currWorkStatus);
            EventStationWorkStatusChanged?.Invoke(station, currWorkStatus);
        }


        /// <summary>
        ///  处理工站的业务状态发生改变
        /// 未提供JustRealTimeUI的工站通过此函数刷新界面
        /// 以提供JustRealTimeUI的工站 只是通过此函数向MainStation发送消息，相关界面更新功能由工站自身维护
        /// </summary>
        /// <param name="station"></param>
        /// <param name="currCustomStatus"></param>
        void _StationCustomStatusChanged(IPlatStation station, int currCustomStatus)
        {

            if (_StationMsgReciever.ContainsKey(station))
            {
                List<IStationMsgReceiver> uis = _StationMsgReciever[station];
                foreach (IStationMsgReceiver ui in uis)
                    ui.OnCustomStatusChanged(currCustomStatus);
            }
            MainStation.OnStationCustomStatusChanged(station, currCustomStatus);
            EventStationCustomStatusChanged?.Invoke(station, currCustomStatus);

        }

        /// <summary>
        /// 产品加工完成消息
        /// 未提供JustRealTimeUI的工站通过此函数刷新界面
        /// 以提供JustRealTimeUI的工站 只是通过此函数向MainStation发送消息，相关界面更新功能由工站自身维护
        /// </summary>
        /// <param name="station">消息发送者</param>
        /// <param name="PassCount">本次生产完成的成品数量</param>
        /// <param name="NGCount">本次生产的次品数量</param>
        /// <param name="NGInfo">次品信息</param>
        void _StationProductFinished(IPlatStation station, int passCount, string[] passIDs, int ngCount, string[] ngIDs, string[] ngInfo)
        {

            if (_StationMsgReciever.ContainsKey(station))
            {
                List<IStationMsgReceiver> uis = _StationMsgReciever[station];
                foreach (IStationMsgReceiver ui in uis)
                    ui.OnProductFinished(passCount, passIDs, ngCount, ngIDs, ngInfo);
            }
            MainStation.OnStationProductFinished(station, passCount, passIDs, ngCount, ngIDs, ngInfo);
            ActStationProductFinished?.Invoke(station, passCount, passIDs, ngCount, ngIDs, ngInfo);

        }

        /// <summary>
        /// 处理工站发来的其他定制化的消息
        /// 只是向MainStation发送消息，定制化的界面显示由工站通过自身提供的JustRealTimeUI完成
        /// </summary>
        /// <param name="station"></param>
        /// <param name="msg"></param>
        void _StationCustomizeMsg(IPlatStation station, string msgCategory, object[] msgParams)//异步模式有问题(异步线程未运行'),待日后改进
        {

            if (_StationMsgReciever.ContainsKey(station))
            {
                List<IStationMsgReceiver> uis = _StationMsgReciever[station];
                foreach (IStationMsgReceiver ui in uis)
                    ui.OnCustomizeMsg(msgCategory, msgParams);
            }

            MainStation.OnStationCustomizeMsg(station, msgCategory, msgParams);
            ActStationCustomizeMsg?.Invoke(station, msgCategory, msgParams);
        }

        /// <summary>
        /// 设置工站使能
        /// </summary>
        /// <param name="stationName"></param>
        /// <param name="enable"></param>
        public void SetStationEnabled(string stationName, bool enable)
        {
            if (!_initorStationNames().Contains(stationName))
                throw new ArgumentException("stationName = \"" + stationName + "\" is not included by Station-Name List");
            if (!_dictStationEnabled.ContainsKey(stationName))
            {
                _dictStationEnabled.Add(stationName, enable);
                return;
            }
            else
                _dictStationEnabled[stationName] = enable;
            IPlatStation station = GetStation(stationName);
            if (enable)
            {
                station.WorkStatusChanged -= StationWorkStatusChanged;
                station.WorkStatusChanged += StationWorkStatusChanged;
                station.CustomStatusChanged -= StationCustomStatusChanged;
                station.CustomStatusChanged += StationCustomStatusChanged;
                if (station is ICmdWorkBase)
                {
                    (station as ICmdWorkBase).WorkMsg2Outter -= StationTxtMsg;
                    (station as ICmdWorkBase).WorkMsg2Outter += StationTxtMsg;
                }
                if (station is IStationBase)
                {
                    (station as IStationBase).ActStationCustomizeMsg -= StationCustomizeMsg;
                    (station as IStationBase).ActStationCustomizeMsg += StationCustomizeMsg;
                    (station as IStationBase).ActProductFinished -= StationProductFinished;
                    (station as IStationBase).ActProductFinished += StationProductFinished;
                }

            }
            else
            {
                station.WorkStatusChanged -= StationWorkStatusChanged;
                station.CustomStatusChanged -= StationCustomStatusChanged;
                if (station is ICmdWorkBase)
                    (station as ICmdWorkBase).WorkMsg2Outter -= StationTxtMsg;
                if (station is IStationBase)
                {
                    (station as IStationBase).ActStationCustomizeMsg -= StationCustomizeMsg;
                    (station as IStationBase).ActProductFinished -= StationProductFinished;
                }

            }
            _cfg.Save();
        }

        /// <summary>
        /// 获取工站使能
        /// </summary>
        /// <param name="stationName"></param>
        /// <returns></returns>
        public bool GetStationEnabled(string stationName)
        {
            if (!_initorStationNames().Contains(stationName))
                throw new ArgumentException("stationName = \"" + stationName + "\" is not included by Station-Name List");

            if (!_dictStationEnabled.ContainsKey(stationName))
            {
                _dictStationEnabled.Add(stationName, true);
                _cfg.Save();
            }
            return _dictStationEnabled[stationName];
        }

        /// <summary>
        /// 获取所有已使能的工站名称
        /// </summary>
        /// <returns></returns>
        public string[] AllEnabledStationNames()
        {
            List<string> ret = new List<string>();
            List<string> allStationNames = _initorStationNames();
            foreach (string stationName in allStationNames)
                if (GetStationEnabled(stationName))
                    ret.Add(stationName);
            return ret.ToArray();
        }


        bool IsStationRunning(IPlatStation station)
        {
            IWorkStatus ws = station.CurrWorkStatus;
            return ws == IWorkStatus.Running || ws == IWorkStatus.Pausing || ws == IWorkStatus.Interactiving;
        }

        /// <summary>
        /// 停止工站日志记录/显示
        /// 在程序退出前调用
        /// </summary>
        public void Stop()
        {
            string errorInfo;
            MainStation.Stop(out errorInfo);
            string[] stationNames = AllStationNames();
            if (null != stationNames)
                foreach (string stationName in stationNames)
                {
                    IPlatStation station = GetStation(stationName);
                    if (IsStationRunning(station))
                    {
                        ICmdResult ret = station.Stop(1000);
                        if (ret != ICmdResult.Success)
                        {
                            //日后可能添加强制关闭的系统日志...
                            station.Abort();
                        }
                    }
                }
           
            if (null != stationNames)
                foreach (string stationName in stationNames)
                {
                    IPlatStation station = GetStation(stationName);
                    station.WorkStatusChanged -= StationWorkStatusChanged;
                    station.CustomStatusChanged -= StationCustomStatusChanged;
                    if (station is ICmdWorkBase)
                        (station as ICmdWorkBase).WorkMsg2Outter -= StationTxtMsg;

                    if (station is IStationBase)
                    {
                        (station as IStationBase).ActStationCustomizeMsg -= StationCustomizeMsg;
                        (station as IStationBase).ActProductFinished -= StationProductFinished;
                    }
                }

            Thread.Sleep(2000);
        }
    }
}
