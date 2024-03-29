
namespace Body.IMainStation.ProjectHipMainUC
{
    partial class UcHipMainStation
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
            this.ucGroupBox3 = new Cell.UI.UcGroupBox();
            this.pnl_hwindow = new System.Windows.Forms.Panel();
            this.ucGroupBox1 = new Cell.UI.UcGroupBox();
            this.ucGroupBox3.SuspendLayout();
            this.SuspendLayout();
            // 
            // ucGroupBox3
            // 
            this.ucGroupBox3.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.ucGroupBox3.BorderColor = System.Drawing.Color.DarkGray;
            this.ucGroupBox3.BorderSize = 1;
            this.ucGroupBox3.BorderStyle = System.Windows.Forms.ButtonBorderStyle.Solid;
            this.ucGroupBox3.Controls.Add(this.pnl_hwindow);
            this.ucGroupBox3.FColor = System.Drawing.Color.White;
            this.ucGroupBox3.Location = new System.Drawing.Point(3, 3);
            this.ucGroupBox3.Name = "ucGroupBox3";
            this.ucGroupBox3.Size = new System.Drawing.Size(452, 515);
            this.ucGroupBox3.TabIndex = 3;
            this.ucGroupBox3.TabStop = false;
            this.ucGroupBox3.TColor = System.Drawing.Color.White;
            this.ucGroupBox3.Text = "实时图片";
            this.ucGroupBox3.TitleAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.ucGroupBox3.TitleBackGroundCor = System.Drawing.Color.Turquoise;
            this.ucGroupBox3.TitleFont = new System.Drawing.Font("微软雅黑", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            // 
            // pnl_hwindow
            // 
            this.pnl_hwindow.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pnl_hwindow.BackColor = System.Drawing.Color.White;
            this.pnl_hwindow.Location = new System.Drawing.Point(2, 29);
            this.pnl_hwindow.Name = "pnl_hwindow";
            this.pnl_hwindow.Size = new System.Drawing.Size(448, 484);
            this.pnl_hwindow.TabIndex = 0;
            this.pnl_hwindow.SizeChanged += new System.EventHandler(this.pnl_hwindow_SizeChanged);
            // 
            // ucGroupBox1
            // 
            this.ucGroupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.ucGroupBox1.BorderColor = System.Drawing.Color.DarkGray;
            this.ucGroupBox1.BorderSize = 1;
            this.ucGroupBox1.BorderStyle = System.Windows.Forms.ButtonBorderStyle.Solid;
            this.ucGroupBox1.FColor = System.Drawing.Color.White;
            this.ucGroupBox1.Location = new System.Drawing.Point(459, 3);
            this.ucGroupBox1.Name = "ucGroupBox1";
            this.ucGroupBox1.Size = new System.Drawing.Size(512, 515);
            this.ucGroupBox1.TabIndex = 1;
            this.ucGroupBox1.TabStop = false;
            this.ucGroupBox1.TColor = System.Drawing.Color.White;
            this.ucGroupBox1.Text = "检测结果";
            this.ucGroupBox1.TitleAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.ucGroupBox1.TitleBackGroundCor = System.Drawing.Color.Turquoise;
            this.ucGroupBox1.TitleFont = new System.Drawing.Font("微软雅黑", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            // 
            // UcHipMainStation
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.ucGroupBox3);
            this.Controls.Add(this.ucGroupBox1);
            this.Name = "UcHipMainStation";
            this.Size = new System.Drawing.Size(975, 521);
            this.ucGroupBox3.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private Cell.UI.UcGroupBox ucGroupBox1;
        private Cell.UI.UcGroupBox ucGroupBox3;
        private System.Windows.Forms.Panel pnl_hwindow;
    }
}
