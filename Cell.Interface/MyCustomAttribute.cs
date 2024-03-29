using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cell.Interface
{
    /// <summary>
    /// 名称属性
    /// </summary>
    [AttributeUsage(AttributeTargets.All, AllowMultiple = false, Inherited = false)]
    public class MyDisplayNameAttribute : Attribute
    {
        public MyDisplayNameAttribute(string name)
        {
            Name = name;
        }
        public string Name { get; set; }
    }

    /// <summary>
    /// 版本属性
    /// </summary>
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = false)]
    public class MyVersionAttribute : Attribute
    {
        public MyVersionAttribute(string info)
        {
            Info = info;
        }
        public string Info { get; set; }

    }
}
