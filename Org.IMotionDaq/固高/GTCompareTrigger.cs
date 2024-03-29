using Cell.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Org.IMotionDaq
{
    public class GTCompareTrigger : IPlatModule_CmprTrg, IPlatRealtimeUIProvider
    {
        internal enum TriggerType
        {
            AxisSlave = 0, //伺服轴触发 每个一轴对应一路输出
            PosTrig, //位置触发板卡
        }

        internal GTCompareTrigger(TriggerType trigType, int[] devs)
        {
        }

        public int EncoderChannels { get; } = 0;

        public int LinerCmps { get; } = 0;

        public int TableCmps { get; } = 0;

        public int TimerCmps { get; } = 0;

        public int TrigChannels { get; } = 0;

        int IPlatModule_CmprTrg.EncoderCount => throw new NotImplementedException();

        int IPlatModule_CmprTrg.CompareCount => throw new NotImplementedException();

        int IPlatModule_CmprTrg.TriggerCount => throw new NotImplementedException();

        /// <summary>获取触发（输出）通道的使能状态</summary>
        int IPlatModule_CmprTrg.GetTrigEnable(int trigChn, out bool isEnabled)
        {
            isEnabled = false;
            return 0;
        }

        /// <summary>设置触发（输出）通道的使能状态</summary>
        int IPlatModule_CmprTrg.SetTrigEnable(int trigChn, bool isEnable)
        {
            throw new NotImplementedException();
        }

        int IPlatModule_CmprTrg.ResetTriggedCount(int trigChn)
        {
            throw new NotImplementedException();
        }

        int IPlatModule_CmprTrg.SoftTrigger(int trigChn)
        {
            throw new NotImplementedException();
        }

        int IPlatModule_CmprTrg.GetCmpTrigBinds(int cmpID, out int[] trgChns)
        {
            throw new NotImplementedException();
        }

        int IPlatModule_CmprTrg.GetTriggedCount(int trigChn, out int count)
        {
            throw new NotImplementedException();
        }
        public string GetErrorInfo(int errorCode)
        {
            throw new NotImplementedException();
        }
        public UcRealTimeUI GetRealtimeUI()
        {
            throw new NotImplementedException();
        }
    }
}
