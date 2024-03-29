using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;

namespace Cell.Tools
{
    public class XmlHelper
    {

        /// <summary>
        /// 替换节点里面的内容
        /// </summary>
        /// <param name="NodeName"></param>
        public static void ReplaceNodeText(string Path, string NodeName, string Value)
        {
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load(Path);
            //取指定的单个结点
            XmlNode oldChild = xmlDoc.DocumentElement.SelectSingleNode(NodeName);
            if (oldChild != null)
                oldChild.InnerText = Value;
            xmlDoc.Save(Path);
        }
        /// <summary>
        /// 修改属性
        /// </summary>
        /// <param name="xmlPath"></param>
        public static void ModifyAttribute(string Path, string ClassNode, string NodeName, string AttributeName, string AttributeValue)
        {
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load(Path);
            XmlElement element = (XmlElement)xmlDoc.SelectSingleNode(string.Format("{0}/{1}", ClassNode, NodeName));
            element.SetAttribute(AttributeName, AttributeValue);
            xmlDoc.Save(Path);
        }



        /// <summary>
        ///  XML 的根节点
        /// </summary>
        private string ClassNodeName { get; set; } = "版本名称";
        // XML 的路径
        private string xmlPath { get; set; }

        public XmlHelper(string _ClassNodeName, string _xmlPath)
        {
            ClassNodeName = _ClassNodeName;
            xmlPath = _xmlPath;
        }

        public void CreatXmlTree(string xmlPath)
        {
            XElement xElement = new XElement(
                 new XElement("BookStore",
                     new XElement("Book",
                         new XElement("Name", "C#入门", new XAttribute("BookName", "C#")),
                         new XElement("Author", "Martin", new XAttribute("Name", "Martin")),
                         new XElement("Adress", "上海"),
                         new XElement("Date", DateTime.Now.ToString("yyyy-MM-dd"))
                          ),
                    new XElement("Book",
                        new XElement("Name", "WCF入门", new XAttribute("BookName", "WCF")),
                        new XElement("Author", "Mary", new XAttribute("Name", "Mary")),
                        new XElement("Adress", "北京"),
                        new XElement("Date", DateTime.Now.ToString("yyyy-MM-dd"))
                           )
                           )
                 );

            //需要指定编码格式，否则在读取时会抛：根级别上的数据无效。 第 1 行 位置 1异常
            XmlWriterSettings settings = new XmlWriterSettings();
            settings.Encoding = new UTF8Encoding(false);
            settings.Indent = true;
            XmlWriter xw = XmlWriter.Create(xmlPath, settings);
            xElement.Save(xw);
            //写入文件
            xw.Flush();
            xw.Close();
        }

        /// <summary>
        /// 新增节点
        /// </summary>
        /// <param name="NodeName"> 节点名称</param>
        /// <param name="NodeText">节点 内容</param>
        public void Create(string NodeName, string NodeText)
        {
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load(this.xmlPath);

            var root = xmlDoc.DocumentElement;//取到根结点
            XmlNode newNode = xmlDoc.CreateNode("element", NodeName, "");
            newNode.InnerText = NodeText;

            //添加为根元素的第一层子结点
            root.AppendChild(newNode);
            xmlDoc.Save(xmlPath);
        }

        /// <summary>
        /// 新增属性
        /// </summary>
        /// <param name="NodeName">根节点</param>
        /// <param name="AttributeName">属性名称</param>
        /// <param name="AttributeValue">属性值</param>
        public void CreateAttribute(string NodeName, string AttributeName, string AttributeValue)
        {
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load(xmlPath);
            var root = xmlDoc.DocumentElement;//取到根结点
            XmlElement node = (XmlElement)xmlDoc.SelectSingleNode(string.Format("{0}/{1}", ClassNodeName, NodeName));
            node.SetAttribute(AttributeName, AttributeValue);
            xmlDoc.Save(xmlPath);
        }

        /// <summary>
        /// 删除节点
        /// </summary>
        /// <param name="ClassNodeName">根节点</param>
        /// <param name="NodeName">需要删除子节点</param>
        public void Delete(string NodeName)
        {
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load(xmlPath);
            var root = xmlDoc.DocumentElement;//取到根结点

            var element = xmlDoc.SelectSingleNode(string.Format("{0}/{1}", ClassNodeName, NodeName));
            root.RemoveChild(element);
            xmlDoc.Save(xmlPath);
        }

        /// <summary>
        ///  删除属性
        /// </summary>
        /// <param name="NodeName">子节点名称</param>
        /// <param name="AttributeName">子节点属性</param>
        public void DeleteAttribute(string NodeName, string AttributeName)
        {
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load(xmlPath);
            XmlElement node = (XmlElement)xmlDoc.SelectSingleNode(string.Format("{0}/{1}", ClassNodeName, NodeName));
            //移除指定属性
            node.RemoveAttribute(AttributeName);
            //移除当前节点所有属性，不包括默认属性
            //node.RemoveAllAttributes();
            xmlDoc.Save(xmlPath);
        }

        /// <summary>
        /// 修改属性
        /// </summary>
        /// <param name="xmlPath"></param>
        public void ModifyAttribute(string NodeName, string AttributeName, string AttributeValue)
        {
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load(xmlPath);
            XmlElement element = (XmlElement)xmlDoc.SelectSingleNode(string.Format("{0}/{1}", ClassNodeName, NodeName));
            element.SetAttribute(AttributeName, AttributeValue);
            xmlDoc.Save(xmlPath);
        }

        /// <summary>
        /// 获取所有节点
        /// </summary>
        /// <param name="NodeName"></param>
        public string Select(string NodeName)
        {
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load(this.xmlPath);
            //取根结点
            var root = xmlDoc.DocumentElement;//取到根结点

            //取指定的单个结点
            XmlNode oldChild = xmlDoc.SelectSingleNode(string.Format("{0}/{1}", ClassNodeName, NodeName));
            if (oldChild == null) return null;
            return oldChild.InnerText;
            //取指定的结点的集合
            //  XmlNodeList nodes = xmlDoc.SelectNodes(string.Format("{0}/{1}", ClassNodeName, NodeName));

            //取到所有的xml结点
            //  XmlNodeList  x= xmlDoc.GetElementsByTagName("*");
        }

        /// <summary>
        /// 获取属性
        /// </summary>
        /// <param name="xmlPath"></param>
        public string SelectAttribute(string NodeName, string AttributeName)
        {
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load(this.xmlPath);
            XmlElement element = (XmlElement)xmlDoc.SelectSingleNode(string.Format("{0}/{1}", ClassNodeName, NodeName));

            return element.GetAttribute(AttributeName);
        }
    }
}
