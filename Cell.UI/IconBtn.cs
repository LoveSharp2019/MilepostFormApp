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
    public class IconBtn : Button
    {
        public IconBtn()
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

        //protected override void OnPaint(System.Windows.Forms.PaintEventArgs e)
        //{

        //    base.OnPaint(e);

        //    if (Enabled == false) // 判断按钮是否被禁用
        //    {

        //        using (SolidBrush brush = new SolidBrush(Color.Red)) // 设置禁用状态时的文本颜色为灰色
        //            e.Graphics.DrawString("Disabled", Font, brush, ClientRectangle);

        //    }

        //    // 正常情况下的绘制操作...
        //}
    }
}
