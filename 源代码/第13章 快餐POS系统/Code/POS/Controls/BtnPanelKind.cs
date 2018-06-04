using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Runtime.Serialization;
using System.Security.Permissions;
/*商品类别面板 用于显示商品类别信息
 类1：BtnPanelKind
类说明：商品类别控件。
主要公共方法：
（1）、LoadInfo(int page,bool isLoad)，加载商品类别信息，page：页数，isLoad：是否初次加载。
（2）、LeftClick()，向左翻页方法。
（3）、RightClick()，向右翻页方法。
（4）、SetInfoAfterClicked(string dep_id)，商品类别按钮点击后调用的方法，dep_id：商品类别ID
委托：
（1）、GetInfoEventHandler，商品种类面板加载信息
（2）、SetInfoEventHandler，点击商品类别按钮后引发的事件
类2：ButtonDIY_Kind
类的说明：自定义按钮
事件：ButtonDIY_Click，按钮点击事件，点击后调用BtnPanelKind类中的方法进行处理。

 */
namespace POS.Controls
{
    /// <summary>
    /// 商品种类面板
    /// </summary>
    public partial class BtnPanelKind : UserControl
    {
        /// <summary>
        /// 得到一个BtnPanelKind实例
        /// </summary>
        /// <returns>BtnPanelKind实例</returns>
        public static BtnPanelKind InitButtonPanel() { return new BtnPanelKind(); }


        /// <summary>
        /// 构造方法
        /// </summary>
        public BtnPanelKind()
        {
            InitializeComponent();
            this.ColumnRow = new Size(1, 1);
            this.SetVisibleNumber(1);
        }

        #region//委托

        /// <summary>
        /// 定义一个委托
        /// </summary>
        /// <param name="btnPanelKind">商品类别控件对象</param>
        /// <param name="isLoad">是否为给按钮面板加载信息（若是isLoad=true,若翻页时调用则isLoad=false）</param>
        public delegate void GetInfoEventHandler(BtnPanelKind btnPanelKind, bool isLoad);


       /// <summary>
       /// 定义一个委托
       /// </summary>
       /// <param name="dep_id">商品类别</param>
       public delegate void SetInfoEventHandler(string dep_id);

        /// <summary>
        /// 商品种类面板加载信息
        /// </summary>
       [Description(" 商品种类面板加载信息"),Category("自定义")]
        public event GetInfoEventHandler GetInfo;

       /// <summary>
       /// 点击商品类别按钮后引发的事件
       /// </summary>
       [Description(" 点击商品类别按钮后引发的事件"), Category("自定义")]
       public event SetInfoEventHandler SetInfo;

        #endregion

        #region////Designer(自动生成的代码)
       /// <summary> 
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;


        /// <summary> 
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region 组件设计器生成的代码

        /// <summary> 
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 435F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 435F));
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 1;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 435F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(284, 337);
            this.tableLayoutPanel1.TabIndex = 0;

            // 
            // BtnPanelKind
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tableLayoutPanel1);
            this.Name = "BtnPanelKind";
            this.Size = new System.Drawing.Size(284, 337);
            this.SizeChanged += new System.EventHandler(this.ButtonPanel_SizeChanged);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;

        #endregion

        #region//字段
        public DataSet datasetProdKind;
        /// <summary>
        /// 一页的首个按钮的商品类别id(first 表示主餐类，进系统默认显示主餐类）
        /// </summary>
        public string pageFirstDept_id="first";
        /// <summary>
        ///RichButtonDIY数组（数组的大小等于一个页面上允许的最大button数）
        /// </summary>
        private ButtonDIY_Kind[] buttonDIY;

        /// <summary>
        /// 当前类别面板的页数(从0开始)
        /// </summary>
        private int page = 0;

        /// <summary>
        /// 按钮点击后颜色是否改变
        /// </summary>
        private bool isBtnColorChange = false;

        /// <summary>
        /// 翻页控件
        /// </summary>
        private PageControl pageControl;

        /// <summary>
        /// 商品面板的列数和行数
        /// </summary>
        private Size columnRow = new Size(4, 4);

        /// <summary>
        /// 给每个button设置不同的显示的文字
        /// </summary>
        private string[] stringArray;

        /// <summary>
        /// button的总数量（如果有两页，则是两页button的数量和）
        /// </summary>
        private int totalBtn = 16;

        #endregion

        #region//属性

        /// <summary>
        ///RichButtonDIY数组（数组的大小等于一个页面上允许的最大button数）
        /// </summary>
        public ButtonDIY_Kind[] ButtonDIY
        {
            get { return buttonDIY; }
            set { buttonDIY = value; }
        }

        /// <summary>
        /// 当前类别面板的页数(从0开始)
        /// </summary>
        public int Page
        {
            get { return page; }
            set { page = value; }
        }

        
        /// <summary>
        /// 按钮点击后颜色是否改变
        /// </summary>
        [Description("设置按钮点击后颜色是否改变"), Category("自定义")]
        public bool IsBtnColorChange
        {
            get { return isBtnColorChange; }
            set
            {

                this.isBtnColorChange = value;
                if (buttonDIY != null)
                {
                    for (int i = 0; i < buttonDIY.Length; i++)
                    {
                        buttonDIY[i].IsBtnColorChange = value;
                    }
                }
            }
        }

        /// <summary>
        /// 翻页控件
        /// </summary>
        [Description("绑定翻页控件"), Category("自定义")]
        public PageControl PageControl
        {
            get { return this.pageControl; }
            set { this.pageControl = value; }
        }

        /// <summary>
        /// 返回一个页面上允许的按钮最大数
        /// </summary>
        public int PageBtnNum
        {
            get { return this.columnRow.Width * this.columnRow.Height; }
        }
   
        /// <summary>
        /// 商品面板的列数和行数
        /// </summary>
        [Description("设置商品面板的列数和行数(width代表行，height代表列)"), Category("自定义")]
        public Size ColumnRow
        {
            get { return columnRow; }//tableLayoutPanel1.ColumnCount, tableLayoutPanel1.RowCount
            set
            {
                //将size大小设置为指定的值
                columnRow.Width = value.Width;
                columnRow.Height = value.Height;

                int btnNumber = value.Height * value.Width;

                //清空列的Style
                this.tableLayoutPanel1.ColumnStyles.Clear();
                //清空行的Style
                this.tableLayoutPanel1.RowStyles.Clear();

                //设置tableLayoutPanel1的行数和列数
                tableLayoutPanel1.ColumnCount = value.Width;
                tableLayoutPanel1.RowCount = value.Height;

                //设置行的Style
                for (int row = 0; row < value.Height; row++)
                {
                    this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 1 / (value.Height)));
                }

                //设置列的Style
                for (int column = 0; column < value.Width; column++)
                {
                    this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 1 / (value.Width)));
                }



                //清空tableLayoutPanel1中的所有控件
                this.tableLayoutPanel1.Controls.Clear();

                //若richButtonDIY不为null,清空原来button数组占的资源
                if (buttonDIY != null)
                {
                    for (int i = 0; i < buttonDIY.Length; i++)
                    {
                        buttonDIY[i].Dispose();
                    }
                }

                //创建button数组
                buttonDIY = new ButtonDIY_Kind[btnNumber];
                stringArray = new string[btnNumber];

                for (int i = 0; i < btnNumber; i++)
                {
                    buttonDIY[i] = ButtonDIY_Kind.InitRichButtonDIY();

                }

                //将每一个button加入tableLayoutPanel1的指定位置
                for (int row = 0; row < value.Height; row++)
                {
                    for (int column = 0; column < value.Width; column++)
                    {
                        //逐行从左往右加
                        this.tableLayoutPanel1.Controls.Add(this.buttonDIY[row * value.Width + column], column, row);
                    }
                }

                SetSize();
                //设置button的总数，初始值为16
                TotalBtn = totalBtn;

            }
        }

        /// <summary>
        /// 给每个button设置不同的显示的文字
        /// </summary>
        [Description("给每个button设置显示的文字，第一行是第一个button，第二行是第二个button,依次类推"), Category("自定义")]
        public string[] StringArray
        {
            get { return stringArray; }
            set
            {
                stringArray = value;
                if (buttonDIY != null && stringArray != null)
                {

                    for (int i = 0; i < stringArray.Length; i++)
                    {
                        buttonDIY[i].Button.Text = stringArray[i];

                    }
                }
            }
        }

        /// <summary>
        /// button的总数量（如果有两页，则是两页button的数量和）
        /// </summary>
        [Description("button的总数量"), Category("自定义")]
        public int TotalBtn
        {
            get { return totalBtn; }//(visibleNum > buttonNum) ? visibleNum : buttonNum;
            set
            {
                totalBtn = value;
                if (pageControl != null)
                {
      
                        if (value > columnRow.Width * columnRow.Height)
                        {
                            this.SetVisibleNumber(columnRow.Width * columnRow.Height);
                            this.pageControl.picKindLeft.Visible = true;
                            this.pageControl.picKindRight.Visible = false;
                        }
                        else
                        {
                            this.SetVisibleNumber(value);
                            this.pageControl.picKindRight.Visible = false;
                            this.pageControl.picKindLeft.Visible = false;
                        }
                
                  
                }
            }
        }


        #endregion

        #region//方法

        #region//公共方法

        /// <summary>
        /// 获取指定编号的按钮的商品类别
        /// </summary>
        /// <param name="index">索引</param>
        /// <returns>string 类别编号</returns>
        public string RetrunDept_id(int index)
        {
            if (index < this.TotalBtn)
            {
                try
                {
                    return this.datasetProdKind.Tables[0].Rows[index]["dep_id"] + "";
                }
                catch { return "first"; }
            }
            else
            {
                return "";
            }
        }

        /// <summary>
        /// 加载商品类别信息,供主界面调用
        /// </summary>
        /// <param name="page">页码数</param>
        /// <param name="isLoad">初次加载时为true,翻页时为false</param>
        public void LoadInfo(int page,bool isLoad)
        {
            this.page = page;
            try
            {

                //事件触发方法
                GetInfo(this,isLoad);
                //事件触发方法
                SetInfo(this.pageFirstDept_id);
            }
            catch { MessageBox.Show("没有注册商品类别面板加载信息的事件或者根据该商品类别下面没有商品"); }


        }

        /// <summary>
        /// 商品类别按钮点击后调用的方法
        /// </summary>
        /// <param name="dep_id">类别编号</param>
        public void SetInfoAfterClicked(string dep_id)
        {
            try
            {

                //事件触发方法
                SetInfo(dep_id);
            }
            catch { MessageBox.Show("没有注册商品类别面板加载信息的事件或者根据该商品类别下面没有商品"); }



        }


        /// <summary>
        /// 参数设定时调用，以改变功能面板上的文字信息
        /// </summary>
        public void ReloadFunc()
        {
            if (pageControl != null)
            {
                page = 0;
                //一页容纳的按钮总数
                int btnNumber = columnRow.Width * columnRow.Height;
                if ((TotalBtn / btnNumber) > 0)
                {
                    this.SetVisibleNumber(btnNumber);
                    this.pageControl.picKindRight.Visible = false;
                    this.pageControl.picKindLeft.Visible = true;
                }
                else
                {
                    this.pageControl.picKindRight.Visible = false;
                    this.pageControl.picKindLeft.Visible = false;
                }

                LoadInfo(page, false);
            }
        }



        /// <summary>
        /// 向左翻页方法
        /// </summary>
        public void LeftClick()
        {

            if (pageControl != null)
            {
                //表示
                this.pageControl.picKindLeft.Visible = true;
                //一页容纳的按钮总数
                int btnNumber = columnRow.Width * columnRow.Height;
                //总共的按钮数
                int totalButon = this.TotalBtn;
                //页码数加一
                page--;
                if (page < (totalButon / btnNumber))
                {
                    this.SetVisibleNumber(btnNumber);
                    //翻到第一页让右按钮消失
                    if (page == 0)
                    {
                        this.pageControl.picKindRight.Visible = false;

                    }
                }

                LoadInfo(page,false);

            }


        }







        /// <summary>
        /// 向右翻页方法
        /// </summary>
        public void RightClick()
        {

            if (pageControl != null)
            {

                this.pageControl.picKindRight.Visible = true;

                //一页容纳的按钮总数
                int btnNumber = columnRow.Width * columnRow.Height;
                //页码数加一
                page++;
                if (page < (totalBtn / btnNumber))//--------------------------
                {

                    //翻到最后一页页让左按钮消失
                    if ((page + 1) * btnNumber == TotalBtn)
                    {
                        this.pageControl.picKindLeft.Visible = false;

                    }
                }
                if (page == (TotalBtn / btnNumber))
                {
                    this.SetVisibleNumber(TotalBtn - btnNumber * page);
                    this.pageControl.picKindLeft.Visible = false;

                }

                LoadInfo(page,false);


            }


        }


        #endregion

        #region//私有方法


        /// <summary>
        /// 设置面板上button的大小
        /// </summary>
        private void SetSize()
        {
            int rows = this.columnRow.Height;
            int collumns = this.columnRow.Width;
            for (int i = 0; i < buttonDIY.Length; i++)
            {
                this.buttonDIY[i].Size = new Size(this.tableLayoutPanel1.Width / collumns - 6, this.tableLayoutPanel1.Height / rows - 6);
            }

        }


        /// <summary>
        /// button面板上显示button的数量
        /// </summary>
        /// <param name="number">button面板上显示button的数量</param>
        private void SetVisibleNumber(int number)
        {

            for (int i = 0; i < number; i++)
            {

                try
                {
                    buttonDIY[i].Visible = true;
                }
                catch { }


            }
            for (int i = number; i < columnRow.Width * columnRow.Height; i++)
            {
                try
                {
                    buttonDIY[i].Visible = false;
                }
                catch { }

            }

        }

        #endregion

        #endregion

        #region//事件
        /// <summary>
        /// 改变大小
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ButtonPanel_SizeChanged(object sender, EventArgs e)
        {
            this.tableLayoutPanel1.Size = new Size(this.Size.Width, this.Size.Height);
            SetSize();
        }

        #endregion

    }





    /// <summary>
    /// 自定义按钮
    /// </summary>
    public class ButtonDIY_Kind : UserControl
    {
        /// <summary>
        /// 得到一个ButtonDIY_Kind对象
        /// </summary>
        /// <returns>ButtonDIY_Kind对象</returns>
        public static ButtonDIY_Kind InitRichButtonDIY() { return new ButtonDIY_Kind(); }

        /// <summary>
        /// 构造方法
        /// </summary>
        public ButtonDIY_Kind()
        {
            InitializeComponent();
        }

        #region//Designer
        /// <summary> 
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        /// <summary>
        /// button1
        /// </summary>
        private System.Windows.Forms.Button button1;


        #region 组件设计器生成的代码

        /// <summary> 
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.button1 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.button1.Dock = System.Windows.Forms.DockStyle.Fill;
            //this.button1.Location = new System.Drawing.Point(0, 0);
            this.button1.Name = "button1";
            //this.button1.Size = new System.Drawing.Size(107, 67);
            this.button1.TabIndex = 0;
            this.button1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.button1.UseVisualStyleBackColor = false;
            this.button1.Click += new System.EventHandler(this.ButtonDIY_Click);
            // 
            // RichButtonDIY
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.button1);
            this.Name = "RichButtonDIY";
            //this.Size = new System.Drawing.Size(107, 67);
            this.ResumeLayout(false);

        }

        #endregion
        #endregion


        #region//字段

        /// <summary>
        /// 绑定BtnPanelKind控件
        /// </summary>
        private BtnPanelKind btnPanelKind;

        /// <summary>
        /// 商品种类
        /// </summary>
        private string dep_id;

        /// <summary>
        /// 按钮点击后颜色是否改变
        /// </summary>
        private bool isBtnColorChange = false;

        /// <summary>商品ID号
        /// 商品ID号
        /// </summary>
        private string prod_id = "";

        /// <summary>商品数量
        /// 商品数量
        /// </summary>
        private int prodNumber;

        #endregion


        #region//属性

        /// <summary>
        /// 绑定BtnPanelKind控件
        /// </summary>
        public BtnPanelKind BtnPanelKind
        {
            get { return btnPanelKind; }
            set { btnPanelKind = value; }
        }

        /// <summary>
        /// 商品种类
        /// </summary>
        public string Dep_id
        {
            get { return dep_id; }
            set { dep_id = value; }
        }

        
        /// <summary>
        /// 按钮点击后颜色是否改变
        /// </summary>
        [Description("设置按钮点击后颜色是否改变"), Category("自定义")]
        public bool IsBtnColorChange
        {
            get { return isBtnColorChange; }
            set { this.isBtnColorChange = value; }

        }

        /// <summary>得到button的引用
        /// 得到button的引用
        /// </summary>
        public Button Button
        {
            get { return this.button1; }
            set { }
        }

        /// <summary>商品ID号
        /// 商品ID号
        /// </summary>
        public string Prod_id
        {
            get{ return prod_id; }
            set{  prod_id = value; }
        }

        
        /// <summary>商品数量
        /// 商品数量
        /// </summary>     
        public int ProdNumber
        {
            get { return prodNumber; }

            set { prodNumber = value; }
        }

        /// <summary>设置button上显示的文字
        /// 设置button上显示的文字
        /// </summary>
        public string SetText
        {

            get { return this.button1.Text; }
            set { this.button1.Text = value; }
        }

        // public  SetShape
        #endregion


        #region//事件
       /// <summary>
       /// 按钮点击事件
       /// </summary>
       /// <param name="sender"></param>
       /// <param name="e"></param>
        private void ButtonDIY_Click(object sender, EventArgs e)
        {
            if (btnPanelKind != null)
            {
                
                //把本对象的索引传到btnPanelKind
                //btnPanelKind.LastClick = this.lastClick;
                btnPanelKind.SetInfoAfterClicked(dep_id);
               
                if (isBtnColorChange)
                {
                    if (this.BackColor == Color.Coral)
                    {
                        this.BackColor = Color.White;
                    }
                    else
                    {
                        this.BackColor = Color.Coral;
                    }
                }
                
            }
        }

        #endregion
    }

}
