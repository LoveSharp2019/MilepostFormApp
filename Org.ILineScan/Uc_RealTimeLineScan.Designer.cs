
namespace Org.ILineScan
{
    partial class Uc_RealTimeLineScan
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
            this.picBox = new System.Windows.Forms.PictureBox();
            this.rbTips = new System.Windows.Forms.RichTextBox();
            this.btClearTips = new System.Windows.Forms.Button();
            this.btDev = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.cbImgDispMode = new System.Windows.Forms.ComboBox();
            this.btGrab = new System.Windows.Forms.Button();
            this.btGrabOne = new System.Windows.Forms.Button();
            this.btSave = new System.Windows.Forms.Button();
            this.cbImgFileFormat = new System.Windows.Forms.ComboBox();
            this.label6 = new System.Windows.Forms.Label();
            this.gbParam = new System.Windows.Forms.GroupBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btn_getJob = new System.Windows.Forms.Button();
            this.btn_setJob = new System.Windows.Forms.Button();
            this.txt_jobname = new Cell.UI.UcTextBoxPop();
            ((System.ComponentModel.ISupportInitialize)(this.picBox)).BeginInit();
            this.gbParam.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // picBox
            // 
            this.picBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.picBox.BackColor = System.Drawing.SystemColors.ControlDark;
            this.picBox.Location = new System.Drawing.Point(0, 0);
            this.picBox.Name = "picBox";
            this.picBox.Size = new System.Drawing.Size(640, 475);
            this.picBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.picBox.TabIndex = 0;
            this.picBox.TabStop = false;
            this.picBox.SizeChanged += new System.EventHandler(this.picBox_SizeChanged);
            this.picBox.Paint += new System.Windows.Forms.PaintEventHandler(this.picBox_Paint);
            // 
            // rbTips
            // 
            this.rbTips.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.rbTips.BackColor = System.Drawing.SystemColors.Control;
            this.rbTips.Location = new System.Drawing.Point(0, 478);
            this.rbTips.Name = "rbTips";
            this.rbTips.ReadOnly = true;
            this.rbTips.Size = new System.Drawing.Size(830, 47);
            this.rbTips.TabIndex = 1;
            this.rbTips.Text = "";
            // 
            // btClearTips
            // 
            this.btClearTips.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btClearTips.Location = new System.Drawing.Point(756, 452);
            this.btClearTips.Name = "btClearTips";
            this.btClearTips.Size = new System.Drawing.Size(74, 23);
            this.btClearTips.TabIndex = 2;
            this.btClearTips.Text = "清空信息";
            this.btClearTips.UseVisualStyleBackColor = true;
            this.btClearTips.Click += new System.EventHandler(this.btClearTips_Click);
            // 
            // btDev
            // 
            this.btDev.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btDev.Location = new System.Drawing.Point(646, 3);
            this.btDev.Name = "btDev";
            this.btDev.Size = new System.Drawing.Size(63, 23);
            this.btDev.TabIndex = 3;
            this.btDev.Text = "打开相机";
            this.btDev.UseVisualStyleBackColor = true;
            this.btDev.Click += new System.EventHandler(this.btDev_Click);
            // 
            // label2
            // 
            this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(714, 8);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(53, 12);
            this.label2.TabIndex = 6;
            this.label2.Text = "显示模式";
            // 
            // cbImgDispMode
            // 
            this.cbImgDispMode.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.cbImgDispMode.FormattingEnabled = true;
            this.cbImgDispMode.Items.AddRange(new object[] {
            "sdk",
            "halcon",
            "bitmap"});
            this.cbImgDispMode.Location = new System.Drawing.Point(766, 4);
            this.cbImgDispMode.Name = "cbImgDispMode";
            this.cbImgDispMode.Size = new System.Drawing.Size(65, 20);
            this.cbImgDispMode.TabIndex = 7;
            // 
            // btGrab
            // 
            this.btGrab.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btGrab.Location = new System.Drawing.Point(4, 20);
            this.btGrab.Name = "btGrab";
            this.btGrab.Size = new System.Drawing.Size(86, 23);
            this.btGrab.TabIndex = 8;
            this.btGrab.Text = "开始采集";
            this.btGrab.UseVisualStyleBackColor = true;
            this.btGrab.Click += new System.EventHandler(this.btGrab_Click);
            // 
            // btGrabOne
            // 
            this.btGrabOne.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btGrabOne.Location = new System.Drawing.Point(5, 55);
            this.btGrabOne.Name = "btGrabOne";
            this.btGrabOne.Size = new System.Drawing.Size(88, 23);
            this.btGrabOne.TabIndex = 9;
            this.btGrabOne.Text = "获取图片";
            this.btGrabOne.UseVisualStyleBackColor = true;
            this.btGrabOne.Click += new System.EventHandler(this.btGrabOne_Click);
            // 
            // btSave
            // 
            this.btSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btSave.Location = new System.Drawing.Point(791, 297);
            this.btSave.Name = "btSave";
            this.btSave.Size = new System.Drawing.Size(37, 23);
            this.btSave.TabIndex = 13;
            this.btSave.Text = "保存";
            this.btSave.UseVisualStyleBackColor = true;
            this.btSave.Click += new System.EventHandler(this.btSaveImage_Click);
            // 
            // cbImgFileFormat
            // 
            this.cbImgFileFormat.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.cbImgFileFormat.FormattingEnabled = true;
            this.cbImgFileFormat.Items.AddRange(new object[] {
            "Bmp",
            "Jpg",
            "Png",
            "Tif"});
            this.cbImgFileFormat.Location = new System.Drawing.Point(701, 299);
            this.cbImgFileFormat.Name = "cbImgFileFormat";
            this.cbImgFileFormat.Size = new System.Drawing.Size(84, 20);
            this.cbImgFileFormat.TabIndex = 12;
            // 
            // label6
            // 
            this.label6.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(646, 303);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(53, 12);
            this.label6.TabIndex = 12;
            this.label6.Text = "图片格式";
            // 
            // gbParam
            // 
            this.gbParam.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.gbParam.Controls.Add(this.txt_jobname);
            this.gbParam.Controls.Add(this.btn_setJob);
            this.gbParam.Controls.Add(this.btn_getJob);
            this.gbParam.Location = new System.Drawing.Point(646, 45);
            this.gbParam.Name = "gbParam";
            this.gbParam.Size = new System.Drawing.Size(182, 141);
            this.gbParam.TabIndex = 12;
            this.gbParam.TabStop = false;
            this.gbParam.Text = "相机参数";
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.btGrab);
            this.groupBox1.Controls.Add(this.btGrabOne);
            this.groupBox1.Location = new System.Drawing.Point(646, 201);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(182, 84);
            this.groupBox1.TabIndex = 15;
            this.groupBox1.TabStop = false;
            // 
            // btn_getJob
            // 
            this.btn_getJob.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btn_getJob.Location = new System.Drawing.Point(7, 39);
            this.btn_getJob.Name = "btn_getJob";
            this.btn_getJob.Size = new System.Drawing.Size(71, 23);
            this.btn_getJob.TabIndex = 10;
            this.btn_getJob.Text = "获取Job";
            this.btn_getJob.UseVisualStyleBackColor = true;
            this.btn_getJob.Click += new System.EventHandler(this.btn_getJob_Click);
            // 
            // btn_setJob
            // 
            this.btn_setJob.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btn_setJob.Location = new System.Drawing.Point(5, 79);
            this.btn_setJob.Name = "btn_setJob";
            this.btn_setJob.Size = new System.Drawing.Size(73, 23);
            this.btn_setJob.TabIndex = 11;
            this.btn_setJob.Text = "设置Job";
            this.btn_setJob.UseVisualStyleBackColor = true;
            this.btn_setJob.Click += new System.EventHandler(this.btn_setJob_Click);
            // 
            // txt_jobname
            // 
            this.txt_jobname.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txt_jobname.EmptyTextTip = null;
            this.txt_jobname.EmptyTextTipColor = System.Drawing.Color.DarkGray;
            this.txt_jobname.Font = new System.Drawing.Font("微软雅黑", 9F);
            this.txt_jobname.Location = new System.Drawing.Point(79, 79);
            this.txt_jobname.Name = "txt_jobname";
            this.txt_jobname.Size = new System.Drawing.Size(99, 23);
            this.txt_jobname.TabIndex = 12;
            // 
            // Uc_RealTimeLineScan
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.cbImgFileFormat);
            this.Controls.Add(this.btSave);
            this.Controls.Add(this.gbParam);
            this.Controls.Add(this.cbImgDispMode);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.btDev);
            this.Controls.Add(this.btClearTips);
            this.Controls.Add(this.rbTips);
            this.Controls.Add(this.picBox);
            this.Name = "Uc_RealTimeLineScan";
            this.Size = new System.Drawing.Size(833, 528);
            this.Load += new System.EventHandler(this.UcCmr_Load);
            this.SizeChanged += new System.EventHandler(this.UcCmr_SizeChanged);
            this.VisibleChanged += new System.EventHandler(this.UcCmr_VisibleChanged);
            ((System.ComponentModel.ISupportInitialize)(this.picBox)).EndInit();
            this.gbParam.ResumeLayout(false);
            this.gbParam.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox picBox;
        private System.Windows.Forms.RichTextBox rbTips;
        private System.Windows.Forms.Button btClearTips;
        private System.Windows.Forms.Button btDev;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox cbImgDispMode;
        private System.Windows.Forms.Button btGrab;
        private System.Windows.Forms.Button btGrabOne;
        private System.Windows.Forms.Button btSave;
        private System.Windows.Forms.ComboBox cbImgFileFormat;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.GroupBox gbParam;
        private System.Windows.Forms.GroupBox groupBox1;
        private Cell.UI.UcTextBoxPop txt_jobname;
        private System.Windows.Forms.Button btn_setJob;
        private System.Windows.Forms.Button btn_getJob;
    }
}
