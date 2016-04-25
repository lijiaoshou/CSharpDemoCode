#define DEBUG
using System;

namespace ConsoleDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            //MyWebServiceReference.MyWebServiceSoapClient proxyClient = new MyWebServiceReference.MyWebServiceSoapClient();

            //string result = proxyClient.Add(1,2);
            string result=WebServiceOperate.GetServiceByPost();

            Console.WriteLine(result);
            Console.ReadKey();

            #region old
            //XMLOperate.CreateXmlByXmlDocument();
            //XMLOperate.CreateXmlByXmlTextWriter();
            //string strXml = XMLOperate.ReadXmlByXmlTextReader();
            //Console.WriteLine(strXml);
            //Console.ReadKey();
            #endregion

            #region old

            //            ClassPeople cp = new ClassPeople();
            //            cp.DoWork("李超", cp.SayChinese);
            //            cp.DoWork("lichao",cp.SayEnglish);


            //#if DEBUG
            //            Console.WriteLine("调试状态");
            //#endif

            //            Console.ReadKey();
            #endregion
        }
    }

    public class abClass : A, B
    {
        int B.Add(int a, int b)
        {
            throw new NotImplementedException();
        }

        int A.Add(int a, int b)
        {
            throw new NotImplementedException();
        }
    }

    public interface A
    {
         int Add(int a, int b);
    }

    public interface B
    {
         int Add(int a,int b);
    }

    public delegate void SayDelegaet(string name);

    public class ClassPeople
    {
        public void SayChinese(string name)
        {
            Console.WriteLine("你好，"+name);
        }

        public void SayEnglish(string name)
        {
            Console.WriteLine("Hello, " + name);
        }

        public void DoWork(string name, SayDelegaet MakeSay)
        {
            MakeSay(name);
        }
    }
}
