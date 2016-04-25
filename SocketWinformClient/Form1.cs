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

namespace SocketWinformClient
{
    public partial class Form1 : Form
    {


        //装上电话，创建一个信息发送socket对象
        Socket socket = new Socket(AddressFamily.InterNetwork,SocketType.Stream,ProtocolType.Tcp);

        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //拨打电话,将该Socket对象连接到指定网络端点
            IPAddress serverIp = IPAddress.Parse(textBox1.Text.ToString());//ip地址
            int serverPort = Convert.ToInt32(textBox2.Text.ToString());//端口号
            IPEndPoint iep = new IPEndPoint(serverIp,serverPort);//创建一个端点
            socket.Connect(iep);//连接服务端t

            //交谈，使用send发送消息到指定的远程网络端点
            byte[] byteMessage;
            byteMessage = Encoding.ASCII.GetBytes(textBox3.Text.ToString());//把字符串转换为byte编码格式
            socket.Send(byteMessage);//发送消息

            //挂机，关闭socket对象
            socket.Shutdown(SocketShutdown.Both);
            socket.Close();
        }
    }
}
