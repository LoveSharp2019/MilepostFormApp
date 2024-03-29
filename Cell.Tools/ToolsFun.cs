using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Cell.Tools
{
    public class ToolsFun
    {
        /// <summary>
        /// 判断string是不是一个合法的IP地址
        /// </summary>
        /// <param name="txt"></param>
        /// <returns></returns>
        public static bool IsIPAddress(string txt)
        {
            if (string.IsNullOrEmpty(txt))
                return false;

            Regex rx = new Regex(@"((?:(?:25[0-5]|2[0-4]\d|((1\d{2})|([1-9]?\d)))\.){3}(?:25[0-5]|2[0-4]\d|((1\d{2})|([1-9]?\d))))");
            if (!rx.IsMatch(txt))
                return false;
            return true;
        }

        public static bool IsNullableType(Type type)
        {
            return !type.IsValueType;
        }
    }
}
