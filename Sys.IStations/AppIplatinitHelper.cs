using Cell.Interface;
using Org.IBarcode;
using Org.ICamera;
using Org.IMotionDaq;
using Org.ILineScan;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Sys.IStations
{
    /// <summary>
    ///  加载内外部 所有继承IPlatInitializable的类
    /// </summary>
    public class AppIplatinitHelper
    {
        internal AppIplatinitHelper()
        {
            List<string> assNamesTouched = new List<string>();
            appendDlls = new List<string>();
            InitTypes = new List<Type>();
            //先将本程序集的类型提取出来
            IBarcodeHelper.IBarcodeAss();
            ICameraHelper.ICameraAss();
            IMotionHelper.IMotionAss();
            ILineScanHelper.ILineScanAss();


            Assembly[] assemblies = AppDomain.CurrentDomain.GetAssemblies();

            foreach (Assembly ass in assemblies)
            {
                if (assNamesTouched.Contains(ass.FullName))
                    continue;
                else
                    assNamesTouched.Add(ass.FullName);
                Type[] ts = ass.GetTypes();
                foreach (Type t in ts)
                    if (typeof(IPlatInitializable).IsAssignableFrom(t) && t.IsClass && !t.IsAbstract)
                        if (!InitTypes.Contains(t))
                            InitTypes.Add(t);
            }
        }

        /// <summary>
        ///  外部加载的dll 路劲
        /// </summary>
        List<string> appendDlls;

        /// <summary>
        /// 当前程序所有继承IPlatInitializable的类
        /// </summary>
        List<Type> InitTypes;

        public string[] AllApendDllPaths()
        {
            return appendDlls.ToArray();
        }


        /// <summary>
        /// 加载一个指定的dll 以及继承IPlatInitializable的类
        /// </summary>
        /// <param name="dllPath"></param>
        public void AppendDll(string dllPath)
        {
            if (string.IsNullOrWhiteSpace(dllPath))
                throw new ArgumentNullException("AppIplatinitHelper.AppendDll(dllPath) fialed By: dllPath is null or whitespace");

            string dllName = Path.GetFileNameWithoutExtension(dllPath);
            if (string.IsNullOrWhiteSpace(dllName))
                throw new ArgumentException("AppIplatinitHelper.AppendDll(dllPath) fialed By:dllPath's filename is empty!");

            string dllExtention = Path.GetExtension(dllPath);
            if (dllExtention == string.Empty)
                dllPath += ".dll";
            else
            {
                if (dllExtention != ".dll")
                    throw new ArgumentException("AppIplatinitHelper.AppendDll(dllPath) fialed By:dllPath = " + dllPath + " isnot a dll file");
            }
            string fullPath = Path.GetFullPath(dllPath);
            if (!File.Exists(fullPath))
                throw new ArgumentNullException("AppIplatinitHelper.AppendDll(dllPath) fialed By: dllPath = " + dllPath + " is not Existed");

            if (appendDlls.Contains(dllPath))
                return;

            Type[] ts = InstantiatedClassesInDll(fullPath);
            if (null != ts)
                foreach (Type t in ts)
                    if (!InitTypes.Contains(t))
                        InitTypes.Add(t);
            appendDlls.Add(fullPath);
        }

        /// <summary>
        /// 移除 指定dll 以及继承IPlatInitializable的类
        /// </summary>
        /// <param name="dllPath"></param>
        public void RemoveDll(string dllPath)
        {
            if (string.IsNullOrWhiteSpace(dllPath))
                return;

            string dllName = Path.GetFileNameWithoutExtension(dllPath);
            if (string.IsNullOrWhiteSpace(dllName))
                return;

            string dllExtention = Path.GetExtension(dllPath);
            if (dllExtention == string.Empty)
                dllPath += ".dll";
            else
            {
                if (dllExtention != ".dll")
                    return;
            }
            string fullPath = Path.GetFullPath(dllPath);
            if (appendDlls.Contains(dllPath))
                appendDlls.Remove(dllPath);
            if (!File.Exists(fullPath))
                return;

            Type[] ts = InstantiatedClassesInDll(dllPath);
            if (null == ts)
                return;
            foreach (Type t in ts)
                if (InitTypes.Contains(t))
                    InitTypes.Remove(t);
        }

        public void ClearDll()
        {
            appendDlls.Clear();
            InitTypes.Clear();
        }


        /// <summary>
        /// 从指定dll 库中 获取 继承IPlatInitializable的类
        /// </summary>
        /// <param name="dllPath"></param>
        /// <returns></returns>
        public Type[] InstantiatedClassesInDll(string dllPath)
        {
            if (string.IsNullOrWhiteSpace(dllPath))
                throw new ArgumentNullException("InstantiatedClassesInDll.AppendDll(dllPath) fialed By: dllPath is null or whitespace");

            string dllName = Path.GetFileNameWithoutExtension(dllPath);
            if (string.IsNullOrWhiteSpace(dllName))
                throw new ArgumentException("InstantiatedClassesInDll.AppendDll(dllPath) fialed By:dllPath's filename is empty!");

            string dllExtention = Path.GetExtension(dllPath);
            if (dllExtention == string.Empty)
                dllPath += ".dll";
            else
            {
                if (dllExtention != ".dll")
                    throw new ArgumentException("InstantiatedClassesInDll.AppendDll(dllPath) fialed By:dllPath = " + dllPath + " isnot a dll file");
            }
            string fullPath = Path.GetFullPath(dllPath);
            if (!File.Exists(fullPath))
                throw new ArgumentNullException("InstantiatedClassesInDll.AppendDll(dllPath) fialed By: dllPath = " + dllPath + " is not Existed");

            List<Type> ret = new List<Type>();
            Assembly ass = Assembly.LoadFrom(dllPath);
            Type[] ts = ass.GetTypes();
            foreach (Type t in ts)
                if (typeof(IPlatInitializable).IsAssignableFrom(t) && t.IsClass && !t.IsAbstract)
                    ret.Add(t);
            return ret.ToArray();
        }


        /// <summary>
        /// 获取继承 IPlatInitializable 所有实体类
        /// </summary>
        /// <returns></returns>
        public Type[] InstantiatedClasses()
        {
            return InitTypes.ToArray();
        }




        /// <summary>
        ///   同时继承IPlatInitializable，superClass的实体类
        /// </summary>
        /// <param name="superClass"></param>
        /// <returns></returns>
        public Type[] InstantiatedClasses(Type superClass)
        {
            List<Type> ret = new List<Type>();
            foreach (Type t in InitTypes)
                if (superClass.IsAssignableFrom(t))
                    ret.Add(t);
            return ret.ToArray();
        }

        /// <summary>
        ///  创建 实体类名
        /// </summary>
        /// <param name="t"></param>
        /// <returns></returns>
        public IPlatInitializable CreateInstance(Type t)
        {
            if (t == null)
                throw new ArgumentNullException("CreateInstance(Type t) failed By: t = null");
            if (!typeof(IPlatInitializable).IsAssignableFrom(t) || !t.IsClass || t.IsAbstract)
                throw new ArgumentException("CreateInstance(Type t) failed By: t is not an  entity class inherited from IPlatInitializable");
            ConstructorInfo[] ctors = t.GetConstructors(System.Reflection.BindingFlags.Instance
                                                          | System.Reflection.BindingFlags.NonPublic
                                                          | System.Reflection.BindingFlags.Public);
            if (null == ctors)
                throw new Exception("CreateInstance(Type t) failed By: Not found t-Instance's Constructor");
            foreach (ConstructorInfo ctor in ctors)
            {
                ParameterInfo[] ps = ctor.GetParameters();
                if (ps == null || ps.Length == 0)
                    return ctor.Invoke(null) as IPlatInitializable;
            }

            return null;
        }

        /// <summary>
        /// 创建 指定实体类名的 实体类
        /// </summary>
        /// <param name="typeName"></param>
        /// <returns></returns>
        public IPlatInitializable CreateInstance(string typeName)
        {
            return CreateInstance(Type.GetType(typeName));
        }

        /// <summary>
        /// 获取实体类名
        /// </summary>
        /// <param name="it"></param>
        /// <returns></returns>
        public static string DispalyTypeName(Type it)
        {
            MyDisplayNameAttribute[] vn = it.GetCustomAttributes(typeof(MyDisplayNameAttribute), false) as MyDisplayNameAttribute[];
            if (null != vn && vn.Length > 0)
                return vn[0].Name;
            else
                return it.Name;
        }

    }
}
