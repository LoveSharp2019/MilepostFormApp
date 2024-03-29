using Cell.DataModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cell.Interface
{
    public delegate void WorkStatusChange(object sender, IWorkStatus currWorkStatus);

    public interface IPlatOrder
    {
        string Name { get; set; }
        /// <summary>线程工作状态发生变化事件</summary>
        event WorkStatusChange WorkStatusChanged;

        /// <summary>当前线程工作状态</summary>
        IWorkStatus CurrWorkStatus { get; } //当前线程工作状态

        /// <summary>开始运行</summary>
        ICmdResult Start();//开始运行

        /// <summary>停止运行</summary>
        ICmdResult Stop(int timeoutMilliseconds = -1);

        /// <summary>暂停</summary>
        ICmdResult Pause(int timeoutMilliseconds = -1);

        /// <summary>从暂停中恢复运行</summary>
        ICmdResult Resume(int timeoutMilliseconds = -1);

        /// <summary>强制停止线程运行</summary>
        void Abort();

    }
}
