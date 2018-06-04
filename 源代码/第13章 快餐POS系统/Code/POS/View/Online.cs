using System;
using System.Data;
using System.Windows.Forms;
using POS.Common;
using POS.Service;
using POS.Data;
//上线零用钱的输入窗体
/*主要事件： 
（1）、btndone_Click(object sender, EventArgs e)用于确定上线信息
*/
namespace POS.View
{
    /// <summary>
    /// 上线窗体类
    /// </summary>
    public partial class Online :Form
    {

        /// <summary>
        /// 声明ReadIni对象dataset
        /// </summary>
        ReadIni readIni;

        /// <summary>
        /// 构造函数
        /// </summary>
        public Online()
        {
            InitializeComponent();
            this.StartPosition = FormStartPosition.CenterScreen;//窗体居中
            cashier_sum.Focus ();//获取焦点
            shop_name.Text = Info.shop_name;
            pos_id.Text = Info.pos_id;
            emp_id.Text = Info.emp_id;
            emp_name.Text = Info.emp_name ;
            online_date.Text = DateTime.Now.ToString("yyyy-MM-dd");
            online_time.Text = DateTime.Now.ToString("HH:mm:ss");
            online_shift.Text = Info.shift_num;
        }

        #region 数字键盘的点击事件
        private void btnNum0_Click_1(object sender, EventArgs e)
        {
            cashier_sum.Text = cashier_sum.Text + 0;
        }
        private void btnNum1_Click_1(object sender, EventArgs e)
        {
            cashier_sum.Text = cashier_sum.Text+ 1;
        }
        private void btnNum2_Click(object sender, EventArgs e)
        {
            cashier_sum.Text = cashier_sum.Text + 2;
        }
        private void btnNum3_Click_1(object sender, EventArgs e)
        {
            cashier_sum.Text = cashier_sum.Text + 3;
        }

        private void btnNum4_Click_1(object sender, EventArgs e)
        {
            cashier_sum.Text = cashier_sum.Text + 4;
        }

        private void btnNum5_Click_1(object sender, EventArgs e)
        {
            cashier_sum.Text = cashier_sum.Text +5;
        }

        private void btnNum6_Click_1(object sender, EventArgs e)
        {
            cashier_sum.Text = cashier_sum.Text + 6;
        }

        private void btnNum7_Click_1(object sender, EventArgs e)
        {
            cashier_sum.Text = cashier_sum.Text + 7;
        }

        private void btnNum8_Click_1(object sender, EventArgs e)
        {

            cashier_sum.Text = cashier_sum.Text + 8;
        }

        private void btnNum9_Click_1(object sender, EventArgs e)
        {
            cashier_sum.Text = cashier_sum.Text + 9;
        }
        private void btndot_Click(object sender, EventArgs e)
        {
            cashier_sum.Text = cashier_sum.Text + ".";
        }
        #endregion

        #region 数字键盘上功能按钮的点击事件

        /// <summary>
        /// 删除按钮点击事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btndelete_Click(object sender, EventArgs e)
        {
            if (cashier_sum.SelectedText.Length > 0)//判断文本框中是否有选中的内容
            {
                cashier_sum_selectedtext();
            }
            else
            {
                cashier_sum.Focus();
                cashier_sum.Text = "";
            }     
       }

        /// <summary>
        /// 确定按钮点击事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btndone_Click(object sender, EventArgs e)
        {
               try
               {
                   Info.cashier_sum = Convert.ToDecimal(cashier_sum.Text);

               }
               catch
               {
                   Info.cashier_sum = 0;
               }
               Info.remain_sum =Info.cashier_sum;
               readIni = new ReadIni("Info.ini");
               readIni.WriteString("RepastErp", "cashier_sum", Convert.ToString(Info.cashier_sum));
               readIni.WriteString("RepastErp", "remain_sum", Convert.ToString(Info.remain_sum));
               //上线时往pos_rounds表中插入相关数据
               InsertPos_rounds insertPos_rounds = InsertPos_rounds.InitController();
               
               insertPos_rounds.InsertPosrounds(Info.shop_id, Info.pos_id, Info.login_date, Info.cashier_sum, Info.emp_id, "0", Info.shift_num, DateTime.Now);
               this.Visible = false; 
        }

        /// <summary>
        /// 取消按钮点击事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btncancel_Click(object sender, EventArgs e)
        {
            Info.cashier_sum = 0;
            //将与交班有关信息作为参数传递，记录在Pos_rounds表的映射类中
            InsertPos_rounds.InitController().InsertPosrounds(Info.shop_id, Info.pos_id, Info.login_date, Info.cashier_sum, Info.emp_id, "0", Info.shift_num, DateTime.Now);
            this.Visible = false;
        }

        #endregion

        #region 数字键盘上功能按钮调用的事件

        /// <summary>
        /// 删除按钮点击事件中调用的文本框中选择内容删除的事件
        /// </summary>
        public void cashier_sum_selectedtext()
        {
            //删除选中的内容
            cashier_sum.Text = cashier_sum.Text.Substring(0, cashier_sum.Text.IndexOf(cashier_sum.SelectedText)) + cashier_sum.Text.Substring(cashier_sum.Text.IndexOf(cashier_sum.SelectedText) + cashier_sum.SelectedText.Length);
            cashier_sum.SelectionStart = cashier_sum.TextLength; //设置光标位置到文本最后 
        }
        #endregion

        private void cashier_sum_TextChanged(object sender, EventArgs e)
        {
            try
            {
                Decimal cashiersum = Convert.ToDecimal(cashier_sum.Text);
                 
            }
            catch
            {
                cashier_sum.Text = "";
            }
           
        }
    }
}
