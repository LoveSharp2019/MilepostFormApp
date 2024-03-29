using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cell.DataModel
{
    /// <summary>
    /// 扫码枪工作模式
    /// </summary>
    public enum cBarcodeSanMode
    {
        Unknown,
        [Description("主动模式")]
        Initiative, //主动模式 (问答式获取条码)
        [Description("被动模式")]
        Passive //被动模式，在回调函数中获取扫描条码
    }
}
