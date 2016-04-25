using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Xml;

namespace ConsoleDemo
{
    public class WebServiceOperate
    {
        public static string GetServiceByGet()
        {
            int x = 5;
            int y = 6;
            string strURL = "http://localhost:26298/MyWebService.asmx?op=Add&";
            strURL += "x=" + x + "&y=" + y;

            //创建一个HTTP请求
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(strURL);
            //request.Method="get";
            HttpWebResponse response = (System.Net.HttpWebResponse)request.GetResponse();

            Stream s = response.GetResponseStream();
            //转为XML，自己进行处理
            XmlTextReader Reader = new XmlTextReader(s);
            Reader.MoveToContent();
            string strValue = Reader.ReadInnerXml();
            strValue = strValue.Replace("&lt;","<");
            strValue = strValue.Replace("&gt;",">");
            Reader.Close();
            return strValue;
        }

        public static string GetServiceByPost()
        {
            int x = 5;
            int y = 6;

            string strURL = "http://localhost:26298/MyWebService.asmx?op=Add&";

            //创建按一个HTTP请求
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(strURL);
            //Post请求方式
            request.Method = "POST";
            //内容类型
            request.ContentType = "application/x-www-form-urlencoded";
            //设置参数，并进行URL编码
            string paraUrlCoded1 = HttpUtility.UrlEncode("x");
            string paraUrlCoded2 = HttpUtility.UrlEncode("y");
            paraUrlCoded1 += "=" + HttpUtility.UrlEncode(x.ToString());
            paraUrlCoded2 += "=" + HttpUtility.UrlEncode(y.ToString());

            byte[] payload1;
            byte[] payload2;
            //将URL编码后的字符串转化为字节
            payload1 = System.Text.Encoding.UTF8.GetBytes(paraUrlCoded1);
            payload2 = System.Text.Encoding.UTF8.GetBytes(paraUrlCoded2);
            //设置请求的ContentLength
            request.ContentLength = payload1.Length + payload2.Length;

            //发送请求，获得请求流
            Stream writer = request.GetRequestStream();
            //将请求参数写入流
            writer.Write(payload1,0,payload1.Length);
            writer.Write(payload2,0,payload2.Length);

            //关闭请求刘
            writer.Close();
            //获得响应流
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            Stream s = response.GetResponseStream();
            //转化为XML，自己进行处理
            XmlTextReader Reader = new XmlTextReader(s);
            Reader.MoveToContent();
            string strValue = Reader.ReadInnerXml();
            strValue = strValue.Replace("&lt;", "<");
            strValue = strValue.Replace("&gt;", ">");
            Reader.Close();
            return strValue;
        }
    }
}
