using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace Ex10_6
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
            connection_str = "Data Source = ZGC-20130515AMW\\SQLEXPRESS; Initial Catalog = gbxxdb22; User Id = test; Password = 123456";

            SqlConnection myConnection = new SqlConnection();
            myConnection.ConnectionString = connection_str;
            myConnection.Open();

            //利用SqlDataAdapter对象查询TB_Commoninftb表中的内容，返回表中所有的记录  
            sql_str = "select * from TB_Commoninf";
            SqlDataAdapter myDataAdapter = new SqlDataAdapter(sql_str, myConnection);
            DataSet myDataSet = new DataSet();
            BindingSource myBindingSource = new BindingSource();
            myDataAdapter.Fill(myDataSet, "TB_Commoninf");
            myBindingSource = new BindingSource(myDataSet, "TB_Commoninf");

            Binding binding_bh = new Binding("Text", myBindingSource, "cno");
            textBox1.DataBindings.Add(binding_bh);
            Binding binding_xm = new Binding("Text", myBindingSource, "name");
            textBox2.DataBindings.Add(binding_xm);
            Binding binding_xb = new Binding("Text", myBindingSource, "sex");
            textBox3.DataBindings.Add(binding_xb);
            Binding binding_mz = new Binding("Text", myBindingSource, "nation");
            textBox4.DataBindings.Add(binding_mz);
            Binding binding_sfzh = new Binding("Text", myBindingSource, "cid");
            textBox5.DataBindings.Add(binding_sfzh);
            Binding binding_zw = new Binding("Text", myBindingSource, "position");
            textBox6.DataBindings.Add(binding_zw);
            Binding binding_jg = new Binding("Text", myBindingSource, "nation");
            textBox7.DataBindings.Add(binding_jg);
            Binding binding_csrq = new Binding("Text", myBindingSource, "birthday");
            textBox8.DataBindings.Add(binding_csrq);
            Binding binding_rdsj = new Binding("Text", myBindingSource, "partyTime");
            textBox9.DataBindings.Add(binding_rdsj);

            bindingNavigator1.BindingSource = myBindingSource;
            myConnection.Close();
        }
    }
}
