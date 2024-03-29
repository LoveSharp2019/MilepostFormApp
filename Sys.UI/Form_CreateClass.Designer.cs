
using Cell.UI;
using Tissue.UI;

namespace Sys.UI
{
    partial class Form_CreateClass
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            this.dgv_IPlatDev = new Cell.UI.UcDataGridView();
            this.DevID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DevType = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DevVer = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.gb_dgv = new Cell.UI.UcGroupBox();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.panel2 = new System.Windows.Forms.Panel();
            this.chkSelfUI = new Cell.UI.UcCheckBox();
            this.btRemove = new Cell.UI.IconBtn();
            this.btCfg = new Cell.UI.IconBtn();
            this.btAdd = new Cell.UI.IconBtn();
            this.btDebug = new Cell.UI.IconBtn();
            this.gbParams = new Cell.UI.UcGroupBox();
            this.btInit = new Cell.UI.IconBtn();
            this.labelID = new System.Windows.Forms.Label();
            this.tbDevID = new Cell.UI.UcTextBoxPop();
            this.btCancel = new Cell.UI.IconBtn();
            this.btEditSave = new Cell.UI.IconBtn();
            ((System.ComponentModel.ISupportInitialize)(this.btn_zoom)).BeginInit();
            this.pnl_base2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.iconPic_title)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_IPlatDev)).BeginInit();
            this.gb_dgv.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.gbParams.SuspendLayout();
            this.SuspendLayout();
            // 
            // btn_zoom
            // 
            this.btn_zoom.Enabled = false;
            this.btn_zoom.Visible = false;
            // 
            // pnl_context
            // 
            this.pnl_context.Size = new System.Drawing.Size(1094, 682);
            // 
            // pnl_base2
            // 
            this.pnl_base2.Location = new System.Drawing.Point(984, 0);
            // 
            // dgv_IPlatDev
            // 
            this.dgv_IPlatDev.AllowUserToAddRows = false;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            this.dgv_IPlatDev.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.dgv_IPlatDev.BackgroundColor = System.Drawing.Color.White;
            this.dgv_IPlatDev.BorderStyle = System.Windows.Forms.BorderStyle.None;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(174)))), ((int)(((byte)(219)))));
            dataGridViewCellStyle2.Font = new System.Drawing.Font("微软雅黑", 12F);
            dataGridViewCellStyle2.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgv_IPlatDev.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.dgv_IPlatDev.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgv_IPlatDev.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.DevID,
            this.DevType,
            this.DevVer});
            this.dgv_IPlatDev.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgv_IPlatDev.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.dgv_IPlatDev.Location = new System.Drawing.Point(1, 34);
            this.dgv_IPlatDev.Name = "dgv_IPlatDev";
            this.dgv_IPlatDev.RowTemplate.Height = 23;
            this.dgv_IPlatDev.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgv_IPlatDev.Size = new System.Drawing.Size(523, 643);
            this.dgv_IPlatDev.TabIndex = 1;
            this.dgv_IPlatDev.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgv_IPlatDev_CellClick);
            // 
            // DevID
            // 
            this.DevID.DataPropertyName = "DevID";
            this.DevID.HeaderText = "设备编码";
            this.DevID.Name = "DevID";
            this.DevID.ReadOnly = true;
            // 
            // DevType
            // 
            this.DevType.HeaderText = "设备类型";
            this.DevType.Name = "DevType";
            this.DevType.ReadOnly = true;
            // 
            // DevVer
            // 
            this.DevVer.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.DevVer.HeaderText = "程序集版本";
            this.DevVer.Name = "DevVer";
            this.DevVer.ReadOnly = true;
            // 
            // gb_dgv
            // 
            this.gb_dgv.BorderColor = System.Drawing.Color.Black;
            this.gb_dgv.BorderSize = 0;
            this.gb_dgv.BorderStyle = System.Windows.Forms.ButtonBorderStyle.Solid;
            this.gb_dgv.Controls.Add(this.dgv_IPlatDev);
            this.gb_dgv.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gb_dgv.FColor = System.Drawing.Color.Gainsboro;
            this.gb_dgv.Location = new System.Drawing.Point(0, 0);
            this.gb_dgv.Name = "gb_dgv";
            this.gb_dgv.Size = new System.Drawing.Size(525, 678);
            this.gb_dgv.TabIndex = 3;
            this.gb_dgv.TabStop = false;
            this.gb_dgv.TColor = System.Drawing.Color.Gray;
            this.gb_dgv.Text = "已添加设备列表";
            this.gb_dgv.TitleAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.gb_dgv.TitleBackGroundCor = System.Drawing.Color.Silver;
            this.gb_dgv.TitleFont = new System.Drawing.Font("微软雅黑", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            // 
            // splitContainer1
            // 
            this.splitContainer1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 36);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.gb_dgv);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.panel2);
            this.splitContainer1.Panel2.Controls.Add(this.gbParams);
            this.splitContainer1.Size = new System.Drawing.Size(1094, 682);
            this.splitContainer1.SplitterDistance = 529;
            this.splitContainer1.TabIndex = 4;
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.Gray;
            this.panel2.Controls.Add(this.chkSelfUI);
            this.panel2.Controls.Add(this.btRemove);
            this.panel2.Controls.Add(this.btCfg);
            this.panel2.Controls.Add(this.btAdd);
            this.panel2.Controls.Add(this.btDebug);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(0, 575);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(557, 103);
            this.panel2.TabIndex = 1;
            // 
            // chkSelfUI
            // 
            this.chkSelfUI.AutoSize = true;
            this.chkSelfUI.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Bold);
            this.chkSelfUI.Location = new System.Drawing.Point(439, 72);
            this.chkSelfUI.Name = "chkSelfUI";
            this.chkSelfUI.Size = new System.Drawing.Size(95, 20);
            this.chkSelfUI.TabIndex = 16;
            this.chkSelfUI.Text = "自带界面";
            this.chkSelfUI.UseVisualStyleBackColor = true;
            // 
            // btRemove
            // 
            this.btRemove.BackColor = System.Drawing.Color.Gray;
            this.btRemove.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btRemove.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btRemove.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Bold);
            this.btRemove.ForeColor = System.Drawing.Color.White;
            this.btRemove.IconBackColor = System.Drawing.Color.White;
            this.btRemove.IconForeColor = System.Drawing.Color.Black;
            this.btRemove.IconSize = 32;
            this.btRemove.IconStyle = Cell.IconFont.FontIcons.None;
            this.btRemove.Location = new System.Drawing.Point(21, 11);
            this.btRemove.Name = "btRemove";
            this.btRemove.Size = new System.Drawing.Size(133, 38);
            this.btRemove.TabIndex = 12;
            this.btRemove.Text = "移除所选设备";
            this.btRemove.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btRemove.UseVisualStyleBackColor = true;
            this.btRemove.Click += new System.EventHandler(this.btRemove_Click);
            // 
            // btCfg
            // 
            this.btCfg.BackColor = System.Drawing.Color.Gray;
            this.btCfg.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btCfg.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btCfg.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btCfg.ForeColor = System.Drawing.Color.White;
            this.btCfg.IconBackColor = System.Drawing.Color.White;
            this.btCfg.IconForeColor = System.Drawing.Color.Black;
            this.btCfg.IconSize = 32;
            this.btCfg.IconStyle = Cell.IconFont.FontIcons.None;
            this.btCfg.Location = new System.Drawing.Point(401, 11);
            this.btCfg.Name = "btCfg";
            this.btCfg.Size = new System.Drawing.Size(133, 38);
            this.btCfg.TabIndex = 15;
            this.btCfg.Text = "编辑设备配置";
            this.btCfg.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btCfg.UseVisualStyleBackColor = true;
            this.btCfg.Click += new System.EventHandler(this.btCfg_Click);
            // 
            // btAdd
            // 
            this.btAdd.BackColor = System.Drawing.Color.Gray;
            this.btAdd.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btAdd.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btAdd.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btAdd.ForeColor = System.Drawing.Color.White;
            this.btAdd.IconBackColor = System.Drawing.Color.White;
            this.btAdd.IconForeColor = System.Drawing.Color.Black;
            this.btAdd.IconSize = 32;
            this.btAdd.IconStyle = Cell.IconFont.FontIcons.None;
            this.btAdd.Location = new System.Drawing.Point(218, 11);
            this.btAdd.Name = "btAdd";
            this.btAdd.Size = new System.Drawing.Size(133, 38);
            this.btAdd.TabIndex = 13;
            this.btAdd.Text = "添加新设备";
            this.btAdd.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btAdd.UseVisualStyleBackColor = true;
            this.btAdd.Click += new System.EventHandler(this.btAdd_Click);
            // 
            // btDebug
            // 
            this.btDebug.BackColor = System.Drawing.Color.Gray;
            this.btDebug.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btDebug.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btDebug.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btDebug.ForeColor = System.Drawing.Color.White;
            this.btDebug.IconBackColor = System.Drawing.Color.White;
            this.btDebug.IconForeColor = System.Drawing.Color.Black;
            this.btDebug.IconSize = 32;
            this.btDebug.IconStyle = Cell.IconFont.FontIcons.None;
            this.btDebug.Location = new System.Drawing.Point(218, 62);
            this.btDebug.Name = "btDebug";
            this.btDebug.Size = new System.Drawing.Size(133, 38);
            this.btDebug.TabIndex = 14;
            this.btDebug.Text = "调试所选设备";
            this.btDebug.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btDebug.UseVisualStyleBackColor = true;
            this.btDebug.Click += new System.EventHandler(this.btDebug_Click);
            // 
            // gbParams
            // 
            this.gbParams.BorderColor = System.Drawing.Color.Black;
            this.gbParams.BorderSize = 0;
            this.gbParams.BorderStyle = System.Windows.Forms.ButtonBorderStyle.Solid;
            this.gbParams.Controls.Add(this.btInit);
            this.gbParams.Controls.Add(this.labelID);
            this.gbParams.Controls.Add(this.tbDevID);
            this.gbParams.Controls.Add(this.btCancel);
            this.gbParams.Controls.Add(this.btEditSave);
            this.gbParams.Dock = System.Windows.Forms.DockStyle.Top;
            this.gbParams.FColor = System.Drawing.Color.White;
            this.gbParams.Location = new System.Drawing.Point(0, 0);
            this.gbParams.Name = "gbParams";
            this.gbParams.Size = new System.Drawing.Size(557, 575);
            this.gbParams.TabIndex = 0;
            this.gbParams.TabStop = false;
            this.gbParams.TColor = System.Drawing.Color.White;
            this.gbParams.Text = "初始化参数";
            this.gbParams.TitleAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.gbParams.TitleBackGroundCor = System.Drawing.Color.Silver;
            this.gbParams.TitleFont = new System.Drawing.Font("微软雅黑", 18F);
            // 
            // btInit
            // 
            this.btInit.BackColor = System.Drawing.Color.Gray;
            this.btInit.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btInit.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btInit.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btInit.ForeColor = System.Drawing.Color.White;
            this.btInit.IconBackColor = System.Drawing.Color.White;
            this.btInit.IconForeColor = System.Drawing.Color.Black;
            this.btInit.IconSize = 32;
            this.btInit.IconStyle = Cell.IconFont.FontIcons.None;
            this.btInit.Location = new System.Drawing.Point(31, 46);
            this.btInit.Name = "btInit";
            this.btInit.Size = new System.Drawing.Size(51, 23);
            this.btInit.TabIndex = 9;
            this.btInit.Text = "初始化";
            this.btInit.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btInit.UseVisualStyleBackColor = true;
            this.btInit.Click += new System.EventHandler(this.btInit_Click);
            // 
            // labelID
            // 
            this.labelID.AutoSize = true;
            this.labelID.Location = new System.Drawing.Point(88, 53);
            this.labelID.Name = "labelID";
            this.labelID.Size = new System.Drawing.Size(41, 12);
            this.labelID.TabIndex = 8;
            this.labelID.Text = "设备ID";
            // 
            // tbDevID
            // 
            this.tbDevID.BackColor = System.Drawing.SystemColors.Control;
            this.tbDevID.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tbDevID.EmptyTextTip = null;
            this.tbDevID.EmptyTextTipColor = System.Drawing.Color.DarkGray;
            this.tbDevID.Enabled = false;
            this.tbDevID.Font = new System.Drawing.Font("微软雅黑", 9F);
            this.tbDevID.Location = new System.Drawing.Point(135, 46);
            this.tbDevID.Name = "tbDevID";
            this.tbDevID.Size = new System.Drawing.Size(269, 23);
            this.tbDevID.TabIndex = 7;
            // 
            // btCancel
            // 
            this.btCancel.BackColor = System.Drawing.Color.Gray;
            this.btCancel.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btCancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btCancel.Font = new System.Drawing.Font("宋体", 9F);
            this.btCancel.ForeColor = System.Drawing.Color.White;
            this.btCancel.IconBackColor = System.Drawing.Color.White;
            this.btCancel.IconForeColor = System.Drawing.Color.Black;
            this.btCancel.IconSize = 32;
            this.btCancel.IconStyle = Cell.IconFont.FontIcons.None;
            this.btCancel.Location = new System.Drawing.Point(519, 46);
            this.btCancel.Name = "btCancel";
            this.btCancel.Size = new System.Drawing.Size(43, 23);
            this.btCancel.TabIndex = 6;
            this.btCancel.Text = "取消";
            this.btCancel.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btCancel.UseVisualStyleBackColor = true;
            // 
            // btEditSave
            // 
            this.btEditSave.BackColor = System.Drawing.Color.Gray;
            this.btEditSave.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btEditSave.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btEditSave.Font = new System.Drawing.Font("宋体", 9F);
            this.btEditSave.ForeColor = System.Drawing.Color.White;
            this.btEditSave.IconBackColor = System.Drawing.Color.White;
            this.btEditSave.IconForeColor = System.Drawing.Color.Black;
            this.btEditSave.IconSize = 32;
            this.btEditSave.IconStyle = Cell.IconFont.FontIcons.None;
            this.btEditSave.Location = new System.Drawing.Point(410, 46);
            this.btEditSave.Name = "btEditSave";
            this.btEditSave.Size = new System.Drawing.Size(103, 23);
            this.btEditSave.TabIndex = 5;
            this.btEditSave.Text = "编辑初始化参数";
            this.btEditSave.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btEditSave.UseVisualStyleBackColor = true;
            this.btEditSave.Click += new System.EventHandler(this.btEditSave_Click);
            // 
            // Form_CreateClass
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1094, 718);
            this.Controls.Add(this.splitContainer1);
            this.Name = "Form_CreateClass";
            this.Text = "设备管理";
            this.Load += new System.EventHandler(this.Form_CreateDevice_Load);
            this.Controls.SetChildIndex(this.pnl_context, 0);
            this.Controls.SetChildIndex(this.splitContainer1, 0);
            ((System.ComponentModel.ISupportInitialize)(this.btn_zoom)).EndInit();
            this.pnl_base2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.iconPic_title)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_IPlatDev)).EndInit();
            this.gb_dgv.ResumeLayout(false);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.gbParams.ResumeLayout(false);
            this.gbParams.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private UcDataGridView dgv_IPlatDev;
        private System.Windows.Forms.DataGridViewTextBoxColumn DevID;
        private System.Windows.Forms.DataGridViewTextBoxColumn DevType;
        private System.Windows.Forms.DataGridViewTextBoxColumn DevVer;
        private UcGroupBox gb_dgv;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private UcGroupBox gbParams;
        private System.Windows.Forms.Panel panel2;
        private UcCheckBox chkSelfUI;
        private IconBtn btRemove;
        private IconBtn btCfg;
        private IconBtn btAdd;
        private IconBtn btDebug;
        private IconBtn btInit;
        private System.Windows.Forms.Label labelID;
        private UcTextBoxPop tbDevID;
        private IconBtn btCancel;
        private IconBtn btEditSave;
    }
}