
using Cell.UI;
using Tissue.UI;

namespace MilepostFormApp
{
    partial class Form1
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.btn_MulChlName = new Cell.UI.IconBtn();
            this.btn_Devcfg = new Cell.UI.IconBtn();
            this.btn_stationtest = new Cell.UI.IconBtn();
            this.pnl_UserControl = new System.Windows.Forms.Panel();
            this.iconBtn3 = new Cell.UI.IconBtn();
            this.btn_strat = new Cell.UI.IconBtn();
            this.btn_rest = new Cell.UI.IconBtn();
            this.btn_stop = new Cell.UI.IconBtn();
            this.pnl_leftpart = new System.Windows.Forms.Panel();
            this.tbtn_Showlog = new Cell.UI.ToggleBtn();
            this.btn_home = new Cell.UI.IconBtn();
            this.panel2 = new System.Windows.Forms.Panel();
            this.pnl_toppart = new System.Windows.Forms.Panel();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabel2 = new System.Windows.Forms.ToolStripStatusLabel();
            this.statusLabelUserName = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabel3 = new System.Windows.Forms.ToolStripStatusLabel();
            this.statusLabelUserLevel = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabel6 = new System.Windows.Forms.ToolStripStatusLabel();
            this.statusLabelDevStatus = new System.Windows.Forms.ToolStripStatusLabel();
            ((System.ComponentModel.ISupportInitialize)(this.btn_zoom)).BeginInit();
            this.pnl_context.SuspendLayout();
            this.pnl_base2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.iconPic_title)).BeginInit();
            this.pnl_leftpart.SuspendLayout();
            this.panel2.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnl_context
            // 
            this.pnl_context.Controls.Add(this.panel2);
            this.pnl_context.Controls.Add(this.pnl_leftpart);
            this.pnl_context.Controls.Add(this.statusStrip1);
            this.pnl_context.Size = new System.Drawing.Size(1169, 704);
            // 
            // pnl_base2
            // 
            this.pnl_base2.Location = new System.Drawing.Point(1059, 0);
            // 
            // iconPic_title
            // 
            this.iconPic_title.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("iconPic_title.BackgroundImage")));
            this.iconPic_title.IconStyle = Cell.IconFont.FontIcons.A_fa_home;
            // 
            // btn_MulChlName
            // 
            this.btn_MulChlName.BackColor = System.Drawing.Color.Gray;
            this.btn_MulChlName.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btn_MulChlName.Dock = System.Windows.Forms.DockStyle.Top;
            this.btn_MulChlName.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_MulChlName.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btn_MulChlName.ForeColor = System.Drawing.Color.White;
            this.btn_MulChlName.IconBackColor = System.Drawing.Color.Transparent;
            this.btn_MulChlName.IconForeColor = System.Drawing.Color.Chartreuse;
            this.btn_MulChlName.IconSize = 50;
            this.btn_MulChlName.IconStyle = Cell.IconFont.FontIcons.E_icon_ol;
            this.btn_MulChlName.Image = ((System.Drawing.Image)(resources.GetObject("btn_MulChlName.Image")));
            this.btn_MulChlName.Location = new System.Drawing.Point(0, 209);
            this.btn_MulChlName.Name = "btn_MulChlName";
            this.btn_MulChlName.Size = new System.Drawing.Size(198, 54);
            this.btn_MulChlName.TabIndex = 1;
            this.btn_MulChlName.Text = "设备多通道";
            this.btn_MulChlName.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btn_MulChlName.UseVisualStyleBackColor = false;
            this.btn_MulChlName.Visible = false;
            this.btn_MulChlName.Click += new System.EventHandler(this.iconBtn1_Click);
            // 
            // btn_Devcfg
            // 
            this.btn_Devcfg.BackColor = System.Drawing.Color.Gray;
            this.btn_Devcfg.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btn_Devcfg.Dock = System.Windows.Forms.DockStyle.Top;
            this.btn_Devcfg.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_Devcfg.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btn_Devcfg.ForeColor = System.Drawing.Color.White;
            this.btn_Devcfg.IconBackColor = System.Drawing.Color.Transparent;
            this.btn_Devcfg.IconForeColor = System.Drawing.Color.Chartreuse;
            this.btn_Devcfg.IconSize = 50;
            this.btn_Devcfg.IconStyle = Cell.IconFont.FontIcons.E_icon_documents_alt;
            this.btn_Devcfg.Image = ((System.Drawing.Image)(resources.GetObject("btn_Devcfg.Image")));
            this.btn_Devcfg.Location = new System.Drawing.Point(0, 155);
            this.btn_Devcfg.Name = "btn_Devcfg";
            this.btn_Devcfg.Size = new System.Drawing.Size(198, 54);
            this.btn_Devcfg.TabIndex = 0;
            this.btn_Devcfg.Text = "设备配置";
            this.btn_Devcfg.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btn_Devcfg.UseVisualStyleBackColor = true;
            this.btn_Devcfg.Click += new System.EventHandler(this.button1_Click);
            // 
            // btn_stationtest
            // 
            this.btn_stationtest.BackColor = System.Drawing.Color.Gray;
            this.btn_stationtest.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btn_stationtest.Dock = System.Windows.Forms.DockStyle.Top;
            this.btn_stationtest.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_stationtest.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btn_stationtest.ForeColor = System.Drawing.Color.White;
            this.btn_stationtest.IconBackColor = System.Drawing.Color.Transparent;
            this.btn_stationtest.IconForeColor = System.Drawing.Color.Chartreuse;
            this.btn_stationtest.IconSize = 50;
            this.btn_stationtest.IconStyle = Cell.IconFont.FontIcons.E_icon_map;
            this.btn_stationtest.Image = ((System.Drawing.Image)(resources.GetObject("btn_stationtest.Image")));
            this.btn_stationtest.Location = new System.Drawing.Point(0, 263);
            this.btn_stationtest.Name = "btn_stationtest";
            this.btn_stationtest.Size = new System.Drawing.Size(198, 54);
            this.btn_stationtest.TabIndex = 2;
            this.btn_stationtest.Text = "工站调试";
            this.btn_stationtest.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btn_stationtest.UseVisualStyleBackColor = true;
            this.btn_stationtest.Click += new System.EventHandler(this.iconBtn2_Click);
            // 
            // pnl_UserControl
            // 
            this.pnl_UserControl.BackColor = System.Drawing.Color.White;
            this.pnl_UserControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnl_UserControl.Location = new System.Drawing.Point(0, 10);
            this.pnl_UserControl.Name = "pnl_UserControl";
            this.pnl_UserControl.Size = new System.Drawing.Size(971, 672);
            this.pnl_UserControl.TabIndex = 4;
            // 
            // iconBtn3
            // 
            this.iconBtn3.BackColor = System.Drawing.Color.Gray;
            this.iconBtn3.Cursor = System.Windows.Forms.Cursors.Hand;
            this.iconBtn3.Dock = System.Windows.Forms.DockStyle.Top;
            this.iconBtn3.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.iconBtn3.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.iconBtn3.ForeColor = System.Drawing.Color.White;
            this.iconBtn3.IconBackColor = System.Drawing.Color.Transparent;
            this.iconBtn3.IconForeColor = System.Drawing.Color.Chartreuse;
            this.iconBtn3.IconSize = 50;
            this.iconBtn3.IconStyle = Cell.IconFont.FontIcons.E_social_googledrive;
            this.iconBtn3.Image = ((System.Drawing.Image)(resources.GetObject("iconBtn3.Image")));
            this.iconBtn3.Location = new System.Drawing.Point(0, 101);
            this.iconBtn3.Name = "iconBtn3";
            this.iconBtn3.Size = new System.Drawing.Size(198, 54);
            this.iconBtn3.TabIndex = 6;
            this.iconBtn3.Text = "主界面配置";
            this.iconBtn3.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.iconBtn3.UseVisualStyleBackColor = true;
            this.iconBtn3.Visible = false;
            this.iconBtn3.Click += new System.EventHandler(this.iconBtn3_Click);
            // 
            // btn_strat
            // 
            this.btn_strat.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btn_strat.BackColor = System.Drawing.Color.Gray;
            this.btn_strat.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btn_strat.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_strat.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btn_strat.ForeColor = System.Drawing.Color.White;
            this.btn_strat.IconBackColor = System.Drawing.Color.Transparent;
            this.btn_strat.IconForeColor = System.Drawing.Color.Chartreuse;
            this.btn_strat.IconSize = 50;
            this.btn_strat.IconStyle = Cell.IconFont.FontIcons.E_arrow_triangle_right_alt2;
            this.btn_strat.Image = ((System.Drawing.Image)(resources.GetObject("btn_strat.Image")));
            this.btn_strat.Location = new System.Drawing.Point(21, 377);
            this.btn_strat.Name = "btn_strat";
            this.btn_strat.Size = new System.Drawing.Size(132, 67);
            this.btn_strat.TabIndex = 7;
            this.btn_strat.Text = "启  动";
            this.btn_strat.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btn_strat.UseVisualStyleBackColor = true;
            this.btn_strat.Click += new System.EventHandler(this.btn_strat_Click);
            // 
            // btn_rest
            // 
            this.btn_rest.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btn_rest.BackColor = System.Drawing.Color.Gray;
            this.btn_rest.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btn_rest.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_rest.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btn_rest.ForeColor = System.Drawing.Color.White;
            this.btn_rest.IconBackColor = System.Drawing.Color.Transparent;
            this.btn_rest.IconForeColor = System.Drawing.Color.Chartreuse;
            this.btn_rest.IconSize = 50;
            this.btn_rest.IconStyle = Cell.IconFont.FontIcons.E_arrow_back;
            this.btn_rest.Image = ((System.Drawing.Image)(resources.GetObject("btn_rest.Image")));
            this.btn_rest.Location = new System.Drawing.Point(21, 450);
            this.btn_rest.Name = "btn_rest";
            this.btn_rest.Size = new System.Drawing.Size(132, 66);
            this.btn_rest.TabIndex = 8;
            this.btn_rest.Text = "复  位";
            this.btn_rest.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btn_rest.UseVisualStyleBackColor = true;
            this.btn_rest.Click += new System.EventHandler(this.btn_rest_Click);
            // 
            // btn_stop
            // 
            this.btn_stop.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btn_stop.BackColor = System.Drawing.Color.Gray;
            this.btn_stop.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btn_stop.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_stop.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btn_stop.ForeColor = System.Drawing.Color.White;
            this.btn_stop.IconBackColor = System.Drawing.Color.Transparent;
            this.btn_stop.IconForeColor = System.Drawing.Color.Chartreuse;
            this.btn_stop.IconSize = 50;
            this.btn_stop.IconStyle = Cell.IconFont.FontIcons.E_icon_stop_alt2;
            this.btn_stop.Image = ((System.Drawing.Image)(resources.GetObject("btn_stop.Image")));
            this.btn_stop.Location = new System.Drawing.Point(21, 522);
            this.btn_stop.Name = "btn_stop";
            this.btn_stop.Size = new System.Drawing.Size(132, 68);
            this.btn_stop.TabIndex = 9;
            this.btn_stop.Text = "停  止";
            this.btn_stop.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btn_stop.UseVisualStyleBackColor = true;
            this.btn_stop.Click += new System.EventHandler(this.btn_stop_Click);
            // 
            // pnl_leftpart
            // 
            this.pnl_leftpart.BackColor = System.Drawing.Color.Gray;
            this.pnl_leftpart.Controls.Add(this.tbtn_Showlog);
            this.pnl_leftpart.Controls.Add(this.btn_stationtest);
            this.pnl_leftpart.Controls.Add(this.btn_stop);
            this.pnl_leftpart.Controls.Add(this.btn_MulChlName);
            this.pnl_leftpart.Controls.Add(this.btn_rest);
            this.pnl_leftpart.Controls.Add(this.btn_Devcfg);
            this.pnl_leftpart.Controls.Add(this.btn_strat);
            this.pnl_leftpart.Controls.Add(this.iconBtn3);
            this.pnl_leftpart.Controls.Add(this.btn_home);
            this.pnl_leftpart.Dock = System.Windows.Forms.DockStyle.Left;
            this.pnl_leftpart.Location = new System.Drawing.Point(0, 0);
            this.pnl_leftpart.Name = "pnl_leftpart";
            this.pnl_leftpart.Size = new System.Drawing.Size(198, 682);
            this.pnl_leftpart.TabIndex = 10;
            // 
            // tbtn_Showlog
            // 
            this.tbtn_Showlog.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.tbtn_Showlog.BackColor = System.Drawing.Color.Gray;
            this.tbtn_Showlog.BoxSize = 22;
            this.tbtn_Showlog.Checked = false;
            this.tbtn_Showlog.Cursor = System.Windows.Forms.Cursors.Hand;
            this.tbtn_Showlog.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.tbtn_Showlog.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.tbtn_Showlog.ForeColor = System.Drawing.Color.White;
            this.tbtn_Showlog.IconBackColor = System.Drawing.Color.Transparent;
            this.tbtn_Showlog.IconForeColor = System.Drawing.Color.Chartreuse;
            this.tbtn_Showlog.IconSize = 50;
            this.tbtn_Showlog.IconStyle = Cell.IconFont.FontIcons.E_icon_error_circle_alt;
            this.tbtn_Showlog.Image = ((System.Drawing.Image)(resources.GetObject("tbtn_Showlog.Image")));
            this.tbtn_Showlog.Location = new System.Drawing.Point(21, 596);
            this.tbtn_Showlog.Name = "tbtn_Showlog";
            this.tbtn_Showlog.Size = new System.Drawing.Size(132, 66);
            this.tbtn_Showlog.TabIndex = 10;
            this.tbtn_Showlog.Text = "日  志";
            this.tbtn_Showlog.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tbtn_Showlog.UseVisualStyleBackColor = false;
            this.tbtn_Showlog.Click += new System.EventHandler(this.tbtn_Showog_Click);
            // 
            // btn_home
            // 
            this.btn_home.BackColor = System.Drawing.Color.Gray;
            this.btn_home.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btn_home.Dock = System.Windows.Forms.DockStyle.Top;
            this.btn_home.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_home.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btn_home.ForeColor = System.Drawing.Color.White;
            this.btn_home.IconBackColor = System.Drawing.Color.Transparent;
            this.btn_home.IconForeColor = System.Drawing.Color.Chartreuse;
            this.btn_home.IconSize = 50;
            this.btn_home.IconStyle = Cell.IconFont.FontIcons.E_icon_house_alt;
            this.btn_home.Image = ((System.Drawing.Image)(resources.GetObject("btn_home.Image")));
            this.btn_home.Location = new System.Drawing.Point(0, 0);
            this.btn_home.Name = "btn_home";
            this.btn_home.Size = new System.Drawing.Size(198, 101);
            this.btn_home.TabIndex = 7;
            this.btn_home.Text = " 主  页";
            this.btn_home.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btn_home.UseVisualStyleBackColor = true;
            this.btn_home.Click += new System.EventHandler(this.btn_home_Click);
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.pnl_UserControl);
            this.panel2.Controls.Add(this.pnl_toppart);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(198, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(971, 682);
            this.panel2.TabIndex = 11;
            // 
            // pnl_toppart
            // 
            this.pnl_toppart.BackColor = System.Drawing.Color.Gray;
            this.pnl_toppart.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnl_toppart.Location = new System.Drawing.Point(0, 0);
            this.pnl_toppart.Name = "pnl_toppart";
            this.pnl_toppart.Size = new System.Drawing.Size(971, 10);
            this.pnl_toppart.TabIndex = 5;
            // 
            // statusStrip1
            // 
            this.statusStrip1.ImageScalingSize = new System.Drawing.Size(32, 32);
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel1,
            this.toolStripStatusLabel2,
            this.statusLabelUserName,
            this.toolStripStatusLabel3,
            this.statusLabelUserLevel,
            this.toolStripStatusLabel6,
            this.statusLabelDevStatus});
            this.statusStrip1.Location = new System.Drawing.Point(0, 682);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(1169, 22);
            this.statusStrip1.TabIndex = 2;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // toolStripStatusLabel1
            // 
            this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            this.toolStripStatusLabel1.Size = new System.Drawing.Size(164, 17);
            this.toolStripStatusLabel1.Text = "苏州鼎纳自动化技术有限公司";
            // 
            // toolStripStatusLabel2
            // 
            this.toolStripStatusLabel2.Name = "toolStripStatusLabel2";
            this.toolStripStatusLabel2.Size = new System.Drawing.Size(35, 17);
            this.toolStripStatusLabel2.Text = "用户:";
            // 
            // statusLabelUserName
            // 
            this.statusLabelUserName.ForeColor = System.Drawing.SystemColors.HotTrack;
            this.statusLabelUserName.Name = "statusLabelUserName";
            this.statusLabelUserName.Size = new System.Drawing.Size(44, 17);
            this.statusLabelUserName.Text = "未登录";
            // 
            // toolStripStatusLabel3
            // 
            this.toolStripStatusLabel3.Name = "toolStripStatusLabel3";
            this.toolStripStatusLabel3.Size = new System.Drawing.Size(35, 17);
            this.toolStripStatusLabel3.Text = "权限:";
            // 
            // statusLabelUserLevel
            // 
            this.statusLabelUserLevel.ForeColor = System.Drawing.SystemColors.HotTrack;
            this.statusLabelUserLevel.Name = "statusLabelUserLevel";
            this.statusLabelUserLevel.Size = new System.Drawing.Size(44, 17);
            this.statusLabelUserLevel.Text = "操作员";
            // 
            // toolStripStatusLabel6
            // 
            this.toolStripStatusLabel6.Name = "toolStripStatusLabel6";
            this.toolStripStatusLabel6.Size = new System.Drawing.Size(59, 17);
            this.toolStripStatusLabel6.Text = "设备状态:";
            // 
            // statusLabelDevStatus
            // 
            this.statusLabelDevStatus.ForeColor = System.Drawing.SystemColors.HotTrack;
            this.statusLabelDevStatus.Name = "statusLabelDevStatus";
            this.statusLabelDevStatus.Size = new System.Drawing.Size(44, 17);
            this.statusLabelDevStatus.Text = "未运行";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1169, 740);
            this.Name = "Form1";
            this.Text = "主界面";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.Form1_FormClosed);
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.btn_zoom)).EndInit();
            this.pnl_context.ResumeLayout(false);
            this.pnl_context.PerformLayout();
            this.pnl_base2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.iconPic_title)).EndInit();
            this.pnl_leftpart.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private IconBtn btn_Devcfg;
        private IconBtn btn_MulChlName;
        private IconBtn btn_stationtest;
        private System.Windows.Forms.Panel pnl_UserControl;
        private IconBtn iconBtn3;
        private IconBtn btn_stop;
        private IconBtn btn_rest;
        private IconBtn btn_strat;
        private System.Windows.Forms.Panel pnl_leftpart;
        private IconBtn btn_home;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Panel pnl_toppart;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel2;
        private System.Windows.Forms.ToolStripStatusLabel statusLabelUserName;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel3;
        private System.Windows.Forms.ToolStripStatusLabel statusLabelUserLevel;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel6;
        private System.Windows.Forms.ToolStripStatusLabel statusLabelDevStatus;
        private ToggleBtn tbtn_Showlog;
    }
}

