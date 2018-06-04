using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using POS.Service;
using POS.View;
/*用于查询当前员工当前班次的营业额
 类说明： 单机查询控件，查询本pos机的营业情况
主要事件：
（1）、Pos_Tur_Load，单机查询控件加载事件

 */
namespace POS.Controls
{
    /// <summary>
    /// 用于查询当前班次的营业额
    /// </summary>
    public partial class Pos_Tur : UserControl
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
        public Pos_Tur()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 取消
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Cancel_Click(object sender, EventArgs e)
        {
            this.mainForm.ReturnTlpMain.Controls.Remove(this);
            this.mainForm.ReturnTlpMain.Controls.Add(this.mainForm.ReturnTlpRight, 1, 0);
            this.mainForm.functionPanel.rightContrl = "交易";
        }
        /// <summary>
        /// 加载
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Pos_Tur_Load(object sender, EventArgs e)
        {
            
            ShopAndPosAndCashier postur = new ShopAndPosAndCashier();
           
            
                //DataSet dataset = postur.PosTur(3);
                //try
                //{
                //    //if (i == 0)
                //    //    PosTurlabe.Text = "Pos机号为[" + dataset.Tables[0].Rows[0][0].ToString() + "]的营业额是：\n" +Convert .ToDouble ( dataset.Tables[0].Rows[0][1]).ToString("#.0") + "元";
                //    //else if (i == 1)
                //    //    PosTurlabeMonth.Text = "本Pos机本月营业额是：\n" + Convert.ToDouble(dataset.Tables[0].Rows[0][1]).ToString("#.0") + "元";

                //    PosTurlabeDay.Text = "当天营业额是：\n" + Convert.ToDouble(dataset.Tables[0].Rows[0][1]).ToString("#.0") + "元";
                    
                //}
                //catch (Exception)
                //{
                //    //PosTurlabeMonth.Text = "Pos机号为[" + dataset.Tables[0].Rows[0][0].ToString() + "]的营业额是：\n 0元";
                //    //PosTurlabeMonth.Text = "本Pos机本月营业额是：\n0元";
                //    //PosTurlabeWeek.Text = "本Pos机本周营业额是：\n0元";
                //    //PosTurlabeDay.Text = "本Pos机本日营业额是：\n0元";
                //}
            try
            {
                PosTurlabeDay.Text = "当前班次营业额是：\n" + postur.GetSaleMoney().ToString("#.0") + "元";
            }
            catch { }
            
        }

    }
}
