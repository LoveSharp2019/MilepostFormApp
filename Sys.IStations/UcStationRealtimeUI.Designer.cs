
using Cell.IconFont;
using Cell.UI;
using Tissue.UI;

namespace Sys.IStations
{
    partial class UcStationRealtimeUI
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
            this.gbStationName = new Cell.UI.UcGroupBox();
            this.tbPFCount = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.tbStartTime = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.cbCmds = new System.Windows.Forms.ComboBox();
            this.btSendCmd = new Cell.UI.IconBtn();
            this.btResetProductInfo = new Cell.UI.IconBtn();
            this.tbRunTime = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.tbNGCount = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.tbPassCount = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.lstBoxCustomStatus = new System.Windows.Forms.ListBox();
            this.lampWorkStatus = new Cell.UI.LampButton();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.ucScrollTips1 = new Tissue.UI.UcRichTextScrollTips();
            this.gbStationName.SuspendLayout();
            this.SuspendLayout();
            // 
            // gbStationName
            // 
            this.gbStationName.BorderColor = System.Drawing.Color.Silver;
            this.gbStationName.BorderSize = 1;
            this.gbStationName.BorderStyle = System.Windows.Forms.ButtonBorderStyle.Solid;
            this.gbStationName.Controls.Add(this.tbPFCount);
            this.gbStationName.Controls.Add(this.label6);
            this.gbStationName.Controls.Add(this.tbStartTime);
            this.gbStationName.Controls.Add(this.label5);
            this.gbStationName.Controls.Add(this.cbCmds);
            this.gbStationName.Controls.Add(this.btSendCmd);
            this.gbStationName.Controls.Add(this.btResetProductInfo);
            this.gbStationName.Controls.Add(this.tbRunTime);
            this.gbStationName.Controls.Add(this.label4);
            this.gbStationName.Controls.Add(this.tbNGCount);
            this.gbStationName.Controls.Add(this.label3);
            this.gbStationName.Controls.Add(this.tbPassCount);
            this.gbStationName.Controls.Add(this.label1);
            this.gbStationName.Controls.Add(this.label2);
            this.gbStationName.Controls.Add(this.lstBoxCustomStatus);
            this.gbStationName.Controls.Add(this.lampWorkStatus);
            this.gbStationName.Dock = System.Windows.Forms.DockStyle.Top;
            this.gbStationName.FColor = System.Drawing.Color.White;
            this.gbStationName.ForeColor = System.Drawing.SystemColors.ControlText;
            this.gbStationName.Location = new System.Drawing.Point(0, 0);
            this.gbStationName.Name = "gbStationName";
            this.gbStationName.Size = new System.Drawing.Size(372, 147);
            this.gbStationName.TabIndex = 0;
            this.gbStationName.TabStop = false;
            this.gbStationName.TColor = System.Drawing.Color.White;
            this.gbStationName.Text = "工站:未设置";
            this.gbStationName.TitleAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.gbStationName.TitleBackGroundCor = System.Drawing.Color.DeepSkyBlue;
            this.gbStationName.TitleFont = new System.Drawing.Font("微软雅黑", 12F);
            // 
            // tbPFCount
            // 
            this.tbPFCount.Location = new System.Drawing.Point(57, 96);
            this.tbPFCount.Name = "tbPFCount";
            this.tbPFCount.ReadOnly = true;
            this.tbPFCount.Size = new System.Drawing.Size(56, 21);
            this.tbPFCount.TabIndex = 19;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(4, 100);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(53, 12);
            this.label6.TabIndex = 18;
            this.label6.Text = "统计次数";
            // 
            // tbStartTime
            // 
            this.tbStartTime.Location = new System.Drawing.Point(57, 72);
            this.tbStartTime.Name = "tbStartTime";
            this.tbStartTime.ReadOnly = true;
            this.tbStartTime.Size = new System.Drawing.Size(125, 21);
            this.tbStartTime.TabIndex = 17;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(4, 76);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(53, 12);
            this.label5.TabIndex = 16;
            this.label5.Text = "开始时间";
            // 
            // cbCmds
            // 
            this.cbCmds.FormattingEnabled = true;
            this.cbCmds.Location = new System.Drawing.Point(71, 119);
            this.cbCmds.Name = "cbCmds";
            this.cbCmds.Size = new System.Drawing.Size(163, 20);
            this.cbCmds.TabIndex = 13;
            // 
            // btSendCmd
            // 
            this.btSendCmd.BackColor = System.Drawing.Color.Gray;
            this.btSendCmd.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btSendCmd.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btSendCmd.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btSendCmd.ForeColor = System.Drawing.Color.White;
            this.btSendCmd.IconBackColor = System.Drawing.Color.White;
            this.btSendCmd.IconForeColor = System.Drawing.Color.Black;
            this.btSendCmd.IconSize = 32;
            this.btSendCmd.IconStyle = Cell.IconFont.FontIcons.None;
            this.btSendCmd.Location = new System.Drawing.Point(2, 118);
            this.btSendCmd.Name = "btSendCmd";
            this.btSendCmd.Size = new System.Drawing.Size(66, 23);
            this.btSendCmd.TabIndex = 12;
            this.btSendCmd.Text = "发送指令";
            this.btSendCmd.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btSendCmd.UseVisualStyleBackColor = true;
            this.btSendCmd.Click += new System.EventHandler(this.btSendCmd_Click);
            // 
            // btResetProductInfo
            // 
            this.btResetProductInfo.BackColor = System.Drawing.Color.Gray;
            this.btResetProductInfo.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btResetProductInfo.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btResetProductInfo.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btResetProductInfo.ForeColor = System.Drawing.Color.White;
            this.btResetProductInfo.IconBackColor = System.Drawing.Color.White;
            this.btResetProductInfo.IconForeColor = System.Drawing.Color.Black;
            this.btResetProductInfo.IconSize = 32;
            this.btResetProductInfo.IconStyle = Cell.IconFont.FontIcons.None;
            this.btResetProductInfo.Location = new System.Drawing.Point(318, 93);
            this.btResetProductInfo.Name = "btResetProductInfo";
            this.btResetProductInfo.Size = new System.Drawing.Size(47, 23);
            this.btResetProductInfo.TabIndex = 11;
            this.btResetProductInfo.Text = "重置";
            this.btResetProductInfo.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btResetProductInfo.UseVisualStyleBackColor = true;
            this.btResetProductInfo.Click += new System.EventHandler(this.btResetProductInfo_Click);
            // 
            // tbRunTime
            // 
            this.tbRunTime.Location = new System.Drawing.Point(238, 72);
            this.tbRunTime.Name = "tbRunTime";
            this.tbRunTime.ReadOnly = true;
            this.tbRunTime.Size = new System.Drawing.Size(126, 21);
            this.tbRunTime.TabIndex = 10;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(183, 77);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(53, 12);
            this.label4.TabIndex = 9;
            this.label4.Text = "运行时长";
            // 
            // tbNGCount
            // 
            this.tbNGCount.Location = new System.Drawing.Point(238, 95);
            this.tbNGCount.Name = "tbNGCount";
            this.tbNGCount.ReadOnly = true;
            this.tbNGCount.Size = new System.Drawing.Size(75, 21);
            this.tbNGCount.TabIndex = 8;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(218, 99);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(17, 12);
            this.label3.TabIndex = 7;
            this.label3.Text = "NG";
            // 
            // tbPassCount
            // 
            this.tbPassCount.Location = new System.Drawing.Point(141, 95);
            this.tbPassCount.Name = "tbPassCount";
            this.tbPassCount.ReadOnly = true;
            this.tbPassCount.Size = new System.Drawing.Size(75, 21);
            this.tbPassCount.TabIndex = 6;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(114, 101);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(29, 12);
            this.label1.TabIndex = 5;
            this.label1.Text = "PASS";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(13, 39);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(0, 12);
            this.label2.TabIndex = 4;
            // 
            // lstBoxCustomStatus
            // 
            this.lstBoxCustomStatus.BackColor = System.Drawing.SystemColors.InactiveCaption;
            this.lstBoxCustomStatus.FormattingEnabled = true;
            this.lstBoxCustomStatus.ItemHeight = 12;
            this.lstBoxCustomStatus.Items.AddRange(new object[] {
            "1",
            "2",
            "3",
            "4",
            "5",
            "6",
            "7",
            "8",
            "9",
            "0"});
            this.lstBoxCustomStatus.Location = new System.Drawing.Point(155, 27);
            this.lstBoxCustomStatus.Name = "lstBoxCustomStatus";
            this.lstBoxCustomStatus.Size = new System.Drawing.Size(209, 40);
            this.lstBoxCustomStatus.TabIndex = 2;
            // 
            // lampWorkStatus
            // 
            this.lampWorkStatus.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.lampWorkStatus.Enabled = false;
            this.lampWorkStatus.FlatAppearance.BorderColor = System.Drawing.Color.Black;
            this.lampWorkStatus.FlatAppearance.BorderSize = 0;
            this.lampWorkStatus.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lampWorkStatus.IconSize = new System.Drawing.Size(32, 32);
            this.lampWorkStatus.ImageIndex = 0;
            this.lampWorkStatus.LampColor = Cell.UI.LampButton.LColor.Gray;
            this.lampWorkStatus.Location = new System.Drawing.Point(5, 27);
            this.lampWorkStatus.Name = "lampWorkStatus";
            this.lampWorkStatus.Size = new System.Drawing.Size(147, 40);
            this.lampWorkStatus.TabIndex = 0;
            this.lampWorkStatus.Text = "未开始";
            this.lampWorkStatus.UseVisualStyleBackColor = false;
            // 
            // timer1
            // 
            this.timer1.Interval = 1000;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // ucScrollTips1
            // 
            this.ucScrollTips1.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.ucScrollTips1.Dock = System.Windows.Forms.DockStyle.Top;
            this.ucScrollTips1.IsAppendTimeInfo = true;
            this.ucScrollTips1.IsAutoScrollLast = true;
            this.ucScrollTips1.Location = new System.Drawing.Point(0, 147);
            this.ucScrollTips1.Margin = new System.Windows.Forms.Padding(2);
            this.ucScrollTips1.MaxTipsCount = 1000;
            this.ucScrollTips1.Name = "ucScrollTips1";
            this.ucScrollTips1.Size = new System.Drawing.Size(372, 101);
            this.ucScrollTips1.TabIndex = 2;
            // 
            // UcStationRealtimeUI
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this.ucScrollTips1);
            this.Controls.Add(this.gbStationName);
            this.Name = "UcStationRealtimeUI";
            this.Size = new System.Drawing.Size(372, 247);
            this.Load += new System.EventHandler(this.UcStationBaseRealtimeUI_Load);
            this.gbStationName.ResumeLayout(false);
            this.gbStationName.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private UcGroupBox gbStationName;
        private System.Windows.Forms.ComboBox cbCmds;
        private IconBtn btSendCmd;
        private IconBtn btResetProductInfo;
        private System.Windows.Forms.TextBox tbRunTime;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox tbNGCount;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox tbPassCount;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ListBox lstBoxCustomStatus;
        private LampButton lampWorkStatus;
        private System.Windows.Forms.TextBox tbPFCount;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox tbStartTime;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Timer timer1;
        private UcRichTextScrollTips ucScrollTips1;
    }
}
