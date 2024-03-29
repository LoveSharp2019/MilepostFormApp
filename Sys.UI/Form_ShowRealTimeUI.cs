using Cell.Interface;
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

namespace Sys.UI
{
    public partial class Form_ShowRealTimeUI : windowBase
    {
        /// <summary>
        /// 显示事实调试界面
        /// </summary>
        public Form_ShowRealTimeUI()
        {
            InitializeComponent();
        }

        UcRealTimeUI _ui = null;

        /// <summary>
        /// 更新模块状态
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void timerUpdateUI_Tick(object sender, EventArgs e)
        {
            _ui?.UpdateSrc2UI();
        }

        public void SetRTUI(UcRealTimeUI ui)
        {
            if (_ui == ui)
                return;
            timerUpdateUI.Enabled = false;
            this.clearcontrol();
            _ui = ui;
            if (null != _ui)
            {
                pnl_context.Controls.Add(ui);
                Size = new Size(ui.Size.Width + 2, ui.Size.Height + 36);
                ui.Dock = DockStyle.Fill;
            }

        }

        private void Form_ShowRealTimeUI_VisibleChanged(object sender, EventArgs e)
        {
            if (null != _ui)
                timerUpdateUI.Enabled = Visible;
        }
    }
}
