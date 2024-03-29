using Cell.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Org.IMotionDaq
{
    public class GTDio : IPlatModule_DIO
    {
        internal int use_card_no = 0;
        internal GTDio(int cardid)
        {
            IsOpen = false;
            use_card_no = cardid;
            //固高是固定 16路的 dido
            DICount = 0;
            DOCount = 0;
        }

        internal void Open()
        {

            if (IsOpen)
                Close();

            //固高是固定 16路的 dido
            DICount = 16;
            DOCount = 16;

            IsOpen = true;
        }

        internal void Close()
        {
            DICount = 0;
            DOCount = 0;
            IsOpen = false;
            return;
        }

        /// <summary>
        ///输入点数量 
        /// </summary>
        /// <returns></returns>
        public int DICount { get; private set; }

        /// <summary>
        /// 输出点数量 
        /// </summary>
        /// <returns></returns>
        public int DOCount { get; private set; }

        public bool IsOpen { get; private set; }


        /// <summary>
        /// 获取单个输入点状态
        /// ArgumentOutofRange
        /// </summary>
        /// <param name="index">输入点序号，从0开始</param>
        /// <returns></returns>
        public int GetDI(int index, out bool isON)
        {
            if (index < 0 || index >= DICount)
                throw new Exception(string.Format("GetDI(index = {0}, isON) index is out of range:0~{1}", index, DICount - 1));

            isON = false;
            if (!IsOpen)
                return (int)ErrorDef.NotOpen;

            int divalue;
            if (GugaoCardHelper.GetDi(use_card_no, Gugao_mc.MC_GPI, out divalue) != 0)
                return (int)ErrorDef.InvokeFailed;
            if ((divalue & (1 << index)) != 0)
                isON = true;
            return (int)ErrorDef.Success;
        }

        /// <summary>
        /// 获取所有的输入点状态
        /// </summary>
        /// <returns>byte[0]的最低位表示第0个输入点的当前状态</returns>
        public int GetAllDIs(out bool[] isONs)
        {
            List<bool> ret = new List<bool>();

            if (!IsOpen)
            {
                isONs = new bool[] { };
                return (int)ErrorDef.NotOpen;
            }

            int divalue;
            if (GugaoCardHelper.GetDi(use_card_no, Gugao_mc.MC_GPI, out divalue) != 0)
            {
                isONs = new bool[] { };
                return (int)ErrorDef.InvokeFailed;
            }

            for (int i = 0; i < DICount; i++)
            {
                if ((divalue & (1 << i)) != 0)
                    ret.Add(true);
                else
                    ret.Add(false);
            }
            isONs = ret.ToArray();
            return (int)ErrorDef.Success;

        }

        /// <summary>
        /// 获取单个输出点状态
        /// ArgumentOutofRange
        /// </summary>
        /// <param name="index">输出点序号，从0开始</param>
        /// <returns></returns>
        public int GetDO(int index, out bool isON)
        {
            if (index < 0 || index >= DOCount)
                throw new Exception(string.Format("GetDO(index = {0}, isON) index is out of range:0~{1}", index, DOCount - 1));

            isON = false;
            if (!IsOpen)
                return (int)ErrorDef.NotOpen;

            int divalue;
            if (GugaoCardHelper.GetDo(use_card_no, Gugao_mc.MC_GPO, out divalue) != 0)
                return (int)ErrorDef.InvokeFailed;
            if ((divalue & (1 << index)) != 0)
                isON = true;
            return (int)ErrorDef.Success;
        }

        /// <summary>
        /// 获取所有的输出点状态
        /// </summary>
        /// <returns>byte[0]的最低位表示第0个输出点的当前状态</returns>
        public int GetAllDOs(out bool[] isONs)
        {
            List<bool> ret = new List<bool>();

            if (!IsOpen)
            {
                isONs = new bool[] { };
                return (int)ErrorDef.NotOpen;
            }

            int divalue;
            if (GugaoCardHelper.GetDo(use_card_no, Gugao_mc.MC_GPO, out divalue) != 0)
            {
                isONs = new bool[] { };
                return (int)ErrorDef.InvokeFailed;
            }

            for (int i = 0; i < DICount; i++)
            {
                if ((divalue & (1 << i)) != 0)
                    ret.Add(true);
                else
                    ret.Add(false);
            }
            isONs = ret.ToArray();
            return (int)ErrorDef.Success;
        }


        /// <summary>
        /// 设置单个输出点状态
        /// ArgumentOutofRange
        /// </summary>
        /// <param name="index">输出点序号，从0开始</param>
        /// <param name="bit"></param>
        /// <returns></returns>
        public int SetDO(int index, bool isON)
        {
            if (index < 0 || index >= DOCount)
                throw new ArgumentOutOfRangeException(string.Format("SetDO(index = {0}, isON) index is out of range:0~{1}", index, DOCount - 1));
            if (!IsOpen)
                return (int)ErrorDef.NotOpen;
            if (GugaoCardHelper.SetDoBit(use_card_no, Gugao_mc.MC_GPO, (short)index, isON) != 0)
                return (int)ErrorDef.InvokeFailed;
            return (int)ErrorDef.Success;
        }
        /// <summary>
        /// 按顺序一次设置多个输出点状态
        /// ArgumentNull
        /// ArgumentOutofRange
        /// </summary>
        /// <param name="bits">状态值</param>
        /// <param name="beginDOIndex">待设置输出点的起始序号（从0开始）</param>
        /// <returns></returns>
        public int SetDOs(bool[] isONs, int beginIndex, int count)
        {
            if (null == isONs)
                throw new ArgumentNullException("SetDOs(bool[] isONs ...) faied By:isONs = null");
            if (beginIndex < 0 || beginIndex >= DOCount)
                throw new ArgumentOutOfRangeException(string.Format(" SetDOs(bool[] isONs, int beginIndex = {0},int count) failed By:beginIndex is out of range 0~{1}", beginIndex, DOCount - 1));
            if (count < 0 || beginIndex + count > isONs.Count() || beginIndex + count > DOCount)
                throw new ArgumentOutOfRangeException(string.Format(" SetDOs(bool[] isONs, int beginIndex = {0},int count ={1}) failed : DOCount = {2},isONs.Count = {3}", beginIndex, count, DOCount, isONs.Length));

            if (!IsOpen)
                return (int)ErrorDef.NotOpen;

            int value = 0;
            for (int i = 0; i < isONs.Count(); i++)
            {
                if (isONs[i])
                    value |= (1 << i);
            }

            // 设置指定位 为 0
            //  value &=~ (1 << i);

            if (0 != GugaoCardHelper.SetDo(use_card_no, Gugao_mc.MC_GPO, value))
                return (int)ErrorDef.InvokeFailed;

            return (int)ErrorDef.Success;
        }


        /// <summary>
        /// 一次设置多个输出点状态
        /// ArgumentNull
        /// ArgumentOutofRange
        /// </summary>
        /// <param name="bits">待设置的状态</param>
        /// <param name="doIndexs">待设置的点位序号</param>
        /// <returns></returns>
        public int SetDOs(bool[] isONs, int[] indexs)
        {
            //  功能未开发 可后补
            return (int)ErrorDef.Unsupported;
        }

        public string GetErrorInfo(int errorCode)
        {
            string ret = "UnDefined-Error:" + errorCode;
            switch (errorCode)
            {
                case (int)ErrorDef.Success:
                    ret = "Success";
                    break;
                case (int)ErrorDef.NotOpen://卡未打开
                    ret = "Device is not open ";
                    break;
                case (int)ErrorDef.InvokeFailed: //调用库函数出错
                    ret = "Invoke Failed ";
                    break;
                case (int)ErrorDef.Unsupported: //不支持
                    ret = "Unsupported";
                    break;
                default:

                    break;
            }


            return ret;
        }
    }

}
