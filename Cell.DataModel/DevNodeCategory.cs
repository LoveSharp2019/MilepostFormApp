using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cell.DataModel
{
    public enum DevNodeCategory
    {
        MotionDaqDev,
        Module, //运动控制器中各module的父节点
        MotionModule, //运动控制器中的轴模块
        DioModule,//运动控制器中的数字量IO模块
        AioModule,//运动控制器中的模拟量IO模块
        CmpTrigModule, //运动控制器中的位置比较触发模块
        TrigCtrlDev,//触发控制器设备
        LightCtrlTDev,//带触发功能的光源控制器设备
    }
}
