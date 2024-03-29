using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Org.IMotionDaq
{
    public class GugaoCardHelper
    {
        private static object lockobj = new object();//线程锁
        /// <summary>
        /// 初始化卡
        /// </summary>
        /// <returns></returns>
        public static int InitCard(int cardno, string ConfigFile)
        {
            short rtn = 0;
            //打开运动控制器。参数必须为（0,1），不能修改。
            rtn += Gugao_mc.GT_Open((short)cardno, 0, 1);
            rtn += Gugao_mc.GT_Reset((short)cardno);
            rtn += Gugao_mc.GT_LoadConfig((short)cardno, ConfigFile);
            return rtn;
        }

        /// <summary>
        /// 扩展模块初始化
        /// </summary>
        /// <param name="cardn">卡号</param>
        /// <param name="CfgFile">配置文件</param>
        /// <returns></returns>
        public static int ExtIOModuleInit(int cardn, string CfgFile)
        {
            short rtn = 0;
            rtn += Gugao_mc.GT_OpenExtMdl((short)cardn, "dll");
            // "ExtModule.cfg"
            rtn += Gugao_mc.GT_LoadExtConfig((short)cardn, CfgFile);
            rtn += Gugao_mc.GT_ResetExtMdl((short)cardn);

            //short moduleNo = extModuleNo; //0-15  扩展卡号从0开始
            //short index = diInputIndex;   //0-15
            //bool isValue = false;
            //ushort pExtDi;
            //sRtn = mc.GT_GetExtIoBit(CardNo, moduleNo, index, out pExtDi);

            // GT_GetExtIoBit
            //GT_GetExtDoValue 
            return rtn;
        }

        /// <summary>
        /// 控制卡清除状态
        /// </summary>
        /// <param name="axis"></param>
        /// <param name="count"></param>
        /// <returns></returns>
        public static int ClrSts(int cardno, short axis, short count)
        {
            return Gugao_mc.GT_ClrSts((short)cardno, axis, count);
        }

        /// <summary>
        /// 位置清零
        /// </summary>
        /// <param name="axis"></param>
        /// <param name="count"></param>
        /// <returns></returns>
        public static short ZeroPos(int cardno, short axis, short count)
        {
            return Gugao_mc.GT_ZeroPos((short)cardno, axis, count);
        }

        /// <summary>
        /// 控制卡清除状态
        /// </summary>
        /// <param name="axis"></param>
        /// <param name="count"></param>
        /// <returns></returns>
        public static int GTClose(int cardno)
        {
            return Gugao_mc.GT_Close((short)cardno);
        }

        /// <summary>
        /// 伺服使能开启
        /// </summary>
        /// <param name="axis"></param>
        /// <returns></returns>
        public static int AxisOn(int cardno, int axis)
        {
            return Gugao_mc.GT_AxisOn((short)cardno, (short)axis); // 伺服使能           
        }
        /// <summary>
        /// 伺服使能关闭
        /// </summary>
        /// <param name="axis"></param>
        /// <returns></returns>
        public static short AxisOff(int cardno, int axis)
        {
            return Gugao_mc.GT_AxisOff((short)cardno, (short)axis); // 伺服失能          
        }

        /// <summary>
        /// 获取DI值 0~15
        /// </summary>
        /// <param name="diType"></param>
        /// <param name="pValue"></param>
        /// <returns></returns>
        public static short GetDi(int cardno, short diType, out int pValue)
        {
            return Gugao_mc.GT_GetDi((short)cardno, diType, out pValue);
        }

        /// <summary>
        /// 获取D0值 0~15
        /// </summary>
        /// <param name="diType"></param>
        /// <param name="pValue"></param>
        /// <returns></returns>
        public static short GetDo(int cardno, short diType, out int pValue)
        {
            return Gugao_mc.GT_GetDo((short)cardno, diType, out pValue);
        }

        /// <summary>
        /// DI/O  输出 0~15
        /// </summary>
        /// <param name="cardno"></param>
        /// <param name="doType"></param>
        /// <param name="doIndex"></param>
        /// <param name="boolValue"></param>
        /// <returns></returns>
        public static short SetDoBit(int cardno, short doType, short doIndex, bool boolValue)
        {
            //Gugao_mc.MC_GPI MC_GPO
            return Gugao_mc.GT_SetDoBit((short)cardno, doType, doIndex, (short)(boolValue ? 1 : 0));
        }

        /// <summary>
        /// DI/O  输出 0~15
        /// </summary>
        /// <param name="cardno"></param>
        /// <param name="doType"></param>
        /// <param name="doIndex"></param>
        /// <param name="boolValue"></param>
        /// <returns></returns>
        public static short SetDo(int cardno, short doType, int intValue)
        {
            return Gugao_mc.GT_SetDo((short)cardno, doType, intValue);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="cardno"></param>
        /// <param name="adc">adc 起始通道号，取值范围：[1, 8]</param>
        /// <param name="pValue">读取的输入电压值。单位：伏特。</param>
        /// <param name="count">读取的通道数，默认为 1。1 次最多可以读取 8 路 adc 输入电压值。</param>
        /// <returns></returns>
        public static short GetAdc(int cardno, short adc, out double pValue, short count)
        {
            uint pClock;
            return Gugao_mc.GT_GetAdc((short)cardno, adc, out pValue, count, out pClock);

        }

        /// <summary>
        /// 多轴查询轴状态
        /// </summary>
        /// <param name="axis"></param>
        /// <param name="pSts"></param>
        /// <param name="count"></param>
        /// <returns></returns>
        public static short GetSts(int cardno, short axis, out int[] pSts, short count)
        {
            uint pClock;
            pSts = new int[count];
            lock (lockobj)
            {
                return Gugao_mc.GT_GetSts((short)cardno, axis, out pSts[0], count, out pClock);
            }

        }

        public static short GetSts(int cardno, short axis, out int pSts, short count)
        {
            uint pClock;
            lock (lockobj)
            {
                return Gugao_mc.GT_GetSts((short)cardno, axis, out pSts, count, out pClock);
            }

        }
        /// <summary>
        /// 单轴查询轴状态
        /// </summary>
        /// <param name="axis"></param>
        /// <param name="pSts"></param>
        /// <returns></returns>
        public static short GetSts(int cardno, short axis, out int pSts)
        {
            uint pClock;
            lock (lockobj)
            {
                return Gugao_mc.GT_GetSts((short)cardno, axis, out pSts, 1, out pClock);
            }
        }

        /// <summary>
        /// 轴回零状态
        /// </summary>
        /// <param name="cardno">卡号</param>
        /// <param name="axis">轴号</param>
        /// <param name="pHomeStatus">结构体</param>
        /// <returns></returns>
        public static short GetHomeStatus(int cardno, short axis, out Gugao_mc.THomeStatus pHomeStatus)
        {
            return Gugao_mc.GT_GetHomeStatus((short)cardno, axis, out pHomeStatus);
        }

        /// <summary>
        /// 设置软限位
        /// </summary>
        /// <param name="axis"></param>
        /// <param name="positive"></param>
        /// <param name="negative"></param>
        /// <returns></returns>
        public static short SetSoftLimit(int cardno, short axis, int positive, int negative)
        {
            return Gugao_mc.GT_SetSoftLimit((short)cardno, axis, positive, negative);
        }

        /// <summary>
        /// 获取软限位
        /// </summary>
        /// <param name="axis"></param>
        /// <param name="positive"></param>
        /// <param name="negative"></param>
        /// <returns></returns>
        public static short GetSoftLimit(int cardno, short axis, out int positive, out int negative)
        {
            return Gugao_mc.GT_GetSoftLimit((short)cardno, axis, out positive, out negative);
        }

        /// <summary>
        /// 获取规划位置
        /// </summary>
        /// <param name="profile">起始规划轴号，正整数</param>
        /// <param name="pValue"></param>
        /// <param name="count"></param>
        /// <returns></returns>
        public static short GetPrfPos(int cardno, short axis, out double pValue, short count)
        {
            uint pClock;
            lock (lockobj)
            {
                return Gugao_mc.GT_GetPrfPos((short)cardno, axis, out pValue, count, out pClock);
            }
        }
        /// <summary>
        /// 获取实际位置
        /// </summary>
        /// <param name="encoder">编码器 起始轴号 1</param>
        /// <param name="pValue"></param>
        /// <param name="count"></param>
        /// <returns></returns>
        public static short GetEncPos(int cardno, short encoder, out double pValue, short count)
        {
            uint pClock;
            lock (lockobj)
            {
                return Gugao_mc.GT_GetEncPos((short)cardno, encoder, out pValue, count, out pClock);
            }
        }

        /// <summary>
        /// 设置规划位置
        /// </summary>
        /// <param name="profile">规划轴编号，正整数。</param>
        /// <param name="pValue"></param>
        /// <param name="count"></param>
        /// <returns></returns>
        public static short SetPrfPos(int cardno, short profile, int pValue)
        {
            lock (lockobj)
            {
                return Gugao_mc.GT_SetPrfPos((short)cardno, profile, pValue);
            }
        }
        /// <summary>
        /// 设置实际位置
        /// </summary>
        /// <param name="encoder"></param>
        /// <param name="pValue"></param>
        /// <returns></returns>
        public static short SetEncPos(int cardno, short encoder, int pValue)
        {
            lock (lockobj)
            {
                return Gugao_mc.GT_SetEncPos((short)cardno, encoder, pValue);
            }
        }

        /// <summary>
        /// 设置为点位运动模式
        /// </summary>
        /// <param name="profile"></param>
        /// <returns></returns>
        public static short PrfTrap(int cardno, short profile)
        {
            return Gugao_mc.GT_PrfTrap((short)cardno, profile); //定轴为点位运动模式   
        }

        /// <summary>
        /// 设置点位运动模式参数
        /// </summary>
        /// <param name="profile"></param>
        /// <param name="pPrm"></param>
        /// <returns></returns>
        public static short SetTrapPrm(int cardno, short profile, ref Gugao_mc.TTrapPrm pPrm)
        {
            return Gugao_mc.GT_SetTrapPrm((short)cardno, profile, ref pPrm); //设置点位运动模式下的运动参数 
        }

        public static short GetTrapPrm(int cardno, short profile, out Gugao_mc.TTrapPrm pPrm)
        {
            return Gugao_mc.GT_GetTrapPrm((short)cardno, profile, out pPrm); //获取点位运动模式下的运动参数 
        }


        /// <summary>
        /// 设置目标位置
        /// </summary>
        /// <param name="profile"></param>
        /// <param name="pos"></param>
        /// <returns></returns>
        public static short SetPos(int cardno, short profile, int pos)
        {
            return Gugao_mc.GT_SetPos((short)cardno, profile, pos); //设置目标位置
        }

        /// <summary>
        /// 设置目标速度
        /// </summary>
        /// <param name="profile"></param>
        /// <param name="vel"></param>
        /// <returns></returns>
        public static short SetVel(int cardno, short profile, double vel)
        {
            return Gugao_mc.GT_SetVel((short)cardno, profile, vel);//设置目标速度  
        }

        public static short PrfJog(int cardno, short profile)
        {
            return Gugao_mc.GT_PrfJog((short)cardno, profile);
        }

        public static short SetJogPrm(int cardno, short profile, ref Gugao_mc.TJogPrm pJog)
        {
            return Gugao_mc.GT_SetJogPrm((short)cardno, profile, ref pJog);
        }


        /// <summary>
        /// 伺服使能开启
        /// </summary>
        /// <param name="axis"></param>
        /// <returns></returns>
        public static short AxisOn(int cardno, short axis)
        {
            return Gugao_mc.GT_AxisOn((short)cardno, axis); // 伺服使能


        }
        /// <summary>
        /// 伺服使能关闭
        /// </summary>
        /// <param name="axis"></param>
        /// <returns></returns>
        public static short AxisOff(int cardno, short axis)
        {
            return Gugao_mc.GT_AxisOff((short)cardno, axis); // 伺服失能          
        }

        /// <summary>
        /// 轴停止
        /// </summary>
        /// <param name="mask"> 轴号 位运算符</param>
        /// <param name="option">停止方式 位运算符 0 平滑 1 紧急</param>
        /// <returns></returns>
        public static short Stop(int cardno, int mask, int option)
        {
            return Gugao_mc.GT_Stop((short)cardno, mask, option);

        }

        /// <summary>
        /// 启动点位运动
        /// </summary>
        /// <param name="mask"></param>
        /// <returns></returns>
        public static short StartAxis(int cardno, int mask)
        {
            return Gugao_mc.GT_Update((short)cardno, mask);
        }

        /// <summary>
        /// 回Home指令
        /// </summary>
        /// <param name="axis"></param>
        /// <param name="pHomePrm"></param>
        /// <returns></returns>
        public static short GoHome(int cardno, short axis, ref Gugao_mc.THomePrm pHomePrm)
        {
            return Gugao_mc.GT_GoHome((short)cardno, axis, ref pHomePrm);

        }

        public static bool GoHomeFun(int cardno, short axis, short homeMode, double vel1, double vel2, short searchDir, short zDir, CancellationTokenSource cts = null)
        {
            short sRtn = 0;
            Gugao_mc.THomePrm tHomePrm;
            Gugao_mc.THomeStatus pHomeStatus;
            //使用home回零或home+index回零，若轴停止在home点则需要先移开home点在开启回零。由实际情况确认。
            sRtn += Gugao_mc.GT_Stop((short)cardno, 1 << (axis - 1), 0);
            sRtn += Gugao_mc.GT_ClrSts((short)cardno, axis, 1);
            sRtn += Gugao_mc.GT_ZeroPos((short)cardno, axis, 1);
            sRtn += Gugao_mc.GT_GetHomePrm((short)cardno, axis, out tHomePrm);
            tHomePrm.mode = homeMode;  //回零方式
            tHomePrm.edge = 0;//设置捕获沿： 设置捕获沿： 0-下降沿 1-上升沿 
            tHomePrm.searchHomeDistance = 0;    //搜索限位距离，0表示最大距离搜索
            tHomePrm.searchIndexDistance = 0;   //搜索index距离，0为无限大
            tHomePrm.moveDir = searchDir;       //回零方向
            tHomePrm.indexDir = zDir;           //index搜索方向
            tHomePrm.velHigh = vel1;            //搜索限位的速度
            tHomePrm.velLow = vel2;             //搜索原点、index的速度
            tHomePrm.smoothTime = 50;           //平滑时间，运动加减速平滑
            tHomePrm.acc = vel1 / 100;    //加速度【经验值】
            tHomePrm.dec = vel2 / 100;    //减速度
            tHomePrm.escapeStep = 100;//限位回零后方式时第一次找到限位反向移动距离
            tHomePrm.homeOffset = 0;    //零点偏移设置

            sRtn += GoHome((short)cardno, axis, ref tHomePrm);//启动SmartHome回原点
            do
            {
                // 超时设置
                if (cts?.Token.IsCancellationRequested == true)
                {
                    Stop(cardno, 1 << (axis - 1), 0);
                    return false;
                }

                sRtn += GetHomeStatus((short)cardno, axis, out pHomeStatus);//获取回原点状态

            } while (pHomeStatus.run == 1 && pHomeStatus.stage != 100); // 等待搜索原点停止
            Thread.Sleep(1000);     //等待电机完全停止，时间由电机调试效果确定
            sRtn += ZeroPos((short)cardno, axis, 1);  //回零完成手动清零
            if (sRtn == 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }


        /// <summary>
        /// 绝对运动
        /// </summary>
        /// <param name="cardno"></param>
        /// <param name="axisn"></param>
        /// <param name="prfpos_mm"></param>
        /// <param name="prfvel_ms"></param>
        /// <param name="acc"></param>
        /// <param name="dec"></param>
        /// <param name="cts"></param>
        /// <returns></returns>
        public static short AbsMove(int cardno, short axisn, double prfpos, double prfvel, double accdec, CancellationTokenSource cts = null)
        {

            short rRun = 0;
            Gugao_mc.TTrapPrm trapprm = new Gugao_mc.TTrapPrm();
            trapprm.acc = accdec;
            trapprm.dec = accdec;
            trapprm.smoothTime = 1;

            rRun += PrfTrap((short)cardno, axisn); //定轴为点位运动模式            
            rRun += SetTrapPrm((short)cardno, axisn, ref trapprm); //设置点位运动模式下的运动参数       
            rRun += SetPos((short)cardno, axisn, Convert.ToInt32(prfpos)); //设置目标位置
            rRun += SetVel((short)cardno, axisn, prfvel);//设置目标速度
            rRun += StartAxis((short)cardno, 1 << (axisn - 1));

            if (rRun != 0)
            {
                return rRun;
            }

            int sts = 0;
            do
            {
                if (cts?.Token.IsCancellationRequested == true)
                {
                    Stop((short)cardno, 1 << (axisn - 1), 1 << (axisn - 1));
                    return -101;
                }
                // 读取AXIS轴的状态               
                if (GetSts((short)cardno, axisn, out sts, 1) != 0)
                {
                    return rRun;
                }

                Thread.Sleep(100);

            } while ((sts & 0x400) != 0 || (sts & 0x800) == 0);// 等待AXIS轴规划停止   并且到位
            //判断 轴停止并且已经到位 才算  运行成功
            //if ((sts & 0x400) != 0) // 说明 在运动          
            //if ((sts & 0x800) == 0) // 说明 没有到位

            return rRun;

        }

        /// <summary>
        /// 相对运动
        /// </summary>
        /// <param name="axis">轴号</param>
        /// <param name="velP">速度</param>
        /// <param name="acc">加速度</param>
        /// <param name="dec">减速度</param>
        /// <param name="posP">位置</param>
        /// <returns></returns>
        public static short RelMove(int cardno, short axis, double prfpos, double prfvel, double accdec, CancellationTokenSource cts = null)  //单轴增量位置点位运动
        {
            short sRtn = 0;     //返回值
            double prfPos; //规划脉冲
            Gugao_mc.TTrapPrm trap;
            sRtn += PrfTrap((short)cardno, axis);     //设置为点位运动，模式切换需要停止轴运动。
            //若返回值为 1：若当前轴在规划运动，请调用GT_Stop停止运动再调用该指令。
            sRtn += GetTrapPrm((short)cardno, axis, out trap);       /*读取点位运动参数（不一定需要）。若返回值为 1：请检查当前轴是否为 Trap 模式
                                                                    若不是，请先调用 GT_PrfTrap 将当前轴设置为 Trap 模式。*/
            trap.acc = accdec;              //单位pulse/ms2
            trap.dec = accdec;              //单位pulse/ms2
            trap.velStart = 0;           //起跳速度，默认为0。
            trap.smoothTime = 1;         //平滑时间，使加减速更为平滑。范围[0,50]单位ms。

            sRtn += SetTrapPrm((short)cardno, axis, ref trap);//设置点位运动参数。
            sRtn += GetPrfPos((short)cardno, axis, out prfPos, 1);//读取规划位置
            sRtn += SetVel((short)cardno, axis, prfvel);        //设置目标速度
            sRtn += SetPos((short)cardno, axis, (int)(prfpos + prfPos));        //设置目标位置
            sRtn += StartAxis((short)cardno, 1 << (axis - 1));   //更新轴运动
            if (sRtn != 0)
            {
                return sRtn;
            }

            int sts = 0;
            do
            {
                if (cts?.Token.IsCancellationRequested == true)
                {
                    Stop((short)cardno, 1 << (axis - 1), 1 << (axis - 1));
                    return -101;
                }
                // 读取AXIS轴的状态               
                if (GetSts((short)cardno, axis, out sts, 1) != 0)
                {
                    return sRtn;
                }

                Thread.Sleep(100);

            } while ((sts & 0x400) != 0 || (sts & 0x800) == 0);// 等待AXIS轴规划停止   并且到位
            //判断 轴停止并且已经到位 才算  运行成功
            //if ((sts & 0x400) != 0) // 说明 在运动          
            //if ((sts & 0x800) == 0) // 说明 没有到位

            return sRtn;
        }


        /// <summary>
        /// JOG运动
        /// </summary>
        /// <param name="axis">轴号</param>
        /// <param name="velJ">速度</param>
        /// <param name="acc">加速度</param>
        /// <param name="dec">减速度</param>
        /// <returns></returns>
        public static int JOG(int cardno, short axis, double velJ, double acc, double dec)     //jog运动模式
        {
            short sRtn = 0;
            Gugao_mc.TJogPrm pJog;
            sRtn += PrfJog(cardno, axis);
            pJog.acc = acc;
            pJog.dec = dec;
            pJog.smooth = 1;//平滑系数,取值范围[0, 1),平滑系数的数值越大，加减速过程越平稳。
            sRtn += SetJogPrm(cardno, axis, ref pJog);//设置jog运动参数
            sRtn += SetVel(cardno, axis, velJ);//设置目标速度,velJd的符号决定JOG运动方向
            sRtn += StartAxis(cardno, 1 << (axis - 1));//更新轴运动           
            return sRtn;

        }

    }
}
