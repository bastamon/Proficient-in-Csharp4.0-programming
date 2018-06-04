using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace Ex10_4
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            string connection_str, sql_str;
            connection_str = "Data Source = ZGC-20130515AMW\\SQLEXPRESS; Initial Catalog = gbxxdb; User Id = test; Password = 123456";

            SqlConnection myConnection = new SqlConnection();
            myConnection.ConnectionString = connection_str;
            myConnection.Open();
            //利用SqlDataAdapter对象查询TB_Commoninf表中的内容返回表中所有的记录
            sql_str = "select * from TB_Commoninf";
            SqlDataAdapter myDataAdapter = new SqlDataAdapter(sql_str, myConnection);
            DataSet myDataSet = new DataSet();
            myDataAdapter.Fill(myDataSet, "TB_Commoninf");

            listBox1.Items.Add("编号\t姓名\t\t性别\t民族\t\t身份证号\t\t职务\t\t籍贯\t出生日期\t入党时间");
            listBox1.Items.Add("========================================================================================================================");

            for (int i = 0; i < myDataSet.Tables[0].Rows.Count; i++)
            {
                listBox1.Items.Add(String.Format("{0}\t{1}\t{2}\t{3}\t{4}\t{5}\t{6}\t{7}\t{8}",
                     myDataSet.Tables[0].Rows[i].ItemArray[0].ToString(),
                     myDataSet.Tables[0].Rows[i].ItemArray[1].ToString(),
                     myDataSet.Tables[0].Rows[i].ItemArray[2].ToString(),
                     myDataSet.Tables[0].Rows[i].ItemArray[3].ToString(),
                     myDataSet.Tables[0].Rows[i].ItemArray[4].ToString(),
                     myDataSet.Tables[0].Rows[i].ItemArray[5].ToString(), 
                     myDataSet.Tables[0].Rows[i].ItemArray[6].ToString(),
                     myDataSet.Tables[0].Rows[i].ItemArray[7].ToString(),
                     myDataSet.Tables[0].Rows[i].ItemArray[8].ToString()));
            }
            myConnection.Close();
        }
    }
}
