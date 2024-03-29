using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cell.DataModel
{
    /// <summary>
    /// 图像的像素格式
    /// </summary>
    public enum IPlatImgPixFormat
    {
        Unknown = -1, //未知类型
        Mono8 = 0,
        RGB24,
        RGB24_P, //RGB24 平面
    }
}
