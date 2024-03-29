using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cell.DataModel
{
     public class IDevCellInfo
    {
        public IDevCellInfo(string devID, int moduleIndex, int channelIndex)
        {
            DeviceID = devID;
            ModuleIndex = moduleIndex;
            ChannelIndex = channelIndex;
        }
        public string DeviceID { get; private set; }
        public int ModuleIndex { get; private set; }
        public int ChannelIndex { get; private set; }

        public override bool Equals(object obj)
        {
            if (obj == null) return false;
            if (obj is IDevCellInfo)
            {
                var b = (IDevCellInfo)obj;

                return this.DeviceID == b.DeviceID && this.ModuleIndex == b.ModuleIndex && this.ChannelIndex == b.ChannelIndex;
            }

            return base.Equals(obj);
        }
        public static bool operator ==(IDevCellInfo left, IDevCellInfo right)
        {
            if ((left as object) == null)
            {
                if ((right as object) == null)
                    return true;
                return false;
            }
            return left.Equals(right);
        }

        public static bool operator !=(IDevCellInfo left, IDevCellInfo right)
        {
            return !(left == right);
        }

        public override int GetHashCode()
        {
            unchecked // Overflow is fine, just wrap
            {
                int hash = 17;
                hash = hash * 23 + DeviceID.GetHashCode();
                hash = hash * 23 + ModuleIndex.GetHashCode();
                hash = hash * 23 + ChannelIndex.GetHashCode();
                return hash;
            }
        }


    }
}
