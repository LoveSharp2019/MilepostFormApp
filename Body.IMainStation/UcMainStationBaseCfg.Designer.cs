
using System.Windows.Forms;

namespace Body.IMainStation
{
    partial class UcMainStationBaseCfg
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
            this.tabControlCF1 = new System.Windows.Forms.TabControl();
            this.tpCustom = new System.Windows.Forms.TabPage();
            this.tpSysCfg = new System.Windows.Forms.TabPage();
            this.pnSysCfg = new System.Windows.Forms.Panel();
            this.lbSysTips = new System.Windows.Forms.Label();
            this.btSysEditCancel = new System.Windows.Forms.Button();
            this.btSysEditSave = new System.Windows.Forms.Button();
            this.tpMSCfg = new System.Windows.Forms.TabPage();
            this.pnMS = new System.Windows.Forms.Panel();
            this.lbMsTips = new System.Windows.Forms.Label();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.fpFixedDos = new System.Windows.Forms.FlowLayoutPanel();
            this.fpFixedDIs = new System.Windows.Forms.FlowLayoutPanel();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.fpDos = new System.Windows.Forms.FlowLayoutPanel();
            this.fpDis = new System.Windows.Forms.FlowLayoutPanel();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.pnCfg = new System.Windows.Forms.Panel();
            this.btMSEditCancel = new System.Windows.Forms.Button();
            this.btMSEditSave = new System.Windows.Forms.Button();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.tabControlCF1.SuspendLayout();
            this.tpSysCfg.SuspendLayout();
            this.tpMSCfg.SuspendLayout();
            this.pnMS.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControlCF1
            // 
            this.tabControlCF1.Controls.Add(this.tpCustom);
            this.tabControlCF1.Controls.Add(this.tpSysCfg);
            this.tabControlCF1.Controls.Add(this.tpMSCfg);
            this.tabControlCF1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControlCF1.ItemSize = new System.Drawing.Size(120, 35);
            this.tabControlCF1.Location = new System.Drawing.Point(0, 0);
            this.tabControlCF1.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.tabControlCF1.Multiline = true;
            this.tabControlCF1.Name = "tabControlCF1";
            this.tabControlCF1.SelectedIndex = 0;
            this.tabControlCF1.Size = new System.Drawing.Size(1046, 703);
            this.tabControlCF1.TabIndex = 0;
            this.tabControlCF1.SelectedIndexChanged += new System.EventHandler(this.tabControlCF1_SelectedIndexChanged);
            this.tabControlCF1.Selecting += new System.Windows.Forms.TabControlCancelEventHandler(this.tabControlCF1_Selecting);
            this.tabControlCF1.VisibleChanged += new System.EventHandler(this.tabControlCF1_VisibleChanged);
            // 
            // tpCustom
            // 
            this.tpCustom.Location = new System.Drawing.Point(4, 39);
            this.tpCustom.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.tpCustom.Name = "tpCustom";
            this.tpCustom.Size = new System.Drawing.Size(1038, 660);
            this.tpCustom.TabIndex = 3;
            this.tpCustom.Text = "参数设置";
            this.tpCustom.UseVisualStyleBackColor = true;
            // 
            // tpSysCfg
            // 
            this.tpSysCfg.BackColor = System.Drawing.SystemColors.Control;
            this.tpSysCfg.Controls.Add(this.pnSysCfg);
            this.tpSysCfg.Controls.Add(this.lbSysTips);
            this.tpSysCfg.Controls.Add(this.btSysEditCancel);
            this.tpSysCfg.Controls.Add(this.btSysEditSave);
            this.tpSysCfg.Location = new System.Drawing.Point(4, 39);
            this.tpSysCfg.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.tpSysCfg.Name = "tpSysCfg";
            this.tpSysCfg.Size = new System.Drawing.Size(1038, 660);
            this.tpSysCfg.TabIndex = 2;
            this.tpSysCfg.Text = "系统参数";
            // 
            // pnSysCfg
            // 
            this.pnSysCfg.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pnSysCfg.AutoScroll = true;
            this.pnSysCfg.Location = new System.Drawing.Point(2, 2);
            this.pnSysCfg.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.pnSysCfg.Name = "pnSysCfg";
            this.pnSysCfg.Size = new System.Drawing.Size(970, 635);
            this.pnSysCfg.TabIndex = 6;
            // 
            // lbSysTips
            // 
            this.lbSysTips.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lbSysTips.AutoSize = true;
            this.lbSysTips.Location = new System.Drawing.Point(6, 643);
            this.lbSysTips.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lbSysTips.Name = "lbSysTips";
            this.lbSysTips.Size = new System.Drawing.Size(29, 12);
            this.lbSysTips.TabIndex = 5;
            this.lbSysTips.Text = "信息";
            // 
            // btSysEditCancel
            // 
            this.btSysEditCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btSysEditCancel.Enabled = false;
            this.btSysEditCancel.Location = new System.Drawing.Point(1062, 639);
            this.btSysEditCancel.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.btSysEditCancel.Name = "btSysEditCancel";
            this.btSysEditCancel.Size = new System.Drawing.Size(53, 23);
            this.btSysEditCancel.TabIndex = 3;
            this.btSysEditCancel.Text = "取消";
            this.btSysEditCancel.UseVisualStyleBackColor = true;
            this.btSysEditCancel.Click += new System.EventHandler(this.btSysEditCancel_Click);
            // 
            // btSysEditSave
            // 
            this.btSysEditSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btSysEditSave.Location = new System.Drawing.Point(1006, 639);
            this.btSysEditSave.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.btSysEditSave.Name = "btSysEditSave";
            this.btSysEditSave.Size = new System.Drawing.Size(53, 23);
            this.btSysEditSave.TabIndex = 2;
            this.btSysEditSave.Text = "编辑";
            this.btSysEditSave.UseVisualStyleBackColor = true;
            this.btSysEditSave.Click += new System.EventHandler(this.btSysEditSave_Click);
            // 
            // tpMSCfg
            // 
            this.tpMSCfg.Controls.Add(this.pnMS);
            this.tpMSCfg.Location = new System.Drawing.Point(4, 39);
            this.tpMSCfg.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.tpMSCfg.Name = "tpMSCfg";
            this.tpMSCfg.Padding = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.tpMSCfg.Size = new System.Drawing.Size(1038, 660);
            this.tpMSCfg.TabIndex = 1;
            this.tpMSCfg.Text = "MS_CFG";
            this.tpMSCfg.UseVisualStyleBackColor = true;
            // 
            // pnMS
            // 
            this.pnMS.BackColor = System.Drawing.SystemColors.Control;
            this.pnMS.Controls.Add(this.lbMsTips);
            this.pnMS.Controls.Add(this.groupBox3);
            this.pnMS.Controls.Add(this.groupBox2);
            this.pnMS.Controls.Add(this.groupBox1);
            this.pnMS.Controls.Add(this.btMSEditCancel);
            this.pnMS.Controls.Add(this.btMSEditSave);
            this.pnMS.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnMS.Location = new System.Drawing.Point(2, 2);
            this.pnMS.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.pnMS.Name = "pnMS";
            this.pnMS.Size = new System.Drawing.Size(1034, 656);
            this.pnMS.TabIndex = 1;
            // 
            // lbMsTips
            // 
            this.lbMsTips.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lbMsTips.AutoSize = true;
            this.lbMsTips.Location = new System.Drawing.Point(4, 636);
            this.lbMsTips.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lbMsTips.Name = "lbMsTips";
            this.lbMsTips.Size = new System.Drawing.Size(29, 12);
            this.lbMsTips.TabIndex = 5;
            this.lbMsTips.Text = "信息";
            // 
            // groupBox3
            // 
            this.groupBox3.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox3.Controls.Add(this.tableLayoutPanel1);
            this.groupBox3.Location = new System.Drawing.Point(2, 2);
            this.groupBox3.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Padding = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.groupBox3.Size = new System.Drawing.Size(1030, 175);
            this.groupBox3.TabIndex = 4;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "按钮/灯 设备通道设置";
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Controls.Add(this.fpFixedDos, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.fpFixedDIs, 0, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(2, 16);
            this.tableLayoutPanel1.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 1;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(1026, 157);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // fpFixedDos
            // 
            this.fpFixedDos.AutoScroll = true;
            this.fpFixedDos.Dock = System.Windows.Forms.DockStyle.Fill;
            this.fpFixedDos.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.fpFixedDos.Location = new System.Drawing.Point(515, 2);
            this.fpFixedDos.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.fpFixedDos.Name = "fpFixedDos";
            this.fpFixedDos.Size = new System.Drawing.Size(509, 153);
            this.fpFixedDos.TabIndex = 1;
            // 
            // fpFixedDIs
            // 
            this.fpFixedDIs.AutoScroll = true;
            this.fpFixedDIs.Dock = System.Windows.Forms.DockStyle.Fill;
            this.fpFixedDIs.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.fpFixedDIs.Location = new System.Drawing.Point(2, 2);
            this.fpFixedDIs.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.fpFixedDIs.Name = "fpFixedDIs";
            this.fpFixedDIs.Size = new System.Drawing.Size(509, 153);
            this.fpFixedDIs.TabIndex = 0;
            // 
            // groupBox2
            // 
            this.groupBox2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox2.Controls.Add(this.tableLayoutPanel2);
            this.groupBox2.Location = new System.Drawing.Point(2, 180);
            this.groupBox2.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Padding = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.groupBox2.Size = new System.Drawing.Size(1028, 175);
            this.groupBox2.TabIndex = 3;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "拓展DIO设备通道配置";
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.ColumnCount = 2;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel2.Controls.Add(this.fpDos, 0, 0);
            this.tableLayoutPanel2.Controls.Add(this.fpDis, 0, 0);
            this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel2.Location = new System.Drawing.Point(2, 16);
            this.tableLayoutPanel2.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 1;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(1024, 157);
            this.tableLayoutPanel2.TabIndex = 1;
            // 
            // fpDos
            // 
            this.fpDos.AutoScroll = true;
            this.fpDos.Dock = System.Windows.Forms.DockStyle.Fill;
            this.fpDos.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.fpDos.Location = new System.Drawing.Point(514, 2);
            this.fpDos.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.fpDos.Name = "fpDos";
            this.fpDos.Size = new System.Drawing.Size(508, 153);
            this.fpDos.TabIndex = 2;
            // 
            // fpDis
            // 
            this.fpDis.AutoScroll = true;
            this.fpDis.Dock = System.Windows.Forms.DockStyle.Fill;
            this.fpDis.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.fpDis.Location = new System.Drawing.Point(2, 2);
            this.fpDis.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.fpDis.Name = "fpDis";
            this.fpDis.Size = new System.Drawing.Size(508, 153);
            this.fpDis.TabIndex = 1;
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.pnCfg);
            this.groupBox1.Location = new System.Drawing.Point(2, 356);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.groupBox1.Size = new System.Drawing.Size(1030, 272);
            this.groupBox1.TabIndex = 2;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "主工站参数";
            // 
            // pnCfg
            // 
            this.pnCfg.AutoScroll = true;
            this.pnCfg.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnCfg.Location = new System.Drawing.Point(2, 16);
            this.pnCfg.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.pnCfg.Name = "pnCfg";
            this.pnCfg.Size = new System.Drawing.Size(1026, 254);
            this.pnCfg.TabIndex = 0;
            // 
            // btMSEditCancel
            // 
            this.btMSEditCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btMSEditCancel.Enabled = false;
            this.btMSEditCancel.Location = new System.Drawing.Point(979, 632);
            this.btMSEditCancel.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.btMSEditCancel.Name = "btMSEditCancel";
            this.btMSEditCancel.Size = new System.Drawing.Size(53, 23);
            this.btMSEditCancel.TabIndex = 1;
            this.btMSEditCancel.Text = "取消";
            this.btMSEditCancel.UseVisualStyleBackColor = true;
            this.btMSEditCancel.Click += new System.EventHandler(this.btMSEditCancel_Click);
            // 
            // btMSEditSave
            // 
            this.btMSEditSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btMSEditSave.Location = new System.Drawing.Point(923, 632);
            this.btMSEditSave.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.btMSEditSave.Name = "btMSEditSave";
            this.btMSEditSave.Size = new System.Drawing.Size(53, 23);
            this.btMSEditSave.TabIndex = 0;
            this.btMSEditSave.Text = "编辑";
            this.btMSEditSave.UseVisualStyleBackColor = true;
            this.btMSEditSave.Click += new System.EventHandler(this.btMSEditSave_Click);
            // 
            // timer1
            // 
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // UcMainStationBaseCfg
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tabControlCF1);
            this.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.Name = "UcMainStationBaseCfg";
            this.Size = new System.Drawing.Size(1046, 703);
            this.Load += new System.EventHandler(this.UcMainStationBaseCfg_Load);
            this.tabControlCF1.ResumeLayout(false);
            this.tpSysCfg.ResumeLayout(false);
            this.tpSysCfg.PerformLayout();
            this.tpMSCfg.ResumeLayout(false);
            this.pnMS.ResumeLayout(false);
            this.pnMS.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.tableLayoutPanel2.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabPage tpMSCfg;
        private TabControl tabControlCF1;
        private System.Windows.Forms.Panel pnMS;
        private System.Windows.Forms.Button btMSEditSave;
        private System.Windows.Forms.Button btMSEditCancel;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TabPage tpSysCfg;
        private System.Windows.Forms.Button btSysEditCancel;
        private System.Windows.Forms.Button btSysEditSave;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.FlowLayoutPanel fpFixedDos;
        private System.Windows.Forms.FlowLayoutPanel fpFixedDIs;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.FlowLayoutPanel fpDos;
        private System.Windows.Forms.FlowLayoutPanel fpDis;
        private System.Windows.Forms.Panel pnCfg;
        private System.Windows.Forms.Label lbMsTips;
        private System.Windows.Forms.Label lbSysTips;
        private System.Windows.Forms.TabPage tpCustom;
        private System.Windows.Forms.Panel pnSysCfg;
        private System.Windows.Forms.Timer timer1;
    }
}
