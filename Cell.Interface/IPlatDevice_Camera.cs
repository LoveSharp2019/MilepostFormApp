using Cell.DataModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cell.Interface
{
    public delegate void CmrAcqFrameDelegate(IPlatDevice_Camera cmr, IPlat_Image frame);

    public interface IPlatDevice_Camera : IPlatDevice
    {
        /// <summary>相机是否处于触发模式</summary>

        int GetTrigMode(out cCmrTrigMode tm);
        int SetTrigMode(cCmrTrigMode tm);

        /// <summary>X方向镜像</summary>       
        int GetReverseX(out bool enabled);
        int SetReverseX(bool enabled);

        /// <summary>Y方向镜像</summary>     
        int GetReverseY(out bool enabled);
        int SetReverseY(bool enabled);

        /// <summary>设置相机增益参数</summary>
        int SetGain(double value);
        /// <summary>获取相机增益参数</summary>
        int GetGain(out double value);

        /// <summary>设置相机曝光时间 </summary>
        int SetExposureTime(double ExposureTimeNum);
        /// <summary>获取相机曝光时间</summary>
        int GetExposureTime(out double ExposureTimeNum);

        /// <summary>内部图片缓存的最大数量</summary>
        int GetBuffSize(out int maxNum);
        int SetBuffSize(int maxNum);
      

        /// <summary>图像采集回调函数</summary>
        bool IsRegistAcqFrameCallback { get; } // 是否已经注册了回调函数
        int RegistAcqFrameCallback(CmrAcqFrameDelegate callback);
        void RemoveAcqFrameCallback(CmrAcqFrameDelegate callback);
        void ClearAcqFrameCallback();



        int SetBalanceAuto(bool enable);//关闭自动白平衡

        bool IsGrabbing { get; }
        /// <summary>开始图像采集</summary>
        /// <returns></returns>
        int StartGrab();
        /// <summary>停止图像采集</summary>
        int StopGrab();

        /// <summary>实时抓拍一张图片</summary>
        int GrabOne(out IPlat_Image img, int timeoutMilSeconds = -1);

        /// <summary>软触发一次使相机拍照</summary>
        int SoftwareTrig();


        /// <summary>当前已缓存的帧数</summary>
        int CurrBuffCount();
        int ClearBuff();

        /// <summary>
        /// 从队列中取出指定数量的图片
        /// </summary>
        /// <param name="images"></param>
        /// <param name="framecount"></param>
        /// <returns></returns>
        int DeqFrames(out IPlat_Image[] images, int framecount, int timeoutMilSec);

    }
}
