
using Cell.UI;
using Tissue.UI;

namespace Body.IMainStation
{
    partial class UcMainStationBasePanel
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

        #region 组件设计器生成的代码

        /// <summary> 
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.btPackup = new System.Windows.Forms.Button();
            this.pnCumstom = new System.Windows.Forms.Panel();
            this.panelStatus = new System.Windows.Forms.Panel();
            this.lampWarning = new Cell.UI.LampButton();
            this.lampWorkStatus = new Cell.UI.LampButton();
            this.panelDio = new System.Windows.Forms.Panel();
            this.gbDeclearedIO = new System.Windows.Forms.GroupBox();
            this.splitContainer2 = new System.Windows.Forms.SplitContainer();
            this.tbPanelDIs = new System.Windows.Forms.FlowLayoutPanel();
            this.tbPanelDOs = new System.Windows.Forms.FlowLayoutPanel();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.lampResetLight = new Cell.UI.LampButton();
            this.lampResetButton = new Cell.UI.LampButton();
            this.lampStartLight = new Cell.UI.LampButton();
            this.lampStopLight = new Cell.UI.LampButton();
            this.lampPauseLight = new Cell.UI.LampButton();
            this.lampStartButton = new Cell.UI.LampButton();
            this.lampStopButton = new Cell.UI.LampButton();
            this.lampPauseButton = new Cell.UI.LampButton();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.lampAlam = new Cell.UI.LampButton();
            this.lampGreen = new Cell.UI.LampButton();
            this.lampYellow = new Cell.UI.LampButton();
            this.lampRed = new Cell.UI.LampButton();
            this.lampEMGLight = new Cell.UI.LampButton();
            this.lampEMGButton = new Cell.UI.LampButton();
            this.panelStatus.SuspendLayout();
            this.panelDio.SuspendLayout();
            this.gbDeclearedIO.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).BeginInit();
            this.splitContainer2.Panel1.SuspendLayout();
            this.splitContainer2.Panel2.SuspendLayout();
            this.splitContainer2.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // timer1
            // 
            this.timer1.Enabled = true;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // btPackup
            // 
            this.btPackup.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btPackup.Location = new System.Drawing.Point(1058, 5);
            this.btPackup.Margin = new System.Windows.Forms.Padding(2);
            this.btPackup.Name = "btPackup";
            this.btPackup.Size = new System.Drawing.Size(38, 9);
            this.btPackup.TabIndex = 46;
            this.btPackup.Text = "^^^";
            this.btPackup.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            this.btPackup.UseVisualStyleBackColor = true;
            this.btPackup.Click += new System.EventHandler(this.btPackup_Click);
            // 
            // pnCumstom
            // 
            this.pnCumstom.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pnCumstom.BackColor = System.Drawing.SystemColors.ControlDark;
            this.pnCumstom.Location = new System.Drawing.Point(2, 165);
            this.pnCumstom.Margin = new System.Windows.Forms.Padding(2);
            this.pnCumstom.Name = "pnCumstom";
            this.pnCumstom.Size = new System.Drawing.Size(1094, 412);
            this.pnCumstom.TabIndex = 45;
            // 
            // panelStatus
            // 
            this.panelStatus.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panelStatus.Controls.Add(this.groupBox1);
            this.panelStatus.Controls.Add(this.lampWarning);
            this.panelStatus.Controls.Add(this.lampWorkStatus);
            this.panelStatus.Location = new System.Drawing.Point(2, 15);
            this.panelStatus.Margin = new System.Windows.Forms.Padding(2);
            this.panelStatus.Name = "panelStatus";
            this.panelStatus.Size = new System.Drawing.Size(1094, 50);
            this.panelStatus.TabIndex = 61;
            // 
            // lampWarning
            // 
            this.lampWarning.BackColor = System.Drawing.SystemColors.ControlLight;
            this.lampWarning.Enabled = false;
            this.lampWarning.FlatAppearance.BorderColor = System.Drawing.Color.Black;
            this.lampWarning.FlatAppearance.BorderSize = 0;
            this.lampWarning.Font = new System.Drawing.Font("宋体", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lampWarning.IconSize = new System.Drawing.Size(32, 32);
            this.lampWarning.ImageIndex = 0;
            this.lampWarning.LampColor = Cell.UI.LampButton.LColor.Gray;
            this.lampWarning.Location = new System.Drawing.Point(184, 2);
            this.lampWarning.Name = "lampWarning";
            this.lampWarning.Size = new System.Drawing.Size(288, 43);
            this.lampWarning.TabIndex = 59;
            this.lampWarning.Text = "正常";
            this.lampWarning.UseVisualStyleBackColor = false;
            // 
            // lampWorkStatus
            // 
            this.lampWorkStatus.BackColor = System.Drawing.SystemColors.ControlLight;
            this.lampWorkStatus.Enabled = false;
            this.lampWorkStatus.FlatAppearance.BorderColor = System.Drawing.Color.Black;
            this.lampWorkStatus.FlatAppearance.BorderSize = 0;
            this.lampWorkStatus.Font = new System.Drawing.Font("宋体", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lampWorkStatus.IconSize = new System.Drawing.Size(32, 32);
            this.lampWorkStatus.ImageIndex = 0;
            this.lampWorkStatus.LampColor = Cell.UI.LampButton.LColor.Gray;
            this.lampWorkStatus.Location = new System.Drawing.Point(2, 2);
            this.lampWorkStatus.Name = "lampWorkStatus";
            this.lampWorkStatus.Size = new System.Drawing.Size(180, 43);
            this.lampWorkStatus.TabIndex = 58;
            this.lampWorkStatus.Text = "未开始/已停止";
            this.lampWorkStatus.UseVisualStyleBackColor = false;
            // 
            // panelDio
            // 
            this.panelDio.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panelDio.Controls.Add(this.gbDeclearedIO);
            this.panelDio.Controls.Add(this.groupBox2);
            this.panelDio.Location = new System.Drawing.Point(2, 66);
            this.panelDio.Margin = new System.Windows.Forms.Padding(2);
            this.panelDio.Name = "panelDio";
            this.panelDio.Size = new System.Drawing.Size(1094, 96);
            this.panelDio.TabIndex = 62;
            // 
            // gbDeclearedIO
            // 
            this.gbDeclearedIO.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.gbDeclearedIO.Controls.Add(this.splitContainer2);
            this.gbDeclearedIO.Location = new System.Drawing.Point(250, 0);
            this.gbDeclearedIO.Margin = new System.Windows.Forms.Padding(2);
            this.gbDeclearedIO.Name = "gbDeclearedIO";
            this.gbDeclearedIO.Padding = new System.Windows.Forms.Padding(2);
            this.gbDeclearedIO.Size = new System.Drawing.Size(840, 96);
            this.gbDeclearedIO.TabIndex = 63;
            this.gbDeclearedIO.TabStop = false;
            this.gbDeclearedIO.Text = "其他输入/输出设备";
            // 
            // splitContainer2
            // 
            this.splitContainer2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.splitContainer2.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.splitContainer2.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.splitContainer2.Location = new System.Drawing.Point(2, 16);
            this.splitContainer2.Margin = new System.Windows.Forms.Padding(2);
            this.splitContainer2.Name = "splitContainer2";
            // 
            // splitContainer2.Panel1
            // 
            this.splitContainer2.Panel1.BackColor = System.Drawing.SystemColors.Control;
            this.splitContainer2.Panel1.Controls.Add(this.tbPanelDIs);
            this.splitContainer2.Panel1MinSize = 0;
            // 
            // splitContainer2.Panel2
            // 
            this.splitContainer2.Panel2.BackColor = System.Drawing.SystemColors.Control;
            this.splitContainer2.Panel2.Controls.Add(this.tbPanelDOs);
            this.splitContainer2.Panel2MinSize = 0;
            this.splitContainer2.Size = new System.Drawing.Size(834, 78);
            this.splitContainer2.SplitterDistance = 675;
            this.splitContainer2.SplitterWidth = 2;
            this.splitContainer2.TabIndex = 0;
            // 
            // tbPanelDIs
            // 
            this.tbPanelDIs.AutoScroll = true;
            this.tbPanelDIs.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tbPanelDIs.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.tbPanelDIs.Location = new System.Drawing.Point(0, 0);
            this.tbPanelDIs.Margin = new System.Windows.Forms.Padding(2);
            this.tbPanelDIs.Name = "tbPanelDIs";
            this.tbPanelDIs.Size = new System.Drawing.Size(675, 78);
            this.tbPanelDIs.TabIndex = 0;
            // 
            // tbPanelDOs
            // 
            this.tbPanelDOs.AutoScroll = true;
            this.tbPanelDOs.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tbPanelDOs.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.tbPanelDOs.Location = new System.Drawing.Point(0, 0);
            this.tbPanelDOs.Margin = new System.Windows.Forms.Padding(2);
            this.tbPanelDOs.Name = "tbPanelDOs";
            this.tbPanelDOs.Size = new System.Drawing.Size(157, 78);
            this.tbPanelDOs.TabIndex = 0;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.lampEMGLight);
            this.groupBox2.Controls.Add(this.lampEMGButton);
            this.groupBox2.Controls.Add(this.lampResetLight);
            this.groupBox2.Controls.Add(this.lampResetButton);
            this.groupBox2.Controls.Add(this.lampStartLight);
            this.groupBox2.Controls.Add(this.lampStopLight);
            this.groupBox2.Controls.Add(this.lampPauseLight);
            this.groupBox2.Controls.Add(this.lampStartButton);
            this.groupBox2.Controls.Add(this.lampStopButton);
            this.groupBox2.Controls.Add(this.lampPauseButton);
            this.groupBox2.Location = new System.Drawing.Point(2, 0);
            this.groupBox2.Margin = new System.Windows.Forms.Padding(2);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Padding = new System.Windows.Forms.Padding(2);
            this.groupBox2.Size = new System.Drawing.Size(244, 95);
            this.groupBox2.TabIndex = 62;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "控制面板";
            // 
            // lampResetLight
            // 
            this.lampResetLight.BackColor = System.Drawing.SystemColors.Control;
            this.lampResetLight.Enabled = false;
            this.lampResetLight.FlatAppearance.BorderColor = System.Drawing.Color.Black;
            this.lampResetLight.FlatAppearance.BorderSize = 0;
            this.lampResetLight.IconSize = new System.Drawing.Size(32, 32);
            this.lampResetLight.ImageIndex = 0;
            this.lampResetLight.LampColor = Cell.UI.LampButton.LColor.Gray;
            this.lampResetLight.Location = new System.Drawing.Point(201, 36);
            this.lampResetLight.Margin = new System.Windows.Forms.Padding(2);
            this.lampResetLight.Name = "lampResetLight";
            this.lampResetLight.Size = new System.Drawing.Size(38, 23);
            this.lampResetLight.TabIndex = 16;
            this.lampResetLight.Text = "灯";
            this.lampResetLight.UseVisualStyleBackColor = false;
            // 
            // lampResetButton
            // 
            this.lampResetButton.BackColor = System.Drawing.SystemColors.Control;
            this.lampResetButton.Enabled = false;
            this.lampResetButton.FlatAppearance.BorderColor = System.Drawing.Color.Black;
            this.lampResetButton.FlatAppearance.BorderSize = 0;
            this.lampResetButton.IconSize = new System.Drawing.Size(32, 32);
            this.lampResetButton.ImageIndex = 0;
            this.lampResetButton.LampColor = Cell.UI.LampButton.LColor.Gray;
            this.lampResetButton.Location = new System.Drawing.Point(124, 36);
            this.lampResetButton.Margin = new System.Windows.Forms.Padding(2);
            this.lampResetButton.Name = "lampResetButton";
            this.lampResetButton.Size = new System.Drawing.Size(73, 23);
            this.lampResetButton.TabIndex = 14;
            this.lampResetButton.Text = "复位按钮";
            this.lampResetButton.UseVisualStyleBackColor = false;
            // 
            // lampStartLight
            // 
            this.lampStartLight.BackColor = System.Drawing.SystemColors.Control;
            this.lampStartLight.Enabled = false;
            this.lampStartLight.FlatAppearance.BorderColor = System.Drawing.Color.Black;
            this.lampStartLight.FlatAppearance.BorderSize = 0;
            this.lampStartLight.IconSize = new System.Drawing.Size(32, 32);
            this.lampStartLight.ImageIndex = 0;
            this.lampStartLight.LampColor = Cell.UI.LampButton.LColor.Gray;
            this.lampStartLight.Location = new System.Drawing.Point(84, 17);
            this.lampStartLight.Margin = new System.Windows.Forms.Padding(2);
            this.lampStartLight.Name = "lampStartLight";
            this.lampStartLight.Size = new System.Drawing.Size(38, 23);
            this.lampStartLight.TabIndex = 9;
            this.lampStartLight.Text = "灯";
            this.lampStartLight.UseVisualStyleBackColor = false;
            // 
            // lampStopLight
            // 
            this.lampStopLight.BackColor = System.Drawing.SystemColors.Control;
            this.lampStopLight.Enabled = false;
            this.lampStopLight.FlatAppearance.BorderColor = System.Drawing.Color.Black;
            this.lampStopLight.FlatAppearance.BorderSize = 0;
            this.lampStopLight.IconSize = new System.Drawing.Size(32, 32);
            this.lampStopLight.ImageIndex = 0;
            this.lampStopLight.LampColor = Cell.UI.LampButton.LColor.Gray;
            this.lampStopLight.Location = new System.Drawing.Point(82, 54);
            this.lampStopLight.Margin = new System.Windows.Forms.Padding(2);
            this.lampStopLight.Name = "lampStopLight";
            this.lampStopLight.Size = new System.Drawing.Size(38, 23);
            this.lampStopLight.TabIndex = 11;
            this.lampStopLight.Text = "灯";
            this.lampStopLight.UseVisualStyleBackColor = false;
            // 
            // lampPauseLight
            // 
            this.lampPauseLight.BackColor = System.Drawing.SystemColors.Control;
            this.lampPauseLight.Enabled = false;
            this.lampPauseLight.FlatAppearance.BorderColor = System.Drawing.Color.Black;
            this.lampPauseLight.FlatAppearance.BorderSize = 0;
            this.lampPauseLight.IconSize = new System.Drawing.Size(32, 32);
            this.lampPauseLight.ImageIndex = 0;
            this.lampPauseLight.LampColor = Cell.UI.LampButton.LColor.Gray;
            this.lampPauseLight.Location = new System.Drawing.Point(83, 36);
            this.lampPauseLight.Margin = new System.Windows.Forms.Padding(2);
            this.lampPauseLight.Name = "lampPauseLight";
            this.lampPauseLight.Size = new System.Drawing.Size(38, 23);
            this.lampPauseLight.TabIndex = 10;
            this.lampPauseLight.Text = "灯";
            this.lampPauseLight.UseVisualStyleBackColor = false;
            // 
            // lampStartButton
            // 
            this.lampStartButton.BackColor = System.Drawing.SystemColors.Control;
            this.lampStartButton.Enabled = false;
            this.lampStartButton.FlatAppearance.BorderColor = System.Drawing.Color.Black;
            this.lampStartButton.FlatAppearance.BorderSize = 0;
            this.lampStartButton.IconSize = new System.Drawing.Size(32, 32);
            this.lampStartButton.ImageIndex = 0;
            this.lampStartButton.LampColor = Cell.UI.LampButton.LColor.Gray;
            this.lampStartButton.Location = new System.Drawing.Point(5, 17);
            this.lampStartButton.Margin = new System.Windows.Forms.Padding(2);
            this.lampStartButton.Name = "lampStartButton";
            this.lampStartButton.Size = new System.Drawing.Size(75, 23);
            this.lampStartButton.TabIndex = 4;
            this.lampStartButton.Text = "开始按钮";
            this.lampStartButton.UseVisualStyleBackColor = false;
            // 
            // lampStopButton
            // 
            this.lampStopButton.BackColor = System.Drawing.SystemColors.Control;
            this.lampStopButton.Enabled = false;
            this.lampStopButton.FlatAppearance.BorderColor = System.Drawing.Color.Black;
            this.lampStopButton.FlatAppearance.BorderSize = 0;
            this.lampStopButton.IconSize = new System.Drawing.Size(32, 32);
            this.lampStopButton.ImageIndex = 0;
            this.lampStopButton.LampColor = Cell.UI.LampButton.LColor.Gray;
            this.lampStopButton.Location = new System.Drawing.Point(5, 54);
            this.lampStopButton.Margin = new System.Windows.Forms.Padding(2);
            this.lampStopButton.Name = "lampStopButton";
            this.lampStopButton.Size = new System.Drawing.Size(75, 23);
            this.lampStopButton.TabIndex = 6;
            this.lampStopButton.Text = "停止按钮";
            this.lampStopButton.UseVisualStyleBackColor = false;
            // 
            // lampPauseButton
            // 
            this.lampPauseButton.BackColor = System.Drawing.SystemColors.Control;
            this.lampPauseButton.Enabled = false;
            this.lampPauseButton.FlatAppearance.BorderColor = System.Drawing.Color.Black;
            this.lampPauseButton.FlatAppearance.BorderSize = 0;
            this.lampPauseButton.IconSize = new System.Drawing.Size(32, 32);
            this.lampPauseButton.ImageIndex = 0;
            this.lampPauseButton.LampColor = Cell.UI.LampButton.LColor.Gray;
            this.lampPauseButton.Location = new System.Drawing.Point(5, 36);
            this.lampPauseButton.Margin = new System.Windows.Forms.Padding(2);
            this.lampPauseButton.Name = "lampPauseButton";
            this.lampPauseButton.Size = new System.Drawing.Size(75, 23);
            this.lampPauseButton.TabIndex = 5;
            this.lampPauseButton.Text = "暂停按钮";
            this.lampPauseButton.UseVisualStyleBackColor = false;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.lampAlam);
            this.groupBox1.Controls.Add(this.lampGreen);
            this.groupBox1.Controls.Add(this.lampYellow);
            this.groupBox1.Controls.Add(this.lampRed);
            this.groupBox1.Location = new System.Drawing.Point(477, 2);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(2);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(2);
            this.groupBox1.Size = new System.Drawing.Size(411, 43);
            this.groupBox1.TabIndex = 61;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "设备信号灯";
            // 
            // lampAlam
            // 
            this.lampAlam.BackColor = System.Drawing.SystemColors.Control;
            this.lampAlam.Enabled = false;
            this.lampAlam.FlatAppearance.BorderColor = System.Drawing.Color.Black;
            this.lampAlam.FlatAppearance.BorderSize = 0;
            this.lampAlam.IconSize = new System.Drawing.Size(32, 32);
            this.lampAlam.ImageIndex = 0;
            this.lampAlam.LampColor = Cell.UI.LampButton.LColor.Gray;
            this.lampAlam.Location = new System.Drawing.Point(302, 14);
            this.lampAlam.Margin = new System.Windows.Forms.Padding(2);
            this.lampAlam.Name = "lampAlam";
            this.lampAlam.Size = new System.Drawing.Size(94, 23);
            this.lampAlam.TabIndex = 3;
            this.lampAlam.Text = "蜂鸣器";
            this.lampAlam.UseVisualStyleBackColor = false;
            // 
            // lampGreen
            // 
            this.lampGreen.BackColor = System.Drawing.SystemColors.Control;
            this.lampGreen.Enabled = false;
            this.lampGreen.FlatAppearance.BorderColor = System.Drawing.Color.Black;
            this.lampGreen.FlatAppearance.BorderSize = 0;
            this.lampGreen.IconSize = new System.Drawing.Size(32, 32);
            this.lampGreen.ImageIndex = 0;
            this.lampGreen.LampColor = Cell.UI.LampButton.LColor.Gray;
            this.lampGreen.Location = new System.Drawing.Point(204, 14);
            this.lampGreen.Margin = new System.Windows.Forms.Padding(2);
            this.lampGreen.Name = "lampGreen";
            this.lampGreen.Size = new System.Drawing.Size(94, 23);
            this.lampGreen.TabIndex = 2;
            this.lampGreen.Text = "绿灯";
            this.lampGreen.UseVisualStyleBackColor = false;
            // 
            // lampYellow
            // 
            this.lampYellow.BackColor = System.Drawing.SystemColors.Control;
            this.lampYellow.Enabled = false;
            this.lampYellow.FlatAppearance.BorderColor = System.Drawing.Color.Black;
            this.lampYellow.FlatAppearance.BorderSize = 0;
            this.lampYellow.IconSize = new System.Drawing.Size(32, 32);
            this.lampYellow.ImageIndex = 0;
            this.lampYellow.LampColor = Cell.UI.LampButton.LColor.Gray;
            this.lampYellow.Location = new System.Drawing.Point(106, 14);
            this.lampYellow.Margin = new System.Windows.Forms.Padding(2);
            this.lampYellow.Name = "lampYellow";
            this.lampYellow.Size = new System.Drawing.Size(94, 23);
            this.lampYellow.TabIndex = 1;
            this.lampYellow.Text = "黄灯";
            this.lampYellow.UseVisualStyleBackColor = false;
            // 
            // lampRed
            // 
            this.lampRed.BackColor = System.Drawing.SystemColors.Control;
            this.lampRed.Enabled = false;
            this.lampRed.FlatAppearance.BorderColor = System.Drawing.Color.Black;
            this.lampRed.FlatAppearance.BorderSize = 0;
            this.lampRed.IconSize = new System.Drawing.Size(32, 32);
            this.lampRed.ImageIndex = 0;
            this.lampRed.LampColor = Cell.UI.LampButton.LColor.Gray;
            this.lampRed.Location = new System.Drawing.Point(8, 14);
            this.lampRed.Margin = new System.Windows.Forms.Padding(2);
            this.lampRed.Name = "lampRed";
            this.lampRed.Size = new System.Drawing.Size(94, 23);
            this.lampRed.TabIndex = 0;
            this.lampRed.Text = "红灯";
            this.lampRed.UseVisualStyleBackColor = false;
            // 
            // lampEMGLight
            // 
            this.lampEMGLight.BackColor = System.Drawing.SystemColors.Control;
            this.lampEMGLight.Enabled = false;
            this.lampEMGLight.FlatAppearance.BorderColor = System.Drawing.Color.Black;
            this.lampEMGLight.FlatAppearance.BorderSize = 0;
            this.lampEMGLight.IconSize = new System.Drawing.Size(32, 32);
            this.lampEMGLight.ImageIndex = 0;
            this.lampEMGLight.LampColor = Cell.UI.LampButton.LColor.Gray;
            this.lampEMGLight.Location = new System.Drawing.Point(202, 17);
            this.lampEMGLight.Margin = new System.Windows.Forms.Padding(2);
            this.lampEMGLight.Name = "lampEMGLight";
            this.lampEMGLight.Size = new System.Drawing.Size(38, 23);
            this.lampEMGLight.TabIndex = 17;
            this.lampEMGLight.Text = "灯";
            this.lampEMGLight.UseVisualStyleBackColor = false;
            // 
            // lampEMGButton
            // 
            this.lampEMGButton.BackColor = System.Drawing.SystemColors.Control;
            this.lampEMGButton.Enabled = false;
            this.lampEMGButton.FlatAppearance.BorderColor = System.Drawing.Color.Black;
            this.lampEMGButton.FlatAppearance.BorderSize = 0;
            this.lampEMGButton.IconSize = new System.Drawing.Size(32, 32);
            this.lampEMGButton.ImageIndex = 0;
            this.lampEMGButton.LampColor = Cell.UI.LampButton.LColor.Gray;
            this.lampEMGButton.Location = new System.Drawing.Point(124, 17);
            this.lampEMGButton.Margin = new System.Windows.Forms.Padding(2);
            this.lampEMGButton.Name = "lampEMGButton";
            this.lampEMGButton.Size = new System.Drawing.Size(73, 23);
            this.lampEMGButton.TabIndex = 15;
            this.lampEMGButton.Text = "急停按钮";
            this.lampEMGButton.UseVisualStyleBackColor = false;
            // 
            // UcMainStationBasePanel
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.Controls.Add(this.panelDio);
            this.Controls.Add(this.panelStatus);
            this.Controls.Add(this.btPackup);
            this.Controls.Add(this.pnCumstom);
            this.Name = "UcMainStationBasePanel";
            this.Size = new System.Drawing.Size(1100, 577);
            this.Load += new System.EventHandler(this.UcMainStationPanelBase_Load);
            this.VisibleChanged += new System.EventHandler(this.UcMainStationPanelBase_VisibleChanged);
            this.panelStatus.ResumeLayout(false);
            this.panelDio.ResumeLayout(false);
            this.gbDeclearedIO.ResumeLayout(false);
            this.splitContainer2.Panel1.ResumeLayout(false);
            this.splitContainer2.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).EndInit();
            this.splitContainer2.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private System.ComponentModel.BackgroundWorker backgroundWorker1;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.Button btPackup;
        private System.Windows.Forms.Panel pnCumstom;
        private System.Windows.Forms.Panel panelStatus;
        private LampButton lampWarning;
        private LampButton lampWorkStatus;
        private System.Windows.Forms.Panel panelDio;
        private System.Windows.Forms.GroupBox gbDeclearedIO;
        private System.Windows.Forms.SplitContainer splitContainer2;
        private System.Windows.Forms.GroupBox groupBox2;
        private LampButton lampStartLight;
        private LampButton lampStopLight;
        private LampButton lampPauseLight;
        private LampButton lampStartButton;
        private LampButton lampStopButton;
        private LampButton lampPauseButton;
        private System.Windows.Forms.GroupBox groupBox1;
        private LampButton lampAlam;
        private LampButton lampGreen;
        private LampButton lampYellow;
        private LampButton lampRed;
        private LampButton lampResetLight;
        private LampButton lampResetButton;
        private System.Windows.Forms.FlowLayoutPanel tbPanelDIs;
        private System.Windows.Forms.FlowLayoutPanel tbPanelDOs;
        private LampButton lampEMGLight;
        private LampButton lampEMGButton;
    }
}
