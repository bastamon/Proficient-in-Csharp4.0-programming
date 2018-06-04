using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using HBMISR.GUI.MainGUI;
using HBMISR.Data;
using HBMISR.Service;

namespace HBMISR.GUI.NoteGUI
{
    /// <summary>
    /// 信息采集表
    /// </summary>
    public partial class FrmInformationGathering : Form
    {
        #region
        bool change = false;
        bool remarkchange = false;
        DataOperation dataOperation;
        ReadIni readini;
        ComboBox language;

        private bool show11 = false, show12 = false, show13 = false, show14 = false;
        private bool show21 = false, show22 = false, show23 = false, show24 = false;
        private bool show31 = false, show32 = false, show33 = false, show34 = false;
        private bool show41 = false, show42 = false, show43 = false, show44 = false;
        private bool show51 = false, show52 = false, show53 = false, show54 = false;
        /// <summary>
        /// 保存海外学习表中的id字段
        /// </summary>
        Int32[] idStudy = new Int32[3];
        /// <summary>
        /// 保存海外工作表中的id字段
        /// </summary>
        Int32[] idWork = new Int32[3];
        /// <summary>
        /// 保存重大事项信息表中的id字段
        /// </summary>
        Int32[] idMatter = new Int32[9];
        /// <summary>
        /// 保存参加培训和实践锻炼情况表中的id字段
        /// </summary>
        Int32[] idTrain = new Int32[2];
        /// <summary>
        /// 保存培养锻炼措施需求表中的id字段
        /// </summary>
        Int32[] idMethord = new Int32[14];
        /// <summary>
        /// 保存熟悉外语语种表中的id字
        /// </summary>
        Int32[] idLanguage = new Int32[5];
        SAbroad sAbroad = new SAbroad();
        WAbroad wAbroad = new WAbroad();
        IContent iContent = new IContent();
        Train train = new Train();
        TMethod tMethod = new TMethod();
        FLanguage fLanguage = new FLanguage();

        StudyAbroad study1;
        StudyAbroad study2;
        StudyAbroad study3;
        WorkAbroad work1;
        WorkAbroad work2;
        WorkAbroad work3;
        Content1 cont1;
        Content1 cont2;
        Content1 cont3;
        Content1 cont4;
        Content1 cont5;
        Content1 cont6;
        Content1 cont7;
        Content1 cont8;
        Content1 cont9;
        Exercise exercise1;
        Exercise exercise2;
        Demand demand1;
        Demand demand2;
        Demand demand3;
        Demand demand4;
        Demand demand5;
        Demand demand6;
        Demand demand7;
        Demand demand8;
        Demand demand9;
        Demand demand10;
        Demand demand11;
        Demand demand12;
        Demand demand13;
        Demand demand14;
        Language lan1;
        Language lan2;
        Language lan3;
        Language lan4;
        Language lan5;

        DateTime myTime = Convert.ToDateTime(DateTime.Now.Year + "." + DateTime.Now.Month + "." + DateTime.Now.Day);

        #endregion
        /// <summary>
        /// 记录cid
        /// </summary>
        public string listViewCid = "";
        /// <summary>
        /// 标记当前这一条培训锻炼情况信息是新增的还是修改的
        /// </summary>
        public bool isEditOfT = false;
        /// <summary>
        /// 标记当前修改的培训锻炼情况信息的ID
        /// </summary>
        public string tid = "";
        public bool isSingle = false;
        /// <summary>
        /// 存储培养措施中的选中序号
        /// </summary>
        List<int> three = new List<int>();
        public ControlMain ci;

        /// <summary>
        /// 信息采集表构造函数
        /// </summary>
        public FrmInformationGathering()
        {
            try
            {
                readini = new ReadIni();

                string filepath = readini.ReadString("filePath");
                dataOperation = new DataOperation(filepath);
                InitializeComponent();
                language = new ComboBox();
            }
            catch (Exception) { }
        }

        /// <summary>
        /// 信息采集表初始化
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void InformationGathering_Load(object sender, EventArgs e)
        {
            try
            {

                DataOperation oper = new DataOperation(Application.StartupPath + "\\DB\\DBHBMSU.db");
                string sql1 = "select distinct reportMatter from TB_TRAINEXERCISE";
                DataTable dtReportMatter = oper.GetOneDataTable_sql(sql1);

                if (dtReportMatter.Rows.Count > 0)
                {
                    for (int ii = 0; ii < dtReportMatter.Rows.Count; ii++)
                    {
                        comMatter.Items.Add(dtReportMatter.Rows[ii]["reportMatter"].ToString());
                    }
                }
                string sql = "select name, department, position from TB_Commoninfo where cid = '" + listViewCid + "'";

                DataTable dt = dataOperation.GetOneDataTable_sql(sql);
                comboBox_Name.Text = dt.Rows[0]["name"].ToString();

                positon_tB.Text = dt.Rows[0]["position"].ToString();
                department_tB.Text = dt.Rows[0]["department"].ToString();

                //设置培养措施锻炼情况
                #region
                listView_TrainExercise.View = View.Details;
                listView_TrainExercise.LabelEdit = true;
                listView_TrainExercise.GridLines = true;
                listView_TrainExercise.AllowColumnReorder = true;
                listView_TrainExercise.LabelEdit = false;
                listView_TrainExercise.HideSelection = false;
                listView_TrainExercise.Columns.Add("", 0, HorizontalAlignment.Center);
                listView_TrainExercise.Columns.Add("报告事项", 120, HorizontalAlignment.Center);
                listView_TrainExercise.Columns.Add("起始时间", 120, HorizontalAlignment.Center);
                listView_TrainExercise.Columns.Add("结束时间", 120, HorizontalAlignment.Center);
                listView_TrainExercise.Columns.Add("培训形式", 153, HorizontalAlignment.Center);
                listView_TrainExercise.Columns.Add("内容", 300, HorizontalAlignment.Left);
                #endregion

                LoadInfo(listViewCid);

                change = true;
            }
            catch (Exception) { }
        }

        /// <summary>
        /// 显示信息
        /// </summary>
        /// <param name="cid"></param>
        public void LoadInfo(string cid)
        {
            #region//创建临时存储的对象
            study1 = new StudyAbroad();
            study2 = new StudyAbroad();
            study3 = new StudyAbroad();
            work1 = new WorkAbroad();
            work2 = new WorkAbroad();
            work3 = new WorkAbroad();
            cont1 = new Content1();
            cont2 = new Content1();
            cont3 = new Content1();
            cont4 = new Content1();
            cont5 = new Content1();
            cont6 = new Content1();
            cont7 = new Content1();
            cont8 = new Content1();
            cont9 = new Content1();
            exercise1 = new Exercise();
            exercise2 = new Exercise();
            demand1 = new Demand();
            demand2 = new Demand();
            demand3 = new Demand();
            demand4 = new Demand();
            demand5 = new Demand();
            demand6 = new Demand();
            demand7 = new Demand();
            demand8 = new Demand();
            demand9 = new Demand();
            demand10 = new Demand();
            demand11 = new Demand();
            demand12 = new Demand();
            demand13 = new Demand();
            demand14 = new Demand();
            lan1 = new Language();
            lan2 = new Language();
            lan3 = new Language();
            lan4 = new Language();
            lan5 = new Language();
            #endregion

            try
            {
                clearWindow();
                string sql1 = string.Empty;

                #region//海外学习
                sql1 = "select id,startTime,endTime,country,academy,degree from TB_SAbroad where cid in('" + cid + "')";
                DataTable sAbroadTB = dataOperation.GetOneDataTable_sql(sql1);

                if (sAbroadTB.Rows.Count > 0)
                {
                    for (int i = 0; i < sAbroadTB.Rows.Count; i++)
                    {
                        if (i == 0)
                        {
                            idStudy[0] = Convert.ToInt32(sAbroadTB.Rows[0]["id"]);
                            beginStudy1.Text = sAbroadTB.Rows[0]["startTime"].ToString();
                            endStudy1.Text = sAbroadTB.Rows[0]["endTime"].ToString();
                            studyCountry1.Text = sAbroadTB.Rows[0]["country"].ToString();
                            schoolMajor1.Text = sAbroadTB.Rows[0]["academy"].ToString();
                            degree1.Text = sAbroadTB.Rows[0]["degree"].ToString();

                            study1.StartTime = sAbroadTB.Rows[0]["startTime"].ToString();
                            study1.EndTime = sAbroadTB.Rows[0]["endTime"].ToString();

                        }
                        if (i == 1)
                        {
                            idStudy[1] = Convert.ToInt32(sAbroadTB.Rows[1]["id"]);
                            beginStudy2.Text = sAbroadTB.Rows[1]["startTime"].ToString();
                            endStudy2.Text = sAbroadTB.Rows[1]["endTime"].ToString();
                            studyCountry2.Text = sAbroadTB.Rows[1]["country"].ToString();
                            schoolMajor2.Text = sAbroadTB.Rows[1]["academy"].ToString();
                            degree2.Text = sAbroadTB.Rows[1]["degree"].ToString();

                            study2.StartTime = sAbroadTB.Rows[1]["startTime"].ToString();
                            study2.EndTime = sAbroadTB.Rows[1]["endTime"].ToString();




                        }
                        if (i == 2)
                        {
                            idStudy[2] = Convert.ToInt32(sAbroadTB.Rows[2]["id"]);
                            beginStudy3.Text = sAbroadTB.Rows[2]["startTime"].ToString();
                            endStudy3.Text = sAbroadTB.Rows[2]["endTime"].ToString();
                            studyCountry3.Text = sAbroadTB.Rows[2]["country"].ToString();
                            schoolMajor3.Text = sAbroadTB.Rows[2]["academy"].ToString();
                            degree3.Text = sAbroadTB.Rows[2]["degree"].ToString();
                            study3.StartTime = sAbroadTB.Rows[2]["startTime"].ToString();
                            study3.EndTime = sAbroadTB.Rows[0]["endTime"].ToString();

                        }

                    }


                }


                #endregion

                #region//海外工作
                sql1 = "select id, startTime,endTime,abroadCountry,departmentPosition,specialtyArea from TB_WAbroad where cid='" + cid + "'";
                DataTable wAbroadTB = dataOperation.GetOneDataTable_sql(sql1);
                if (wAbroadTB.Rows.Count > 0)
                {
                    for (int i = 0; i < wAbroadTB.Rows.Count; i++)
                    {
                        if (i == 0)
                        {
                            idWork[0] = Convert.ToInt32(wAbroadTB.Rows[0]["id"]);
                            beginWork1.Text = wAbroadTB.Rows[0]["startTime"].ToString();
                            endWork1.Text = wAbroadTB.Rows[0]["endTime"].ToString();
                            workCountry1.Text = wAbroadTB.Rows[0]["abroadCountry"].ToString();
                            workDept1.Text = wAbroadTB.Rows[0]["departmentPosition"].ToString();
                            workArea1.Text = wAbroadTB.Rows[0]["specialtyArea"].ToString();
                            work1.StartTime = wAbroadTB.Rows[0]["startTime"].ToString();
                            work1.EndTime = wAbroadTB.Rows[0]["endTime"].ToString();

                        }
                        if (i == 1)
                        {
                            idWork[1] = Convert.ToInt32(wAbroadTB.Rows[1]["id"]);
                            beginWork2.Text = wAbroadTB.Rows[1]["startTime"].ToString();
                            endWork2.Text = wAbroadTB.Rows[1]["endTime"].ToString();
                            workCountry2.Text = wAbroadTB.Rows[1]["abroadCountry"].ToString();
                            workDept2.Text = wAbroadTB.Rows[1]["departmentPosition"].ToString();
                            workArea2.Text = wAbroadTB.Rows[1]["specialtyArea"].ToString();
                            work2.StartTime = wAbroadTB.Rows[1]["startTime"].ToString();
                            work2.EndTime = wAbroadTB.Rows[1]["endTime"].ToString();
                        }
                        if (i == 2)
                        {
                            idWork[2] = Convert.ToInt32(wAbroadTB.Rows[2]["id"]);
                            beginWork3.Text = wAbroadTB.Rows[2]["startTime"].ToString();
                            endWork3.Text = wAbroadTB.Rows[2]["endTime"].ToString();
                            workCountry3.Text = wAbroadTB.Rows[2]["abroadCountry"].ToString();
                            workDept3.Text = wAbroadTB.Rows[2]["departmentPosition"].ToString();
                            workArea3.Text = wAbroadTB.Rows[2]["specialtyArea"].ToString();
                            work3.StartTime = wAbroadTB.Rows[2]["startTime"].ToString();
                            work3.EndTime = wAbroadTB.Rows[2]["endTime"].ToString();
                        }

                    }

                }


                #endregion

                #region //重大事项信息
                sql1 = "select id, content,matter from TB_GreatContent where cid='" + cid + "'";
                DataTable greatContentTB = dataOperation.GetOneDataTable_sql(sql1);
                if (greatContentTB.Rows.Count > 0)
                {
                    for (int i = 0; i < greatContentTB.Rows.Count; i++)
                    {
                        if (Convert.ToInt32(greatContentTB.Rows[i]["matter"].ToString().Trim()) == 1)
                        {
                            content1.Text = greatContentTB.Rows[i]["content"].ToString();
                            idMatter[0] = Convert.ToInt32(greatContentTB.Rows[i]["id"]);
                            cont1.Content = content1.Text;

                        }
                        if (Convert.ToInt32(greatContentTB.Rows[i]["matter"].ToString().Trim()) == 2)
                        {
                            content2.Text = greatContentTB.Rows[i]["content"].ToString();
                            idMatter[1] = Convert.ToInt32(greatContentTB.Rows[i]["id"]);
                            cont2.Content = content2.Text;
                        }
                        if (Convert.ToInt32(greatContentTB.Rows[i]["matter"].ToString().Trim()) == 3)
                        {
                            content3.Text = greatContentTB.Rows[i]["content"].ToString();
                            idMatter[2] = Convert.ToInt32(greatContentTB.Rows[i]["id"]);
                            cont3.Content = content3.Text;
                        }
                        if (Convert.ToInt32(greatContentTB.Rows[i]["matter"].ToString().Trim()) == 4)
                        {
                            content4.Text = greatContentTB.Rows[i]["content"].ToString();
                            idMatter[3] = Convert.ToInt32(greatContentTB.Rows[i]["id"]);
                            cont4.Content = content4.Text;
                        }
                        if (Convert.ToInt32(greatContentTB.Rows[i]["matter"].ToString().Trim()) == 5)
                        {
                            content5.Text = greatContentTB.Rows[i]["content"].ToString();
                            idMatter[4] = Convert.ToInt32(greatContentTB.Rows[i]["id"]);
                            cont5.Content = content5.Text;
                        }
                        if (Convert.ToInt32(greatContentTB.Rows[i]["matter"].ToString().Trim()) == 6)
                        {
                            content6.Text = greatContentTB.Rows[i]["content"].ToString();
                            idMatter[5] = Convert.ToInt32(greatContentTB.Rows[i]["id"]);
                            cont6.Content = content6.Text;
                        }
                        if (Convert.ToInt32(greatContentTB.Rows[i]["matter"].ToString().Trim()) == 7)
                        {
                            content7.Text = greatContentTB.Rows[i]["content"].ToString();
                            idMatter[6] = Convert.ToInt32(greatContentTB.Rows[i]["id"]);
                            cont7.Content = content7.Text;
                        }
                        if (Convert.ToInt32(greatContentTB.Rows[i]["matter"].ToString().Trim()) == 8)
                        {
                            content8.Text = greatContentTB.Rows[i]["content"].ToString();
                            idMatter[7] = Convert.ToInt32(greatContentTB.Rows[i]["id"]);
                            cont8.Content = content8.Text;
                        }
                        if (Convert.ToInt32(greatContentTB.Rows[i]["matter"].ToString().Trim()) == 9)
                        {
                            content9.Text = greatContentTB.Rows[i]["content"].ToString();
                            idMatter[8] = Convert.ToInt32(greatContentTB.Rows[i]["id"]);
                            cont9.Content = content9.Text;
                        }

                    }
                }
                #endregion

                //培养措施需求
                #region
                sql1 = "select id, options,note14 from TB_TrainMethord where cid='" + cid + "'";
                DataTable trainMethodTB = dataOperation.GetOneDataTable_sql(sql1);
                if (trainMethodTB.Rows.Count > 0)
                {
                    for (int i = 0; i < trainMethodTB.Rows.Count; i++)
                    {
                        switch (Convert.ToInt32(trainMethodTB.Rows[i]["options"]))
                        {

                            case 1:
                                explain1.Checked = true;
                                idMethord[0] = Convert.ToInt32(trainMethodTB.Rows[i]["id"].ToString().Trim());
                                demand1.Explain = true;
                                break;

                            case 2:
                                explain2.Checked = true;
                                idMethord[1] = Convert.ToInt32(trainMethodTB.Rows[i]["id"]);
                                demand2.Explain = true;
                                break;
                            case 3:

                                explain3.Checked = true;
                                idMethord[2] = Convert.ToInt32(trainMethodTB.Rows[i]["id"]);
                                demand3.Explain = true;
                                break;

                            case 4:
                                explain4.Checked = true;
                                idMethord[3] = Convert.ToInt32(trainMethodTB.Rows[i]["id"]);
                                demand4.Explain = true;
                                break;

                            case 5:
                                explain5.Checked = true;
                                idMethord[4] = Convert.ToInt32(trainMethodTB.Rows[i]["id"]);
                                demand5.Explain = true;
                                break;

                            case 6:
                                explain6.Checked = true;
                                idMethord[5] = Convert.ToInt32(trainMethodTB.Rows[i]["id"]);
                                demand6.Explain = true;
                                break;

                            case 7:
                                explain7.Checked = true;
                                idMethord[6] = Convert.ToInt32(trainMethodTB.Rows[i]["id"]);
                                demand7.Explain = true;
                                break;

                            case 8:
                                explain8.Checked = true;
                                idMethord[7] = Convert.ToInt32(trainMethodTB.Rows[i]["id"]);
                                demand8.Explain = true;
                                break;

                            case 9:
                                explain9.Checked = true;
                                idMethord[8] = Convert.ToInt32(trainMethodTB.Rows[i]["id"]);
                                demand9.Explain = true;
                                break;

                            case 10:
                                explain10.Checked = true;
                                idMethord[9] = Convert.ToInt32(trainMethodTB.Rows[i]["id"]);
                                demand10.Explain = true;
                                break;

                            case 11:
                                explain11.Checked = true;
                                idMethord[10] = Convert.ToInt32(trainMethodTB.Rows[i]["id"]);
                                demand11.Explain = true;
                                break;

                            case 12:
                                explain12.Checked = true;
                                idMethord[11] = Convert.ToInt32(trainMethodTB.Rows[i]["id"]);
                                demand12.Explain = true;
                                break;

                            case 13:

                                explain13.Checked = true;
                                idMethord[12] = Convert.ToInt32(trainMethodTB.Rows[i]["id"]);
                                demand13.Explain = true;
                                break;

                            case 14:
                                explain14.Checked = true;
                                note14.ReadOnly = false;
                                note14.Text = trainMethodTB.Rows[i]["note14"].ToString();
                                idMethord[13] = Convert.ToInt32(trainMethodTB.Rows[i]["id"]);
                                demand14.Note14 = trainMethodTB.Rows[i]["note14"].ToString();
                                demand14.Explain = true;
                                break;
                        }
                    }
                }
                #endregion

                #region//熟悉外语语种信息
                sql1 = "select id, foreignKind,level from TB_FamiliarForeign where cid='" + cid + "'";
                DataTable familiarForeignTB = dataOperation.GetOneDataTable_sql(sql1);
                if (familiarForeignTB.Rows.Count > 0)
                {
                    for (int i = 0; i < familiarForeignTB.Rows.Count; i++)
                    {
                        switch (i)
                        {

                            case 0:
                                foreignKind1.Text = familiarForeignTB.Rows[i]["foreignKind"].ToString();
                                lan1.ForeignKind = foreignKind1.Text;
                                if (familiarForeignTB.Rows[i]["level"].ToString().Equals("精通"))
                                {
                                    show11 = true;
                                    this.level11.BackgroundImage = global::HBMISR.Properties.Resources.choice;
                                    lan1.Show11 = true;

                                }
                                if (familiarForeignTB.Rows[i]["level"].ToString().Equals("熟练"))
                                {
                                    show12 = true;
                                    this.level12.BackgroundImage = global::HBMISR.Properties.Resources.choice;
                                    lan1.Show12 = true;
                                }
                                if (familiarForeignTB.Rows[i]["level"].ToString().Equals("良好"))
                                {
                                    show13 = true;
                                    this.level13.BackgroundImage = global::HBMISR.Properties.Resources.choice;
                                    lan1.Show13 = true;
                                }
                                if (familiarForeignTB.Rows[i]["level"].ToString().Equals("一般"))
                                {
                                    show14 = true;
                                    this.level14.BackgroundImage = global::HBMISR.Properties.Resources.choice;
                                    lan1.Show14 = true;
                                }
                                if (familiarForeignTB.Rows[i]["id"] != null)
                                    idLanguage[0] = Convert.ToInt32(familiarForeignTB.Rows[i]["id"]);
                                break;

                            case 1:
                                foreignKind2.Text = familiarForeignTB.Rows[i]["foreignKind"].ToString();
                                lan2.ForeignKind = foreignKind2.Text;
                                if (familiarForeignTB.Rows[i]["level"].ToString().Equals("精通"))
                                {
                                    show21 = true;
                                    this.level21.BackgroundImage = global::HBMISR.Properties.Resources.choice;
                                    lan2.Show21 = true;
                                }
                                if (familiarForeignTB.Rows[i]["level"].ToString().Equals("熟练"))
                                {
                                    show22 = true;
                                    this.level22.BackgroundImage = global::HBMISR.Properties.Resources.choice;
                                    lan2.Show22 = true;
                                }
                                if (familiarForeignTB.Rows[i]["level"].ToString().Equals("良好"))
                                {
                                    show23 = true;
                                    this.level23.BackgroundImage = global::HBMISR.Properties.Resources.choice;
                                    lan2.Show23 = true;
                                }
                                if (familiarForeignTB.Rows[i]["level"].ToString().Equals("一般"))
                                {
                                    show24 = true;
                                    this.level24.BackgroundImage = global::HBMISR.Properties.Resources.choice;
                                    lan2.Show24 = true;
                                }
                                if (familiarForeignTB.Rows[i]["id"] != null)
                                    idLanguage[1] = Convert.ToInt32(familiarForeignTB.Rows[i]["id"]);
                                break;

                            case 2:
                                foreignKind3.Text = familiarForeignTB.Rows[i]["foreignKind"].ToString();
                                lan3.ForeignKind = foreignKind3.Text;
                                if (familiarForeignTB.Rows[i]["level"].ToString().Equals("精通"))
                                {
                                    show31 = true;
                                    this.level31.BackgroundImage = global::HBMISR.Properties.Resources.choice;
                                    lan3.Show31 = true;
                                }
                                if (familiarForeignTB.Rows[i]["level"].ToString().Equals("熟练"))
                                {
                                    show32 = true;
                                    this.level32.BackgroundImage = global::HBMISR.Properties.Resources.choice;
                                    lan3.Show32 = true;
                                }
                                if (familiarForeignTB.Rows[i]["level"].ToString().Equals("良好"))
                                {
                                    show33 = true;
                                    this.level33.BackgroundImage = global::HBMISR.Properties.Resources.choice;
                                    lan3.Show33 = true;
                                }
                                if (familiarForeignTB.Rows[i]["level"].ToString().Equals("一般"))
                                {
                                    show34 = true;
                                    this.level34.BackgroundImage = global::HBMISR.Properties.Resources.choice;
                                    lan3.Show34 = true;
                                }
                                if (familiarForeignTB.Rows[i]["id"] != null)
                                    idLanguage[2] = Convert.ToInt32(familiarForeignTB.Rows[i]["id"]);
                                break;

                            case 3:
                                foreignKind4.Text = familiarForeignTB.Rows[i]["foreignKind"].ToString();
                                lan4.ForeignKind = foreignKind4.Text;
                                if (familiarForeignTB.Rows[i]["level"].ToString().Equals("精通"))
                                {
                                    show41 = true;
                                    this.level41.BackgroundImage = global::HBMISR.Properties.Resources.choice;
                                    lan4.Show41 = true;
                                }
                                if (familiarForeignTB.Rows[i]["level"].ToString().Equals("熟练"))
                                {
                                    show42 = true;
                                    this.level42.BackgroundImage = global::HBMISR.Properties.Resources.choice;
                                    lan4.Show42 = true;
                                }
                                if (familiarForeignTB.Rows[i]["level"].ToString().Equals("良好"))
                                {
                                    show43 = true;
                                    this.level43.BackgroundImage = global::HBMISR.Properties.Resources.choice;
                                    lan4.Show43 = true;
                                }
                                if (familiarForeignTB.Rows[i]["level"].ToString().Equals("一般"))
                                {
                                    show44 = true;
                                    this.level44.BackgroundImage = global::HBMISR.Properties.Resources.choice;
                                    lan4.Show44 = true;
                                }
                                if (familiarForeignTB.Rows[i]["id"] != null)
                                    idLanguage[3] = Convert.ToInt32(familiarForeignTB.Rows[i]["id"]);
                                break;

                            case 4:
                                foreignKind5.Text = familiarForeignTB.Rows[i]["foreignKind"].ToString();
                                lan5.ForeignKind = foreignKind5.Text;
                                if (familiarForeignTB.Rows[i]["level"].ToString().Equals("精通"))
                                {
                                    show51 = true;
                                    this.level51.BackgroundImage = global::HBMISR.Properties.Resources.choice;
                                    lan5.Show51 = true;
                                }
                                if (familiarForeignTB.Rows[i]["level"].ToString().Equals("熟练"))
                                {
                                    show52 = true;
                                    this.level52.BackgroundImage = global::HBMISR.Properties.Resources.choice;
                                    lan5.Show52 = true;
                                }
                                if (familiarForeignTB.Rows[i]["level"].ToString().Equals("良好"))
                                {
                                    show53 = true;
                                    this.level53.BackgroundImage = global::HBMISR.Properties.Resources.choice;
                                    lan5.Show53 = true;
                                }
                                if (familiarForeignTB.Rows[i]["level"].ToString().Equals("一般"))
                                {
                                    show54 = true;
                                    this.level54.BackgroundImage = global::HBMISR.Properties.Resources.choice;
                                    lan5.Show54 = true;
                                }
                                if (familiarForeignTB.Rows[i]["id"] != null)
                                    idLanguage[4] = Convert.ToInt32(familiarForeignTB.Rows[i]["id"]);
                                break;
                        }
                    }
                }
                #endregion

                //培养措施需求
                #region
                string sql = "select * from TB_TrainExercise where cid = '" + cid + "'";
                DataTable TB_TrainExercise = dataOperation.GetOneDataTable_sql(sql);
                for (int i = 0; i < TB_TrainExercise.Rows.Count; i++)
                {
                    ListViewItem item = new ListViewItem();
                    item.Tag = TB_TrainExercise.Rows[i]["id"].ToString();
                    item.SubItems.Add(TB_TrainExercise.Rows[i]["reportMatter"].ToString());
                    item.SubItems.Add(TB_TrainExercise.Rows[i]["starttime"].ToString());
                    item.SubItems.Add(TB_TrainExercise.Rows[i]["endtime"].ToString());
                    item.SubItems.Add(TB_TrainExercise.Rows[i]["reportcontent"].ToString());
                    item.SubItems.Add(TB_TrainExercise.Rows[i]["content"].ToString());
                    listView_TrainExercise.Items.Add(item);
                }
                #endregion
            }
            catch (Exception)
            {
                MessageBox.Show("加载信息时出错！");
            }
        }

        /// <summary>
        /// 保存信息
        /// </summary>
        /// <param name="cid"></param>
        public void SaveInfo(string cid)
        {
            try
            {
                DataTable dt;
                if (IsSaveLanguage() == 1)
                {
                    #region//插入海外学习信息
                    Insert_Serve server = new Insert_Serve();
                    sAbroad.Id = idStudy[0];
                    if (studyCountry1.Text.Equals(""))
                    {
                        server.Delete(sAbroad);
                    }
                    else
                    {
                        sAbroad.Cid = cid;
                        sAbroad.StartTime = beginStudy1.Text;
                        sAbroad.EndTime = endStudy1.Text;
                        sAbroad.Country = studyCountry1.Text;
                        sAbroad.Academy = schoolMajor1.Text;
                        sAbroad.Degree = degree1.Text;
                        sAbroad.PreviousStart = study1.StartTime;
                        sAbroad.PreviousEnd = study1.EndTime;
                        study1.StartTime = sAbroad.StartTime;
                        study1.EndTime = sAbroad.EndTime;
                        study1.Country = studyCountry1.Text;
                        study1.Academy = schoolMajor1.Text;
                        study1.Degree = degree1.Text;
                        server.Insert(sAbroad);
                        dt = dataOperation.GetOneDataTable_sql("select * from TB_SAbroad where cid='" + sAbroad.Cid + "' and country='" + sAbroad.Country + "' and academy='" + sAbroad.Academy + "' and degree='" + sAbroad.Degree + "' and startTime='" + sAbroad.StartTime + "' and endTime='" + sAbroad.EndTime + "'");
                        idStudy[0] = Convert.ToInt32(dt.Rows[0]["id"]);

                    }
                    sAbroad.Id = idStudy[1];


                    if (studyCountry2.Text.Equals(""))
                    {
                        server.Delete(sAbroad);
                    }
                    else
                    {

                        sAbroad.Cid = cid;
                        sAbroad.StartTime = beginStudy2.Text;
                        sAbroad.EndTime = endStudy2.Text;
                        sAbroad.Country = studyCountry2.Text;
                        sAbroad.Academy = schoolMajor2.Text;
                        sAbroad.Degree = degree2.Text;
                        sAbroad.PreviousStart = study2.StartTime;
                        sAbroad.PreviousEnd = study2.EndTime;
                        study2.StartTime = sAbroad.StartTime;
                        study2.EndTime = sAbroad.EndTime;
                        study2.Country = studyCountry2.Text;
                        study2.Academy = schoolMajor2.Text;
                        study2.Degree = degree2.Text;

                        server.Insert(sAbroad);
                        dt = dataOperation.GetOneDataTable_sql("select * from TB_SAbroad where cid='" + sAbroad.Cid + "' and country='" + sAbroad.Country + "' and academy='" + sAbroad.Academy + "' and degree='" + sAbroad.Degree + "' and startTime='" + sAbroad.StartTime + "' and endTime='" + sAbroad.EndTime + "'");
                        idStudy[1] = Convert.ToInt32(dt.Rows[0]["id"]);

                    }
                    sAbroad.Id = idStudy[2];
                    if (studyCountry3.Text.Equals(""))
                    {
                        server.Delete(sAbroad);
                    }
                    else
                    {

                        sAbroad.Cid = cid;
                        sAbroad.StartTime = beginStudy3.Text;
                        sAbroad.EndTime = endStudy3.Text;
                        sAbroad.Country = studyCountry3.Text;
                        sAbroad.Academy = schoolMajor3.Text;
                        sAbroad.Degree = degree3.Text;
                        sAbroad.PreviousStart = study3.StartTime;
                        sAbroad.PreviousEnd = study3.EndTime;
                        study3.StartTime = sAbroad.StartTime;
                        study3.EndTime = sAbroad.EndTime;
                        study3.Country = studyCountry3.Text;
                        study3.Academy = schoolMajor3.Text;
                        study3.Degree = degree3.Text;
                        server.Insert(sAbroad);
                        dt = dataOperation.GetOneDataTable_sql("select * from TB_SAbroad where cid='" + sAbroad.Cid + "' and country='" + sAbroad.Country + "' and academy='" + sAbroad.Academy + "' and degree='" + sAbroad.Degree + "' and startTime='" + sAbroad.StartTime + "' and endTime='" + sAbroad.EndTime + "'");
                        idStudy[2] = Convert.ToInt32(dt.Rows[0]["id"]);
                    }



                    #endregion

                    #region//插入海外工作信息

                    wAbroad.Id = idWork[0];
                    if (workCountry1.Text.Equals(""))
                    {
                        server.Delete(wAbroad);
                    }
                    else
                    {

                        wAbroad.Cid = cid;
                        wAbroad.StartTime = beginWork1.Text;
                        wAbroad.EndTime = endWork1.Text;
                        wAbroad.Country = workCountry1.Text;
                        wAbroad.DepartmentPosition = workDept1.Text;
                        wAbroad.Specialty = workArea1.Text;
                        wAbroad.PreviousStart = work1.StartTime;
                        wAbroad.PreviousEnd = work1.EndTime;
                        work1.StartTime = wAbroad.StartTime;
                        work1.EndTime = wAbroad.EndTime;
                        work1.Country = workCountry1.Text;
                        work1.DepartmentPosition = workDept1.Text;
                        work1.Specialty = workArea1.Text;
                        server.Insert(wAbroad);
                        dt = dataOperation.GetOneDataTable_sql("select * from TB_WAbroad where cid='" + wAbroad.Cid + "'and startTime='" + wAbroad.StartTime + "'and endTime='" + wAbroad.EndTime + "'and abroadCountry='" + wAbroad.Country + "'and departmentPosition='" + wAbroad.DepartmentPosition + "' and specialtyArea='" + wAbroad.Specialty + "'");
                        idWork[0] = Convert.ToInt32(dt.Rows[0]["id"]);

                    }
                    wAbroad.Id = idWork[1];
                    if (workCountry2.Text.Equals(""))
                    {
                        server.Delete(wAbroad);
                    }
                    else
                    {

                        wAbroad.Cid = cid;
                        wAbroad.StartTime = beginWork2.Text;
                        wAbroad.EndTime = endWork2.Text;
                        wAbroad.Country = workCountry2.Text;
                        wAbroad.DepartmentPosition = workDept2.Text;
                        wAbroad.Specialty = workArea2.Text;
                        wAbroad.PreviousStart = work2.StartTime;
                        wAbroad.PreviousEnd = work2.EndTime;
                        work2.StartTime = wAbroad.StartTime;
                        work2.EndTime = wAbroad.EndTime;
                        work2.Country = workCountry2.Text;
                        work2.DepartmentPosition = workDept2.Text;
                        work2.Specialty = workArea2.Text;
                        server.Insert(wAbroad);
                        dt = dataOperation.GetOneDataTable_sql("select * from TB_WAbroad where cid='" + wAbroad.Cid + "'and startTime='" + wAbroad.StartTime + "'and endTime='" + wAbroad.EndTime + "'and abroadCountry='" + wAbroad.Country + "'and departmentPosition='" + wAbroad.DepartmentPosition + "' and specialtyArea='" + wAbroad.Specialty + "'");
                        idWork[1] = Convert.ToInt32(dt.Rows[0]["id"]);

                    }
                    wAbroad.Id = idWork[2];
                    if (workCountry3.Text.Equals(""))
                    {
                        server.Delete(wAbroad);
                    }
                    else
                    {

                        wAbroad.Cid = cid;
                        wAbroad.StartTime = beginWork3.Text;
                        wAbroad.EndTime = endWork3.Text;
                        wAbroad.Country = workCountry3.Text;
                        wAbroad.DepartmentPosition = workDept3.Text;
                        wAbroad.Specialty = workArea3.Text;
                        wAbroad.PreviousStart = work3.StartTime;
                        wAbroad.PreviousEnd = work3.EndTime;
                        work3.StartTime = wAbroad.StartTime;
                        work3.EndTime = wAbroad.EndTime;
                        work3.Country = workCountry3.Text;
                        work3.DepartmentPosition = workDept3.Text;
                        work3.Specialty = workArea3.Text;
                        server.Insert(wAbroad);
                        dt = dataOperation.GetOneDataTable_sql("select * from TB_WAbroad where cid='" + wAbroad.Cid + "'and startTime='" + wAbroad.StartTime + "'and endTime='" + wAbroad.EndTime + "'and abroadCountry='" + wAbroad.Country + "'and departmentPosition='" + wAbroad.DepartmentPosition + "' and specialtyArea='" + wAbroad.Specialty + "'");
                        idWork[2] = Convert.ToInt32(dt.Rows[0]["id"]);

                    }
                    #endregion

                    #region//插入重大事项信息
                    iContent.Cid = cid;

                    if (!(cont1.Content == content1.Text))
                    {
                        if (content1.Text.Equals(""))
                        {
                            iContent.Id = idMatter[0];
                            server.Delete(iContent);

                        }
                        else
                        {

                            iContent.Id = idMatter[0];
                            iContent.Matter = 1;
                            iContent.Content = content1.Text;
                            server.Insert(iContent);

                            cont1.Content = iContent.Content;
                            dt = dataOperation.GetOneDataTable_sql("select * from TB_GreatContent where cid='" + cid + "' and matter='" + 1 + "'");
                            idMatter[0] = Convert.ToInt32(dt.Rows[0]["id"]);

                        }
                    }

                    if (!(cont2.Content == content2.Text))
                    {
                        if (content2.Text.Equals(""))
                        {
                            iContent.Id = idMatter[1];
                            server.Delete(iContent);
                        }
                        else
                        {

                            iContent.Id = idMatter[1];
                            iContent.Matter = 2;
                            iContent.Content = content2.Text;
                            server.Insert(iContent);


                            cont2.Content = iContent.Content;
                            dt = dataOperation.GetOneDataTable_sql("select * from TB_GreatContent where cid='" + cid + "' and matter='" + 2 + "'");
                            idMatter[1] = Convert.ToInt32(dt.Rows[0]["id"]);

                        }
                    }

                    if (!(cont3.Content == content3.Text || cont3.Content == null))
                    {
                        if (content3.Text.Equals(""))
                        {
                            iContent.Id = idMatter[2];
                            server.Delete(iContent);
                        }
                        else
                        {

                            iContent.Id = idMatter[2];
                            iContent.Matter = 3;
                            iContent.Content = content3.Text;
                            server.Insert(iContent);

                            cont3.Content = iContent.Content;
                            dt = dataOperation.GetOneDataTable_sql("select * from TB_GreatContent where cid='" + cid + "' and matter='" + 3 + "'");
                            idMatter[2] = Convert.ToInt32(dt.Rows[0]["id"]);

                        }
                    }

                    if (!(cont4.Content == content4.Text || cont4.Content == null))
                    {
                        if (content4.Text.Equals(""))
                        {
                            iContent.Id = idMatter[3];
                            server.Delete(iContent);
                        }
                        else
                        {

                            iContent.Id = idMatter[3];
                            iContent.Matter = 4;
                            iContent.Content = content4.Text;
                            server.Insert(iContent);

                            cont4.Content = iContent.Content;
                            dt = dataOperation.GetOneDataTable_sql("select * from TB_GreatContent where cid='" + cid + "' and matter='" + 4 + "'");
                            idMatter[3] = Convert.ToInt32(dt.Rows[0]["id"]);

                        }
                    }

                    if (!(cont5.Content == content5.Text || cont5.Content == null))
                    {
                        if (content5.Text.Equals(""))
                        {
                            iContent.Id = idMatter[4];
                            server.Delete(iContent);
                        }
                        else
                        {

                            iContent.Id = idMatter[4];
                            iContent.Matter = 5;
                            iContent.Content = content5.Text;
                            server.Insert(iContent);

                            cont5.Content = iContent.Content;
                            dt = dataOperation.GetOneDataTable_sql("select * from TB_GreatContent where cid='" + cid + "' and matter='" + 5 + "'");
                            idMatter[4] = Convert.ToInt32(dt.Rows[0]["id"]);

                        }
                    }

                    if (!(cont6.Content == content6.Text || cont6.Content == null))
                    {
                        if (content6.Text.Equals(""))
                        {
                            iContent.Id = idMatter[5];
                            server.Delete(iContent);
                        }
                        else
                        {

                            iContent.Id = idMatter[5];
                            iContent.Matter = 6;
                            iContent.Content = content6.Text;
                            server.Insert(iContent);

                            cont6.Content = iContent.Content;
                            dt = dataOperation.GetOneDataTable_sql("select * from TB_GreatContent where cid='" + cid + "' and matter='" + 6 + "'");
                            idMatter[5] = Convert.ToInt32(dt.Rows[0]["id"]);

                        }

                    }

                    if (!(cont7.Content == content7.Text || cont7.Content == null))
                    {

                        if (content7.Text.Equals(""))
                        {
                            iContent.Id = idMatter[6];
                            server.Delete(iContent);
                        }
                        else
                        {

                            iContent.Id = idMatter[6];
                            iContent.Matter = 7;
                            iContent.Content = content7.Text;
                            server.Insert(iContent);

                            cont7.Content = iContent.Content;
                            dt = dataOperation.GetOneDataTable_sql("select * from TB_GreatContent where cid='" + cid + "' and matter='" + 7 + "'");
                            idMatter[6] = Convert.ToInt32(dt.Rows[0]["id"]);

                        }
                    }

                    if (!(cont8.Content == content8.Text || cont8.Content == null))
                    {

                        if (content8.Text.Equals(""))
                        {
                            iContent.Id = idMatter[7];
                            server.Delete(iContent);
                        }
                        else
                        {

                            iContent.Id = idMatter[7];
                            iContent.Matter = 8;
                            iContent.Content = content8.Text;
                            server.Insert(iContent);

                            cont8.Content = iContent.Content;
                            dt = dataOperation.GetOneDataTable_sql("select * from TB_GreatContent where cid='" + cid + "' and matter='" + 8 + "'");
                            idMatter[7] = Convert.ToInt32(dt.Rows[0]["id"]);

                        }
                    }

                    if (!(cont9.Content == content9.Text || cont9.Content == null))
                    {
                        if (content9.Text.Equals(""))
                        {
                            iContent.Id = idMatter[8];
                            server.Delete(iContent);
                        }
                        else
                        {

                            iContent.Id = idMatter[8];
                            iContent.Matter = 9;
                            iContent.Content = content9.Text;
                            server.Insert(iContent);

                            cont9.Content = iContent.Content;
                            dt = dataOperation.GetOneDataTable_sql("select * from TB_GreatContent where cid='" + cid + "' and matter='" + 9 + "'");
                            idMatter[8] = Convert.ToInt32(dt.Rows[0]["id"]);
                        }
                    }


                    #endregion

                    #region//培养锻炼措施需求
                    tMethod.Cid = cid;
                    if (demand1.Explain != explain1.Checked)
                    {
                        if (explain1.Checked == true)
                        {
                            tMethod.Options = 1;
                            tMethod.Id = idMethord[0];
                            server.Insert(tMethod);
                            demand1.Explain = true;
                            dt = dataOperation.GetOneDataTable_sql("select * from TB_TrainMethord where cid='" + cid + "' and options='" + 1 + "'");
                            idMethord[0] = Convert.ToInt32(dt.Rows[0]["id"]);

                        }
                        else
                        {
                            server.DelTMethord(idMethord[0]);
                            demand1.Explain = false;
                        }
                    }



                    if (demand2.Explain != explain2.Checked)
                    {
                        if (explain2.Checked == true)
                        {
                            tMethod.Options = 2;
                            tMethod.Id = idMethord[1];
                            server.Insert(tMethod);
                            demand2.Explain = true;
                            dt = dataOperation.GetOneDataTable_sql("select * from TB_TrainMethord where cid='" + cid + "' and options='" + 2 + "'");
                            idMethord[1] = Convert.ToInt32(dt.Rows[0]["id"]);
                        }
                        else
                        {
                            server.DelTMethord(idMethord[1]);
                            demand2.Explain = false;
                        }
                    }
                    if (demand3.Explain != explain3.Checked)
                    {
                        if (explain3.Checked == true)
                        {
                            tMethod.Options = 3;
                            tMethod.Id = idMethord[2];
                            server.Insert(tMethod);
                            demand3.Explain = true;
                            dt = dataOperation.GetOneDataTable_sql("select * from TB_TrainMethord where cid='" + cid + "' and options='" + 3 + "'");
                            idMethord[2] = Convert.ToInt32(dt.Rows[0]["id"]);
                        }
                        else
                        {
                            server.DelTMethord(idMethord[2]);
                            demand3.Explain = false;
                        }
                    }
                    if (demand4.Explain != explain4.Checked)
                    {
                        if (explain4.Checked == true)
                        {
                            tMethod.Options = 4;
                            tMethod.Id = idMethord[3];
                            server.Insert(tMethod);
                            demand4.Explain = true;
                            dt = dataOperation.GetOneDataTable_sql("select * from TB_TrainMethord where cid='" + cid + "' and options='" + 4 + "'");
                            idMethord[3] = Convert.ToInt32(dt.Rows[0]["id"]);
                        }
                        else
                        {
                            server.DelTMethord(idMethord[3]);
                            demand4.Explain = false;
                        }
                    }
                    if (demand5.Explain != explain5.Checked)
                    {
                        if (explain5.Checked == true)
                        {
                            tMethod.Options = 5;
                            tMethod.Id = idMethord[4];
                            server.Insert(tMethod);
                            demand5.Explain = true;
                            dt = dataOperation.GetOneDataTable_sql("select * from TB_TrainMethord where cid='" + cid + "' and options='" + 5 + "'");
                            idMethord[4] = Convert.ToInt32(dt.Rows[0]["id"]);
                        }
                        else
                        {
                            server.DelTMethord(idMethord[4]);
                            demand5.Explain = false;
                        }
                    }
                    if (demand6.Explain != explain6.Checked)
                    {
                        if (explain6.Checked == true)
                        {
                            tMethod.Options = 6;
                            tMethod.Id = idMethord[5];
                            server.Insert(tMethod);
                            demand6.Explain = true;
                            dt = dataOperation.GetOneDataTable_sql("select * from TB_TrainMethord where cid='" + cid + "' and options='" + 6 + "'");
                            idMethord[5] = Convert.ToInt32(dt.Rows[0]["id"]);
                        }
                        else
                        {
                            server.DelTMethord(idMethord[5]);
                            demand6.Explain = false;
                        }
                    }
                    if (demand7.Explain != explain7.Checked)
                    {
                        if (explain7.Checked == true)
                        {
                            tMethod.Options = 7;
                            tMethod.Id = idMethord[6];
                            server.Insert(tMethod);
                            demand7.Explain = true;
                            dt = dataOperation.GetOneDataTable_sql("select * from TB_TrainMethord where cid='" + cid + "' and options='" + 7 + "'");
                            idMethord[6] = Convert.ToInt32(dt.Rows[0]["id"]);
                        }
                        else
                        {
                            server.DelTMethord(idMethord[6]);
                            demand7.Explain = false;
                        }
                    }
                    if (demand8.Explain != explain8.Checked)
                    {
                        if (explain8.Checked == true)
                        {
                            tMethod.Options = 8;
                            tMethod.Id = idMethord[7];
                            server.Insert(tMethod);
                            demand8.Explain = true;
                            dt = dataOperation.GetOneDataTable_sql("select * from TB_TrainMethord where cid='" + cid + "' and options='" + 8 + "'");
                            idMethord[7] = Convert.ToInt32(dt.Rows[0]["id"]);
                        }
                        else
                        {
                            server.DelTMethord(idMethord[7]);
                            demand8.Explain = false;
                        }
                    }
                    if (demand9.Explain != explain9.Checked)
                    {
                        if (explain9.Checked == true)
                        {
                            tMethod.Options = 9;
                            tMethod.Id = idMethord[8];
                            server.Insert(tMethod);
                            demand9.Explain = true;
                            dt = dataOperation.GetOneDataTable_sql("select * from TB_TrainMethord where cid='" + cid + "' and options='" + 9 + "'");
                            idMethord[8] = Convert.ToInt32(dt.Rows[0]["id"]);
                        }
                        else
                        {
                            server.DelTMethord(idMethord[8]);
                            demand9.Explain = false;
                        }
                    }
                    if (demand10.Explain != explain10.Checked)
                    {
                        if (explain10.Checked == true)
                        {
                            tMethod.Options = 10;
                            tMethod.Id = idMethord[9];
                            server.Insert(tMethod);
                            demand10.Explain = true;
                            dt = dataOperation.GetOneDataTable_sql("select * from TB_TrainMethord where cid='" + cid + "' and options='" + 10 + "'");
                            idMethord[9] = Convert.ToInt32(dt.Rows[0]["id"]);
                        }
                        else
                        {
                            server.DelTMethord(idMethord[9]);
                            demand10.Explain = false;
                        }
                    }
                    if (demand11.Explain != explain11.Checked)
                    {
                        if (explain11.Checked == true)
                        {
                            tMethod.Options = 11;
                            tMethod.Id = idMethord[10];
                            server.Insert(tMethod);
                            demand11.Explain = true;
                            dt = dataOperation.GetOneDataTable_sql("select * from TB_TrainMethord where cid='" + cid + "' and options='" + 11 + "'");
                            idMethord[10] = Convert.ToInt32(dt.Rows[0]["id"]);
                        }
                        else
                        {
                            server.DelTMethord(idMethord[10]);
                            demand11.Explain = false;
                        }
                    }
                    if (demand12.Explain != explain12.Checked)
                    {
                        if (explain12.Checked == true)
                        {
                            tMethod.Options = 12;
                            tMethod.Id = idMethord[11];
                            server.Insert(tMethod);
                            demand12.Explain = true;
                            dt = dataOperation.GetOneDataTable_sql("select * from TB_TrainMethord where cid='" + cid + "' and options='" + 12 + "'");
                            idMethord[11] = Convert.ToInt32(dt.Rows[0]["id"]);
                        }
                        else
                        {
                            server.DelTMethord(idMethord[11]);
                            demand12.Explain = false;
                        }
                    }
                    if (demand13.Explain != explain13.Checked)
                    {
                        if (explain13.Checked == true)
                        {
                            tMethod.Options = 13;
                            tMethod.Id = idMethord[12];
                            server.Insert(tMethod);
                            demand13.Explain = true;
                            dt = dataOperation.GetOneDataTable_sql("select * from TB_TrainMethord where cid='" + cid + "' and options='" + 13 + "'");
                            idMethord[12] = Convert.ToInt32(dt.Rows[0]["id"]);
                        }
                        else
                        {
                            server.DelTMethord(idMethord[12]);
                            demand13.Explain = false;

                        }
                    }
                    if (!(demand14.Explain == explain14.Checked) || note14.Text != demand14.Note14)
                    {
                        if (explain14.Checked == true)
                        {
                            tMethod.Options = 14;
                            tMethod.Id = idMethord[13];
                            tMethod.Note14 = note14.Text;
                            server.Insert(tMethod);
                            demand14.Explain = true;
                            demand14.Note14 = note14.Text;
                            dt = dataOperation.GetOneDataTable_sql("select * from TB_TrainMethord where cid='" + cid + "' and options='" + 14 + "'");
                            idMethord[13] = Convert.ToInt32(dt.Rows[0]["id"]);
                        }
                        else
                        {
                            server.DelTMethord(idMethord[13]);
                            demand14.Explain = false;
                            demand14.Note14 = "";
                        }
                    }
                    #endregion

                    #region//插入熟悉外语语种
                    fLanguage.Cid = cid;
                    if (!((lan1.ForeignKind == foreignKind1.Text || lan1.ForeignKind == null) && lan1.Show11 == show11 && lan1.Show12 == show12 && lan1.Show13 == show13 && lan1.Show14 == show14))//只有信息改变后才执行if里面的
                    {
                        if (!(show11 == false && show12 == false && show13 == false && show14 == false) || (foreignKind11.Text != string.Empty))
                        {
                            fLanguage.Number = 1;
                            fLanguage.ForeignKind = foreignKind1.Text;
                            if (show11 == true)
                                fLanguage.Level = "精通";
                            if (show12 == true)
                                fLanguage.Level = "熟练";
                            if (show13 == true)
                                fLanguage.Level = "良好";
                            if (show14 == true)
                                fLanguage.Level = "一般";
                            fLanguage.Id = idLanguage[0];
                            server.Insert(fLanguage);

                            lan1.ForeignKind = fLanguage.ForeignKind;
                            dt = dataOperation.GetOneDataTable_sql("select * from TB_FamiliarForeign where cid='" + cid + "'and foreignKind='" + fLanguage.ForeignKind + "' ");
                            idLanguage[0] = Convert.ToInt32(dt.Rows[0]["id"]);
                        }
                        else
                        {
                            fLanguage.Id = idLanguage[0];
                            server.Delete(fLanguage);

                        }
                    }
                    if (!((lan2.ForeignKind == foreignKind2.Text || lan2.ForeignKind == null) && lan2.Show21 == show21 && lan2.Show22 == show22 && lan2.Show23 == show23 && lan2.Show24 == show24))
                    {
                        if (!(show21 == false && show22 == false && show23 == false && show24 == false) || (foreignKind22.Text != string.Empty))
                        {
                            fLanguage.Number = 2;
                            fLanguage.ForeignKind = foreignKind2.Text;
                            if (show21 == true)
                                fLanguage.Level = "精通";
                            if (show22 == true)
                                fLanguage.Level = "熟练";
                            if (show23 == true)
                                fLanguage.Level = "良好";
                            if (show24 == true)
                                fLanguage.Level = "一般";
                            fLanguage.Id = idLanguage[1];
                            server.Insert(fLanguage);

                            lan2.ForeignKind = fLanguage.ForeignKind;
                            dt = dataOperation.GetOneDataTable_sql("select * from TB_FamiliarForeign where cid='" + cid + "'and foreignKind='" + fLanguage.ForeignKind + "' ");
                            idLanguage[1] = Convert.ToInt32(dt.Rows[0]["id"]);
                        }
                        else
                        {
                            fLanguage.Id = idLanguage[1];
                            server.Delete(fLanguage);

                        }
                    }
                    if (!((lan3.ForeignKind == foreignKind3.Text || lan3.ForeignKind == null) && lan3.Show31 == show31 && lan3.Show32 == show32 && lan3.Show33 == show33 && lan3.Show34 == show34))
                    {
                        if (!(show31 == false && show32 == false && show33 == false && show34 == false) || (foreignKind33.Text != string.Empty))
                        {
                            fLanguage.Number = 3;
                            fLanguage.ForeignKind = foreignKind3.Text;
                            if (show31 == true)
                                fLanguage.Level = "精通";
                            if (show32 == true)
                                fLanguage.Level = "熟练";
                            if (show33 == true)
                                fLanguage.Level = "良好";
                            if (show34 == true)
                                fLanguage.Level = "一般";
                            fLanguage.Id = idLanguage[2];
                            server.Insert(fLanguage);

                            lan3.ForeignKind = fLanguage.ForeignKind;
                            dt = dataOperation.GetOneDataTable_sql("select * from TB_FamiliarForeign where cid='" + cid + "'and foreignKind='" + fLanguage.ForeignKind + "' ");
                            idLanguage[2] = Convert.ToInt32(dt.Rows[0]["id"]);
                        }
                        else
                        {

                            fLanguage.Id = idLanguage[2];
                            server.Delete(fLanguage);

                        }
                    }
                    if (!((lan4.ForeignKind == foreignKind4.Text || lan4.ForeignKind == null) && lan4.Show41 == show41 && lan4.Show42 == show42 && lan4.Show43 == show43 && lan4.Show44 == show44))
                    {
                        if (!(show41 == false && show42 == false && show43 == false && show44 == false) || (foreignKind44.Text != string.Empty))
                        {
                            fLanguage.Number = 4;
                            fLanguage.ForeignKind = foreignKind4.Text;
                            if (show41 == true)
                                fLanguage.Level = "精通";
                            if (show42 == true)
                                fLanguage.Level = "熟练";
                            if (show43 == true)
                                fLanguage.Level = "良好";
                            if (show44 == true)
                                fLanguage.Level = "一般";
                            fLanguage.Id = idLanguage[3];
                            server.Insert(fLanguage);

                            lan4.ForeignKind = fLanguage.ForeignKind;
                            dt = dataOperation.GetOneDataTable_sql("select * from TB_FamiliarForeign where cid='" + cid + "'and foreignKind='" + fLanguage.ForeignKind + "' ");
                            idLanguage[3] = Convert.ToInt32(dt.Rows[0]["id"]);
                        }
                        else
                        {
                            fLanguage.Id = idLanguage[3]; 
                            server.Delete(fLanguage);
                        }
                    }
                    if (!((lan5.ForeignKind == foreignKind5.Text || lan5.ForeignKind == null) && lan5.Show51 == show51 && lan5.Show52 == show52 && lan5.Show53 == show53 && lan5.Show54 == show54))
                    {
                        if (!(show51 == false && show52 == false && show53 == false && show54 == false) || (foreignKind55.Text != string.Empty))
                        {
                            fLanguage.Number = 5;
                            fLanguage.ForeignKind = foreignKind5.Text;
                            if (show51 == true)
                                fLanguage.Level = "精通";
                            if (show52 == true)
                                fLanguage.Level = "熟练";
                            if (show53 == true)
                                fLanguage.Level = "良好";
                            if (show54 == true)
                                fLanguage.Level = "一般";
                            fLanguage.Id = idLanguage[4];
                            server.Insert(fLanguage);

                            lan5.ForeignKind = fLanguage.ForeignKind;
                            dt = dataOperation.GetOneDataTable_sql("select * from TB_FamiliarForeign where cid='" + cid + "'and foreignKind='" + fLanguage.ForeignKind + "' ");
                            idLanguage[4] = Convert.ToInt32(dt.Rows[0]["id"]);
                        }
                        else
                        {
                            fLanguage.Id = idLanguage[4];
                            server.Delete(fLanguage);
                        }
                    }
                    #endregion

                    //实践锻炼情况
                    #region
                    for (int i = 0; i < listView_TrainExercise.Items.Count; i ++ )
                    {
                        if(listView_TrainExercise.Items[i].Tag.Equals(""))
                        {
                            Train train = new Train();
                            train.Cid = listViewCid;
                            train.ReportMatter = listView_TrainExercise.Items[i].SubItems[1].Text;
                            train.StartTime = listView_TrainExercise.Items[i].SubItems[2].Text;
                            train.EndTime = listView_TrainExercise.Items[i].SubItems[3].Text;
                            train.ReportContent = listView_TrainExercise.Items[i].SubItems[4].Text;
                            train.Content1 = listView_TrainExercise.Items[i].SubItems[5].Text;
                            listView_TrainExercise.Items[i].Tag = dataOperation.InsertTrainExercise(train).ToString();
                        }
                    }
                    #endregion
                    MessageBox.Show("保存成功！","提示");
                }
                else
                {
                    MessageBox.Show("请把信息录入完整！","提示");

                }
            }
            catch (Exception) { }

        }

        /// <summary>
        /// 检查信息录入是不是完整
        /// </summary>
        /// <returns></returns>
        public int IsSaveLanguage()
        {
            int same = 0;

            if (foreignKind1.Text == "" && show11 == false && show12 == false && show13 == false && show14 == false || (foreignKind1.Text != "" && (show11 != false || show12 != false || show13 != false || show14 != false)))
                same = 1;
            else
                return 0;

            if (foreignKind2.Text == "" && show21 == false && show22 == false && show23 == false && show24 == false || (foreignKind2.Text != "" && (show21 != false || show22 != false || show23 != false || show24 != false)))
                same = 1;
            else
                return 0;
            if (foreignKind3.Text == "" && show31 == false && show32 == false && show33 == false && show34 == false || (foreignKind3.Text != "" && (show31 != false || show32 != false || show33 != false || show34 != false)))
                same = 1;
            else
                return 0;
            if (foreignKind4.Text == "" && show41 == false && show42 == false && show43 == false && show44 == false || (foreignKind4.Text != "" && (show41 != false || show42 != false || show43 != false || show44 != false)))
                same = 1;
            else
                return 0;
            if (foreignKind5.Text == "" && show51 == false && show52 == false && show53 == false && show54 == false || (foreignKind5.Text != "" && (show51 != false || show52 != false || show53 != false || show54 != false)))
                same = 1;
            else
                return 0;

            return same;
        }

        /// <summary>
        /// 清空信息采集表
        /// </summary>
        public void clearWindow()
        {
            clearArray();
            beginStudy1.Value = myTime;
            endStudy1.Value = myTime;
            beginStudy2.Value = myTime;
            endStudy2.Value = myTime;
            beginStudy3.Value = myTime;
            endStudy3.Value = myTime;
            studyCountry1.Text = "";
            studyCountry2.Text = "";
            studyCountry3.Text = "";
            schoolMajor1.Text = "";
            schoolMajor2.Text = "";
            schoolMajor3.Text = "";
            degree1.Text = "";
            degree2.Text = "";
            degree3.Text = "";
            beginWork1.Value = myTime;
            endWork1.Value = myTime;
            beginWork2.Value = myTime;
            endWork2.Value = myTime;
            beginWork3.Value = myTime;
            endWork3.Value = myTime;
            workCountry1.Text = "";
            workCountry2.Text = "";
            workCountry3.Text = "";
            workDept1.Text = "";
            workDept2.Text = "";
            workDept3.Text = "";
            workArea1.Text = "";
            workArea2.Text = "";
            workArea3.Text = "";
            foreignKind11.Text = "";
            foreignKind22.Text = "";
            foreignKind33.Text = "";
            foreignKind44.Text = "";
            foreignKind55.Text = "";
            content1.Text = "";
            content2.Text = "";
            content3.Text = "";
            content4.Text = "";
            content5.Text = "";
            content6.Text = "";
            content7.Text = "";
            content8.Text = "";
            content9.Text = "";
            explain1.Checked = false;
            explain2.Checked = false;
            explain3.Checked = false;
            explain4.Checked = false;
            explain5.Checked = false;
            explain6.Checked = false;
            explain7.Checked = false;
            explain8.Checked = false;
            explain9.Checked = false;
            explain10.Checked = false;
            explain11.Checked = false;
            explain12.Checked = false;
            explain13.Checked = false;
            explain14.Checked = false;
            show11 = false; show12 = false; show13 = false; show14 = false;
            show21 = false; show22 = false; show23 = false; show24 = false;
            show31 = false; show32 = false; show33 = false; show34 = false;
            show41 = false; show42 = false; show43 = false; show44 = false;
            show51 = false; show52 = false; show53 = false; show54 = false;
            this.level11.BackgroundImage = null;
            this.level12.BackgroundImage = null;
            this.level13.BackgroundImage = null;
            this.level14.BackgroundImage = null;
            this.level21.BackgroundImage = null;
            this.level22.BackgroundImage = null;
            this.level23.BackgroundImage = null;
            this.level24.BackgroundImage = null;
            this.level31.BackgroundImage = null;
            this.level32.BackgroundImage = null;
            this.level33.BackgroundImage = null;
            this.level34.BackgroundImage = null;
            this.level41.BackgroundImage = null;
            this.level42.BackgroundImage = null;
            this.level43.BackgroundImage = null;
            this.level44.BackgroundImage = null;
            this.level51.BackgroundImage = null;
            this.level52.BackgroundImage = null;
            this.level53.BackgroundImage = null;
            this.level54.BackgroundImage = null;
        }

        /// <summary>
        /// 清空临时数据数组
        /// </summary>
        public void clearArray()
        {
            try
            {
                for (int i = 0; i < 3; i++)
                {
                    idStudy[i] = 0;
                }

                for (int i = 0; i < 3; i++)
                {
                    idWork[i] = 0;
                }

                for (int i = 0; i < 9; i++)
                {
                    idMatter[i] = -1;
                }

                for (int i = 0; i < 2; i++)
                {
                    idTrain[i] = -1;
                }

                for (int i = 0; i < 14; i++)
                {
                    idMethord[i] = -1;

                }

                for (int i = 0; i < 5; i++)
                {
                    idLanguage[i] = -1;

                }
                three.Clear();
            }
            catch (Exception) { }

        }

        #region
        /// <summary>
        /// 在panel中打钩
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void level11_Paint(object sender, EventArgs e)
        {
            if (show11 == false)
            {
                show11 = true;
                show12 = false;
                show13 = false;
                show14 = false;
            }
            else
            {
                show11 = false;
                this.level11.BackgroundImage = null;
            }

            if (show11 == true && show12 == false && show13 == false && show14 == false)
            {
                this.level11.BackgroundImage = global::HBMISR.Properties.Resources.choice;
                this.level12.BackgroundImage = null;
                this.level13.BackgroundImage = null;
                this.level14.BackgroundImage = null;
            }

            if(change)
            {
                remarkchange = true;
            }
        }

        /// <summary>
        /// 在panel中打钩
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void level12_Paint(object sender, EventArgs e)
        {
            if (show12 == false)
            {
                show12 = true;
                show11 = false;
                show13 = false;
                show14 = false;
            }
            else
            {
                show12 = false;
                this.level12.BackgroundImage = null;
            }

            if (show12 == true && show11 == false && show13 == false && show14 == false)
            {
                this.level12.BackgroundImage = global::HBMISR.Properties.Resources.choice;
                this.level11.BackgroundImage = null;
                this.level13.BackgroundImage = null;
                this.level14.BackgroundImage = null;
            }

            if (change)
            {
                remarkchange = true;
            }

        }

        /// <summary>
        /// 在panel中打钩
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void level13_Paint(object sender, EventArgs e)
        {
            if (show13 == false)
            {
                show13 = true;
                show11 = false;
                show12 = false;
                show14 = false;
            }
            else
            {
                show13 = false;
                this.level13.BackgroundImage = null;
            }

            if ((show13 == true) && (show12 == false) && (show11 == false) && (show14 == false))
            {
                this.level13.BackgroundImage = global::HBMISR.Properties.Resources.choice;
                this.level12.BackgroundImage = null;
                this.level11.BackgroundImage = null;
                this.level14.BackgroundImage = null;
            }

            if (change)
            {
                remarkchange = true;
            }
        }

        /// <summary>
        /// 在panel中打钩
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void level14_Paint(object sender, EventArgs e)
        {
            if (show14 == false)
            {
                show14 = true;
                show11 = false;
                show13 = false;
                show12 = false;
            }
            else
            {
                show14 = false;
                this.level14.BackgroundImage = null;
            }

            if (show14 == true && show12 == false && show13 == false && show11 == false)
            {
                this.level14.BackgroundImage = global::HBMISR.Properties.Resources.choice;
                this.level12.BackgroundImage = null;
                this.level13.BackgroundImage = null;
                this.level11.BackgroundImage = null;
            }

            if (change)
            {
                remarkchange = true;
            }
        }

        /// <summary>
        /// 在panel中打钩
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void level21_Paint(object sender, EventArgs e)
        {
            if (show21 == false)
            {
                show21 = true;
                show22 = false;
                show23 = false;
                show24 = false;
            }
            else
            {
                show21 = false;
                this.level21.BackgroundImage = null;
            }
            if (show21 == true && show22 == false && show23 == false && show24 == false)
            {
                this.level21.BackgroundImage = global::HBMISR.Properties.Resources.choice;
                this.level22.BackgroundImage = null;
                this.level23.BackgroundImage = null;
                this.level24.BackgroundImage = null;
            }

            if (change)
            {
                remarkchange = true;
            }

        }

        /// <summary>
        /// 在panel中打钩
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void level22_Paint(object sender, EventArgs e)
        {
            if (show22 == false)
            {
                show22 = true;
                show21 = false;
                show23 = false;
                show24 = false;
            }
            else
            {
                show22 = false;
                this.level22.BackgroundImage = null;
            }

            if (show22 == true && show21 == false && show23 == false && show24 == false)
            {
                this.level21.BackgroundImage = null;
                this.level22.BackgroundImage = global::HBMISR.Properties.Resources.choice;
                this.level23.BackgroundImage = null;
                this.level24.BackgroundImage = null;
            }

            if (change)
            {
                remarkchange = true;
            }
        }

        /// <summary>
        /// 在panel中打钩
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void level23_Paint(object sender, EventArgs e)
        {
            if (show23 == false)
            {
                show23 = true;
                show22 = false;
                show21 = false;
                show24 = false;
            }
            else
            {
                show23 = false;
                this.level23.BackgroundImage = null;
            }

            if (show23 == true && show22 == false && show21 == false && show24 == false)
            {
                this.level23.BackgroundImage = global::HBMISR.Properties.Resources.choice;
                this.level22.BackgroundImage = null;
                this.level21.BackgroundImage = null;
                this.level24.BackgroundImage = null;
            }

            if (change)
            {
                remarkchange = true;
            }
        }

        /// <summary>
        /// 在panel中打钩
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void level24_Paint(object sender, EventArgs e)
        {
            if (show24 == false)
            {
                show24 = true;
                show22 = false;
                show23 = false;
                show21 = false;
            }
            else
            {
                show24 = false;
                this.level24.BackgroundImage = null;
            }

            if (show24 == true && show22 == false && show23 == false && show21 == false)
            {
                this.level24.BackgroundImage = global::HBMISR.Properties.Resources.choice;
                this.level22.BackgroundImage = null;
                this.level23.BackgroundImage = null;
                this.level21.BackgroundImage = null;
            }

            if (change)
            {
                remarkchange = true;
            }

        }

        /// <summary>
        /// 在panel中打钩
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void level31_Paint(object sender, EventArgs e)
        {
            if (show31 == false)
            {
                show31 = true;
                show32 = false;
                show33 = false;
                show34 = false;
            }
            else
            {
                show31 = false;
                this.level31.BackgroundImage = null;
            }

            if (show31 == true && show32 == false && show33 == false && show34 == false)
            {
                this.level31.BackgroundImage = global::HBMISR.Properties.Resources.choice;
                this.level32.BackgroundImage = null;
                this.level33.BackgroundImage = null;
                this.level34.BackgroundImage = null;
            }

            if (change)
            {
                remarkchange = true;
            }
        }

        /// <summary>
        /// 在panel中打钩
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void level32_Paint(object sender, EventArgs e)
        {
            if (show32 == false)
            {
                show32 = true;
                show31 = false;
                show33 = false;
                show34 = false;
            }
            else
            {
                show32 = false;
                this.level32.BackgroundImage = null;
            }

            if (show32 == true && show31 == false && show33 == false && show34 == false)
            {
                this.level32.BackgroundImage = global::HBMISR.Properties.Resources.choice;
                this.level31.BackgroundImage = null;
                this.level33.BackgroundImage = null;
                this.level34.BackgroundImage = null;
            }

            if (change)
            {
                remarkchange = true;
            }

        }

        /// <summary>
        /// 在panel中打钩
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void level33_Paint(object sender, EventArgs e)
        {
            if (show33 == false)
            {
                show33 = true;
                show32 = false;
                show31 = false;
                show34 = false;
            }
            else
            {
                show33 = false;
                this.level33.BackgroundImage = null;
            }

            if (show33 == true && show32 == false && show31 == false && show34 == false)
            {
                this.level33.BackgroundImage = global::HBMISR.Properties.Resources.choice;
                this.level32.BackgroundImage = null;
                this.level31.BackgroundImage = null;
                this.level34.BackgroundImage = null;
            }

            if (change)
            {
                remarkchange = true;
            }

        }

        /// <summary>
        /// 在panel中打钩
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void level34_Paint(object sender, EventArgs e)
        {
            if (show34 == false)
            {
                show34 = true;
                show32 = false;
                show33 = false;
                show31 = false;
            }
            else
            {
                show34 = false;
                this.level34.BackgroundImage = null;
            }

            if (show34 == true && show32 == false && show33 == false && show31 == false)
            {
                this.level34.BackgroundImage = global::HBMISR.Properties.Resources.choice;
                this.level32.BackgroundImage = null;
                this.level33.BackgroundImage = null;
                this.level31.BackgroundImage = null;
            }

            if (change)
            {
                remarkchange = true;
            }

        }

        /// <summary>
        /// 在panel中打钩
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void level41_Paint(object sender, EventArgs e)
        {
            if (show41 == false)
            {
                show41 = true;
                show42 = false;
                show43 = false;
                show44 = false;
            }
            else
            {
                show41 = false;
                this.level41.BackgroundImage = null;
            }

            if (show41 == true && show42 == false && show43 == false && show44 == false)
            {
                this.level41.BackgroundImage = global::HBMISR.Properties.Resources.choice;
                this.level42.BackgroundImage = null;
                this.level43.BackgroundImage = null;
                this.level44.BackgroundImage = null;
            }

            if (change)
            {
                remarkchange = true;
            }
        }

        /// <summary>
        /// 在panel中打钩
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void level42_Paint(object sender, EventArgs e)
        {
            if (show42 == false)
            {
                show42 = true;
                show41 = false;
                show43 = false;
                show44 = false;
            }
            else
            {
                show42 = false;
                this.level42.BackgroundImage = null;
            }

            if (show42 == true && show41 == false && show43 == false && show44 == false)
            {
                this.level42.BackgroundImage = global::HBMISR.Properties.Resources.choice;
                this.level41.BackgroundImage = null;
                this.level43.BackgroundImage = null;
                this.level44.BackgroundImage = null;
            }

            if (change)
            {
                remarkchange = true;
            }
        }

        /// <summary>
        /// 在panel中打钩
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void level43_Paint(object sender, EventArgs e)
        {
            if (show43 == false)
            {
                show43 = true;
                show42 = false;
                show41 = false;
                show44 = false;
            }
            else
            {
                show43 = false;
                this.level43.BackgroundImage = null;
            }

            if (show43 == true && show42 == false && show41 == false && show44 == false)
            {
                this.level43.BackgroundImage = global::HBMISR.Properties.Resources.choice;
                this.level42.BackgroundImage = null;
                this.level41.BackgroundImage = null;
                this.level44.BackgroundImage = null;
            }

            if (change)
            {
                remarkchange = true;
            }
        }

        /// <summary>
        /// 在panel中打钩
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void level44_Paint(object sender, EventArgs e)
        {
            if (show44 == false)
            {
                show44 = true;
                show42 = false;
                show43 = false;
                show41 = false;
            }
            else
            {
                show44 = false;
                this.level44.BackgroundImage = null;
            }

            if (show44 == true && show42 == false && show43 == false && show41 == false)
            {
                this.level44.BackgroundImage = global::HBMISR.Properties.Resources.choice;
                this.level42.BackgroundImage = null;
                this.level43.BackgroundImage = null;
                this.level41.BackgroundImage = null;
            }

            if (change)
            {
                remarkchange = true;
            }
        }

        /// <summary>
        /// 在panel中打钩
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void level51_Paint(object sender, EventArgs e)
        {
            if (show51 == false)
            {
                show51 = true;
                show52 = false;
                show53 = false;
                show54 = false;
            }
            else
            {
                show51 = false;
                this.level51.BackgroundImage = null;
            }

            if (show51 == true && show52 == false && show53 == false && show54 == false)
            {
                this.level51.BackgroundImage = global::HBMISR.Properties.Resources.choice;
                this.level52.BackgroundImage = null;
                this.level53.BackgroundImage = null;
                this.level54.BackgroundImage = null;
            }

            if (change)
            {
                remarkchange = true;
            }
        }

        /// <summary>
        /// 在panel中打钩
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void level52_Paint(object sender, EventArgs e)
        {
            if (show52 == false)
            {
                show52 = true;
                show51 = false;
                show53 = false;
                show54 = false;
            }
            else
            {
                show52 = false;
                this.level52.BackgroundImage = null;
            }

            if (show52 == true && show51 == false && show53 == false && show54 == false)
            {
                this.level52.BackgroundImage = global::HBMISR.Properties.Resources.choice;
                this.level51.BackgroundImage = null;
                this.level53.BackgroundImage = null;
                this.level54.BackgroundImage = null;
            }

            if (change)
            {
                remarkchange = true;
            }
        }

        /// <summary>
        /// 在panel中打钩
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void level53_Paint(object sender, EventArgs e)
        {
            if (show53 == false)
            {
                show53 = true;
                show52 = false;
                show51 = false;
                show54 = false;
            }
            else
            {
                show53 = false;
                this.level53.BackgroundImage = null;
            }

            if (show53 == true && show52 == false && show51 == false && show54 == false)
            {
                this.level53.BackgroundImage = global::HBMISR.Properties.Resources.choice;
                this.level52.BackgroundImage = null;
                this.level51.BackgroundImage = null;
                this.level54.BackgroundImage = null;
            }

            if (change)
            {
                remarkchange = true;
            }
        }

        /// <summary>
        /// 在panel中打钩
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void level54_Paint(object sender, EventArgs e)
        {
            if (show54 == false)
            {
                show54 = true;
                show52 = false;
                show53 = false;
                show51 = false;
            }
            else
            {
                show54 = false;
                this.level54.BackgroundImage = null;
            }

            if (show54 == true && show52 == false && show53 == false && show51 == false)
            {
                this.level54.BackgroundImage = global::HBMISR.Properties.Resources.choice;
                this.level52.BackgroundImage = null;
                this.level53.BackgroundImage = null;
                this.level51.BackgroundImage = null;
            }

            if (change)
            {
                remarkchange = true;
            }
        }
        #endregion

        /// <summary>
        /// 保存按钮的事件监听
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button_Save_Click(object sender, EventArgs e)
        {
            SaveInfo(listViewCid);
            remarkchange = false;
        }

        #region
        /// <summary>
        /// 培养锻炼措施需求中点击复选框产生的事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void explain1_CheckedChanged(object sender, EventArgs e)
        {
            three.Add(1);
            JudgeThree();

            if (change)
            {
                remarkchange = true;
            }
        }
        /// <summary>
        /// 培养锻炼措施需求中点击复选框产生的事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void explain2_CheckedChanged(object sender, EventArgs e)
        {
            three.Add(2);
            JudgeThree();

            if (change)
            {
                remarkchange = true;
            }
        }
        /// <summary>
        /// 培养锻炼措施需求中点击复选框产生的事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void explain3_CheckedChanged(object sender, EventArgs e)
        {
            three.Add(3);
            JudgeThree();

            if (change)
            {
                remarkchange = true;
            }
        }
        /// <summary>
        /// 培养锻炼措施需求中点击复选框产生的事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void explain4_CheckedChanged(object sender, EventArgs e)
        {
            three.Add(4);
            JudgeThree();

            if (change)
            {
                remarkchange = true;
            }
        }
        /// <summary>
        /// 培养锻炼措施需求中点击复选框产生的事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void explain5_CheckedChanged(object sender, EventArgs e)
        {
            three.Add(5);
            JudgeThree();

            if (change)
            {
                remarkchange = true;
            }

        }
        /// <summary>
        /// 培养锻炼措施需求中点击复选框产生的事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void explain6_CheckedChanged(object sender, EventArgs e)
        {
            three.Add(6);
            JudgeThree();

            if (change)
            {
                remarkchange = true;
            }

        }
        /// <summary>
        /// 培养锻炼措施需求中点击复选框产生的事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void explain7_CheckedChanged(object sender, EventArgs e)
        {
            three.Add(7);
            JudgeThree();

            if (change)
            {
                remarkchange = true;
            }
        }
        /// <summary>
        /// 培养锻炼措施需求中点击复选框产生的事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void explain8_CheckedChanged(object sender, EventArgs e)
        {
            three.Add(8);
            JudgeThree();

            if (change)
            {
                remarkchange = true;
            }

        }
        /// <summary>
        /// 培养锻炼措施需求中点击复选框产生的事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void explain9_CheckedChanged(object sender, EventArgs e)
        {
            three.Add(9);
            JudgeThree();

            if (change)
            {
                remarkchange = true;
            }
        }
        /// <summary>
        /// 培养锻炼措施需求中点击复选框产生的事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void explain10_CheckedChanged(object sender, EventArgs e)
        {
            three.Add(10);
            JudgeThree();

            if (change)
            {
                remarkchange = true;
            }
        }
        /// <summary>
        /// 培养锻炼措施需求中点击复选框产生的事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void explain11_CheckedChanged(object sender, EventArgs e)
        {
            three.Add(11);
            JudgeThree();

            if (change)
            {
                remarkchange = true;
            }
        }
        /// <summary>
        /// 培养锻炼措施需求中点击复选框产生的事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void explain12_CheckedChanged(object sender, EventArgs e)
        {
            three.Add(12);
            JudgeThree();

            if (change)
            {
                remarkchange = true;
            }
        }
        /// <summary>
        /// 培养锻炼措施需求中点击复选框产生的事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void explain13_CheckedChanged(object sender, EventArgs e)
        {
            three.Add(13);
            JudgeThree();

            if (change)
            {
                remarkchange = true;
            }
        }
        /// <summary>
        /// 检查第十四项是不是点中 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void explain14_CheckedChanged(object sender, EventArgs e)
        {
            three.Add(14);
            JudgeThree();
            if (explain14.Checked)
            {
                note14.Enabled = true;
            }
            else
            {
                note14.Enabled = false;
                note14.Text = "";
            }

        }

        /// <summary>
        ///  判断选项有没有超过三个
        /// </summary>
        public void JudgeThree()
        {
            int i = 0;
            if (explain1.Checked)
            {
                i++;
                if (i > 3)
                {
                    MessageBox.Show("选项不能超过三个！");
                    CancelLast();
                    return;
                }
            }
            if (explain2.Checked)
            {
                i++;
                if (i > 3)
                {
                    MessageBox.Show("选项不能超过三个！");
                    CancelLast();
                    return;
                }
            }
            if (explain3.Checked)
            {
                i++;
                if (i > 3)
                {
                    MessageBox.Show("选项不能超过三个！");
                    CancelLast();
                    return;
                }
            }
            if (explain4.Checked)
            {
                i++;
                if (i > 3)
                {
                    MessageBox.Show("选项不能超过三个！");
                    CancelLast();
                    return;
                }
            }
            if (explain5.Checked)
            {
                i++;
                if (i > 3)
                {
                    MessageBox.Show("选项不能超过三个！");
                    CancelLast();
                    return;
                }
            }
            if (explain6.Checked)
            {
                i++;
                if (i > 3)
                {
                    MessageBox.Show("选项不能超过三个！");
                    CancelLast();
                    return;
                }
            }
            if (explain7.Checked)
            {
                i++;
                if (i > 3)
                {
                    MessageBox.Show("选项不能超过三个！");
                    CancelLast();
                    return;
                }
            }
            if (explain8.Checked)
            {
                i++;
                if (i > 3)
                {
                    MessageBox.Show("选项不能超过三个！");
                    CancelLast();
                    return;
                }
            }
            if (explain9.Checked)
            {
                i++;
                if (i > 3)
                {
                    MessageBox.Show("选项不能超过三个！");
                    CancelLast();
                    return;
                }
            }
            if (explain10.Checked)
            {
                i++;
                if (i > 3)
                {
                    MessageBox.Show("选项不能超过三个！");
                    CancelLast();
                    return;
                }
            }
            if (explain11.Checked)
            {
                i++;
                if (i > 3)
                {
                    MessageBox.Show("选项不能超过三个！");
                    CancelLast();
                    return;
                }
            }
            if (explain12.Checked)
            {
                i++;
                if (i > 3)
                {
                    MessageBox.Show("选项不能超过三个！");
                    CancelLast();
                    return;
                }
            }
            if (explain13.Checked)
            {
                i++;
                if (i > 3)
                {
                    MessageBox.Show("选项不能超过三个！");
                    CancelLast();
                    return;
                }
            }
            if (explain14.Checked)
            {
                i++;
                note14.ReadOnly = false;
                if (i > 3)
                {
                    MessageBox.Show("选项不能超过三个！");
                    CancelLast();
                    note14.ReadOnly = true;
                    return;
                }
            }
            else
            {
                note14.Text = "";
                note14.ReadOnly = true;

            }

        }
        /// <summary>
        /// 消去培养措施选中的第四项
        /// </summary>
        public void CancelLast()
        {
            switch (three[three.Count - 1])
            {
                case 1: explain1.Checked = false; demand1.Explain = false; three.RemoveAt(three.Count - 1); break;
                case 2: explain2.Checked = false; demand2.Explain = false; three.RemoveAt(three.Count - 1); break;
                case 3: explain3.Checked = false; demand3.Explain = false; three.RemoveAt(three.Count - 1); break;
                case 4: explain4.Checked = false; demand4.Explain = false; three.RemoveAt(three.Count - 1); break;
                case 5: explain5.Checked = false; demand5.Explain = false; three.RemoveAt(three.Count - 1); break;
                case 6: explain6.Checked = false; demand6.Explain = false; three.RemoveAt(three.Count - 1); break;
                case 7: explain7.Checked = false; demand7.Explain = false; three.RemoveAt(three.Count - 1); break;
                case 8: explain8.Checked = false; demand8.Explain = false; three.RemoveAt(three.Count - 1); break;
                case 9: explain9.Checked = false; demand9.Explain = false; three.RemoveAt(three.Count - 1); break;
                case 10: explain10.Checked = false; demand10.Explain = false; three.RemoveAt(three.Count - 1); break;
                case 11: explain11.Checked = false; demand11.Explain = false; three.RemoveAt(three.Count - 1); break;
                case 12: explain12.Checked = false; demand12.Explain = false; three.RemoveAt(three.Count - 1); break;
                case 13: explain13.Checked = false; demand13.Explain = false; three.RemoveAt(three.Count - 1); break;
                case 14: explain14.Checked = false; demand14.Explain = false; three.RemoveAt(three.Count - 1); break;

            }
        }
        /// <summary>
        /// 当主界面切换时调用的函数，改变培养措施的第十四项的编辑状态
        /// </summary>
        public void JudgeNote14IsCheck()
        {
            if (explain14.Checked)
            {
                note14.ReadOnly = false;
            }
            else
            {
                note14.Text = "";
                note14.ReadOnly = true;
            }
        }
        #endregion

        /// <summary>
        /// 增加一条信息记录
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Add_Click(object sender, EventArgs e)
        {
            if (comMatter.Text.Equals(""))
            {
                MessageBox.Show("报告事项不能为空!");
                return;
            }
            if (txtContent.Text.Equals(""))
            {
                MessageBox.Show("报告形式不能为空!");
                return;
            }

            if (txtReporContent.Text.Equals(""))
            {
                MessageBox.Show("内容不能为空!");
                return;
            }
            if (isEditOfT)//如果是更新已存在的信息
            {
                //更新临时信息
                listView_TrainExercise.SelectedItems[0].SubItems[1].Text = comMatter.Text.Trim();
                listView_TrainExercise.SelectedItems[0].SubItems[2].Text = dtpStartTime.Value.ToString("yyyy'年'MM'月'dd'日'");
                listView_TrainExercise.SelectedItems[0].SubItems[3].Text = dtpEndTime.Value.ToString("yyyy'年'MM'月'dd'日'");
                listView_TrainExercise.SelectedItems[0].SubItems[4].Text = txtContent.Text.Trim();
                listView_TrainExercise.SelectedItems[0].SubItems[5].Text = txtReporContent.Text.Trim();
                //如果已经存在于数据库中的数据
 
                if (listView_TrainExercise.SelectedItems[0].Tag.ToString() != "")//直接更新
                {
                    string sql = "update TB_TrainExercise set reportContent='" + txtContent.Text.Trim() + "',reportMatter='" + comMatter.Text.Trim() + "',startTime='" + dtpStartTime.Value.ToString("yyyy'年'MM'月'dd'日'") + "',endTime='" + dtpEndTime.Value.ToString("yyyy'年'MM'月'dd'日'") + "',content='" + txtReporContent.Text.Trim() + "'  where id='" + listView_TrainExercise.SelectedItems[0].Tag.ToString() + "' and cid='" + listViewCid + "'";
                    dataOperation.OperateData_sql(sql);
                    MessageBox.Show("更新成功！", "提示!");
                }
                isEditOfT = false;
            }
            else
            {
                ListViewItem item = new ListViewItem();
                item.Tag = "";
                item.SubItems.Add(comMatter.Text.Trim());
                item.SubItems.Add(dtpStartTime.Value.ToString("yyyy'年'MM'月'dd'日'"));
                item.SubItems.Add(dtpEndTime.Value.ToString("yyyy'年'MM'月'dd'日'"));
                item.SubItems.Add(txtContent.Text.Trim());
                item.SubItems.Add(txtReporContent.Text.Trim());
                listView_TrainExercise.Items.Add(item);
            }
            this.comMatter.SelectedIndex = -1;
            this.txtContent.Text = "";
            this.txtReporContent.Text = "";
        }

        /// <summary>
        ///  获取鼠标点击焦点
        /// </summary>
        /// <param name="dataGridView1"></param>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        private int GetRowFromPoint(DataGridView dataGridView1, int x, int y)
        {
            try
            {
                for (int i = 0; i < dataGridView1.RowCount; i++)
                {
                    Rectangle rec = dataGridView1.GetRowDisplayRectangle(i, false);

                    if (dataGridView1.RectangleToScreen(rec).Contains(x, y))
                        return i;
                }
                return -1;

            }
            catch (Exception) { return -1; }
        }

        private void btClear_Click(object sender, EventArgs e)
        {
            this.comMatter.SelectedIndex = -1;
            this.txtContent.Text = "";
            this.txtReporContent.Text = "";
            isEditOfT = false;
        }

        /// <summary>
        /// 报告事项选择变化时
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void comMatter_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtContent.Items.Clear();
            txtContent.Text = "";
            DataOperation oper = new DataOperation(Application.StartupPath + "\\DB\\DBHBMSU.db");

            string sql1 = "select  reportContent from TB_TRAINEXERCISE where reportMatter='" + comMatter.Text + "'";
            DataTable dtReportContent = oper.GetOneDataTable_sql(sql1);

            for (int i = 0; i < dtReportContent.Rows.Count; i++)
            {
                txtContent.Items.Add(dtReportContent.Rows[i]["reportContent"].ToString());
            }

        }

        /// <summary>
        /// 熟悉外语语种变化时
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void foreignKind11_MouseClick(object sender, MouseEventArgs e)
        {
            foreignKind1.Focus();
        }

        /// <summary>
        /// 熟悉外语语种变化时
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void foreignKind22_MouseClick(object sender, MouseEventArgs e)
        {
            foreignKind2.Focus();
        }

        /// <summary>
        /// 熟悉外语语种变化时
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void foreignKind33_MouseClick(object sender, MouseEventArgs e)
        {
            foreignKind3.Focus();
        }

        /// <summary>
        /// 熟悉外语语种变化时
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void foreignKind44_MouseClick(object sender, MouseEventArgs e)
        {
            foreignKind4.Focus();
        }

        /// <summary>
        /// 熟悉外语语种变化时
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void foreignKind55_MouseClick(object sender, MouseEventArgs e)
        {
            foreignKind5.Focus();
        }

        /// <summary>
        /// 限制培训结束时间
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dtpEndTime_ValueChanged(object sender, EventArgs e)
        {
            int tempEnd = Convert.ToInt32(dtpEndTime.Value.ToString("yyyyMMdd"));
            int tempStart = Convert.ToInt32(dtpStartTime.Value.ToString("yyyyMMdd"));
            int now = Convert.ToInt32(DateTime.Now.ToString("yyyyMMdd"));
            if (tempEnd > now)
            {
                MessageBox.Show("结束时间应小于当前时间","提示");
                dtpEndTime.Value = DateTime.Now;
            }
            if (tempEnd <　tempStart )
            {
                MessageBox.Show("结束时间应大于开始时间", "提示");
                dtpEndTime.Value = DateTime.Now;
            }
        }

        /// <summary>
        /// 限制培训开始时间
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dtpStartTime_ValueChanged(object sender, EventArgs e)
        {
            int tempEnd = Convert.ToInt32(dtpEndTime.Value.ToString("yyyyMMdd"));
            int tempStart = Convert.ToInt32(dtpStartTime.Value.ToString("yyyyMMdd"));
            int now = Convert.ToInt32(DateTime.Now.ToString("yyyyMMdd"));
            if (tempStart > tempEnd )
            {
                MessageBox.Show("开始时间应小于结束时间", "提示");
                dtpStartTime.Value = dtpEndTime.Value;
            }
        }

        /// <summary>
        /// 限制第一个海外学习结束时间
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void endStudy1_ValueChanged(object sender, EventArgs e)
        {
            int tempEnd = Convert.ToInt32(endStudy1.Value.ToString("yyyyMMdd"));
            int tempStart = Convert.ToInt32(beginStudy1.Value.ToString("yyyyMMdd"));
            int now = Convert.ToInt32(DateTime.Now.ToString("yyyyMMdd"));
            if (tempEnd > now)
            {
                MessageBox.Show("结束时间应小于当前时间", "提示");
                endStudy1.Value = DateTime.Now;
            }
            if (tempEnd < tempStart)
            {
                MessageBox.Show("结束时间应大于开始时间", "提示");
                endStudy1.Value = DateTime.Now;
            }

            if (change)
            {
                remarkchange = true;
            }
        }

        /// <summary>
        /// 限制第一个海外学习开始时间
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void beginStudy1_ValueChanged(object sender, EventArgs e)
        {
            int tempEnd = Convert.ToInt32(endStudy1.Value.ToString("yyyyMMdd"));
            int tempStart = Convert.ToInt32(beginStudy1.Value.ToString("yyyyMMdd"));
            int now = Convert.ToInt32(DateTime.Now.ToString("yyyyMMdd"));
            if (tempStart > tempEnd)
            {
                MessageBox.Show("开始时间应小于结束时间", "提示");
                beginStudy1.Value = endStudy1.Value;
            }
        }

        /// <summary>
        /// 限制第二个海外学习开始时间
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void beginStudy2_ValueChanged(object sender, EventArgs e)
        {
            int tempEnd = Convert.ToInt32(endStudy2.Value.ToString("yyyyMMdd"));
            int tempStart = Convert.ToInt32(beginStudy2.Value.ToString("yyyyMMdd"));
            int now = Convert.ToInt32(DateTime.Now.ToString("yyyyMMdd"));
            if (tempStart > tempEnd)
            {
                MessageBox.Show("开始时间应小于结束时间", "提示");
                beginStudy2.Value = endStudy2.Value;
            }
            if (change)
            {
                remarkchange = true;
            }
        }

        /// <summary>
        /// 限制第二个海外学习结束时间
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void endStudy2_ValueChanged(object sender, EventArgs e)
        {
            int tempEnd = Convert.ToInt32(endStudy2.Value.ToString("yyyyMMdd"));
            int tempStart = Convert.ToInt32(beginStudy2.Value.ToString("yyyyMMdd"));
            int now = Convert.ToInt32(DateTime.Now.ToString("yyyyMMdd"));
            if (tempEnd > now)
            {
                MessageBox.Show("结束时间应小于当前时间", "提示");
                endStudy2.Value = DateTime.Now;
            }
            if (tempEnd < tempStart)
            {
                MessageBox.Show("结束时间应大于开始时间", "提示");
                endStudy2.Value = DateTime.Now;
            }
            if (change)
            {
                remarkchange = true;
            }
        }

        /// <summary>
        /// 限制第三个海外学习开始时间
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void beginStudy3_ValueChanged(object sender, EventArgs e)
        {
            int tempEnd = Convert.ToInt32(endStudy3.Value.ToString("yyyyMMdd"));
            int tempStart = Convert.ToInt32(beginStudy3.Value.ToString("yyyyMMdd"));
            int now = Convert.ToInt32(DateTime.Now.ToString("yyyyMMdd"));
            if (tempStart > tempEnd)
            {
                MessageBox.Show("开始时间应小于结束时间", "提示");
                beginStudy3.Value = endStudy3.Value;
            }
            if (change)
            {
                remarkchange = true;
            }
        }

        /// <summary>
        /// 限制第一个海外学习结束时间
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void endStudy3_ValueChanged(object sender, EventArgs e)
        {
            int tempEnd = Convert.ToInt32(endStudy3.Value.ToString("yyyyMMdd"));
            int tempStart = Convert.ToInt32(beginStudy3.Value.ToString("yyyyMMdd"));
            int now = Convert.ToInt32(DateTime.Now.ToString("yyyyMMdd"));
            if (tempEnd > now)
            {
                MessageBox.Show("结束时间应小于当前时间", "提示");
                endStudy3.Value = DateTime.Now;
            }
            if (tempEnd < tempStart)
            {
                MessageBox.Show("结束时间应大于开始时间", "提示");
                endStudy3.Value = DateTime.Now;
            }
            if (change)
            {
                remarkchange = true;
            }
        }

        /// <summary>
        /// 限制第一个开始工作时间
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void beginWork1_ValueChanged(object sender, EventArgs e)
        {
            int tempEnd = Convert.ToInt32(endWork1.Value.ToString("yyyyMMdd"));
            int tempStart = Convert.ToInt32(beginWork1.Value.ToString("yyyyMMdd"));
            int now = Convert.ToInt32(DateTime.Now.ToString("yyyyMMdd"));
            if (tempStart > tempEnd)
            {
                MessageBox.Show("开始时间应小于结束时间", "提示");
                beginWork1.Value = endWork1.Value;
            }
            if (change)
            {
                remarkchange = true;
            }
        }

        /// <summary>
        /// 限制第一个结束工作时间
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void endWork1_ValueChanged(object sender, EventArgs e)
        {
            int tempEnd = Convert.ToInt32(endWork1.Value.ToString("yyyyMMdd"));
            int tempStart = Convert.ToInt32(beginWork1.Value.ToString("yyyyMMdd"));
            int now = Convert.ToInt32(DateTime.Now.ToString("yyyyMMdd"));
            if (tempEnd > now)
            {
                MessageBox.Show("结束时间应小于当前时间", "提示");
                endWork1.Value = DateTime.Now;
            }
            if (tempEnd < tempStart)
            {
                MessageBox.Show("结束时间应大于开始时间", "提示");
                endWork1.Value = DateTime.Now;
            }
            if (change)
            {
                remarkchange = true;
            }
        }

        /// <summary>
        /// 限制第二个开始工作时间
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void beginWork2_ValueChanged(object sender, EventArgs e)
        {
            int tempEnd = Convert.ToInt32(endWork2.Value.ToString("yyyyMMdd"));
            int tempStart = Convert.ToInt32(beginWork2.Value.ToString("yyyyMMdd"));
            int now = Convert.ToInt32(DateTime.Now.ToString("yyyyMMdd"));
            if (tempStart > tempEnd)
            {
                MessageBox.Show("开始时间应小于结束时间", "提示");
                beginWork2.Value = endWork2.Value;
            }
            if (change)
            {
                remarkchange = true;
            }
        }

        /// <summary>
        /// 限制第二个结束工作时间
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void endWork2_ValueChanged(object sender, EventArgs e)
        {
            int tempEnd = Convert.ToInt32(endWork2.Value.ToString("yyyyMMdd"));
            int tempStart = Convert.ToInt32(beginWork2.Value.ToString("yyyyMMdd"));
            int now = Convert.ToInt32(DateTime.Now.ToString("yyyyMMdd"));
            if (tempEnd > now)
            {
                MessageBox.Show("结束时间应小于当前时间", "提示");
                endWork2.Value = DateTime.Now;
            }
            if (tempEnd < tempStart)
            {
                MessageBox.Show("结束时间应大于开始时间", "提示");
                endWork2.Value = DateTime.Now;
            }
            if (change)
            {
                remarkchange = true;
            }
        }

        /// <summary>
        /// 限制第三个开始工作时间
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void beginWork3_ValueChanged(object sender, EventArgs e)
        {
            int tempEnd = Convert.ToInt32(endWork3.Value.ToString("yyyyMMdd"));
            int tempStart = Convert.ToInt32(beginWork3.Value.ToString("yyyyMMdd"));
            int now = Convert.ToInt32(DateTime.Now.ToString("yyyyMMdd"));
            if (tempStart > tempEnd)
            {
                MessageBox.Show("开始时间应小于结束时间", "提示");
                beginWork3.Value = endWork3.Value;
            }
            if (change)
            {
                remarkchange = true;
            }
        }

        /// <summary>
        /// 限制第三个结束工作时间
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void endWork3_ValueChanged(object sender, EventArgs e)
        {
            int tempEnd = Convert.ToInt32(endWork3.Value.ToString("yyyyMMdd"));
            int tempStart = Convert.ToInt32(beginWork3.Value.ToString("yyyyMMdd"));
            int now = Convert.ToInt32(DateTime.Now.ToString("yyyyMMdd"));
            if (tempEnd > now)
            {
                MessageBox.Show("结束时间应小于当前时间", "提示");
                endWork3.Value = DateTime.Now;
            }
            if (tempEnd < tempStart)
            {
                MessageBox.Show("结束时间应大于开始时间", "提示");
                endWork3.Value = DateTime.Now;
            }
            if (change)
            {
                remarkchange = true;
            }
        }

        /// <summary>
        /// 右键删除培训措施
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TSMI_DeleteTrainExercise_Click(object sender, EventArgs e)
        {
            if (listView_TrainExercise.SelectedItems[0].Tag.ToString().Equals(""))
            {
                listView_TrainExercise.SelectedItems[0].Remove();
            }
            else
            {
                int id = Convert.ToInt32(listView_TrainExercise.SelectedItems[0].Tag.ToString());
                dataOperation.OperateData_sql("Delete from TB_TrainExercise where id = '" + id + "'");
                listView_TrainExercise.SelectedItems[0].Remove();
            }
            this.comMatter.SelectedIndex = -1;
            this.txtContent.Text = "";
            this.txtReporContent.Text = "";
            isEditOfT = false;
            MessageBox.Show("删除成功！", "提示");
        }
        /// <summary>
        /// 右键修改培训措施
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TSMI_EditTrainExercise_Click(object sender, EventArgs e)
        { 
            //string str = "";
            //for (int i = 0; i < listView_TrainExercise.SelectedItems[0].SubItems.Count; i++)
            //{
            //    str = str + "||" + listView_TrainExercise.SelectedItems[0].SubItems[i].Text.ToString();
            //}
            //MessageBox.Show(str+listView_TrainExercise.SelectedItems[0].Tag.ToString());
            //return;
            tid = listView_TrainExercise.SelectedItems[0].Index.ToString();
            comMatter.Text = listView_TrainExercise.SelectedItems[0].SubItems[1].Text.ToString();
            dtpStartTime.Text = listView_TrainExercise.SelectedItems[0].SubItems[2].Text.ToString();
            dtpEndTime.Text = listView_TrainExercise.SelectedItems[0].SubItems[3].Text.ToString();
            txtContent.Text = listView_TrainExercise.SelectedItems[0].SubItems[4].Text.ToString();
            txtReporContent.Text = listView_TrainExercise.SelectedItems[0].SubItems[5].Text.ToString();

            isEditOfT = true; 
        }
        /// <summary>
        /// 培养措施中，对选中项改变的事件的处理，listView_TrainExercise有选中项时，鼠标可对其进行点击，无选中项时，鼠标不可对其点击
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void listView_TrainExercise_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listView_TrainExercise.SelectedItems.Count == 0)
            {
                TSMI_DeleteTrainExercise.Enabled = false;
                TSMI_EditTrainExercise.Enabled = false;
            }
            else
            {
                TSMI_DeleteTrainExercise.Enabled = true;
                TSMI_EditTrainExercise.Enabled = true;
            }
        }

        /// <summary>
        /// listView_TrainExercise上鼠标松开的事件处理，判断是否有选中的项，如果没有第一条设置为选中。
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void listView_TrainExercise_MouseUp(object sender, MouseEventArgs e)
        {
            if (listView_TrainExercise.SelectedItems.Count == 0 && listView_TrainExercise.Items.Count != 0)
            {
                listView_TrainExercise.Items[0].Selected = true;
            }
        }

        /// <summary>
        /// 文本框信息改变事件的处理，标记信息改变
        /// </summary>
        /// <param name="sender">degree3</param>
        /// <param name="e"></param>
        private void degree3_TextChanged(object sender, EventArgs e)
        {
            if(change)
            {
                remarkchange = true;
            }
        }

        /// <summary>
        /// 文本框信息改变事件的处理，标记信息改变
        /// </summary>
        /// <param name="sender">txtReporContent</param>
        /// <param name="e"></param>
        private void txtReporContent_TextChanged(object sender, EventArgs e)
        {
            if (change)
            {
                remarkchange = true;
            }
        }

        /// <summary>
        /// 注明文本框事件的处理，标记信息改变
        /// </summary>
        /// <param name="sender">note14</param>
        /// <param name="e"></param>
        private void note14_TextChanged(object sender, EventArgs e)
        {
            if (change)
            {
                remarkchange = true;
            }
        }

        /// <summary>
        /// 窗体关闭时事件的处理，当有信息改变时，提示是否保存。
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FrmInformationGathering_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (remarkchange)
            {
                switch (MessageBox.Show("是否保存当前信息？", "提示", MessageBoxButtons.YesNoCancel))
                {
                    case DialogResult.Yes:
                        SaveInfo(listViewCid);
                        break;
                    case DialogResult.Cancel:
                        e.Cancel = true;
                        break;
                }
            }
        }

       
    }
}










