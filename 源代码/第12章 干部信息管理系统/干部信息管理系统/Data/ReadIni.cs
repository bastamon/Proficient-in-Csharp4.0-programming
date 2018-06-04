using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices;
using System.Windows.Forms;
namespace HBMISR.Data
{
    /// <summary>
    /// 读取系统的配置文件
    /// </summary>
    public class ReadIni
    {
        public static string filepath;
       
        /// <summary>
        /// 构造函数，给filepath赋值
        /// </summary>
        public ReadIni()
        {
            filepath = this.ReadString("filePath");
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
        /// 读取配置文件
        /// </summary>
        /// <param name="key">标识字段</param>
        /// <returns>标识字段的值</returns>
        public string ReadString(string key)
        {
            string fPath = Application.StartupPath + "\\config.ini";
            StringBuilder temp = new StringBuilder(1024);
            GetPrivateProfileString("hbs", key, "", temp, 1024, fPath);
            return temp.ToString();
        }

        /// <summary>
        /// 写入配置文件
        /// </summary>
        /// <param name="key">标识字段</param>
        /// <param name="val">标识字段的值</param>
        public void WriteString(string key, string val)
        {
            string fPath = Application.StartupPath + "\\config.ini";
            WritePrivateProfileString("hbs", key, val, fPath);
        }
    }
}
