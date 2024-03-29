using Cell.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Org.IMotionDaq
{
    public class GTAio : IPlatModule_AIO
    {
        internal int use_card_no = 0;

        /// <summary>
        ///模拟量输入
        /// </summary>
        /// <returns></returns>
        public int AICount { get; private set; }

        /// <summary>
        /// 模拟量输出
        /// </summary>
        /// <returns></returns>
        public int AOCount { get; private set; }

        public bool IsOpen { get; private set; }

        internal GTAio(int cardid)
        {
            IsOpen = false;
            use_card_no = cardid;
            //固高是固定 8路的 AIAO
            AICount = 0;
            AOCount = 0;
        }

        internal void Open()
        {
            if (IsOpen)
                Close();

            AICount = 8;
            AOCount = 8;
            IsOpen = true;
        }

        internal void Close()
        {
            AICount = 0;
            AOCount = 0;
            IsOpen = false;
            return;
        }

        /// <summary>
        /// 获取单个输入点状态
        /// ArgumentOutofRange
        /// </summary>
        /// <param name="index">输入点序号，从0开始</param>
        /// <returns></returns>
        public int GetAI(int index, out double volt)
        {
            if (index < 0 || index >= AICount)
                throw new Exception(string.Format("GetAI(index = {0}, volt) index is out of range:0~{1}", index, AICount - 1));

            if (!IsOpen)
            {
                volt = 0;
                return (int)ErrorDef.NotOpen;
            }
            // 固高的adc 索引从1 开始 需要转换
            int opt = GugaoCardHelper.GetAdc(use_card_no, (short)(index + 1), out volt, 1);
            if (opt != 0)
                return (int)ErrorDef.InvokeFailed;
            return (int)ErrorDef.Success;
        }

        /// <summary>
        /// 获取所有通道模拟量数值
        /// </summary>
        /// <returns>byte[0]的最低位表示第0个输入点的当前状态</returns>
        public int GetAllAIs(out double[] volts)
        {
            List<double> ret = new List<double>();

            if (!IsOpen)
            {
                volts = new double[] { };
                return (int)ErrorDef.NotOpen;
            }

            double[] ValueAdc = new double[8];
            // 固高的adc 索引从1 开始 需要转换
            int opt = GugaoCardHelper.GetAdc(use_card_no, 1, out ValueAdc[0], 8);
            if (opt != 0)
            {
                volts = new double[] { };
                return (int)ErrorDef.InvokeFailed;
            }

            volts = ValueAdc.ToArray();
            return (int)ErrorDef.Success;
        }


        /// <summary>
        /// 获取单个输出点状态
        /// ArgumentOutofRange
        /// </summary>
        /// <param name="index">输出点序号，从0开始</param>
        /// <returns></returns>
        public int GetAO(int index, out double volt)
        {
            volt = 0;
            return (int)ErrorDef.Unsupported;
        }

        /// <summary>
        /// 获取所有的输出点状态
        /// </summary>
        /// <returns>byte[0]的最低位表示第0个输出点的当前状态</returns>
        public int GetAllAOs(out double[] volts)
        {
            volts = new double[] { };
            return (int)ErrorDef.Unsupported;
        }

        /// <summary>
        /// 设置单个输出点状态
        /// ArgumentOutofRange
        /// </summary>
        public int SetAO(int index, double volt)
        {
            return (int)ErrorDef.Unsupported;
        }
        /// <summary>
        /// 按顺序一次设置多个输出点状态
        /// ArgumentNull
        /// ArgumentOutofRange
        /// </summary>
        public int SetAOs(double[] volts, int beginIndex, int count)
        {
            return (int)ErrorDef.Unsupported;
        }


        /// <summary>
        /// 一次设置多个输出点状态
        /// ArgumentNull
        /// ArgumentOutofRange
        /// </summary>
        /// <param name="bits">待设置的状态</param>
        /// <param name="doIndexs">待设置的点位序号</param>
        /// <returns></returns>
        public int SetAOs(double[] volts, int[] indexs)
        {
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
