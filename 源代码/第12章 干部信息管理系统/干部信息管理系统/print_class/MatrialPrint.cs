using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Word;
using System.Collections;
using HBMISR.Data;
using System.Data;

namespace HBMISR.print_class
{
    /// <summary>
    /// 导出Word文档的类，功能：导出考察材料
    /// </summary>
    class MatrialPrint
    {
        //把Word读取到内存中
        Word._Application wordappliction = null;
        Word._Document mydoc = null;
        object missing = System.Reflection.Missing.Value;
        object readOnly = false;
        object isVisible = true;

        /// <summary>
        /// 导出后备干部考察材料表
        /// </summary>
        /// <param name="idlist">需要导出后备干部的id号集合</param>
        public void exportword(ArrayList idlist)
        {
            //选择保存路径
            #region
            string savepath = "";
            SaveFileDialog sa = new SaveFileDialog();
            sa.Filter = "Document(*.doc)|*.doc";
            sa.FileName = "考察材料表";
            if (sa.ShowDialog() == DialogResult.OK)
                savepath = sa.FileName;
            else
                return;
            #endregion

            //创建word应用程序
            wordappliction = new Word.Application();
            object filepath = System.Windows.Forms.Application.StartupPath + "\\wordModel" + "\\考察材料1.doc";
            Word.Document mydoc = wordappliction.Documents.Open(ref filepath, ref missing, ref readOnly,
                                                  ref missing, ref missing, ref missing, ref missing, ref missing,
                                                  ref missing, ref missing, ref missing, ref isVisible, ref missing,
                                                  ref missing, ref missing, ref missing);
            //wordappliction.Visible = true;
            mydoc.ActiveWindow.Selection.WholeStory();
            mydoc.ActiveWindow.Selection.Tables[1].Select();
            mydoc.ActiveWindow.Selection.Copy();
            for (int i = 1; i < idlist.Count; i++)
            {
                //插入分页符
                #region
                Word.Paragraph para1;
                para1 = mydoc.Content.Paragraphs.Add(ref missing);
                object pBreak = (int)WdBreakType.wdSectionBreakNextPage;
                //控制位置
                para1.Range.InsertBreak(ref pBreak);
                #endregion
                //粘贴剪贴板中的内容，同时加上加上备注。
                para1.Range.Paste();
            }

            string selectid = "";
            for (int i = 0; i < idlist.Count; i++)
            {
                if (i == idlist.Count - 1)
                    selectid = selectid + "'" + idlist[i] + "'";
                else
                    selectid = selectid + "'" + idlist[i] + "',";
            }
            string sql = "select  name,material,cid from TB_CommonInfo where isdelete=0 order by rank,joinTeam desc";
            string cond = "cid in (" + selectid + ")";
            DataOperation dataOp = new DataOperation();
            DataTable datatableT = dataOp.GetOneDataTable_sql(sql);
            DataTable dt = new DataTable();
            dt = datatableT.Clone();
            DataView dv = datatableT.AsDataView();
            dv.RowFilter = cond;
            dt = dv.ToTable();

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                mydoc.Tables[i + 1].Cell(1, 1).Range.Text = dt.Rows[i][0].ToString() + "同志考察材料";
                mydoc.Tables[i + 1].Cell(2, 1).Range.Text = dt.Rows[i][1].ToString();
            }

            #region
            object path = savepath;
            //wordappliction.Documents.Save(path);
            object myobj = System.Reflection.Missing.Value; ;
            mydoc.SaveAs(ref path, ref myobj, ref myobj, ref myobj, ref myobj, ref myobj,
                ref myobj, ref myobj, ref myobj, ref myobj, ref myobj, ref myobj,
                ref myobj, ref myobj, ref myobj, ref myobj);
            #endregion
            //关闭退出文档
            #region
            //关闭文档
            mydoc.Close(ref myobj, ref myobj, ref myobj);
            //退出应用程序。
            wordappliction.Quit();
            #endregion
            MessageBox.Show("导出成功!");
        }
    }
}
