
namespace Sys.IStations
{
    partial class FormStationBaseAxisPanel
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
            this.components = new System.ComponentModel.Container();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.panelAxisOpt = new System.Windows.Forms.Panel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.btOpenAllDev = new System.Windows.Forms.Button();
            this.cbSaveWorkPos = new System.Windows.Forms.ComboBox();
            this.cbMoveWorkPos = new System.Windows.Forms.ComboBox();
            this.btSaveCurr2WorkPos = new System.Windows.Forms.Button();
            this.btMoveToWorkPos = new System.Windows.Forms.Button();
            this.splitContainer2 = new System.Windows.Forms.SplitContainer();
            this.panelAxisStatus = new System.Windows.Forms.Panel();
            this.timerFlush = new System.Windows.Forms.Timer(this.components);
            this.ucRichTextScrollTips1 = new Tissue.UI.UcRichTextScrollTips();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).BeginInit();
            this.splitContainer2.Panel1.SuspendLayout();
            this.splitContainer2.Panel2.SuspendLayout();
            this.splitContainer2.SuspendLayout();
            this.SuspendLayout();
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.panelAxisOpt);
            this.splitContainer1.Panel1.Controls.Add(this.panel1);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.splitContainer2);
            this.splitContainer1.Size = new System.Drawing.Size(881, 450);
            this.splitContainer1.SplitterDistance = 390;
            this.splitContainer1.TabIndex = 0;
            // 
            // panelAxisOpt
            // 
            this.panelAxisOpt.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panelAxisOpt.AutoScroll = true;
            this.panelAxisOpt.BackColor = System.Drawing.SystemColors.ActiveBorder;
            this.panelAxisOpt.Location = new System.Drawing.Point(2, 1);
            this.panelAxisOpt.Name = "panelAxisOpt";
            this.panelAxisOpt.Size = new System.Drawing.Size(386, 332);
            this.panelAxisOpt.TabIndex = 1;
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.BackColor = System.Drawing.SystemColors.ActiveBorder;
            this.panel1.Controls.Add(this.btOpenAllDev);
            this.panel1.Controls.Add(this.cbSaveWorkPos);
            this.panel1.Controls.Add(this.cbMoveWorkPos);
            this.panel1.Controls.Add(this.btSaveCurr2WorkPos);
            this.panel1.Controls.Add(this.btMoveToWorkPos);
            this.panel1.Location = new System.Drawing.Point(2, 337);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(386, 110);
            this.panel1.TabIndex = 0;
            // 
            // btOpenAllDev
            // 
            this.btOpenAllDev.Location = new System.Drawing.Point(9, 4);
            this.btOpenAllDev.Name = "btOpenAllDev";
            this.btOpenAllDev.Size = new System.Drawing.Size(114, 23);
            this.btOpenAllDev.TabIndex = 4;
            this.btOpenAllDev.Text = "打开所有轴设备";
            this.btOpenAllDev.UseVisualStyleBackColor = true;
            this.btOpenAllDev.Click += new System.EventHandler(this.btOpenAllDev_Click);
            // 
            // cbSaveWorkPos
            // 
            this.cbSaveWorkPos.FormattingEnabled = true;
            this.cbSaveWorkPos.Location = new System.Drawing.Point(119, 83);
            this.cbSaveWorkPos.Name = "cbSaveWorkPos";
            this.cbSaveWorkPos.Size = new System.Drawing.Size(213, 20);
            this.cbSaveWorkPos.TabIndex = 3;
            // 
            // cbMoveWorkPos
            // 
            this.cbMoveWorkPos.FormattingEnabled = true;
            this.cbMoveWorkPos.Location = new System.Drawing.Point(119, 54);
            this.cbMoveWorkPos.Name = "cbMoveWorkPos";
            this.cbMoveWorkPos.Size = new System.Drawing.Size(213, 20);
            this.cbMoveWorkPos.TabIndex = 2;
            // 
            // btSaveCurr2WorkPos
            // 
            this.btSaveCurr2WorkPos.Location = new System.Drawing.Point(2, 82);
            this.btSaveCurr2WorkPos.Name = "btSaveCurr2WorkPos";
            this.btSaveCurr2WorkPos.Size = new System.Drawing.Size(114, 23);
            this.btSaveCurr2WorkPos.TabIndex = 1;
            this.btSaveCurr2WorkPos.Text = "当前位置保存为";
            this.btSaveCurr2WorkPos.UseVisualStyleBackColor = true;
            this.btSaveCurr2WorkPos.Click += new System.EventHandler(this.btSaveCurr2WorkPos_Click);
            // 
            // btMoveToWorkPos
            // 
            this.btMoveToWorkPos.Location = new System.Drawing.Point(2, 53);
            this.btMoveToWorkPos.Name = "btMoveToWorkPos";
            this.btMoveToWorkPos.Size = new System.Drawing.Size(114, 23);
            this.btMoveToWorkPos.TabIndex = 0;
            this.btMoveToWorkPos.Text = "移动到指定位置";
            this.btMoveToWorkPos.UseVisualStyleBackColor = true;
            this.btMoveToWorkPos.Click += new System.EventHandler(this.btMoveToWorkPos_Click);
            // 
            // splitContainer2
            // 
            this.splitContainer2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer2.Location = new System.Drawing.Point(0, 0);
            this.splitContainer2.Name = "splitContainer2";
            this.splitContainer2.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer2.Panel1
            // 
            this.splitContainer2.Panel1.Controls.Add(this.panelAxisStatus);
            // 
            // splitContainer2.Panel2
            // 
            this.splitContainer2.Panel2.Controls.Add(this.ucRichTextScrollTips1);
            this.splitContainer2.Size = new System.Drawing.Size(487, 450);
            this.splitContainer2.SplitterDistance = 333;
            this.splitContainer2.TabIndex = 0;
            // 
            // panelAxisStatus
            // 
            this.panelAxisStatus.AutoScroll = true;
            this.panelAxisStatus.BackColor = System.Drawing.SystemColors.ActiveBorder;
            this.panelAxisStatus.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelAxisStatus.Location = new System.Drawing.Point(0, 0);
            this.panelAxisStatus.Name = "panelAxisStatus";
            this.panelAxisStatus.Size = new System.Drawing.Size(487, 333);
            this.panelAxisStatus.TabIndex = 0;
            // 
            // timerFlush
            // 
            this.timerFlush.Tick += new System.EventHandler(this.timerFlush_Tick);
            // 
            // ucRichTextScrollTips1
            // 
            this.ucRichTextScrollTips1.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.ucRichTextScrollTips1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ucRichTextScrollTips1.IsAppendTimeInfo = true;
            this.ucRichTextScrollTips1.IsAutoScrollLast = true;
            this.ucRichTextScrollTips1.Location = new System.Drawing.Point(0, 0);
            this.ucRichTextScrollTips1.Margin = new System.Windows.Forms.Padding(2);
            this.ucRichTextScrollTips1.MaxTipsCount = 100;
            this.ucRichTextScrollTips1.Name = "ucRichTextScrollTips1";
            this.ucRichTextScrollTips1.Size = new System.Drawing.Size(487, 113);
            this.ucRichTextScrollTips1.TabIndex = 0;
            // 
            // FormStationBaseAxisPanel
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(881, 450);
            this.Controls.Add(this.splitContainer1);
            this.Name = "FormStationBaseAxisPanel";
            this.Text = "工站:轴面板";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FormStationBaseAxisPanel_FormClosing);
            this.Load += new System.EventHandler(this.FormAxisInStationBase_Load);
            this.VisibleChanged += new System.EventHandler(this.FormStationBaseAxisPanel_VisibleChanged);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.splitContainer2.Panel1.ResumeLayout(false);
            this.splitContainer2.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).EndInit();
            this.splitContainer2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.Panel panelAxisOpt;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.SplitContainer splitContainer2;
        private System.Windows.Forms.Panel panelAxisStatus;
        private System.Windows.Forms.ComboBox cbSaveWorkPos;
        private System.Windows.Forms.ComboBox cbMoveWorkPos;
        private System.Windows.Forms.Button btSaveCurr2WorkPos;
        private System.Windows.Forms.Button btMoveToWorkPos;
        private System.Windows.Forms.Timer timerFlush;
        private System.Windows.Forms.Button btOpenAllDev;
        private Tissue.UI.UcRichTextScrollTips ucRichTextScrollTips1;
    }
}