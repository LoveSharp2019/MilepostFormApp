using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Cell.UI
{
    public class UcDataGridView : DataGridView
    {
        public UcDataGridView()
        {
            //奇数行的单元格的背景色为黄绿色
            AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(240, 240, 240);
            ColumnHeadersDefaultCellStyle.Font = new System.Drawing.Font("微软雅黑", 12);
            ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(0, 174, 219);

            AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(140, 240, 240);
            ColumnHeadersDefaultCellStyle.Font = new System.Drawing.Font("微软雅黑", 12);
            ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(0, 174, 219);

            AllowUserToAddRows = false;

            BorderStyle = System.Windows.Forms.BorderStyle.None;
            ColumnHeadersHeight = 38;
            ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.Font = new System.Drawing.Font("微软雅黑", 12);

        }
    }
}
