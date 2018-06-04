using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading;
using System.IO;
using HBMISR.GUI.NoteGUI;
using HBMISR.Data;
using HBMISR.GUI.OtherGUI;
using System.Collections;
using HBMISR.GUI.AnalyzeGUI;
using HBMISR.Service;
using System.Security.Cryptography;
namespace HBMISR.GUI.MainGUI
{
    /// <summary>
    /// 控件类
    /// 1、构成主界面的重要组成部分
    /// </summary>
    public partial class ControlMain : UserControl
    {
        private const ulong FC_TAG = 0xFC010203040506CF;

        private const int BUFFER_SIZE = 128 * 1024;

        private const string myPassword = "C#";
        ListViewItem SelectedItem = null;

        DataTable dt;

        DataOperation da;

        public string cid = string.Empty;

        bool isSelected = false;

        string comboxtext_qd = "";

        /// <summary>
        /// 显示时间的线程
        /// </summary>
        Thread showTime;

        //做删除用
        public List<ListViewDel> list = new List<ListViewDel>();

        ReadIni readIni;

        /// <summary>
        /// 要导入的文件路径
        /// </summary>
        string inputPath = string.Empty;

        public bool isReactChange = true;

        public string delCidUse = "";

        public bool canClearListView = false;

        bool canSecondClear = true;//分别查询可进班子和非可进班子，若第一个table不为空，防止清空listView

        /// <summary>
        /// 指定进入那种连接模式
        /// </summary>
        public string panelStyle = "insert";
        Thread myThread;
        public delegate void Rank();
        /// <summary>
        /// 构造方法
        /// </summary>
        public ControlMain()
        {
            try
            {
                InitializeComponent();
                readIni = new ReadIni();
                da = new DataOperation();
            }
            catch (Exception) { }

        }

        /// <summary>
        /// 生成listview列头
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ControlMain_Load(object sender, EventArgs e)
        {
            try
            {
                ReadIni ini = new ReadIni();
                nowUnit.Text = ini.ReadString("unitName");
                //在录入界面显示单位名称
                //用一个线程显示系统时间
                showTime = new Thread(new ThreadStart(run));//创建线程

                showTime.IsBackground = true;//主线程退出后子线程也强行关闭

                showTime.Start();//启动线程

                controlSearch.controlMain = this;

                //基本信息表头的设定
                listView.View = View.Details;
                listView.LabelEdit = true;
                listView.GridLines = true;
                listView.AllowColumnReorder = true;
                listView.LabelEdit = false;
                listView.HideSelection = false;
                listView.MultiSelect = false;
                listView.Columns.Add("", 0, HorizontalAlignment.Center);
                listView.Columns.Add("序号", 50, HorizontalAlignment.Center);
                listView.Columns.Add("姓名", 80, HorizontalAlignment.Center);
                listView.Columns.Add("性别", 60, HorizontalAlignment.Center);
                listView.Columns.Add("民族", 110, HorizontalAlignment.Center);
                listView.Columns.Add("工作单位", 200, HorizontalAlignment.Center);
                listView.Columns.Add("工作部门", 200, HorizontalAlignment.Center);
                listView.Columns.Add("职务", 200, HorizontalAlignment.Left);
                listView.Columns.Add("现任职级", 80, HorizontalAlignment.Center);
                listView.Columns.Add("党派", 75, HorizontalAlignment.Center);
                listView.Columns.Add("籍贯", 140, HorizontalAlignment.Center);
                listView.Columns.Add("出生年月", 120, HorizontalAlignment.Center);
                listView.Columns.Add("入党时间", 120, HorizontalAlignment.Center);
                listView.Columns.Add("参加工作时间", 120, HorizontalAlignment.Center);
                listView.Columns.Add("全日制学历", 120, HorizontalAlignment.Center);
                listView.Columns.Add("全日制学位", 120, HorizontalAlignment.Center);
                listView.Columns.Add("全日制毕业院校", 140, HorizontalAlignment.Center);
                listView.Columns.Add("全日制专业", 140, HorizontalAlignment.Center);
                listView.Columns.Add("在职学历", 120, HorizontalAlignment.Center);
                listView.Columns.Add("在职学位", 120, HorizontalAlignment.Center);
                listView.Columns.Add("在职毕业院校", 140, HorizontalAlignment.Center);
                listView.Columns.Add("在职专业", 140, HorizontalAlignment.Center);
                listView.Columns.Add("专业技术职务", 140, HorizontalAlignment.Center);
                listView.Columns.Add("专业技术职务级别", 150, HorizontalAlignment.Center);
                listView.Columns.Add("历任主要职务", 250, HorizontalAlignment.Center);
                listView.Columns.Add("熟悉领域", 140, HorizontalAlignment.Center);
                listView.Columns.Add("培养方向", 140, HorizontalAlignment.Center);
                listView.Columns.Add("培养措施", 150, HorizontalAlignment.Center);
                listView.Columns.Add("近期可提拔使用", 130, HorizontalAlignment.Center);
                comboBox1.SelectedIndex = 1;

                //简要情况登记表表头信息的设定
                listView_Vital.View = View.Details;
                listView_Vital.LabelEdit = true;
                listView_Vital.GridLines = true;
                listView_Vital.AllowColumnReorder = true;
                listView_Vital.LabelEdit = false;
                listView_Vital.HideSelection = false;
                listView_Vital.Columns.Add("", 0, HorizontalAlignment.Center);
                listView_Vital.Columns.Add("开始时间", 90, HorizontalAlignment.Center);
                listView_Vital.Columns.Add("结束时间", 90, HorizontalAlignment.Center);
                listView_Vital.Columns.Add("内容", 756, HorizontalAlignment.Left);

                //奖惩情况表头信息的设定
                listView_Award.View = View.Details;
                listView_Award.LabelEdit = true;
                listView_Award.GridLines = true;
                listView_Award.AllowColumnReorder = true;
                listView_Award.LabelEdit = false;
                listView_Award.HideSelection = false;
                listView_Award.Columns.Add("", 0, HorizontalAlignment.Center);
                listView_Award.Columns.Add("类别", 80, HorizontalAlignment.Center);
                listView_Award.Columns.Add("级别", 100, HorizontalAlignment.Center);
                listView_Award.Columns.Add("时间", 100, HorizontalAlignment.Center);
                listView_Award.Columns.Add("内容", 656, HorizontalAlignment.Left);

                //家庭成员关系表头信息的设定
                listView_Realation.View = View.Details;
                listView_Realation.LabelEdit = true;
                listView_Realation.GridLines = true;
                listView_Realation.AllowColumnReorder = true;
                listView_Realation.LabelEdit = false;
                listView_Realation.HideSelection = false;
                listView_Realation.Columns.Add("", 0, HorizontalAlignment.Center);
                listView_Realation.Columns.Add("称谓", 60, HorizontalAlignment.Center);
                listView_Realation.Columns.Add("姓名", 100, HorizontalAlignment.Center);
                listView_Realation.Columns.Add("出生年月", 100, HorizontalAlignment.Center);
                listView_Realation.Columns.Add("年龄", 60, HorizontalAlignment.Center);
                listView_Realation.Columns.Add("国籍", 100, HorizontalAlignment.Center);
                listView_Realation.Columns.Add("党派", 100, HorizontalAlignment.Center);
                listView_Realation.Columns.Add("民族", 80, HorizontalAlignment.Center);
                listView_Realation.Columns.Add("工作单位及职务", 236, HorizontalAlignment.Center);
                listView_Realation.Columns.Add("备注", 100, HorizontalAlignment.Center);

                //海外学习表头信息的设定
                listView_StudyAbord.View = View.Details;
                listView_StudyAbord.LabelEdit = true;
                listView_StudyAbord.GridLines = true;
                listView_StudyAbord.AllowColumnReorder = true;
                listView_StudyAbord.LabelEdit = false;
                listView_StudyAbord.HideSelection = false;
                listView_StudyAbord.Columns.Add("", 0, HorizontalAlignment.Center);
                listView_StudyAbord.Columns.Add("海外学习开始时间", 130, HorizontalAlignment.Center);
                listView_StudyAbord.Columns.Add("海外学习结束时间", 130, HorizontalAlignment.Center);
                listView_StudyAbord.Columns.Add("海外学习国别", 205, HorizontalAlignment.Center);
                listView_StudyAbord.Columns.Add("海外学习院校及专业", 250, HorizontalAlignment.Center);
                listView_StudyAbord.Columns.Add("海外学习所获学历和学位", 216, HorizontalAlignment.Center);

                //海外工作表头信息的设定
                listView_WorkAbord.View = View.Details;
                listView_WorkAbord.LabelEdit = true;
                listView_WorkAbord.GridLines = true;
                listView_WorkAbord.AllowColumnReorder = true;
                listView_WorkAbord.LabelEdit = false;
                listView_WorkAbord.HideSelection = false;
                listView_WorkAbord.Columns.Add("", 0, HorizontalAlignment.Center);
                listView_WorkAbord.Columns.Add("海外工作开始时间", 130, HorizontalAlignment.Center);
                listView_WorkAbord.Columns.Add("海外工作结束时间", 130, HorizontalAlignment.Center);
                listView_WorkAbord.Columns.Add("海外工作国别", 205, HorizontalAlignment.Center);
                listView_WorkAbord.Columns.Add("海外工作单位及职务", 250, HorizontalAlignment.Center);
                listView_WorkAbord.Columns.Add("海外工作专业领域", 216, HorizontalAlignment.Center);

                //海外工作表头信息的设定
                listView_TrainMessure.View = View.Details;
                listView_TrainMessure.LabelEdit = true;
                listView_TrainMessure.GridLines = true;
                listView_TrainMessure.AllowColumnReorder = true;
                listView_TrainMessure.LabelEdit = false;
                listView_TrainMessure.HideSelection = false;
                listView_TrainMessure.Columns.Add("", 0, HorizontalAlignment.Center);
                listView_TrainMessure.Columns.Add("报告事项", 130, HorizontalAlignment.Center);
                listView_TrainMessure.Columns.Add("起始时间", 100, HorizontalAlignment.Center);
                listView_TrainMessure.Columns.Add("结束时间", 100, HorizontalAlignment.Center);
                listView_TrainMessure.Columns.Add("培训形式", 110, HorizontalAlignment.Center);
                listView_TrainMessure.Columns.Add("内容", 496, HorizontalAlignment.Left);
            }
            catch (Exception) { }
        }

        /// <summary>
        /// 正副职改变时，刷新页面。
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void comboBox1_SelectedIndexChanged_1(object sender, EventArgs e)
        {
            if (nowUnit.Text.Equals(""))
            {
                MessageBox.Show("请先创建或打开数据库文件！");
                return;
            }
            try
            {
                if (comboBox1.Text.Equals("所有人员"))
                {
                    comboxtext_qd = "所有人员";
                }
                else
                {
                    comboxtext_qd = comboBox1.Text.Substring(0, 2);
                }
                showAll();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        public void showAll()
        {
            try
            {
                listView.Items.Clear();
                ReadIni readIni = new ReadIni();
                readIni.WriteString("qd", comboxtext_qd);
                DataOperation dataOperation = new DataOperation();
                string sql1 = "";
                string sql2 = "";
                if (comboxtext_qd.Equals("正职"))
                {
                    controlSearch.option.SelectedIndex = -1;
                    controlSearch.inputContent.Text = "";
                    sql1 = " select * from TB_CommonInfo where (isDelete='0' or isDelete is null) and qd='1' order by rank,joinTeam desc";
                }
                else
                {
                    if (comboxtext_qd.Equals("副职"))
                    {
                        controlSearch.option.SelectedIndex = -1;
                        controlSearch.inputContent.Text = "";
                        sql1 = "select * from TB_CommonInfo where (isDelete='0' or isDelete is null) and qd='0' order by rank,joinTeam desc";
                    }
                    else
                    {
                        controlSearch.option.SelectedIndex = -1;
                        controlSearch.inputContent.Text = "";
                        sql1 = "select * from TB_CommonInfo where (isDelete='0' or isDelete is null) order by rank,joinTeam desc";
                    }
                }
                DataTable dt1 = dataOperation.GetOneDataTable_sql(sql1);
                DataTable dt2 = dataOperation.GetOneDataTable_sql(sql2);

                if (dt1 == null || dt1.Rows.Count == 0)
                    canSecondClear = true;
                else
                    canSecondClear = false;

                if (isReactChange == true)
                {
                    ShowSearch(dt1);
                    ShowSearch(dt2);
                }
                isReactChange = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        /// <summary>
        /// 向listview中添加人员信息。
        /// </summary>
        /// <param name="dt">存储人员信息的数据表</param>
        public void ShowSearch(DataTable dt)
        {
            try
            {
                if (canClearListView)
                {
                    listView.Items.Clear();
                }

                if (dt.Rows.Count > 0 && dt != null)
                {
                    string version = readIni.ReadString("version");
                    DataOperation oper = new DataOperation(Application.StartupPath + "\\DB\\DBHBMSU.db");
                    string sql1 = "select * from TB_Grade where kind='" + version + "'";
                    DataTable dt1 = oper.GetOneDataTable_sql(sql1);

                    int from = listView.Items.Count;
                    groupBox1.Text = "初步人选名册（" + (dt.Rows.Count + from) + ")";
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        ListViewItem it = new ListViewItem();
                        it.Tag = dt.Rows[i]["cid"];
                        it.SubItems.Add((i + 1 + from).ToString());
                        it.SubItems.Add(dt.Rows[i]["name"].ToString());
                        it.SubItems.Add(dt.Rows[i]["sex"].ToString());
                        it.SubItems.Add(dt.Rows[i]["nation"].ToString());
                        it.SubItems.Add(dt.Rows[i]["unitname"].ToString());
                        it.SubItems.Add(dt.Rows[i]["department"].ToString());
                        it.SubItems.Add(dt.Rows[i]["position"].ToString());
                        if (dt.Rows[i]["grade"].ToString() != string.Empty && dt.Rows.Count > 0 && Convert.ToInt32(dt.Rows[i]["grade"]) != -1)
                        {
                            switch (Convert.ToInt32(dt.Rows[i]["grade"]))
                            {
                                case 1:
                                    it.SubItems.Add("办事员"); break;
                                case 2:
                                    it.SubItems.Add("科员"); break;
                                case 3:
                                    it.SubItems.Add("副科级"); break;
                                case 4:
                                    it.SubItems.Add("正科级"); break;
                                case 5:
                                    it.SubItems.Add("副县处级"); break;
                                case 6:
                                    it.SubItems.Add("正县处级"); break;
                                case 7:
                                    it.SubItems.Add("副厅级"); break;
                                case 8:
                                    it.SubItems.Add("正厅级"); break;

                            }
                        }
                        else
                        {
                            it.SubItems.Add("");
                        }
                        it.SubItems.Add(dt.Rows[i]["partyClass"].ToString());
                        it.SubItems.Add(dt.Rows[i]["native"].ToString());
                        it.SubItems.Add(dt.Rows[i]["birthday"].ToString());
                        it.SubItems.Add(dt.Rows[i]["partyTime"].ToString());
                        it.SubItems.Add(dt.Rows[i]["workTime"].ToString());
                        it.SubItems.Add(dt.Rows[i]["fullEducation"].ToString());
                        it.SubItems.Add(dt.Rows[i]["fullDegree"].ToString());
                        it.SubItems.Add(dt.Rows[i]["fullSchool"].ToString());
                        it.SubItems.Add(dt.Rows[i]["fullSpecialty"].ToString());
                        it.SubItems.Add(dt.Rows[i]["workEducation"].ToString());
                        it.SubItems.Add(dt.Rows[i]["workDegree"].ToString());
                        it.SubItems.Add(dt.Rows[i]["workGraduate"].ToString());
                        it.SubItems.Add(dt.Rows[i]["workSpecialty"].ToString());
                        it.SubItems.Add(dt.Rows[i]["technicalPost"].ToString());
                        it.SubItems.Add(dt.Rows[i]["SPDegree"].ToString());
                        it.SubItems.Add(dt.Rows[i]["experiencePost"].ToString());
                        it.SubItems.Add(dt.Rows[i]["knowField"].ToString());
                        it.SubItems.Add(dt.Rows[i]["trainDirection"].ToString());
                        it.SubItems.Add(dt.Rows[i]["trainMeasure"].ToString());
                        if (Convert.ToInt32(dt.Rows[i]["joinTeam"]) == 1)
                        {
                            it.SubItems.Add("是");
                        }
                        else
                        {
                            it.SubItems.Add("否");
                        }
                        listView.Items.Add(it);

                    }
                    listView.Items[0].Selected = true;
                    isReactChange = true;
                }
                else
                {
                    if (canSecondClear)
                    {
                        listView.Items.Clear();
                        groupBox1.Text = "初步人选名册";
                    }
                }

            }
            catch (Exception) { }
        }

        /// <summary>
        /// 显示时间的线程
        /// </summary>
        private void run()//子线程运行的方法
        {
            try
            {
                MethodInvoker mi = new MethodInvoker(ShowTime);
                for (; ; )
                {
                    Thread.Sleep(1000);//每隔一秒执行一下主线程的howTime()方法
                    this.BeginInvoke(mi);
                }
            }
            catch (Exception)
            {

            }
        }

        /// <summary>
        /// 显示时间
        /// </summary>
        private void ShowTime()//子线程操作主线程的这个方法
        {
            nowTime.Text = System.DateTime.Now + "";
        }

        /// <summary>
        /// 重读单位名称
        /// </summary>
        public void reUnit()
        {
            try
            {
                ReadIni ini = new ReadIni();
                nowUnit.Text = ini.ReadString("unitName");
            }
            catch (Exception) { }
        }

        /// <summary>
        /// 向listview中添加数据
        /// </summary>
        /// <param name="sql"></param>
        public void ShowInfo(string sql)//ListView重新从数据库中获取数据M
        {
            try
            {
                dt = da.GetOneDataTable_sql(sql);
                if (dt != null)
                {
                    int n = dt.Rows.Count;
                    if (n != 0)
                    {
                        n = dt.Rows.Count;
                        groupBox1.Text = "初步人选名册（" + n + ")";
                        for (int i = 0; i < n; i++)
                        {
                            ListViewItem it = new ListViewItem("");
                            it.Tag = dt.Rows[i]["cid"];
                            it.SubItems.Add((i + 1).ToString());
                            it.SubItems.Add(dt.Rows[i]["name"].ToString());
                            it.SubItems.Add(dt.Rows[i]["sex"].ToString());
                            it.SubItems.Add(dt.Rows[i]["nation"].ToString());
                            it.SubItems.Add(dt.Rows[i]["unitname"].ToString());
                            it.SubItems.Add(dt.Rows[i]["department"].ToString());
                            it.SubItems.Add(dt.Rows[i]["position"].ToString());
                            if (!dt.Rows[i]["grade"].ToString().Equals("") && dt.Rows.Count > 0 && Convert.ToInt32(dt.Rows[i]["grade"]) != -1)
                            {
                                string version = readIni.ReadString("version");
                                DataOperation oper = new DataOperation(Application.StartupPath + "\\DB\\DBHBMSU.db");
                                string sql1 = "select * from TB_Grade where kind='" + version + "'";
                                DataTable dt1 = oper.GetOneDataTable_sql(sql1);

                                switch (Convert.ToInt32(dt.Rows[i]["grade"]))
                                {

                                    case 1:

                                        it.SubItems.Add(dt1.Rows[0]["grade"].ToString()); break;
                                    case 2:

                                        it.SubItems.Add(dt1.Rows[1]["grade"].ToString()); break;
                                    case 3:

                                        it.SubItems.Add(dt1.Rows[2]["grade"].ToString()); break;
                                    case 4:

                                        it.SubItems.Add(dt1.Rows[3]["grade"].ToString()); break;
                                    case 5:

                                        it.SubItems.Add(dt1.Rows[4]["grade"].ToString()); break;
                                    case 6:

                                        it.SubItems.Add(dt1.Rows[5]["grade"].ToString()); break;
                                    case 7:

                                        it.SubItems.Add(dt1.Rows[6]["grade"].ToString()); break;
                                    case 8:

                                        it.SubItems.Add(dt1.Rows[7]["grade"].ToString()); break;

                                }
                            }
                            else
                                it.SubItems.Add("");


                            it.SubItems.Add(dt.Rows[i]["partyClass"].ToString());
                            it.SubItems.Add(dt.Rows[i]["native"].ToString());
                            it.SubItems.Add(dt.Rows[i]["birthday"].ToString());
                            it.SubItems.Add(dt.Rows[i]["partyTime"].ToString());
                            it.SubItems.Add(dt.Rows[i]["workTime"].ToString());
                            it.SubItems.Add(dt.Rows[i]["fullEducation"].ToString());
                            it.SubItems.Add(dt.Rows[i]["fullDegree"].ToString());
                            it.SubItems.Add(dt.Rows[i]["fullSchool"].ToString());
                            it.SubItems.Add(dt.Rows[i]["fullSpecialty"].ToString());
                            it.SubItems.Add(dt.Rows[i]["workEducation"].ToString());
                            it.SubItems.Add(dt.Rows[i]["workDegree"].ToString());
                            it.SubItems.Add(dt.Rows[i]["workGraduate"].ToString());
                            it.SubItems.Add(dt.Rows[i]["workSpecialty"].ToString());
                            it.SubItems.Add(dt.Rows[i]["technicalPost"].ToString());
                            it.SubItems.Add(dt.Rows[i]["SPDegree"].ToString());
                            it.SubItems.Add(dt.Rows[i]["experiencePost"].ToString());
                            it.SubItems.Add(dt.Rows[i]["knowField"].ToString());
                            it.SubItems.Add(dt.Rows[i]["trainDirection"].ToString());
                            it.SubItems.Add(dt.Rows[i]["trainMeasure"].ToString());
                            if (Convert.ToInt32(dt.Rows[i]["joinTeam"]) == 1)
                                it.SubItems.Add("是");
                            else
                                it.SubItems.Add("否");
                            listView.Items.Add(it);
                        }
                        if (listView.Items.Count != 0)
                        {
                            listView.Items[0].Selected = true;
                        }
                    }
                }
            }
            catch (Exception) { }
        }

        /// <summary>
        /// 当选中的人员变化时，RemainInfo中的内容也随之改变
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AllInformation_ListView_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                isSelected = false;
                int i = ((ListView)sender).SelectedItems.Count;
                if (((ListView)sender).SelectedItems.Count > 0)
                {
                    cid = (((ListView)sender).SelectedItems[0].Tag).ToString();
                    delCidUse = cid;
                    SelectedItem = ((ListView)sender).SelectedItems[0];
                    clearPage();
                    readShowTable(cid);
                    isSelected = true;
                }
                else
                {
                    cid = null;
                }
                if (listView.SelectedItems.Count == 0)
                {

                    TSMI_Delete.Enabled = false;
                    TSMI_Alter.Enabled = false;
                }
                else
                {
                    TSMI_Delete.Enabled = true;
                    TSMI_Alter.Enabled = true;
                }
            }
            catch (Exception) { }

        }

        /// <summary>
        /// 当点击label_regist，所调用的方法
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void label_regist_Click(object sender, EventArgs e)
        {
            try
            {
                if (panelStyle == "insert")
                {
                    if (comboxtext_qd.Equals("所有人员"))
                    {
                        MessageBox.Show("请选择正副职！", "提示");
                        return;
                    }
                    if (listView.SelectedItems.Count > 0)
                    {
                        int selected = Convert.ToInt32(listView.SelectedItems[0].Index.ToString());
                        FrmSituationRegist frmSituationRegist = new FrmSituationRegist();
                        frmSituationRegist.listViewCid = cid;
                        frmSituationRegist.isSingle = true;
                        frmSituationRegist.ci = this;
                        frmSituationRegist.ShowDialog();
                        showAll();
                        listView.Items[selected].Selected = true;
                    }
                    else
                    {
                        MessageBox.Show("请选择人员！", "提示");
                        return;
                    }
                }
                else
                    if (panelStyle == "file")
                    {
                        if (folderBrowserDialog1.ShowDialog() == DialogResult.OK)
                        {
                            string filepath = folderBrowserDialog1.SelectedPath;
                            string location = System.Reflection.Assembly.GetExecutingAssembly().Location;
                            string serviceFileName = location.Substring(0, location.LastIndexOf('\\') + 1) + "空白报表";
                            CopyDirectory(serviceFileName, filepath, true);
                        }
                    }
            }
            catch (Exception) { }

        }
        /// <summary>
        /// 复制目录
        /// </summary>
        /// <param name="SourcePath">源路径</param>
        /// <param name="TargetPath">目标路径</param>
        /// <param name="Overwrite">是否覆盖</param>
        public void CopyDirectory(string SourcePath, string TargetPath, bool Overwrite)
        {
            // 如果源目录不存在，则退出
            if (!Directory.Exists(SourcePath))
            {
                return;
            }

            try
            {

                // 如果目标路径不存在，则创建此文件夹
                if (!Directory.Exists(TargetPath))
                {
                    Directory.CreateDirectory(TargetPath);
                }
            }
            catch (Exception ex)
            {
                string ErrInfo = ex.Message;
                return;
            }
            if (Directory.Exists(TargetPath))
            {

                // 遍历源路径的文件夹，获取文件名（带路径的）
                foreach (string FileName in Directory.GetFiles(SourcePath))
                {
                    try
                    {

                        //复制文件
                        File.Copy(FileName, Path.Combine(TargetPath, Path.GetFileName(FileName)), Overwrite);
                    }
                    catch (Exception ex)
                    {
                        string ErrInfo = ex.Message;
                    }
                }

                // 子文件夹的遍历

                foreach (string SubPath in Directory.GetDirectories(SourcePath))
                {

                    //复制文件
                    CopyDirectory(SubPath, Path.Combine(TargetPath, Path.GetFileName(SubPath)), Overwrite);
                }
                MessageBox.Show("文件已成功导出至:" + TargetPath + " 目录下！");
            }

        }
        /// <summary>
        /// 当点击label_information_Gather，所调用的方法
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void label_informationGathering_Click(object sender, EventArgs e)
        {
            try
            {
                if (panelStyle == "insert")
                {

                    if (comboxtext_qd.Equals("所有人员"))
                    {
                        MessageBox.Show("请选择正副职！");
                        return;
                    }
                    if (listView.SelectedItems.Count > 0)
                    {
                        int selected = Convert.ToInt32(listView.SelectedItems[0].Index.ToString());
                        FrmInformationGathering frmInformationGathering = new FrmInformationGathering();
                        frmInformationGathering.listViewCid = cid;
                        frmInformationGathering.isSingle = true;
                        frmInformationGathering.ci = this;
                        frmInformationGathering.ShowDialog();
                        showAll();
                        listView.Items[selected].Selected = true;
                    }
                    else
                    {
                        MessageBox.Show("请选择人员！");
                        return;
                    }
                }
            }
            catch (Exception) { }

        }

        /// <summary>
        /// 当点击label_meeting，所调用的方法
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void label_meeting_Click(object sender, EventArgs e)
        {
            try
            {
                if (this.panelStyle == "insert")
                {
                    if (comboxtext_qd.Equals("所有人员"))
                    {
                        MessageBox.Show("请选择正副职！", "提示");
                        return;
                    }
                    if (listView.SelectedItems.Count > 0)
                    {
                        int selected = Convert.ToInt32(listView.SelectedItems[0].Index.ToString());
                        FrmSituationRegist frmSituationRegist = new FrmSituationRegist();
                        frmSituationRegist.listViewCid = cid;
                        frmSituationRegist.isSingle = true;
                        frmSituationRegist.ci = this;
                        frmSituationRegist.ShowDialog();
                        showAll();
                        listView.Items[selected].Selected = true;
                    }
                    else
                    {
                        MessageBox.Show("请选择人员！", "提示");
                        return;
                    }
                }
                if (this.panelStyle == "file")
                {
                    if (!nowUnit.Text.Equals(""))
                    {
                        if (MessageBox.Show("要执行此操作，必须先关闭当前正在操作的文件，是否关闭？", "提示", MessageBoxButtons.YesNo) == DialogResult.Yes)
                        {
                            nowUnit.Text = "";
                            listView.Items.Clear();
                            clearPage();
                            cid = string.Empty;
                            OpenDataFile();
                        }
                    }
                    else
                    {
                        OpenDataFile();
                    }
                }
                if (panelStyle == "search")
                {
                    new FrmSearchForm().ShowDialog();
                }
            }
            catch (Exception) { }
        }

        public void OpenFile()
        {
            openFileDialog1.Filter = ".hbmis文件|*.hbmis";
            openFileDialog1.Title = "请选择要打开的文件！";
            openFileDialog1.CheckFileExists = true;
            openFileDialog1.Multiselect = false;
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                string sql = "";
                listView.Items.Clear();
                readIni.WriteString("filePath", openFileDialog1.FileName.ToString());
                da = new DataOperation();
                DataTable datatable1 = da.GetOneDataTable_sql("Select * from TB_LocalUnit");
                if (datatable1.Rows.Count != 0)
                {
                    nowUnit.Text = datatable1.Rows[0]["unitname"].ToString();
                    readIni.WriteString("unitname", datatable1.Rows[0]["unitname"].ToString());
                    readIni.WriteString("unitclass", datatable1.Rows[0]["unitclass"].ToString());
                }

                this.Visible = false;
                Visible = true;
                if (comboxtext_qd.Equals("正职"))
                {
                    sql = "select * from TB_CommonInfo where isDelete=0 and qd=1  and (TB_CommonInfo.state is  null or TB_CommonInfo.state ='') and (TB_CommonInfo.promote is null or TB_CommonInfo.promote='')";
                }
                if (comboxtext_qd.Equals("副职"))
                {
                    sql = "select * from TB_CommonInfo where isDelete=0 and qd=0  and (TB_CommonInfo.state is  null or TB_CommonInfo.state ='') and (TB_CommonInfo.promote is null or TB_CommonInfo.promote='')";
                }
                if (comboxtext_qd.Equals("所有人员"))
                {
                    sql = "select * from TB_CommonInfo where isDelete=0  and (TB_CommonInfo.state is  null or TB_CommonInfo.state ='') and (TB_CommonInfo.promote is null or TB_CommonInfo.promote='')";
                }
                ShowInfo(sql);
            }
        }

        /// <summary>
        /// 当点击label_dailog，所调用的方法
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void label_dailog_Click(object sender, EventArgs e)
        {
            try
            {
                if (panelStyle == "insert")
                {

                    if (comboxtext_qd.Equals("所有人员"))
                    {
                        MessageBox.Show("请选择正副职！");
                        return;
                    }
                    if (listView.SelectedItems.Count > 0)
                    {
                        int selected = Convert.ToInt32(listView.SelectedItems[0].Index.ToString());
                        FrmInformationGathering frmInformationGathering = new FrmInformationGathering();
                        frmInformationGathering.listViewCid = cid;
                        frmInformationGathering.isSingle = true;
                        frmInformationGathering.ci = this;
                        frmInformationGathering.ShowDialog();
                        showAll();
                        listView.Items[selected].Selected = true;
                    }
                    else
                    {
                        MessageBox.Show("请选择人员！");
                        return;
                    }
                }
                if (this.panelStyle == "file")
                {
                    if (nowUnit.Text.Equals(""))//2013年3月1日10:08:45
                    {
                        MessageBox.Show("当前系统未打开任何单位的数据文件，因此无法执行数据导出功能！");
                    }
                    else
                    {
                        saveFileDialog1.Title = "导出";
                        saveFileDialog1.Filter = ".hbz文件|*.hbz";
                        string sourceFileName = readIni.ReadString("filePath");
                        saveFileDialog1.AddExtension = true;
                        saveFileDialog1.DefaultExt = ".hbz";
                        if (saveFileDialog1.ShowDialog() == DialogResult.OK)
                        {
                            string s = saveFileDialog1.FileName.ToString();
                            openFileDialog1.Title = "请选择您要存放的位置！";
                            EncryptFile(sourceFileName, s, myPassword);
                            MessageBox.Show("导出成功", "提示");
                        }
                    }
                }
                if (panelStyle == "search")
                {
                    FrmAge age = new FrmAge();
                    age.ci = this;
                    age.ShowDialog();
                }
            }
            catch (Exception) { }
        }

        /// <summary>
        /// 创建Rijndael SymmetricAlgorithm
        /// </summary>
        /// <param name="password">密码</param>
        /// <param name="salt"></param>
        /// <returns>加密对象</returns>
        private static SymmetricAlgorithm CreateRijndael(string password, byte[] salt)
        {
            PasswordDeriveBytes pdb = new PasswordDeriveBytes(password, salt, "SHA256", 1000);

            SymmetricAlgorithm sma = Rijndael.Create();
            sma.KeySize = 256;
            sma.Key = pdb.GetBytes(32);
            sma.Padding = PaddingMode.PKCS7;
            return sma;
        }
        /// <summary>
        /// 加密文件随机数生成
        /// </summary>
        private static RandomNumberGenerator rand = new RNGCryptoServiceProvider();
        /// <summary>
        /// 生成指定长度的随机Byte数组
        /// </summary>
        /// <param name="count">Byte数组长度</param>
        /// <returns>随机Byte数组</returns>
        private static byte[] GenerateRandomBytes(int count)
        {
            byte[] bytes = new byte[count];
            rand.GetBytes(bytes);
            return bytes;
        }
        /// <summary>
        /// 加密文件
        /// </summary>
        /// <param name="inFile">待加密文件</param>
        /// <param name="outFile">加密后输入文件</param>
        /// <param name="password">加密密码</param>
        public static void EncryptFile(string inFile, string outFile, string password)
        {
            using (FileStream fin = File.OpenRead(inFile), fout = File.OpenWrite(outFile))
            {
                long lSize = fin.Length; // 输入文件长度
                int size = (int)lSize;
                byte[] bytes = new byte[BUFFER_SIZE]; // 缓存
                int read = -1; // 输入文件读取数量
                int value = 0;

                // 获取IV和salt
                byte[] IV = GenerateRandomBytes(16);
                byte[] salt = GenerateRandomBytes(16);

                // 创建加密对象
                SymmetricAlgorithm sma = CreateRijndael(password, salt);
                sma.IV = IV;

                // 在输出文件开始部分写入IV和salt
                fout.Write(IV, 0, IV.Length);
                fout.Write(salt, 0, salt.Length);

                // 创建散列加密
                HashAlgorithm hasher = SHA256.Create();
                using (CryptoStream cout = new CryptoStream(fout, sma.CreateEncryptor(), CryptoStreamMode.Write), chash = new CryptoStream(Stream.Null, hasher, CryptoStreamMode.Write))
                {
                    BinaryWriter bw = new BinaryWriter(cout);
                    bw.Write(lSize);

                    bw.Write(FC_TAG);

                    // 读写字节块到加密流缓冲区
                    while ((read = fin.Read(bytes, 0, bytes.Length)) != 0)
                    {
                        cout.Write(bytes, 0, read);
                        chash.Write(bytes, 0, read);
                        value += read;
                    }
                    // 关闭加密流
                    chash.Flush();
                    chash.Close();

                    // 读取散列
                    byte[] hash = hasher.Hash;

                    // 输入文件写入散列
                    cout.Write(hash, 0, hash.Length);

                    // 关闭文件流
                    cout.Flush();
                    cout.Close();
                }
            }
        }

        /// <summary>
        /// 当点击label_name_sheet，所调用的方法。
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void label_name_sheet_Click(object sender, EventArgs e)
        {
            try
            {
                if (panelStyle == "insert")
                {
                    if (comboxtext_qd.Equals("所有人员"))
                    {
                        MessageBox.Show("请选择正副职！");
                        return;
                    }
                    if (listView.SelectedItems.Count > 0)
                    {
                        FrmMaterial frmMaterial = new FrmMaterial();
                        frmMaterial.listViewCid = cid;
                        frmMaterial.controlMain = this;
                        frmMaterial.isSingle = true;
                        frmMaterial.ShowDialog();
                        showAll();
                    }
                    else
                    {
                        MessageBox.Show("请选择人员！");
                        return;
                    }
                }
                if (this.panelStyle == "file")
                {
                    nowUnit.Text = "";
                    this.listView.Items.Clear();
                    clearPage();
                    MessageBox.Show("当前文件已经关闭！", "提示");
                }
            }
            catch (Exception) { }
        }
        /// <summary>
        /// 当点击label_Material，所调用的方法(新增人员)。
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void label_Material_Click(object sender, EventArgs e)
        {
            try
            {
                if (panelStyle == "insert")
                {
                    if (comboxtext_qd.Equals("所有人员"))
                    {
                        MessageBox.Show("请选择正副职！");
                        return;
                    }
                    if (listView.SelectedItems.Count > 0)
                    {
                        FrmMaterial frmMaterial = new FrmMaterial();
                        frmMaterial.listViewCid = cid;
                        frmMaterial.controlMain = this;
                        frmMaterial.isSingle = true;
                        frmMaterial.ShowDialog();
                        showAll();
                    }
                    else
                    {
                        MessageBox.Show("请选择人员！");
                        return;
                    }
                }
            }
            catch (Exception) { }

        }
        /// <summary>
        /// 点击删除事件的处理，实现删除选中的人员。
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_Delete_Click(object sender, EventArgs e)
        {
            Delete();
            TSMI_Recover.Enabled = true;
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void allpeople_Lb_Click(object sender, EventArgs e)
        {
            try
            {
                if (this.panelStyle == "insert")
                {

                    ReadIni readIni = new ReadIni();
                    if (!readIni.ReadString("filePath").Equals(""))
                    {
                        if (!comboxtext_qd.Equals("所有人员"))
                        {
                            FrmNameSheet FrmNameSheet = new FrmNameSheet();
                            FrmNameSheet.controlMain = this;
                            FrmNameSheet.cid = cid;
                            FrmNameSheet.Clear_Button.Visible = true;
                            FrmNameSheet.ShowDialog();
                        }
                        else
                        {
                            MessageBox.Show("请选择正副职！", "提示");
                        }
                    }
                    else
                    {
                        MessageBox.Show("请先创建文件或打开文件！", "提示");
                    }
                }
                if (this.panelStyle == "search")
                {
                    new FrmAnalyze().ShowDialog();
                }
                if (this.panelStyle == "file")
                {
                    if (nowUnit.Text.Equals(""))
                    {
                        FrmUnit frmUnit = new FrmUnit();
                        frmUnit.LoginMain = true;
                        frmUnit.ci = this;
                        frmUnit.ShowDialog();
                    }
                    else
                    {
                        if (MessageBox.Show("要执行此操作，必须先关闭当前正在操作的文件，是否关闭？", "提示", MessageBoxButtons.YesNo) == DialogResult.Yes)
                        {
                            nowUnit.Text = "";
                            listView.Items.Clear();
                            clearPage();
                            cid = string.Empty;
                            groupBox1.Text = "干部人选名册";
                            FrmUnit frmUnit = new FrmUnit();
                            frmUnit.LoginMain = true;
                            frmUnit.ci = this;
                            frmUnit.ShowDialog();
                        }
                    }
                }
            }
            catch (Exception) { }
        }

        /// <summary>
        /// 双击listview事件处理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AllInformation_ListView_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            try
            {
                if (isSelected == true)
                {
                    if (cid != string.Empty)
                    {
                        FrmNameSheet frmNameSheet = new FrmNameSheet();
                        frmNameSheet.Clear_Button.Visible = false;
                        frmNameSheet.controlMain = this;
                        frmNameSheet.cid = this.cid;
                        frmNameSheet.isModify = true;
                        frmNameSheet.ShowDialog();
                    }
                    else
                    {
                        MessageBox.Show("请选择人员！", "提示");
                    }

                }
            }
            catch (Exception) { }
        }

        /// <summary>
        /// 鼠标进入allpeople_Lb事件的处理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void allpeople_Lb_MouseEnter(object sender, EventArgs e)
        {
            allpeople_Lb.ForeColor = Color.Red;
        }

        /// <summary>
        /// 鼠标移开allpeople_Lb事件的处理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void allpeople_Lb_MouseLeave(object sender, EventArgs e)
        {
            allpeople_Lb.ForeColor = Color.Black;
        }

        /// <summary>
        /// 当鼠标移开时，调用的方法
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void label_information_gather_MouseLeave(object sender, EventArgs e)
        {
            Label l = (Label)sender;
            l.ForeColor = Color.Black;
        }

        /// <summary>
        /// 对鼠标移开label_information事件处理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void label_information_gather_MouseEnter(object sender, EventArgs e)
        {
            Label l = (Label)sender;
            l.ForeColor = Color.Red;

        }

        /// <summary>
        /// 右键删除事件的处理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TSMI_Delete_Click(object sender, EventArgs e)
        {
            Delete();
            TSMI_Recover.Enabled = true;
        }

        /// <summary>
        /// 右键修改事件的处理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TSMI_Alter_Click(object sender, EventArgs e)
        {
            if (listView.SelectedItems.Count > 0)
            {
                FrmNameSheet frmNameSheet = new FrmNameSheet();
                frmNameSheet.Clear_Button.Visible = false;
                frmNameSheet.controlMain = this;
                frmNameSheet.cid = this.cid;
                frmNameSheet.isModify = true;
                frmNameSheet.ShowInfo();
                frmNameSheet.ShowDialog();
            }
            else
            {
                MessageBox.Show("请选择人员！");

            }
        }

        /// <summary>
        /// 右键撤销删除事件的处理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TSMI_Recover_Click(object sender, EventArgs e)
        {
            try
            {
                if (list.Count > 0)
                {
                    DataOperation dataOperation = new DataOperation();
                    string recoverCid = list[list.Count() - 1].Cid;
                    string sql = "";
                    if (comboxtext_qd.Equals("正职"))
                        sql = "select * from TB_CommonInfo where cid='" + recoverCid + "' and qd=1";
                    if (comboxtext_qd.Equals("副职"))
                        sql = "select * from TB_CommonInfo where cid='" + recoverCid + "' and qd=0";
                    if (comboxtext_qd.Equals("所有人员"))
                        sql = "select * from TB_CommonInfo where cid='" + recoverCid + "'";
                    DataTable dt = dataOperation.GetOneDataTable_sql(sql);
                    dataOperation.InsertMark(recoverCid, false);
                    if (dt != null && dt.Rows.Count > 0)
                    {
                        list.RemoveAt(list.Count() - 1);
                        ListViewItem it = new ListViewItem("");
                        it.Tag = dt.Rows[0]["cid"];
                        it.SubItems.Add((listView.Items.Count + 1).ToString());
                        it.SubItems.Add(dt.Rows[0]["name"].ToString());
                        it.SubItems.Add(dt.Rows[0]["sex"].ToString());
                        it.SubItems.Add(dt.Rows[0]["nation"].ToString());
                        it.SubItems.Add(dt.Rows[0]["department"].ToString());
                        it.SubItems.Add(dt.Rows[0]["position"].ToString());

                        if (dt.Rows[0]["grade"].ToString() != "" && dt.Rows.Count > 0 && Convert.ToInt32(dt.Rows[0]["grade"].ToString()) != -1)
                        {
                            string version = readIni.ReadString("version");
                            DataOperation oper = new DataOperation(Application.StartupPath + "\\DB\\DBHBMSU.db");
                            string sql1 = "select * from TB_Grade where kind='" + version + "'";
                            DataTable dt1 = oper.GetOneDataTable_sql(sql1);

                            switch (Convert.ToInt32(dt.Rows[0]["grade"]))
                            {

                                case 0:

                                    it.SubItems.Add(dt1.Rows[0]["grade"].ToString()); break;
                                case 1:

                                    it.SubItems.Add(dt1.Rows[1]["grade"].ToString()); break;
                                case 2:

                                    it.SubItems.Add(dt1.Rows[2]["grade"].ToString()); break;
                                case 3:

                                    it.SubItems.Add(dt1.Rows[3]["grade"].ToString()); break;
                                case 4:

                                    it.SubItems.Add(dt1.Rows[4]["grade"].ToString()); break;
                                case 5:

                                    it.SubItems.Add(dt1.Rows[5]["grade"].ToString()); break;
                                case 6:

                                    it.SubItems.Add(dt1.Rows[6]["grade"].ToString()); break;
                                case 7:

                                    it.SubItems.Add(dt1.Rows[7]["grade"].ToString()); break;

                            }
                        }
                        else
                            it.SubItems.Add("");


                        it.SubItems.Add(dt.Rows[0]["partyClass"].ToString());
                        it.SubItems.Add(dt.Rows[0]["native"].ToString());

                        it.SubItems.Add(dt.Rows[0]["birthday"].ToString());
                        it.SubItems.Add(dt.Rows[0]["partyTime"].ToString());
                        it.SubItems.Add(dt.Rows[0]["workTime"].ToString());
                        it.SubItems.Add(dt.Rows[0]["fullEducation"].ToString());
                        it.SubItems.Add(dt.Rows[0]["fullDegree"].ToString());
                        it.SubItems.Add(dt.Rows[0]["fullSchool"].ToString());
                        it.SubItems.Add(dt.Rows[0]["fullSpecialty"].ToString());
                        it.SubItems.Add(dt.Rows[0]["workEducation"].ToString());
                        it.SubItems.Add(dt.Rows[0]["workDegree"].ToString());
                        it.SubItems.Add(dt.Rows[0]["workGraduate"].ToString());
                        it.SubItems.Add(dt.Rows[0]["workSpecialty"].ToString());
                        it.SubItems.Add(dt.Rows[0]["technicalPost"].ToString());
                        it.SubItems.Add(dt.Rows[0]["experiencePost"].ToString());
                        it.SubItems.Add(dt.Rows[0]["knowField"].ToString());
                        it.SubItems.Add(dt.Rows[0]["trainDirection"].ToString());
                        it.SubItems.Add(dt.Rows[0]["trainMeasure"].ToString());
                        if (Convert.ToInt32(dt.Rows[0]["joinTeam"]) == 1)
                            it.SubItems.Add("是");
                        else
                            it.SubItems.Add("否");
                        listView.Items.Add(it);

                        AddNumber();

                        listView.Items[listView.Items.Count - 1].Selected = true;
                        groupBox1.Text = "初步人选名册（" + listView.Items.Count + ")";
                    }
                }
                if (list.Count == 0)
                {
                    TSMI_Recover.Enabled = false;
                }
            }
            catch (Exception) { }
        }

        /// <summary>
        /// 删除选中的具体的人员
        /// </summary>
        private void Delete()
        {
            try
            {
                //在ListView中删除一条数据
                listView.Items.RemoveAt(listView.SelectedIndices[0]);

                if (listView.Items.Count != 0)
                {
                    groupBox1.Text = "初步人选名册（" + listView.Items.Count + ")";
                }
                else
                {
                    cid = string.Empty;
                    groupBox1.Text = "初步人选名册";
                }
                AddNumber();

                da.InsertMark(delCidUse, true);

                ListViewDel del = new ListViewDel();

                del.Cid = delCidUse;

                list.Add(del);

                if (listView.Items.Count > 0)
                {
                    listView.Items[0].Selected = true;
                }
                else
                {
                    clearPage();
                }
            }
            catch (Exception) { }
        }

        /// <summary>
        /// 添加序号
        /// </summary>
        public void AddNumber()
        {
            for (int i = 0; i < listView.Items.Count; i++)
            {
                listView.Items[i].SubItems[1].Text = (i + 1).ToString();
            }
        }

        /// <summary>
        /// listView上鼠标点击事件的处理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void listView_MouseClick(object sender, MouseEventArgs e)
        {

        }

        /// <summary>
        /// 鼠标松开事件的处理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void listView_MouseUp(object sender, MouseEventArgs e)
        {
            if (listView.SelectedItems.Count == 0 && SelectedItem != null)
            {
                SelectedItem.Selected = true;
            }
        }
        /// <summary>
        /// 读取人员的相关信息
        /// </summary>
        /// <param name="cid"></param>
        public void readShowTable(string cid)
        {
            //简要情况
            //姓名、年龄、健康状况、出生地、熟悉专业有何专长、备注、三年考核,考察材料
            #region
            string sql = "select name,age, health, birthplace, specialtyskill, remark, photo, starttime, result1, result2, result3, Material from TB_Commoninfo where cid = '" + cid + "'";
            DataTable TB_TestMaterial = da.GetOneDataTable_sql(sql);
            if (TB_TestMaterial.Rows.Count > 0)
            {
                textBox_name.Text = TB_TestMaterial.Rows[0]["name"].ToString();
                textBox_age.Text = TB_TestMaterial.Rows[0]["age"].ToString();
                textBox_health.Text = TB_TestMaterial.Rows[0]["health"].ToString();
                textBox_birthplace.Text = TB_TestMaterial.Rows[0]["birthplace"].ToString();
                textBox_specialty.Text = TB_TestMaterial.Rows[0]["specialtyskill"].ToString();
                textBox_remark.Text = TB_TestMaterial.Rows[0]["remark"].ToString();
                if (TB_TestMaterial.Rows[0]["photo"] != DBNull.Value)
                {
                    byte[] image = (byte[])TB_TestMaterial.Rows[0]["photo"];
                    this.pictureBox_photo.Image = Image.FromStream(new MemoryStream(image, false));
                }
                else
                {
                    this.pictureBox_photo.Image = null;
                }
                int n = 0;
                if (!TB_TestMaterial.Rows[0]["starttime"].ToString().Equals(""))
                {
                    n = Convert.ToInt32(TB_TestMaterial.Rows[0]["starttime"].ToString());
                    textBox_year1.Text = n.ToString();
                    textBox_year2.Text = (n + 1).ToString();
                    textBox_year3.Text = (n + 2).ToString();
                    textBox_reult1.Text = TB_TestMaterial.Rows[0]["result1"].ToString();
                    textBox_reult2.Text = TB_TestMaterial.Rows[0]["result2"].ToString();
                    textBox_reult3.Text = TB_TestMaterial.Rows[0]["result3"].ToString();
                }
                richTextBox1.Text = TB_TestMaterial.Rows[0]["material"].ToString();
            }
            #endregion

            //个人简历
            #region
            sql = "select betime, entime, content from TB_Resume where cid = '" + cid + "'";
            DataTable TB_Resume = da.GetOneDataTable_sql(sql);
            for (int i = 0; i < TB_Resume.Rows.Count; i++)
            {
                ListViewItem item = new ListViewItem();
                item.SubItems.Add(TB_Resume.Rows[i]["betime"].ToString());
                item.SubItems.Add(TB_Resume.Rows[i]["entime"].ToString());
                item.SubItems.Add(TB_Resume.Rows[i]["content"].ToString());
                listView_Vital.Items.Add(item);
            }
            #endregion

            //奖惩情况
            #region
            sql = "select * from TB_PunishAward where cid = '" + cid + "'";
            DataTable TB_PunishAward = da.GetOneDataTable_sql(sql);
            for (int i = 0; i < TB_PunishAward.Rows.Count; i++)
            {
                ListViewItem item = new ListViewItem();
                item.SubItems.Add(TB_PunishAward.Rows[i]["class"].ToString());
                item.SubItems.Add(TB_PunishAward.Rows[i]["grade"].ToString());
                item.SubItems.Add(TB_PunishAward.Rows[i]["time"].ToString());
                item.SubItems.Add(TB_PunishAward.Rows[i]["department"].ToString());
                listView_Award.Items.Add(item);
            }
            #endregion

            //家庭成员关系
            #region
            sql = "select * from TB_family where cid = '" + cid + "'";
            DataTable TB_Family = da.GetOneDataTable_sql(sql);
            for (int i = 0; i < TB_Family.Rows.Count; i++)
            {
                ListViewItem item = new ListViewItem();
                item.SubItems.Add(TB_Family.Rows[i]["relationship"].ToString());
                item.SubItems.Add(TB_Family.Rows[i]["name"].ToString());
                item.SubItems.Add(TB_Family.Rows[i]["birthday"].ToString());
                if (TB_Family.Rows[i]["age"].ToString() == "0")
                {
                    item.SubItems.Add("");
                }
                else
                {
                    item.SubItems.Add(TB_Family.Rows[i]["age"].ToString());
                }
                item.SubItems.Add(TB_Family.Rows[i]["country"].ToString());
                item.SubItems.Add(TB_Family.Rows[i]["party"].ToString());
                item.SubItems.Add(TB_Family.Rows[i]["nation"].ToString());
                item.SubItems.Add(TB_Family.Rows[i]["deptJob"].ToString());
                item.SubItems.Add(TB_Family.Rows[i]["remark"].ToString());
                listView_Realation.Items.Add(item);
            }
            #endregion

            //海外学习
            #region
            sql = "select * from TB_SAbroad where cid = '" + cid + "'";
            DataTable TB_SAbroad = da.GetOneDataTable_sql(sql);
            for (int i = 0; i < TB_SAbroad.Rows.Count; i++)
            {
                ListViewItem item = new ListViewItem();
                item.SubItems.Add(TB_SAbroad.Rows[i]["starttime"].ToString());
                item.SubItems.Add(TB_SAbroad.Rows[i]["endtime"].ToString());
                item.SubItems.Add(TB_SAbroad.Rows[i]["country"].ToString());
                item.SubItems.Add(TB_SAbroad.Rows[i]["academy"].ToString());
                item.SubItems.Add(TB_SAbroad.Rows[i]["degree"].ToString());
                listView_StudyAbord.Items.Add(item);
            }
            #endregion

            //海外工作
            #region
            sql = "select * from TB_WAbroad where cid = '" + cid + "'";
            DataTable TB_WAbroad = da.GetOneDataTable_sql(sql);
            for (int i = 0; i < TB_WAbroad.Rows.Count; i++)
            {
                ListViewItem item = new ListViewItem();
                item.SubItems.Add(TB_WAbroad.Rows[i]["starttime"].ToString());
                item.SubItems.Add(TB_WAbroad.Rows[i]["endtime"].ToString());
                item.SubItems.Add(TB_WAbroad.Rows[i]["abroadcountry"].ToString());
                item.SubItems.Add(TB_WAbroad.Rows[i]["departmentPosition"].ToString());
                item.SubItems.Add(TB_WAbroad.Rows[i]["specialtyArea"].ToString());
                listView_WorkAbord.Items.Add(item);
            }
            #endregion

            //重大报告事项
            #region
            sql = "select * from TB_GreatContent where cid = '" + cid + "'";
            DataTable TB_GreatContent = da.GetOneDataTable_sql(sql);
            for (int i = 0; i < TB_GreatContent.Rows.Count; i++)
            {
                if (Convert.ToInt32(TB_GreatContent.Rows[i]["matter"].ToString().Trim()) == 1)
                {
                    content1.Text = TB_GreatContent.Rows[i]["content"].ToString();
                }
                if (Convert.ToInt32(TB_GreatContent.Rows[i]["matter"].ToString().Trim()) == 2)
                {
                    content2.Text = TB_GreatContent.Rows[i]["content"].ToString();
                }
                if (Convert.ToInt32(TB_GreatContent.Rows[i]["matter"].ToString().Trim()) == 3)
                {
                    content3.Text = TB_GreatContent.Rows[i]["content"].ToString();
                }
                if (Convert.ToInt32(TB_GreatContent.Rows[i]["matter"].ToString().Trim()) == 4)
                {
                    content4.Text = TB_GreatContent.Rows[i]["content"].ToString();
                }
                if (Convert.ToInt32(TB_GreatContent.Rows[i]["matter"].ToString().Trim()) == 5)
                {
                    content5.Text = TB_GreatContent.Rows[i]["content"].ToString();
                }
                if (Convert.ToInt32(TB_GreatContent.Rows[i]["matter"].ToString().Trim()) == 6)
                {
                    content6.Text = TB_GreatContent.Rows[i]["content"].ToString();
                }
                if (Convert.ToInt32(TB_GreatContent.Rows[i]["matter"].ToString().Trim()) == 7)
                {
                    content7.Text = TB_GreatContent.Rows[i]["content"].ToString();
                }
                if (Convert.ToInt32(TB_GreatContent.Rows[i]["matter"].ToString().Trim()) == 8)
                {
                    content8.Text = TB_GreatContent.Rows[i]["content"].ToString();
                }
                if (Convert.ToInt32(TB_GreatContent.Rows[i]["matter"].ToString().Trim()) == 9)
                {
                    content9.Text = TB_GreatContent.Rows[i]["content"].ToString();
                }
            }
            #endregion

            //熟悉外语语种
            #region
            sql = "select * from TB_FamiliarForeign where cid = '" + cid + "'";
            DataTable TB_FamiliarForeign = da.GetOneDataTable_sql(sql);
            if (TB_FamiliarForeign.Rows.Count > 0)
            {
                for (int i = 0; i < TB_FamiliarForeign.Rows.Count; i++)
                {
                    string tempLevel = TB_FamiliarForeign.Rows[i]["level"].ToString();
                    switch (i)
                    {
                        case 0:
                            foreignKind1.Text = TB_FamiliarForeign.Rows[i]["foreignKind"].ToString();
                            if (tempLevel.Equals("精通"))
                            {
                                this.level11.BackgroundImage = global::HBMISR.Properties.Resources.choice;
                                break;
                            }
                            if (tempLevel.Equals("熟练"))
                            {
                                this.level12.BackgroundImage = global::HBMISR.Properties.Resources.choice;
                                break;
                            }
                            if (tempLevel.Equals("良好"))
                            {
                                this.level13.BackgroundImage = global::HBMISR.Properties.Resources.choice;
                                break;
                            }
                            if (tempLevel.Equals("一般"))
                            {
                                this.level14.BackgroundImage = global::HBMISR.Properties.Resources.choice;
                                break;
                            }
                            break;

                        case 1:

                            foreignKind2.Text = TB_FamiliarForeign.Rows[i]["foreignKind"].ToString();

                            if (tempLevel.Equals("精通"))
                            {
                                this.level21.BackgroundImage = global::HBMISR.Properties.Resources.choice;
                                break;
                            }
                            if (tempLevel.Equals("熟练"))
                            {
                                this.level22.BackgroundImage = global::HBMISR.Properties.Resources.choice;
                                break;
                            }
                            if (tempLevel.Equals("良好"))
                            {
                                this.level23.BackgroundImage = global::HBMISR.Properties.Resources.choice;
                                break;
                            }
                            if (tempLevel.Equals("一般"))
                            {
                                this.level24.BackgroundImage = global::HBMISR.Properties.Resources.choice;
                                break;
                            }
                            break;

                        case 2:

                            foreignKind3.Text = TB_FamiliarForeign.Rows[i]["foreignKind"].ToString();

                            if (tempLevel.Equals("精通"))
                            {
                                this.level31.BackgroundImage = global::HBMISR.Properties.Resources.choice;
                                break;
                            }
                            if (tempLevel.Equals("熟练"))
                            {
                                this.level32.BackgroundImage = global::HBMISR.Properties.Resources.choice;
                                break;
                            }
                            if (tempLevel.Equals("良好"))
                            {
                                this.level33.BackgroundImage = global::HBMISR.Properties.Resources.choice;
                                break;
                            }
                            if (tempLevel.Equals("一般"))
                            {
                                this.level34.BackgroundImage = global::HBMISR.Properties.Resources.choice;
                                break;
                            }
                            break;

                        case 3:

                            foreignKind4.Text = TB_FamiliarForeign.Rows[i]["foreignKind"].ToString();

                            if (tempLevel.Equals("精通"))
                            {
                                this.level41.BackgroundImage = global::HBMISR.Properties.Resources.choice;
                                break;
                            }
                            if (tempLevel.Equals("熟练"))
                            {
                                this.level42.BackgroundImage = global::HBMISR.Properties.Resources.choice;
                                break;
                            }
                            if (tempLevel.Equals("良好"))
                            {
                                this.level43.BackgroundImage = global::HBMISR.Properties.Resources.choice;
                                break;
                            }
                            if (tempLevel.Equals("一般"))
                            {
                                this.level44.BackgroundImage = global::HBMISR.Properties.Resources.choice;
                                break;
                            }
                            break;

                        case 4:

                            foreignKind5.Text = TB_FamiliarForeign.Rows[i]["foreignKind"].ToString();

                            if (tempLevel.Equals("精通"))
                            {
                                this.level51.BackgroundImage = global::HBMISR.Properties.Resources.choice;
                                break;
                            }
                            if (tempLevel.Equals("熟练"))
                            {
                                this.level52.BackgroundImage = global::HBMISR.Properties.Resources.choice;
                                break;
                            }
                            if (tempLevel.Equals("良好"))
                            {
                                this.level53.BackgroundImage = global::HBMISR.Properties.Resources.choice;
                            }
                            if (tempLevel.Equals("一般"))
                            {
                                this.level54.BackgroundImage = global::HBMISR.Properties.Resources.choice;
                                break;
                            }
                            break;
                    }
                }
            }
            #endregion

            //参加培训
            #region
            sql = "select * from TB_TrainExercise where cid = '" + cid + "'";
            DataTable TB_TrainExercise = da.GetOneDataTable_sql(sql);
            for (int i = 0; i < TB_TrainExercise.Rows.Count; i++)
            {
                ListViewItem item = new ListViewItem();
                item.SubItems.Add(TB_TrainExercise.Rows[i]["reportMatter"].ToString());
                item.SubItems.Add(TB_TrainExercise.Rows[i]["startTime"].ToString());
                item.SubItems.Add(TB_TrainExercise.Rows[i]["endtime"].ToString());
                item.SubItems.Add(TB_TrainExercise.Rows[i]["reportContent"].ToString());
                item.SubItems.Add(TB_TrainExercise.Rows[i]["content"].ToString());
                listView_TrainMessure.Items.Add(item);
            }
            #endregion

            //培养措施需求
            #region
            sql = "select * from TB_TrainMethord where cid = '" + cid + "'";
            DataTable TB_TrainMethord = da.GetOneDataTable_sql(sql);
            for (int i = 0; i < TB_TrainMethord.Rows.Count; i++)
            {
                switch (Convert.ToInt32(TB_TrainMethord.Rows[i]["options"]))
                {
                    case 1:
                        explain1.Checked = true;
                        break;
                    case 2:
                        explain2.Checked = true;
                        break;
                    case 3:
                        explain3.Checked = true;
                        break;
                    case 4:
                        explain4.Checked = true;
                        break;
                    case 5:
                        explain5.Checked = true;
                        break;
                    case 6:
                        explain6.Checked = true;
                        break;
                    case 7:
                        explain7.Checked = true;
                        break;
                    case 8:
                        explain8.Checked = true;
                        break;
                    case 9:
                        explain9.Checked = true;
                        break;
                    case 10:
                        explain10.Checked = true;
                        break;

                    case 11:
                        explain11.Checked = true;
                        break;
                    case 12:
                        explain12.Checked = true;
                        break;
                    case 13:
                        explain13.Checked = true;
                        break;
                    case 14:
                        explain14.Checked = true;
                        note14.Text = TB_TrainMethord.Rows[i]["note14"].ToString();
                        break;
                }
            }
            #endregion
        }

        /// <summary>
        /// 清空页面中的所有信息
        /// </summary>
        public void clearPage()
        {
            //清空培养措施
            #region
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
            note14.Text = "";
            #endregion

            //清空熟悉外语语种
            #region
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
            #endregion

            //重大报告事项
            #region
            content1.Text = "";
            content2.Text = "";
            content3.Text = "";
            content4.Text = "";
            content5.Text = "";
            content6.Text = "";
            content7.Text = "";
            content8.Text = "";
            content9.Text = "";
            #endregion

            //考察材料
            #region
            richTextBox1.Text = "";
            #endregion

            //个人简历清空
            listView_Vital.Items.Clear();

            //奖惩清空清空
            listView_Award.Items.Clear();

            //个人简要情况清空
            textBox_age.Text = "";
            textBox_birthplace.Text = "";
            textBox_health.Text = "";
            textBox_name.Text = "";
            textBox_remark.Text = "";
            textBox_reult1.Text = "";
            textBox_reult2.Text = "";
            textBox_reult3.Text = "";
            textBox_specialty.Text = "";
            textBox_year1.Text = "";
            textBox_year2.Text = "";
            textBox_year3.Text = "";
            this.pictureBox_photo.Image = null;

            //家庭成员关系清空
            listView_Realation.Items.Clear();

            //海外学习清空
            listView_StudyAbord.Items.Clear();

            //海外工作清空
            listView_WorkAbord.Items.Clear();

            //培养措施清空
            listView_TrainMessure.Items.Clear();
        }

        /// <summary>
        /// 打开数据文件
        /// </summary>
        public void OpenDataFile()
        {
            openFileDialog1.Filter = "数据文件(*.hbmis *.hbs)|*.hbmis;*.hbs";
            openFileDialog1.Title = "请选择您要打开的文件！";
            openFileDialog1.CheckFileExists = true;
            openFileDialog1.Multiselect = false;
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    DataOperation oper = new DataOperation(openFileDialog1.FileName.ToString());
                    DataTable dt = oper.GetOneDataTable_sql("select * from TB_LocalUnit");
                    if (dt == null)
                    {
                        MessageBox.Show("文件打开失败！", "提示");
                        return;
                    }
                    if (dt.Rows.Count == 1)
                    {
                        readIni.WriteString("filePath", openFileDialog1.FileName.ToString());
                        readIni.WriteString("unitName", dt.Rows[0]["unitname"].ToString());
                        readIni.WriteString("unitClass", dt.Rows[0]["unitclass"].ToString());

                        nowUnit.Text = dt.Rows[0]["unitname"].ToString();
                        string sql = "";
                        if (comboxtext_qd.Equals("正职"))
                        {
                            sql = "select * from TB_CommonInfo where isDelete=0 and qd=1";
                        }
                        else if (comboxtext_qd.Equals("副职"))
                        {
                            sql = "select * from TB_CommonInfo where isDelete=0 and qd=0 ";
                        }
                        else
                        {
                            sql = "select * from TB_CommonInfo where isDelete=0";
                        }
                        ShowInfo(sql);
                        controlSearch.option.Enabled = true;
                        comboBox1.Enabled = true;
                        button1.Enabled = true;
                    }
                    else
                    {
                        MessageBox.Show("上报端系统不能打开综合端文件！", "提示");
                        return;
                    }
                }
                catch { MessageBox.Show("文件打开失败！", "提示"); }
            }
        }

        /// <summary>
        /// 当拖动某项时
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void listView_ItemDrag(object sender, ItemDragEventArgs e)
        {
            listView.DoDragDrop(e.Item, DragDropEffects.Move);
        }
        /// <summary>
        /// 用鼠标拖动某项至该控件的区域时
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void listView_DragEnter(object sender, DragEventArgs e)
        {
            e.Effect = DragDropEffects.Move;
        }
        /// <summary>
        /// 拖动时拖着某项置于某行上方时
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void listView_DragOver(object sender, DragEventArgs e)
        {
            Point ptScreen = new Point(e.X, e.Y);
            Point pt = listView.PointToClient(ptScreen);
            ListViewItem item = listView.GetItemAt(pt.X, pt.Y);
            if (item != null)
            {
                item.Selected = true;
            }
        }

        /// <summary>
        /// 拖动结束
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void listView_DragDrop(object sender, DragEventArgs e)
        {
            try
            {
                ListViewItem draggedItem = (ListViewItem)e.Data.GetData(typeof(ListViewItem));
                Point ptScreen = new Point(e.X, e.Y);
                Point pt = listView.PointToClient(ptScreen);
                ListViewItem TargetItem = listView.GetItemAt(pt.X, pt.Y);//拖动的项将放置于该项之前    
                listView.Items.Insert(TargetItem.Index, (ListViewItem)draggedItem.Clone());
                listView.Items.Remove(draggedItem);
            }
            catch { }
            for (int i = 0; i < listView.Items.Count; i++)
            {
                listView.Items[i].SubItems[1].Text = (i + 1).ToString();
                string cid = listView.Items[i].Tag.ToString();
                SQLPoolManage.sqlPoolManage.PushSQL("update TB_CommonInfo set rank=" + i + " where cid='" + cid + "'");
            }
            //MessageBox.Show(listView.Items[1].SubItems[1].Text.ToString());
            //定时触发SQL执行线程 
            System.Threading.Timer threadTimer = new System.Threading.Timer(new System.Threading.TimerCallback(SQLPoolManage.sqlPoolManage.ExecuteSQL), null, 0, 100);
        }
    }
}
