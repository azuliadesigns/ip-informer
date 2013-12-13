using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace IPInformer
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new SysTrayApplication());
        }
    }



    public class SysTrayApplication : ApplicationContext
    {
        private NotifyIcon _trayIcon;
        private ContextMenuStrip _menu;
        private ToolStripMenuItem _exitMenu;

        public SysTrayApplication()
        {
            _trayIcon = new NotifyIcon();
            _trayIcon.Icon = IPInformer.Properties.Resources.icon;
            _trayIcon.Text = GetIPAddress();
            _trayIcon.Visible = true;

            _exitMenu = new ToolStripMenuItem("Exit");
            _exitMenu.Click += new EventHandler(ExitMenu_Click);

            _menu = new ContextMenuStrip();
            _menu.Items.Add(_exitMenu);
            _trayIcon.ContextMenuStrip = _menu;

            Application.ThreadExit += new EventHandler(Application_ThreadExit);
        }

        private void  Application_ThreadExit(object sender, EventArgs e)
        {
 	        _trayIcon.Visible = false;
        }

        private void ExitMenu_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private string GetIPAddress()
        {
            StringBuilder localIP = new StringBuilder();
            IPHostEntry host = Dns.GetHostEntry(Dns.GetHostName());

            foreach (IPAddress ip in host.AddressList)
            {
                if (ip.AddressFamily == AddressFamily.InterNetwork)
                {
                    localIP.AppendLine(ip.ToString());
                }
            }

            return localIP.ToString();
        }
    }
}