using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices;
using System.Windows.Forms;
////包含完成对数据库的所有基本操作：sql语句、存储过程等。程序配置信息 、操作Ini文件的类
namespace POS.Common
{
    /// <summary>
    /// 操作Ini文件的类
    /// </summary>
    public class ReadIni
    {
        /// <summary>
        /// 要操作的Ini文件的名字
        /// </summary>
        string fName = "config.ini";

        /// <summary>
        /// 构造方法
        /// </summary>
        public ReadIni()
        {
        }
        /// <summary>
        /// 构造方法
        /// </summary>
        /// <param name="fName">Ini文件的名字</param>
        public ReadIni(string fName)
        {
            this.fName = fName;
        }

        [DllImport("kernel32")]
        private static extern int GetPrivateProfileString(
            string section,  //ini文件中的段落
            string key,      //ini文件中的关键字
            string def,     //无法读取时的缺省数值
            StringBuilder retVal,       //读取数值
            int size,       //数值的大小   
            string fPath    //ini文件的完整路径和名称
            );
        [DllImport("kernel32")]
        private static extern int WritePrivateProfileString(
            string section,
            string key,
            string val,         //ini文件中关键字的值
            string fPath
            );
        /// <summary>
        /// 根据类别和关键字读取数据
        /// </summary>
        /// <param name="dept">Ini文件中的参数类别</param>
        /// <param name="key">关键字</param>
        /// <returns>关键字对应的值</returns>
        public string ReadString(string dept,string key)
        {
            string fPath = Application.StartupPath + "\\" + fName;
            StringBuilder temp = new StringBuilder(1024);
            GetPrivateProfileString(dept, key, "", temp, 1024, fPath);
            return temp.ToString();
        }
        /// <summary>
        /// 根据类别和关键往Ini文件中写数据
        /// </summary>
        /// <param name="dept">Ini文件中的参数类别</param>
        /// <param name="key">关键字</param>
        /// <param name="val">关键字对应的值</param>
        public void WriteString(string dept, string key, string val)
        {
            string fPath = Application.StartupPath + "\\" + fName;
            WritePrivateProfileString(dept, key, val, fPath);
        }
 

    }
}
