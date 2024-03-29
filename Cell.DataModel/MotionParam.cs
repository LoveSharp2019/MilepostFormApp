using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cell.DataModel
{
    public class MotionParam
    {
        /// <summary>起始速度</summary>
        public double vs { get; set; } = 10;
        /// <summary>最大速度</summary>
        public double vm { get; set; } = 20;
        /// <summary>结束速度</summary>
        public double ve { get; set; } = 10;
        /// <summary>加速度</summary>
        public double acc { get; set; } = 10;
        /// <summary>减速度</summary>
        public double dec { get; set; } = 10;
        /// <summary>s曲线因子(0~1.0)</summary>
        public double curve { get; set; } = 1;
        /// <summary>加加速</summary>
        public double jerk { get; set; } = 10;
    }

    public class HomeParam
    {
        /// <summary>归零模式  0:使用Org（原点）作为归零参考  
        public int mode { get; set; }
        /// <summary>归零运动的方向  True:正方向</summary>
        public bool dir { get; set; }
        /// <summary>加速度/减速度</summary>
        public double acc { get; set; }
        /// <summary>最大速度</summary>
        public double vm { get; set; }
        /// <summary>寻找原点速度</summary>
        public double vo { get; set; }
        /// <summary>接近速度</summary>
        public double va { get; set; }
        /// <summary>回零偏移量(回零后显示的位置)</summary>
        public double shift { get; set; }
        public double offset { get; set; }
    }

    public enum CompareMode
    {
        disable = 0,//禁用触发
        liner = 1,  //仅线性触发
        table = 2,   //仅仅点表（非线性）触发
        linetable = 3, //同时支持线性和点表模式
        timer = 4, //内部定时器模拟的脉冲信号
    }
}
