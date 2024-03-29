using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Cell.Interface
{
    /// <summary>
    ///  如果 wpf j接口 使用  System.Windows.Controls.UserControl
    ///  如果是 winform 使用  System.Windows.Forms.UserControl
    /// </summary>
    public class UcRealTimeUI : UserControl
    {
        public UcRealTimeUI() : base()
        {

        }

        /// <summary>更新数据源状态到界面上</summary>
        public virtual void UpdateSrc2UI() { }
       
    }
}
