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
using Common.Print;
using POS.Common;
/*用于查询销售单的详细信息和账单重印等
 类说明： 交易查询
主要方法：
（1）、search查找功能
（2）、showdataGridView1() 显示第一个datagridview 信息
（3）、showMessageDia（）显示第二个和第三个datagrivew信息
（4）、showDialog（） 显示右上角详细信息
（5）、printAgain（） 重印账单

主要事件说明：
（1）、btnSearch_Click(object sender, EventArgs e) 查找事件
 
 */
namespace POS.Controls
{
    public partial class CheckSale0 : UserControl
    {
        #region 构造函数
        /// <summary>
        /// 构造函数
        /// </summary>
        public CheckSale0()
        {
            InitializeComponent();
            startdateTime.Value = DateTime.Now;
            enddateTime.Value = DateTime.Now;

            Downbutton2.Enabled = false;
            UPbutton3.Enabled = false;
        }
        #endregion

        #region 字段
        bool flag = true;
        DataSet dataSet;
        string sale_date0, sale_date1, sale_id;
        SeeAboutSale seeAboutSale = new SeeAboutSale();
        private static Form checkSale;
        /// <summary>
        /// .......
        /// </summary>
        public static Form key_form;
        /// <summary>
        /// 查询账单窗体对象
        /// </summary>
        public static Form CheckSale
        {
            get { return checkSale; }
            set { checkSale = value; }
        }
        private MainForm mainForm;
        /// <summary>
        /// 坐标点
        /// </summary>
        public static Point keyboard_point;
        /// <summary>
        /// 主界面
        /// </summary>
        public MainForm MainForm
        {
            get { return mainForm; }
            set { mainForm = value; }
        }
        /// <summary>
        /// 获得dataGridView1
        /// </summary>
        public DataGridView Datagridview
        {
            get { return dataGridView1; }
        }
        #endregion

        #region 事件
        /// <summary>
        /// 查找事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSearch_Click(object sender, EventArgs e)
        {
            search();
        }
        /// <summary>
        /// 向下
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Downbutton2_Click(object sender, EventArgs e)
        {
            try
            {
                this.dataGridView1.CurrentCell = this.dataGridView1[this.dataGridView1.CurrentCell.ColumnIndex, this.dataGridView1.CurrentCell.RowIndex - 1];
                if (printfagain.Enabled)
                {
                    showDialog();
                    showMessageDia();
                }
            }
            catch { }
        }
        /// <summary>
        /// 向上
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void UPbutton3_Click(object sender, EventArgs e)
        {
            try
            {
                this.dataGridView1.CurrentCell = this.dataGridView1[this.dataGridView1.CurrentCell.ColumnIndex, this.dataGridView1.CurrentCell.RowIndex + 1];
                if (printfagain.Enabled)
                {
                    showDialog();
                    showMessageDia();
                }
            }
            catch { }
        }
        /// <summary>
        /// 账单冲印事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void printfagain_Click(object sender, EventArgs e)
        {
            if (dataGridView1.Rows.Count == 0)
                return;

            printfagain.Enabled = false;
            printAgain();
        }
        /// <summary>
        /// datagridview1的单元格点击事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            printfagain.Enabled = true;
            showMessageDia();
            showDialog();
        }
        /// <summary>
        /// 查找事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CheckSale0_Load(object sender, EventArgs e)
        {
            this.btnSearch_Click(btnSearch, new EventArgs());
        }
        /// <summary>
        /// 销售单点击事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtSaleId_MouseClick(object sender, MouseEventArgs e)
        {
            key_form.Close();
            keyboard_point = new Point(this.txtSaleId.Location.X, this.txtSaleId.Location.Y);
            flag = true;
            timer1.Enabled = true;
            KeyBoard_Form.flag = true;
            new KeyBoard_Form().Show();
        }
        /// <summary>
        /// 总金额获得焦点事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtTotalMoney_MouseClick(object sender, MouseEventArgs e)
        {
            key_form.Close();
            keyboard_point = new Point(this.txtTotalMoney.Location.X, this.txtTotalMoney.Location.Y);
            timer1.Enabled = true;
            this.flag = false;
            KeyBoard_Form.flag = false;
            new KeyBoard_Form().Show();
        }
        /// <summary>
        /// timer事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void timer1_Tick(object sender, EventArgs e)
        {
            if (flag)
                this.txtSaleId.Text = SeeAboutSale.str;
            else
                this.txtTotalMoney.Text = SeeAboutSale.numstr;
        }
        /// <summary>
        /// 取消事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Cancel_btn_Click(object sender, EventArgs e)
        {
            checkSale.Close();
        }
        /// <summary>
        /// 选择销售单事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtSaleId_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Back)
            {
                SeeAboutSale.str = SeeAboutSale.str.Substring(0, SeeAboutSale.str.Length - 1);
            }
        }

        #endregion

        #region 方法
        /// <summary>
        /// 查找功能
        /// </summary>
        public void search()
        {
            printfagain.Enabled = true;
            if (showdataGridView1() && this.dataGridView1.Rows.Count != 0)
            {

                showMessageDia();
                showDialog();

            }
            else
            {
                this.dataGridView2.DataSource = null;
                this.dataGridView3.DataSource = null;
                this.sale_id_lab.Text = "";
                this.saletime_lab.Text = "";
                this.status_lab.Text = "";
                this.cashier_lab.Text = "";
                this.pos_id_lab.Text = "";
                this.money_lab.Text = "";
                this.Rebate_lab.Text = "";
            }
            this.dataGridView1_CellClick(dataGridView1, new DataGridViewCellEventArgs(1, 0));
        }
        #region 显示第一个datagridview的信息,并设置异常处理
        /// <summary>
        /// 屏蔽datagridview1中不必要列的显示
        /// </summary>
        private void AllowViewDataGridView1()
        {
            try
            {
                for (int i = 4; i < dataGridView1.ColumnCount; i++)
                {
                    dataGridView1.Columns[i].Visible = false;
                }
            }
            catch { }
        }
       /// <summary>
        /// 显示第一个datagridview的信息
       /// </summary>
       /// <returns>bool</returns>
        public bool showdataGridView1()
        {
            try
            {
                try
                {
                    if (this.txtTotalMoney.Text != "")
                        Convert.ToDouble(this.txtTotalMoney.Text);
                }
                catch
                {
                    MessageBox.Show("输入的金额不正确");
                    return false;
                }
                dataGridView1.AllowUserToAddRows = false;
                dataGridView2.AllowUserToAddRows = false;
                dataGridView3.AllowUserToAddRows = false;
                dataGridView1.ScrollBars = ScrollBars.Vertical;
                dataGridView3.ScrollBars = ScrollBars.Vertical;
                sale_date0 = this.startdateTime.Value.AddDays(-1).ToString("yyyy-MM-dd HH:mm:ss");
                sale_date1 = this.enddateTime.Value.ToString("yyyy-MM-dd HH:mm:ss");

                dataSet = seeAboutSale.searchBusiness(this.txtSaleId.Text, this.txtTotalMoney.Text, sale_date0, sale_date1);
                dataGridView1.DataSource = dataSet.Tables[0];
                AllowViewDataGridView1();
                totalnum.Text = "共" + dataSet.Tables[0].Rows.Count + "笔交易";
                dataGridView1.Columns[0].Width = 4 * this.dataGridView1.Size.Width / 9;
                dataGridView1.Columns[1].Width = this.dataGridView1.Size.Width / 6;
                dataGridView1.Columns[2].Width = this.dataGridView1.Size.Width / 6;
                dataGridView1.Columns[3].Width = 2 * this.dataGridView1.Size.Width / 9;


                try
                {
                    for (int i = 0; i < dataGridView1.Columns.Count; i++)
                    {
                        this.dataGridView1.Columns[i].SortMode = DataGridViewColumnSortMode.NotSortable;
                    }
                }
                catch { }
                return true;
            }
            catch
            {
                return false;
            }
        }
        #endregion

        #region 获得对应右上角的信息
        /// <summary>
        /// 获得右上角信息
        /// </summary>
        public void showDialog()
        {
            try
            {
                sale_id_lab.Text = getString("SALE_ID");//销售单号
                saletime_lab.Text = getString("SALE_DATE");//销售时间
                try
                {
                    string str = getString("MEAL_KIND");//状态
                    if (str == "1")
                        status_lab.Text = "员工餐";
                    else if (str == "2")
                        status_lab.Text = "招待";
                    else
                        status_lab.Text = "核准";
                    if (getString("status_id") == "3")
                        status_lab.Text = "作废";
                }
                catch
                {
                    status_lab.Text = "核准";
                }
                cashier_lab.Text = getString("SALE_USER");//收银员编号
                pos_id_lab.Text = getString("pos_id");//Pos机号
                money_lab.Text = Convert.ToDouble(getString("TOT_SALES")).ToString("0.00");//金额
                double rebate = 0;
                for (int i = 0; i < dataGridView2.Rows.Count; i++)
                {
                    rebate += Convert.ToDouble(dataGridView2.Rows[i].Cells["折扣"].Value.ToString());
                }
                Rebate_lab.Text = rebate.ToString("0.00");
            }
            catch (Exception)
            {
                sale_id_lab.Text = saletime_lab.Text = status_lab.Text = cashier_lab.Text = pos_id_lab.Text = money_lab.Text = Rebate_lab.Text = "";
                printfagain.Enabled = false;
            }

        }
        /// <summary>
        /// 获得对应的值
        /// </summary>
        /// <param name="str">列名称</param>
        /// <returns>对应单元格值</returns>
        public string getString(string str)
        {
            return this.dataGridView1.Rows[this.dataGridView1.CurrentRow.Index].Cells[str].Value.ToString();
        }
        #endregion

        #region 显示第二个，第三个datagridview的信息
        /// <summary>
        /// 屏蔽DataGridView2中不必要的列的显示
        /// </summary>
        private void AllowViewDataGridView2()
        {
            try
            {
                for (int i = 7; i < dataGridView2.Columns.Count; i++)
                { dataGridView2.Columns[i].Visible = false; }
            }
            catch { }
        }
        /// <summary>
        /// 显示第二个,第三个datagridview的相应信息
        /// </summary>
        public void showMessageDia()
        {
            AddNum addnum = new AddNum();
            if (dataGridView1.Rows.Count == 0)
            {
                printfagain.Enabled = false;
                Downbutton2.Enabled = false;
                UPbutton3.Enabled = false;
                label14.Text = "共有0条记录";
                dataGridView2.DataSource = null;
                dataGridView3.DataSource = null;
            }
            else
            {
                for (int i = 0; i < dataGridView1.Rows.Count; i++)
                {
                    if (this.dataGridView1.Rows[i].Cells["status_id"].Value.ToString() == "3")
                        this.dataGridView1.Rows[i].DefaultCellStyle.BackColor = Color.Red;
                    else if (this.dataGridView1.Rows[i].Cells["MEAL_KIND"].Value.ToString() == "1")
                        this.dataGridView1.Rows[i].DefaultCellStyle.BackColor = Color.Blue;
                    else if (this.dataGridView1.Rows[i].Cells["MEAL_KIND"].Value.ToString() == "2")
                        this.dataGridView1.Rows[i].DefaultCellStyle.BackColor = Color.Yellow;
                }
                Downbutton2.Enabled = true;
                UPbutton3.Enabled = true;
                sale_id = dataGridView1.CurrentRow.Cells["销售单编号"].Value.ToString();
                dataGridView2.DataSource = addnum.AddSeriNum(seeAboutSale.payDetail(sale_id));
                //隐藏不别要行
                AllowViewDataGridView2();
                for (int i = 0; i < dataGridView2.Rows.Count; i++)
                {
                    if (dataGridView2.Rows[i].Cells["状态"].Value.ToString() == "3")
                        dataGridView2.Rows[i].DefaultCellStyle.BackColor = Color.Red;
                }
                dataGridView2.Columns[0].Width = (int)dataGridView2.Size.Width * 2 / 21;
                dataGridView2.Columns[1].Width = (int)dataGridView2.Size.Width * 6 / 21;
                dataGridView2.Columns[2].Width = (int)dataGridView2.Size.Width * 3 / 21;
                dataGridView2.Columns[3].Width = (int)dataGridView2.Size.Width * 3 / 21;
                dataGridView2.Columns[4].Width = (int)dataGridView2.Size.Width * 1 / 21;
                dataGridView2.Columns[5].Width = (int)dataGridView2.Size.Width * 3 / 21;
                dataGridView2.Columns[6].Width = (int)dataGridView2.Size.Width * 3 / 21;

                label14.Text = "共有" + dataGridView2.Rows.Count.ToString() + "条记录";
                dataGridView3.DataSource = seeAboutSale.payWays(sale_id);
                dataGridView3.Columns[0].Width = (int)dataGridView3.Size.Width / 2;
                dataGridView3.Columns[1].Width = (int)dataGridView3.Size.Width / 2;
            }
        }
        #endregion
        /// <summary>
        /// 账单重印 打印账单
        /// </summary>
        public void printAgain()
        {
            showMessageDia();
            double cash = 0;
            double cashnum = 0;
            double changenum = 0;
            decimal goodsnum = 0;


            int counttotle = dataGridView2.Rows.Count;
            List<string> list = new List<string>();
            //list.Add(mainForm.OperPara.TicketHead);//外界设置
            mainForm.AddExtraInfo(list, mainForm.OperPara.TicketHead);
            list.Add("销售时间: " + dataGridView1.CurrentRow.Cells["SALE_DATE"].Value.ToString());
            list.Add("款台:" + dataGridView1.CurrentRow.Cells["POS号"].Value.ToString() + "   收银员:" + dataGridView1.CurrentRow.Cells["收银员"].Value.ToString());
            list.Add("编号:" + dataGridView1.CurrentRow.Cells["销售单编号"].Value.ToString());
            if (dataGridView1.CurrentRow.Cells["MEAL_KIND"].Value.ToString() == "1")
                list.Add("结账类型：员工餐");
            else if (dataGridView1.CurrentRow.Cells["MEAL_KIND"].Value.ToString() == "2")
                list.Add("结账类型：招待");
            list.Add("品名        数量        小计");
            list.Add("---------------------------");
            for (int i = 0; i < counttotle; i++)
            {
                try
                {
                    //组合餐的子产品不打印
                    if (dataGridView2.Rows[i].Cells["组合类型"].Value.ToString() != "2")
                    {
                        decimal qty = Convert.ToDecimal(dataGridView2.Rows[i].Cells["数量"].Value);
                        goodsnum += qty;
                        string price = (decimal.Round(Convert.ToDecimal(dataGridView2.Rows[i].Cells["单价"].Value) * qty +Convert.ToDecimal(dataGridView2.Rows[i].Cells["折扣"].Value), 2)).ToString();
                        string prod_name = dataGridView2.Rows[i].Cells["商品名称"].Value.ToString();
                        string prod_id = dataGridView2.Rows[i].Cells["商品ID"].Value.ToString();
                        list.Add(prod_name.PadRight(10,' ')+ qty.ToString() + "      " + price.ToString());  
                    }
                }
                catch { }
            }
            list.Add("      ");
            list.Add("数量合计:" + "      " + goodsnum.ToString());
            decimal saletotal = decimal.Round(Convert.ToDecimal(dataGridView1.CurrentRow.Cells["总销售额"].Value.ToString()), 2);
            list.Add("合计:" + "      " + saletotal);
            for (int j = 0; j < dataGridView3.Rows.Count; j++)
            {
                cash = Convert.ToDouble(dataGridView3.Rows[j].Cells["金额"].Value);
                if (cash > 0)
                {
                    cashnum += cash;
                }
                else
                    changenum += cash;
            }
            if (changenum < 0)
                changenum = -changenum;
            list.Add("现金" + "       " + cashnum.ToString("0.00"));
            list.Add("找回" + "       " + changenum.ToString("0.00"));
            list.Add("--------账单重印--------");
            //list.Add(mainForm.OperPara.TicketTail);
            mainForm.AddExtraInfo(list, mainForm.OperPara.TicketTail);

            LPTControl.Port = Info.printPort;
            //LPTControl.NewRow(2);
            foreach (string s in list)
            {
                if (!LPTControl.WriteLine(s))
                {
                    MessageBox.Show("没有安装打印机", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
            }
            LPTControl.NewRow(1);

            LPTControl.CutPaper();

            LPTControl.Close();


        }
        #endregion

    }
}
