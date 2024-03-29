using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Org.IMotionDaq
{
    class readMe
    {
        /* 引用库
         *  gts.dll
         *  LAFunc.dll
         *  PIFunc.dll
         *  VFunc.dll
         */

        /**  固高控制卡 常用信息
         *  固高分四轴卡等等 本示例使用 Multiple 卡 多卡号
         *  
         *  固高分多个卡 区分卡 用卡号需要加载不同的cfg文件 （初始板卡自带IO）
         *  每个卡可以扩展多个IO模块 加载不同的cfg文件 区分用Model号
         *  固高 4/8 轴卡 通用 16 路 di/do  
         *  
         *  模拟量输出 8路轴控接口（-10V~+10V）
                       4路非轴接口（0~10V
         *  
         *  
GTS-400-PG-VB-PCI 4轴脉冲标准控制
GTS-400-PV-VB-PCI 4轴脉冲+模拟量标准控制
GTS-800-PG-VB-PCI 8轴脉冲标准控制
GTS-800-PV-VB-PCI 8轴脉冲+模拟量标准控制
GTS-400-PG-VB-PCI-LASER 4轴脉冲激光控制
GTS-400-PV-VB-PCI-LASER 4轴脉冲+模拟量激光控制
GTS-800-PG-VB-PCI-LASER 8轴脉冲激光控制
GTS-800-PV-VB-PCI-LASER 8轴脉冲+模拟量激光控制

        扩展模块
        HCB2-1616-DTD01
        HCB2-1616-DTS01  16DI/16DO
         */

        /* 常见API 返回Code
0 指令执行成功 无
1 指令执行错误 1. 检查当前指令的执行条件是否满足
2 license 不支持 1. 如果需要此功能，请与生产厂商联系。
7 指令参数错误 1．检查当前指令输入参数的取值
8 不支持该指令 DSP 固件不支持该指令对应的功能
-1~-5 主机和运动控制器通讯失败
1. 是否正确安装运动控制器驱动程序
2. 检查运动控制器是否接插牢靠
3. 更换主机
4. 更换控制器
5. 运动控制器的金手指是否干净
-6 打开控制器失败
1. 是否正确安装运动控制器驱动程序
2. 是否调用了 2 次 GT_Open 指令
3. 其他程序是否已经打开运动控制器，或进程
中是否还驻留着打开控制器的程序
-7 运动控制器没有响应 1. 更换运动控制器
-8 多线程资源忙 指令在线程里执行超时才返回，有可能是 PCI 通
信异常，导致指令无法及时返回
* **/

        /* 轴状态
         * 
            //GlobalVariable.Xinfo.bFlagAlarm = (intvalue[0] & 0x2) == 0;          // 伺服报警
            //GlobalVariable.Xinfo.bFlagMError = (intvalue[0] & 0x10) == 0;        //运动出错
            //GlobalVariable.Xinfo.bFlagPosLimit = (intvalue[0] & 0x20) == 0;      // 正向限位
            //GlobalVariable.Xinfo.bFlagNegLimit = (intvalue[0] & 0x40) == 0;      // 负向限位
            //GlobalVariable.Xinfo.bFlagServoOn = (intvalue[0] & 0x200) != 0;      // 伺服使能标志
            //GlobalVariable.Xinfo.bFlagMotion = (intvalue[0] & 0x400) != 0;       // 规划器正在运动标志
            //GlobalVariable.Xinfo.bFlagInPos = (intvalue[0] & 0x800) != 0; //到位信号
         */
    }
}
