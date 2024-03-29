using Cell.DataModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Cell.Interface
{
    public interface IPlatStation : IPlatCmdWork, IPlatInitializable, IPlatRealtimeUIProvider, IPlatCfgUIProvider
    {
        IStationRunMode RunMode { get; }
        bool SetRunMode(IStationRunMode runMode);


        /// <summary>
        /// 生成本工站测试窗口，（可为空值） 
        /// 每调用一次本函数，将生成一个新的窗口对象
        /// </summary>
        Form GenForm();


        /// <summary>
        /// 向工站发送复位指令
        /// </summary>
        /// <returns></returns>
        ICmdResult Reset();

    }
}
