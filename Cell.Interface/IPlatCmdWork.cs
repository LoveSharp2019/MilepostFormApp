using Cell.DataModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cell.Interface
{
    public delegate void CustomStatusChange(object sender, int currCustomStatus);
    /// <summary>
    /// 可接收用户指令的工作（线程）接口类
    /// </summary>
    public interface IPlatCmdWork : IPlatOrder
    {

        /// <summary>用户自定义状态发生变化事件</summary>
        event CustomStatusChange CustomStatusChanged;

        /// <summary>当前线程自定义状态（与工作逻辑相关）</summary>
        int CurrCustomStatus { get; }

        /// <summary>所有自定义(工作)状态</summary>
        int[] AllCustomStatus { get; }

        /// <summary>获取自定义状态名称</summary>
        string GetCustomStatusName(int status);

        /// <summary>获取本对象（线程）支持的所有用户自定义指令</summary>
        int[] AllCmds { get; }

        /// <summary>用户自定义指令名称</summary>
        string GetCmdName(int cmd);

        /// <summary>向线程发送一条自定义指令</summary>
        ICmdResult SendCmd(int cmd, int timeoutMilliseconds = -1);

        /// <summary>
        /// 线程内部轮询周期
        /// </summary>
        int CycleMilliseconds { get; set; }
    }
}
