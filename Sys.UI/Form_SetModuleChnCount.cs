using Cell.DataModel;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Sys.UI
{
    public partial class Form_SetModuleChnCount : Form
    {
        public Form_SetModuleChnCount()
        {
            InitializeComponent();
        }

        private void Form_SetChnCount_Load(object sender, EventArgs e)
        {

        }

        ModuleCategory _moduleCategory = ModuleCategory.Dio;
        public ModuleCategory Category
        {
            get { return _moduleCategory; }
            set
            {
                _moduleCategory = value;
                switch (_moduleCategory)
                {
                    case ModuleCategory.Dio:
                        label1.Text = "DI通道数量";
                        label2.Text = "DO通道数量";
                        label2.Visible = true;
                        num2.Visible = true;
                        break;
                    case ModuleCategory.Aio:
                        label1.Text = "AI通道数量";
                        label2.Text = "AO通道数量";
                        label2.Visible = true;
                        num2.Visible = true;
                        break;
                    case ModuleCategory.Motion:
                        label1.Text = "轴通道数量";
                        label2.Visible = false;
                        num2.Visible = false;
                        break;
                    case ModuleCategory.Trig:
                        label1.Text = "比较触发通道数";
                        num1.Left = label1.Right + 3;
                        label2.Visible = false;
                        num2.Visible = false;
                        break;
                }
            }
        }


        private void btOK_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
        }

        private void btCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
        }

        public int ChannelCount1
        {
            get { return Convert.ToInt32(num1.Value); }
            set { num1.Value = value; }
        }

        public int ChannelCount2
        {
            get { return Convert.ToInt32(num2.Value); }
            set { num2.Value = value; }
        }
    }
}
