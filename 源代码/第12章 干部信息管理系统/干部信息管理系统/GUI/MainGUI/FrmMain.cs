using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Diagnostics;
using HBMISR.Data;
namespace HBMISR.GUI.MainGUI
{
    /// <summary>
    /// 窗体类，系统主界面
    /// </summary>
    public partial class FrmMain : Form
    {
        /// <summary>
        /// 构造方法
        /// </summary>
        public FrmMain()
        {
            InitializeComponent();
            this.Width = (Screen.PrimaryScreen.WorkingArea.Width) * 6 / 7;
            this.Height = (Screen.PrimaryScreen.WorkingArea.Height) * 18 / 19;

            ReadIni ini = new ReadIni();
            string version = ini.ReadString("version");
            if (version == "")
            {
                version = ini.ReadString("tempversion");
            }
            this.Text = "干部信息管理系统(" + version + "---上报端)";
        }

        /// <summary>
        /// 窗体即将关闭事件的处理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
           DataOperation dataOperation = new DataOperation();
           dataOperation.DeleteMarkedRecord(controlMain.list);
        }

        /// <summary>
        /// 窗体关闭后的事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MainForm_FormClosed(object sender, FormClosedEventArgs  e)
        {
            Process.GetCurrentProcess().Kill();
            Application.Exit();
        }

        /// <summary>
        /// 窗体加载事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FrmMain_Load(object sender, EventArgs e)
        {
            controlHead.controlNavigation.controlMain = controlMain;
            controlHead.controlNavigation.cp = controlPrint;
        }

        private void FrmMain_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)//如果输入的是回车键 
            {
                controlMain.controlSearch.Search_Click(sender, e);
            }
        }
    }
}
