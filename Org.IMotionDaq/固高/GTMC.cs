using Cell.DataModel;
using Cell.Tools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Org.IMotionDaq
{
    public class GTMC : IPlatModule_MC_Base
    {
        /// <summary>
        /// 轴数量
        /// </summary>
        public int use_axis_num { get; private set; }
        /// <summary>
        /// 卡号
        /// </summary>
        public int use_card_no { get; private set; }

        /// <summary>
        /// 控制卡 各个轴 会零等参数保存
        /// </summary>
        public AppCfgFromXml _cfgFile = null;

        DictionaryEx<string, object> _dictMC = null;//用于存储本控制卡的相关拓展参数（脉冲当量）


        /// <summary>
        /// 脉冲当量关键字
        /// </summary>
        string factorKeyName = "PulseFactor";

        /// <summary>
        ///  加载
        /// </summary>
        /// <param name="cardid">卡号</param>
        /// <param name="axisnum">轴的个数</param>
        /// <param name="CfgFile">配置文件</param>
        internal GTMC(int cardid, int axisnum, AppCfgFromXml CfgFile)
        {
            AxisCount = 0;
            use_axis_num = axisnum;
            use_card_no = cardid;
            _cfgFile = CfgFile;
            IsOpen = false;
        }

        MotionParam[] _motionParams;
        double[] pulseFactors = null; // 脉冲当量

        // 这里需要加载配置文件里面的资料 一般都是 回零模式等相关信息
        internal void Open()
        {
            double Axs_Param = 0;

            AxisCount = use_axis_num;
            if (AxisCount == 0)
            {
                IsOpen = true;
                return;
            }
            lock (_cfgFile)
            {
                if (!_cfgFile.ContainsItem("Card_" + use_card_no))
                    _cfgFile.AddItem("Card_" + use_card_no, new DictionaryEx<string, object>());
                _dictMC = _cfgFile.GetItemValue("Card_" + use_card_no) as DictionaryEx<string, object>;


                if (!_dictMC.ContainsKey(factorKeyName))
                {
                    pulseFactors = new double[AxisCount];
                    for (int i = 0; i < AxisCount; i++)
                        pulseFactors[i] = 1;
                    _dictMC.Add(factorKeyName, pulseFactors);
                }
                else
                {
                    double[] daTmp = _dictMC[factorKeyName] as double[];
                    if (daTmp.Length < AxisCount)
                    {
                        _dictMC.Remove(factorKeyName);
                        pulseFactors = new double[AxisCount];
                        for (int i = 0; i < AxisCount; i++)
                            pulseFactors[i] = 1;
                        _dictMC.Add(factorKeyName, pulseFactors);
                    }
                    else
                        pulseFactors = _dictMC[factorKeyName] as double[];
                }

            }

            _motionParams = new MotionParam[AxisCount];
            for (int i = 0; i < AxisCount; i++)
            {
                _motionParams[i] = new MotionParam();
                _motionParams[i].vs = Axs_Param / pulseFactors[i];
                _motionParams[i].vm = Axs_Param / pulseFactors[i];
                _motionParams[i].ve = Axs_Param / pulseFactors[i];
                _motionParams[i].acc = Axs_Param / pulseFactors[i];
                _motionParams[i].dec = Axs_Param / pulseFactors[i];
                _motionParams[i].curve = Axs_Param;
            }
            IsOpen = true;
        }

        internal void Close()
        {
            AxisCount = 0;
            _motionParams = null;
            IsOpen = false;
        }

        /// <summary>
        ///  检测属性
        /// </summary>
        /// <param name="axis">序号0~3  或者 0~7</param>
        void _CheckAxisEnable(int axis)
        {
            if (!IsOpen)
                throw new Exception("GTMC is not Open!");
            if (axis < 0 || axis >= AxisCount)
                throw new ArgumentOutOfRangeException(string.Format("GTMC failed:axis={0} is out of range(Axis Count = {1})", axis, AxisCount));
        }

        #region 获取（指定轴的）单个运动状态(IO)

        /// <summary>
        /// 是否报警
        /// </summary>
        /// <param name="axis"></param>
        /// <returns></returns>
        public override bool IsALM(int axis)
        {
            _CheckAxisEnable(axis);
            int sts = 0;
            if (GugaoCardHelper.GetSts(use_card_no, (short)axis, out sts) != 0)
                throw new Exception("GTMC 'IsALM' InvokeFailed!");

            return (sts & 0x2) == 0;
        }

        public override bool IsEMG(int axis)
        {
            _CheckAxisEnable(axis);
            int sts = 0;
            if (GugaoCardHelper.GetSts(use_card_no, (short)axis, out sts) != 0)
                throw new Exception("GTMC 'IsEMG' InvokeFailed!");
            //0x100 急停信号
            return (sts & 0x100) == 0;
        }

        public override bool IsINP(int axis)
        {
            _CheckAxisEnable(axis);
            int sts = 0;
            if (GugaoCardHelper.GetSts(use_card_no, (short)axis, out sts) != 0)
                throw new Exception("GTMC 'IsINP' InvokeFailed!");
            return (sts & 0x800) == 0;
        }

        public override bool IsORG(int axis)
        {
            throw new Exception("GTMC 'IsEMG' InvokeFailed!");
        }

        public override bool IsPL(int axis)
        {
            _CheckAxisEnable(axis);
            int sts = 0;
            if (GugaoCardHelper.GetSts(use_card_no, (short)axis, out sts) != 0)
                throw new Exception("GTMC 'IsPL' InvokeFailed!");
            return (sts & 0x20) == 0;
        }

        public override bool IsNL(int axis)
        {
            _CheckAxisEnable(axis);
            int sts = 0;
            if (GugaoCardHelper.GetSts(use_card_no, (short)axis, out sts) != 0)
                throw new Exception("GTMC 'IsNL' InvokeFailed!");
            return (sts & 0x40) == 0;
        }

        public override bool IsSNL(int axis)
        {
            // 固高软限位 和硬限位 同一个信号
            _CheckAxisEnable(axis);
            int sts = 0;
            if (GugaoCardHelper.GetSts(use_card_no, (short)axis, out sts) != 0)
                throw new Exception("GTMC 'IsNL' InvokeFailed!");
            return (sts & 0x40) == 0;
        }

        public override bool IsSPL(int axis)
        {
            _CheckAxisEnable(axis);
            int sts = 0;
            if (GugaoCardHelper.GetSts(use_card_no, (short)axis, out sts) != 0)
                throw new Exception("GTMC 'IsPL' InvokeFailed!");
            return (sts & 0x20) == 0;
        }

        public override bool IsSVO(int axis)
        {
            _CheckAxisEnable(axis);
            int sts = 0;
            if (GugaoCardHelper.GetSts(use_card_no, (short)axis, out sts) != 0)
                throw new Exception("GTMC 'IsSVO' InvokeFailed!");
            return (sts & 0x200) == 0;
        }

        public override bool IsMDN(int axis)
        {
            _CheckAxisEnable(axis);
            int sts = 0;
            if (GugaoCardHelper.GetSts(use_card_no, (short)axis, out sts) != 0)
                throw new Exception("GTMC 'IsMDN' InvokeFailed!");
            //0x80 平滑停止信号
            return (sts & 0x80) == 0;
        }


        /// <summary>
        /// 规划期是否运动中
        /// </summary>
        /// <param name="axis"></param>
        /// <returns>true  正在运动中</returns>
        public override bool IsMOV(int axis)
        {
            _CheckAxisEnable(axis);
            int sts = 0;
            if (GugaoCardHelper.GetSts(use_card_no, (short)axis, out sts) != 0)
                throw new Exception("GTMC 'IsMOV' InvokeFailed!");
            //0x400 正在运动信号
            return (sts & 0x400) == 0;
        }

        /// <summary>
        /// 是否回零
        /// </summary>
        /// <param name="axis"></param>
        /// <param name="isDone">true 说明执行过回零操作</param>
        /// <returns></returns>
        public override int IsHomeDone(int axis, out bool isDone)
        {
            Gugao_mc.THomeStatus tHomeStatus = new Gugao_mc.THomeStatus();//回原点状态结构体
            if (GugaoCardHelper.GetHomeStatus(use_card_no, (short)axis, out tHomeStatus) != 0)
                throw new Exception("GTMC 'IsHomeDone' InvokeFailed!");
            //stage=100  回零成功
            isDone = tHomeStatus.stage == 100;
            return (int)ErrorDef.Success;
        }
        #endregion

        #region 获取（指定轴的）多个运动状态(IO)
        public override int GetMotionStatus(int axis, out bool[] ret)
        {
            _CheckAxisEnable(axis);
            int sts = 0;
            if (GugaoCardHelper.GetSts(use_card_no, (short)axis, out sts) != 0)
                throw new Exception("GTMC 'GetMotionStatus' InvokeFailed!");

            ret = new bool[12];
            ret[MSID_ALM] = (sts & 0x2) == 0;
            ret[MSID_PL] = (sts & 0x20) == 0;
            ret[MSID_NL] = (sts & 0x40) == 0;
            bool isDone = false;
            if (IsHomeDone(axis, out isDone) == 0)
                ret[MSID_ORG] = isDone;
            ret[MSID_SVO] = (sts & 0x200) == 0;
            ret[MSID_INP] = (sts & 0x800) == 0;
            ret[MSID_EMG] = (sts & 0x100) == 0;

            ret[MSID_SPL] = (sts & 0x20) == 0;
            ret[MSID_SNL] = (sts & 0x40) == 0;
            ret[MSID_MDN] = (sts & 0x80) == 0;
            ret[MSID_ASTP] = (sts & 0x100) == 0;
            ret[MSID_MOV] = (sts & 0x400) == 0;

            return (int)ErrorDef.Success;
        }
        #endregion

        #region 轴运动回零 等参数 

        public override int GetPulseFactor(int axis, out double fact)
        {
            fact = 0;
            _CheckAxisEnable(axis);
            fact = pulseFactors[axis];
            return (int)ErrorDef.Success;
        }

        public override int SetPulseFactor(int axis, double plsFactor)
        {
            _CheckAxisEnable(axis);

            // 浅复制  pulseFactors  实际运用的时候 查看是否修改了 cfg 里面的参数
            pulseFactors[axis] = plsFactor;
            lock (_cfgFile)
                _cfgFile.Save();

            return (int)ErrorDef.Success;
        }

        /// <summary>
        /// 获取正软极限
        /// </summary>
        /// <param name="axis"></param>
        /// <param name="enable"></param>
        /// <param name="pos"></param>
        /// <returns></returns>
        public override int GetSoftLimit(int axis, out double posPT, out double posNT)
        {
            posNT = posPT = 0;
            _CheckAxisEnable(axis);

            if ((pulseFactors[axis]) <= 0)
                throw new ArgumentOutOfRangeException("GetSoftLimit(axis ,...) fialed By:factor<=0,factor=" + pulseFactors[axis]);
            int negative = 0;
            int positive = 0;
            if (GugaoCardHelper.GetSoftLimit(0, (short)axis, out positive, out negative) != 0)
                throw new Exception("GTMC 'GetSoftLimit' InvokeFailed!");
            posPT = positive / ((double)pulseFactors[axis]);
            posNT = negative / ((double)pulseFactors[axis]);
            return (int)ErrorDef.Success;
        }

        /// <summary>
        /// 设置正软极限
        /// </summary>
        /// <param name="axis"></param>
        /// <param name="enable"></param>
        /// <param name="pos"></param>
        /// <returns></returns>
        public override int SetSoftLimit(int axis, double posPT, double posNT)
        {
            _CheckAxisEnable(axis);
            if ((pulseFactors[axis]) <= 0)
                throw new ArgumentOutOfRangeException("SetSoftLimit(axis ,...) fialed By:factor<=0,factor=" + pulseFactors[axis]);

            if (GugaoCardHelper.SetSoftLimit(0, (short)axis, (int)(posPT * ((double)pulseFactors[axis])), (int)(posNT * ((double)pulseFactors[axis]))) != 0)
                throw new Exception("GTMC 'SetSoftLimit' InvokeFailed!");
            return (int)ErrorDef.Success;
        }


        /// <summary>
        /// 获取运动参数
        /// </summary>
        /// <param name="axis"></param>
        /// <param name="mp"></param>
        /// <returns></returns>
        public override int GetMotionParam(int axis, out MotionParam mp)
        {
            _CheckAxisEnable(axis);
            mp = _motionParams[axis];
            return (int)ErrorDef.Success;
        }

        /// <summary>
        /// 设置单轴运动参数
        /// </summary>
        /// <param name="axis"></param>
        /// <param name="mp"></param>
        /// <returns></returns>
        public override int SetMotionParam(int axis, MotionParam mp)
        {
            _CheckAxisEnable(axis);


            _motionParams[axis] = mp;



            return (int)ErrorDef.Success;
        }

        public override int GetHomeParam(int axis, out HomeParam pm)
        {
            int iAxs_Param = 0;
            double dAxs_Param = 0;
            pm = new HomeParam();


            pm.offset = 0;
            return (int)ErrorDef.Success;
        }


        /// <summary>
        /// 设置单轴回零参数
        /// </summary>
        /// <param name="axis"></param>
        /// <param name="hp"></param>
        /// <returns></returns>
        public override int SetHomeParam(int axis, HomeParam hp)
        {
            _CheckAxisEnable(axis);


            return (int)ErrorDef.Success;
        }
        #endregion

        #region 设置/获取轴位置数据
        /// <summary>
        /// 获取规划位置
        /// </summary>
        /// <param name="axis"></param>
        /// <param name="cmdPos"></param>
        /// <returns></returns>
        public override int GetPrfPos(int axis, out double cmdPos)
        {
            cmdPos = 0;
            _CheckAxisEnable(axis);

            if ((pulseFactors[axis]) <= 0)
                throw new ArgumentOutOfRangeException("GetPrfPos(axis ,...) fialed By:factor<=0,factor=" + pulseFactors[axis]);

            if (GugaoCardHelper.GetPrfPos(use_card_no, (short)axis, out cmdPos, 1) != 0)
                return (int)ErrorDef.InvokeFailed;

            cmdPos = cmdPos / ((double)pulseFactors[axis]);
            return (int)ErrorDef.Success;
        }

        /// <summary>
        /// 设置规划位置
        /// </summary>
        /// <param name="axis"></param>
        /// <param name="cmdPos"></param>
        /// <returns></returns>
        public override int SetPrfPos(int axis, double cmdPos)
        {
            _CheckAxisEnable(axis);

            int pos = 0;
            if (GugaoCardHelper.SetPrfPos(use_card_no, (short)axis, pos) != 0)
                return (int)ErrorDef.InvokeFailed;

            cmdPos = pos / ((double)pulseFactors[axis]);

            return (int)ErrorDef.Success;
        }

        /// <summary>
        /// 获取编码器位置
        /// </summary>
        /// <param name="axis"></param>
        /// <param name="fbkPos"></param>
        /// <returns></returns>
        public override int GetEncPos(int axis, out double fbkPos)
        {
            fbkPos = 0;
            _CheckAxisEnable(axis);

            if ((pulseFactors[axis]) <= 0)
                throw new ArgumentOutOfRangeException("GetEncPos(axis ,...) fialed By:factor<=0,factor=" + pulseFactors[axis]);

            if (GugaoCardHelper.GetEncPos(use_card_no, (short)axis, out fbkPos, 1) != 0)
                return (int)ErrorDef.InvokeFailed;

            fbkPos = fbkPos / ((double)pulseFactors[axis]);
            return (int)ErrorDef.Success;
        }

        /// <summary>
        /// 设置编码器位置
        /// </summary>
        /// <param name="axis"></param>
        /// <param name="fbkPos"></param>
        /// <returns></returns>
        public override int SetEncPos(int axis, double fbkPos)
        {
            _CheckAxisEnable(axis);

            int pos = 0;
            if (GugaoCardHelper.SetEncPos(use_card_no, (short)axis, pos) != 0)
                return (int)ErrorDef.InvokeFailed;

            fbkPos = pos / ((double)pulseFactors[axis]);
            return (int)ErrorDef.Success;
        }
        #endregion

        #region 启动/停止 清除报警 归零
        public override int ClearAlarm(int axis)
        {
            _CheckAxisEnable(axis);

            if (!IsOpen)
            {
                return (int)ErrorDef.InvokeFailed;
            }

            if (GugaoCardHelper.ClrSts(use_card_no, (short)axis, 1) != 0)
                return (int)ErrorDef.InvokeFailed;

            return (int)ErrorDef.Success;
        }

        /// <summary>
        /// 使能
        /// </summary>
        /// <param name="axis"></param>
        /// <returns></returns>
        public override int ServoOn(int axis)
        {
            _CheckAxisEnable(axis);
            if (!IsOpen)
            {
                return (int)ErrorDef.InvokeFailed;
            }

            if (GugaoCardHelper.AxisOn(use_card_no, (short)axis) != 0)
                return (int)ErrorDef.InvokeFailed;

            return (int)ErrorDef.Success;
        }

        /// <summary>
        /// 励磁OFF
        /// </summary>
        /// <param name="axis"></param>
        /// <returns></returns>
        public override int ServoOff(int axis)
        {
            _CheckAxisEnable(axis);
            if (!IsOpen)
            {
                return (int)ErrorDef.InvokeFailed;
            }

            if (GugaoCardHelper.AxisOff(use_card_no, (short)axis) != 0)
                return (int)ErrorDef.InvokeFailed;

            return (int)ErrorDef.Success;
        }

        /// <summary>
        /// 所有轴平滑停止
        /// </summary>
        public override void Stop()
        {
            for (int i = 0; i < AxisCount; i++)
            {
                GugaoCardHelper.Stop(use_card_no, 1 << i, 0);
            }
        }

        /// <summary>
        /// 单轴停止
        /// </summary>
        /// <param name="axis"></param>
        /// <returns></returns>
        public override int StopAxis(int axis)
        {
            _CheckAxisEnable(axis);
            if (GugaoCardHelper.Stop(use_card_no, 1 << axis, 0) != 0)
                return (int)ErrorDef.InvokeFailed;
            return (int)ErrorDef.Success;
        }

        /// <summary>
        /// 单轴急停
        /// </summary>
        /// <param name="axis"></param>
        /// <returns></returns>
        public override int StopAxisEmg(int axis)
        {
            _CheckAxisEnable(axis);
            if (GugaoCardHelper.Stop(use_card_no, 1 << axis, 1 << axis) != 0)
                return (int)ErrorDef.InvokeFailed;
            return (int)ErrorDef.Success;
        }

        /// <summary>
        /// 所有轴急停
        /// </summary>
        public override void StopEmg()
        {
            for (int i = 0; i < AxisCount; i++)
            {
                GugaoCardHelper.Stop(use_card_no, 1 << i, 1 << i);
            }
        }

        /// <summary>
        /// 单轴回原点
        /// </summary>
        /// <param name="axis"></param>
        /// <returns></returns>
        public override int Home(int axis)
        {
            _CheckAxisEnable(axis);

            // Gugao_mc.HOME_MODE_LIMIT 

            bool isValue = false;
            /*设置回零参数*/
            short homeDir = 1;  //回零方向【1为正方向，-1 为负方向】
            short indexDir = 1; //搜索index方向【1为正方向，-1 为负方向】
            short homeEdge = 0; //回零home index 信号触发边沿
            double velHigh = 80;//寻找限位速度
            double velLow = 10;  //寻找home、index速度
            short mode = Gugao_mc.HOME_MODE_LIMIT_HOME;    //11限位+HOME回原点,10限位回原点

            if (!GugaoCardHelper.GoHomeFun(use_card_no, (short)axis, mode, velHigh, velLow, homeDir, indexDir))
                return (int)ErrorDef.InvokeFailed;
            return (int)ErrorDef.Success;
        }
        #endregion

        #region 单轴运动

        public override int AbsMove(int axis, double position)
        {
            _CheckAxisEnable(axis);
            double vel = 0;    //运行速度
            double accdec = 0; //加减速
            if (GugaoCardHelper.AbsMove(use_card_no, (short)axis, position * (double)pulseFactors[axis], vel * (double)pulseFactors[axis], accdec) != 0)
                return (int)ErrorDef.InvokeFailed;
            return (int)ErrorDef.Success;
        }

        public override int RelMove(int axis, double distance)
        {
            _CheckAxisEnable(axis);
            double vel = 0;    //运行速度
            double accdec = 0; //加减速
            if (GugaoCardHelper.RelMove(use_card_no, (short)axis, distance * (double)pulseFactors[axis], vel * (double)pulseFactors[axis], accdec) != 0)
                return (int)ErrorDef.InvokeFailed;
            return (int)ErrorDef.Success;
        }

        /// <summary>
        /// 单轴jog运动
        /// </summary>
        /// <param name="axis"></param>
        /// <param name="isPositive"></param>
        /// <returns></returns>
        public override int Jog(int axis, double velocity, bool isPositive)
        {
            _CheckAxisEnable(axis);
            double vel = velocity;
            double acc = Math.Abs(velocity * 10);
            double dec = Math.Abs(velocity * 10);
            if (GugaoCardHelper.JOG(use_card_no, (short)axis, vel * (double)pulseFactors[axis], acc * (double)pulseFactors[axis], dec * (double)pulseFactors[axis]) != 0)
                return (int)ErrorDef.InvokeFailed;
            return (int)ErrorDef.Success;
        }
        #endregion   
    }
}
