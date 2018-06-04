using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using POS.Common;
using POS.View;
/*用于点单功能，包含销售单的相关信息
 类说明：点单控件，显示点餐信息并可以对商品数量进行加一或减一
主要公共方法：
（1）、ClearListOrderDinner()，清空点单控件中的商品
（2）、DelOneGroup()，删除当前选中的一个group_prod下的商品
（3）、DiscountProd()，对一个商品组打折
（4）、AltGroupIdNumber(decimal number)，改变点单中组商品的数量
（5）、LoadInfo()，点单控件加载信息
委托：
（1）、GetInfoEventHandler，点单控件加载数据
（2）、DataAlterEventHandler，改变一个商品组的商品数量
（3）、DataDelEventHandler，删除一个group_prod下的商品或者对一个商品组打折
（4）、BeicanClickEventHandler，点击备餐按钮

 */
namespace POS.Controls
{
    /// <summary>
    /// 点单控件
    /// </summary>
    public partial class OrderMenu : UserControl
    {
        /// <summary>
        /// 构造方法
        /// </summary>
        public OrderMenu()
        {
            InitializeComponent();
        }


        #region//委托

        /// <summary>
        /// 定义一个委托
        /// </summary>
        /// <param name="orderMenu">点单控件</param>
       public  delegate void GetInfoEventHandler(OrderMenu orderMenu);

       /// <summary>
       /// 定义一个委托J(点击备餐按钮）
       /// </summary>
       public delegate void BeicanClickEventHandler();

       /// <summary>
       /// 定义一个委托
       /// </summary>
       /// <param name="selectedGroupProd">选中的商品组号</param>
       public delegate void DataDelEventHandler(string selectedGroupProd);

        /// <summary>
       /// 定义一个委托
        /// </summary>
       /// <param name="group_prod">组商品号</param>
       /// <param name="type">操作类型，0代表自加1，1代表自减1，2代表按照number控件中给定的数值修改原来一个group_prod下的商品数量</param>
       /// <param name="number">商品数量</param>
       public delegate void DataAlterEventHandler(string group_prod,int type,decimal number);

        /// <summary>
        /// 声明一个委托对象（点单控件加载数据）
        /// </summary>
        [Description(" 点单控件加载数据"), Category("自定义")]
        public event GetInfoEventHandler GetInfo;

        /// <summary>
        /// 声明一个委托对象（改变一个商品组的商品数量）
        /// </summary>
        [Description("改变一个商品组的商品数量"), Category("自定义")]
        public event DataAlterEventHandler AlterProd;

        /// <summary>
        /// 声明一个委托对象（删除一个商品组）
        /// </summary>
        [Description("删除一个商品组"), Category("自定义")]
        public event DataDelEventHandler DeleteGroup;


        /// <summary>
        /// 声明一个委托对象（对一个商品组打折）
        /// </summary>
        [Description("对一个商品组打折"), Category("自定义")]
        public event DataDelEventHandler DiscountGroupProd;

        /// <summary>
        /// 声明一个委托对象（点击备餐按钮）
        /// </summary>
        [Description("点击备餐按钮时执行"), Category("自定义")]
        public event BeicanClickEventHandler BeicanClick;

        #endregion

        #region//字段


        /// <summary>
        /// 保存DataGridView中每行对应的：商品ID、组合号、商品数量
        /// </summary>
        string[,] id_groupProd_num_combo;


        /// <summary>
        /// 保存选中的套餐id的在数组中的索引
        /// </summary>
        private string selectedGroupProd = "";

        /// <summary>
        /// 主窗体
        /// </summary>
        private MainForm mainForm;

        /// <summary>
        /// 保存需要在DataGridView中选中的行号
        /// </summary>
        List<int> selectedRowIndex = new List<int>();

        #endregion

        #region//属性


        /// <summary>
        /// 上按钮
        /// </summary>
        public Button BtnUp
        {
            get { return btnUp; }
            set { btnUp = value; }
        }

        /// <summary>
        /// 备餐按钮
        /// </summary>
        public Button BtnBeiCan
        {
            get { return btnBeiCan; }
            set { btnBeiCan = value; }
        }

        /// <summary>
        /// 减按钮
        /// </summary>
        public Button BtnMinuse
        {
            get { return btnMinuse; }
            set { btnMinuse = value; }
        }

        /// <summary>
        /// 加按钮
        /// </summary>
        public Button BtnAdd
        {
            get { return btnAdd; }
            set { btnAdd = value; }
        }

        /// <summary>
        /// 下按钮
        /// </summary>
        public Button BtnDown
        {
            get { return btnDown; }
            set { btnDown = value; }
        }

        /// <summary>
        /// 设置点单控件上一个销售单的商品总价
        /// </summary>
        public string TotalPrice
        {
            get { return this.totalPrice2.Text; }
            set {  this.totalPrice2.Text = value; }
        }

        /// <summary>
        /// 设置点单控件上一个销售单的Sale_type
        /// </summary>
        public string Sale_type
        {
            get { return this.sale_type.Text; }
            set { this.sale_type.Text = value; }
        }

        /// <summary>
        /// 保存选中的套餐id的在数组中的索引
        /// </summary>
        public string SelectedGroupProd
        {
            get { return selectedGroupProd; }
            set { selectedGroupProd = value; }
        }

        /// <summary>
        /// 主窗体
        /// </summary>
        public MainForm MainForm
        {
            get { return mainForm; }
            set { mainForm = value; }
        }

        /// <summary>
        /// 返回DgvOrderMenu
        /// </summary>
        public DataGridView DgvOrderMenu
        {
            get { return this.dgvOrderMenu; }
        }

        #endregion

        #region//方法

        #region//公共方法

        /// <summary>
        /// 清空点单控件
        /// </summary>
        public void ClearListOrderDinner()
        {
            this.dgvOrderMenu.DataSource = null;
            this.sale_type.Text = "";
            this.totalPrice2.Text = "0.0元";
        }

        /// <summary>
        /// 删除当前选中的一个组
        /// </summary>
        public void DelOneGroup()
        {
            //触发事件的方法
            DeleteGroup(selectedGroupProd);
        }
        /// <summary>
        /// 对一个商品组打折
        /// </summary>
        public void DiscountProd()
        {
            //触发事件的方法
            DiscountGroupProd(selectedGroupProd);
        }

        /// <summary>
        /// 改变点单中组商品的数量
        /// </summary>
        /// <param name="number">加减的数量</param>
        /// <returns></returns>
        public void AltGroupIdNumber(decimal number)
        {

            AlterProd(this.selectedGroupProd, 2, number);
        }

        /// <summary>
        /// 给点单控件OrderDinner加载数据
        /// </summary>
        public void LoadInfo()
        {
            //触发事件的方法
            GetInfo(this);

            //如果上次对商品数量进行修改或者打折或者折让的话
            if (Info.isSelectPre)
            {
                //选中上次修改数量或者打折或者折让的商品
                SelectProdGroup(Info.selectedGroupProd);
                Info.isSelectPre = false;
            }
            else
            {
                //选中最后一个商品
                SelectLastGroupProd();
            }
            if (IsVerticalScrollAppear())
            {
                //让滚动条一直靠到最下面
                if (this.dgvOrderMenu.RowCount > 0)
                {
                    //VerticalScrollToTop(this.dgvOrderMenu.RowCount - 1);
                    VerticalScrollToTop(this.dgvOrderMenu.SelectedRows[0].Index);
                }
            }
         
        }

        /// <summary>
        /// 给prodId_groupProd_number赋值
        /// </summary>
        /// <param name="ds"></param>
        public void Init_Index_Group_Array(DataSet ds)
        {
            int length = 0;
            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                //排除组合餐的子产品
                if (ds.Tables[0].Rows[i]["Comb_Type"].ToString() != "2")
                {
                    length++;
                }

            }
            id_groupProd_num_combo = new string[length, 4];
            length = 0;
            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {

                //排除组合餐的子产品
                if (ds.Tables[0].Rows[i]["Comb_Type"].ToString() != "2")
                {
                    id_groupProd_num_combo[length, 0] = ds.Tables[0].Rows[i]["prod_id"].ToString();
                    id_groupProd_num_combo[length, 1] = ds.Tables[0].Rows[i]["group_prod"].ToString();
                    id_groupProd_num_combo[length, 2] = ds.Tables[0].Rows[i]["qty"].ToString();
                    id_groupProd_num_combo[length, 3] = ds.Tables[0].Rows[i]["comb_type"].ToString();
                    length++;
                }
            }


        }

        #endregion

        #region//私有方法

       

        /// <summary>
        /// 设置控件和里面的元素的大小
        /// </summary>
        private void SetSize()
        {
            this.dgvOrderMenu.Width = this.Width * 430 / 484;
            this.dgvOrderMenu.Height = this.Height * 222 / 242;

            this.dgvOrderMenu.Location = new Point(0, 0);
            this.btnDown.Location = new Point(this.dgvOrderMenu.Width, 0);
            this.btnAdd.Location = new Point(this.dgvOrderMenu.Width, this.Height / 5);
            this.btnMinuse.Location = new Point(this.dgvOrderMenu.Width, this.Height * 2 / 5);
            this.btnBeiCan.Location = new Point(this.dgvOrderMenu.Width, this.Height * 3 / 5);
            this.btnUp.Location = new Point(this.dgvOrderMenu.Width, this.Height * 4 / 5);

            this.sale_type.Location = new Point(this.Width * 60 / 430, this.dgvOrderMenu.Height);
            this.totalPrice1.Location = new Point(this.Width * 154 / 430, this.dgvOrderMenu.Height);
            this.totalPrice2.Location = new Point(this.Width * 340 / 430, this.dgvOrderMenu.Height);

            this.btnAdd.Width = this.Width - this.dgvOrderMenu.Width;
            this.btnBeiCan.Width = this.Width - this.dgvOrderMenu.Width;
            this.btnUp.Width = this.Width - this.dgvOrderMenu.Width;
            this.btnDown.Width = this.Width - this.dgvOrderMenu.Width;
            this.btnMinuse.Width = this.Width - this.dgvOrderMenu.Width;


            this.btnAdd.Height = this.Height / 5;
            this.btnBeiCan.Height = this.Height / 5;
            this.btnUp.Height = this.Height / 5;
            this.btnDown.Height = this.Height / 5;
            this.btnMinuse.Height = this.Height / 5;

            this.dgvOrderMenu.Columns[0].Width = this.dgvOrderMenu.Width * 30 / 430;
            this.dgvOrderMenu.Columns[1].Width = this.dgvOrderMenu.Width * 124 / 430;
            this.dgvOrderMenu.Columns[2].Width = this.dgvOrderMenu.Width * 88 / 430;
            this.dgvOrderMenu.Columns[3].Width = this.dgvOrderMenu.Width * 48 / 430;
            this.dgvOrderMenu.Columns[4].Width = this.dgvOrderMenu.Width * 48 / 430;
            this.dgvOrderMenu.Columns[5].Width = this.dgvOrderMenu.Width * 92 / 430;

        }

        /// <summary>
        /// 根据DataGridView中选中的行号索引得到该行所对应的Group_prod，并把Group_prod对应的数量写入Info类中
        /// </summary>
        /// <param name="rowIndex">当前在DataGridView点击的行号索引</param>
        /// <returns></returns>
        private void GetGroup(int rowIndex)
        {
            try
            {
                if (id_groupProd_num_combo != null)
                {
                    this.selectedGroupProd = id_groupProd_num_combo[rowIndex, 1];
                    try
                    {
                        //把组合餐数量写入Info
                        Info.selectedGroupProd_qty = Convert.ToDecimal(id_groupProd_num_combo[rowIndex, 2]);
                    }
                    catch { Info.selectedGroupProd_qty = 0; }

                    //得到需要选中的索引数组
                    GetSelectingRowIndexs(rowIndex);

                    //向Info中写入商品ID，如果是组合餐则是组合餐的ID

                    //若选中的行索引的comb_type不是2
                    if (id_groupProd_num_combo[rowIndex, 3].Equals("0"))
                    {
                        //跟据行索引获取被选中的(Group_prod),并把把组合餐号写入Info
                        Info.selectedGroupProd = id_groupProd_num_combo[rowIndex, 1];
                        Info.canResetComb = false;
                        Info.prod_id = id_groupProd_num_combo[rowIndex, 0];
                    }
                    //若选中的行索引的comb_type不是2
                   else if (id_groupProd_num_combo[rowIndex, 3].Equals("1"))
                    {
                        Info.canResetComb = true;
                        Info.selectedGroupProd = id_groupProd_num_combo[rowIndex, 1];
                        Info.prod_id = id_groupProd_num_combo[rowIndex, 0];
                    }
                    else
                    {
                        //从选中的行索引数组中查找comb_type不是2的商品ID
                        for (int i = 0; i < selectedRowIndex.Count; i++)
                        {
                            if (id_groupProd_num_combo[selectedRowIndex[i], 3].Equals("0"))
                            {
                                Info.selectedGroupProd = id_groupProd_num_combo[selectedRowIndex[i], 1];
                                Info.canResetComb = false;
                                Info.prod_id = id_groupProd_num_combo[selectedRowIndex[i], 0];
                            }
                            else if (id_groupProd_num_combo[selectedRowIndex[i], 3].Equals("1"))
                            {
                                Info.selectedGroupProd = id_groupProd_num_combo[selectedRowIndex[i], 1];
                                Info.canResetComb = true;
                                Info.prod_id = id_groupProd_num_combo[selectedRowIndex[i], 0];
                            }
                                
                        }
                    }
                }


            }
            catch { }

        }

        /// <summary>
        /// 根据DataGridView中选中的行号索引得到一个Group_prod下的需要选中的行号索引数组
        /// </summary>
        /// <param name="rowIndex">当前在DataGridView点击的行号索引</param>
        private void GetSelectingRowIndexs(int rowIndex)
        {
            try
            {
                selectedRowIndex.Clear();
                if (id_groupProd_num_combo != null)
                {

                    for (int i = 0; i < id_groupProd_num_combo.GetLength(0); i++)
                    {
                        if (selectedGroupProd == id_groupProd_num_combo[i, 1])
                        {
                            selectedRowIndex.Add(i);
                        }
                    }

                }


            }
            catch { }
        }

        /// <summary>
        /// 根据DataGridView中选中的行号索引对应的Group_prod得到需要选中的行数组
        /// </summary>
        /// <param name="selectedGroup_Prod">当前在DataGridView中选中的一个组合餐号</param>
        /// <returns></returns>
        private void GetSelectingRowIndexs(string selectedGroup_Prod)
        {
            selectedRowIndex.Clear();
            if (id_groupProd_num_combo != null)
            {

                for (int i = 0; i < id_groupProd_num_combo.GetLength(0); i++)
                {
                    if (selectedGroup_Prod == id_groupProd_num_combo[i, 1])
                    {
                        selectedRowIndex.Add(i);
                    }
                }
                //更改选择记录为当前记录
                this.selectedGroupProd = selectedGroup_Prod ;

            }

        }

        /// <summary>
        /// 垂直滚动条和顶部的距离
        /// </summary>
        /// <param name="i">行的索引</param>
        private void VerticalScrollToTop(int i)
        {
            this.dgvOrderMenu.FirstDisplayedScrollingRowIndex = i;

        }

        /// <summary>
        /// 是否出现垂直滚动条
        /// </summary>
        /// <returns>出现了返回true,否则返回false</returns>
        private bool IsVerticalScrollAppear()
        {
            if (this.dgvOrderMenu.ScrollBars.ToString() == "Vertical")
            {
                return true;
            }
            else
            {
                return false;
            }

        }

        #endregion

        #endregion

        #region//事件

        /// <summary>
        /// 点单控件加载事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OrderMenu_Load(object sender, EventArgs e)
        {
           
           
                this.dgvOrderMenu.Columns[0].HeaderText = "序";
                this.dgvOrderMenu.Columns[1].HeaderText = "商品名称";
                this.dgvOrderMenu.Columns[2].HeaderText = "单价";
                this.dgvOrderMenu.Columns[3].HeaderText = "数量";
                this.dgvOrderMenu.Columns[4].HeaderText = "折扣";
                this.dgvOrderMenu.Columns[5].HeaderText = "小计";
                SetSize();
            
           
        }

        /// <summary>
        /// DataGridView点击事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dgvOrderMenu_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            try
            {
                DataGridView dgv = (DataGridView)sender;
                if (e.RowIndex >= 0)
                {
                    GetGroup(e.RowIndex);
                    for (int i = 0; i < selectedRowIndex.Count; i++)
                    {
                        dgv.Rows[selectedRowIndex[i]].Selected = true;
                       
                    }
                   
                }
            }
            catch
            {

            }
        }

        //备餐按钮点击事件
        private void btnBeiCan_Click(object sender, EventArgs e)
        {
            try
            {
                if (Info.isCheckChit || Info.isCheckOut || Info.IsTiDan || mainForm.ReturenOrderMenu.DgvOrderMenu.Rows.Count == 0)
                {
                    if (mainForm.ReturenOrderMenu.DgvOrderMenu.Rows.Count == 0)
                    {
                        MessageBox.Show("无交易" + "状态下暂时不能进行此项操作！");
                        return;
                    }
                    if (Info.isCheckOut)
                    {
                        MessageBox.Show("结账" + "状态下暂时不能进行此项操作！");
                        return;
                    }
                    if (Info.isCheckChit)
                    {
                        MessageBox.Show("兑换券" + "状态下暂时不能进行此项操作！");
                        return;
                    }
                    if (Info.IsTiDan)
                    {
                        MessageBox.Show("提单" + "状态下暂时不能进行此项操作！");
                        return;
                    }
                }
                else
                {
                    BeicanClick();
                }
               
                

            }
            catch { }
 
        }

        // 商品的数量加一
        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (Info.isCheckChit || Info.isCheckOut || Info.IsTiDan || mainForm.ReturenOrderMenu.DgvOrderMenu.Rows.Count == 0)
            {
                if (mainForm.ReturenOrderMenu.DgvOrderMenu.Rows.Count == 0)
                {
                    MessageBox.Show("无交易" + "状态下暂时不能进行此项操作！");
                    return;
                }
                if (Info.isCheckOut)
                {
                    MessageBox.Show("结账" + "状态下暂时不能进行此项操作！");
                    return;
                }
                if (Info.isCheckChit)
                {
                    MessageBox.Show("兑换券" + "状态下暂时不能进行此项操作！");
                    return;
                }
                if (Info.IsTiDan)
                {
                    MessageBox.Show("提单" + "状态下暂时不能进行此项操作！");
                    return;
                }
            }
            else
            {
                //触发事件的方法
                AlterProd(this.selectedGroupProd, 0, 0);

                if (this.mainForm.Beican != null && this.mainForm.Beican.IsNowBeiCan)
                {
                    this.mainForm.Beican.LoadInfo(Info.sale_id);
                }
                
            }
               
            
           
        }
        //商品数量减一
        private void btnMinuse_Click(object sender, EventArgs e)
        {
            if (Info.isCheckChit || Info.isCheckOut || Info.IsTiDan || mainForm.ReturenOrderMenu.DgvOrderMenu.Rows.Count == 0)
            {
                if (mainForm.ReturenOrderMenu.DgvOrderMenu.Rows.Count == 0)
                {
                    MessageBox.Show("无交易" + "状态下暂时不能进行此项操作！");
                    return;
                }
                if (Info.isCheckOut)
                {
                    MessageBox.Show("结账" + "状态下暂时不能进行此项操作！");
                    return;
                }
                if (Info.isCheckChit)
                {
                    MessageBox.Show("兑换券" + "状态下暂时不能进行此项操作！");
                    return;
                }
                if (Info.IsTiDan)
                {
                    MessageBox.Show("提单" + "状态下暂时不能进行此项操作！");
                    return;
                }
            }
            else
            {
                //触发事件的方法
                AlterProd(this.selectedGroupProd, 1,1);//我把number参数
                if (this.mainForm.Beican != null && this.mainForm.Beican.IsNowBeiCan)
                {
                    this.mainForm.Beican.LoadInfo(Info.sale_id);
                }
            }

        }
        /// <summary>
        /// 选中最后一个GroupProd下的商品
        /// </summary>
        private void SelectLastGroupProd()
        {

            GetSelectingRowIndexs(dgvOrderMenu.Rows.Count-1);

            GetGroup(dgvOrderMenu.Rows.Count-1);

                for (int i = 0; i < dgvOrderMenu.Rows.Count; i++)
                {
                    dgvOrderMenu.Rows[i].Selected = false;
                }
                for (int i = 0; i < selectedRowIndex.Count; i++)
                {
                    dgvOrderMenu.Rows[selectedRowIndex[i]].Selected = true;
                }
            
                
        }

        /// <summary>
        /// 选中在修改商品的数量、对商品打折、折让时的商品
        /// </summary>
        /// <param name="prodGroup">商品的prodGroup</param>
        private  void SelectProdGroup(string prodGroup)
        {
            GetSelectingRowIndexs(prodGroup);
            for (int i = 0; i < dgvOrderMenu.Rows.Count; i++)
            {
                dgvOrderMenu.Rows[i].Selected = false;
            }
            for (int i = 0; i < selectedRowIndex.Count; i++)
            {
                dgvOrderMenu.Rows[selectedRowIndex[i]].Selected = true;
            }
        }

        //选中商品下移一位
        private void btnDown_Click(object sender, EventArgs e)
        {
            try
            {

                GetSelectingRowIndexs(selectedGroupProd);

                int index = selectedRowIndex[selectedRowIndex.Count - 1] + 1;
                if (index < dgvOrderMenu.Rows.Count)
                {
                    GetGroup(index);

                    for (int i = 0; i < dgvOrderMenu.Rows.Count; i++)
                    {
                        dgvOrderMenu.Rows[i].Selected = false;
                    }
                    for (int i = 0; i < selectedRowIndex.Count; i++)
                    {
                        dgvOrderMenu.Rows[selectedRowIndex[i]].Selected = true;
                    }
                    if (IsVerticalScrollAppear())
                    {
                        VerticalScrollToTop(index);
                    }
                }
            }
            catch { }
        }

        //选中商品上移一位
        private void btnUp_Click(object sender, EventArgs e)
        {
            try
            {
                GetSelectingRowIndexs(selectedGroupProd);

                int index = selectedRowIndex[0] - 1;
                if (index >= 0)
                {
                    GetGroup(index);

                    for (int i = 0; i < dgvOrderMenu.Rows.Count; i++)
                    {
                        dgvOrderMenu.Rows[i].Selected = false;
                    }
                    for (int i = 0; i < selectedRowIndex.Count; i++)
                    {
                        dgvOrderMenu.Rows[selectedRowIndex[i]].Selected = true;
                    }
                    if (IsVerticalScrollAppear())
                    {
                        
                        VerticalScrollToTop(index);
                    }
                }
            }
            catch { }
        }

        //控件大小改变事件
        private void OrderMenu_SizeChanged(object sender, EventArgs e)
        {
            SetSize();
        }
        #endregion


       


    }
}
