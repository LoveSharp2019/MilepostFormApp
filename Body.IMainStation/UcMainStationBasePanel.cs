using Cell.DataModel;
using Cell.UI;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Body.IMainStation
{
    /// <summary>
    /// 主界面公用 显示面板 显示所有的 dio 以及 运行状态
    /// </summary>
    public partial class UcMainStationBasePanel : UserControl
    {

        LampButton[] _fixedDiLamps = null;
        LampButton[] _fixedDoLamps = null;
        bool[] _needFlushFixedDis = null;
        bool[] _needFlushFixedDos = null;

        List<LampButton> _lstDiLamps = new List<LampButton>();//声明的DI
        List<LampButton> _lstDoLamps = new List<LampButton>();
        List<bool> _needFlushLstDis = new List<bool>();//是否需要更新自定义DI
        List<bool> _needFlushLstDos = new List<bool>();

        public UcMainStationBasePanel()
        {
            InitializeComponent();
        }

        private void UcMainStationPanelBase_Load(object sender, EventArgs e)
        {

            _fixedDiLamps = new LampButton[] { lampResetButton, lampStartButton, lampPauseButton, lampStopButton, lampEMGButton };
            _needFlushFixedDis = new bool[] { false, false, false, false, false }; //是否需要更新(红绿黄蜂鸣)信号灯

            _fixedDoLamps = new LampButton[] { lampResetLight, lampStartLight, lampPauseLight, lampStopLight, lampEMGLight, lampRed, lampYellow, lampGreen, lampAlam };
            _needFlushFixedDos = new bool[] { false, false, false, false, false, false, false, false, false };//是否需要更新（控制面板按钮状态）

            AdjustShowPart();
            UpdateUIByStationCfg();
        }


        IMainStationBase _mainStation = null;


        public void SetMainStation(IMainStationBase ms)
        {
            _mainStation = ms;
            if (Created)
                UpdateUIByStationCfg();
        }
        MSShowPart _showPart = MSShowPart.StatusDIO;
        public MSShowPart ShowPart
        {
            get
            {
                return _showPart;
            }
            set
            {
                _showPart = value;
                if (Created)
                    AdjustShowPart();

            }
        }
        void AdjustShowPart()
        {
            if (InvokeRequired)
            {
                Invoke(new Action(AdjustShowPart));
                return;
            }

            switch (_showPart)
            {
                case MSShowPart.None:
                    panelStatus.Visible = false;
                    panelDio.Visible = false;
                    pnCumstom.Top = btPackup.Bottom + 2;
                    break;
                case MSShowPart.Status:
                    panelStatus.Visible = true;
                    panelDio.Visible = false;
                    pnCumstom.Top = panelStatus.Bottom + 2;
                    break;
                case MSShowPart.StatusDIO:
                    panelStatus.Visible = true;
                    panelDio.Visible = true;
                    pnCumstom.Top = panelDio.Bottom + 2;
                    break;
            }

            pnCumstom.Height = Height - pnCumstom.Top - 2;


        }

        internal void OnMSWorkStatus(IWorkStatus ws)
        {
            Invoke(new Action(() =>
            {
                lampWorkStatus.Text = ws.ToString();
                if (ws == IWorkStatus.Pausing || ws == IWorkStatus.Interactiving)
                    lampWorkStatus.LampColor = LampButton.LColor.Yellow;
                else if (ws == IWorkStatus.NormalEnd || ws == IWorkStatus.CommandExit || ws == IWorkStatus.UnStart)
                    lampWorkStatus.LampColor = LampButton.LColor.Gray;
                else if (ws == IWorkStatus.Running)
                    lampWorkStatus.LampColor = LampButton.LColor.Green;
                else
                    lampWorkStatus.LampColor = LampButton.LColor.Red;
            }));
        }

        internal void OnAlarm(bool isAlarm, string info)
        {
            Invoke(new Action(() =>
            {
                if (!isAlarm)
                {
                    lampWarning.LampColor = LampButton.LColor.Gray;
                    lampWarning.Text = "正常未报警/(已消除)";
                }
                else
                {
                    lampWarning.LampColor = LampButton.LColor.Red;
                    lampWarning.Text = info;
                }
            }));
        }

        /// <summary>
        /// 将定制的UIPanel 贴到pnCumstom控件中
        /// </summary>
        /// <param name="ctrl"></param>
        public void AppendCustomUIPanel(Control ctrl)
        {
            pnCumstom.Controls.Clear();
            if (null == ctrl)
                return;
            ctrl.Dock = DockStyle.Fill;
            if (ctrl is Form)
            {
                (ctrl as Form).TopLevel = false;
                (ctrl as Form).FormBorderStyle = FormBorderStyle.None;
            }
            pnCumstom.Controls.Add(ctrl);

        }



        /// <summary>
        /// 根据主公站配置更新界面元素（主要是自定义IO列表）
        /// 
        /// </summary>
        public void UpdateUIByStationCfg()
        {
            if (InvokeRequired)
            {
                Invoke(new Action(UpdateUIByStationCfg));
                return;
            }



            _lstDiLamps.Clear();
            _lstDoLamps.Clear();
            _needFlushLstDis.Clear();
            _needFlushLstDos.Clear();

            if (null == _mainStation)
            {
                Enabled = false;
                lampWorkStatus.Text = "主工站未设置！";
                lampWorkStatus.LampColor = LampButton.LColor.Red;
                return;
            }

            Enabled = true;
            string[] arFixedDINames = Enum.GetNames(typeof(FixedDI));
            for (int i = 0; i < arFixedDINames.Length; i++)
            {
                string diName = arFixedDINames[i];
                DioInfo ioinfo = (DioInfo)_mainStation._cfg.GetItemValue(diName);
                if (ioinfo == null || string.IsNullOrEmpty(ioinfo.GlobalChnName))
                {
                    _needFlushFixedDis[i] = false;
                    _fixedDiLamps[i].BackColor = SystemColors.ControlDarkDark;

                }
                else
                {
                    if (!ioinfo.Enabled)
                    {
                        _needFlushFixedDis[i] = false;
                        _fixedDiLamps[i].BackColor = SystemColors.ControlDark;
                    }
                    else
                    {
                        _needFlushFixedDis[i] = true;
                        _fixedDiLamps[i].BackColor = SystemColors.Control;
                    }
                }

            }

            string[] arFixedDONames = Enum.GetNames(typeof(FixedDO));
            for (int i = 0; i < arFixedDONames.Length; i++)
            {
                string doName = arFixedDONames[i];
                DioInfo ioinfo = (DioInfo)_mainStation._cfg.GetItemValue(doName);
                if (ioinfo == null || string.IsNullOrEmpty(ioinfo.GlobalChnName))
                {
                    _needFlushFixedDos[i] = false;
                    _fixedDoLamps[i].BackColor = SystemColors.ControlDarkDark;

                }
                else
                {
                    if (!ioinfo.Enabled)
                    {
                        _needFlushFixedDos[i] = false;
                        _fixedDoLamps[i].BackColor = SystemColors.ControlDark;
                    }
                    else
                    {
                        _needFlushFixedDos[i] = true;
                        _fixedDoLamps[i].BackColor = SystemColors.Control;
                    }
                }

            }

            string[] diNames = _mainStation.DeclearedDiNames;
            for (int i = 0; i < diNames.Length; i++)
            {
                LampButton lb = new LampButton();
                lb.Text = diNames[i];
                lb.Height = 25;
                lb.Width = 150;
                lb.Enabled = false;
                _lstDiLamps.Add(lb);
                tbPanelDIs.Controls.Add(lb);
                DioInfo ioInfo = _mainStation.GetDeclearedDiInfo(diNames[i]);
                if (string.IsNullOrEmpty(ioInfo.GlobalChnName))
                {
                    _needFlushLstDis.Add(false);
                    lb.BackColor = SystemColors.ControlDarkDark;
                }
                else
                {
                    if (!ioInfo.Enabled)
                    {
                        _needFlushLstDis.Add(false);
                        lb.BackColor = SystemColors.ControlDark;
                    }
                    else
                    {
                        _needFlushLstDis.Add(true);
                        lb.BackColor = SystemColors.Control;
                    }
                }


            }



            string[] doNames = _mainStation.DeclearedDoNames;
            for (int i = 0; i < doNames.Length; i++)
            {
                LampButton lb = new LampButton();
                lb.Text = doNames[i];
                lb.Height = 25;
                lb.Width = 150;
                lb.Enabled = false;
                _lstDoLamps.Add(lb);
                tbPanelDOs.Controls.Add(lb);
                DioInfo ioInfo = _mainStation.GetDeclearedDoInfo(doNames[i]);
                if (string.IsNullOrEmpty(ioInfo.GlobalChnName))
                {
                    _needFlushLstDos.Add(false);
                    lb.BackColor = SystemColors.ControlDarkDark;
                }
                else
                {
                    if (!ioInfo.Enabled)
                    {
                        _needFlushLstDos.Add(false);
                        lb.BackColor = SystemColors.ControlDark;
                    }
                    else
                    {
                        _needFlushLstDos.Add(true);
                        lb.BackColor = SystemColors.Control;
                    }
                }


            }
        }
        private void UcMainStationPanelBase_VisibleChanged(object sender, EventArgs e)
        {

        }

        /// <summary>
        /// 隐藏/显示 IO面板
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btPackup_Click(object sender, EventArgs e)
        {
            if (ShowPart == MSShowPart.None)
                ShowPart = MSShowPart.Status;
            else if (ShowPart == MSShowPart.Status)
                ShowPart = MSShowPart.StatusDIO;
            else
                ShowPart = MSShowPart.None;
        }

        /// <summary>
        /// 状态刷新定时器
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void timer1_Tick(object sender, EventArgs e)
        {
            if (_mainStation == null || !Visible)
            {
                timer1.Interval = 1000;
                return;
            }

            timer1.Interval = 200;
            if (ShowPart != MSShowPart.StatusDIO) //不需要刷新
                return;

            //刷新FixDI
            Array fi = Enum.GetValues(typeof(FixedDI));
            bool sigON = false;
            int i = 0;
            foreach (int v in fi)
            {
                if (!_needFlushFixedDis[i])
                {
                    i++;
                    continue;
                }
                int ret = _mainStation.GetFixDiSig((FixedDI)v, out sigON);
                if (ret == 0)
                {
                    _fixedDiLamps[i].BackColor = SystemColors.Control;
                    if (sigON)
                    {
                        if ((FixedDI)v == FixedDI.急停按钮)
                            _fixedDiLamps[i].LampColor = LampButton.LColor.Red;
                        else
                            _fixedDiLamps[i].LampColor = LampButton.LColor.Green;
                    }
                    else
                    {
                        _fixedDiLamps[i].LampColor = LampButton.LColor.Gray;
                    }
                }
                else
                    _fixedDiLamps[i].BackColor = Color.Orange;
                i++;
            }

            Array fo = Enum.GetValues(typeof(FixedDO));
            i = 0;
            foreach (int v in fo)
            {
                if (!_needFlushFixedDos[i])
                {
                    i++;
                    continue;
                }
                int ret = _mainStation.GetFixDoSig((FixedDO)v, out sigON);
                if (ret == 0)
                {
                    _fixedDoLamps[i].BackColor = SystemColors.Control;
                    if (sigON)
                    {
                        if ((FixedDO)v == FixedDO.红灯)
                            _fixedDoLamps[i].LampColor = LampButton.LColor.Red;
                        else if ((FixedDO)v == FixedDO.黄灯)
                            _fixedDoLamps[i].LampColor = LampButton.LColor.Yellow;
                        else
                            _fixedDoLamps[i].LampColor = LampButton.LColor.Green;
                    }
                    else
                    {
                        _fixedDoLamps[i].LampColor = LampButton.LColor.Gray;
                    }
                }
                else
                    _fixedDoLamps[i].BackColor = Color.Orange;
                i++;
            }

            //刷新自定义DI
            string[] diNames = _mainStation.DeclearedDiNames;
            if (null != diNames)
                for (i = 0; i < diNames.Length; i++)
                {
                    if (!_needFlushLstDis[i])
                        continue;
                    int ret = _mainStation.GetDiSig(diNames[i], out sigON);
                    if (ret != 0)
                        _lstDiLamps[i].BackColor = Color.Orange;
                    else
                    {
                        _lstDiLamps[i].BackColor = SystemColors.Control;
                        if (sigON)
                            _lstDiLamps[i].LampColor = LampButton.LColor.Green;
                        else
                            _lstDiLamps[i].LampColor = LampButton.LColor.Gray;
                    }

                }


            //刷新自定义DO
            string[] doNames = _mainStation.DeclearedDoNames;
            if (null != doNames)
                for (i = 0; i < doNames.Length; i++)
                {
                    if (!_needFlushLstDos[i])
                        continue;
                    int ret = _mainStation.GetDoSig(doNames[i], out sigON);
                    if (ret != 0)
                        _lstDoLamps[i].BackColor = Color.Orange;
                    else
                    {
                        _lstDoLamps[i].BackColor = SystemColors.Control;
                        if (sigON)
                            _lstDoLamps[i].LampColor = LampButton.LColor.Green;
                        else
                            _lstDoLamps[i].LampColor = LampButton.LColor.Gray;
                    }
                }
        }
    }

    /// <summary>
    /// 主工站显示 
    /// </summary>
    public enum MSShowPart
    {
        /// <summary>
        /// 只显示下拉按钮
        /// </summary>
        None = 0,
        /// <summary>
        /// 显示基础信息
        /// </summary>
        Status = 1,

        /// <summary>
        /// 显示基础信息和DIO
        /// </summary>
        StatusDIO = 2,

    }
}
