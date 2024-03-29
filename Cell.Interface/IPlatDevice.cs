using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cell.Interface
{

    /// <summary>
    /// 设备类通用接口
    /// </summary>
    public interface IPlatDevice : IPlatErrCodeMsg, IPlatInitializable
    {

        string DeviceModel { get; }

        /// <summary>
        /// 设备状态
        /// </summary>
        string DeviceStatus { get; }
        /// <summary>
        /// 打开设备
        /// </summary>
        int OpenDevice();

        /// <summary>
        /// 关闭设备
        /// </summary>
        int CloseDevice();

        /// <summary>
        /// 设备是否已经打开
        /// </summary>
        bool IsDeviceOpen { get; }
    }
}
