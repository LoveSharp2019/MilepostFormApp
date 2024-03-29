using Cell.DataModel;
using Cell.Interface;
using Sys.IStations;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Tissue.UI;

namespace Body.IMainStation
{
    /// <summary>
    /// 显示工站的运行信息
    /// </summary>
    public partial class FormStationsRunInfo : Form
    {


        public FormStationsRunInfo()
        {
            InitializeComponent();
            AppStationManager mgr = AppHubCenter.Instance.StationMgr;
            mgr.EventStationWorkStatusChanged += OnStationWorkStatusChanged;
            mgr.EventStationCustomStatusChanged += OnStationCustomStatusChanged;
            mgr.EventStationTxtMsg += OnStationTxtMsg;
            mgr.ActStationProductFinished += OnStationProductFinished;
            mgr.ActStationCustomizeMsg += OnStationCustomizeMsg;
        }

        int _maxTips = 100;

        private void FormStationsRunInfo_Load(object sender, EventArgs e)
        {
            IsHideWhenFormClose = true;
            AdjustStationList();

        }

        Dictionary<IPlatStation, UcRichTextScrollTips> _dctStationTips = new Dictionary<IPlatStation, UcRichTextScrollTips>();

        bool _isAdjusting = false;
        void AdjustStationList()
        {
            _isAdjusting = true;
            //清空汇总信息
            while (tabControl1.TabCount > 0)
                tabControl1.TabPages.RemoveAt(0);

            TabPage tp = new TabPage("信息汇总");
            tabControl1.TabPages.Add(tp);
            ucGatherTips.Dock = DockStyle.Fill;
            ucGatherTips.MaxTipsCount = 100;
            tp.Controls.Add(ucGatherTips);

            _dctStationTips.Clear();
            AppStationManager mgr = AppHubCenter.Instance.StationMgr;
            string[] stationNames = mgr.AllStationNames();
            if (null == stationNames)
                return;
            foreach (string sn in stationNames)
            {
                tp = new TabPage(sn);
                tabControl1.TabPages.Add(tp);

                IPlatStation station = mgr.GetStation(sn);
                UcRichTextScrollTips rchInfos = new UcRichTextScrollTips();
                _dctStationTips.Add(station, rchInfos);
                rchInfos.MaxTipsCount = _maxTips;
                rchInfos.Dock = DockStyle.Fill;
                tp.Controls.Add(rchInfos);
                tabControl1.SelectedTab = tp;

            }
            tabControl1.SelectedIndex = 0;

            _isAdjusting = false;
        }


        void ShowStationTxt(IPlatStation station, string txt)
        {
            if (_isAdjusting)
                return;
            if (!_dctStationTips.ContainsKey(station))
                return;
            if (string.IsNullOrEmpty(txt))
                return;
            //在工站界面中显示的信息
            _dctStationTips[station].AppendText(txt);
            //在汇总界面中显示信息
            string censusInfo = (station as IPlatStation).Name + ":" + txt;
            ucGatherTips.AppendText(censusInfo);

        }

        void OnStationWorkStatusChanged(object station, IWorkStatus currWorkStatus)
        {
            if (_isAdjusting)
                return;
            if (!Created)
                return;
            IPlatStation st = station as IPlatStation;

            string info = "工作状态:" + IStationBase.WorkStatusName(currWorkStatus);
            ShowStationTxt(st, info);

        }

        void OnStationCustomStatusChanged(object station, int currCustomStatus)
        {
            if (_isAdjusting)
                return;
            if (!Created)
                return;
            IPlatStation st = station as IPlatStation;
            if (!_dctStationTips.ContainsKey(st))
                return;

            string txt = "运行状态:" + st.GetCustomStatusName(currCustomStatus);
            ShowStationTxt(st, txt);
        }

        delegate void dgStationTxtMsg(object station, string msgInfo);
        void OnStationTxtMsg(object station, string msgInfo)
        {
            if (_isAdjusting)
                return;
            if (!Created)
                return;
            if (string.IsNullOrEmpty(msgInfo))
                return;
            IPlatStation st = station as IPlatStation;
            if (!_dctStationTips.ContainsKey(st))
                return;
            ShowStationTxt(st, "运行信息：" + msgInfo);
        }

        delegate void dgStationCustomizeMsg(object station, string msgCategory, object[] msgParams);
        void OnStationCustomizeMsg(object station, string msgCategory, object[] msgParams)
        {
            if (_isAdjusting)
                return;
            if (!Created)
                return;
            IPlatStation st = station as IPlatStation;
            if (!_dctStationTips.ContainsKey(st))
                return;
            string txt = "CustomizeMsg:Categoty=\"" + msgCategory + "\"参数:...";
            ShowStationTxt(st, txt);
        }

        delegate void dgStationProductFinished(object station, int passCount, string[] passIDs, int ngCount, string[] ngIDs, string[] ngInfo);
        void OnStationProductFinished(object station, int passCount, string[] passIDs, int ngCount, string[] ngIDs, string[] ngInfo)
        {
            if (_isAdjusting)
                return;
            if (!Created)
                return;
            IPlatStation st = station as IPlatStation;
            if (!_dctStationTips.ContainsKey(st))
                return;

            string txt = " 生产完成： 良品数：" + passCount + " 次品数：" + passCount;
            ShowStationTxt(st, txt);

        }




        /// <summary>
        /// 用隐藏代替关闭窗口
        /// </summary>
        public bool IsHideWhenFormClose { get; set; }




        private void FormStationsRunInfo_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (IsHideWhenFormClose)
            {
                e.Cancel = true;
                Hide();
                return;
            }

            e.Cancel = false;
        }


        /// <summary>
        /// 更新当前工站列表
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btUpdateStations_Click(object sender, EventArgs e)
        {
            AdjustStationList();
        }
    }
}
