using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cell.DataModel
{
    public class CosParams
    {
        /// <summary>
        /// 创建一个对象
        /// </summary>
        /// <param name="type">参数类型,不能为空值</param>
        /// <param name="limit">参数限制</param>
        /// <param name="range">参数范围，如果参数限制值为cValueLimit.Min/Max/range，此值不能为空</param>
        /// <param name="summary">参数简介文本，可为空值</param>
        /// <returns></returns>
        public static CosParams Create(string name, Type type, cValueLimit limit, object[] range, bool BoolCallBack = false, string summary = null)
        {
            if (null == type)
                throw new ArgumentNullException("CosParams.Create(Type type....) failed By:type = null");

            if ((limit & cValueLimit.Min) != 0 && (limit & cValueLimit.Max) != 0)
            {
                if (null == range || range.Length != 2)
                    throw new ArgumentException(string.Format("CosParams.Create(type = {0},limit = {1}, object[] range ...) failed By:{2}",
                                                        type.Name, limit.ToString(),
                                                        null == range ? "range == null" : ("range's count = " + range.Length + "!Must be 2 ")));
            }
            if ((limit & cValueLimit.Min) != 0 || (limit & cValueLimit.Max) != 0)
                if (null == range || range.Length == 0)
                    throw new ArgumentException(string.Format("CosParams.Create(type = {0},limit = {1}, object[] range ...) failed By:range is null or empty!",
                                                       type.Name, limit.ToString()));
            if ((limit & cValueLimit.Range) != 0)
                if (null == range)
                    range = new object[] { };
            return new CosParams(name, type, limit, range, BoolCallBack, summary);
        }


        /// <summary>
        /// 创建一个对象
        /// </summary>
        /// <param name="type">参数类型,不能为空值</param>
        /// <param name="limit">参数限制</param>
        /// <param name="range">参数范围，cValueLimit.Min/Max/range，此值不能为空</param>
        /// <param name="summary">参数简介文本，可为空值</param>
        /// <returns></returns>
        public static CosParams Create(string name, Type type, cValueLimit limit, object[] range, string summary = null, bool BoolCallBack = false)
        {
            if (null == type)
                throw new ArgumentNullException("CosParams.Create(Type type....) failed By:type = null");

            if ((limit & cValueLimit.Min) != 0 && (limit & cValueLimit.Max) != 0)
            {
                if (null == range || range.Length != 2)
                    throw new ArgumentException(string.Format("CosParams.Create(type = {0},limit = {1}, object[] range ...) failed By:{2}",
                                                        type.Name, limit.ToString(),
                                                        null == range ? "range == null" : ("range's count = " + range.Length + "!Must be 2 ")));
            }
            if ((limit & cValueLimit.Min) != 0 || (limit & cValueLimit.Max) != 0)
                if (null == range || range.Length == 0)
                    throw new ArgumentException(string.Format("CosParams.Create(type = {0},limit = {1}, object[] range ...) failed By:range is null or empty!",
                                                       type.Name, limit.ToString()));
            if ((limit & cValueLimit.Range) != 0)
                if (null == range)
                    range = new object[] { };
            return new CosParams(name, type, limit, range, BoolCallBack, summary);
        }

        /// <summary>
        /// 序列化的时候 要加上这个 否则会报错
        /// </summary>
        public CosParams()
        {
        }

        public CosParams(string name, Type type, cValueLimit limit, object[] range, bool boolCallBack, string description)
        {
            pName = name;
            ptype = type.ToString();
            pvLimit = limit;
            pvalue = range;
            pDescription = description;
            pboolCallBack = boolCallBack;

        }

        /// <summary>
        /// 参数名称
        /// </summary>
        public string pName { get; set; }

        /// <summary>
        ///  参数的 类型
        /// </summary>
        public string ptype { get; set; }

        /// <summary>
        ///  参数校验
        /// </summary>
        public cValueLimit pvLimit { get; set; }

        /// <summary>
        /// 参数的数值
        /// </summary>
        public object[] pvalue { get; set; }

        /// <summary>
        ///  参数是否需要回调刷新
        /// </summary>
        public bool pboolCallBack { get; set; }

        /// <summary>
        ///  描述
        /// </summary>
        public string pDescription { get; set; }


    }
}
