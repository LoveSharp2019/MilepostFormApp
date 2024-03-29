using Cell.DataModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cell.Interface
{

    public interface IPlatDevice_LineScan : IPlatDevice
    {
        bool IsGrabbing { get; set; }
        /// <summary>设置传感器job</summary>
        int GetSeneorJob(out string job);
        int SetSeneorJob(string tm);

        /// <summary>开始图像采集</summary>
        /// <returns></returns>
        int StartGrab();
        /// <summary>停止图像采集</summary>
        int StopGrab();

        /// <summary>实时抓拍一张图片</summary>
        int GetOneImg(out IPlat_Image img, int timeoutMilSeconds = -1);

    }
}
