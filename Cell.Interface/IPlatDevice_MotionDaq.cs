using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cell.Interface
{

    /// <summary>
    ///  运控
    /// </summary>
    public interface IPlatDevice_MotionDaq : IPlatDevice, IPlatRealtimeUIProvider, IPlatCfgUIProvider, IDisposable
    {
        /// <summary>
        /// 设备上连接的MotionCtrl（运动控制模块）的模块数量
        /// </summary>
        int McMCount { get; }

        /// <summary>
        /// 设备上连接的DIO（数字IO模块）的模块数量
        /// </summary>
        int DioMCount { get; }

        /// <summary>
        /// 设备上连接的AIO（模拟量IO采集模块）的模块数量
        /// </summary>
        int AioMCount { get; }

        /// <summary>
        /// 位置比较触发 模块数量
        /// </summary>
        int CompareTriggerMCount { get; }

        /// <summary>
        /// 获取运动控制器模块
        /// </summary>
        /// <param name="index">序号，从0开始</param>
        /// <returns></returns>
        IPlatModule_Motion GetMc(int index);

        /// <summary>
        /// 获取数字IO控制器模块
        /// </summary>
        /// <param name="index">序号，从0开始</param>
        /// <returns></returns>
        IPlatModule_DIO GetDio(int index);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="index">序号，从0开始</param>
        /// <returns></returns>
        IPlatModule_AIO GetAio(int index);


        /// <summary>
        ///   此功能 待实现 需要实际使用中 迭代
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        IPlatModule_CmprTrg GetCompareTrigger(int index);
    }
}
