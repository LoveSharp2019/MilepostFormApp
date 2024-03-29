using Cell.IconFont;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Cell.UI
{
    public class ToggleBtn : Button
    {
        public ToggleBtn()
        {
            this.BackColor = Color.Gray;
            this.Cursor = System.Windows.Forms.Cursors.Hand;
            this.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.ForeColor = System.Drawing.Color.White;
            this.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
        }

        private FontIcons _iconStyle = FontIcons.None;
        private int _iconSize = 32;
        private Color _iconForeColor = Color.Black;
        private Color _iconBackColor = Color.White;

        /// <summary>
        /// 控件样式
        /// </summary>
        /// <value>The box style.</value>
        [Description("图片样式"), Category("自定义图像")]
        public FontIcons IconStyle
        {
            get { return _iconStyle; }
            set
            {
                if (_iconStyle == value)
                {
                    return;
                }
                _iconStyle = value;
                SetBackImage();
            }
        }

        void SetBackImage()
        {
            if (_iconStyle == FontIcons.None)
                this.Image = null;
            else
                this.Image = FontImages.GetImage(IconStyle, IconSize, _iconForeColor, _iconBackColor);
        }

        [Description("图片大小"), Category("自定义图像")]
        public int IconSize
        {
            get { return _iconSize; }
            set
            {
                if (_iconSize == value)
                {
                    return;
                }
                _iconSize = value;
                SetBackImage();
            }
        }

        [Description("前景色"), Category("自定义图像")]
        public Color IconForeColor
        {
            get { return _iconForeColor; }
            set
            {
                if (_iconForeColor == value)
                {
                    return;
                }
                _iconForeColor = value;
                SetBackImage();
            }
        }

        [Description("背景色"), Category("自定义图像")]
        public Color IconBackColor
        {
            get { return _iconBackColor; }
            set
            {
                if (_iconBackColor == value)
                {
                    return;
                }
                _iconBackColor = value;
                SetBackImage();
            }
        }


        private bool _checked { get; set; } = false;

        [Description("选择状态"), Category("自定义")]
        public bool Checked
        {
            get { return _checked; }
            set
            {
                if (_checked == value)
                    return;
                _checked = value;
                this.Refresh();
            }
        }

        private int _boxSize { get; set; } = 10;

        [Description("Box尺寸"), Category("自定义")]
        public int BoxSize
        {
            get { return _boxSize; }
            set
            {
                if (_boxSize == value)
                    return;
                _boxSize = value;
                this.Refresh();
            }
        }

        protected override void OnPaint(System.Windows.Forms.PaintEventArgs e)
        {

            base.OnPaint(e);
            // 获取按钮的绘图区域

            Rectangle rect = new Rectangle(this.Width - _boxSize - 2, this.Height - 2 - _boxSize, _boxSize, _boxSize);

            // 创建画布并绘制矩形框
            using (Pen pen = new Pen(Color.White)) // 边框颜色
            {
                e.Graphics.DrawRectangle(pen, rect);
            }

            if (_checked)
            {
                Rectangle rect1 = new Rectangle(this.Width - _boxSize , this.Height  - _boxSize, _boxSize-3, _boxSize-3);

                System.Drawing.SolidBrush brush1 = new System.Drawing.SolidBrush(System.Drawing.Color.White);

                e.Graphics.FillRectangle(brush1, rect1);
                brush1.Dispose();
                
            }

            // 正常情况下的绘制操作...
        }


    }
}

