using System;
using System.Windows.Forms;
using POS.Service;
using POS.Controls;
using System.Drawing;
//用于数字输入窗体，适用于交易查询
/*主要事件：
（1）、KeyBoard_Form_Load(object sender, EventArgs e)初始化窗体
*/
namespace POS.View
{
    public partial class KeyBoard_Form : Form
    {
        /// <summary>
        /// 用于控制输入总金额获得焦点false ,销售单获得焦点true
        /// </summary>
        public static bool flag = true;
        /// <summary>
        /// 构造函数
        /// </summary>
        public KeyBoard_Form()
        {
            InitializeComponent();
            CheckSale.key_form = this;
            CheckSale0.key_form = this;
        }

        #region//数字键
        private void btn1_Click(object sender, EventArgs e)
        {
            if (flag)
                SeeAboutSale.str += "1";
            else
                SeeAboutSale.numstr += "1";
            ////btn1.Focus = false;
            //btn1.Capture = false;
            button3.Focus();
        }

        private void btn2_Click(object sender, EventArgs e)
        {
            if (flag)
                SeeAboutSale.str += "2";
            else
                SeeAboutSale.numstr += "2";
            button3.Focus();
        }

        private void btn3_Click(object sender, EventArgs e)
        {
            if (flag)
                SeeAboutSale.str += "3";
            else
                SeeAboutSale.numstr += "3";
            button3.Focus();
        }

        private void btn4_Click(object sender, EventArgs e)
        {
            if (flag)
                SeeAboutSale.str += "4";
            else
                SeeAboutSale.numstr += "4";
            button3.Focus();
        }

        private void btn5_Click(object sender, EventArgs e)
        {
            if (flag)
                SeeAboutSale.str += "5";
            else
                SeeAboutSale.numstr += "5";
            button3.Focus();
        }

        private void btn6_Click(object sender, EventArgs e)
        {
            if (flag)
                SeeAboutSale.str += "6";
            else
                SeeAboutSale.numstr += "6";
            button3.Focus();
        }

        private void btn7_Click(object sender, EventArgs e)
        {
            if (flag)
                SeeAboutSale.str += "7";
            else
                SeeAboutSale.numstr += "7";
            button3.Focus();
        }

        private void btn8_Click(object sender, EventArgs e)
        {
            if (flag)
                SeeAboutSale.str += "8";
            else
                SeeAboutSale.numstr += "8";
            button3.Focus();
        }

        private void btn9_Click(object sender, EventArgs e)
        {
            if (flag)
                SeeAboutSale.str += "9";
            else
                SeeAboutSale.numstr += "9";
            button3.Focus();
        }

        private void btn0_Click(object sender, EventArgs e)
        {
            if (flag)
                SeeAboutSale.str += "0";
            else
                SeeAboutSale.numstr += "0";
            button3.Focus();
        }

        private void Spot_btn_Click(object sender, EventArgs e)
        {
            if (flag)
                SeeAboutSale.str += ".";
            else
                SeeAboutSale.numstr += ".";
            button3.Focus();
        }
        #endregion
        
        #region//功能键
        /// <summary>
        /// 清空按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (flag)
                SeeAboutSale.str = "";
            else
                SeeAboutSale.numstr = "";
            button3.Focus();
        }

        /// <summary>
        /// 关闭按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        /// <summary>
        /// 退格按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button3_Click(object sender, EventArgs e)
        {
            string str;
            if (flag)
            {
                try
                {
                    str = SeeAboutSale.str;
                    SeeAboutSale.str = str.Substring(0, str.Length - 1);
                }
                catch
                {
                    SeeAboutSale.str = "";
                }
            }
            else
            {
                try
                {
                    str = SeeAboutSale.numstr;
                    SeeAboutSale.numstr = str.Substring(0, str.Length - 1);
                }
                catch
                {
                    SeeAboutSale.numstr = "";
                }
            }

        }
        #endregion
      
        /// <summary>
        /// 窗体的初始位置
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void KeyBoard_Form_Load(object sender, EventArgs e)
        {
            this.Location =new Point( CheckSale0.keyboard_point.X ,CheckSale0 .keyboard_point .Y +this.Size .Height /3);
        }
        /// <summary>
        /// 按键事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void KeyBoard_Form_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (flag)
            {
                if (e.KeyChar == (char)Keys.Back)
                {
                    try
                    {
                        SeeAboutSale.str = SeeAboutSale.str.Substring(0, SeeAboutSale.str.Length - 1);
                    }
                    catch { }
                }
            }
            else
            {
                if (e.KeyChar == (char)Keys.Back)
                {
                    try
                    {
                        SeeAboutSale.numstr = SeeAboutSale.numstr.Substring(0, SeeAboutSale.numstr.Length - 1);
                    }
                    catch { }
                }
            }
        }
        /// <summary>
        /// 键盘处理事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button3_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (flag)
            {
                if (e.KeyChar == (char)Keys.Back)
                {
                    try
                    {
                        SeeAboutSale.str = SeeAboutSale.str.Substring(0, SeeAboutSale.str.Length - 1);
                    }
                    catch { }
                }
            }
            else
            {
                if (e.KeyChar == (char)Keys.Back)
                {
                    try
                    {
                        SeeAboutSale.numstr = SeeAboutSale.numstr.Substring(0, SeeAboutSale.numstr.Length - 1);
                    }
                    catch { }
                }
            }
        }


    }
}
