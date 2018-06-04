
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using HBMISR.Data;
using HBMISR.GUI.MainGUI;
using HBMISR.Service;
using HBMISR.GUI.OtherGUI;
using HBMISR.server;


namespace HBMISR.GUI.NoteGUI
{
    /// <summary>
    /// 初步人选名册
    /// </summary>
    public partial class FrmNameSheet : Form
    {
        /// <summary>
        /// 标记是否修改，值为true时表示修改
        /// </summary>
        public bool change = false;
        bool remarkchange = false;
        DateTime myTime = Convert.ToDateTime(DateTime.Now.Year + "." + DateTime.Now.Month + "." + DateTime.Now.Day);

        /// <summary>
        /// 记录ControlMain类型的实例
        /// </summary>
        public ControlMain controlMain;
        ReadIni readIni = new ReadIni();
        string filePath = string.Empty;

        /// <summary>
        /// 标记是修改人员还是新录入人员，如果是修改值为true，否则值为false；
        /// </summary>
        public bool isModify;

        /// <summary>
        /// 记录人员cid
        /// </summary>
        public string cid = string.Empty;

        public bool modified = false;


        public bool QD = true;
        string unitClass;
        public string strUnitClass = string.Empty;
        public string unitname = string.Empty;
        public bool canDefaultSet = false;
        int ii;
        DataOperation da;

        /// <summary>
        /// 初步人选名册构造函数
        /// </summary>
        public FrmNameSheet()
        {
            try
            {
                //读取到数据库的保存路径
                InitializeComponent();
                ii = SetGrade();//设置级别
                da = new DataOperation();
            }
            catch (Exception) { }
        }

        /// <summary>
        /// //给级别下拉列表设置级别
        /// </summary>
        /// <returns></returns>
        public int SetGrade()
        {
            string version = readIni.ReadString("version");
            DataOperation oper = new DataOperation(Application.StartupPath + "\\DB\\DBHBMSU.db");
            string sql = "select * from TB_Grade where kind='" + version + "'";
            DataTable dt = oper.GetOneDataTable_sql(sql);
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                this.grade.Items.Add(dt.Rows[dt.Rows.Count - i - 1]["grade"].ToString());
            }
            return dt.Rows.Count;
        }

        /// <summary>
        /// 初步人选名册初始化
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FrmNameSheet_Load(object sender, EventArgs e)
        {
            unitname = readIni.ReadString("unitname");
            systemdate.Text = DateTime.Now.ToString("yyyy'年'MM'月'") + "01日";
            PartyTime.Text = DateTime.Now.ToString("yyyy'年'MM'月'") + "01日";
            JobTime.Text = DateTime.Now.ToString("yyyy'年'MM'月'") + "01日";
            BrithdayTime.Text = DateTime.Now.ToString("yyyy'年'MM'月'") + "01日";

            if (isModify)
            {
                ShowInfo();
            }
            else
            {
                DefaultSet();
            }
            change = true;
        }

        /// <summary>
        /// 修改时  显示后备干部原有信息
        /// </summary>
        public void ShowInfo()
        {
            try
            {
                string sql = "select * from TB_CommonInfo where cid='" + cid + "'";
                DataTable dt = da.GetOneDataTable_sql(sql);

                this.systemdate.Text = dt.Rows[0]["systemTime"].ToString() + "年";

                this.Name_TextBox.Text = dt.Rows[0]["name"].ToString();
                if (dt.Rows[0]["sex"].ToString() == "男")
                {
                    rdbMan.Checked = true;
                }
                else
                {
                    rdbWoman.Checked = true;
                }
                this.unitClass = dt.Rows[0]["unitclass"].ToString();
                this.unitname = dt.Rows[0]["unitname"].ToString();
                this.Nation_TextBox.Text = dt.Rows[0]["nation"].ToString();
                this.HomeTown_TextBox.Text = dt.Rows[0]["birthplace"].ToString();
                this.Union_TextBox.Text = dt.Rows[0]["department"].ToString();
                this.TrainMessure_TextBox.Text = dt.Rows[0]["trainMeasure"].ToString();
                this.KnowField_TextBox.Text = dt.Rows[0]["knowField"].ToString();
                this.TrainDirection_TextBox.Text = dt.Rows[0]["trainDirection"].ToString();
                this.BrithdayTime.Text = dt.Rows[0]["birthday"].ToString();
                this.PartyTime.Text = dt.Rows[0]["partyTime"].ToString();
                this.cid = dt.Rows[0]["cid"].ToString();
                this.identity.Text = this.cid;
                string tempdp = dt.Rows[0]["partyClass"].ToString();
                if (tempdp == "")
                {
                    this.partyClass.Text = "";
                }
                else
                {
                    int pos = this.partyClass.Items.IndexOf((object)(tempdp));
                    if (pos < 0)
                    {
                        this.partyClass.Text = "其他";
                        this.showDP();
                        this.textBox1.Text = tempdp;
                    }
                    else
                    {
                        this.partyClass.Text = tempdp;
                    }
                }
                this.SpecialityPosition_Degree.Text = dt.Rows[0]["SPDegree"].ToString();
                if (dt.Rows[0]["grade"].ToString() != string.Empty)
                {
                    int a = Convert.ToInt32(dt.Rows[0]["grade"]);

                    this.grade.SelectedIndex = (ii - Convert.ToInt32(dt.Rows[0]["grade"]));
                }
                else
                    this.grade.Text = "";
                this.JobTime.Text = dt.Rows[0]["workTime"].ToString();
                this.MainPosition_TextBox.Text = dt.Rows[0]["experiencePost"].ToString();
                this.Position_TexBox.Text = dt.Rows[0]["position"].ToString();
                if (Convert.ToBoolean(dt.Rows[0]["joinTeam"]) == true)
                    this.checkBox_promote.Checked = true;
                else
                    this.checkBox_promote.Checked = false;
                this.FullEducation_ComboBox.Text = dt.Rows[0]["fullEducation"].ToString();
                this.FullDegree_ComboBox.Text = dt.Rows[0]["fullDegree"].ToString();
                this.FullSchool_TextBox.Text = dt.Rows[0]["fullSchool"].ToString();
                this.FullSpeciality_TextBox.Text = dt.Rows[0]["fullSpecialty"].ToString();
                this.OnEducation_ComboBox.Text = dt.Rows[0]["workEducation"].ToString();
                this.OnDegree_ComboBox.Text = dt.Rows[0]["workDegree"].ToString();
                this.OnGraduateSchool_TextBox.Text = dt.Rows[0]["workGraduate"].ToString();
                this.OnSpeciality_TextBox.Text = dt.Rows[0]["workSpecialty"].ToString();
                this.HomeTown_TextBox.Text = dt.Rows[0]["native"].ToString();
                this.HomeTown_TextBox.Text = dt.Rows[0]["native"].ToString();
                this.SpecialityPosition_TextBox.Text = dt.Rows[0]["technicalPost"].ToString();
                this.SpecialityPosition_Degree.Text = dt.Rows[0]["SPDegree"].ToString();
                if (Convert.ToBoolean(dt.Rows[0]["twoYGE"]))
                    TYGE_checkBox.Checked = true;
                else
                    TYGE_checkBox.Checked = false;
                if (Convert.ToBoolean(dt.Rows[0]["publicSelect"]))
                    PublicSelect_checkBox.Checked = true;
                else
                    PublicSelect_checkBox.Checked = false;
                if (Convert.ToBoolean(dt.Rows[0]["isTwoYear"]))
                    isTwoYear.Checked = true;
                else
                    isTwoYear.Checked = false;
                if (Convert.ToBoolean(dt.Rows[0]["isGuide"]))
                    isGuide.Checked = true;
                else
                    isGuide.Checked = false;
            }
            catch (Exception) { }
        }

        /// <summary>
        /// 默认普通增加时的事件监听
        /// </summary>
        public void DefaultSet()
        {
            Nation_TextBox.SelectedIndex = 0;
            partyClass.SelectedIndex = 0;
            checkBox_promote.Checked = false;
            ReadIni ini = new ReadIni();
            unitClass = ini.ReadString("unitclass");
        }

        /// <summary>
        /// 获取各个文本框中的内容,将其导入数据库//
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Complete_Butoon_Click(object sender, EventArgs e)
        {
            if (!SaveAll())
            {
                return;
            }
            remarkchange = false;
            hideDP();
            this.Close();
        }

        /// <summary>
        ///  保存初步人选名册的信息
        /// </summary>
        public void Save()
        {
            try
            {
                CommonInfo a = new CommonInfo();
                DataOperation dataoperation = new DataOperation();
                //插入数据库
                if (controlMain.comboBox1.Text.Equals("正职干部"))
                {
                    a.Qd = true;
                    QD = true;
                }
                else
                {
                    a.Qd = false;
                    QD = false;
                }
                a.Unitname = this.unitname;
                a.UnitClass = this.unitClass;
                a.Cid = identity.Text.Trim();
                a.SystemTime = systemdate.Text;
                a.Name = this.Name_TextBox.Text;

                ConvertPinYin convertPinYin = new ConvertPinYin();
                a.UnitNamePinYin = convertPinYin.Hanzi2Pinyin(this.unitname) + convertPinYin.Hanzi2PY(this.unitname);
                a.InitialFullSpelling = convertPinYin.Hanzi2Pinyin(this.Name_TextBox.Text.Trim()) + convertPinYin.Hanzi2PY(this.Name_TextBox.Text.Trim());

                if (rdbMan.Checked)
                {
                    a.Sex = "男";
                }
                else
                {
                    a.Sex = "女";
                }
                a.Nation = this.Nation_TextBox.Text;
                a.Department = this.Union_TextBox.Text;
                a.Position = this.Position_TexBox.Text;
                a.Native = this.HomeTown_TextBox.Text;
                a.Birthday = this.BrithdayTime.Text;
                if (partyClass.SelectedIndex != -1 && !partyClass.SelectedItem.Equals("无党派"))
                    a.PartyTime = this.PartyTime.Text;
                else
                    a.PartyTime = "";
                a.WorkTime = this.JobTime.Text;
                //全日制
                a.FullEducation = this.FullEducation_ComboBox.Text;
                a.FullDegree = this.FullDegree_ComboBox.Text;
                a.FullSchool = this.FullSchool_TextBox.Text;
                a.FullSpecialty = this.FullSpeciality_TextBox.Text;

                //在职
                a.WorkEducation = this.OnEducation_ComboBox.Text;
                a.WorkDegree = this.OnDegree_ComboBox.Text;
                a.WorkGraduate = this.OnGraduateSchool_TextBox.Text;
                a.WorkSpecialty = this.OnSpeciality_TextBox.Text;

                a.TechnicalPost = this.SpecialityPosition_TextBox.Text;
                a.ExperiencePost = this.MainPosition_TextBox.Text;
                a.KnowField = this.KnowField_TextBox.Text;
                a.SPDegree = SpecialityPosition_Degree.Text;
                if (this.partyClass.SelectedItem.ToString() == "其他")
                {
                    a.PartyClass = this.textBox1.Text.Trim();
                }
                else
                {
                    a.PartyClass = this.partyClass.Text;
                }
                //级别
                if (this.grade.Text.Equals(""))//级别越大数字越大
                    a.Grade = -1;
                else
                    a.Grade = ii - this.grade.SelectedIndex;
                a.TrainDirection = this.TrainDirection_TextBox.Text;
                a.TrainMeasure = this.TrainMessure_TextBox.Text;
                if (PublicSelect_checkBox.Checked)
                    a.Publicselect = true;
                else
                    a.Publicselect = false;

                if (TYGE_checkBox.Checked)
                    a.TYGE1 = true;
                else
                    a.TYGE1 = false;

                if (checkBox_promote.Checked)
                    a.JoinTeam = true;
                else
                    a.JoinTeam = false;

                if (isTwoYear.Checked)
                    a.IsTwoYear = true;
                else
                    a.IsTwoYear = false;

                if (isGuide.Checked)
                    a.IsGuide = true;
                else
                    a.IsGuide = false;
                if (age_tB.Text != "")
                    a.Age = Convert.ToInt32(age_tB.Text.Trim());

                //传递给数据操作类
                dataoperation.InsertNameSheet(a);

                MessageBox.Show("保存成功！", "提示");

                //在listView中添加录入的项
                ListViewItem it = new ListViewItem("");  //根据选择正副的不同，来定

                it.Tag = a.Cid.ToString();
                it.SubItems.Add(Convert.ToString(controlMain.listView.Items.Count + 1));
                it.SubItems.Add(a.Name.ToString());
                it.SubItems.Add(a.Sex.ToString());
                it.SubItems.Add(a.Nation.ToString());
                it.SubItems.Add(a.Unitname.ToString());
                it.SubItems.Add(a.Department.ToString());
                it.SubItems.Add(a.Position.ToString());
                it.SubItems.Add(this.grade.Text);
                it.SubItems.Add(this.partyClass.Text);
                it.SubItems.Add(a.Native.ToString());
                it.SubItems.Add(a.Birthday.ToString());
                it.SubItems.Add(a.PartyTime.ToString());
                it.SubItems.Add(a.WorkTime.ToString());
                it.SubItems.Add(a.FullEducation.ToString());
                it.SubItems.Add(a.FullDegree.ToString());
                it.SubItems.Add(a.FullSchool.ToString());
                it.SubItems.Add(a.FullSpecialty.ToString());
                it.SubItems.Add(a.WorkEducation.ToString());
                it.SubItems.Add(a.WorkDegree.ToString());
                it.SubItems.Add(a.WorkGraduate.ToString());
                it.SubItems.Add(a.WorkSpecialty.ToString());
                it.SubItems.Add(a.TechnicalPost.ToString());
                it.SubItems.Add(a.SPDegree.ToString());
                it.SubItems.Add(a.ExperiencePost.ToString());
                it.SubItems.Add(a.KnowField.ToString());
                it.SubItems.Add(a.TrainDirection.ToString());
                it.SubItems.Add(a.TrainMeasure.ToString());
                //if (a.JoinTeam == true)
                //    it.SubItems.Add("是");
                //else
                //    it.SubItems.Add("否");
                if (checkBox_promote.Checked)
                    it.SubItems.Add("是");
                else
                    it.SubItems.Add("否");
                //将输入的类容
                controlMain.listView.Items.Add(it);
                controlMain.groupBox1.Text = "初步人选名册（" + controlMain.listView.Items.Count + ")";
                for (int i = 0; i < controlMain.listView.Items.Count; i++)
                {
                    controlMain.listView.Items[i].Text = (i + 1).ToString();
                }

                controlMain.listView.Items[controlMain.listView.Items.Count - 1].Selected = true;

                //录入完毕，清空表中的内容。

                this.Name_TextBox.Text = null;
                //this.Sex_ComboBox.Text = null;
                //this.Nation_TextBox.Text = null;
                this.Union_TextBox.Text = null;
                this.Position_TexBox.Text = null;
                this.HomeTown_TextBox.Text = null;
                this.BrithdayTime.Text = null;
                this.PartyTime.Text = null;
                this.JobTime.Text = null;
                PublicSelect_checkBox.Checked = false;
                isTwoYear.Checked = false;
                TYGE_checkBox.Checked = false;
                isGuide.Checked = false;
                this.FullEducation_ComboBox.SelectedIndex = -1;
                this.FullDegree_ComboBox.Text = null;
                this.FullSchool_TextBox.Text = null;
                this.FullSpeciality_TextBox.Text = null;
                this.OnEducation_ComboBox.SelectedIndex = -1;
                this.OnDegree_ComboBox.Text = null;
                this.OnGraduateSchool_TextBox.Text = null;
                this.OnSpeciality_TextBox.Text = null;
                this.SpecialityPosition_TextBox.Text = null;
                this.MainPosition_TextBox.Text = null;
                this.KnowField_TextBox.Text = null;
                this.TrainDirection_TextBox.Text = null;
                this.TrainMessure_TextBox.Text = null;
                this.grade.Text = null;
                this.SpecialityPosition_Degree.SelectedIndex = -1;
                this.partyClass.Enabled = true;
                this.identity.Text = "";
                this.FullDegree_ComboBox.Enabled = true;
                this.FullSpeciality_TextBox.Enabled = true;
                this.OnDegree_ComboBox.Enabled = true;
                this.OnSpeciality_TextBox.Enabled = true;
                DefaultSet();
            }
            catch (Exception) { }

        }

        /// <summary>
        /// 清除录入的信息的事件监听
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Clear_Button_Click(object sender, EventArgs e)
        {
            this.Name_TextBox.Text = null;

            this.Nation_TextBox.Text = null;
            this.Union_TextBox.Text = null;
            this.Position_TexBox.Text = null;
            this.HomeTown_TextBox.Text = null;
            this.BrithdayTime.Text = null;
            this.PartyTime.Text = null;
            this.SpecialityPosition_Degree.Text = null;
            this.JobTime.Text = null;

            PublicSelect_checkBox.Checked = false;
            isTwoYear.Checked = false;
            TYGE_checkBox.Checked = false;
            isGuide.Checked = false;
            this.age_tB.Text = "";
            this.FullEducation_ComboBox.SelectedIndex = -1;
            this.FullDegree_ComboBox.Text = null;
            this.FullSchool_TextBox.Text = null;
            this.FullSpeciality_TextBox.Text = null;
            this.OnEducation_ComboBox.SelectedIndex = -1;
            this.OnDegree_ComboBox.Text = null;
            this.OnGraduateSchool_TextBox.Text = null;
            this.OnSpeciality_TextBox.Text = null;
            this.SpecialityPosition_TextBox.Text = null;
            this.MainPosition_TextBox.Text = null;
            this.KnowField_TextBox.Text = null;
            this.TrainDirection_TextBox.Text = null;
            this.TrainMessure_TextBox.Text = null;
            this.grade.Text = null;
            this.partyClass.SelectedIndex = -1;
            hideDP();
            checkBox_promote.Checked = false;
            this.partyClass.Enabled = true;
            this.FullDegree_ComboBox.Enabled = true;
            this.FullSpeciality_TextBox.Enabled = true;
            this.OnDegree_ComboBox.Enabled = true;
            this.OnSpeciality_TextBox.Enabled = true;
            this.identity.Text = "";
        }

        //退出按钮事件监听
        private void Exit_Button_Click(object sender, EventArgs e)
        {
            if (remarkchange)
            {
                switch (MessageBox.Show("是否保存当前信息？", "提示", MessageBoxButtons.YesNoCancel))
                {
                    case DialogResult.Yes:
                        if (!SaveAll())
                        {
                            return;
                        }
                        else
                        {
                            remarkchange = false;
                            this.Dispose();
                        }
                        break;
                    case DialogResult.Cancel:
                        break;
                    case System.Windows.Forms.DialogResult.No:
                        remarkchange = false;
                        this.Dispose();
                        break;
                }
            }
            else
            {
                this.Dispose();
            }
        }

        /// <summary>
        /// 修改原有的一条信息
        /// </summary>
        public void Modify()
        {
            try
            {
                CommonInfo a = new CommonInfo();
                if ((this.Name_TextBox.Text != null) && (this.Name_TextBox.Text != string.Empty))
                {
                    //插入数据库
                    DataOperation dataOperation = new DataOperation();

                    //单位类别
                    a.UnitClass = this.unitClass;

                    //单位名称
                    a.Unitname = this.unitname;

                    //列入后备的时间
                    a.SystemTime = systemdate.Text;

                    //姓名
                    a.Name = this.Name_TextBox.Text;

                    ConvertPinYin convertPinYin = new ConvertPinYin();
                    a.UnitNamePinYin = convertPinYin.Hanzi2Pinyin(this.unitname) + convertPinYin.Hanzi2PY(this.unitname);
                    a.InitialFullSpelling = convertPinYin.Hanzi2Pinyin(this.Name_TextBox.Text.Trim()) + convertPinYin.Hanzi2PY(this.Name_TextBox.Text.Trim());

                    //性别
                    if (rdbMan.Checked)
                    {
                        a.Sex = "男";
                    }
                    else
                    {
                        a.Sex = "女";
                    }

                    //民族
                    a.Nation = this.Nation_TextBox.Text;

                    //籍贯
                    a.Native = this.HomeTown_TextBox.Text;

                    //出生年月
                    a.Birthday = this.BrithdayTime.Text;

                    //身份证号
                    a.Cid = this.cid;//旧的cid

                    //熟悉领域
                    a.KnowField = this.KnowField_TextBox.Text;

                    //专业技术职称级别
                    a.SPDegree = this.SpecialityPosition_Degree.Text;

                    //专业技术职称
                    a.TechnicalPost = this.SpecialityPosition_TextBox.Text;

                    //级别
                    if (this.grade.Text.Equals(""))
                        a.Grade = -1;
                    else
                    {
                        a.Grade = grade.Items.Count - grade.SelectedIndex;
                    }

                    if (PublicSelect_checkBox.Checked)
                        a.Publicselect = true;
                    else
                        a.Publicselect = false;

                    if (TYGE_checkBox.Checked)
                        a.TYGE1 = true;
                    else
                        a.TYGE1 = false;

                    if (checkBox_promote.Checked)
                        a.JoinTeam = true;
                    else
                        a.JoinTeam = false;

                    if (isTwoYear.Checked)
                        a.IsTwoYear = true;
                    else
                        a.IsTwoYear = false;

                    if (isGuide.Checked)
                        a.IsGuide = true;
                    else
                        a.IsGuide = false;

                    //党派
                    if (partyClass.Text == "")
                    {
                        a.PartyClass = this.partyClass.Text;
                    }
                    else
                        if (partyClass.SelectedItem.ToString().Trim() == "其他")
                        {
                            a.PartyClass = this.textBox1.Text.Trim();
                        }
                        else
                        {
                            a.PartyClass = this.partyClass.Text;
                        }
                    //入党时间
                    if (partyClass.SelectedIndex == -1 || !partyClass.SelectedItem.Equals("无党派"))
                        a.PartyTime = this.PartyTime.Text;
                    else
                        a.PartyTime = "";

                    //工作部门
                    a.Department = this.Union_TextBox.Text;
                    //工作单位
                    a.Unitname = this.unitname;

                    //职务
                    a.Position = this.Position_TexBox.Text;

                    //参加工作时间
                    a.WorkTime = this.JobTime.Text;

                    //培养方向
                    a.TrainDirection = this.TrainDirection_TextBox.Text;

                    //培养措施
                    a.TrainMeasure = this.TrainMessure_TextBox.Text;

                    //历任主要职务
                    a.ExperiencePost = this.MainPosition_TextBox.Text;

                    //全日制
                    a.FullEducation = this.FullEducation_ComboBox.Text;
                    a.FullDegree = this.FullDegree_ComboBox.Text;
                    a.FullSchool = this.FullSchool_TextBox.Text;
                    a.FullSpecialty = this.FullSpeciality_TextBox.Text;

                    //在职
                    a.WorkEducation = this.OnEducation_ComboBox.Text;
                    a.WorkDegree = this.OnDegree_ComboBox.Text;
                    a.WorkGraduate = this.OnGraduateSchool_TextBox.Text;
                    a.WorkSpecialty = this.OnSpeciality_TextBox.Text;

                    //传递给数据操作类
                    dataOperation.UpdateNameSheet(a, identity.Text.Trim());
                    string s = identity.Text.Trim().ToString();
                    controlMain.listView.SelectedItems[0].Tag = s;
                    controlMain.cid = s;
                    controlMain.listView.SelectedItems[0].SubItems[2].Text = a.Name.ToString();
                    controlMain.listView.SelectedItems[0].SubItems[3].Text = a.Sex.ToString();
                    controlMain.listView.SelectedItems[0].SubItems[4].Text = a.Nation.ToString();
                    controlMain.listView.SelectedItems[0].SubItems[9].Text = a.PartyClass.ToString();
                    controlMain.listView.SelectedItems[0].SubItems[11].Text = a.Birthday.ToString();
                    controlMain.listView.SelectedItems[0].SubItems[24].Text = a.ExperiencePost.ToString();
                    controlMain.listView.SelectedItems[0].SubItems[22].Text = a.TechnicalPost.ToString();
                    controlMain.listView.SelectedItems[0].SubItems[5].Text = a.Unitname.ToString();
                    controlMain.listView.SelectedItems[0].SubItems[7].Text = a.Position.ToString();
                    controlMain.listView.SelectedItems[0].SubItems[6].Text = a.Department.ToString();
                    controlMain.listView.SelectedItems[0].SubItems[8].Text = this.grade.Text;
                    controlMain.listView.SelectedItems[0].SubItems[10].Text = a.Native.ToString();
                    controlMain.listView.SelectedItems[0].SubItems[12].Text = a.PartyTime.ToString();
                    controlMain.listView.SelectedItems[0].SubItems[13].Text = a.WorkTime.ToString();
                    controlMain.listView.SelectedItems[0].SubItems[14].Text = a.FullEducation.ToString();
                    controlMain.listView.SelectedItems[0].SubItems[15].Text = a.FullDegree.ToString();
                    controlMain.listView.SelectedItems[0].SubItems[16].Text = a.FullSchool.ToString();
                    controlMain.listView.SelectedItems[0].SubItems[17].Text = a.FullSpecialty.ToString();
                    controlMain.listView.SelectedItems[0].SubItems[18].Text = a.WorkEducation.ToString();
                    controlMain.listView.SelectedItems[0].SubItems[19].Text = a.WorkDegree.ToString();
                    controlMain.listView.SelectedItems[0].SubItems[20].Text = a.WorkGraduate.ToString();
                    controlMain.listView.SelectedItems[0].SubItems[21].Text = a.WorkSpecialty.ToString();
                    controlMain.listView.SelectedItems[0].SubItems[23].Text = a.SPDegree.ToString();
                    controlMain.listView.SelectedItems[0].SubItems[25].Text = a.KnowField.ToString();
                    controlMain.listView.SelectedItems[0].SubItems[26].Text = a.TrainDirection.ToString();
                    controlMain.listView.SelectedItems[0].SubItems[27].Text = a.TrainMeasure.ToString();
                    if (a.JoinTeam == true)
                        controlMain.listView.SelectedItems[0].SubItems[28].Text = "是";
                    else
                        controlMain.listView.SelectedItems[0].SubItems[28].Text = "否";
                    modified = true;
                }
                else
                {
                    MessageBox.Show("姓名不能为空!", "提示");
                }
            }
            catch (Exception) { }
        }

        /// <summary>
        /// 培训措施按钮的点击事件监听
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                FrmMultiChoise m = new FrmMultiChoise();
                m.ShowDialog();
                TrainMessure_TextBox.Text = TrainMessure_TextBox.Text + m.choices;
            }
            catch (Exception) { }
        }

        /// <summary>
        /// 入党时间  当失去焦点时候对日期的检查
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void PartyTime_MouseLeave(object sender, EventArgs e)
        {
            string monthD = string.Empty;
            string dayD = string.Empty;
            string monthN = string.Empty;
            string dayN = string.Empty;
            monthD = PartyTime.Value.Month.ToString();
            dayD = PartyTime.Value.Day.ToString();
            monthN = DateTime.Now.Month.ToString();
            dayN = DateTime.Now.Day.ToString();

            if (Convert.ToInt32(PartyTime.Value.Month.ToString()) < 10)
            {
                monthD = "0" + PartyTime.Value.Month.ToString();
            }
            if (Convert.ToInt32(PartyTime.Value.Day.ToString()) < 10)
            {
                dayD = "0" + PartyTime.Value.Day.ToString();
            }
            if (Convert.ToInt32(DateTime.Now.Month.ToString()) < 10)
            {
                monthN = "0" + DateTime.Now.Month.ToString();
            }
            if (Convert.ToInt32(DateTime.Now.Day.ToString()) < 10)
            {
                dayN = "0" + DateTime.Now.Day.ToString();
            }
            if (Convert.ToInt32(PartyTime.Value.Year.ToString() + monthD + dayD) > Convert.ToInt32(DateTime.Now.Year.ToString() + monthN + dayN))
            {
                MessageBox.Show("不能大于当前时间！", "提示");
                PartyTime.Value = myTime;
            }
        }

        /// <summary>
        ///  参加工作 当失去焦点的时候对日期的检查
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void JobTime_MouseLeave(object sender, EventArgs e)
        {
            string monthD = string.Empty;
            string dayD = string.Empty;
            string monthN = string.Empty;
            string dayN = string.Empty;
            monthD = JobTime.Value.Month.ToString();
            dayD = JobTime.Value.Day.ToString();
            monthN = DateTime.Now.Month.ToString();
            dayN = DateTime.Now.Day.ToString();

            if (Convert.ToInt32(JobTime.Value.Month.ToString()) < 10)
            {
                monthD = "0" + JobTime.Value.Month.ToString();
            }
            if (Convert.ToInt32(JobTime.Value.Day.ToString()) < 10)
            {
                dayD = "0" + JobTime.Value.Day.ToString();
            }
            if (Convert.ToInt32(DateTime.Now.Month.ToString()) < 10)
            {
                monthN = "0" + DateTime.Now.Month.ToString();
            }
            if (Convert.ToInt32(DateTime.Now.Day.ToString()) < 10)
            {
                dayN = "0" + DateTime.Now.Day.ToString();
            }


            if (Convert.ToInt32(JobTime.Value.Year.ToString() + monthD + dayD) > Convert.ToInt32(DateTime.Now.Year.ToString() + monthN + dayN))
            {
                MessageBox.Show("不能大于当前时间！", "提示");
                JobTime.Value = myTime;
            }
        }

        /// <summary>
        /// 如果是群众 入党时间不让填
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void partyClass_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (partyClass.SelectedIndex > -1)
            {
                if (partyClass.SelectedItem.Equals("无党派"))
                {
                    PartyTime.Enabled = false;
                }
                else
                {
                    PartyTime.Enabled = true;
                }
                if (partyClass.SelectedItem.Equals("其他"))
                {
                    showDP();
                }
                else
                {
                    hideDP();
                }
            }
            if (change)
            {
                remarkchange = true;
            }
        }
        //显示手动录入党派的文本框
        private void showDP()
        {
            label15.Visible = true;
            textBox1.Visible = true;
            textBox1.Text = "";
        }
        //隐藏手动录入党派文本框
        private void hideDP()
        {
            label15.Visible = false;
            textBox1.Visible = false;
            textBox1.Text = "";
        }
        /// <summary>
        /// 身份证号框失去焦点时对身份证的检查
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void identity_Leave(object sender, EventArgs e)
        {
            CheckID();
        }

        /// <summary>
        /// 检查身份证号
        /// </summary>
        /// <returns></returns>
        private bool CheckID()
        {
            try
            {
                if (identity.Text == "")
                {
                    MessageBox.Show("身份证号不能为空！", "提示");
                    return false;

                }
                if (!(identity.Text.Trim().Length == 18 || identity.Text.Trim().Length == 15))
                {
                    MessageBox.Show("身份证号位数不对！", "提示");
                    return false;

                }
                else
                {
                    if (cid == string.Empty || cid != identity.Text)
                    {
                        DataOperation oper = new DataOperation();
                        if (oper.IsExitCid(identity.Text.Trim().ToString()) == true)
                        {
                            MessageBox.Show("已存在此身份证号！", "提示");
                            identity.Text = "";
                            return false;
                        }
                    }
                }
                return true;
            }
            catch
            {
                MessageBox.Show("身份证号输入有误！", "提示");
                return false;
            }
        }

        /// <summary>
        /// 专业职称级别的选项发生变化时的事件监听
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SpecialityPosition_TextBox_TextChanged(object sender, EventArgs e)
        {
            if (SpecialityPosition_TextBox.Text == "")
            {
                SpecialityPosition_Degree.SelectedIndex = -1;
                SpecialityPosition_Degree.Enabled = false;
            }
            else
            {
                SpecialityPosition_Degree.Enabled = true;
            }

            if (change)
            {
                remarkchange = true;
            }
        }

        /// <summary>
        /// 检查出生日期的合法性
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BrithdayTime_ValueChanged(object sender, EventArgs e)
        {
            int temp = Convert.ToInt32(BrithdayTime.Value.ToString("yyyyMM"));
            int now = Convert.ToInt32(DateTime.Now.ToString("yyyyMM"));
            if (temp > now)
            {
                MessageBox.Show("出生年月应小于当前时间!", "提示");
                BrithdayTime.Value = DateTime.Now;
                return;
            }
            else
            {
                if (temp != now)
                {
                    int temp1 = Convert.ToInt32(BrithdayTime.Value.ToString("MM"));
                    int now1 = Convert.ToInt32(DateTime.Now.ToString("MM"));
                    int temp2 = Convert.ToInt32(BrithdayTime.Value.ToString("yyyy"));
                    int now2 = Convert.ToInt32(DateTime.Now.ToString("yyyy"));
                    if (temp1 <= now1)
                    {
                        age_tB.Text = (now2 - temp2).ToString();
                    }
                    else
                    {
                        age_tB.Text = (now2 - temp2 - 1).ToString();
                    }
                }
            }
        }

        /// <summary>
        /// 限制列入后备的年份
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void systemdate_ValueChanged(object sender, EventArgs e)
        {
            int time = Convert.ToInt32(DateTime.Now.ToString("yyyy"));

            int time1 = Convert.ToInt32(systemdate.Text);

            if (time1 > time)
            {
                MessageBox.Show("列入后备年份应小于或等于当前年份！", "提示");
                systemdate.Text = DateTime.Now.ToString("yyyy") + "年";
            }

            if (change)
            {
                remarkchange = true;
            }
        }

        /// <summary>
        /// 文本框中信息改变事件的处理，标记信息改变
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Name_TextBox_TextChanged(object sender, EventArgs e)
        {
            if (change)
            {
                remarkchange = true;
            }
        }

        /// <summary>
        /// 文本框中信息改变事件的处理，标记信息改变
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Nation_TextBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (change)
            {
                remarkchange = true;
            }
        }

        /// <summary>
        /// 文本框中信息改变事件的处理，标记信息改变
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void rdbMan_CheckedChanged(object sender, EventArgs e)
        {
            if (change)
            {
                remarkchange = true;
            }
        }

        /// <summary>
        /// 文本框中信息改变事件的处理，标记信息改变
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void HomeTown_TextBox_TextChanged(object sender, EventArgs e)
        {
            if (change)
            {
                remarkchange = true;
            }
        }

        /// <summary>
        /// 文本框中信息改变事件的处理，标记信息改变
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void identity_TextChanged(object sender, EventArgs e)
        {
            if (change)
            {
                remarkchange = true;
            }
        }

        /// <summary>
        /// 文本框中信息改变事件的处理，标记信息改变
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void KnowField_TextBox_TextChanged(object sender, EventArgs e)
        {
            if (change)
            {
                remarkchange = true;
            }
        }

        /// <summary>
        /// 文本框中信息改变事件的处理，标记信息改变
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SpecialityPosition_Degree_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (change)
            {
                remarkchange = true;
            }
        }

        /// <summary>
        /// 文本框中信息改变事件的处理，标记信息改变
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MainPosition_TextBox_TextChanged(object sender, EventArgs e)
        {
            if (change)
            {
                remarkchange = true;
            }
        }

        /// <summary>
        /// 下拉列表中的选中项改变事件的处理。
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void grade_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (change)
            {
                remarkchange = true;
            }
        }

        /// <summary>
        /// 事件限制
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void PartyTime_ValueChanged(object sender, EventArgs e)
        {
            int temp = Convert.ToInt32(PartyTime.Value.ToString("yyyyMM"));
            int now = Convert.ToInt32(DateTime.Now.ToString("yyyyMM"));
            if (temp > now)
            {
                MessageBox.Show("入党时间应小于当前时间", "提示");
                BrithdayTime.Value = DateTime.Now;
            }
            if (change)
            {
                remarkchange = true;
            }
        }

        /// <summary>
        /// 文本框中信息改变事件的处理，标记信息改变
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Union_TextBox_TextChanged(object sender, EventArgs e)
        {
            if (change)
            {
                remarkchange = true;
            }
        }

        /// <summary>
        /// 文本框中信息改变事件的处理，标记信息改变
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Position_TexBox_TextChanged(object sender, EventArgs e)
        {
            if (change)
            {
                remarkchange = true;
            }
        }

        /// <summary>
        /// 工作时间限制
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void JobTime_ValueChanged(object sender, EventArgs e)
        {
            int temp = Convert.ToInt32(PartyTime.Value.ToString("yyyyMM"));
            int now = Convert.ToInt32(DateTime.Now.ToString("yyyyMM"));
            if (temp > now)
            {
                MessageBox.Show("参加工作时间应小于当前时间", "提示");
                PartyTime.Value = DateTime.Now;
            }
            if (change)
            {
                remarkchange = true;
            }
        }

        /// <summary>
        /// 文本框中信息改变事件的处理，标记信息改变
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TrainDirection_TextBox_TextChanged(object sender, EventArgs e)
        {
            if (change)
            {
                remarkchange = true;
            }
        }

        /// <summary>
        /// 文本框中信息改变事件的处理，标记信息改变
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TrainMessure_TextBox_TextChanged(object sender, EventArgs e)
        {
            if (change)
            {
                remarkchange = true;
            }
        }

        /// <summary>
        /// 文本框中信息改变事件的处理，标记信息改变
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void PublicSelect_checkBox_CheckedChanged(object sender, EventArgs e)
        {
            if (change)
            {
                remarkchange = true;
            }
        }

        /// <summary>
        /// 选中状态改变标记
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void checkBox_promote_CheckedChanged(object sender, EventArgs e)
        {
            if (change)
            {
                remarkchange = true;
            }
        }

        /// <summary>
        /// 选中状态改变标记
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void isTwoYear_CheckedChanged(object sender, EventArgs e)
        {
            if (change)
            {
                remarkchange = true;
            }
        }

        /// <summary>
        /// 选中状态改变标记
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TYGE_checkBox_CheckedChanged(object sender, EventArgs e)
        {
            if (change)
            {
                remarkchange = true;
            }
        }

        /// <summary>
        /// 选中状态改变标记
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void isGuide_CheckedChanged(object sender, EventArgs e)
        {
            if (change)
            {
                remarkchange = true;
            }
        }

        /// <summary>
        /// 全日制学历下拉列表中的选中项改变事件的处理，标记修改
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FullEducation_ComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (change)
            {
                remarkchange = true;
            }
        }

        /// <summary>
        /// 全日制学位下拉列表中的选中项改变事件的处理，标记修改
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FullDegree_ComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (change)
            {
                remarkchange = true;
            }
        }

        /// <summary>
        /// 文本框中信息改变事件的处理，标记信息改变
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FullSpeciality_TextBox_TextChanged(object sender, EventArgs e)
        {
            if (change)
            {
                remarkchange = true;
            }
        }

        /// <summary>
        /// 文本框中信息改变事件的处理，标记信息改变
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FullSchool_TextBox_TextChanged(object sender, EventArgs e)
        {
            if (change)
            {
                remarkchange = true;
            }
        }

        /// <summary>
        /// 下拉列表中的选中项改变事件的处理。
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnEducation_ComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (change)
            {
                remarkchange = true;
            }
        }

        /// <summary>
        /// 在职学位下拉列表中的选中项改变事件的处理，标记修改
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnDegree_ComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (change)
            {
                remarkchange = true;
            }
        }

        /// <summary>
        /// 文本框中信息改变事件的处理，标记信息改变
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnSpeciality_TextBox_TextChanged(object sender, EventArgs e)
        {
            if (change)
            {
                remarkchange = true;
            }
        }

        /// <summary>
        /// 文本框中信息改变事件的处理，标记信息改变
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnGraduateSchool_TextBox_TextChanged(object sender, EventArgs e)
        {
            if (change)
            {
                remarkchange = true;
            }
        }

        /// <summary>
        /// 身份证号码限制
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void identity_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (identity.Text.ToString().Length == 18 && e.KeyChar != 8)
            {
                MessageBox.Show("身份证号码应为18位！", "提示");
                e.Handled = true;
            }
            else
            {
                if (identity.Text.ToString().Length == 17)
                {
                    if ((e.KeyChar < 48 || e.KeyChar > 57) && (e.KeyChar != 8) && e.KeyChar != 'X' && e.KeyChar != 'x')
                    {
                        MessageBox.Show("身份证号码有误，请重新输入！", "提示");
                        e.Handled = true;
                    }
                }
                else
                {
                    //只能输入数字
                    if ((e.KeyChar < 48 || e.KeyChar > 57) && (e.KeyChar != 8))
                    {
                        MessageBox.Show("身份证号码有误，请重新输入！", "提示");
                        e.Handled = true;
                    }
                }
            }
        }

        /// <summary>
        /// 窗体关闭时事件的处理，当有信息改变时，提示是否保存。
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FrmNameSheet_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (remarkchange)
            {
                switch (MessageBox.Show("是否保存当前信息？", "提示", MessageBoxButtons.YesNoCancel))
                {
                    case DialogResult.Yes:
                        if (!SaveAll())
                        {
                            e.Cancel = true;
                            return;
                        }
                        break;
                    case DialogResult.Cancel:
                        e.Cancel = true;
                        break;
                }
            }
        }

        /// <summary>
        /// 保存成功返回true;
        /// </summary>
        /// <returns></returns>
        public bool SaveAll()
        {

            if ((this.Name_TextBox.Text != null) && (this.Name_TextBox.Text != string.Empty))
            {
                if (Union_TextBox.Text == string.Empty)
                {
                    MessageBox.Show("所在工作部门不能为空！", "提示");
                    return false;
                }
                if (Position_TexBox.Text == string.Empty)
                {
                    MessageBox.Show("职务不能为空！", "提示");
                    return false;
                }
                if (!rdbMan.Checked && !rdbWoman.Checked)
                {
                    MessageBox.Show("请选择性别！", "提示");
                    return false;
                }
                if (this.BrithdayTime.Text.Trim().Equals(DateTime.Now.ToString("yyyy'年'MM'月'")))
                {
                    MessageBox.Show("请输入出生年月！", "提示");
                    return false;
                }

                if (grade.Text != null && grade.Text.Trim() != "")
                {
                    string sql = "select name from TB_CommonInfo where cid='" + identity.Text.Trim() + "'";
                    if (da.GetOneDataTable_sql(sql).Rows.Count > 0)
                    {
                        isModify = true;
                    }
                    else
                    {
                        isModify = false;
                    }
                    if (isModify)
                    {
                        if (CheckID())
                        {
                            if ((SpecialityPosition_Degree.Text == "" && SpecialityPosition_TextBox.Text == "") || (SpecialityPosition_Degree.Text != "" && SpecialityPosition_TextBox.Text != ""))
                            {
                                Modify();
                                MessageBox.Show("保存成功！", "提示");
                                return true;
                            }
                            else
                            {
                                MessageBox.Show("请把专业技术职称和专业技术职务录入完整！", "提示");
                            }
                        }
                    }
                    else
                    {
                        if (CheckID())
                        {
                            if ((SpecialityPosition_Degree.Text == "" && SpecialityPosition_TextBox.Text == "") || (SpecialityPosition_Degree.Text != "" && SpecialityPosition_TextBox.Text != ""))
                            {
                                Save();
                                return true;
                            }
                            else
                            {
                                MessageBox.Show("专业技术职称和专业技术职务有一个为空！", "提示");
                            }
                        }
                        else
                        {
                            return false;
                        }
                    }
                }
                else
                {
                    MessageBox.Show("级别不能为空！", "提示");
                }
            }
            else
            {
                MessageBox.Show("姓名不能为空！", "提示");
            }
            return false;
        }

        private void identity_KeyPress_1(object sender, KeyPressEventArgs e)
        {
            if (identity.Text.ToString().Length == 18 && e.KeyChar != 8)
            {
                MessageBox.Show("身份证号码应为18位！", "提示");
                e.Handled = true;
            }
            else
            {
                if (identity.Text.ToString().Length == 17)
                {
                    if ((e.KeyChar < 48 || e.KeyChar > 57) && (e.KeyChar != 8) && e.KeyChar != 'X' && e.KeyChar != 'x')
                    {
                        MessageBox.Show("身份证号码有误，请重新输入！", "提示");
                        e.Handled = true;
                    }
                }
                else
                {
                    //只能输入数字
                    if ((e.KeyChar < 48 || e.KeyChar > 57) && (e.KeyChar != 8))
                    {
                        MessageBox.Show("身份证号码有误，请重新输入！", "提示");
                        e.Handled = true;
                    }
                }
            }
        }
    }
}