using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SpeedDevelpTools
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            MessageBox.Show("原生事件");
        }

        private void Alert(object sender,EventArgs e)
        {

            MessageBox.Show("另外绑定的事件");
        }

        private void button2_Click(object sender, EventArgs e)
        {
            
            textBox1.Text = GetFunctionNames(button1);
        }

        private string GetFunctionNames(Control control)
        {
            PropertyInfo propertyInfo = control.GetType().GetProperty("Events", BindingFlags.NonPublic | BindingFlags.Static | BindingFlags.Instance);
            EventHandlerList eventHandlerList = propertyInfo.GetValue(control, new object[] { }) as EventHandlerList;
            FieldInfo fieldInfo = typeof(Control).GetField("EventClick", BindingFlags.NonPublic | BindingFlags.Static);

            var eventKey = fieldInfo.GetValue(control);
            var eventHandler = eventHandlerList[eventKey] as Delegate;
            Delegate[] invocationList = eventHandler.GetInvocationList();

            StringBuilder sb = new StringBuilder();

            foreach (var handler in invocationList)
            {
                sb.Append(handler.GetMethodInfo().Name + "\r\n");
                //names.Add(handler.GetMethodInfo().Name);
            }

            return sb.ToString();
        }
    }
}
