using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Diagnostics;
using HBMISR.GUI.MainGUI;
using HBMISR.Data;
using System.IO;

namespace HBMISR.GUI.OtherGUI
{
    /// <summary>
    /// ������
    /// ���ܣ�
    /// 1����֤���룬����ϵͳ
    /// </summary>
    public partial class FrmLogin : Form
    {
        /// <summary>
        /// ��ȡini�ļ�
        /// </summary>
        ReadIni readIni;

        /// <summary>
        /// �����ļ���·��
        /// </summary>
        string filePath;

        /// <summary>
        /// ָʾ����hbmis�ļ��Ƿ�ɹ���
        /// </summary>
        public bool IsSuccessful = false;

        /// <summary>
        /// �汾��
        /// </summary>
        private string version = "";

        /// <summary>
        /// ���캯��
        /// </summary>
        public FrmLogin()
        {
            InitializeComponent();
            readIni = new ReadIni();
            filePath = readIni.ReadString("filePath");
        }

        /// <summary>
        /// ��radioButton1Ϊѡ��״̬ʱ����ȡ��汾��
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            version = radioButton1.Text;
        }

        /// <summary>
        /// ��radioButton2Ϊѡ��״̬ʱ����ȡ��汾��
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            version = radioButton2.Text;
        }

        /// <summary>
        /// ��radioButton3Ϊѡ��״̬ʱ����ȡ��汾��
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void radioButton3_CheckedChanged(object sender, EventArgs e)
        {
            version = radioButton3.Text;
        }

        /// <summary>
        /// ��������¼�����,�ж��û��Ƿ��Ѿ�ѡ��汾��.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FrmLogin_Load(object sender, EventArgs e)
        {
            if (readIni.ReadString("version") != "" && File.Exists(readIni.ReadString("filepath")))
            {
                this.Text = this.Text + "(" + readIni.ReadString("version") + "---�ϱ���)";
                radioButton1.Visible = false;
                radioButton2.Visible = false;
                radioButton3.Visible = false;
            }
            else
            {
                this.Text = this.Text + "(�ϱ���)";
                string tempversion = readIni.ReadString("version");
                if (tempversion != "")
                {
                    this.Text = this.Text + "(" + readIni.ReadString("version") + "---�ϱ���)";
                    radioButton1.Visible = true;
                    radioButton2.Visible = true;
                    radioButton3.Visible = true;

                    switch (tempversion)
                    {
                        case "��(��)��":
                            radioButton1.Checked = true;
                            break;
                        case "��(��)��":
                            radioButton2.Checked = true;
                            break;
                        case "��(��)��":
                            radioButton3.Checked = true;
                            break;
                    }
                }
            }
        }

        /// <summary>
        /// �������Ĺرհ�ť�¼�����,������ǰ�߳�,ͻ������
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FrmLogin_FormClosed(object sender, FormClosedEventArgs e)
        {
            Process.GetCurrentProcess().Kill();
            Application.Exit();
        }

        /// <summary>
        /// �����¼��ť�¼������ж������Ƿ�������ȷ���Ƿ�ѡ��汾�ţ��Ƿ��Դ��������ļ���
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_Login_Click(object sender, EventArgs e)
        {
            if (tb_Password.Text.Equals(readIni.ReadString("password")))
            {
                if (radioButton1.Visible)
                {
                    if (radioButton1.Checked == true || radioButton2.Checked == true || radioButton3.Checked == true)
                    {
                        readIni.WriteString("tempversion", version);
                        FrmUnit frmUnit = new FrmUnit();
                        frmUnit.frmlogin = this;
                        frmUnit.ShowDialog();
                        if (IsSuccessful)
                        {
                            FrmMain frmMain = new FrmMain();
                            frmMain.Show();
                        }
                        this.Visible = false;
                        this.Size = new Size(0, 0);
                    }
                    else
                    {
                        MessageBox.Show("��ѡ��汾�ţ�","��ʾ");
                    }
                }
                else
                {
                    FrmMain frmMain = new FrmMain();
                    frmMain.Show();
                    this.Visible = false;
                }
            }
            else
            {
                MessageBox.Show("����������������룡");
                tb_Password.Text = "";
            }
        }
    }
}
 
