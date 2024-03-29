using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Cell.UI
{
    public partial class windowBase : Form
    {
        Point _pointFormMove;
        public windowBase()
        {
            InitializeComponent();
            this.DoubleBuffered = true;
        }

        public void clearcontrol()
        {
            pnl_context.Controls.Clear();
        }

        protected override void OnTextChanged(EventArgs e)
        {
            base.OnTextChanged(e);
            lbl_title.Text = this.Text;
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            e.Graphics.DrawRectangle(Pens.Gray, 0, 0, this.Width - 1, this.Height - 1);
        }

        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);
            this.Refresh();
        }

        private void btn_close_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btn_zoom_Click(object sender, EventArgs e)
        {
            switch (this.WindowState)
            {
                case FormWindowState.Normal:
                    窗体最大化();
                    break;
                case FormWindowState.Maximized:
                    this.WindowState = FormWindowState.Normal;
                    break;
            }
        }

        void 窗体最大化()
        {
            this.MaximumSize = new Size(Screen.PrimaryScreen.WorkingArea.Width, Screen.PrimaryScreen.WorkingArea.Height);
            this.WindowState = FormWindowState.Maximized;
        }

        private void btn_min_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void btn_MouseHover(object sender, EventArgs e)
        {
            ((PictureBox)sender).BackColor = Color.Red;
        }

        private void btn_MouseLeave(object sender, EventArgs e)
        {
            ((PictureBox)sender).BackColor = Color.Transparent;
        }

        private void btn_MouseDown(object sender, MouseEventArgs e)
        {
            _pointFormMove = new Point(e.X, e.Y);
        }

        private void btn_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                this.Location = new Point(this.Location.X + e.X - _pointFormMove.X,
                    this.Location.Y + e.Y - _pointFormMove.Y);
            }
        }
    }
}
