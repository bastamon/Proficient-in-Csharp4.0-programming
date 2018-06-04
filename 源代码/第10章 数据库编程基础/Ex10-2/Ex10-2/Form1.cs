using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace Ex10_2
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string connection_str, sql_str;
            connection_str = "Data Source = ZGC-20130515AMW\\SQLEXPRESS; Initial Catalog = gbxxdb; User Id = test; Password = 123456";

            SqlConnection myConnection = new SqlConnection();
            myConnection.ConnectionString = connection_str;
            myConnection.Open();

            sql_str = "select count(*) from dbo.TB_Commoninf ";
            SqlCommand myCommand = new SqlCommand();
            myCommand.CommandText = sql_str;
            myCommand.Connection = myConnection;
            string gb_number = myCommand.ExecuteScalar().ToString();
            MessageBox.Show(this, "TB_Commoninf 表中共有" + gb_number + "条记录。", "SqlCommand对象测试程序", MessageBoxButtons.OKCancel);

            myConnection.Close();
        }
    }
}
