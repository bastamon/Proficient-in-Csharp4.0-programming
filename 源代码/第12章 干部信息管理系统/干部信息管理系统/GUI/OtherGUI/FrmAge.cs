using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using HBMISR.Data;
using HBMISR.GUI.MainGUI;


namespace HBMISR.GUI.OtherGUI
{
    /// <summary>
    /// 自定义年龄段窗体
    /// </summary>
    public partial class FrmAge : Form
    {
        DataOperation data = new DataOperation();
        DataTable dt = new DataTable();
        Label[] a;

        /// <summary>
        /// 记录ControlMain类型的实例
        /// </summary>
        public ControlMain ci;

        /// <summary>
        /// 存放初始年龄
        /// </summary>
        string Beginstr;

        /// <summary>
        /// 存放结束年龄
        /// </summary>
        string Endstr;

        /// <summary>
        /// 中间变量
        /// </summary>
        string Middlestr;

        /// <summary>
        /// 总人数
        /// </summary>
        Int32 sum = 0;

        /// <summary>
        /// 存储正副职和所有人员
        /// </summary>
        Int32 qd = 0;

        /// <summary>
        /// 自定义年龄段构造函数
        /// </summary>
        public FrmAge()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 自定义年龄段搜索按钮的事件监听
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
private void btn_search_Click(object sender, EventArgs e)
{
    int h = 0;

    if (rb0.Checked == true)
        qd = 0;
    if (rb1.Checked == true)
        qd = 1;
    if (rb.Checked == true)
        qd = -1;

    if (txt_Agebegin.Text != "" && txt_Ageend.Text != "")
    {
        Beginstr = txt_Agebegin.Text;
        Endstr = txt_Ageend.Text;
        if (Convert.ToInt32(Beginstr) > Convert.ToInt32(Endstr))
        {
            Middlestr = Beginstr;
            Beginstr = Endstr;
            Endstr = Middlestr;
        }
        flowLayoutPanel1.Controls.Clear();
        if (qd == -1)
        {
            dt = data.GetOneDataTable_sql("select birthday from TB_Commoninfo where isdelete = 0");
            h = data.GetRows("select count(*) from TB_Commoninfo where isdelete = 0");
        }
        else
        {
            dt = data.GetOneDataTable_sql("select birthday from TB_Commoninfo where isdelete = 0 and qd='" + qd + "'");
            h = data.GetRows("select count(*) from TB_Commoninfo where isdelete = 0 and qd='" + qd + "'");
        }
        if (Beginstr != "" && Endstr != "")
        {
            int age1 = Convert.ToInt32(Beginstr);

            int age2 = Convert.ToInt32(Endstr);

            a = new Label[age2 - age1 + 1];
            for (int i = 0; i <= age2 - age1; i++)
            {
                a[i] = new Label();
                a[i].Visible = true;
                a[i].Tag = age1 + i;
                a[i].Text = "0";
                flowLayoutPanel1.Controls.Add(a[i]);
            }


            //系统时间
            int year2 = Convert.ToInt32(time1.Text.Substring(0, 4));
            int mounth2 = Convert.ToInt32(time1.Text.Substring(5, 2));

            for (int j = 0; j < h; j++)
            {
                //人员出生日期
                string brithday1 = dt.Rows[j]["birthday"].ToString();

                int year1 = Convert.ToInt32(brithday1.Substring(0, 4));
                int mounth1 = Convert.ToInt32(brithday1.Substring(5, 2));

                if (mounth2 < mounth1)
                {
                    year1 = year1 + 1; //出生年月
                }

                if ((age1 <= year2 - year1) && (year2 - year1 <= age2))
                {
                    search(a, year2 - year1);
                }
            }

            for (int i = 0; i <= age2 - age1; i++)
            {
                Middlestr = a[i].Text;
                sum += Convert.ToInt32(Middlestr);
                a[i].Text = (age1 + i).ToString() + "岁:" + Middlestr;
            }

        }
        groupBox1.Text = "年龄详情" + "(总人数:" + sum.ToString() + ")";
    }
    else
    {
        MessageBox.Show("年龄为空！！", "提示");
    }

}

        /// <summary>
        /// 限制年龄输入为数字
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txt_Agebegin_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar != 8 && !Char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        /// <summary>
        /// 限制年龄输入为数字
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txt_Ageend_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar != 8 && !Char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        /// <summary>
        /// 折半查找年龄
        /// </summary>
        /// <param name="a"></param>
        /// <param name="age"></param>
        public void search(Label[] a, int age)
        {
            int mid = 0, bot, top, find = 0;

            top = 0;

            bot = a.Length - 1;

            do
            {
                mid = (top + bot) / 2;
                if (age == Convert.ToInt32(a[mid].Tag))
                    find = 1;
                else
                    if (age > Convert.ToInt32(a[mid].Tag))
                        top = mid + 1;
                    else
                        bot = mid - 1;
            }
            while ((top <= bot) && (find == 0));

            if (find == 1)
                a[mid].Text = (Convert.ToInt32(a[mid].Text) + 1).ToString();
        }

        /// <summary>
        /// 正副职变化时的事件监听
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void rb0_CheckedChanged(object sender, EventArgs e)
        {
            time1.Text = DateTime.Now.ToString();
            flowLayoutPanel1.Controls.Clear();
            groupBox1.Text = "年龄详情(人数)";
        }

        /// <summary>
        /// 正副职变化时的事件监听
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void rb1_CheckedChanged(object sender, EventArgs e)
        {
            time1.Text = DateTime.Now.ToString();
            flowLayoutPanel1.Controls.Clear();
            groupBox1.Text = "年龄详情(人数)";
        }

        /// <summary>
        /// 正副职变化时的事件监听
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void rb_CheckedChanged(object sender, EventArgs e)
        {
            time1.Text = DateTime.Now.ToString();
            flowLayoutPanel1.Controls.Clear();
            groupBox1.Text = "年龄详情(人数)";
        }

        /// <summary>
        /// 自定义年龄段初始化
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AgeForm_Load(object sender, EventArgs e)
        {
            time1.Text = DateTime.Now.ToString("yyyy'年'MM'月'") + "01日";

            if (ci.comboBox1.Text == "正职后备")
                rb1.Checked = true;
            if (ci.comboBox1.Text == "副职后备")
                rb0.Checked = true;
            if (ci.comboBox1.Text == "所有人员")
                rb.Checked = true;
        }
    }
}
