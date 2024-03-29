
using Cell.UI;
using Org.IMotionDaq;
using System.Windows.Forms;

namespace Sys.IStations
{
    partial class UcSimpleAxisInStation
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
            this.ucAxisTest = new Org.IMotionDaq.UcAxisTest();
            this.gbAxisName = new Cell.UI.UcGroupBox();
            this.btCfg = new Cell.UI.IconBtn();
            this.cbMode = new System.Windows.Forms.ComboBox();
            this.gbAxisName.SuspendLayout();
            this.SuspendLayout();
            // 
            // ucAxisTest
            // 
            this.ucAxisTest.DisplayMode = Org.IMotionDaq.UcAxisTest.JFDisplayMode.simplest_pos;
            this.ucAxisTest.IsBoxShowError = false;
            this.ucAxisTest.IsRepeating = false;
            this.ucAxisTest.Location = new System.Drawing.Point(3, 26);
            this.ucAxisTest.Name = "ucAxisTest";
            this.ucAxisTest.Size = new System.Drawing.Size(292, 50);
            this.ucAxisTest.TabIndex = 0;
            // 
            // gbAxisName
            // 
            this.gbAxisName.BorderColor = System.Drawing.Color.Black;
            this.gbAxisName.BorderSize = 1;
            this.gbAxisName.BorderStyle = System.Windows.Forms.ButtonBorderStyle.Solid;
            this.gbAxisName.Controls.Add(this.btCfg);
            this.gbAxisName.Controls.Add(this.cbMode);
            this.gbAxisName.Controls.Add(this.ucAxisTest);
            this.gbAxisName.FColor = System.Drawing.Color.White;
            this.gbAxisName.Location = new System.Drawing.Point(1, 1);
            this.gbAxisName.Name = "gbAxisName";
            this.gbAxisName.Size = new System.Drawing.Size(387, 80);
            this.gbAxisName.TabIndex = 1;
            this.gbAxisName.TabStop = false;
            this.gbAxisName.TColor = System.Drawing.Color.White;
            this.gbAxisName.Text = "轴名称";
            this.gbAxisName.TitleAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.gbAxisName.TitleBackGroundCor = System.Drawing.Color.DeepSkyBlue;
            this.gbAxisName.TitleFont = new System.Drawing.Font("微软雅黑", 12F);
            // 
            // btCfg
            // 
            this.btCfg.BackColor = System.Drawing.Color.Gray;
            this.btCfg.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btCfg.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btCfg.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btCfg.ForeColor = System.Drawing.Color.White;
            this.btCfg.IconBackColor = System.Drawing.Color.White;
            this.btCfg.IconForeColor = System.Drawing.Color.Black;
            this.btCfg.IconSize = 32;
            this.btCfg.IconStyle = Cell.IconFont.FontIcons.None;
            this.btCfg.Location = new System.Drawing.Point(301, 52);
            this.btCfg.Name = "btCfg";
            this.btCfg.Size = new System.Drawing.Size(73, 23);
            this.btCfg.TabIndex = 3;
            this.btCfg.Text = "配置";
            this.btCfg.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btCfg.UseVisualStyleBackColor = true;
            this.btCfg.Click += new System.EventHandler(this.btCfg_Click);
            // 
            // cbMode
            // 
            this.cbMode.FormattingEnabled = true;
            this.cbMode.Items.AddRange(new object[] {
            "位置模式",
            "Jog模式"});
            this.cbMode.Location = new System.Drawing.Point(301, 26);
            this.cbMode.Name = "cbMode";
            this.cbMode.Size = new System.Drawing.Size(73, 20);
            this.cbMode.TabIndex = 2;
            this.cbMode.SelectedIndexChanged += new System.EventHandler(this.cbMode_SelectedIndexChanged);
            // 
            // UcSimpleAxisInStation
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.gbAxisName);
            this.Name = "UcSimpleAxisInStation";
            this.Size = new System.Drawing.Size(390, 83);
            this.Load += new System.EventHandler(this.UcAxisInStation_Load);
            this.gbAxisName.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private UcAxisTest ucAxisTest;
        private UcGroupBox gbAxisName;
        private IconBtn btCfg;
        private ComboBox cbMode;
    }
}
