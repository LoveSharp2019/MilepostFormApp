using Cell.DataModel;
using Cell.Interface;
using HalconDotNet;
using Sys.IStations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Sys.IStations
{
    [MyVersion("1.0.0.0")]
    [MyDisplayName("Hip采图工站")]
    public class GrabStation : IStationBase
    {
        public static string CMC_ShowImage = "ShowImage";

        //

        enum Dev_cmr
        {
            飞拍相机
        }

        enum Dev_LScan
        {
            线扫激光
        }

        public enum CustomStatus
        {
            初始化,
            复位,
            开始运行,
            开始飞拍,
            结束飞拍,
            开始线扫,
            结束线扫,
        }

        public GrabStation()
        {
            DeclearAllCustomStatus(typeof(CustomStatus));

            // 注册站内 相机
            foreach (var name in Enum.GetValues(typeof(Dev_LScan)))
            {
                DeclearDevChn(NamedChnType.LineScan, name.ToString());
            }
            foreach (var name in Enum.GetValues(typeof(Dev_cmr)))
            {
                DeclearDevChn(NamedChnType.Camera, name.ToString());
            }
        }

        CustomStatus _CurrCS
        {
            get { return (CustomStatus)CurrCustomStatus; }
            set { ChangeCustomStatus((int)value); }
        }

        protected override void PrepareWhenWorkStart()
        {
            _CurrCS = CustomStatus.初始化;

            //_cmr.RegistAcqFrameCallback(_CmrFrameCallback);

            // OpenEnableDevs();

            if (!IsNeedResetWhenStart())
                _CurrCS = CustomStatus.开始运行;
        }

        protected override void ExecuteReset()
        {
            SendMsg2Outter("开始复位");
            _CurrCS = CustomStatus.复位;


            //  清空 图片缓存


            SendMsg2Outter("复位完成");

            _CurrCS = CustomStatus.开始运行;
        }

        IPlat_Image _Image; 
        // 强行退出的时候 Runloop 还在运行
        protected override void RunLoopInWork()
        {
            Thread.Sleep(3000);
            string errInfo = "Unknown";

            switch (_CurrCS)
            {
                case CustomStatus.初始化:


                    _CurrCS = CustomStatus.复位;
                    break;
                case CustomStatus.复位:

                    // 等待PLC 信号
                    _CurrCS = CustomStatus.开始运行;
                    break;
                case CustomStatus.开始运行:

                    //  等待PLC 信号 开始信号

                    //信号 ==飞拍
                    _CurrCS = CustomStatus.开始飞拍;


                    //if (!SnapCmrImageAlias(Dev_cmr.飞拍相机.ToString(), out _Image, out errInfo))
                    //{
                    //    SendMsg2Outter("拍照失败" + errInfo);
                    //    ExitWork(WorkExitCode.Error, "拍照失败" + errInfo);
                    //}                  
                    //  NotifyCustomizeMsg(CMC_ShowImage, new object[] { _Image });

                    SendMsg2Outter("拍照完成");

                    //信号 ==线扫
                    break;
                case CustomStatus.开始飞拍:

                    // 等待PLC 结束信号
                    _CurrCS = CustomStatus.结束飞拍;
                    break;

                case CustomStatus.结束飞拍:

                    // 等待下一个信号 是飞拍 还是线扫
                    _CurrCS = CustomStatus.开始运行;
                    break;
                case CustomStatus.开始线扫:
                    //等待线扫 结束信号

                    //取图
                    _CurrCS = CustomStatus.结束线扫;
                    break;

                case CustomStatus.结束线扫:

                    // 等待下一个信号 是飞拍 还是线扫
                    _CurrCS = CustomStatus.开始运行;
                    break;
                default:
                    break;
            }
        }

        protected override void OnPause() { }
        protected override void OnResume() { }
        protected override void OnStop() { }


        /// <summary>
        /// 清除任务并退出
        /// </summary>
        protected override void CleanupWhenWorkExit()
        {
            string errInfo = "";
            //停止拍摄
            if (!EnableCmrGrabAlias(Dev_cmr.飞拍相机.ToString(), false, out errInfo))
                LogAndExitWork(WorkExitCode.Error, errInfo);
            // 关闭相机
            if (!EnableAllCmrDev(false, out errInfo))
                LogAndExitWork(WorkExitCode.Error, errInfo);
            // 关闭线扫
            if (!EnableAllLineScanDev(false, out errInfo))
                LogAndExitWork(WorkExitCode.Error, errInfo);
        }


        public override UcRealTimeUI GetRealtimeUI()
        {
            return null;
        }

        void _CmrFrameCallback(IPlatDevice_Camera cmr, IPlat_Image frame) //相机回调函数
        {
            // NotifyCustomizeMsg(CMC_ShowImage, new object[] { _markImg1 , cmr.});
        }

        //打开所有设备
        void OpenEnableDevs()
        {
            string errInfo;
            if (!OpenAllDevices(out errInfo))
                LogAndExitWork(WorkExitCode.Error, "打开设备失败，" + errInfo);

            //使能电机
            if (!EnableAllAxis(out errInfo))
                LogAndExitWork(WorkExitCode.Error, "电机使能失败，" + errInfo);

            //打开所有相机设备
            if (!EnableAllCmrDev(true, out errInfo))
                LogAndExitWork(WorkExitCode.Error, errInfo);

            //打开所有线扫相机
            if (!EnableAllLineScanDev(true, out errInfo))
                LogAndExitWork(WorkExitCode.Error, errInfo);

            //设置所有相机为软件采图模式
            if (!SetCmrTrigModeAlias(Dev_cmr.飞拍相机.ToString(), cCmrTrigMode.software, out errInfo))
                LogAndExitWork(WorkExitCode.Error, errInfo);

            //打开相机
            if (!EnableCmrGrabAlias(Dev_cmr.飞拍相机.ToString(), true, out errInfo))
                LogAndExitWork(WorkExitCode.Error, errInfo);
        }
    }
}
