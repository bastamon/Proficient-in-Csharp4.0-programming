using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
/*用于公告信息
 类说明：滚动字母控件
主要属性：
（1）、ShowText，获取和设置显示文字
（2）、IsRoll，是否让文字滚动

 */
namespace POS.Controls
{
    /// <summary>
    /// 滚动字母控件
    /// </summary>
    public partial class Roll : UserControl
    {
        /// <summary>
        /// 构造方法
        /// </summary>
        public Roll()
        {
            InitializeComponent();
        }

        #region//字段

        /// <summary>
        /// 当不滚动时设置文字的X坐标
        /// </summary>
        private int local_X;

        /// <summary>
        /// 设定文字是否滚动
        /// </summary>
        private bool isRoll = true;

        /// <summary>
        /// 设置滚动的字体名字
        /// </summary>
        private string fontName = "宋体";

        #endregion

        #region//属性

        /// <summary>
        /// 设置滚动的字体
        /// </summary>
        [Description("设置滚动的字体"), Category("自定义")]
        public string RollText
        {
            get { return this.lblRoll.Text; }
            set { this.lblRoll.Text = value; }
        }

        /// <summary>
        /// 当不滚动时设置文字的X坐标
        /// </summary>
        [Description("当不滚动时设置文字的X坐标"), Category("自定义")]
        public int Local_X1
        {
            get { return local_X; }
            set { local_X = value; this.lblRoll.Location = new Point(local_X, this.lblRoll.Location.Y); }
        }

        /// <summary>
        /// 设置显示的文字
        /// </summary>
        [Description("设置显示的文字"), Category("自定义")]
        public string ShowText
        {
            get { return this.lblRoll.Text; }
            set { this.lblRoll.Text = value; }
        }

        /// <summary>
        /// 设定文字是否滚动
        /// </summary>
        public bool IsRoll
        {
            get { return isRoll; }
            set { isRoll = value; }
        }

        /// <summary>
        /// 设置滚动的字体名字
        /// </summary>
         [Description("设置滚动的字体名字"), Category("自定义")]
        public string FontName
        {
            get { return fontName; }
            set { fontName = value; }
        }

        /// <summary>
        /// 设置滚动字母的背景颜色
        /// </summary>
        [Description("设置滚动字母的背景颜色"), Category("自定义")]
        public Color SetBackColor
        {
            get { return this.lblRoll.BackColor; }
            set { this.lblRoll.BackColor = value; }
        }

        /// <summary>
        /// 设置滚动字母的前景颜色
        /// </summary>
        [Description("设置滚动字母的前景颜色"), Category("自定义")]
        public Color SetForeColor
        {
            get { return this.lblRoll.ForeColor; }
            set { this.lblRoll.ForeColor = value; }
        }

        #endregion

        #region//事件

        /// <summary>
        /// lblRoll大小改变事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void lblRoll_SizeChanged(object sender, EventArgs e)
        {
            if (this.Height < this.lblRoll.Height)
                this.Height = this.lblRoll.Height;
        }
      
        /// <summary>
        /// Roll控件大小改变事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Roll_SizeChanged(object sender, EventArgs e)
        {
            try
            {
                lblRoll.Location = new Point(this.Width * 533 / 784, this.Height * 6 / 29);
                this.lblRoll.Font = new Font(fontName, (float)(this.Width * 0.003 + this.Height * 0.5) - 2);
            }
            catch { }


        }

        /// <summary>
        /// timer Tick事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void timer1_Tick(object sender, EventArgs e)
        {
            if (isRoll)
            {
                this.lblRoll.Location = new Point(this.lblRoll.Location.X - 3, 0);
                if (this.lblRoll.Location.X + this.lblRoll.Width < 0)
                {
                    this.lblRoll.Location = new Point(this.Width, 0);
                }
            }
        }

        #endregion

    }
}
