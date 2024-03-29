using Cell.DataModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cell.Interface
{
    public interface IPlatModule_Motion : IPlatErrCodeMsg
    {
        /// <summary>模块是否处于打开（可用）状态</summary>
        bool IsOpen { get; }
        /// <summary> 模块包含的轴数量 /// </summary>
        int AxisCount { get; }

        #region 获取（指定轴的）单个运动状态(IO)
        /// <summary>获取报警状态</summary>
        bool IsALM(int axis);
        /// <summary>获取伺服上电状态</summary>
        bool IsSVO(int axis);
        /// <summary>获取运动完成（停止）状态</summary>
        bool IsMDN(int axis);
        /// <summary>获取运动到位状态</summary>
        bool IsINP(int axis);
        /// <summary>获取急停信号状态</summary>
        bool IsEMG(int axis);
        /// <summary>获取正限位信号状态</summary>
        bool IsPL(int axis);
        /// <summary>获取负限位信号状态</summary>
        bool IsNL(int axis);
        /// <summary>获取原点信号状态</summary>
        bool IsORG(int axis);
        /// <summary>获取软正限位状态</summary>
        bool IsSPL(int axis);
        /// <summary>获取软负限位信号状态</summary>
        bool IsSNL(int axis);
        /// <summary>规划期正在运动信号</summary>
        bool IsMOV(int axis);

        int IsHomeDone(int axis, out bool isDone);

        #endregion

        #region 获取（指定轴的）多个状态(IO)
        /// <summary>
        /// 轴报警信号位置，通过GetMotionIOs（axisID）函数获取的数组中的序号
        /// 如果轴不支持此信号，应该返回一个负数
        ///  MSID=MotionStatus Index
        /// </summary>
        int MSID_ALM { get; }
        int MSID_SVO { get; }
        int MSID_MDN { get; }
        int MSID_INP { get; }
        int MSID_EMG { get; }
        int MSID_PL { get; }
        int MSID_NL { get; }
        int MSID_ORG { get; }
        int MSID_SPL { get; }
        int MSID_SNL { get; }



        /// <summary>
        /// 一次获取轴的多个运动IO状态
        /// </summary>
        /// <param name="axisIndex">从0开始</param>
        /// <returns></returns>
        int GetMotionStatus(int axis, out bool[] status);
        #endregion

        #region 轴运动参数


        /// <summary>
        /// 获取轴脉冲当量
        /// 如果调用失败，会抛出一个异常
        /// </summary>
        /// <param name="axis"></param>
        /// <returns></returns>
        int GetPulseFactor(int axis, out double fact);

        /// <summary>
        /// 设置轴脉冲当量
        /// </summary>
        /// <param name="axis"></param>
        /// <param name="plsFactor"></param>
        /// <returns>调用成功时返回0，失败则返回负数</returns>
        int SetPulseFactor(int axis, double plsFactor);

        /// <summary>
        /// 设置正负限位
        /// </summary>
        /// <param name="axis">轴号</param>
        /// <param name="posPT">正脉冲</param>
        /// <param name="posNT">负脉冲</param>
        /// <returns></returns>
        int GetSoftLimit(int axis, out double posPT, out double posNT);
        int SetSoftLimit(int axis, double posPT, double posNT);


        /// <summary>
        /// 获取单轴运动参数
        /// </summary>
        /// <param name="axis"></param>
        /// <returns></returns>
        int GetMotionParam(int axis, out MotionParam mp);
        int SetMotionParam(int axis, MotionParam mp);

        /// <summary>
        /// 单轴回零参数
        /// </summary>
        /// <param name="axis"></param>
        /// <returns></returns>
        int GetHomeParam(int axis, out HomeParam hp);

        int SetHomeParam(int axis, HomeParam hp);


        #endregion

        #region 设置/获取 轴状态数据
        /// <summary>
        /// 获取规划位置
        /// </summary>
        /// <param name="axis"></param>
        /// <param name="cmdPos"></param>
        /// <returns></returns>
        int GetPrfPos(int axis, out double cmdPos);
        /// <summary>
        /// 设置规划位置
        /// </summary>
        /// <param name="axis"></param>
        /// <param name="cmdPos"></param>
        /// <returns></returns>
        int SetPrfPos(int axis, double cmdPos);

        /// <summary>
        /// 获取 实际位置
        /// </summary>
        /// <param name="axis"></param>
        /// <param name="fbkPos"></param>
        /// <returns></returns>
        int GetEncPos(int axis, out double fbkPos);

        /// <summary>
        /// 设置实际位置
        /// </summary>
        /// <param name="axis"></param>
        /// <param name="fbkPos"></param>
        /// <returns></returns>
        int SetEncPos(int axis, double fbkPos);
        #endregion

        #region 启动/停止 清除报警 归零
        /// <summary>清除轴报警信号</summary>
        int ClearAlarm(int axis);


        int ServoOn(int axis);
        int ServoOff(int axis);



        int StopAxis(int axis);
        int StopAxisEmg(int axis);

        /// <summary>停止所有轴</summary>
        void Stop();
        /// <summary>急停所有轴</summary>
        void StopEmg();


        int Home(int axis);
        #endregion

        #region 单轴运动

        /// <summary>
        /// 单轴PTP绝对运动
        /// </summary>
        /// <param name="axis"></param>
        /// <param name="position"></param>
        /// <returns></returns>
        int AbsMove(int axis, double position);

        /// <summary>
        /// 单轴PTP相对运动
        /// </summary>
        /// <param name="axis"></param>
        /// <param name="position"></param>
        /// <returns></returns>
        int RelMove(int axis, double distance);


        #endregion

        #region 单轴速度模式运动
        int Jog(int axis, double vel,bool isPositive);

        #endregion

    }
}
