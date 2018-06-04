using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace POS.Models
{
    /// <summary>
    /// 保存从POS_FUNCSET表中读取的数据（功能设定时用到）
    /// </summary>
  public  class ModelFunctions
    {
        /// <summary>
        /// 功能编号
        /// </summary>
        private int funct_id;
        /// <summary>
        /// 功能编号
        /// </summary>
        public int Funct_id
        {
            get { return funct_id; }
            set { funct_id = value; }
        }
        /// <summary>
       /// 项目名称
        /// </summary>
        private string funct_name;
        /// <summary>
        /// 项目名称(不是按钮上显示的名字）
        /// </summary>
        public string Funct_name
        {
            get { return funct_name; }
            set { funct_name = value; }
        }
        /// <summary>
       /// 位置序号
        /// </summary>
        private int position_id;
        /// <summary>
        /// 位置序号
        /// </summary>
        public int Position_id
        {
            get { return position_id; }
            set { position_id = value; }
        }
        /// <summary>
       /// 显示名称
        /// </summary>
        private string disp_name;
        /// <summary>
        /// 显示名称
        /// </summary>
        public string Disp_name
        {
            get { return disp_name; }
            set { disp_name = value; }
        }
        /// <summary>
       /// 是否显示（可见）
        /// </summary>
        private bool visible;
        /// <summary>
        /// 是否显示（可见）
        /// </summary>
        public bool Visible
        {
            get { return visible; }
            set { visible = value; }
        }
        /// <summary>
       /// 按钮颜色
        /// </summary>
        private Color color;
        /// <summary>
        /// 按钮颜色
        /// </summary>
        public Color Color
        {
            get { return color; }
            set { color = value; }
        }
        /// <summary>
       /// 字体颜色
        /// </summary>
        private Color font_color;
        /// <summary>
        /// 字体颜色
        /// </summary>
        public Color Font_color
        {
            get { return font_color; }
            set { font_color = value; }
        }
        /// <summary>
       /// 字体大小
        /// </summary>
        private float font_size;
        /// <summary>
        /// 字体大小
        /// </summary>
        public float Font_size
        {
            get { return font_size; }
            set { font_size = value; }
        }
        /// <summary>
       /// 字体
        /// </summary>
        private string font_name;
        /// <summary>
        /// 字体
        /// </summary>
        public string Font_name
        {
            get { return font_name; }
            set { font_name = value; }
        }
        /// <summary>
       /// 热键
        /// </summary>
        private int hotkey;
         /// <summary>
        /// 热键
        /// </summary>
        public int Hotkey
        {
            get { return hotkey; }
            set { hotkey = value; }
        }

          /// <summary>
          /// 员工级别
          /// </summary>
        private string emp_level;
        /// <summary>
        /// 员工级别
        /// </summary>
        public string Emp_level
        {
            get { return emp_level; }
            set { emp_level = value; }
        }

      /// <summary>
      /// 按钮在面板上的索引
      /// </summary>
        private int index;
        /// <summary>
        /// 按钮在面板上的索引
        /// </summary>
        public int Index
        {
            get { return index; }
            set { index = value; }
        }
       
    }
}
