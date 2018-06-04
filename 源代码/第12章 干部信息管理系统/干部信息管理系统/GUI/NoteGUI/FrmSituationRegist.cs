using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Collections;
using HBMISR.GUI.MainGUI;
using HBMISR.Data;
using HBMISR.Service;
using HBMISR.server;

namespace HBMISR.GUI.NoteGUI
{
    /// <summary>
    /// 简要情况登记表
    /// </summary>
    public partial class FrmSituationRegist : Form
    {
        #region//定义变量
        ReadIni readini;
        Insert_Serve serve;
        TextBox textBox_In_ListBox = new TextBox();

        /// <summary>
        /// 用于存储照片的二进制信息
        /// </summary>
        private byte[] image = null;

        private int selectionIdx = 0;

        DataGridView dataGridView1;
        bool selectedPath = false;
        ArrayList delList = new ArrayList();

        #endregion

        /// <summary>
        /// 加载时change为false，加载完成后置为true。
        /// </summary>
        bool change = false;

        /// <summary>
        /// 标记修改，修改为true，
        /// </summary>
        bool remarkchange = false;

        DataOperation da = null;

        /// <summary>
        /// 记录ControlMain类型的实例
        /// </summary>
        public ControlMain ci;

        /// <summary>
        /// 保存被状态的信息
        /// </summary>
        public string listViewCid = "";

        /// <summary>
        /// 标记当前这一条个人简历/家庭成员信息是新增的还是修改的
        /// </summary>
        public bool isEditOfR = false, isEditOfF = false, isEditOfA = false;
        /// <summary>
        /// 标记当前修改的个人简历的ID/家庭成员
        /// </summary>
        public string rid = "", fid = "", aid = "";
        public bool isSingle = false;
        DateTime myTime = Convert.ToDateTime(DateTime.Now.Year + "." + DateTime.Now.Month + "." + DateTime.Now.Day);

        /// <summary>
        /// 宣布取消资格的人放入其中，等窗口关闭时对这些人进行删除处理
        /// </summary>
        public List<ListViewDel> list = new List<ListViewDel>();

        /// <summary>
        /// 简要情况登记表构造函数
        /// </summary>
        public FrmSituationRegist()
        {
            InitializeComponent();
            readini = new ReadIni();
            string filepath = readini.ReadString("filePath");
        }

        /// <summary>
        /// 简要情况登记表初始化
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void PeopleNameSheet_Load(object sender, EventArgs e)
        {
            dateTimePicker_StartTime.Text = DateTime.Now.ToString("yyyy'年'MM'月'") + "01日";
            dateTimePicker_EndTime.Text = DateTime.Now.ToString("yyyy'年'MM'月'") + "01日";
            fBirthday.Text = DateTime.Now.ToString("yyyy'年'MM'月'") + "01日";
            aAwardTime.Text = DateTime.Now.ToString("yyyy'年'MM'月'") + "01日";

            da = new DataOperation();
            serve = new Insert_Serve();

            //年份设置
            #region
            comboBox_first_year.Items.Add((DateTime.Now.Year - 4).ToString());
            comboBox_first_year.Items.Add((DateTime.Now.Year - 3).ToString());
            comboBox_first_year.Items.Add((DateTime.Now.Year - 2).ToString());
            #endregion

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

            //家庭成员关系表头信息的设定
            listView_Relation.View = View.Details;
            listView_Relation.LabelEdit = true;
            listView_Relation.GridLines = true;
            listView_Relation.AllowColumnReorder = true;
            listView_Relation.LabelEdit = false;
            listView_Relation.HideSelection = false;
            listView_Relation.Columns.Add("", 0, HorizontalAlignment.Center);
            listView_Relation.Columns.Add("称谓", 60, HorizontalAlignment.Center);
            listView_Relation.Columns.Add("姓名", 100, HorizontalAlignment.Center);
            listView_Relation.Columns.Add("出生年月", 100, HorizontalAlignment.Center);
            listView_Relation.Columns.Add("年龄", 60, HorizontalAlignment.Center);
            listView_Relation.Columns.Add("国籍", 100, HorizontalAlignment.Center);
            listView_Relation.Columns.Add("党派", 100, HorizontalAlignment.Center);
            listView_Relation.Columns.Add("民族", 80, HorizontalAlignment.Center);
            listView_Relation.Columns.Add("工作单位及职务", 236, HorizontalAlignment.Center);
            listView_Relation.Columns.Add("备注", 100, HorizontalAlignment.Center);

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

            LoadInfo(listViewCid);
            readShowTable(listViewCid);

            change = true;
        }

        /// <summary>
        /// 读出id为cid的人员的信息
        /// </summary>
        /// <param name="cid">后备干部id号</param>
        public void LoadInfo(string cid)
        {
            try
            {
                string sql0 = "select name, department, position from TB_Commoninfo where cid = '" + cid + "'";
                DataTable nameTable = da.GetOneDataTable_sql(sql0);
                comboBox_name.Text = nameTable.Rows[0]["name"].ToString();
                department_tB.Text = nameTable.Rows[0]["department"].ToString();
                positon_tB.Text = nameTable.Rows[0]["position"].ToString();

                string sql4 = "select age, birthday,birthplace,specialtySkill,health,remark,photo ,startTime,result1,result2,result3,state from TB_CommonInfo where cid='" + cid + "'";
                DataTable briefInfo = da.GetOneDataTable_sql(sql4);
                if (briefInfo.Rows.Count > 0)
                {
                    this.textBox_birthday.Text = briefInfo.Rows[0]["birthday"].ToString();
                    this.textBox_BirthPlace.Text = briefInfo.Rows[0]["birthplace"].ToString();
                    this.textBox_speciality.Text = briefInfo.Rows[0]["specialtySkill"].ToString();
                    this.textBox_remark.Text = briefInfo.Rows[0]["remark"].ToString();
                    if (briefInfo.Rows[0]["age"].ToString().Equals("0"))
                        this.textBox_age.Text = string.Empty;
                    else
                        this.textBox_age.Text = briefInfo.Rows[0]["age"].ToString();
                    this.comboBox_healthy_state.Text = briefInfo.Rows[0]["health"].ToString();

                    if ((briefInfo.Rows[0]["startTime"] != DBNull.Value) && (briefInfo.Rows[0]["startTime"].ToString() != string.Empty))
                    {
                        Int32 intTime = Convert.ToInt32(briefInfo.Rows[0]["startTime"].ToString().Trim());
                        string beYear = intTime.ToString();
                        this.comboBox_first_year.Text = beYear;
                        this.textBox_second_year.Text = (++intTime).ToString();
                        this.textBox_third_year.Text = (++intTime).ToString();
                    }


                    if (briefInfo.Rows[0]["photo"] != DBNull.Value)
                    {
                        image = (byte[])briefInfo.Rows[0]["photo"];
                        this.PhotopictureBox.Image = Image.FromStream(new MemoryStream(image, false));
                    }

                    this.result1.Text = briefInfo.Rows[0]["result1"].ToString();
                    this.result2.Text = briefInfo.Rows[0]["result2"].ToString();
                    this.result3.Text = briefInfo.Rows[0]["result3"].ToString();

                    this.textBox_remark.Text = briefInfo.Rows[0]["remark"].ToString();

                }
            }
            catch (Exception) { }
        }

        /// <summary>
        /// 清空窗口
        /// </summary>
        public void ClearWindow()
        {
            //清空界面
            string sqlClear1 = "select * from TB_Family where id='-1'";
            DataTable dataTable1 = da.GetOneDataTable_sql(sqlClear1);

            string sqlClear2 = "select * from TB_PunishAward where id='-1'";
            DataTable dataTable2 = da.GetOneDataTable_sql(sqlClear2);

            string sqlClear3 = "select * from TB_Resume where id='-1'";
            DataTable dataTable3 = da.GetOneDataTable_sql(sqlClear3);

            //清空窗口中的人员信息
            textBox_birthday.Text = "";
            textBox_age.Text = "";
            textBox_BirthPlace.Text = "";
            textBox_speciality.Text = "";
            comboBox_first_year.Text = "";
            result1.SelectedIndex = -1;
            result2.SelectedIndex = -1;
            result3.SelectedIndex = -1;
            textBox_second_year.Text = "";
            textBox_third_year.Text = "";
            textBox_remark.Text = "";
            textBox_birthday.Text = "";
            comboBox_healthy_state.Text = "";
            comboBox_healthy_state.SelectedIndex = -1;
            //清空图片
            this.PhotopictureBox.Image = null;
        }

        /// <summary>
        /// 保存按钮的点击事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button_sure_Click(object sender, EventArgs e)
        {
            SaveCurrent(listViewCid);
            remarkchange = false;
        }

        /// <summary>
        /// 保存方法
        /// </summary>
        /// <param name="cid"></param>
public void SaveCurrent(string cid)//保存当前人员的全部信息
{
    try
    {
        CommonInfo CommonInfo = new CommonInfo();
        string photo = "select * from TB_CommonInfo  where CID='" + cid + "'";
        DataTable tablePhoto = da.GetOneDataTable_sql(photo);
        if (tablePhoto.Rows[0]["photo"] == DBNull.Value || selectedPath == true)
        {
            if (this.PhotopictureBox.ImageLocation == null)//选择的照片不为空
            {
                CommonInfo.Photo = null;
            }
            else
            {
                CommonInfo.Photo = File.ReadAllBytes(this.PhotopictureBox.ImageLocation);//把照片以字节流传入
            }
        }
        else
        {
            CommonInfo.Photo = (byte[])tablePhoto.Rows[0]["photo"];
        }

        CommonInfo.Result1 = result1.Text;
        CommonInfo.Result2 = result2.Text;
        CommonInfo.Result3 = result3.Text;

        if (!textBox_age.Text.Equals(""))
        {
            CommonInfo.Age = Convert.ToInt32(textBox_age.Text);
        }
        else
        {
            CommonInfo.Age = 0;
        }
        CommonInfo.Birthplace = textBox_BirthPlace.Text;
        CommonInfo.SpecialtySkill = textBox_speciality.Text;
        CommonInfo.Health = comboBox_healthy_state.Text;
        CommonInfo.StartYear = comboBox_first_year.Text;
        CommonInfo.Remark = textBox_remark.Text;
        CommonInfo.Cid = cid;
        serve.Insert(CommonInfo);

        //插入个人简历
        for (int i = 0; i < listView_Vital.Items.Count; i++)
        {
            if (listView_Vital.Items[i].Tag.ToString().Equals(""))
            {
                ResumeProperty resume = new ResumeProperty();
                resume.Cid = listViewCid;
                resume.Betime = listView_Vital.Items[i].SubItems[1].Text.ToString();
                resume.Entime = listView_Vital.Items[i].SubItems[2].Text.ToString();
                resume.Content = listView_Vital.Items[i].SubItems[3].Text.ToString();
                listView_Vital.Items[i].Tag = da.InsertResume(resume).ToString();
            }
        }

        //插入家庭成员关系
        for (int i = 0; i < listView_Relation.Items.Count; i++)
        {
            if (listView_Relation.Items[i].Tag.ToString().Equals(""))
            {
                ClassFamily family = new ClassFamily();
                family.Cid = listViewCid;
                family.Call = listView_Relation.Items[i].SubItems[1].Text.ToString();
                family.Name = listView_Relation.Items[i].SubItems[2].Text.ToString();
                family.Birthday = listView_Relation.Items[i].SubItems[3].Text.ToString();
                if (listView_Relation.Items[i].SubItems[4].Text.ToString() != "")
                    family.Age = Convert.ToInt16(listView_Relation.Items[i].SubItems[4].Text.ToString());
                else
                    family.Age = 0;
                family.Country = listView_Relation.Items[i].SubItems[5].Text.ToString();
                family.PartyClass = listView_Relation.Items[i].SubItems[6].Text.ToString();
                family.Nation = listView_Relation.Items[i].SubItems[7].Text.ToString();
                family.WorkPostion = listView_Relation.Items[i].SubItems[8].Text.ToString();
                family.Remark = listView_Relation.Items[i].SubItems[9].Text.ToString();
                listView_Relation.Items[i].Tag = da.InsertFamily(family).ToString();
            }
        }

        //插入奖惩情况
        for (int i = 0; i < listView_Award.Items.Count; i++)
        {
            if (listView_Award.Items[i].Tag.ToString().Equals(""))
            {
                PunishAward punishAward = new PunishAward();
                punishAward.Cid = listViewCid;
                punishAward.AwardClass = listView_Award.Items[i].SubItems[1].Text.ToString();
                punishAward.Degree = listView_Award.Items[i].SubItems[2].Text.ToString();
                punishAward.Time = listView_Award.Items[i].SubItems[3].Text.ToString();
                punishAward.Content = listView_Award.Items[i].SubItems[4].Text.ToString();
                listView_Award.Items[i].Tag = da.InsertAward(punishAward).ToString();
            }
        }
        MessageBox.Show("保存成功！", "提示");
    }
    catch (Exception)
    {

    }
}


        /// <summary>
        /// 限制输入只能为数字
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void JudgeType(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar != 8 && !Char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        #region//处理拖拽操作的事件

        /// <summary>
        /// 符合拖拽的条件时出发拖拽操作
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dataGridView1_CellMouseMove(object sender, DataGridViewCellMouseEventArgs e)
        {
            try
            {


                dataGridView1 = (DataGridView)sender;

                dataGridView1.Rows[selectionIdx].Selected = true;
                if ((e.Clicks < 2) && (e.Button == MouseButtons.Left))
                {

                    if ((e.ColumnIndex == -1) && (e.RowIndex > -1))
                        dataGridView1.DoDragDrop(dataGridView1.Rows[e.RowIndex], DragDropEffects.Move);//调用拖拽函数，并把相应参数传进去

                }
            }
            catch (Exception) { }


        }

        /// <summary>
        /// 拖放完成时发生
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dataGridView1_DragDrop(object sender, DragEventArgs e)
        {
            try
            {
                dataGridView1 = (DataGridView)sender;
                int idx = GetRowFromPoint(dataGridView1, e.X, e.Y);
                if (idx < 0) return;

                if (e.Data.GetDataPresent(typeof(DataGridViewRow)))
                {
                    DataGridViewRow row = (DataGridViewRow)e.Data.GetData(typeof(DataGridViewRow));//这行数据是从dataGridView1_CellMouseMove事件传过来的
                    DataRow nr = ((DataTable)dataGridView1.DataSource).NewRow();//用dataGridView中的DataTable实例化一个DataRow对象
                    for (int i = 0; i < row.Cells.Count; i++)
                    {
                        nr[i] = row.Cells[i].Value;//把DataGridViewRow对象中的数据拷贝到DataRow对象中
                    }
                    dataGridView1.Rows.Remove(row);//删除要拖拽的那一行
                    ((DataTable)dataGridView1.DataSource).Rows.InsertAt(nr, idx);//InsertAt()第一个参数只能是DataRow的对象，而非DataGridViewRow的对象(nr是我们实例化的DataRow对象)
                    selectionIdx = idx;
                    dataGridView1.Rows[selectionIdx].Selected = true;
                    for (int j = 0; j < dataGridView1.Rows.Count; j++)
                    {
                        dataGridView1.Rows[j].Cells["id"].Value = j + 1;
                    }

                }
            }
            catch (Exception) { }
        }
        /// <summary>
        /// 判断是不是在窗体内部
        /// </summary>
        /// <param name="dataGridView1">窗体</param>
        /// <param name="x">x坐标</param>
        /// <param name="y">y坐标</param>
        /// <returns>0或-1</returns>
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

        /// <summary>
        /// 选取鼠标所在的行号并赋值给selectionIdx
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dataGridView1_CellMouseDown(object sender, DataGridViewCellMouseEventArgs e)
        {
            try
            {
                if (e.RowIndex >= 0)
                    selectionIdx = e.RowIndex;//选取鼠标所在的行号并赋值给selectionIdx
            }
            catch (Exception) { }
        }
        /// <summary>
        /// 鼠标选择变化
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            try
            {
                dataGridView1 = (DataGridView)sender;
                if ((dataGridView1.Rows.Count > 0) && (dataGridView1.SelectedRows.Count > 0))
                {

                    if (dataGridView1.Rows.Count <= selectionIdx)
                        selectionIdx = dataGridView1.Rows.Count - 1;
                    //dataGridView1.Rows[selectionIdx].Selected = true;
                    //dataGridView1.CurrentCell = dataGridView1.Rows[selectionIdx].Cells[2];
                }
            }
            catch (Exception) { }

        }

        /// <summary>
        /// 鼠标拖动到某处时的效果
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dataGridView1_DragEnter(object sender, DragEventArgs e)
        {
            try
            {
                e.Effect = DragDropEffects.Move;
            }
            catch (Exception) { }
        }
        #endregion

        /// <summary>
        /// 选择图片
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button_image_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog ofd = new OpenFileDialog())
            {
                ofd.Title = "请选择要插入的图片";
                //ofd.Filter = "Gif图片(建议尺寸119×133)|*.gif(建议尺寸119×133)|Jpg图片(建议尺寸119×133)|*.jpg|BMP图片(建议尺寸119×133)|*.bmp(建议尺寸119×133)";
                ofd.Filter = "Jpg图片(建议尺寸119×133)|*.jpg";
                ofd.CheckFileExists = true;
                ofd.Multiselect = false;
                if (ofd.ShowDialog(this) == DialogResult.OK)
                {
                    this.PhotopictureBox.ImageLocation = ofd.FileName;
                    selectedPath = true;
                }
                else
                {
                    if (this.PhotopictureBox.Image == null)
                        MessageBox.Show("您还没有选择图片", "提示");
                }
            }

            if (change)
            {
                remarkchange = true;
            }
        }

        /// <summary>
        /// 取消
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button_cancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        /// <summary>
        /// 家庭成员及重要社会关系确定
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void fSure_Click(object sender, EventArgs e)
        {
            string call = "";
            string name = "";
            string birthday = "";
            string age = "";
            string party = this.fParty.Text;
            string nation = "";
            string country = "";
            string workplace = "";
            string remark = this.fRemark.Text;
            //修改
            if (!this.fage.Text.Equals(""))
            {
                try
                {
                    if (Convert.ToInt32(this.fage.Text.Trim()) > 0 && Convert.ToInt32(this.fage.Text.Trim()) < 100)
                    {
                        age = this.fage.Text;
                    }
                    else
                    {
                        MessageBox.Show("年龄录入有误，请确认");
                        return;
                    }
                }
                catch
                {
                    MessageBox.Show("年龄录入有误，请确认");
                    return;
                }
            }
            else
                if (remark != "已故")
                {
                    MessageBox.Show("年龄不能为空！", "提示");
                    return;
                }
                else
                    if (remark == "已故")
                    {
                        age = "0";
                    }
            //修改
            if (!this.fCall.Text.Equals(""))
            {//称谓列表不为空时
                if (this.fCall.Text.Equals("其他"))
                {
                    if (textBox2.Text.Trim() != "")
                    {
                        call = textBox2.Text;//用户选择其他选项说明家庭成员的称谓是自己手动录入的
                    }
                    else
                    {
                        MessageBox.Show("请手动输入所要录入的家庭成员的称谓!", "提示");
                        return;
                    }
                }
                else
                {
                    call = this.fCall.Text;//称谓是列表选择的，直接赋值
                }
            }
            else
            {
                MessageBox.Show("称谓不能为空！", "提示");
                return;
            }

            if (!this.fName.Text.Equals(""))
            {
                name = this.fName.Text;
            }
            else
            {
                MessageBox.Show("姓名不能为空！", "提示");
                return;
            }

            birthday = this.fBirthday.Text;

            if (!this.fCountry.Text.Equals(""))
            {
                country = this.fCountry.Text.Trim();
            }
            else
            {
                MessageBox.Show("国籍不能为空！", "提示");
                return;
            }
            //因为添加了一项“ ”空白项，所以此处不应该再做限定了
            //if (!this.fParty.Text.Equals(""))
            //{
            //    party = this.fParty.Text.Trim();
            //}
            //else
            //{
            //    MessageBox.Show("党派不能为空！", "提示");
            //    return;
            //}

            if (!this.fNation.Text.Equals(""))
            {
                nation = this.fNation.Text.Trim();
            }
            else
            {
                MessageBox.Show("民族不能为空！", "提示");
                return;
            }

            if (!this.fWorkPlace.Text.Equals(""))
            {
                workplace = this.fWorkPlace.Text.Trim();
            }
            else
            {
                MessageBox.Show("工作单位及职务不能为空！", "提示");
                return;
            }
            if (isEditOfF)
            {
                //更新临时信息
                listView_Relation.SelectedItems[0].SubItems[1].Text = call;
                listView_Relation.SelectedItems[0].SubItems[2].Text = name;
                listView_Relation.SelectedItems[0].SubItems[3].Text = birthday;
                if (age != "0")
                    listView_Relation.SelectedItems[0].SubItems[4].Text = age;
                else
                    listView_Relation.SelectedItems[0].SubItems[4].Text = "";
                listView_Relation.SelectedItems[0].SubItems[5].Text = country;
                listView_Relation.SelectedItems[0].SubItems[6].Text = party;
                listView_Relation.SelectedItems[0].SubItems[7].Text = nation;
                listView_Relation.SelectedItems[0].SubItems[8].Text = workplace;
                listView_Relation.SelectedItems[0].SubItems[9].Text = remark;

                if (listView_Relation.SelectedItems[0].Tag.ToString() != "")//直接更新
                {
                    string sql = "update TB_family set relationship='" + call + "',name='" + name + "',birthday='" + birthday + "',age=" + age + ",country='" + country + "',party='" + party + "',nation='" + nation + "',deptJob='" + workplace + "',remark='" + remark + "' where id='" + listView_Relation.SelectedItems[0].Tag.ToString() + "' and cid='" + listViewCid + "'";
                    da.OperateData_sql(sql);
                    MessageBox.Show("家庭成员" + name + "的信息更新成功！", "提示!");
                }
                isEditOfF = false;
            }
            else
            {
                ListViewItem item = new ListViewItem();
                item.Tag = "";
                item.SubItems.Add(call);
                item.SubItems.Add(name);
                item.SubItems.Add(birthday);
                item.SubItems.Add(age);
                item.SubItems.Add(country);
                item.SubItems.Add(party);
                item.SubItems.Add(nation);
                item.SubItems.Add(workplace);
                item.SubItems.Add(remark);
                listView_Relation.Items.Add(item);
            }
            this.fCall.SelectedIndex = -1;
            this.fName.Text = "";
            this.fBirthday.Text = "";
            this.fParty.SelectedIndex = -1;
            this.fNation.SelectedIndex = -1;
            this.fWorkPlace.Text = "";
            this.fRemark.Text = "";
            this.fage.Text = "";
            this.hideC();
        }
        /// <summary>
        /// 修改家庭成员及其重要社会关系
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TSMI_EditFamily_Click(object sender, EventArgs e)
        {
            fid = listView_Relation.SelectedItems[0].Index.ToString();
            string tempdp = listView_Relation.SelectedItems[0].SubItems[1].Text.ToString();
            int pos = this.fCall.Items.IndexOf((object)(tempdp));
            if (pos < 0 && tempdp != "")
            {
                this.fCall.Text = "其他";
                this.showC();
                this.textBox2.Text = tempdp;
            }
            else
            {
                this.fCall.Text = tempdp;
            }
            fName.Text = listView_Relation.SelectedItems[0].SubItems[2].Text.ToString();
            fRemark.Text = listView_Relation.SelectedItems[0].SubItems[9].Text.ToString();
            fBirthday.Text = listView_Relation.SelectedItems[0].SubItems[3].Text.ToString();
            fCountry.Text = listView_Relation.SelectedItems[0].SubItems[5].Text.ToString();
            fParty.Text = listView_Relation.SelectedItems[0].SubItems[6].Text.ToString();
            if (fRemark.Text == "已故")
                fage.Text = "";
            else
                fage.Text = listView_Relation.SelectedItems[0].SubItems[4].Text.ToString();
            fNation.Text = listView_Relation.SelectedItems[0].SubItems[7].Text.ToString();
            fWorkPlace.Text = listView_Relation.SelectedItems[0].SubItems[8].Text.ToString();
            isEditOfF = true;
        }
        /// <summary>
        /// 奖惩情况确定
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void aSure_Click(object sender, EventArgs e)
        {
            string awardclass = "";
            string degree = "";
            string time = "";
            string content = "";

            if (!this.aClass.Text.Equals(""))
            {
                awardclass = aClass.Text;
            }
            else
            {
                MessageBox.Show("奖惩类别不能为空！", "提示");
                return;
            }

            if (!this.aDegree.Text.Equals(""))
            {
                degree = aDegree.Text;
            }
            else
            {
                MessageBox.Show("奖惩级别不能为空！", "提示");
                return;
            }

            time = this.aAwardTime.Text.ToString();

            if (!this.aDepartment.Text.Equals(""))
            {
                content = aDepartment.Text;
            }
            else
            {
                MessageBox.Show("奖惩内容不能为空！", "提示");
                return;
            }
            if (isEditOfA)//如果是更新已存在的信息
            {
                //更新临时信息
                listView_Award.SelectedItems[0].SubItems[1].Text = awardclass;
                listView_Award.SelectedItems[0].SubItems[2].Text = degree;
                listView_Award.SelectedItems[0].SubItems[3].Text = time;
                listView_Award.SelectedItems[0].SubItems[4].Text = content;
                //如果已经存在于数据库中的数据

                if (listView_Award.SelectedItems[0].Tag.ToString() != "")//直接更新
                {
                    string sql = "update TB_PunishAward set class='" + awardclass + "',grade='" + degree + "',time='" + time + "',department='" + content + "' where id='" + listView_Award.SelectedItems[0].Tag.ToString() + "' and cid='" + listViewCid + "'";
                    da.OperateData_sql(sql);
                    MessageBox.Show("更新成功！", "提示!");
                }
                isEditOfA = false;
            }
            else
            {
                ListViewItem item = new ListViewItem();
                item.Tag = "";
                item.SubItems.Add(awardclass);
                item.SubItems.Add(degree);
                item.SubItems.Add(time);
                item.SubItems.Add(content);
                listView_Award.Items.Add(item);
            }
            this.aClass.SelectedIndex = -1;
            this.aDegree.SelectedIndex = -1;
            this.aDepartment.Text = "";
            hideC();
        }
        /// <summary>
        /// 手动录入称谓的文本框清空并隐藏
        /// </summary>
        private void hideC()
        {
            label38.Visible = false;
            textBox2.Text = "";
            textBox2.Visible = false;
        }
        /// <summary>
        /// 手动录入称谓的文本框清空并显示
        /// </summary>
        private void showC()
        {
            label38.Visible = true;
            textBox2.Text = "";
            textBox2.Visible = true;
        }
        /// <summary>
        /// 个人简历清空
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button3_Click(object sender, EventArgs e)
        {
            textBox_ResumeInput.Text = "";
            isEditOfR = false;
        }

        /// <summary>
        ///  奖惩情况清空
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void aClear_Click(object sender, EventArgs e)
        {
            this.aClass.SelectedIndex = -1;
            this.aDegree.SelectedIndex = -1;
            this.aDepartment.Text = "";
            isEditOfA = false;
        }

        /// <summary>
        /// 家庭成员及重要社会关系清空
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void fClear_Click(object sender, EventArgs e)
        {
            this.fCall.SelectedIndex = -1;
            this.fName.Text = "";
            this.fBirthday.Text = "";
            this.fParty.SelectedIndex = -1;
            this.fNation.SelectedIndex = -1;
            this.fWorkPlace.Text = "";
            this.fRemark.Text = "";
            this.fage.Text = "";
            this.hideC();
            isEditOfF = false;
        }

        /// <summary>
        /// 考核第一年输入
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void comboBox_first_year_TextChanged(object sender, EventArgs e)
        {
            if (comboBox_first_year.Text != "" && textBox_second_year.Text != "" && textBox_third_year.Text != "")
            {
                Int32 a = Convert.ToInt32(comboBox_first_year.Text);
                textBox_second_year.Text = (++a).ToString();
                textBox_third_year.Text = (++a).ToString();
                Int32 n;
                n = Convert.ToInt32(DateTime.Now.Year.ToString());
                if (a > n)
                {
                    MessageBox.Show("输入的时间有误！", "提示");
                    comboBox_first_year.Text = "";
                    comboBox_first_year.SelectedIndex = -1;
                    textBox_second_year.Text = "";
                    textBox_third_year.Text = "";
                    result1.Text = "";
                    result2.Text = "";
                    result3.Text = "";
                    result1.SelectedIndex = -1;
                    result2.SelectedIndex = -1;
                    result3.SelectedIndex = -1;
                }
            }
        }

        /// <summary>
        /// 第一年考核选择
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void comboBox_firstYear_SelectedIndexChanged(object sender, EventArgs e)
        {
            Int32 a = Convert.ToInt32(comboBox_first_year.Text.Trim());
            textBox_second_year.Text = (++a).ToString();
            textBox_third_year.Text = (++a).ToString();
            if (change)
            {
                remarkchange = true;
            }
        }

        /// <summary>
        /// 考核第三年输入
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void textBox_third_year_TextChanged(object sender, EventArgs e)
        {
            if (comboBox_first_year.Text != "" && textBox_second_year.Text != "" && textBox_third_year.Text != "")
            {
                Int32 a = Convert.ToInt32(comboBox_first_year.Text);
                textBox_second_year.Text = (++a).ToString();
                textBox_third_year.Text = (++a).ToString();
                Int32 n;
                n = Convert.ToInt32(DateTime.Now.Year.ToString());
                if (a > n)
                {

                    MessageBox.Show("输入的时间有误！", "提示");
                    comboBox_first_year.SelectedIndex = -1;
                    textBox_second_year.Text = "";
                    textBox_third_year.Text = "";
                }
            }

        }

        /// <summary>
        /// 限制年龄只能为数字
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void textBox_age_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar != 8 && !Char.IsDigit(e.KeyChar))
            {
                MessageBox.Show("请输入阿拉伯数字!");
                e.Handled = true;
            }

        }

        /// <summary>
        /// 限制家庭成员年龄只能是数字
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void fage_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar != 8 && !Char.IsDigit(e.KeyChar))
            {
                MessageBox.Show("请输入阿拉伯数字!", "提示");
                e.Handled = true;
            }
        }

        /// <summary>
        /// 限制考核第一年只能是数字
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void comboBox_first_year_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar != 8 && !Char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        /// <summary>
        /// 窗口关闭时删除被标记取消资格的人
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>

        private void PeopleNameSheet_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (remarkchange)
            {
                switch (MessageBox.Show("是否保存当前信息？", "提示", MessageBoxButtons.YesNoCancel))
                {
                    case DialogResult.Yes:
                        SaveCurrent(listViewCid);
                        break;
                    case DialogResult.Cancel:
                        e.Cancel = true;
                        break;
                }
            }
        }

        /// <summary>
        /// 出生年月值改变的事件的处理，判断输入时间的合理性，并计算年龄
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void fBirthday_ValueChanged(object sender, EventArgs e)
        {
            if (Convert.ToInt32(fBirthday.Value.ToString("yyyyMM")) > Convert.ToInt32(DateTime.Now.ToString("yyyyMM")))
            {
                MessageBox.Show("出生年月应小于等于当前时间！", "提示");
                fBirthday.Value = DateTime.Now;
                fage.Text = "";
                return;
            }

            if (Convert.ToInt32(fBirthday.Value.ToString("yyyyMM")) < Convert.ToInt32(DateTime.Now.ToString("yyyyMM")))
            {
                string temp = DateTime.Today.ToString("yyyy'年'MM'月'");
                int year = Convert.ToInt32(temp.Substring(0, 4));
                int month = Convert.ToInt32(temp.Substring(5, 2));
                string temp1 = fBirthday.Value.ToString("yyyy'年'MM'月'");
                int year1 = Convert.ToInt32(temp1.Substring(0, 4));
                int month1 = Convert.ToInt32(temp1.Substring(5, 2));

                if (fRemark.Text == "已故")
                {
                    fage.Text = "";
                }
                else
                {
                    if (month >= month1)
                    {
                        fage.Text = (year - year1).ToString();
                    }
                    else
                    {
                        fage.Text = (year - year1 - 1).ToString();
                    }
                }
            }
        }

        /// <summary>
        /// 简历中的开始时间值改变的事件处理，对简历中开始时间的合理性进行检测
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dateTimePicker_StartTime_ValueChanged(object sender, EventArgs e)
        {
            int tempEnd = Convert.ToInt32(dateTimePicker_EndTime.Value.ToString("yyyyMM"));
            int tempStart = Convert.ToInt32(dateTimePicker_StartTime.Value.ToString("yyyyMM"));
            int now = Convert.ToInt32(DateTime.Now.ToString("yyyyMM"));
            if (tempStart > tempEnd)
            {
                MessageBox.Show("开始时间应小于结束时间", "提示");
                dateTimePicker_StartTime.Value = dateTimePicker_EndTime.Value;
            }
            if (change)
            {
                remarkchange = true;
            }
        }

        /// <summary>
        /// 简历中的结束时间值改变的事件处理，对简历中结束时间的合理性进行检测
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dateTimePicker_EndTime_ValueChanged(object sender, EventArgs e)
        {
            int tempEnd = Convert.ToInt32(dateTimePicker_EndTime.Value.ToString("yyyyMM"));
            int tempStart = Convert.ToInt32(dateTimePicker_StartTime.Value.ToString("yyyyMM"));
            int now = Convert.ToInt32(DateTime.Now.ToString("yyyyMM"));
            if (tempEnd > now)
            {
                MessageBox.Show("结束时间应小于当前时间", "提示");
                dateTimePicker_EndTime.Value = DateTime.Now;
            }
            if (tempEnd < tempStart)
            {
                MessageBox.Show("结束时间应大于开始时间", "提示");
                dateTimePicker_EndTime.Value = DateTime.Now;
            }

            if (change)
            {
                remarkchange = true;
            }
        }

        /// <summary>
        /// 读取人员的相关信息
        /// </summary>
        /// <param name="cid"></param>
        public void readShowTable(string cid)
        {
            //个人简历
            #region
            string sql = "select id, betime, entime, content from TB_Resume where cid = '" + cid + "'";
            DataTable TB_Resume = da.GetOneDataTable_sql(sql);
            for (int i = 0; i < TB_Resume.Rows.Count; i++)
            {
                ListViewItem item = new ListViewItem();
                item.Tag = TB_Resume.Rows[i]["id"].ToString();
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
                item.Tag = TB_PunishAward.Rows[i]["id"].ToString();
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
                item.Tag = TB_Family.Rows[i]["id"].ToString();
                item.SubItems.Add(TB_Family.Rows[i]["relationship"].ToString());
                item.SubItems.Add(TB_Family.Rows[i]["name"].ToString());
                item.SubItems.Add(TB_Family.Rows[i]["birthday"].ToString());
                if (TB_Family.Rows[i]["age"].ToString() == "0")
                    item.SubItems.Add("");
                else
                    item.SubItems.Add(TB_Family.Rows[i]["age"].ToString());
                item.SubItems.Add(TB_Family.Rows[i]["country"].ToString());
                item.SubItems.Add(TB_Family.Rows[i]["party"].ToString());
                item.SubItems.Add(TB_Family.Rows[i]["nation"].ToString());
                item.SubItems.Add(TB_Family.Rows[i]["deptJob"].ToString());
                item.SubItems.Add(TB_Family.Rows[i]["remark"].ToString());
                listView_Relation.Items.Add(item);
            }
            #endregion
        }

        /// <summary>
        /// 点击个人简历中确定按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button_Sure_Resume_Click(object sender, EventArgs e)
        {
            if (textBox_ResumeInput.Text.Equals(""))
            {
                MessageBox.Show("内容不能为空！", "提示");
                return;
            }
            string time1 = dateTimePicker_StartTime.Text;
            string time2 = "";
            if (!dateTimePicker_EndTime.Text.Equals(DateTime.Now.ToString("yyyy'年'MM'月'")))
            {
                time2 = dateTimePicker_EndTime.Text;
            }
            string content = textBox_ResumeInput.Text.Trim();
            if (isEditOfR)//如果是更新已存在的信息
            {
                //更新临时信息
                listView_Vital.SelectedItems[0].SubItems[1].Text = time1;
                listView_Vital.SelectedItems[0].SubItems[2].Text = time2;
                listView_Vital.SelectedItems[0].SubItems[3].Text = content;
                //如果已经存在于数据库中的数据
                ResumeProperty resume = new ResumeProperty();
                if (listView_Vital.SelectedItems[0].Tag.ToString() != "")//直接更新
                {
                    string sql = "update TB_Resume set betime='" + time1 + "',entime='" + time2 + "',content='" + content + "' where id='" + listView_Vital.SelectedItems[0].Tag.ToString() + "' and cid='" + listViewCid + "'";
                    da.OperateData_sql(sql);
                    MessageBox.Show(time1 + "至" + time2 + "的个人简历信息更新成功！", "提示!");
                }
                isEditOfR = false;
                //remarkchange = true;
            }
            //新增的，临时存放
            else
            {
                ListViewItem item = new ListViewItem();
                item.Tag = "";
                item.SubItems.Add(time1);
                item.SubItems.Add(time2);
                item.SubItems.Add(content);
                listView_Vital.Items.Add(item);
            }
            dateTimePicker_StartTime.Value = dateTimePicker_EndTime.Value;
            textBox_ResumeInput.Text = "";
        }

        /// <summary>
        /// 右击快捷键中删除事件的处理，删除一条个人简历
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TSMI_DeleteResume_Click(object sender, EventArgs e)
        {
            DialogResult objDialogResult = MessageBox.Show("您确定要删除这条记录吗？", "确认", MessageBoxButtons.YesNo);
            if (objDialogResult == DialogResult.Yes)
            {
                if (listView_Vital.SelectedItems[0].Tag.ToString().Equals(""))//说明是刚刚新添加的记录，还未写入数据库中
                {
                    listView_Vital.SelectedItems[0].Remove();
                }
                else
                {
                    int id = Convert.ToInt32(listView_Vital.SelectedItems[0].Tag.ToString());
                    da.OperateData_sql("Delete from TB_Resume where id = '" + id + "'");
                    listView_Vital.SelectedItems[0].Remove();
                }
                textBox_ResumeInput.Text = "";
                isEditOfR = false;
                MessageBox.Show("删除成功！", "提示");
            }
            else
            {
                return;
            }
        }
        /// <summary>
        /// 右击快捷键中修改事件的处理，修改一条个人简历
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TSMI_EditResume_Click(object sender, EventArgs e)
        {
            //MessageBox.Show(listView_Vital.SelectedItems[0].Index.ToString()+"\\"+listView_Vital.SelectedItems[0].Tag.ToString());
            rid = listView_Vital.SelectedItems[0].Index.ToString();
            dateTimePicker_StartTime.Text = listView_Vital.SelectedItems[0].SubItems[1].Text.ToString();
            dateTimePicker_EndTime.Text = listView_Vital.SelectedItems[0].SubItems[2].Text.ToString();
            textBox_ResumeInput.Text = listView_Vital.SelectedItems[0].SubItems[3].Text.ToString();
            isEditOfR = true;
        }
        /// <summary>
        /// listView_Vital鼠标松开事件的处理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void listView_Vital_MouseUp(object sender, MouseEventArgs e)
        {
            if (listView_Vital.SelectedItems.Count == 0 && listView_Vital.Items.Count != 0)
            {
                listView_Vital.Items[0].Selected = true;
            }
        }

        /// <summary>
        /// listView_Award右击删除事件的处理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TSMI_DeleteAward_Click(object sender, EventArgs e)
        {
            if (listView_Award.SelectedItems[0].Tag.ToString().Equals(""))
            {
                listView_Award.SelectedItems[0].Remove();
            }
            else
            {
                int id = Convert.ToInt32(listView_Award.SelectedItems[0].Tag.ToString());
                da.OperateData_sql("Delete from TB_Punishaward where id = '" + id + "'");
                listView_Award.SelectedItems[0].Remove();
            }
            this.aClass.SelectedIndex = -1;
            this.aDegree.SelectedIndex = -1;
            this.aDepartment.Text = "";
            isEditOfA = false;
            MessageBox.Show("删除成功！", "提示");
        }
        /// <summary>
        /// listView_Award右击修改事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TSMI_EditAward_Click(object sender, EventArgs e)
        {
            aid = listView_Award.SelectedItems[0].Index.ToString();
            aClass.Text = listView_Award.SelectedItems[0].SubItems[1].Text.ToString();
            aDegree.Text = listView_Award.SelectedItems[0].SubItems[2].Text.ToString();
            aAwardTime.Text = listView_Award.SelectedItems[0].SubItems[3].Text.ToString();
            aDepartment.Text = listView_Award.SelectedItems[0].SubItems[4].Text.ToString();
            isEditOfA = true;
        }
        /// <summary>
        /// listView_Award鼠标松开事件的处理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void listView_Award_MouseUp(object sender, MouseEventArgs e)
        {
            if (listView_Award.SelectedItems.Count == 0 && listView_Award.Items.Count != 0)
            {
                listView_Award.Items[0].Selected = true;
            }
        }

        /// <summary>
        /// listView_Relation右击删除事件的处理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TSMI_DeleteFamily_Click(object sender, EventArgs e)
        {
            if (listView_Relation.SelectedItems[0].Tag.ToString().Equals(""))
            {
                listView_Relation.SelectedItems[0].Remove();
            }
            else
            {
                int id = Convert.ToInt32(listView_Relation.SelectedItems[0].Tag.ToString());
                da.OperateData_sql("Delete from TB_family where id = '" + id + "'");
                listView_Relation.SelectedItems[0].Remove();
            }

            this.fCall.SelectedIndex = -1;
            this.fName.Text = "";
            this.fBirthday.Text = "";
            this.fParty.SelectedIndex = -1;
            this.fNation.SelectedIndex = -1;
            this.fWorkPlace.Text = "";
            this.fRemark.Text = "";
            this.fage.Text = "";
            this.hideC();
            isEditOfF = false;
            MessageBox.Show("删除成功！", "提示");
        }

        /// <summary>
        /// listView_Relation鼠标松开事件的处理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void listView_Relation_MouseUp(object sender, MouseEventArgs e)
        {
            if (listView_Relation.SelectedItems.Count == 0 && listView_Relation.Items.Count != 0)
            {
                listView_Relation.Items[0].Selected = true;
            }
        }

        /// <summary>
        /// listView_Vital选中项改变的事件处理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void listView_Vital_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listView_Vital.SelectedItems.Count == 0)
            {
                TSMI_DeleteResume.Enabled = false;
                TSMI_EditResume.Enabled = false;
            }
            else
            {
                TSMI_DeleteResume.Enabled = true;
                TSMI_EditResume.Enabled = true;
            }
        }

        /// <summary>
        /// listView_Award选中项改变的事件处理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void listView_Award_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listView_Award.SelectedItems.Count == 0)
            {
                TSMI_DeleteAward.Enabled = false;
                TSMI_EditAward.Enabled = false;
            }
            else
            {
                TSMI_DeleteAward.Enabled = true;
                TSMI_EditAward.Enabled = true;
            }
        }

        /// <summary>
        /// listView_Relation选中项改变的事件处理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void listView_Relation_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listView_Relation.SelectedItems.Count == 0)
            {
                TSMI_DeleteFamily.Enabled = false;
                TSMI_EditFamily.Enabled = false;
            }
            else
            {
                TSMI_DeleteFamily.Enabled = true;
                TSMI_EditFamily.Enabled = true;
            }
        }

        #region
        /// <summary>
        /// 文本框信息改变事件的处理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void textBox_BirthPlace_TextChanged(object sender, EventArgs e)
        {
            if (change)
            {
                remarkchange = true;
            }
        }

        /// <summary>
        /// 选中的下拉选项改变的事件的处理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void comboBox_healthy_state_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (change)
            {
                remarkchange = true;
            }
        }

        /// <summary>
        /// 文本框信息改变事件的处理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void textBox_speciality_TextChanged(object sender, EventArgs e)
        {
            if (change)
            {
                remarkchange = true;
            }
        }

        /// <summary>
        /// 文本框信息改变事件的处理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void textBox_remark_TextChanged(object sender, EventArgs e)
        {
            if (change)
            {
                remarkchange = true;
            }
        }

        /// <summary>
        /// 选中的下拉选项改变的事件的处理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void result1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (change)
            {
                remarkchange = true;
            }
        }

        /// <summary>
        /// 选中的下拉选项改变的事件的处理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void result2_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (change)
            {
                remarkchange = true;
            }
        }

        /// <summary>
        /// 选中的下拉选项改变的事件的处理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void result3_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (change)
            {
                remarkchange = true;
            }
        }

        private void textBox_ResumeInput_TextChanged(object sender, EventArgs e)
        {
            if (change)
            {
                remarkchange = true;
            }
        }

        /// <summary>
        /// 选中的下拉选项改变的事件的处理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void fCall_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (change)
            {
                remarkchange = true;
            }
            if (fCall.SelectedIndex >= 0)
            {
                if (fCall.SelectedItem.ToString() == "其他")
                {
                    showC();
                }
                else
                {
                    hideC();
                }
            }
        }

        /// <summary>
        /// 文本框信息改变事件的处理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void fName_TextChanged(object sender, EventArgs e)
        {
            if (change)
            {
                remarkchange = true;
            }
        }

        /// <summary>
        /// 选中的下拉选项改变的事件的处理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void aClass_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (change)
            {
                remarkchange = true;
            }
        }

        /// <summary>
        /// 文本框信息改变事件的处理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void aDepartment_TextChanged(object sender, EventArgs e)
        {
            if (change)
            {
                remarkchange = true;
            }
        }
        #endregion

        /// <summary>
        /// 限制奖惩时间
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void aAwardTime_ValueChanged(object sender, EventArgs e)
        {
            if (Convert.ToInt32(aAwardTime.Value.ToString("yyyyMM")) > Convert.ToInt32(DateTime.Now.ToString("yyyyMM")))
            {
                MessageBox.Show("授予应小于等于当前时间！", "提示");
                aAwardTime.Value = DateTime.Now;
                return;
            }
        }

        private void fRemark_TextChanged(object sender, EventArgs e)
        {
            if (fRemark.Text.Equals("已故") || fRemark.Text.Equals("已过世") || fRemark.Text.Equals("死亡"))
            {
                fage.Text = "";
            }
        }

        private void fRemark_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (fRemark.Text.Equals("已故") || fRemark.Text.Equals("已过世") || fRemark.Text.Equals("死亡"))
            {
                fage.Text = "";
            }
        }

        /// <summary>
        /// 当拖动某项时
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void listView_Vital_ItemDrag(object sender, ItemDragEventArgs e)
        {
            listView_Vital.DoDragDrop(e.Item, DragDropEffects.Move);
        }
        /// <summary>
        /// 用鼠标拖动某项至该控件的区域时
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void listView_Vital_DragEnter(object sender, DragEventArgs e)
        {
            e.Effect = DragDropEffects.Move;
        }
        /// <summary>
        /// 拖动时拖着某项置于某行上方时
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void listView_Vital_DragOver(object sender, DragEventArgs e)
        {
            Point ptScreen = new Point(e.X, e.Y);
            Point pt = listView_Vital.PointToClient(ptScreen);
            ListViewItem item = listView_Vital.GetItemAt(pt.X, pt.Y);
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
        private void listView_Vital_DragDrop(object sender, DragEventArgs e)
        {
            try
            {
                ListViewItem draggedItem = (ListViewItem)e.Data.GetData(typeof(ListViewItem));
                Point ptScreen = new Point(e.X, e.Y);
                Point pt = listView_Vital.PointToClient(ptScreen);
                ListViewItem TargetItem = listView_Vital.GetItemAt(pt.X, pt.Y);//拖动的项将放置于该项之前    
                listView_Vital.Items.Insert(TargetItem.Index, (ListViewItem)draggedItem.Clone());
                listView_Vital.Items.Remove(draggedItem);
                da.OperateData_sql("delete from TB_Resume where cid='" + listViewCid + "'");
                for (int i = 0; i < listView_Vital.Items.Count; i++)
                {
                    listView_Vital.Items[i].Tag = "";
                }
                remarkchange = true;
            }
            catch { }
        }

    }
}