using Cell.Tools;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Cell.Tools
{
    /// <summary> 
    ///  泛型配置文件 （dicName 的object 类型如有新增ArryList 类型 请重新新增XML序列化方法）
    /// </summary>
    public class AppCfgFromXml
    {
        public DictionaryEx<string, object> dicName { get; set; }
        public DictionaryEx<string, List<string>> dictTag { get; set; }

        /// <summary>
        /// 配置文件的名称   XmlIgnore 不在XML 序列化内
        /// </summary>
        [XmlIgnore]
        public string FilePath { get; private set; }

        /// <summary>
        /// 代理:配置项值改变
        /// </summary>
        /// <param name="itemName"></param>
        /// <param name="newValue"></param>
        public delegate void ItemChange(string itemName, object newValue);

        [XmlIgnore]
        public ItemChange ItemChangedEvent;

        public AppCfgFromXml()
        {
            FilePath = null;
            dictTag = new DictionaryEx<string, List<string>>();
            dicName = new DictionaryEx<string, object>();
        }

        [XmlIgnore]
        public string[] AllTags
        {
            get
            {
                if (0 == dictTag.Count)
                    return new string[] { };
                return dictTag.Keys.ToArray<string>();
            }
        }


        /// <summary>
        /// 获取标签下的所有配置项名称
        /// </summary>
        /// <param name="tag"></param>
        /// <returns></returns>
        public string[] ItemNamesInTag(string tag)
        {
            lock (this)
            {
                if (!dictTag.ContainsKey(tag))
                    return new string[] { };
                return dictTag[tag].ToArray();
            }

        }

        /// <summary>
        /// 获取所有数据项名称
        /// </summary>
        /// <returns></returns>
        public string[] AllItemNames()
        { return dicName.Keys.ToArray(); }

        public bool ContainsItem(string itemName)
        {
            return dicName.ContainsKey(itemName);

        }

        public void AddItem(string name, object value, string tag = null)
        {
            lock (this)
            {
                dicName.Add(name, value);
                if (null == tag)
                    tag = "";
                if (!dictTag.ContainsKey(tag))
                    dictTag.Add(tag, new List<string>());
                dictTag[tag].Add(name);

            }
        }

        public void RemoveItem(string itemName)
        {
            lock (this)
            {
                if (dicName.ContainsKey(itemName))
                    dicName.Remove(itemName);
                string tag = GetItemTag(itemName);
                if (tag != null)
                {
                    dictTag[tag].Remove(itemName);
                    if (dictTag[tag].Count == 0)
                        dictTag.Remove(tag);
                }
            }

        }


        public string GetItemTag(string itemName)
        {
            foreach (KeyValuePair<string, List<string>> kv in dictTag)
                if (kv.Value.Contains(itemName))
                    return kv.Key;

            return null;
        }

        public object GetItemValue(string itemName)
        {
            if (dicName.ContainsKey(itemName))
                return dicName[itemName];
            return null;
        }

        public void SetItemValue(string itemName, object itemValue)
        {
            lock (this)
            {
                dicName[itemName] = itemValue;
            }
            if (null != ItemChangedEvent)
                ItemChangedEvent(itemName, itemValue);
        }

        public void NotifyItemChanged(string itemName)
        {
            if (null != ItemChangedEvent)
                ItemChangedEvent(itemName, GetItemValue(itemName));
        }


        /// <summary>
        /// 从文件中（重新）加载配置
        /// </summary>
        public void Load()
        {
            Load(FilePath, false);
        }

        /// <summary>
        /// 从文件中加载参数配置
        /// </summary>
        /// <param name="filePath">文件路径</param>
        /// <param name="isOpenOrCreate">文件不存在时是否创建新文件，=True:创建新文件 ;  =False：不创建新文件，会抛出一个异常</param>
        public void Load(string filePath, bool isOpenOrCreate)
        {
            try
            {
                if (!File.Exists(filePath)) //文件不存在
                {
                    if (!isOpenOrCreate)
                        throw new FileNotFoundException(string.Format("文件不存在 filePath={0},isOpenOrCreate={1} failed by: FilePath is Nonexists!", filePath, isOpenOrCreate));
                    // 如果目录不存在则创建目录
                    if (!Directory.Exists(Path.GetDirectoryName(filePath)))
                        Directory.CreateDirectory(Path.GetDirectoryName(filePath));

                    AppCfgFromXml configModel = new AppCfgFromXml();
                    XmlSerializeHelper.StringToXmlToFile(XmlSerializeHelper.XmlSerialize<AppCfgFromXml>(configModel), filePath);
                }

                AppCfgFromXml dictNameValue = XmlSerializeHelper.DESerializer<AppCfgFromXml>(XmlSerializeHelper.xmlFileConvertToT(filePath));

                dicName = dictNameValue.dicName;
                dictTag = dictNameValue.dictTag;
                FilePath = filePath;
            }
            catch (Exception ex)
            {
                throw new FileNotFoundException(string.Format("文件加载失败 filePath={0},msg={1} !", filePath, ex.Message));
            }
        }

        public void Save()
        {
            Save(FilePath);
        }


        public void Save(string filePath)
        {
            try
            {
                AppCfgFromXml configModel = new AppCfgFromXml();
                configModel.dictTag = dictTag;
                configModel.dicName = dicName;
                XmlSerializeHelper.StringToXmlToFile(XmlSerializeHelper.XmlSerialize<AppCfgFromXml>(configModel), filePath);
            }
            catch (Exception ex)
            {
                throw new FileNotFoundException(string.Format("保存文件失败 filePath={0},msg={1} !", filePath, ex.Message));
            }
        }

    }

}
