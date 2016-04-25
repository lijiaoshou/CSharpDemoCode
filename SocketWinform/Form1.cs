using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SocketWinform
{
    public partial class Form1 : Form
    {

        //创建线程，用以侦听端口号，接收消息
        Thread mythread;

        Socket socket;
        //定义侦听端口号
        int port = 8000;
        //标记发送消息的结束符号
        const string strSuffix = "$$";
        public static ManualResetEvent allDone = new ManualResetEvent(false);

        private const int MAX_SOCKET = 10;

        //获取本机IP地址
        private static IPAddress GetServerIP()
        {
            IPHostEntry ieh = Dns.GetHostEntry(Dns.GetHostName());
            return ieh.AddressList[2];
        }

        public class StateObject
        {
            public Socket workSocket = null;
            public const int BufferSize = 1024;
            public byte[] buffer = new byte[BufferSize];
            public StringBuilder sb = new StringBuilder();
        }

        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                //创建一个线程，让侦听在独立线程中运行
                mythread = new Thread(new ThreadStart(BeginListen));
                mythread.Start();
                this.button1.Enabled = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void BeginListen()
        {
            socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

            //申请号码，将该socket对象绑定到指定网络端点
            IPAddress ServerIp = GetServerIP();
            IPEndPoint iep = new IPEndPoint(ServerIp, 8000);
            socket.Bind(iep);

            //待机状态，将socket置于侦听状态
            socket.Listen(5);

            //接听电话，accept以同步方式从侦听socket的连接请求队列中提取第一个挂起的连接请求
            //然后创建并返回一个新的socket实例
            //Socket newSocket = socket.Accept();
            allDone.Reset();//将事件状态设置为非最终状态，导致线程阻止
            Socket newSocket = socket.BeginAccept(new AsyncCallback(AcceptCallback),socket);
            allDone.WaitOne();//阻止当前线程，知道当前实例收到信号

            //接收消息，从新的socket实例接受数据
            byte[] byteMessage = new byte[100];
            newSocket.Receive(byteMessage);

            string result = Encoding.Default.GetString(byteMessage);
            MessageBox.Show(result);

            //挂机，关闭socket
            socket.Close();
            mythread.Abort();
        }

        //异步回调函数
        public void AcceptCallback(IAsyncResult ar)
        {
            //获取用户定义的对象，它包含关于异步操作的信息
            Socket listener = (Socket)ar.AsyncState;

            //异步接受传入的连接尝试，并创建新的socket来处理远程主机通信
            Socket client = listener.EndAccept(ar);
            allDone.Set();
            StateObject state = new StateObject();
            state.workSocket = client;

            //远端信息
            EndPoint tempRemoteEP = client.RemoteEndPoint;
            IPEndPoint tempRemoteIP = (IPEndPoint)tempRemoteEP;
            string rempip = tempRemoteIP.Address.ToString();
            string remoport = tempRemoteIP.Port.ToString();
            IPHostEntry host = Dns.GetHostEntry(tempRemoteIP.Address);
            string HostName = host.HostName;
            string msg = "接受[" + HostName + "]" + rempip + ":" + remoport + "远程计算机连接";

            textBox1.Text = msg;

            client.BeginReceive(state.buffer,0,StateObject.BufferSize,0,new AsyncCallback(readCallback),state);
        }

        //异步信息接受和发送返回数据方法
        public void readCallback(IAsyncResult ar)
        {
            StateObject state = (StateObject)ar.AsyncState;
            Socket handler = state.workSocket;
            int bytesRead = handler.EndReceive(ar);
            if (bytesRead > 0)
            {
                string strmsg = Encoding.Default.GetString(state.buffer,0,bytesRead);
                state.sb.Append(strmsg);
                string content = state.sb.ToString();

                //远端信息
                EndPoint tempRemoteEP = handler.RemoteEndPoint;
                IPEndPoint tempRemoteIP = (IPEndPoint)tempRemoteEP;
                string rempip = tempRemoteIP.Address.ToString();
                string remoport = tempRemoteIP.Port.ToString();
                IPHostEntry host = Dns.GetHostEntry(tempRemoteIP.Address);
                string HostName = host.HostName;

                textBox1.Text += DateTime.Now.ToString() + "接收来自" + HostName + ":" + strmsg;

                //信息量比较大时可以分段发送，以9999作为结束标志
                if (content.IndexOf(strSuffix) > -1)
                {
                    string msg = MsgTransact(content);
                    textBox1.Text += "返回消息：" + msg;
                    Send(handler, msg);//异步发送
                }
                else
                {
                    handler.BeginReceive(state.buffer,0,StateObject.BufferSize,0,new 
                        AsyncCallback(readCallback),state);
                }
            }
        }

        //异步发送
        private void Send(Socket handler,string data)
        {
            byte[] byteData = Encoding.Default.GetBytes(data+strSuffix);
            //handler.Send(byteData);
            handler.BeginSend(byteData,0,byteData.Length,0,new AsyncCallback(SendCallback),handler);
        }

        //信息数据处理方法MsgTransact
        private string MsgTransact(string receivedMsg)
        {
            string returnMsg = "";
            if (receivedMsg.ToLower().StartsWith("ip"))
            {
                returnMsg = GetServerIP().ToString();
                return returnMsg;
            }
            if (receivedMsg.ToLower().StartsWith("help"))
            {
                returnMsg = "你好，我是机器人，有什么可以帮你的吗?";
            }
            else {
                int n = receivedMsg.IndexOf(" ");
                string codeMsg = receivedMsg;
                string msgcontent = "";
                if (n > 0)
                {
                    codeMsg = receivedMsg.Substring(0,n+1).Trim();//取信息的前缀
                    msgcontent = receivedMsg.Substring(n);
                }
                returnMsg = GetResponseMsg(msgcontent);
            }
            return returnMsg;
        }

        //从数据库查询应该返回的匹配信息
        public string GetResponseMsg(string comeMsg)
        {
            string content = null;
            DataRow dataRow = null;

            //根据关键字找出是哪种类别
            
        }
    }
}
