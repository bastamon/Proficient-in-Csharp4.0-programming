using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace Exercise10._3___1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            // 利用SqlConnection对象，连接数据库
            string mystr;
            mystr = "Data Source =ZGC-20130515AMW\\SQLEXPRESS; Initial Catalog = gbxxdb;User Id = sa;Password = ";  //OK
            SqlConnection myconn = new SqlConnection();
            myconn.ConnectionString = mystr;
            myconn.Open();
            if (myconn.State == ConnectionState.Open)
                MessageBox.Show(this, "恭喜，成功连接!", "连接SQL Server数据库测试程序", MessageBoxButtons.OKCancel);
            else
                MessageBox.Show(this, "连接失败，程序还有问题!", "连接SQL Server数据库测试程序", MessageBoxButtons.OKCancel);

            // 利用SqlDataReader对象，查询TB_Commoninf表中的内容，返回表中前六个字段的所有记录
            string sql_str;
            sql_str = "select cno, name, sex, nation, CID, position, native from TB_Commoninf where sex='男' order by cno asc";
            SqlCommand myCommand = new SqlCommand();
            myCommand.CommandText = sql_str;
            myCommand.Connection = myconn;
            SqlDataReader myDataReader = myCommand.ExecuteReader();
            listBox1.Items.Add("编号\t姓名\t性别\t民族\t身份证号\t\t职务\t籍贯");
            listBox1.Items.Add("=====================================================================");
            while (myDataReader.Read())
            {
                listBox1.Items.Add(String.Format("{0}\t{1}\t{2}\t{3}\t{4}\t{5}\t{6}",
                    myDataReader[0].ToString().TrimEnd(), myDataReader[1].ToString().TrimEnd(),
                    myDataReader[2].ToString().TrimEnd(), myDataReader[3].ToString().TrimEnd(),
                    myDataReader[4].ToString().TrimEnd(), myDataReader[5].ToString().TrimEnd(),
                    myDataReader[6].ToString().TrimEnd()));
            }
            myconn.Close();
        }
    }
}
