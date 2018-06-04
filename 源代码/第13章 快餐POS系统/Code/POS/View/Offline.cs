using System;
using System.Windows.Forms;
using POS.Common;
using POS.Service;
using System.Collections.Generic;
using Common.Print;
using System.Threading;
//下线窗体，对下线所有信息处理
/*主要方法：
（1）、Run() 用于打印下线小票信息
主要事件：
（1）、btndone_Click(object sender, EventArgs e)确定下线

*/
namespace POS.View
{
    /// <summary>
    /// 下线窗体类
    /// </summary>
    public partial class Offline : Form
    {
        /// <summary>
        /// true表示在功能面板点击下线，false代表主窗体点击×接着点确定
        /// </summary>
        public bool isExitType = false;

        /// <summary>
        /// GetMoney_income的对象
        /// </summary>
        GetMoney_income getmoney_income;

        /// <summary>
        /// GetSale00_data的对象
        /// </summary>
        GetSale00_data getSale00_data;

        /// <summary>
        /// GetSale01_data的对象
        /// </summary>
        GetSale01_data getSale01_data;

        /// <summary>
        /// 构造函数
        /// </summary>
        public Offline()
        {
            InitializeComponent();

            this.StartPosition = FormStartPosition.CenterScreen;//窗体居中
            //界面相关显示的信息
            shop_name.Text = Info.shop_name;
            pos_id.Text = Info.pos_id;
            emp_id.Text = Info.emp_id;
            emp_name.Text = Info.emp_name;
            login_date.Text = Info.login_date.ToString("yyyy-MM-dd HH:mm:ss");
            offline_shift.Text = Convert.ToString(Info.shift_num); 
            cashier_sum.Text = Convert.ToString (Info.cashier_sum);
            largeBillsNum.Text=Convert.ToString(Decimal.ToInt32(Info.largeBillsNum));
            offline_time.Text = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            this.Visible = true;
        }

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
        ///主线程调用用于打印下线小票
        /// </summary>
        public void Run()
        {
            try
            {
                getmoney_income = new GetMoney_income();
                getSale00_data = new GetSale00_data();
                getSale01_data = new GetSale01_data();
                List<string> list = new List<string>();
                //list.Add(this.mainForm.OperPara.TicketHead);
                this.mainForm.AddExtraInfo(list, this.mainForm.OperPara.TicketHead);
                list.Add("**********下线处理**********");
                list.Add("收银台号：" + Info.pos_id + "        " + "员工号：" + Info.emp_id);
                list.Add("----" + Info.exit_date + "----");
                list.Add("班次：" + Info.shift_num + "          " + "零用金：" + Info.cashier_sum);
                list.Add("现金收入：" + "          " + "￥：" + getmoney_income.ReturenMoney_income);
                list.Add("作废总额：" + "          " + "￥：" + getSale00_data.ReturenWaste_amount);
                list.Add("招待餐饮：" + "          " + "￥：" + getSale00_data.ReturenTreat_amount);
                list.Add("员工餐饮：" + "          " + "￥：" + getSale00_data.ReturenEmp_amount);
                list.Add("VIP卡收入：" + "         " + "￥：" + getSale00_data.ReturenVIP_amount);
                list.Add("促销金额：" + "          " + "￥：" + getSale01_data.ReturenPromotion_amount);
                list.Add("折扣总额：" + "          " + "￥：" + getSale01_data.ReturenDiscount_amount);
                list.Add("抽大钞金额：" + "        " + "￥：" +Decimal.ToInt32(Info.largeBillsNum));
                list.Add("                                 ");
                //list.Add(this.mainForm.OperPara.TicketTail);
                this.mainForm.AddExtraInfo(list, this.mainForm.OperPara.TicketTail);
                LPTControl.Port = Info.printPort;
                //LPTControl.NewRow(2);
                foreach (string s in list)
                {
                    LPTControl.WriteLine(s);
                }
                LPTControl.NewRow(1);
                LPTControl.OpenBox();
                LPTControl.CutPaper();
                LPTControl.Close();
            }
            catch { }
        }

        /// <summary>
        /// 下线界面中确定按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btndone_Click(object sender, EventArgs e)
        {
              isExitType = true;
              Info.exit_date = DateTime.Now;
              //下线更新pos_rounds表（本地）
              if(InsertPos_rounds.InitController().Update_Posrounds(Info.exit_date))
              {
                  //创建一个线程用于打印下线小票
                  Thread thread = new Thread(new ThreadStart(Run));
                  thread.Start();
                  //删除提单前的空的销售单
                  ChangeInfo changeinfo= new ChangeInfo();
                  changeinfo.DelSaletmp00(Info.sale_id);
                  ReadIni readIni = new ReadIni();
                  readIni.WriteString("RepastErp", "sale_id", "");
                  //删除本地临时表中符合要求的数据
                  DelLocalDB dellocaldb = new DelLocalDB(this.mainForm);
                  dellocaldb.DelSaletmp();

                  this.Dispose();
              }
              //else
              //{
              //    MessageBox.Show("下线失败！请在本Pos机上下线。");
              //}
             
            
        }
        /// <summary>
        /// 下线界面中取消按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Dispose();

        }  
    }
}
