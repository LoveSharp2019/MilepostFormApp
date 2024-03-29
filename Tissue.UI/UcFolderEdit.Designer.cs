
using Cell.UI;
using Tissue.UI;

namespace Tissue.UI
{
    partial class UcFolderEdit
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(UcFolderEdit));
            this.iconBtn1 = new Cell.UI.IconBtn();
            this.ucTextBoxPop1 = new Cell.UI.UcTextBoxPop();
            this.SuspendLayout();
            // 
            // iconBtn1
            // 
            this.iconBtn1.BackColor = System.Drawing.Color.Gray;
            this.iconBtn1.Cursor = System.Windows.Forms.Cursors.Hand;
            this.iconBtn1.Dock = System.Windows.Forms.DockStyle.Right;
            this.iconBtn1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.iconBtn1.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.iconBtn1.ForeColor = System.Drawing.Color.White;
            this.iconBtn1.IconBackColor = System.Drawing.SystemColors.ActiveBorder;
            this.iconBtn1.IconForeColor = System.Drawing.Color.Yellow;
            this.iconBtn1.IconSize = 28;
            this.iconBtn1.IconStyle = Cell.IconFont.FontIcons.A_fa_folder_open;
            this.iconBtn1.Image = ((System.Drawing.Image)(resources.GetObject("iconBtn1.Image")));
            this.iconBtn1.Location = new System.Drawing.Point(268, 0);
            this.iconBtn1.Name = "iconBtn1";
            this.iconBtn1.Size = new System.Drawing.Size(28, 24);
            this.iconBtn1.TabIndex = 0;
            this.iconBtn1.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.iconBtn1.UseVisualStyleBackColor = false;
            this.iconBtn1.Click += new System.EventHandler(this.iconBtn1_Click);
            // 
            // ucTextBoxPop1
            // 
            this.ucTextBoxPop1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.ucTextBoxPop1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ucTextBoxPop1.EmptyTextTip = null;
            this.ucTextBoxPop1.EmptyTextTipColor = System.Drawing.Color.DarkGray;
            this.ucTextBoxPop1.Font = new System.Drawing.Font("微软雅黑", 9F);
            this.ucTextBoxPop1.Location = new System.Drawing.Point(0, 0);
            this.ucTextBoxPop1.Name = "ucTextBoxPop1";
            this.ucTextBoxPop1.Size = new System.Drawing.Size(268, 23);
            this.ucTextBoxPop1.TabIndex = 1;
            // 
            // UcFolderEdit
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.ucTextBoxPop1);
            this.Controls.Add(this.iconBtn1);
            this.Name = "UcFolderEdit";
            this.Size = new System.Drawing.Size(296, 24);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private IconBtn iconBtn1;
        private UcTextBoxPop ucTextBoxPop1;
    }
}
