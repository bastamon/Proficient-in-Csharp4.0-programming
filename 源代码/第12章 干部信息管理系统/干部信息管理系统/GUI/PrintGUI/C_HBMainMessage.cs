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
using System.IO;
using System.Reflection;
using HBMISR.Data;
using HBMISR.Service;

namespace HBMISR.GUI.PrintGUI
{
    /// <summary>
    /// 该容器显示的是后备干部简要情况登记表
    /// </summary>
    public partial class C_HBMainMessage : UserControl
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
        /// <summary>
        /// //单位类别
        /// </summary>
        public string Unitclass
        {
            get { return unitclass; }
            set { unitclass = value; }
        }

        /// <summary>
        /// //正副职
        /// </summary>
        private string qd;
        /// <summary>
        /// //正副职
        /// </summary>
        public string Qd
        {
            get { return qd; }
            set { qd = value; }
        }

        /// <summary>
        /// //单位编号
        /// </summary>
        /// 
        private string uid;
        public string Uid
        {
            get { return uid; }
            set { uid = value; }
        }

        /// <summary>
        /// 打印后备干部简要情况登记表构造函数
        /// </summary>
        public C_HBMainMessage()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 打印后备干部简要情况登记表初始化
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void C_HBMainMessage_Load(object sender, EventArgs e)
        {
            this.RV_HBMainMessage.SetDisplayMode(DisplayMode.PrintLayout);

            //创建打印照片的临时文件夹
            //存在则删除并创建，删除过去用过的图片，防止因图片出现的错误
            if (Directory.Exists(Application.StartupPath + "\\printimage"))
            {
                Directory.Delete(Application.StartupPath + "\\printimage", true);
                Directory.CreateDirectory(Application.StartupPath + "\\printimage");
            }
            else //不存在创建
            {
                Directory.CreateDirectory(Application.StartupPath + "\\printimage");
            }

            int i = 0;
            string selectid = "";

            for (i = 0; i < idlist.Count; i++)
            {
                if (i == idlist.Count - 1)
                    selectid = selectid + "'" + idlist[i] + "'";
                else
                    selectid = selectid + "'" + idlist[i] + "',";
            }

            ////读取后备干部的相关信息
            //string sql = "select * from TB_CommonInfo where cid in (" + selectid + ")  order by rank";

            //DataOperation dataOp = new DataOperation();
            //DataTable datatable = dataOp.GetOneDataTable_sql(sql);
            string sql = "select * from TB_CommonInfo order by rank,joinTeam desc";
            string cond = "cid in (" + selectid + ")";
            DataOperation dataOp = new DataOperation();
            DataTable datatable1 = dataOp.GetOneDataTable_sql(sql);
            DataTable datatable = new DataTable();
            datatable = datatable1.Clone();
            DataView dv = datatable1.AsDataView();
            dv.RowFilter = cond;
            datatable = dv.ToTable();

            //用来存放后备干部的家庭和社会关系
            DataTable famliy_dt = new DataTable();

            List<Class_HBMainMessage> HBMainMessageList = new List<Class_HBMainMessage>();

            for (i = 0; i < datatable.Rows.Count; i++)
            {
                Class_HBMainMessage c = new Class_HBMainMessage();

                #region
                //得到该干部的基本信息
                #region
                c.Cid = datatable.Rows[i]["CID"].ToString();
                c.Hbname = datatable.Rows[i]["name"].ToString();
                c.Sex = datatable.Rows[i]["sex"].ToString();
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
                c.Birthday = todate(datatable.Rows[i]["birthday"].ToString()) + "\n(" + nowAge + "岁)";
                c.Nation = datatable.Rows[i]["nation"].ToString();
                c.Native = datatable.Rows[i]["native"].ToString();
                c.Birthplace = datatable.Rows[i]["birthplace"].ToString();
                c.Partytime = todate(datatable.Rows[i]["partyTime"].ToString());
                c.Worktime = todate(datatable.Rows[i]["workTime"].ToString());
                c.Health = datatable.Rows[i]["health"].ToString();
                c.Technicalpost = datatable.Rows[i]["technicalPost"].ToString();
                c.Specialtskill = datatable.Rows[i]["specialtySkill"].ToString(); ;
                c.Fullteach = datatable.Rows[i]["fullEducation"].ToString() + datatable.Rows[i]["fullDegree"].ToString();
                c.FullGraduateSpecialty = datatable.Rows[i]["fullSchool"].ToString() + datatable.Rows[i]["fullSpecialty"].ToString();
                c.Workteach = datatable.Rows[i]["workEducation"].ToString() + datatable.Rows[i]["workDegree"].ToString();
                c.WorkGraduateSpecialty = datatable.Rows[i]["workGraduate"].ToString() + datatable.Rows[i]["workSpecialty"].ToString();
                c.Nowpost = datatable.Rows[i]["position"].ToString();
                c.Knowfiled = datatable.Rows[i]["knowField"].ToString();
                c.Traindirection = datatable.Rows[i]["trainDirection"].ToString();
                c.Trainmeasuer = datatable.Rows[i]["trainMeasure"].ToString();

                //打印图片
                byte[] b;
                if (datatable.Rows[i]["photo"] != System.DBNull.Value)
                {
                    b = (byte[])datatable.Rows[i]["photo"];
                    //创建图片路径及名称
                    FileStream fs = new FileStream(Application.StartupPath + "\\printimage\\" + c.Cid + ".jpg", FileMode.Create);
                    BinaryWriter bw = new BinaryWriter(fs);
                    try
                    {
                        bw.Write(b);//将byte[]写进图片文件
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                    finally
                    {//关闭文件流
                        bw.Close();
                        fs.Close();
                    }
                    c.Photo = "file:////" + Application.StartupPath + "\\printimage\\" + c.Cid + ".jpg";
                }
                #endregion
                //使用函数得到该干部的简历情况、奖惩情况、年度考核情况

                c.Resume = HBresume(c.Cid);
                c.RewardsAndpunishment = HBrewardsAndpunishment(c.Cid);
                c.Yearcheck = HByearcheck(c.Cid);

                //得到该干部的家庭信息
                famliy_dt = dataOp.GetOneDataTable_sql("select * from TB_Family where cid ='" + c.Cid + "'");

                //家庭和社会关系是7条
                #region
                if (famliy_dt.Rows.Count == 7)
                {
                    //第1条数据
                    #region
                    c.Appellation1 = famliy_dt.Rows[0]["relationship"].ToString();
                    c.Name1 = famliy_dt.Rows[0]["name"].ToString();
                    c.Age1 = famliy_dt.Rows[0]["age"].ToString();
                    c.Polityface1 = famliy_dt.Rows[0]["party"].ToString();
                    if (famliy_dt.Rows[0]["remark"].ToString().Trim() == "已故")
                        c.Unitjob1 = famliy_dt.Rows[0]["deptJob"].ToString() + "(已故)";
                    else
                        c.Unitjob1 = famliy_dt.Rows[0]["deptJob"].ToString();
                    #endregion

                    //第2条数据
                    #region
                    c.Appellation2 = famliy_dt.Rows[1]["relationship"].ToString();
                    c.Name2 = famliy_dt.Rows[1]["name"].ToString();
                    c.Age2 = famliy_dt.Rows[1]["age"].ToString();
                    c.Polityface2 = famliy_dt.Rows[1]["party"].ToString();
                    if (famliy_dt.Rows[1]["remark"].ToString().Trim() == "已故")
                        c.Unitjob2 = famliy_dt.Rows[1]["deptJob"].ToString() + "(已故)";
                    else
                        c.Unitjob2 = famliy_dt.Rows[1]["deptJob"].ToString();
                    #endregion

                    //第3条数据
                    #region
                    c.Appellation3 = famliy_dt.Rows[2]["relationship"].ToString();
                    c.Name3 = famliy_dt.Rows[2]["name"].ToString();
                    c.Age3 = famliy_dt.Rows[2]["age"].ToString();
                    c.Polityface3 = famliy_dt.Rows[2]["party"].ToString();
                    if (famliy_dt.Rows[2]["remark"].ToString().Trim() == "已故")
                        c.Unitjob3 = famliy_dt.Rows[2]["deptJob"].ToString() + "(已故)";
                    else
                        c.Unitjob3 = famliy_dt.Rows[2]["deptJob"].ToString();
                    #endregion

                    //第4条数据
                    #region
                    c.Appellation4 = famliy_dt.Rows[3]["relationship"].ToString();
                    c.Name4 = famliy_dt.Rows[3]["name"].ToString();
                    c.Age4 = famliy_dt.Rows[3]["age"].ToString();
                    c.Polityface4 = famliy_dt.Rows[3]["party"].ToString();
                    if (famliy_dt.Rows[3]["remark"].ToString().Trim() == "已故")
                        c.Unitjob4 = famliy_dt.Rows[3]["deptJob"].ToString() + "(已故)";
                    else
                        c.Unitjob4 = famliy_dt.Rows[3]["deptJob"].ToString();
                    #endregion

                    //第5条数据
                    #region
                    c.Appellation5 = famliy_dt.Rows[4]["relationship"].ToString();
                    c.Name5 = famliy_dt.Rows[4]["name"].ToString();
                    c.Age5 = famliy_dt.Rows[4]["age"].ToString();
                    c.Polityface5 = famliy_dt.Rows[4]["party"].ToString();
                    if (famliy_dt.Rows[4]["remark"].ToString().Trim() == "已故")
                        c.Unitjob5 = famliy_dt.Rows[4]["deptJob"].ToString() + "(已故)";
                    else
                        c.Unitjob5 = famliy_dt.Rows[4]["deptJob"].ToString();
                    #endregion

                    //第6条数据
                    #region
                    c.Appellation6 = famliy_dt.Rows[5]["relationship"].ToString();
                    c.Name6 = famliy_dt.Rows[5]["name"].ToString();
                    c.Age6 = famliy_dt.Rows[5]["age"].ToString();
                    c.Polityface6 = famliy_dt.Rows[5]["party"].ToString();
                    if (famliy_dt.Rows[5]["remark"].ToString().Trim() == "已故")
                        c.Unitjob6 = famliy_dt.Rows[5]["deptJob"].ToString() + "(已故)";
                    else
                        c.Unitjob6 = famliy_dt.Rows[5]["deptJob"].ToString();
                    #endregion

                    //第7条数据
                    #region
                    c.Appellation7 = famliy_dt.Rows[6]["relationship"].ToString();
                    c.Name7 = famliy_dt.Rows[6]["name"].ToString();
                    c.Age7 = famliy_dt.Rows[6]["age"].ToString();
                    c.Polityface7 = famliy_dt.Rows[6]["party"].ToString();
                    if (famliy_dt.Rows[6]["remark"].ToString().Trim() == "已故")
                        c.Unitjob7 = famliy_dt.Rows[6]["deptJob"].ToString() + "(已故)";
                    else
                        c.Unitjob7 = famliy_dt.Rows[6]["deptJob"].ToString();
                    #endregion
                }
                #endregion

                //家庭和社会关系是6条
                #region
                else if (famliy_dt.Rows.Count == 6)
                {
                    //第1条数据
                    #region
                    c.Appellation1 = famliy_dt.Rows[0]["relationship"].ToString();
                    c.Name1 = famliy_dt.Rows[0]["name"].ToString();
                    c.Age1 = famliy_dt.Rows[0]["age"].ToString();
                    c.Polityface1 = famliy_dt.Rows[0]["party"].ToString();
                    if (famliy_dt.Rows[0]["remark"].ToString().Trim() == "已故")
                        c.Unitjob1 = famliy_dt.Rows[0]["deptJob"].ToString() + "(已故)";
                    else
                        c.Unitjob1 = famliy_dt.Rows[0]["deptJob"].ToString();
                    #endregion

                    //第2条数据
                    #region
                    c.Appellation2 = famliy_dt.Rows[1]["relationship"].ToString();
                    c.Name2 = famliy_dt.Rows[1]["name"].ToString();
                    c.Age2 = famliy_dt.Rows[1]["age"].ToString();
                    c.Polityface2 = famliy_dt.Rows[1]["party"].ToString();
                    if (famliy_dt.Rows[1]["remark"].ToString().Trim() == "已故")
                        c.Unitjob2 = famliy_dt.Rows[1]["deptJob"].ToString() + "(已故)";
                    else
                        c.Unitjob2 = famliy_dt.Rows[1]["deptJob"].ToString();
                    #endregion

                    //第3条数据
                    #region
                    c.Appellation3 = famliy_dt.Rows[2]["relationship"].ToString();
                    c.Name3 = famliy_dt.Rows[2]["name"].ToString();
                    c.Age3 = famliy_dt.Rows[2]["age"].ToString();
                    c.Polityface3 = famliy_dt.Rows[2]["party"].ToString();
                    if (famliy_dt.Rows[2]["remark"].ToString().Trim() == "已故")
                        c.Unitjob3 = famliy_dt.Rows[2]["deptJob"].ToString() + "(已故)";
                    else
                        c.Unitjob3 = famliy_dt.Rows[2]["deptJob"].ToString();
                    #endregion

                    //第4条数据
                    #region
                    c.Appellation4 = famliy_dt.Rows[3]["relationship"].ToString();
                    c.Name4 = famliy_dt.Rows[3]["name"].ToString();
                    c.Age4 = famliy_dt.Rows[3]["age"].ToString();
                    c.Polityface4 = famliy_dt.Rows[3]["party"].ToString();
                    if (famliy_dt.Rows[3]["remark"].ToString().Trim() == "已故")
                        c.Unitjob4 = famliy_dt.Rows[3]["deptJob"].ToString() + "(已故)";
                    else
                        c.Unitjob4 = famliy_dt.Rows[3]["deptJob"].ToString();
                    #endregion

                    //第5条数据
                    #region
                    c.Appellation5 = famliy_dt.Rows[4]["relationship"].ToString();
                    c.Name5 = famliy_dt.Rows[4]["name"].ToString();
                    c.Age5 = famliy_dt.Rows[4]["age"].ToString();
                    c.Polityface5 = famliy_dt.Rows[4]["party"].ToString();
                    if (famliy_dt.Rows[4]["remark"].ToString().Trim() == "已故")
                        c.Unitjob5 = famliy_dt.Rows[4]["deptJob"].ToString() + "(已故)";
                    else
                        c.Unitjob5 = famliy_dt.Rows[4]["deptJob"].ToString();
                    #endregion

                    //第6条数据
                    #region
                    c.Appellation6 = famliy_dt.Rows[5]["relationship"].ToString();
                    c.Name6 = famliy_dt.Rows[5]["name"].ToString();
                    c.Age6 = famliy_dt.Rows[5]["age"].ToString();
                    c.Polityface6 = famliy_dt.Rows[5]["party"].ToString();
                    if (famliy_dt.Rows[5]["remark"].ToString().Trim() == "已故")
                        c.Unitjob6 = famliy_dt.Rows[5]["deptJob"].ToString() + "(已故)";
                    else
                        c.Unitjob6 = famliy_dt.Rows[5]["deptJob"].ToString();
                    #endregion


                }
                #endregion

                //家庭和社会关系是5条
                #region
                else if (famliy_dt.Rows.Count == 5)
                {
                    //第1条数据
                    #region
                    c.Appellation1 = famliy_dt.Rows[0]["relationship"].ToString();
                    c.Name1 = famliy_dt.Rows[0]["name"].ToString();
                    c.Age1 = famliy_dt.Rows[0]["age"].ToString();
                    c.Polityface1 = famliy_dt.Rows[0]["party"].ToString();
                    if (famliy_dt.Rows[0]["remark"].ToString().Trim() == "已故")
                        c.Unitjob1 = famliy_dt.Rows[0]["deptJob"].ToString() + "(已故)";
                    else
                        c.Unitjob1 = famliy_dt.Rows[0]["deptJob"].ToString();
                    #endregion

                    //第2条数据
                    #region
                    c.Appellation2 = famliy_dt.Rows[1]["relationship"].ToString();
                    c.Name2 = famliy_dt.Rows[1]["name"].ToString();
                    c.Age2 = famliy_dt.Rows[1]["age"].ToString();
                    c.Polityface2 = famliy_dt.Rows[1]["party"].ToString();
                    if (famliy_dt.Rows[1]["remark"].ToString().Trim() == "已故")
                        c.Unitjob2 = famliy_dt.Rows[1]["deptJob"].ToString() + "(已故)";
                    else
                        c.Unitjob2 = famliy_dt.Rows[1]["deptJob"].ToString();
                    #endregion

                    //第3条数据
                    #region
                    c.Appellation3 = famliy_dt.Rows[2]["relationship"].ToString();
                    c.Name3 = famliy_dt.Rows[2]["name"].ToString();
                    c.Age3 = famliy_dt.Rows[2]["age"].ToString();
                    c.Polityface3 = famliy_dt.Rows[2]["party"].ToString();
                    if (famliy_dt.Rows[2]["remark"].ToString().Trim() == "已故")
                        c.Unitjob3 = famliy_dt.Rows[2]["deptJob"].ToString() + "(已故)";
                    else
                        c.Unitjob3 = famliy_dt.Rows[2]["deptJob"].ToString();
                    #endregion

                    //第4条数据
                    #region
                    c.Appellation4 = famliy_dt.Rows[3]["relationship"].ToString();
                    c.Name4 = famliy_dt.Rows[3]["name"].ToString();
                    c.Age4 = famliy_dt.Rows[3]["age"].ToString();
                    c.Polityface4 = famliy_dt.Rows[3]["party"].ToString();
                    if (famliy_dt.Rows[3]["remark"].ToString().Trim() == "已故")
                        c.Unitjob4 = famliy_dt.Rows[3]["deptJob"].ToString() + "(已故)";
                    else
                        c.Unitjob4 = famliy_dt.Rows[3]["deptJob"].ToString();
                    #endregion

                    //第5条数据
                    #region
                    c.Appellation5 = famliy_dt.Rows[4]["relationship"].ToString();
                    c.Name5 = famliy_dt.Rows[4]["name"].ToString();
                    c.Age5 = famliy_dt.Rows[4]["age"].ToString();
                    c.Polityface5 = famliy_dt.Rows[4]["party"].ToString();
                    if (famliy_dt.Rows[4]["remark"].ToString().Trim() == "已故")
                        c.Unitjob5 = famliy_dt.Rows[4]["deptJob"].ToString() + "(已故)";
                    else
                        c.Unitjob5 = famliy_dt.Rows[4]["deptJob"].ToString();
                    #endregion

                }
                #endregion

                //家庭和社会关系是4条
                #region
                else if (famliy_dt.Rows.Count == 4)
                {
                    //第1条数据
                    #region
                    c.Appellation1 = famliy_dt.Rows[0]["relationship"].ToString();
                    c.Name1 = famliy_dt.Rows[0]["name"].ToString();
                    c.Age1 = famliy_dt.Rows[0]["age"].ToString();
                    c.Polityface1 = famliy_dt.Rows[0]["party"].ToString();
                    if (famliy_dt.Rows[0]["remark"].ToString().Trim() == "已故")
                        c.Unitjob1 = famliy_dt.Rows[0]["deptJob"].ToString() + "(已故)";
                    else
                        c.Unitjob1 = famliy_dt.Rows[0]["deptJob"].ToString();
                    #endregion

                    //第2条数据
                    #region
                    c.Appellation2 = famliy_dt.Rows[1]["relationship"].ToString();
                    c.Name2 = famliy_dt.Rows[1]["name"].ToString();
                    c.Age2 = famliy_dt.Rows[1]["age"].ToString();
                    c.Polityface2 = famliy_dt.Rows[1]["party"].ToString();
                    if (famliy_dt.Rows[1]["remark"].ToString().Trim() == "已故")
                        c.Unitjob2 = famliy_dt.Rows[1]["deptJob"].ToString() + "(已故)";
                    else
                        c.Unitjob2 = famliy_dt.Rows[1]["deptJob"].ToString();
                    #endregion

                    //第3条数据
                    #region
                    c.Appellation3 = famliy_dt.Rows[2]["relationship"].ToString();
                    c.Name3 = famliy_dt.Rows[2]["name"].ToString();
                    c.Age3 = famliy_dt.Rows[2]["age"].ToString();
                    c.Polityface3 = famliy_dt.Rows[2]["party"].ToString();
                    if (famliy_dt.Rows[2]["remark"].ToString().Trim() == "已故")
                        c.Unitjob3 = famliy_dt.Rows[2]["deptJob"].ToString() + "(已故)";
                    else
                        c.Unitjob3 = famliy_dt.Rows[2]["deptJob"].ToString();
                    #endregion

                    //第4条数据
                    #region
                    c.Appellation4 = famliy_dt.Rows[3]["relationship"].ToString();
                    c.Name4 = famliy_dt.Rows[3]["name"].ToString();
                    c.Age4 = famliy_dt.Rows[3]["age"].ToString();
                    c.Polityface4 = famliy_dt.Rows[3]["party"].ToString();
                    if (famliy_dt.Rows[3]["remark"].ToString().Trim() == "已故")
                        c.Unitjob4 = famliy_dt.Rows[3]["deptJob"].ToString() + "(已故)";
                    else
                        c.Unitjob4 = famliy_dt.Rows[3]["deptJob"].ToString();
                    #endregion

                }
                #endregion

                //家庭和社会关系是3条
                #region
                else if (famliy_dt.Rows.Count == 3)
                {
                    //第1条数据
                    #region
                    c.Appellation1 = famliy_dt.Rows[0]["relationship"].ToString();
                    c.Name1 = famliy_dt.Rows[0]["name"].ToString();
                    c.Age1 = famliy_dt.Rows[0]["age"].ToString();
                    c.Polityface1 = famliy_dt.Rows[0]["party"].ToString();
                    if (famliy_dt.Rows[0]["remark"].ToString().Trim() == "已故")
                        c.Unitjob1 = famliy_dt.Rows[0]["deptJob"].ToString() + "(已故)";
                    else
                        c.Unitjob1 = famliy_dt.Rows[0]["deptJob"].ToString();
                    #endregion

                    //第2条数据
                    #region
                    c.Appellation2 = famliy_dt.Rows[1]["relationship"].ToString();
                    c.Name2 = famliy_dt.Rows[1]["name"].ToString();
                    c.Age2 = famliy_dt.Rows[1]["age"].ToString();
                    c.Polityface2 = famliy_dt.Rows[1]["party"].ToString();
                    if (famliy_dt.Rows[1]["remark"].ToString().Trim() == "已故")
                        c.Unitjob2 = famliy_dt.Rows[1]["deptJob"].ToString() + "(已故)";
                    else
                        c.Unitjob2 = famliy_dt.Rows[1]["deptJob"].ToString();
                    #endregion

                    //第3条数据
                    #region
                    c.Appellation3 = famliy_dt.Rows[2]["relationship"].ToString();
                    c.Name3 = famliy_dt.Rows[2]["name"].ToString();
                    c.Age3 = famliy_dt.Rows[2]["age"].ToString();
                    c.Polityface3 = famliy_dt.Rows[2]["party"].ToString();
                    if (famliy_dt.Rows[2]["remark"].ToString().Trim() == "已故")
                        c.Unitjob3 = famliy_dt.Rows[2]["deptJob"].ToString() + "(已故)";
                    else
                        c.Unitjob3 = famliy_dt.Rows[2]["deptJob"].ToString();
                    #endregion

                }
                #endregion

                //家庭和社会关系是2条
                #region
                else if (famliy_dt.Rows.Count == 2)
                {
                    //第1条数据
                    #region
                    c.Appellation1 = famliy_dt.Rows[0]["relationship"].ToString();
                    c.Name1 = famliy_dt.Rows[0]["name"].ToString();
                    c.Age1 = famliy_dt.Rows[0]["age"].ToString();
                    c.Polityface1 = famliy_dt.Rows[0]["party"].ToString();
                    if (famliy_dt.Rows[0]["remark"].ToString().Trim() == "已故")
                        c.Unitjob1 = famliy_dt.Rows[0]["deptJob"].ToString() + "(已故)";
                    else
                        c.Unitjob1 = famliy_dt.Rows[0]["deptJob"].ToString();
                    #endregion

                    //第2条数据
                    #region
                    c.Appellation2 = famliy_dt.Rows[1]["relationship"].ToString();
                    c.Name2 = famliy_dt.Rows[1]["name"].ToString();
                    c.Age2 = famliy_dt.Rows[1]["age"].ToString();
                    c.Polityface2 = famliy_dt.Rows[1]["party"].ToString();
                    if (famliy_dt.Rows[1]["remark"].ToString().Trim() == "已故")
                        c.Unitjob2 = famliy_dt.Rows[1]["deptJob"].ToString() + "(已故)";
                    else
                        c.Unitjob2 = famliy_dt.Rows[1]["deptJob"].ToString();
                    #endregion

                }
                #endregion

                //家庭和社会关系是1条
                #region
                else if (famliy_dt.Rows.Count == 1)
                {
                    //第1条数据
                    #region
                    c.Appellation1 = famliy_dt.Rows[0]["relationship"].ToString();
                    c.Name1 = famliy_dt.Rows[0]["name"].ToString();
                    c.Age1 = famliy_dt.Rows[0]["age"].ToString();
                    c.Polityface1 = famliy_dt.Rows[0]["party"].ToString();
                    if (famliy_dt.Rows[0]["remark"].ToString().Trim() == "已故")
                        c.Unitjob1 = famliy_dt.Rows[0]["deptJob"].ToString() + "(已故)";
                    else
                        c.Unitjob1 = famliy_dt.Rows[0]["deptJob"].ToString();
                    #endregion

                }
                #endregion

                #endregion

                HBMainMessageList.Add(c);
            }

            this.RV_HBMainMessage.LocalReport.EnableExternalImages = true;
            this.BS_HBMainMessage.DataSource = HBMainMessageList;
            this.RV_HBMainMessage.RefreshReport();
        }

        /// <summary>
        /// 后备干部简历
        /// </summary>
        /// <param name="cid">后备干部id号</param>
        /// <returns></returns>
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
                    if (fc >= 21 && (int)fc % 21 == 0)
                    {
                        resume += "\n\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t";
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
                    rewardsAndpunishment = rewardsAndpunishment + "\t\t" + datatable.Rows[i]["time"].ToString() + "，" + datatable.Rows[i]["department"].ToString() + "(" + datatable.Rows[i]["grade"].ToString() + datatable.Rows[i]["name"].ToString() + ")";
                }

                //如果第i条是惩罚
                if (datatable.Rows[i]["class"].ToString().Equals("惩"))
                {
                    rewardsAndpunishment = rewardsAndpunishment + "\t\t" + datatable.Rows[i]["time"].ToString() + "，" + datatable.Rows[i]["department"].ToString() + "(" + datatable.Rows[i]["grade"].ToString() + datatable.Rows[i]["name"].ToString() + ")";
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
        /// <param name="cid"></param>
        /// <returns></returns>
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
                yearcheck = "\t\t" + year1.ToString() + "年、" + year2.ToString() + "年年度考核均为" + yearcheck1 + "，" + year3.ToString() + "年年度考核为" + yearcheck3 + "。";
            }
            //1、3年一样，1、3年与2年不一样
            else if ((yearcheck1.Equals(yearcheck3)) && (!yearcheck1.Equals(yearcheck2)))
            {
                yearcheck = "\t\t" + year1.ToString() + "年、" + year3.ToString() + "年年度考核均为" + yearcheck1 + "，" + year2.ToString() + "年年度考核为" + yearcheck2 + "。";
            }
            //2、3年一样，2、3年与1年不一样
            else if ((yearcheck2.Equals(yearcheck3)) && (!yearcheck2.Equals(yearcheck1)))
            {
                yearcheck = "\t\t" + year1.ToString() + "年年度考核为" + yearcheck1 + "，" + year2.ToString() + "年、" + year3.ToString() + "年年度考核均为" + yearcheck2 + "。";
            }
            //3年都一样
            else if ((yearcheck1.Equals(yearcheck2)) && (yearcheck1.Equals(yearcheck3)))
            {
                yearcheck = "\t\t" + year1.ToString() + "年、" + year2.ToString() + "年、" + year3.ToString() + "年年度考核均为" + yearcheck1 + "。";
            }
            return yearcheck;
        }

        /// <summary>
        /// 将****年**月改为****.**
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        private string todate(string date)
        {
            date = date.Replace("年", ".");
            date = date.Replace("月", "");
            return date;
        }
    }
}