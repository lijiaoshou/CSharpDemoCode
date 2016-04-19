using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace ConsoleDemo
{
    public class XMLOperate
    {
        #region xml相关类
        //XmlNode
        //XmlText
        //XmlComment
        //XmlElement
        //XmlAttribute
        //XmlDeclaration
        #endregion

        /// <summary>
        /// 通过XmlDocument来创建xml文档
        /// </summary>
        public static void CreateXmlByXmlDocument()
        {
            //创建空的xml文档
            XmlDocument xmldoc = new XmlDocument();
            //头部加入xml声明段落
            XmlNode xmlnode = xmldoc.CreateNode(XmlNodeType.XmlDeclaration, "", "");
            xmldoc.AppendChild(xmlnode);
            //增加注释
            XmlComment xmlComm = xmldoc.CreateComment("注释内容");
            xmldoc.AppendChild(xmlComm);
            //为xml文档加入元素/加入一个根元素
            XmlElement xmlelem = xmldoc.CreateElement("", "学生", "");
            //增加一个属性
            XmlAttribute xmlAttr = xmldoc.CreateAttribute("专业");
            xmlAttr.Value = "计算机";
            xmlelem.Attributes.Append(xmlAttr);
            xmldoc.AppendChild(xmlelem);
            //增加一个子元素
            XmlElement xmlelem2 = xmldoc.CreateElement("姓名");
            XmlText xmltext = xmldoc.CreateTextNode("李超");
            xmlelem2.AppendChild(xmltext);
            xmlelem.AppendChild(xmlelem2);

            XmlElement xmlelem3 = xmldoc.CreateElement("性别");
            xmltext = xmldoc.CreateTextNode("男");
            xmlelem3.AppendChild(xmltext);
            xmlelem.AppendChild(xmlelem3);

            //保存创建好的xml文档
            try
            {
                xmldoc.Save(AppDomain.CurrentDomain.BaseDirectory + "sampledata1.xml");
            }
            catch (Exception ex)
            {
                File.CreateText(AppDomain.CurrentDomain.BaseDirectory + "error.xml");
                
            }
            
        }

        /// <summary>
        /// 通过XmlTextWriter来创建xml文档
        /// </summary>
        public static void CreateXmlByXmlTextWriter()
        {
            //创建XmlTextWriter类的实例对象
            XmlTextWriter textWriter = new XmlTextWriter(AppDomain.CurrentDomain.BaseDirectory+"sampledata2.xml",null);
            //设置格式，对元素内容进行缩进
            textWriter.Formatting = Formatting.Indented;
            //书写版本为“1.0”的xml声明
            textWriter.WriteStartDocument();
            //写出名称和文本之间带有空格的处理指令
            string PItext = "type='text/xsl' href='book.xsl'";
            textWriter.WriteProcessingInstruction("xml-stylesheet",PItext);
            //增加指定名称和可选属性的DocType声明
            textWriter.WriteDocType("学生",null,null,"<!ENTITY sex '男'>");
            //增加注释
            textWriter.WriteComment("XML注释");
            //开始创建元素
            textWriter.WriteStartElement("学生");
            //创建属性
            textWriter.WriteAttributeString("专业","计算机");
            textWriter.WriteAttributeString("日期", "2008-09-01");
            //创建元素
            textWriter.WriteElementString("姓名","李超");
            textWriter.WriteStartElement("性别");
            textWriter.WriteEntityRef("sex");
            textWriter.WriteFullEndElement();
            textWriter.WriteElementString("年龄","25");
            //写CData信息
            textWriter.WriteCData("年龄大了");
            //关闭根
            textWriter.WriteEndElement();
            textWriter.WriteEndDocument();
            //写XML文件，关闭textWriter
            textWriter.Flush();
            textWriter.Close();

        }

        /// <summary>
        /// 通过XmlDocumnet读取xml文档
        /// </summary>
        public static void ReadXmlByXmlDocument()
        {
            //加载指定xml文档
            XmlDocument doc = new XmlDocument();
            doc.Load(AppDomain.CurrentDomain.BaseDirectory+ "sampledata1.xml");
            //读取xml节点数据
            XmlNodeReader reader = new XmlNodeReader(doc);
            string s = "", v = "";
            while(reader.Read())
            {
                //判断当前读取的节点类型
                switch (reader.NodeType)
                {
                    case XmlNodeType.Element:
                        s = reader.Name;
                        break;
                    case XmlNodeType.Text:
                        {
                            if (s.Equals("Name"))
                                v = reader.Value;
                            else
                                v = reader.Value;
                        }
                        break;
                }
            }

            //关闭XmlNodeReader
            if (reader != null)
            {
                reader.Close();
            }
        }

        /// <summary>
        /// 通过XmlTextReader来读取xml文档
        /// </summary>
        /// <returns></returns>
        public static string ReadXmlByXmlTextReader()
        {
            StringBuilder sb = new StringBuilder();
            //创建一个XmlTextReader类的对象并调用Read方法来读取文件
            XmlTextReader textReader = new XmlTextReader(AppDomain.CurrentDomain.BaseDirectory+ "sampledata2.xml");
            //节点非空则执行循环体
            textReader.Read();
            while (textReader.Read())
            {

            }
            //得到当前结点类型
            XmlNodeType nType = textReader.NodeType;
            switch (nType)
            {
                case XmlNodeType.XmlDeclaration:
                    break;
                case XmlNodeType.Comment:
                    break;
                case XmlNodeType.Attribute:
                    break;
                case XmlNodeType.Element:
                    break;
                case XmlNodeType.Entity:
                    break;
                case XmlNodeType.DocumentType:
                    break;
                case XmlNodeType.Whitespace:
                    break;

            }

            //读取该元素的各种属性值
            textReader.MoveToElement();
            sb.Append("Name:"+textReader.Name);
            sb.Append("Base URI:" + textReader.BaseURI);
            sb.Append("Local Name:" + textReader.LocalName);
            sb.Append("Attribute Count:" + textReader.AttributeCount.ToString());
            sb.Append("Depth:" + textReader.Depth);
            sb.Append("Line Number:" + textReader.LineNumber.ToString());
            sb.Append("Node Type:" + textReader.NodeType.ToString());
            sb.Append("Attribute Count:" + textReader.Value.ToString());

            return sb.ToString();
        }

        /// <summary>
        /// XML转DataSet
        /// </summary>
        public static void ReadXmlToDataSet()
        {
            #region 使用文件名
            //文档树不要超过三层，读入DataSet的数据仅仅两层元素的id号
            //根元素不要使用属性
            //根元素超过一个，则多余的根元素机器子元素无效
            DataSet ds = new DataSet();
            ds.ReadXml(AppDomain.CurrentDomain.BaseDirectory + "sampledata2.xml");
            #endregion

            #region 使用FileStream文件流
            FileStream fs = new FileStream(AppDomain.CurrentDomain.BaseDirectory + "sampledata2.xml", FileMode.Open);
            ds.ReadXmlSchema(fs);
            #endregion

            #region 使用StreamReader
            StreamReader sr = new StreamReader(AppDomain.CurrentDomain.BaseDirectory + "sampledata2.xml");
            ds.ReadXmlSchema(sr);
            sr.Close();
            #endregion

            #region 使用XmlTextReader
            FileStream fs1 = new FileStream(AppDomain.CurrentDomain.BaseDirectory + "sampledata2.xml", FileMode.Open);
            XmlTextReader xmlreader = new XmlTextReader(fs1);
            ds.ReadXmlSchema(xmlreader);
            xmlreader.Close();
            #endregion
        }

        /// <summary>
        /// DataSet转XML
        /// </summary>
        public static void ReadDataSetToXml()
        {
            //可以先从数据库查一个ds出来
            DataSet ds = new DataSet();
            #region 写入指定文件
            ds.WriteXml(AppDomain.CurrentDomain.BaseDirectory + "dstoxml.xml");
            #endregion

            #region 转换为xmlm格式字符串
            string str = ds.GetXml();
            #endregion

            #region 表示为xmls架构字符串
            string str1 = ds.GetXmlSchema();
            #endregion
        }

    }
}
