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
/*数字按钮控件 运用于数字选取
 类说明： 数字控件，获取用户输入的数字，并记录到程序的配置信息类Info中。
主要事件：
（1）、BtnClick，数字按钮点击事件
（2）、TxtInput_TextChanged，输入数字改变时引发的事件，把用户输入的数字记录到Info类中的静态字段中。

 */
namespace POS.Controls
{
    /// <summary>
    /// 数字按钮控件
    /// </summary>
    public partial class Number : UserControl
    {

        #region//自动生成的代码
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
            this.SuspendLayout();
            // 
            // Number
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Name = "Number";
            this.Size = new System.Drawing.Size(163, 394);
            this.SizeChanged += new System.EventHandler(this.NumberPanel_SizeChanged);
            this.ResumeLayout(false);

        }
        #endregion
        #endregion

        /// <summary>
        /// 构造方法
        /// </summary>
        public Number()
        {
            InitializeComponent();
            IniButtons();
        }

        #region//字段
        /// <summary>
        /// 输入文本框
        /// </summary>
        private TextBox txtInput = new TextBox();

        /// <summary>
        /// 定义按钮数组
        /// </summary>
        private System.Windows.Forms.Button[] buttons = new Button[14];

        /// <summary>
        /// 传递主窗体
        /// </summary>
        private MainForm mainForm;

         /// <summary>
        /// 给每个button设置不同的显示的文字
        /// </summary>
        private string[] stringArray;

        /// <summary>
        /// 设置面板上button的统一颜色
        /// </summary>
        private Color color;

        /// <summary>
        /// 数字控件记录当前系统的功能
        /// </summary>
        private string functionName = "";

        #endregion

        #region//属性

        
        /// <summary>
        /// 传递主窗体
        /// </summary>
        public MainForm MainForm
        {
            get { return mainForm; }
            set { mainForm = value; }
        }


        /// <summary>
        /// 设置textBox的font
        /// </summary>
        [Description("设置textBox的font"), Category("自定义")]
        public Font TextBoxFont
        {
            get { return this.txtInput.Font; }
            set
            {
                this.txtInput.Font = value;
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
                if (buttons != null && stringArray != null)
                {

                    for (int i = 0; i < stringArray.Length && i < buttons.Length; i++)
                    {
                        buttons[i].Text = stringArray[i];

                    }
                }
            }
        }

        /// <summary>
        /// 设置button的font
        /// </summary>
        [Description("设置button的font"), Category("自定义")]
        public Font ButtonFont
        {
            get { return this.buttons[0].Font; }
            set
            {
                for (int i = 0; i < buttons.Length; i++)
                    this.buttons[i].Font = value;


            }
        }

        /// <summary>
        /// 设置面板上button的统一颜色
        /// </summary>
        [Description("设置面板上button的统一颜色"), Category("自定义")]
        public Color AllBtnColor
        {
            get { return color; }
            set
            {

                color = value;
                for (int i = 0; i < buttons.Length; i++)
                {
                    this.buttons[i].BackColor = value;
                }


            }
        }

        /// <summary>
        /// 获取数字控件Number的输入值
        /// </summary>
        [Description("获取数字控件Number的输入值"), Category("自定义")]
        public string TextBoxText
        {
            get { return this.txtInput.Text; }
            set { this.txtInput.Text = value; }
        }
        
        /// <summary>
        /// 数字控件记录当前系统的功能
        /// </summary>
        [Description("数字控件记录当前系统的功能"), Category("自定义")]
        public string FunctionName
        {
            get { return functionName; }
            set 
            { 
                functionName = value; 
                if(buttons!=null)
                {
                   this.buttons[12].Text = value;
                }
            }
        }


        #endregion

        #region//方法
        /// <summary>
        /// 初始化面板上的按钮
        /// </summary>
        private void IniButtons()
        {

            for (int i = 0; i < buttons.Length; i++)
            {
                buttons[i] = new Button();
                buttons[i].Font = new System.Drawing.Font("宋体", 12.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
                //给每个button的tag做标记
                this.buttons[i].Tag = i.ToString();
                this.buttons[i].Margin = new Padding(0, 0, 0, 0);
                //给buttons[i]注册事件BtnClick
                this.buttons[i].Click += new EventHandler(BtnClick);
                //将buttons[i]加入控件
                this.Controls.Add(buttons[i]);
            }
            this.txtInput.Location = new Point(0, 0);
            this.txtInput.Margin = new Padding(0, 0, 0, 0);
            this.txtInput.SizeChanged += new EventHandler(txtInput_SizeChanged);
            this.txtInput.TextChanged += new System.EventHandler(this.TxtInput_TextChanged);
            this.Controls.Add(txtInput);
            SetSize();


        }
      
        /// <summary>
        /// 设置面板上button的位置和大小
        /// </summary>
        private void SetSize()
        {
            int n = 0;
            //改变坐标
            for (int r = 0; r < 5; r++)
            {

                for (int c = 0; c < 3; c++)
                {
                    if (r == 4 && c == 1)
                    {
                        this.buttons[n++].Location = new System.Drawing.Point((c + 1) * (this.Width / 3), r * ((this.Height - txtInput.Height) / 5) + txtInput.Height);
                    }
                    else
                    {
                        if (n < buttons.Length - 1)
                            this.buttons[n++].Location = new System.Drawing.Point(c * (this.Width / 3), r * ((this.Height - txtInput.Height) / 5) + txtInput.Height);
                    }
                }
            }
            //改变大小
            for (int i = 0; i < buttons.Length; i++)
            {
                if (i == 12)
                {
                    this.buttons[i].Size = new Size(this.Width / 3 * 2, (this.Height - txtInput.Height) / 5);
                }
                else
                {
                    this.buttons[i].Size = new Size(this.Width / 3, (this.Height - txtInput.Height) / 5);
                }
            }
            this.txtInput.Size = new Size(this.Width, this.txtInput.Size.Height);
        }

        #endregion

        #region//事件

        /// <summary>
        /// Number控件大小改变事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void NumberPanel_SizeChanged(object sender, EventArgs e)
        {
            SetSize();
        }

        /// <summary>
        /// 输入数字改变时引发的事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TxtInput_TextChanged(Object sender, EventArgs e)
        {
                try
                {
                    if (this.txtInput.Text.Trim() == "")
                    {
                        Info.inputNumber = 0;
                    }
                    else
                    {
                        Info.inputNumber = Convert.ToDecimal(this.txtInput.Text.Trim());
                        if (Info.isCheckOut)
                        {                            
                            if (0 < Info.inputNumber)
                            {
                            }
                            else
                            {
                                MessageBox.Show("输入值不能为负值！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                this.txtInput.Text = "";
                            }
                        }
                    }
                }
                catch
                {
                    
                      
                        this.txtInput.Text = "";
                        
                }

           
        }

        /// <summary>
        /// 数字按钮点击事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnClick(Object sender, EventArgs e)
        {

            Button btn = (Button)sender;
            //根据数字按钮上的数字分别执行相应的操作
            bool b = true;
            switch (btn.Text)
            {
                case "1": this.txtInput.Text += "1"; b = false ; break;
                case "2": this.txtInput.Text += "2"; b = false; break;
                case "3": this.txtInput.Text += "3"; b = false; break;
                case "4": this.txtInput.Text += "4"; b = false; break;
                case "5": this.txtInput.Text += "5"; b = false; break;
                case "6": this.txtInput.Text += "6"; b = false; break;
                case "7": this.txtInput.Text += "7"; b = false; break;
                case "8": this.txtInput.Text += "8"; b = false; break;
                case "9": this.txtInput.Text += "9"; b = false; break;
                case "0": this.txtInput.Text += "0"; b = false; break;
                case ".": this.txtInput.Text += "."; b = false; break;
                case "修改":
                    if (this.txtInput.Text != "")
                    {
                        this.txtInput.Text = this.txtInput.Text.Substring(0, this.txtInput.Text.Length - 1); b = false;
                    }
                    break;
                case "清除": this.txtInput.Text = ""; b = false; break;

             
            }
            if (b)
            {
                this.mainForm.FunctionPanel.BtnClick(btn.Text);
            }
        }
        /// <summary>
        /// 数组改变大小时引发的事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtInput_SizeChanged(Object sender, EventArgs e)
        {

            SetSize();

        }

        #endregion

        #region//公共方法
        /// <summary>
        /// 清空数字框和Info中的inputNumber变量
        /// </summary>
        public void ClearNum()
        {
            this.txtInput.Text = "";
            Info.inputNumber = 0;
        }
        #endregion

    }
}
