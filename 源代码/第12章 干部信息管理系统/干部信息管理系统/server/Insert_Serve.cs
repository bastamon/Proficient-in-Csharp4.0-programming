using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using HBMISR.Service;
using HBMISR.Data;

namespace HBMISR.Service
{
    class Insert_Serve
    {
        public Insert_Serve()
        {
            
        }

        public void  Insert(CommonInfo  commoInfo)
        {
            DataOperation dataOperation = new DataOperation();
            if (commoInfo.IsRemainInfo == false)
                dataOperation.InsertBriefRigist(commoInfo);
            else
                dataOperation.InsertBriefRigistThreeYear(commoInfo);
        }
        

        public void Insert(SAbroad sAbroad)
        {
            DataOperation dataOperation = new DataOperation();
            string sql="select * from TB_SAbroad where id  in ('"+sAbroad.Id +"')";
            DataTable tb = dataOperation.GetOneDataTable_sql(sql);
            if (tb.Rows.Count > 0)
             dataOperation.Update1(sAbroad);
            else
            dataOperation.Insert1(sAbroad);         
        
        }

        public void Insert(WAbroad wAbroad)
        {
            DataOperation dataOperation = new DataOperation();
            string sql = "select * from TB_WAbroad where id  in ('" + wAbroad.Id + "')";
            DataTable tb = dataOperation.GetOneDataTable_sql(sql);
            if (tb.Rows.Count > 0)
                dataOperation.Update1(wAbroad);
            else

                dataOperation.Insert1(wAbroad);     

         }

         public void Insert(IContent iContent)
         {
             DataOperation dataOperation = new DataOperation();
             string sql = "select * from TB_GreatContent where id  in ('" + iContent.Id + "')";
           DataTable tb = dataOperation.GetOneDataTable_sql(sql);
           if (tb.Rows.Count > 0)
               dataOperation.Update1(iContent);
           else
               dataOperation.Insert1(iContent);
        }

        public void Insert(TMethod tMethod)
        {
            DataOperation dataOperation = new DataOperation();
            string sql = "select * from TB_TrainMethord where id  in ('" + tMethod.Id + "')";

            DataTable tb = dataOperation.GetOneDataTable_sql(sql);
            if (tb.Rows.Count > 0)
                dataOperation.Update1(tMethod);
            else
                dataOperation.Insert1(tMethod);

        }
        public void Insert(FLanguage fLanguage)
        {
            DataOperation dataOperation = new DataOperation();
            string sql = "select * from TB_FamiliarForeign where id  in ('" + fLanguage.Id + "')";
            DataTable tb = dataOperation.GetOneDataTable_sql(sql);
            if (tb.Rows.Count > 0)
                dataOperation.Update1(fLanguage);
            else
                dataOperation.Insert1(fLanguage);
        }
        public void Delete(SAbroad sAbroad)
        {
            DataOperation dataOperation = new DataOperation();
            string sql = "delete  from TB_SAbroad where id  in ('" + sAbroad.Id + "')";
            dataOperation.DeleteRecord(sql);
        }

        /// <summary>
        /// 删除海外工作的一条记录
        /// </summary>
        /// <param name="wAbroad">WAbrod类对象</param>
        public void Delete(WAbroad wAbroad)
        {
            DataOperation dataOperation = new DataOperation();
            string sql = "delete from TB_WAbroad where id  in ('" + wAbroad.Id + "')";
            dataOperation.DeleteRecord(sql);
        }

        /// <summary>
        /// 删除报告事项
        /// </summary>
        /// <param name="iContent">IContent类对象</param>
        public  void Delete(IContent iContent)
        {
            DataOperation dataOperation = new DataOperation();
            string sql = "delete  from TB_GreatContent where id  in ('" + iContent.Id + "')";
            dataOperation.DeleteRecord(sql);
        }

        /// <summary>
        /// 删除培养措施
        /// </summary>
        /// <param name="train">Train类对象</param>
        public  void Delete(Train train)
        {
            DataOperation dataOperation = new DataOperation();
            string sql = "delete  from TB_TrainExercise where id  in ('" + train.Id + "')";
            dataOperation.DeleteRecord(sql);
        }

        public  void DelTMethord(int p)
        {
            DataOperation dataOperation = new DataOperation();
            if (p != -1)
            {
                string sql = "delete  from TB_TrainMethord where id  in ('" + p + "')";
                dataOperation.DeleteRecord(sql);
            }
        }

        public void Delete(FLanguage fLanguage)
        {
            DataOperation dataOperation = new DataOperation();
            string sql = "delete  from TB_FamiliarForeign where id  in ('" + fLanguage.Id + "')";
            dataOperation.DeleteRecord(sql);
        }
    }
}
