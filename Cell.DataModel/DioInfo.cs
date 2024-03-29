using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Cell.DataModel
{
    /// <summary>
    /// 用于标识（被声明的）DI属性
    /// </summary>
    [Serializable]
    public class DioInfo
    {
        /// <summary>
        /// 全局通道名称（设备通道命名管理器中）
        /// </summary>
        public string GlobalChnName { get; set; }

        /// <summary>
        /// 是否启用（如果启用，开关时会触发回调函数）
        /// </summary>
        public bool Enabled { get; set; }

    }
}
