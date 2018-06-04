using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using HBMISR.Data;
using HBMISR.GUI.MainGUI;

namespace HBMISR.GUI.OtherGUI
{
    public partial class FrmChangePassWord : Form
    {
        /// <summary>
        /// 修改密码的窗体
        /// </summary>
        public ControlNavigation cn;

        /// <summary>
        /// 主界面中显示单位信息
        /// </summary>
        private string reUnit;

        /// <summary>
        /// 修改密码的构造函数
        /// </summary>
        public FrmChangePassWord()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 修改密码的确定键
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button1_Click(object sender, EventArgs e)
        {
            ReadIni readIni = new ReadIni();
            if (readIni.ReadString("password").Equals(oldPassWord.Text))
            {
                if (newPassWord.Text.Equals(confirm.Text))
                {
                    readIni.WriteString("password", newPassWord.Text);
                    MessageBox.Show("密码修改成功！", "提示");
                    this.Close();
                }
                else
                {
                    MessageBox.Show("新密码与确认密码不一致！", "提示");
                    oldPassWord.Text = "";
                    newPassWord.Text = "";
                    confirm.Text = "";
                }
            }
            else
            {
                MessageBox.Show("原始密码不正确！", "提示");
                oldPassWord.Text = "";
                newPassWord.Text = "";
                confirm.Text = "";
            }


        }

        /// <summary>
        /// 退出修改密码界面
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        public string ReUnit
        {
            get { return reUnit; }
            set { reUnit = value; }
        }
    }
}
