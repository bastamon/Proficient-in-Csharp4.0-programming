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
using POS.Data;
/*用于交班信息的查询功能
 类说明： 交班查询控件
主要方法：
（1）、showinquire()，查询交班信息

 */
namespace POS.Controls
{
    /// <summary>
    /// 交班查询控件
    /// </summary>
    public partial class Pos_rounds_inquire : UserControl
    {


        DataSet dataSet;
        string  date1, date2;
        Pos_rounds_check pos_rounds_check = new Pos_rounds_check();


        /// <summary>
        /// 主界面
        /// </summary>
        private MainForm mainForm;
        /// <summary>
        /// 主界面
        /// </summary>
        public MainForm MainForm
        {
            get { return mainForm; }
            set { mainForm = value; }
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        public Pos_rounds_inquire()
        {
            InitializeComponent();
            dataSet = pos_rounds_check.pos_roundsInquire("", "");
            dgvinquire.DataSource = dataSet.Tables[0];
            dgvinquire.Columns[0].HeaderText = "班次";
            dgvinquire.Columns[1].HeaderText = "上线时间";
            dgvinquire.Columns[2].HeaderText = "下线时间";
            dgvinquire.Columns[3].HeaderText = "员工 ";
            dgvinquire.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            //SetSize();


            begin_date.Value = DateTime.Now;//DateTime.Now.AddDays (-1);
            over_date.Value = DateTime.Now;
            
        }

        /// <summary>
        /// 查询交班信息
        /// </summary>
        public void showinquire()
        {
            date1 = begin_date.Value.ToString("yyyy-MM-dd ");
            date2 = over_date.Value.AddDays (+1).ToString("yyyy-MM-dd");
            dataSet = pos_rounds_check.pos_roundsInquire(date1, date2);
            dgvinquire.DataSource = dataSet.Tables[0];
            
        }

        /// <summary>
        /// 查询按钮点击事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void inquery_Click(object sender, EventArgs e)
        {
            showinquire();
        }

        /// <summary>
        /// 取消按钮点击事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void exit_Click(object sender, EventArgs e)
        {
           
            this.mainForm.ReturnTlpMain.Controls.Remove(this.mainForm.Pos_rounds_inquire);
            this.mainForm.ReturnTlpMain.Controls.Add(this.mainForm.TlpRight, 1, 0);
            this.mainForm.functionPanel.rightContrl = "交易";
            this.mainForm.Pos_rounds_inquire.Dispose();
            this.mainForm.Pos_rounds_inquire = null;
        }

        /// <summary>
        /// 当控件不是活动控件时引发
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Pos_rounds_inquire_Leave(object sender, EventArgs e)
        {
            dataSet = pos_rounds_check.pos_roundsInquire("", "");
            dgvinquire.DataSource = dataSet.Tables[0];
        }

        /// <summary>
        /// 设置控件和里面的元素的大小
        /// </summary>
        //private void SetSize()
        //{
        //    this.dgvinquire.Width = this.Width;
        //    this.dgvinquire.Height = this.Height * 222 / 242;

        //    this.dgvinquire.Location = new Point(0, 0);
        //    this.inquery.Location = new Point(this.tableLayoutPanel3.Width * 137/ 759, this.dgvinquire.Height * 9/ 94);
        //    this.exit.Location = new Point(this.tableLayoutPanel3.Width * 460 / 759, this.tableLayoutPanel3.Height * 9 / 94);
        //    this.inputtip.Location = new Point(this.tableLayoutPanel2.Width * 49 / 765, this.tableLayoutPanel2.Height * 22 / 71);
        //    this.begin_date.Location = new Point(this.tableLayoutPanel2.Width * 477 / 765, this.tableLayoutPanel2.Height * 25 / 71);
        //    this.connection.Location = new Point(this.tableLayoutPanel2.Width * 483 / 765, this.tableLayoutPanel2.Height * 22 / 71);
        //    this.over_date.Location = new Point(this.tableLayoutPanel2.Width * 534 / 765, this.tableLayoutPanel2.Height * 25 / 71);
            
        //    this.inquery.Width = this.dgvinquire.Width * 147 / 765;
        //    this.exit.Width = this.dgvinquire.Width * 147 / 765;
        //    this.inputtip.Width = this.dgvinquire.Width * 205 / 765;
        //    this.begin_date.Width = this.dgvinquire.Width * 193 / 765;
        //    this.connection.Width = this.dgvinquire.Width * 32 / 765;
        //    this.over_date.Width = this.dgvinquire.Width * 193 / 765;
            


        //    this.inquery.Height = this.dgvinquire.Height * 50 / 744;
        //    this.exit.Height = this.dgvinquire.Height * 50 / 744;
        //    this.inputtip.Height = this.dgvinquire.Height * 42 / 744;
        //    this.begin_date.Height = this.dgvinquire.Height * 35 / 744;
        //    this.connection.Height = this.dgvinquire.Height * 38 / 744;
        //    this.over_date.Height = this.dgvinquire.Height * 35 / 744;

           

        //    this.dgvinquire.Columns[0].Width = this.dgvinquire.Width * 60 / 765;
        //    this.dgvinquire.Columns[1].Width = this.dgvinquire.Width * 255 / 765;
        //    this.dgvinquire.Columns[2].Width = this.dgvinquire.Width * 255 / 765;
        //    this.dgvinquire.Columns[3].Width = this.dgvinquire.Width * 140 / 765;
          

        //}
        /// <summary>
        /// 加载事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Pos_rounds_inquire_Load(object sender, EventArgs e)
        {
            showinquire();
        }

        


       
       

       
      













    }
}
