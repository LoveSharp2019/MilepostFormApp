using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Cell.UI
{
    public class UcCheckBox : CheckBox
    {
        public UcCheckBox()
        {
        //    this.BackColor = Color.Transparent;
        //    this.Font = new Font("微软雅黑", 12);
        //    this.ForeColor = Color.Black;
        //    this.FlatStyle = FlatStyle.Popup;
        }

        protected override void OnPaint(PaintEventArgs pevent)
        {
            base.OnPaint(pevent);
        }
    }
}
