
using Cell.UI;

namespace Tissue.UI
{
    partial class UcMsDioSetting
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
            this.lamp = new Cell.UI.LampButton();
            this.cbChns = new System.Windows.Forms.ComboBox();
            this.chkEnable = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // lamp
            // 
            this.lamp.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lamp.BackColor = System.Drawing.SystemColors.Control;
            this.lamp.FlatAppearance.BorderColor = System.Drawing.Color.Black;
            this.lamp.FlatAppearance.BorderSize = 0;
            this.lamp.IconSize = new System.Drawing.Size(32, 32);
            this.lamp.ImageIndex = 0;
            this.lamp.LampColor = Cell.UI.LampButton.LColor.Gray;
            this.lamp.Location = new System.Drawing.Point(0, 0);
            this.lamp.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.lamp.Name = "lamp";
            this.lamp.Size = new System.Drawing.Size(132, 20);
            this.lamp.TabIndex = 0;
            this.lamp.UseVisualStyleBackColor = false;
            // 
            // cbChns
            // 
            this.cbChns.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.cbChns.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.cbChns.FormattingEnabled = true;
            this.cbChns.Location = new System.Drawing.Point(134, -1);
            this.cbChns.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.cbChns.Name = "cbChns";
            this.cbChns.Size = new System.Drawing.Size(136, 22);
            this.cbChns.TabIndex = 1;
            // 
            // chkEnable
            // 
            this.chkEnable.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.chkEnable.AutoSize = true;
            this.chkEnable.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.chkEnable.Location = new System.Drawing.Point(270, 2);
            this.chkEnable.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.chkEnable.Name = "chkEnable";
            this.chkEnable.Size = new System.Drawing.Size(54, 18);
            this.chkEnable.TabIndex = 2;
            this.chkEnable.Text = "使能";
            this.chkEnable.UseVisualStyleBackColor = true;
            // 
            // UcMsDioSetting
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ControlLight;
            this.Controls.Add(this.chkEnable);
            this.Controls.Add(this.cbChns);
            this.Controls.Add(this.lamp);
            this.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.MaximumSize = new System.Drawing.Size(500, 20);
            this.MinimumSize = new System.Drawing.Size(200, 20);
            this.Name = "UcMsDioSetting";
            this.Size = new System.Drawing.Size(328, 20);
            this.Load += new System.EventHandler(this.UcMsDioSetting_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private LampButton lamp;
        private System.Windows.Forms.ComboBox cbChns;
        private System.Windows.Forms.CheckBox chkEnable;
    }
}
