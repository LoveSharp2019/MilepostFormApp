﻿
namespace Sys.UI
{
    partial class UcNamesEdit_Dio
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
            this.pnDi = new System.Windows.Forms.Panel();
            this.pnDo = new System.Windows.Forms.Panel();
            this.lbDi = new System.Windows.Forms.Label();
            this.lbDo = new System.Windows.Forms.Label();
            this.rtTips = new System.Windows.Forms.RichTextBox();
            this.SuspendLayout();
            // 
            // pnDi
            // 
            this.pnDi.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.pnDi.AutoScroll = true;
            this.pnDi.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.pnDi.Location = new System.Drawing.Point(4, 23);
            this.pnDi.Name = "pnDi";
            this.pnDi.Size = new System.Drawing.Size(200, 552);
            this.pnDi.TabIndex = 0;
            // 
            // pnDo
            // 
            this.pnDo.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.pnDo.AutoScroll = true;
            this.pnDo.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.pnDo.Location = new System.Drawing.Point(210, 23);
            this.pnDo.Name = "pnDo";
            this.pnDo.Size = new System.Drawing.Size(200, 552);
            this.pnDo.TabIndex = 1;
            // 
            // lbDi
            // 
            this.lbDi.AutoSize = true;
            this.lbDi.Location = new System.Drawing.Point(4, 4);
            this.lbDi.Name = "lbDi";
            this.lbDi.Size = new System.Drawing.Size(65, 12);
            this.lbDi.TabIndex = 2;
            this.lbDi.Text = "Di名称列表";
            // 
            // lbDo
            // 
            this.lbDo.AutoSize = true;
            this.lbDo.Location = new System.Drawing.Point(228, 5);
            this.lbDo.Name = "lbDo";
            this.lbDo.Size = new System.Drawing.Size(65, 12);
            this.lbDo.TabIndex = 3;
            this.lbDo.Text = "Do名称列表";
            // 
            // rtTips
            // 
            this.rtTips.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.rtTips.BackColor = System.Drawing.SystemColors.Control;
            this.rtTips.Location = new System.Drawing.Point(0, 576);
            this.rtTips.Name = "rtTips";
            this.rtTips.Size = new System.Drawing.Size(573, 90);
            this.rtTips.TabIndex = 4;
            this.rtTips.Text = "";
            // 
            // UcNamesEdit_Dio
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.rtTips);
            this.Controls.Add(this.lbDo);
            this.Controls.Add(this.lbDi);
            this.Controls.Add(this.pnDo);
            this.Controls.Add(this.pnDi);
            this.Name = "UcNamesEdit_Dio";
            this.Size = new System.Drawing.Size(573, 667);
            this.Load += new System.EventHandler(this.UcDioCellNames_Load);
            this.Resize += new System.EventHandler(this.UcDioCellNames_Resize);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel pnDi;
        private System.Windows.Forms.Panel pnDo;
        private System.Windows.Forms.Label lbDi;
        private System.Windows.Forms.Label lbDo;
        private System.Windows.Forms.RichTextBox rtTips;
    }
}
