
using Cell.UI;

namespace Sys.IStations
{
    partial class FormAddNames
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
            this.dgvAvailedNames = new Cell.UI.UcDataGridView();
            this.ColumnName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColumnInfo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.btOK = new Cell.UI.IconBtn();
            this.btCancel = new Cell.UI.IconBtn();
            ((System.ComponentModel.ISupportInitialize)(this.btn_zoom)).BeginInit();
            this.pnl_context.SuspendLayout();
            this.pnl_base2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.iconPic_title)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvAvailedNames)).BeginInit();
            this.SuspendLayout();
            // 
            // btn_zoom
            // 
            this.btn_zoom.Enabled = false;
            // 
            // pnl_context
            // 
            this.pnl_context.Controls.Add(this.btCancel);
            this.pnl_context.Controls.Add(this.btOK);
            this.pnl_context.Controls.Add(this.dgvAvailedNames);
            this.pnl_context.Size = new System.Drawing.Size(511, 379);
            // 
            // pnl_base2
            // 
            this.pnl_base2.Location = new System.Drawing.Point(401, 0);
            // 
            // dgvAvailedNames
            // 
            this.dgvAvailedNames.AllowUserToAddRows = false;
            this.dgvAvailedNames.AllowUserToDeleteRows = false;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(140)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            this.dgvAvailedNames.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.dgvAvailedNames.BorderStyle = System.Windows.Forms.BorderStyle.None;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(174)))), ((int)(((byte)(219)))));
            dataGridViewCellStyle2.Font = new System.Drawing.Font("微软雅黑", 12F);
            dataGridViewCellStyle2.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvAvailedNames.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.dgvAvailedNames.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvAvailedNames.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.ColumnName,
            this.ColumnInfo});
            this.dgvAvailedNames.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.dgvAvailedNames.Location = new System.Drawing.Point(2, 2);
            this.dgvAvailedNames.Name = "dgvAvailedNames";
            this.dgvAvailedNames.ReadOnly = true;
            this.dgvAvailedNames.RowHeadersWidth = 30;
            this.dgvAvailedNames.RowTemplate.Height = 23;
            this.dgvAvailedNames.Size = new System.Drawing.Size(506, 340);
            this.dgvAvailedNames.TabIndex = 0;
            // 
            // ColumnName
            // 
            this.ColumnName.HeaderText = "名称项";
            this.ColumnName.Name = "ColumnName";
            this.ColumnName.ReadOnly = true;
            this.ColumnName.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.ColumnName.Width = 200;
            // 
            // ColumnInfo
            // 
            this.ColumnInfo.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.ColumnInfo.HeaderText = "简介";
            this.ColumnInfo.Name = "ColumnInfo";
            this.ColumnInfo.ReadOnly = true;
            this.ColumnInfo.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // btOK
            // 
            this.btOK.BackColor = System.Drawing.Color.Gray;
            this.btOK.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btOK.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btOK.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btOK.ForeColor = System.Drawing.Color.White;
            this.btOK.IconBackColor = System.Drawing.Color.White;
            this.btOK.IconForeColor = System.Drawing.Color.Black;
            this.btOK.IconSize = 32;
            this.btOK.IconStyle = Cell.IconFont.FontIcons.None;
            this.btOK.Location = new System.Drawing.Point(158, 346);
            this.btOK.Name = "btOK";
            this.btOK.Size = new System.Drawing.Size(75, 30);
            this.btOK.TabIndex = 1;
            this.btOK.Text = "确定";
            this.btOK.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btOK.UseVisualStyleBackColor = true;
            this.btOK.Click += new System.EventHandler(this.btOK_Click);
            // 
            // btCancel
            // 
            this.btCancel.BackColor = System.Drawing.Color.Gray;
            this.btCancel.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btCancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btCancel.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btCancel.ForeColor = System.Drawing.Color.White;
            this.btCancel.IconBackColor = System.Drawing.Color.White;
            this.btCancel.IconForeColor = System.Drawing.Color.Black;
            this.btCancel.IconSize = 32;
            this.btCancel.IconStyle = Cell.IconFont.FontIcons.None;
            this.btCancel.Location = new System.Drawing.Point(288, 346);
            this.btCancel.Name = "btCancel";
            this.btCancel.Size = new System.Drawing.Size(75, 30);
            this.btCancel.TabIndex = 2;
            this.btCancel.Text = "取消";
            this.btCancel.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btCancel.UseVisualStyleBackColor = true;
            this.btCancel.Click += new System.EventHandler(this.btCancel_Click);
            // 
            // FormAddNames
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(511, 415);
            this.Name = "FormAddNames";
            this.Text = "FormAddNames";
            this.Load += new System.EventHandler(this.FormAddNames_Load);
            ((System.ComponentModel.ISupportInitialize)(this.btn_zoom)).EndInit();
            this.pnl_context.ResumeLayout(false);
            this.pnl_base2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.iconPic_title)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvAvailedNames)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private UcDataGridView dgvAvailedNames;
        private IconBtn btOK;
        private IconBtn btCancel;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnName;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnInfo;
    }
}