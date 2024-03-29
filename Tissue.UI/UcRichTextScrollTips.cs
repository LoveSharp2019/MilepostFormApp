using Cell.IconFont;
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
    /// 用于滚动显示文本信息的控件，当文本条数超过指定大小时，会删除前一般的条数
    /// </summary>
    public partial class UcRichTextScrollTips : UserControl
    {
        public delegate void RichTextDelegate(string str);
        ImageList _imageList;
        public UcRichTextScrollTips()
        {
            InitializeComponent();
        }


        [Category("属性"), Description("为每条信息添加时间记录"), Browsable(true)]
        public bool IsAppendTimeInfo
        {
            get { return chkTimeFlag.Checked; }
            set { chkTimeFlag.Checked = value; }
        }

        [Category("属性"), Description("添加信息后滚动到最后一行"), Browsable(true)]
        public bool IsAutoScrollLast
        {
            get { return chkScrollLast.Checked; }
            set { chkScrollLast.Checked = value; }
        }



        [Category("属性"), Description("显示信息最大条数"), Browsable(true)]
        public int MaxTipsCount
        {
            get { return _maxTipsCount; }
            set
            {
                if (value < numTipsCount.Minimum || value > numTipsCount.Maximum)
                    return;
                numTipsCount.Value = value;
            }
        }



        int _maxTipsCount = 100; //信息显示的最大数量

        /// <summary>
        /// 添加一条信息
        /// </summary>
        /// <param name="txt"></param>
        public void AppendText(string txt)
        {
            if (txt == null)
                return;

            if (!Created)
                return;
            if (!Enabled)
                return;
            SetRichTextControl(txt);
        }


        private void UcScrollTips_Load(object sender, EventArgs e)
        {
            _imageList = new ImageList();
            _imageList.ColorDepth = ColorDepth.Depth32Bit;
            _imageList.ImageSize = new Size(18, 18);

            _imageList.Images.Add("0", FontImages.GetImage(FontIcons.A_fa_caret_right, 18, ColorTranslator.FromHtml("#077409")));
            _imageList.Images.Add("1", FontImages.GetImage(FontIcons.A_fa_exchange, 18, ColorTranslator.FromHtml("#077409")));
            this.btCfg.ImageList = this._imageList;

            btCfg.ImageAlign = ContentAlignment.MiddleCenter;
            btCfg.ImageIndex = 0;
        }


        private void btClearTips_Click(object sender, EventArgs e)
        {
            rchTips.Clear();
        }


        private void numTipsCount_ValueChanged(object sender, EventArgs e)
        {
            _maxTipsCount = Convert.ToInt32(numTipsCount.Value);
        }

        private void chkScrollLast_CheckedChanged(object sender, EventArgs e)
        {
            if (chkScrollLast.Checked)
            {
                rchTips.Select(rchTips.TextLength, 0); //滚到最后一行
                rchTips.ScrollToCaret();//滚动到控件光标处 
            }

        }

        public void Clear()
        {
            if (InvokeRequired)
            {
                Invoke(new Action(Clear));
                return;
            }
            rchTips.Clear();
        }

        bool ShowCfgPanel
        {
            get { return numTipsCount.Visible; }
            set
            {
                if (value == numTipsCount.Visible)
                    return;
                btCfg.ImageIndex = value ? 1 : 0;
                label1.Visible = value;
                numTipsCount.Visible = value;
                chkScrollLast.Visible = value;
                chkTimeFlag.Visible = value;
            }
        }


        /// <summary>
        /// 显示 或 收起 参数设置面板
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btCfg_Click(object sender, EventArgs e)
        {
            ShowCfgPanel = !ShowCfgPanel;
        }

        private void SetRichTextControl(string str)
        {
            if (rchTips.InvokeRequired)
            {
                RichTextDelegate rtd = new RichTextDelegate(SetRichTextControl);
                this.Invoke(rtd, new object[] { str });
            }
            else
            {
                if (this.rchTips.Lines.Length > _maxTipsCount)
                {
                    string[] sLines = rchTips.Lines;
                    string[] sNewLines = new string[_maxTipsCount];
                    Array.Copy(sLines, this.rchTips.Lines.Length - _maxTipsCount, sNewLines, 0, _maxTipsCount);
                    rchTips.Lines = sNewLines;
                }
                if (IsAppendTimeInfo)
                    this.rchTips.AppendText(string.Format("{0} : {1} \r\n", DateTime.Now.ToString(), str));
                else
                    this.rchTips.AppendText(string.Format("{0} \r\n", str));

                if (chkScrollLast.Checked)
                    rchTips.ScrollToCaret();//滚动到控件光标处 
            }
        }

    }
}
