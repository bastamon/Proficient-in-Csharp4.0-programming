using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using POS.View;
using POS.Models;
using POS.Service;
/*备餐控件，从销售表中读取信息，尽起显示点餐信息 
主要方法：
（1）、LoadInfo(string sale_id)，备餐控件加载信息，sale_id：销售单号。
*/
namespace POS.Controls
{
    /// <summary>
    /// 备餐控件
    /// </summary>
    public partial class Beican : UserControl
    {
        /// <summary>
        /// 标示当前是备餐页面是否可见
        /// </summary>
        public bool IsNowBeiCan = false;
        /// <summary>
        /// 构造方法
        /// </summary>
        public Beican()
        {
            InitializeComponent();
        }
        

        #region//字段
        /// <summary>
        /// 主界面
        /// </summary>
        private MainForm mainForm;
        #endregion

        #region//属性
        /// <summary>
        /// 主界面
        /// </summary>
        public MainForm MainForm
        {
            get { return mainForm; }
            set { mainForm = value; }
        }
        #endregion

        #region//方法

        /// <summary>
        /// 加载信息
        /// </summary>
        /// <param name="sale_id">销售单号</param>
        public void LoadInfo(string sale_id)
        {
            this.IsNowBeiCan = true;
            GetSaleTemp01 getSaletemp01 = new GetSaleTemp01();
            getSaletemp01.GetBeiCan(sale_id);

            List<OrderDinner> ListOrderDinner = new List<OrderDinner>();
            OrderDinner orderDinner;
            int recordNumber = getSaletemp01.RecordNumber;
            string pos_dip = "";
            bool ex;
            for (int i = 0; i < recordNumber; i++)
            {
                orderDinner = new OrderDinner();
                orderDinner.ProdName = getSaletemp01.Pos_disp(i);
                pos_dip = orderDinner.ProdName;

                ex=false;//判断该元素是否已存在ListOrderDinner中true为存在false为不存在
                orderDinner.Number = 0;
                if (getSaletemp01.Comb_Type(i).Equals("1"))
                { }
                else
                {
                    foreach (OrderDinner a in ListOrderDinner)
                    {
                        if (a.ProdName.Equals(pos_dip))
                        {
                            ex = true;
                            break;
                        }
                    }
                    if (ex)
                    {
                    }
                    else
                    {
                        for (int j = i; j < getSaletemp01.RecordNumber; j++)
                        {
                            if (getSaletemp01.Pos_disp(j).Equals(pos_dip))
                            {
                                orderDinner.Number += (int)getSaletemp01.ProdNumber(j);
                            }
                        }
                        ListOrderDinner.Add(orderDinner);
                    }
                }
            }
            this.dgvBeican.DataSource = null;
            this.dgvBeican.DataSource = ListOrderDinner;

        }

        /// <summary>
        /// 设置dgvBeican的大小
        /// </summary>
        private void SetSize()
        {
            try
            {
                this.dgvBeican.Columns[0].HeaderText = "商品名称";
                this.dgvBeican.Columns[1].HeaderText = "数量";
                this.dgvBeican.Columns[0].Width = this.Width * 3 / 4;
                this.dgvBeican.Columns[1].Width = this.Width * 1 / 4;
                this.dgvBeican.Height = this.Height * 577 / 617;
                this.btnExit.Height = this.Height * 40 / 617;
            }
            catch { }

        }

        #endregion

        #region//事件

        /// <summary>
        /// 退出事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnExit_Click(object sender, EventArgs e)
        {
            this.mainForm.ReturnTlpMain.Controls.Remove(this);
            this.mainForm.ReturnTlpMain.Controls.Add(this.mainForm.ReturnTlpRight, 1, 0);
            this.mainForm.functionPanel.rightContrl = "交易";
            this.IsNowBeiCan = false;
            
        }



        /// <summary>
        /// 备餐控件大小改变事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Beican_SizeChanged(object sender, EventArgs e)
        {
            SetSize();
        }

        /// <summary>
        /// 备餐控件加载事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Beican_Load(object sender, EventArgs e)
        {
            SetSize();           
        }

        #endregion
    }


}
