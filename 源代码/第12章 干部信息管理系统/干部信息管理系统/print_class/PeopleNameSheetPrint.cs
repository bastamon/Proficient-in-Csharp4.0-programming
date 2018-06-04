using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Word;
using System.Collections;
using System.Data;
using HBMISR.Data;

namespace HBMISR.print_class
{
    /// <summary>
    /// 导出Word类，功能：导出初步人选名册
    /// </summary>
    class PeopleNameSheetPrint
    {
        //把Word读取到内存中
        Word._Application wordappliction = null;
        object missing = System.Reflection.Missing.Value;
        object readOnly = false;
        object isVisible = true;
        private static int pagecount = 5;//记录花名册一页显示多少条记录

        private string unit;//单位名称

        public string Unit
        {
            get { return unit; }
            set { unit = value; }
        }

        private string unitclass;//单位类别
        public string Unitclass
        {
            get { return unitclass; }
            set { unitclass = value; }
        }

        private string qd;//正副职
        public string Qd
        {
            get { return qd; }
            set { qd = value; }
        }

        private string uid;//单位编号
        public string Uid
        {
            get { return uid; }
            set { uid = value; }
        }
        /// <summary>
        /// 导出初步人选名册
        /// </summary>
        /// <param name="idlist">传递所选干部的所有id</param>
        public void exportword(ArrayList idlist) 
        {
            #region
            //选择保存路径
            #region
            string savepath = "";
            SaveFileDialog sa = new SaveFileDialog();
            sa.Filter = "Document(*.doc)|*.doc";
            sa.FileName = "初步人选名册";
            if (sa.ShowDialog() == DialogResult.OK)
                savepath = sa.FileName;
            else
                return;
            #endregion

            //创建word应用程序
            wordappliction = new Word.Application();
            //打开模板
            object filepath = System.Windows.Forms.Application.StartupPath + "\\wordModel" + "\\花名册.doc";
            
            Word.Document mydoc = wordappliction.Documents.Open(ref filepath, ref missing, ref readOnly,
                                                              ref missing, ref missing, ref missing, ref missing, ref missing,
                                                              ref missing, ref missing, ref missing, ref isVisible, ref missing,
                                                              ref missing, ref missing, ref missing);
            //打开模板复制其中的表            
            #endregion

            //通过idlist读取所选后备干部的信息，用datatable存在内存中。
            string selectid = "";

            for (int i = 0; i < idlist.Count; i++)           
            {
                if (i == idlist.Count - 1)
                    selectid = selectid + "'" + idlist[i] + "'";
                else
                    selectid = selectid + "'" + idlist[i] + "',";
            }
           
            string sql = "select name,cid,sex,nation,department,position,native,birthday,age,partyTime,workTime,fullEducation,fullDegree,fullSchool,fullSpecialty,workEducation,workDegree,workGraduate,workSpecialty,technicalPost,joinTeam,experiencePost,knowField,trainDirection,trainMeasure,partyClass from TB_CommonInfo order by rank,joinTeam desc";

            string cond = "cid in (" + selectid + ")";
            DataOperation dataOp = new DataOperation();
            DataTable datatableT = dataOp.GetOneDataTable_sql(sql);
            DataTable datatable = new DataTable();
            datatable = datatableT.Clone();
            DataView dv = datatableT.AsDataView();
            dv.RowFilter = cond;
            datatable = dv.ToTable();
            //确定表头的单位信息
            string title = null;

            if (this.Unitclass.Equals("省直单位"))
            {
                if (this.Qd.Equals("正职"))
                    title = this.Unit + "正厅级";
                else if (this.Qd.Equals("副职"))
                    title = this.Unit + "副厅级";
            }
            else if (this.Unitclass.Equals("省辖市"))
            {
                title = this.Unit + "党政" + this.Qd;
            }
            else if (this.Unitclass.Equals("省管高校"))
            {
                if (this.Qd.Equals("正职"))
                    title = this.Unit + "正校级";

                else if (this.Qd.Equals("副职"))
                    title = this.Unit + "副校级";
            }
            else if (this.Unitclass.Equals("省管企业"))
            {
                title = this.Unit + "领导班子" + this.Qd ;
            }//县处级
            else if (this.Unitclass.Equals("市直单位"))
            {
               if (this.Qd.Equals("正职"))
                    title = this.Unit + "正县级";

                else if (this.Qd.Equals("副职"))
                    title = this.Unit + "副县级";
            }
            else if(this.Unitclass.Equals("市管企业"))
            {
                    title = this.Unit + "领导班子" + this.Qd ;
            }
            else if(this.Unitclass.Equals("市管学校"))
            {
                if (this.Qd.Equals("正职"))
                    title = this.Unit + "正校级";

                else if (this.Qd.Equals("副职"))
                    title = this.Unit + "副校级";
            }
            else if(this.Unitclass.Equals("县(市、区)"))
            { 
                    title = this.Unit + "党政" + this.Qd;
            }//乡科级
            else if(this.Unitclass.Equals("县(市、区)直"))
            {
                if (this.Qd.Equals("正职"))
                    title = this.Unit + "正科级";

                else if (this.Qd.Equals("副职"))
                    title = this.Unit + "副科级";
            }
            else if(this.Unitclass.Equals("县管学校"))
            {
                if (this.Qd.Equals("正职"))
                    title = this.Unit + "正校级";

                else if (this.Qd.Equals("副职"))
                    title = this.Unit + "副校级";
            }
            else if(this.Unitclass.Equals("县管企业"))
            {
                    title = this.Unit + "领导班子" + this.Qd;
            }
            else if(this.Unitclass.Equals("乡(镇、街道)"))
            {
                    title = this.Unit + "党政"+this.Qd;
            }
            mydoc.Tables[1].Cell(1,1).Range.Text = title;
            mydoc.Tables[3].Cell(1, 1).Range.Text = title + "后备干部初步人选名册";
            #region
            mydoc.ActiveWindow.Selection.WholeStory();
            mydoc.ActiveWindow.Selection.Tables[4].Select();
            mydoc.ActiveWindow.Selection.Copy();
            #endregion

            //添加前面的说明信息
            //以下几个变量用于后备干部信息的统计
            int allcount = 0;//统计总人数
            int mancount = 0;//记录男总人数
            int womencoumt = 0;//记录女总人数
            int unpartyount = 0;//记录非中共党员人数
            int fewnationcount = 0;//记录少数民族人数
            int age40 = 0;//记录40岁以下人数
            int age41_45 = 0;//记录41到45岁人数
            int age46_50 = 0;//记录46到50岁人数
            int age51 = 0;//记录51岁以上人数            
            int doctorgraduate = 0;//记录博士研究生人数
            int mastergraduate = 0;//记录硕士研究生人数
            int graduate = 0;//记录大学生人数
            string aveage = "";//记录平均年龄


            //所用到的一切信息统计结果
            #region
            //总人数
            allcount = datatable.Rows.Count;
            //男人数
            DataTable datatable1 = dataOp.GetOneDataTable_sql("select CID from TB_CommonInfo where cid in (" + selectid + ") and sex = '男'");
            mancount = datatable1.Rows.Count;
            //女人数
            datatable1 = dataOp.GetOneDataTable_sql("select CID from TB_CommonInfo where cid in (" + selectid + ") and sex = '女'");
            womencoumt = datatable1.Rows.Count;
            //非中共
            datatable1 = dataOp.GetOneDataTable_sql("select CID from TB_CommonInfo where cid in (" + selectid + ") and partyClass = '中共'");
            unpartyount = allcount - datatable1.Rows.Count;
            //少数民族
            datatable1 = dataOp.GetOneDataTable_sql("select CID from TB_CommonInfo where cid in (" + selectid + ") and nation like '%汉%'");
            fewnationcount = allcount - datatable1.Rows.Count;
            //40岁及40岁以下
            datatable1 = dataOp.GetOneDataTable_sql("select CID from TB_CommonInfo where cid in (" + selectid + ") and age <= 40");
            age40 = datatable1.Rows.Count;
            //41岁到45岁
            datatable1 = dataOp.GetOneDataTable_sql("select CID from TB_CommonInfo where cid in (" + selectid + ") and age > 40 and age <=45");
            age41_45 = datatable1.Rows.Count;
            //46岁到50岁
            datatable1 = dataOp.GetOneDataTable_sql("select CID from TB_CommonInfo where cid in (" + selectid + ") and age > 45 and age <=50");
            age46_50 = datatable1.Rows.Count;
            //51岁以上
            datatable1.Clear();
            datatable1 = dataOp.GetOneDataTable_sql("select CID from TB_CommonInfo where cid in (" + selectid + ") and age > 50");
            age51 = datatable1.Rows.Count;
            //平均年龄
            datatable1 = dataOp.GetOneDataTable_sql("select AVG(age) as aveage from TB_CommonInfo where cid in (" + selectid + ")");
            aveage = datatable1.Rows[0]["aveage"].ToString();
            //平均年龄计算结果可能为空
            if (aveage.Equals("") || aveage == null)
                aveage = "0";
            //博士
            datatable1 = dataOp.GetOneDataTable_sql("select CID from TB_CommonInfo where cid in (" + selectid + ") and ((fullDegree = '博士' and (fullEducation='研究生' or fullEducation='党校研究生')) or (workDegree='博士' and (workEducation='研究生' or workEducation='党校研究生')))");
            doctorgraduate = datatable1.Rows.Count;
            String doctor = "";
            for (int i = 0; i < datatable1.Rows.Count; i++)
            {
                if (i == datatable1.Rows.Count - 1)
                    doctor = doctor + "'" + datatable1.Rows[i][0] + "'";
                else
                    doctor = doctor + "'" + datatable1.Rows[i][0] + "',";
            }
            //硕士
            datatable1 = dataOp.GetOneDataTable_sql("select CID from TB_CommonInfo where cid in (" + selectid + ") and cid not in(" + doctor + ") and ((fullDegree = '硕士' and (fullEducation='研究生' or fullEducation='党校研究生')) or (workDegree='硕士' and (workEducation='研究生' or workEducation='党校研究生')))");
            mastergraduate = datatable1.Rows.Count;
            String master = "";
            for (int i = 0; i < datatable1.Rows.Count; i++)
            {
                if (i == datatable1.Rows.Count - 1)
                    master = master + "'" + datatable1.Rows[i][0] + "'";
                else
                    master = master + "'" + datatable1.Rows[i][0] + "',";
            }
            //学士
            datatable1 = dataOp.GetOneDataTable_sql("select CID from TB_CommonInfo where cid in (" + selectid + ") and cid not in (" + doctor + ") and cid not in (" + master + ") and ((fullDegree = '学士' and (fullEducation='大学本科' or fullEducation='党校大学')) or (workDegree='学士' and (workEducation='大学本科' or workEducation='党校大学')))");
            graduate = datatable1.Rows.Count;
            #endregion

            //表头和说明信息
            #region
            string explain = "    " + this.Unit + "党政" + this.Qd + "后备干部初步人选共" + allcount + "人，其中：男" + mancount + "人，女" + womencoumt + "人，非中共党员干部" + unpartyount + "人，少数民族干部" + fewnationcount + "人，40岁及以下" + age40 + "人，41-45岁" + age41_45 + "人，46-50岁" + age46_50 + "人，51岁以上" + age51 + "人，平均年龄" + Math.Round(Convert.ToDouble(aveage), 2) + "岁，博士研究生" + doctorgraduate + "人，硕士研究生" + mastergraduate + "人，大学" + graduate + "人。";
            
            #endregion
            mydoc.Tables[2].Range.Text = explain;



            int tablecount1 = 0;
            if (datatable.Rows.Count % pagecount == 0)
                tablecount1 = datatable.Rows.Count / pagecount - 1;
            else
                tablecount1 = datatable.Rows.Count / pagecount;
           
            #region

            for (int i = 0; i < tablecount1; i++)
            {
                //第二页的前两行
                Word.Paragraph para1 = mydoc.Content.Paragraphs.Add(ref　missing);
                Word.Paragraph para2 = mydoc.Content.Paragraphs.Add(ref　missing);
                Word.Paragraph para3 = mydoc.Content.Paragraphs.Add(ref　missing);
                //在此粘贴复制的表格
                //控制位置
                object pBreak = (int)WdBreakType.wdSectionBreakNextPage;
                para1.Range.InsertBreak(ref pBreak);
                para3.Range.Paste();
                if(i == tablecount1 - 1)
                    para3.Range.Text = " 注：后备干部人选是民主党派成员的，在“入党时间”栏填写党派名称及加入时间；无党派人士，此栏不填。";
            }
            if( tablecount1 == 0 )
            {
                Word.Paragraph para1 = mydoc.Content.Paragraphs.Add(ref　missing);
                para1.Range.Text = " 注：后备干部人选是民主党派成员的，在“入党时间”栏填写党派名称及加入时间；无党派人士，此栏不填。";
            }
            #endregion

            for (int i = 0; i < datatable.Rows.Count; i++)
            {

                int tableindex = i / pagecount + 4;
                int rowindex = i % pagecount + 3;
                
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
                    Remarks = "(" + strS + ")";
                }
                else
                    if (strS != "" && strN != "")
                    {
                        Remarks = "(" + strS + "、" + strN + ")";
                    }
                    else
                        if (strS == "" && strN != "")
                        {
                            Remarks = "(" + strN + ")";
                        }
                mydoc.Tables[tableindex].Cell(rowindex, 1).Select();
                mydoc.Tables[tableindex].Cell(rowindex, 1).Range.Text = Remarks;
                //在选中的位置添加文本
                string strna = datatable.Rows[i]["name"].ToString() + "\n";
                wordappliction.Selection.Font.Name = "宋体 ";
                wordappliction.Selection.Font.Size = 10.5f;
                wordappliction.Selection.TypeText(strna);
                try
                {
                    mydoc.Tables[tableindex].Cell(rowindex, 2).Range.Text = datatable.Rows[i]["department"].ToString() + "\n" + datatable.Rows[i]["position"].ToString(); //"工作单位及职务";
                    mydoc.Tables[tableindex].Cell(rowindex, 3).Range.Text = datatable.Rows[i]["native"].ToString();//"籍贯";
                    string age = datatable.Rows[i]["birthday"].ToString().Replace("年",".");
                    mydoc.Tables[tableindex].Cell(rowindex, 4).Range.Text = age.Replace("月",""); //"出生年月";
                    string partytime = datatable.Rows[i]["partyTime"].ToString().Replace("年", ".");
                    mydoc.Tables[tableindex].Cell(rowindex, 5).Range.Text = partytime.Replace("月","");//"入党时间";
                    string worktime = datatable.Rows[i]["workTime"].ToString().Replace("年",".");
                    mydoc.Tables[tableindex].Cell(rowindex, 6).Range.Text = worktime.Replace("月",""); ;//"参加工作时间";
                    mydoc.Tables[tableindex].Cell(rowindex, 7).Range.Text = datatable.Rows[i]["fullEducation"].ToString() + "\n" + datatable.Rows[i]["fullDegree"].ToString(); //"学历学位";
                    mydoc.Tables[tableindex].Cell(rowindex, 8).Range.Text = datatable.Rows[i]["fullSchool"].ToString() + "\n" + datatable.Rows[i]["fullSpecialty"].ToString();// "毕业院校及专业";
                    mydoc.Tables[tableindex].Cell(rowindex, 9).Range.Text = datatable.Rows[i]["workEducation"].ToString() + "\n" + datatable.Rows[i]["workDegree"].ToString(); //"学历学位";
                    mydoc.Tables[tableindex].Cell(rowindex, 10).Range.Text = datatable.Rows[i]["workGraduate"].ToString() + "\n" + datatable.Rows[i]["workSpecialty"].ToString();//"毕业院校及专业";
                    mydoc.Tables[tableindex].Cell(rowindex, 11).Range.Text = datatable.Rows[i]["technicalPost"].ToString(); //"专业技术职务";
                    mydoc.Tables[tableindex].Cell(rowindex, 12).Range.Text = datatable.Rows[i]["experiencePost"].ToString(); //"历任主要职务";
                    mydoc.Tables[tableindex].Cell(rowindex, 13).Range.Text = datatable.Rows[i]["knowField"].ToString(); //"熟悉领域";
                    mydoc.Tables[tableindex].Cell(rowindex, 14).Range.Text = datatable.Rows[i]["trainDirection"].ToString(); //"培养方向";
                    mydoc.Tables[tableindex].Cell(rowindex, 15).Range.Text = datatable.Rows[i]["trainMeasure"].ToString();//"培养措施";
                }
                catch(Exception ex)
                {
                    MessageBox.Show("导出失败，请重新操作！"+ex.Message);
                    return;
                }
            }  
            try
            {
                object path = savepath;
                //wordappliction.Documents.Save(path);
                object myobj = System.Reflection.Missing.Value; ;
                mydoc.SaveAs(ref path, ref myobj, ref myobj, ref myobj, ref myobj, ref myobj,
                    ref myobj, ref myobj, ref myobj, ref myobj, ref myobj, ref myobj,
                    ref myobj, ref myobj, ref myobj, ref myobj);

                //关闭文档
                mydoc.Close(ref myobj, ref myobj, ref myobj);
                //退出应用程序。
                wordappliction.Quit();

                MessageBox.Show("导出成功!");
            }
            catch (Exception ex) {
                MessageBox.Show(ex.Message);
            }          
        }
    }
}

