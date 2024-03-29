
using Cell.UI;

namespace Sys.IStations
{
    partial class FormStationBaseDioPanel
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
            this.components = new System.ComponentModel.Container();
            this.timerFlush = new System.Windows.Forms.Timer(this.components);
            this.splitContainerH = new System.Windows.Forms.SplitContainer();
            this.splitContainerV = new System.Windows.Forms.SplitContainer();
            this.panelDOs = new System.Windows.Forms.FlowLayoutPanel();
            this.panelDIs = new System.Windows.Forms.FlowLayoutPanel();
            this.btReflush = new Cell.UI.IconBtn();
            this.btOpenAllDev = new Cell.UI.IconBtn();
            this.ucRichTextScrollTips1 = new Tissue.UI.UcRichTextScrollTips();
            this.toolTips = new System.Windows.Forms.ToolTip(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerH)).BeginInit();
            this.splitContainerH.Panel1.SuspendLayout();
            this.splitContainerH.Panel2.SuspendLayout();
            this.splitContainerH.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerV)).BeginInit();
            this.splitContainerV.Panel1.SuspendLayout();
            this.splitContainerV.Panel2.SuspendLayout();
            this.splitContainerV.SuspendLayout();
            this.SuspendLayout();
            // 
            // timerFlush
            // 
            this.timerFlush.Tick += new System.EventHandler(this.timerFlush_Tick);
            // 
            // splitContainerH
            // 
            this.splitContainerH.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.splitContainerH.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainerH.Location = new System.Drawing.Point(0, 0);
            this.splitContainerH.Name = "splitContainerH";
            this.splitContainerH.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainerH.Panel1
            // 
            this.splitContainerH.Panel1.Controls.Add(this.splitContainerV);
            // 
            // splitContainerH.Panel2
            // 
            this.splitContainerH.Panel2.Controls.Add(this.btReflush);
            this.splitContainerH.Panel2.Controls.Add(this.btOpenAllDev);
            this.splitContainerH.Panel2.Controls.Add(this.ucRichTextScrollTips1);
            this.splitContainerH.Size = new System.Drawing.Size(800, 450);
            this.splitContainerH.SplitterDistance = 347;
            this.splitContainerH.TabIndex = 2;
            // 
            // splitContainerV
            // 
            this.splitContainerV.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.splitContainerV.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainerV.Location = new System.Drawing.Point(0, 0);
            this.splitContainerV.Name = "splitContainerV";
            // 
            // splitContainerV.Panel1
            // 
            this.splitContainerV.Panel1.Controls.Add(this.panelDOs);
            // 
            // splitContainerV.Panel2
            // 
            this.splitContainerV.Panel2.Controls.Add(this.panelDIs);
            this.splitContainerV.Size = new System.Drawing.Size(800, 347);
            this.splitContainerV.SplitterDistance = 266;
            this.splitContainerV.TabIndex = 0;
            // 
            // panelDOs
            // 
            this.panelDOs.AutoScroll = true;
            this.panelDOs.BackColor = System.Drawing.SystemColors.ActiveBorder;
            this.panelDOs.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelDOs.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.panelDOs.Location = new System.Drawing.Point(0, 0);
            this.panelDOs.Name = "panelDOs";
            this.panelDOs.Size = new System.Drawing.Size(262, 343);
            this.panelDOs.TabIndex = 0;
            // 
            // panelDIs
            // 
            this.panelDIs.AutoScroll = true;
            this.panelDIs.BackColor = System.Drawing.SystemColors.ActiveBorder;
            this.panelDIs.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelDIs.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.panelDIs.Location = new System.Drawing.Point(0, 0);
            this.panelDIs.Name = "panelDIs";
            this.panelDIs.Size = new System.Drawing.Size(526, 343);
            this.panelDIs.TabIndex = 0;
            // 
            // btReflush
            // 
            this.btReflush.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btReflush.BackColor = System.Drawing.Color.Gray;
            this.btReflush.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btReflush.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btReflush.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btReflush.ForeColor = System.Drawing.Color.White;
            this.btReflush.IconBackColor = System.Drawing.Color.White;
            this.btReflush.IconForeColor = System.Drawing.Color.Black;
            this.btReflush.IconSize = 32;
            this.btReflush.IconStyle = Cell.IconFont.FontIcons.None;
            this.btReflush.Location = new System.Drawing.Point(694, 0);
            this.btReflush.Name = "btReflush";
            this.btReflush.Size = new System.Drawing.Size(86, 23);
            this.btReflush.TabIndex = 3;
            this.btReflush.Text = "刷新设备状态";
            this.btReflush.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btReflush.UseVisualStyleBackColor = true;
            this.btReflush.Click += new System.EventHandler(this.btReflush_Click);
            // 
            // btOpenAllDev
            // 
            this.btOpenAllDev.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btOpenAllDev.BackColor = System.Drawing.Color.Gray;
            this.btOpenAllDev.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btOpenAllDev.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btOpenAllDev.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btOpenAllDev.ForeColor = System.Drawing.Color.White;
            this.btOpenAllDev.IconBackColor = System.Drawing.Color.White;
            this.btOpenAllDev.IconForeColor = System.Drawing.Color.Black;
            this.btOpenAllDev.IconSize = 32;
            this.btOpenAllDev.IconStyle = Cell.IconFont.FontIcons.None;
            this.btOpenAllDev.Location = new System.Drawing.Point(564, 0);
            this.btOpenAllDev.Name = "btOpenAllDev";
            this.btOpenAllDev.Size = new System.Drawing.Size(105, 23);
            this.btOpenAllDev.TabIndex = 4;
            this.btOpenAllDev.Text = "打开所有DIO设备";
            this.btOpenAllDev.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btOpenAllDev.UseVisualStyleBackColor = true;
            this.btOpenAllDev.Click += new System.EventHandler(this.btOpenAllDev_Click);
            // 
            // ucRichTextScrollTips1
            // 
            this.ucRichTextScrollTips1.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.ucRichTextScrollTips1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ucRichTextScrollTips1.IsAppendTimeInfo = true;
            this.ucRichTextScrollTips1.IsAutoScrollLast = true;
            this.ucRichTextScrollTips1.Location = new System.Drawing.Point(0, 0);
            this.ucRichTextScrollTips1.Margin = new System.Windows.Forms.Padding(2);
            this.ucRichTextScrollTips1.MaxTipsCount = 100;
            this.ucRichTextScrollTips1.Name = "ucRichTextScrollTips1";
            this.ucRichTextScrollTips1.Size = new System.Drawing.Size(796, 95);
            this.ucRichTextScrollTips1.TabIndex = 0;
            // 
            // toolTips
            // 
            this.toolTips.ShowAlways = true;
            // 
            // FormStationBaseDioPanel
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.splitContainerH);
            this.Name = "FormStationBaseDioPanel";
            this.Text = "工站:DIO面板";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FormStationBaseDioPanel_FormClosing);
            this.Load += new System.EventHandler(this.FormStationBaseDioPanel_Load);
            this.VisibleChanged += new System.EventHandler(this.FormStationBaseDioPanel_VisibleChanged);
            this.splitContainerH.Panel1.ResumeLayout(false);
            this.splitContainerH.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerH)).EndInit();
            this.splitContainerH.ResumeLayout(false);
            this.splitContainerV.Panel1.ResumeLayout(false);
            this.splitContainerV.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerV)).EndInit();
            this.splitContainerV.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Timer timerFlush;
        private System.Windows.Forms.SplitContainer splitContainerH;
        private System.Windows.Forms.SplitContainer splitContainerV;
        private System.Windows.Forms.FlowLayoutPanel panelDOs;
        private System.Windows.Forms.FlowLayoutPanel panelDIs;
        private System.Windows.Forms.ToolTip toolTips;
        private IconBtn btReflush;
        private IconBtn btOpenAllDev;
        private Tissue.UI.UcRichTextScrollTips ucRichTextScrollTips1;
    }
}