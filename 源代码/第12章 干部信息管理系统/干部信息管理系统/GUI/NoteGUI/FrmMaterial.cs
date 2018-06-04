using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using HBMISR.GUI.MainGUI;
using HBMISR.Data;

namespace HBMISR.GUI.NoteGUI
{
    /// <summary>
    /// 考察材料
    /// </summary>
    public partial class FrmMaterial : Form
    {
        bool change = false;
        bool remarkchange = false;

        /// <summary>
        /// 人员cid
        /// </summary>
        public string listViewCid = "";

        public bool isSingle = false;

        /// <summary>
        /// 记录ControlMain类型的实例
        /// </summary>
        public ControlMain controlMain;

        DataOperation dataOperation = null;

        /// <summary>
        /// 考察材料构造函数
        /// </summary>
        public FrmMaterial()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 考察材料初始化
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Form1_Load(object sender, EventArgs e)
        {
            DataOperation dataOperation = new DataOperation();
            DataTable dt1 = dataOperation.GetOneDataTable_sql("select name,material from TB_CommonInfo where cid='" + listViewCid + "'");
            name_tB.Text = dt1.Rows[0]["name"].ToString();
            richTextBoxExtended1.Text = "";
            richTextBoxExtended1.Text = dt1.Rows[0]["material"].ToString();
            change = true;
        }

        /// <summary>
        /// 保存按钮事件监听
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                dataOperation = new DataOperation();
                dataOperation.InsertMaterial(listViewCid, richTextBoxExtended1.Text);
                MessageBox.Show("保存成功！");
                remarkchange = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show("保存时出错！" + ex.Message);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void richTextBoxExtended1_TextChanged(object sender, EventArgs e)
        {
            if (change)
            {
                remarkchange = true;
            }
        }


        /// <summary>
        /// 窗体关闭时事件的处理，当有信息改变时，提示是否保存。
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FrmMaterial_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (remarkchange)
            {
                switch (MessageBox.Show("是否保存当前信息？", "提示", MessageBoxButtons.YesNoCancel))
                {
                    case DialogResult.Yes:
                        dataOperation.InsertMaterial(listViewCid, richTextBoxExtended1.Text);
                        MessageBox.Show("保存成功！");
                        break;
                    case DialogResult.Cancel:
                        e.Cancel = true;
                        break;
                }
            }
        }

        private void Item_Copy_Click(object sender, EventArgs e)
        {
            if (this.richTextBoxExtended1.SelectedText == "")
            {
                return;
            }
            else
                Clipboard.SetDataObject(this.richTextBoxExtended1.SelectedText, false);

        }

        private void Item_Paste_Click(object sender, EventArgs e)
        {
            IDataObject iData = Clipboard.GetDataObject();
            if (iData.GetDataPresent(DataFormats.Text))
            {
                richTextBoxExtended1.Paste();
            }
            else
            {
                MessageBox.Show("格式错误！");
            }
        }

        private void PasteEnable()
        {
            IDataObject iData = Clipboard.GetDataObject();
            if (iData.GetDataPresent(DataFormats.Text) && iData.GetData(DataFormats.Text).ToString().Trim() != "")
            {
                Item_Paste.Enabled = true;
            }
            else
            {
                Item_Paste.Enabled = false;
            }
        }

        private void richTextBoxExtended1_MouseDown(object sender, MouseEventArgs e)
        {
            if (this.richTextBoxExtended1.SelectedText == "")
            {
                Item_Copy.Enabled = false;
            }
            else
            {
                Item_Copy.Enabled = true;
            }
            PasteEnable();
        }
    }
}
