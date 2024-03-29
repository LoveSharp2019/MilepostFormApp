using Cell.DataModel;
using Cell.Interface;
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
    public partial class UcAxisStatusByName : UserControl
    {
        public UcAxisStatusByName()
        {
            InitializeComponent();
        }

        private void UcAxisStatusByName_Load(object sender, EventArgs e)
        {

        }

        string _axisName = null;
        public void SetAxisName(string axisName)
        {
            //_isAxisEnabled = false;
            _axisName = axisName;
            gbAxisName.Text = _axisName;
            IDevCellInfo ci = AppHubCenter.Instance.MDCellNameMgr.GetAxisCellInfo(_axisName);
            if (ci == null)
            {
                gbAxisName.Text += "  轴名无效";
                ucAxisStatus1.Enabled = false;
                return;
            }
            IPlatDevice_MotionDaq dev = AppHubCenter.Instance.InitorManager.GetInitor(ci.DeviceID) as IPlatDevice_MotionDaq;
            if (null == dev)
            {
                gbAxisName.Text += "  设备无效";
                ucAxisStatus1.Enabled = false;
                return;
            }
            if (!dev.IsDeviceOpen)
            {
                gbAxisName.Text += "  设备未打开";
                ucAxisStatus1.Enabled = false;
                return;
            }
            if (ci.ModuleIndex >= dev.McMCount)
            {
                gbAxisName.Text += "  模块号无效";
                ucAxisStatus1.Enabled = false;
                return;
            }

            IPlatModule_Motion mm = dev.GetMc(ci.ModuleIndex);

            if (ci.ChannelIndex >= mm.AxisCount)
            {
                gbAxisName.Text += "  轴序号无效";
                ucAxisStatus1.Enabled = false;
                return;
            }
            ucAxisStatus1.Enabled = true;
            ucAxisStatus1.SetAxis(mm, ci.ChannelIndex);
        }

        public void UpdateAxisStatus()
        {
            ucAxisStatus1.UpdateAxisStatus();
        }
    }
}
