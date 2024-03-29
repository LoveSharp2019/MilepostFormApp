using Cell.DataModel;
using Cell.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Org.IMotionDaq
{
    public abstract class IPlatModule_MC_Base : IPlatModule_Motion
    {
        public bool IsOpen { get; set; }

        public int AxisCount { get; set; }
        /// <summary>
        /// 报警信号
        /// </summary>
        public int MSID_ALM { get { return 0; } }
        /// <summary>
        /// 正限位
        /// </summary>
        public int MSID_PL { get { return 1; } }
        /// <summary>
        /// 负限位
        /// </summary>
        public int MSID_NL { get { return 2; } }
        /// <summary>
        /// 原点信号
        /// </summary>
        public int MSID_ORG { get { return 3; } }
        /// <summary>
        /// 急停信号
        /// </summary>
        public int MSID_EMG { get { return 4; } }

        /// <summary>
        /// 到位/零速度检出信号
        /// </summary>
        public int MSID_INP { get { return 5; } }
        /// <summary>
        /// 伺服激磁信号
        /// </summary>
        public int MSID_SVO { get { return 6; } }
        /// <summary>
        /// 软正极限信号
        /// </summary>
        public int MSID_SPL { get { return 7; } }
        /// <summary>
        /// 软负极限信号
        /// </summary>
        public int MSID_SNL { get { return 8; } }
        /// <summary>
        /// motion done信号
        /// </summary>
        public int MSID_MDN { get { return 9; } }
        /// <summary>
        /// 异常停止信号
        /// </summary>
        public int MSID_ASTP { get { return 10; } }
        /// <summary>
        /// 规划期正在运动信号
        /// </summary>
        public int MSID_MOV { get { return 11; } }

        #region  接口


        #region 获取（指定轴的）单个运动状态(IO)
        /// <summary>获取报警状态</summary>
        public abstract bool IsALM(int axis);
        /// <summary>获取伺服上电状态</summary>
        public abstract bool IsSVO(int axis);
        /// <summary>获取运动完成（停止）状态</summary>
        public abstract bool IsMDN(int axis);
        /// <summary>获取运动到位状态</summary>
        public abstract bool IsINP(int axis);
        /// <summary>获取急停信号状态</summary>
        public abstract bool IsEMG(int axis);
        /// <summary>获取正限位信号状态</summary>
        public abstract bool IsPL(int axis);
        /// <summary>获取负限位信号状态</summary>
        public abstract bool IsNL(int axis);
        /// <summary>获取原点信号状态</summary>
        public abstract bool IsORG(int axis);
        /// <summary>获取软正限位状态</summary>
        public abstract bool IsSPL(int axis);
        /// <summary>获取软负限位信号状态</summary>
        public abstract bool IsSNL(int axis);
        /// <summary>规划期正在运动信号</summary>
        public abstract bool IsMOV(int axis);

        /// <summary>
        /// 回零完成信号
        /// </summary>
        /// <param name="axis"></param>
        /// <param name="isDone"></param>
        /// <returns></returns>
        public abstract int IsHomeDone(int axis, out bool isDone);

        #endregion

        #region 获取（指定轴的）多个状态(IO)

        /// <summary>
        /// 一次获取轴的多个运动IO状态
        /// </summary>
        /// <param name="axisIndex">从0开始</param>
        /// <returns></returns>
        public abstract int GetMotionStatus(int axis, out bool[] status);
        #endregion

        #region 轴运动参数


        /// <summary>
        /// 获取轴脉冲当量
        /// 如果调用失败，会抛出一个异常
        /// </summary>
        /// <param name="axis"></param>
        /// <returns></returns>
        public abstract int GetPulseFactor(int axis, out double fact);

        /// <summary>
        /// 设置轴脉冲当量
        /// </summary>
        /// <param name="axis"></param>
        /// <param name="plsFactor"></param>
        /// <returns>调用成功时返回0，失败则返回负数</returns>
        public abstract int SetPulseFactor(int axis, double plsFactor);

        public abstract int GetSoftLimit(int axis, out double posPT, out double posNT);
        public abstract int SetSoftLimit(int axis, double posPT, double posNT);

        /// <summary>
        /// 获取单轴运动参数
        /// </summary>
        /// <param name="axis"></param>
        /// <returns></returns>
        public abstract int GetMotionParam(int axis, out MotionParam mp);
        public abstract int SetMotionParam(int axis, MotionParam mp);

        /// <summary>
        /// 单轴回零参数
        /// </summary>
        /// <param name="axis"></param>
        /// <returns></returns>
        public abstract int GetHomeParam(int axis, out HomeParam hp);

        public abstract int SetHomeParam(int axis, HomeParam hp);


        #endregion

        #region 设置/获取 轴状态数据
        public abstract int GetPrfPos(int axis, out double cmdPos);
        public abstract int SetPrfPos(int axis, double cmdPos);

        public abstract int GetEncPos(int axis, out double fbkPos);
        public abstract int SetEncPos(int axis, double fbkPos);
        #endregion

        #region 启动/停止 清除报警 归零
        /// <summary>清除轴报警信号</summary>
        public abstract int ClearAlarm(int axis);


        public abstract int ServoOn(int axis);
        public abstract int ServoOff(int axis);



        public abstract int StopAxis(int axis);
        public abstract int StopAxisEmg(int axis);

        /// <summary>停止所有轴</summary>
        public abstract void Stop();
        /// <summary>急停所有轴</summary>
        public abstract void StopEmg();


        public abstract int Home(int axis);
        #endregion

        #region 单轴运动

        /// <summary>
        /// 单轴PTP绝对运动
        /// </summary>
        /// <param name="axis"></param>
        /// <param name="position"></param>
        /// <returns></returns>
        public abstract int AbsMove(int axis, double position);

        /// <summary>
        /// 单轴PTP相对运动
        /// </summary>
        /// <param name="axis"></param>
        /// <param name="position"></param>
        /// <returns></returns>
        public abstract int RelMove(int axis, double distance);

        #endregion

        #region 单轴速度模式运动
        public abstract int Jog(int axis, double vel, bool isPositive);

        #endregion

        #endregion

        /// <summary>
        /// 获取当前异常信息
        /// </summary>
        /// <param name="errorCode"></param>
        /// <returns></returns>
        public string GetErrorInfo(int errorCode)
        {
            switch (errorCode)
            {
                case (int)ErrorDef.Success://操作成功，无错误
                    return "Success";
                case (int)ErrorDef.Unsupported://设备不支持此功能
                    return "Unsupported";
                case (int)ErrorDef.ParamError://参数错误（不支持的参数）
                    return "Param Error";
                case (int)ErrorDef.InvokeFailed://库函数调用出错
                    return "Inner API invoke failed";
                case (int)ErrorDef.Allowed://调用成功，但不是所有的参数都支持
                    return "Allowed,Not all param are supported";
                case (int)ErrorDef.InitFailedWhenOpenCard:
                    return "Not initialized when open ";
                case (int)ErrorDef.LtcCHNoIdel:
                    return "No Idel LtcCh can be used";//没有闲置的锁存通道
                case (int)ErrorDef.NotOpen:
                    return "Card is not Open";
                default://未定义的错误类型
                    return "Unknown-Error";
            }
        }
    }
}
