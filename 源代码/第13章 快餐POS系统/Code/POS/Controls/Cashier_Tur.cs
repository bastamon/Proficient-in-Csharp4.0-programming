using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using POS.View;
using POS.Service;
using POS.Common;
/*
 * 用于显示员工机本周、本月营业额  
 * 备注：暂时未使用
 */
namespace POS.Controls
{
    /// <summary>
    /// 用于显示员工机本周、本月营业额 备注：暂时未使用
    /// </summary>
    public partial class Cashier_Tur : UserControl
    {
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
        /// 构造函数
        /// </summary>
        public Cashier_Tur()
        {
            InitializeComponent();
        }
        /// <summary>
        /// 取消事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Cancel_Click(object sender, EventArgs e)
        {
            this.mainForm.ReturnTlpMain.Controls.Remove(this);
            this.mainForm.ReturnTlpMain.Controls.Add(this.mainForm.ReturnTlpRight, 1, 0);
            this.mainForm.functionPanel.rightContrl = "交易";
            
        }
        #region//显示对应的记录
        /// <summary>
        /// 显示对应的记录
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Cashier_Tur_Load(object sender, EventArgs e)
        {
            ShopAndPosAndCashier cashier = new ShopAndPosAndCashier();
            for (int i = 0; i < 4; i++)
            {
                DataSet dataset = cashier.Cashier(i );
                try
                {
                    //if(i==0)
                    //cashierTurlabe.Text = "收银员编号为[" + dataset.Tables[0].Rows[0][0].ToString() + "]的营业额是：\n" +Convert .ToDouble ( dataset.Tables[0].Rows[0][1]).ToString ("#.0") + "元";
                    //else if(i==1)
                    //    cashierTurlabeMonth.Text = "此收银员本月营业额是：\n" + Convert.ToDouble(dataset.Tables[0].Rows[0][1]).ToString("#.0") + "元";
                     if(i==2)
                        cashierTurlabeWeek.Text = "此收银员本周营业额是：\n" + Convert.ToDouble(dataset.Tables[0].Rows[0][1]).ToString("#.0") + "元";
                    else
                        cashierTurlabeDay.Text = "此收银员本日营业额是：\n" + Convert.ToDouble(dataset.Tables[0].Rows[0][1]).ToString("#.0") + "元";
                }
                catch (Exception)
                {
                    //cashierTurlabe.Text = "收银员编号为[" + Info.emp_id + "]的营业额是：\n0元";
                    //cashierTurlabeMonth.Text = "此收银员本月营业额是：\n0元";
                    //cashierTurlabeWeek.Text = "此收银员本周营业额是：\n0元";
                    //cashierTurlabeDay.Text = "此收银员本日营业额是：\n0元";
                }
            }
        }
        #endregion

    }
}
