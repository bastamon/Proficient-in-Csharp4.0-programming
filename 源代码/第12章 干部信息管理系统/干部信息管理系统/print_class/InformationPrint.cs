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
    /// 导出Word类，功能：导出信息采集表
    /// </summary>
    class InformationPrint
    {
        //把Word读取到内存中
        Word._Application wordappliction = null;
        Word._Document mydoc = null;
        object missing = System.Reflection.Missing.Value;
        object readOnly = false;
        object isVisible = true;

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
        /// 导出word的方法
        /// </summary>
        /// <param name="idlist">导出后备干部的id号集合</param>
        public void exportword(ArrayList idlist)  //建议把Datatable类型的参数传过来，建议读视图。
        {
            //表头信息
            #region
            string title = null;
            if (this.Unitclass.Equals("省直单位"))
            {
                if (this.Qd.Equals("正职"))
                    title = "正厅级后备干部考察对象";

                else if (this.Qd.Equals("副职"))
                    title = "副厅级后备干部考察对象";
            }
            else if (this.Unitclass.Equals("省辖市"))
            {
                title = "党政" + this.Qd + "后备干部考察对象";
            }
            else if (this.Unitclass.Equals("省管高校"))
            {
                if (this.Qd.Equals("正职"))
                    title = "正校级后备干部考察对象";
                else
                    if (this.Qd.Equals("副职"))
                        title = "副校级后备干部考察对象";
            }
            else if (this.Unitclass.Equals("省管企业"))
            {
                title = "领导班子" + this.Qd + "后备干部考察对象";
            }
            if (this.Unitclass.Equals("市直单位"))
            {
                if (this.Qd.Equals("正职"))
                    title = "正县级后备干部考察对象";

                else if (this.Qd.Equals("副职"))
                    title = "副县级后备干部考察对象";
            }
            else if (this.Unitclass.Equals("县(市、区)"))
            {
                title = "党政" + this.Qd + "后备干部考察对象";
            }
            else if (this.Unitclass.Equals("市管学校"))
            {
                if (this.Qd.Equals("正职"))
                    title = "正校级后备干部考察对象";

                else if (this.Qd.Equals("副职"))
                    title = "副校级后备干部考察对象";
            }
            else if (this.Unitclass.Equals("市管企业"))
            {
                title = "领导班子" + this.Qd + "后备干部考察对象";
            }
            else if (this.Unitclass.Equals("县(市、区)直"))
            {
                if (this.Qd.Equals("正职"))
                    title = "正科级后备干部考察对象";

                else if (this.Qd.Equals("副职"))
                    title = "副科级后备干部考察对象";
            }
            else if (this.Unitclass.Equals("乡(镇、街道)"))
            {
                title = "党政" + this.Qd + "后备干部考察对象";
            }
            else if (this.Unitclass.Equals("县管学校"))
            {
                if (this.Qd.Equals("正职"))
                    title = "正校级后备干部考察对象";

                else if (this.Qd.Equals("副职"))
                    title = "副校级后备干部考察对象";
            }
            else if (this.Unitclass.Equals("县管企业"))
            {
                title = "领导班子" + this.Qd + "后备干部考察对象";
            }


            #endregion

            //选择保存路径
            #region
            string savepath = "";
            SaveFileDialog sa = new SaveFileDialog();
            sa.Filter = "Document(*.doc)|*.doc";
            sa.FileName = "信息采集表";
            if (sa.ShowDialog() == DialogResult.OK)
                savepath = sa.FileName;
            else
                return;
            #endregion

            //创建word应用程序
            wordappliction = new Word.Application();

            //打开模板
            object filepath = System.Windows.Forms.Application.StartupPath + "\\wordModel" + "\\信息采集表.doc";
            mydoc = wordappliction.Documents.Open(ref filepath, ref missing, ref readOnly,
                                                  ref missing, ref missing, ref missing, ref missing, ref missing,
                                                  ref missing, ref missing, ref missing, ref isVisible, ref missing,
                                                  ref missing, ref missing, ref missing);
            //打开模板复制其中的表
            #region
            mydoc.ActiveWindow.Selection.WholeStory();
            mydoc.ActiveWindow.Selection.Select();
            mydoc.ActiveWindow.Selection.Copy();
            #endregion

            string selectid = "";

            for (int i = 0; i < idlist.Count; i++)
            {
                if (i == idlist.Count - 1)
                    selectid = selectid + "'" + idlist[i] + "'";
                else
                    selectid = selectid + "'" + idlist[i] + "',";
            }

            //读取后备干部的相关信息
            string sql = "select CID,name,department,position from TB_CommonInfo order by rank, joinTeam desc";
            string cond = "cid in (" + selectid + ")";
            DataOperation dataOp = new DataOperation();
            DataTable datatableT = dataOp.GetOneDataTable_sql(sql);
            DataTable datatable = new DataTable();
            datatable = datatableT.Clone();
            DataView dv = datatableT.AsDataView();
            dv.RowFilter = cond;
            datatable = dv.ToTable();

            //根据datatable中数据的数量进行文档的拷贝粘贴
            for (int i = 0; i < datatable.Rows.Count - 1; i++)
            #region
            {
                //加入两段避免了最后一行文本被弄到下面
                Word.Paragraph para1;
                Word.Paragraph para2;
                para1 = mydoc.Content.Paragraphs.Add(ref　missing);
                para2 = mydoc.Content.Paragraphs.Add(ref　missing);
                object pBreak = (int)WdBreakType.wdSectionBreakNextPage;
                para2.Range.InsertBreak(ref pBreak);
                //调用粘贴方法即可粘贴剪贴板中的内容。
                para2.Range.Paste();
            }
            #endregion

            //遍历datatable,读取后备干部信息，将后备干部信息写入文档
            for (int i = 0; i < datatable.Rows.Count; i++)
            #region
            {
                //表一:干部考察对象
                mydoc.Tables[i * 9 + 1].Cell(1, 1).Range.Text = title;
                mydoc.Tables[i * 9 + 1].Cell(4, 1).Range.InsertAfter(datatable.Rows[i]["department"].ToString());
                mydoc.Tables[i * 9 + 1].Cell(5, 1).Range.InsertAfter(datatable.Rows[i]["name"].ToString());
                mydoc.Tables[i * 9 + 1].Cell(6, 1).Range.InsertAfter(datatable.Rows[i]["position"].ToString());

                //表二：家庭主要成员及重要社会关系
                #region
                //得到该干部的家庭信息
                DataTable datatable2 = dataOp.GetOneDataTable_sql("select * from TB_Family where cid ='" + datatable.Rows[i]["CID"].ToString() + "'");
                int n = datatable2.Rows.Count;
                for (int j = 0; j < n; j++)
                {
                    mydoc.Tables[i * 9 + 2].Cell(j + 2, 1).Range.Text = datatable2.Rows[j]["relationship"].ToString();
                    mydoc.Tables[i * 9 + 2].Cell(j + 2, 2).Range.Text = datatable2.Rows[j]["name"].ToString();
                    string age = datatable2.Rows[j]["birthday"].ToString().Replace("年", ".");
                    mydoc.Tables[i * 9 + 2].Cell(j + 2, 3).Range.Text = age.Replace("月", "");
                    mydoc.Tables[i * 9 + 2].Cell(j + 2, 4).Range.Text = datatable2.Rows[j]["country"].ToString();
                    mydoc.Tables[i * 9 + 2].Cell(j + 2, 5).Range.Text = datatable2.Rows[j]["party"].ToString();
                    mydoc.Tables[i * 9 + 2].Cell(j + 2, 6).Range.Text = datatable2.Rows[j]["nation"].ToString();
                    mydoc.Tables[i * 9 + 2].Cell(j + 2, 7).Range.Text = datatable2.Rows[j]["deptJob"].ToString();
                    mydoc.Tables[i * 9 + 2].Cell(j + 2, 8).Range.Text = datatable2.Rows[j]["remark"].ToString();
                }
                #endregion

                datatable2.Clear();
                //表三：海外学习
                #region
                datatable2 = dataOp.GetOneDataTable_sql("select * from TB_SAbroad where cid ='" + datatable.Rows[i]["CID"].ToString() + "'");
                n = datatable2.Rows.Count;
                for (int j = 0; j < n; j++)
                {
                    mydoc.Tables[i * 9 + 3].Cell(j + 2, 1).Range.Text = datatable2.Rows[j]["startTime"].ToString();
                    mydoc.Tables[i * 9 + 3].Cell(j + 2, 2).Range.Text = datatable2.Rows[j]["endTime"].ToString();
                    mydoc.Tables[i * 9 + 3].Cell(j + 2, 3).Range.Text = datatable2.Rows[j]["country"].ToString();
                    mydoc.Tables[i * 9 + 3].Cell(j + 2, 4).Range.Text = datatable2.Rows[j]["academy"].ToString();
                    mydoc.Tables[i * 9 + 3].Cell(j + 2, 5).Range.Text = datatable2.Rows[j]["degree"].ToString();
                }
                #endregion

                datatable2.Clear();
                //表四：海外工作
                #region
                datatable2 = dataOp.GetOneDataTable_sql("select * from TB_WAbroad where cid ='" + datatable.Rows[i]["CID"].ToString() + "'");
                n = datatable2.Rows.Count;
                for (int j = 0; j < n; j++)
                {
                    mydoc.Tables[i * 9 + 4].Cell(j + 2, 1).Range.Text = datatable2.Rows[j]["startTime"].ToString();
                    mydoc.Tables[i * 9 + 4].Cell(j + 2, 2).Range.Text = datatable2.Rows[j]["endTime"].ToString();
                    mydoc.Tables[i * 9 + 4].Cell(j + 2, 3).Range.Text = datatable2.Rows[j]["abroadCountry"].ToString();
                    mydoc.Tables[i * 9 + 4].Cell(j + 2, 4).Range.Text = datatable2.Rows[j]["departmentPosition"].ToString();
                    mydoc.Tables[i * 9 + 4].Cell(j + 2, 5).Range.Text = datatable2.Rows[j]["specialtyArea"].ToString();
                }
                #endregion

                //重大事项信息
                #region
                datatable2 = dataOp.GetOneDataTable_sql("select * from TB_GreatContent where CID = '" + datatable.Rows[i]["CID"].ToString() + "'");
                for (int r = 0; r < datatable2.Rows.Count; r++)
                {
                    string connect = null;
                    //第五张表
                    #region
                    if (datatable2.Rows[r]["matter"].ToString().Equals("1"))
                    {
                        connect = datatable2.Rows[r]["content"].ToString();
                        if (connect == null || connect.Equals(""))
                            connect = "无";
                        mydoc.Tables[i * 9 + 5].Cell(2, 2).Range.Text = connect;
                        continue;
                    }
                    else if (datatable2.Rows[r]["matter"].ToString().Equals("2"))
                    {
                        connect = datatable2.Rows[r]["content"].ToString();
                        if (connect == null || connect.Equals(""))
                            connect = "无";
                        mydoc.Tables[i * 9 + 5].Cell(3, 2).Range.Text = connect;
                        continue;
                    }
                    else if (datatable2.Rows[r]["matter"].ToString().Equals("3"))
                    {
                        connect = datatable2.Rows[r]["content"].ToString();
                        if (connect == null || connect.Equals(""))
                            connect = "无";
                        mydoc.Tables[i * 9 + 5].Cell(4, 2).Range.Text = connect;
                        continue;
                    }
                    else if (datatable2.Rows[r]["matter"].ToString().Equals("4"))
                    {
                        connect = datatable2.Rows[r]["content"].ToString();
                        if (connect == null || connect.Equals(""))
                            connect = "无";
                        mydoc.Tables[i * 9 + 5].Cell(5, 2).Range.Text = connect;
                        continue;
                    }
                    else if (datatable2.Rows[r]["matter"].ToString().Equals("5"))
                    {
                        connect = datatable2.Rows[r]["content"].ToString();
                        if (connect == null || connect.Equals(""))
                            connect = "无";
                        mydoc.Tables[i * 9 + 5].Cell(6, 2).Range.Text = connect;
                        continue;
                    }
                    #endregion
                    //第六张表
                    #region
                    else if (datatable2.Rows[r]["matter"].ToString().Equals("6"))
                    {
                        connect = datatable2.Rows[r]["content"].ToString();
                        if (connect == null || connect.Equals(""))
                            connect = "无";
                        mydoc.Tables[i * 9 + 6].Cell(2, 2).Range.Text = connect;
                        continue;
                    }

                    else if (datatable2.Rows[r]["matter"].ToString().Equals("7"))
                    {
                        connect = datatable2.Rows[r]["content"].ToString();
                        if (connect == null || connect.Equals(""))
                            connect = "无";
                        mydoc.Tables[i * 9 + 6].Cell(3, 2).Range.Text = connect;
                        continue;
                    }
                    else if (datatable2.Rows[r]["matter"].ToString().Equals("8"))
                    {
                        connect = datatable2.Rows[r]["content"].ToString();
                        if (connect == null || connect.Equals(""))
                            connect = "无";
                        mydoc.Tables[i * 9 + 6].Cell(4, 2).Range.Text = connect;
                        continue;
                    }
                    else if (datatable2.Rows[r]["matter"].ToString().Equals("9"))
                    {
                        connect = datatable2.Rows[r]["content"].ToString();
                        if (connect == null || connect.Equals(""))
                            connect = "无";
                        mydoc.Tables[i * 9 + 6].Cell(5, 2).Range.Text = connect;
                        continue;
                    }
                    #endregion
                }
                #endregion


                datatable2.Clear();
                //表七：熟悉外语语种
                #region
                datatable2 = dataOp.GetOneDataTable_sql("select * from TB_FamiliarForeign where cid ='" + datatable.Rows[i]["CID"].ToString() + "'");
                n = datatable2.Rows.Count;
                for (int j = 0; j < n; j++)
                {
                    string level = datatable2.Rows[j]["level"].ToString();
                    mydoc.Tables[i * 9 + 7].Cell(j + 3, 2).Range.Text = datatable2.Rows[j]["foreignKind"].ToString();

                    if (level.Equals("精通"))
                        mydoc.Tables[i * 9 + 7].Cell(j + 3, 3).Range.Text = "√";
                    else if (level.Equals("熟练"))
                        mydoc.Tables[i * 9 + 7].Cell(j + 3, 4).Range.Text = "√";
                    else if (level.Equals("良好"))
                        mydoc.Tables[i * 9 + 7].Cell(j + 3, 5).Range.Text = "√";
                    else if (level.Equals("一般"))
                        mydoc.Tables[i * 9 + 7].Cell(j + 3, 6).Range.Text = "√";
                }
                #endregion

                datatable2.Clear();
                //表八：参加培训及实践锻炼情况
                #region
                datatable2 = dataOp.GetOneDataTable_sql("select * from TB_TrainExercise where cid ='" + datatable.Rows[i]["CID"].ToString() + "'");

                for (int r = 0; r < datatable2.Rows.Count; r++)
                {
                    string starttime = datatable2.Rows[r]["startTime"].ToString().Replace("年", ".");
                    string endtime = datatable2.Rows[r]["endTime"].ToString().Replace("年", ".");
                    if (datatable2.Rows[r]["reportMatter"].ToString().Equals("参加培训情况"))
                        mydoc.Tables[i * 9 + 8].Cell(2, 2).Range.InsertAfter(starttime.Replace("月", ".").Replace("日", "") + "到" + endtime.Replace("月", ".").Replace("日", "") + "：" + datatable2.Rows[r]["Content"].ToString() + "\n");
                    if (datatable2.Rows[r]["reportMatter"].ToString().Equals("参加实践锻炼情况"))
                        mydoc.Tables[i * 9 + 8].Cell(3, 2).Range.InsertAfter(starttime.Replace("月", ".").Replace("日", "") + "到" + endtime.Replace("月", ".").Replace("日", "") + "：" + datatable2.Rows[r]["Content"].ToString() + "\n");
                }
                #endregion

                datatable2.Clear();
                //表九：培养锻炼措施需求
                datatable2 = dataOp.GetOneDataTable_sql("select * from TB_TrainMethord where cid ='" + datatable.Rows[i]["CID"].ToString() + "'");

                for (int r = 0; r < datatable2.Rows.Count; r++)
                {
                    #region
                    string select = datatable2.Rows[r]["options"].ToString();
                    if (select.Equals("1"))
                    {
                        mydoc.Tables[i * 9 + 9].Cell(1, 2).Range.Text = "√"; continue;
                    }
                    else if (select.Equals("2"))
                    {
                        mydoc.Tables[i * 9 + 9].Cell(2, 2).Range.Text = "√"; continue;
                    }
                    else if (select.Equals("3"))
                    {
                        mydoc.Tables[i * 9 + 9].Cell(3, 2).Range.Text = "√"; continue;
                    }
                    else if (select.Equals("4"))
                    {
                        mydoc.Tables[i * 9 + 9].Cell(4, 2).Range.Text = "√"; continue;
                    }
                    else if (select.Equals("5"))
                    {
                        mydoc.Tables[i * 9 + 9].Cell(5, 2).Range.Text = "√"; continue;
                    }
                    else if (select.Equals("6"))
                    {
                        mydoc.Tables[i * 9 + 9].Cell(6, 2).Range.Text = "√"; continue;
                    }
                    else if (select.Equals("7"))
                    {
                        mydoc.Tables[i * 9 + 9].Cell(7, 2).Range.Text = "√"; continue;
                    }
                    else if (select.Equals("8"))
                    {
                        mydoc.Tables[i * 9 + 9].Cell(8, 2).Range.Text = "√"; continue;
                    }
                    else if (select.Equals("9"))
                    {
                        mydoc.Tables[i * 9 + 9].Cell(9, 2).Range.Text = "√"; continue;
                    }
                    else if (select.Equals("10"))
                    {
                        mydoc.Tables[i * 9 + 9].Cell(10, 2).Range.Text = "√"; continue;
                    }
                    else if (select.Equals("11"))
                    {
                        mydoc.Tables[i * 9 + 9].Cell(11, 2).Range.Text = "√"; continue;
                    }
                    else if (select.Equals("12"))
                    {
                        mydoc.Tables[i * 9 + 9].Cell(12, 2).Range.Text = "√"; continue;
                    }
                    else if (select.Equals("13"))
                    {
                        mydoc.Tables[i * 9 + 9].Cell(13, 2).Range.Text = "√"; continue;
                    }
                    else if (select.Equals("14"))
                    {
                        mydoc.Tables[i * 9 + 9].Cell(14, 2).Range.Text = "√";
                        mydoc.Tables[i * 9 + 9].Cell(15, 1).Range.Text = datatable2.Rows[r]["note14"].ToString();
                        continue;
                    }
                    #endregion
                }
                datatable2.Clear();

            }
            #endregion
            //#region
            object path = savepath;
            //wordappliction.Documents.Save(path);
            object myobj = System.Reflection.Missing.Value; ;
            mydoc.SaveAs(ref path, ref myobj, ref myobj, ref myobj, ref myobj, ref myobj,
                ref myobj, ref myobj, ref myobj, ref myobj, ref myobj, ref myobj,
                ref myobj, ref myobj, ref myobj, ref myobj);
            //关闭退出文档
            #region
            //关闭文档
            mydoc.Close(ref myobj, ref myobj, ref myobj);
            //退出应用程序。
            wordappliction.Quit();
            #endregion
            MessageBox.Show("导出成功!");
        }
    }
}
