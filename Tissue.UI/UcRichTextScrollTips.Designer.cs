
using Cell.UI;
using Tissue.UI;

namespace Tissue.UI
{
    partial class UcRichTextScrollTips
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
            this.rchTips = new System.Windows.Forms.RichTextBox();
            this.btClearTips = new IconBtn();
            this.label1 = new System.Windows.Forms.Label();
            this.numTipsCount = new System.Windows.Forms.NumericUpDown();
            this.chkTimeFlag = new System.Windows.Forms.CheckBox();
            this.chkScrollLast = new System.Windows.Forms.CheckBox();
            this.btCfg = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.numTipsCount)).BeginInit();
            this.SuspendLayout();
            // 
            // rchTips
            // 
            this.rchTips.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.rchTips.BackColor = System.Drawing.Color.White;
            this.rchTips.Location = new System.Drawing.Point(1, 23);
            this.rchTips.Margin = new System.Windows.Forms.Padding(2);
            this.rchTips.Name = "rchTips";
            this.rchTips.ReadOnly = true;
            this.rchTips.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.Vertical;
            this.rchTips.Size = new System.Drawing.Size(305, 61);
            this.rchTips.TabIndex = 0;
            this.rchTips.Text = "";
            // 
            // btClearTips
            // 
            this.btClearTips.BackColor = System.Drawing.Color.Gray;
            this.btClearTips.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btClearTips.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btClearTips.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btClearTips.ForeColor = System.Drawing.Color.White;
            this.btClearTips.IconBackColor = System.Drawing.Color.White;
            this.btClearTips.IconForeColor = System.Drawing.Color.Black;
            this.btClearTips.IconSize = 32;
            this.btClearTips.IconStyle = Cell.IconFont.FontIcons.None;
            this.btClearTips.Location = new System.Drawing.Point(1, 0);
            this.btClearTips.Margin = new System.Windows.Forms.Padding(2);
            this.btClearTips.Name = "btClearTips";
            this.btClearTips.Size = new System.Drawing.Size(40, 23);
            this.btClearTips.TabIndex = 1;
            this.btClearTips.Text = "清空";
            this.btClearTips.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btClearTips.UseVisualStyleBackColor = true;
            this.btClearTips.Click += new System.EventHandler(this.btClearTips_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(68, 6);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(29, 12);
            this.label1.TabIndex = 2;
            this.label1.Text = "数量";
            this.label1.Visible = false;
            // 
            // numTipsCount
            // 
            this.numTipsCount.Location = new System.Drawing.Point(101, 2);
            this.numTipsCount.Margin = new System.Windows.Forms.Padding(2);
            this.numTipsCount.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.numTipsCount.Minimum = new decimal(new int[] {
            2,
            0,
            0,
            0});
            this.numTipsCount.Name = "numTipsCount";
            this.numTipsCount.Size = new System.Drawing.Size(60, 21);
            this.numTipsCount.TabIndex = 3;
            this.numTipsCount.Value = new decimal(new int[] {
            100,
            0,
            0,
            0});
            this.numTipsCount.Visible = false;
            this.numTipsCount.ValueChanged += new System.EventHandler(this.numTipsCount_ValueChanged);
            // 
            // chkTimeFlag
            // 
            this.chkTimeFlag.AutoSize = true;
            this.chkTimeFlag.Checked = true;
            this.chkTimeFlag.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkTimeFlag.Location = new System.Drawing.Point(164, 4);
            this.chkTimeFlag.Margin = new System.Windows.Forms.Padding(2);
            this.chkTimeFlag.Name = "chkTimeFlag";
            this.chkTimeFlag.Size = new System.Drawing.Size(48, 16);
            this.chkTimeFlag.TabIndex = 4;
            this.chkTimeFlag.Text = "时间";
            this.chkTimeFlag.UseVisualStyleBackColor = true;
            this.chkTimeFlag.Visible = false;
            // 
            // chkScrollLast
            // 
            this.chkScrollLast.AutoSize = true;
            this.chkScrollLast.Checked = true;
            this.chkScrollLast.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkScrollLast.Location = new System.Drawing.Point(214, 4);
            this.chkScrollLast.Margin = new System.Windows.Forms.Padding(2);
            this.chkScrollLast.Name = "chkScrollLast";
            this.chkScrollLast.Size = new System.Drawing.Size(48, 16);
            this.chkScrollLast.TabIndex = 5;
            this.chkScrollLast.Text = "滚动";
            this.chkScrollLast.UseVisualStyleBackColor = true;
            this.chkScrollLast.Visible = false;
            this.chkScrollLast.CheckedChanged += new System.EventHandler(this.chkScrollLast_CheckedChanged);
            // 
            // btCfg
            // 
            this.btCfg.ImageIndex = 0;
            this.btCfg.Location = new System.Drawing.Point(43, 1);
            this.btCfg.Margin = new System.Windows.Forms.Padding(2);
            this.btCfg.Name = "btCfg";
            this.btCfg.Size = new System.Drawing.Size(21, 21);
            this.btCfg.TabIndex = 6;
            this.btCfg.UseVisualStyleBackColor = true;
            this.btCfg.Click += new System.EventHandler(this.btCfg_Click);
            // 
            // UcRichTextScrollTips
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.Controls.Add(this.btCfg);
            this.Controls.Add(this.chkScrollLast);
            this.Controls.Add(this.chkTimeFlag);
            this.Controls.Add(this.numTipsCount);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btClearTips);
            this.Controls.Add(this.rchTips);
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "UcRichTextScrollTips";
            this.Size = new System.Drawing.Size(308, 86);
            this.Load += new System.EventHandler(this.UcScrollTips_Load);
            ((System.ComponentModel.ISupportInitialize)(this.numTipsCount)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.RichTextBox rchTips;
        private IconBtn btClearTips;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.NumericUpDown numTipsCount;
        private System.Windows.Forms.CheckBox chkTimeFlag;
        private System.Windows.Forms.CheckBox chkScrollLast;
        private System.Windows.Forms.Button btCfg;
    }
}
