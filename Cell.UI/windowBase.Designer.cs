
namespace Cell.UI
{
    partial class windowBase
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(windowBase));
            this.pnl_base1 = new System.Windows.Forms.Panel();
            this.pnl_base3 = new System.Windows.Forms.Panel();
            this.lbl_title = new System.Windows.Forms.Label();
            this.pnl_base2 = new System.Windows.Forms.Panel();
            this.btn_min = new System.Windows.Forms.PictureBox();
            this.btn_zoom = new System.Windows.Forms.PictureBox();
            this.btn_close = new System.Windows.Forms.PictureBox();
            this.pnl_context = new System.Windows.Forms.Panel();
            this.iconPic_title = new IconPic();
            this.pnl_base1.SuspendLayout();
            this.pnl_base3.SuspendLayout();
            this.pnl_base2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.btn_min)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btn_zoom)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btn_close)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.iconPic_title)).BeginInit();
            this.SuspendLayout();
            // 
            // pnl_base1
            // 
            this.pnl_base1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(57)))), ((int)(((byte)(61)))), ((int)(((byte)(73)))));
            this.pnl_base1.Controls.Add(this.pnl_base3);
            this.pnl_base1.Controls.Add(this.pnl_base2);
            this.pnl_base1.Cursor = System.Windows.Forms.Cursors.Hand;
            this.pnl_base1.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnl_base1.Location = new System.Drawing.Point(0, 0);
            this.pnl_base1.Name = "pnl_base1";
            this.pnl_base1.Size = new System.Drawing.Size(800, 36);
            this.pnl_base1.TabIndex = 0;
            this.pnl_base1.MouseDown += new System.Windows.Forms.MouseEventHandler(this.btn_MouseDown);
            this.pnl_base1.MouseMove += new System.Windows.Forms.MouseEventHandler(this.btn_MouseMove);
            // 
            // pnl_base3
            // 
            this.pnl_base3.Controls.Add(this.lbl_title);
            this.pnl_base3.Controls.Add(this.iconPic_title);
            this.pnl_base3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnl_base3.Location = new System.Drawing.Point(0, 0);
            this.pnl_base3.Name = "pnl_base3";
            this.pnl_base3.Size = new System.Drawing.Size(690, 36);
            this.pnl_base3.TabIndex = 18;
            this.pnl_base3.MouseDown += new System.Windows.Forms.MouseEventHandler(this.btn_MouseDown);
            this.pnl_base3.MouseMove += new System.Windows.Forms.MouseEventHandler(this.btn_MouseMove);
            // 
            // lbl_title
            // 
            this.lbl_title.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lbl_title.Font = new System.Drawing.Font("宋体", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lbl_title.ForeColor = System.Drawing.Color.White;
            this.lbl_title.Location = new System.Drawing.Point(36, 0);
            this.lbl_title.Name = "lbl_title";
            this.lbl_title.Size = new System.Drawing.Size(654, 36);
            this.lbl_title.TabIndex = 17;
            this.lbl_title.Text = "标题啊";
            this.lbl_title.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.lbl_title.MouseDown += new System.Windows.Forms.MouseEventHandler(this.btn_MouseDown);
            this.lbl_title.MouseMove += new System.Windows.Forms.MouseEventHandler(this.btn_MouseMove);
            // 
            // pnl_base2
            // 
            this.pnl_base2.Controls.Add(this.btn_min);
            this.pnl_base2.Controls.Add(this.btn_zoom);
            this.pnl_base2.Controls.Add(this.btn_close);
            this.pnl_base2.Dock = System.Windows.Forms.DockStyle.Right;
            this.pnl_base2.Location = new System.Drawing.Point(690, 0);
            this.pnl_base2.Name = "pnl_base2";
            this.pnl_base2.Size = new System.Drawing.Size(110, 36);
            this.pnl_base2.TabIndex = 0;
            // 
            // btn_min
            // 
            this.btn_min.BackColor = System.Drawing.Color.Transparent;
            this.btn_min.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btn_min.Dock = System.Windows.Forms.DockStyle.Right;
            this.btn_min.Image = ((System.Drawing.Image)(resources.GetObject("btn_min.Image")));
            this.btn_min.Location = new System.Drawing.Point(2, 0);
            this.btn_min.Name = "btn_min";
            this.btn_min.Size = new System.Drawing.Size(36, 36);
            this.btn_min.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.btn_min.TabIndex = 16;
            this.btn_min.TabStop = false;
            this.btn_min.Click += new System.EventHandler(this.btn_min_Click);
            this.btn_min.MouseLeave += new System.EventHandler(this.btn_MouseLeave);
            this.btn_min.MouseHover += new System.EventHandler(this.btn_MouseHover);
            // 
            // btn_zoom
            // 
            this.btn_zoom.BackColor = System.Drawing.Color.Transparent;
            this.btn_zoom.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btn_zoom.Dock = System.Windows.Forms.DockStyle.Right;
            this.btn_zoom.Image = ((System.Drawing.Image)(resources.GetObject("btn_zoom.Image")));
            this.btn_zoom.Location = new System.Drawing.Point(38, 0);
            this.btn_zoom.Name = "btn_zoom";
            this.btn_zoom.Size = new System.Drawing.Size(36, 36);
            this.btn_zoom.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.btn_zoom.TabIndex = 15;
            this.btn_zoom.TabStop = false;
            this.btn_zoom.Click += new System.EventHandler(this.btn_zoom_Click);
            this.btn_zoom.MouseLeave += new System.EventHandler(this.btn_MouseLeave);
            this.btn_zoom.MouseHover += new System.EventHandler(this.btn_MouseHover);
            // 
            // btn_close
            // 
            this.btn_close.BackColor = System.Drawing.Color.Transparent;
            this.btn_close.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btn_close.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btn_close.Dock = System.Windows.Forms.DockStyle.Right;
            this.btn_close.Image = ((System.Drawing.Image)(resources.GetObject("btn_close.Image")));
            this.btn_close.Location = new System.Drawing.Point(74, 0);
            this.btn_close.Name = "btn_close";
            this.btn_close.Size = new System.Drawing.Size(36, 36);
            this.btn_close.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.btn_close.TabIndex = 14;
            this.btn_close.TabStop = false;
            this.btn_close.Click += new System.EventHandler(this.btn_close_Click);
            this.btn_close.MouseLeave += new System.EventHandler(this.btn_MouseLeave);
            this.btn_close.MouseHover += new System.EventHandler(this.btn_MouseHover);
            // 
            // pnl_context
            // 
            this.pnl_context.BackColor = System.Drawing.Color.Transparent;
            this.pnl_context.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnl_context.Location = new System.Drawing.Point(0, 36);
            this.pnl_context.Name = "pnl_context";
            this.pnl_context.Size = new System.Drawing.Size(800, 414);
            this.pnl_context.TabIndex = 1;
            // 
            // iconPic_title
            // 
            this.iconPic_title.BackColor = System.Drawing.Color.Transparent;
            this.iconPic_title.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("iconPic_title.BackgroundImage")));
            this.iconPic_title.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.iconPic_title.Dock = System.Windows.Forms.DockStyle.Left;
            this.iconPic_title.IconBackColor = System.Drawing.Color.Transparent;
            this.iconPic_title.IconForeColor = System.Drawing.Color.Chartreuse;
            this.iconPic_title.IconSize = 36;
            this.iconPic_title.IconStyle = Cell.IconFont.FontIcons.A_fa_list_alt;
            this.iconPic_title.Location = new System.Drawing.Point(0, 0);
            this.iconPic_title.Name = "iconPic_title";
            this.iconPic_title.Size = new System.Drawing.Size(36, 36);
            this.iconPic_title.TabIndex = 18;
            this.iconPic_title.TabStop = false;
            this.iconPic_title.MouseDown += new System.Windows.Forms.MouseEventHandler(this.btn_MouseDown);
            this.iconPic_title.MouseMove += new System.Windows.Forms.MouseEventHandler(this.btn_MouseMove);
            // 
            // windowBase
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.pnl_context);
            this.Controls.Add(this.pnl_base1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "windowBase";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "windowBase";
            this.pnl_base1.ResumeLayout(false);
            this.pnl_base3.ResumeLayout(false);
            this.pnl_base2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.btn_min)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btn_zoom)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btn_close)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.iconPic_title)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnl_base1;
        private System.Windows.Forms.PictureBox btn_min;
        public System.Windows.Forms.PictureBox btn_zoom;
        private System.Windows.Forms.PictureBox btn_close;
        private System.Windows.Forms.Label lbl_title;
        public System.Windows.Forms.Panel pnl_context;
        public System.Windows.Forms.Panel pnl_base2;
        private System.Windows.Forms.Panel pnl_base3;
        public IconPic iconPic_title;
    }
}