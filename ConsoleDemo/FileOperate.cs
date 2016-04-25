using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ConsoleDemo
{
    public class FileOperate
    {
        public static string DisplayAboutSystemInformation()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("SystemInformation.UserName:"+SystemInformation.UserName+"\r\n");
            sb.Append("SystemInformation.ComputerName:" + SystemInformation.ComputerName + "\r\n");
            sb.Append("SystemInformation.BootMode:"+SystemInformation.BootMode.ToString());
            sb.Append("SystemInformation.MonitorCount:" + SystemInformation.MonitorCount);
            sb.Append("SystemInformation.PrimaryMonitorSize:" + SystemInformation.PrimaryMonitorSize.Width + ","
                      + SystemInformation.PrimaryMonitorSize.Height+"\r\n");
            sb.Append("SystemInformation.PrimaryMonitorMaximizedWindowSize:" + SystemInformation.PrimaryMonitorMaximizedWindowSize.Width + ","
                        + SystemInformation.PrimaryMonitorMaximizedWindowSize.Height + "\r\n");

            return sb.ToString();
        }


    }
}
