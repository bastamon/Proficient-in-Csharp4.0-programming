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
using POS.Common;
/*用于分店收入信息查询功能 备注：暂时未使用
说明： 单机查询控件，查询本pos机的营业情况
主要事件：
（1）、Pos_Tur_Load，单机查询控件加载事件
 */
namespace POS.Controls
{
    /// <summary>
    /// 分店收入信息 备注：暂时未使用
    /// </summary>
    public partial class Shop_Tur : UserControl
    {
        private MainForm mainForm;
        /// <summary>
        /// 构造函数
        /// </summary>
        public Shop_Tur()
        {
            InitializeComponent();
        }
        /// <summary>
        /// 主窗体
        /// </summary>
        public MainForm MainForm
        {
            get { return mainForm; }
            set { mainForm = value; }
        }
        #region//显示所查询的信息
        /// <summary>
        /// 显示对应的信息
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void shop_Tur_Load(object sender, EventArgs e)
        {
            ShopAndPosAndCashier shopTur = new ShopAndPosAndCashier();
            for (int i = 0; i < 4; i++)
            {
                DataSet dataset = shopTur.ShopTur(i);
                try
                {
                    //if (i == 0)
                    //    ShopTurlabe.Text = "店号为[" + dataset.Tables[0].Rows[0][0].ToString() + "]的总营业额是：\n" +Convert .ToDouble ( dataset.Tables[0].Rows[0][1]).ToString("#.00") + "元";
                    if (i == 1)
                        ShopTurlabeMonth.Text = "本店的本月营业额是：\n" + Convert.ToDouble(dataset.Tables[0].Rows[0][1]).ToString("#.0") + "元";
                    else if (i == 2)
                    {
                        //ShopTurlabeWeek.Text = "本店的本周营业额是：\n" + Convert.ToDouble(dataset.Tables[0].Rows[0][1]).ToString("#.0") + "元";
                    }
                    else
                    {
                        ShopTurlabeDay.Text = "本店的本日营业额是：\n" + Convert.ToDouble(dataset.Tables[0].Rows[0][1]).ToString("#.0") + "元";
                    }

                }
                catch
                {
                    //ShopTurlabe.Text = "店号为[" + Info.shop_id + "]的总营业额是：\n0元";
                    //ShopTurlabeMonth.Text = "本店的本月营业额是：\n0元";
                    //ShopTurlabeWeek.Text = "本店的本周营业额是：\n0元";
                    //ShopTurlabeDay.Text = "本店的本日营业额是：\n0元";
                }
            }

        }
        #endregion
        /// <summary>
        /// 取消
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button2_Click(object sender, EventArgs e)
        {
            this.mainForm.ReturnTlpMain.Controls.Remove(this);
            this.mainForm.ReturnTlpMain.Controls.Add(this.mainForm.ReturnTlpRight, 1, 0);
            this.mainForm.functionPanel.rightContrl = "交易";
        }
    }
}
