using Cell.Interface;
using Sys.IStations;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Body.IMainStation
{
    /// <summary>
    /// 生产运行时的主界面
    /// </summary>
    public partial class FormAuto : Form
    {
        public FormAuto()
        {
            InitializeComponent();
        }

        /// <summary>
        /// MainForm收到的消息会发送到本窗口中
        /// </summary>
        public FormStationsRunInfo _fmSri = new FormStationsRunInfo();

        private void FormAuto_Load(object sender, EventArgs e)
        {
            IPlatMainStation mainStation = AppHubCenter.Instance.StationMgr.MainStation;
            if (null == mainStation)
            {
                MessageBox.Show("MainStation is not Regist,App will Exit");
                Process.GetCurrentProcess().Kill();
            }
            Control mainStationPanel = mainStation.UIPanel;
            mainStationPanel.Dock = DockStyle.Fill;
            splitContainer1.Panel2Collapsed = true;

            mainStationPanel.Parent = splitContainer1.Panel1;
            splitContainer1.Panel1.Controls.Add(mainStationPanel);
            mainStationPanel.Show();
            AdjustView();
            _fmSri.TopLevel = false;
            _fmSri.Dock = DockStyle.Fill;
            _fmSri.FormBorderStyle = FormBorderStyle.None;
            _fmSri.Show();
            splitContainer2.Panel2.Controls.Add(_fmSri);
        }


        string[] _currStations = null; //当前正在显示的工站


        /// <summary>
        /// 根据当前已激活的工站，布局界面
        /// </summary>
        public void AdjustView()
        {
            if (InvokeRequired)
            {
                BeginInvoke(new Action(AdjustView));//Invoke(new Action(AdjustView));
                return;
            }
            AppStationManager stationMgr = AppHubCenter.Instance.StationMgr;
            string[] allEnabledStationName = stationMgr.AllEnabledStationNames();
            if (allEnabledStationName == null || allEnabledStationName.Length == 0)
            {
                _currStations = null;
                pnStations.Controls.Clear();
                return;
            }

            if (_currStations != null)
            {
                if (_currStations.Length == allEnabledStationName.Length)
                {
                    bool isSame = true;
                    for (int i = 0; i < _currStations.Length; i++)
                        if (_currStations[i] != allEnabledStationName[i])
                        {
                            isSame = false;
                            break;
                        }
                    if (isSame) //不需要更新界面
                        return;
                }
            }
            _currStations = allEnabledStationName;
            //将当前工站界面和消息回调解除绑定
            foreach (Control ui in pnStations.Controls)
                stationMgr.RemoveStationMsgReciever(ui as IStationMsgReceiver);

            pnStations.Controls.Clear();
            foreach (string enabledStationName in allEnabledStationName)
            {
                IPlatStation station = stationMgr.GetStation(enabledStationName);
                UcStationRealtimeUI ui = new UcStationRealtimeUI();
                ui.JfDisplayMode = UcStationRealtimeUI.JFDisplayMode.simple;
                ui.SetStation(station);
                stationMgr.AppendStationMsgReceiver(station, ui);
                pnStations.Controls.Add(ui);
            }
        }

        private void FormAuto_VisibleChanged(object sender, EventArgs e)
        {
            if (Visible)
            {
                AdjustView();

            }
        }

        /// <summary>
        /// 是否显示工站运行信息
        /// </summary>
        public void RefreshUI()
        {
            AdjustView();
        }


        /// <summary>
        /// 是否显示工站运行信息
        /// </summary>
        public bool IsShowSRI
        {
            get { return !splitContainer1.Panel2Collapsed; }
            set
            {
                splitContainer1.Panel2Collapsed = !value;
                splitContainer1.SplitterDistance = Height - 300;
            }
        }
    }
}
