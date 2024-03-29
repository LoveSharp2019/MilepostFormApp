using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Cell.UI
{
    public class UcGroupBox : GroupBox
    {
        private ContentAlignment _titleAlign = ContentAlignment.MiddleCenter;

        [Browsable(true), Description("标题位置"), Category("自定义分组")]
        public ContentAlignment TitleAlign
        {

            get { return _titleAlign; }
            set
            {
                _titleAlign = value;
                this.Invalidate();
            }
        }

        private Font _titleFont = new System.Drawing.Font("微软雅黑", 12F);

        [Browsable(true), Description("标题字体"), Category("自定义分组")]
        public Font TitleFont
        {
            get { return _titleFont; }
            set
            {
                _titleFont = value;
                this.Invalidate();
            }
        }

        private Color _titleBackGroundCor = Color.Black;
        [Browsable(true), Description("标题背景色颜色"), Category("自定义分组")]
        public Color TitleBackGroundCor
        {
            get { return _titleBackGroundCor; }
            set
            {
                _titleBackGroundCor = value;
                this.Invalidate();
            }
        }

        private ButtonBorderStyle _borderStyle = ButtonBorderStyle.Solid;
        [Browsable(true), Description("边框样式"), Category("自定义分组")]
        public ButtonBorderStyle BorderStyle
        {
            get { return _borderStyle; }
            set
            {
                _borderStyle = value;
                this.Invalidate();
            }
        }

        private Color _BorderColor = Color.Black;
        [Browsable(true), Description("边框颜色"), Category("自定义分组")]
        public Color BorderColor
        {
            get { return _BorderColor; }
            set
            {
                _BorderColor = value;
                this.Invalidate();
            }
        }

        private int _BorderSize = 1;

        [Browsable(true), Description("边框粗细"), Category("自定义分组")]
        public int BorderSize
        {
            get { return _BorderSize; }
            set
            {
                _BorderSize = value;
                this.Invalidate();
            }
        }

        private Color _FColor = Color.White;
        [Browsable(true), Description("渐变色1"), Category("自定义分组")]
        public Color FColor
        {
            get { return _FColor; }
            set
            {
                _FColor = value;
                this.Invalidate();
            }
        }

        private Color _TColor = Color.White;
        [Browsable(true), Description("渐变色2"), Category("自定义分组")]
        public Color TColor
        {
            get { return _TColor; }
            set
            {
                _TColor = value;
                this.Invalidate();
            }
        }

        // 重写 group 内容区域
        public override Rectangle DisplayRectangle
        {
            get
            {
                Rectangle rect = base.DisplayRectangle;
                SizeF fontSize = new SizeF(1, 1);
                using (Graphics graphics = CreateGraphics())
                {
                    fontSize = graphics.MeasureString(this.Text, this._titleFont);
                }
                return new Rectangle(new Point(1, (int)fontSize.Height), new Size(this.Width - 2, this.Height - (int)fontSize.Height - 1));
            }
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            //背景颜色
            e.Graphics.Clear(System.Drawing.SystemColors.Control);

            using (Brush b = new LinearGradientBrush(this.ClientRectangle, FColor, TColor, LinearGradientMode.Vertical)) //实例化刷子，第一个参数指示上色区域，第二个和第三个参数分别渐变颜色的开始和结束，第四个参数表示颜色的方向。
                e.Graphics.FillRectangle(b, this.ClientRectangle);

            // 测量字体大小
            SizeF fontSize = e.Graphics.MeasureString(this.Text, this._titleFont);

            //标题背景色          
            e.Graphics.FillRectangle(new SolidBrush(TitleBackGroundCor), new Rectangle(new Point(0, 0), new Size(this.Width, (int)fontSize.Height)));

            // 画标题位置
            if (_titleAlign == ContentAlignment.MiddleCenter || _titleAlign == ContentAlignment.TopCenter || _titleAlign == ContentAlignment.BottomCenter)
                e.Graphics.DrawString(this.Text, this._titleFont, Brushes.Black, (this.Width - fontSize.Width) / 2, 1);
            if (_titleAlign == ContentAlignment.MiddleLeft || _titleAlign == ContentAlignment.BottomLeft || _titleAlign == ContentAlignment.TopLeft)
                e.Graphics.DrawString(this.Text, this._titleFont, Brushes.Black, 1, 1);
            if (_titleAlign == ContentAlignment.MiddleRight || _titleAlign == ContentAlignment.BottomRight || _titleAlign == ContentAlignment.TopRight)
                e.Graphics.DrawString(this.Text, this._titleFont, Brushes.Black, this.Width - fontSize.Width - 1, 1);

            // 画边框
            ControlPaint.DrawBorder(e.Graphics,
                            this.ClientRectangle,
                            this._BorderColor,
                            this._BorderSize,
                            this._borderStyle,
                            this._BorderColor,
                            this._BorderSize,
                            this._borderStyle,
                            this._BorderColor,
                            this._BorderSize,
                            this._borderStyle,
                            this._BorderColor,
                            this._BorderSize,
                            this._borderStyle);

        }

    }
}
