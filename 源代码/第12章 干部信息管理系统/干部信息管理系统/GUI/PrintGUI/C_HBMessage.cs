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
    /// 后备干部的信息采集表
    /// </summary>
    public partial class C_HBMessage : UserControl
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
        /// //单位编号
        /// </summary>
        private string uid;
        public string Uid
        {
            get { return uid; }
            set { uid = value; }
        }
        /// <summary>
        /// 后备干部的信息采集表构造函数
        /// </summary>
        public C_HBMessage()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 加载事件的处理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void C_HBMessage_Load(object sender, EventArgs e)
        {
            this.RV_HBMessage.SetDisplayMode(DisplayMode.PrintLayout);
                        
            List<Class_HBMessage> HBMessageList = new List<Class_HBMessage>();

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
            #endregion            
           
            string sql = "select CID,name,department,position from TB_CommonInfo order by rank,joinTeam desc";
            string cond = "cid in (" + selectid + ")";
            DataOperation dataOp = new DataOperation();
            DataTable datatable1 = dataOp.GetOneDataTable_sql(sql);
            DataTable datatable = new DataTable();
            datatable = datatable1.Clone();
            DataView dv = datatable1.AsDataView();
            dv.RowFilter = cond;
            datatable = dv.ToTable();
            DataTable datatable2 = dataOp.GetOneDataTable_sql("");

            for (i = 0; i < datatable.Rows.Count; i++)
            {
                Class_HBMessage c = new Class_HBMessage();

                #region

                #region
                c.Cid = datatable.Rows[i]["CID"].ToString();
                c.Unit = datatable.Rows[i]["department"].ToString();
                c.Hbname = datatable.Rows[i]["name"].ToString();
                c.Post = datatable.Rows[i]["position"].ToString();
                #endregion
                
                //家庭及社会关系信息
                #region
                datatable2 = dataOp.GetOneDataTable_sql("select * from TB_Family where cid ='" + c.Cid + "'");

                //家庭及社会关系信息7条
                #region
                if (datatable2.Rows.Count == 7)
                {
                    //第1条家庭信息
                    #region
                    c.Appellation1 = datatable2.Rows[0]["relationship"].ToString();
                    c.Name1 = datatable2.Rows[0]["name"].ToString();
                    c.Birth1 = todate(datatable2.Rows[0]["birthday"].ToString());
                    c.Nationality1 = datatable2.Rows[0]["country"].ToString();
                    c.Polities1 = datatable2.Rows[0]["party"].ToString();
                    c.Nayional1 = datatable2.Rows[0]["nation"].ToString();
                    c.Unitjob1 = datatable2.Rows[0]["deptJob"].ToString();
                    c.Notes1 = datatable2.Rows[0]["remark"].ToString();
                    #endregion

                    //第2条家庭信息
                    #region
                    c.Appellation2 = datatable2.Rows[1]["relationship"].ToString();
                    c.Name2 = datatable2.Rows[1]["name"].ToString();
                    c.Birth2 = todate(datatable2.Rows[1]["birthday"].ToString());
                    c.Nationality2 = datatable2.Rows[1]["country"].ToString();
                    c.Polities2 = datatable2.Rows[1]["party"].ToString();
                    c.Nayional2 = datatable2.Rows[1]["nation"].ToString();
                    c.Unitjob2 = datatable2.Rows[1]["deptJob"].ToString();
                    c.Notes2 = datatable2.Rows[1]["remark"].ToString();
                    #endregion

                    //第3条家庭信息
                    #region
                    c.Appellation3 = datatable2.Rows[2]["relationship"].ToString();
                    c.Name3 = datatable2.Rows[2]["name"].ToString();
                    c.Birth3 = todate(datatable2.Rows[2]["birthday"].ToString());
                    c.Nationality3 = datatable2.Rows[2]["country"].ToString();
                    c.Polities3 = datatable2.Rows[2]["party"].ToString();
                    c.Nayional3 = datatable2.Rows[2]["nation"].ToString();
                    c.Unitjob3 = datatable2.Rows[2]["deptJob"].ToString();
                    c.Notes3 = datatable2.Rows[2]["remark"].ToString();
                    #endregion

                    //第4条家庭信息
                    #region
                    c.Appellation4 = datatable2.Rows[3]["relationship"].ToString();
                    c.Name4 = datatable2.Rows[3]["name"].ToString();
                    c.Birth4 = todate(datatable2.Rows[3]["birthday"].ToString());
                    c.Nationality4 = datatable2.Rows[3]["country"].ToString();
                    c.Polities4 = datatable2.Rows[3]["party"].ToString();
                    c.Nayional4 = datatable2.Rows[3]["nation"].ToString();
                    c.Unitjob4 = datatable2.Rows[3]["deptJob"].ToString();
                    c.Notes4 = datatable2.Rows[3]["remark"].ToString();
                    #endregion

                    //第5条家庭信息
                    #region
                    c.Appellation5 = datatable2.Rows[4]["relationship"].ToString();
                    c.Name5 = datatable2.Rows[4]["name"].ToString();
                    c.Birth5 = todate(datatable2.Rows[4]["birthday"].ToString());
                    c.Nationality5 = datatable2.Rows[4]["country"].ToString();
                    c.Polities5 = datatable2.Rows[4]["party"].ToString();
                    c.Nayional5 = datatable2.Rows[4]["nation"].ToString();
                    c.Unitjob5 = datatable2.Rows[4]["deptJob"].ToString();
                    c.Notes5 = datatable2.Rows[4]["remark"].ToString();
                    #endregion

                    //第6条家庭信息
                    #region
                    c.Appellation6 = datatable2.Rows[5]["relationship"].ToString();
                    c.Name6 = datatable2.Rows[5]["name"].ToString();
                    c.Birth6 = todate(datatable2.Rows[5]["birthday"].ToString());
                    c.Nationality6 = datatable2.Rows[5]["country"].ToString();
                    c.Polities6 = datatable2.Rows[5]["party"].ToString();
                    c.Nayional6 = datatable2.Rows[5]["nation"].ToString();
                    c.Unitjob6 = datatable2.Rows[5]["deptJob"].ToString();
                    c.Notes6 = datatable2.Rows[5]["remark"].ToString();
                    #endregion

                    //第7条家庭信息
                    #region
                    c.Appellation7 = datatable2.Rows[6]["relationship"].ToString();
                    c.Name7 = datatable2.Rows[6]["name"].ToString();
                    c.Birth7 = todate(datatable2.Rows[6]["birthday"].ToString());
                    c.Nationality7 = datatable2.Rows[6]["country"].ToString();
                    c.Polities7 = datatable2.Rows[6]["party"].ToString();
                    c.Nayional7 = datatable2.Rows[6]["nation"].ToString();
                    c.Unitjob7 = datatable2.Rows[6]["deptJob"].ToString();
                    c.Notes7 = datatable2.Rows[6]["remark"].ToString();
                    #endregion
                }
                #endregion

                //家庭及社会关系信息6条
                #region
                else if (datatable2.Rows.Count == 6)
                {
                    //第1条家庭信息
                    #region
                    c.Appellation1 = datatable2.Rows[0]["relationship"].ToString();
                    c.Name1 = datatable2.Rows[0]["name"].ToString();
                    c.Birth1 = todate(datatable2.Rows[0]["birthday"].ToString());
                    c.Nationality1 = datatable2.Rows[0]["country"].ToString();
                    c.Polities1 = datatable2.Rows[0]["party"].ToString();
                    c.Nayional1 = datatable2.Rows[0]["nation"].ToString();
                    c.Unitjob1 = datatable2.Rows[0]["deptJob"].ToString();
                    c.Notes1 = datatable2.Rows[0]["remark"].ToString();
                    #endregion

                    //第2条家庭信息
                    #region
                    c.Appellation2 = datatable2.Rows[1]["relationship"].ToString();
                    c.Name2 = datatable2.Rows[1]["name"].ToString();
                    c.Birth2 = todate(datatable2.Rows[1]["birthday"].ToString());
                    c.Nationality2 = datatable2.Rows[1]["country"].ToString();
                    c.Polities2 = datatable2.Rows[1]["party"].ToString();
                    c.Nayional2 = datatable2.Rows[1]["nation"].ToString();
                    c.Unitjob2 = datatable2.Rows[1]["deptJob"].ToString();
                    c.Notes2 = datatable2.Rows[1]["remark"].ToString();
                    #endregion

                    //第3条家庭信息
                    #region
                    c.Appellation3 = datatable2.Rows[2]["relationship"].ToString();
                    c.Name3 = datatable2.Rows[2]["name"].ToString();
                    c.Birth3 = todate(datatable2.Rows[2]["birthday"].ToString());
                    c.Nationality3 = datatable2.Rows[2]["country"].ToString();
                    c.Polities3 = datatable2.Rows[2]["party"].ToString();
                    c.Nayional3 = datatable2.Rows[2]["nation"].ToString();
                    c.Unitjob3 = datatable2.Rows[2]["deptJob"].ToString();
                    c.Notes3 = datatable2.Rows[2]["remark"].ToString();
                    #endregion

                    //第4条家庭信息
                    #region
                    c.Appellation4 = datatable2.Rows[3]["relationship"].ToString();
                    c.Name4 = datatable2.Rows[3]["name"].ToString();
                    c.Birth4 = todate(datatable2.Rows[3]["birthday"].ToString());
                    c.Nationality4 = datatable2.Rows[3]["country"].ToString();
                    c.Polities4 = datatable2.Rows[3]["party"].ToString();
                    c.Nayional4 = datatable2.Rows[3]["nation"].ToString();
                    c.Unitjob4 = datatable2.Rows[3]["deptJob"].ToString();
                    c.Notes4 = datatable2.Rows[3]["remark"].ToString();
                    #endregion

                    //第5条家庭信息
                    #region
                    c.Appellation5 = datatable2.Rows[4]["relationship"].ToString();
                    c.Name5 = datatable2.Rows[4]["name"].ToString();
                    c.Birth5 = todate(datatable2.Rows[4]["birthday"].ToString());
                    c.Nationality5 = datatable2.Rows[4]["country"].ToString();
                    c.Polities5 = datatable2.Rows[4]["party"].ToString();
                    c.Nayional5 = datatable2.Rows[4]["nation"].ToString();
                    c.Unitjob5 = datatable2.Rows[4]["deptJob"].ToString();
                    c.Notes5 = datatable2.Rows[4]["remark"].ToString();
                    #endregion

                    //第6条家庭信息
                    #region
                    c.Appellation6 = datatable2.Rows[5]["relationship"].ToString();
                    c.Name6 = datatable2.Rows[5]["name"].ToString();
                    c.Birth6 = todate(datatable2.Rows[5]["birthday"].ToString());
                    c.Nationality6 = datatable2.Rows[5]["country"].ToString();
                    c.Polities6 = datatable2.Rows[5]["party"].ToString();
                    c.Nayional6 = datatable2.Rows[5]["nation"].ToString();
                    c.Unitjob6 = datatable2.Rows[5]["deptJob"].ToString();
                    c.Notes6 = datatable2.Rows[5]["remark"].ToString();
                    #endregion

                }
                #endregion

                //家庭及社会关系信息5条
                #region
                else if (datatable2.Rows.Count == 5)
                {
                    //第1条家庭信息
                    #region
                    c.Appellation1 = datatable2.Rows[0]["relationship"].ToString();
                    c.Name1 = datatable2.Rows[0]["name"].ToString();
                    c.Birth1 = todate(datatable2.Rows[0]["birthday"].ToString());
                    c.Nationality1 = datatable2.Rows[0]["country"].ToString();
                    c.Polities1 = datatable2.Rows[0]["party"].ToString();
                    c.Nayional1 = datatable2.Rows[0]["nation"].ToString();
                    c.Unitjob1 = datatable2.Rows[0]["deptJob"].ToString();
                    c.Notes1 = datatable2.Rows[0]["remark"].ToString();
                    #endregion

                    //第2条家庭信息
                    #region
                    c.Appellation2 = datatable2.Rows[1]["relationship"].ToString();
                    c.Name2 = datatable2.Rows[1]["name"].ToString();
                    c.Birth2 = todate(datatable2.Rows[1]["birthday"].ToString());
                    c.Nationality2 = datatable2.Rows[1]["country"].ToString();
                    c.Polities2 = datatable2.Rows[1]["party"].ToString();
                    c.Nayional2 = datatable2.Rows[1]["nation"].ToString();
                    c.Unitjob2 = datatable2.Rows[1]["deptJob"].ToString();
                    c.Notes2 = datatable2.Rows[1]["remark"].ToString();
                    #endregion

                    //第3条家庭信息
                    #region
                    c.Appellation3 = datatable2.Rows[2]["relationship"].ToString();
                    c.Name3 = datatable2.Rows[2]["name"].ToString();
                    c.Birth3 = todate(datatable2.Rows[2]["birthday"].ToString());
                    c.Nationality3 = datatable2.Rows[2]["country"].ToString();
                    c.Polities3 = datatable2.Rows[2]["party"].ToString();
                    c.Nayional3 = datatable2.Rows[2]["nation"].ToString();
                    c.Unitjob3 = datatable2.Rows[2]["deptJob"].ToString();
                    c.Notes3 = datatable2.Rows[2]["remark"].ToString();
                    #endregion

                    //第4条家庭信息
                    #region
                    c.Appellation4 = datatable2.Rows[3]["relationship"].ToString();
                    c.Name4 = datatable2.Rows[3]["name"].ToString();
                    c.Birth4 = todate(datatable2.Rows[3]["birthday"].ToString());
                    c.Nationality4 = datatable2.Rows[3]["country"].ToString();
                    c.Polities4 = datatable2.Rows[3]["party"].ToString();
                    c.Nayional4 = datatable2.Rows[3]["nation"].ToString();
                    c.Unitjob4 = datatable2.Rows[3]["deptJob"].ToString();
                    c.Notes4 = datatable2.Rows[3]["remark"].ToString();
                    #endregion

                    //第5条家庭信息
                    #region
                    c.Appellation5 = datatable2.Rows[4]["relationship"].ToString();
                    c.Name5 = datatable2.Rows[4]["name"].ToString();
                    c.Birth5 = todate(datatable2.Rows[4]["birthday"].ToString());
                    c.Nationality5 = datatable2.Rows[4]["country"].ToString();
                    c.Polities5 = datatable2.Rows[4]["party"].ToString();
                    c.Nayional5 = datatable2.Rows[4]["nation"].ToString();
                    c.Unitjob5 = datatable2.Rows[4]["deptJob"].ToString();
                    c.Notes5 = datatable2.Rows[4]["remark"].ToString();
                    #endregion                 

                }
                #endregion

                //家庭及社会关系信息4条
                #region
                else if (datatable2.Rows.Count == 4)
                {
                    //第1条家庭信息
                    #region
                    c.Appellation1 = datatable2.Rows[0]["relationship"].ToString();
                    c.Name1 = datatable2.Rows[0]["name"].ToString();
                    c.Birth1 = todate(datatable2.Rows[0]["birthday"].ToString());
                    c.Nationality1 = datatable2.Rows[0]["country"].ToString();
                    c.Polities1 = datatable2.Rows[0]["party"].ToString();
                    c.Nayional1 = datatable2.Rows[0]["nation"].ToString();
                    c.Unitjob1 = datatable2.Rows[0]["deptJob"].ToString();
                    c.Notes1 = datatable2.Rows[0]["remark"].ToString();
                    #endregion

                    //第2条家庭信息
                    #region
                    c.Appellation2 = datatable2.Rows[1]["relationship"].ToString();
                    c.Name2 = datatable2.Rows[1]["name"].ToString();
                    c.Birth2 = todate(datatable2.Rows[1]["birthday"].ToString());
                    c.Nationality2 = datatable2.Rows[1]["country"].ToString();
                    c.Polities2 = datatable2.Rows[1]["party"].ToString();
                    c.Nayional2 = datatable2.Rows[1]["nation"].ToString();
                    c.Unitjob2 = datatable2.Rows[1]["deptJob"].ToString();
                    c.Notes2 = datatable2.Rows[1]["remark"].ToString();
                    #endregion

                    //第3条家庭信息
                    #region
                    c.Appellation3 = datatable2.Rows[2]["relationship"].ToString();
                    c.Name3 = datatable2.Rows[2]["name"].ToString();
                    c.Birth3 = todate(datatable2.Rows[2]["birthday"].ToString());
                    c.Nationality3 = datatable2.Rows[2]["country"].ToString();
                    c.Polities3 = datatable2.Rows[2]["party"].ToString();
                    c.Nayional3 = datatable2.Rows[2]["nation"].ToString();
                    c.Unitjob3 = datatable2.Rows[2]["deptJob"].ToString();
                    c.Notes3 = datatable2.Rows[2]["remark"].ToString();
                    #endregion

                    //第4条家庭信息
                    #region
                    c.Appellation4 = datatable2.Rows[3]["relationship"].ToString();
                    c.Name4 = datatable2.Rows[3]["name"].ToString();
                    c.Birth4 = todate(datatable2.Rows[3]["birthday"].ToString());
                    c.Nationality4 = datatable2.Rows[3]["country"].ToString();
                    c.Polities4 = datatable2.Rows[3]["party"].ToString();
                    c.Nayional4 = datatable2.Rows[3]["nation"].ToString();
                    c.Unitjob4 = datatable2.Rows[3]["deptJob"].ToString();
                    c.Notes4 = datatable2.Rows[3]["remark"].ToString();
                    #endregion                   
                }
                #endregion

                //家庭及社会关系信息3条
                #region
                else if (datatable2.Rows.Count == 3)
                {
                    //第1条家庭信息
                    #region
                    c.Appellation1 = datatable2.Rows[0]["relationship"].ToString();
                    c.Name1 = datatable2.Rows[0]["name"].ToString();
                    c.Birth1 = todate(datatable2.Rows[0]["birthday"].ToString());
                    c.Nationality1 = datatable2.Rows[0]["country"].ToString();
                    c.Polities1 = datatable2.Rows[0]["party"].ToString();
                    c.Nayional1 = datatable2.Rows[0]["nation"].ToString();
                    c.Unitjob1 = datatable2.Rows[0]["deptJob"].ToString();
                    c.Notes1 = datatable2.Rows[0]["remark"].ToString();
                    #endregion

                    //第2条家庭信息
                    #region
                    c.Appellation2 = datatable2.Rows[1]["relationship"].ToString();
                    c.Name2 = datatable2.Rows[1]["name"].ToString();
                    c.Birth2 = todate(datatable2.Rows[1]["birthday"].ToString());
                    c.Nationality2 = datatable2.Rows[1]["country"].ToString();
                    c.Polities2 = datatable2.Rows[1]["party"].ToString();
                    c.Nayional2 = datatable2.Rows[1]["nation"].ToString();
                    c.Unitjob2 = datatable2.Rows[1]["deptJob"].ToString();
                    c.Notes2 = datatable2.Rows[1]["remark"].ToString();
                    #endregion

                    //第3条家庭信息
                    #region
                    c.Appellation3 = datatable2.Rows[2]["relationship"].ToString();
                    c.Name3 = datatable2.Rows[2]["name"].ToString();
                    c.Birth3 = todate(datatable2.Rows[2]["birthday"].ToString());
                    c.Nationality3 = datatable2.Rows[2]["country"].ToString();
                    c.Polities3 = datatable2.Rows[2]["party"].ToString();
                    c.Nayional3 = datatable2.Rows[2]["nation"].ToString();
                    c.Unitjob3 = datatable2.Rows[2]["deptJob"].ToString();
                    c.Notes3 = datatable2.Rows[2]["remark"].ToString();
                    #endregion

                }
                #endregion

                //家庭及社会关系信息2条
                #region
                else if (datatable2.Rows.Count == 2)
                {
                    //第1条家庭信息
                    #region
                    c.Appellation1 = datatable2.Rows[0]["relationship"].ToString();
                    c.Name1 = datatable2.Rows[0]["name"].ToString();
                    c.Birth1 = todate(datatable2.Rows[0]["birthday"].ToString());
                    c.Nationality1 = datatable2.Rows[0]["country"].ToString();
                    c.Polities1 = datatable2.Rows[0]["party"].ToString();
                    c.Nayional1 = datatable2.Rows[0]["nation"].ToString();
                    c.Unitjob1 = datatable2.Rows[0]["deptJob"].ToString();
                    c.Notes1 = datatable2.Rows[0]["remark"].ToString();
                    #endregion

                    //第2条家庭信息
                    #region
                    c.Appellation2 = datatable2.Rows[1]["relationship"].ToString();
                    c.Name2 = datatable2.Rows[1]["name"].ToString();
                    c.Birth2 = todate(datatable2.Rows[1]["birthday"].ToString());
                    c.Nationality2 = datatable2.Rows[1]["country"].ToString();
                    c.Polities2 = datatable2.Rows[1]["party"].ToString();
                    c.Nayional2 = datatable2.Rows[1]["nation"].ToString();
                    c.Unitjob2 = datatable2.Rows[1]["deptJob"].ToString();
                    c.Notes2 = datatable2.Rows[1]["remark"].ToString();
                    #endregion

                }
                #endregion

                //家庭及社会关系信息1条
                #region
                else if (datatable2.Rows.Count == 1)
                {
                    //第1条家庭信息
                    #region
                    c.Appellation1 = datatable2.Rows[0]["relationship"].ToString();
                    c.Name1 = datatable2.Rows[0]["name"].ToString();
                    c.Birth1 = todate(datatable2.Rows[0]["birthday"].ToString());
                    c.Nationality1 = datatable2.Rows[0]["country"].ToString();
                    c.Polities1 = datatable2.Rows[0]["party"].ToString();
                    c.Nayional1 = datatable2.Rows[0]["nation"].ToString();
                    c.Unitjob1 = datatable2.Rows[0]["deptJob"].ToString();
                    c.Notes1 = datatable2.Rows[0]["remark"].ToString();
                    #endregion                   
                }
                #endregion

                //清除datatable2的数据
                datatable2.Clear();                
                //家庭及社会关系到此结束
                #endregion

                //海外学习
                #region
                datatable2 = dataOp.GetOneDataTable_sql("select * from TB_SAbroad where cid ='" + c.Cid + "'");

                //海外学习0条
                #region
                if (datatable2.Rows.Count == 0)
                {
                    c.Studystarttime1 = "无";
                    c.Studystarttime2 = "无";
                    c.Studystarttime3 = "无";
                }
                #endregion

                //海外学习1条
                #region
                else if (datatable2.Rows.Count == 1)
                {
                    //第一条海外学习信息
                    c.Studystarttime1 = todate2(datatable2.Rows[0]["startTime"].ToString());
                    c.Studyendtime1 = todate2(datatable2.Rows[0]["endTime"].ToString());
                    c.Studycountry1 = datatable2.Rows[0]["country"].ToString();
                    c.Studyacademy1 = datatable2.Rows[0]["academy"].ToString();
                    c.Studydegree1 = datatable2.Rows[0]["degree"].ToString();

                    c.Studystarttime2 = "无";
                    c.Studystarttime3 = "无";
                }
                #endregion

                //海外学习2条
                #region
                else if (datatable2.Rows.Count == 2)
                {
                    //第一条海外学习信息
                    c.Studystarttime1 = todate2(datatable2.Rows[0]["startTime"].ToString());
                    c.Studyendtime1 = todate2(datatable2.Rows[0]["endTime"].ToString());
                    c.Studycountry1 = datatable2.Rows[0]["country"].ToString();
                    c.Studyacademy1 = datatable2.Rows[0]["academy"].ToString();
                    c.Studydegree1 = datatable2.Rows[0]["degree"].ToString();

                    //第2条海外学习信息
                    c.Studystarttime2 = todate2(datatable2.Rows[1]["startTime"].ToString());
                    c.Studyendtime2 = todate2(datatable2.Rows[1]["endTime"].ToString());
                    c.Studycountry2 = datatable2.Rows[1]["country"].ToString();
                    c.Studyacademy2 = datatable2.Rows[1]["academy"].ToString();
                    c.Studydegree2= datatable2.Rows[1]["degree"].ToString();

                    c.Studystarttime3 = "无";
                }
                #endregion

                //海外学习3条
                #region
                else if (datatable2.Rows.Count == 3)
                {
                    //第一条海外学习信息
                    c.Studystarttime1 = todate2(datatable2.Rows[0]["startTime"].ToString());
                    c.Studyendtime1 = todate2(datatable2.Rows[0]["endTime"].ToString());
                    c.Studycountry1 = datatable2.Rows[0]["country"].ToString();
                    c.Studyacademy1 = datatable2.Rows[0]["academy"].ToString();
                    c.Studydegree1 = datatable2.Rows[0]["degree"].ToString();

                    //第2条海外学习信息
                    c.Studystarttime2 = todate2(datatable2.Rows[1]["startTime"].ToString());
                    c.Studyendtime2 = todate2(datatable2.Rows[1]["endTime"].ToString());
                    c.Studycountry2 = datatable2.Rows[1]["country"].ToString();
                    c.Studyacademy2 = datatable2.Rows[1]["academy"].ToString();
                    c.Studydegree2 = datatable2.Rows[1]["degree"].ToString();

                    //第3条海外学习信息
                    c.Studystarttime3 = todate2(datatable2.Rows[2]["startTime"].ToString());
                    c.Studyendtime3 = todate2(datatable2.Rows[2]["endTime"].ToString());
                    c.Studycountry3 = datatable2.Rows[2]["country"].ToString();
                    c.Studyacademy3 = datatable2.Rows[2]["academy"].ToString();
                    c.Studydegree3 = datatable2.Rows[2]["degree"].ToString();
                }
                #endregion
                //清除datatable2的数据
                datatable2.Clear();
                //海外学习数据到此结束
                #endregion

                //海外工作
                #region
                datatable2 = dataOp.GetOneDataTable_sql("select * from TB_WAbroad where cid ='" + c.Cid + "'");

                //海外工作0条
                #region
                if (datatable2.Rows.Count == 0)
                {
                    c.Workstarttime1 = "无";
                    c.Workstarttime2 = "无";
                    c.Workstarttime3 = "无";
                }
                #endregion

                //海外工作1条
                #region
                else if (datatable2.Rows.Count == 1)
                {
                    //第一条海外工作信息
                    c.Workstarttime1 = todate2(datatable2.Rows[0]["startTime"].ToString());
                    c.Workendtime1 = todate2(datatable2.Rows[0]["endTime"].ToString());
                    c.Workcountry1 = datatable2.Rows[0]["abroadCountry"].ToString();
                    c.Workunit1 = datatable2.Rows[0]["departmentPosition"].ToString();
                    c.Workarea1 = datatable2.Rows[0]["specialtyArea"].ToString();

                    c.Workstarttime2 = "无";
                    c.Workstarttime3 = "无";
                }
                #endregion

                //海外工作2条
                #region
                else if (datatable2.Rows.Count == 2)
                {
                    //第一条海外工作信息
                    c.Workstarttime1 = todate2(datatable2.Rows[0]["startTime"].ToString());
                    c.Workendtime1 = todate2(datatable2.Rows[0]["endTime"].ToString());
                    c.Workcountry1 = datatable2.Rows[0]["abroadCountry"].ToString();
                    c.Workunit1 = datatable2.Rows[0]["departmentPosition"].ToString();
                    c.Workarea1 = datatable2.Rows[0]["specialtyArea"].ToString();

                    //第2条海外工作信息
                    c.Workstarttime2 = todate2(datatable2.Rows[1]["startTime"].ToString());
                    c.Workendtime2 = todate2(datatable2.Rows[1]["endTime"].ToString());
                    c.Workcountry2 = datatable2.Rows[1]["abroadCountry"].ToString();
                    c.Workunit2 = datatable2.Rows[1]["departmentPosition"].ToString();
                    c.Workarea2 = datatable2.Rows[1]["specialtyArea"].ToString();

                    c.Workstarttime3 = "无";
                }
                #endregion

                //海外工作3条
                #region
                else if (datatable2.Rows.Count == 3)
                {
                    //第一条海外工作信息
                    c.Workstarttime1 = todate2(datatable2.Rows[0]["startTime"].ToString());
                    c.Workendtime1 = todate2(datatable2.Rows[0]["endTime"].ToString());
                    c.Workcountry1 = datatable2.Rows[0]["abroadCountry"].ToString();
                    c.Workunit1 = datatable2.Rows[0]["departmentPosition"].ToString();
                    c.Workarea1 = datatable2.Rows[0]["specialtyArea"].ToString();

                    //第2条海外工作信息
                    c.Workstarttime2 = todate2(datatable2.Rows[1]["startTime"].ToString());
                    c.Workendtime2 = todate2(datatable2.Rows[1]["endTime"].ToString());
                    c.Workcountry2 = datatable2.Rows[1]["abroadCountry"].ToString();
                    c.Workunit2 = datatable2.Rows[1]["departmentPosition"].ToString();
                    c.Workarea2 = datatable2.Rows[1]["specialtyArea"].ToString();

                    //第3条海外工作信息
                    c.Workstarttime3 = todate2(datatable2.Rows[2]["startTime"].ToString());
                    c.Workendtime3 = todate2(datatable2.Rows[2]["endTime"].ToString());
                    c.Workcountry3 = datatable2.Rows[2]["abroadCountry"].ToString();
                    c.Workunit3 = datatable2.Rows[2]["departmentPosition"].ToString();
                    c.Workarea3 = datatable2.Rows[2]["specialtyArea"].ToString();
                }
                #endregion

                //清除datatable2的数据
                datatable2.Clear();
                //海外工作信息到此读取结束
                #endregion

                //重大事项信息
                #region               
                datatable2 = dataOp.GetOneDataTable_sql("select * from TB_GreatContent where CID = '" + c.Cid + "'");                
                for (int r = 0; r < datatable2.Rows.Count; r++)
                {
                    if (datatable2.Rows[r]["matter"].ToString().Equals("1"))
                    {
                        c.Greatmessage1 = datatable2.Rows[r]["content"].ToString();
                        if (c.Greatmessage1 == null || c.Greatmessage1.Equals(""))
                            c.Greatmessage1 = "无";
                        continue;                        
                    }
                    else if (datatable2.Rows[r]["matter"].ToString().Equals("2"))
                    {
                        c.Greatmessage2 = datatable2.Rows[r]["content"].ToString();
                        if (c.Greatmessage2 == null || c.Greatmessage2.Equals(""))
                            c.Greatmessage2 = "无";
                        continue;
                    }
                    else if (datatable2.Rows[r]["matter"].ToString().Equals("3"))
                    {
                        c.Greatmessage3 = datatable2.Rows[r]["content"].ToString();
                        if (c.Greatmessage3 == null || c.Greatmessage3.Equals(""))
                            c.Greatmessage3 = "无";
                        continue;
                    }
                    else if (datatable2.Rows[r]["matter"].ToString().Equals("4"))
                    {
                        c.Greatmessage4 = datatable2.Rows[r]["content"].ToString();
                        if (c.Greatmessage4 == null || c.Greatmessage4.Equals(""))
                            c.Greatmessage4 = "无";
                        continue;
                    }
                    else if (datatable2.Rows[r]["matter"].ToString().Equals("5"))
                    {
                        c.Greatmessage5 = datatable2.Rows[r]["content"].ToString();
                        if (c.Greatmessage5 == null || c.Greatmessage5.Equals(""))
                            c.Greatmessage5 = "无";
                        continue;
                    }
                    else if (datatable2.Rows[r]["matter"].ToString().Equals("6"))
                    {
                        c.Greatmessage6 = datatable2.Rows[r]["content"].ToString();
                        if (c.Greatmessage6 == null || c.Greatmessage6.Equals(""))
                            c.Greatmessage6 = "无"; 
                        continue;
                    }
                    else if (datatable2.Rows[r]["matter"].ToString().Equals("7"))
                    {
                        c.Greatmessage7 = datatable2.Rows[r]["content"].ToString();
                        if (c.Greatmessage7 == null || c.Greatmessage7.Equals(""))
                            c.Greatmessage7 = "无"; 
                        continue;
                    }
                    else if (datatable2.Rows[r]["matter"].ToString().Equals("8"))
                    {
                        c.Greatmessage8 = datatable2.Rows[r]["content"].ToString();
                        if (c.Greatmessage8 == null || c.Greatmessage8.Equals(""))
                            c.Greatmessage8 = "无"; 
                        continue;
                    }
                    else if (datatable2.Rows[r]["matter"].ToString().Equals("9"))
                    {
                        c.Greatmessage9 = datatable2.Rows[r]["content"].ToString();
                        if (c.Greatmessage9 == null || c.Greatmessage9.Equals(""))
                            c.Greatmessage9 = "无"; 
                        continue;
                    }
                }
                    //datatable2数据清空
                    datatable2.Clear();
                //重大事项信息到此结束
                #endregion

                //熟悉外语语种信息
                #region
                datatable2 = dataOp.GetOneDataTable_sql("select * from TB_FamiliarForeign where cid ='" + c.Cid + "'");

                //熟悉外语语种是1条
                #region
                if (datatable2.Rows.Count == 1)
                {
                    string level = null;
                    //第一条外语信息
                    c.Foreignlanguage1 = datatable2.Rows[0]["foreignKind"].ToString();
                    level = datatable2.Rows[0]["level"].ToString();
                    if (level.Equals("精通"))
                        c.Master1 = "√";
                    else if (level.Equals("熟练"))
                        c.Skilled1 = "√";
                    else if (level.Equals("良好"))
                        c.Well1 = "√";
                    else if (level.Equals("一般"))
                        c.Soso1 = "√";

                }
                #endregion

                //熟悉外语语种是2条
                #region
                else if (datatable2.Rows.Count == 2)
                {
                    string level = null;
                    //第一条外语信息
                    c.Foreignlanguage1 = datatable2.Rows[0]["foreignKind"].ToString();
                    level = datatable2.Rows[0]["level"].ToString();
                    if (level.Equals("精通"))
                        c.Master1 = "√";
                    else if (level.Equals("熟练"))
                        c.Skilled1 = "√";
                    else if (level.Equals("良好"))
                        c.Well1 = "√";
                    else if (level.Equals("一般"))
                        c.Soso1 = "√";

                    //第2条外语信息
                    c.Foreignlanguage2 = datatable2.Rows[1]["foreignKind"].ToString();
                    level = datatable2.Rows[1]["level"].ToString();
                    if (level.Equals("精通"))
                        c.Master2 = "√";
                    else if (level.Equals("熟练"))
                        c.Skilled2 = "√";
                    else if (level.Equals("良好"))
                        c.Well2 = "√";
                    else if (level.Equals("一般"))
                        c.Soso2 = "√";
                }
                #endregion

                //熟悉外语语种是3条
                #region
                else if (datatable2.Rows.Count == 3)
                {
                    string level = null;
                    //第一条外语信息
                    c.Foreignlanguage1 = datatable2.Rows[0]["foreignKind"].ToString();
                    level = datatable2.Rows[0]["level"].ToString();
                    if (level.Equals("精通"))
                        c.Master1 = "√";
                    else if (level.Equals("熟练"))
                        c.Skilled1 = "√";
                    else if (level.Equals("良好"))
                        c.Well1 = "√";
                    else if (level.Equals("一般"))
                        c.Soso1 = "√";

                    //第2条外语信息
                    c.Foreignlanguage2 = datatable2.Rows[1]["foreignKind"].ToString();
                    level = datatable2.Rows[1]["level"].ToString();
                    if (level.Equals("精通"))
                        c.Master2 = "√";
                    else if (level.Equals("熟练"))
                        c.Skilled2 = "√";
                    else if (level.Equals("良好"))
                        c.Well2 = "√";
                    else if (level.Equals("一般"))
                        c.Soso2 = "√";

                    //第3条外语信息
                    c.Foreignlanguage3 = datatable2.Rows[2]["foreignKind"].ToString();
                    level = datatable2.Rows[2]["level"].ToString();
                    if (level.Equals("精通"))
                        c.Master3 = "√";
                    else if (level.Equals("熟练"))
                        c.Skilled3 = "√";
                    else if (level.Equals("良好"))
                        c.Well3 = "√";
                    else if (level.Equals("一般"))
                        c.Soso3 = "√";
                }
                #endregion

                //熟悉外语语种是4条
                #region
                else if (datatable2.Rows.Count == 4)
                {
                    string level = null;
                    //第一条外语信息
                    c.Foreignlanguage1 = datatable2.Rows[0]["foreignKind"].ToString();
                    level = datatable2.Rows[0]["level"].ToString();
                    if (level.Equals("精通"))
                        c.Master1 = "√";
                    else if (level.Equals("熟练"))
                        c.Skilled1 = "√";
                    else if (level.Equals("良好"))
                        c.Well1 = "√";
                    else if (level.Equals("一般"))
                        c.Soso1 = "√";

                    //第2条外语信息
                    c.Foreignlanguage2 = datatable2.Rows[1]["foreignKind"].ToString();
                    level = datatable2.Rows[1]["level"].ToString();
                    if (level.Equals("精通"))
                        c.Master2 = "√";
                    else if (level.Equals("熟练"))
                        c.Skilled2 = "√";
                    else if (level.Equals("良好"))
                        c.Well2 = "√";
                    else if (level.Equals("一般"))
                        c.Soso2 = "√";

                    //第3条外语信息
                    c.Foreignlanguage3 = datatable2.Rows[2]["foreignKind"].ToString();
                    level = datatable2.Rows[2]["level"].ToString();
                    if (level.Equals("精通"))
                        c.Master3 = "√";
                    else if (level.Equals("熟练"))
                        c.Skilled3 = "√";
                    else if (level.Equals("良好"))
                        c.Well3 = "√";
                    else if (level.Equals("一般"))
                        c.Soso3 = "√";

                    //第4条外语信息
                    c.Foreignlanguage4 = datatable2.Rows[3]["foreignKind"].ToString();
                    level = datatable2.Rows[3]["level"].ToString();
                    if (level.Equals("精通"))
                        c.Master4 = "√";
                    else if (level.Equals("熟练"))
                        c.Skilled4 = "√";
                    else if (level.Equals("良好"))
                        c.Well4 = "√";
                    else if (level.Equals("一般"))
                        c.Soso4 = "√";
                }
                #endregion

                //熟悉外语语种是5条
                #region
                else if (datatable2.Rows.Count == 5)
                {
                    string level = null;
                    //第一条外语信息
                    c.Foreignlanguage1 = datatable2.Rows[0]["foreignKind"].ToString();
                    level = datatable2.Rows[0]["level"].ToString();
                    if (level.Equals("精通"))
                        c.Master1 = "√";
                    else if (level.Equals("熟练"))
                        c.Skilled1 = "√";
                    else if (level.Equals("良好"))
                        c.Well1 = "√";
                    else if (level.Equals("一般"))
                        c.Soso1 = "√";

                    //第2条外语信息
                    c.Foreignlanguage2 = datatable2.Rows[1]["foreignKind"].ToString();
                    level = datatable2.Rows[1]["level"].ToString();
                    if (level.Equals("精通"))
                        c.Master2 = "√";
                    else if (level.Equals("熟练"))
                        c.Skilled2 = "√";
                    else if (level.Equals("良好"))
                        c.Well2 = "√";
                    else if (level.Equals("一般"))
                        c.Soso2 = "√";

                    //第3条外语信息
                    c.Foreignlanguage3 = datatable2.Rows[2]["foreignKind"].ToString();
                    level = datatable2.Rows[2]["level"].ToString();
                    if (level.Equals("精通"))
                        c.Master3 = "√";
                    else if (level.Equals("熟练"))
                        c.Skilled3 = "√";
                    else if (level.Equals("良好"))
                        c.Well3 = "√";
                    else if (level.Equals("一般"))
                        c.Soso3 = "√";

                    //第4条外语信息
                    c.Foreignlanguage4 = datatable2.Rows[3]["foreignKind"].ToString();
                    level = datatable2.Rows[3]["level"].ToString();
                    if (level.Equals("精通"))
                        c.Master4 = "√";
                    else if (level.Equals("熟练"))
                        c.Skilled4 = "√";
                    else if (level.Equals("良好"))
                        c.Well4 = "√";
                    else if (level.Equals("一般"))
                        c.Soso4 = "√";

                    //第5条外语信息
                    c.Foreignlanguage5 = datatable2.Rows[4]["foreignKind"].ToString();
                    level = datatable2.Rows[4]["level"].ToString();
                    if (level.Equals("精通"))
                        c.Master5 = "√";
                    else if (level.Equals("熟练"))
                        c.Skilled5 = "√";
                    else if (level.Equals("良好"))
                        c.Well5 = "√";
                    else if (level.Equals("一般"))
                        c.Soso5 = "√";
                }
                #endregion

                //清除datatable2的数据
                datatable2.Clear();
                //熟悉外语语种信息到此处理结束
                #endregion

                //参加培训和锻炼情况
                #region
                datatable2 = dataOp.GetOneDataTable_sql("select * from TB_TrainExercise where cid ='" + c.Cid + "'");
                string s1 = "";
                string s2 = "";
                for (int r = 0; r < datatable2.Rows.Count; r++) 
                {
                    //if (datatable2.Rows[r]["reportMatter"].ToString().Equals(""))
                    //     = datatable2.Rows[r]["reportContent"].ToString();
                   
                    //if (datatable2.Rows[r]["reportMatter"].ToString().Equals("2"))
                    //    datatable2.Rows[r]["reportContent"].ToString();
                    string starttime = datatable2.Rows[r]["startTime"].ToString().Replace("年", ".");
                    string endtime = datatable2.Rows[r]["endTime"].ToString().Replace("年", ".");
                    if (datatable2.Rows[r]["reportMatter"].ToString().Equals("参加培训情况"))
                        s1 = s1 + starttime.Replace("月", ".").Replace("日", "") + "到" + endtime.Replace("月", ".").Replace("日", "") + "：" + datatable2.Rows[r]["Content"].ToString() + "\n";
                    if (datatable2.Rows[r]["reportMatter"].ToString().Equals("参加实践锻炼情况"))
                        s2 = s2 + starttime.Replace("月", ".").Replace("日", "") + "到" + endtime.Replace("月", ".").Replace("日", "") + "：" + datatable2.Rows[r]["Content"].ToString() + "\n";
                }
                c.Exercise = s2;
                c.Train = s1;

                //清除datatable2的数据
                datatable2.Clear();
                //参加培训和锻炼情况到此处理结束
                #endregion

                //培养锻炼措施需求
                #region
                datatable2 = dataOp.GetOneDataTable_sql("select * from TB_TrainMethord where cid ='" + c.Cid + "'");

                for (int r = 0; r < datatable2.Rows.Count; r++)
                {
                    #region
                    string select = datatable2.Rows[r]["options"].ToString();
                    if (select.Equals("1"))
                    {
                        c.Select1 = "√"; continue;
                    }
                    else if (select.Equals("2"))
                    {
                        c.Select2 = "√"; continue;
                    }
                    else if (select.Equals("3"))
                    {
                        c.Select3 = "√"; continue;
                    }
                    else if (select.Equals("4"))
                    {
                        c.Select4 = "√"; continue;
                    }
                    else if (select.Equals("5"))
                    {
                        c.Select5 = "√"; continue;
                    }
                    else if (select.Equals("6"))
                    {
                        c.Select6 = "√"; continue;
                    }
                    else if (select.Equals("7"))
                    {
                        c.Select7 = "√"; continue;
                    }
                    else if (select.Equals("8"))
                    {
                        c.Select8 = "√"; continue;
                    }
                    else if (select.Equals("9"))
                    {
                        c.Select9 = "√"; continue;
                    }
                    else if (select.Equals("10"))
                    {
                        c.Select10 = "√"; continue;
                    }
                    else if (select.Equals("11"))
                    {
                        c.Select11 = "√"; continue;
                    }
                    else if (select.Equals("12"))
                    {
                        c.Select12 = "√"; continue;
                    }
                    else if (select.Equals("13"))
                    {
                        c.Select13 = "√"; continue;
                    }
                    else if (select.Equals("14"))
                    {
                        c.Select14 = "√";
                        c.Elsenote = datatable2.Rows[r]["note14"].ToString();
                        continue;
                    }
                    #endregion
                }

                //清除datatable2的数据
                datatable2.Clear();
                //培养锻炼措施需求到此处理结束
                #endregion

                #endregion              


                HBMessageList.Add(c);
               
            }

            //表头和说明信息
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

                else if (this.Qd.Equals("副职"))
                    title = "副校级后备干部考察对象";
            }
            else if (this.Unitclass.Equals("省管企业"))
            {
                title = "领导班子" + this.Qd + "后备干部考察对象";
            }
            else if (this.Unitclass.Equals("市直单位"))
            {
                if (this.Qd.Equals("正职"))
                    title = "正县级后备干部考察对象";

                else if (this.Qd.Equals("副职"))
                    title = "副县级后备干部考察对象";
            }
            else if (this.Unitclass.Equals("市管企业"))
            {
                title = "领导班子" + this.Qd + "后备干部考察对象";
            }
            else if (this.Unitclass.Equals("市管学校"))
            {
                if (this.Qd.Equals("正职"))
                    title = "正校级后备干部考察对象";

                else if (this.Qd.Equals("副职"))
                    title = "副校级后备干部考察对象";
            }
            else if (this.Unitclass.Equals("县(市、区)"))
            {
                title = "党政" + this.Qd + "后备干部考察对象";
            }//乡科级
            else if (this.Unitclass.Equals("县(市、区)直"))
            {
                if (this.Qd.Equals("正职"))
                    title = "正科级后备干部考察对象";

                else if (this.Qd.Equals("副职"))
                    title = "副科级后备干部考察对象";
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
            else if (this.Unitclass.Equals("乡(镇、街道)"))
            {
                title = "党政" + this.Qd + "后备干部考察对象";
            }
            #endregion

            ReportParameter[] rptParaA = new ReportParameter[] { new ReportParameter("title", title) };
            //使用名称和值实例化新的 ReportParameter。
            //在一起的报表设置一个参数，刚实例化的ReportParameter的第一个参数与关联
            RV_HBMessage.LocalReport.SetParameters(rptParaA);

            //关闭导出按钮
            //this.RV_HBMessage.ShowExportButton = false;

            this.BS_HBMessage.DataSource = HBMessageList;
            this.RV_HBMessage.RefreshReport();
        }

        /// <summary>
        /// 将****年**月改为****.**
        /// </summary>
        /// <param name="date">****年**月</param>
        /// <returns>****.**</returns>
        private string todate(string date)
        {
            date = date.Replace("年", ".");
            date = date.Replace("月", "");
            return date;
        }

        //将****年**月**日改为****.**.**
        /// <summary>
        /// 获得日期
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        private string todate2(string date)
        {            
            return date;
        }
    }
}
