﻿using Cell.Interface;
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
using Tissue.UI;

namespace Org.IMotionDaq
{
    /// <summary>
    /// 显示/调试运动控制器中的多个轴模块
    /// </summary>
    public partial class FormMotions : windowBase
    {
        public FormMotions()
        {
            InitializeComponent();
        }

        private void FormAxises_Load(object sender, EventArgs e)
        {
            UpdateModleStatus();
        }

        List<IPlatModule_Motion> _lstModules = new List<IPlatModule_Motion>();

        public void ClearModules()
        {
            _lstModules.Clear();
            tabCtrl.TabPages.Clear();
        }

        public void AddModule(IPlatModule_Motion module, string moduleName)
        {
            if (null == module)
                return;
            if (_lstModules.Contains(module))
                return;

            if (null == moduleName)
                moduleName = "Motion";
            TabPage tp = new TabPage();
            tabCtrl.TabPages.Add(tp);
            UcMotion uc = new UcMotion();
            uc.Dock = DockStyle.Fill;
            uc.Parent = tp;
            uc.Visible = true;
            uc.SetModuleInfo(module, null);
            tp.Text = moduleName;
            tp.Name = moduleName;
            tp.Controls.Add(uc);
            _lstModules.Add(module);
        }

        public void UpdateModleStatus()
        {
            if (InvokeRequired)
            {
                BeginInvoke(new Action(UpdateModleStatus));
                return;
            }
            if (_lstModules.Count == 0)
                return;
            if (tabCtrl.SelectedIndex < 0)
                return;
            UcMotion uc = tabCtrl.TabPages[tabCtrl.SelectedIndex].Controls[0] as UcMotion;
            uc.UpdateSrc2UI();
        }
    }
}
