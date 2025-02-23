﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Cell.IconFont
{
    /// <summary>
    /// Class ControlHelper.
    /// </summary>
    public static class ControlHelper
    {
        #region 设置控件Enabled，切不改变控件颜色
        /// <summary>
        /// 功能描述:设置控件Enabled，切不改变控件颜色
        /// </summary>
        /// <param name="c">c</param>
        /// <param name="enabled">enabled</param>
        public static void SetControlEnabled(this Control c, bool enabled)
        {
            if (!c.IsDisposed)
            {
                if (enabled)
                {
                    ControlHelper.SetWindowLong(c.Handle, -16, -134217729 & ControlHelper.GetWindowLong(c.Handle, -16));
                }
                else
                {
                    ControlHelper.SetWindowLong(c.Handle, -16, 134217728 + ControlHelper.GetWindowLong(c.Handle, -16));
                }
            }
        }

        /// <summary>
        /// 功能描述:设置控件Enabled，切不改变控件颜色      
        /// </summary>
        /// <param name="cs">cs</param>
        /// <param name="enabled">enabled</param>
        public static void SetControlEnableds(Control[] cs, bool enabled)
        {
            for (int i = 0; i < cs.Length; i++)
            {
                Control c = cs[i];
                SetControlEnabled(c, enabled);
            }
        }
        #endregion
        /// <summary>
        /// Sets the window long.
        /// </summary>
        /// <param name="hWnd">The h WND.</param>
        /// <param name="nIndex">Index of the n.</param>
        /// <param name="wndproc">The wndproc.</param>
        /// <returns>System.Int32.</returns>
        [DllImport("user32.dll ")]
        public static extern int SetWindowLong(IntPtr hWnd, int nIndex, int wndproc);

        /// <summary>
        /// Gets the window long.
        /// </summary>
        /// <param name="hWnd">The h WND.</param>
        /// <param name="nIndex">Index of the n.</param>
        /// <returns>System.Int32.</returns>
        [DllImport("user32.dll ")]
        public static extern int GetWindowLong(IntPtr hWnd, int nIndex);

        /// <summary>
        /// Gets the foreground window.
        /// </summary>
        /// <returns>IntPtr.</returns>
        [DllImport("user32.dll", CharSet = CharSet.Auto, ExactSpelling = true)]
        public static extern IntPtr GetForegroundWindow();

        /// <summary>
        /// Threads the base call back.
        /// </summary>
        /// <param name="parent">The parent.</param>
        /// <param name="obj">The object.</param>
        private static void ThreadBaseCallBack(Control parent, object obj)
        {
            if (obj is Exception)
            {
                if (parent != null)
                {
                    ThreadInvokerControl(parent, delegate
                    {
                        Exception ex = obj as Exception;
                    });
                }
            }
        }
        /// <summary>
        /// 委托调用主线程控件
        /// </summary>
        /// <param name="parent">主线程控件</param>
        /// <param name="action">修改控件方法</param>
        public static void ThreadInvokerControl(Control parent, Action action)
        {
            if (parent != null)
            {
                if (parent.InvokeRequired)
                {
                    parent.BeginInvoke(action);
                }
                else
                {
                    action();
                    SetForegroundWindow(parent.Handle);
                }
            }
        }



        /// <summary>
        /// Sets the foreground window.
        /// </summary>
        /// <param name="hWnd">The h WND.</param>
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
        [DllImport("user32.dll")]
        public static extern bool SetForegroundWindow(IntPtr hWnd);



        /// <summary>
        /// Closes the process panel.
        /// </summary>
        /// <param name="parent">The parent.</param>
        public static void CloseProcessPanel(Control parent)
        {
            if (parent.InvokeRequired)
            {
                parent.BeginInvoke(new MethodInvoker(delegate
                {
                    CloseProcessPanel(parent);
                }));
            }
            else if (parent != null)
            {
                Control control = HaveProcessPanelControl(parent);
                if (control != null)
                {
                    Form frm = control.Tag as Form;
                    if (frm != null && !frm.IsDisposed && frm.Visible)
                    {
                        if (frm.InvokeRequired)
                        {
                            frm.BeginInvoke(new MethodInvoker(delegate
                            {
                                frm.Hide();
                            }));
                        }
                        else
                        {
                            frm.Hide();
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Haves the process panel control.
        /// </summary>
        /// <param name="parent">The parent.</param>
        /// <returns>Control.</returns>
        public static Control HaveProcessPanelControl(Control parent)
        {
            Control[] array = parent.Controls.Find("myprogressPanelext", false);
            Control result;
            if (array.Length > 0)
            {
                result = array[0];
            }
            else
            {
                result = null;
            }
            return result;
        }



        /// <summary>
        /// Converts to array.
        /// </summary>
        /// <param name="controls">The controls.</param>
        /// <returns>Control[].</returns>
        public static Control[] ToArray(this System.Windows.Forms.Control.ControlCollection controls)
        {
            if (controls == null || controls.Count <= 0)
                return new Control[0];
            List<Control> lst = new List<Control>();
            foreach (Control item in controls)
            {
                lst.Add(item);
            }
            return lst.ToArray();
        }


        #region 根据控件宽度截取字符串
        /// <summary>
        /// 功能描述:根据控件宽度截取字符串   
        /// </summary>
        /// <param name="strSource">字符串</param>
        /// <param name="fltControlWidth">控件宽度</param>
        /// <param name="g">Graphics</param>
        /// <param name="font">字体</param>
        /// <returns>截取后的字符串</returns>
        public static string GetSubString(
            string strSource,
            float fltControlWidth,
            System.Drawing.Graphics g,
            System.Drawing.Font font)
        {
            try
            {
                fltControlWidth = fltControlWidth - 20;
                strSource = strSource.Trim();
                while (true)
                {

                    System.Drawing.SizeF sizeF = g.MeasureString(strSource.Replace(" ", "A"), font);
                    if (sizeF.Width > fltControlWidth)
                    {
                        strSource = strSource.TrimEnd('…');
                        if (strSource.Length <= 1)
                            return "";
                        strSource = strSource.Substring(0, strSource.Length - 1).Trim() + "…";
                    }
                    else
                    {
                        return strSource;
                    }
                }
            }
            finally
            {
                g.Dispose();
            }
        }
        #endregion

        #region 获取字符串宽度
        /// <summary>
        /// 功能描述:获取字符串宽度
        /// </summary>
        /// <param name="strSource">strSource</param>
        /// <param name="g">g</param>
        /// <param name="font">font</param>
        /// <returns>返回值</returns>
        public static int GetStringWidth(
           string strSource,
           System.Drawing.Graphics g,
           System.Drawing.Font font)
        {
            string[] strs = strSource.Split(new string[] { "\r\n" }, StringSplitOptions.RemoveEmptyEntries);
            float fltWidth = 0;
            foreach (var item in strs)
            {
                System.Drawing.SizeF sizeF = g.MeasureString(strSource.Replace(" ", "A"), font);
                if (sizeF.Width > fltWidth)
                    fltWidth = sizeF.Width;
            }

            return (int)fltWidth;
        }
        #endregion

        #region 动画特效
        /// <summary>
        /// Animates the window.
        /// </summary>
        /// <param name="whnd">The WHND.</param>
        /// <param name="dwtime">The dwtime.</param>
        /// <param name="dwflag">The dwflag.</param>
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
        [DllImport("user32.dll")]
        public static extern bool AnimateWindow(IntPtr whnd, int dwtime, int dwflag);
        //dwflag的取值如下
        /// <summary>
        /// The aw hor positive
        /// </summary>
        public const Int32 AW_HOR_POSITIVE = 0x00000001;
        //从左到右显示
        /// <summary>
        /// The aw hor negative
        /// </summary>
        public const Int32 AW_HOR_NEGATIVE = 0x00000002;
        //从右到左显示
        /// <summary>
        /// The aw ver positive
        /// </summary>
        public const Int32 AW_VER_POSITIVE = 0x00000004;
        //从上到下显示
        /// <summary>
        /// The aw ver negative
        /// </summary>
        public const Int32 AW_VER_NEGATIVE = 0x00000008;
        //从下到上显示
        /// <summary>
        /// The aw center
        /// </summary>
        public const Int32 AW_CENTER = 0x00000010;
        //若使用了AW_HIDE标志，则使窗口向内重叠，即收缩窗口；否则使窗口向外扩展，即展开窗口
        /// <summary>
        /// The aw hide
        /// </summary>
        public const Int32 AW_HIDE = 0x00010000;
        //隐藏窗口，缺省则显示窗口
        /// <summary>
        /// The aw activate
        /// </summary>
        public const Int32 AW_ACTIVATE = 0x00020000;
        //激活窗口。在使用了AW_HIDE标志后不能使用这个标志
        /// <summary>
        /// The aw slide
        /// </summary>
        public const Int32 AW_SLIDE = 0x00040000;
        //使用滑动类型。缺省则为滚动动画类型。当使用AW_CENTER标志时，这个标志就被忽略
        /// <summary>
        /// The aw blend
        /// </summary>
        public const Int32 AW_BLEND = 0x00080000;
        //透明度从高到低
        #endregion




        /// <summary>
        /// 设置GDI高质量模式抗锯齿
        /// </summary>
        /// <param name="g">The g.</param>
        public static void SetGDIHigh(this Graphics g)
        {
            g.SmoothingMode = SmoothingMode.AntiAlias;  //使绘图质量最高，即消除锯齿
            g.InterpolationMode = InterpolationMode.HighQualityBicubic;
            g.CompositingQuality = CompositingQuality.HighQuality;
        }

        /// <summary>
        /// 根据矩形和圆得到一个圆角矩形Path
        /// </summary>
        /// <param name="rect">The rect.</param>
        /// <param name="cornerRadius">The corner radius.</param>
        /// <returns>GraphicsPath.</returns>
        public static GraphicsPath CreateRoundedRectanglePath(this Rectangle rect, int cornerRadius)
        {
            GraphicsPath roundedRect = new GraphicsPath();
            roundedRect.AddArc(rect.X, rect.Y, cornerRadius * 2, cornerRadius * 2, 180, 90);
            roundedRect.AddLine(rect.X + cornerRadius, rect.Y, rect.Right - cornerRadius * 2, rect.Y);
            roundedRect.AddArc(rect.X + rect.Width - cornerRadius * 2, rect.Y, cornerRadius * 2, cornerRadius * 2, 270, 90);
            roundedRect.AddLine(rect.Right, rect.Y + cornerRadius * 2, rect.Right, rect.Y + rect.Height - cornerRadius * 2);
            roundedRect.AddArc(rect.X + rect.Width - cornerRadius * 2, rect.Y + rect.Height - cornerRadius * 2, cornerRadius * 2, cornerRadius * 2, 0, 90);
            roundedRect.AddLine(rect.Right - cornerRadius * 2, rect.Bottom, rect.X + cornerRadius * 2, rect.Bottom);
            roundedRect.AddArc(rect.X, rect.Bottom - cornerRadius * 2, cornerRadius * 2, cornerRadius * 2, 90, 90);
            roundedRect.AddLine(rect.X, rect.Bottom - cornerRadius * 2, rect.X, rect.Y + cornerRadius * 2);
            roundedRect.CloseFigure();
            return roundedRect;
        }

        /// <summary>
        /// Creates the rounded rectangle path.
        /// </summary>
        /// <param name="rect">The rect.</param>
        /// <param name="cornerRadius">The corner radius.</param>
        /// <returns>GraphicsPath.</returns>
        public static GraphicsPath CreateRoundedRectanglePath(this RectangleF rect, int cornerRadius)
        {
            GraphicsPath roundedRect = new GraphicsPath();
            roundedRect.AddArc(rect.X, rect.Y, cornerRadius * 2, cornerRadius * 2, 180, 90);
            roundedRect.AddLine(rect.X + cornerRadius, rect.Y, rect.Right - cornerRadius * 2, rect.Y);
            roundedRect.AddArc(rect.X + rect.Width - cornerRadius * 2, rect.Y, cornerRadius * 2, cornerRadius * 2, 270, 90);
            roundedRect.AddLine(rect.Right, rect.Y + cornerRadius * 2, rect.Right, rect.Y + rect.Height - cornerRadius * 2);
            roundedRect.AddArc(rect.X + rect.Width - cornerRadius * 2, rect.Y + rect.Height - cornerRadius * 2, cornerRadius * 2, cornerRadius * 2, 0, 90);
            roundedRect.AddLine(rect.Right - cornerRadius * 2, rect.Bottom, rect.X + cornerRadius * 2, rect.Bottom);
            roundedRect.AddArc(rect.X, rect.Bottom - cornerRadius * 2, cornerRadius * 2, cornerRadius * 2, 90, 90);
            roundedRect.AddLine(rect.X, rect.Bottom - cornerRadius * 2, rect.X, rect.Y + cornerRadius * 2);
            roundedRect.CloseFigure();
            return roundedRect;
        }
        /// <summary>
        /// Gets the colors.
        /// </summary>
        /// <value>The colors.</value>
        public static Color[] Colors { get; private set; }

        static ControlHelper()
        {
            List<Color> list = new List<Color>();
            list.Add(Color.FromArgb(55, 162, 218));
            list.Add(Color.FromArgb(50, 197, 233));
            list.Add(Color.FromArgb(103, 224, 227));
            list.Add(Color.FromArgb(159, 230, 184));
            list.Add(Color.FromArgb(255, 219, 92));
            list.Add(Color.FromArgb(255, 159, 127));
            list.Add(Color.FromArgb(251, 114, 147));
            list.Add(Color.FromArgb(224, 98, 174));
            list.Add(Color.FromArgb(230, 144, 209));
            list.Add(Color.FromArgb(231, 188, 243));
            list.Add(Color.FromArgb(157, 150, 245));
            list.Add(Color.FromArgb(131, 120, 234));
            list.Add(Color.FromArgb(150, 191, 255));

            list.Add(Color.FromArgb(243, 67, 54));
            list.Add(Color.FromArgb(156, 39, 176));
            list.Add(Color.FromArgb(103, 58, 183));
            list.Add(Color.FromArgb(63, 81, 181));
            list.Add(Color.FromArgb(33, 150, 243));
            list.Add(Color.FromArgb(0, 188, 211));
            list.Add(Color.FromArgb(3, 169, 244));
            list.Add(Color.FromArgb(0, 150, 136));
            list.Add(Color.FromArgb(139, 195, 74));
            list.Add(Color.FromArgb(76, 175, 80));
            list.Add(Color.FromArgb(204, 219, 57));
            list.Add(Color.FromArgb(233, 30, 99));
            list.Add(Color.FromArgb(254, 234, 59));
            list.Add(Color.FromArgb(254, 192, 7));
            list.Add(Color.FromArgb(254, 152, 0));
            list.Add(Color.FromArgb(255, 87, 34));
            list.Add(Color.FromArgb(121, 85, 72));
            list.Add(Color.FromArgb(158, 158, 158));
            list.Add(Color.FromArgb(96, 125, 139));
            list.Add(Color.FromArgb(252, 117, 85));
            list.Add(Color.FromArgb(172, 113, 191));
            list.Add(Color.FromArgb(115, 131, 253));
            list.Add(Color.FromArgb(78, 206, 255));
            list.Add(Color.FromArgb(121, 195, 82));
            list.Add(Color.FromArgb(255, 163, 28));
            list.Add(Color.FromArgb(255, 185, 15));
            list.Add(Color.FromArgb(255, 181, 197));
            list.Add(Color.FromArgb(255, 110, 180));
            list.Add(Color.FromArgb(255, 69, 0));
            list.Add(Color.FromArgb(255, 48, 48));
            list.Add(Color.FromArgb(154, 205, 50));
            list.Add(Color.FromArgb(155, 205, 155));
            list.Add(Color.FromArgb(154, 50, 205));
            list.Add(Color.FromArgb(131, 111, 255));
            list.Add(Color.FromArgb(124, 205, 124));
            list.Add(Color.FromArgb(0, 206, 209));
            list.Add(Color.FromArgb(0, 178, 238));
            list.Add(Color.FromArgb(56, 142, 142));

            Type typeFromHandle = typeof(Color);
            PropertyInfo[] properties = typeFromHandle.GetProperties();
            PropertyInfo[] array = properties;
            for (int i = 0; i < array.Length; i++)
            {
                PropertyInfo propertyInfo = array[i];
                if (propertyInfo.PropertyType == typeof(Color) && (propertyInfo.Name.StartsWith("Dark") || propertyInfo.Name.StartsWith("Medium")))
                {
                    object value = propertyInfo.GetValue(null, null);
                    list.Add((Color)value);
                }
            }
            Colors = list.ToArray();
        }
        /// <summary>
        /// Draws the string.
        /// </summary>
        /// <param name="g">The g.</param>
        /// <param name="s">The s.</param>
        /// <param name="font">The font.</param>
        /// <param name="brush">The brush.</param>
        /// <param name="point">The point.</param>
        /// <param name="format">The format.</param>
        /// <param name="angle">The angle.</param>
        public static void DrawString(Graphics g, string s, Font font, Brush brush, PointF point, StringFormat format, float angle)
        {
            Matrix transform = g.Transform;
            Matrix transform2 = g.Transform;
            transform2.RotateAt(angle, point);
            g.Transform = transform2;
            g.DrawString(s, font, brush, point, format);
            g.Transform = transform;
        }

        /// <summary>
        /// Gets the rhombus from rectangle.
        /// </summary>
        /// <param name="rect">The rect.</param>
        /// <returns>Point[].</returns>
        public static Point[] GetRhombusFromRectangle(Rectangle rect)
        {
            return new Point[5]
            {
                new Point(rect.X, rect.Y + rect.Height / 2),
                new Point(rect.X + rect.Width / 2, rect.Y + rect.Height - 1),
                new Point(rect.X + rect.Width - 1, rect.Y + rect.Height / 2),
                new Point(rect.X + rect.Width / 2, rect.Y),
                new Point(rect.X, rect.Y + rect.Height / 2)
            };
        }

        /// <summary>
        /// Computes the paint location y.
        /// </summary>
        /// <param name="max">The maximum.</param>
        /// <param name="min">The minimum.</param>
        /// <param name="height">The height.</param>
        /// <param name="value">The value.</param>
        /// <returns>System.Single.</returns>
        public static float ComputePaintLocationY(int max, int min, int height, int value)
        {
            if ((float)(max - min) == 0f)
            {
                return height;
            }
            return (float)height - (float)(value - min) * 1f / (float)(max - min) * (float)height;
        }

        /// <summary>
        /// Computes the paint location y.
        /// </summary>
        /// <param name="max">The maximum.</param>
        /// <param name="min">The minimum.</param>
        /// <param name="height">The height.</param>
        /// <param name="value">The value.</param>
        /// <returns>System.Single.</returns>
        public static float ComputePaintLocationY(float max, float min, float height, float value)
        {
            if (max - min == 0f)
            {
                return height;
            }
            return height - (value - min) / (max - min) * height;
        }


        /// <summary>
        /// Paints the coordinate divide.
        /// </summary>
        /// <param name="g">The g.</param>
        /// <param name="penLine">The pen line.</param>
        /// <param name="penDash">The pen dash.</param>
        /// <param name="font">The font.</param>
        /// <param name="brush">The brush.</param>
        /// <param name="sf">The sf.</param>
        /// <param name="degree">The degree.</param>
        /// <param name="max">The maximum.</param>
        /// <param name="min">The minimum.</param>
        /// <param name="width">The width.</param>
        /// <param name="height">The height.</param>
        /// <param name="left">The left.</param>
        /// <param name="right">The right.</param>
        /// <param name="up">Up.</param>
        /// <param name="down">Down.</param>
        public static void PaintCoordinateDivide(Graphics g, System.Drawing.Pen penLine, System.Drawing.Pen penDash, Font font, System.Drawing.Brush brush, StringFormat sf, int degree, int max, int min, int width, int height, int left = 60, int right = 8, int up = 8, int down = 8)
        {
            for (int i = 0; i <= degree; i++)
            {
                int value = (max - min) * i / degree + min;
                int num = (int)ComputePaintLocationY(max, min, height - up - down, value) + up + 1;
                g.DrawLine(penLine, left - 1, num, left - 4, num);
                if (i != 0)
                {
                    g.DrawLine(penDash, left, num, width - right, num);
                }
                g.DrawString(value.ToString(), font, brush, new Rectangle(-5, num - font.Height / 2, left, font.Height), sf);
            }
        }

        /// <summary>
        /// Paints the triangle.
        /// </summary>
        /// <param name="g">The g.</param>
        /// <param name="brush">The brush.</param>
        /// <param name="point">The point.</param>
        /// <param name="size">The size.</param>
        /// <param name="direction">The direction.</param>
        public static void PaintTriangle(Graphics g, System.Drawing.Brush brush, Point point, int size, GraphDirection direction)
        {
            Point[] array = new Point[4];
            switch (direction)
            {
                case GraphDirection.Leftward:
                    array[0] = new Point(point.X, point.Y - size);
                    array[1] = new Point(point.X, point.Y + size);
                    array[2] = new Point(point.X - 2 * size, point.Y);
                    break;
                case GraphDirection.Rightward:
                    array[0] = new Point(point.X, point.Y - size);
                    array[1] = new Point(point.X, point.Y + size);
                    array[2] = new Point(point.X + 2 * size, point.Y);
                    break;
                case GraphDirection.Upward:
                    array[0] = new Point(point.X - size, point.Y);
                    array[1] = new Point(point.X + size, point.Y);
                    array[2] = new Point(point.X, point.Y - 2 * size);
                    break;
                default:
                    array[0] = new Point(point.X - size, point.Y);
                    array[1] = new Point(point.X + size, point.Y);
                    array[2] = new Point(point.X, point.Y + 2 * size);
                    break;
            }
            array[3] = array[0];
            g.FillPolygon(brush, array);
        }

        /// <summary>
        /// Paints the triangle.
        /// </summary>
        /// <param name="g">The g.</param>
        /// <param name="brush">The brush.</param>
        /// <param name="point">The point.</param>
        /// <param name="size">The size.</param>
        /// <param name="direction">The direction.</param>
        public static void PaintTriangle(Graphics g, System.Drawing.Brush brush, PointF point, int size, GraphDirection direction)
        {
            PointF[] array = new PointF[4];
            switch (direction)
            {
                case GraphDirection.Leftward:
                    array[0] = new PointF(point.X, point.Y - (float)size);
                    array[1] = new PointF(point.X, point.Y + (float)size);
                    array[2] = new PointF(point.X - (float)(2 * size), point.Y);
                    break;
                case GraphDirection.Rightward:
                    array[0] = new PointF(point.X, point.Y - (float)size);
                    array[1] = new PointF(point.X, point.Y + (float)size);
                    array[2] = new PointF(point.X + (float)(2 * size), point.Y);
                    break;
                case GraphDirection.Upward:
                    array[0] = new PointF(point.X - (float)size, point.Y);
                    array[1] = new PointF(point.X + (float)size, point.Y);
                    array[2] = new PointF(point.X, point.Y - (float)(2 * size));
                    break;
                default:
                    array[0] = new PointF(point.X - (float)size, point.Y);
                    array[1] = new PointF(point.X + (float)size, point.Y);
                    array[2] = new PointF(point.X, point.Y + (float)(2 * size));
                    break;
            }
            array[3] = array[0];
            g.FillPolygon(brush, array);
        }

        /// <summary>
        /// Adds the array data.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="array">The array.</param>
        /// <param name="data">The data.</param>
        /// <param name="max">The maximum.</param>
        public static void AddArrayData<T>(ref T[] array, T[] data, int max)
        {
            if (data == null || data.Length == 0)
            {
                return;
            }
            if (array.Length == max)
            {
                Array.Copy(array, data.Length, array, 0, array.Length - data.Length);
                Array.Copy(data, 0, array, array.Length - data.Length, data.Length);
            }
            else if (array.Length + data.Length > max)
            {
                T[] array2 = new T[max];
                for (int i = 0; i < max - data.Length; i++)
                {
                    array2[i] = array[i + (array.Length - max + data.Length)];
                }
                for (int j = 0; j < data.Length; j++)
                {
                    array2[array2.Length - data.Length + j] = data[j];
                }
                array = array2;
            }
            else
            {
                T[] array3 = new T[array.Length + data.Length];
                for (int k = 0; k < array.Length; k++)
                {
                    array3[k] = array[k];
                }
                for (int l = 0; l < data.Length; l++)
                {
                    array3[array3.Length - data.Length + l] = data[l];
                }
                array = array3;
            }
        }

        /// <summary>
        /// Converts the size.
        /// </summary>
        /// <param name="size">The size.</param>
        /// <param name="angle">The angle.</param>
        /// <returns>SizeF.</returns>
        public static SizeF ConvertSize(SizeF size, float angle)
        {
            System.Drawing.Drawing2D.Matrix matrix = new System.Drawing.Drawing2D.Matrix();
            matrix.Rotate(angle);
            PointF[] array = new PointF[4];
            array[0].X = (0f - size.Width) / 2f;
            array[0].Y = (0f - size.Height) / 2f;
            array[1].X = (0f - size.Width) / 2f;
            array[1].Y = size.Height / 2f;
            array[2].X = size.Width / 2f;
            array[2].Y = size.Height / 2f;
            array[3].X = size.Width / 2f;
            array[3].Y = (0f - size.Height) / 2f;
            matrix.TransformPoints(array);
            float num = float.MaxValue;
            float num2 = float.MinValue;
            float num3 = float.MaxValue;
            float num4 = float.MinValue;
            PointF[] array2 = array;
            for (int i = 0; i < array2.Length; i++)
            {
                PointF pointF = array2[i];
                if (pointF.X < num)
                {
                    num = pointF.X;
                }
                if (pointF.X > num2)
                {
                    num2 = pointF.X;
                }
                if (pointF.Y < num3)
                {
                    num3 = pointF.Y;
                }
                if (pointF.Y > num4)
                {
                    num4 = pointF.Y;
                }
            }
            return new SizeF(num2 - num, num4 - num3);
        }



        /// <summary>
        /// Gets the pow.
        /// </summary>
        /// <param name="digit">The digit.</param>
        /// <returns>System.Int32.</returns>
        private static int GetPow(int digit)
        {
            int num = 1;
            for (int i = 0; i < digit; i++)
            {
                num *= 10;
            }
            return num;
        }

        /// <summary>
        /// Calculates the maximum section from.
        /// </summary>
        /// <param name="values">The values.</param>
        /// <returns>System.Int32.</returns>
        public static double CalculateMaxSectionFrom(double[] values)
        {
            double num = values.Max();
            return CalculateMaxSection(num);
        }

        public static double CalculateMaxSectionFrom(double[][] values)
        {
            double num = values.Max(p => p.Max());
            return CalculateMaxSection(num);
        }

        private static double CalculateMaxSection(double num)
        {
            if (num <= 5)
            {
                return 5;
            }
            if (num <= 10)
            {
                return 10;
            }
            int digit = num.ToString().Length - 2;
            int num2 = int.Parse(num.ToString().Substring(0, 2));
            if (num2 < 12)
            {
                return 12 * GetPow(digit);
            }
            if (num2 < 14)
            {
                return 14 * GetPow(digit);
            }
            if (num2 < 16)
            {
                return 16 * GetPow(digit);
            }
            if (num2 < 18)
            {
                return 18 * GetPow(digit);
            }
            if (num2 < 20)
            {
                return 20 * GetPow(digit);
            }
            if (num2 < 22)
            {
                return 22 * GetPow(digit);
            }
            if (num2 < 24)
            {
                return 24 * GetPow(digit);
            }
            if (num2 < 26)
            {
                return 26 * GetPow(digit);
            }
            if (num2 < 28)
            {
                return 28 * GetPow(digit);
            }
            if (num2 < 30)
            {
                return 30 * GetPow(digit);
            }
            if (num2 < 40)
            {
                return 40 * GetPow(digit);
            }
            if (num2 < 50)
            {
                return 50 * GetPow(digit);
            }
            if (num2 < 60)
            {
                return 60 * GetPow(digit);
            }
            if (num2 < 80)
            {
                return 80 * GetPow(digit);
            }
            return 100 * GetPow(digit);
        }

        /// <summary>
        /// Gets the color light.
        /// </summary>
        /// <param name="color">The color.</param>
        /// <returns>System.Drawing.Color.</returns>
        public static System.Drawing.Color GetColorLight(System.Drawing.Color color)
        {
            return System.Drawing.Color.FromArgb(color.R + (255 - color.R) * 40 / 100, color.G + (255 - color.G) * 40 / 100, color.B + (255 - color.B) * 40 / 100);
        }

        /// <summary>
        /// Gets the color light five.
        /// </summary>
        /// <param name="color">The color.</param>
        /// <returns>System.Drawing.Color.</returns>
        public static System.Drawing.Color GetColorLightFive(System.Drawing.Color color)
        {
            return System.Drawing.Color.FromArgb(color.R + (255 - color.R) * 50 / 100, color.G + (255 - color.G) * 50 / 100, color.B + (255 - color.B) * 50 / 100);
        }

        /// <summary>
        /// Gets the points from.
        /// </summary>
        /// <param name="points">The points.</param>
        /// <param name="soureWidth">Width of the soure.</param>
        /// <param name="sourceHeight">Height of the source.</param>
        /// <param name="width">The width.</param>
        /// <param name="height">The height.</param>
        /// <param name="dx">The dx.</param>
        /// <param name="dy">The dy.</param>
        /// <returns>PointF[].</returns>
        public static PointF[] GetPointsFrom(string points, float soureWidth, float sourceHeight, float width, float height, float dx = 0f, float dy = 0f)
        {
            string[] array = points.Split(new char[1]
            {
                ' '
            }, StringSplitOptions.RemoveEmptyEntries);
            PointF[] array2 = new PointF[array.Length];
            for (int i = 0; i < array.Length; i++)
            {
                int num = array[i].IndexOf(',');
                float num2 = Convert.ToSingle(array[i].Substring(0, num));
                float num3 = Convert.ToSingle(array[i].Substring(num + 1));
                array2[i] = new PointF(width * (num2 + dx) / soureWidth, height * (num3 + dy) / sourceHeight);
            }
            return array2;
        }






        /// <summary>
        /// 返回指定图片中的非透明区域；
        /// </summary>
        /// <param name="img">位图</param>
        /// <returns></returns>
        public static GraphicsPath CalculateControlGraphicsPath(Bitmap bitmap, Color? colorTransparent = null)
        {
            // Create GraphicsPath for our bitmap calculation 
            //创建 GraphicsPath
            GraphicsPath graphicsPath = new GraphicsPath();
            // Use the top left pixel as our transparent color 
            //使用左上角的一点的颜色作为我们透明色

            Color _colorTransparent = bitmap.GetPixel(0, 0);
            if (colorTransparent != null && colorTransparent != Color.Transparent && colorTransparent != Color.Empty)
                _colorTransparent = colorTransparent.Value;
            // This is to store the column value where an opaque pixel is first found. 
            // This value will determine where we start scanning for trailing opaque pixels.
            //第一个找到点的X
            int colOpaquePixel = 0;
            // Go through all rows (Y axis) 
            // 偏历所有行（Y方向）
            for (int row = 0; row < bitmap.Height; row++)
            {
                // Reset value 
                //重设
                colOpaquePixel = 0;
                // Go through all columns (X axis) 
                //偏历所有列（X方向）
                for (int col = 0; col < bitmap.Width; col++)
                {
                    // If this is an opaque pixel, mark it and search for anymore trailing behind 
                    //如果是不需要透明处理的点则标记，然后继续偏历
                    if (bitmap.GetPixel(col, row) != _colorTransparent)
                    {
                        // Opaque pixel found, mark current position
                        //记录当前
                        colOpaquePixel = col;
                        // Create another variable to set the current pixel position 
                        //建立新变量来记录当前点
                        int colNext = col;
                        // Starting from current found opaque pixel, search for anymore opaque pixels 
                        // trailing behind, until a transparent   pixel is found or minimum width is reached 
                        ///从找到的不透明点开始，继续寻找不透明点,一直到找到或则达到图片宽度 
                        for (colNext = colOpaquePixel; colNext < bitmap.Width; colNext++)
                            if (bitmap.GetPixel(colNext, row) == _colorTransparent)
                                break;
                        // Form a rectangle for line of opaque   pixels found and add it to our graphics path 
                        //将不透明点加到graphics path
                        graphicsPath.AddRectangle(new Rectangle(colOpaquePixel, row, colNext - colOpaquePixel, 1));
                        // No need to scan the line of opaque pixels just found 
                        col = colNext;
                    }
                }
            }
            // Return calculated graphics path 
            return graphicsPath;
        }

        /// <summary>
        /// 颜色加深
        /// </summary>
        /// <param name="color"></param>
        /// <param name="correctionFactor">-1.0f <= correctionFactor <= 1.0f</param>
        /// <returns></returns>
        public static Color ChangeColor(this Color color, float correctionFactor)
        {
            float red = (float)color.R;
            float green = (float)color.G;
            float blue = (float)color.B;

            if (correctionFactor < 0)
            {
                correctionFactor = 1 + correctionFactor;
                red *= correctionFactor;
                green *= correctionFactor;
                blue *= correctionFactor;
            }
            else
            {
                red = (255 - red) * correctionFactor + red;
                green = (255 - green) * correctionFactor + green;
                blue = (255 - blue) * correctionFactor + blue;
            }

            if (red < 0) red = 0;

            if (red > 255) red = 255;

            if (green < 0) green = 0;

            if (green > 255) green = 255;

            if (blue < 0) blue = 0;

            if (blue > 255) blue = 255;



            return Color.FromArgb(color.A, (int)red, (int)green, (int)blue);
        }
    }
}
