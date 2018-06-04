using System;
using System.Windows.Forms;
using POS.Common;
using POS.Service;
using System.Data;

using System.Security.Cryptography;
using System.Text;

// 登陆窗体类,登录窗体和帐号密码验证界面
/*主要私有方法：
（1）IsLoginSucess()，验证帐号密码
*/
namespace POS.View
{
    /// <summary>
    /// 登陆窗体类,登录窗体和帐号密码验证
    /// </summary>
    public partial class Login : Form
    {
        #region 字段和属性

        /// <summary>
        /// 是否由下线转入登录界面
        /// </summary>
        public bool isOffLine = false;

        /// <summary>
        /// 主窗体
        /// </summary>
        private MainForm mainForm;
        /// <summary>
        /// 主窗体
        /// </summary>
        public MainForm MainForm
        {
            get { return mainForm; }
            set { mainForm = value; }
        }

        /// <summary>
        /// 判断大小写转换的布尔值
        /// </summary>
        bool Capslock = true;

        /// <summary>
        /// 判断暂时离机
        /// </summary>
        public static bool leave = false;


        /// <summary>
        /// 判断光标位置，确定是输入账号还是密码
        /// </summary>
        public bool isUp = true;

        /// <summary>
        /// 返回txtEmp_Id对象
        /// </summary>
        public TextBox TxtEmp_Id
        {
            get { return this.txtEmp_Id; }
        }
        #endregion
        /// <summary>
        /// 构造方法
        /// </summary>
        public Login()
        {
            InitializeComponent();
            //登录窗体居中
            this.StartPosition = FormStartPosition.CenterScreen;
        }

        #region 帐号文本框内容变化事件和点击事件

        /// <summary>
        /// 帐号文本框内容变化后设置光标到文本最后
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtEmp_Id_TextChanged(object sender, EventArgs e)
        {
            txtEmp_Id.SelectionStart = txtEmp_Id.TextLength; //设置光标位置到文本最后
        }

        /// <summary>
        /// 帐号文本框的点击事件，改变isUP的值为ture，此时从软键盘输入的是帐号
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtEmp_Id_Click(object sender, EventArgs e)
        {
            isUp = true;
        }

        #endregion

        #region 密码文本框内容变化事件和点击事件

        /// <summary>
        /// 密码文本框文本内容变化的事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtPass_TextChanged(object sender, EventArgs e)
        {
            txtPass.SelectionStart = txtPass.TextLength; //设置光标位置到文本最后
        }


        /// <summary>
        ///  密码文本框的点击事件，改变isUP的值为false，此时从软键盘输入的是密码
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtPass_Click(object sender, EventArgs e)
        {
            isUp = false;
        }

        #endregion

        #region 软键盘输入的点击事件
        /// <summary>
        /// 软键盘上的Button按钮点击事件，实现输入1
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn1_Click(object sender, EventArgs e)
        {
            if (isUp == false)
            {
                if (txtPass.SelectedText.Length > 0)//判断文本框中是否有选中的内容
                {
                    txtPass.Text = txtPass.Text.Substring(0, txtPass.Text.IndexOf(txtPass.SelectedText)) + "1" + txtPass.Text.Substring(txtPass.Text.IndexOf(txtPass.SelectedText) + txtPass.SelectedText.Length);
                }
                else
                {
                    txtPass.Focus();
                    txtPass.Text += "1";
                }
            }
            else
            {
                if (txtEmp_Id.SelectedText.Length > 0)//判断文本框中是否有选中的内容
                {
                    txtEmp_Id.Text = txtEmp_Id.Text.Substring(0, txtEmp_Id.Text.IndexOf(txtEmp_Id.SelectedText)) + "1" + txtEmp_Id.Text.Substring(txtEmp_Id.Text.IndexOf(txtEmp_Id.SelectedText) + txtEmp_Id.SelectedText.Length);
                }
                else
                {
                    txtEmp_Id.Focus();
                    txtEmp_Id.Text += "1";
                }
            }
        }
        /// <summary>
        /// 软键盘上的Button按钮点击事件，实现输入2 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn2_Click(object sender, EventArgs e)
        {
            if (isUp == false)
            {
                if (txtPass.SelectedText.Length > 0)//判断文本框中是否有选中的内容
                {
                    txtPass.Text = txtPass.Text.Substring(0, txtPass.Text.IndexOf(txtPass.SelectedText)) + "2" + txtPass.Text.Substring(txtPass.Text.IndexOf(txtPass.SelectedText) + txtPass.SelectedText.Length);
                }
                else
                {
                    txtPass.Focus();
                    txtPass.Text += "2";
                }
            }
            else
            {
                if (txtEmp_Id.SelectedText.Length > 0)//判断文本框中是否有选中的内容
                {
                    txtEmp_Id.Text = txtEmp_Id.Text.Substring(0, txtEmp_Id.Text.IndexOf(txtEmp_Id.SelectedText)) + "2" + txtEmp_Id.Text.Substring(txtEmp_Id.Text.IndexOf(txtEmp_Id.SelectedText) + txtEmp_Id.SelectedText.Length);
                }
                else
                {
                    txtEmp_Id.Focus();
                    txtEmp_Id.Text += "2";
                }
            }
        }
        /// <summary>
        /// 软键盘上的Button按钮点击事件，实现输入3 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn3_Click(object sender, EventArgs e)
        {
            if (isUp == false)
            {
                if (txtPass.SelectedText.Length > 0)//判断文本框中是否有选中的内容
                {
                    txtPass.Text = txtPass.Text.Substring(0, txtPass.Text.IndexOf(txtPass.SelectedText)) + "3" + txtPass.Text.Substring(txtPass.Text.IndexOf(txtPass.SelectedText) + txtPass.SelectedText.Length);
                }
                else
                {
                    txtPass.Focus();
                    txtPass.Text += "3";
                }
            }
            else
            {
                if (txtEmp_Id.SelectedText.Length > 0)//判断文本框中是否有选中的内容
                {
                    txtEmp_Id.Text = txtEmp_Id.Text.Substring(0, txtEmp_Id.Text.IndexOf(txtEmp_Id.SelectedText)) + "3" + txtEmp_Id.Text.Substring(txtEmp_Id.Text.IndexOf(txtEmp_Id.SelectedText) + txtEmp_Id.SelectedText.Length);
                }
                else
                {
                    txtEmp_Id.Focus();
                    txtEmp_Id.Text += "3";
                }
            }
        }
        /// <summary>
        /// 软键盘上的Button按钮点击事件，实现输入4 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn4_Click(object sender, EventArgs e)
        {
            if (isUp == false)
            {
                if (txtPass.SelectedText.Length > 0)//判断文本框中是否有选中的内容
                {
                    txtPass.Text = txtPass.Text.Substring(0, txtPass.Text.IndexOf(txtPass.SelectedText)) + "4" + txtPass.Text.Substring(txtPass.Text.IndexOf(txtPass.SelectedText) + txtPass.SelectedText.Length);
                }
                else
                {
                    txtPass.Focus();
                    txtPass.Text += "4";
                }
            }
            else
            {
                if (txtEmp_Id.SelectedText.Length > 0)//判断文本框中是否有选中的内容
                {
                    txtEmp_Id.Text = txtEmp_Id.Text.Substring(0, txtEmp_Id.Text.IndexOf(txtEmp_Id.SelectedText)) + "4" + txtEmp_Id.Text.Substring(txtEmp_Id.Text.IndexOf(txtEmp_Id.SelectedText) + txtEmp_Id.SelectedText.Length);
                }
                else
                {
                    txtEmp_Id.Focus();
                    txtEmp_Id.Text += "4";
                }
            }
        }
        /// <summary>
        /// 软键盘上的Button按钮点击事件，实现输入5 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn5_Click(object sender, EventArgs e)
        {
            if (isUp == false)
            {
                if (txtPass.SelectedText.Length > 0)//判断文本框中是否有选中的内容
                {
                    txtPass.Text = txtPass.Text.Substring(0, txtPass.Text.IndexOf(txtPass.SelectedText)) + "5" + txtPass.Text.Substring(txtPass.Text.IndexOf(txtPass.SelectedText) + txtPass.SelectedText.Length);
                }
                else
                {
                    txtPass.Focus();
                    txtPass.Text += "5";
                }
            }
            else
            {
                if (txtEmp_Id.SelectedText.Length > 0)//判断文本框中是否有选中的内容
                {
                    txtEmp_Id.Text = txtEmp_Id.Text.Substring(0, txtEmp_Id.Text.IndexOf(txtEmp_Id.SelectedText)) + "5" + txtEmp_Id.Text.Substring(txtEmp_Id.Text.IndexOf(txtEmp_Id.SelectedText) + txtEmp_Id.SelectedText.Length);
                }
                else
                {
                    txtEmp_Id.Focus();
                    txtEmp_Id.Text += "5";
                }
            }
        }
        /// <summary>
        /// 软键盘上的Button按钮点击事件，实现输入6 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn6_Click(object sender, EventArgs e)
        {
            if (isUp == false)
            {
                if (txtPass.SelectedText.Length > 0)//判断文本框中是否有选中的内容
                {
                    txtPass.Text = txtPass.Text.Substring(0, txtPass.Text.IndexOf(txtPass.SelectedText)) + "6" + txtPass.Text.Substring(txtPass.Text.IndexOf(txtPass.SelectedText) + txtPass.SelectedText.Length);
                }
                else
                {
                    txtPass.Focus();
                    txtPass.Text += "6";
                }
            }
            else
            {
                if (txtEmp_Id.SelectedText.Length > 0)//判断文本框中是否有选中的内容
                {
                    txtEmp_Id.Text = txtEmp_Id.Text.Substring(0, txtEmp_Id.Text.IndexOf(txtEmp_Id.SelectedText)) + "6" + txtEmp_Id.Text.Substring(txtEmp_Id.Text.IndexOf(txtEmp_Id.SelectedText) + txtEmp_Id.SelectedText.Length);
                }
                else
                {
                    txtEmp_Id.Focus();
                    txtEmp_Id.Text += "6";
                }
            }
        }
        /// <summary>
        /// 软键盘上的Button按钮点击事件，实现输入7
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn7_Click(object sender, EventArgs e)
        {

            if (isUp == false)
            {
                if (txtPass.SelectedText.Length > 0)//判断文本框中是否有选中的内容
                {
                    txtPass.Text = txtPass.Text.Substring(0, txtPass.Text.IndexOf(txtPass.SelectedText)) + "7" + txtPass.Text.Substring(txtPass.Text.IndexOf(txtPass.SelectedText) + txtPass.SelectedText.Length);
                }
                else
                {
                    txtPass.Focus();
                    txtPass.Text += "7";
                }
            }
            else
            {
                if (txtEmp_Id.SelectedText.Length > 0)//判断文本框中是否有选中的内容
                {
                    txtEmp_Id.Text = txtEmp_Id.Text.Substring(0, txtEmp_Id.Text.IndexOf(txtEmp_Id.SelectedText)) + "7" + txtEmp_Id.Text.Substring(txtEmp_Id.Text.IndexOf(txtEmp_Id.SelectedText) + txtEmp_Id.SelectedText.Length);
                }
                else
                {
                    txtEmp_Id.Focus();
                    txtEmp_Id.Text += "7";
                }
            }
        }
        /// <summary>
        /// 软键盘上的Button按钮点击事件，实现输入8 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn8_Click(object sender, EventArgs e)
        {
            if (isUp == false)
            {
                if (txtPass.SelectedText.Length > 0)//判断文本框中是否有选中的内容
                {
                    txtPass.Text = txtPass.Text.Substring(0, txtPass.Text.IndexOf(txtPass.SelectedText)) + "8" + txtPass.Text.Substring(txtPass.Text.IndexOf(txtPass.SelectedText) + txtPass.SelectedText.Length);
                }
                else
                {
                    txtPass.Focus();
                    txtPass.Text += "8";
                }
            }
            else
            {
                if (txtEmp_Id.SelectedText.Length > 0)//判断文本框中是否有选中的内容
                {
                    txtEmp_Id.Text = txtEmp_Id.Text.Substring(0, txtEmp_Id.Text.IndexOf(txtEmp_Id.SelectedText)) + "8" + txtEmp_Id.Text.Substring(txtEmp_Id.Text.IndexOf(txtEmp_Id.SelectedText) + txtEmp_Id.SelectedText.Length);
                }
                else
                {
                    txtEmp_Id.Focus();
                    txtEmp_Id.Text += "8";
                }
            }
        }
        /// <summary>
        /// 软键盘上的Button按钮点击事件，实现输入9 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn9_Click(object sender, EventArgs e)
        {
            if (isUp == false)
            {
                if (txtPass.SelectedText.Length > 0)//判断文本框中是否有选中的内容
                {
                    txtPass.Text = txtPass.Text.Substring(0, txtPass.Text.IndexOf(txtPass.SelectedText)) + "9" + txtPass.Text.Substring(txtPass.Text.IndexOf(txtPass.SelectedText) + txtPass.SelectedText.Length);
                }
                else
                {
                    txtPass.Focus();
                    txtPass.Text += "9";
                }
            }
            else
            {
                if (txtEmp_Id.SelectedText.Length > 0)//判断文本框中是否有选中的内容
                {
                    txtEmp_Id.Text = txtEmp_Id.Text.Substring(0, txtEmp_Id.Text.IndexOf(txtEmp_Id.SelectedText)) + "9" + txtEmp_Id.Text.Substring(txtEmp_Id.Text.IndexOf(txtEmp_Id.SelectedText) + txtEmp_Id.SelectedText.Length);
                }
                else
                {
                    txtEmp_Id.Focus();
                    txtEmp_Id.Text += "9";
                }
            }
        }
        /// <summary>
        /// 软键盘上的Button按钮点击事件，实现输入0 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn0_Click(object sender, EventArgs e)
        {
            if (isUp == false)
            {
                if (txtPass.SelectedText.Length > 0)//判断文本框中是否有选中的内容
                {
                    txtPass.Text = txtPass.Text.Substring(0, txtPass.Text.IndexOf(txtPass.SelectedText)) + "0" + txtPass.Text.Substring(txtPass.Text.IndexOf(txtPass.SelectedText) + txtPass.SelectedText.Length);
                }
                else
                {
                    txtPass.Focus();
                    txtPass.Text += "0";
                }
            }
            else
            {
                if (txtEmp_Id.SelectedText.Length > 0)//判断文本框中是否有选中的内容
                {
                    txtEmp_Id.Text = txtEmp_Id.Text.Substring(0, txtEmp_Id.Text.IndexOf(txtEmp_Id.SelectedText)) + "0" + txtEmp_Id.Text.Substring(txtEmp_Id.Text.IndexOf(txtEmp_Id.SelectedText) + txtEmp_Id.SelectedText.Length);
                }
                else
                {
                    txtEmp_Id.Focus();
                    txtEmp_Id.Text += "0";
                }
            }
        }
        /// <summary>
        /// 软键盘上的Button按钮点击事件，实现输入Q或q
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnQ_Click(object sender, EventArgs e)
        {
            if (isUp == false)
            {
                if (Capslock == true)//判断为大写
                {
                    if (txtPass.SelectedText.Length > 0)//判断文本框中是否有选中的内容
                    {
                        txtPass.Text = txtPass.Text.Substring(0, txtPass.Text.IndexOf(txtPass.SelectedText)) + "Q" + txtPass.Text.Substring(txtPass.Text.IndexOf(txtPass.SelectedText) + txtPass.SelectedText.Length);
                    }
                    else
                    {
                        txtPass.Focus();
                        txtPass.Text += "Q";
                    }


                }
                else
                {
                    if (txtPass.SelectedText.Length > 0)//判断文本框中是否有选中的内容
                    {
                        txtPass.Text = txtPass.Text.Substring(0, txtPass.Text.IndexOf(txtPass.SelectedText)) + "q" + txtPass.Text.Substring(txtPass.Text.IndexOf(txtPass.SelectedText) + txtPass.SelectedText.Length);
                    }
                    else
                    {
                        txtPass.Focus();
                        txtPass.Text += "q";
                    }
                }
            }
            else
            {
                if (Capslock == true)//判断为大写
                {
                    if (txtEmp_Id.SelectedText.Length > 0)//判断文本框中是否有选中的内容
                    {
                        txtEmp_Id.Text = txtEmp_Id.Text.Substring(0, txtEmp_Id.Text.IndexOf(txtEmp_Id.SelectedText)) + "Q" + txtEmp_Id.Text.Substring(txtEmp_Id.Text.IndexOf(txtEmp_Id.SelectedText) + txtEmp_Id.SelectedText.Length);
                    }
                    else
                    {
                        txtEmp_Id.Focus();
                        txtEmp_Id.Text += "Q";
                    }
                }
                else
                {
                    if (txtEmp_Id.SelectedText.Length > 0)//判断文本框中是否有选中的内容
                    {
                        txtEmp_Id.Text = txtEmp_Id.Text.Substring(0, txtEmp_Id.Text.IndexOf(txtEmp_Id.SelectedText)) + "q" + txtEmp_Id.Text.Substring(txtEmp_Id.Text.IndexOf(txtEmp_Id.SelectedText) + txtEmp_Id.SelectedText.Length);
                    }
                    else
                    {
                        txtEmp_Id.Focus();
                        txtEmp_Id.Text += "q";
                    }
                }
            }
        }
        /// <summary>
        /// 软键盘上的Button按钮点击事件，实现输入W或w
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnW_Click(object sender, EventArgs e)
        {
            if (isUp == false)
            {
                if (Capslock == true)//判断为大写
                {
                    if (txtPass.SelectedText.Length > 0)//判断文本框中是否有选中的内容
                    {
                        txtPass.Text = txtPass.Text.Substring(0, txtPass.Text.IndexOf(txtPass.SelectedText)) + "W" + txtPass.Text.Substring(txtPass.Text.IndexOf(txtPass.SelectedText) + txtPass.SelectedText.Length);
                    }
                    else
                    {
                        txtPass.Focus();
                        txtPass.Text += "W";
                    }


                }
                else
                {
                    if (txtPass.SelectedText.Length > 0)//判断文本框中是否有选中的内容
                    {
                        txtPass.Text = txtPass.Text.Substring(0, txtPass.Text.IndexOf(txtPass.SelectedText)) + "w" + txtPass.Text.Substring(txtPass.Text.IndexOf(txtPass.SelectedText) + txtPass.SelectedText.Length);
                    }
                    else
                    {
                        txtPass.Focus();
                        txtPass.Text += "w";
                    }
                }
            }
            else
            {
                if (Capslock == true)//判断为大写
                {
                    if (txtEmp_Id.SelectedText.Length > 0)//判断文本框中是否有选中的内容
                    {
                        txtEmp_Id.Text = txtEmp_Id.Text.Substring(0, txtEmp_Id.Text.IndexOf(txtEmp_Id.SelectedText)) + "W" + txtEmp_Id.Text.Substring(txtEmp_Id.Text.IndexOf(txtEmp_Id.SelectedText) + txtEmp_Id.SelectedText.Length);
                    }
                    else
                    {
                        txtEmp_Id.Focus();
                        txtEmp_Id.Text += "W";
                    }
                }
                else
                {
                    if (txtEmp_Id.SelectedText.Length > 0)//判断文本框中是否有选中的内容
                    {
                        txtEmp_Id.Text = txtEmp_Id.Text.Substring(0, txtEmp_Id.Text.IndexOf(txtEmp_Id.SelectedText)) + "w" + txtEmp_Id.Text.Substring(txtEmp_Id.Text.IndexOf(txtEmp_Id.SelectedText) + txtEmp_Id.SelectedText.Length);
                    }
                    else
                    {
                        txtEmp_Id.Focus();
                        txtEmp_Id.Text += "w";
                    }
                }
            }
        }
        /// <summary>
        /// 软键盘上的Button按钮点击事件，实现输入E或e
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnE_Click(object sender, EventArgs e)
        {
            if (isUp == false)
            {
                if (Capslock == true)//判断为大写
                {
                    if (txtPass.SelectedText.Length > 0)//判断文本框中是否有选中的内容
                    {
                        txtPass.Text = txtPass.Text.Substring(0, txtPass.Text.IndexOf(txtPass.SelectedText)) + "E" + txtPass.Text.Substring(txtPass.Text.IndexOf(txtPass.SelectedText) + txtPass.SelectedText.Length);
                    }
                    else
                    {
                        txtPass.Focus();
                        txtPass.Text += "E";
                    }


                }
                else
                {
                    if (txtPass.SelectedText.Length > 0)//判断文本框中是否有选中的内容
                    {
                        txtPass.Text = txtPass.Text.Substring(0, txtPass.Text.IndexOf(txtPass.SelectedText)) + "e" + txtPass.Text.Substring(txtPass.Text.IndexOf(txtPass.SelectedText) + txtPass.SelectedText.Length);
                    }
                    else
                    {
                        txtPass.Focus();
                        txtPass.Text += "e";
                    }
                }
            }
            else
            {
                if (Capslock == true)//判断为大写
                {
                    if (txtEmp_Id.SelectedText.Length > 0)//判断文本框中是否有选中的内容
                    {
                        txtEmp_Id.Text = txtEmp_Id.Text.Substring(0, txtEmp_Id.Text.IndexOf(txtEmp_Id.SelectedText)) + "E" + txtEmp_Id.Text.Substring(txtEmp_Id.Text.IndexOf(txtEmp_Id.SelectedText) + txtEmp_Id.SelectedText.Length);
                    }
                    else
                    {
                        txtEmp_Id.Focus();
                        txtEmp_Id.Text += "E";
                    }
                }
                else
                {
                    if (txtEmp_Id.SelectedText.Length > 0)//判断文本框中是否有选中的内容
                    {
                        txtEmp_Id.Text = txtEmp_Id.Text.Substring(0, txtEmp_Id.Text.IndexOf(txtEmp_Id.SelectedText)) + "e" + txtEmp_Id.Text.Substring(txtEmp_Id.Text.IndexOf(txtEmp_Id.SelectedText) + txtEmp_Id.SelectedText.Length);
                    }
                    else
                    {
                        txtEmp_Id.Focus();
                        txtEmp_Id.Text += "e";
                    }
                }
            }
        }
        /// <summary>
        /// 软键盘上的Button按钮点击事件，实现输入R或r
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnR_Click(object sender, EventArgs e)
        {
            if (isUp == false)
            {
                if (Capslock == true)//判断为大写
                {
                    if (txtPass.SelectedText.Length > 0)//判断文本框中是否有选中的内容
                    {
                        txtPass.Text = txtPass.Text.Substring(0, txtPass.Text.IndexOf(txtPass.SelectedText)) + "R" + txtPass.Text.Substring(txtPass.Text.IndexOf(txtPass.SelectedText) + txtPass.SelectedText.Length);
                    }
                    else
                    {
                        txtPass.Focus();
                        txtPass.Text += "R";
                    }


                }
                else
                {
                    if (txtPass.SelectedText.Length > 0)//判断文本框中是否有选中的内容
                    {
                        txtPass.Text = txtPass.Text.Substring(0, txtPass.Text.IndexOf(txtPass.SelectedText)) + "r" + txtPass.Text.Substring(txtPass.Text.IndexOf(txtPass.SelectedText) + txtPass.SelectedText.Length);
                    }
                    else
                    {
                        txtPass.Focus();
                        txtPass.Text += "r";
                    }
                }
            }
            else
            {
                if (Capslock == true)//判断为大写
                {
                    if (txtEmp_Id.SelectedText.Length > 0)//判断文本框中是否有选中的内容
                    {
                        txtEmp_Id.Text = txtEmp_Id.Text.Substring(0, txtEmp_Id.Text.IndexOf(txtEmp_Id.SelectedText)) + "R" + txtEmp_Id.Text.Substring(txtEmp_Id.Text.IndexOf(txtEmp_Id.SelectedText) + txtEmp_Id.SelectedText.Length);
                    }
                    else
                    {
                        txtEmp_Id.Focus();
                        txtEmp_Id.Text += "R";
                    }
                }
                else
                {
                    if (txtEmp_Id.SelectedText.Length > 0)//判断文本框中是否有选中的内容
                    {
                        txtEmp_Id.Text = txtEmp_Id.Text.Substring(0, txtEmp_Id.Text.IndexOf(txtEmp_Id.SelectedText)) + "r" + txtEmp_Id.Text.Substring(txtEmp_Id.Text.IndexOf(txtEmp_Id.SelectedText) + txtEmp_Id.SelectedText.Length);
                    }
                    else
                    {
                        txtEmp_Id.Focus();
                        txtEmp_Id.Text += "r";
                    }
                }
            }
        }
        /// <summary>
        /// 软键盘上的Button按钮点击事件，实现输入T或t
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnT_Click(object sender, EventArgs e)
        {
            if (isUp == false)
            {
                if (Capslock == true)//判断为大写
                {
                    if (txtPass.SelectedText.Length > 0)//判断文本框中是否有选中的内容
                    {
                        txtPass.Text = txtPass.Text.Substring(0, txtPass.Text.IndexOf(txtPass.SelectedText)) + "T" + txtPass.Text.Substring(txtPass.Text.IndexOf(txtPass.SelectedText) + txtPass.SelectedText.Length);
                    }
                    else
                    {
                        txtPass.Focus();
                        txtPass.Text += "T";
                    }


                }
                else
                {
                    if (txtPass.SelectedText.Length > 0)//判断文本框中是否有选中的内容
                    {
                        txtPass.Text = txtPass.Text.Substring(0, txtPass.Text.IndexOf(txtPass.SelectedText)) + "t" + txtPass.Text.Substring(txtPass.Text.IndexOf(txtPass.SelectedText) + txtPass.SelectedText.Length);
                    }
                    else
                    {
                        txtPass.Focus();
                        txtPass.Text += "t";
                    }
                }
            }
            else
            {
                if (Capslock == true)//判断为大写
                {
                    if (txtEmp_Id.SelectedText.Length > 0)//判断文本框中是否有选中的内容
                    {
                        txtEmp_Id.Text = txtEmp_Id.Text.Substring(0, txtEmp_Id.Text.IndexOf(txtEmp_Id.SelectedText)) + "T" + txtEmp_Id.Text.Substring(txtEmp_Id.Text.IndexOf(txtEmp_Id.SelectedText) + txtEmp_Id.SelectedText.Length);
                    }
                    else
                    {
                        txtEmp_Id.Focus();
                        txtEmp_Id.Text += "T";
                    }
                }
                else
                {
                    if (txtEmp_Id.SelectedText.Length > 0)//判断文本框中是否有选中的内容
                    {
                        txtEmp_Id.Text = txtEmp_Id.Text.Substring(0, txtEmp_Id.Text.IndexOf(txtEmp_Id.SelectedText)) + "t" + txtEmp_Id.Text.Substring(txtEmp_Id.Text.IndexOf(txtEmp_Id.SelectedText) + txtEmp_Id.SelectedText.Length);
                    }
                    else
                    {
                        txtEmp_Id.Focus();
                        txtEmp_Id.Text += "t";
                    }
                }
            }
        }
        /// <summary>
        /// 软键盘上的Button按钮点击事件，实现输入Y或y
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnY_Click(object sender, EventArgs e)
        {
            if (isUp == false)
            {
                if (Capslock == true)//判断为大写
                {
                    if (txtPass.SelectedText.Length > 0)//判断文本框中是否有选中的内容
                    {
                        txtPass.Text = txtPass.Text.Substring(0, txtPass.Text.IndexOf(txtPass.SelectedText)) + "Y" + txtPass.Text.Substring(txtPass.Text.IndexOf(txtPass.SelectedText) + txtPass.SelectedText.Length);
                    }
                    else
                    {
                        txtPass.Focus();
                        txtPass.Text += "Y";
                    }


                }
                else
                {
                    if (txtPass.SelectedText.Length > 0)//判断文本框中是否有选中的内容
                    {
                        txtPass.Text = txtPass.Text.Substring(0, txtPass.Text.IndexOf(txtPass.SelectedText)) + "y" + txtPass.Text.Substring(txtPass.Text.IndexOf(txtPass.SelectedText) + txtPass.SelectedText.Length);
                    }
                    else
                    {
                        txtPass.Focus();
                        txtPass.Text += "y";
                    }
                }
            }
            else
            {
                if (Capslock == true)//判断为大写
                {
                    if (txtEmp_Id.SelectedText.Length > 0)//判断文本框中是否有选中的内容
                    {
                        txtEmp_Id.Text = txtEmp_Id.Text.Substring(0, txtEmp_Id.Text.IndexOf(txtEmp_Id.SelectedText)) + "Y" + txtEmp_Id.Text.Substring(txtEmp_Id.Text.IndexOf(txtEmp_Id.SelectedText) + txtEmp_Id.SelectedText.Length);
                    }
                    else
                    {
                        txtEmp_Id.Focus();
                        txtEmp_Id.Text += "Y";
                    }
                }
                else
                {
                    if (txtEmp_Id.SelectedText.Length > 0)//判断文本框中是否有选中的内容
                    {
                        txtEmp_Id.Text = txtEmp_Id.Text.Substring(0, txtEmp_Id.Text.IndexOf(txtEmp_Id.SelectedText)) + "y" + txtEmp_Id.Text.Substring(txtEmp_Id.Text.IndexOf(txtEmp_Id.SelectedText) + txtEmp_Id.SelectedText.Length);
                    }
                    else
                    {
                        txtEmp_Id.Focus();
                        txtEmp_Id.Text += "y";
                    }
                }
            }
        }
        /// <summary>
        /// 软键盘上的Button按钮点击事件，实现输入U或u
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnU_Click(object sender, EventArgs e)
        {
            if (isUp == false)
            {
                if (Capslock == true)//判断为大写
                {
                    if (txtPass.SelectedText.Length > 0)//判断文本框中是否有选中的内容
                    {
                        txtPass.Text = txtPass.Text.Substring(0, txtPass.Text.IndexOf(txtPass.SelectedText)) + "U" + txtPass.Text.Substring(txtPass.Text.IndexOf(txtPass.SelectedText) + txtPass.SelectedText.Length);
                    }
                    else
                    {
                        txtPass.Focus();
                        txtPass.Text += "U";
                    }


                }
                else
                {
                    if (txtPass.SelectedText.Length > 0)//判断文本框中是否有选中的内容
                    {
                        txtPass.Text = txtPass.Text.Substring(0, txtPass.Text.IndexOf(txtPass.SelectedText)) + "u" + txtPass.Text.Substring(txtPass.Text.IndexOf(txtPass.SelectedText) + txtPass.SelectedText.Length);
                    }
                    else
                    {
                        txtPass.Focus();
                        txtPass.Text += "u";
                    }
                }
            }
            else
            {
                if (Capslock == true)//判断为大写
                {
                    if (txtEmp_Id.SelectedText.Length > 0)//判断文本框中是否有选中的内容
                    {
                        txtEmp_Id.Text = txtEmp_Id.Text.Substring(0, txtEmp_Id.Text.IndexOf(txtEmp_Id.SelectedText)) + "U" + txtEmp_Id.Text.Substring(txtEmp_Id.Text.IndexOf(txtEmp_Id.SelectedText) + txtEmp_Id.SelectedText.Length);
                    }
                    else
                    {
                        txtEmp_Id.Focus();
                        txtEmp_Id.Text += "U";
                    }
                }
                else
                {
                    if (txtEmp_Id.SelectedText.Length > 0)//判断文本框中是否有选中的内容
                    {
                        txtEmp_Id.Text = txtEmp_Id.Text.Substring(0, txtEmp_Id.Text.IndexOf(txtEmp_Id.SelectedText)) + "u" + txtEmp_Id.Text.Substring(txtEmp_Id.Text.IndexOf(txtEmp_Id.SelectedText) + txtEmp_Id.SelectedText.Length);
                    }
                    else
                    {
                        txtEmp_Id.Focus();
                        txtEmp_Id.Text += "u";
                    }
                }
            }
        }
        /// <summary>
        /// 软键盘上的Button按钮点击事件，实现输入I或i
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnI_Click(object sender, EventArgs e)
        {
            if (isUp == false)
            {
                if (Capslock == true)//判断为大写
                {
                    if (txtPass.SelectedText.Length > 0)//判断文本框中是否有选中的内容
                    {
                        txtPass.Text = txtPass.Text.Substring(0, txtPass.Text.IndexOf(txtPass.SelectedText)) + "I" + txtPass.Text.Substring(txtPass.Text.IndexOf(txtPass.SelectedText) + txtPass.SelectedText.Length);
                    }
                    else
                    {
                        txtPass.Focus();
                        txtPass.Text += "I";
                    }


                }
                else
                {
                    if (txtPass.SelectedText.Length > 0)//判断文本框中是否有选中的内容
                    {
                        txtPass.Text = txtPass.Text.Substring(0, txtPass.Text.IndexOf(txtPass.SelectedText)) + "i" + txtPass.Text.Substring(txtPass.Text.IndexOf(txtPass.SelectedText) + txtPass.SelectedText.Length);
                    }
                    else
                    {
                        txtPass.Focus();
                        txtPass.Text += "i";
                    }
                }
            }
            else
            {
                if (Capslock == true)//判断为大写
                {
                    if (txtEmp_Id.SelectedText.Length > 0)//判断文本框中是否有选中的内容
                    {
                        txtEmp_Id.Text = txtEmp_Id.Text.Substring(0, txtEmp_Id.Text.IndexOf(txtEmp_Id.SelectedText)) + "I" + txtEmp_Id.Text.Substring(txtEmp_Id.Text.IndexOf(txtEmp_Id.SelectedText) + txtEmp_Id.SelectedText.Length);
                    }
                    else
                    {
                        txtEmp_Id.Focus();
                        txtEmp_Id.Text += "I";
                    }
                }
                else
                {
                    if (txtEmp_Id.SelectedText.Length > 0)//判断文本框中是否有选中的内容
                    {
                        txtEmp_Id.Text = txtEmp_Id.Text.Substring(0, txtEmp_Id.Text.IndexOf(txtEmp_Id.SelectedText)) + "i" + txtEmp_Id.Text.Substring(txtEmp_Id.Text.IndexOf(txtEmp_Id.SelectedText) + txtEmp_Id.SelectedText.Length);
                    }
                    else
                    {
                        txtEmp_Id.Focus();
                        txtEmp_Id.Text += "i";
                    }
                }
            }
        }
        /// <summary>
        /// 软键盘上的Button按钮点击事件，实现输入O或o
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnO_Click(object sender, EventArgs e)
        {
            if (isUp == false)
            {
                if (Capslock == true)//判断为大写
                {
                    if (txtPass.SelectedText.Length > 0)//判断文本框中是否有选中的内容
                    {
                        txtPass.Text = txtPass.Text.Substring(0, txtPass.Text.IndexOf(txtPass.SelectedText)) + "O" + txtPass.Text.Substring(txtPass.Text.IndexOf(txtPass.SelectedText) + txtPass.SelectedText.Length);
                    }
                    else
                    {
                        txtPass.Focus();
                        txtPass.Text += "O";
                    }


                }
                else
                {
                    if (txtPass.SelectedText.Length > 0)//判断文本框中是否有选中的内容
                    {
                        txtPass.Text = txtPass.Text.Substring(0, txtPass.Text.IndexOf(txtPass.SelectedText)) + "o" + txtPass.Text.Substring(txtPass.Text.IndexOf(txtPass.SelectedText) + txtPass.SelectedText.Length);
                    }
                    else
                    {
                        txtPass.Focus();
                        txtPass.Text += "o";
                    }
                }
            }
            else
            {
                if (Capslock == true)//判断为大写
                {
                    if (txtEmp_Id.SelectedText.Length > 0)//判断文本框中是否有选中的内容
                    {
                        txtEmp_Id.Text = txtEmp_Id.Text.Substring(0, txtEmp_Id.Text.IndexOf(txtEmp_Id.SelectedText)) + "O" + txtEmp_Id.Text.Substring(txtEmp_Id.Text.IndexOf(txtEmp_Id.SelectedText) + txtEmp_Id.SelectedText.Length);
                    }
                    else
                    {
                        txtEmp_Id.Focus();
                        txtEmp_Id.Text += "O";
                    }
                }
                else
                {
                    if (txtEmp_Id.SelectedText.Length > 0)//判断文本框中是否有选中的内容
                    {
                        txtEmp_Id.Text = txtEmp_Id.Text.Substring(0, txtEmp_Id.Text.IndexOf(txtEmp_Id.SelectedText)) + "o" + txtEmp_Id.Text.Substring(txtEmp_Id.Text.IndexOf(txtEmp_Id.SelectedText) + txtEmp_Id.SelectedText.Length);
                    }
                    else
                    {
                        txtEmp_Id.Focus();
                        txtEmp_Id.Text += "o";
                    }
                }
            }
        }
        /// <summary>
        /// 软键盘上的Button按钮点击事件，实现输入P或p
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnP_Click(object sender, EventArgs e)
        {
            if (isUp == false)
            {
                if (Capslock == true)//判断为大写
                {
                    if (txtPass.SelectedText.Length > 0)//判断文本框中是否有选中的内容
                    {
                        txtPass.Text = txtPass.Text.Substring(0, txtPass.Text.IndexOf(txtPass.SelectedText)) + "P" + txtPass.Text.Substring(txtPass.Text.IndexOf(txtPass.SelectedText) + txtPass.SelectedText.Length);
                    }
                    else
                    {
                        txtPass.Focus();
                        txtPass.Text += "P";
                    }


                }
                else
                {
                    if (txtPass.SelectedText.Length > 0)//判断文本框中是否有选中的内容
                    {
                        txtPass.Text = txtPass.Text.Substring(0, txtPass.Text.IndexOf(txtPass.SelectedText)) + "p" + txtPass.Text.Substring(txtPass.Text.IndexOf(txtPass.SelectedText) + txtPass.SelectedText.Length);
                    }
                    else
                    {
                        txtPass.Focus();
                        txtPass.Text += "p";
                    }
                }
            }
            else
            {
                if (Capslock == true)//判断为大写
                {
                    if (txtEmp_Id.SelectedText.Length > 0)//判断文本框中是否有选中的内容
                    {
                        txtEmp_Id.Text = txtEmp_Id.Text.Substring(0, txtEmp_Id.Text.IndexOf(txtEmp_Id.SelectedText)) + "P" + txtEmp_Id.Text.Substring(txtEmp_Id.Text.IndexOf(txtEmp_Id.SelectedText) + txtEmp_Id.SelectedText.Length);
                    }
                    else
                    {
                        txtEmp_Id.Focus();
                        txtEmp_Id.Text += "P";
                    }
                }
                else
                {
                    if (txtEmp_Id.SelectedText.Length > 0)//判断文本框中是否有选中的内容
                    {
                        txtEmp_Id.Text = txtEmp_Id.Text.Substring(0, txtEmp_Id.Text.IndexOf(txtEmp_Id.SelectedText)) + "p" + txtEmp_Id.Text.Substring(txtEmp_Id.Text.IndexOf(txtEmp_Id.SelectedText) + txtEmp_Id.SelectedText.Length);
                    }
                    else
                    {
                        txtEmp_Id.Focus();
                        txtEmp_Id.Text += "p";
                    }
                }
            }
        }
        /// <summary>
        /// 软键盘上的Button按钮点击事件，实现输入A或a
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnA_Click(object sender, EventArgs e)
        {
            if (isUp == false)
            {
                if (Capslock == true)//判断为大写
                {
                    if (txtPass.SelectedText.Length > 0)//判断文本框中是否有选中的内容
                    {
                        txtPass.Text = txtPass.Text.Substring(0, txtPass.Text.IndexOf(txtPass.SelectedText)) + "A" + txtPass.Text.Substring(txtPass.Text.IndexOf(txtPass.SelectedText) + txtPass.SelectedText.Length);
                    }
                    else
                    {
                        txtPass.Focus();
                        txtPass.Text += "A";
                    }


                }
                else
                {
                    if (txtPass.SelectedText.Length > 0)//判断文本框中是否有选中的内容
                    {
                        txtPass.Text = txtPass.Text.Substring(0, txtPass.Text.IndexOf(txtPass.SelectedText)) + "a" + txtPass.Text.Substring(txtPass.Text.IndexOf(txtPass.SelectedText) + txtPass.SelectedText.Length);
                    }
                    else
                    {
                        txtPass.Focus();
                        txtPass.Text += "a";
                    }
                }
            }
            else
            {
                if (Capslock == true)//判断为大写
                {
                    if (txtEmp_Id.SelectedText.Length > 0)//判断文本框中是否有选中的内容
                    {
                        txtEmp_Id.Text = txtEmp_Id.Text.Substring(0, txtEmp_Id.Text.IndexOf(txtEmp_Id.SelectedText)) + "A" + txtEmp_Id.Text.Substring(txtEmp_Id.Text.IndexOf(txtEmp_Id.SelectedText) + txtEmp_Id.SelectedText.Length);
                    }
                    else
                    {
                        txtEmp_Id.Focus();
                        txtEmp_Id.Text += "A";
                    }
                }
                else
                {
                    if (txtEmp_Id.SelectedText.Length > 0)//判断文本框中是否有选中的内容
                    {
                        txtEmp_Id.Text = txtEmp_Id.Text.Substring(0, txtEmp_Id.Text.IndexOf(txtEmp_Id.SelectedText)) + "a" + txtEmp_Id.Text.Substring(txtEmp_Id.Text.IndexOf(txtEmp_Id.SelectedText) + txtEmp_Id.SelectedText.Length);
                    }
                    else
                    {
                        txtEmp_Id.Focus();
                        txtEmp_Id.Text += "a";
                    }
                }
            }
        }
        /// <summary>
        /// 软键盘上的Button按钮点击事件，实现输入S或s
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnS_Click(object sender, EventArgs e)
        {
            if (isUp == false)
            {
                if (Capslock == true)//判断为大写
                {
                    if (txtPass.SelectedText.Length > 0)//判断文本框中是否有选中的内容
                    {
                        txtPass.Text = txtPass.Text.Substring(0, txtPass.Text.IndexOf(txtPass.SelectedText)) + "S" + txtPass.Text.Substring(txtPass.Text.IndexOf(txtPass.SelectedText) + txtPass.SelectedText.Length);
                    }
                    else
                    {
                        txtPass.Focus();
                        txtPass.Text += "S";
                    }


                }
                else
                {
                    if (txtPass.SelectedText.Length > 0)//判断文本框中是否有选中的内容
                    {
                        txtPass.Text = txtPass.Text.Substring(0, txtPass.Text.IndexOf(txtPass.SelectedText)) + "s" + txtPass.Text.Substring(txtPass.Text.IndexOf(txtPass.SelectedText) + txtPass.SelectedText.Length);
                    }
                    else
                    {
                        txtPass.Focus();
                        txtPass.Text += "s";
                    }
                }
            }
            else
            {
                if (Capslock == true)//判断为大写
                {
                    if (txtEmp_Id.SelectedText.Length > 0)//判断文本框中是否有选中的内容
                    {
                        txtEmp_Id.Text = txtEmp_Id.Text.Substring(0, txtEmp_Id.Text.IndexOf(txtEmp_Id.SelectedText)) + "S" + txtEmp_Id.Text.Substring(txtEmp_Id.Text.IndexOf(txtEmp_Id.SelectedText) + txtEmp_Id.SelectedText.Length);
                    }
                    else
                    {
                        txtEmp_Id.Focus();
                        txtEmp_Id.Text += "S";
                    }
                }
                else
                {
                    if (txtEmp_Id.SelectedText.Length > 0)//判断文本框中是否有选中的内容
                    {
                        txtEmp_Id.Text = txtEmp_Id.Text.Substring(0, txtEmp_Id.Text.IndexOf(txtEmp_Id.SelectedText)) + "s" + txtEmp_Id.Text.Substring(txtEmp_Id.Text.IndexOf(txtEmp_Id.SelectedText) + txtEmp_Id.SelectedText.Length);
                    }
                    else
                    {
                        txtEmp_Id.Focus();
                        txtEmp_Id.Text += "s";
                    }
                }
            }
        }
        /// <summary>
        /// 软键盘上的Button按钮点击事件，实现输入D或d
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnD_Click(object sender, EventArgs e)
        {
            if (isUp == false)
            {
                if (Capslock == true)//判断为大写
                {
                    if (txtPass.SelectedText.Length > 0)//判断文本框中是否有选中的内容
                    {
                        txtPass.Text = txtPass.Text.Substring(0, txtPass.Text.IndexOf(txtPass.SelectedText)) + "D" + txtPass.Text.Substring(txtPass.Text.IndexOf(txtPass.SelectedText) + txtPass.SelectedText.Length);
                    }
                    else
                    {
                        txtPass.Focus();
                        txtPass.Text += "D";
                    }


                }
                else
                {
                    if (txtPass.SelectedText.Length > 0)//判断文本框中是否有选中的内容
                    {
                        txtPass.Text = txtPass.Text.Substring(0, txtPass.Text.IndexOf(txtPass.SelectedText)) + "d" + txtPass.Text.Substring(txtPass.Text.IndexOf(txtPass.SelectedText) + txtPass.SelectedText.Length);
                    }
                    else
                    {
                        txtPass.Focus();
                        txtPass.Text += "d";
                    }
                }
            }
            else
            {
                if (Capslock == true)//判断为大写
                {
                    if (txtEmp_Id.SelectedText.Length > 0)//判断文本框中是否有选中的内容
                    {
                        txtEmp_Id.Text = txtEmp_Id.Text.Substring(0, txtEmp_Id.Text.IndexOf(txtEmp_Id.SelectedText)) + "D" + txtEmp_Id.Text.Substring(txtEmp_Id.Text.IndexOf(txtEmp_Id.SelectedText) + txtEmp_Id.SelectedText.Length);
                    }
                    else
                    {
                        txtEmp_Id.Focus();
                        txtEmp_Id.Text += "D";
                    }
                }
                else
                {
                    if (txtEmp_Id.SelectedText.Length > 0)//判断文本框中是否有选中的内容
                    {
                        txtEmp_Id.Text = txtEmp_Id.Text.Substring(0, txtEmp_Id.Text.IndexOf(txtEmp_Id.SelectedText)) + "d" + txtEmp_Id.Text.Substring(txtEmp_Id.Text.IndexOf(txtEmp_Id.SelectedText) + txtEmp_Id.SelectedText.Length);
                    }
                    else
                    {
                        txtEmp_Id.Focus();
                        txtEmp_Id.Text += "d";
                    }
                }
            }
        }
        /// <summary>
        /// 软键盘上的Button按钮点击事件，实现输入F或f
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnF_Click(object sender, EventArgs e)
        {
            if (isUp == false)
            {
                if (Capslock == true)//判断为大写
                {
                    if (txtPass.SelectedText.Length > 0)//判断文本框中是否有选中的内容
                    {
                        txtPass.Text = txtPass.Text.Substring(0, txtPass.Text.IndexOf(txtPass.SelectedText)) + "F" + txtPass.Text.Substring(txtPass.Text.IndexOf(txtPass.SelectedText) + txtPass.SelectedText.Length);
                    }
                    else
                    {
                        txtPass.Focus();
                        txtPass.Text += "F";
                    }


                }
                else
                {
                    if (txtPass.SelectedText.Length > 0)//判断文本框中是否有选中的内容
                    {
                        txtPass.Text = txtPass.Text.Substring(0, txtPass.Text.IndexOf(txtPass.SelectedText)) + "f" + txtPass.Text.Substring(txtPass.Text.IndexOf(txtPass.SelectedText) + txtPass.SelectedText.Length);
                    }
                    else
                    {
                        txtPass.Focus();
                        txtPass.Text += "f";
                    }
                }
            }
            else
            {
                if (Capslock == true)//判断为大写
                {
                    if (txtEmp_Id.SelectedText.Length > 0)//判断文本框中是否有选中的内容
                    {
                        txtEmp_Id.Text = txtEmp_Id.Text.Substring(0, txtEmp_Id.Text.IndexOf(txtEmp_Id.SelectedText)) + "F" + txtEmp_Id.Text.Substring(txtEmp_Id.Text.IndexOf(txtEmp_Id.SelectedText) + txtEmp_Id.SelectedText.Length);
                    }
                    else
                    {
                        txtEmp_Id.Focus();
                        txtEmp_Id.Text += "F";
                    }
                }
                else
                {
                    if (txtEmp_Id.SelectedText.Length > 0)//判断文本框中是否有选中的内容
                    {
                        txtEmp_Id.Text = txtEmp_Id.Text.Substring(0, txtEmp_Id.Text.IndexOf(txtEmp_Id.SelectedText)) + "f" + txtEmp_Id.Text.Substring(txtEmp_Id.Text.IndexOf(txtEmp_Id.SelectedText) + txtEmp_Id.SelectedText.Length);
                    }
                    else
                    {
                        txtEmp_Id.Focus();
                        txtEmp_Id.Text += "f";
                    }
                }
            }
        }
        /// <summary>
        /// 软键盘上的Button按钮点击事件，实现输入G或g
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnG_Click(object sender, EventArgs e)
        {
            if (isUp == false)
            {
                if (Capslock == true)//判断为大写
                {
                    if (txtPass.SelectedText.Length > 0)//判断文本框中是否有选中的内容
                    {
                        txtPass.Text = txtPass.Text.Substring(0, txtPass.Text.IndexOf(txtPass.SelectedText)) + "G" + txtPass.Text.Substring(txtPass.Text.IndexOf(txtPass.SelectedText) + txtPass.SelectedText.Length);
                    }
                    else
                    {
                        txtPass.Focus();
                        txtPass.Text += "G";
                    }


                }
                else
                {
                    if (txtPass.SelectedText.Length > 0)//判断文本框中是否有选中的内容
                    {
                        txtPass.Text = txtPass.Text.Substring(0, txtPass.Text.IndexOf(txtPass.SelectedText)) + "g" + txtPass.Text.Substring(txtPass.Text.IndexOf(txtPass.SelectedText) + txtPass.SelectedText.Length);
                    }
                    else
                    {
                        txtPass.Focus();
                        txtPass.Text += "g";
                    }
                }
            }
            else
            {
                if (Capslock == true)//判断为大写
                {
                    if (txtEmp_Id.SelectedText.Length > 0)//判断文本框中是否有选中的内容
                    {
                        txtEmp_Id.Text = txtEmp_Id.Text.Substring(0, txtEmp_Id.Text.IndexOf(txtEmp_Id.SelectedText)) + "G" + txtEmp_Id.Text.Substring(txtEmp_Id.Text.IndexOf(txtEmp_Id.SelectedText) + txtEmp_Id.SelectedText.Length);
                    }
                    else
                    {
                        txtEmp_Id.Focus();
                        txtEmp_Id.Text += "G";
                    }
                }
                else
                {
                    if (txtEmp_Id.SelectedText.Length > 0)//判断文本框中是否有选中的内容
                    {
                        txtEmp_Id.Text = txtEmp_Id.Text.Substring(0, txtEmp_Id.Text.IndexOf(txtEmp_Id.SelectedText)) + "g" + txtEmp_Id.Text.Substring(txtEmp_Id.Text.IndexOf(txtEmp_Id.SelectedText) + txtEmp_Id.SelectedText.Length);
                    }
                    else
                    {
                        txtEmp_Id.Focus();
                        txtEmp_Id.Text += "g";
                    }
                }
            }
        }
        /// <summary>
        /// 软键盘上的Button按钮点击事件，实现输入H或h
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnH_Click(object sender, EventArgs e)
        {
            if (isUp == false)
            {
                if (Capslock == true)//判断为大写
                {
                    if (txtPass.SelectedText.Length > 0)//判断文本框中是否有选中的内容
                    {
                        txtPass.Text = txtPass.Text.Substring(0, txtPass.Text.IndexOf(txtPass.SelectedText)) + "H" + txtPass.Text.Substring(txtPass.Text.IndexOf(txtPass.SelectedText) + txtPass.SelectedText.Length);
                    }
                    else
                    {
                        txtPass.Focus();
                        txtPass.Text += "H";
                    }


                }
                else
                {
                    if (txtPass.SelectedText.Length > 0)//判断文本框中是否有选中的内容
                    {
                        txtPass.Text = txtPass.Text.Substring(0, txtPass.Text.IndexOf(txtPass.SelectedText)) + "h" + txtPass.Text.Substring(txtPass.Text.IndexOf(txtPass.SelectedText) + txtPass.SelectedText.Length);
                    }
                    else
                    {
                        txtPass.Focus();
                        txtPass.Text += "h";
                    }
                }
            }
            else
            {
                if (Capslock == true)//判断为大写
                {
                    if (txtEmp_Id.SelectedText.Length > 0)//判断文本框中是否有选中的内容
                    {
                        txtEmp_Id.Text = txtEmp_Id.Text.Substring(0, txtEmp_Id.Text.IndexOf(txtEmp_Id.SelectedText)) + "H" + txtEmp_Id.Text.Substring(txtEmp_Id.Text.IndexOf(txtEmp_Id.SelectedText) + txtEmp_Id.SelectedText.Length);
                    }
                    else
                    {
                        txtEmp_Id.Focus();
                        txtEmp_Id.Text += "H";
                    }
                }
                else
                {
                    if (txtEmp_Id.SelectedText.Length > 0)//判断文本框中是否有选中的内容
                    {
                        txtEmp_Id.Text = txtEmp_Id.Text.Substring(0, txtEmp_Id.Text.IndexOf(txtEmp_Id.SelectedText)) + "h" + txtEmp_Id.Text.Substring(txtEmp_Id.Text.IndexOf(txtEmp_Id.SelectedText) + txtEmp_Id.SelectedText.Length);
                    }
                    else
                    {
                        txtEmp_Id.Focus();
                        txtEmp_Id.Text += "h";
                    }
                }
            }
        }
        /// <summary>
        /// 软键盘上的Button按钮点击事件，实现输入J或j
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnJ_Click(object sender, EventArgs e)
        {
            if (isUp == false)
            {
                if (Capslock == true)//判断为大写
                {
                    if (txtPass.SelectedText.Length > 0)//判断文本框中是否有选中的内容
                    {
                        txtPass.Text = txtPass.Text.Substring(0, txtPass.Text.IndexOf(txtPass.SelectedText)) + "J" + txtPass.Text.Substring(txtPass.Text.IndexOf(txtPass.SelectedText) + txtPass.SelectedText.Length);
                    }
                    else
                    {
                        txtPass.Focus();
                        txtPass.Text += "J";
                    }


                }
                else
                {
                    if (txtPass.SelectedText.Length > 0)//判断文本框中是否有选中的内容
                    {
                        txtPass.Text = txtPass.Text.Substring(0, txtPass.Text.IndexOf(txtPass.SelectedText)) + "j" + txtPass.Text.Substring(txtPass.Text.IndexOf(txtPass.SelectedText) + txtPass.SelectedText.Length);
                    }
                    else
                    {
                        txtPass.Focus();
                        txtPass.Text += "j";
                    }
                }
            }
            else
            {
                if (Capslock == true)//判断为大写
                {
                    if (txtEmp_Id.SelectedText.Length > 0)//判断文本框中是否有选中的内容
                    {
                        txtEmp_Id.Text = txtEmp_Id.Text.Substring(0, txtEmp_Id.Text.IndexOf(txtEmp_Id.SelectedText)) + "J" + txtEmp_Id.Text.Substring(txtEmp_Id.Text.IndexOf(txtEmp_Id.SelectedText) + txtEmp_Id.SelectedText.Length);
                    }
                    else
                    {
                        txtEmp_Id.Focus();
                        txtEmp_Id.Text += "J";
                    }
                }
                else
                {
                    if (txtEmp_Id.SelectedText.Length > 0)//判断文本框中是否有选中的内容
                    {
                        txtEmp_Id.Text = txtEmp_Id.Text.Substring(0, txtEmp_Id.Text.IndexOf(txtEmp_Id.SelectedText)) + "j" + txtEmp_Id.Text.Substring(txtEmp_Id.Text.IndexOf(txtEmp_Id.SelectedText) + txtEmp_Id.SelectedText.Length);
                    }
                    else
                    {
                        txtEmp_Id.Focus();
                        txtEmp_Id.Text += "j";
                    }
                }
            }
        }
        /// <summary>
        /// 软键盘上的Button按钮点击事件，实现输入K或k
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnK_Click(object sender, EventArgs e)
        {
            if (isUp == false)
            {
                if (Capslock == true)//判断为大写
                {
                    if (txtPass.SelectedText.Length > 0)//判断文本框中是否有选中的内容
                    {
                        txtPass.Text = txtPass.Text.Substring(0, txtPass.Text.IndexOf(txtPass.SelectedText)) + "K" + txtPass.Text.Substring(txtPass.Text.IndexOf(txtPass.SelectedText) + txtPass.SelectedText.Length);
                    }
                    else
                    {
                        txtPass.Focus();
                        txtPass.Text += "K";
                    }


                }
                else
                {
                    if (txtPass.SelectedText.Length > 0)//判断文本框中是否有选中的内容
                    {
                        txtPass.Text = txtPass.Text.Substring(0, txtPass.Text.IndexOf(txtPass.SelectedText)) + "k" + txtPass.Text.Substring(txtPass.Text.IndexOf(txtPass.SelectedText) + txtPass.SelectedText.Length);
                    }
                    else
                    {
                        txtPass.Focus();
                        txtPass.Text += "k";
                    }
                }
            }
            else
            {
                if (Capslock == true)//判断为大写
                {
                    if (txtEmp_Id.SelectedText.Length > 0)//判断文本框中是否有选中的内容
                    {
                        txtEmp_Id.Text = txtEmp_Id.Text.Substring(0, txtEmp_Id.Text.IndexOf(txtEmp_Id.SelectedText)) + "K" + txtEmp_Id.Text.Substring(txtEmp_Id.Text.IndexOf(txtEmp_Id.SelectedText) + txtEmp_Id.SelectedText.Length);
                    }
                    else
                    {
                        txtEmp_Id.Focus();
                        txtEmp_Id.Text += "K";
                    }
                }
                else
                {
                    if (txtEmp_Id.SelectedText.Length > 0)//判断文本框中是否有选中的内容
                    {
                        txtEmp_Id.Text = txtEmp_Id.Text.Substring(0, txtEmp_Id.Text.IndexOf(txtEmp_Id.SelectedText)) + "k" + txtEmp_Id.Text.Substring(txtEmp_Id.Text.IndexOf(txtEmp_Id.SelectedText) + txtEmp_Id.SelectedText.Length);
                    }
                    else
                    {
                        txtEmp_Id.Focus();
                        txtEmp_Id.Text += "k";
                    }
                }
            }
        }
        /// <summary>
        /// 软键盘上的Button按钮点击事件，实现输入L或l
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnL_Click(object sender, EventArgs e)
        {
            if (isUp == false)
            {
                if (Capslock == true)//判断为大写
                {
                    if (txtPass.SelectedText.Length > 0)//判断文本框中是否有选中的内容
                    {
                        txtPass.Text = txtPass.Text.Substring(0, txtPass.Text.IndexOf(txtPass.SelectedText)) + "L" + txtPass.Text.Substring(txtPass.Text.IndexOf(txtPass.SelectedText) + txtPass.SelectedText.Length);
                    }
                    else
                    {
                        txtPass.Focus();
                        txtPass.Text += "L";
                    }


                }
                else
                {
                    if (txtPass.SelectedText.Length > 0)//判断文本框中是否有选中的内容
                    {
                        txtPass.Text = txtPass.Text.Substring(0, txtPass.Text.IndexOf(txtPass.SelectedText)) + "l" + txtPass.Text.Substring(txtPass.Text.IndexOf(txtPass.SelectedText) + txtPass.SelectedText.Length);
                    }
                    else
                    {
                        txtPass.Focus();
                        txtPass.Text += "l";
                    }
                }
            }
            else
            {
                if (Capslock == true)//判断为大写
                {
                    if (txtEmp_Id.SelectedText.Length > 0)//判断文本框中是否有选中的内容
                    {
                        txtEmp_Id.Text = txtEmp_Id.Text.Substring(0, txtEmp_Id.Text.IndexOf(txtEmp_Id.SelectedText)) + "L" + txtEmp_Id.Text.Substring(txtEmp_Id.Text.IndexOf(txtEmp_Id.SelectedText) + txtEmp_Id.SelectedText.Length);
                    }
                    else
                    {
                        txtEmp_Id.Focus();
                        txtEmp_Id.Text += "L";
                    }
                }
                else
                {
                    if (txtEmp_Id.SelectedText.Length > 0)//判断文本框中是否有选中的内容
                    {
                        txtEmp_Id.Text = txtEmp_Id.Text.Substring(0, txtEmp_Id.Text.IndexOf(txtEmp_Id.SelectedText)) + "l" + txtEmp_Id.Text.Substring(txtEmp_Id.Text.IndexOf(txtEmp_Id.SelectedText) + txtEmp_Id.SelectedText.Length);
                    }
                    else
                    {
                        txtEmp_Id.Focus();
                        txtEmp_Id.Text += "l";
                    }
                }
            }
        }
        /// <summary>
        /// 软键盘上的Button按钮点击事件，实现输入Z或z
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnZ_Click(object sender, EventArgs e)
        {
            if (isUp == false)
            {
                if (Capslock == true)//判断为大写
                {
                    if (txtPass.SelectedText.Length > 0)//判断文本框中是否有选中的内容
                    {
                        txtPass.Text = txtPass.Text.Substring(0, txtPass.Text.IndexOf(txtPass.SelectedText)) + "Z" + txtPass.Text.Substring(txtPass.Text.IndexOf(txtPass.SelectedText) + txtPass.SelectedText.Length);
                    }
                    else
                    {
                        txtPass.Focus();
                        txtPass.Text += "Z";
                    }


                }
                else
                {
                    if (txtPass.SelectedText.Length > 0)//判断文本框中是否有选中的内容
                    {
                        txtPass.Text = txtPass.Text.Substring(0, txtPass.Text.IndexOf(txtPass.SelectedText)) + "z" + txtPass.Text.Substring(txtPass.Text.IndexOf(txtPass.SelectedText) + txtPass.SelectedText.Length);
                    }
                    else
                    {
                        txtPass.Focus();
                        txtPass.Text += "z";
                    }
                }
            }
            else
            {
                if (Capslock == true)//判断为大写
                {
                    if (txtEmp_Id.SelectedText.Length > 0)//判断文本框中是否有选中的内容
                    {
                        txtEmp_Id.Text = txtEmp_Id.Text.Substring(0, txtEmp_Id.Text.IndexOf(txtEmp_Id.SelectedText)) + "Z" + txtEmp_Id.Text.Substring(txtEmp_Id.Text.IndexOf(txtEmp_Id.SelectedText) + txtEmp_Id.SelectedText.Length);
                    }
                    else
                    {
                        txtEmp_Id.Focus();
                        txtEmp_Id.Text += "Z";
                    }
                }
                else
                {
                    if (txtEmp_Id.SelectedText.Length > 0)//判断文本框中是否有选中的内容
                    {
                        txtEmp_Id.Text = txtEmp_Id.Text.Substring(0, txtEmp_Id.Text.IndexOf(txtEmp_Id.SelectedText)) + "z" + txtEmp_Id.Text.Substring(txtEmp_Id.Text.IndexOf(txtEmp_Id.SelectedText) + txtEmp_Id.SelectedText.Length);
                    }
                    else
                    {
                        txtEmp_Id.Focus();
                        txtEmp_Id.Text += "z";
                    }
                }
            }
        }
        /// <summary>
        /// 软键盘上的Button按钮点击事件，实现输入X或x
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnX_Click(object sender, EventArgs e)
        {
            if (isUp == false)
            {
                if (Capslock == true)//判断为大写
                {
                    if (txtPass.SelectedText.Length > 0)//判断文本框中是否有选中的内容
                    {
                        txtPass.Text = txtPass.Text.Substring(0, txtPass.Text.IndexOf(txtPass.SelectedText)) + "X" + txtPass.Text.Substring(txtPass.Text.IndexOf(txtPass.SelectedText) + txtPass.SelectedText.Length);
                    }
                    else
                    {
                        txtPass.Focus();
                        txtPass.Text += "X";
                    }


                }
                else
                {
                    if (txtPass.SelectedText.Length > 0)//判断文本框中是否有选中的内容
                    {
                        txtPass.Text = txtPass.Text.Substring(0, txtPass.Text.IndexOf(txtPass.SelectedText)) + "x" + txtPass.Text.Substring(txtPass.Text.IndexOf(txtPass.SelectedText) + txtPass.SelectedText.Length);
                    }
                    else
                    {
                        txtPass.Focus();
                        txtPass.Text += "x";
                    }
                }
            }
            else
            {
                if (Capslock == true)//判断为大写
                {
                    if (txtEmp_Id.SelectedText.Length > 0)//判断文本框中是否有选中的内容
                    {
                        txtEmp_Id.Text = txtEmp_Id.Text.Substring(0, txtEmp_Id.Text.IndexOf(txtEmp_Id.SelectedText)) + "X" + txtEmp_Id.Text.Substring(txtEmp_Id.Text.IndexOf(txtEmp_Id.SelectedText) + txtEmp_Id.SelectedText.Length);
                    }
                    else
                    {
                        txtEmp_Id.Focus();
                        txtEmp_Id.Text += "X";
                    }
                }
                else
                {
                    if (txtEmp_Id.SelectedText.Length > 0)//判断文本框中是否有选中的内容
                    {
                        txtEmp_Id.Text = txtEmp_Id.Text.Substring(0, txtEmp_Id.Text.IndexOf(txtEmp_Id.SelectedText)) + "x" + txtEmp_Id.Text.Substring(txtEmp_Id.Text.IndexOf(txtEmp_Id.SelectedText) + txtEmp_Id.SelectedText.Length);
                    }
                    else
                    {
                        txtEmp_Id.Focus();
                        txtEmp_Id.Text += "x";
                    }
                }
            }
        }
        /// <summary>
        /// 软键盘上的Button按钮点击事件，实现输入C或c
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnC_Click(object sender, EventArgs e)
        {
            if (isUp == false)
            {
                if (Capslock == true)//判断为大写
                {
                    if (txtPass.SelectedText.Length > 0)//判断文本框中是否有选中的内容
                    {
                        txtPass.Text = txtPass.Text.Substring(0, txtPass.Text.IndexOf(txtPass.SelectedText)) + "C" + txtPass.Text.Substring(txtPass.Text.IndexOf(txtPass.SelectedText) + txtPass.SelectedText.Length);
                    }
                    else
                    {
                        txtPass.Focus();
                        txtPass.Text += "C";
                    }


                }
                else
                {
                    if (txtPass.SelectedText.Length > 0)//判断文本框中是否有选中的内容
                    {
                        txtPass.Text = txtPass.Text.Substring(0, txtPass.Text.IndexOf(txtPass.SelectedText)) + "c" + txtPass.Text.Substring(txtPass.Text.IndexOf(txtPass.SelectedText) + txtPass.SelectedText.Length);
                    }
                    else
                    {
                        txtPass.Focus();
                        txtPass.Text += "c";
                    }
                }
            }
            else
            {
                if (Capslock == true)//判断为大写
                {
                    if (txtEmp_Id.SelectedText.Length > 0)//判断文本框中是否有选中的内容
                    {
                        txtEmp_Id.Text = txtEmp_Id.Text.Substring(0, txtEmp_Id.Text.IndexOf(txtEmp_Id.SelectedText)) + "C" + txtEmp_Id.Text.Substring(txtEmp_Id.Text.IndexOf(txtEmp_Id.SelectedText) + txtEmp_Id.SelectedText.Length);
                    }
                    else
                    {
                        txtEmp_Id.Focus();
                        txtEmp_Id.Text += "C";
                    }
                }
                else
                {
                    if (txtEmp_Id.SelectedText.Length > 0)//判断文本框中是否有选中的内容
                    {
                        txtEmp_Id.Text = txtEmp_Id.Text.Substring(0, txtEmp_Id.Text.IndexOf(txtEmp_Id.SelectedText)) + "c" + txtEmp_Id.Text.Substring(txtEmp_Id.Text.IndexOf(txtEmp_Id.SelectedText) + txtEmp_Id.SelectedText.Length);
                    }
                    else
                    {
                        txtEmp_Id.Focus();
                        txtEmp_Id.Text += "c";
                    }
                }
            }
        }
        /// <summary>
        /// 软键盘上的Button按钮点击事件，实现输入V或v
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnV_Click(object sender, EventArgs e)
        {
            if (isUp == false)
            {
                if (Capslock == true)//判断为大写
                {
                    if (txtPass.SelectedText.Length > 0)//判断文本框中是否有选中的内容
                    {
                        txtPass.Text = txtPass.Text.Substring(0, txtPass.Text.IndexOf(txtPass.SelectedText)) + "V" + txtPass.Text.Substring(txtPass.Text.IndexOf(txtPass.SelectedText) + txtPass.SelectedText.Length);
                    }
                    else
                    {
                        txtPass.Focus();
                        txtPass.Text += "V";
                    }


                }
                else
                {
                    if (txtPass.SelectedText.Length > 0)//判断文本框中是否有选中的内容
                    {
                        txtPass.Text = txtPass.Text.Substring(0, txtPass.Text.IndexOf(txtPass.SelectedText)) + "v" + txtPass.Text.Substring(txtPass.Text.IndexOf(txtPass.SelectedText) + txtPass.SelectedText.Length);
                    }
                    else
                    {
                        txtPass.Focus();
                        txtPass.Text += "v";
                    }
                }
            }
            else
            {
                if (Capslock == true)//判断为大写
                {
                    if (txtEmp_Id.SelectedText.Length > 0)//判断文本框中是否有选中的内容
                    {
                        txtEmp_Id.Text = txtEmp_Id.Text.Substring(0, txtEmp_Id.Text.IndexOf(txtEmp_Id.SelectedText)) + "V" + txtEmp_Id.Text.Substring(txtEmp_Id.Text.IndexOf(txtEmp_Id.SelectedText) + txtEmp_Id.SelectedText.Length);
                    }
                    else
                    {
                        txtEmp_Id.Focus();
                        txtEmp_Id.Text += "V";
                    }
                }
                else
                {
                    if (txtEmp_Id.SelectedText.Length > 0)//判断文本框中是否有选中的内容
                    {
                        txtEmp_Id.Text = txtEmp_Id.Text.Substring(0, txtEmp_Id.Text.IndexOf(txtEmp_Id.SelectedText)) + "v" + txtEmp_Id.Text.Substring(txtEmp_Id.Text.IndexOf(txtEmp_Id.SelectedText) + txtEmp_Id.SelectedText.Length);
                    }
                    else
                    {
                        txtEmp_Id.Focus();
                        txtEmp_Id.Text += "v";
                    }
                }
            }
        }
        /// <summary>
        /// 软键盘上的Button按钮点击事件，实现输入B或b
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnB_Click(object sender, EventArgs e)
        {
            if (isUp == false)
            {
                if (Capslock == true)//判断为大写
                {
                    if (txtPass.SelectedText.Length > 0)//判断文本框中是否有选中的内容
                    {
                        txtPass.Text = txtPass.Text.Substring(0, txtPass.Text.IndexOf(txtPass.SelectedText)) + "B" + txtPass.Text.Substring(txtPass.Text.IndexOf(txtPass.SelectedText) + txtPass.SelectedText.Length);
                    }
                    else
                    {
                        txtPass.Focus();
                        txtPass.Text += "B";
                    }


                }
                else
                {
                    if (txtPass.SelectedText.Length > 0)//判断文本框中是否有选中的内容
                    {
                        txtPass.Text = txtPass.Text.Substring(0, txtPass.Text.IndexOf(txtPass.SelectedText)) + "b" + txtPass.Text.Substring(txtPass.Text.IndexOf(txtPass.SelectedText) + txtPass.SelectedText.Length);
                    }
                    else
                    {
                        txtPass.Focus();
                        txtPass.Text += "b";
                    }
                }
            }
            else
            {
                if (Capslock == true)//判断为大写
                {
                    if (txtEmp_Id.SelectedText.Length > 0)//判断文本框中是否有选中的内容
                    {
                        txtEmp_Id.Text = txtEmp_Id.Text.Substring(0, txtEmp_Id.Text.IndexOf(txtEmp_Id.SelectedText)) + "B" + txtEmp_Id.Text.Substring(txtEmp_Id.Text.IndexOf(txtEmp_Id.SelectedText) + txtEmp_Id.SelectedText.Length);
                    }
                    else
                    {
                        txtEmp_Id.Focus();
                        txtEmp_Id.Text += "B";
                    }
                }
                else
                {
                    if (txtEmp_Id.SelectedText.Length > 0)//判断文本框中是否有选中的内容
                    {
                        txtEmp_Id.Text = txtEmp_Id.Text.Substring(0, txtEmp_Id.Text.IndexOf(txtEmp_Id.SelectedText)) + "b" + txtEmp_Id.Text.Substring(txtEmp_Id.Text.IndexOf(txtEmp_Id.SelectedText) + txtEmp_Id.SelectedText.Length);
                    }
                    else
                    {
                        txtEmp_Id.Focus();
                        txtEmp_Id.Text += "b";
                    }
                }
            }
        }
        /// <summary>
        /// 软键盘上的Button按钮点击事件，实现输入N或n
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnN_Click(object sender, EventArgs e)
        {
            if (isUp == false)
            {
                if (Capslock == true)//判断为大写
                {
                    if (txtPass.SelectedText.Length > 0)//判断文本框中是否有选中的内容
                    {
                        txtPass.Text = txtPass.Text.Substring(0, txtPass.Text.IndexOf(txtPass.SelectedText)) + "N" + txtPass.Text.Substring(txtPass.Text.IndexOf(txtPass.SelectedText) + txtPass.SelectedText.Length);
                    }
                    else
                    {
                        txtPass.Focus();
                        txtPass.Text += "N";
                    }


                }
                else
                {
                    if (txtPass.SelectedText.Length > 0)//判断文本框中是否有选中的内容
                    {
                        txtPass.Text = txtPass.Text.Substring(0, txtPass.Text.IndexOf(txtPass.SelectedText)) + "n" + txtPass.Text.Substring(txtPass.Text.IndexOf(txtPass.SelectedText) + txtPass.SelectedText.Length);
                    }
                    else
                    {
                        txtPass.Focus();
                        txtPass.Text += "n";
                    }
                }
            }
            else
            {
                if (Capslock == true)//判断为大写
                {
                    if (txtEmp_Id.SelectedText.Length > 0)//判断文本框中是否有选中的内容
                    {
                        txtEmp_Id.Text = txtEmp_Id.Text.Substring(0, txtEmp_Id.Text.IndexOf(txtEmp_Id.SelectedText)) + "N" + txtEmp_Id.Text.Substring(txtEmp_Id.Text.IndexOf(txtEmp_Id.SelectedText) + txtEmp_Id.SelectedText.Length);
                    }
                    else
                    {
                        txtEmp_Id.Focus();
                        txtEmp_Id.Text += "N";
                    }
                }
                else
                {
                    if (txtEmp_Id.SelectedText.Length > 0)//判断文本框中是否有选中的内容
                    {
                        txtEmp_Id.Text = txtEmp_Id.Text.Substring(0, txtEmp_Id.Text.IndexOf(txtEmp_Id.SelectedText)) + "n" + txtEmp_Id.Text.Substring(txtEmp_Id.Text.IndexOf(txtEmp_Id.SelectedText) + txtEmp_Id.SelectedText.Length);
                    }
                    else
                    {
                        txtEmp_Id.Focus();
                        txtEmp_Id.Text += "n";
                    }
                }
            }
        }
        /// <summary>
        /// 软键盘上的Button按钮点击事件，实现输入M或m
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnM_Click(object sender, EventArgs e)
        {
            if (isUp == false)
            {
                if (Capslock == true)//判断为大写
                {
                    if (txtPass.SelectedText.Length > 0)//判断文本框中是否有选中的内容
                    {
                        txtPass.Text = txtPass.Text.Substring(0, txtPass.Text.IndexOf(txtPass.SelectedText)) + "M" + txtPass.Text.Substring(txtPass.Text.IndexOf(txtPass.SelectedText) + txtPass.SelectedText.Length);
                    }
                    else
                    {
                        txtPass.Focus();
                        txtPass.Text += "M";
                    }


                }
                else
                {
                    if (txtPass.SelectedText.Length > 0)//判断文本框中是否有选中的内容
                    {
                        txtPass.Text = txtPass.Text.Substring(0, txtPass.Text.IndexOf(txtPass.SelectedText)) + "m" + txtPass.Text.Substring(txtPass.Text.IndexOf(txtPass.SelectedText) + txtPass.SelectedText.Length);
                    }
                    else
                    {
                        txtPass.Focus();
                        txtPass.Text += "m";
                    }
                }
            }
            else
            {
                if (Capslock == true)//判断为大写
                {
                    if (txtEmp_Id.SelectedText.Length > 0)//判断文本框中是否有选中的内容
                    {
                        txtEmp_Id.Text = txtEmp_Id.Text.Substring(0, txtEmp_Id.Text.IndexOf(txtEmp_Id.SelectedText)) + "M" + txtEmp_Id.Text.Substring(txtEmp_Id.Text.IndexOf(txtEmp_Id.SelectedText) + txtEmp_Id.SelectedText.Length);
                    }
                    else
                    {
                        txtEmp_Id.Focus();
                        txtEmp_Id.Text += "M";
                    }
                }
                else
                {
                    if (txtEmp_Id.SelectedText.Length > 0)//判断文本框中是否有选中的内容
                    {
                        txtEmp_Id.Text = txtEmp_Id.Text.Substring(0, txtEmp_Id.Text.IndexOf(txtEmp_Id.SelectedText)) + "m" + txtEmp_Id.Text.Substring(txtEmp_Id.Text.IndexOf(txtEmp_Id.SelectedText) + txtEmp_Id.SelectedText.Length);
                    }
                    else
                    {
                        txtEmp_Id.Focus();
                        txtEmp_Id.Text += "m";
                    }
                }
            }
        }

        #endregion

        #region 软键盘功能按钮的点击事件
        /// <summary>
        ///  软键盘上的Button按钮点击事件，实现改变关于大小写的Capslock的值
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CapsLock_Click(object sender, EventArgs e)
        {
            if (Capslock == true)
            {
                Capslock = false;
            }
            else
            {
                Capslock = true;
            }

        }

        /// <summary>
        /// 软键盘上的Button按钮点击事件，实现关于大小写的Capslock的值的改变
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (isUp == false)//判断确定对密码框进行操作
            {
                if (txtPass.SelectedText.Length > 0)//判断文本框中是否有选中的内容
                {
                    Pass_selectedtext();
                }

                else
                {
                    txtPass.Focus();
                    txtPass.Text = "";
                }
            }
            else  //对帐号框进行操作
            {
                if (txtEmp_Id.SelectedText.Length > 0)//判断文本框中是否有选中的内容
                {
                    Emp_Id_selectedtext();
                }
                else
                {
                    txtEmp_Id.Focus();
                    txtEmp_Id.Text = "";
                }
            }


        }


        /// <summary>
        /// 软键盘上的Backspace按钮点击事件，实现删除当前文本框的字符串的最后一位(退格清除)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Backspace_Click(object sender, EventArgs e)
        {
            Backspace_check();
        }

        /// <summary>
        /// 确定按钮点击事件，调用LoginCheck()方法
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Done_Click(object sender, EventArgs e)
        {
            Done.Enabled = false;
            exit.Enabled = false;
            IsLoginSucess();//调用IsLoginSucess()方法
            mainForm.Login.TxtEmp_Id.Focus();
            mainForm.Login.isUp = true;
        }
        /// <summary>
        /// 回车(Enter)按钮点击事件，调用LoginCheck()方法
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Enter_Click(object sender, EventArgs e)
        {
            Done.Enabled = false;
            exit.Enabled = false;
            IsLoginSucess();//调用IsLoginSucess()方法
            mainForm.Login.TxtEmp_Id.Focus();
            mainForm.Login.isUp = true;
        }
        /// <summary>
        /// 退出按钮点击事件，
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void exit_Click(object sender, EventArgs e)
        {
            Info.isexit = true;
            this.Dispose();//关闭窗口
            Application.Exit();//退出系统
        }
        #endregion


        #region 软键盘功能按钮的点击事件中调用的方法
        /// <summary>
        /// 调用验证帐号密码
        /// </summary>
        private void IsLoginSucess()
        {
            GetUserInfo getUserInfo = new GetUserInfo();//声明并创建GetUserInfo类的对象getUserInfo
            getUserInfo.UserInfo(this.txtEmp_Id.Text);//调用GetUserInfo类的UserInfo方法，并传递用户输入的帐号，密码去验证
            //保存员工等级
            Info.emp_level = getUserInfo.ReturenEmp_level;

            ReadIni readIni = new ReadIni("Info.ini");


            #region 如满足条件清空班别及Info.ini中的相关信息
            Clear clear = new Clear();
            clear.ClearTime();
            #endregion
            if (getUserInfo.IsLogin(txtPass.Text.Trim()))//调用getUserInfo.IsLogin()方法返回true，则执行下列操作
            {
                #region 设置班别
                try
                {
                    string shift_num = Convert.ToString(Convert.ToInt32(this.mainForm.OperPara.GetIniConfig("workNumber")) + 1);
                    if (shift_num.Length == 1)
                    {
                        Info.shift_num = "0" + shift_num;
                        this.mainForm.OperPara.WriteIniConfig("workNumber", Info.shift_num);
                    }
                    else
                    {
                        Info.shift_num = shift_num;
                        this.mainForm.OperPara.WriteIniConfig("workNumber", Info.shift_num);
                    }
                }
                catch
                {
                    Info.shift_num = "1";
                    this.mainForm.OperPara.WriteIniConfig("workNumber", Info.shift_num);
                }
                #endregion

                #region 往Info中写入相关信息
                Info.login_date = DateTime.Now;
                this.mainForm.OperPara.WriteIniInfo("login_date", Info.login_date.ToString());
                //上次上线时间
                this.mainForm.OperPara.WriteIniConfig("preLoginTime", Info.login_date.ToString());
                //员工编号
                Info.emp_id = getUserInfo.ReturenEmp_Id;//调用getUserInfo.ReturenEmp_Id属性并将返回值赋值给Info.emp_id 
                this.mainForm.OperPara.WriteIniInfo("emp_id", this.txtEmp_Id.Text);
                //员工密码
                Info.password = getUserInfo.ReturenPass;//调用getUserInfo.ReturenPass属性并将返回值赋值给Info.password 
                //店铺编号
                Info.shop_id = getUserInfo.ReturenShop_id;//调用getUserInfo.ReturenShop_id属性并将返回值赋值给Info.shop_id 
                this.mainForm.OperPara.WriteIniInfo("shop_id", Info.shop_id);
                //员工名字
                Info.emp_name = getUserInfo.ReturenEmp_name;//调用getUserInfo.ReturenShop_id属性并将返回值赋值给Info.shop_id 
                this.mainForm.OperPara.WriteIniInfo("emp_name", Info.emp_name);

                try
                {
                    GetShopName getShopName = new GetShopName();//创建GetShopName的对象getShopName
                    getShopName.ShopName(Info.shop_id);//调用函数获取分店名字
                    Info.shop_name = getShopName.ReturenShop_name;//将分店名字赋值给Info.shop_name
                    this.mainForm.OperPara.WriteIniInfo("shop_name", Info.shop_name);
                }
                catch
                {
                    Info.shop_name = "";
                    this.mainForm.OperPara.WriteIniInfo("shop_name", "");
                }
                #endregion

                #region 获取天气有关的信息
                try
                {
                    GetWeather_info getWeather_info = new GetWeather_info();
                    getWeather_info.Weather_info(Info.shop_id, Info.login_date.Date);
                    Info.w_date = getWeather_info.ReturenW_date;
                    Info.weather = getWeather_info.ReturenWeather;
                    Info.low_temper = Convert.ToInt32(getWeather_info.ReturenLow_temper);
                    Info.hight_temper = Convert.ToInt32(getWeather_info.ReturenHight_temper);
                }
                catch
                {
                    Info.w_date = "";
                    Info.weather = "";
                    Info.low_temper = 0;
                    Info.hight_temper = 0;
                }
                finally
                {
                    this.mainForm.ShowInfo1.ChangeText2(Info.weather, Info.low_temper, Info.hight_temper);
                }
                #endregion

                #region pos_id从配置文件中的读取及判定
                String pos_id = this.mainForm.OperPara.GetIniConfig("txtPosid");
                if (pos_id.Length == 1)
                {
                    Info.pos_id = "0" + pos_id;

                }
                else
                {
                    Info.pos_id = pos_id;
                }
                #endregion
                try
                {
                    Info.printmsg = this.mainForm.OperPara.GetIniConfig("printmsg");
                }
                catch { }
                this.Visible = false;
                Done.Enabled = true;
                exit.Enabled = true;
                txtPass.Text = "";
                txtEmp_Id.Text = "";

                try
                {

                    //未推出系统的情况下正常下线后以后的人再次登录
                    Online oneline = new Online();
                    oneline.ShowDialog();

                }
                catch { MessageBox.Show("上线界面加载错误！"); }
                try
                {
                    Info.deal_number = Convert.ToInt32(readIni.ReadString("RepastErp", "deal_number"));
                }
                catch { Info.deal_number = 1; this.mainForm.OperPara.WriteIniInfo("deal_number", "1"); }
                //形成销售单号
                string s = Info.deal_number.ToString();
                switch (s.Length)
                {

                    case 1:
                        s = "000" + s;
                        break;
                    case 2:
                        s = "00" + s;
                        break;
                    case 3:
                        s = "0" + s;
                        break;
                    case 4: break;
                    default:
                        s = "0001";
                        Info.deal_number = 1;
                        readIni.WriteString("RepastErp", "deal_number", "1");
                        break;

                }
                Info.sale_id = Info.shop_id + Info.pos_id + DateTime.Now.ToString("yyyyMMddHHmmss") + s;//生成初始的的销售单号并赋值给Info.sale_id    
                this.mainForm.OperPara.WriteIniInfo("sale_id", Info.sale_id);
                //在saletmp00中插入一条销售记录
                InsertSaleTmp00.InitInsertSaleTmp00().DataInsertSaleTmp00();
                //重新加载功能面板
                this.mainForm.LoadFuncPanel(this.mainForm.functionPanel, Info.emp_id, getUserInfo.ReturenEmp_level, true);
                this.mainForm.Visible = true;
                try
                {
                    Info.deal_number = Convert.ToInt32(readIni.ReadString("RepastErp", "deal_number"));//产生初始的的交易号
                }
                catch { Info.deal_number = 1; this.mainForm.OperPara.WriteIniInfo("deal_number", "1"); }

                //如果主窗体可见则重新给ShowInfo控件加载数据
                this.mainForm.ShowInfo1.ChangeText(Info.deal_number.ToString(), Info.emp_name, Info.pos_id, Info.emp_id.ToString(), Info.shift_num.ToString());
                this.mainForm.OperPara.WriteIniInfo("isNormalOff", "false");

            }
            else    //调用getUserInfo.IsLogin()方法返回false，则执行下列操作
            {
                MessageBox.Show("密码或帐号错误！");
                txtPass.Text = "";
                txtEmp_Id.Text = "";
                Done.Enabled = true;
                exit.Enabled = true;

            }
        }
        /// <summary>
        /// Backspace事件调用的验证事件
        /// </summary>
        public void Backspace_check()
        {
            if (isUp == false)//判断确定对密码框进行操作
            {
                // txtPass.Focus();//获取焦点

                if (txtPass.SelectedText.Length > 0)//判断文本框中是否有选中的内容
                {
                    Pass_selectedtext();
                }

                else
                {
                    if (txtPass.Text.Length > 1)//判断Pass的长度，大于1则进行去掉最后一位后显示在密码框中
                    {
                        txtPass.Text = txtPass.Text.Substring(0, txtPass.Text.Length - 1);//去掉Pass最后一位后显示在密码框中
                    }
                    else //清空密码框
                    {
                        txtPass.Text = "";
                    }
                }
            }
            else  //对帐号框进行操作
            {
                if (txtEmp_Id.SelectedText.Length > 0)//判断文本框中是否有选中的内容
                {
                    Emp_Id_selectedtext();
                }
                else
                {
                    //txtEmp_Id.Focus();//获取焦点

                    if (txtEmp_Id.Text.Length > 1)//判断Emp_Id的长度，大于1则进行去掉最后一位后显示在密码框中
                    {
                        txtEmp_Id.Text = txtEmp_Id.Text.Substring(0, txtEmp_Id.Text.Length - 1);//去掉Emp_Id最后一位后显示在帐号框中
                    }
                    else  //清空帐号框
                    {
                        txtEmp_Id.Text = "";
                    }
                }
            }
        }

        /// <summary>
        /// 帐号文本框中选择内容的事件
        /// </summary>
        public void Emp_Id_selectedtext()
        {
            //删除选中的内容
            txtEmp_Id.Text = txtEmp_Id.Text.Substring(0, txtEmp_Id.Text.IndexOf(txtEmp_Id.SelectedText)) + txtEmp_Id.Text.Substring(txtEmp_Id.Text.IndexOf(txtEmp_Id.SelectedText) + txtEmp_Id.SelectedText.Length);

            txtEmp_Id.SelectionStart = txtEmp_Id.TextLength; //设置光标位置到文本最后 
        }
        /// <summary>
        /// 密码文本框中选择内容的事件
        /// </summary>
        public void Pass_selectedtext()
        {
            //删除选中的内容
            txtPass.Text = txtPass.Text.Substring(0, txtPass.Text.IndexOf(txtPass.SelectedText)) + txtPass.Text.Substring(txtPass.Text.IndexOf(txtPass.SelectedText) + txtPass.SelectedText.Length);

            txtPass.SelectionStart = txtPass.TextLength; //设置光标位置到文本最后 
        }
        #endregion

        #region enter键盘按下事件
        /// <summary>
        /// 键盘按下事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MainForm_KeyDown(object sender, KeyEventArgs e)
        {
            Done.Enabled = false;
            exit.Enabled = false;
            IsLoginSucess();//调用IsLoginSucess()方法
            mainForm.Login.TxtEmp_Id.Focus();
            mainForm.Login.isUp = true;
        }
        #endregion
    }
}
