using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using POS.Common;
using System.Security.Cryptography;
//用于更改员工密码信息
/*主要事件：
（1）、button1_Click(object sender, EventArgs e)确定事件，把用修改的正确密码更新至数据库
主要方法：
（1）、InfoToMD5(string info) 用于MD5加密数据信息
*/
namespace POS.View
{
    /// <summary>
    /// 用于更改员工密码信息
    /// </summary>
    public partial class ChangePassword : Form
    {
        /// <summary>
        /// 构造函数 创建修改密码窗体
        /// </summary>
        public ChangePassword()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 确定按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button1_Click(object sender, EventArgs e)
        {
            string str = "select * from employee where emp_id ="+Info.emp_id;
            DataSet ds = DBSql.SCreateDataSet(str);
            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                string md5PassDB = ds.Tables[0].Rows[0]["PASSWORD"].ToString();
                string oldmd5PassInput = InfoToMD5(oldPassWord.Text);
                string newMD5Pass = InfoToMD5(newPassWord.Text);
                if (md5PassDB.Equals(oldmd5PassInput))
                {
                    if (newPassWord.Text.Equals(confirm.Text))
                    {
                        if (newPassWord.Text.Trim() == "")
                        {
                            MessageBox.Show("密码不可为空","消息提示",MessageBoxButtons .OK ,MessageBoxIcon.Information );
                            return;
                        }
                        //往服务器中插入交班信息
                        ReadIni readIni = new ReadIni("config.ini");
                        string srvIp = readIni.ReadString("RepastErp", "txtServerIP");
                        string srvPort = readIni.ReadString("RepastErp", "txtPort");
                        string srvDBName = readIni.ReadString("RepastErp", "txtIPdataname");
                        string srvUserName = readIni.ReadString("RepastErp", "txtFTPuser");
                        string srvPassword = readIni.ReadString("RepastErp", "txtFTPpassword");
                        string sql = "update OPENDATASOURCE('SQLOLEDB','Data Source=" + srvIp + "," + srvPort + ";User ID=" + srvUserName + ";Password=" + srvPassword + "' )." + srvDBName + ".dbo.employee set PASSWORD='" + newMD5Pass + "' where emp_id=" + Info.emp_id;
                        string sql2 = "update employee set PASSWORD='" + newMD5Pass + "' where emp_id=" + Info.emp_id;
                        bool b = DBSql.SRunSQL(sql2);
                         b = DBSql.SRunSQL(sql);
                        if (b)
                        {
                            MessageBox.Show("密码修改成功！");
                          
                        }
                        else
                        {
                            MessageBox.Show("密码修改失败！");
                        }
                        oldPassWord.Text = "";
                        newPassWord.Text = "";
                        confirm.Text = "";
                        this.Dispose();
                    }
                    else
                    {
                        MessageBox.Show("新密码与确认密码不一致！");
                        oldPassWord.Text = "";
                        newPassWord.Text = "";
                        confirm.Text = "";
                    }
                }
                else
                {
                    MessageBox.Show("原始密码不正确！");
                    oldPassWord.Text = "";
                    newPassWord.Text = "";
                    confirm.Text = "";
                }
            }
            else
            {
                MessageBox.Show("该用户不存在！");
            }
           

        }
        /// <summary>
        /// 取消
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }


        #region 用MD5对信息加密
        /// <summary>
        /// 将给定信息进行MD5加密处理。
        /// </summary>
        /// <param name="info">加密前的信息</param>
        /// <returns>用MD5算法加密处理后的信息</returns>
        private string InfoToMD5(string info)
        {
            byte[] result = Encoding.Unicode.GetBytes(info);
            MD5 md5 = new MD5CryptoServiceProvider();
            byte[] output = md5.ComputeHash(result);
            string str = BitConverter.ToString(output).Replace("-", "");
            return str;
        }
        #endregion 
        /// <summary>
        /// 原始密码修改框
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void oldPassWord_TextChanged(object sender, EventArgs e)
        {

        }
        /// <summary>
        /// 新密码修改框
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void newPassWord_KeyUp(object sender, KeyEventArgs e)
        {
            if (Keys.Space ==e.KeyCode )
                this.newPassWord.Text = this.newPassWord.Text.Replace(" ", "");
        }
        /// <summary>
        /// 重复密码修改框
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void confirm_KeyUp(object sender, KeyEventArgs e)
        {
            if (Keys.Space == e.KeyCode)
                this.confirm .Text = this.confirm .Text.Replace(" ", "");
        }

    }
}
