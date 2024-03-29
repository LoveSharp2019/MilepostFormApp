using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cell.DataModel
{
    public enum CCRet
    {
        Normal = 0, //正常退出CheckCommand
        Resume,//从暂停/恢复中退出CheckCommand
        Error,
    }

    /// <summary>
    /// 固定的DI
    /// </summary>
    public enum FixedDI
    {
        复位按钮 = 1,
        开始按钮 = 2,
        暂停按钮 = 4,
        停止按钮 = 8,
        急停按钮 = 16,
    }

    /// <summary>
    /// 固定DO 输出
    /// </summary>
    public enum FixedDO
    {
        复位按钮灯 = 1,
        开始按钮灯 = 2,
        暂停按钮灯 = 4,
        停止按钮灯 = 8,
        急停按钮灯 = 16,
        红灯 = 32,
        黄灯 = 64,
        绿灯 = 128,
        蜂鸣器 = 256,
    }

}
