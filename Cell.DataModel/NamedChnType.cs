using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cell.DataModel
{
    public enum NamedChnType //需要设置的通道（名称）类型
    {
        None = 0,
        Di,     //数字输入
        Do,     //数字输出
        Axis,   //轴
        Ai, //模拟量输入
        Ao,//模拟量输出
        Camera, //相机
        LineScan //线扫激光
    }
}
