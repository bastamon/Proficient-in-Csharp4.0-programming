using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SQLite;
using System.Data;
using HBMISR.Service;
using System.Windows.Forms;
using System.Threading;

namespace HBMISR.Data
{
    class DataOperation
    {
        private SQLiteConnection conn;
        private SQLiteCommand cmd;
        private string datasource;
        MySQLiteConnection myCon;

        #region//构造函数

        /// <summary>
        /// 与数据库连接
        /// </summary>
        /// <param name="datasource">数据库路径</param>
        public void GetConnection(string datasource)
        {

            this.datasource = datasource;
            myCon = new MySQLiteConnection(datasource);
            conn = myCon.GetSQLiteConnection();
        }

        /// <summary>
        /// 构造方法
        /// </summary>
        /// <param name="datasource">数据库路径</param>
        public DataOperation(string datasource)
        {
            this.datasource = datasource;
            myCon = new MySQLiteConnection(datasource);
            conn = myCon.GetSQLiteConnection();
        }

        /// <summary>
        /// 构造方法
        /// </summary>
        public DataOperation()
        {
            ReadIni read = new ReadIni();
            string datasource = read.ReadString("filePath");
            if ((datasource == string.Empty))
            {
                datasource = Application.StartupPath + "\\DB\\Default.db";
            }
            else
            {
                this.datasource = datasource;
            }
            MySQLiteConnection myconn = new MySQLiteConnection(datasource);
            conn = myconn.GetSQLiteConnection();
        }

        #endregion

        /// <summary>
        /// 操作数据库，执行各种SQL语句
        /// </summary>
        /// <param name="strSql"></param>
        /// <returns>返回整型0或1</returns>
        public int OperateData_sql(string strSql)
        {
            try
            {
                conn.Open();
                SQLiteCommand cmd = new SQLiteCommand(strSql, conn);
                cmd.ExecuteNonQuery();
                return 1;
            }
            catch
            {
                return 0;
            }
            finally
            {
                conn.Close();
            }
        }
        /// <summary>
        /// 操作数据库，执行各种SQL语句
        /// </summary>
        /// <param name="strSql"></param>
        /// <returns>返回整型0或1</returns>
        public int OperateRank_sql(string[] strSql)
        {
            try
            {
                conn.Open();
                for (int i = 0; i < strSql.Count(); i++)
                {
                    string sql = "update TB_CommonInfo set rank=" + i + " where cid='" + strSql[i].ToString() + "'";
                    SQLiteCommand cmd = new SQLiteCommand(sql, conn);
                    cmd.ExecuteNonQuery();
                }
                return 1;
            }
            catch
            {
                return 0;
            }
            finally
            {
                conn.Close();
            }
        }
        /// <summary>
        /// 根据SQL语句得到一个DataTable
        /// </summary>
        /// <param name="sql">sql语句</param>
        /// <returns></returns>
        public DataTable GetOneDataTable_sql(string sql)
        {

            try
            {
                SQLiteDataAdapter da = new SQLiteDataAdapter(sql, conn);
                DataTable dt = new DataTable();
                da.Fill(dt);
                return dt;

            }
            catch (SQLiteException ex)
            {
                MessageBox.Show(ex.Message);
                return null;
            }
            finally
            {
                conn.Close();
            }
        }

        /// <summary>
        /// 检测cid号是否存在
        /// </summary>
        /// <param name="cid">cid字符串</param>
        /// <returns></returns>
        public bool IsExitCid(string cid)
        {
            DataTable dt = GetOneDataTable_sql("select CID from TB_CommonInfo");
            if (dt != null && dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    if (dt.Rows[i]["cid"].ToString().Equals(cid))
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        /// <summary>
        /// 插入一个DataTable
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="table"></param>
        /// <returns></returns>
        public bool InsertTable(string sql, DataTable table)
        {
            bool b = false;
            SQLiteDataAdapter da;
            SQLiteCommandBuilder commandbuiler;
            if (table != null)
            {
                try
                {
                    da = new SQLiteDataAdapter(sql, conn);
                    commandbuiler = new SQLiteCommandBuilder(da);
                    da.Update(table);
                    b = true;
                    return b;
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
            return false;
        }

        /// <summary>
        /// 创建DB文件
        /// </summary>
        /// <param name="datasource">数据文件存放的路径</param>
        /// <returns></returns>
        public bool CreatDB(string datasource)
        {
            Boolean b = false;
            try
            {
                SQLiteConnection conn1 = new SQLiteConnection();
                if (!System.IO.File.Exists(datasource))
                {
                    SQLiteConnection.CreateFile(datasource);
                    //获得连接
                    SQLiteConnectionStringBuilder conStr = new SQLiteConnectionStringBuilder();
                    //与数据库连接
                    conStr.DataSource = datasource;
                    conn1.ConnectionString = conStr.ToString();
                }
                else
                {
                    System.IO.File.Delete(datasource);
                    //获得连接
                    SQLiteConnectionStringBuilder conStr = new SQLiteConnectionStringBuilder();
                    //与数据库连接
                    conStr.DataSource = datasource;
                    conn1.ConnectionString = conStr.ToString();
                }
                using (cmd = conn1.CreateCommand())
                {
                    conn1.Open();

                    //初步人员名册表
                    cmd.CommandText = "create table TB_CommonInfo ([CID]    varchar   primary key  not null ,[unitname] varchar , [name]    varchar(15) ,[sex]   varchar(2)  ,[nation]   varchar(10)   ,[department]  varchar(30)   ,[position]  varchar(20)  ,[native]    varchar(30)   ,[birthplace] varchar(30),[birthday]    varchar(22) ,[age] integer  ,[partyTime]  varchar(22)  , [workTime]   varchar(22) ,[health]  varchar(10) ,[technicalPost]  varchar(20)   ,[specialtySkill] varchar(22) ,[fullEducation]   varchar(22), [fullDegree]  varchar(22),[fullSchool]  varchar(20),[fullSpecialty] varchar(20), [workEducation] varchar(20) ,[workDegree] varchar(20) ,[workGraduate]  varchar(26) ,[workSpecialty]  varchar(26) ,[knowField]   varchar(20), [trainDirection]  varchar(20)  , [trainMeasure]   varchar(26) , [examine] varchar(30)   ,[remark]  varchar(30), [experiencePost]  varchar(50) ,[joinTeam] bool null, [status]  varchr(10),[photo] blob,[qd] bool,[startTime] varchar(10) null,[result1] varchar(6) null,[result2] varchar(6) null,[result3] varchar(6) null,[isDelete] bool,[partyClass] varchar(8) null,[grade] integer null,[rank] integer null,[material] varchar,[unitClass] varchr(8) null,[state] varchar(4) null,[stateTime] varchar,[promote] varchar,[promoteTime] varchar,[oldUnit] varchar,[SPDegree] varchar,[isTwoYear] bool,[isGuide] bool,[systemTime] varchar null,[publicSelect] bool,[twoYGE] bool,[extend1] varchar null,[extend2] varchar null, [extend3] varchar null)";
                    cmd.ExecuteNonQuery();           

                    //本地单位
                    cmd.CommandText = "create table TB_LocalUnit ([ID]   integer   primary  key autoincrement  not null,[unitName]    varchar(40)  , [registTime]   varchar(22) ,[unitClass]  varchar(30) ,[extend1] varchar null,[extend2] varchar null)";
                    cmd.ExecuteNonQuery();


                    //家庭情况及社会关系表
                    cmd.CommandText = "create table TB_Family (  [ID]    integer   primary  key autoincrement  not null, [CID]   varchar  not null ,[relationship]  varchar(6)    null ,[name]     varchar(20)    null, [birthday]    varchar(22)   null, [country]     varchar(20)       null, [party]    varchar(20)  null, [nation]  varchar(20)  null, [deptJob] varchar(30)   null,[age] integer null,[polityface] varchar(20) null,[remark]  varchar(100)  null,[extend1] varchar null,[extend2] varchar null)";
                    cmd.ExecuteNonQuery();


                    //海外学习
                    cmd.CommandText = "create table TB_SAbroad  ([ID]    integer   primary key autoincrement not null, [CID]  varchar  not null , [startTime]    varchar(20)   null,  [endTime]  varchar(20)    null,   [country]   varchar(30)   null, [academy]   varchar(30)   null,[degree]   varchar   (30)  null,[extend1] varchar null,[extend2] varchar null)";
                    cmd.ExecuteNonQuery();

                    //海外工作
                    cmd.CommandText = "create table TB_WAbroad ([ID]    integer    primary key autoincrement  not null, [CID]   varchar     not null, [startTime]    varchar(20)     null,  [endTime]   varchar(20)  null,  [abroadCountry]    varchar(20)  null, [departmentPosition]   varchar(30)     null, [specialtyArea]  varchar(30)     null,[extend1] varchar null,[extend2] varchar null)";
                    cmd.ExecuteNonQuery();

                    //重大事项信息
                    cmd.CommandText = "create table TB_GreatContent ( [ID]   integer    primary key autoincrement not null , [CID]      varchar    not null,[content]    varchar(50)  null, [matter]   varchar   null,[extend1] varchar null,[extend2] varchar null)";
                    cmd.ExecuteNonQuery();

                    //熟悉外语语种
                    cmd.CommandText = "create table TB_FamiliarForeign ([ID]      integer     primary key autoincrement  not null,  [CID]  varchar   not null, [foreignKind]   varchar(20)     null, [level]   varchar(20)   null,[number] integer null,[extend1] varchar null,[extend2] varchar null)";
                    cmd.ExecuteNonQuery();

                    //参加培训和实践锻炼情况
                    cmd.CommandText = "create table TB_TrainExercise ([ID]     integer   primary key autoincrement  not null, [CID]      varchar     not null, [reportMatter]   varchar null,[startTime] varchar,[endTime] varchar,[reportContent]    varchar(100)  null,[content] varchar,[extend1] varchar null,[extend2] varchar null)";
                    cmd.ExecuteNonQuery();

                    //培训锻炼措施需求
                    cmd.CommandText = "create table TB_TrainMethord([ID] integer primary key autoincrement  not null,[CID] varchar not null,[options] integer null, [note14] varchar null,[extend1] varchar null,[extend2] varchar null)";
                    cmd.ExecuteNonQuery();

                    //个人简历
                    cmd.CommandText = "create table TB_Resume([ID] integer primary key autoincrement  not null,[CID] varchar not null,[betime] varchar(22) null,[entime] varchar[22] null,[content] varchar(50) null,[extend1] varchar null,[extend2] varchar null)";
                    cmd.ExecuteNonQuery();

                    //奖惩关系表
                    cmd.CommandText = "create table TB_PunishAward ([ID] integer  primary key autoincrement not null, [CID]    varchar     not null, [class]    varchar(2)  null, [time]  varchar(22)     null, [grade]    varchar(20)     null,  [department]   varchar(20)    null, [name]   varchar(36)  null,[extend1] varchar null,[extend2] varchar null)";
                    cmd.ExecuteNonQuery();    

                }


                conn1.Close();
                conn1.Dispose();

                b = true;

            }
            catch (Exception ex)
            {
                throw ex;
            }
            return b;
        }

        #region
        /// <summary>
        /// 假删除或者恢复一条记录
        /// </summary>
        /// <param name="cid">人员的cid</param>
        /// <param name="b">bool值，值为true时，删除操作，值为false时，恢复操作</param>
        public void InsertMark(string cid, bool b)//假删除一条记录
        {
            string sql = "update TB_CommonInfo set isDelete=@isDelete1 where cid='" + cid + "'";
            try
            {
                conn.Open();
                using (SQLiteCommand cmd = new SQLiteCommand(sql, conn))
                {

                    cmd.Parameters.Add("@isDelete1", System.Data.DbType.Boolean).Value = b;
                    cmd.ExecuteNonQuery();
                }

            }
            catch (Exception ex)
            {
                throw ex;

            }
            finally
            {
                conn.Close();

            }
        }

        /// <summary>
        /// 删除被标记的干部的所有信息
        /// </summary>
        /// <param name="list">后备干部的id号</param>
        public void DeleteMarkedRecord(List<ListViewDel> list)
        {
            string sql18 = "delete from TB_CommonInfo where isDelete='1'";
            for (int i = 0; i < list.Count; i++)
            {
                string sql1 = "delete from TB_FamiliarForeign where cid='" + list[i].Cid + "'";
                string sql2 = "delete from TB_Family where cid='" + list[i].Cid + "'";
                string sql3 = "delete from TB_WAbroad where cid='" + list[i].Cid + "'";
                string sql4 = "delete from TB_SAbroad where cid='" + list[i].Cid + "'";
                string sql5 = "delete from TB_GreatContent where cid='" + list[i].Cid + "'";
                string sql6 = "delete from TB_TrainMethord where cid='" + list[i].Cid + "'";
                string sql7 = "delete from TB_Resume where cid='" + list[i].Cid + "'";
                string sql8 = "delete from TB_TrainExercise where cid='" + list[i].Cid + "'";
                string sql9 = "delete from TB_PunishAward where cid='" + list[i].Cid + "'";
                string sql10 = "delete from TB_COUNTY where cid='" + list[i].Cid + "'";
                DeleteRecord(sql1);
                DeleteRecord(sql2);
                DeleteRecord(sql3);
                DeleteRecord(sql4);
                DeleteRecord(sql5);
                DeleteRecord(sql6);
                DeleteRecord(sql7);
                DeleteRecord(sql8);
                DeleteRecord(sql9);
                DeleteRecord(sql10);
            }

            DeleteRecord(sql18);
        }

        /// <summary>
        /// 更新数据库
        /// </summary>
        /// <param name="commoInfo">简要情况信息</param>
        /// <returns>bool值，值为true时，表示操作成功，值为false时表示操作失败</returns>
        public bool InsertBriefRigist(CommonInfo commoInfo)
        {
            if (commoInfo.Cid != null)
            {
                string sql = "update  TB_CommonInfo set age=@age1 ,birthplace=@birthplace1 ,health=@health1,specialtySkill=@specialtySkill1,remark=@remark1,photo=@photo1,startTime=@startTime1,result1=@result11,result2=@result21,result3=@result31,state=@state1 where Cid='" + commoInfo.Cid + "'";

                try
                {
                    conn.Open();
                    using (SQLiteCommand cmd = new SQLiteCommand(sql, conn))
                    {
                        cmd.Parameters.Add("@age1", System.Data.DbType.Int32).Value = commoInfo.Age;
                        cmd.Parameters.Add("@birthplace1", System.Data.DbType.String).Value = commoInfo.Birthplace;
                        cmd.Parameters.Add("@health1", System.Data.DbType.String).Value = commoInfo.Health;
                        cmd.Parameters.Add("@specialtySkill1", System.Data.DbType.String).Value = commoInfo.SpecialtySkill;
                        cmd.Parameters.Add("@remark1", System.Data.DbType.String).Value = commoInfo.Remark;
                        cmd.Parameters.Add("@photo1", System.Data.DbType.Binary).Value = commoInfo.Photo;
                        cmd.Parameters.Add("@startTime1", System.Data.DbType.String).Value = commoInfo.StartYear;
                        cmd.Parameters.Add("@result11", System.Data.DbType.String).Value = commoInfo.Result1;
                        cmd.Parameters.Add("@result21", System.Data.DbType.String).Value = commoInfo.Result2;
                        cmd.Parameters.Add("@result31", System.Data.DbType.String).Value = commoInfo.Result3;
                        cmd.Parameters.Add("@state1", System.Data.DbType.String).Value = commoInfo.State;
                        cmd.ExecuteNonQuery();
                        return true;
                    }
                }
                catch (Exception e)
                {
                    return false;
                    throw e;
                }
                finally
                {
                    conn.Close();
                }
            }
            return false;
        }

        /// <summary>
        /// 更新数据库三年考核
        /// </summary>
        /// <param name="commoInfo"></param>
        /// <returns></returns>
        public bool InsertBriefRigistThreeYear(CommonInfo commoInfo)
        {
            if (commoInfo.Cid != null)
            {
                string sql = "update  TB_CommonInfo set  startTime=@startTime1,result1=@result11,result2=@result21,result3=@result31 where Cid='" + commoInfo.Cid + "'";

                try
                {
                    conn.Open();
                    using (SQLiteCommand cmd = new SQLiteCommand(sql, conn))
                    {

                        cmd.Parameters.Add("@startTime1", System.Data.DbType.String).Value = commoInfo.StartYear;
                        cmd.Parameters.Add("@result11", System.Data.DbType.String).Value = commoInfo.Result1;
                        cmd.Parameters.Add("@result21", System.Data.DbType.String).Value = commoInfo.Result2;
                        cmd.Parameters.Add("@result31", System.Data.DbType.String).Value = commoInfo.Result3;

                        cmd.ExecuteNonQuery();
                        return true;
                    }
                }
                catch (Exception e)
                {
                    return false;
                    throw e;
                }
                finally
                {
                    conn.Close();
                }
            }
            return false;
        }

        /// <summary>
        /// 执行无返回值的sql语句
        /// </summary>
        /// <param name="sql">sql语句</param>
        /// <returns></returns>
        public bool DeleteRecord(string sql)
        {

            try
            {
                conn.Open();

                using (SQLiteCommand cmd = new SQLiteCommand(sql, conn))
                {
                    cmd.ExecuteNonQuery();
                }
                return true;
            }

            catch (Exception e)
            {
                return false;
                throw e;
            }
            finally
            {
                conn.Close();
            }
        }
        #region

        public void Insert1(WAbroad wAbroad)//海外工作
        {
            if (wAbroad.Cid != null)
            {
                DataOperation dataOperation = new DataOperation();
                DataTable dt = dataOperation.GetOneDataTable_sql("select * from TB_WAbroad where cid='" + wAbroad.Cid + "' and abroadCountry='" + wAbroad.Country + "' and startTime='" + wAbroad.PreviousStart + "' and endTime='" + wAbroad.PreviousEnd + "'");
                if (dt.Rows.Count == 0)
                {
                    String sql = "insert into TB_WAbroad(cid,startTime,endTime,abroadCountry,departmentPosition,specialtyArea)values(@cid1,@startTime1,@endTime1,@abroadCountry1,@departmentPosition1,@specialtyArea1)";
                    try
                    {
                        conn.Open();
                        using (SQLiteCommand cmd = new SQLiteCommand(sql, conn))
                        {
                            cmd.Parameters.Add("@cid1", System.Data.DbType.String).Value = wAbroad.Cid.ToString().Trim();
                            cmd.Parameters.Add("@startTime1", System.Data.DbType.String).Value = wAbroad.StartTime;
                            cmd.Parameters.Add("@endTime1", System.Data.DbType.String).Value = wAbroad.EndTime;
                            cmd.Parameters.Add("@abroadCountry1", System.Data.DbType.String).Value = wAbroad.Country.ToString();
                            cmd.Parameters.Add("@departmentPosition1", System.Data.DbType.String).Value = wAbroad.DepartmentPosition.ToString();
                            cmd.Parameters.Add("@specialtyArea1", System.Data.DbType.String).Value = wAbroad.Specialty.ToString();
                            cmd.ExecuteNonQuery();

                        }



                    }
                    catch (Exception e)
                    {
                        throw e;


                    }
                    finally
                    {

                        conn.Close();
                    }
                }
            }


        }

        public void Insert1(SAbroad sAbroad)//海外学习
        {
            if (sAbroad.Cid != null)
            {
                DataOperation dataOperation = new DataOperation();
                DataTable dt = dataOperation.GetOneDataTable_sql("select * from TB_SAbroad where cid='" + sAbroad.Cid + "' and country='" + sAbroad.Country + "' and startTime='" + sAbroad.PreviousStart + "' and endTime='" + sAbroad.PreviousEnd + "' and academy='" + sAbroad.Academy + "' and degree='" + sAbroad.Degree + "'");
                if (dt.Rows.Count == 0)
                {
                    string sql = "insert into TB_SAbroad(cid,startTime,endTime,country,academy,degree)values(@cid1,@startTime1,@endTime1,@country1,@academy1,@degree1)";
                    try
                    {
                        conn.Open();
                        using (SQLiteCommand cmd = new SQLiteCommand(sql, conn))
                        {
                            cmd.Parameters.Add("@cid1", System.Data.DbType.String).Value = sAbroad.Cid;
                            cmd.Parameters.Add("@startTime1", System.Data.DbType.String).Value = sAbroad.StartTime;
                            cmd.Parameters.Add("@endTime1", System.Data.DbType.String).Value = sAbroad.EndTime;
                            cmd.Parameters.Add("@country1", System.Data.DbType.String).Value = sAbroad.Country;
                            cmd.Parameters.Add("@academy1", System.Data.DbType.String).Value = sAbroad.Academy;
                            cmd.Parameters.Add("@degree1", System.Data.DbType.String).Value = sAbroad.Degree;
                            cmd.ExecuteNonQuery();
                        }

                    }
                    catch (Exception e)
                    {
                        throw e;
                    }
                    finally
                    {

                        conn.Close();
                    }
                }
            }
        }

        public void Insert1(IContent iContent)//重大事项信息报告内容:TB_GreatContent
        {
            if (iContent.Cid != null)
            {
                DataOperation dataOperation = new DataOperation();
                DataTable dt = dataOperation.GetOneDataTable_sql("select * from TB_GreatContent where cid='" + iContent.Cid + "' and content='" + iContent.Content + "' and matter='" + iContent.Matter + "'");
                if (dt.Rows.Count == 0)
                {
                    string sql = "insert into TB_GreatContent(cid,content,matter)values(@cid1,@content1,@matter1)";
                    try
                    {
                        conn.Open();
                        using (SQLiteCommand cmd = new SQLiteCommand(sql, conn))
                        {
                            cmd.Parameters.Add("@cid1", System.Data.DbType.String).Value = iContent.Cid;
                            cmd.Parameters.Add("@content1", System.Data.DbType.String).Value = iContent.Content;
                            cmd.Parameters.Add("@matter1", System.Data.DbType.Int32).Value = iContent.Matter;
                            cmd.ExecuteNonQuery();
                        }



                    }
                    catch (Exception e)
                    {
                        throw e;

                    }
                    finally
                    {

                        conn.Close();
                    }
                }
            }
        }

        public void Insert1(TMethod tMethod)//培养锻炼措施需求:TB_TrainMethord
        {
            if (tMethod.Cid != null)
            {
                DataOperation dataOperation = new DataOperation();
                DataTable dt = dataOperation.GetOneDataTable_sql("select * from TB_TrainMethord where cid='" + tMethod.Cid + "' and options='" + tMethod.Options + "'");

                if (dt.Rows.Count == 0)
                {
                    string sql = "insert into TB_TrainMethord(cid,options,note14)values(@cid1,@options1,@note141)";
                    try
                    {
                        conn.Open();
                        using (SQLiteCommand cmd = new SQLiteCommand(sql, conn))
                        {
                            cmd.Parameters.Add("@cid1", System.Data.DbType.String).Value = tMethod.Cid.ToString().Trim();
                            cmd.Parameters.Add("@options1", System.Data.DbType.String).Value = tMethod.Options;
                            cmd.Parameters.Add("@note141", System.Data.DbType.String).Value = tMethod.Note14;

                            cmd.ExecuteNonQuery();
                        }



                    }
                    catch (Exception e)
                    {
                        throw e;

                    }
                    finally
                    {

                        conn.Close();
                    }
                }
            }
        }

        public void Insert1(FLanguage fLanguage)//熟悉外语语种:TB_FamiliarForeign
        {

            if (fLanguage.Cid != null)
            {

                DataOperation dataOperation = new DataOperation();
                DataTable dt = dataOperation.GetOneDataTable_sql("select * from TB_FamiliarForeign where cid='" + fLanguage.Cid + "' and foreignKind='" + fLanguage.ForeignKind + "'");

                if (dt.Rows.Count == 0)
                {
                    string sql = "insert into TB_FamiliarForeign(cid,number,foreignKind,level)values(@cid1,@number1,@foreignKind1,@level1)";

                    try
                    {
                        conn.Open();
                        using (SQLiteCommand cmd = new SQLiteCommand(sql, conn))
                        {
                            cmd.Parameters.Add("@cid1", System.Data.DbType.String).Value = fLanguage.Cid;
                            cmd.Parameters.Add("@number1", System.Data.DbType.Int32).Value = fLanguage.Number;
                            cmd.Parameters.Add("@foreignKind1", System.Data.DbType.String).Value = fLanguage.ForeignKind;
                            cmd.Parameters.Add("@level1", System.Data.DbType.String).Value = fLanguage.Level;
                            cmd.ExecuteNonQuery();
                        }



                    }
                    catch (Exception e)
                    {
                        throw e;

                    }
                    finally
                    {

                        conn.Close();
                    }
                }
            }

        }

        public void IsExitLanguage(FLanguage fLanguage)
        {
            DataOperation dataOperation = new DataOperation();


        }
        #endregion

        #region//更新InformationGathering中的信息
        /// <summary>
        /// 更新海外工作表
        /// </summary>
        /// <param name="wAbroad">属性类的对象，包含了海外工作的所有信息</param>
        public void Update1(WAbroad wAbroad)
        {

            String sql = "update TB_WAbroad set startTime=@startTime1,endTime=@endTime1,abroadCountry=@abroadCountry1,departmentPosition=@departmentPosition1,specialtyArea=@specialtyArea1 where id='" + wAbroad.Id + "'";
            try
            {
                conn.Open();
                using (SQLiteCommand cmd = new SQLiteCommand(sql, conn))
                {
                    cmd.Parameters.Add("@startTime1", System.Data.DbType.String).Value = wAbroad.StartTime;
                    cmd.Parameters.Add("@endTime1", System.Data.DbType.String).Value = wAbroad.EndTime;
                    cmd.Parameters.Add("@abroadCountry1", System.Data.DbType.String).Value = wAbroad.Country.ToString();
                    cmd.Parameters.Add("@departmentPosition1", System.Data.DbType.String).Value = wAbroad.DepartmentPosition.ToString();
                    cmd.Parameters.Add("@specialtyArea1", System.Data.DbType.String).Value = wAbroad.Specialty.ToString();
                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception e)
            {
                throw e;

            }
            finally
            {

                conn.Close();
            }
        }

        public void Update1(SAbroad sAbroad)//海外学习
        {
            string sql = "update TB_SAbroad set startTime=@startTime1,endTime=@endTime1,country=@country1,academy=@academy1,degree=@degree1 where id='" + sAbroad.Id + "'";
            try
            {
                conn.Open();
                using (SQLiteCommand cmd = new SQLiteCommand(sql, conn))
                {

                    cmd.Parameters.Add("@startTime1", System.Data.DbType.String).Value = sAbroad.StartTime;
                    cmd.Parameters.Add("@endTime1", System.Data.DbType.String).Value = sAbroad.EndTime;
                    cmd.Parameters.Add("@country1", System.Data.DbType.String).Value = sAbroad.Country;
                    cmd.Parameters.Add("@academy1", System.Data.DbType.String).Value = sAbroad.Academy;
                    cmd.Parameters.Add("@degree1", System.Data.DbType.String).Value = sAbroad.Degree;
                    cmd.ExecuteNonQuery();
                }

            }
            catch (Exception e)
            {
                throw e;
            }
            finally
            {

                conn.Close();
            }
        }

        public void Update1(IContent iContent)//重大事项信息报告内容:TB_GreatContent
        {
            string sql = "update TB_GreatContent set content=@content1,matter=@matter1 where id='" + iContent.Id + "'";
            try
            {
                conn.Open();
                using (SQLiteCommand cmd = new SQLiteCommand(sql, conn))
                {

                    cmd.Parameters.Add("@content1", System.Data.DbType.String).Value = iContent.Content;
                    cmd.Parameters.Add("@matter1", System.Data.DbType.Int32).Value = iContent.Matter;
                    cmd.ExecuteNonQuery();
                }



            }
            catch (Exception e)
            {
                throw e;

            }
            finally
            {

                conn.Close();
            }
        }

        public void Update1(TMethod tMethod)//培养锻炼措施需求:TB_TrainMethord
        {

            string sql = "update TB_TrainMethord set  options=@options1,note14=@note141 where id='" + tMethod.Id + "'";
            try
            {
                conn.Open();
                using (SQLiteCommand cmd = new SQLiteCommand(sql, conn))
                {

                    cmd.Parameters.Add("@options1", System.Data.DbType.String).Value = tMethod.Options;
                    cmd.Parameters.Add("@note141", System.Data.DbType.String).Value = tMethod.Note14;
                    cmd.ExecuteNonQuery();
                }



            }
            catch (Exception e)
            {
                throw e;

            }
            finally
            {

                conn.Close();
            }
        }

        public void Update1(FLanguage fLanguage)//熟悉外语语种:TB_FamiliarForeign
        {
            string sql = "update  TB_FamiliarForeign set number=@number1,foreignKind=@foreignKind1,level=@level1 where id='" + fLanguage.Id + "'";
            try
            {
                conn.Open();
                using (SQLiteCommand cmd = new SQLiteCommand(sql, conn))
                {

                    cmd.Parameters.Add("@number1", System.Data.DbType.Int32).Value = fLanguage.Number;
                    cmd.Parameters.Add("@foreignKind1", System.Data.DbType.String).Value = fLanguage.ForeignKind;
                    cmd.Parameters.Add("@level1", System.Data.DbType.String).Value = fLanguage.Level;
                    cmd.ExecuteNonQuery();
                }



            }
            catch (Exception e)
            {
                throw e;

            }
            finally
            {

                conn.Close();
            }

        }


        #endregion
        #endregion
        /// <summary>
        /// 更新海外工作表
        /// </summary>
        /// <param name="wAbroad">属性类的对象，包含了海外工作的所有信息</param>
        public void Insertpeopleall(InCommonInfo commoInfo, string datatable)
        {
            try
            {
                string sql = "Insert into '" + datatable + "'(qd,name,unitclass,unitname,sex,department,position,birthday,age) values(@qd1,@name1,@unitclass1,@unitname1,@sex1,@department1,@position1,@birthday1,@age1)";
                conn.Open();
                using (SQLiteCommand cmd = new SQLiteCommand(sql, conn))
                {
                    cmd.Parameters.Add("@qd1", System.Data.DbType.Boolean).Value = commoInfo.Qd;
                    cmd.Parameters.Add("@name1", System.Data.DbType.String).Value = commoInfo.Name;
                    cmd.Parameters.Add("@unitclass1", System.Data.DbType.String).Value = commoInfo.Unitclass;
                    cmd.Parameters.Add("@unitname1", System.Data.DbType.String).Value = commoInfo.Unitname;
                    cmd.Parameters.Add("@sex1", System.Data.DbType.String).Value = commoInfo.Sex;
                    cmd.Parameters.Add("@department1", System.Data.DbType.String).Value = commoInfo.Department;
                    cmd.Parameters.Add("@position1", System.Data.DbType.String).Value = commoInfo.Position;
                    cmd.Parameters.Add("@birthday1", System.Data.DbType.String).Value = commoInfo.Birthday;
                    cmd.Parameters.Add("@age1", System.Data.DbType.Int16).Value = commoInfo.Age;
                    cmd.ExecuteNonQuery();
                }
            }
            catch (SQLiteException ex)
            {
                MessageBox.Show(ex.Message);

            }
            finally
            {
                conn.Close();
            }
        }

        /// <summary>
        /// 插入后备人员信息
        /// </summary>
        /// <param name="commoInfo">CommonInfo类型实例</param>
        public void Insertpeopleall(CommonInfo commoInfo)
        {
            try
            {
                string sql = "Insert into TB_CommonInfo (extend3, extend2, qd,cid,name,unitname,unitclass,sex,department,position,birthday,age,isdelete,jointeam,twoYGE,publicSelect,isTwoYear,isGuide,systemtime) values(@extend11, @extend22, @qd1,@cid1,@name1,@unitname1,@unitclass1,@sex1,@department1,@position1,@birthday1,@age1,@isdelete1,@jointeam1,@twoYGE1,@publicSelect1,@isTwoYear1,@isGuide1,@systemtime1)";
                conn.Open();
                using (SQLiteCommand cmd = new SQLiteCommand(sql, conn))
                {

                    cmd.Parameters.Add("@extend11", System.Data.DbType.String).Value = commoInfo.InitialFullSpelling;
                    cmd.Parameters.Add("@extend22", System.Data.DbType.String).Value = commoInfo.UnitNamePinYin;

                    cmd.Parameters.Add("@qd1", System.Data.DbType.Boolean).Value = commoInfo.Qd;
                    cmd.Parameters.Add("@name1", System.Data.DbType.String).Value = commoInfo.Name;
                    cmd.Parameters.Add("@cid1", System.Data.DbType.String).Value = commoInfo.Cid;
                    cmd.Parameters.Add("@unitname1", System.Data.DbType.String).Value = commoInfo.Unitname;
                    cmd.Parameters.Add("@unitclass1", System.Data.DbType.String).Value = commoInfo.UnitClass;
                    cmd.Parameters.Add("@sex1", System.Data.DbType.String).Value = commoInfo.Sex;
                    cmd.Parameters.Add("@department1", System.Data.DbType.String).Value = commoInfo.Department;
                    cmd.Parameters.Add("@position1", System.Data.DbType.String).Value = commoInfo.Position;
                    cmd.Parameters.Add("@birthday1", System.Data.DbType.String).Value = commoInfo.Birthday;
                    cmd.Parameters.Add("@age1", System.Data.DbType.Int16).Value = commoInfo.Age;
                    cmd.Parameters.Add("@isdelete1", System.Data.DbType.Boolean).Value = false;
                    cmd.Parameters.Add("@jointeam1", System.Data.DbType.Boolean).Value = false;
                    cmd.Parameters.Add("@twoYGE1", System.Data.DbType.Boolean).Value = false;
                    cmd.Parameters.Add("@publicSelect1", System.Data.DbType.Boolean).Value = false;
                    cmd.Parameters.Add("@isTwoYear1", System.Data.DbType.Boolean).Value = false;
                    cmd.Parameters.Add("@isGuide1", System.Data.DbType.Boolean).Value = false;
                    cmd.Parameters.Add("@systemtime1", System.Data.DbType.String).Value = commoInfo.SystemTime;
                    cmd.ExecuteNonQuery();
                }

            }
            catch (SQLiteException ex)
            {
                MessageBox.Show(ex.Message);

            }
            finally
            {
                conn.Close();
            }
        }

        #region
        /// <summary>
        /// 录入初步人选名册，插入数据库中
        /// </summary>
        /// <param name="commoInfo">CommonInfo实例</param>
        public void InsertNameSheet(CommonInfo commoInfo)
        {
            try
            {
                string sql = "Insert into TB_CommonInfo (extend3, extend2, qd,cid,name,age,unitname,unitclass,sex,nation,department,position,native,birthday,partytime,worktime,fulleducation,fulldegree,fullschool,fullspecialty," +
                    "workeducation,workdegree,workgraduate,workspecialty,technicalPost,experiencePost,knowfield,traindirection,trainmeasure,jointeam,partyClass,grade,SPDegree,isTwoYear,isGuide,systemTime,publicSelect,twoYGE,isdelete) values(@extend11, @extend22, @qd1,@cid1,@name1,@age1,@unitname1,@unitclass1,@sex1," +
                    "@nation1,@department1,@position1,@native1,@birthday1,@partyTime1,@worktime1,@fullEducation1,@fullDegree1,@fullSchool1,@fullSpecialty1,@workEducation1," +
                    "@workDegree1,@workGraduate1,@workSpecialty1,@technicalPost1,@experiencePost1,@knowField1,@trainDirection1,@trainMeasure1, @joinTeam1,@partyClass1,@grade1,@SPDegree1,@isTwoYear1,@isGuide1,@systemTime1,@publicSelect1,@twoYGE1,@isdelete1)";

                conn.Open();
                using (SQLiteCommand cmd = new SQLiteCommand(sql, conn))
                {
                    cmd.Parameters.Add("@extend11", System.Data.DbType.String).Value = commoInfo.InitialFullSpelling;
                    cmd.Parameters.Add("@extend22", System.Data.DbType.String).Value = commoInfo.UnitNamePinYin;

                    cmd.Parameters.Add("@qd1", System.Data.DbType.Boolean).Value = commoInfo.Qd;
                    cmd.Parameters.Add("@name1", System.Data.DbType.String).Value = commoInfo.Name;
                    cmd.Parameters.Add("@cid1", System.Data.DbType.String).Value = commoInfo.Cid;
                    cmd.Parameters.Add("@age1", System.Data.DbType.Int32).Value = commoInfo.Age;
                    cmd.Parameters.Add("@unitname1", System.Data.DbType.String).Value = commoInfo.Unitname;
                    cmd.Parameters.Add("@unitclass1", System.Data.DbType.String).Value = commoInfo.UnitClass;

                    cmd.Parameters.Add("@sex1", System.Data.DbType.String).Value = commoInfo.Sex;
                    cmd.Parameters.Add("@nation1", System.Data.DbType.String).Value = commoInfo.Nation;
                    cmd.Parameters.Add("@department1", System.Data.DbType.String).Value = commoInfo.Department;
                    cmd.Parameters.Add("@position1", System.Data.DbType.String).Value = commoInfo.Position;
                    cmd.Parameters.Add("@native1", System.Data.DbType.String).Value = commoInfo.Native;
                    cmd.Parameters.Add("@birthday1", System.Data.DbType.String).Value = commoInfo.Birthday;
                    cmd.Parameters.Add("@partyTime1", System.Data.DbType.String).Value = commoInfo.PartyTime;
                    cmd.Parameters.Add("@worktime1", System.Data.DbType.String).Value = commoInfo.WorkTime;
                    cmd.Parameters.Add("@fullEducation1", System.Data.DbType.String).Value = commoInfo.FullEducation;
                    cmd.Parameters.Add("@fullDegree1", System.Data.DbType.String).Value = commoInfo.FullDegree;
                    cmd.Parameters.Add("@fullSchool1", System.Data.DbType.String).Value = commoInfo.FullSchool;
                    cmd.Parameters.Add("@fullSpecialty1", System.Data.DbType.String).Value = commoInfo.FullSpecialty;
                    cmd.Parameters.Add("@workEducation1", System.Data.DbType.String).Value = commoInfo.WorkEducation;
                    cmd.Parameters.Add("@workDegree1", System.Data.DbType.String).Value = commoInfo.WorkDegree;
                    cmd.Parameters.Add("@workGraduate1", System.Data.DbType.String).Value = commoInfo.WorkGraduate;
                    cmd.Parameters.Add("@workSpecialty1", System.Data.DbType.String).Value = commoInfo.WorkSpecialty;
                    cmd.Parameters.Add("@technicalPost1", System.Data.DbType.String).Value = commoInfo.TechnicalPost;
                    cmd.Parameters.Add("@experiencePost1", System.Data.DbType.String).Value = commoInfo.ExperiencePost;
                    cmd.Parameters.Add("@knowField1", System.Data.DbType.String).Value = commoInfo.KnowField;
                    cmd.Parameters.Add("@trainDirection1", System.Data.DbType.String).Value = commoInfo.TrainDirection;
                    cmd.Parameters.Add("@trainMeasure1", System.Data.DbType.String).Value = commoInfo.TrainMeasure;
                    cmd.Parameters.Add("@joinTeam1", System.Data.DbType.Boolean).Value = commoInfo.JoinTeam;
                    cmd.Parameters.Add("@partyClass1", System.Data.DbType.String).Value = commoInfo.PartyClass;
                    cmd.Parameters.Add("@grade1", System.Data.DbType.Int32).Value = commoInfo.Grade;
                    cmd.Parameters.Add("@SPDegree1", System.Data.DbType.String).Value = commoInfo.SPDegree;
                    cmd.Parameters.Add("@isTwoYear1", System.Data.DbType.Boolean).Value = commoInfo.IsTwoYear;
                    cmd.Parameters.Add("@isGuide1", System.Data.DbType.Boolean).Value = commoInfo.IsGuide;
                    cmd.Parameters.Add("@systemTime1", System.Data.DbType.String).Value = commoInfo.SystemTime;
                    cmd.Parameters.Add("@publicSelect1", System.Data.DbType.Boolean).Value = commoInfo.Publicselect;
                    cmd.Parameters.Add("@twoYGE1", System.Data.DbType.Boolean).Value = commoInfo.TYGE1;
                    cmd.Parameters.Add("@isdelete1", System.Data.DbType.Boolean).Value = false;
                    cmd.ExecuteNonQuery();
                }

            }
            catch (SQLiteException ex)
            {
                MessageBox.Show(ex.Message);

            }
            finally
            {
                conn.Close();
            }
        }

        /// <summary>
        /// 更新人选名册
        /// </summary>
        /// <param name="commoInfo">CommonInfo类型实例，新的cid号</param>
        /// <param name="newId"></param>
        public void UpdateNameSheet(CommonInfo commoInfo, string newId)
        {
            try
            {
                string sql = "update  TB_CommonInfo set extend3 = @extend11, extend2 = @extend22, cid=@newCid,name=@name1,sex=@sex1,nation=@nation1,department=@department1,position=@position1,native=@native1,birthday=@birthday1,partytime=@partyTime1,worktime=@worktime1,fulleducation=@fullEducation1,fulldegree=@fullDegree1,fullschool=@fullSchool1,fullspecialty=@fullSpecialty1,workeducation=@workEducation1,workdegree=@workDegree1,workgraduate=@workGraduate1,workspecialty=@workSpecialty1,technicalPost=@technicalPost1,experiencePost=@experiencePost1,knowfield=@knowField1,traindirection=@trainDirection1,trainmeasure=@trainMeasure1,jointeam=@joinTeam1,partyClass=@partyClass1,grade=@grade1,SPDegree=@SPDegree1,isTwoYear=@isTwoYear1,isGuide=@isGuide1,systemTime=@systemTime1,publicSelect = @publicSelect1,twoYGE = @twoYGE1,isdelete = @isdelete1 where cid='" + commoInfo.Cid + "'";
                conn.Open();
                using (SQLiteCommand cmd = new SQLiteCommand(sql, conn))
                {
                    cmd.Parameters.Add("@extend11", System.Data.DbType.String).Value = commoInfo.InitialFullSpelling;
                    cmd.Parameters.Add("@extend22", System.Data.DbType.String).Value = commoInfo.UnitNamePinYin;

                    cmd.Parameters.Add("@newCid", System.Data.DbType.String).Value = commoInfo.Cid;
                    cmd.Parameters.Add("@name1", System.Data.DbType.String).Value = commoInfo.Name;
                    cmd.Parameters.Add("@sex1", System.Data.DbType.String).Value = commoInfo.Sex;
                    cmd.Parameters.Add("@nation1", System.Data.DbType.String).Value = commoInfo.Nation;
                    cmd.Parameters.Add("@department1", System.Data.DbType.String).Value = commoInfo.Department;
                    cmd.Parameters.Add("@position1", System.Data.DbType.String).Value = commoInfo.Position;
                    cmd.Parameters.Add("@native1", System.Data.DbType.String).Value = commoInfo.Native;
                    cmd.Parameters.Add("@birthday1", System.Data.DbType.String).Value = commoInfo.Birthday;
                    cmd.Parameters.Add("@partyTime1", System.Data.DbType.String).Value = commoInfo.PartyTime;
                    cmd.Parameters.Add("@worktime1", System.Data.DbType.String).Value = commoInfo.WorkTime;
                    cmd.Parameters.Add("@fullEducation1", System.Data.DbType.String).Value = commoInfo.FullEducation;
                    cmd.Parameters.Add("@fullDegree1", System.Data.DbType.String).Value = commoInfo.FullDegree;
                    cmd.Parameters.Add("@fullSchool1", System.Data.DbType.String).Value = commoInfo.FullSchool;
                    cmd.Parameters.Add("@fullSpecialty1", System.Data.DbType.String).Value = commoInfo.FullSpecialty;
                    cmd.Parameters.Add("@workEducation1", System.Data.DbType.String).Value = commoInfo.WorkEducation;
                    cmd.Parameters.Add("@workDegree1", System.Data.DbType.String).Value = commoInfo.WorkDegree;
                    cmd.Parameters.Add("@workGraduate1", System.Data.DbType.String).Value = commoInfo.WorkGraduate;
                    cmd.Parameters.Add("@workSpecialty1", System.Data.DbType.String).Value = commoInfo.WorkSpecialty;
                    cmd.Parameters.Add("@technicalPost1", System.Data.DbType.String).Value = commoInfo.TechnicalPost;
                    cmd.Parameters.Add("@experiencePost1", System.Data.DbType.String).Value = commoInfo.ExperiencePost;
                    cmd.Parameters.Add("@knowField1", System.Data.DbType.String).Value = commoInfo.KnowField;
                    cmd.Parameters.Add("@trainDirection1", System.Data.DbType.String).Value = commoInfo.TrainDirection;
                    cmd.Parameters.Add("@trainMeasure1", System.Data.DbType.String).Value = commoInfo.TrainMeasure;
                    cmd.Parameters.Add("@joinTeam1", DbType.Boolean).Value = commoInfo.JoinTeam;
                    cmd.Parameters.Add("@partyClass1", System.Data.DbType.String).Value = commoInfo.PartyClass;
                    cmd.Parameters.Add("@grade1", System.Data.DbType.Int32).Value = commoInfo.Grade;
                    cmd.Parameters.Add("@SPDegree1", System.Data.DbType.String).Value = commoInfo.SPDegree;
                    cmd.Parameters.Add("@isTwoYear1", System.Data.DbType.Boolean).Value = commoInfo.IsTwoYear;
                    cmd.Parameters.Add("@isGuide1", System.Data.DbType.Boolean).Value = commoInfo.IsGuide;
                    cmd.Parameters.Add("@systemTime1", System.Data.DbType.String).Value = commoInfo.SystemTime;
                    cmd.Parameters.Add("@publicSelect1", System.Data.DbType.Boolean).Value = commoInfo.Publicselect;
                    cmd.Parameters.Add("@twoYGE1", System.Data.DbType.Boolean).Value = commoInfo.TYGE1;
                    cmd.Parameters.Add("@isdelete1", System.Data.DbType.Boolean).Value = false;
                    cmd.ExecuteNonQuery();
                }
                conn.Close();
                if (!commoInfo.Cid.Equals(newId))
                {
                    UpdataTada15(commoInfo.Cid, newId);
                }
            }
            catch (SQLiteException ex)
            {
                MessageBox.Show("录入失败！" + ex.Message);
            }
            finally
            {
                conn.Close();
            }
        }

        /// <summary>
        /// 更新cid
        /// </summary>
        /// <param name="cidOld">更新前的cid</param>
        /// <param name="cidNew">新的cid</param>
        /// <returns></returns>
        public bool UpdataTada15(string cidOld, string cidNew) //修改上报端人员的CID
        {
            bool b = false;
            #region
            string str1 = "update TB_CommonInfo set CID=@CID1 where CID=@cid2";
            string str2 = "update TB_COUNTY set CID=@CID1 where CID=@cid2";
            
            string str9 = "update TB_FamiliarForeign set CID=@CID1 where CID=@cid2";
            string str10 = "update TB_Family set CID=@CID1 where CID=@cid2";
            string str11 = "update TB_GreatContent set CID=@CID1 where CID=@cid2";
            string str13 = "update TB_PunishAward set CID=@CID1 where CID=@cid2";
            string str14 = "update TB_Resume set CID=@CID1 where CID=@cid2";
            string str15 = "update TB_SAbroad set CID=@CID1 where CID=@cid2";
            string str16 = "update TB_TrainExercise set CID=@CID1 where CID=@cid2";
            string str17 = "update TB_TrainMethord set CID=@CID1 where CID=@cid2";
            string str18 = "update TB_WAbroad set CID=@CID1 where CID=@cid2";
            #endregion
            try
            {
                conn.Open();
                using (SQLiteCommand cmd = new SQLiteCommand(str1, conn))
                {
                    cmd.Parameters.Add("@CID1", System.Data.DbType.String).Value = cidNew;
                    cmd.Parameters.Add("@cid2", System.Data.DbType.String).Value = cidOld;
                    cmd.ExecuteNonQuery();
                }
                using (SQLiteCommand cmd = new SQLiteCommand(str2, conn))
                {
                    cmd.Parameters.Add("@CID1", System.Data.DbType.String).Value = cidNew;
                    cmd.Parameters.Add("@cid2", System.Data.DbType.String).Value = cidOld;
                    cmd.ExecuteNonQuery();
                }
                using (SQLiteCommand cmd = new SQLiteCommand(str9, conn))
                {
                    cmd.Parameters.Add("@CID1", System.Data.DbType.String).Value = cidNew;
                    cmd.Parameters.Add("@cid2", System.Data.DbType.String).Value = cidOld;
                    cmd.ExecuteNonQuery();
                }
                using (SQLiteCommand cmd = new SQLiteCommand(str10, conn))
                {
                    cmd.Parameters.Add("@CID1", System.Data.DbType.String).Value = cidNew;
                    cmd.Parameters.Add("@cid2", System.Data.DbType.String).Value = cidOld;
                    cmd.ExecuteNonQuery();
                }
                using (SQLiteCommand cmd = new SQLiteCommand(str11, conn))
                {
                    cmd.Parameters.Add("@CID1", System.Data.DbType.String).Value = cidNew;
                    cmd.Parameters.Add("@cid2", System.Data.DbType.String).Value = cidOld;
                    cmd.ExecuteNonQuery();
                }
                using (SQLiteCommand cmd = new SQLiteCommand(str13, conn))
                {
                    cmd.Parameters.Add("@CID1", System.Data.DbType.String).Value = cidNew;
                    cmd.Parameters.Add("@cid2", System.Data.DbType.String).Value = cidOld;
                    cmd.ExecuteNonQuery();
                }
                using (SQLiteCommand cmd = new SQLiteCommand(str14, conn))
                {
                    cmd.Parameters.Add("@CID1", System.Data.DbType.String).Value = cidNew;
                    cmd.Parameters.Add("@cid2", System.Data.DbType.String).Value = cidOld;
                    cmd.ExecuteNonQuery();
                }
                using (SQLiteCommand cmd = new SQLiteCommand(str15, conn))
                {
                    cmd.Parameters.Add("@CID1", System.Data.DbType.String).Value = cidNew;
                    cmd.Parameters.Add("@cid2", System.Data.DbType.String).Value = cidOld;
                    cmd.ExecuteNonQuery();
                }
                using (SQLiteCommand cmd = new SQLiteCommand(str16, conn))
                {
                    cmd.Parameters.Add("@CID1", System.Data.DbType.String).Value = cidNew;
                    cmd.Parameters.Add("@cid2", System.Data.DbType.String).Value = cidOld;
                    cmd.ExecuteNonQuery();
                }
                using (SQLiteCommand cmd = new SQLiteCommand(str17, conn))
                {
                    cmd.Parameters.Add("@CID1", System.Data.DbType.String).Value = cidNew;
                    cmd.Parameters.Add("@cid2", System.Data.DbType.String).Value = cidOld;
                    cmd.ExecuteNonQuery();
                }
                using (SQLiteCommand cmd = new SQLiteCommand(str18, conn))
                {
                    cmd.Parameters.Add("@CID1", System.Data.DbType.String).Value = cidNew;
                    cmd.Parameters.Add("@cid2", System.Data.DbType.String).Value = cidOld;
                    cmd.ExecuteNonQuery();
                }
                return b;
            }
            catch (Exception ex)
            {

                throw ex;

            }
            finally
            {
                conn.Close();
            }

        }
        #endregion



        /// <summary>
        /// 插入考察材料
        /// </summary>
        /// <param name="cid">人员cid</param>
        /// <param name="material">考察材料的内容</param>
        public void InsertMaterial(string cid, string material)
        {
            try
            {
                conn.Open();
                using (SQLiteCommand cmd = new SQLiteCommand("update TB_CommonInfo set material=@material1 where cid='" + cid + "'", conn))
                {

                    cmd.Parameters.Add("@material1", System.Data.DbType.String).Value = material;
                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show("插入考察材料时出错！" + ex.Message);
            }
            finally
            {

                conn.Close();
            }

        }

        /// <summary>
        /// 根据SQL语句得到行数
        /// </summary>
        /// <param name="sql"></param>
        /// <returns></returns>
        public int GetRows(string sql)
        {
            int count;
            try
            {
                conn.Open();
                SQLiteCommand cmd = new SQLiteCommand(sql, conn);
                count = Convert.ToInt32(cmd.ExecuteScalar());
                return count;
            }
            catch
            {
                return 0;
            }
            finally
            {
                conn.Close();
            }
        }

        /// <summary>
        /// 插入cid到谈话会议表
        /// </summary>
        /// <param name="cid"></param>
        public void InsertCID_Six(string cid)
        {
            string sql1 = "insert into TB_Meeting_AB (cid)values(@cid1)";
            string sql2 = "insert into TB_Meeting_ABC (cid)values(@cid2)";
            string sql3 = "insert into TB_Meeting_ABCD (cid)values(@cid3)";
            string sql4 = "insert into TB_Dialog_AB (cid)values(@cid4)";
            string sql5 = "insert into TB_Dialog_ABC(cid)values(@cid5)";
            string sql6 = "insert into TB_Dialog_ABCD(cid)values(@cid6)";
            try
            {
                conn.Open();
                using (SQLiteCommand cmd = new SQLiteCommand(sql1, conn))
                {
                    cmd.Parameters.Add("@cid1", System.Data.DbType.String).Value = cid;
                    cmd.ExecuteNonQuery();
                }


                using (SQLiteCommand cmd = new SQLiteCommand(sql2, conn))
                {
                    cmd.Parameters.Add("@cid2", System.Data.DbType.String).Value = cid;
                    cmd.ExecuteNonQuery();
                }


                using (SQLiteCommand cmd = new SQLiteCommand(sql3, conn))
                {
                    cmd.Parameters.Add("@cid3", System.Data.DbType.String).Value = cid;
                    cmd.ExecuteNonQuery();
                }


                using (SQLiteCommand cmd = new SQLiteCommand(sql4, conn))
                {
                    cmd.Parameters.Add("@cid4", System.Data.DbType.String).Value = cid;
                    cmd.ExecuteNonQuery();
                }


                using (SQLiteCommand cmd = new SQLiteCommand(sql5, conn))
                {
                    cmd.Parameters.Add("@cid5", System.Data.DbType.String).Value = cid;
                    cmd.ExecuteNonQuery();
                }


                using (SQLiteCommand cmd = new SQLiteCommand(sql6, conn))
                {
                    cmd.Parameters.Add("@cid6", System.Data.DbType.String).Value = cid;
                    cmd.ExecuteNonQuery();
                }



            }
            catch (Exception e)
            {
                throw e;

            }
            finally
            {
                conn.Close();
            }

        }

        public void DeleteListViewUnit(string unitName, string unitClass)
        {
            string sql = "delete from TB_UNIT where unitName=@unitName1 and unitClass=@unitClass1";
            try
            {

                conn.Open();
                using (SQLiteCommand cmd = new SQLiteCommand(sql, conn))
                {
                    cmd.Parameters.Add("unitName1", System.Data.DbType.String).Value = unitName;
                    cmd.Parameters.Add("unitClass1", System.Data.DbType.String).Value = unitClass;
                    cmd.ExecuteNonQuery();
                }



            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);


            }
            finally
            {

                conn.Close();
            }

        }

        #region
        //对ListView进行排序，修改TB_CommonInfo中的number字段
        public void OrderTable(List<Order> list)
        {
            string sql = "";
            for (int i = 0; i < list.Count; i++)
            {

                sql = "update TB_CommonInfo set number='" + list[i].Number + "' where cid='" + list[i].Cid + "'";
                try
                {
                    using (SQLiteCommand cmd = new SQLiteCommand(sql, conn))
                    {
                        cmd.ExecuteNonQuery();
                    }

                }
                catch (Exception ex)
                {
                    MessageBox.Show("在更新第" + i + "条记录时出错！" + ex.Message);
                }



            }


        }
        #endregion

        #region//拖拽后修改list中number值
        public void OrderList(List<Order> list, Int32 previous, Int32 now)
        {
            Int32 temp;
            temp = list[previous].Number;
            list[previous].Number = list[now].Number;
            list[now].Number = temp;

        }
        #endregion

        //更新信息
        #region
        /// <summary>
        /// 更新数据库
        /// </summary>
        /// <param name="rp">更新数据库所需要的属性类实例</param>
        /// <param name="n">更新信息的条数</param>
        public void UpdateResume(ResumeProperty rp, int n)
        {
            string sql = "UPDATE TB_Resume set betime = @betime1, entime = @entime1 , content = @content1 WHERE ID ='" + n + "'";
            conn.Open();
            using (SQLiteCommand cmd = new SQLiteCommand(sql, conn))
            {
                cmd.Parameters.Add("@betime1", System.Data.DbType.String).Value = rp.Betime.ToString();
                cmd.Parameters.Add("@entime1", System.Data.DbType.String).Value = rp.Entime.ToString();
                cmd.Parameters.Add("@content1", System.Data.DbType.String).Value = rp.Content.ToString();
                cmd.ExecuteNonQuery();
            }
            conn.Close();

        }

        #endregion

        #region
        /// <summary>
        /// 删除个人简历表里的记录
        /// </summary>
        /// <param name="n">要删除记录的id号</param>
        public void DeleteResume(int n)
        {
            string sql = "DELETE FROM TB_Resume WHERE ID='" + n + "'";
            try
            {
                conn.Open();
                SQLiteCommand cmd = new SQLiteCommand(sql, conn);
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                conn.Close();
            }
        }
        #endregion

        /// <summary>
        /// 民主测评表删除之前错误录入的抓党建信息
        /// </summary>
        /// <param name="id">要删除记录的id号</param>
        public void deletedb(string id)
        {
            string sql = "DELETE FROM TB_COUNTY WHERE CID='" + id + "'";
            try
            {
                conn.Open();
                SQLiteCommand cmd = new SQLiteCommand(sql, conn);
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                conn.Close();
            }


        }


        #region

        /// <summary>
        /// // 向个人简历表添加信息
        /// </summary>
        /// <param name="Resume">ResumeProperty类对象</param>
        /// <returns>int类型</returns>
        public int InsertResume(ResumeProperty Resume)
        {
            try
            {
                string sql = "insert into TB_Resume(cid,betime,entime,content) values(@cid,@betime,@entime,@content)";
                conn.Open();
                using (SQLiteCommand cmd = new SQLiteCommand(sql, conn))
                {
                    cmd.Parameters.Add("@cid", System.Data.DbType.String).Value = Resume.Cid;
                    cmd.Parameters.Add("@betime", System.Data.DbType.String).Value = Resume.Betime;
                    cmd.Parameters.Add("@entime", System.Data.DbType.String).Value = Resume.Entime;
                    cmd.Parameters.Add("@content", System.Data.DbType.String).Value = Resume.Content;
                    cmd.ExecuteNonQuery();
                }

                string sql1 = "select MAX(ID) FROM TB_Resume";
                DataTable dt = new DataTable();
                SQLiteCommand comd = new SQLiteCommand(sql1, conn);
                SQLiteDataAdapter da = new SQLiteDataAdapter(comd);//容易忘记添加操作语句
                da.Fill(dt);
                int n = Convert.ToInt32(dt.Rows[0][0].ToString());
                return n;
            }
            catch (SQLiteException ex)
            {
                MessageBox.Show(ex.Message);
                return 0;
            }
            finally
            {
                conn.Close();
            }
        }
        #endregion

        //读取简历
        /// <summary>
        /// 
        /// </summary>
        /// <param name="s">id号</param>
        /// <returns>返回DataTable类型</returns>
        public DataTable resume(string s)
        {
            try
            {
                string sql = "select * FROM TB_Resume WHERE CID ='" + s + "'";
                SQLiteCommand commd = new SQLiteCommand(sql, conn);
                SQLiteDataAdapter da = new SQLiteDataAdapter(commd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                conn.Close();
                return dt;
            }
            catch (SQLiteException e)
            {
                MessageBox.Show(e.Message); return null;
            }
        }

        public void saveNumber(Object nAndC)
        {
            string[] numAndCid = nAndC.ToString().Split(',');
            int m = numAndCid.Length / 2;
            for (int i = 0; i < m; i++)
            {
                string sql0 = "Update TB_CommonInfo set rank = '" + numAndCid[i * 2] + "' WHERE cid = '" + numAndCid[i * 2 + 1] + "'";
                OperateData_sql(sql0);
            }
        }

        /// <summary>
        /// 更新TB_Commoninfo表
        /// </summary>
        /// <param name="commoninfo">其中cid为修改前的cid</param>
        /// <param name="cid">新的cid</param>
        public void UpdatePeopleAll(CommonInfo commoninfo, string cid)
        {
            try
            {
                string sql;
                conn.Open();
                sql = "update TB_CommonInfo set extend3= @extend33, extend2 = @extend22, unitclass = @unitclass1, unitname=@unitname1, name=@name1,sex=@sex1,birthday = @birthday1,age = @age1,department=@department1,position=@position1,systemtime = @systemtime1 where cid='" + commoninfo.Cid + "'";
                using (SQLiteCommand cmd = new SQLiteCommand(sql, conn))
                {
                    cmd.Parameters.Add("@extend33", System.Data.DbType.String).Value = commoninfo.InitialFullSpelling;
                    cmd.Parameters.Add("@extend22", System.Data.DbType.String).Value = commoninfo.UnitNamePinYin;

                    cmd.Parameters.Add("@unitclass1", System.Data.DbType.String).Value = commoninfo.UnitClass;
                    cmd.Parameters.Add("@unitname1", System.Data.DbType.String).Value = commoninfo.Unitname;
                    cmd.Parameters.Add("@name1", System.Data.DbType.String).Value = commoninfo.Name;
                    cmd.Parameters.Add("@sex1", System.Data.DbType.String).Value = commoninfo.Sex;
                    cmd.Parameters.Add("@birthday1", System.Data.DbType.String).Value = commoninfo.Birthday;
                    cmd.Parameters.Add("@age1", System.Data.DbType.String).Value = commoninfo.Age;
                    cmd.Parameters.Add("@department1", System.Data.DbType.String).Value = commoninfo.Department;
                    cmd.Parameters.Add("@position1", System.Data.DbType.String).Value = commoninfo.Position;
                    cmd.Parameters.Add("@systemtime1", System.Data.DbType.String).Value = commoninfo.SystemTime;
                    cmd.ExecuteNonQuery();
                }
                conn.Close();
                UpdataTada15(commoninfo.Cid, cid);
            }
            catch (SQLiteException ex)
            {
                conn.Close();
                MessageBox.Show(ex.Message);

            }
            finally
            {

            }
        }

        /// <summary>
        /// 更新TB_Commoninfo,只更新非cid的字段
        /// </summary>
        /// <param name="commoninfo">Commoninfo类对象</param>
        public void UpdatePeopleAll(CommonInfo commoninfo)
        {
            try
            {
                string sql;
                conn.Open();
                sql = "update TB_CommonInfo set extend3 = @extend11, extend2 = @extend22, unitclass = @unitclass1, unitname=@unitname1, name=@name1,sex=@sex1,birthday = @birthday1,age = @age1,department=@department1,position=@position1 where cid='" + commoninfo.Cid + "'";
                using (SQLiteCommand cmd = new SQLiteCommand(sql, conn))
                {
                    cmd.Parameters.Add("@extend11", System.Data.DbType.String).Value = commoninfo.InitialFullSpelling;
                    cmd.Parameters.Add("@extend22", System.Data.DbType.String).Value = commoninfo.UnitNamePinYin;

                    cmd.Parameters.Add("@unitclass1", System.Data.DbType.String).Value = commoninfo.UnitClass;
                    cmd.Parameters.Add("@unitname1", System.Data.DbType.String).Value = commoninfo.Unitname;
                    cmd.Parameters.Add("@name1", System.Data.DbType.String).Value = commoninfo.Name;
                    cmd.Parameters.Add("@sex1", System.Data.DbType.String).Value = commoninfo.Sex;
                    cmd.Parameters.Add("@birthday1", System.Data.DbType.String).Value = commoninfo.Birthday;
                    cmd.Parameters.Add("@age1", System.Data.DbType.String).Value = commoninfo.Age;
                    cmd.Parameters.Add("@department1", System.Data.DbType.String).Value = commoninfo.Department;
                    cmd.Parameters.Add("@position1", System.Data.DbType.String).Value = commoninfo.Position;
                    cmd.Parameters.Add("@systemtime1", System.Data.DbType.String).Value = commoninfo.SystemTime;
                    cmd.ExecuteNonQuery();
                }
                conn.Close();
            }
            catch (SQLiteException ex)
            {
                conn.Close();
                MessageBox.Show(ex.Message);

            }
            finally
            {

            }
        }

        /// <summary>
        /// 更新TB_NMeeting或TB_NDialog
        /// </summary>
        /// <param name="incommoninfo">Incommoninfo类对象</param>
        /// <param name="s">要更新的表</param>
        public void UpdatePeopleAll(InCommonInfo incommoninfo, string s)
        {
            try
            {
                string sql;
                conn.Open();
                sql = "update '" + s + "' set unitclass = @unitclass1, unitname=@unitname1, name=@name1,sex=@sex1,birthday = @birthday1,age = @age1,department=@department1,position=@position1 where id='" + incommoninfo.Id + "'";
                using (SQLiteCommand cmd = new SQLiteCommand(sql, conn))
                {
                    cmd.Parameters.Add("@unitclass1", System.Data.DbType.String).Value = incommoninfo.Unitclass;
                    cmd.Parameters.Add("@unitname1", System.Data.DbType.String).Value = incommoninfo.Unitname;
                    cmd.Parameters.Add("@name1", System.Data.DbType.String).Value = incommoninfo.Name;
                    cmd.Parameters.Add("@sex1", System.Data.DbType.String).Value = incommoninfo.Sex;
                    cmd.Parameters.Add("@birthday1", System.Data.DbType.String).Value = incommoninfo.Birthday;
                    cmd.Parameters.Add("@age1", System.Data.DbType.String).Value = incommoninfo.Age;
                    cmd.Parameters.Add("@department1", System.Data.DbType.String).Value = incommoninfo.Department;
                    cmd.Parameters.Add("@position1", System.Data.DbType.String).Value = incommoninfo.Position;
                    cmd.ExecuteNonQuery();
                }
                conn.Close();
            }
            catch (SQLiteException ex)
            {
                conn.Close();
                MessageBox.Show(ex.Message);

            }
            finally
            {

            }
        }

        /// <summary>
        /// 插入家庭成员关系
        /// </summary>
        /// <param name="family">ClassFamily类型的实例</param>
        /// <returns></returns>
        public int InsertFamily(ClassFamily family)
        {
            try
            {
                string sql = "insert into TB_family(cid,relationship,name,birthday,country,party,nation,deptJob,age,remark) values(@cid1,@relationship1,@name1,@birthday1, @country1,@party1,@nation1,@deptJob1,@age1,@remark1)";

                conn.Open();

                using (SQLiteCommand cmd = new SQLiteCommand(sql, conn))
                {
                    cmd.Parameters.Add("@cid1", System.Data.DbType.String).Value = family.Cid;
                    cmd.Parameters.Add("@relationship1", System.Data.DbType.String).Value = family.Call;
                    cmd.Parameters.Add("@name1", System.Data.DbType.String).Value = family.Name;
                    cmd.Parameters.Add("@birthday1", System.Data.DbType.String).Value = family.Birthday;
                    cmd.Parameters.Add("@party1", System.Data.DbType.String).Value = family.PartyClass;
                    cmd.Parameters.Add("@country1", System.Data.DbType.String).Value = family.Country;
                    cmd.Parameters.Add("@nation1", System.Data.DbType.String).Value = family.Nation;
                    cmd.Parameters.Add("@deptJob1", System.Data.DbType.String).Value = family.WorkPostion;
                    cmd.Parameters.Add("@age1", System.Data.DbType.String).Value = family.Age;
                    cmd.Parameters.Add("@remark1", System.Data.DbType.String).Value = family.Remark;
                    cmd.ExecuteNonQuery();
                }


                string sql1 = "SELECT MAX(ID) FROM TB_family";
                DataTable dt = new DataTable();
                SQLiteCommand comd = new SQLiteCommand(sql1, conn);
                SQLiteDataAdapter da = new SQLiteDataAdapter(comd);//容易忘记添加操作语句
                da.Fill(dt);
                int n = Convert.ToInt32(dt.Rows[0][0].ToString());
                return n;
            }

            catch (SQLiteException ex)
            {
                MessageBox.Show(ex.Message);
                return 0;
            }
            finally
            {
                conn.Close();
            }
        }

        /// <summary>
        /// 插入奖惩情况
        /// </summary>
        /// <param name="punishAward">PunishAward类型的实例</param>
        /// <returns></returns>
        public int InsertAward(PunishAward punishAward)
        {
            try
            {
                string sql = "insert into TB_PunishAward(cid,class,time,grade,department) values(@cid1,@class1,@time1,@grade1,@department1)";
                conn.Open();
                using (SQLiteCommand cmd = new SQLiteCommand(sql, conn))
                {
                    cmd.Parameters.Add("@cid1", System.Data.DbType.String).Value = punishAward.Cid;
                    cmd.Parameters.Add("@class1", System.Data.DbType.String).Value = punishAward.AwardClass;
                    cmd.Parameters.Add("@time1", System.Data.DbType.String).Value = punishAward.Time;
                    cmd.Parameters.Add("@grade1", System.Data.DbType.String).Value = punishAward.Degree;
                    cmd.Parameters.Add("@department1", System.Data.DbType.String).Value = punishAward.Content;
                    cmd.ExecuteNonQuery();
                }
                string sql1 = "SELECT MAX(ID) FROM TB_PunishAward";
                DataTable dt = new DataTable();
                SQLiteCommand comd = new SQLiteCommand(sql1, conn);
                SQLiteDataAdapter da = new SQLiteDataAdapter(comd);//容易忘记添加操作语句
                da.Fill(dt);
                int n = Convert.ToInt32(dt.Rows[0][0].ToString());
                return n;
            }
            catch (SQLiteException ex)
            {
                MessageBox.Show(ex.Message);
                return 0;
            }
            finally
            {
                conn.Close();
            }
        }

        /// <summary>
        /// 参加培训和实践锻炼情况:TB_TrainExercise
        /// </summary>
        /// <param name="train">Train类型的实例</param>
        public int InsertTrainExercise(Train train)
        {
            if (train.Cid != null)
            {
                string sql = "insert into TB_TrainExercise(cid,reportContent,reportMatter,startTime,endTime,content)values(@cid1,@reportContent1,@reportMatter1,@startTime1,@endTime1,@content1)";
                try
                {
                    conn.Open();
                    using (SQLiteCommand cmd = new SQLiteCommand(sql, conn))
                    {
                        cmd.Parameters.Add("@cid1", System.Data.DbType.String).Value = train.Cid;
                        cmd.Parameters.Add("@reportContent1", System.Data.DbType.String).Value = train.ReportContent;
                        cmd.Parameters.Add("@reportMatter1", System.Data.DbType.String).Value = train.ReportMatter;
                        cmd.Parameters.Add("@startTime1", System.Data.DbType.String).Value = train.StartTime;
                        cmd.Parameters.Add("@endTime1", System.Data.DbType.String).Value = train.EndTime;
                        cmd.Parameters.Add("@content1", System.Data.DbType.String).Value = train.Content1;
                        cmd.ExecuteNonQuery();
                    }
                    string sql1 = "SELECT MAX(ID) FROM TB_TrainExercise";
                    DataTable dt = new DataTable();
                    SQLiteCommand comd = new SQLiteCommand(sql1, conn);
                    SQLiteDataAdapter da = new SQLiteDataAdapter(comd);//容易忘记添加操作语句
                    da.Fill(dt);
                    int n = Convert.ToInt32(dt.Rows[0][0].ToString());
                    return n;
                }
                catch (Exception e)
                {
                    MessageBox.Show(e.Message);
                    return 0;
                }
                finally
                {
                    conn.Close();
                }
            }
            else
            {
                return 0;
            }
        }

        /// <summary>
        /// 修改总表中的信息
        /// </summary>
        /// <param name="unitName">单位名称</param>
        /// <param name="unitClass">单位类别</param>
        /// <param name="uid">单位的id号</param>
        public void ModifyUnit(string unitName, string unitClass, string uid)
        {
            try
            {
                string sql = "update TB_UNIT set unitName=@unitName1,unitClass=@unitClass1 where uid = '" + uid + "'";

                conn.Open();
                using (SQLiteCommand cmd = new SQLiteCommand(sql, conn))
                {
                    cmd.Parameters.Add("@unitName1", System.Data.DbType.String).Value = unitName;
                    cmd.Parameters.Add("@unitClass1", System.Data.DbType.String).Value = unitClass;
                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
    /// <summary>
    /// 异常处理类
    /// </summary>
    public class CryptoHelpException : ApplicationException
    {
        public CryptoHelpException(string msg) : base(msg) { }
    }
}



