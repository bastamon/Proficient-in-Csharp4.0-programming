using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
//数据同步窗体
/*主要方法：
（1）、ChangeState(string headText)更改进度条文本信息
（2）、Add(int i)增加进度条进度信息。参数： i 为进度值
*/
namespace POS.View
{
    /// <summary>
    /// 数据同步
    /// </summary>
    public partial class ProgressForm : Form
    {
        /// <summary>
        /// 
        /// </summary>
        public ProgressForm()
        {
            InitializeComponent();

        }
        /// <summary>
        /// 更改进度条文本名称
        /// </summary>
        /// <param name="headText">名称</param>
        public void ChangeState(string headText)
        {
            this.progressBar1.Value = 0;
            this.Text = headText;
        }
        /// <summary>
        /// 增加进度条进度
        /// </summary>
        /// <param name="i">进度值</param>
        public void Add(int i)
        {
            if (this.progressBar1.Value >= 100)
            {
                this.progressBar1.Value = 0;
            }
            else
            {
                try
                {
                    if (i > 0)
                    {
                        this.progressBar1.Value = this.progressBar1.Value + i;
                    }
                    else
                    {
                        this.progressBar1.Value++;
                    }
                }
                catch { this.progressBar1.Value = 0; }
            }
        }
    }
}
