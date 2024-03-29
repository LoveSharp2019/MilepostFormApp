using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cell.DataModel
{
    public class ucProperty
    {
        private string name = string.Empty;
        public string Name
        {
            get { return name; }
            set { name = value; }
        }

        private object value = null;
        public object Value
        {
            get { return this.value; }
            set { this.value = value; }
        }

        Type _paramType = typeof(object);
        public Type ParamType
        {
            get { return _paramType; }
            set { _paramType = value; }
        }

        private string description = string.Empty;
        public string Description
        {
            get { return description; }
            set { description = value; }
        }

        private TypeConverter tConverter = null;
        public TypeConverter TConverter
        {
            get { return tConverter; }
            set { tConverter = value; }
        }

        private object _editor = null;
        public virtual object Editor   //属性编辑器   
        {
            get { return _editor; }
            set { _editor = value; }
        }

        public override string ToString()
        {
            return string.Format("Name:{0},Value:{1}", name.ToString(), value.ToString());
        }
    }
}
