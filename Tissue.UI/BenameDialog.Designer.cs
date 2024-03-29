using Cell.UI;

namespace Tissue.UI
{
    partial class BenameDialog
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
            this.btCancel = new Cell.UI.IconBtn();
            this.btOK = new Cell.UI.IconBtn();
            this.tbNameTxt = new Cell.UI.UcTextBoxPop();
            this.label1 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.btn_zoom)).BeginInit();
            this.pnl_context.SuspendLayout();
            this.pnl_base2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.iconPic_title)).BeginInit();
            this.SuspendLayout();
            // 
            // btn_zoom
            // 
            this.btn_zoom.Enabled = false;
            // 
            // pnl_context
            // 
            this.pnl_context.Controls.Add(this.btCancel);
            this.pnl_context.Controls.Add(this.btOK);
            this.pnl_context.Controls.Add(this.tbNameTxt);
            this.pnl_context.Controls.Add(this.label1);
            this.pnl_context.Size = new System.Drawing.Size(315, 96);
            // 
            // pnl_base2
            // 
            this.pnl_base2.Location = new System.Drawing.Point(205, 0);
            // 
            // btCancel
            // 
            this.btCancel.BackColor = System.Drawing.Color.Gray;
            this.btCancel.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btCancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btCancel.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btCancel.ForeColor = System.Drawing.Color.White;
            this.btCancel.IconBackColor = System.Drawing.Color.White;
            this.btCancel.IconForeColor = System.Drawing.Color.Black;
            this.btCancel.IconSize = 32;
            this.btCancel.IconStyle = Cell.IconFont.FontIcons.None;
            this.btCancel.Location = new System.Drawing.Point(191, 54);
            this.btCancel.Name = "btCancel";
            this.btCancel.Size = new System.Drawing.Size(75, 32);
            this.btCancel.TabIndex = 7;
            this.btCancel.Text = "取消";
            this.btCancel.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btCancel.UseVisualStyleBackColor = true;
            this.btCancel.Click += new System.EventHandler(this.btCancel_Click);
            // 
            // btOK
            // 
            this.btOK.BackColor = System.Drawing.Color.Gray;
            this.btOK.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btOK.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btOK.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btOK.ForeColor = System.Drawing.Color.White;
            this.btOK.IconBackColor = System.Drawing.Color.White;
            this.btOK.IconForeColor = System.Drawing.Color.Black;
            this.btOK.IconSize = 32;
            this.btOK.IconStyle = Cell.IconFont.FontIcons.None;
            this.btOK.Location = new System.Drawing.Point(58, 54);
            this.btOK.Name = "btOK";
            this.btOK.Size = new System.Drawing.Size(75, 32);
            this.btOK.TabIndex = 6;
            this.btOK.Text = "确定";
            this.btOK.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btOK.UseVisualStyleBackColor = true;
            this.btOK.Click += new System.EventHandler(this.btOK_Click);
            // 
            // tbNameTxt
            // 
            this.tbNameTxt.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tbNameTxt.EmptyTextTip = null;
            this.tbNameTxt.EmptyTextTipColor = System.Drawing.Color.DarkGray;
            this.tbNameTxt.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.tbNameTxt.Location = new System.Drawing.Point(58, 17);
            this.tbNameTxt.Name = "tbNameTxt";
            this.tbNameTxt.Size = new System.Drawing.Size(208, 26);
            this.tbNameTxt.TabIndex = 5;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1.Location = new System.Drawing.Point(17, 23);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(35, 14);
            this.label1.TabIndex = 4;
            this.label1.Text = "名称";
            // 
            // BenameDialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(315, 132);
            this.Name = "BenameDialog";
            this.Text = "BenameDialog";
            ((System.ComponentModel.ISupportInitialize)(this.btn_zoom)).EndInit();
            this.pnl_context.ResumeLayout(false);
            this.pnl_context.PerformLayout();
            this.pnl_base2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.iconPic_title)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private IconBtn btCancel;
        private IconBtn btOK;
        private UcTextBoxPop tbNameTxt;
        private System.Windows.Forms.Label label1;
    }
}