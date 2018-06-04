using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Collections;
using Microsoft.Reporting.WinForms;
using HBMISR.Service;
using HBMISR.Data;


namespace HBMISR.GUI.PrintGUI
{
  
    /// <summary>
    /// 用来显示初步人选名册
    /// </summary>
    public partial class C_HBNameList : UserControl
    {       
        /// <summary>
        /// //记录需要打印的后备干部编号
        /// </summary>
        public ArrayList idlist = new ArrayList();

        /// <summary>
        /// //单位名称
        /// </summary>
        private string unit;
        public string Unit
        {
            get { return unit; }
            set { unit = value; }
        }

        /// <summary>
        /// //单位类别
        /// </summary>
        private string unitclass;
        public string Unitclass
        {
            get { return unitclass; }
            set { unitclass = value; }
        }

        /// <summary>
        /// //正副职
        /// </summary>
        private string qd;
        public string Qd
        {
            get { return qd; }
            set { qd = value; }
        }

        /// <summary>
        /// /\ 用来显示初步人选名册构造函数
      /// </summary>
        public C_HBNameList()
        {
            InitializeComponent();
        }

        /// <summary>
        ///  用来显示初步人选名册初始化
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void C_HBNameList_Load(object sender, EventArgs e)
        {            
            this.RV_HBNameList.SetDisplayMode(DisplayMode.PrintLayout);
            #region
            int i = 0;
            string selectid = "";
            for (i = 0; i < idlist.Count; i++)
            {
                if (i == idlist.Count - 1)
                    selectid = selectid + "'" + idlist[i] + "'";
                else
                    selectid = selectid + "'" + idlist[i] + "',";
            }

            //读取后备干部的相关信息
            //string sql = "select name,sex,nation,department,position,native,birthday,age,partyTime,workTime,fullEducation,fullDegree,fullSchool,fullSpecialty,workEducation,workDegree,workGraduate,workSpecialty,technicalPost,joinTeam,experiencePost,knowField,trainDirection,trainMeasure,partyClass from TB_CommonInfo where cid in (" + selectid + ")  order by joinTeam desc";
                       
            //DataOperation dataOp = new DataOperation();
            //DataTable datatable = dataOp.GetOneDataTable_sql(sql);
             

            string sql = "select name,cid,sex,nation,department,position,native,birthday,age,partyTime,workTime,fullEducation,fullDegree,fullSchool,fullSpecialty,workEducation,workDegree,workGraduate,workSpecialty,technicalPost,joinTeam,experiencePost,knowField,trainDirection,trainMeasure,partyClass from TB_CommonInfo order by rank, joinTeam desc";

            string cond="cid in ("+selectid+")";
            DataOperation dataOp = new DataOperation();
            DataTable datatable1 = dataOp.GetOneDataTable_sql(sql);
            DataTable datatable = new DataTable();
            datatable = datatable1.Clone();
            DataView dv = datatable1.AsDataView();
            dv.RowFilter = cond;
            datatable = dv.ToTable();

            //DataRow[] rows = datatable1.Select(cond);
            //foreach (DataRow row in rows)  // 将查询的结果添加到dt中；
            //{
            //    datatable.Rows.Add(row.ItemArray);
            //}
            #endregion

            List<Class_HBNameList> HBNameList = new List<Class_HBNameList>();

            //一页五条信息，五条五条信息输出
            for (i = 0; i < (datatable.Rows.Count / 5) * 5; i = i + 5)
            {
                Class_HBNameList c = new Class_HBNameList();

                c.Pagecount = i.ToString();

                //第i个人的信息
                #region
                string strS = "", strN = "";
                string Remarks = "";
                if (datatable.Rows[i]["sex"].ToString().Trim() != "男")
                {
                    strS = datatable.Rows[i]["sex"].ToString().Trim();
                }
                if (datatable.Rows[i]["nation"].ToString().Trim() != "汉族")
                {
                    strN = datatable.Rows[i]["nation"].ToString().Trim();
                }
                if (strS != "" && strN == "")
                {
                    Remarks = "\n(" + strS + ")";
                }
                else
                    if (strS != "" && strN != "")
                    {
                        Remarks = "\n(" + strS + "、" + strN + ")";
                    }
                    else
                        if (strS == "" && strN != "")
                        {
                            Remarks = "\n(" + strN + ")";
                        }
                c.Hbname1 = datatable.Rows[i]["name"].ToString() + Remarks;
                //c.Hbname1 = datatable.Rows[i]["name"].ToString() + "\n(" + datatable.Rows[i]["sex"].ToString() + "、" + datatable.Rows[i]["nation"].ToString() + ")";
                c.Unitjop1 = datatable.Rows[i]["department"].ToString() + "\n" + datatable.Rows[i]["position"].ToString();
                c.Native1 = datatable.Rows[i]["native"].ToString();
                c.Birthday1 = todate(datatable.Rows[i]["birthday"].ToString());
                c.PartyTime1 = todate(datatable.Rows[i]["partyTime"].ToString());
                c.WorkTime1 = todate(datatable.Rows[i]["workTime"].ToString());
                c.FullED1 = isAddEnter(datatable.Rows[i]["fullEducation"].ToString(), datatable.Rows[i]["fullDegree"].ToString());
                c.FullSS1 = isAddEnter(datatable.Rows[i]["fullSchool"].ToString(), datatable.Rows[i]["fullSpecialty"].ToString());
                c.WorkED1 = isAddEnter(datatable.Rows[i]["workEducation"].ToString(), datatable.Rows[i]["workDegree"].ToString());
                c.WorkSS1 = isAddEnter(datatable.Rows[i]["workGraduate"].ToString(), datatable.Rows[i]["workSpecialty"].ToString());
                c.TechnicalPost1 = datatable.Rows[i]["technicalPost"].ToString();
                c.ExperiencePost1 = datatable.Rows[i]["experiencePost"].ToString();
                c.KnowField1 = datatable.Rows[i]["knowField"].ToString();
                c.TrainDirection1 = datatable.Rows[i]["trainDirection"].ToString();
                c.TrainMeasure1 = datatable.Rows[i]["trainMeasure"].ToString();
                HBNameList.Add(c);
                #endregion

                //第i+1个人的信息
                #region
                Class_HBNameList c1 = new Class_HBNameList();
                string strS1 = "", strN1 = "";
                string Remarks1 = "";
                if (datatable.Rows[i + 1]["sex"].ToString().Trim() != "男")
                {
                    strS1 = datatable.Rows[i + 1]["sex"].ToString().Trim();
                }
                if (datatable.Rows[i + 1]["nation"].ToString().Trim() != "汉族")
                {
                    strN1 = datatable.Rows[i + 1]["nation"].ToString().Trim();
                }
                if (strS1 != "" && strN1 == "")
                {
                    Remarks1 = "\n(" + strS1 + ")";
                }
                else
                    if (strS1 != "" && strN1 != "")
                    {
                        Remarks1 = "\n(" + strS1 + "、" + strN1 + ")";
                    }
                    else
                        if (strS1 == "" && strN1 != "")
                        {
                            Remarks1 = "\n(" + strN1 + ")";
                        }
                c1.Hbname1 = datatable.Rows[i + 1]["name"].ToString() + Remarks1;
                c1.Unitjop1 = datatable.Rows[i + 1]["department"].ToString() + "\n" + datatable.Rows[i + 1]["position"].ToString();
                c1.Native1 = datatable.Rows[i + 1]["native"].ToString();
                c1.Birthday1 = todate(datatable.Rows[i + 1]["birthday"].ToString());
                c1.PartyTime1 = todate(datatable.Rows[i + 1]["partyTime"].ToString());
                c1.WorkTime1 = todate(datatable.Rows[i + 1]["workTime"].ToString());
                c1.FullED1 = isAddEnter(datatable.Rows[i + 1]["fullEducation"].ToString(), datatable.Rows[i + 1]["fullDegree"].ToString());
                c1.FullSS1 = isAddEnter(datatable.Rows[i + 1]["fullSchool"].ToString(), datatable.Rows[i + 1]["fullSpecialty"].ToString());
                c1.WorkED1 = isAddEnter(datatable.Rows[i + 1]["workEducation"].ToString(), datatable.Rows[i + 1]["workDegree"].ToString());
                c1.WorkSS1 = isAddEnter(datatable.Rows[i + 1]["workGraduate"].ToString(), datatable.Rows[i + 1]["workSpecialty"].ToString());
                c1.TechnicalPost1 = datatable.Rows[i + 1]["technicalPost"].ToString();
                c1.ExperiencePost1 = datatable.Rows[i + 1]["experiencePost"].ToString();
                c1.KnowField1 = datatable.Rows[i + 1]["knowField"].ToString();
                c1.TrainDirection1 = datatable.Rows[i + 1]["trainDirection"].ToString();
                c1.TrainMeasure1 = datatable.Rows[i + 1]["trainMeasure"].ToString();
                HBNameList.Add(c1);
                #endregion

                //第i+2个人的信息
                #region
                Class_HBNameList c2 = new Class_HBNameList();
                string strS2 = "", strN2 = "";
                string Remarks2 = "";
                if (datatable.Rows[i + 2]["sex"].ToString().Trim() != "男")
                {
                    strS2 = datatable.Rows[i + 2]["sex"].ToString().Trim();
                }
                if (datatable.Rows[i + 2]["nation"].ToString().Trim() != "汉族")
                {
                    strN2 = datatable.Rows[i + 2]["nation"].ToString().Trim();
                }
                if (strS2 != "" && strN2 == "")
                {
                    Remarks2 = "\n(" + strS2 + ")";
                }
                else
                    if (strS2 != "" && strN2 != "")
                    {
                        Remarks2 = "\n(" + strS2 + "、" + strN2 + ")";
                    }
                    else
                        if (strS2 == "" && strN2 != "")
                        {
                            Remarks2 = "\n(" + strN2 + ")";
                        }
                c2.Hbname1 = datatable.Rows[i + 2]["name"].ToString() + Remarks2;
                c2.Unitjop1 = datatable.Rows[i + 2]["department"].ToString() + "\n" + datatable.Rows[i + 2]["position"].ToString();
                c2.Native1 = datatable.Rows[i + 2]["native"].ToString();
                c2.Birthday1 = todate(datatable.Rows[i + 2]["birthday"].ToString());
                c2.PartyTime1 = todate(datatable.Rows[i + 2]["partyTime"].ToString());
                c2.WorkTime1 = todate(datatable.Rows[i + 2]["workTime"].ToString());
                c2.FullED1 = isAddEnter(datatable.Rows[i + 2]["fullEducation"].ToString(), datatable.Rows[i + 2]["fullDegree"].ToString());
                c2.FullSS1 = isAddEnter(datatable.Rows[i + 2]["fullSchool"].ToString(), datatable.Rows[i + 2]["fullSpecialty"].ToString());
                c2.WorkED1 = isAddEnter(datatable.Rows[i + 2]["workEducation"].ToString(), datatable.Rows[i + 2]["workDegree"].ToString());
                c2.WorkSS1 = isAddEnter(datatable.Rows[i + 2]["workGraduate"].ToString(), datatable.Rows[i + 2]["workSpecialty"].ToString());
                c2.TechnicalPost1 = datatable.Rows[i + 2]["technicalPost"].ToString();
                c2.ExperiencePost1 = datatable.Rows[i + 2]["experiencePost"].ToString();
                c2.KnowField1 = datatable.Rows[i + 2]["knowField"].ToString();
                c2.TrainDirection1 = datatable.Rows[i + 2]["trainDirection"].ToString();
                c2.TrainMeasure1 = datatable.Rows[i + 2]["trainMeasure"].ToString();
                HBNameList.Add(c2);
                #endregion

                //第i+3个人的信息
                #region
                Class_HBNameList c3 = new Class_HBNameList();
                string strS3 = "", strN3 = "";
                string Remarks3 = "";
                if (datatable.Rows[i + 3]["sex"].ToString().Trim() != "男")
                {
                    strS3 = datatable.Rows[i + 3]["sex"].ToString().Trim();
                }
                if (datatable.Rows[i + 3]["nation"].ToString().Trim() != "汉族")
                {
                    strN3 = datatable.Rows[i + 3]["nation"].ToString().Trim();
                }
                if (strS3 != "" && strN3 == "")
                {
                    Remarks3 = "\n(" + strS3 + ")";
                }
                else
                    if (strS3 != "" && strN3 != "")
                    {
                        Remarks3 = "\n(" + strS3 + "、" + strN3 + ")";
                    }
                    else
                        if (strS3 == "" && strN3 != "")
                        {
                            Remarks3 = "\n(" + strN3 + ")";
                        }
                c3.Hbname1 = datatable.Rows[i + 3]["name"].ToString() + Remarks3;
                c3.Unitjop1 = datatable.Rows[i + 3]["department"].ToString() + "\n" + datatable.Rows[i + 3]["position"].ToString();
                c3.Native1 = datatable.Rows[i + 3]["native"].ToString();
                c3.Birthday1 = todate(datatable.Rows[i + 3]["birthday"].ToString());
                c3.PartyTime1 = todate(datatable.Rows[i + 3]["partyTime"].ToString());
                c3.WorkTime1 = todate(datatable.Rows[i + 3]["workTime"].ToString());
                c3.FullED1 = isAddEnter(datatable.Rows[i + 3]["fullEducation"].ToString(), datatable.Rows[i + 3]["fullDegree"].ToString());
                c3.FullSS1 = isAddEnter(datatable.Rows[i + 3]["fullSchool"].ToString(), datatable.Rows[i + 3]["fullSpecialty"].ToString());
                c3.WorkED1 = isAddEnter(datatable.Rows[i + 3]["workEducation"].ToString(), datatable.Rows[i + 3]["workDegree"].ToString());
                c3.WorkSS1 = isAddEnter(datatable.Rows[i + 3]["workGraduate"].ToString(), datatable.Rows[i + 3]["workSpecialty"].ToString());
                c3.TechnicalPost1 = datatable.Rows[i + 3]["technicalPost"].ToString();
                c3.ExperiencePost1 = datatable.Rows[i + 3]["experiencePost"].ToString();
                c3.KnowField1 = datatable.Rows[i + 3]["knowField"].ToString();
                c3.TrainDirection1 = datatable.Rows[i + 3]["trainDirection"].ToString();
                c3.TrainMeasure1 = datatable.Rows[i + 3]["trainMeasure"].ToString();
                HBNameList.Add(c3);
                #endregion

                //第i+4个人的信息
                #region
                Class_HBNameList c4 = new Class_HBNameList();
                string strS4 = "", strN4 = "";
                string Remarks4 = "";
                if (datatable.Rows[i + 4]["sex"].ToString().Trim() != "男")
                {
                    strS4 = datatable.Rows[i + 4]["sex"].ToString().Trim();
                }
                if (datatable.Rows[i + 4]["nation"].ToString().Trim() != "汉族")
                {
                    strN4 = datatable.Rows[i + 4]["nation"].ToString().Trim();
                }
                if (strS4 != "" && strN4 == "")
                {
                    Remarks4 = "\n(" + strS4 + ")";
                }
                else
                    if (strS4 != "" && strN4 != "")
                    {
                        Remarks4 = "\n(" + strS4 + "、" + strN4 + ")";
                    }
                    else
                        if (strS4 == "" && strN4 != "")
                        {
                            Remarks4 = "\n(" + strN4 + ")";
                        }
                c4.Hbname1 = datatable.Rows[i + 4]["name"].ToString() + Remarks4;
                c4.Unitjop1 = datatable.Rows[i + 4]["department"].ToString() + "\n" + datatable.Rows[i + 4]["position"].ToString();
                c4.Native1 = datatable.Rows[i + 4]["native"].ToString();
                c4.Birthday1 = todate(datatable.Rows[i + 4]["birthday"].ToString());
                c4.PartyTime1 = todate(datatable.Rows[i + 4]["partyTime"].ToString());
                c4.WorkTime1 = todate(datatable.Rows[i + 4]["workTime"].ToString());
                c4.FullED1 = isAddEnter(datatable.Rows[i + 4]["fullEducation"].ToString(), datatable.Rows[i + 4]["fullDegree"].ToString());
                c4.FullSS1 = isAddEnter(datatable.Rows[i + 4]["fullSchool"].ToString(), datatable.Rows[i + 4]["fullSpecialty"].ToString());
                c4.WorkED1 = isAddEnter(datatable.Rows[i + 4]["workEducation"].ToString(), datatable.Rows[i + 4]["workDegree"].ToString());
                c4.WorkSS1 = isAddEnter(datatable.Rows[i + 4]["workGraduate"].ToString(), datatable.Rows[i + 4]["workSpecialty"].ToString());
                c4.TechnicalPost1 = datatable.Rows[i + 4]["technicalPost"].ToString();
                c4.ExperiencePost1 = datatable.Rows[i + 4]["experiencePost"].ToString();
                c4.KnowField1 = datatable.Rows[i + 4]["knowField"].ToString();
                c4.TrainDirection1 = datatable.Rows[i + 4]["trainDirection"].ToString();
                c4.TrainMeasure1 = datatable.Rows[i + 4]["trainMeasure"].ToString();
                HBNameList.Add(c4);
                #endregion
            }

            //将剩下的datatable.Rows.Count % 5 条信息输出
            //显示一个空表
            #region
            if (datatable.Rows.Count == 0)
            {
                Class_HBNameList c = new Class_HBNameList();

                c.Pagecount = i.ToString();

                HBNameList.Add(c);
            }
            #endregion
            #region
            if (datatable.Rows.Count % 5 == 1)
            {
                Class_HBNameList c = new Class_HBNameList();

                c.Pagecount = i.ToString();

                //第i个人的信息
                #region
                string strS = "", strN = "";
                string Remarks = "";
                if (datatable.Rows[i]["sex"].ToString().Trim() != "男")
                {
                    strS = datatable.Rows[i]["sex"].ToString().Trim();
                }
                if (datatable.Rows[i]["nation"].ToString().Trim() != "汉族")
                {
                    strN = datatable.Rows[i]["nation"].ToString().Trim();
                }
                if (strS != "" && strN == "")
                {
                    Remarks = "\n(" + strS + ")";
                }
                else
                    if (strS != "" && strN != "")
                    {
                        Remarks = "\n(" + strS + "、" + strN + ")";
                    }
                    else
                        if (strS == "" && strN != "")
                        {
                            Remarks = "\n(" + strN + ")";
                        }
                c.Hbname1 = datatable.Rows[i]["name"].ToString() + Remarks;
                c.Unitjop1 = datatable.Rows[i]["department"].ToString() + "\n" + datatable.Rows[i]["position"].ToString();
                c.Native1 = datatable.Rows[i]["native"].ToString();
                c.Birthday1 = todate(datatable.Rows[i]["birthday"].ToString());
                c.PartyTime1 = todate(datatable.Rows[i]["partyTime"].ToString());
                c.WorkTime1 = todate(datatable.Rows[i]["workTime"].ToString());
                c.FullED1 = isAddEnter(datatable.Rows[i]["fullEducation"].ToString(), datatable.Rows[i]["fullDegree"].ToString());
                c.FullSS1 = isAddEnter(datatable.Rows[i]["fullSchool"].ToString(), datatable.Rows[i]["fullSpecialty"].ToString());
                c.WorkED1 = isAddEnter(datatable.Rows[i]["workEducation"].ToString(), datatable.Rows[i]["workDegree"].ToString());
                c.WorkSS1 = isAddEnter(datatable.Rows[i]["workGraduate"].ToString(), datatable.Rows[i]["workSpecialty"].ToString());
                c.TechnicalPost1 = datatable.Rows[i]["technicalPost"].ToString();
                c.ExperiencePost1 = datatable.Rows[i]["experiencePost"].ToString();
                c.KnowField1 = datatable.Rows[i]["knowField"].ToString();
                c.TrainDirection1 = datatable.Rows[i]["trainDirection"].ToString();
                c.TrainMeasure1 = datatable.Rows[i]["trainMeasure"].ToString();
                HBNameList.Add(c);
                #endregion
            }
            #endregion

            #region
            if (datatable.Rows.Count % 5 == 2)
            {
                Class_HBNameList c = new Class_HBNameList();

                c.Pagecount = i.ToString();

                //第i个人的信息
                #region
                string strS = "", strN = "";
                string Remarks = "";
                if (datatable.Rows[i]["sex"].ToString().Trim() != "男")
                {
                    strS = datatable.Rows[i]["sex"].ToString().Trim();
                }
                if (datatable.Rows[i]["nation"].ToString().Trim() != "汉族")
                {
                    strN = datatable.Rows[i]["nation"].ToString().Trim();
                }
                if (strS != "" && strN == "")
                {
                    Remarks = "\n(" + strS + ")";
                }
                else
                    if (strS != "" && strN != "")
                    {
                        Remarks = "\n(" + strS + "、" + strN + ")";
                    }
                    else
                        if (strS == "" && strN != "")
                        {
                            Remarks = "\n(" + strN + ")";
                        }
                c.Hbname1 = datatable.Rows[i]["name"].ToString() + Remarks;
                c.Unitjop1 = datatable.Rows[i]["department"].ToString() + "\n" + datatable.Rows[i]["position"].ToString();
                c.Native1 = datatable.Rows[i]["native"].ToString();
                c.Birthday1 = todate(datatable.Rows[i]["birthday"].ToString());
                c.PartyTime1 = todate(datatable.Rows[i]["partyTime"].ToString());
                c.WorkTime1 = todate(datatable.Rows[i]["workTime"].ToString());
                c.FullED1 = isAddEnter(datatable.Rows[i]["fullEducation"].ToString(), datatable.Rows[i]["fullDegree"].ToString());
                c.FullSS1 = isAddEnter(datatable.Rows[i]["fullSchool"].ToString(), datatable.Rows[i]["fullSpecialty"].ToString());
                c.WorkED1 = isAddEnter(datatable.Rows[i]["workEducation"].ToString(), datatable.Rows[i]["workDegree"].ToString());
                c.WorkSS1 = isAddEnter(datatable.Rows[i]["workGraduate"].ToString(), datatable.Rows[i]["workSpecialty"].ToString());
                c.TechnicalPost1 = datatable.Rows[i]["technicalPost"].ToString();
                c.ExperiencePost1 = datatable.Rows[i]["experiencePost"].ToString();
                c.KnowField1 = datatable.Rows[i]["knowField"].ToString();
                c.TrainDirection1 = datatable.Rows[i]["trainDirection"].ToString();
                c.TrainMeasure1 = datatable.Rows[i]["trainMeasure"].ToString();
                HBNameList.Add(c);
                #endregion

                //第i+1个人的信息
                #region
                Class_HBNameList c1 = new Class_HBNameList();
                string strS1 = "", strN1 = "";
                string Remarks1 = "";
                if (datatable.Rows[i + 1]["sex"].ToString().Trim() != "男")
                {
                    strS1 = datatable.Rows[i + 1]["sex"].ToString().Trim();
                }
                if (datatable.Rows[i + 1]["nation"].ToString().Trim() != "汉族")
                {
                    strN1 = datatable.Rows[i + 1]["nation"].ToString().Trim();
                }
                if (strS1 != "" && strN1 == "")
                {
                    Remarks1 = "\n(" + strS1 + ")";
                }
                else
                    if (strS1 != "" && strN1 != "")
                    {
                        Remarks1 = "\n(" + strS1 + "、" + strN1 + ")";
                    }
                    else
                        if (strS1 == "" && strN1 != "")
                        {
                            Remarks1 = "\n(" + strN1 + ")";
                        }
                c1.Hbname1 = datatable.Rows[i + 1]["name"].ToString() + Remarks1;
                c1.Unitjop1 = datatable.Rows[i + 1]["department"].ToString() + "\n" + datatable.Rows[i + 1]["position"].ToString();
                c1.Native1 = datatable.Rows[i + 1]["native"].ToString();
                c1.Birthday1 = todate(datatable.Rows[i + 1]["birthday"].ToString());
                c1.PartyTime1 = todate(datatable.Rows[i + 1]["partyTime"].ToString());
                c1.WorkTime1 = todate(datatable.Rows[i + 1]["workTime"].ToString());
                c1.FullED1 = isAddEnter(datatable.Rows[i + 1]["fullEducation"].ToString(), datatable.Rows[i + 1]["fullDegree"].ToString());
                c1.FullSS1 = isAddEnter(datatable.Rows[i + 1]["fullSchool"].ToString(), datatable.Rows[i + 1]["fullSpecialty"].ToString());
                c1.WorkED1 = isAddEnter(datatable.Rows[i + 1]["workEducation"].ToString(), datatable.Rows[i + 1]["workDegree"].ToString());
                c1.WorkSS1 = isAddEnter(datatable.Rows[i + 1]["workGraduate"].ToString(), datatable.Rows[i + 1]["workSpecialty"].ToString());
                c1.TechnicalPost1 = datatable.Rows[i + 1]["technicalPost"].ToString();
                c1.ExperiencePost1 = datatable.Rows[i + 1]["experiencePost"].ToString();
                c1.KnowField1 = datatable.Rows[i + 1]["knowField"].ToString();
                c1.TrainDirection1 = datatable.Rows[i + 1]["trainDirection"].ToString();
                c1.TrainMeasure1 = datatable.Rows[i + 1]["trainMeasure"].ToString();
                HBNameList.Add(c1);
                #endregion
            }
            #endregion

            #region
            if (datatable.Rows.Count % 5 == 3)
            {
                Class_HBNameList c = new Class_HBNameList();
                c.Pagecount = i.ToString();

                //第i个人的信息
                #region
                string strS = "", strN = "";
                string Remarks = "";
                if (datatable.Rows[i]["sex"].ToString().Trim() != "男")
                {
                    strS = datatable.Rows[i]["sex"].ToString().Trim();
                }
                if (datatable.Rows[i]["nation"].ToString().Trim() != "汉族")
                {
                    strN = datatable.Rows[i]["nation"].ToString().Trim();
                }
                if (strS != "" && strN == "")
                {
                    Remarks = "\n(" + strS + ")";
                }
                else
                    if (strS != "" && strN != "")
                    {
                        Remarks = "\n(" + strS + "、" + strN + ")";
                    }
                    else
                        if (strS == "" && strN != "")
                        {
                            Remarks = "\n(" + strN + ")";
                        }
                c.Hbname1 = datatable.Rows[i]["name"].ToString() + Remarks;
                c.Unitjop1 = datatable.Rows[i]["department"].ToString() + "\n" + datatable.Rows[i]["position"].ToString();
                c.Native1 = datatable.Rows[i]["native"].ToString();
                c.Birthday1 = todate(datatable.Rows[i]["birthday"].ToString());
                c.PartyTime1 = todate(datatable.Rows[i]["partyTime"].ToString());
                c.WorkTime1 = todate(datatable.Rows[i]["workTime"].ToString());
                c.FullED1 = isAddEnter(datatable.Rows[i]["fullEducation"].ToString(), datatable.Rows[i]["fullDegree"].ToString());
                c.FullSS1 = isAddEnter(datatable.Rows[i]["fullSchool"].ToString(), datatable.Rows[i]["fullSpecialty"].ToString());
                c.WorkED1 = isAddEnter(datatable.Rows[i]["workEducation"].ToString(), datatable.Rows[i]["workDegree"].ToString());
                c.WorkSS1 = isAddEnter(datatable.Rows[i]["workGraduate"].ToString(), datatable.Rows[i]["workSpecialty"].ToString());
                c.TechnicalPost1 = datatable.Rows[i]["technicalPost"].ToString();
                c.ExperiencePost1 = datatable.Rows[i]["experiencePost"].ToString();
                c.KnowField1 = datatable.Rows[i]["knowField"].ToString();
                c.TrainDirection1 = datatable.Rows[i]["trainDirection"].ToString();
                c.TrainMeasure1 = datatable.Rows[i]["trainMeasure"].ToString();
                HBNameList.Add(c);
                #endregion
                Class_HBNameList c1 = new Class_HBNameList();
                //第i+1个人的信息
                #region
                string strS1 = "", strN1 = "";
                string Remarks1 = "";
                if (datatable.Rows[i + 1]["sex"].ToString().Trim() != "男")
                {
                    strS1 = datatable.Rows[i + 1]["sex"].ToString().Trim();
                }
                if (datatable.Rows[i + 1]["nation"].ToString().Trim() != "汉族")
                {
                    strN1 = datatable.Rows[i + 1]["nation"].ToString().Trim();
                }
                if (strS1 != "" && strN1 == "")
                {
                    Remarks1 = "\n(" + strS1 + ")";
                }
                else
                    if (strS1 != "" && strN1 != "")
                    {
                        Remarks1 = "\n(" + strS1 + "、" + strN1 + ")";
                    }
                    else
                        if (strS1 == "" && strN1 != "")
                        {
                            Remarks1 = "\n(" + strN1 + ")";
                        }
                c1.Hbname1 = datatable.Rows[i + 1]["name"].ToString() + Remarks1;
                c1.Unitjop1 = datatable.Rows[i + 1]["department"].ToString() + "\n" + datatable.Rows[i + 1]["position"].ToString();
                c1.Native1 = datatable.Rows[i + 1]["native"].ToString();
                c1.Birthday1 = todate(datatable.Rows[i + 1]["birthday"].ToString());
                c1.PartyTime1 = todate(datatable.Rows[i + 1]["partyTime"].ToString());
                c1.WorkTime1 = todate(datatable.Rows[i + 1]["workTime"].ToString());
                c1.FullED1 = isAddEnter(datatable.Rows[i + 1]["fullEducation"].ToString(), datatable.Rows[i + 1]["fullDegree"].ToString());
                c1.FullSS1 = isAddEnter(datatable.Rows[i + 1]["fullSchool"].ToString(), datatable.Rows[i + 1]["fullSpecialty"].ToString());
                c1.WorkED1 = isAddEnter(datatable.Rows[i + 1]["workEducation"].ToString(), datatable.Rows[i + 1]["workDegree"].ToString());
                c1.WorkSS1 = isAddEnter(datatable.Rows[i + 1]["workGraduate"].ToString(), datatable.Rows[i + 1]["workSpecialty"].ToString());
                c1.TechnicalPost1 = datatable.Rows[i + 1]["technicalPost"].ToString();
                c1.ExperiencePost1 = datatable.Rows[i + 1]["experiencePost"].ToString();
                c1.KnowField1 = datatable.Rows[i + 1]["knowField"].ToString();
                c1.TrainDirection1 = datatable.Rows[i + 1]["trainDirection"].ToString();
                c1.TrainMeasure1 = datatable.Rows[i + 1]["trainMeasure"].ToString();
                HBNameList.Add(c1);
                #endregion

                //第i+2个人的信息
                #region
                Class_HBNameList c2 = new Class_HBNameList();
                string strS2 = "", strN2 = "";
                string Remarks2 = "";
                if (datatable.Rows[i + 2]["sex"].ToString().Trim() != "男")
                {
                    strS2 = datatable.Rows[i + 2]["sex"].ToString().Trim();
                }
                if (datatable.Rows[i + 2]["nation"].ToString().Trim() != "汉族")
                {
                    strN2 = datatable.Rows[i + 2]["nation"].ToString().Trim();
                }
                if (strS2 != "" && strN2 == "")
                {
                    Remarks2 = "\n(" + strS2 + ")";
                }
                else
                    if (strS2 != "" && strN2 != "")
                    {
                        Remarks2 = "\n(" + strS2 + "、" + strN2 + ")";
                    }
                    else
                        if (strS2 == "" && strN2 != "")
                        {
                            Remarks2 = "\n(" + strN2 + ")";
                        }
                c2.Hbname1 = datatable.Rows[i + 2]["name"].ToString() + Remarks2;
                c2.Unitjop1 = datatable.Rows[i + 2]["department"].ToString() + "\n" + datatable.Rows[i + 2]["position"].ToString();
                c2.Native1 = datatable.Rows[i + 2]["native"].ToString();
                c2.Birthday1 = todate(datatable.Rows[i + 2]["birthday"].ToString());
                c2.PartyTime1 = todate(datatable.Rows[i + 2]["partyTime"].ToString());
                c2.WorkTime1 = todate(datatable.Rows[i + 2]["workTime"].ToString());
                c2.FullED1 = isAddEnter(datatable.Rows[i + 2]["fullEducation"].ToString(), datatable.Rows[i + 2]["fullDegree"].ToString());
                c2.FullSS1 = isAddEnter(datatable.Rows[i + 2]["fullSchool"].ToString(), datatable.Rows[i + 2]["fullSpecialty"].ToString());
                c2.WorkED1 = isAddEnter(datatable.Rows[i + 2]["workEducation"].ToString(), datatable.Rows[i + 2]["workDegree"].ToString());
                c2.WorkSS1 = isAddEnter(datatable.Rows[i + 2]["workGraduate"].ToString(), datatable.Rows[i + 2]["workSpecialty"].ToString());
                c2.TechnicalPost1 = datatable.Rows[i + 2]["technicalPost"].ToString();
                c2.ExperiencePost1 = datatable.Rows[i + 2]["experiencePost"].ToString();
                c2.KnowField1 = datatable.Rows[i + 2]["knowField"].ToString();
                c2.TrainDirection1 = datatable.Rows[i + 2]["trainDirection"].ToString();
                c2.TrainMeasure1 = datatable.Rows[i + 2]["trainMeasure"].ToString();
                HBNameList.Add(c2);
                #endregion
            }
            #endregion

            //datatable.Rows.Count % 5 == 4
            #region
            if (datatable.Rows.Count % 5 == 4)
            {
                Class_HBNameList c = new Class_HBNameList();

                c.Pagecount = i.ToString();

                //第i个人的信息
                #region
                string strS = "", strN = "";
                string Remarks = "";
                if (datatable.Rows[i]["sex"].ToString().Trim() != "男")
                {
                    strS = datatable.Rows[i]["sex"].ToString().Trim();
                }
                if (datatable.Rows[i]["nation"].ToString().Trim() != "汉族")
                {
                    strN = datatable.Rows[i]["nation"].ToString().Trim();
                }
                if (strS != "" && strN == "")
                {
                    Remarks = "\n(" + strS + ")";
                }
                else
                    if (strS != "" && strN != "")
                    {
                        Remarks = "\n(" + strS + "、" + strN + ")";
                    }
                    else
                        if (strS == "" && strN != "")
                        {
                            Remarks = "\n(" + strN + ")";
                        }
                c.Hbname1 = datatable.Rows[i]["name"].ToString() + Remarks;
                c.Unitjop1 = datatable.Rows[i]["department"].ToString() + "\n" + datatable.Rows[i]["position"].ToString();
                c.Native1 = datatable.Rows[i]["native"].ToString();
                c.Birthday1 = todate(datatable.Rows[i]["birthday"].ToString());
                c.PartyTime1 = todate(datatable.Rows[i]["partyTime"].ToString());
                c.WorkTime1 = todate(datatable.Rows[i]["workTime"].ToString());
                c.FullED1 = isAddEnter(datatable.Rows[i]["fullEducation"].ToString(), datatable.Rows[i]["fullDegree"].ToString());
                c.FullSS1 = isAddEnter(datatable.Rows[i]["fullSchool"].ToString(), datatable.Rows[i]["fullSpecialty"].ToString());
                c.WorkED1 = isAddEnter(datatable.Rows[i]["workEducation"].ToString(), datatable.Rows[i]["workDegree"].ToString());
                c.WorkSS1 = isAddEnter(datatable.Rows[i]["workGraduate"].ToString(), datatable.Rows[i]["workSpecialty"].ToString());
                c.TechnicalPost1 = datatable.Rows[i]["technicalPost"].ToString();
                c.ExperiencePost1 = datatable.Rows[i]["experiencePost"].ToString();
                c.KnowField1 = datatable.Rows[i]["knowField"].ToString();
                c.TrainDirection1 = datatable.Rows[i]["trainDirection"].ToString();
                c.TrainMeasure1 = datatable.Rows[i]["trainMeasure"].ToString();
                HBNameList.Add(c);
                #endregion

                //第i+1个人的信息
                #region
                Class_HBNameList c1 = new Class_HBNameList();
                string strS1 = "", strN1 = "";
                string Remarks1 = "";
                if (datatable.Rows[i + 1]["sex"].ToString().Trim() != "男")
                {
                    strS1 = datatable.Rows[i + 1]["sex"].ToString().Trim();
                }
                if (datatable.Rows[i + 1]["nation"].ToString().Trim() != "汉族")
                {
                    strN1 = datatable.Rows[i + 1]["nation"].ToString().Trim();
                }
                if (strS1 != "" && strN1 == "")
                {
                    Remarks1 = "\n(" + strS1 + ")";
                }
                else
                    if (strS1 != "" && strN1 != "")
                    {
                        Remarks1 = "\n(" + strS1 + "、" + strN1 + ")";
                    }
                    else
                        if (strS1 == "" && strN1 != "")
                        {
                            Remarks1 = "\n(" + strN1 + ")";
                        }
                c1.Hbname1 = datatable.Rows[i + 1]["name"].ToString() + Remarks1;
                c1.Unitjop1 = datatable.Rows[i + 1]["department"].ToString() + "\n" + datatable.Rows[i + 1]["position"].ToString();
                c1.Native1 = datatable.Rows[i + 1]["native"].ToString();
                c1.Birthday1 = todate(datatable.Rows[i + 1]["birthday"].ToString());
                c1.PartyTime1 = todate(datatable.Rows[i + 1]["partyTime"].ToString());
                c1.WorkTime1 = todate(datatable.Rows[i + 1]["workTime"].ToString());
                c1.FullED1 = isAddEnter(datatable.Rows[i + 1]["fullEducation"].ToString(), datatable.Rows[i + 1]["fullDegree"].ToString());
                c1.FullSS1 = isAddEnter(datatable.Rows[i + 1]["fullSchool"].ToString(), datatable.Rows[i + 1]["fullSpecialty"].ToString());
                c1.WorkED1 = isAddEnter(datatable.Rows[i + 1]["workEducation"].ToString(), datatable.Rows[i + 1]["workDegree"].ToString());
                c1.WorkSS1 = isAddEnter(datatable.Rows[i + 1]["workGraduate"].ToString(), datatable.Rows[i + 1]["workSpecialty"].ToString());
                c1.TechnicalPost1 = datatable.Rows[i + 1]["technicalPost"].ToString();
                c1.ExperiencePost1 = datatable.Rows[i + 1]["experiencePost"].ToString();
                c1.KnowField1 = datatable.Rows[i + 1]["knowField"].ToString();
                c1.TrainDirection1 = datatable.Rows[i + 1]["trainDirection"].ToString();
                c1.TrainMeasure1 = datatable.Rows[i + 1]["trainMeasure"].ToString();
                HBNameList.Add(c1);
                #endregion

                //第i+2个人的信息
                #region
                Class_HBNameList c2 = new Class_HBNameList();
                string strS2 = "", strN2 = "";
                string Remarks2 = "";
                if (datatable.Rows[i + 2]["sex"].ToString().Trim() != "男")
                {
                    strS2 = datatable.Rows[i + 2]["sex"].ToString().Trim();
                }
                if (datatable.Rows[i + 2]["nation"].ToString().Trim() != "汉族")
                {
                    strN2 = datatable.Rows[i + 2]["nation"].ToString().Trim();
                }
                if (strS2 != "" && strN2 == "")
                {
                    Remarks2 = "\n(" + strS2 + ")";
                }
                else
                    if (strS2 != "" && strN2 != "")
                    {
                        Remarks2 = "\n(" + strS2 + "、" + strN2 + ")";
                    }
                    else
                        if (strS2 == "" && strN2 != "")
                        {
                            Remarks2 = "\n(" + strN2 + ")";
                        }
                c2.Hbname1 = datatable.Rows[i + 2]["name"].ToString() + Remarks2;
                c2.Unitjop1 = datatable.Rows[i + 2]["department"].ToString() + "\n" + datatable.Rows[i + 2]["position"].ToString();
                c2.Native1 = datatable.Rows[i + 2]["native"].ToString();
                c2.Birthday1 = todate(datatable.Rows[i + 2]["birthday"].ToString());
                c2.PartyTime1 = todate(datatable.Rows[i + 2]["partyTime"].ToString());
                c2.WorkTime1 = todate(datatable.Rows[i + 2]["workTime"].ToString());
                c2.FullED1 = isAddEnter(datatable.Rows[i + 2]["fullEducation"].ToString(), datatable.Rows[i + 2]["fullDegree"].ToString());
                c2.FullSS1 = isAddEnter(datatable.Rows[i + 2]["fullSchool"].ToString(), datatable.Rows[i + 2]["fullSpecialty"].ToString());
                c2.WorkED1 = isAddEnter(datatable.Rows[i + 2]["workEducation"].ToString(), datatable.Rows[i + 2]["workDegree"].ToString());
                c2.WorkSS1 = isAddEnter(datatable.Rows[i + 2]["workGraduate"].ToString(), datatable.Rows[i + 2]["workSpecialty"].ToString());
                c2.TechnicalPost1 = datatable.Rows[i + 2]["technicalPost"].ToString();
                c2.ExperiencePost1 = datatable.Rows[i + 2]["experiencePost"].ToString();
                c2.KnowField1 = datatable.Rows[i + 2]["knowField"].ToString();
                c2.TrainDirection1 = datatable.Rows[i + 2]["trainDirection"].ToString();
                c2.TrainMeasure1 = datatable.Rows[i + 2]["trainMeasure"].ToString();
                HBNameList.Add(c2);
                #endregion

                //第i+3个人的信息
                #region
                Class_HBNameList c3 = new Class_HBNameList();
                string strS3 = "", strN3 = "";
                string Remarks3 = "";
                if (datatable.Rows[i + 3]["sex"].ToString().Trim() != "男")
                {
                    strS3 = datatable.Rows[i + 3]["sex"].ToString().Trim();
                }
                if (datatable.Rows[i + 3]["nation"].ToString().Trim() != "汉族")
                {
                    strN3 = datatable.Rows[i + 3]["nation"].ToString().Trim();
                }
                if (strS3 != "" && strN3 == "")
                {
                    Remarks3 = "\n(" + strS3 + ")";
                }
                else
                    if (strS3 != "" && strN3 != "")
                    {
                        Remarks3 = "\n(" + strS3 + "、" + strN3 + ")";
                    }
                    else
                        if (strS3 == "" && strN3 != "")
                        {
                            Remarks3 = "\n(" + strN3 + ")";
                        }
                c3.Hbname1 = datatable.Rows[i + 3]["name"].ToString() + Remarks3;
                c3.Unitjop1 = datatable.Rows[i + 3]["department"].ToString() + "\n" + datatable.Rows[i + 3]["position"].ToString();
                c3.Native1 = datatable.Rows[i + 3]["native"].ToString();
                c3.Birthday1 = todate(datatable.Rows[i + 3]["birthday"].ToString());
                c3.PartyTime1 = todate(datatable.Rows[i + 3]["partyTime"].ToString());
                c3.WorkTime1 = todate(datatable.Rows[i + 3]["workTime"].ToString());
                c3.FullED1 = isAddEnter(datatable.Rows[i + 3]["fullEducation"].ToString(), datatable.Rows[i + 3]["fullDegree"].ToString());
                c3.FullSS1 = isAddEnter(datatable.Rows[i + 3]["fullSchool"].ToString(), datatable.Rows[i + 3]["fullSpecialty"].ToString());
                c3.WorkED1 = isAddEnter(datatable.Rows[i + 3]["workEducation"].ToString(), datatable.Rows[i + 3]["workDegree"].ToString());
                c3.WorkSS1 = isAddEnter(datatable.Rows[i + 3]["workGraduate"].ToString(), datatable.Rows[i + 3]["workSpecialty"].ToString());
                c3.TechnicalPost1 = datatable.Rows[i + 3]["technicalPost"].ToString();
                c3.ExperiencePost1 = datatable.Rows[i + 3]["experiencePost"].ToString();
                c3.KnowField1 = datatable.Rows[i + 3]["knowField"].ToString();
                c3.TrainDirection1 = datatable.Rows[i + 3]["trainDirection"].ToString();
                c3.TrainMeasure1 = datatable.Rows[i + 3]["trainMeasure"].ToString();
                HBNameList.Add(c3);
                #endregion
            }
            #endregion

            //信息统计
            //以下几个变量用于后备干部信息的统计
            int allcount = 0;//统计总人数
            string sallcount = "";
            string mancount = "";//记录男总人数
            string womencoumt = "";//记录女总人数
            string unpartyount = "";//记录非中共党员人数
            string fewnationcount = "";//记录少数民族人数
            string age40 = "";//记录40岁以下人数
            string age41_45 = "";//记录41到45岁人数
            string age46_50 = "";//记录46到50岁人数
            string age51 = "";//记录51岁以上人数            
            string doctorgraduate = "";//记录博士研究生人数
            string mastergraduate = "";//记录硕士研究生人数
            string graduate = "";//记录大学生人数
            string aveage = "";//记录平均年龄
            #region
            //总人数
            allcount = datatable.Rows.Count;
            sallcount = allcount.ToString();
            
            //男人数
            datatable.Clear();
            datatable = dataOp.GetOneDataTable_sql("select CID from TB_CommonInfo where cid in (" + selectid + ") and sex = '男'");
            mancount = datatable.Rows.Count.ToString();            
            //女人数
            datatable.Clear();
            datatable = dataOp.GetOneDataTable_sql("select CID from TB_CommonInfo where cid in (" + selectid + ") and sex = '女'");
            womencoumt = datatable.Rows.Count.ToString();
            //非中共
            datatable.Clear();
            datatable = dataOp.GetOneDataTable_sql("select CID from TB_CommonInfo where cid in (" + selectid + ") and partyClass = '中共'");
            unpartyount = (allcount - datatable.Rows.Count).ToString();
            //少数民族
            datatable.Clear();
            datatable = dataOp.GetOneDataTable_sql("select CID from TB_CommonInfo where cid in (" + selectid + ") and nation like '%汉%'");
            fewnationcount = (allcount - datatable.Rows.Count).ToString();
            //40岁及40岁以下
            datatable.Clear();
            datatable = dataOp.GetOneDataTable_sql("select CID from TB_CommonInfo where cid in (" + selectid + ") and age <= 40");
            age40 = datatable.Rows.Count.ToString();
            //41岁到45岁
            datatable.Clear();
            datatable = dataOp.GetOneDataTable_sql("select CID from TB_CommonInfo where cid in (" + selectid + ") and age > 40 and age <=45");
            age41_45 = datatable.Rows.Count.ToString();
            //46岁到50岁
            datatable.Clear();
            datatable = dataOp.GetOneDataTable_sql("select CID from TB_CommonInfo where cid in (" + selectid + ") and age > 45 and age <=50");
            age46_50 = datatable.Rows.Count.ToString();
            //51岁以上
            datatable.Clear();
            datatable = dataOp.GetOneDataTable_sql("select CID from TB_CommonInfo where cid in (" + selectid + ") and age > 50");
            age51 = datatable.Rows.Count.ToString();
            //平均年龄
            datatable.Clear();
            datatable = dataOp.GetOneDataTable_sql("select AVG(age) as aveage from TB_CommonInfo where cid in (" + selectid + ")");
            aveage = datatable.Rows[0]["aveage"].ToString();
            //平均年龄计算结果可能为空
            if (aveage.Equals("") || aveage == null)
                aveage = "0";
            aveage = Math.Round(Convert.ToDouble(aveage), 2).ToString();
            //博士
            datatable.Clear();
            datatable = dataOp.GetOneDataTable_sql("select CID from TB_CommonInfo where cid in (" + selectid + ") and ((fullDegree = '博士' and (fullEducation='研究生' or fullEducation='党校研究生')) or (workDegree='博士' and (workEducation='研究生' or workEducation='党校研究生')))");
            doctorgraduate = datatable.Rows.Count.ToString();
            String doctor = "";
            for (int j = 0; j < datatable.Rows.Count; j++)
            {
                if (j == datatable.Rows.Count - 1)
                    doctor = doctor + "'" + datatable.Rows[j][0] + "'";
                else
                    doctor = doctor + "'" + datatable.Rows[j][0] + "',";
            }
            //硕士
            datatable.Clear();
            datatable = dataOp.GetOneDataTable_sql("select CID from TB_CommonInfo where cid in (" + selectid + ") and cid not in(" + doctor + ") and ((fullDegree = '硕士' and (fullEducation='研究生' or fullEducation='党校研究生')) or (workDegree='硕士' and (workEducation='研究生' or workEducation='党校研究生')))");
            mastergraduate = datatable.Rows.Count.ToString();
            String master = "";
            for (int j = 0; j < datatable.Rows.Count; j++)
            {
                if (j == datatable.Rows.Count - 1)
                    master = master + "'" + datatable.Rows[j][0] + "'";
                else
                    master = master + "'" + datatable.Rows[j][0] + "',";
            }
            //学士
            datatable.Clear();
            datatable = dataOp.GetOneDataTable_sql("select CID from TB_CommonInfo where cid in (" + selectid + ") and cid not in (" + doctor + ") and cid not in (" + master + ") and ((fullDegree = '学士' and (fullEducation='大学本科' or fullEducation='党校大学')) or (workDegree='学士' and (workEducation='大学本科' or workEducation='党校大学')))");
            graduate = datatable.Rows.Count.ToString();
            //清除缓存中的数据，在之后的代码中不用这些数据了
            datatable.Clear();
            #endregion
            //表头和说明信息
            #region
            string explain = "";
            string unitqd = "";
            string title = null;
            string title1 = null;

            if (this.Unitclass.Equals("省直单位"))
            {
                if (this.Qd.Equals("正职"))
                {
                    title = this.Unit + "正厅级\n后备干部初步人选名册";
                    title1 = this.Unit + "正厅级后备干部初步人选名册";
                    unitqd = "正厅级";
                }
                else if (this.Qd.Equals("副职"))
                {
                    title = this.Unit + "副厅级\n后备干部初步人选名册";
                    title1 = this.Unit + "副厅级后备干部初步人选名册";
                    unitqd = "副厅级";
                }
            }
            else if (this.Unitclass.Equals("省辖市"))
            {
                title = this.Unit + "党政" + this.Qd + "\n后备干部初步人选名册";
                title1 = this.Unit + "党政" + this.Qd + "后备干部初步人选名册";
                unitqd = "党政" + this.Qd;
            }
            else if (this.Unitclass.Equals("省管高校"))
            {
                if (this.Qd.Equals("正职"))
                {
                    title = this.Unit + "正校级\n后备干部初步人选名册";
                    title1 = this.Unit + "正校级后备干部初步人选名册";
                    unitqd = "正校级";
                }
                else if (this.Qd.Equals("副职"))
                {
                    title = this.Unit + "副校级\n后备干部初步人选名册";
                    title1 = this.Unit + "副校级后备干部初步人选名册";
                    unitqd = "副校级";
                }
            }
            else if (this.Unitclass.Equals("省管企业"))
            {
                title = this.Unit + "领导班子" + this.Qd + "\n后备干部初步人选名册";
                title1 = this.Unit + "领导班子" + this.Qd + "后备干部初步人选名册";
                unitqd = "领导班子" + this.Qd;
            }
            //市厅级
            else if (this.Unitclass.Equals("市直单位"))
            {
                if (this.Qd.Equals("正职"))
                {
                    title = this.Unit + "正县级\n后备干部初步人选名册";
                    title1 = this.Unit + "正县级后备干部初步人选名册";
                    unitqd = "正县级";
                }
                else if (this.Qd.Equals("副职"))
                {
                    title = this.Unit + "副县级\n后备干部初步人选名册";
                    title1 = this.Unit + "副县级后备干部初步人选名册";
                    unitqd = "副县级";
                }
            }
            else if (this.Unitclass.Equals("县(市、区)"))
            {
                title = this.Unit + "党政" + this.Qd + "\n后备干部初步人选名册";
                title1 = this.Unit + "党政" + this.Qd + "后备干部初步人选名册";
                unitqd = "党政" + this.Qd;
            }
            else if (this.Unitclass.Equals("市管学校"))
            {
                if (this.Qd.Equals("正职"))
                {
                    title = this.Unit + "正校级\n后备干部初步人选名册";
                    title1 = this.Unit + "正校级后备干部初步人选名册";
                    unitqd = "正校级";
                }
                else if (this.Qd.Equals("副职"))
                {
                    title = this.Unit + "副校级\n后备干部初步人选名册";
                    title1 = this.Unit + "副校级后备干部初步人选名册";
                    unitqd = "副校级";
                }
            }
            else if (this.Unitclass.Equals("市管企业"))
            {
                title = this.Unit + "领导班子" + this.Qd + "\n后备干部初步人选名册";
                title1 = this.Unit + "领导班子" + this.Qd + "后备干部初步人选名册";
                unitqd = "领导班子" + this.Qd;
            }


            else if (this.Unitclass.Equals("县(市、区)直"))
            {
                if (this.Qd.Equals("正职"))
                {
                    title = this.Unit + "正科级\n后备干部初步人选名册";
                    title1 = this.Unit + "正科级后备干部初步人选名册";
                    unitqd = "正科级";
                }
                else if (this.Qd.Equals("副职"))
                {
                    title = this.Unit + "副科级\n后备干部初步人选名册";
                    title1 = this.Unit + "副科级后备干部初步人选名册";
                    unitqd = "副科级";
                }
            }
            else if (this.Unitclass.Equals("乡(镇、街道)"))
            {
                title = this.Unit + "党政" + this.Qd + "\n后备干部初步人选名册";
                title1 = this.Unit + "党政" + this.Qd + "后备干部初步人选名册";
                unitqd = "党政" + this.Qd;
            }
            else if (this.Unitclass.Equals("县管学校"))
            {
                if (this.Qd.Equals("正职"))
                {
                    title = this.Unit + "正校级\n后备干部初步人选名册";
                    title1 = this.Unit + "正校级后备干部初步人选名册";
                    unitqd = "正校级";
                }
                else if (this.Qd.Equals("副职"))
                {
                    title = this.Unit + "副校级\n后备干部初步人选名册";
                    title1 = this.Unit + "副校级后备干部初步人选名册";
                    unitqd = "副校级";
                }
            }
            else if (this.Unitclass.Equals("县管企业"))
            {
                title = this.Unit + "领导班子" + this.Qd + "\n后备干部初步人选名册";
                title1 = this.Unit + "领导班子" + this.Qd + "后备干部初步人选名册";
                unitqd = "领导班子" + this.Qd;
            }


            explain = "    " + this.Unit + unitqd + "后备干部初步人选共" + sallcount + "人，其中：男" + mancount + "人，女" + womencoumt + "人，非中共党员干部" + unpartyount + "人，少数民族干部" + fewnationcount + "人，40岁及以下" + age40 + "人，41-45岁" + age41_45 + "人，46-50岁" + age46_50 + "人，51岁以上" + age51 + "人，平均年龄" + aveage + "岁，博士研究生" + doctorgraduate + "人，硕士研究生" + mastergraduate + "人，大学" + graduate + "人。";
            explain = setexplain(explain);
            #endregion

            //由于参数问题，暂时将参数date接受title1的值
            ReportParameter[] rptParaA = new ReportParameter[]{new ReportParameter("title", title),
                                                new ReportParameter("date", title1),
                                                new ReportParameter("explain", explain)};
            //使用名称和值实例化新的 ReportParameter。
            //在一起的报表设置一个参数，刚实例化的ReportParameter的第一个参数与关联
            this.RV_HBNameList.LocalReport.SetParameters(rptParaA);

            this.BS_HBNameList.DataSource = HBNameList;
            
            this.RV_HBNameList.RefreshReport();
        }

        /// <summary>
        /// 将****年**月格式改为****.**格式
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        private string todate(string date) 
        {
            date = date.Replace("年", ".");
            date = date.Replace("月", "");
            return date;
        }           
  
        /// <summary>
        /// 处理说明信息的换行问题
        /// </summary>
        /// <param name="explain"></param>
        /// <returns></returns>
        private string setexplain(string explain)
        {
            string ret = "";//记录变量explain插入换行结果并返回该值
            double charcount = 0.0;//字符的计数变量
            foreach (char c in explain)
            {               
                //c为数字或者' '或者'-'或者'.'
                if (c == ' '|| (c >= '0' && c <= '9') || c == '-' || c == '.')
                    charcount += 0.5;
                else
                    charcount += 1;
                ret += c;
                //每n个汉字宽度添加换行\n
                if (charcount >= 1 && ( charcount % 29.5 == 0 || (int)charcount % 30 == 0))
                {
                    charcount = 0.0;
                    ret += "\n";
                }
            }
            return ret;
        }
    
        /// <summary>
        /// 判断是否加入换行符，返回一个字符串
        /// </summary>
        /// <param name="str1"></param>
        /// <param name="str2"></param>
        /// <returns></returns>
        private string isAddEnter(string str1,string str2)
        {
            if (str1 == null || str1.Equals(""))
                return str2;

            if (str2 == null || str2.Equals(""))
                return str1;

            return str1 + "\n" + str2;
        }

    }
}
