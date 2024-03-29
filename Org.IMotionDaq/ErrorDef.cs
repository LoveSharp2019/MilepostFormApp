using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Org.IMotionDaq
{
    /// <summary>
    /// 控制卡相关 自定义错误信息
    /// </summary>
    internal enum ErrorDef
    {
        Success = 0,//操作成功，无错误
        Unsupported = -1,//设备不支持此功能
        ParamError = -2,//参数错误（不支持的参数）
        InvokeFailed = -3,//库函数调用出错
        Allowed = 1,//调用成功，但不是所有的参数都支持
        InitFailedWhenOpenCard = -4,//
        LtcCHNoIdel = -5,//锁存通道已用完
        NotOpen = -6,
    }

}
