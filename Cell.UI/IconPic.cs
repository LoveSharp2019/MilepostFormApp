using Cell.IconFont;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Cell.UI
{
    public class IconPic : PictureBox
    {
        public IconPic()
        {
            this.BackColor = Color.Gray;
            this.BackgroundImageLayout = ImageLayout.Center;
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
                this.BackgroundImage = null;
            else
                this.BackgroundImage = FontImages.GetImage(IconStyle, IconSize, _iconForeColor, _iconBackColor);
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
    }
}
