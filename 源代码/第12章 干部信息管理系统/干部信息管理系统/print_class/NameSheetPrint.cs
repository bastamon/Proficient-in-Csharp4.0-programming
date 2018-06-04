using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using Word;
using System.Reflection;
using System.Windows.Forms;
using System.Collections;
using System.Data;
using HBMISR.Data;
using System.IO;
using System.Drawing;


namespace HBMISR.print_class
{
    /// <summary>
    /// 导出Word文档，功能：导出简要信息登记表
    /// </summary>
    class NameSheetPrint
    {
        //定义变量
        #region
        Word._Application wordappliction = null;
        Word._Document mydoc = null;   //作用：实现复制
        object missing = System.Reflection.Missing.Value;
        object readOnly = false;
        object isVisible = true;
        #endregion
        /// <summary>
        /// 打印简要情况登记表
        /// </summary>
        public NameSheetPrint()
        {
            if (Directory.Exists(System.Windows.Forms.Application.StartupPath + "\\图片夹"))
            {
                Directory.Delete(System.Windows.Forms.Application.StartupPath + "\\图片夹", true);
            }
            Directory.CreateDirectory(System.Windows.Forms.Application.StartupPath + "\\图片夹");
        }
        /// <summary>
        /// 导出所选后备干部简要情况登记表
        /// </summary>
        /// <param name="idlist">所选后备干部id号集合</param>
        public void exportword(ArrayList idlist)  //建议把Datatable类型的参数传过来，建议读视图。
        {
            //弹出对话框，选择保存的路径
            #region
            string savepath = "";
            SaveFileDialog sa = new SaveFileDialog();
            sa.Filter = "Document(*.doc)|*.doc";
            sa.FileName = "简要情况登记表";
            if (sa.ShowDialog() == DialogResult.OK)
                savepath = sa.FileName;
            else
                return;
            #endregion

            //创建word应用程序
            wordappliction = new Word.Application();

            //打开指定路径的内容
            #region
            object filepath = System.Windows.Forms.Application.StartupPath + "\\wordModel" + "\\简要情况.doc";
            mydoc = wordappliction.Documents.Open(ref filepath, ref missing, ref readOnly,
                                                  ref missing, ref missing, ref missing, ref missing, ref missing,
                                                  ref missing, ref missing, ref missing, ref isVisible, ref missing,
                                                  ref missing, ref missing, ref missing);
            #endregion
            ////设置word创建的word程序的可见性
            wordappliction.Visible = true;

            //将整个活动区域全部复制
            #region
            mydoc.ActiveWindow.Selection.WholeStory();
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

            ImageCollection(selectid);

            //读取后备干部的相关信息
            string sql = "select * from TB_CommonInfo order by rank, joinTeam desc";
            string cond = "cid in (" + selectid + ")";
            DataOperation dataOp = new DataOperation();
            DataTable datatableT = dataOp.GetOneDataTable_sql(sql);
            DataTable datatable = new DataTable();
            datatable = datatableT.Clone();
            DataView dv = datatableT.AsDataView();
            dv.RowFilter = cond;
            datatable = dv.ToTable();

            Object myobj = Missing.Value;
            for (int i = 0; i < datatable.Rows.Count - 1; i++)
            {
                //加入两段避免了最后一行文本被弄到下面
                Word.Paragraph para1;
                Word.Paragraph para2;
                para1 = mydoc.Content.Paragraphs.Add(ref　myobj);
                para2 = mydoc.Content.Paragraphs.Add(ref　myobj);
                object pBreak = (int)WdBreakType.wdSectionBreakNextPage;
                para2.Range.InsertBreak(ref pBreak);
                //调用粘贴方法即可粘贴剪贴板中的内容。
                para2.Range.Paste();
            }
            Shape s = null;
            for (int i = 0; i < datatable.Rows.Count; i++)
            {
                mydoc.ActiveWindow.Selection.WholeStory();
                wordappliction.Selection.Tables[2 * i + 1].Cell(1, 2).Range.Text = datatable.Rows[i]["name"].ToString(); //姓名
                wordappliction.Selection.Tables[2 * i + 1].Cell(1, 4).Range.Text = datatable.Rows[i]["sex"].ToString();//性别;
                string age = datatable.Rows[i]["birthday"].ToString().Replace("年", ".");

                int nowAge = 0;
                int temp1 = Convert.ToInt32(datatable.Rows[i]["birthday"].ToString().Substring(5, 2));
                int now1 = Convert.ToInt32(DateTime.Now.ToString("MM"));
                int temp2 = Convert.ToInt32(datatable.Rows[i]["birthday"].ToString().Substring(0, 4));
                int now2 = Convert.ToInt32(DateTime.Now.ToString("yyyy"));
                if (temp1 <= now1)
                {
                    nowAge = now2 - temp2;
                }
                else
                {
                    nowAge = now2 - temp2 - 1;
                }
                wordappliction.Selection.Tables[2 * i + 1].Cell(1, 6).Range.Text = age.Replace("月", "") + "\n(" + nowAge + "岁)"; //出生年月

                string filepath1 = System.Windows.Forms.Application.StartupPath + "\\图片夹\\" + datatable.Rows[i]["cid"].ToString() + ".jpg";

                if (System.IO.File.Exists(filepath1))
                {

                    double height = 0.0;
                    double width = 0.0;
                    Image pic = Image.FromFile(filepath1);//strFilePath是该图片的绝对路径
                    int intWidth = pic.Width;//长度像素值
                    int intHeight = pic.Height;//高度像素值
                    pic.Dispose();
                    if ((double)intHeight / intWidth > 118 / 91.0)
                    {
                        height = 118;
                        width = 118 * (double)intWidth / intHeight;
                    }
                    else
                    {
                        width = 91;
                        height = 91 * (double)intHeight / intWidth;
                    }
                    object LinkToFile = false;
                    object SaveWithDocument = true;
                    object Anchor = wordappliction.Selection.Tables[2 * i + 1].Cell(1, 7).Range;

                    wordappliction.ActiveDocument.InlineShapes.AddPicture(filepath1, ref LinkToFile, ref SaveWithDocument, ref Anchor);

                    wordappliction.ActiveDocument.InlineShapes[1].Select();
                    wordappliction.ActiveDocument.InlineShapes[1].Width = (int)width;//图片宽度

                    wordappliction.ActiveDocument.InlineShapes[1].Height = (int)height;//图片高度
                    try
                    {
                        s = wordappliction.ActiveDocument.InlineShapes[1].ConvertToShape();
                        s.WrapFormat.Type = Word.WdWrapType.wdWrapNone;
                        s = null;
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                    mydoc.ActiveWindow.Selection.WholeStory();
                }

                wordappliction.Selection.Tables[2 * i + 1].Cell(2, 2).Range.Text = datatable.Rows[i]["nation"].ToString(); //民族
                wordappliction.Selection.Tables[2 * i + 1].Cell(2, 4).Range.Text = datatable.Rows[i]["native"].ToString();//籍贯
                wordappliction.Selection.Tables[2 * i + 1].Cell(2, 6).Range.Text = datatable.Rows[i]["birthplace"].ToString();//出生地
                string partytime = datatable.Rows[i]["partyTime"].ToString().Replace("年", ".");
                wordappliction.Selection.Tables[2 * i + 1].Cell(3, 2).Range.Text = partytime.Replace("月", ""); //入党时间
                string worktime = datatable.Rows[i]["workTime"].ToString().Replace("年", ".");
                wordappliction.Selection.Tables[2 * i + 1].Cell(3, 4).Range.Text = worktime.Replace("月", ""); //参加工作时间
                wordappliction.Selection.Tables[2 * i + 1].Cell(3, 6).Range.Text = datatable.Rows[i]["health"].ToString(); //健康状况
                wordappliction.Selection.Tables[2 * i + 1].Cell(4, 2).Range.Text = datatable.Rows[i]["technicalPost"].ToString(); //专业技术职务
                wordappliction.Selection.Tables[2 * i + 1].Cell(4, 4).Range.Text = datatable.Rows[i]["specialtySkill"].ToString(); //熟悉专业有何专长
                wordappliction.Selection.Tables[2 * i + 1].Cell(5, 3).Range.Text = datatable.Rows[i]["fullEducation"].ToString() + datatable.Rows[i]["fullDegree"].ToString();//全日制教育
                wordappliction.Selection.Tables[2 * i + 1].Cell(5, 5).Range.Text = datatable.Rows[i]["fullSchool"].ToString() + datatable.Rows[i]["fullSpecialty"].ToString();//毕业院校及专业
                wordappliction.Selection.Tables[2 * i + 1].Cell(6, 3).Range.Text = datatable.Rows[i]["workEducation"].ToString() + datatable.Rows[i]["workDegree"].ToString();//在职教育
                wordappliction.Selection.Tables[2 * i + 1].Cell(6, 5).Range.Text = datatable.Rows[i]["workGraduate"].ToString() + datatable.Rows[i]["workSpecialty"].ToString();
                wordappliction.Selection.Tables[2 * i + 1].Cell(7, 2).Range.Text = datatable.Rows[i]["position"].ToString();
                wordappliction.Selection.Tables[2 * i + 1].Cell(8, 2).Range.Text = datatable.Rows[i]["knowField"].ToString();
                wordappliction.Selection.Tables[2 * i + 1].Cell(9, 2).Range.Text = datatable.Rows[i]["trainDirection"].ToString(); //"培养方向";
                wordappliction.Selection.Tables[2 * i + 1].Cell(10, 2).Range.Text = datatable.Rows[i]["trainMeasure"].ToString(); //"培养措施";
                wordappliction.Selection.Tables[2 * i + 1].Cell(11, 2).Range.Text = "\n" + HBresume(datatable.Rows[i]["CID"].ToString());//"简历";

                //第二页：奖惩情况，年度考核，家庭成员关系
                mydoc.ActiveWindow.Selection.WholeStory(); //表示在选中的范围内
                wordappliction.Selection.Tables[2 * i + 2].Cell(1, 2).Range.Text = HBrewardsAndpunishment(datatable.Rows[i]["CID"].ToString());//"奖惩情况";
                wordappliction.Selection.Tables[2 * i + 2].Cell(2, 2).Range.Text = HByearcheck(datatable.Rows[i]["CID"].ToString());// "年度考核结果";\

                //得到该干部的家庭信息
                DataTable famliy_dt = dataOp.GetOneDataTable_sql("select * from TB_Family where cid ='" + datatable.Rows[i]["CID"].ToString() + "'");

                int n = famliy_dt.Rows.Count; //家庭人员的个数
                for (int j = 0; j < n; j++)
                {
                    wordappliction.Selection.Tables[2 * i + 2].Cell(j + 4, 2).Range.Text = famliy_dt.Rows[j]["relationship"].ToString(); //"称谓";
                    wordappliction.Selection.Tables[2 * i + 2].Cell(j + 4, 3).Range.Text = famliy_dt.Rows[j]["name"].ToString(); //"姓名";
                    if (famliy_dt.Rows[j]["age"].ToString() == "0")
                        wordappliction.Selection.Tables[2 * i + 2].Cell(j + 4, 4).Range.Text = "";
                    else
                        wordappliction.Selection.Tables[2 * i + 2].Cell(j + 4, 4).Range.Text = famliy_dt.Rows[j]["age"].ToString();//"年龄";
                    wordappliction.Selection.Tables[2 * i + 2].Cell(j + 4, 5).Range.Text = famliy_dt.Rows[j]["party"].ToString();//"政治面貌";
                    if (famliy_dt.Rows[j]["remark"].ToString().Trim() == "已故")
                    {
                        wordappliction.Selection.Tables[2 * i + 2].Cell(j + 4, 6).Range.Text = famliy_dt.Rows[j]["deptJob"].ToString() + "(已故)";//"工作单位及职务";
                    }
                    else
                        wordappliction.Selection.Tables[2 * i + 2].Cell(j + 4, 6).Range.Text = famliy_dt.Rows[j]["deptJob"].ToString();//"工作单位及职务";
                }
            }
            //把文件保存在选定的保存路径上
            #region
            object path = savepath;
            //wordappliction.Documents.Save(path);
            mydoc.SaveAs(ref path, ref myobj, ref myobj, ref myobj, ref myobj, ref myobj,
                ref myobj, ref myobj, ref myobj, ref myobj, ref myobj, ref myobj,
                ref myobj, ref myobj, ref myobj, ref myobj);
            #endregion

            //关闭退出文档
            #region
            //关闭文档
            mydoc.Close(ref myobj, ref myobj, ref myobj);

            //退出应用程序。
            wordappliction.Quit();
            #endregion
            MessageBox.Show("导出成功!");
            return;
        }

        //后备干部简历
        private string HBresume(string cid)
        {
            string resume = "";
            //string sql = "select * from TB_Resume where cid ='" + cid + "'order by betime";
            string sql = "select * from TB_Resume where cid ='" + cid + "'";
            DataOperation dataOp = new DataOperation();
            DataTable datatable = dataOp.GetOneDataTable_sql(sql);

            string content = "";//记录简历内容的临时变量
            double fc = 0.0;//用来记录content的长度

            for (int i = 0; i < datatable.Rows.Count; i++)
            {
                if (!datatable.Rows[i]["entime"].ToString().Equals(""))
                {
                    resume = resume + todate(datatable.Rows[i]["betime"].ToString()) + "--" + todate(datatable.Rows[i]["entime"].ToString()) + " ";
                }
                else
                {
                    resume = resume + todate(datatable.Rows[i]["betime"].ToString()) + "--        ";
                }
                content = datatable.Rows[i]["content"].ToString();

                foreach (char c in content)
                {
                    //c为字母或者数字
                    if ((c >= 'a' && c <= 'z') || (c >= 'A' && c <= 'Z') || (c >= '0' && c <= '9'))
                        fc += 0.5;
                    else
                        fc += 1;

                    resume += c;

                    //换行并且填充空白
                    if (fc >= 18 && (int)fc % 18 == 0)
                    {
                        resume += "\n                 ";
                        fc = 0.0;
                    }
                }
                //读取一条记录后在后面加换行，若为最后一条不加换行
                if (i == datatable.Rows.Count - 1)
                { }
                else
                    resume += "\n";

                fc = 0.0;
            }

            return resume;
        }

        /// <summary>
        /// 将****年**月改为****.**
        /// </summary>
        /// <param name="date">****年**月</param>
        /// <returns>****.**格式的日期</returns>
        private string todate(string date)
        {
            date = date.Replace("年", ".");
            date = date.Replace("月", "");
            return date;
        }


        /// <summary>
        /// 后备干部的奖惩情况
        /// </summary>
        /// <param name="cid">后备干部id号</param>
        /// <returns>奖惩情况</returns>
        private string HBrewardsAndpunishment(string cid)
        {
            string rewardsAndpunishment = "";

            string sql = "select * from TB_PunishAward where cid ='" + cid + "'order by time";

            DataOperation dataOp = new DataOperation();
            DataTable datatable = dataOp.GetOneDataTable_sql(sql);

            for (int i = 0; i < datatable.Rows.Count; i++)
            {
                //如果第i条是奖励
                if (datatable.Rows[i]["class"].ToString().Equals("奖"))
                {
                    rewardsAndpunishment = rewardsAndpunishment + "\t\t" + datatable.Rows[i]["time"].ToString() + "，" + datatable.Rows[i]["department"].ToString() + "(" + datatable.Rows[i]["grade"].ToString() + ")" + datatable.Rows[i]["name"].ToString();
                }

                //如果第i条是惩罚
                if (datatable.Rows[i]["class"].ToString().Equals("惩"))
                {
                    rewardsAndpunishment = rewardsAndpunishment + "\t\t" + datatable.Rows[i]["time"].ToString() + "，" + datatable.Rows[i]["department"].ToString() + "(" + datatable.Rows[i]["grade"].ToString() + "'" + datatable.Rows[i]["name"].ToString();
                }

                //读取一条记录后在后面加换行，若为最后一条不加换行
                if (i == datatable.Rows.Count - 1)
                { }
                else
                    rewardsAndpunishment += "\n";
            }

            return rewardsAndpunishment;
        }

        /// <summary>
        /// 后备干部年度考核
        /// </summary>
        /// <param name="cid">后备干部id号</param>
        /// <returns>考核结果</returns>
        private string HByearcheck(string cid)
        {
            string yearcheck = "";
            string sql = "select startTime,result1,result2,result3 from TB_CommonInfo where cid ='" + cid + "'";
            DataOperation dataOp = new DataOperation();
            DataTable datatable = dataOp.GetOneDataTable_sql(sql);

            int year1, year2, year3;
            string yearcheck1, yearcheck2, yearcheck3;

            string s = datatable.Rows[0]["startTime"].ToString();
            if (s == null || s.Equals(""))
            {
                return "";
            }
            else
            {
                year1 = Convert.ToInt32(s);
            }

            year2 = year1 + 1;
            year3 = year1 + 2;


            yearcheck1 = datatable.Rows[0]["result1"].ToString();
            yearcheck2 = datatable.Rows[0]["result2"].ToString();
            yearcheck3 = datatable.Rows[0]["result3"].ToString();

            //3年都不一样
            if ((!yearcheck1.Equals(yearcheck2)) && (!yearcheck1.Equals(yearcheck3)) && (!yearcheck2.Equals(yearcheck3)))
            {
                yearcheck = "\t\t" + year1.ToString() + "年年度考核为" + yearcheck1 + "，" + year2.ToString() + "年年度考核为" + yearcheck2 + "，" + year3.ToString() + "年年度考核为" + yearcheck3 + "。";
            }
            //1、2年一样，1、2年与3年不一样
            else if ((yearcheck1.Equals(yearcheck2)) && (!yearcheck1.Equals(yearcheck3)))
            {
                yearcheck = "\t\t" + year1.ToString() + "—" + year2.ToString() + "年年度考核均为" + yearcheck1 + "，" + year3.ToString() + "年年度考核为" + yearcheck3 + "。";
            }
            //1、3年一样，1、3年与2年不一样
            else if ((yearcheck1.Equals(yearcheck3)) && (!yearcheck1.Equals(yearcheck2)))
            {
                yearcheck = "\t\t" + year1.ToString() + "年、" + year3.ToString() + "年年度考核均为" + yearcheck1 + "，" + year2.ToString() + "年年度考核为" + yearcheck2 + "。";
            }
            //2、3年一样，2、3年与1年不一样
            else if ((yearcheck2.Equals(yearcheck3)) && (!yearcheck2.Equals(yearcheck1)))
            {
                yearcheck = "\t\t" + year1.ToString() + "年年度考核为" + yearcheck1 + "，" + year2.ToString() + "—" + year3.ToString() + "年年度考核均为" + yearcheck2 + "。";
            }
            //3年都一样
            else if ((yearcheck1.Equals(yearcheck2)) && (yearcheck1.Equals(yearcheck3)))
            {
                yearcheck = "\t\t" + year1.ToString() + "—" + year3.ToString() + "年年度考核均为" + yearcheck1 + "。";
            }
            return yearcheck;
        }

        /// <summary>
        /// 储存后备干部照片
        /// </summary>
        /// <param name="selectid"></param>
        public void ImageCollection(string selectid)
        {
            DataTable datatable;
            DataOperation da = new DataOperation();
            //读取后备干部的相关信息
            string sql = "select cid, photo from TB_CommonInfo where cid in (" + selectid + ")";
            datatable = da.GetOneDataTable_sql(sql);

            for (int i = 0; i < datatable.Rows.Count; i++)
            {
                if (datatable.Rows[i]["photo"] != System.DBNull.Value)
                {
                    byte[] m = (byte[])datatable.Rows[i]["photo"];

                    FileStream fa = new FileStream(System.Windows.Forms.Application.StartupPath + "\\图片夹\\" + datatable.Rows[i]["cid"].ToString() + ".jpg", FileMode.CreateNew);
                    BinaryWriter writefile = new BinaryWriter(fa);
                    writefile.Write(m, 0, m.Length);
                    fa.Close();
                }
            }
        }
    }
}
