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

namespace Tissue.UI
{
    /// <summary>
    /// 用于在主公站中设置DIO属性
    /// </summary>
    public partial class UcMsDioSetting : UserControl
    {
        public UcMsDioSetting()
        {
            InitializeComponent();
        }



        private void UcMsDioSetting_Load(object sender, EventArgs e)
        {

        }

        [Category("属性"), Description("是否为DO设备"), Browsable(true)]
        public bool IsDout
        {
            get { return lamp.Enabled; }
            set { lamp.Enabled = value; }
        }


        [Category("属性"), Description("DIO名称"), Browsable(true)]
        public string DioName
        {
            get { return lamp.Text; }
            set { lamp.Text = value; }
        }

        /// <summary>
        /// 灯按钮 ，用于添加Click回调
        /// </summary>
        public LampButton DioLamp { get { return lamp; } }


        /// <summary>
        /// DIO是否使能的按钮
        /// </summary>
        public CheckBox DioCheckBox { get { return chkEnable; } }

        /// <summary>
        /// 设置全局通道名称的对象
        /// </summary>
        public ComboBox DioCombo { get { return cbChns; } }
    }
}
