
namespace Org.IMotionDaq
{
    partial class UcCmprTrgChn
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
            this.lbCmpID = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.lbEncoder = new System.Windows.Forms.Label();
            this.cbTrigChns = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.btSwTrigged = new System.Windows.Forms.Button();
            this.btResetCount = new System.Windows.Forms.Button();
            this.btCfg = new System.Windows.Forms.Button();
            this.lbMode = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.tbEncPos = new System.Windows.Forms.TextBox();
            this.tbTrgCnt = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // lbCmpID
            // 
            this.lbCmpID.AutoSize = true;
            this.lbCmpID.Location = new System.Drawing.Point(3, 7);
            this.lbCmpID.Name = "lbCmpID";
            this.lbCmpID.Size = new System.Drawing.Size(53, 12);
            this.lbCmpID.TabIndex = 6;
            this.lbCmpID.Text = "比较器ID";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(244, 8);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(59, 12);
            this.label3.TabIndex = 7;
            this.label3.Text = "触发通道:";
            // 
            // lbEncoder
            // 
            this.lbEncoder.AutoSize = true;
            this.lbEncoder.Location = new System.Drawing.Point(58, 7);
            this.lbEncoder.Margin = new System.Windows.Forms.Padding(1, 0, 1, 0);
            this.lbEncoder.Name = "lbEncoder";
            this.lbEncoder.Size = new System.Drawing.Size(53, 12);
            this.lbEncoder.TabIndex = 9;
            this.lbEncoder.Text = "编码器ID";
            // 
            // cbTrigChns
            // 
            this.cbTrigChns.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbTrigChns.FormattingEnabled = true;
            this.cbTrigChns.Location = new System.Drawing.Point(304, 5);
            this.cbTrigChns.Margin = new System.Windows.Forms.Padding(1, 1, 1, 1);
            this.cbTrigChns.Name = "cbTrigChns";
            this.cbTrigChns.Size = new System.Drawing.Size(39, 20);
            this.cbTrigChns.TabIndex = 12;
            this.cbTrigChns.SelectedIndexChanged += new System.EventHandler(this.cbTrigChns_SelectedIndexChanged);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(346, 9);
            this.label4.Margin = new System.Windows.Forms.Padding(1, 0, 1, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(35, 12);
            this.label4.TabIndex = 13;
            this.label4.Text = "计数:";
            // 
            // btSwTrigged
            // 
            this.btSwTrigged.Location = new System.Drawing.Point(426, 3);
            this.btSwTrigged.Margin = new System.Windows.Forms.Padding(1, 1, 1, 1);
            this.btSwTrigged.Name = "btSwTrigged";
            this.btSwTrigged.Size = new System.Drawing.Size(37, 23);
            this.btSwTrigged.TabIndex = 16;
            this.btSwTrigged.Text = "软触发";
            this.btSwTrigged.UseVisualStyleBackColor = true;
            this.btSwTrigged.Click += new System.EventHandler(this.btSwTrigged_Click);
            // 
            // btResetCount
            // 
            this.btResetCount.Location = new System.Drawing.Point(464, 3);
            this.btResetCount.Margin = new System.Windows.Forms.Padding(1, 1, 1, 1);
            this.btResetCount.Name = "btResetCount";
            this.btResetCount.Size = new System.Drawing.Size(37, 23);
            this.btResetCount.TabIndex = 17;
            this.btResetCount.Text = "重置";
            this.btResetCount.UseVisualStyleBackColor = true;
            this.btResetCount.Click += new System.EventHandler(this.btResetCount_Click);
            // 
            // btCfg
            // 
            this.btCfg.Location = new System.Drawing.Point(571, 3);
            this.btCfg.Margin = new System.Windows.Forms.Padding(1, 1, 1, 1);
            this.btCfg.Name = "btCfg";
            this.btCfg.Size = new System.Drawing.Size(45, 23);
            this.btCfg.TabIndex = 19;
            this.btCfg.Text = "配置";
            this.btCfg.UseVisualStyleBackColor = true;
            this.btCfg.Click += new System.EventHandler(this.btCfg_Click);
            // 
            // lbMode
            // 
            this.lbMode.AutoSize = true;
            this.lbMode.Location = new System.Drawing.Point(504, 9);
            this.lbMode.Margin = new System.Windows.Forms.Padding(1, 0, 1, 0);
            this.lbMode.Name = "lbMode";
            this.lbMode.Size = new System.Drawing.Size(65, 12);
            this.lbMode.TabIndex = 20;
            this.lbMode.Text = "Mode:Liner";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(115, 7);
            this.label5.Margin = new System.Windows.Forms.Padding(1, 0, 1, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(59, 12);
            this.label5.TabIndex = 21;
            this.label5.Text = "位置反馈:";
            // 
            // tbEncPos
            // 
            this.tbEncPos.Location = new System.Drawing.Point(174, 3);
            this.tbEncPos.Name = "tbEncPos";
            this.tbEncPos.ReadOnly = true;
            this.tbEncPos.Size = new System.Drawing.Size(64, 21);
            this.tbEncPos.TabIndex = 22;
            // 
            // tbTrgCnt
            // 
            this.tbTrgCnt.Location = new System.Drawing.Point(380, 4);
            this.tbTrgCnt.Name = "tbTrgCnt";
            this.tbTrgCnt.ReadOnly = true;
            this.tbTrgCnt.Size = new System.Drawing.Size(46, 21);
            this.tbTrgCnt.TabIndex = 23;
            // 
            // UcCmprTrgChn
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tbTrgCnt);
            this.Controls.Add(this.tbEncPos);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.lbMode);
            this.Controls.Add(this.btCfg);
            this.Controls.Add(this.btResetCount);
            this.Controls.Add(this.btSwTrigged);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.cbTrigChns);
            this.Controls.Add(this.lbEncoder);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.lbCmpID);
            this.Name = "UcCmprTrgChn";
            this.Size = new System.Drawing.Size(621, 27);
            this.Load += new System.EventHandler(this.UcCmprTrgChn_Load);
            this.VisibleChanged += new System.EventHandler(this.UcCmprTrgChn_VisibleChanged);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label lbCmpID;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label lbEncoder;
        private System.Windows.Forms.ComboBox cbTrigChns;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button btSwTrigged;
        private System.Windows.Forms.Button btResetCount;
        private System.Windows.Forms.Button btCfg;
        private System.Windows.Forms.Label lbMode;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox tbEncPos;
        private System.Windows.Forms.TextBox tbTrgCnt;
    }
}
