using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cell.DataModel
{
    /// <summary>
    /// 站点中 停止的附带的报错信息
    /// </summary>
    public class IWorkExitException : Exception
    {
        public IWorkExitException(WorkExitCode exitCode, string info, object param) : base()
        {
            ExitCode = exitCode;
            ExitInfo = info;
            ExitParam = param;
        }

        public WorkExitCode ExitCode { get; private set; }
        public string ExitInfo { get; private set; }
        protected object ExitParam { get; private set; }
    }
}
