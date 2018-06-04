using System;
using System.Windows.Forms;
using POS.View;
using POS.Common;
using POS.Service;

using System.Security.Cryptography;
using System.Text;
namespace POS
{
    static class Program
    {
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main()
        {
            try
            {

                if (!Program.runOrNot())
                {
                    try
                    {
                        Application.EnableVisualStyles();
                        Application.SetCompatibleTextRenderingDefault(false);
                        Application.Run(new MainForm());
                    }
                    catch
                    {
                        Application.Exit();
                    }
                }
            }
            catch
            {
                Application.Exit();
            }
        }

        /// <summary>
        /// 判断该程序是否已被启动过，true为已启动，false为没有启动
        /// </summary>
        /// <returns></returns>
        private static bool runOrNot()
        {
            string process = System.Diagnostics.Process.GetCurrentProcess().ProcessName;
            if ((System.Diagnostics.Process.GetProcessesByName(process)).GetUpperBound(0) > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}


