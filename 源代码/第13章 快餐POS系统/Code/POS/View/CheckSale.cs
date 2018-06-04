using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using POS.Service;
using POS.Controls;
// 用于查询销售单的详细信息 包括结账信息、产品信息。
/*主要事件：
（1）、CheckSale_Load(object sender, EventArgs e) 加载事件，用于界面初始化
主要方法：
（1）、CheckSale(MainForm mainform) 用于查询销售单
*/
namespace POS.View
{
    /// <summary>
    /// 查单窗体
    /// </summary>
    public partial class CheckSale : Form
    {
        private MainForm mainForm;
        /// <summary>
        /// 传递主窗体
        /// </summary>
        public MainForm MainForm
        {
            get { return mainForm; }
            set { mainForm = value; }
        }
        /// <summary>
        /// 获得查单控件
        /// </summary>
        public CheckSale0 CheckSale01
        {
            get { return checkSale01; }
        }
        /// <summary>
        /// 按钮窗体
        /// </summary>
        public static Form key_form;
        KeyBoard_Form kbf = new KeyBoard_Form();
        /// <summary>
        /// 查单
        /// </summary>
        /// <param name="mainform">主窗体</param>
        public CheckSale(MainForm mainform)
        {
            InitializeComponent();
            this.mainForm=mainform;
            this.Location = new Point(0, 0);
            this.checkSale01.MainForm = this.mainForm;
            //key_form =new KeyBoard_Form ();
        }
        //初始位置
        private void CheckSale_LocationChanged(object sender, EventArgs e)
        {
            this.Location = new Point(0, 0);
        }
        //加载事件
        private void CheckSale_Load(object sender, EventArgs e)
        {
            CheckSale0.CheckSale = this;
            SeeAboutSale.str = "";
            SeeAboutSale.numstr = "";
            this.MinimumSize = this.Size;
            this.Location = new Point(0,0);
        }
        //关闭
        private void CheckSale_FormClosed_1(object sender, FormClosedEventArgs e)
        {
            key_form.Close();
        }
    }
}
