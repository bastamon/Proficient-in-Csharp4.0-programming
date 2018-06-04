using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using HBMISR.Data;
using System.Collections;


namespace HBMISR.GUI.AnalyzeGUI
{
    /// <summary>
    /// 窗体类，高级级查询
    /// </summary>
    public partial class FrmSearchForm: Form
    {
        DataOperation a1;   //连接单位数据库的表
        string filepath = string.Empty;
        DataTable dt;
        /// <summary>
        /// 读取ini文件
        /// </summary>
        ReadIni ini;

        /// <summary>
        /// 保存版本号
        /// </summary>
        string version;

        /// <summary>
        /// 保存级别
        /// </summary>
        DataTable dt2;

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
        /// 存储查询数据编号
        /// </summary>
        int[] arry;

        /// <summary>
        /// 读取总表
        /// </summary>
        DataOperation a2;

        /// <summary>
        /// 构造方法
        /// </summary>
        public FrmSearchForm()
        {
            InitializeComponent ();
            ini = new ReadIni ();
            version = ini.ReadString ("version");
            //读取所有单位的信息表
            a2 = new DataOperation (Application.StartupPath + "\\DB\\DBHBMSU.db");
        }

        /// <summary>
        /// 窗体加载事件的处理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SearchForm_Load( object sender, EventArgs e )
        {
            time1.Text = DateTime.Now.ToString("yyyy'年'MM'月'") + "01日";

            a1 = new DataOperation ();
            string s = "select distinct nation from TB_CommonInfo ";
            DataTable datatable = a1.GetOneDataTable_sql (s);

            listView1.View = View.Details;
            listView1.LabelEdit = true;
            listView1.GridLines = true;
            listView1.AllowColumnReorder = true;
            listView1.LabelEdit = false;
            listView1.HideSelection = false;
            listView1.Columns.Add ("选择", 50, HorizontalAlignment.Center);
            listView1.Columns.Add ("姓名", 80, HorizontalAlignment.Center);
            listView1.Columns.Add ("性别", 70, HorizontalAlignment.Center);
            listView1.Columns.Add ("单位", 179, HorizontalAlignment.Left);
            listView1.Columns.Add ("职务", 247, HorizontalAlignment.Left);

            dt2 = a2.GetOneDataTable_sql ("select grade from TB_Grade where kind='" + version + "'");
            for( int i = 0;i < dt2.Rows.Count;i++ )
            {
                Grade_ComboBox.Items.Add (dt2.Rows[i]["grade"].ToString ());
            }

            PostionClass_ComboBox.SelectedIndex = 0;
        }

        /// <summary>
        /// 查询按钮的监听
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Search_Button_Click( object sender, EventArgs e )
        {
            listView1.Items.Clear ();
            //单位类别、单位名字、级别、职务类型、性别、党派、民族、
            //全日制学历、全日制学位、在职学历、在职学位、年龄段、起始时间、结束时间
            int count1 = 0; //分母
            int count2 = 0; //分子
            string sql = string.Empty;
            string sql1 = string.Empty;
            string sql2 = string.Empty;
            string sql3 = string.Empty;
            string sql4 = string.Empty;
            string sql5 = string.Empty;
            string sql6 = string.Empty;
            string sql7 = string.Empty;
            string sql8 = string.Empty;
            string sql9 = string.Empty;
            string uid = string.Empty;
            dt = a1.GetOneDataTable_sql ("SELECT count(*) FROM TB_CommonInfo where isDelete='0'");

            count1 = Convert.ToInt32 (dt.Rows[0][0].ToString());

            //单位类别、单位名字、级别、职务类型、性别、党派、民族、
            //全日制学历、全日制学位、在职学历、在职学位、年龄段、起始时间、结束时间
            if( Grade_ComboBox.Text != string.Empty )
            {
                sql2 = Grade_ComboBox.Text;
                if( sql2.Equals ("正厅级") )
                    sql2 = "grade = '8'";
                if( sql2.Equals ("副厅级") )
                    sql2 = "grade='7'";
                if( sql2.Equals ("正县处级") )
                    sql2 = "grade='6'";
                if( sql2.Equals ("副县处级") )
                    sql2 = "grade='5'";
                if( sql2.Equals ("正科级") )
                    sql2 = "grade='4'";
                if( sql2.Equals ("副科级") )
                    sql2 = "grade='3'";
                if( sql2.Equals ("科员") )
                    sql2 = "grade='2'";
                if( sql2.Equals ("办事员") )
                    sql2 = "grade='1'";
            }
            if( Party_ComboBox.Text != string.Empty )//党派
            {
                sql3 = Party_ComboBox.Text;
                sql3 = "partyClass= '" + sql3 + "'";
            }

            if( Nation_ComboBox.Text != string.Empty )//民族
            {
                if( Nation_ComboBox.Text == "少数民族" )
                    sql4 = "nation!='汉族'";
                else
                {
                    sql4 = Nation_ComboBox.Text;
                    sql4 = "nation= '" + sql4 + "'";
                }
            }
            if( FullEducation_ComboBox.Text != string.Empty )//全日制学历
            {
                sql5 = FullEducation_ComboBox.Text;
                sql5 = "fulleducation= '" + sql5 + "'";
            }

            if( FullDegree_ComboBox.Text != string.Empty )//全日制学位
            {
                sql6 = FullDegree_ComboBox.Text;
                sql6 = "fulldegree = '" + sql6 + "'";

            }
            if( OnEducation_ComboBox.Text != string.Empty ) //在职学历
            {
                sql7 = OnEducation_ComboBox.Text;
                sql7 = "workEducation= '" + sql7 + "'";

            }
            if( OnDegree_ComboBox.Text != string.Empty )//在职学位
            {
                sql8 = OnDegree_ComboBox.Text;
                sql8 = "workDegree = '" + sql8 + "'";
            }
            //性别
            if( Sex_ComboBox.Text != string.Empty )
            {
                sql9 = Sex_ComboBox.Text;
                sql9 = "sex = '" + sql9 + "'";
            }


            string[] sql0 = new string[9] { sql1, sql2, sql3, sql4, sql5, sql6, sql7, sql8, sql9 };

            sql = "SELECT * FROM TB_Commoninfo WHERE isdelete = 0";

            for( int j = 0;j < 9;j++ )
            {
                if( sql0[j].ToString () != "" )
                    sql += " and " + sql0[j];
            }

            if( PostionClass_ComboBox.Text == "正职" ) //职务类型
            {
                sql = sql + " and qd = '1'";
            }

            if( PostionClass_ComboBox.Text == "副职" )
            {
                sql = sql + " and qd = '0'";
            }

            //年龄段
            dt = a1.GetOneDataTable_sql (sql);

            int h = dt.Rows.Count;

            arry = new int[h];

            //符合年龄条件的数
            #region
            if( Age_ComboBox.Text != string.Empty )
            {
                try
                {
                    //统计时的年月
                    int year2 = Convert.ToInt32(time1.Text.Substring(0, 4));
                    int mounth2 = Convert.ToInt32(time1.Text.Substring(5, 2));

                    int i = 0;

                    switch( Age_ComboBox.Text )
                    {

                        case "45岁到50岁":
                        for( int j = 0;j < h;j++ )
                        {
                            //出生的年月
                            string brithday1 = dt.Rows[j]["birthday"].ToString ();
                            int year1 = Convert.ToInt32 (brithday1.Substring (0, 4));
                            int mounth1 = Convert.ToInt32 (brithday1.Substring (5, 2));
                            if( mounth2 < mounth1 )
                            {
                                year1 = year1 + 1; //出生年月
                            }
                            if( year2 - year1 <= 50 && year2 - year1 > 45 )
                            {
                                count2 = count2 + 1;
                                arry[i++] = j;
                            }
                        }
                        break;

                        case "40岁到45岁":
                        for( int j = 0;j < h;j++ )
                        {
                            //出生的年月
                            string brithday1 = dt.Rows[j]["birthday"].ToString ();
                            int year1 = Convert.ToInt32 (brithday1.Substring (0, 4));
                            int mounth1 = Convert.ToInt32 (brithday1.Substring (5, 2));
                            if( mounth2 < mounth1 )
                            {
                                year1 = year1 + 1; //出生年月
                            }
                            if( year2 - year1 <= 45 && year2 - year1 > 40 )
                            {
                                count2 = count2 + 1;
                                arry[i++] = j;
                            }
                        }
                        break;

                        case "35岁到40岁":

                        for( int j = 0;j < h;j++ )
                        {
                            //出生的年月
                            string brithday1 = dt.Rows[j]["birthday"].ToString ();
                            int year1 = Convert.ToInt32 (brithday1.Substring (0, 4));
                            int mounth1 = Convert.ToInt32 (brithday1.Substring (5, 2));
                            if( mounth2 < mounth1 )
                            {
                                year1 = year1 + 1; //出生年月
                            }
                            if( year2 - year1 <= 40 && year2 - year1 > 35 )
                            {
                                count2 = count2 + 1;
                                arry[i++] = j;
                            }
                        }
                        break;

                        case "33岁到35岁":
                        for( int j = 0;j < h;j++ )
                        {
                            //出生的年月
                            string brithday1 = dt.Rows[j]["birthday"].ToString ();
                            int year1 = Convert.ToInt32 (brithday1.Substring (0, 4));
                            int mounth1 = Convert.ToInt32 (brithday1.Substring (5, 2));
                            if( mounth2 < mounth1 )
                            {
                                year1 = year1 + 1; //出生年月
                            }
                            if( year2 - year1 <= 35 && year2 - year1 > 33 )
                            {
                                count2 = count2 + 1;
                                arry[i++] = j;
                            }
                        }
                        break;

                        case "30岁到33岁":

                        for( int j = 0;j < h;j++ )
                        {
                            //出生的年月
                            string brithday1 = dt.Rows[j]["birthday"].ToString ();
                            int year1 = Convert.ToInt32 (brithday1.Substring (0, 4));
                            int mounth1 = Convert.ToInt32 (brithday1.Substring (5, 2));
                            if( mounth2 < mounth1 )
                            {
                                year1 = year1 + 1; //出生年月
                            }
                            if( year2 - year1 <= 33 && year2 - year1 > 30 )
                            {
                                count2 = count2 + 1;
                                arry[i++] = j;
                            }
                        }
                        break;
                        case "30岁以下":
                        for( int j = 0;j < h;j++ )
                        {
                            //出生的年月
                            string brithday1 = dt.Rows[j]["birthday"].ToString ();
                            int year1 = Convert.ToInt32 (brithday1.Substring (0, 4));
                            int mounth1 = Convert.ToInt32 (brithday1.Substring (5, 2));
                            if( mounth2 < mounth1 )
                            {
                                year1 = year1 + 1; //出生年月
                            }
                            if( year2 - year1 <= 30 )
                            {
                                count2 = count2 + 1;
                                arry[i++] = j;
                            }
                        }
                        break;
                        case "自定义":
                        if( txtBegin.Text != "" && txtEnd.Text != "" )
                        {
                            Beginstr = txtBegin.Text;
                            Endstr = txtEnd.Text;
                            if( Convert.ToInt32 (Beginstr) > Convert.ToInt32 (Endstr) )
                            {
                                Middlestr = Beginstr;
                                Beginstr = Endstr;
                                Endstr = Middlestr;
                            }
                            if( Beginstr != "" && Endstr != "" )
                            {
                                int age1 = Convert.ToInt32 (Beginstr);
                                int age2 = Convert.ToInt32 (Endstr);

                                for( int j = 0;j < h;j++ )
                                {
                                    //人员出生日期
                                    string brithday1 = dt.Rows[j]["birthday"].ToString ();
                                    int year1 = Convert.ToInt32 (brithday1.Substring (0, 4));
                                    int mounth1 = Convert.ToInt32 (brithday1.Substring (5, 2));
                                    if( mounth2 < mounth1 )
                                    {
                                        year1 = year1 + 1; //出生年月
                                    }
                                    if( ( age1 <= year2 - year1 ) && ( year2 - year1 <= age2 ) )
                                    {
                                        count2 = count2 + 1;
                                        arry[i++] = j;
                                    }
                                }

                            }
                        }
                        else
                            MessageBox.Show ("自定义年龄为空！","提示");
                        break;
                    }
                }
                catch( Exception )
                {
                    count2 = h;

                    MessageBox.Show ("您输入的年龄段有误！","提示");
                }
            }
            else
            {
                count2 = h;
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    arry[i] = i;
                }
            }
            #endregion

            if( count1 != 0 )
            {
                Count_Label.Text = count1.ToString ();
                findNumber.Text = count2.ToString ();
                Percent_Label.Text = Math.Round (Convert.ToDouble (count2) / Convert.ToDouble (count1), 3) * 100 + "％";

                listView1.CheckBoxes = true;//列表里是否显示复选框

                //人员信息
if( count2 != 0 )
{
    for( int x = 0;x < count2;x++ )
    {
        ListViewItem list = new ListViewItem ();
        list.SubItems.Add (dt.Rows[arry[x]]["name"].ToString ());
        list.SubItems.Add (dt.Rows[arry[x]]["sex"].ToString ());
        list.SubItems.Add (dt.Rows[arry[x]]["unitname"].ToString ());
        list.SubItems.Add (dt.Rows[arry[x]]["position"].ToString ());

        list.SubItems.Add(dt.Rows[arry[x]]["CID"].ToString());//CID不在表中显示，但是用户执行导出操作时要用

        listView1.Items.Add (list);
    }
}
                count2 = 0;
                count1 = 0;
            }
            else
            {
                this.Percent_Label.Text = "";
                this.findNumber.Text = "";
                this.Count_Label.Text = "";
                MessageBox.Show ("暂时没有此单位的人员信息！","提示");
            }
        }

        /// <summary>
        /// 是否显示自定义年龄对话框
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Age_ComboBox_SelectedIndexChanged( object sender, EventArgs e )
        {
            if( Age_ComboBox.SelectedIndex == 7 )
            {
                txtBegin.Visible = true;
                txtEnd.Visible = true;
                label12.Visible = true;
            }
            else
            {
                txtBegin.Text = "";
                txtEnd.Text = "";
                txtBegin.Visible = false;
                txtEnd.Visible = false;
                label12.Visible = false;
            }
        }

        /// <summary>
        /// 清空筛选条件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cancel_Click( object sender, EventArgs e )
        {
            listView1.Items.Clear ();
            this.Sex_ComboBox.SelectedIndex = -1;
            this.Grade_ComboBox.SelectedIndex = -1;
            this.FullDegree_ComboBox.SelectedIndex = -1;
            this.FullEducation_ComboBox.SelectedIndex = -1;
            this.Nation_ComboBox.SelectedIndex = -1;
            this.OnDegree_ComboBox.SelectedIndex = -1;
            this.OnEducation_ComboBox.SelectedIndex = -1;
            this.PostionClass_ComboBox.SelectedIndex = -1;
            this.Party_ComboBox.SelectedIndex = -1;
            this.Age_ComboBox.SelectedIndex = -1;

            this.Percent_Label.Text = "";
            this.findNumber.Text = "";
            this.Count_Label.Text = "";

            txtBegin.Visible = false;
            txtEnd.Visible = false;
            label12.Visible = false;

        }

        /// <summary>
        /// 全选按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AllSelect_Click( object sender, EventArgs e )
        {
            foreach( ListViewItem item in this.listView1.Items )
            {
                item.Checked = true;
            }
        }

        /// <summary>
        /// 反选按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OppositeSelect_Click( object sender, EventArgs e )
        {
            foreach( ListViewItem item in this.listView1.Items )
            {
                item.Checked = !item.Checked;
            }
        }

        /// <summary>
        /// 高级分析面板中的导出数据
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button1_Click( object sender, EventArgs e )
        {
            ArrayList idList = getSelectID ();

            if( idList.Count == 0 )
            {
                MessageBox.Show ("您未选择任何后备干部!");
                return;
            }
            AllPrint ap = new AllPrint ();
            ap.exportword (idList);
        }

        /// <summary>
        /// 得到选中的后备干部的id
        /// </summary>
        /// <returns></returns>
        public ArrayList getSelectID()
        {
            ArrayList list = new ArrayList ();
            //用foreach循环得到选中的后备干部的id
            foreach( ListViewItem listviewItem in this.listView1.CheckedItems )
            {
                string sid = listviewItem.SubItems[5].Text;
                list.Add (sid);
            }
            return list;
        }
    }
}
