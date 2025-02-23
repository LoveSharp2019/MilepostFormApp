﻿using Cell.DataModel;
using Cell.Interface;
using Org.IMotionDaq;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Sys.IStations
{
    public partial class UcSimpleAxisInStation : UserControl
    {
        public UcSimpleAxisInStation()
        {
            InitializeComponent();
        }

        private void UcAxisInStation_Load(object sender, EventArgs e)
        {

        }
        string _axisName = "";
        public void SetAxisName(string axisName)
        {
            _axisName = axisName;
            gbAxisName.Text = axisName;
            IDevCellInfo ci = AppHubCenter.Instance.MDCellNameMgr.GetAxisCellInfo(axisName);
            if (null == ci)
            {
                gbAxisName.Text += " 无通道信息";
                ucAxisTest.SetAxis(null, 0);
                cbMode.Enabled = false;
                btCfg.Enabled = false;
                return;
            }
            IPlatDevice_MotionDaq dev = AppHubCenter.Instance.InitorManager.GetInitor(ci.DeviceID) as IPlatDevice_MotionDaq;
            if (null == dev)
            {
                gbAxisName.Text += " 无设备:" + ci.DeviceID;
                ucAxisTest.SetAxis(null, 0);
                cbMode.Enabled = false;
                btCfg.Enabled = false;
                return;
            }
            if (!dev.IsDeviceOpen)
            {
                gbAxisName.Text += " 设备未打开";
                ucAxisTest.SetAxis(null, 0);
                cbMode.Enabled = false;
                btCfg.Enabled = false;
                return;
            }

            if (dev.McMCount <= ci.ModuleIndex)
            {
                gbAxisName.Text += " 模块Idx = :" + ci.ModuleIndex + " 超限";
                ucAxisTest.SetAxis(null, 0);
                cbMode.Enabled = false;
                btCfg.Enabled = false;
                return;
            }
            IPlatModule_Motion md = dev.GetMc(ci.ModuleIndex);
            if (ci.ChannelIndex >= md.AxisCount)
            {
                gbAxisName.Text += " 轴Idx = :" + ci.ChannelIndex + " 超限";
                ucAxisTest.SetAxis(null, 0);
                cbMode.Enabled = false;
                btCfg.Enabled = false;
                return;
            }
            ucAxisTest.SetAxis(md, ci.ChannelIndex);
            cbMode.Enabled = true;
            btCfg.Enabled = true;
            return;
        }


        public UcAxisTest UcAxis { get { return ucAxisTest; } }


        /// <summary>
        /// 显示轴配置/调试窗口
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btCfg_Click(object sender, EventArgs e)
        {
            FormAxisTest fm = new FormAxisTest();
            IDevCellInfo ci = AppHubCenter.Instance.MDCellNameMgr.GetAxisCellInfo(_axisName);
            if (null == ci)
            {
                gbAxisName.Text += " 无通道信息";
                ucAxisTest.SetAxis(null, 0);
                cbMode.Enabled = false;
                btCfg.Enabled = false;
                return;
            }
            IPlatDevice_MotionDaq dev = AppHubCenter.Instance.InitorManager.GetInitor(ci.DeviceID) as IPlatDevice_MotionDaq;

            IPlatModule_Motion md = dev.GetMc(ci.ModuleIndex);
            fm.SetAxisInfo(md, ci.ChannelIndex, _axisName);
            fm.ShowDialog();
        }

        /// <summary>
        /// 运动模式改变
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cbMode_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbMode.SelectedIndex < 0)
                return;
            else if (cbMode.SelectedIndex == 0) //位置模式
                ucAxisTest.DisplayMode = UcAxisTest.JFDisplayMode.simplest_pos;
            else //速度模式
                ucAxisTest.DisplayMode = UcAxisTest.JFDisplayMode.simplest_vel;
        }

        /// <summary>
        /// 更新轴状态信息
        /// </summary>
        public void UpdateAxisUI()
        {
            ucAxisTest.UpdateAxisUI();
        }
    }
}
