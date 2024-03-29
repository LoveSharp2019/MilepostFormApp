using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Cell.UI
{
    public partial class LampButton : Button
    {
        ControlState _state = ControlState.Normal;
        /// <summary>
        /// 
        /// </summary>
        public LampButton()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 
        /// </summary>
        public enum ControlState
        {
            /// <summary>
            /// 
            /// </summary>
            Normal,
            /// <summary>
            /// 
            /// </summary>
            Hover,
            /// <summary>
            /// 
            /// </summary>
            Pressed,
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="e"></param>
        protected override void OnMouseLeave(EventArgs e)
        {
            base.OnMouseLeave(e);
            _state = ControlState.Normal;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="mevent"></param>
        protected override void OnMouseMove(MouseEventArgs mevent)
        {
            base.OnMouseMove(mevent);
            _state = ControlState.Hover;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="e"></param>
        protected override void OnMouseDown(MouseEventArgs e)
        {
            base.OnMouseDown(e);
            _state = ControlState.Pressed;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="e"></param>
        protected override void OnMouseUp(MouseEventArgs e)
        {
            base.OnMouseUp(e);
            _state = ControlState.Hover;
        }
        private void CalculateRect(
        out Rectangle imageRect, out Rectangle textRect, Graphics g)
        {
            if (Image != null)
            {
                imageRect = new Rectangle(0, ClientRectangle.Height/6,
                    ClientRectangle.Height *2/3, ClientRectangle.Height * 2 / 3);
                textRect = new Rectangle(ClientRectangle.Height *5/ 6, 0,
                    ClientRectangle.Width - ClientRectangle.Height * 5 / 6, ClientRectangle.Height);

            }
            else
            {
                imageRect = new Rectangle(0, 0, 0, 0);
                textRect = ClientRectangle;
            }

        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="e"></param>
        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaintBackground(e);
            Graphics g = e.Graphics;
            Rectangle imageRect;
            Rectangle textRect;

            CalculateRect(out imageRect, out textRect, g);
            g.SmoothingMode = SmoothingMode.HighQuality;


            if (_state == ControlState.Normal)
            {
                using (SolidBrush brush = new SolidBrush(BackColor))
                {
                    g.FillRectangle(brush, this.ClientRectangle);
                }
            }
            else if (_state == ControlState.Hover)
            {
                using (SolidBrush brush = new SolidBrush(System.Drawing.Color.LightGray))
                {
                    g.FillRectangle(brush, ClientRectangle);
                }
            }
            else if (_state == ControlState.Pressed)
            {
                using (SolidBrush brush = new SolidBrush(System.Drawing.Color.Gainsboro))
                {
                    g.FillRectangle(brush, ClientRectangle);
                }
            }

            if (Image != null)
            {
                g.InterpolationMode = InterpolationMode.HighQualityBilinear;
                g.DrawImage(Image, imageRect.Left, imageRect.Top, imageRect.Width,imageRect.Height);              
            }
            //画文字      
            if (Text != "")
            {
                TextRenderer.DrawText(
                    g,
                    Text,
                    Font,
                   textRect,
                    ForeColor,
                    TextFormatFlags.VerticalCenter | TextFormatFlags.Left);
            }

        }


        [Category("General"), Description("Control's Value"), Browsable(true)]
        public Size IconSize
        {
            get { return imageList1.ImageSize; }
            set { imageList1.ImageSize = value; }
        }

        public enum LColor
        {
            Gray = 0,
            Green = 1,
            Red ,
            Yellow
        }

        public LColor LampColor
        {
            get
            {
                return (LColor)ImageIndex;
            }
            set
            {
                ImageIndex = (int)value;
            }
        }
    }
}
