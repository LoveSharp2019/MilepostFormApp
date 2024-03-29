using Body.IMainStation;
using Body.IMainStation.ProjectHipMainUC;
using Cell.DataModel;
using Cell.IconFont;
using Cell.Interface;
using Cell.Tools;
using Cell.UI;
using Sys.IStations;
using Sys.UI;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Serialization;
using Tissue.UI;

namespace MilepostFormApp
{
    public partial class Form1 : windowBase
    {
        System.Windows.Forms.Timer timerFlush;

        public Form1()
        {
            InitializeComponent();

            this.Text = "Hip尺寸机检测设备AOI";

            this.timerFlush = new System.Windows.Forms.Timer();
            this.timerFlush.Enabled = true;
            this.timerFlush.Interval = 200;
            this.timerFlush.Tick += new System.EventHandler(this.timerFlush_Tick);
        }


        bool _isLastAlarming = false;
        IWorkStatus _lastStatus = IWorkStatus.UnStart;//上一次更新时的状态

        private void timerFlush_Tick(object sender, EventArgs e) //主界面刷新
        {
            IPlatMainStation ms = AppHubCenter.Instance.StationMgr.MainStation;
            if (null == ms)
                return;
            if (ms.IsAlarming != _isLastAlarming)
            {
                _isLastAlarming = !_isLastAlarming;
                if (_isLastAlarming)
                {
                    statusLabelDevStatus.Text = "已报警:" + ms.GetAlarmInfo();
                    BackColor = Color.OrangeRed;
                    statusStrip1.BackColor = Color.OrangeRed;
                }
                else //报警状态转为正常
                {
                    statusLabelDevStatus.Text = "未报警";
                    _lastStatus = ms.WorkStatus;
                    //添加代码 ... 
                    switch (_lastStatus)
                    {
                        case IWorkStatus.UnStart:// = 0,    //线程未开始运行
                            BackColor = Color.White;
                            statusStrip1.BackColor = Color.White;
                            statusLabelDevStatus.Text = "未运行/已停止";
                            break;
                        case IWorkStatus.Running://,        //线程正在运行，未退出
                            BackColor = Color.Green;
                            statusStrip1.BackColor = Color.Green;
                            statusLabelDevStatus.Text = "运行中...";
                            break;
                        case IWorkStatus.Pausing://,        //线程暂停中
                            BackColor = Color.Yellow;
                            statusStrip1.BackColor = Color.Yellow;
                            statusLabelDevStatus.Text = "暂停中...";
                            break;
                        case IWorkStatus.Interactiving://,  //人机交互 ， 等待人工干预指令
                            BackColor = Color.YellowGreen;
                            statusStrip1.BackColor = Color.YellowGreen;
                            statusLabelDevStatus.Text = "等待人工确认中...";
                            break;
                        case IWorkStatus.NormalEnd://,     //线程正常完成后退出
                            BackColor = Color.White;
                            statusStrip1.BackColor = Color.White;
                            statusLabelDevStatus.Text = "已停止/正常结束";
                            break;
                        case IWorkStatus.CommandExit://,    //收到退出指令
                            BackColor = Color.White;
                            statusStrip1.BackColor = Color.White;
                            statusLabelDevStatus.Text = "已停止/指令结束";
                            break;
                        case IWorkStatus.ErrorExit://,      //发生错误退出，（重启或人工消除错误后可恢复）
                            BackColor = Color.DarkRed;
                            statusStrip1.BackColor = Color.DarkRed;
                            statusLabelDevStatus.Text = "已停止/发生错误";
                            break;
                        case IWorkStatus.ExceptionExit://,  //发生异常退出 ,  (不可恢复的错误)

                            BackColor = Color.DarkRed;
                            statusStrip1.BackColor = Color.DarkRed;
                            statusLabelDevStatus.Text = "已停止/发生异常";
                            break;
                        case IWorkStatus.AbortExit://,      //由调用者强制退出
                            BackColor = Color.DarkRed;
                            statusStrip1.BackColor = Color.DarkRed;
                            statusLabelDevStatus.Text = "已停止/指令强制";
                            break;
                        default:
                            break;
                    }
                }

            }

            if (_isLastAlarming)
                return;
            IWorkStatus currWs = ms.WorkStatus;
            if (currWs == _lastStatus)
                return;
            _lastStatus = currWs;
            switch (_lastStatus)
            {
                case IWorkStatus.UnStart:// = 0,    //线程未开始运行
                    BackColor = Color.White;
                    statusStrip1.BackColor = Color.White;
                    statusLabelDevStatus.Text = "未运行/已停止";
                    break;
                case IWorkStatus.Running://,        //线程正在运行，未退出
                    BackColor = Color.Green;
                    statusStrip1.BackColor = Color.Green;
                    statusLabelDevStatus.Text = "运行中...";
                    break;
                case IWorkStatus.Pausing://,        //线程暂停中
                    statusStrip1.BackColor = Color.Yellow;
                    BackColor = Color.Yellow;
                    statusLabelDevStatus.Text = "暂停中...";
                    break;
                case IWorkStatus.Interactiving://,  //人机交互 ， 等待人工干预指令
                    statusStrip1.BackColor = Color.YellowGreen;
                    BackColor = Color.YellowGreen;
                    statusLabelDevStatus.Text = "等待人工确认中...";
                    break;
                case IWorkStatus.NormalEnd://,     //线程正常完成后退出
                    statusStrip1.BackColor = Color.White;
                    BackColor = Color.White;
                    statusLabelDevStatus.Text = "已停止/正常结束";
                    break;
                case IWorkStatus.CommandExit://,    //收到退出指令
                    statusStrip1.BackColor = Color.White;
                    BackColor = Color.White;
                    statusLabelDevStatus.Text = "已停止/指令结束";
                    break;
                case IWorkStatus.ErrorExit://,      //发生错误退出，（重启或人工消除错误后可恢复）
                    statusStrip1.BackColor = Color.DarkRed;
                    BackColor = Color.DarkRed;
                    statusLabelDevStatus.Text = "已停止/发生错误";
                    break;
                case IWorkStatus.ExceptionExit://,  //发生异常退出 ,  (不可恢复的错误)
                    statusStrip1.BackColor = Color.DarkRed;
                    BackColor = Color.DarkRed;
                    statusLabelDevStatus.Text = "已停止/发生异常";
                    break;
                case IWorkStatus.AbortExit://,      //由调用者强制退出
                    statusStrip1.BackColor = Color.DarkRed;
                    BackColor = Color.DarkRed;
                    statusLabelDevStatus.Text = "已停止/指令强制";
                    break;
                default:
                    break;
            }
        }

        /// <summary>
        ///  主界面布局
        /// </summary>
        FormAuto formAuto;
        private void Form1_Load(object sender, EventArgs e)
        {
            // HIP 主界面
            UcHipMainStationVM mainsta = new UcHipMainStationVM();
            AppHubCenter.Instance.StationMgr.DeclearMainStation(mainsta);

            if (AppHubCenter.Instance.StationMgr.MainStation is IMainStationBase)
                (AppHubCenter.Instance.StationMgr.MainStation as IMainStationBase).ValidatedDeclearedItems();

            formAuto = new FormAuto();
            formAuto.Show();
            formAuto.TopLevel = false;
            formAuto.Parent = pnl_UserControl;
            formAuto.Dock = DockStyle.Fill;
            pnl_UserControl.Controls.Add(formAuto);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Form_CreateClass form_CreateDevice = new Form_CreateClass();
            form_CreateDevice.InitorCaption = "设备";
            form_CreateDevice.InitorType = typeof(IPlatDevice);
            form_CreateDevice.Show();
        }

        private void iconBtn1_Click(object sender, EventArgs e)
        {
            Form_DevCellNameMgr form_DevCellNameMgr = new Form_DevCellNameMgr();
            form_DevCellNameMgr.Show();
        }

        private void iconBtn2_Click(object sender, EventArgs e)
        {
            Form_CreateClass form_CreateDevice = new Form_CreateClass();
            form_CreateDevice.InitorCaption = "工站";
            form_CreateDevice.InitorType = typeof(IPlatStation);
            form_CreateDevice.Show();
        }


        private void btn_mainStation_Click(object sender, EventArgs e)
        {
            foreach (var control in this.pnl_UserControl.Controls)
            {
                if (control is UserControl)
                    (control as UserControl).Dispose();
            }
            pnl_UserControl.Controls.Clear();
        }
     

        private void iconBtn3_Click(object sender, EventArgs e)
        {
            Form fconfig = new Form();
            UcMainStationBaseCfg ucHipMainStation = new UcMainStationBaseCfg();
            ucHipMainStation.Dock = DockStyle.Fill;
            ucHipMainStation.SetMainStation((AppHubCenter.Instance.StationMgr.MainStation as IMainStationBase));
            fconfig.Controls.Add(ucHipMainStation);
            fconfig.WindowState = FormWindowState.Maximized;
            fconfig.Show();

        }

        private void btn_strat_Click(object sender, EventArgs e)
        {
            IPlatMainStation ms = AppHubCenter.Instance.StationMgr.MainStation;
            if (ms.WorkStatus == IWorkStatus.Running)
            {
                MessageBox.Show("无效操作:正在运行中");
                return;
            }
            string errorInfo;
            if (!_isStationWorking(ms.WorkStatus))
            {
                ///先将所有使能工站切换为自动模式
                AppStationManager mgr = AppHubCenter.Instance.StationMgr;
                string[] allEnableStationNames = mgr.AllEnabledStationNames();
                if (null != allEnableStationNames)
                    foreach (string sn in allEnableStationNames)
                    {
                        IPlatStation station = mgr.GetStation(sn);
                        if (!station.SetRunMode(IStationRunMode.Auto))
                        {
                            MessageBox.Show("启动运行失败，未能将工站：" + sn + "切换为自动运行模式");
                            return;
                        }
                    }


                bool isOK = ms.Start(out errorInfo);
                if (!isOK)
                {
                    MessageBox.Show("启动失败:" + errorInfo);
                    return;
                }
            }

            if (ms.WorkStatus == IWorkStatus.Pausing) //当前处于暂停状态
            {
                bool isOK = ms.Resume(out errorInfo);
                if (!isOK)
                {
                    MessageBox.Show("恢复运行失败:" + errorInfo);
                    return;
                }
            }
        }

        bool _isStationWorking(IWorkStatus status)
        {
            return status == IWorkStatus.Running || status == IWorkStatus.Pausing || status == IWorkStatus.Interactiving;
        }

        private void btn_stop_Click(object sender, EventArgs e)
        {
            IPlatMainStation ms = AppHubCenter.Instance.StationMgr.MainStation;
            string errInfo = "";
            if (!ms.Stop(out errInfo))
            {
                MessageBox.Show("停止操作失败:" + errInfo);
                return;
            }
        }

        private void btn_home_Click(object sender, EventArgs e)
        {
          //  formAuto.SendToBack();
        }

        private void tbtn_Showog_Click(object sender, EventArgs e)
        {
            formAuto.IsShowSRI = tbtn_Showlog.Checked = !tbtn_Showlog.Checked;
        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            // 停止 各个子工站
            string errInfo = "";
            AppHubCenter.Instance.StationMgr.MainStation.Stop(out errInfo);

        }

        private void btn_rest_Click(object sender, EventArgs e)
        {
            string errInfo = "";

            IPlatMainStation ms = AppHubCenter.Instance.StationMgr.MainStation;
            if (!ms.ClearAlarming(out errInfo))
                MessageBox.Show("清除报警失败:" + errInfo);


            if (!ms.Reset(out errInfo))
                MessageBox.Show("归零操作失败:" + errInfo);

        }

        private void btn_stationcfg_Click(object sender, EventArgs e)
        {

        }
    }


}
