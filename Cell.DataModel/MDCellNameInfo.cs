using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cell.DataModel
{
    [Serializable]
    public class MDCellNameInfo //MotionDaq's Info
    {
        public MDCellNameInfo()
        {
            MotionModules = new List<List<string>>();
            CmpTrigModles = new List<List<string>>();
            DioModules = new List<List<List<string>>>();
            AioModules = new List<List<List<string>>>();
        }
        public List<List<string>> MotionModules;
        public List<List<string>> CmpTrigModles;
        public List<List<List<string>>> DioModules;
        public List<List<List<string>>> AioModules;
    }
}
