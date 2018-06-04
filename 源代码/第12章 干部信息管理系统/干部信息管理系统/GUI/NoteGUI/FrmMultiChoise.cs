using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace HBMISR.GUI.NoteGUI
{
    /// <summary>
    /// 培养措施选择
    /// </summary>
    public partial class FrmMultiChoise : Form
    {
        public string choices = string.Empty;

        /// <summary>
        /// 培养措施选择构造函数
        /// </summary>
        public FrmMultiChoise()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 取消
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button2_Click(object sender, EventArgs e)
        {
            choices = string.Empty;
            this.Dispose();
        }

        /// <summary>
        /// 确定
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button1_Click(object sender, EventArgs e)
        {
            if (checkBox1.Checked == true)
                choices += label1.Text+" ";
            if (checkBox2.Checked == true)
                choices += label2.Text + " ";
            if (checkBox3.Checked == true)
                choices += label3.Text + " ";
            if (checkBox4.Checked == true)
                choices += label4.Text + " ";
            this.Dispose();
        }
    }
}
