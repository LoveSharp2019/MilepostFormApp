using Cell.DataModel;
using Cell.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sys.IStations
{
    /// <summary>
    /// 设备通道对象 ， 包含设备对象/通道类型/通道信息
    /// </summary>
    public class AppDevChannel
    {

        /// <summary>
        /// 检查DIO/轴...等设备通道是否存在
        /// </summary>
        /// <param name="category"></param>
        /// <param name="name"></param>
        /// <param name="dev"></param>
        /// <param name="ci"></param>
        /// <param name="errorInfo"></param>
        /// <returns></returns>
        public static bool CheckChannel(IDevCellType cellType, string name, out IPlatDevice dev, out IDevCellInfo ci, out string errorInfo)
        {
            dev = null;
            ci = null;
            errorInfo = "Success";


            if (string.IsNullOrEmpty(name))
            {
                errorInfo = "参数项\"cellName\"为空";
                return false;
            }
            AppDevCellNameManeger nameMgr = AppHubCenter.Instance.MDCellNameMgr;
            AppInitorManager initorMgr = AppHubCenter.Instance.InitorManager;
            IDevCellInfo cellInfo = null;
            if (cellType == IDevCellType.DO) //获取数字量输出通道信息
            {
                cellInfo = nameMgr.GetDoCellInfo(name);
                if (null == cellInfo)
                {
                    errorInfo = "设备命名表中不存在DO：" + name;
                    return false;
                }

                dev = initorMgr.GetInitor(cellInfo.DeviceID) as IPlatDevice;
                if (null == dev)
                {
                    errorInfo = "DO:\"" + name + "\" 所属设备:\"" + cellInfo.DeviceID + "\"在设备列表中不存在";
                    return false;
                }

                if (!typeof(IPlatDevice_MotionDaq).IsAssignableFrom(dev.GetType()))
                {
                    errorInfo = "DO:\"" + name + "\" 所属设备:\"" + cellInfo.DeviceID + "\"类型不是Device_MotionDaq ";
                    return false;
                }

                IPlatDevice_MotionDaq md = dev as IPlatDevice_MotionDaq;
                if (!md.IsInitOK)
                {
                    errorInfo = "DO:\"" + name + "\" 所属设备:\"" + cellInfo.DeviceID + "\"未完成初始化动作 ";
                    return false;
                }

                if (!md.IsDeviceOpen)
                {
                    errorInfo = "DO:\"" + name + "\" 所属设备:\"" + cellInfo.DeviceID + "\"未打开 ";
                    return false;
                }

                if (cellInfo.ModuleIndex >= md.DioMCount)
                {
                    errorInfo = "DO:\"" + name + "\" ModuleIndex = :" + cellInfo.ModuleIndex + "超出设备DIO模块数量: " + md.DioMCount;
                    return false;
                }

                if (cellInfo.ChannelIndex >= md.GetDio(cellInfo.ModuleIndex).DOCount)
                {
                    errorInfo = "DO:\"" + name + "\" Channel = :" + cellInfo.ChannelIndex + "超出模块DO通道数量: " + md.GetDio(cellInfo.ModuleIndex).DOCount;
                    return false;
                }
                ci = cellInfo;
                errorInfo = "Success";
                return true;

            }
            else if (cellType == IDevCellType.DI) //获取数字量输入
            {
                cellInfo = nameMgr.GetDiCellInfo(name);
                if (null == cellInfo)
                {
                    errorInfo = "设备命名表中不存在DI：" + name;
                    return false;
                }

                dev = initorMgr.GetInitor(cellInfo.DeviceID) as IPlatDevice;
                if (null == dev)
                {
                    errorInfo = "DI:\"" + name + "\" 所属设备:\"" + cellInfo.DeviceID + "\"在设备列表中不存在";
                    return false;
                }

                if (!typeof(IPlatDevice_MotionDaq).IsAssignableFrom(dev.GetType()))
                {
                    errorInfo = "DI:\"" + name + "\" 所属设备:\"" + cellInfo.DeviceID + "\"类型不是Device_MotionDaq ";
                    return false;
                }

                IPlatDevice_MotionDaq md = dev as IPlatDevice_MotionDaq;
                if (!md.IsInitOK)
                {
                    errorInfo = "DI:\"" + name + "\" 所属设备:\"" + cellInfo.DeviceID + "\"未完成初始化动作 ";
                    return false;
                }

                if (!md.IsDeviceOpen)
                {
                    errorInfo = "DI:\"" + name + "\" 所属设备:\"" + cellInfo.DeviceID + "\"未打开 ";
                    return false;
                }

                if (cellInfo.ModuleIndex >= md.DioMCount)
                {
                    errorInfo = "DI:\"" + name + "\" ModuleIndex = :" + cellInfo.ModuleIndex + "超出设备DIO模块数量: " + md.DioMCount;
                    return false;
                }

                if (cellInfo.ChannelIndex >= md.GetDio(cellInfo.ModuleIndex).DICount)
                {
                    errorInfo = "DI:\"" + name + "\" Channel = :" + cellInfo.ChannelIndex + "超出模块DI通道数量: " + md.GetDio(cellInfo.ModuleIndex).DICount;
                    return false;
                }
                ci = cellInfo;
                errorInfo = "Success";
                return true;
            }
            else if (cellType == IDevCellType.AI)
            {
                cellInfo = nameMgr.GetAiCellInfo(name);
                if (null == cellInfo)
                {
                    errorInfo = "设备命名表中不存在AI：" + name;
                    return false;
                }

                dev = initorMgr.GetInitor(cellInfo.DeviceID) as IPlatDevice;
                if (null == dev)
                {
                    errorInfo = "AI:\"" + name + "\" 所属设备:\"" + cellInfo.DeviceID + "\"在设备列表中不存在";
                    return false;
                }

                if (!typeof(IPlatDevice_MotionDaq).IsAssignableFrom(dev.GetType()))
                {
                    errorInfo = "AI:\"" + name + "\" 所属设备:\"" + cellInfo.DeviceID + "\"类型不是Device_MotionDaq ";
                    return false;
                }

                IPlatDevice_MotionDaq md = dev as IPlatDevice_MotionDaq;
                if (!md.IsInitOK)
                {
                    errorInfo = "AI:\"" + name + "\" 所属设备:\"" + cellInfo.DeviceID + "\"未完成初始化动作 ";
                    return false;
                }

                if (!md.IsDeviceOpen)
                {
                    errorInfo = "AI:\"" + name + "\" 所属设备:\"" + cellInfo.DeviceID + "\"未打开 ";
                    return false;
                }

                if (cellInfo.ModuleIndex >= md.AioMCount)
                {
                    errorInfo = "AI:\"" + name + "\" ModuleIndex = :" + cellInfo.ModuleIndex + "超出设备AIO模块数量: " + md.AioMCount;
                    return false;
                }

                if (cellInfo.ChannelIndex >= md.GetAio(cellInfo.ModuleIndex).AICount)
                {
                    errorInfo = "AI:\"" + name + "\" Channel = :" + cellInfo.ChannelIndex + "超出模块AI通道数量: " + md.GetAio(cellInfo.ModuleIndex).AICount;
                    return false;
                }
                ci = cellInfo;
                errorInfo = "Success";
                return true;
            }
            else if (cellType == IDevCellType.AO)
            {
                cellInfo = nameMgr.GetAoCellInfo(name);
                if (null == cellInfo)
                {
                    errorInfo = "设备命名表中不存在AO：" + name;
                    return false;
                }

                dev = initorMgr.GetInitor(cellInfo.DeviceID) as IPlatDevice;
                if (null == dev)
                {
                    errorInfo = "AO:\"" + name + "\" 所属设备:\"" + cellInfo.DeviceID + "\"在设备列表中不存在";
                    return false;
                }

                if (!typeof(IPlatDevice_MotionDaq).IsAssignableFrom(dev.GetType()))
                {
                    errorInfo = "AO:\"" + name + "\" 所属设备:\"" + cellInfo.DeviceID + "\"类型不是Device_MotionDaq ";
                    return false;
                }

                IPlatDevice_MotionDaq md = dev as IPlatDevice_MotionDaq;
                if (!md.IsInitOK)
                {
                    errorInfo = "AO:\"" + name + "\" 所属设备:\"" + cellInfo.DeviceID + "\"未完成初始化动作 ";
                    return false;
                }

                if (!md.IsDeviceOpen)
                {
                    errorInfo = "AO:\"" + name + "\" 所属设备:\"" + cellInfo.DeviceID + "\"未打开 ";
                    return false;
                }

                if (cellInfo.ModuleIndex >= md.AioMCount)
                {
                    errorInfo = "AO:\"" + name + "\" ModuleIndex = :" + cellInfo.ModuleIndex + "超出设备AIO模块数量: " + md.AioMCount;
                    return false;
                }

                if (cellInfo.ChannelIndex >= md.GetAio(cellInfo.ModuleIndex).AOCount)
                {
                    errorInfo = "AO:\"" + name + "\" Channel = :" + cellInfo.ChannelIndex + "超出模块AO通道数量: " + md.GetAio(cellInfo.ModuleIndex).AOCount;
                    return false;
                }
                ci = cellInfo;
                errorInfo = "Success";
                return true;
            }
            else if (cellType == IDevCellType.Axis)
            {
                cellInfo = nameMgr.GetAxisCellInfo(name);
                if (null == cellInfo)
                {
                    errorInfo = "设备命名表中不存在Axis：" + name;
                    return false;
                }

                dev = initorMgr.GetInitor(cellInfo.DeviceID) as IPlatDevice;
                if (null == dev)
                {
                    errorInfo = "Axis:\"" + name + "\" 所属设备:\"" + cellInfo.DeviceID + "\"在设备列表中不存在";
                    return false;
                }

                if (!typeof(IPlatDevice_MotionDaq).IsAssignableFrom(dev.GetType()))
                {
                    errorInfo = "Axis:\"" + name + "\" 所属设备:\"" + cellInfo.DeviceID + "\"类型不是Device_MotionDaq ";
                    return false;
                }

                IPlatDevice_MotionDaq md = dev as IPlatDevice_MotionDaq;
                if (!md.IsInitOK)
                {
                    errorInfo = "Axis:\"" + name + "\" 所属设备:\"" + cellInfo.DeviceID + "\"未完成初始化动作 ";
                    return false;
                }

                if (!md.IsDeviceOpen)
                {
                    errorInfo = "Axis:\"" + name + "\" 所属设备:\"" + cellInfo.DeviceID + "\"未打开 ";
                    return false;
                }

                if (cellInfo.ModuleIndex >= md.McMCount)
                {
                    errorInfo = "Axis:\"" + name + "\" ModuleIndex = :" + cellInfo.ModuleIndex + "超出设备轴模块数量: " + md.McMCount;
                    return false;
                }

                if (cellInfo.ChannelIndex >= md.GetMc(cellInfo.ModuleIndex).AxisCount)
                {
                    errorInfo = "Axis:\"" + name + "\" Channel = :" + cellInfo.ModuleIndex + "超出模块轴通道数量: " + md.GetMc(cellInfo.ModuleIndex).AxisCount;
                    return false;
                }
                ci = cellInfo;
                errorInfo = "Success";
                return true;
            }
            else if (cellType == IDevCellType.CmpTrig)
            {
                cellInfo = nameMgr.GetCmpTrigCellInfo(name);
                if (null == cellInfo)
                {
                    errorInfo = "设备命名表中不存在CmpTrig：" + name;
                    return false;
                }

                dev = initorMgr.GetInitor(cellInfo.DeviceID) as IPlatDevice;
                if (null == dev)
                {
                    errorInfo = "CmpTrig:\"" + name + "\" 所属设备:\"" + cellInfo.DeviceID + "\"在设备列表中不存在";
                    return false;
                }

                if (!typeof(IPlatDevice_MotionDaq).IsAssignableFrom(dev.GetType()))
                {
                    errorInfo = "CmpTrig:\"" + name + "\" 所属设备:\"" + cellInfo.DeviceID + "\"类型不是Device_MotionDaq ";
                    return false;
                }

                IPlatDevice_MotionDaq md = dev as IPlatDevice_MotionDaq;
                if (!md.IsInitOK)
                {
                    errorInfo = "CmpTrig:\"" + name + "\" 所属设备:\"" + cellInfo.DeviceID + "\"未完成初始化动作 ";
                    return false;
                }

                if (!md.IsDeviceOpen)
                {
                    errorInfo = "CmpTrig:\"" + name + "\" 所属设备:\"" + cellInfo.DeviceID + "\"未打开 ";
                    return false;
                }

                if (cellInfo.ModuleIndex >= md.CompareTriggerMCount)
                {
                    errorInfo = "CmpTrig:\"" + name + "\" ModuleIndex = :" + cellInfo.ModuleIndex + "超出设备比较触发模块数量: " + md.CompareTriggerMCount;
                    return false;
                }

                if (cellInfo.ChannelIndex >= md.GetCompareTrigger(cellInfo.ModuleIndex).CompareCount)
                {
                    errorInfo = "CmpTrig:\"" + name + "\" Channel = :" + cellInfo.ModuleIndex + "超出模块比较触发通道数量: " + md.GetCompareTrigger(cellInfo.ModuleIndex).CompareCount;
                    return false;
                }
                ci = cellInfo;
                errorInfo = "Success";
                return true;
            }          
            else
            {
                errorInfo = "不支持的参数项\"CellType\" = " + cellType.ToString();
            }
            return false;
        }

        public AppDevChannel(IDevCellType cellType, string name)
        {
            CellType = cellType;
            Name = name;

        }

        public IDevCellType CellType { get; private set; }

        public string Name { get; private set; }

        /// <summary>
        /// 只是获取通道对应的设备，不做安全性检查（如通道序号是否合法/设备是否已经打开...）
        /// </summary>
        /// <returns></returns>
        public IPlatDevice Device()
        {
            IDevCellInfo ci = CellInfo();
            if (null == ci)
                return null;
            IPlatInitializable initor = AppHubCenter.Instance.InitorManager.GetInitor(ci.DeviceID);
            switch (CellType)
            {
                case IDevCellType.DI:
                    return initor as IPlatDevice_MotionDaq;
                case IDevCellType.DO:
                    return initor as IPlatDevice_MotionDaq;
                case IDevCellType.Axis:
                    return initor as IPlatDevice_MotionDaq;
                case IDevCellType.AI:
                    return initor as IPlatDevice_MotionDaq;
                case IDevCellType.AO:
                    return initor as IPlatDevice_MotionDaq;
                case IDevCellType.CmpTrig:
                    return initor as IPlatDevice_MotionDaq;
                default:
                    break;
            }
            return null;

        }

        public IDevCellInfo CellInfo()
        {

            switch (CellType)
            {
                case IDevCellType.DI:
                    return AppHubCenter.Instance.MDCellNameMgr.GetDiCellInfo(Name);
                case IDevCellType.DO:
                    return AppHubCenter.Instance.MDCellNameMgr.GetDoCellInfo(Name);
                case IDevCellType.Axis:
                    return AppHubCenter.Instance.MDCellNameMgr.GetAxisCellInfo(Name);
                case IDevCellType.AI:
                    return AppHubCenter.Instance.MDCellNameMgr.GetAiCellInfo(Name);
                case IDevCellType.AO:
                    return AppHubCenter.Instance.MDCellNameMgr.GetAoCellInfo(Name);
                case IDevCellType.CmpTrig:
                    return AppHubCenter.Instance.MDCellNameMgr.GetCmpTrigCellInfo(Name);
                case IDevCellType.Light:
                    return AppHubCenter.Instance.MDCellNameMgr.GetLightCtrlChannelInfo(Name);
                case IDevCellType.Trig:
                    return AppHubCenter.Instance.MDCellNameMgr.GetTrigCtrlChannelInfo(Name);
                default:
                    break;
            }
            return null;

        }

        public bool IsDevOpen()
        {
            IPlatDevice dev = Device();
            if (null == dev)
                return false;


            return dev.IsDeviceOpen;
        }

        /// <summary>
        /// 打开并使能设备通道
        /// </summary>
        /// <param name="errInfo"></param>
        /// <returns></returns>
        public bool OpenDev(out string errInfo)
        {
            errInfo = "Success";
            if (string.IsNullOrEmpty(Name))
            {
                errInfo = "名称未设置/空字串";
                return false;
            }

            IDevCellInfo ci = CellInfo();
            if (null == ci)
            {
                errInfo = "通道名称:\"" + Name + "\"在设备名称表中不存在";
                return false;
            }

            IPlatDevice dev = Device();
            if (dev == null)
            {
                errInfo = "通道:\"" + Name + "\"所属设备:\"" + ci.DeviceID + "\"在设备表中不存在";
                return false;
            }

            if (!dev.IsDeviceOpen)
            {
                int errCode = dev.OpenDevice();
                if (0 != errCode)
                {
                    errInfo = "通道:\"" + Name + "\"所属设备:\"" + ci.DeviceID + "\"打开失败:" + dev.GetErrorInfo(errCode);
                    return false;
                }
            }

            return true;
        }


        ///// <summary>
        ///// 使通道可用（如伺服上电，光源/触发可用）
        ///// 建议在 打开设备->检查通道可用性 之后调用
        ///// </summary>
        ///// <param name="errorInfo"></param>
        ///// <returns></returns>
        public bool EnabledChannel(out string errorInfo)
        {
            errorInfo = "Unknown-Error";
            bool isOK = false;
            if (!CheckAvalid(out errorInfo))
                return false;
            int errorCode = 0;
            IPlatDevice dev = Device();

            switch (CellType)
            {
                case IDevCellType.DI:
                    isOK = true;
                    errorInfo = "Success";
                    break;
                case IDevCellType.DO:
                    errorInfo = "Success";
                    isOK = true;
                    break;
                case IDevCellType.Axis:
                    {

                        IPlatDevice_MotionDaq devMD = Device() as IPlatDevice_MotionDaq;
                        IDevCellInfo ci = CellInfo();
                        IPlatModule_Motion mm = devMD.GetMc(ci.ModuleIndex);
                        errorCode = mm.ServoOn(ci.ChannelIndex);
                        if (errorCode != 0)
                            errorInfo = mm.GetErrorInfo(errorCode);
                        else
                        {
                            isOK = true;
                            errorInfo = "Success";
                        }

                    }
                    break;
                case IDevCellType.AI:
                    errorInfo = "Success";
                    isOK = true;
                    break;
                case IDevCellType.AO:
                    errorInfo = "Success";
                    isOK = true;
                    break;
                case IDevCellType.CmpTrig:
                    errorInfo = "Success";
                    isOK = true;
                    break;            
                default:
                    errorInfo = "未定义的通道类型";
                    break;
            }
            return isOK;
        }



        /// <summary>
        /// 检查通道信息是否有效
        /// 建议在打开设备成功后调用
        /// </summary>
        public bool CheckAvalid(out string invalidInfo)
        {
            IPlatDevice dev = null;
            IDevCellInfo ci = null;
            return CheckChannel(CellType, Name, out dev, out ci, out invalidInfo);
        }

    }
}
