
using Cell.UI;
using Org.IMotionDaq;
using System;

namespace Sys.IStations
{
    partial class UcAxisStatusByName
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
            this.gbAxisName = new Cell.UI.UcGroupBox();
            this.ucAxisStatus1 = new Org.IMotionDaq.UcAxisStatus();
            this.gbAxisName.SuspendLayout();
            this.SuspendLayout();
            // 
            // gbAxisName
            // 
            this.gbAxisName.BorderColor = System.Drawing.Color.Black;
            this.gbAxisName.BorderSize = 0;
            this.gbAxisName.BorderStyle = System.Windows.Forms.ButtonBorderStyle.Solid;
            this.gbAxisName.Controls.Add(this.ucAxisStatus1);
            this.gbAxisName.FColor = System.Drawing.Color.White;
            this.gbAxisName.Location = new System.Drawing.Point(0, 0);
            this.gbAxisName.Name = "gbAxisName";
            this.gbAxisName.Size = new System.Drawing.Size(486, 82);
            this.gbAxisName.TabIndex = 0;
            this.gbAxisName.TabStop = false;
            this.gbAxisName.TColor = System.Drawing.Color.White;
            this.gbAxisName.Text = "groupBox1";
            this.gbAxisName.TitleAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.gbAxisName.TitleBackGroundCor = System.Drawing.Color.DeepSkyBlue;
            this.gbAxisName.TitleFont = new System.Drawing.Font("微软雅黑", 12F);
            // 
            // ucAxisStatus1
            // 
            this.ucAxisStatus1.DisplayMode = Org.IMotionDaq.UcAxisStatus.JFDisplayMode.full;
            this.ucAxisStatus1.Location = new System.Drawing.Point(2, 26);
            this.ucAxisStatus1.Name = "ucAxisStatus1";
            this.ucAxisStatus1.Size = new System.Drawing.Size(480, 51);
            this.ucAxisStatus1.TabIndex = 0;
            // 
            // UcAxisStatusByName
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.gbAxisName);
            this.Name = "UcAxisStatusByName";
            this.Size = new System.Drawing.Size(486, 83);
            this.Load += new System.EventHandler(this.UcAxisStatusByName_Load);
            this.gbAxisName.ResumeLayout(false);
            this.ResumeLayout(false);

        }      

        #endregion

        private UcGroupBox gbAxisName;
        private UcAxisStatus ucAxisStatus1;
    }
}
