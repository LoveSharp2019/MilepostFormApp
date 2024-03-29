
using Cell.UI;
using System.Windows.Forms;

namespace Sys.IStations
{
    partial class UcStationDevChnNameMapping
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
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.lbStationName = new System.Windows.Forms.Label();
            this.btEditCancel = new Cell.UI.IconBtn();
            this.btEditSave = new Cell.UI.IconBtn();
            this.tabControlCF1 = new Cell.UI.UcTabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.tabControlCF1.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(128, 13);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(227, 12);
            this.label1.TabIndex = 1;
            this.label1.Text = "DevChannel Local-Global Name Mapping:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(359, 13);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(35, 12);
            this.label2.TabIndex = 2;
            this.label2.Text = "工站:";
            // 
            // lbStationName
            // 
            this.lbStationName.AutoSize = true;
            this.lbStationName.Location = new System.Drawing.Point(394, 13);
            this.lbStationName.Name = "lbStationName";
            this.lbStationName.Size = new System.Drawing.Size(41, 12);
            this.lbStationName.TabIndex = 3;
            this.lbStationName.Text = "未设置";
            // 
            // btEditCancel
            // 
            this.btEditCancel.BackColor = System.Drawing.Color.Gray;
            this.btEditCancel.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btEditCancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btEditCancel.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btEditCancel.ForeColor = System.Drawing.Color.White;
            this.btEditCancel.IconBackColor = System.Drawing.Color.White;
            this.btEditCancel.IconForeColor = System.Drawing.Color.Black;
            this.btEditCancel.IconSize = 32;
            this.btEditCancel.IconStyle = Cell.IconFont.FontIcons.None;
            this.btEditCancel.Location = new System.Drawing.Point(67, 3);
            this.btEditCancel.Name = "btEditCancel";
            this.btEditCancel.Size = new System.Drawing.Size(54, 28);
            this.btEditCancel.TabIndex = 5;
            this.btEditCancel.Text = "取消";
            this.btEditCancel.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btEditCancel.UseVisualStyleBackColor = true;
            this.btEditCancel.Click += new System.EventHandler(this.btEditCancel_Click);
            // 
            // btEditSave
            // 
            this.btEditSave.BackColor = System.Drawing.Color.Gray;
            this.btEditSave.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btEditSave.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btEditSave.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btEditSave.ForeColor = System.Drawing.Color.White;
            this.btEditSave.IconBackColor = System.Drawing.Color.White;
            this.btEditSave.IconForeColor = System.Drawing.Color.Black;
            this.btEditSave.IconSize = 32;
            this.btEditSave.IconStyle = Cell.IconFont.FontIcons.None;
            this.btEditSave.Location = new System.Drawing.Point(7, 3);
            this.btEditSave.Name = "btEditSave";
            this.btEditSave.Size = new System.Drawing.Size(54, 28);
            this.btEditSave.TabIndex = 4;
            this.btEditSave.Text = "编辑";
            this.btEditSave.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btEditSave.UseVisualStyleBackColor = true;
            this.btEditSave.Click += new System.EventHandler(this.btEditSave_Click);
            // 
            // tabControlCF1
            // 
            this.tabControlCF1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tabControlCF1.Controls.Add(this.tabPage1);
            this.tabControlCF1.Controls.Add(this.tabPage2);
            this.tabControlCF1.DrawMode = System.Windows.Forms.TabDrawMode.OwnerDrawFixed;
            this.tabControlCF1.ItemSize = new System.Drawing.Size(85, 35);
            this.tabControlCF1.Location = new System.Drawing.Point(3, 37);
            this.tabControlCF1.Multiline = true;
            this.tabControlCF1.Name = "tabControlCF1";
            this.tabControlCF1.SelectedIndex = 0;
            this.tabControlCF1.SelectStatucColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.tabControlCF1.Size = new System.Drawing.Size(900, 454);
            this.tabControlCF1.SizeMode = System.Windows.Forms.TabSizeMode.Fixed;
            this.tabControlCF1.TabIndex = 0;
            this.tabControlCF1.TabPageFont = new System.Drawing.Font("宋体", 12F);
            this.tabControlCF1.TabPageFontColor = System.Drawing.Color.Black;
            this.tabControlCF1.TbBackgroundColour = System.Drawing.Color.Empty;
            this.tabControlCF1.UnSelectStatucColor = System.Drawing.Color.Empty;
            this.tabControlCF1.UntabPageFontColor = System.Drawing.Color.Black;
            // 
            // tabPage1
            // 
            this.tabPage1.Location = new System.Drawing.Point(4, 39);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(892, 411);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "tabPage1";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // tabPage2
            // 
            this.tabPage2.Location = new System.Drawing.Point(4, 39);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(892, 411);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "tabPage2";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // UcStationDevChnNameMapping
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.btEditCancel);
            this.Controls.Add(this.btEditSave);
            this.Controls.Add(this.lbStationName);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.tabControlCF1);
            this.Name = "UcStationDevChnNameMapping";
            this.Size = new System.Drawing.Size(906, 494);
            this.Load += new System.EventHandler(this.UcLocDevChnNameMapping_Load);
            this.VisibleChanged += new System.EventHandler(this.UcStationDevChnNameMapping_VisibleChanged);
            this.tabControlCF1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private UcTabControl tabControlCF1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label lbStationName;
        private IconBtn btEditSave;
        private IconBtn btEditCancel;
    }
}
