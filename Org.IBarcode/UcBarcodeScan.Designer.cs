
using Cell.UI;
using Tissue.UI;

namespace Org.IBarcode
{
    partial class UcBarcodeScan
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
            this.chkWorkMode = new System.Windows.Forms.CheckBox();
            this.chkContine = new System.Windows.Forms.CheckBox();
            this.label2 = new System.Windows.Forms.Label();
            this.numInterval = new System.Windows.Forms.NumericUpDown();
            this.label3 = new System.Windows.Forms.Label();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.tbBarcode = new UcTextBoxPop();
            this.btScan = new IconBtn();
            this.ucScrollTips1 = new UcRichTextScrollTips();
            this.btOpenCloseDev = new IconBtn();
            ((System.ComponentModel.ISupportInitialize)(this.numInterval)).BeginInit();
            this.SuspendLayout();
            // 
            // chkWorkMode
            // 
            this.chkWorkMode.AutoSize = true;
            this.chkWorkMode.Location = new System.Drawing.Point(85, 8);
            this.chkWorkMode.Name = "chkWorkMode";
            this.chkWorkMode.Size = new System.Drawing.Size(72, 16);
            this.chkWorkMode.TabIndex = 3;
            this.chkWorkMode.Text = "主动模式";
            this.chkWorkMode.UseVisualStyleBackColor = true;
            this.chkWorkMode.CheckedChanged += new System.EventHandler(this.chkWorkMode_CheckedChanged);
            // 
            // chkContine
            // 
            this.chkContine.AutoSize = true;
            this.chkContine.Location = new System.Drawing.Point(163, 8);
            this.chkContine.Name = "chkContine";
            this.chkContine.Size = new System.Drawing.Size(72, 16);
            this.chkContine.TabIndex = 6;
            this.chkContine.Text = "连续扫码";
            this.chkContine.UseVisualStyleBackColor = true;
            this.chkContine.CheckedChanged += new System.EventHandler(this.chkContine_CheckedChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(250, 9);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(29, 12);
            this.label2.TabIndex = 7;
            this.label2.Text = "间隔";
            // 
            // numInterval
            // 
            this.numInterval.Location = new System.Drawing.Point(280, 5);
            this.numInterval.Maximum = new decimal(new int[] {
            100000,
            0,
            0,
            0});
            this.numInterval.Name = "numInterval";
            this.numInterval.Size = new System.Drawing.Size(66, 21);
            this.numInterval.TabIndex = 8;
            this.numInterval.Value = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(352, 10);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(29, 12);
            this.label3.TabIndex = 9;
            this.label3.Text = "毫秒";
            // 
            // timer1
            // 
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // tbBarcode
            // 
            this.tbBarcode.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tbBarcode.EmptyTextTip = null;
            this.tbBarcode.EmptyTextTipColor = System.Drawing.Color.DarkGray;
            this.tbBarcode.Font = new System.Drawing.Font("微软雅黑", 9F);
            this.tbBarcode.Location = new System.Drawing.Point(85, 34);
            this.tbBarcode.Name = "tbBarcode";
            this.tbBarcode.ReadOnly = true;
            this.tbBarcode.Size = new System.Drawing.Size(293, 23);
            this.tbBarcode.TabIndex = 4;
            // 
            // btScan
            // 
            this.btScan.BackColor = System.Drawing.Color.Gray;
            this.btScan.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btScan.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btScan.Font = new System.Drawing.Font("宋体", 9F);
            this.btScan.ForeColor = System.Drawing.Color.White;
            this.btScan.IconBackColor = System.Drawing.Color.White;
            this.btScan.IconForeColor = System.Drawing.Color.Black;
            this.btScan.IconSize = 32;
            this.btScan.IconStyle = Cell.IconFont.FontIcons.None;
            this.btScan.Location = new System.Drawing.Point(4, 34);
            this.btScan.Name = "btScan";
            this.btScan.Size = new System.Drawing.Size(75, 23);
            this.btScan.TabIndex = 2;
            this.btScan.Text = "扫码";
            this.btScan.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btScan.UseVisualStyleBackColor = true;
            this.btScan.Click += new System.EventHandler(this.btScan_Click);
            // 
            // ucScrollTips1
            // 
            this.ucScrollTips1.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.ucScrollTips1.IsAppendTimeInfo = true;
            this.ucScrollTips1.IsAutoScrollLast = true;
            this.ucScrollTips1.Location = new System.Drawing.Point(4, 62);
            this.ucScrollTips1.Margin = new System.Windows.Forms.Padding(2);
            this.ucScrollTips1.MaxTipsCount = 1000;
            this.ucScrollTips1.Name = "ucScrollTips1";
            this.ucScrollTips1.Size = new System.Drawing.Size(377, 138);
            this.ucScrollTips1.TabIndex = 1;
            this.ucScrollTips1.VisibleChanged += new System.EventHandler(this.ucScrollTips1_VisibleChanged);
            // 
            // btOpenCloseDev
            // 
            this.btOpenCloseDev.BackColor = System.Drawing.Color.Gray;
            this.btOpenCloseDev.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btOpenCloseDev.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btOpenCloseDev.Font = new System.Drawing.Font("宋体", 9F);
            this.btOpenCloseDev.ForeColor = System.Drawing.Color.White;
            this.btOpenCloseDev.IconBackColor = System.Drawing.Color.White;
            this.btOpenCloseDev.IconForeColor = System.Drawing.Color.Black;
            this.btOpenCloseDev.IconSize = 32;
            this.btOpenCloseDev.IconStyle = Cell.IconFont.FontIcons.None;
            this.btOpenCloseDev.Location = new System.Drawing.Point(4, 4);
            this.btOpenCloseDev.Name = "btOpenCloseDev";
            this.btOpenCloseDev.Size = new System.Drawing.Size(75, 23);
            this.btOpenCloseDev.TabIndex = 0;
            this.btOpenCloseDev.Text = "打开设备";
            this.btOpenCloseDev.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btOpenCloseDev.UseVisualStyleBackColor = true;
            this.btOpenCloseDev.Click += new System.EventHandler(this.btOpenCloseDev_Click);
            // 
            // UcBarcodeScan
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Silver;
            this.Controls.Add(this.label3);
            this.Controls.Add(this.numInterval);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.chkContine);
            this.Controls.Add(this.tbBarcode);
            this.Controls.Add(this.chkWorkMode);
            this.Controls.Add(this.btScan);
            this.Controls.Add(this.ucScrollTips1);
            this.Controls.Add(this.btOpenCloseDev);
            this.Name = "UcBarcodeScan";
            this.Size = new System.Drawing.Size(385, 205);
            this.Load += new System.EventHandler(this.UcBarcodeScan_Load);
            ((System.ComponentModel.ISupportInitialize)(this.numInterval)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private IconBtn btOpenCloseDev;
        private UcRichTextScrollTips ucScrollTips1;
        private IconBtn btScan;
        private System.Windows.Forms.CheckBox chkWorkMode;
        private UcTextBoxPop tbBarcode;
        private System.Windows.Forms.CheckBox chkContine;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.NumericUpDown numInterval;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Timer timer1;
    }
}
