using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using HBMISR.Data;
using HBMISR.GUI.OtherGUI;
using HBMISR.GUI.AnalyzeGUI;
using System.IO;
using HBMISR.GUI.PrintGUI;

namespace HBMISR.GUI.MainGUI
{
    /// <summary>
    /// 页首--导航条
    /// </summary>
    public partial class ControlNavigation : UserControl
    {
        /// <summary>
        /// 记录ControlMain类型的实例
        /// </summary>
        public ControlMain controlMain;

        /// <summary>
        /// 记录ControlPrint事件的处理
        /// </summary>
        public ControlPrint cp;

        /// <summary>
        /// 页首导航条构造函数
        /// </summary>
        public ControlNavigation()
        {
            InitializeComponent();

        }

        /// <summary>
        /// 文件管理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button1_Click_File(object sender, EventArgs e)
        {
            cp.Visible = false;
            controlMain.Visible = true;
            controlMain.tabControl1.TabPages[0].Text = "文件管理";

            controlMain.allpeople_Lb.Text = "新建数据文件";
            controlMain.allpeople_Lb.Visible = true;
            controlMain.All_pB.Visible = true;
            controlMain.All_pB.BackgroundImage = Properties.Resources.NewCreate; ;
            controlMain.All_pB.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;

            controlMain.label_meeting.Text = "打开数据文件";
            controlMain.label_meeting.Visible = true;
            controlMain.Meeting_pB.Visible = true;
            controlMain.Meeting_pB.BackgroundImage = Properties.Resources.Open;
            controlMain.Meeting_pB.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;

            controlMain.label_talk.Text = "导出数据文件";
            controlMain.label_talk.Visible = true;
            controlMain.talk_pB.Visible = true;
            controlMain.talk_pB.BackgroundImage = Properties.Resources.FileSaveAs;
            controlMain.talk_pB.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;

            controlMain.label_name_sheet.Text = "关闭数据文件";
            controlMain.label_name_sheet.Visible = true;
            controlMain.reservenamesheet_pB.Visible = true;
            controlMain.reservenamesheet_pB.BackgroundImage = Properties.Resources.FileClose;
            controlMain.reservenamesheet_pB.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;

            controlMain.regist_pB.Visible = true;
            controlMain.label_regist.Visible = true;
            controlMain.label_regist.Text = "导出空白表";
            controlMain.regist_pB.BackgroundImage = Properties.Resources.report;
            controlMain.regist_pB.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;


           
            controlMain.panelStyle = "file";
        }

        /// <summary>
        /// 综合管理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button2_Click_Insert(object sender, EventArgs e)
        {
            if (controlMain.nowUnit.Text.Equals(""))
            {
                MessageBox.Show("请先创建数据文件！", "提示");
                return;
            }
            ReadIni readIni = new ReadIni();

            cp.Visible = false;
            if (readIni.ReadString("filePath").Equals(Application.StartupPath + "\\DB\\Default.db"))
            {
                controlMain.listView.Items.Clear();
            }
            controlMain.Visible = true;
            controlMain.tabControl1.TabPages[0].Text = "录入管理";
            controlMain.reservenamesheet_pB.Visible = true;
            controlMain.regist_pB.Visible = false ;
            controlMain.talk_pB.Visible = true;
            controlMain.Meeting_pB.Visible = true;
           
            controlMain.All_pB.Visible = true;
            controlMain.All_pB.BackgroundImage = Properties.Resources.AllPeoplein;
            controlMain.reservenamesheet_pB.BackgroundImage = Properties.Resources.NameSheet;
            controlMain.regist_pB.BackgroundImage = Properties.Resources.SituationRegiste;
            controlMain.talk_pB.BackgroundImage = Properties.Resources.Dialog;//另存为
            controlMain.Meeting_pB.BackgroundImage = Properties.Resources.meeting;//关闭

            controlMain.reservenamesheet_pB .BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            controlMain.regist_pB.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            controlMain.talk_pB.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            controlMain.Meeting_pB.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;

            controlMain.allpeople_Lb.Visible = true;
            controlMain.label_meeting.Visible = true;
            controlMain.label_talk.Visible = true;
            controlMain.label_name_sheet.Visible = true;            
            controlMain.label_regist.Visible = false;

            controlMain.allpeople_Lb.Text = "基本信息采集";
            controlMain.label_meeting.Text = "简要情况登记";
            controlMain.label_talk.Text = "其他信息采集";
            controlMain.label_name_sheet.Text = "考察材料";

            controlMain.panelStyle = "insert";
        }

        /// <summary>
        /// 数据分析
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button3_Click_Analyse(object sender, EventArgs e)
        {

            if (controlMain.nowUnit.Text.Equals(""))
            {
                MessageBox.Show("请先创建数据文件！", "提示");
                return;
            }
            cp.Visible = false;
            controlMain.Visible = true;

            controlMain.tabControl1.TabPages[0].Text = "数据分析";

            controlMain.allpeople_Lb.Text = "基本分析";
            controlMain.allpeople_Lb.Visible = true;
            controlMain.All_pB.Visible = true;
            controlMain.All_pB.BackgroundImage = Properties.Resources.BasicAnalyze;
            controlMain.All_pB.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;

            controlMain.label_meeting.Text = "高级分析";
            controlMain.label_meeting.Visible = true;
            controlMain.Meeting_pB.Visible = true;
            controlMain.Meeting_pB.BackgroundImage = Properties.Resources.DataAnalyse;
            controlMain.Meeting_pB.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;

            controlMain.label_talk.Text = "年龄分析";
            controlMain.label_talk.Visible = true;
            controlMain.talk_pB.BackgroundImage = Properties.Resources.Age;
            controlMain.talk_pB.Visible = true;
            controlMain.talk_pB.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;


            controlMain.reservenamesheet_pB.Visible = false;
            controlMain.regist_pB.Visible = false;


            controlMain.label_name_sheet.Visible = false;
            controlMain.label_regist.Visible = false;

            controlMain.label_name_sheet.Text = "基本分析";
            controlMain.label_regist.Text = "高级分析";
            controlMain.panelStyle = "search";

        }

        /// <summary>
        /// 打印管理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button4_Click_Print(object sender, EventArgs e)
        {
            if (controlMain.nowUnit.Text.Equals(""))
            {
                MessageBox.Show("请先创建数据文件！", "提示");
                return;
            }
            controlMain.Visible = false;
            cp.Visible = true;
        }

        /// <summary>
        /// 用户管理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button_User_Click(object sender, EventArgs e)
        {
            FrmChangePassWord change = new FrmChangePassWord();
            change.ShowDialog();
        }  
    }
}
