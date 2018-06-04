using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using POS.Common;
/*用于显示交易号、pos机编号、员工号、班次和系统当前时间的功能
 类说明：此为显示交易号、pos机编号、员工号、班次和系统当前时间的控件 
主要公共方法：
（1）、ChangeText(string DealNum, string SaleKind, string PosNum, string EmployeeNum, string WorkNum)，更改交易号、销售对象、pos机编号、员工编号和班次， DealNum：交易号，SaleKind：销售对象，PosNum： pos机编号，EmployeeNum：员工编号，WorkNum：班次 

 */
namespace POS.Controls
{
    /// <summary>
    /// 此为显示交易号、pos机编号、员工号、班次和系统当前时间的控件
    /// </summary>
    public partial class ShowInfo : UserControl
    {
        ReadIni readIni = new ReadIni("Info.ini");
        /// <summary>
        /// 此为系统自动生成的代码
        /// </summary>
        public ShowInfo()
        {
            InitializeComponent();
            SetSize();
        }

        //时间控件的时间，此事件用来给lblSystemTime空间附加当前时间
        private void timer1_Tick(object sender, EventArgs e)
        {
            this.lblSystemTime.Text = DateTime.Now.ToString();
        }

        /// <summary>
        /// 设置所有label的Font
        /// </summary>
        [Description("设置设置所有label的font"), Category("自定义")]
        public Font SetAllLabelFont
        {
            get { return this.lblDealNum0.Font; }
            set
            {
                this.lblDealNum0.Font = value;
                this.lblDealNum.Font = value;
                this.lblEmpName0.Font = value;
                this.lblEmpName.Font = value;
                this.lblPosNum0.Font = value;
                this.lblPosNum.Font = value;
                this.lblEmployeeNum0.Font = value;
                this.lblEmployeeNum.Font = value;
                this.lblWorkNum0.Font = value;
                this.lblWorkNum.Font = value;
                this.lblSystemTime.Font = value;
                this.temperature.Font = value;
            }
        }
        /// <summary>
        /// 设置设置除了lblSaleKind这个label外的所有label的字体颜色
        /// </summary>
        [Description("设置设置除了lblSaleKind这个label外的所有label的字体颜色"), Category("自定义")]
        [DefaultValue(typeof(Color), "Black")]//给予初始值
        public Color SetMostLabelForeColor
        {
            get { return this.lblDealNum0.ForeColor; }
            set
            {
                this.lblDealNum0.ForeColor = value;
                this.lblDealNum.ForeColor = value;
                this.lblEmpName0.ForeColor = value;
                this.lblPosNum0.ForeColor = value;
                this.lblPosNum.ForeColor = value;
                this.lblEmployeeNum0.ForeColor = value;
                this.lblEmployeeNum.ForeColor = value;
                this.lblWorkNum0.ForeColor = value;
                this.lblWorkNum.ForeColor = value;
                this.lblSystemTime.ForeColor = value;
                this.temperature.ForeColor = value;
            }
        }
        /// <summary>
        /// 设置lblSaleKind这个的label的ForeColor
        /// </summary>
        [Description("设置lblSaleKind这个的label的字体颜色"), Category("自定义")]
        [DefaultValue(typeof(Color), "Black")]//给予初始值
        public Color SetlblSaleKindForeColor
        {
            get { return this.lblEmpName.ForeColor; }
            set
            {
                this.lblEmpName.ForeColor = value;
            }
        }
       //设置控件的布局
        private void SetSize()
        {
            //设置各Label的宽和高
            this.picLogo.Size = new Size(this.Height-4, this.Height-4);
            this.lblDealNum0.Size = new Size(77 * this.Width / 1157, this.Height);
            this.lblDealNum.Size = new Size(63 * (this.Width) / 1157, this.Height);           
            this.lblPosNum0.Size = new Size(49 * (this.Width) / 1157, this.Height);
            this.lblPosNum.Size = new Size(43* (this.Width) / 1157, this.Height);
            this.lblEmployeeNum0.Size = new Size(63 * (this.Width) / 1157, this.Height);
            this.lblEmployeeNum.Size = new Size(79 * (this.Width) / 1157, this.Height);
            this.lblEmpName0.Size = new Size(63 * (this.Width) / 1157, this.Height);
            this.lblEmpName.Size = new Size(120 * (this.Width) / 1157, this.Height);
            this.lblWorkNum0.Size = new Size(58 * (this.Width) / 1157, this.Height);
            this.lblWorkNum.Size = new Size(50 * (this.Width) / 1157, this.Height);
            this.weather.Size = new Size(45 * (this.Width) / 1157, this.Height - 2);
            this.temperature.Size = new Size(148 * (this.Width) / 1157, this.Height);
            this.lblSystemTime.Size = new Size(176 * (this.Width) / 1157, this.Height);

            //设定各Label的坐标
            this.picLogo.Location = new System.Drawing.Point(4, 2);
            this.lblDealNum0.Location = new System.Drawing.Point(63* (this.Width) / 1157, 4);//80
            this.lblDealNum.Location = new System.Drawing.Point(135 * (this.Width) / 1157, 4);//63

            this.lblPosNum0.Location = new System.Drawing.Point(222 * (this.Width) / 1157, 4);//49
            this.lblPosNum.Location = new System.Drawing.Point(269* (this.Width) / 1157, 4);//59
          

            this.lblEmployeeNum0.Location = new System.Drawing.Point(325 * (this.Width) / 1157, 4);//63
            this.lblEmployeeNum.Location = new System.Drawing.Point(373 * (this.Width) / 1157, 4);//98

            this.lblEmpName0.Location = new System.Drawing.Point(478 * (this.Width) / 1157, 4);//62
            this.lblEmpName.Location = new System.Drawing.Point(527 * (this.Width) / 1157, 4);//103

            this.lblWorkNum0.Location = new System.Drawing.Point(655 * (this.Width) / 1157, 4);
            this.lblWorkNum.Location = new System.Drawing.Point(705 * (this.Width) / 1157, 4);
            this.weather.Location = new System.Drawing.Point(775 * (this.Width) / 1157, 0);
            this.temperature.Location = new System.Drawing.Point(823 * (this.Width) / 1157, 4);
            this.lblSystemTime.Location = new System.Drawing.Point(976 * (this.Width) / 1157, 4);
            FontSizeChange();
            this.Size=new Size(this.Size.Width,this.lblDealNum.Size.Height);
        }
        /// <summary>
        /// 控件的字体大小随空间的大小的改变而改变 
        /// </summary>
        public void FontSizeChange()
        {
            this.lblDealNum0.Font = new System.Drawing.Font(this.SetAllLabelFont.Name, (this.lblDealNum.Size.Width) / 5);
            this.lblDealNum.Font = new System.Drawing.Font(this.SetAllLabelFont.Name, (this.lblDealNum.Size.Width ) / 5);
            this.lblEmpName0.Font = new System.Drawing.Font(this.SetAllLabelFont.Name, (this.lblDealNum.Size.Width ) / 5);
            this.lblEmpName.Font = new System.Drawing.Font(this.SetAllLabelFont.Name, (this.lblDealNum.Size.Width ) / 5);
            this.lblPosNum0.Font = new System.Drawing.Font(this.SetAllLabelFont.Name, (this.lblDealNum.Size.Width ) / 5);
            this.lblPosNum.Font = new System.Drawing.Font(this.SetAllLabelFont.Name, (this.lblDealNum.Size.Width ) / 5);
            this.lblEmployeeNum0.Font = new System.Drawing.Font(this.SetAllLabelFont.Name, (this.lblDealNum.Size.Width ) / 5);
            this.lblEmployeeNum.Font = new System.Drawing.Font(this.SetAllLabelFont.Name, (this.lblDealNum.Size.Width ) / 5);
            this.lblWorkNum0.Font = new System.Drawing.Font(this.SetAllLabelFont.Name, (this.lblDealNum.Size.Width ) / 5);
            this.lblWorkNum.Font = new System.Drawing.Font(this.SetAllLabelFont.Name, (this.lblDealNum.Size.Width ) / 5);
            this.lblSystemTime.Font = new System.Drawing.Font(this.SetAllLabelFont.Name, (this.lblDealNum.Size.Width ) / 5);
            this.temperature.Font = new System.Drawing.Font(this.SetAllLabelFont.Name, (this.lblDealNum.Size.Width) / 5);
        }
        //该控件的大小改变时触发的事件
        private void ShowInfo_SizeChanged(object sender, EventArgs e)
        {
            try
            {
                SetSize();
            }
            catch { } 
        }
       /// <summary>
       /// 加载
       /// </summary>
       /// <param name="sender"></param>
       /// <param name="e"></param>
        private void ShowInfo_Load(object sender, EventArgs e)
        {
           // LblChangeText chText = new LblChangeText(this.ChangeText);
        }
        /// <summary>
        /// 该事件是用来更改该控件中的内部Label空间的text，需要更改的内容有交易号lblDealNum、
        /// 销售对象lblSaleKind、pos机编号lblPosNum、员工编号lblEmployeeNum和班次lblWorkNum的内容Txt
        /// </summary>
        /// <param name="DealNum">交易号</param>
        /// <param name="emp_name">员工姓名</param>
        /// <param name="PosNum">pos机编号</param>
        /// <param name="EmployeeNum">员工编号</param>
        /// <param name="WorkNum">班次</param> 
        public void ChangeText(string DealNum, string emp_name, string PosNum, string EmployeeNum, string WorkNum)
        {
           
            //对交易号位数的判定，主界面显示交易号应该为四位
            string deal_number1 = Convert.ToString(DealNum);
            
            switch (deal_number1.Length)
            {
                    
                case 1:
                    deal_number1 = "000" + deal_number1;
                    break;
                case 2:
                    deal_number1 = "00" + deal_number1;
                    break;
                case 3:
                    deal_number1 = "0" + deal_number1;
                    break;
                case 4: break;
                default:
                    deal_number1 = "0001";
                    Info.deal_number = 1;
                    readIni.WriteString("RepastErp", "deal_number", "1");
                    break;

            }

        
            this.lblDealNum.Text = deal_number1;
            this.lblEmpName.Text = emp_name;
            this.lblPosNum.Text = PosNum;
            this.lblEmployeeNum.Text = EmployeeNum;
            this.lblWorkNum.Text = WorkNum;
           
        }
        /// <summary>
        /// 该方法是对显示天气状况的控件赋值和显示温度的控件赋值
        /// </summary>
        /// <param name="weather">天气状况</param>
        /// <param name="firstTemperature">第一个温度值</param>
        /// <param name="lastTemperature">第二个温度值</param>
        public void ChangeText2(string weather, int firstTemperature, int lastTemperature)
        {
            this.temperature.Text = weather+"   "+firstTemperature + "℃" + " -- " + lastTemperature + "℃";
            if (weather == "晴" || weather == "晴天")
            {
                this.weather.BackgroundImage = Properties.Resources.晴;
            }
            if (weather == "阴" || weather == "阴天")
            {
                this.weather.BackgroundImage = Properties.Resources.阴;
            }
            if (weather == "雪" || weather == "雪天")
            {
                this.weather.BackgroundImage = Properties.Resources.雪;
            }
            if (weather == "多云")
            {
                this.weather.BackgroundImage = Properties.Resources.多云;
            }
            if (weather == "小雨")
            {
                this.weather.BackgroundImage = Properties.Resources.小雨;
            }
            if (weather == "大雨")
            {
                this.weather.BackgroundImage = Properties.Resources.大雨;
            }
            if (weather == "台风")
            {
                this.weather.BackgroundImage = Properties.Resources.台风;
            }
        }
    }
}
