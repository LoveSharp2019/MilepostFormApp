using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cell.DataModel
{
    public enum LogMode
    {
        None,  //忽略
        Show = 1,//只是显示
        Record = 2,//只是记录
        ShowRecord = Show | Record,//显示并且记录
    }
}
