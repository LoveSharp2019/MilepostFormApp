using Cell.DataModel;
using Cell.Interface;
using Cell.Tools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Sys.IStations
{
    public class AppInitorManager
    {
        public AppInitorManager()
        {
            dictInitors = new SortedDictionary<string, IPlatInitializable>();
        }

        /// <summary>
        ///  加载 所有配置项
        /// </summary>
        public void Init()
        {

            DictionaryEx<string, List<object>> devInitParams = AppHubCenter.Instance.SystemCfg.GetItemValue(AppHubCenter.CK_InitDevParams) as DictionaryEx<string, List<object>>;
            foreach (KeyValuePair<string, List<object>> kv in devInitParams)
            {

                try //尝试初始化
                {
                    IPlatInitializable dev = AppHubCenter.Instance.InitorHelp.CreateInstance(kv.Value[0] as string);

                    string[] paramNames = dev.InitParamNames;
                    if (null != paramNames && paramNames.Length > 0)
                        for (int i = 0; i < paramNames.Length; i++)
                        {
                            object pr = i < kv.Value.Count - 1 ? kv.Value[i + 1] : null;
                            CosParams pd = dev.GetInitParamDescribe(paramNames[i]);
                            // 具体的转型会在序列化的时候完成
                            object relParamVal = Convert.ChangeType(pr, Type.GetType(pd.ptype));
                            dev.SetInitParamValue(paramNames[i], relParamVal);
                        }

                    dictInitors.Add(kv.Key, dev);
                    // 初始化
                    dev.Initialize();

                    // 如果是是设备 则 打开设备
                    if (dev is IPlatDevice)
                    {
                        (dev as IPlatDevice).OpenDevice();
                        Thread.Sleep(100);
                    }

                }
                catch
                {
                    //初始化发生异常

                  //  (AppHubCenter.Instance.SystemCfg.GetItemValue(AppHubCenter.CK_InitDevParams) as DictionaryEx<string, List<object>>).Remove(kv.Key);//从设备配置文件中删除

                }

            }


        }

        /// <summary>
        /// 根据指定的数组类型创建一个数组对象
        /// </summary>
        /// <param name="t"></param>
        /// <returns></returns>
        object GenArrayObject(Type t, int len)
        {
            ConstructorInfo[] ctors = t.GetConstructors(System.Reflection.BindingFlags.Instance
                                                        | System.Reflection.BindingFlags.NonPublic
                                                        | System.Reflection.BindingFlags.Public);

            foreach (ConstructorInfo ctor in ctors)
            {
                ParameterInfo[] ps = ctor.GetParameters();
                if (ps != null && ps.Length == 1 && ps[0].ParameterType == typeof(int))
                    return ctor.Invoke(new object[] { len });
            }
            return null;
        }


        object GenListObject(Type t)
        {
            ConstructorInfo[] ctors = t.GetConstructors(System.Reflection.BindingFlags.Instance
                                                        | System.Reflection.BindingFlags.NonPublic
                                                        | System.Reflection.BindingFlags.Public);

            foreach (ConstructorInfo ctor in ctors)
            {
                ParameterInfo[] ps = ctor.GetParameters();
                if (ps == null || ps.Length == 0)
                    return ctor.Invoke(null);
            }
            return null;
        }



        /// <summary>
        /// 获取当前所有已创建Initor对象名称
        /// </summary>
        public string[] InitorIDs
        {
            get
            {
                return dictInitors.Keys.ToArray();
            }
        }

        /// <summary>根据指定的接口类型筛选ID</summary>
        public string[] GetIDs(Type matchType)
        {
            List<string> ret = new List<string>();
            foreach (KeyValuePair<string, IPlatInitializable> kv in dictInitors)
            {
                if (matchType.IsAssignableFrom(kv.Value.GetType()))
                    ret.Add(kv.Key);
            }
            return ret.ToArray();
        }

        public IPlatInitializable GetInitor(string ID)
        {
            if (string.IsNullOrEmpty(ID))
                return null;
            if (!dictInitors.ContainsKey(ID))
                return null;
            return dictInitors[ID];
        }

        public bool ContainID(string ID)
        {

            return dictInitors.ContainsKey(ID);
        }

        public IPlatInitializable this[string ID]
        {
            get
            {
                return GetInitor(ID);
            }
        }

        /// <summary>
        /// 获取Initor对象对应的ID
        /// </summary>
        /// <param name="initor"></param>
        /// <returns></returns>
        public string GetIDByInitor(IPlatInitializable initor)
        {
            if (!dictInitors.ContainsValue(initor))
                return null;
            return dictInitors.FirstOrDefault(q => q.Value == initor).Key;
        }


        /// <summary>
        /// 保存Initor的初始化参数
        /// 应用场合：当initor自带的界面需要保存初始化参数时，可调用此函数
        /// </summary>
        /// <param name="initor">由InitorManager创建的对象（通过Hub界面创建的设备，工站等）</param>
        /// <param name="errorInfo">创建失败时的错误信息</param>
        /// <returns></returns>
        public bool SaveInitorCfg(IPlatInitializable initor, out string errorInfo) //未测试
        {
            if (null == initor)
            {
                errorInfo = "保存失败，Initor参数为空！";
                return false;
            }
            if (!dictInitors.ContainsValue(initor))
            {
                errorInfo = "保存失败，需要保存参数的对象不是通过InitorManager创建！";
                return false;
            }

            string[] paramNames = initor.InitParamNames;
            if (null == paramNames || 0 == paramNames.Length)
            {
                errorInfo = "Success";
                return true;
            }

            //从系统配置中拿到Initor的参数项的保存对象
            DictionaryEx<string, List<object>> devCfg = AppHubCenter.Instance.SystemCfg.GetItemValue(AppHubCenter.CK_InitDevParams) as DictionaryEx<string, List<object>>;

            List<object> initParams = devCfg[GetIDByInitor(initor)];
            initParams.Clear();
            initParams.Add(initor.GetType().AssemblyQualifiedName);//第一个保存的是Type信息
            for (int i = 0; i < paramNames.Length; i++)
            {
                CosParams prDes = initor.GetInitParamDescribe(paramNames[i]);
                object paramVal = initor.GetInitParamValue(paramNames[i]);

                initParams.Add(paramVal);

            }
            AppHubCenter.Instance.SystemCfg.NotifyItemChanged(AppHubCenter.CK_InitDevParams);
            AppHubCenter.Instance.SystemCfg.Save();

            errorInfo = "Success";
            return true;
        }


        /// <summary>
        /// 添加一个新的实例化类参数 （并且直行保存函数）
        /// </summary>
        /// <param name="id"></param>
        /// <param name="dev"></param>
        public void Add(string id, IPlatInitializable dev)
        {
            dictInitors.Add(id, dev);
            DictionaryEx<string, List<object>> dictInitorParam = AppHubCenter.Instance.SystemCfg.GetItemValue(AppHubCenter.CK_InitDevParams) as DictionaryEx<string, List<object>>;
            List<object> paramsInCfg = new List<object>();
            paramsInCfg.Add(dev.GetType().AssemblyQualifiedName);
            for (int i = 0; i < dev.InitParamNames.Length; i++)
            {
                object paramVal = dev.GetInitParamValue(dev.InitParamNames[i]);
                paramsInCfg.Add(paramVal);
            }
            dictInitorParam.Add(id, paramsInCfg);
            AppHubCenter.Instance.SystemCfg.NotifyItemChanged(AppHubCenter.CK_InitDevParams);
            AppHubCenter.Instance.SystemCfg.Save();

        }

        /// <summary>
        /// 移除一个类
        /// </summary>
        /// <param name="id"></param>
        public void Remove(string id)
        {
            dictInitors.Remove(id);
            DictionaryEx<string, List<object>> dictInitorParam = AppHubCenter.Instance.SystemCfg.GetItemValue(AppHubCenter.CK_InitDevParams) as DictionaryEx<string, List<object>>;
            dictInitorParam.Remove(id);
            AppHubCenter.Instance.SystemCfg.Save();
        }

        /// <summary>
        /// 清空所有实例化类
        /// </summary>
        public void Clear()
        {
            dictInitors.Clear();
            DictionaryEx<string, List<object>> dictInitorParam = AppHubCenter.Instance.SystemCfg.GetItemValue(AppHubCenter.CK_InitDevParams) as DictionaryEx<string, List<object>>;
            dictInitorParam.Clear();
            AppHubCenter.Instance.SystemCfg.Save();
        }


        SortedDictionary<string, IPlatInitializable> dictInitors;



    }
}
