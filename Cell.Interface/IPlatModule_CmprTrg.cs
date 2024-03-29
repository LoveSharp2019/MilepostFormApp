using Cell.DataModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cell.Interface
{
    public interface IPlatModule_CmprTrg : IPlatErrCodeMsg
    {
        /// <summary>
        /// 待实现
        /// </summary>
        int EncoderCount { get; }


        /// <summary>
        /// 比较器数量(比较器是用于连接编码器输入和触发输出，并执行比较计算的中间模块)
        /// </summary>
        int CompareCount { get; }




        /// <summary>
        /// 触发输出通道数量
        /// </summary>
        int TriggerCount { get; }

        /// <summary>获取触发（输出）通道的使能状态</summary>
        int GetTrigEnable(int trigChn, out bool isEnabled);
        /// <summary>设置触发（输出）通道的使能状态</summary>
        int SetTrigEnable(int trigChn, bool isEnable);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="cmpID"></param>
        /// <param name="trgChns"></param>
        /// <returns></returns>
        int GetCmpTrigBinds(int cmpID, out int[] trgChns);

        /// <summary>
        /// 获取触发（输出）通道已经触发的次数（从上一次置0开始）
        /// </summary>
        /// <param name="trigChn"></param>
        /// <param name="count"></param>
        /// <returns></returns>
        int GetTriggedCount(int trigChn, out int count);

        /// <summary>
        /// 软件控制触发通道（输出一个触发信号）
        /// </summary>
        /// <param name="trigChns"></param>
        /// <returns></returns>
        int SoftTrigger(int trigChn);
        /// <summary>
        /// 重置触发通道的触发次数为0
        /// </summary>
        /// <param name="trigChn"></param>
        /// <returns></returns>
        int ResetTriggedCount(int trigChn);
    }
}
