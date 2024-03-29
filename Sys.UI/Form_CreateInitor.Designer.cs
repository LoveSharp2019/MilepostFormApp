
using Cell.UI;
using Tissue.UI;

namespace Sys.UI
{
    partial class Form_CreateInitor
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form_CreateInitor));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            this.panelParams = new System.Windows.Forms.TableLayoutPanel();
            this.lbTips = new System.Windows.Forms.Label();
            this.btCancel = new Cell.UI.IconBtn();
            this.btOK = new Cell.UI.IconBtn();
            this.dgvTypes = new Cell.UI.UcDataGridView();
            this.ColumnType = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColumnBrief = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.gbParams = new Cell.UI.UcGroupBox();
            this.tbID = new Cell.UI.UcTextBoxPop();
            this.label1 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.btn_zoom)).BeginInit();
            this.pnl_base2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.iconPic_title)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvTypes)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.gbParams.SuspendLayout();
            this.SuspendLayout();
            // 
            // btn_zoom
            // 
            this.btn_zoom.Visible = false;
            // 
            // pnl_context
            // 
            this.pnl_context.Size = new System.Drawing.Size(943, 552);
            // 
            // pnl_base2
            // 
            this.pnl_base2.Location = new System.Drawing.Point(833, 0);
            // 
            // panelParams
            // 
            this.panelParams.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panelParams.AutoScroll = true;
            this.panelParams.BackColor = System.Drawing.SystemColors.Control;
            this.panelParams.ColumnCount = 1;
            this.panelParams.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.panelParams.Location = new System.Drawing.Point(5, 56);
            this.panelParams.Margin = new System.Windows.Forms.Padding(3, 10, 3, 10);
            this.panelParams.Name = "panelParams";
            this.panelParams.RowCount = 1;
            this.panelParams.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.panelParams.Size = new System.Drawing.Size(541, 447);
            this.panelParams.TabIndex = 7;
            // 
            // lbTips
            // 
            this.lbTips.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lbTips.AutoSize = true;
            this.lbTips.Location = new System.Drawing.Point(12, 560);
            this.lbTips.Name = "lbTips";
            this.lbTips.Size = new System.Drawing.Size(35, 12);
            this.lbTips.TabIndex = 8;
            this.lbTips.Text = "信息:";
            // 
            // btCancel
            // 
            this.btCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btCancel.BackColor = System.Drawing.Color.Gray;
            this.btCancel.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btCancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btCancel.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btCancel.ForeColor = System.Drawing.Color.White;
            this.btCancel.IconBackColor = System.Drawing.Color.Transparent;
            this.btCancel.IconForeColor = System.Drawing.Color.White;
            this.btCancel.IconSize = 32;
            this.btCancel.IconStyle = Cell.IconFont.FontIcons.A_fa_times_circle;
            this.btCancel.Image = ((System.Drawing.Image)(resources.GetObject("btCancel.Image")));
            this.btCancel.Location = new System.Drawing.Point(847, 549);
            this.btCancel.Name = "btCancel";
            this.btCancel.Size = new System.Drawing.Size(84, 37);
            this.btCancel.TabIndex = 7;
            this.btCancel.Text = "取消";
            this.btCancel.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btCancel.UseVisualStyleBackColor = true;
            this.btCancel.Click += new System.EventHandler(this.btCancel_Click);
            // 
            // btOK
            // 
            this.btOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btOK.BackColor = System.Drawing.Color.Gray;
            this.btOK.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btOK.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btOK.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btOK.ForeColor = System.Drawing.Color.White;
            this.btOK.IconBackColor = System.Drawing.Color.Transparent;
            this.btOK.IconForeColor = System.Drawing.Color.White;
            this.btOK.IconSize = 32;
            this.btOK.IconStyle = Cell.IconFont.FontIcons.A_fa_plus;
            this.btOK.Image = ((System.Drawing.Image)(resources.GetObject("btOK.Image")));
            this.btOK.Location = new System.Drawing.Point(743, 549);
            this.btOK.Name = "btOK";
            this.btOK.Size = new System.Drawing.Size(84, 37);
            this.btOK.TabIndex = 6;
            this.btOK.Text = "创建";
            this.btOK.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btOK.UseVisualStyleBackColor = true;
            this.btOK.Click += new System.EventHandler(this.btOK_Click);
            // 
            // dgvTypes
            // 
            this.dgvTypes.AllowUserToAddRows = false;
            this.dgvTypes.AllowUserToDeleteRows = false;
            this.dgvTypes.AllowUserToResizeRows = false;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            this.dgvTypes.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.dgvTypes.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvTypes.BorderStyle = System.Windows.Forms.BorderStyle.None;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(174)))), ((int)(((byte)(219)))));
            dataGridViewCellStyle2.Font = new System.Drawing.Font("微软雅黑", 12F);
            dataGridViewCellStyle2.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvTypes.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.dgvTypes.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvTypes.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.ColumnType,
            this.ColumnBrief});
            this.dgvTypes.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvTypes.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.dgvTypes.Location = new System.Drawing.Point(0, 0);
            this.dgvTypes.Name = "dgvTypes";
            this.dgvTypes.ReadOnly = true;
            this.dgvTypes.RowHeadersVisible = false;
            this.dgvTypes.RowHeadersWidth = 82;
            this.dgvTypes.RowTemplate.Height = 23;
            this.dgvTypes.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvTypes.Size = new System.Drawing.Size(383, 508);
            this.dgvTypes.TabIndex = 4;
            this.dgvTypes.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvTypes_CellClick);
            // 
            // ColumnType
            // 
            this.ColumnType.HeaderText = "类型";
            this.ColumnType.MinimumWidth = 10;
            this.ColumnType.Name = "ColumnType";
            this.ColumnType.ReadOnly = true;
            // 
            // ColumnBrief
            // 
            this.ColumnBrief.HeaderText = "简介";
            this.ColumnBrief.MinimumWidth = 10;
            this.ColumnBrief.Name = "ColumnBrief";
            this.ColumnBrief.ReadOnly = true;
            // 
            // splitContainer1
            // 
            this.splitContainer1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.splitContainer1.Location = new System.Drawing.Point(1, 38);
            this.splitContainer1.Margin = new System.Windows.Forms.Padding(1);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.dgvTypes);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.gbParams);
            this.splitContainer1.Size = new System.Drawing.Size(939, 508);
            this.splitContainer1.SplitterDistance = 383;
            this.splitContainer1.SplitterWidth = 2;
            this.splitContainer1.TabIndex = 9;
            // 
            // gbParams
            // 
            this.gbParams.BorderColor = System.Drawing.Color.DarkGray;
            this.gbParams.BorderSize = 1;
            this.gbParams.BorderStyle = System.Windows.Forms.ButtonBorderStyle.Dashed;
            this.gbParams.Controls.Add(this.panelParams);
            this.gbParams.Controls.Add(this.tbID);
            this.gbParams.Controls.Add(this.label1);
            this.gbParams.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gbParams.FColor = System.Drawing.Color.White;
            this.gbParams.Location = new System.Drawing.Point(0, 0);
            this.gbParams.Name = "gbParams";
            this.gbParams.Size = new System.Drawing.Size(554, 508);
            this.gbParams.TabIndex = 8;
            this.gbParams.TabStop = false;
            this.gbParams.TColor = System.Drawing.Color.White;
            this.gbParams.Text = "Initor参数";
            this.gbParams.TitleAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.gbParams.TitleBackGroundCor = System.Drawing.Color.Silver;
            this.gbParams.TitleFont = new System.Drawing.Font("微软雅黑", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            // 
            // tbID
            // 
            this.tbID.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tbID.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tbID.EmptyTextTip = null;
            this.tbID.EmptyTextTipColor = System.Drawing.Color.DarkGray;
            this.tbID.Font = new System.Drawing.Font("微软雅黑", 9F);
            this.tbID.Location = new System.Drawing.Point(53, 30);
            this.tbID.Name = "tbID";
            this.tbID.Size = new System.Drawing.Size(497, 23);
            this.tbID.TabIndex = 6;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 34);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(41, 12);
            this.label1.TabIndex = 5;
            this.label1.Text = "全局ID";
            // 
            // Form_CreateInitor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.ClientSize = new System.Drawing.Size(943, 588);
            this.Controls.Add(this.btCancel);
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.lbTips);
            this.Controls.Add(this.btOK);
            this.Name = "Form_CreateInitor";
            this.Text = "Form_CreateInitor";
            this.Load += new System.EventHandler(this.Form_CreateInitor_Load);
            this.Controls.SetChildIndex(this.pnl_context, 0);
            this.Controls.SetChildIndex(this.btOK, 0);
            this.Controls.SetChildIndex(this.lbTips, 0);
            this.Controls.SetChildIndex(this.splitContainer1, 0);
            this.Controls.SetChildIndex(this.btCancel, 0);
            ((System.ComponentModel.ISupportInitialize)(this.btn_zoom)).EndInit();
            this.pnl_base2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.iconPic_title)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvTypes)).EndInit();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.gbParams.ResumeLayout(false);
            this.gbParams.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel panelParams;
        private System.Windows.Forms.Label lbTips;
        private IconBtn btCancel;
        private IconBtn btOK;
        private UcDataGridView dgvTypes;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnType;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnBrief;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private UcGroupBox gbParams;
        private UcTextBoxPop tbID;
        private System.Windows.Forms.Label label1;
    }
}