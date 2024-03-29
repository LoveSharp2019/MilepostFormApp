using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cell.DataModel
{
    public class DevNodeInfo
    {
        public DevNodeInfo()
        {
            DevID = null;
            Categoty = DevNodeCategory.MotionDaqDev;
            ModuleIndex = 0;
        }

        public DevNodeInfo(string devID, DevNodeCategory category, int moduleIndex)
        {
            DevID = devID;
            Categoty = category;
            ModuleIndex = moduleIndex;
        }


        public string DevID;
        public DevNodeCategory Categoty;
        public int ModuleIndex;

    }
}
