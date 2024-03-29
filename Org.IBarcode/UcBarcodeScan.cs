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

namespace Org.IBarcode
{
    /// <summary>
    ///  界面需要重新设计
    /// </summary>
    public partial class UcBarcodeScan : UcRealTimeUI
    {
        public UcBarcodeScan()
        {
            InitializeComponent();
        }

        bool _isRegistedCallback = false;
        IPlatDevice_Barcode _dev = null;
        public void SetDevice(IPlatDevice_Barcode dev)
        {
            if (_isRegistedCallback) //将上一次注册的回调函数抹掉
            {
                _dev.ScanCallBack -= ScanCallback;
                _isRegistedCallback = false;
            }
            _dev = dev;
            if (Created)
            {
                UpdateView();
                if (_dev != null && _dev.GetWorkMode() == cBarcodeSanMode.Passive)
                {
                    dev.ScanCallBack += ScanCallback;
                    _isRegistedCallback = true;
                }
            }
        }

        void ScanCallback(IPlatDevice_Barcode scanner, int resultCode, string barcode)
        {
            Invoke(new Action(() =>
            {
                tbBarcode.Text = barcode;
                if (resultCode == 0)
                {
                    ucScrollTips1.AppendText("扫码成功:" + barcode);
                    tbBarcode.ForeColor = Color.Black;
                }
                else
                {
                    tbBarcode.ForeColor = Color.OrangeRed;
                    ucScrollTips1.AppendText("扫码失败，返回字串：" + barcode + " ErrorInfo：" + _dev.GetErrorInfo(resultCode));
                }
            }));
        }

        void UpdateView()
        {
            if (InvokeRequired)
            {
                Invoke(new Action(UpdateView));
                return;
            }
            if (null == _dev)
            {
                Enabled = false;
                ucScrollTips1.Clear();
                ucScrollTips1.AppendText("扫码设备未设置/为空");
                return;
            }


            if (!_dev.IsInitOK)
            {
                Enabled = false;
                ucScrollTips1.Clear();
                ucScrollTips1.AppendText("扫码设备未初始化，Error:" + _dev.GetInitErrorInfo());
                return;
            }

            Enabled = true;
            if (_dev.IsDeviceOpen)
            {
                btOpenCloseDev.Text = "关闭设备";
                cBarcodeSanMode wm = _dev.GetWorkMode();
                _isSetWorkMode = true;
                chkWorkMode.Checked = wm == cBarcodeSanMode.Initiative ? true : false;
                _isSetWorkMode = false;
                if (_dev.GetWorkMode() == cBarcodeSanMode.Passive) //被动模式
                {
                    btScan.Enabled = false;
                    chkContine.Checked = false;
                    chkContine.Enabled = false;
                    numInterval.Enabled = false;
                }
                else
                {
                    btScan.Enabled = true;
                    chkContine.Enabled = true;
                    if (chkContine.Checked)
                    {
                        timer1.Enabled = true;
                        numInterval.Enabled = false;
                    }
                    else
                        numInterval.Enabled = true;


                }

            }
            else
            {
                btOpenCloseDev.Text = "打开设备";
                btScan.Enabled = false;

                chkContine.Enabled = false;
                chkContine.Checked = false;
                numInterval.Enabled = false;
                timer1.Enabled = false;
                ucScrollTips1.AppendText("当前设备处于关闭状态");
            }
        }

        private void UcBarcodeScan_Load(object sender, EventArgs e)
        {
            UpdateView();
        }

        /// <summary>
        /// 打开/关闭设备
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btOpenCloseDev_Click(object sender, EventArgs e)
        {
            if (null == _dev)
                return;
            if (!_dev.IsInitOK)
                return;
            if (_dev.IsDeviceOpen)
                _dev.CloseDevice();
            else
            {
                int ret = _dev.OpenDevice();
                if (ret != 0)
                {
                    ucScrollTips1.AppendText("打开设备失败，ErrorInfo:" + _dev.GetErrorInfo(ret));
                    return;
                }
            }
            UpdateView();
        }


        //向设备发送扫码指令
        private void btScan_Click(object sender, EventArgs e)
        {
            if (_dev == null)
                return;
            string bc = null;
            int ret = _dev.Scan(out bc);
            if (ret != 0)
            {
                string error = "扫码失败，ErrorInfo:" + _dev.GetErrorInfo(ret);
                ucScrollTips1.AppendText(error);
                tbBarcode.Text = bc;
                tbBarcode.ForeColor = Color.OrangeRed;
                MessageBox.Show(error);
            }
            else
            {
                ucScrollTips1.AppendText("扫码成功：" + bc);
                tbBarcode.Text = bc;
                tbBarcode.ForeColor = Color.Black;
            }
        }

        private void ucScrollTips1_VisibleChanged(object sender, EventArgs e)
        {
            if (Visible)
            {
                UpdateView();
                if (chkContine.Checked)
                    timer1.Enabled = true;
            }
            else
            {
                timer1.Enabled = false;
            }
        }


        /// <summary>
        /// 连续扫码时的刷新定时器
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void timer1_Tick(object sender, EventArgs e)
        {
            if (!Visible)
            {
                timer1.Enabled = false;
                return;
            }

            if (_dev == null || !_dev.IsDeviceOpen)
            {
                timer1.Enabled = false;
                return;
            }

            if (_dev.GetWorkMode() == cBarcodeSanMode.Passive)
            {
                numInterval.Enabled = false;
                timer1.Enabled = false;
                return;
            }
            string bc;
            int ret = _dev.Scan(out bc);
            if (ret == 0)
            {
                tbBarcode.ForeColor = Color.Black;
                ucScrollTips1.AppendText("扫码成功:" + bc);
                tbBarcode.Text = bc;
            }
            else
            {
                tbBarcode.ForeColor = Color.OrangeRed;
                tbBarcode.Text = bc;
                ucScrollTips1.AppendText("扫码失败，ErrorInfo:" + _dev.GetErrorInfo(ret));
            }

        }


        bool _isSetWorkMode = false;
        /// <summary>
        /// 主动/被动模式
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void chkWorkMode_CheckedChanged(object sender, EventArgs e)
        {
            if (_isSetWorkMode)
                return;
            if (_dev == null)
                return;
            if (chkWorkMode.Checked)//主动扫码模式
            {
                int ret = _dev.SetWorkMode(cBarcodeSanMode.Initiative);
                if (ret != 0)
                {
                    ucScrollTips1.AppendText("设置主动工作模式失败，ErrorInfo：" + _dev.GetErrorInfo(ret));
                    cBarcodeSanMode wm = _dev.GetWorkMode();
                    _isSetWorkMode = true;
                    chkWorkMode.Checked = wm == cBarcodeSanMode.Initiative ? true : false;
                    _isSetWorkMode = false;
                    return;
                }

                if (_isRegistedCallback)
                {
                    _dev.ScanCallBack -= ScanCallback;
                    _isRegistedCallback = false;
                }

                chkContine.Enabled = true;

            }
            else//被动扫码模式
            {
                int ret = _dev.SetWorkMode(cBarcodeSanMode.Passive);
                if (ret != 0)
                {
                    ucScrollTips1.AppendText("设置被动工作模式失败，ErrorInfo：" + _dev.GetErrorInfo(ret));
                    cBarcodeSanMode wm = _dev.GetWorkMode();
                    _isSetWorkMode = true;
                    chkWorkMode.Checked = wm == cBarcodeSanMode.Initiative ? true : false;
                    _isSetWorkMode = false;
                    return;
                }
                chkContine.Checked = false;
                chkContine.Enabled = false;
                if (!_isRegistedCallback)
                {
                    _dev.ScanCallBack += ScanCallback;
                    _isRegistedCallback = true;
                }


            }
        }

        private void chkContine_CheckedChanged(object sender, EventArgs e)
        {
            if (chkContine.Checked)
            {
                timer1.Interval = Convert.ToInt32(numInterval.Value);
                numInterval.Enabled = false;
            }
            else
            {
                numInterval.Enabled = true;
            }
            timer1.Enabled = chkContine.Checked;
        }
    }
}
