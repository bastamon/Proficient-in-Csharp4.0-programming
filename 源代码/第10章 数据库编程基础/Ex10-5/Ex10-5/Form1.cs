using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace Ex10_5
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

            //利用SqlDataAdapter对象查询TB_Commoninftb表中的内容，返回表中所有的记录
            sql_str = "select * from TB_Commoninf";
            SqlDataAdapter myDataAdapter = new SqlDataAdapter(sql_str, myConnection);
            DataSet myDataSet = new DataSet();
            myDataAdapter.Fill(myDataSet, "TB_Commoninf");

            myBindingSource = new BindingSource(myDataSet, "TB_Commoninf");
            Binding binding_gbbh = new Binding("Text", myBindingSource, "cno");
            textBox1.DataBindings.Add(binding_gbbh);
            Binding binding_gbxm = new Binding("Text", myBindingSource, "name");
            textBox2.DataBindings.Add(binding_gbxm);
            Binding binding_gbxb = new Binding("Text", myBindingSource, "sex");
            textBox3.DataBindings.Add(binding_gbxb);
            Binding binding_gbmz = new Binding("Text", myBindingSource, "nation");
            textBox4.DataBindings.Add(binding_gbmz);
            Binding binding_gbsfzh = new Binding("Text", myBindingSource, "cid");
            textBox5.DataBindings.Add(binding_gbsfzh);
            Binding binding_gbzw = new Binding("Text", myBindingSource, "position");
            textBox6.DataBindings.Add(binding_gbzw);
            Binding binding_gbjg = new Binding("Text", myBindingSource, "native");
            textBox7.DataBindings.Add(binding_gbjg);
            Binding binding_csrq = new Binding("Text", myBindingSource, "birthday");
            textBox8.DataBindings.Add(binding_csrq);
            Binding binding_rdsj = new Binding("Text", myBindingSource, "partyTime");
            textBox9.DataBindings.Add(binding_rdsj);

            myConnection.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (myBindingSource.Position != 0)
                myBindingSource.MoveFirst();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (myBindingSource.Position != 0)
                myBindingSource.MovePrevious();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (myBindingSource.Position != myBindingSource.Count - 1)
                myBindingSource.MoveNext();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (myBindingSource.Position != myBindingSource.Count - 1)
                myBindingSource.MoveLast();
        }
    }
}
