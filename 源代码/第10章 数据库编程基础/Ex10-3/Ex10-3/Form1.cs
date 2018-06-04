using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace Ex10_3
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            string connection_str;
            connection_str = "Data Source = ZGC-20130515AMW\\SQLEXPRESS; Initial Catalog = gbxxdb; User Id = test; Password = 123456";

            SqlConnection myConnection = new SqlConnection();
            myConnection.ConnectionString = connection_str;
            myConnection.Open();

            //利用SqlDataReader对象查询TB_Commoninf表中的内容返回表中所有的记录
            string sql_str;
            sql_str = "select * from TB_Commoninf";
            SqlCommand myCommand = new SqlCommand();
            myCommand.CommandText = sql_str;
            myCommand.Connection = myConnection;
            SqlDataReader myDataReader = myCommand.ExecuteReader();
            listBox1.Items.Add("编号\t姓名\t\t性别\t民族\t\t身份证号\t\t职务\t\t籍贯\t出生日期\t入党时间");
            listBox1.Items.Add("========================================================================================================================");

            while (myDataReader.Read())
            {
                listBox1.Items.Add(String.Format("{0}\t{1}\t{2}\t{3}\t{4}\t{5}\t{6}\t{7}\t{8}",
                myDataReader[0].ToString(), myDataReader[1].ToString(),
               myDataReader[2].ToString(), myDataReader[3].ToString(),
               myDataReader[4].ToString(), myDataReader[5].ToString(),
               myDataReader[6].ToString(), myDataReader[7].ToString(),
               myDataReader[8].ToString()));
            }
            myConnection.Close();
        }
    }
}
