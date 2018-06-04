using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace POS.Models
{
    //功能设定POS_FUNCSET表中的字段
    class POS_FUNCSET
    {
        private int FUNC_ID;
        /// <summary>
        /// 功能编号
        /// </summary>
        public int FUNC_ID1
        {
            get { return FUNC_ID; }
            set { FUNC_ID = value; }
        }

        private string FUNC_NAME;
        /// <summary>
        /// 项目名称
        /// </summary>
        public string FUNC_NAME1
        {
            get { return FUNC_NAME; }
            set { FUNC_NAME = value; }
        }

        private int POSITION_ID;
        /// <summary>
        /// 位置序号
        /// </summary>
        public int POSITION_ID1
        {
            get { return POSITION_ID; }
            set { POSITION_ID = value; }
        }

        private int befortSetPOSITION_ID;
        /// <summary>
        /// 更改位置序号前的位置序号
        /// </summary>
        public int BefortSetPOSITION_ID
        {
            get { return befortSetPOSITION_ID; }
            set { befortSetPOSITION_ID = value; }
        }

        private string DISP_NAME;
        /// <summary>
        /// 显示名称
        /// </summary>
        public string DISP_NAME1
        {
            get { return DISP_NAME; }
            set { DISP_NAME = value; }
        }

        private bool VISIBLE;
        /// <summary>
        /// 是否显示（可见）
        /// </summary>
        public bool VISIBLE1
        {
            get { return VISIBLE; }
            set { VISIBLE = value; }
        }

        private string COLOR;
        /// <summary>
        /// 按钮颜色
        /// </summary>
        public string COLOR1
        {
            get { return COLOR; }
            set { COLOR = value; }
        }

        private string FONT_COLOR;
        /// <summary>
        /// 字体颜色
        /// </summary>
        public string FONT_COLOR1
        {
            get { return FONT_COLOR; }
            set { FONT_COLOR = value; }
        }

        private int FONT_SIZE;
        /// <summary>
        /// 字体大小
        /// </summary>
        public int FONT_SIZE1
        {
            get { return FONT_SIZE; }
            set { FONT_SIZE = value; }
        }

        private string FONT_NAME;
        /// <summary>
        /// 字体
        /// </summary>
        public string FONT_NAME1
        {
            get { return FONT_NAME; }
            set { FONT_NAME = value; }
        }

        private string EMP_LEVEL;
        /// <summary>
        /// 员工级别
        /// </summary>
        public string EMP_LEVEL1
        {
            get { return EMP_LEVEL; }
            set { EMP_LEVEL = value; }
        }

        private int DEFAULT_POS;
        /// <summary>
        /// 默认
        /// </summary>
        public int DEFAULT_POS1
        {
            get { return DEFAULT_POS; }
            set { DEFAULT_POS = value; }
        }

        private int GROUP_ID;
        /// <summary>
        /// 群组编号
        /// </summary>
        public int GROUP_ID1
        {
            get { return GROUP_ID; }
            set { GROUP_ID = value; }
        }

        private int HOTKEY;
        /// <summary>
        /// 热键
        /// </summary>
        public int HOTKEY1
        {
            get { return HOTKEY; }
            set { HOTKEY = value; }
        }

        private int beforeSetHOTKEY;
        /// <summary>
        /// 更改热键前的热键设置
        /// </summary>
        public int BeforeSetHOTKEY
        {
            get { return beforeSetHOTKEY; }
            set { beforeSetHOTKEY = value; }
        }

        private string MEMO;
        /// <summary>
        /// 备注
        /// </summary>
        public string MEMO1
        {
            get { return MEMO; }
            set { MEMO = value; }
        }

        private bool TRANSFER_VISIBLE;
        /// <summary>
        /// 传输状态
        /// </summary>
        public bool TRANSFER_VISIBLE1
        {
            get { return TRANSFER_VISIBLE; }
            set { TRANSFER_VISIBLE = value; }
        }

    }
}
