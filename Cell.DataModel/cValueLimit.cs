using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cell.DataModel
{
    /// <summary>
    ///  参数的枚举  用于创建构造见面提供数据
    /// </summary>
    public enum cValueLimit
    {
        Non = 0,
        Min = 1,
        Max = 2,
        Range = 4,// 区间
        File = 8,
        Folder = 16,
    }
}
