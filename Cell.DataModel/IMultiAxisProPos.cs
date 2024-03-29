using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cell.DataModel
{
    public class Items
    {
        Items()
        {
            Name = "";
            Value = 0;
        }

        public static Items Create(string name, double value)
        {
            Items ret = new Items();
            ret.Name = name;
            ret.Value = value;
            return ret;
        }

        public string Name { get; set; }
        public double Value { get; set; }
    }

    /// <summary>
    /// 定义 单功能点多轴规划点位
    /// </summary>
    public class IMultiAxisProPos
    {
        public IMultiAxisProPos()
        {
            Name = "";
            Positions = new List<Items>();
        }
        /// <summary>
        /// 点位名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 点位中各轴位置
        /// </summary>
        public List<Items> Positions;


        /// <summary>
        /// 功能点中 所有轴的位置
        /// </summary>
        public string[] AxisNames
        {
            get
            {
                List<string> ret = new List<string>();
                foreach (Items ap in Positions)
                    ret.Add(ap.Name);
                return ret.ToArray();
            }
        }

        /// <summary>
        /// 功能点中 是否包含轴
        /// </summary>
        /// <param name="axisName"></param>
        /// <returns></returns>
        public bool ContainAxis(string axisName)
        {
            foreach (Items pos in Positions)
                if (pos.Name == axisName)
                    return true;
            return false;
        }

        /// <summary>
        /// 从功能点中 移除某个轴
        /// </summary>
        /// <param name="axisName"></param>
        public void RemoveAxis(string axisName)
        {
            for (int i = 0; i < Positions.Count; i++)
            {
                if (Positions[i].Name == axisName)
                {
                    Positions.RemoveAt(i);
                    return;
                }
            }
        }

        /// <summary>
        /// 设置轴坐标，如果坐标轴不存在，则会添加
        /// </summary>
        /// <param name="axisName"></param>
        /// <param name="pos"></param>
        public void SetAxisPos(string axisName, double pos)
        {
            for (int i = 0; i < Positions.Count; i++)
                if (Positions[i].Name == axisName)
                {
                    Positions[i].Value = pos;
                    return;
                }
            Positions.Add(Items.Create(axisName, pos));
        }

        /// <summary>
        /// 获取点位
        /// </summary>
        /// <param name="axisName"></param>
        /// <returns></returns>
        public double GetAxisPos(string axisName)
        {
            foreach (Items ap in Positions)
                if (ap.Name == axisName)
                    return ap.Value;
            throw new ArgumentException(string.Format("GetAxisPos(axisName = {0}) failed by: axisName is not included by AxisNames = {1},PositionName = {2}", axisName, string.Join("|", AxisNames), Name));
        }

        /// <summary>
        /// 点位当前轴号在参数axisNames中不存在的部分（非法）
        /// </summary>
        /// <param name="axisName"></param>
        /// <returns></returns>
        public string[] NotAllowedBy(string[] axisNames)
        {
            if (null == axisNames || 0 == axisNames.Length)
                return AxisNames;
            return axisNames.Except(AxisNames).ToArray();//axisNames存在，而当前轴名称中没有的  
        }

    }
}
