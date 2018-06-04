using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace Ex10_1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string connection_str;
            connection_str = "Data Source = ZGC-20130515AMW\\SQLEXPRESS; Initial Catalog = gbxxdb; User Id = test; Password = 123456";

            SqlConnection myConnection = new SqlConnection();
            myConnection.ConnectionString = connection_str;
            myConnection.Open();
            if (myConnection.State == ConnectionState.Open)
                MessageBox.Show(this, "恭喜，成功连接！", "连接SQL Server数据库测试程序", MessageBoxButtons.OKCancel);
            else
                MessageBox.Show(this, "连接失败，你还要继续努力！", "连接SQL Server数据库测试程序", MessageBoxButtons.OKCancel);
            myConnection.Close();
        }
    }
}
