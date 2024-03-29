using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Cell.UI
{
    public partial class UcTabControl : TabControl
    {
        public UcTabControl()
        {
            InitializeComponent();

            SetStyle(ControlStyles.AllPaintingInWmPaint |  //全部在窗口绘制消息中绘图
                ControlStyles.OptimizedDoubleBuffer, //使用双缓冲
                true);

            this.TabStop = true;        
            this.DrawItem += new System.Windows.Forms.DrawItemEventHandler(this.CustomTabControl_DrawItem);
            this.SelectedIndexChanged += new EventHandler(TBSelectedIndexChanged);
        }


        private void TBSelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.SelectedIndex < 0) return;
            this.TabPages[this.SelectedIndex].Focus();
        }


        protected override void OnPaint(PaintEventArgs pe)
        {
            base.OnPaint(pe);
        }


        /// <summary>
        /// 背景颜色
        /// </summary>
        private Color tbBackgroundColour;
        [Description("获取或设置Tablecontrol背景色")]
        public Color TbBackgroundColour
        {
            get
            {
                if (tbBackgroundColour == null)
                {
                    tbBackgroundColour = Color.FromArgb(8, 32, 80);
                }
                return tbBackgroundColour;
            }
            set { tbBackgroundColour = value; }
        }

        /// <summary>
        /// 选中状态颜色
        /// </summary>
        private Color selectStatusColor;
        [Description("获取或设置选中状态颜色")]
        public Color SelectStatucColor
        {
            get
            {
                if (selectStatusColor == null)
                {
                    selectStatusColor = Color.Gray;
                }
                return selectStatusColor;
            }
            set { selectStatusColor = value; }
        }

        /// <summary>
        /// 非选中状态颜色
        /// </summary>
        private Color unselectStatusColor;
        [Description("获取或设置选中状态颜色")]
        public Color UnSelectStatucColor
        {
            get
            {
                if (unselectStatusColor == null)
                {
                    unselectStatusColor = Color.FromArgb(109, 119, 131);
                }
                return unselectStatusColor;
            }
            set { unselectStatusColor = value; }
        }

        /// <summary>
        /// 非选中字体颜色
        /// </summary>
        private Color untabPageFontColor;
        [Description("获取或设置选中字体颜色")]
        public Color UntabPageFontColor
        {
            get
            {
                if (untabPageFontColor == null)
                {
                    untabPageFontColor = Color.White;
                }
                return untabPageFontColor;
            }
            set { untabPageFontColor = value; }
        }

        /// <summary>
        /// 选中字体颜色
        /// </summary>
        private Color tabPageFontColor;
        [Description("获取或设置选中字体颜色")]
        public Color TabPageFontColor
        {
            get
            {
                if (tabPageFontColor == null)
                {
                    tabPageFontColor =Color.White;
                }
                return tabPageFontColor;
            }
            set { tabPageFontColor = value; }
        }

        /// <summary>
        /// tab页字体
        /// </summary>
        private Font tabPageFont;
        [Description("获取或设置tab页字体")]
        public Font TabPageFont
        {
            get
            {
                if (tabPageFont == null)
                {
                    tabPageFont = new Font("宋体", 12);
                }
                return tabPageFont;
            }
            set { tabPageFont = value; }
        }

        private void CustomTabControl_DrawItem(object sender, DrawItemEventArgs e)
        {
            TabControl tc = sender as TabControl;

            Rectangle rec3 = tc.ClientRectangle;
            e.Graphics.FillRectangle(new SolidBrush(TbBackgroundColour), rec3);

            StringFormat StringF = new StringFormat();
            //设置文字对齐方式
            StringF.Alignment = StringAlignment.Center;
            StringF.LineAlignment = StringAlignment.Center;

            for (int i = 0; i < tc.TabPages.Count; i++)
            {
                //获取标签头工作区域
                Rectangle Rec = tc.GetTabRect(i);
                if (i == tc.SelectedIndex) // 选中项的绘制区域
                {
                    e.Graphics.FillRectangle(new SolidBrush(SelectStatucColor), Rec);
                    e.Graphics.DrawString(tc.TabPages[i].Text, new System.Drawing.Font("微软雅黑", 12), new SolidBrush(UntabPageFontColor), Rec, StringF);

                }
                else
                {
                    //绘制标签头背景颜色
                    e.Graphics.FillRectangle(new SolidBrush(UnSelectStatucColor), Rec);
                    //绘制标签头文字
                    e.Graphics.DrawString(tc.TabPages[i].Text, new System.Drawing.Font("微软雅黑", 12), new SolidBrush(UntabPageFontColor), Rec, StringF);
                }

            }
        }

        /// <summary>
        /// 设置TabPage标签页状态
        /// </summary>
        /// <param name="tabPageIndex"></param>
        public void SetTabPageErrorStatusColor(int tabPageIndex)
        {
            this.TabPages[tabPageIndex].Tag = TabPageStatus.ErrorStatus;
        }
    }

    /// <summary>
    /// 枚举
    /// </summary>
    public enum TabPageStatus : int
    {
        /// <summary>
        /// 错误状态
        /// </summary>
        ErrorStatus = 1,
    }
}
