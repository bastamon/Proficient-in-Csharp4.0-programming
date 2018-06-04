using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Diagnostics;
using HBMISR.GUI.MainGUI;
using HBMISR.Data;
using System.IO;

namespace HBMISR.GUI.OtherGUI
{
    /// <summary>
    /// 窗体类
    /// 功能：
    /// 1、验证密码，进入系统
    /// </summary>
    public partial class FrmLogin : Form
    {
        /// <summary>
        /// 读取ini文件
        /// </summary>
        ReadIni readIni;

        /// <summary>
        /// 数据文件的路径
        /// </summary>
        string filePath;

        /// <summary>
        /// 指示创建hbmis文件是否成功。
        /// </summary>
        public bool IsSuccessful = false;

        /// <summary>
        /// 版本号
        /// </summary>
        private string version = "";

        /// <summary>
        /// 构造函数
        /// </summary>
        public FrmLogin()
        {
            InitializeComponent();
            readIni = new ReadIni();
            filePath = readIni.ReadString("filePath");
        }

        /// <summary>
        /// 当radioButton1为选中状态时，获取其版本。
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            version = radioButton1.Text;
        }

        /// <summary>
        /// 当radioButton2为选中状态时，获取其版本。
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            version = radioButton2.Text;
        }

        /// <summary>
        /// 当radioButton3为选中状态时，获取其版本。
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void radioButton3_CheckedChanged(object sender, EventArgs e)
        {
            version = radioButton3.Text;
        }

        /// <summary>
        /// 窗体加载事件处理,判断用户是否已经选择版本号.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FrmLogin_Load(object sender, EventArgs e)
        {
            if (readIni.ReadString("version") != "" && File.Exists(readIni.ReadString("filepath")))
            {
                this.Text = this.Text + "(" + readIni.ReadString("version") + "---上报端)";
                radioButton1.Visible = false;
                radioButton2.Visible = false;
                radioButton3.Visible = false;
            }
            else
            {
                this.Text = this.Text + "(上报端)";
                string tempversion = readIni.ReadString("version");
                if (tempversion != "")
                {
                    this.Text = this.Text + "(" + readIni.ReadString("version") + "---上报端)";
                    radioButton1.Visible = true;
                    radioButton2.Visible = true;
                    radioButton3.Visible = true;

                    switch (tempversion)
                    {
                        case "市(厅)级":
                            radioButton1.Checked = true;
                            break;
                        case "县(处)级":
                            radioButton2.Checked = true;
                            break;
                        case "乡(科)级":
                            radioButton3.Checked = true;
                            break;
                    }
                }
            }
        }

        /// <summary>
        /// 点击窗体的关闭按钮事件处理,结束当前线程,突出程序。
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FrmLogin_FormClosed(object sender, FormClosedEventArgs e)
        {
            Process.GetCurrentProcess().Kill();
            Application.Exit();
        }

        /// <summary>
        /// 点击登录按钮事件处理，判断密码是否输入正确，是否选择版本号，是否以创建数据文件。
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_Login_Click(object sender, EventArgs e)
        {
            if (tb_Password.Text.Equals(readIni.ReadString("password")))
            {
                if (radioButton1.Visible)
                {
                    if (radioButton1.Checked == true || radioButton2.Checked == true || radioButton3.Checked == true)
                    {
                        readIni.WriteString("tempversion", version);
                        FrmUnit frmUnit = new FrmUnit();
                        frmUnit.frmlogin = this;
                        frmUnit.ShowDialog();
                        if (IsSuccessful)
                        {
                            FrmMain frmMain = new FrmMain();
                            frmMain.Show();
                        }
                        this.Visible = false;
                        this.Size = new Size(0, 0);
                    }
                    else
                    {
                        MessageBox.Show("请选择版本号！","提示");
                    }
                }
                else
                {
                    FrmMain frmMain = new FrmMain();
                    frmMain.Show();
                    this.Visible = false;
                }
            }
            else
            {
                MessageBox.Show("口令错误，请重新输入！");
                tb_Password.Text = "";
            }
        }
    }
}
 
