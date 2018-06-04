using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using POS.View;
/*用于商品类别面板的翻页和商品的翻页
 类说明： 翻页控件（负责商品类别面板和商品面板的翻页）
主要事件：
（1）、picKindLeft_Click，商品类别面板向左翻页
（2）、picKindRight_Click，商品类别面板向右翻页
（3）、picProdLeft_Click，商品面板向左翻页
（4）、picProdRight_Click，商品面板向右翻页

 */
namespace POS.Controls
{
    /// <summary>
    /// 翻页控件
    /// </summary>
    public partial class PageControl : UserControl
    {

        /// <summary>
        /// 构造方法
        /// </summary>
        public PageControl()
        {
            InitializeComponent();
        }

        #region//字段


       
        /// <summary>
        /// 种类面板对象
        /// </summary>
        private BtnPanelKind buttonPanelKind;
        /// <summary>
        /// 商品面板对象
        /// </summary>
        private BtnPanelProd buttonPanelProd;
        /// <summary>
        /// 左右翻页按钮离左右边界的距离
        /// </summary>
        private int distance = 10;

        #endregion

        #region//方法

        /// <summary>
        /// 窗体中的控件大小发生改变时重新设置它们的位置
        /// </summary>
        private void ReLocation()
        {
            ////改变大小时设置字体的大小
            //this.lblKind.Location = new Point(this.Width * 533 / 784, this.Height * 6 / 29);
            //this.lblKind.Font = new Font("宋体", (float)(this.Width * 0.003 + this.Height * 0.5) - 2);

            //this.lblProduct.Location = new Point(this.Width * 533 / 784, this.Height * 6 / 29);
            //this.lblProduct.Font = new Font("宋体", (float)(this.Width * 0.003 + this.Height * 0.5) - 2);

            picKindLeft.Location = new Point(distance, 0);
            lblKind.Location = new Point(picKindLeft.Width + distance, 0);
            picKindRight.Location = new Point(picKindLeft.Width + distance + lblKind.Width, 0);

            picProdLeft.Location = new Point(this.Width - picProdRight.Width - lblProduct.Width - picProdLeft.Width - distance, 0);
            lblProduct.Location = new Point(this.Width - lblProduct.Width - picProdRight.Width - distance, 0);
            picProdRight.Location = new Point(this.Width - picProdRight.Width - distance, 0);
            this.Height = this.lblKind.Height;

         

        }
        #endregion

        #region//属性



        /// <summary>
        /// 绑定种类面板对象
        /// </summary>
        [Description("绑定种类面板对象"), Category("自定义")]
        public BtnPanelKind  ButtonPanelKind
        {
            get { return buttonPanelKind; }
            set { this.buttonPanelKind = value; }
        }

        /// <summary>
        /// 绑定商品面板对象
        /// </summary>
       [Description("绑定商品面板对象"), Category("自定义")]
        public BtnPanelProd ButtonPanelProd
        {
            get { return buttonPanelProd; }
            set { this.buttonPanelProd = value; }
        }

        /// <summary>
        /// 设置左右翻页按钮离左右边界的距离
        /// </summary>
       [Description ("设置左右翻页按钮离左右边界的距离"),Category ("自定义")]
        public int SetDistance
        {
            get { return distance; }
            set 
            {
                if (value < (this.Width - lblKind.Width - lblProduct.Width - picKindLeft.Width - picKindRight.Width - picProdLeft.Width - picProdRight.Width) / 2)
                {
                    distance = value;
                    ReLocation();
                }
            
            }
        }

        /// <summary>
        /// 设置类别和商品这四个字的字体
        /// </summary>
        [Description("设置类别和商品这四个字的字体"), Category("自定义")]
        public Font TextFont
        {
            get {return this.lblKind.Font; }
            set
            {
                this.lblKind.Font = value; this.lblProduct.Font = value;
                ReLocation();
            }
        }

        /// <summary>
        /// 设置整个控件的背景颜色
        /// </summary>
       [Description("设置整个控件的背景颜色"), Category("自定义")]
        public Color BackgroundColor
        {
            get { return this .BackColor; }
            set
            {
               
                this.picKindLeft.BackColor = value;
                this.picKindRight.BackColor = value;
                this.lblKind.BackColor = value;
                this.picProdLeft.BackColor = value;
                this.lblProduct.BackColor = value;
                this.picProdRight.BackColor = value;
                 this.BackColor = value;
            }

        }
        #endregion

        #region//翻页按钮事件
       //System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PageControl));
        #region//鼠标移到上面发生的事件
        //左侧类鼠标离开事件
       private void picKindLeft_MouseLeave(object sender, EventArgs e)
        {
            this.picKindLeft.BackgroundImage = POS.Properties.Resources.右箭头; 
            picKindLeft.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
        }
        //右侧类鼠标离开事件
        private void picKindRight_MouseLeave(object sender, EventArgs e)
        {
            this.picKindRight.BackgroundImage = POS.Properties.Resources.左箭头; ;
            picKindRight.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
        }
        //左侧商品鼠标离开事件
        private void picProdLeft_MouseLeave(object sender, EventArgs e)
        {
            this.picProdLeft.BackgroundImage = POS.Properties.Resources.右箭头; ;
            picProdLeft.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
        }
        //右侧商品鼠标离开事件
        private void picProdRight_MouseLeave(object sender, EventArgs e)
        {
            this.picProdRight.BackgroundImage = POS.Properties.Resources.左箭头; ;
            picProdRight.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
        }
        #endregion

        #region//鼠标离去发生的事件
        //左侧鼠标点击事件
        private void picKindLeft_MouseEnter(object sender, EventArgs e)
        {
            picKindLeft.BackgroundImage = POS.Properties.Resources.右发光箭头;
            picKindLeft.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
        }
        //右侧鼠标点击事件
        private void picKindRight_MouseEnter(object sender, EventArgs e)
        {
            picKindRight.BackgroundImage = POS.Properties.Resources.左发光箭头; ;
            picKindRight.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
        }
        //左侧商品鼠标点击事件
        private void picProdLeft_MouseEnter(object sender, EventArgs e)
        {
            picProdLeft.BackgroundImage = POS.Properties.Resources.右发光箭头;
            picProdLeft.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
        }
        //右侧商品鼠标点击事件
        private void picProdRight_MouseEnter(object sender, EventArgs e)
        {
            picProdRight.BackgroundImage = POS.Properties.Resources.左发光箭头;
            picProdRight.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
        }
        #endregion

        #region//鼠标点击发生的事件
        /// <summary>
        /// 商品类别面板向左翻页
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void picKindLeft_Click(object sender, EventArgs e)
        {
            //获取商品类别面板上第一个按钮上的Dept_id
            int i = (this.ButtonPanelKind.Page-1) * this.ButtonPanelKind.PageBtnNum;
            this.ButtonPanelKind.pageFirstDept_id = this.buttonPanelKind.RetrunDept_id(i);
            if (buttonPanelKind != null)
            {
                buttonPanelKind.LeftClick();
            }
        }
        /// <summary>
        /// 商品类别面板向右翻页
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void picKindRight_Click(object sender, EventArgs e)
        {
            //获取商品类别面板上第一个按钮上的Dept_id
            int i = (this.ButtonPanelKind.Page+1) * this.ButtonPanelKind.PageBtnNum;
            this.ButtonPanelKind.pageFirstDept_id=this.buttonPanelKind .RetrunDept_id (i);

            if (buttonPanelKind != null)
            {
                buttonPanelKind.RightClick();
            }
        }
        /// <summary>
        /// 商品面板向左翻页
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void picProdLeft_Click(object sender, EventArgs e)
        {

            if (buttonPanelProd != null)
            {
                buttonPanelProd.LeftClick();
            }
        }
        /// <summary>
        /// 商品面板向右翻页
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void picProdRight_Click(object sender, EventArgs e)
        {
            if (buttonPanelProd != null)
            {
                buttonPanelProd.RightClick();
            }
        }

      
        #endregion

        /// <summary>
        /// 翻页控件大小改变事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void PageControl_SizeChanged(object sender, EventArgs e)
        {
            ReLocation();
        }

        #endregion
    }
}
