using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cell.Interface
{
    /// <summary>
    /// 实时界面接口
    /// 提供 实时信息显示/功能调试 等界面
    /// </summary>
    public interface IPlatRealtimeUIProvider
    {
        UcRealTimeUI GetRealtimeUI();
    }
}
