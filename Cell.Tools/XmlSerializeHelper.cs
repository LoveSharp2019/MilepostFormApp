using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;

namespace Cell.Tools
{
    public sealed class Utf8Writer : StringWriter
    {
        public override Encoding Encoding => Encoding.UTF8;
    }

    /// <summary>
    /// XML序列化公共处理类
    /// </summary>
    public static class XmlSerializeHelper
    {

        /// <summary>
        /// 将XML文件转换成 XML 字符串
        /// </summary>
        /// <param name="Path">XML文件路径</param>
        /// <returns></returns>
        public static string xmlFileConvertToT(string Path)
        {
            System.Xml.XmlDocument doc = new System.Xml.XmlDocument();//新建对象
            doc.Load(Path);//XML文件路径
            return doc.InnerXml;
        }

        /// <summary>
        /// 将字符串（符合xml格式）转换为XmlDocument
        /// </summary>
        /// <param name="xmlString">XML格式字符串</param>
        /// <param name="Path">XML 文件保存的路径</param>
        /// <returns></returns>
        public static void StringToXmlToFile(string xmlString, string Path)
        {
            XmlDocument document = new XmlDocument();
            document.LoadXml(xmlString);
            document.Save(Path); //这里是你的xml文件     
        }

        /// <summary>
        /// 将实体对象转换成XML
        /// </summary>
        /// <typeparam name="T">实体类型</typeparam>
        /// <param name="obj">实体对象</param>
        //public static string XmlSerialize<T>(T obj)
        //{
        //    try
        //    {
        //        using (Utf8Writer sw = new Utf8Writer())
        //        {
        //            XmlSerializer serializer = new XmlSerializer(obj.GetType());
        //            XmlSerializerNamespaces ns = new XmlSerializerNamespaces();
        //            ns.Add("", "");
        //            serializer.Serialize(sw, obj, ns);
        //            return sw.ToString();
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        throw new Exception("将实体对象转换成XML异常", ex);
        //    }
        //}

        /// <summary>
        /// 将实体对象转换成XML
        /// </summary>
        /// <typeparam name="T">实体类型</typeparam>
        /// <param name="obj">实体对象</param>
        public static string XmlSerialize<T>(T obj)
        {
            try
            {
                using (StringWriter sw = new StringWriter())
                {
                    Type t = obj.GetType();
                    XmlSerializer serializer = new XmlSerializer(obj.GetType());
                    serializer.Serialize(sw, obj);
                    sw.Close();
                    return sw.ToString();
                }
            }
            catch (Exception ex)
            {
                throw new Exception("将实体对象转换成XML异常", ex);
            }
        }

        /// <summary>
        /// 将XML转换成实体对象
        /// </summary>
        /// <typeparam name="T">实体类型</typeparam>
        /// <param name="strXML">XML</param>
        public static T DESerializer<T>(string strXML) where T : class
        {
            try
            {
                using (StringReader sr = new StringReader(strXML))
                {
                    XmlSerializer serializer = new XmlSerializer(typeof(T));
                    return serializer.Deserialize(sr) as T;
                }
            }
            catch (Exception ex)
            {
                throw new Exception("将XML转换成实体对象异常", ex);
            }
        }
    }
}
