using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cell.DataModel
{
    /// <summary>
    /// 相机触发模式
    /// </summary>
    public enum cCmrTrigMode
    {
        disable, //禁用触发功能 
        software,//软触发模式
        hardware_line0, //硬触发模式,触发线
        hardware_line1,
        hardware_line2,
        hardware_line3
    }
}
