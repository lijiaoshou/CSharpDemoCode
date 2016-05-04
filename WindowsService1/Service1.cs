using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;

namespace WindowsService1
{
    public partial class Service1 : ServiceBase
    {
        public Service1()
        {
            InitializeComponent();
        }

        protected override void OnStart(string[] args)
        {
            WriteLog(DateTime.Now.ToString()+"服务开始启动");
        }

        protected override void OnStop()
        {
            WriteLog(DateTime.Now.ToString()+"服务停止");
        }


        //可以把写日志方法换成其他更复杂的方法来实现更为复杂的功能
        /// <summary>
        /// 记录日志方法
        /// </summary>
        /// <param name="logInfo"></param>
        private void WriteLog(string logInfo)
        {
            string currPath = System.AppDomain.CurrentDomain.BaseDirectory;

            string logfilename = currPath + DateTime.Today.ToString("yyyyMMdd")+".txt";

            StreamWriter sw = new StreamWriter(logfilename,true);
            sw.WriteLine(logInfo);
            sw.Close();
        }

        /// <summary>
        /// WindowsService使用的timer控件不是winform的，System.Timers/System.Windows.Forms.Timer
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void timer1_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            WriteLog(DateTime.Now.ToString() + "服务运行中....");
        }
    }
}
