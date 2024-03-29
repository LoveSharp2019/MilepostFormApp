using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cell.DataModel
{
    public enum IDevCellType
    {
        unknown = 0, //未定义的
        DI,
        DO,
        Axis,
        AI,
        AO,
        CmpTrig,
        Light,
        Trig
    }
}
