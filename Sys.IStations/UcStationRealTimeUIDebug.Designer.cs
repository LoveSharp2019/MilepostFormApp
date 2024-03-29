
namespace Sys.IStations
{
    partial class UcStationRealTimeUIDebug
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
            this.pnl_left = new System.Windows.Forms.Panel();
            this.pnl_CustomUc = new System.Windows.Forms.Panel();
            this.pnl_publicUc = new System.Windows.Forms.Panel();
            this.pnl_right = new System.Windows.Forms.Panel();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPageAxis = new System.Windows.Forms.TabPage();
            this.tabPageIO = new System.Windows.Forms.TabPage();
            this.tabPagePos = new System.Windows.Forms.TabPage();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.ucStationRealtimeUI1 = new Sys.IStations.UcStationRealtimeUI();
            this.pnl_left.SuspendLayout();
            this.pnl_publicUc.SuspendLayout();
            this.pnl_right.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnl_left
            // 
            this.pnl_left.BackColor = System.Drawing.SystemColors.HotTrack;
            this.pnl_left.Controls.Add(this.pnl_CustomUc);
            this.pnl_left.Controls.Add(this.pnl_publicUc);
            this.pnl_left.Dock = System.Windows.Forms.DockStyle.Left;
            this.pnl_left.Location = new System.Drawing.Point(0, 0);
            this.pnl_left.Name = "pnl_left";
            this.pnl_left.Size = new System.Drawing.Size(378, 843);
            this.pnl_left.TabIndex = 0;
            // 
            // pnl_CustomUc
            // 
            this.pnl_CustomUc.BackColor = System.Drawing.SystemColors.Info;
            this.pnl_CustomUc.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnl_CustomUc.Location = new System.Drawing.Point(0, 254);
            this.pnl_CustomUc.Name = "pnl_CustomUc";
            this.pnl_CustomUc.Size = new System.Drawing.Size(378, 589);
            this.pnl_CustomUc.TabIndex = 1;
            // 
            // pnl_publicUc
            // 
            this.pnl_publicUc.BackColor = System.Drawing.Color.White;
            this.pnl_publicUc.Controls.Add(this.ucStationRealtimeUI1);
            this.pnl_publicUc.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnl_publicUc.Location = new System.Drawing.Point(0, 0);
            this.pnl_publicUc.Name = "pnl_publicUc";
            this.pnl_publicUc.Size = new System.Drawing.Size(378, 254);
            this.pnl_publicUc.TabIndex = 0;
            // 
            // pnl_right
            // 
            this.pnl_right.BackColor = System.Drawing.Color.SeaShell;
            this.pnl_right.Controls.Add(this.tabControl1);
            this.pnl_right.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnl_right.Location = new System.Drawing.Point(378, 0);
            this.pnl_right.Name = "pnl_right";
            this.pnl_right.Size = new System.Drawing.Size(1277, 843);
            this.pnl_right.TabIndex = 1;
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPageAxis);
            this.tabControl1.Controls.Add(this.tabPageIO);
            this.tabControl1.Controls.Add(this.tabPagePos);
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(1277, 843);
            this.tabControl1.TabIndex = 2;
            // 
            // tabPageAxis
            // 
            this.tabPageAxis.Location = new System.Drawing.Point(4, 22);
            this.tabPageAxis.Name = "tabPageAxis";
            this.tabPageAxis.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageAxis.Size = new System.Drawing.Size(2169, 817);
            this.tabPageAxis.TabIndex = 0;
            this.tabPageAxis.Text = "轴调试";
            this.tabPageAxis.UseVisualStyleBackColor = true;
            // 
            // tabPageIO
            // 
            this.tabPageIO.Location = new System.Drawing.Point(4, 22);
            this.tabPageIO.Name = "tabPageIO";
            this.tabPageIO.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageIO.Size = new System.Drawing.Size(2169, 817);
            this.tabPageIO.TabIndex = 1;
            this.tabPageIO.Text = "IO调试";
            this.tabPageIO.UseVisualStyleBackColor = true;
            // 
            // tabPagePos
            // 
            this.tabPagePos.Location = new System.Drawing.Point(4, 22);
            this.tabPagePos.Name = "tabPagePos";
            this.tabPagePos.Size = new System.Drawing.Size(2169, 817);
            this.tabPagePos.TabIndex = 2;
            this.tabPagePos.Text = "轴位置";
            this.tabPagePos.UseVisualStyleBackColor = true;
            // 
            // tabPage1
            // 
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(2169, 817);
            this.tabPage1.TabIndex = 3;
            this.tabPage1.Text = "2D相机";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // tabPage2
            // 
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(1269, 817);
            this.tabPage2.TabIndex = 4;
            this.tabPage2.Text = "线扫激光";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // ucStationRealtimeUI1
            // 
            this.ucStationRealtimeUI1.BackColor = System.Drawing.Color.White;
            this.ucStationRealtimeUI1.JfDisplayMode = Sys.IStations.UcStationRealtimeUI.JFDisplayMode.full;
            this.ucStationRealtimeUI1.Location = new System.Drawing.Point(3, 3);
            this.ucStationRealtimeUI1.Name = "ucStationRealtimeUI1";
            this.ucStationRealtimeUI1.Size = new System.Drawing.Size(372, 247);
            this.ucStationRealtimeUI1.TabIndex = 0;
            // 
            // UcStationRealTimeUIDebug
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.pnl_right);
            this.Controls.Add(this.pnl_left);
            this.Name = "UcStationRealTimeUIDebug";
            this.Size = new System.Drawing.Size(1655, 843);
            this.Load += new System.EventHandler(this.UcStationRealTimeUIDebug_Load);
            this.pnl_left.ResumeLayout(false);
            this.pnl_publicUc.ResumeLayout(false);
            this.pnl_right.ResumeLayout(false);
            this.tabControl1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnl_left;
        private System.Windows.Forms.Panel pnl_publicUc;
        private System.Windows.Forms.Panel pnl_CustomUc;
        private UcStationRealtimeUI ucStationRealtimeUI1;
        private System.Windows.Forms.Panel pnl_right;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPageAxis;
        private System.Windows.Forms.TabPage tabPageIO;
        private System.Windows.Forms.TabPage tabPagePos;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
    }
}
