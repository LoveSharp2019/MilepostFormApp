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

namespace Org.IMotionDaq
{
    public partial class UcCmprTrgChn : UserControl
    {
        public UcCmprTrgChn()
        {
            InitializeComponent();
        }

        public delegate void TxtMessage(string msg);
        public TxtMessage OnTxtMsg;


        private void UcCmprTrgChn_Load(object sender, EventArgs e)
        {
            AdjustView();
        }



        IPlatModule_CmprTrg _module = null; //触发模块
        string _id = ""; //通道名称，调用者指定
        int _cmpID = -1; //比较器编号
        int _encID = -1;
        int _trgID = -1;//已选择的触发输出通道

        /// <summary>设置模块和编码器通道号</summary>
        public void SetModuleChn(IPlatModule_CmprTrg module, int cmpID, string cmpName = null)
        {
            _module = module;
            _cmpID = cmpID;
            _id = cmpName;
            if (Created)
                AdjustView();
        }

        /// <summary>
        /// 
        /// </summary>
        public void AdjustView()
        {
            if (InvokeRequired)
            {
                BeginInvoke(new Action(AdjustView));
                return;
            }
            if (null == _module)
            {
                lbCmpID.Text = "模块未设置";
                Enabled = false;
                return;
            }
            if (string.IsNullOrEmpty(_id))
                lbCmpID.Text = "比较器:" + _cmpID;
            else
                lbCmpID.Text = _id;
            if (!IsDevValible())
            {
                Enabled = false;
                lbCmpID.Text = "无效通道:" + _cmpID;
                return;
            }
            Enabled = true;
            cbTrigChns.Items.Clear();
            int[] trigBinds;
            int ret = _module.GetCmpTrigBinds(_cmpID, out trigBinds);
            if (ret != 0)
            {
                cbTrigChns.Text = "Error";
                cbTrigChns.Enabled = false;
            }
            else
            {
                cbTrigChns.Enabled = true;
                if (null != trigBinds && trigBinds.Length != 0)
                {

                    for (int i = 0; i < trigBinds.Length; i++)
                        cbTrigChns.Items.Add(trigBinds[i]);
                    if (_trgID < 0)
                        cbTrigChns.SelectedIndex = 0;
                    else
                    {
                        //if (trigBinds.FirstIndex(func(value){ })
                    }
                }

            }




        }

        bool IsDevValible()
        {
            if (null == _module)
                return false;
            if (_cmpID < 0 || _cmpID >= _module.CompareCount)
                return false;
            return true;
        }


        public void UpdateChnStatus()
        {
            return;
            //if (InvokeRequired)
            //{
            //    BeginInvoke(new Action(UpdateChnStatus));
            //    return;
            //}
            //if (!IsDevValible())
            //    return;
            //int encID = 0;
            //int ret = _module.GetCmpEncoderBind(_cmpID, out encID);
            //if (ret != 0)
            //{
            //    lbBindEncoder.Text = "Err";
            //    lbEncoderPos.Text = "Unknown";
            //}
            //else
            //{
            //    lbBindEncoder.Text = encID.ToString();
            //    double pos = 0;
            //    ret = _module.GetEncoderCurrPos(encID, out pos);
            //    if (ret != 0)
            //        lbEncoderPos.Text = "Err";
            //    else
            //        lbEncoderPos.Text = pos.ToString("f3");
            //}

            //if(cbTrigChns.SelectedIndex < 0)
            //{
            //    lbTrigCount.Text = "UnSelect";
            //    return;
            //}
            //else
            //{
            //    int trgChn = Convert.ToInt32(cbTrigChns.SelectedItem.ToString());
            //    if (_module.TriggerCount >= trgChn)
            //    {
            //        lbTrigCount.Text = "ChnError";
            //    }
            //    else
            //    {
            //        int cnt;
            //        ret = _module.GetTriggedCount(trgChn, out cnt);
            //        if (ret != 0)
            //            lbTrigCount.Text = "Error";
            //        else
            //            lbTrigCount.Text = cnt.ToString();
            //    }
            //}






        }


        private void UcCmprTrgChn_VisibleChanged(object sender, EventArgs e)
        {
            if (Visible)
                AdjustView();
        }


        /// <summary>
        /// 配置当前比较器的参数
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btCfg_Click(object sender, EventArgs e)
        {
            //后期 实际使用时候 开发
            //FormCmpSetting fm = new FormCmpSetting();
            //fm.SetChnInfo(_module, _cmpID);
            //if (DialogResult.OK == fm.ShowDialog())
            //    AdjustView();
        }

        /// <summary>
        /// 软触发当前已选输出通道
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btSwTrigged_Click(object sender, EventArgs e)
        {
            if (!IsDevValible())
            {
                ShowTips("无效的设备/通道");
                return;
            }

            int trigChn = cbTrigChns.SelectedIndex;
            if (trigChn < 0)
            {
                ShowTips("请先选择触发输出通道");
                return;
            }

            if (trigChn >= _module.TriggerCount)
            {
                ShowTips(string.Format("无效的触发输出通道:{0} (有效范围：0~{1})", trigChn, _module.TriggerCount - 1));
                return;
            }

            int ret = _module.SoftTrigger(trigChn);
            if (ret != 0)
            {
                ShowTips("软触发失败，ErrorInfo：" + _module.GetErrorInfo(ret));
            }


        }

        /// <summary>
        /// 重置当前已选输出通道计数
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btResetCount_Click(object sender, EventArgs e)
        {
            if (!IsDevValible())
            {
                ShowTips("无效的设备/通道");
                return;
            }

            int trigChn = cbTrigChns.SelectedIndex;
            if (trigChn < 0)
            {
                ShowTips("请先选择触发输出通道");
                return;
            }

            if (trigChn >= _module.TriggerCount)
            {
                ShowTips(string.Format("无效的触发输出通道:{0} (有效范围：0~{1})", trigChn, _module.TriggerCount - 1));
                return;
            }

            int ret = _module.ResetTriggedCount(trigChn);
            if (ret != 0)
            {
                ShowTips("触发计数归零失败，ErrorInfo：" + _module.GetErrorInfo(ret));
            }

        }

        /// <summary>
        /// 输出通道变更
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cbTrigChns_SelectedIndexChanged(object sender, EventArgs e)
        {
            //UpdateChnStatus();
        }


        void ShowTips(string msg)
        {
            if (string.IsNullOrEmpty(msg))
                return;
            if (OnTxtMsg != null)
                OnTxtMsg.Invoke(msg);
            else
                MessageBox.Show(msg);
        }
    }
}
