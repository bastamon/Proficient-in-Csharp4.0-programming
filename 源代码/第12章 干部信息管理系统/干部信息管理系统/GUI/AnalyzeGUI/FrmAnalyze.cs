using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using HBMISR.Data;
using System.Data.SQLite;

namespace HBMISR.GUI.AnalyzeGUI
{
    /// <summary>
    /// 基本分析窗体
    /// </summary>
    public partial class FrmAnalyze : Form
    {
        ReadIni ini;
        string filepath;
        DataOperation data1 = new DataOperation(Application.StartupPath + "\\DB\\Record.db");
        DataOperation data2;
        DataTable dt6;
        DataOperation data3;
        string str1, str2, str3, str4;
        double per1, per2, per3, per4, per5, per6, per7, per8, per9, per10, per11, per12, per13, per14, per15, per16, per17, per18, per19, per20, per21, per22, per23, per24, per25, per26,
            per27, per28, per29, per30, per31, per32, per33, per34, per35, per36, per37, per38, per39, per40;
        int i1, i2, i3, i4, i5, i6;
        string sql1 = "";
        string sql2 = "";
        string sql3 = "";
        /// <summary>
        /// 参加理论培训的干部编号
        /// </summary>
        string[] arry1;
        /// <summary>
        /// 参加出国出境培训的干部编号
        /// </summary>
        string[] arry2;
        /// <summary>
        /// 参加理论培训超过90天的干部编号
        /// </summary>
        string[] arry3;
        /// <summary>
        /// 同时参加理论培训和出国出境培训的干部人数
        /// </summary>
        int i7 = 0;
        /// <summary>
        /// 同时参加理论培训和出国出境培训并且理论培训大于90天的干部人数
        /// </summary>
        int i8 = 0;
        int fg = -1, wg = -1, fe = -1, we = -1;
        /// <summary>
        /// 基本分析窗体构造函数
        /// </summary>
        public FrmAnalyze()
        {
            InitializeComponent();
        }
        /// <summary>
        /// 基本分析窗体初始化
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Analyze_Load(object sender, EventArgs e)
        {
            ini = new ReadIni();
            data3 = new DataOperation(ini.ReadString("filePath"));
            filepath = ini.ReadString("fielPath");
            data2 = new DataOperation();
            //获取当前时间               
            System.DateTime dateTimes = System.DateTime.Now;
            Time_LS.Text = dateTimes.ToString("yyyy-MM-dd");
            button_search_Click(0);
        }

        /// <summary>
        /// 查询信息
        /// </summary>
        /// <param name="qd"></param>
        public void button_search_Click(int qd)
        {
            //统计一个单位
            #region
            //获取总人数                                                                       
            if (qd == -1)
                sql1 = "select count(*) from TB_CommonInfo where isDelete='0'";
            else
                sql1 = "select count(*) from TB_CommonInfo where isDelete='0' and qd='" + qd + "'";
            sql3 = "select count(*) from TB_TrainExercise ";

            string count_people = data3.GetRows(sql1).ToString(); //获取后备干部总人数

            if (Convert.ToInt32(count_people) == 0)
            {
                Count_LS.Text = "0";
                JionTeam_LS.Text = "0(0%)";
                Female_LS.Text = "0(0%)";
                Minority_LS.Text = "0(0%)";
                NoParaty_LS.Text = "0(0%)";
                Mall_LS.Text = "0(0%)";
                SubMall_LS.Text = "0(0%)";
                Country_LS.Text = "0(0%)";
                SubCountry_LS.Text = "0(0%)";
                Unit_LS.Text = "0(0%)";
                SubUnit_LS.Text = "0(0%)";
                Other_LS.Text = "0(0%)";

                FiZ_LS.Text = "0(0%)";
                FoF_LS.Text = "0(0%)";
                FoZ_LS.Text = "0(0%)";
                TiF_LS.Text = "0(0%)";
                TiT_LS.Text = "0(0%)";
                TiZ_LS.Text = "0(0%)";
                lbl_abroad.Text = "0(0%)";
                lbl_bachelor.Text = "0(0%)";
                lbl_basic.Text = "0(0%)";
                lbl_change.Text = "0(0%)";

                lbl_Fhigh.Text = "0(0%)";
                lbl_juniorcollege.Text = "0(0%)";
                lbl_master.Text = "0(0%)";
                lbl_phd.Text = "0(0%)";
                lbl_postgraduate.Text = "0(0%)";
                lbl_principal.Text = "0(0%)";

                lbl_technical.Text = "0(0%)";
                lbl_temppost.Text = "0(0%)";
                lbl_theory.Text = "0(0%)";
                lbl_turn.Text = "0(0%)";
                lbl_undergraduate.Text = "0(0%)";
                lbl_Zhigh.Text = "0(0%)";
                lbl_Fhigh.Text = "0(0%)";
                lbl_leader.Text = "0(0%)";
                lbl_public.Text = "0(0%)";
                lbl_worker.Text = "0(0%)";

                lbl_fullpost.Text = "0(0%)";
                lbl_fullunder.Text = "0(0%)";
                lbl_fulljunior.Text = "0(0%)";
                lbl_fulltechnical.Text = "0(0%)";
            }
            else
            {
                Count_LS.Text = count_people;//总人数
                //近期可提拔使用人数
                JionTeam_LS.Text = data3.GetRows(sql1 + " and joinTeam='1'").ToString();
                per1 = Convert.ToDouble(JionTeam_LS.Text) / Convert.ToDouble(count_people);
                JionTeam_LS.Text += "(" + Math.Round(per1, 3) * 100 + "%" + ")";
                //获取女干部人数
                Female_LS.Text = data3.GetRows(sql1 + " and sex='女' ").ToString();
                per2 = Convert.ToDouble(Female_LS.Text) / Convert.ToDouble(count_people);
                Female_LS.Text += "(" + Math.Round(per2, 3) * 100 + "%" + ")";
                //获取少数民族的干部个数
                Minority_LS.Text = data3.GetRows(sql1 + " and nation!='汉族' ").ToString();
                per3 = Convert.ToDouble(Minority_LS.Text) / Convert.ToDouble(count_people);
                Minority_LS.Text += "(" + Math.Round(per3, 3) * 100 + "%" + ")";
                //获取非中共党员人数
                NoParaty_LS.Text = data3.GetRows(sql1 + " and partyClass!='中共' ").ToString();
                per10 = Convert.ToDouble(NoParaty_LS.Text) / Convert.ToDouble(count_people);
                NoParaty_LS.Text += "(" + Math.Round(per10, 3) * 100 + "%" + ")";
                //获取平均年龄
                #region
                int nowyear = System.DateTime.Now.Year;

                int month = System.DateTime.Now.Month;

                if (qd == -1)
                    sql2 = "select * from TB_CommonInfo where isDelete='0' ";
                else
                    sql2 = "select * from TB_CommonInfo where isDelete='0' and qd='" + qd + "' ";

                DataTable dt2 = data3.GetOneDataTable_sql(sql2);  //新建一个datatable

                int year = 0;
                for (int i = 0; i < dt2.Rows.Count; i++)
                {
                    year = 0;
                    //获取出生年份
                    str2 = dt2.Rows[i]["birthday"].ToString().Substring(0, 4);
                    //出生月份
                    str3 = dt2.Rows[i]["birthday"].ToString().Substring(5, 2);

                    if (Convert.ToInt32(str3) > month)
                    {
                        year = nowyear - Convert.ToInt32(str2) - 1;
                        if (year >= 51)
                            i1++;
                        if (year >= 46 && year <= 50)
                            i2++;
                        if (year >= 41 && year <= 45)
                            i3++;
                        if (year >= 36 && year <= 40)
                            i4++;
                        if (year >= 31 && year <= 35)
                            i5++;
                        if (year <= 30)
                            i6++;
                        //if (year > 45 && year <= 50)
                        //    i1++;
                        //if (year > 40 && year <= 45)
                        //    i2++;
                        //if (year > 35 && year <= 40)
                        //    i3++;
                        //if (year > 33 && year <= 35)
                        //    i4++;
                        //if (year > 30 && year <= 33)
                        //    i5++;
                        //if (year <= 30)
                        //    i6++;
                    }
                    else
                    {
                        year = nowyear - Convert.ToInt32(str2);
                        if (year >= 51)
                            i1++;
                        if (year >= 46 && year <= 50)
                            i2++;
                        if (year >= 41 && year <= 45)
                            i3++;
                        if (year >= 36 && year <= 40)
                            i4++;
                        if (year >= 31 && year <= 35)
                            i5++;
                        if (year <= 30)
                            i6++;
                        //if (year > 45 && year <= 50)
                        //    i1++;
                        //if (year > 40 && year <= 45)
                        //    i2++;
                        //if (year > 35 && year <= 40)
                        //    i3++;
                        //if (year > 33 && year <= 35)
                        //    i4++;
                        //if (year > 30 & year <= 33)
                        //    i5++;
                        //if (year <= 30)
                        //    i6++;
                    }
                }
                ////50岁以下
                //per4 = Convert.ToDouble(i1) / Convert.ToDouble(count_people);
                //FiZ_LS.Text = i1.ToString() + "(" + Math.Round(per4, 3) * 100 + "%" + ")";
                ////45岁以下
                //per5 = Convert.ToDouble(i2) / Convert.ToDouble(count_people);
                //FoF_LS.Text = i2.ToString() + "(" + Math.Round(per5, 3) * 100 + "%" + ")";
                ////40岁以下
                //per6 = Convert.ToDouble(i3) / Convert.ToDouble(count_people);
                //FoZ_LS.Text = i3.ToString() + "(" + Math.Round(per6, 3) * 100 + "%" + ")";
                ////35岁以下
                //per7 = Convert.ToDouble(i4) / Convert.ToDouble(count_people);
                //TiF_LS.Text = i4.ToString() + "(" + Math.Round(per7, 3) * 100 + "%" + ")";
                ////33岁以下
                //per8 = Convert.ToDouble(i5) / Convert.ToDouble(count_people);
                //TiT_LS.Text = i5.ToString() + "(" + Math.Round(per8, 3) * 100 + "%" + ")";
                ////30岁以下
                //per9 = Convert.ToDouble(i6) / Convert.ToDouble(count_people);
                //TiZ_LS.Text = i6.ToString() + "(" + Math.Round(per9, 3) * 100 + "%" + ")";
                //51岁及以上
                per4 = Convert.ToDouble(i1) / Convert.ToDouble(count_people);
                FiZ_LS.Text = i1.ToString() + "(" + Math.Round(per4, 3) * 100 + "%" + ")";
                //46-50
                per5 = Convert.ToDouble(i2) / Convert.ToDouble(count_people);
                FoF_LS.Text = i2.ToString() + "(" + Math.Round(per5, 3) * 100 + "%" + ")";
                //41-45
                per6 = Convert.ToDouble(i3) / Convert.ToDouble(count_people);
                FoZ_LS.Text = i3.ToString() + "(" + Math.Round(per6, 3) * 100 + "%" + ")";
                //36-40
                per7 = Convert.ToDouble(i4) / Convert.ToDouble(count_people);
                TiF_LS.Text = i4.ToString() + "(" + Math.Round(per7, 3) * 100 + "%" + ")";
                //31-35
                per8 = Convert.ToDouble(i5) / Convert.ToDouble(count_people);
                TiT_LS.Text = i5.ToString() + "(" + Math.Round(per8, 3) * 100 + "%" + ")";
                //30岁及以下
                per9 = Convert.ToDouble(i6) / Convert.ToDouble(count_people);
                TiZ_LS.Text = i6.ToString() + "(" + Math.Round(per9, 3) * 100 + "%" + ")";
                i1 = 0;
                i2 = 0;
                i3 = 0;
                i4 = 0;
                i5 = 0;
                i6 = 0;
                #endregion
                //学位情况
                #region
                int all = Convert.ToInt32(count_people);
                int num_bachelor = 0, num_master = 0, num_phd = 0;
                DataTable dt = data3.GetOneDataTable_sql(sql2);
                for (int i = 0; i < all; i++)
                {
                    fg = -1;
                    wg = -1;
                    string s1 = dt.Rows[i]["fullDegree"].ToString();
                    string s2 = dt.Rows[i]["workDegree"].ToString();
                    if (s1 != "" || s2 != "")
                    {
                        if (s1 == "学士")
                            fg = 0;
                        if (s1 == "硕士")
                            fg = 1;
                        if (s1 == "博士")
                            fg = 2;
                        if (s2 == "学士")
                            wg = 0;
                        if (s2 == "硕士")
                            wg = 1;
                        if (s2 == "博士")
                            wg = 2;
                        if (wg >= fg)
                        {
                            if (wg == 0)
                            {
                                num_bachelor++;
                            }
                            else if (wg == 1)
                            {
                                num_master++;
                            }
                            else if (wg == 2)
                            {
                                num_phd++;
                            }
                        }
                        else
                        {
                            if (fg == 0)
                            {
                                num_bachelor++;
                            }
                            else if (fg == 1)
                            {
                                num_master++;
                            }
                            else if (fg == 2)
                            {
                                num_phd++;
                            }
                        }
                    }
                }
                lbl_bachelor.Text = num_bachelor.ToString();//学士
                per18 = Convert.ToDouble(lbl_bachelor.Text) / Convert.ToDouble(count_people);
                lbl_bachelor.Text += "(" + Math.Round(per18, 3) * 100 + "%" + ")";
                lbl_master.Text = num_master.ToString();//硕士
                per19 = Convert.ToDouble(lbl_master.Text) / Convert.ToDouble(count_people);
                lbl_master.Text += "(" + Math.Round(per19, 3) * 100 + "%" + ")";
                lbl_phd.Text = num_phd.ToString();//博士
                per20 = Convert.ToDouble(lbl_phd.Text) / Convert.ToDouble(count_people);
                lbl_phd.Text += "(" + Math.Round(per20, 3) * 100 + "%" + ")";
                num_bachelor = 0;
                num_master = 0;
                num_phd = 0;
                #endregion
                //最高学历
                #region
                //研究生
                //记录研究生、大学本科、大学专科、中专及以下人数
                int n4 = 0, n5 = 0, n6 = 0, n7 = 0;
                for (int i = 0; i < all; i++)
                {
                    fe = -1;
                    we = -1;
                    str4 = "";

                    string s3 = dt.Rows[i]["fullEducation"].ToString();
                    string s4 = dt.Rows[i]["workEducation"].ToString();
                    if (s3 != "" || s4 != "")
                    {
                        if (s3 == "初中")
                            fe = 0;
                        if (s3 == "高中")
                            fe = 1;
                        if (s3 == "中专")
                            fe = 2;
                        if (s3 == "党校大专")
                            fe = 3;
                        if (s3 == "大学专科")
                            fe = 4;
                        if (s3 == "大普")
                            fe = 5;
                        if (s3 == "党校大学")
                            fe = 6;
                        if (s3 == "大学本科")
                            fe = 7;
                        if (s3 == "党校研究生")
                            fe = 8;
                        if (s3 == "研究生")
                            fe = 9;

                        if (s4 == "初中")
                            we = 0;
                        if (s4 == "高中")
                            we = 1;
                        if (s4 == "中专")
                            we = 2;
                        if (s4 == "党校大专")
                            we = 3;
                        if (s4 == "大学专科")
                            we = 4;
                        if (s4 == "大普")
                            we = 5;
                        if (s4 == "党校大学")
                            we = 6;
                        if (s4 == "大学本科")
                            we = 7;
                        if (s4 == "党校研究生")
                            we = 8;
                        if (s4 == "研究生")
                            we = 9;

                        int x = fe;
                        //比较找出最高学历
                        #region
                        if (we > fe)
                        {
                            x = we;
                        }

                        if (x <= 2)
                            n4++;
                        if (2 < x && x <= 5)
                            n5++;
                        if (5 < x && x <= 7)
                            n6++;
                        if (x > 7)
                            n7++;
                        #endregion
                    }
                }
                lbl_postgraduate.Text = n7.ToString();//研究生
                per21 = Convert.ToDouble(lbl_postgraduate.Text) / Convert.ToDouble(count_people);
                lbl_postgraduate.Text += "(" + Math.Round(per21, 3) * 100 + "%" + ")";
                lbl_undergraduate.Text = n6.ToString();//大学本科
                per22 = Convert.ToDouble(lbl_undergraduate.Text) / Convert.ToDouble(count_people);
                lbl_undergraduate.Text += "(" + Math.Round(per22, 3) * 100 + "%" + ")";
                lbl_juniorcollege.Text = n5.ToString();//大学专科
                per23 = Convert.ToDouble(lbl_juniorcollege.Text) / Convert.ToDouble(count_people);
                lbl_juniorcollege.Text += "(" + Math.Round(per23, 3) * 100 + "%" + ")";
                lbl_technical.Text = n4.ToString();// 中专及其以下
                per24 = Convert.ToDouble(lbl_technical.Text) / Convert.ToDouble(count_people);
                lbl_technical.Text += "(" + Math.Round(per24, 3) * 100 + "%" + ")";
                n4 = 0;
                n5 = 0;
                n6 = 0;
                n7 = 0;
                #endregion
                //全日制学历
                #region
                lbl_fullpost.Text = data3.GetRows(sql1 + "and fullEducation='研究生'").ToString();
                per27 = Convert.ToDouble(lbl_fullpost.Text) / Convert.ToDouble(count_people);
                lbl_fullpost.Text += "(" + Math.Round(per27, 3) * 100 + "%" + ")";
                lbl_fullunder.Text = (data3.GetRows(sql1 + "and fullEducation='大学本科'") + data3.GetRows(sql1 + "and fullEducation='党校大学'") + data3.GetRows(sql1 + "and fullEducation='大普'")).ToString();
                per28 = Convert.ToDouble(lbl_fullunder.Text) / Convert.ToDouble(count_people);
                lbl_fullunder.Text += "(" + Math.Round(per28, 3) * 100 + "%" + ")";
                lbl_fulljunior.Text = (data3.GetRows(sql1 + "and fullEducation='大学专科'") + data3.GetRows(sql1 + "and fullEducation='党校大专'")).ToString();
                per29 = Convert.ToDouble(lbl_fulljunior.Text) / Convert.ToDouble(count_people);
                lbl_fulljunior.Text += "(" + Math.Round(per29, 3) * 100 + "%" + ")";
                lbl_fulltechnical.Text = (data3.GetRows(sql1 + "and fullEducation='中专'") + data3.GetRows(sql1 + "and fullEducation='高中'") + data3.GetRows(sql1 + "and fullEducation='初中'")).ToString();
                per37 = Convert.ToDouble(lbl_fulltechnical.Text) / Convert.ToDouble(count_people);
                lbl_fulltechnical.Text += "(" + Math.Round(per37, 3) * 100 + "%" + ")";
                #endregion
                //级别
                #region
                //正厅级
                Mall_LS.Text = data3.GetRows(sql1 + " and grade='8'").ToString();
                per11 = Convert.ToDouble(Mall_LS.Text) / Convert.ToDouble(count_people);
                Mall_LS.Text += "(" + Math.Round(per11, 3) * 100 + "%" + ")";
                //副厅级
                SubMall_LS.Text = data3.GetRows(sql1 + " and grade='7'").ToString();
                per12 = Convert.ToDouble(SubMall_LS.Text) / Convert.ToDouble(count_people);
                SubMall_LS.Text += "(" + Math.Round(per12, 3) * 100 + "%" + ")";
                //正县处级
                Country_LS.Text = data3.GetRows(sql1 + " and grade='6'").ToString();
                per13 = Convert.ToDouble(Country_LS.Text) / Convert.ToDouble(count_people);
                Country_LS.Text += "(" + Math.Round(per13, 3) * 100 + "%" + ")";
                //副县处级
                SubCountry_LS.Text = data3.GetRows(sql1 + " and grade='5'").ToString();
                per14 = Convert.ToDouble(SubCountry_LS.Text) / Convert.ToDouble(count_people);
                SubCountry_LS.Text += "(" + Math.Round(per14, 3) * 100 + "%" + ")";
                //正科级
                Unit_LS.Text = data3.GetRows(sql1 + " and grade='4'").ToString();
                per15 = Convert.ToDouble(Unit_LS.Text) / Convert.ToDouble(count_people);
                Unit_LS.Text += "(" + Math.Round(per15, 3) * 100 + "%" + ")";
                //副科级
                SubUnit_LS.Text = data3.GetRows(sql1 + " and grade='3'").ToString();
                per16 = Convert.ToDouble(SubUnit_LS.Text) / Convert.ToDouble(count_people);
                SubUnit_LS.Text += "(" + Math.Round(per16, 3) * 100 + "%" + ")";
                //科员
                Other_LS.Text = data3.GetRows(sql1 + " and grade='2'").ToString();
                per17 = Convert.ToDouble(Other_LS.Text) / Convert.ToDouble(count_people);
                Other_LS.Text += "(" + Math.Round(per17, 3) * 100 + "%" + ")";
                //办事员
                lbl_worker.Text = data3.GetRows(sql1 + " and grade='1'").ToString();
                per40 = Convert.ToDouble(lbl_worker.Text) / Convert.ToDouble(count_people);
                lbl_worker.Text += "(" + Math.Round(per40, 3) * 100 + "%" + ")";
                #endregion
                //其他
                #region
                //两年以上基层工作经历
                lbl_basic.Text = data3.GetRows(sql1 + " and isTwoYear='1'").ToString();
                per25 = Convert.ToDouble(lbl_basic.Text) / Convert.ToDouble(count_people);
                lbl_basic.Text += "(" + Math.Round(per25, 3) * 100 + "%" + ")";
                //企业、高校、教研所正职任职经历
                lbl_principal.Text = data3.GetRows(sql1 + "and isGuide='1'").ToString();
                per26 = Convert.ToDouble(lbl_principal.Text) / Convert.ToDouble(count_people);
                lbl_principal.Text += "(" + Math.Round(per26, 3) * 100 + "%" + ")";
                if (qd == -1)
                    dt6 = data3.GetOneDataTable_sql("select CID from TB_CommonInfo ");
                else
                    dt6 = data3.GetOneDataTable_sql("select CID from TB_CommonInfo where qd='" + qd + "' ");
                int totalDays = 0;
                int n8 = 0;
                arry1 = new string[dt6.Rows.Count];
                arry3 = new string[dt6.Rows.Count];
                for (int h = 0; h < dt6.Rows.Count; h++)
                {
                    int i = 0;
                    DataTable dt7 = new DataTable();
                    try
                    {
                        int l = data3.GetRows(sql3);

                        if (l != 0)
                        {
                            dt7 = data3.GetOneDataTable_sql("select startTime,endTime from TB_TrainExercise where CID='" + dt6.Rows[h]["CID"] + "' and reportMatter='参加培训情况'" +
                            "and reportContent='参加理论培训'");
                            for (int m = 0; m < dt7.Rows.Count; m++)
                            {
                                DateTime vStart = DateTime.Parse(dt7.Rows[m]["startTime"].ToString());
                                DateTime vEnd = DateTime.Parse(dt7.Rows[m]["endTime"].ToString());
                                TimeSpan vTimeSpan = new TimeSpan(vEnd.Ticks - vStart.Ticks);
                                totalDays += (int)vTimeSpan.TotalDays + 1;
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("参加理论培训和实践锻炼情况表中无内容！" + ex.Message);
                    }
                    if (totalDays / 90 >= 1)
                    {
                        n8++;
                        totalDays = 0;
                        arry3[i++] = dt6.Rows[h]["CID"].ToString();
                    }
                }
                //出国培训
                DataTable dt1 = data3.GetOneDataTable_sql("select distinct CID from TB_TrainExercise where reportMatter='参加培训情况' and reportContent='出国出境培训'");
                per32 = Convert.ToDouble(dt1.Rows.Count) / Convert.ToDouble(count_people);
                lbl_abroad.Text = dt1.Rows.Count.ToString() + "(" + Math.Round(per32, 3) * 100 + "%" + ")";
                //参加理论培训
                for (int j = 0; j < dt1.Rows.Count; j++)
                {
                    for (int h = 0; h < arry3.Length; h++)
                    {
                        if (dt1.Rows[j]["CID"].ToString().Equals(arry3[h]))
                        {
                            i8++;
                        }
                    }
                }
                n8 = n8 + dt1.Rows.Count - i8;
                per31 = Convert.ToDouble(n8) / Convert.ToDouble(count_people);
                lbl_theory.Text = n8.ToString() + "(" + Math.Round(per31, 3) * 100 + "%" + ")";
                n8 = 0;
                DataTable dt5 = data3.GetOneDataTable_sql("select distinct CID from TB_TrainExercise where reportMatter='参加实践锻炼情况' and reportContent='本单位岗位轮换'");
                per30 = Convert.ToDouble(dt5.Rows.Count) / Convert.ToDouble(count_people);
                lbl_turn.Text = dt5.Rows.Count.ToString() + "(" + Math.Round(per30, 3) * 100 + "%" + ")";
                DataTable dt3 = data3.GetOneDataTable_sql("select distinct CID from TB_TrainExercise where  reportMatter='参加实践锻炼情况' and reportContent='交流任职'");
                per33 = Convert.ToDouble(dt3.Rows.Count) / Convert.ToDouble(count_people);
                lbl_change.Text = dt3.Rows.Count.ToString() + "(" + Math.Round(per33, 3) * 100 + "%" + ")";
                DataTable dt4 = data3.GetOneDataTable_sql("select distinct CID from TB_TrainExercise where  reportMatter='参加实践锻炼情况' and reportContent='挂职锻炼'");
                per34 = Convert.ToDouble(dt4.Rows.Count) / Convert.ToDouble(count_people);
                lbl_temppost.Text = dt4.Rows.Count.ToString() + "(" + Math.Round(per34, 3) * 100 + "%" + ")";
                lbl_Zhigh.Text = data3.GetRows(sql1 + " and spdegree='正高'").ToString();
                per35 = Convert.ToDouble(lbl_Zhigh.Text) / Convert.ToDouble(count_people);
                lbl_Zhigh.Text += "(" + Math.Round(per35, 3) * 100 + "%" + ")";
                lbl_Fhigh.Text = data3.GetRows(sql1 + " and spdegree='副高'").ToString();
                per36 = Convert.ToDouble(lbl_Fhigh.Text) / Convert.ToDouble(count_people);
                lbl_Fhigh.Text += "(" + Math.Round(per36, 3) * 100 + "%" + ")";
                lbl_leader.Text = data3.GetRows(sql1 + " and twoYGE='1'").ToString();
                per38 = Convert.ToDouble(lbl_leader.Text) / Convert.ToDouble(count_people);
                lbl_leader.Text += "(" + Math.Round(per38, 3) * 100 + "%" + ")";
                lbl_public.Text = data3.GetRows(sql1 + " and publicSelect='1'").ToString();//公开选拔产生  百分比
                per39 = Convert.ToDouble(lbl_public.Text) / Convert.ToDouble(count_people);//  公开选拔总人数/总人数
                lbl_public.Text += "(" + Math.Round(per39, 3) * 100 + "%" + ")";
                #endregion
                i1 = 0;
                i2 = 0;
                i3 = 0;
                i4 = 0;
                i5 = 0;
                i6 = 0;
            }
            #endregion
        }

        /// <summary>
        /// 正副职选择变化的事件监听
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void rdb_CheckedChanged(object sender, EventArgs e)
        {
            if (rdb.Checked)
            {
                button_search_Click(-1);
            }
        }

        /// <summary>
        /// 正副职选择变化的事件监听
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void rdb1_CheckedChanged(object sender, EventArgs e)
        {
            if (rdb1.Checked)
            {
                button_search_Click(1);
            }
        }

        /// <summary>
        /// 正副职选择变化的事件监听
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void rdb0_CheckedChanged(object sender, EventArgs e)
        {
            if (rdb0.Checked)
            {
                button_search_Click(0);
            }
        }
    }
}
