using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
//用于提示收银员结账找零状态显示相关金额
/*主要方法：SetStatus(string txtrequire, string txtpay, string txtgiveback)设置窗体相应信息，及结账信息
参数说明：txtrequire 应付金额、txtpay已付金额、txtgiveback找零
*/
namespace POS.View
{
    /// <summary>
    /// 用于提示收银员结账状态
    /// </summary>
    public partial class CheckForm : Form
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public CheckForm()
        {
            InitializeComponent();
            timer1.Enabled = true;
        }
        /// <summary>
        /// 确定退出按钮
        /// </summary>
        /// <param name="sender">this</param>
        /// <param name="e">e</param>
        private void btnOk_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }
        /// <summary>
        /// 设置窗体上相应信息，即结账状态信息
        /// </summary>
        /// <param name="txtrequire">应付金额</param>
        /// <param name="txtpay">已付金额</param>
        /// <param name="txtgiveback">找零</param>
        public void SetStatus(string txtrequire, string txtpay, string txtgiveback)
        {
            this.txtRequire.Text = txtrequire;
            this.txtPay.Text = txtpay;
            this.txtGiveBack.Text = txtgiveback;
        }
        /// <summary>
        /// timer事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void timer1_Tick(object sender, EventArgs e)
        {
            if (this.lbBack.ForeColor == Color.Black)
            {
                this.lbBack.ForeColor = Color.Yellow;
                this.txtGiveBack.ForeColor = Color.Yellow;
            }
            else 
            {
                this.lbBack.ForeColor = Color.Black;
                this.txtGiveBack.ForeColor = Color.Black;
            }
        }
    }
}
