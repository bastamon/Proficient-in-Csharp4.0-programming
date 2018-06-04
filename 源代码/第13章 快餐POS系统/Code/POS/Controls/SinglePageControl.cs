using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
/*用于组合餐窗台上翻页功能
 类说明： 翻页控件，尽在组合餐窗体中使用
主要方法：
（1）、picLeft_Click，向左翻页
（2）、picRight_Click，向右翻页

 */
namespace POS.Controls
{
    /// <summary>
    /// 翻页控件，在组合餐窗体上使用
    /// </summary>
    public partial class SinglePageControl : UserControl
    {
          /// <summary>
        /// 构造方法
        /// </summary>
        public SinglePageControl()
        {
            InitializeComponent();
        }

        #region 属性

        /// <summary>
        /// 设置整个控件的背景颜色
        /// </summary>
       [Description("设置整个控件的背景颜色"), Category("自定义")]
        public Color BackgroundColor
        {
            get { return this .BackColor; }
            set
            {
                this.picLeft.BackColor = value;
                this.picRight.BackColor = value;
                 this.BackColor = value;
            }
        }
        #endregion

        #region 翻页按钮事件
        #region 鼠标移到上面发生的事件
       private void picLeft_MouseLeave(object sender, EventArgs e)
        {
            this.picLeft.BackgroundImage = POS.Properties.Resources.右箭头; ;
            this.picLeft.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
        }

        private void picRight_MouseLeave(object sender, EventArgs e)
        {
            this.picRight.BackgroundImage = POS.Properties.Resources.左箭头; ;
            this.picRight.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
        }

     
        #endregion

        #region 鼠标离去发生的事件
        private void picLeft_MouseEnter(object sender, EventArgs e)
        {
            picLeft.BackgroundImage = POS.Properties.Resources.右发光箭头;
            this.picLeft.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
        }

        private void picRight_MouseEnter(object sender, EventArgs e)
        {
            picRight.BackgroundImage = POS.Properties.Resources.左发光箭头; ;
            this.picRight.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
        }

       
        #endregion
        #endregion
    }
}
