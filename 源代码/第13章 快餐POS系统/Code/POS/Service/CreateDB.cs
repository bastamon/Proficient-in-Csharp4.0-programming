using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using POS.Common;
using System.Data;
using System.Windows.Forms;
using System.Net;
using POS.View;
using System.IO;
using System.Data.SqlClient;
//创建POS机数据库 涉及所有前台POS数据库表
/*主要公共方法：
（1）、IsExitDB()，判断本地数据库是否存在
（2）、AddUser(string cUserName, string Password, string DB)创建用户
（3）、AddDataBase(string DBName, string DBPath, string logFileName)附加数据
（4）、LeaveDataBase(string DBName)分离数据库
（5）、CreateDatabase（）创建数据
*/
namespace POS.Service
{
    /// <summary>
    /// 创建数据库
    /// </summary>
    class CreateDB
    {

        string localIp ;
        string localCase  ;
        string localPort  ;
        string localDBName;
        string localUserName  ;
        string localPassword  ;
        ReadIni readIni;
        /// <summary>
        /// 判断本地要创建的数据库是否存在
        /// </summary>
         /// <returns>如果存在返回1表示存在数据库，返回0表示不存在数据库活着数据库没有附加上去，2表示本地信息配置有错误</returns>
        public int IsExitDB()
        {
            try
            {
                 readIni = new ReadIni();
                 localIp = readIni.ReadString("RepastErp", "txtDatabaseIP");
                 localCase = readIni.ReadString("RepastErp", "txtExampleName2");
                 localPort = readIni.ReadString("RepastErp", "txtPort2");
                 localDBName = readIni.ReadString("RepastErp", "txtDatabaseName");
                 localUserName = readIni.ReadString("RepastErp", "txtDatabaseUser");
                 localPassword = readIni.ReadString("RepastErp", "txtDatabasePassword");
                 if (localIp == "")
                 {
                     string strHostName = Dns.GetHostName(); //得到本机的主机名 
                     localIp = Dns.GetHostEntry(strHostName).AddressList[0].ToString(); //取得本机IP    
                     readIni.WriteString("RepastErp", "txtDatabaseIP", localIp);
                 } if (localPort=="")
                 {
                     localPort = "1433";
                     readIni.WriteString("RepastErp", "txtPort2", "1433");
                 }
                 if (localDBName == "")
                 {
                     localDBName = "RepastErp";
                     readIni.WriteString("RepastErp", "txtDatabaseName", "RepastErp");
                 }
                 if (localUserName == "")
                 {
                     localUserName = "sa";
                     readIni.WriteString("RepastErp", "txtDatabaseUser", "sa");
                 }
                 if (localPassword == "")
                 {
                     localPassword = "sa";
                     readIni.WriteString("RepastErp", "txtDatabasePassword", "sa");
                 }
                
                //连接master
                Info.Constr = "server=" + localIp + @"\" + localCase + "," + localPort + ";" + "database=master;" + "uid=" + localUserName + ";pwd=" + "'" + localPassword + "'";


                string sql = "select * from sysdatabases where name='RepastErp'";

                DataSet ds = DBSql.SCreateDataSet(sql);

                if (ds != null)
                {
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        return 1;
                    }
                    else
                    {
                        return 0;
                    }
                }
                else
                {
                    return 2;
                    
                }
            }
            catch (Exception ee)
            {
                throw ee;
            }
           
        }

        /// <summary>
        /// 代码创建用户
        /// </summary>
        /// <param name="cUserName"></param>
        /// <param name="Password"></param>
        /// <param name="DB"></param>
        /// <returns></returns>
        public bool AddUser(string cUserName, string Password, string DB)
        {
            int i;
            System.Data.SqlClient.SqlConnection oCon = new System.Data.SqlClient.SqlConnection("data source=.;initial catalog=master;password=;persist security info=True;user id=sa");
            try
            {
                oCon.Open();
            }
            catch
            {
                return false;
            }
            System.Data.SqlClient.SqlCommand oAddUser = new System.Data.SqlClient.SqlCommand();
            oAddUser.CommandType = System.Data.CommandType.Text;
            oAddUser.Connection = oCon;
            oAddUser.CommandText = "exec sp_addlogin '" + cUserName + "','" + Password + "','" + DB + "'";
            try
            {
               i  = oAddUser.ExecuteNonQuery();
            }
            catch
            {
                return false;
            }
            if (i == 0)
            {
                return false;
            }
            else
            {
                return true;
            }

        }

        /// <summary>
        /// 附加数据库
        /// </summary>
        /// <param name="DBName">数据库名字</param>
        /// <param name="DBPath">数据库路径</param>
        /// <param name="logFileName">日志文件路径</param>
        /// <returns>true表示附加成功</returns>
        public bool AddDataBase(string DBName, string DBPath, string logFileName)
        {
            int i;
            System.Data.SqlClient.SqlConnection oCon = new System.Data.SqlClient.SqlConnection("data source=.;initial catalog=master;password=" + localPassword + ";persist security info=True;user id="+localUserName);
            try
            {
                oCon.Open();
            }
            catch
            {
                return false;
            }
            System.Data.SqlClient.SqlCommand oAddUser = new System.Data.SqlClient.SqlCommand();
            oAddUser.CommandType = System.Data.CommandType.Text;
            oAddUser.Connection = oCon;

            //'数据库名','数据库全路径','数据库日志全路径'
            oAddUser.CommandText = "Sp_attach_db '" + DBName + "','" + DBPath + "','" + logFileName + "'";
            try
            {
                i = oAddUser.ExecuteNonQuery();
            }
            catch
            {
                return false;
            }
            if (i == 0)
            {
                return false;
            }
            else
            {
                return true;
            }
        }


        /// <summary>
        ///分离本地数据库
        /// </summary>
        /// <param name="DBName">数据库名字</param>
        /// <param name="DBPath">数据库路径</param>
        /// <param name="logFileName">日志文件路径</param>
        /// <returns>true表示附加成功</returns>
        public bool LeaveDataBase(string DBName)
        {
            readIni = new ReadIni();
            localIp = readIni.ReadString("RepastErp", "txtDatabaseIP");
            localCase = readIni.ReadString("RepastErp", "txtExampleName2");
            localPort = readIni.ReadString("RepastErp", "txtPort2");
            localDBName = readIni.ReadString("RepastErp", "txtDatabaseName");
            localUserName = readIni.ReadString("RepastErp", "txtDatabaseUser");
            localPassword = readIni.ReadString("RepastErp", "txtDatabasePassword");
            if (localIp == "")
            {
                string strHostName = Dns.GetHostName(); //得到本机的主机名 
                localIp = Dns.GetHostEntry(strHostName).AddressList[0].ToString(); //取得本机IP    
                readIni.WriteString("RepastErp", "txtDatabaseIP", localIp);
            } if (localPort == "")
            {
                localPort = "1433";
                readIni.WriteString("RepastErp", "txtPort2", "1433");
            }
            if (localDBName == "")
            {
                localDBName = "RepastErp";
                readIni.WriteString("RepastErp", "txtDatabaseName", "RepastErp");
            }
            if (localUserName == "")
            {
                localUserName = "sa";
                readIni.WriteString("RepastErp", "txtDatabaseUser", "sa");
            }
            if (localPassword == "")
            {
                localPassword = "sa";
                readIni.WriteString("RepastErp", "txtDatabasePassword", "sa");
            }
            int i;
            System.Data.SqlClient.SqlConnection oCon = new System.Data.SqlClient.SqlConnection("data source=.;initial catalog=master;password=" + localPassword + ";persist security info=True;user id=" + localUserName);
            try
            {
                oCon.Open();
            }
            catch
            {
                return false;
            }
            System.Data.SqlClient.SqlCommand oAddUser = new System.Data.SqlClient.SqlCommand();
            oAddUser.CommandType = System.Data.CommandType.Text;
            oAddUser.Connection = oCon;

            //'数据库名','数据库全路径','数据库日志全路径'
            oAddUser.CommandText = "sp_detach_db '" + DBName;
            try
            {
                i = oAddUser.ExecuteNonQuery();
            }
            catch
            {
                return false;
            }
            if (i == 0)
            {
                return false;
            }
            else
            {
                return true;
            }
        } 



        /// <summary>
        /// 创建数据库
        /// </summary>
        /// <returns>成功创建返回1,失败返回0，原来已经有数据库了返回2</returns>
        public bool CreateDatabase()
        {
            if (MainForm.progressForm == null)
            {
                MainForm.progressForm = new ProgressForm();
            }
            MainForm.progressForm.ChangeState("创建数据库");
            MainForm.progressForm.Show();
            bool b=false ;
            if (File.Exists("C:\\RepastErp_data.mdf"))
            {
                File.Delete("C:\\RepastErp_data.mdf");
            }
            if (File.Exists("C:\\RepastErp_log.ldf"))
            {
                File.Delete("C:\\RepastErp_log.ldf");
            }
                        string create_db = "if exists(select * from sysdatabases where name='RepastErp')"
+"drop database RepastErp create database RepastErp  "
+"on  primary " 
+"("
  +" name='RepastErp_data', "
    +"filename='C:\\RepastErp_data.mdf', "
    +" size=5mb, "
    +" maxsize=100mb, "
   +"filegrowth=15% "
+")"
 +"log on  ("
   +" name='RepastErp_log',"
    +"filename='C:\\RepastErp_log.ldf',"
  +"  size=2mb,"
   +" filegrowth=1mb"
 +")";
b=DBSql.STRunSQL (create_db );
if (MainForm.progressForm != null)
{
    MainForm.progressForm.Add(2);
}
            string combination = "CREATE TABLE [dbo].[COMBINATION] ("
    + "[COMB_ID] [nvarchar] (20) COLLATE Chinese_PRC_CI_AS NOT NULL ,"
    + "[COMB_SNO] [smallint] NOT NULL ,"
    + "[PROD_ID] [nvarchar] (20) COLLATE Chinese_PRC_CI_AS NOT NULL ,"
    +"[QUANTITY] [numeric](18, 4) NOT NULL ,"
    +"[PRICE] [money] NOT NULL ,"
    +"[ISDEFAULT] [bit] NOT NULL ,"
    +"[TRANSFER_STATUS] [char] (1) COLLATE Chinese_PRC_CI_AS NOT NULL ,"
    +"[LAST_UPDATE] [datetime] NOT NULL ,"
    +"[IS_PROMOTION] [bit] NOT NULL ,"
    +"[ENABLE] [bit] NOT NULL ,"
    +"[COLOR] [nvarchar] (50) COLLATE Chinese_PRC_CI_AS NULL ,"
    +"[FONT_NAME] [nvarchar] (50) COLLATE Chinese_PRC_CI_AS NULL ,"
    +"[FONT_SIZE] [nvarchar] (50) COLLATE Chinese_PRC_CI_AS NULL ,"
    +"[FONT_COLOR] [nvarchar] (50) COLLATE Chinese_PRC_CI_AS NULL ,"
    +"[OWNER_SHOP] [varchar] (12) COLLATE Chinese_PRC_CI_AS NULL "
+") ON [PRIMARY]"

+ "            ALTER TABLE [dbo].[COMBINATION] WITH NOCHECK ADD "
+ "	CONSTRAINT [PK_COMBINATION] PRIMARY KEY  CLUSTERED "
+"	("
+ "		[COMB_ID],"
+ "		[COMB_SNO],"
+ "		[PROD_ID]"
+"	)  ON [PRIMARY] ";



          //连接刚创建的本地数据库
          Info.Constr = "server=" + localIp + @"\" + localCase + "," + localPort + ";" + "database=RepastErp;" + "uid=" + localUserName + ";pwd=" + "'" + localPassword + "'";

          b = DBSql.STRunSQL(combination);

          if (MainForm.progressForm != null)
          {
              MainForm.progressForm.Add(1);
          }




            string department = "CREATE TABLE [dbo].[DEPARTMENT] (         "
    + "[DEP_ID] [nvarchar] (10) COLLATE Chinese_PRC_CI_AS NOT NULL ,        "
    +"[DEP_NAME] [nvarchar] (20) COLLATE Chinese_PRC_CI_AS NOT NULL ,      "
    +"[ENABLE] [bit] NOT NULL ,                                            "
    +"[DEP_MEMO] [nvarchar] (200) COLLATE Chinese_PRC_CI_AS NULL ,         "
    +"[TRANSFER_STATUS] [nchar] (2) COLLATE Chinese_PRC_CI_AS NOT NULL ,   "
    +"[REDEEM_ENABLE] [bit] NOT NULL ,                                     "
    +"[GROUP_ID] [nvarchar] (20) COLLATE Chinese_PRC_CI_AS NULL ,          "
    +"[UPPER_DEP] [nvarchar] (10) COLLATE Chinese_PRC_CI_AS NULL ,         "
    +"[IS_LEAF] [bit] NOT NULL ,                                           "
    +"[LAST_UPDATE] [datetime] NOT NULL ,                                  "
    +"[BTN_COLOR] [nvarchar] (20) COLLATE Chinese_PRC_CI_AS NOT NULL ,     "
    +"[FONT_COLOR] [nvarchar] (20) COLLATE Chinese_PRC_CI_AS NOT NULL ,    "
    +"[FONT] [nvarchar] (20) COLLATE Chinese_PRC_CI_AS NOT NULL ,          "
    +"[FONT_SIZE] [int] NOT NULL ,                                         "
    +"[Select_Color] [nvarchar] (20) COLLATE Chinese_PRC_CI_AS NOT NULL ,  "
    +"[Btn_Shape] [nvarchar] (20) COLLATE Chinese_PRC_CI_AS NOT NULL ,     "
    +"[S_NO] [int] NULL                                                    "
+") ON [PRIMARY] "
            + "            ALTER TABLE [dbo].[DEPARTMENT] WITH NOCHECK ADD "
+ "	CONSTRAINT [PK_DEPARTMENT] PRIMARY KEY  CLUSTERED "
+ "	("
+ "		[DEP_ID]"
+ "	)  ON [PRIMARY] ";

            b = DBSql.STRunSQL(department);
            if (MainForm.progressForm != null)
            {
                MainForm.progressForm.Add(1);
            }
            string emp_level = "CREATE TABLE [dbo].[emp_level] (    "
+ "	[Emp_id] [nchar] (1) COLLATE Chinese_PRC_CI_AS NOT NULL,       "
+"	[Emp_lev_name] [nvarchar] (100) COLLATE Chinese_PRC_CI_AS NULL ,"
+"	[DISC_LIMIT] [int] NOT NULL ,                                   "
+"	[LAST_UPDATE] [datetime] NOT NULL                               "
+") ON [PRIMARY]"

+" ALTER TABLE [dbo].[emp_level] WITH NOCHECK ADD "
+ "	CONSTRAINT [PK_emp_level] PRIMARY KEY  CLUSTERED "
+ "	("
+ "		[Emp_id]"
+ "	)  ON [PRIMARY] ";


            b = DBSql.STRunSQL(emp_level);
            if (MainForm.progressForm != null)
            {
                MainForm.progressForm.Add(1);
            }

            string employee = "CREATE TABLE [dbo].[EMPLOYEE] (          "
+ "	[EMP_ID] [nvarchar] (10) COLLATE Chinese_PRC_CI_AS NOT NULL,       "
+"	[SHOP_ID] [nvarchar] (14) COLLATE Chinese_PRC_CI_AS NULL ,          "
+"	[PASSWORD] [nvarchar] (50) COLLATE Chinese_PRC_CI_AS NULL ,         "
+"	[EMP_NAME] [nvarchar] (20) COLLATE Chinese_PRC_CI_AS NOT NULL ,     "
+"	[EMP_MEMO] [nvarchar] (100) COLLATE Chinese_PRC_CI_AS NULL ,        "
+"	[EMP_LEVEL] [nvarchar] (50) COLLATE Chinese_PRC_CI_AS NULL ,        "
+"	[EMP_SHIFT] [nvarchar] (50) COLLATE Chinese_PRC_CI_AS NULL ,        "
+"	[IDCARD_NO] [nvarchar] (20) COLLATE Chinese_PRC_CI_AS NULL ,        "
+"	[TELEPHONE] [nvarchar] (20) COLLATE Chinese_PRC_CI_AS NULL ,        "
+"	[ADDRESS] [nvarchar] (50) COLLATE Chinese_PRC_CI_AS NULL ,          "
+"	[EMP_ZIP] [nvarchar] (10) COLLATE Chinese_PRC_CI_AS NULL ,          "
+"	[DISCOUNT] [smallint] NOT NULL ,                                    "
+"	[EMP_TREAT] [money] NULL ,                                          "
+"	[TRANSFER_STATUS] [nchar] (2) COLLATE Chinese_PRC_CI_AS NOT NULL ,  "
+"	[EMP_SEX] [nchar] (1) COLLATE Chinese_PRC_CI_AS NOT NULL ,          "
+"	[ENABLE] [bit] NOT NULL ,                                           "
+"	[CARD_NO] [nvarchar] (50) COLLATE Chinese_PRC_CI_AS NULL ,          "
+"	[TREAT] [bit] NOT NULL ,                                            "
+"	[LAST_UPDATE] [datetime] NOT NULL ,                                 "
+"	[EMP_EDU] [nvarchar] (50) COLLATE Chinese_PRC_CI_AS NULL ,          "
+"	[TEL2] [nvarchar] (20) COLLATE Chinese_PRC_CI_AS NULL ,             "
+"	[BEGIN_DATE] [datetime] NULL ,                                      "
+"	[END_DATE] [datetime] NULL ,                                        "
+"	[EMP_DUTY] [nvarchar] (50) COLLATE Chinese_PRC_CI_AS NULL ,         "
+"	[BORN_DATE] [datetime] NULL ,                                       "
+"	[EXTENDS1] [nvarchar] (10) COLLATE Chinese_PRC_CI_AS NULL ,         "
+"	[EXTENDS2] [nvarchar] (10) COLLATE Chinese_PRC_CI_AS NULL ,         "
+"	[EXTENDS3] [nvarchar] (10) COLLATE Chinese_PRC_CI_AS NULL ,         "
+"	[EXTENDS4] [nvarchar] (10) COLLATE Chinese_PRC_CI_AS NULL           "
+") ON [PRIMARY] "
            + " ALTER TABLE [dbo].[EMPLOYEE] WITH NOCHECK ADD "
            + "	CONSTRAINT [PK_EMPLOYEE] PRIMARY KEY  CLUSTERED "
            + "	("
            + "		[EMP_ID]"
            + "	)  ON [PRIMARY] ";

            b = DBSql.STRunSQL(employee);
            if (MainForm.progressForm != null)
            {
                MainForm.progressForm.Add(1);
            }

            string message00 = "CREATE TABLE [dbo].[MESSAGE00] (       "
+ "	[SHOP_ID] [nvarchar] (14) COLLATE Chinese_PRC_CI_AS NOT NULL,     "
+ "	[MSG_ID] [nvarchar] (30) COLLATE Chinese_PRC_CI_AS NOT NULL,      "
+"	[STATUS] [char] (1) COLLATE Chinese_PRC_CI_AS NOT NULL ,           "
+"	[INPUT_DATE] [datetime] NOT NULL ,                                 "
+"	[USER_ID] [varchar] (10) COLLATE Chinese_PRC_CI_AS NULL ,          "
+"	[APP_DATE] [datetime] NULL ,                                       "
+"	[MSG_TITLE] [varchar] (100) COLLATE Chinese_PRC_CI_AS NULL ,       "
+"	[MSG_MEMO] [varchar] (100) COLLATE Chinese_PRC_CI_AS NULL ,        "
+"	[Transfer_status] [nchar] (2) COLLATE Chinese_PRC_CI_AS NOT NULL , "
+"	[MSG_SHOP] [varchar] (14) COLLATE Chinese_PRC_CI_AS NULL ,         "
+"	[MSG_CONTENT] [varchar] (500) COLLATE Chinese_PRC_CI_AS NULL ,     "
+"	[EFFECT_DATE] [datetime] NULL ,                                    "
+"	[LAST_UPDATE] [datetime] NULL ,                                    "
+"	[EXTENDS1] [nvarchar] (10) COLLATE Chinese_PRC_CI_AS NULL ,        "
+"	[EXTENDS2] [nvarchar] (10) COLLATE Chinese_PRC_CI_AS NULL ,        "
+"	[EXTENDS3] [nvarchar] (10) COLLATE Chinese_PRC_CI_AS NULL          "
+") ON [PRIMARY]"
            + " ALTER TABLE [dbo].[MESSAGE00] WITH NOCHECK ADD "
            + "	CONSTRAINT [PK_MESSAGE00] PRIMARY KEY  CLUSTERED "
            + "	("
            + "		[SHOP_ID],"
            + "		[MSG_ID]"
            + "	)  ON [PRIMARY] ";
            b = DBSql.STRunSQL(message00);
            if (MainForm.progressForm != null)
            {
                MainForm.progressForm.Add(1);
            }
            string money_out = "CREATE TABLE [dbo].[MONEY_OUT] (        "
+ "	[SHOP_ID] [nvarchar] (14) COLLATE Chinese_PRC_CI_AS NOT NULL,      "
+ "	[MONEY_OUT_ID] [nvarchar] (32) COLLATE Chinese_PRC_CI_AS NOT NULL, "
+ "	[MONEY_OUT_SNO] [smallint] NOT NULL ,                               "
+"	[POS_ID] [nvarchar] (2) COLLATE Chinese_PRC_CI_AS NOT NULL ,        "
+"	[MONEY_OUT_DATE] [datetime] NOT NULL ,                              "
+"	[USER_ID] [varchar] (10) COLLATE Chinese_PRC_CI_AS NOT NULL ,       "
+"	[AMOUNT] [money] NOT NULL ,                                         "
+"	[TRANSFER_STATUS] [varchar] (1) COLLATE Chinese_PRC_CI_AS NOT NULL  "
+") ON [PRIMARY]"
            + " ALTER TABLE [dbo].[MONEY_OUT] WITH NOCHECK ADD "
            + "	CONSTRAINT [PK_MONEY_OUT] PRIMARY KEY  CLUSTERED "
            + "	("
            + "		[SHOP_ID],"
            + "		[MONEY_OUT_ID],"
            + "		[MONEY_OUT_SNO]"
            + "	)  ON [PRIMARY] ";

            b = DBSql.STRunSQL(money_out);
            if (MainForm.progressForm != null)
            {
                MainForm.progressForm.Add(1);
            }

            string payment = "CREATE TABLE [dbo].[PAYMENT] (            "
+ "	[PAY_ID] [nvarchar] (10) COLLATE Chinese_PRC_CI_AS NOT NULL,       "
+"	[PAY_NAME] [nvarchar] (20) COLLATE Chinese_PRC_CI_AS NULL ,         "
+"	[PAY_MEMO] [nvarchar] (100) COLLATE Chinese_PRC_CI_AS NULL ,        "
+"	[PAY_TYPE] [nchar] (1) COLLATE Chinese_PRC_CI_AS NOT NULL ,         "
+"	[Data_Accur] [numeric](18, 0) NOT NULL ,                            "
+"	[Face_Value] [money] NOT NULL ,                                     "
+"	[VISABLE] [bit] NOT NULL ,                                          "
+"	[DISP_NAME] [nvarchar] (20) COLLATE Chinese_PRC_CI_AS NULL ,        "
+"	[COLOR] [nvarchar] (20) COLLATE Chinese_PRC_CI_AS NULL ,            "
+"	[FONT_COLOR] [nvarchar] (20) COLLATE Chinese_PRC_CI_AS NULL ,       "
+"	[FONT_SIZE] [int] NULL ,                                            "
+"	[FONT_NAME] [nvarchar] (20) COLLATE Chinese_PRC_CI_AS NULL ,        "
+"	[CHANGE] [bit] NULL ,                                               "
+"	[TRANSFER_STATUS] [nchar] (1) COLLATE Chinese_PRC_CI_AS NOT NULL ,  "
+"	[PAY_KIND] [nchar] (1) COLLATE Chinese_PRC_CI_AS NULL ,             "
+"	[LAST_UPDATE] [datetime] NOT NULL ,                                 "
+"	[EXTENDS1] [nvarchar] (10) COLLATE Chinese_PRC_CI_AS NULL ,         "
+"	[EXTENDS2] [nvarchar] (10) COLLATE Chinese_PRC_CI_AS NULL ,         "
+"	[EXTENDS3] [nvarchar] (10) COLLATE Chinese_PRC_CI_AS NULL ,         "
+"	[EXTENDS4] [nvarchar] (10) COLLATE Chinese_PRC_CI_AS NULL ,         "
+"	[EXTENDS5] [nvarchar] (10) COLLATE Chinese_PRC_CI_AS NULL           "
+") ON [PRIMARY]"
+ " ALTER TABLE [dbo].[PAYMENT] WITH NOCHECK ADD "
+ "	CONSTRAINT [PK_PAYMENT] PRIMARY KEY  CLUSTERED "
+ "	("
+ "		[PAY_ID]"
+ "	)  ON [PRIMARY] ";
            b = DBSql.STRunSQL(payment);
            if (MainForm.progressForm != null)
            {
                MainForm.progressForm.Add(1);
            }
            string pos_funcset = "CREATE TABLE [dbo].[POS_FUNCSET] (      "
+ "	[FUNC_ID] [int] NOT NULL,                                            "
+ "	[FUNC_NAME] [nvarchar] (50) COLLATE Chinese_PRC_CI_AS NULL ,          "
+ "	[POSITION_ID] [int] NULL ,                                            "
+ "	[DISP_NAME] [nvarchar] (50) COLLATE Chinese_PRC_CI_AS NULL ,          "
+ "	[VISIBLE] [bit] NOT NULL ,                                            "
+ "	[COLOR] [nvarchar] (50) COLLATE Chinese_PRC_CI_AS NULL ,              "
+ "	[FONT_COLOR] [nvarchar] (50) COLLATE Chinese_PRC_CI_AS NULL ,         "
+ "	[FONT_SIZE] [int] NULL ,                                              "
+ "	[FONT_NAME] [nvarchar] (50) COLLATE Chinese_PRC_CI_AS NULL ,          "
+ "	[EMP_LEVEL] [nchar] (1) COLLATE Chinese_PRC_CI_AS NULL ,              "
+ "	[DEFAULT_POSITION] [int] NOT NULL ,                                   "
+ "	[GROUP_ID] [int] NULL ,                                               "
+ "	[HOTKEY] [int] NULL ,                                                 "
+ "	[MEMO] [nvarchar] (100) COLLATE Chinese_PRC_CI_AS NULL ,              "
+ "	[DEFAULT_COLOR] [nvarchar] (50) COLLATE Chinese_PRC_CI_AS NULL ,      "
+ "	[DEFAULT_FONT_COLOR] [nvarchar] (50) COLLATE Chinese_PRC_CI_AS NULL , "
+ "	[DEFAULT_FONT_SIZE] [int] NULL ,                                      "
+ "	[DEFAULT_FONT_NAME] [nvarchar] (50) COLLATE Chinese_PRC_CI_AS NULL ,  "
+ "	[DEFAULT_EMP_LEVEL] [nchar] (1) COLLATE Chinese_PRC_CI_AS NULL ,      "
+ "	[TRANSFER_VISIBLE] [nchar] (2) COLLATE Chinese_PRC_CI_AS NULL         "
+ ") ON [PRIMARY]"
+ " ALTER TABLE [dbo].[POS_FUNCSET] WITH NOCHECK ADD "
+ "	CONSTRAINT [PK_POS_FUNCSET] PRIMARY KEY  CLUSTERED "
+ "	("
+ "		[FUNC_ID]"
+ "	)  ON [PRIMARY] ";
            b = DBSql.STRunSQL(pos_funcset);
            if (MainForm.progressForm != null)
            {
                MainForm.progressForm.Add(1);
            }
            string pos_rounds = "CREATE TABLE [dbo].[POS_ROUNDS] (      "
+ "	[SHOP_ID] [nvarchar] (14) COLLATE Chinese_PRC_CI_AS NOT NULL ,      "
+ "	[POS_ID] [nvarchar] (2) COLLATE Chinese_PRC_CI_AS NOT NULL ,        "
+ "	[LOGIN_DATE] [datetime] NOT NULL ,                                  "
+"	[CASHIER_SUM] [money] NULL ,                                        "
+"	[EXIT_DATE] [datetime] NULL ,                                       "
+"	[USER_ID] [nvarchar] (10) COLLATE Chinese_PRC_CI_AS NULL ,          "
+"	[TRANSFER_STATUS] [nchar] (1) COLLATE Chinese_PRC_CI_AS NOT NULL ,  "
+"	[SHIFT_NUM] [nvarchar] (10) COLLATE Chinese_PRC_CI_AS NULL ,        "
+"	[LAST_UPDATE] [datetime] NULL ,                                     "
+"	[MONEY_OUT_ID] [nvarchar] (10) COLLATE Chinese_PRC_CI_AS NULL       "
+") ON [PRIMARY]"
            + " ALTER TABLE [dbo].[POS_ROUNDS] WITH NOCHECK ADD "
            + "	CONSTRAINT [PK_POS_ROUNDS] PRIMARY KEY  CLUSTERED "
            + "	("
            + "		[SHOP_ID],"
            + "		[POS_ID],"
            + "		[LOGIN_DATE]"
            + "	)  ON [PRIMARY] ";
            b = DBSql.STRunSQL(pos_rounds);
            if (MainForm.progressForm != null)
            {
                MainForm.progressForm.Add(1);
            }
            string product00 = "CREATE TABLE [dbo].[PRODUCT00] (            "
+ "	[PROD_ID] [nvarchar] (20) COLLATE Chinese_PRC_CI_AS NOT NULL ,          "
+"	[PROD_NAME] [nvarchar] (40) COLLATE Chinese_PRC_CI_AS NOT NULL ,        "
+"	[PROD_ENAME] [nvarchar] (40) COLLATE Chinese_PRC_CI_AS NULL ,           "
+"	[PROD_MEMO] [nvarchar] (100) COLLATE Chinese_PRC_CI_AS NULL ,           "
+"	[PROD_KIND] [nchar] (1) COLLATE Chinese_PRC_CI_AS NULL ,                "
+"	[SUP_KIND] [nchar] (1) COLLATE Chinese_PRC_CI_AS NULL ,                 "
+"	[INV_TYPE] [nchar] (1) COLLATE Chinese_PRC_CI_AS NULL ,                 "
+"	[SAFE_ST] [numeric](18, 4) NULL ,                                       "
+"	[WARNING_ST] [numeric](18, 4) NULL ,                                    "
+"	[IMAGE_PATH] [nvarchar] (40) COLLATE Chinese_PRC_CI_AS NULL ,           "
+"	[IN_DATE] [datetime] NULL ,                                             "
+"	[COST] [numeric](19, 6) NOT NULL ,                                      "
+"	[MIN_PRICE] [numeric](19, 6) NOT NULL ,                                 "
+"	[RETURN_PRICE] [numeric](19, 6) NOT NULL ,                              "
+"	[PRICE1] [numeric](19, 6) NOT NULL ,                                    "
+"	[PRICE2] [numeric](19, 6) NOT NULL ,                                    "
+"	[EMP_PRICE] [numeric](19, 6) NOT NULL ,                                 "
+"	[ENABLE] [bit] NOT NULL ,                                               "
+"	[COMBINED] [bit] NOT NULL ,                                             "
+"	[DIS_START] [nvarchar] (5) COLLATE Chinese_PRC_CI_AS NULL ,             "
+"	[DIS_END] [nvarchar] (5) COLLATE Chinese_PRC_CI_AS NULL ,               "
+"	[DIS_PRICE] [money] NULL ,                                              "
+"	[DIS_NUMBER] [int] NULL ,                                               "
+"	[TRANSFER_STATUS] [nchar] (1) COLLATE Chinese_PRC_CI_AS NOT NULL ,      "
+"	[Spec] [nvarchar] (100) COLLATE Chinese_PRC_CI_AS NULL ,                "
+"	[BARCODE] [nvarchar] (20) COLLATE Chinese_PRC_CI_AS NULL ,              "
+"	[BOM_TYPE] [smallint] NULL ,                                            "
+"	[BOM_LEVEL] [smallint] NOT NULL ,                                       "
+"	[OUTINCOME] [bit] NOT NULL ,                                            "
+"	[COMMISION_PRICE] [money] NOT NULL ,                                    "
+"	[TAX] [numeric](18, 4) NOT NULL ,                                       "
+"	[TAX_SIGN] [nvarchar] (10) COLLATE Chinese_PRC_CI_AS NULL ,             "
+"	[MIN_UNIT] [nvarchar] (10) COLLATE Chinese_PRC_CI_AS NULL ,             "
+"	[MIN_CONV] [numeric](18, 4) NULL ,                                      "
+"	[UNIT] [nvarchar] (10) COLLATE Chinese_PRC_CI_AS NULL ,                 "
+"	[SCAT_UNIT] [nvarchar] (10) COLLATE Chinese_PRC_CI_AS NULL ,            "
+"	[SCAT_CONV] [numeric](18, 4) NULL ,                                     "
+"	[INSOUR_UNIT] [nvarchar] (10) COLLATE Chinese_PRC_CI_AS NULL ,          "
+"	[INSOUR_CONV] [numeric](18, 4) NULL ,                                   "
+"	[EPIBO_UNIT] [nvarchar] (10) COLLATE Chinese_PRC_CI_AS NULL ,           "
+"	[EPIBO_CONV] [numeric](18, 4) NULL ,                                    "
+"	[LAST_UPDATE] [datetime] NULL ,                                         "
+"	[COMBO_TYPE] [nchar] (1) COLLATE Chinese_PRC_CI_AS NULL ,               "
+"	[DOWN_DATE] [datetime] NULL ,                                           "
+"	[MEAL_TICKET] [int] NOT NULL ,                                          "
+"	[POS_DISP] [nvarchar] (40) COLLATE Chinese_PRC_CI_AS NULL ,             "
+"	[IS_SALEMONEY] [bit] NULL ,                                             "
+"	[IS_BUSMONEY] [bit] NULL ,                                              "
+"	[SINGEL_COM] [bit] NULL ,                                               "
+"	[IS_CALCUCOST] [bit] NULL ,                                             "
+"	[STOCK_QTY] [numeric](10, 0) NULL ,                                     "
+"	[DINING_QTY] [numeric](10, 0) NULL ,                                    "
+"	[S_NO] [int] NULL ,                                                     "
+"	[Btn_Color] [nvarchar] (20) COLLATE Chinese_PRC_CI_AS NOT NULL ,        "
+"	[FONT_COLOR] [nvarchar] (20) COLLATE Chinese_PRC_CI_AS NOT NULL ,       "
+"	[FONT] [nvarchar] (20) COLLATE Chinese_PRC_CI_AS NOT NULL ,             "
+"	[FONT_SIZE] [int] NOT NULL ,                                            "
+"	[Select_Color] [nvarchar] (20) COLLATE Chinese_PRC_CI_AS NOT NULL ,     "
+"	[DEFAULT_UNIT] [nvarchar] (10) COLLATE Chinese_PRC_CI_AS NULL ,         "
+"	[DEFAULT_CONVERT] [numeric](18, 0) NULL ,                               "
+"	[BOM_UNIT] [nvarchar] (10) COLLATE Chinese_PRC_CI_AS NULL ,             "
+"	[BOM_CONV] [numeric](18, 6) NULL ,                                      "
+"	[INV_DATE] [bit] NULL ,                                                 "
+"	[INV_WEEK] [bit] NULL ,                                                 "
+"	[INV_MONTH] [bit] NULL ,                                                "
+"	[DEP_ID] [nvarchar] (10) COLLATE Chinese_PRC_CI_AS NULL ,               "
+"	[SALE_ONLY_IN_COMB] [bit] NOT NULL ,                                    "
+"	[COST_CONV] [numeric](18, 4) NOT NULL ,                                 "
+"	[COST_UNIT] [nvarchar] (10) COLLATE Chinese_PRC_CI_AS NULL ,            "
+"	[ISPROM] [bit] NULL ,                                                   "
+"	[OWNER_SHOP] [varchar] (12) COLLATE Chinese_PRC_CI_AS NULL,             "
+ "	[CARGO_KIND] [varchar] (10) COLLATE Chinese_PRC_CI_AS NULL,             "
+ "	[SHELF_LIFE] [int] NULL                                                 "
+") ON [PRIMARY]"
            + " ALTER TABLE [dbo].[PRODUCT00] WITH NOCHECK ADD "
            + "	CONSTRAINT [PK_PRODUCT00] PRIMARY KEY  CLUSTERED "
            + "	("
            + "		[PROD_ID]"
            + "	)  ON [PRIMARY] ";
            b = DBSql.STRunSQL(product00);
            if (MainForm.progressForm != null)
            {
                MainForm.progressForm.Add(1);
            }
            string prom_price00 = "CREATE TABLE [dbo].[PROM_PRICE00] (  "
+ "	[SHOP_ID] [nvarchar] (14) COLLATE Chinese_PRC_CI_AS NOT NULL ,      "
+ "	[PROM_ID] [nvarchar] (24) COLLATE Chinese_PRC_CI_AS NOT NULL ,      "
+"	[NAME] [nvarchar] (100) COLLATE Chinese_PRC_CI_AS NULL ,            "
+"	[BEGIN_DATE] [datetime] NULL ,                                      "
+"	[END_DATE] [datetime] NULL ,                                        "
+"	[ENABLE] [bit] NOT NULL ,                                           "
+"	[SALE_METHOD] [int] NOT NULL ,                                      "
+"	[COMPATIBLE] [bigint] NOT NULL ,                                    "
+"	[DISC_MODE] [bit] NOT NULL ,                                        "
+"	[BEGIN_TIME] [nvarchar] (5) COLLATE Chinese_PRC_CI_AS NOT NULL ,    "
+"	[END_TIME] [nvarchar] (5) COLLATE Chinese_PRC_CI_AS NOT NULL ,      "
+"	[B_WEEKDAY] [smallint] NOT NULL ,                                   "
+"	[E_WEEKDAY] [smallint] NULL ,                                       "
+"	[TRANSFER_STATUS] [nchar] (1) COLLATE Chinese_PRC_CI_AS NOT NULL ,  "
+"	[LAST_UPDATE] [datetime] NOT NULL ,                                 "
+"	[STATUS] [nchar] (1) COLLATE Chinese_PRC_CI_AS NOT NULL ,           "
+"	[PROM_MEMO] [nvarchar] (200) COLLATE Chinese_PRC_CI_AS NULL ,       "
+"	[MON] [bit] NULL ,                                                  "
+"	[TUE] [bit] NULL ,                                                  "
+"	[WED] [bit] NULL ,                                                  "
+"	[THR] [bit] NULL ,                                                  "
+"	[FRI] [bit] NULL ,                                                  "
+"	[SAT] [bit] NULL ,                                                  "
+"	[SUN] [bit] NULL                                                    "
+") ON [PRIMARY]"
            + " ALTER TABLE [dbo].[PROM_PRICE00] WITH NOCHECK ADD "
            + "	CONSTRAINT [PK_PROM_PRICE00] PRIMARY KEY  CLUSTERED "
            + "	("
            + "		[SHOP_ID],"
             + "	[PROM_ID]"
            + "	)  ON [PRIMARY] ";
            b = DBSql.STRunSQL(prom_price00);
            if (MainForm.progressForm != null)
            {
                MainForm.progressForm.Add(1);
            }
            string prom_price02 = "CREATE TABLE [dbo].[PROM_PRICE02] (  "
+ "	[SHOP_ID] [nvarchar] (14) COLLATE Chinese_PRC_CI_AS NOT NULL ,      "
+ "	[PROM_ID] [nvarchar] (24) COLLATE Chinese_PRC_CI_AS NOT NULL ,      "
+ "	[PROM_SNO] [int] NOT NULL ,                                         "
+"	[DEP_ID] [nvarchar] (10) COLLATE Chinese_PRC_CI_AS NULL ,           "
+"	[PROD_ID] [nvarchar] (20) COLLATE Chinese_PRC_CI_AS NULL ,          "
+"	[DISCOUNT] [int] NOT NULL ,                                         "
+"	[SPE_PRICE] [money] NOT NULL ,                                      "
+"	[USE_DEP] [bit] NOT NULL ,                                          "
+"	[USE_DISC] [bit] NOT NULL ,                                         "
+"	[BEGIN_AMOUNT] [money] NOT NULL ,                                   "
+"	[END_AMOUNT] [money] NOT NULL ,                                     "
+"	[TRANSFER_STATUS] [bit] NULL ,                                      "
+"	[LAST_UPDATE] [datetime] NOT NULL                                   "
+") ON [PRIMARY]"
            + " ALTER TABLE [dbo].[PROM_PRICE02] WITH NOCHECK ADD "
            + "	CONSTRAINT [PK_PROM_PRICE02] PRIMARY KEY  CLUSTERED "
            + "	("
            + "		[SHOP_ID],"
            + "		[PROM_ID],"
            + "		[PROM_SNO]"
            + "	)  ON [PRIMARY] ";
            b = DBSql.STRunSQL(prom_price02);
            if (MainForm.progressForm != null)
            {
                MainForm.progressForm.Add(1);
            }
            string sale00 = "CREATE TABLE [dbo].[SALE00] (        "
+ "	[SHOP_ID] [nvarchar] (14) COLLATE Chinese_PRC_CI_AS NOT NULL ,"
+ "	[SALE_ID] [nvarchar] (32) COLLATE Chinese_PRC_CI_AS NOT NULL ,"
+"	[STATUS_ID] [nchar] (1) COLLATE Chinese_PRC_CI_AS NOT NULL ,  "
+"	[POS_ID] [nvarchar] (2) COLLATE Chinese_PRC_CI_AS NULL ,      "
+"	[SALE_DATE] [datetime] NULL ,                                 "
+"	[SALE_USER] [nvarchar] (10) COLLATE Chinese_PRC_CI_AS NULL ,  "
+"	[VIP_ID] [nvarchar] (20) COLLATE Chinese_PRC_CI_AS NULL ,     "
+"	[TOT_SALES] [money] NULL ,                                    "
+"	[TRANSFER_STATUS] [nchar] (2) COLLATE Chinese_PRC_CI_AS NULL ,"
+"	[LOCKED] [bit] NOT NULL ,                                     "
+"	[TOT_QUAN] [numeric](18, 4) NULL ,                            "
+"	[CHANGE] [money] NOT NULL ,                                   "
+"	[METHOD_ID] [int] NOT NULL ,                                  "
+"	[TOT_TAX] [money] NOT NULL ,                                  "
+"	[MEAL_kind] [smallint] NOT NULL ,                             "
+"	[LAST_UPDATE] [datetime] NOT NULL ,                           "
+"	[BACK_SALES] [money] NULL ,                                   "
+"	[BACK_QUAN] [numeric](18, 0) NULL ,                           "
+"	[TYPE] [nchar] (1) COLLATE Chinese_PRC_CI_AS NULL ,           "
+"	[STORE_ID] [nvarchar] (20) COLLATE Chinese_PRC_CI_AS NULL     "
+") ON [PRIMARY]"
            + " ALTER TABLE [dbo].[SALE00] WITH NOCHECK ADD "
+ "	CONSTRAINT [PK_SALE00] PRIMARY KEY  CLUSTERED "
+ "	("
+ "		[SHOP_ID],"
+ "		[SALE_ID]"
+ "	)  ON [PRIMARY] ";
            b = DBSql.STRunSQL(sale00);
            if (MainForm.progressForm != null)
            {
                MainForm.progressForm.Add(1);
            }
            string sale01 = "CREATE TABLE [dbo].[SALE01] (            "
+ "	[SHOP_ID] [nvarchar] (14) COLLATE Chinese_PRC_CI_AS NOT NULL ,    "
+ "	[SALE_ID] [nvarchar] (32) COLLATE Chinese_PRC_CI_AS NOT NULL ,    "
+ "	[SALE_SNO] [smallint] NOT NULL ,                                  "
+"	[PROD_ID] [nvarchar] (20) COLLATE Chinese_PRC_CI_AS NOT NULL ,    "
+"	[SALE_PRICE] [money] NOT NULL ,                                   "
+"	[QTY] [int] NOT NULL ,                                            "
+"	[ITEM_DISC] [money] NOT NULL ,                                    "
+"	[PROM_ID] [nvarchar] (20) COLLATE Chinese_PRC_CI_AS NULL ,        "
+"	[PROM_SNO] [int] NULL ,                                           "
+"	[PRICE_TYPE] [nchar] (1) COLLATE Chinese_PRC_CI_AS NOT NULL ,     "
+"	[FREE_EMP] [nvarchar] (10) COLLATE Chinese_PRC_CI_AS NULL ,       "
+"	[COMB_SALE_SNO] [smallint] NULL ,                                 "
+"	[COMB_SNO] [smallint] NULL ,                                      "
+"	[COMB_TYPE] [nchar] (1) COLLATE Chinese_PRC_CI_AS NULL ,          "
+"	[ITEM_TAX] [money] NOT NULL ,                                     "
+"	[OUTINCOME] [bit] NOT NULL ,                                      "
+"	[MEAL_TICKET] [int] NOT NULL ,                                    "
+"	[BY_TOKEN] [bit] NOT NULL ,                                       "
+"	[RELATE_PROD] [nvarchar] (20) COLLATE Chinese_PRC_CI_AS NULL ,    "
+"	[SALE_ORGINAL_PRICE] [numeric](18, 4) NOT NULL ,                  "
+"	[ITEM_DISC_TOT] [numeric](18, 4) NOT NULL ,                       "
+"	[ACT_PRICE] [numeric](18, 6) NOT NULL ,                           "
+"	[ISPROM] [bit] NULL ,                                             "
+"	[GROUP_PROD] [nvarchar] (400) COLLATE Chinese_PRC_CI_AS NULL ,    "
+"	[STATUS_ID] [nchar] (1) COLLATE Chinese_PRC_CI_AS NULL ,          "
+"	[TRANSFER_STATUS] [nchar] (2) COLLATE Chinese_PRC_CI_AS NULL      "
+") ON [PRIMARY]"
            + " ALTER TABLE [dbo].[SALE01] WITH NOCHECK ADD "
+ "	CONSTRAINT [PK_SALE01] PRIMARY KEY  CLUSTERED "
+ "	("
+ "		[SHOP_ID],"
+ "		[SALE_ID],"
+ "		[SALE_SNO]"
+ "	)  ON [PRIMARY] ";
            b = DBSql.STRunSQL(sale01);
            if (MainForm.progressForm != null)
            {
                MainForm.progressForm.Add(1);
            }
            string sale02 = "CREATE TABLE [dbo].[SALE02] (              "
+ "	[SHOP_ID] [nvarchar] (14) COLLATE Chinese_PRC_CI_AS NOT NULL ,      "
+ "	[SALE_ID] [nvarchar] (32) COLLATE Chinese_PRC_CI_AS NOT NULL ,      "
+ "	[SALE_SNO] [smallint] NOT NULL ,                                    "
+"	[PAY_ID] [nvarchar] (10) COLLATE Chinese_PRC_CI_AS NOT NULL ,       "
+"	[AMOUNT] [money] NULL ,                                             "
+"	[TRANSFER_STATUS] [nchar] (1) COLLATE Chinese_PRC_CI_AS NOT NULL ,  "
+"	[LAST_UPDATE] [datetime] NOT NULL ,                                 "
+"	[FACE_VALUE] [money] NULL                                           "
+") ON [PRIMARY] "
            + " ALTER TABLE [dbo].[SALE02] WITH NOCHECK ADD "
+ "	CONSTRAINT [PK_SALE02] PRIMARY KEY  CLUSTERED "
+ "	("
+ "		[SHOP_ID],"
+ "		[SALE_ID],"
+ "		[SALE_SNO]"
+ "	)  ON [PRIMARY] ";
            b = DBSql.STRunSQL(sale02);
            if (MainForm.progressForm != null)
            {
                MainForm.progressForm.Add(1);
            }
            string sale03 = "CREATE TABLE [dbo].[SALE03] (          "
+ "	[SHOP_ID] [nvarchar] (14) COLLATE Chinese_PRC_CI_AS NOT NULL ,  "
+ "	[SALE_ID] [nvarchar] (32) COLLATE Chinese_PRC_CI_AS NOT NULL ,  "
+ "	[SEX] [smallint] NOT NULL ,                                     "
+ "	[ITEM_ID] [smallint] NOT NULL ,                                 "
+"	[PERSON_NUM] [int] NOT NULL ,                                   "
+"	[TRANSFER_STATUS] [nchar] (10) COLLATE Chinese_PRC_CI_AS NULL   "
+") ON [PRIMARY]"
            + " ALTER TABLE [dbo].[SALE03] WITH NOCHECK ADD "
+ "	CONSTRAINT [PK_SALE03] PRIMARY KEY  CLUSTERED "
+ "	("
+ "		[SHOP_ID],"
+ "		[SALE_ID],"
+ "		[SEX],"
+ "		[ITEM_ID]"
+ "	)  ON [PRIMARY] ";
            b = DBSql.STRunSQL(sale03);
            if (MainForm.progressForm != null)
            {
                MainForm.progressForm.Add(1);
            }
            string saletmp00 = "CREATE TABLE [dbo].[SALETMP00] (     "
+ "	[SHOP_ID] [nvarchar] (14) COLLATE Chinese_PRC_CI_AS NOT NULL ,   "
+ "	[SALE_ID] [nvarchar] (32) COLLATE Chinese_PRC_CI_AS NOT NULL ,   "
+"	[STATUS_ID] [nchar] (1) COLLATE Chinese_PRC_CI_AS NOT NULL ,     "
+"	[POS_ID] [nvarchar] (2) COLLATE Chinese_PRC_CI_AS NULL ,         "
+"	[SALE_DATE] [datetime] NOT NULL ,                                "
+"	[SALE_USER] [nvarchar] (10) COLLATE Chinese_PRC_CI_AS NULL ,     "
+"	[VIP_ID] [nvarchar] (20) COLLATE Chinese_PRC_CI_AS NULL ,        "
+"	[TOT_SALES] [money] NULL ,                                       "
+"	[TRANSFER_STATUS] [nchar] (2) COLLATE Chinese_PRC_CI_AS NULL ,   "
+"	[LOCKED] [bit] NOT NULL ,                                        "
+"	[TOT_QUAN] [numeric](18, 4) NULL ,                               "
+"	[CHANGE] [money] NOT NULL ,                                      "
+"	[METHOD_ID] [int] NOT NULL ,                                     "
+"	[TOT_TAX] [money] NOT NULL ,                                     "
+"	[MEAL_KIND] [smallint] NOT NULL ,                                "
+"	[LAST_UPDATE] [datetime] NOT NULL ,                              "
+"	[BACK_SALES] [money] NULL ,                                      "
+"	[BACK_QUAN] [numeric](18, 0) NULL ,                              "
+"	[TYPE] [nchar] (1) COLLATE Chinese_PRC_CI_AS NULL ,              "
+"	[STORE_ID] [nvarchar] (20) COLLATE Chinese_PRC_CI_AS NULL        "
+") ON [PRIMARY]"
            + " ALTER TABLE [dbo].[SALETMP00] WITH NOCHECK ADD "
+ "	CONSTRAINT [PK_SALETMP00] PRIMARY KEY  CLUSTERED "
+ "	("
+ "		[SHOP_ID],"
+ "		[SALE_ID]"
+ "	)  ON [PRIMARY] ";
            b = DBSql.STRunSQL(saletmp00);
            if (MainForm.progressForm != null)
            {
                MainForm.progressForm.Add(1);
            }
            string saletmp01 = "CREATE TABLE [dbo].[SALETMP01] (      "
+ "    [SHOP_ID] [nvarchar] (14) COLLATE Chinese_PRC_CI_AS NOT NULL ,  "
+ "    [SALE_ID] [nvarchar] (32) COLLATE Chinese_PRC_CI_AS NOT NULL ,  "
+ "    [SALE_SNO] [smallint] NOT NULL ,                                "
+"    [PROD_ID] [nvarchar] (20) COLLATE Chinese_PRC_CI_AS NOT NULL ,  "
+"    [SALE_PRICE] [money] NOT NULL ,                                 "
+"    [QTY] [int] NOT NULL ,                                          "
+"    [ITEM_DISC] [money] NOT NULL ,                                  "
+"    [PROM_ID] [nvarchar] (20) COLLATE Chinese_PRC_CI_AS NULL ,      "
+"    [PROM_SNO] [int] NULL ,                                         "
+"    [PRICE_TYPE] [nchar] (1) COLLATE Chinese_PRC_CI_AS NOT NULL ,   "
+"    [FREE_EMP] [nvarchar] (10) COLLATE Chinese_PRC_CI_AS NULL ,     "
+"    [COMB_SALE_SNO] [smallint] NULL ,                               "
+"    [COMB_SNO] [smallint] NULL ,                                    "
+"    [COMB_TYPE] [nchar] (1) COLLATE Chinese_PRC_CI_AS NULL ,        "
+"    [ITEM_TAX] [money] NOT NULL ,                                   "
+"    [OUTINCOME] [bit] NOT NULL ,                                    "
+"    [MEAL_TICKET] [int] NOT NULL ,                                  "
+"    [BY_TOKEN] [bit] NOT NULL ,                                     "
+"    [RELATE_PROD] [nvarchar] (20) COLLATE Chinese_PRC_CI_AS NULL ,  "
+"    [SALE_ORGINAL_PRICE] [numeric](18, 4) NOT NULL ,                "
+"    [ITEM_DISC_TOT] [numeric](18, 4) NOT NULL ,                     "
+"    [ACT_PRICE] [numeric](18, 6) NOT NULL ,                         "
+"    [ISPROM] [bit] NULL ,                                           "
+"    [GROUP_PROD] [nvarchar] (400) COLLATE Chinese_PRC_CI_AS NULL ,  "
+"    [STATUS_ID] [nchar] (1) COLLATE Chinese_PRC_CI_AS NULL ,        "
+"    [TRANSFER_STATUS] [nchar] (2) COLLATE Chinese_PRC_CI_AS NULL    "
+") ON [PRIMARY]"
            + " ALTER TABLE [dbo].[SALETMP01] WITH NOCHECK ADD "
+ "	CONSTRAINT [PK_SALETMP01] PRIMARY KEY  CLUSTERED "
+ "	("
+ "		[SHOP_ID],"
+ "		[SALE_ID],"
+ "		[SALE_SNO]"
+ "	)  ON [PRIMARY] ";
            b = DBSql.STRunSQL(saletmp01);
            if (MainForm.progressForm != null)
            {
                MainForm.progressForm.Add(1);
            }
            string saletmp02 = "CREATE TABLE [dbo].[SALETMP02] (         "
+ "    [SHOP_ID] [nvarchar] (14) COLLATE Chinese_PRC_CI_AS NOT NULL ,     "
+ "    [SALE_ID] [nvarchar] (32) COLLATE Chinese_PRC_CI_AS NOT NULL ,     "
+ "    [SALE_SNO] [smallint] NOT NULL ,                                   "
+"    [PAY_ID] [nvarchar] (10) COLLATE Chinese_PRC_CI_AS NOT NULL ,      "
+"    [AMOUNT] [money] NULL ,                                            "
+"    [TRANSFER_STATUS] [nchar] (1) COLLATE Chinese_PRC_CI_AS NOT NULL , "
+"    [LAST_UPDATE] [datetime] NOT NULL ,                                "
+"    [FACE_VALUE] [money] NULL                                          "
+") ON [PRIMARY]"
            + " ALTER TABLE [dbo].[SALETMP02] WITH NOCHECK ADD "
+ "	CONSTRAINT [PK_SALETMP02] PRIMARY KEY  CLUSTERED "
+ "	("
+ "		[SHOP_ID],"
+ "		[SALE_ID],"
+ "		[SALE_SNO]"
+ "	)  ON [PRIMARY] ";
            b = DBSql.STRunSQL(saletmp02);
            if (MainForm.progressForm != null)
            {
                MainForm.progressForm.Add(1);
            }                                                       
            string saletmp03 = "CREATE TABLE [dbo].[SALETMP03] (         "
+ "    [SHOP_ID] [nvarchar] (14) COLLATE Chinese_PRC_CI_AS NOT NULL ,     "
+ "    [SALE_ID] [nvarchar] (32) COLLATE Chinese_PRC_CI_AS NOT NULL ,     "
+ "    [SEX] [smallint] NOT NULL ,                                        "
+ "    [ITEM_ID] [smallint] NOT NULL,                                    "
+"    [PERSON_NUM] [int] NOT NULL                                        "
+") ON [PRIMARY]"
            + " ALTER TABLE [dbo].[SALETMP03] WITH NOCHECK ADD "
+ "	CONSTRAINT [PK_SALETMP03] PRIMARY KEY  CLUSTERED "
+ "	("
+ "		[SHOP_ID],"
+ "		[SALE_ID],"
+ "		[SEX],"
+ "		[ITEM_ID]"
+ "	)  ON [PRIMARY] ";
            b = DBSql.STRunSQL(saletmp03);
            if (MainForm.progressForm != null)
            {
                MainForm.progressForm.Add(1);
            }
            string shop00 = @"CREATE TABLE [dbo].[SHOP00] (
	[SHOP_ID] [nvarchar] (12) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[SHOP_NAME] [nvarchar] (30) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[SHOP_MEMO] [nvarchar] (50) COLLATE Chinese_PRC_CI_AS NULL ,
	[TEL] [nvarchar] (30) COLLATE Chinese_PRC_CI_AS NULL ,
	[PORT] [nvarchar] (4) COLLATE Chinese_PRC_CI_AS NULL ,
	[LEADER] [nvarchar] (10) COLLATE Chinese_PRC_CI_AS NULL ,
	[SHOP_KIND] [nvarchar] (50) COLLATE Chinese_PRC_CI_AS NULL ,
	[SHOP_IP] [nvarchar] (30) COLLATE Chinese_PRC_CI_AS NULL ,
	[CLOSE_DATE] [int] NULL ,
	[BALANCE] [money] NULL ,
	[PRE_PAID] [money] NULL ,
	[ADDRESS] [nvarchar] (100) COLLATE Chinese_PRC_CI_AS NULL ,
	[ZIP] [nvarchar] (10) COLLATE Chinese_PRC_CI_AS NULL ,
	[CODE] [nvarchar] (20) COLLATE Chinese_PRC_CI_AS NULL ,
	[SHOP_LEVEL] [nvarchar] (50) COLLATE Chinese_PRC_CI_AS NULL ,
	[INDUSTRY] [nvarchar] (50) COLLATE Chinese_PRC_CI_AS NULL ,
	[CITY] [nvarchar] (10) COLLATE Chinese_PRC_CI_AS NULL ,
	[CITY_TYPE] [nvarchar] (50) COLLATE Chinese_PRC_CI_AS NULL ,
	[SO_DATE] [datetime] NULL ,
	[GO_DATE] [datetime] NULL ,
	[CHANGE_DATE] [datetime] NULL ,
	[INVESTMENT] [money] NULL ,
	[BALANCE_POINT] [money] NULL ,
	[SEATS] [int] NULL ,
	[ACREAGE] [numeric](18, 4) NULL ,
	[FAX] [nvarchar] (50) COLLATE Chinese_PRC_CI_AS NULL ,
	[SUPERIOR] [nvarchar] (12) COLLATE Chinese_PRC_CI_AS NULL ,
	[SHOP_TYPE] [nvarchar] (50) COLLATE Chinese_PRC_CI_AS NULL ,
	[REGION_ID] [nvarchar] (10) COLLATE Chinese_PRC_CI_AS NULL ,
	[LAST_UPDATE] [datetime] NOT NULL ,
	[PARTLY] [nvarchar] (50) COLLATE Chinese_PRC_CI_AS NULL ,
	[TEL2] [nvarchar] (30) COLLATE Chinese_PRC_CI_AS NULL ,
	[MEMO2] [nvarchar] (50) COLLATE Chinese_PRC_CI_AS NULL ,
	[STATUS_CBDTYPE] [nvarchar] (50) COLLATE Chinese_PRC_CI_AS NULL ,
	[STATUS_ADDR] [nvarchar] (50) COLLATE Chinese_PRC_CI_AS NULL ,
	[STATUS_FLOOR] [nvarchar] (50) COLLATE Chinese_PRC_CI_AS NULL ,
	[STATUS_CITYTYPE] [nvarchar] (50) COLLATE Chinese_PRC_CI_AS NULL ,
	[STATUS_CITYAREA] [float] NULL ,
	[STATUS_PEOPLE] [float] NULL ,
	[STATUS_NOTAGRO] [float] NULL ,
	[STATUS_FLOW] [float] NULL ,
	[STATUS_SALARY] [money] NULL ,
	[STATUS_businesAREA] [float] NULL ,
	[STATUS_KITCHENAREA] [float] NULL ,
	[STATUS_REDINING] [nvarchar] (50) COLLATE Chinese_PRC_CI_AS NULL ,
	[STATUS_DEVMEMO] [nvarchar] (50) COLLATE Chinese_PRC_CI_AS NULL ,
	[STATUS_DINMEMO] [nvarchar] (50) COLLATE Chinese_PRC_CI_AS NULL ,
	[PPL_SCALE] [nvarchar] (50) COLLATE Chinese_PRC_CI_AS NULL ,
	[PPL_DATE] [datetime] NULL ,
	[PPL_MANFLOW] [int] NULL ,
	[PPL_SCALEMAN] [nvarchar] (30) COLLATE Chinese_PRC_CI_AS NULL ,
	[PPL_PRIZE] [int] NULL ,
	[PPL_MEMO] [nvarchar] (50) COLLATE Chinese_PRC_CI_AS NULL ,
	[PPL_MONEY] [money] NULL ,
	[PPL_TC] [int] NULL ,
	[PPL_AC] [money] NULL ,
	[MAN_AUTHOR] [nvarchar] (50) COLLATE Chinese_PRC_CI_AS NULL ,
	[MAN_NAME] [nvarchar] (20) COLLATE Chinese_PRC_CI_AS NULL ,
	[MAN_MAIN] [nvarchar] (50) COLLATE Chinese_PRC_CI_AS NULL ,
	[IN_DATE] [datetime] NULL ,
	[REPAIR_DATE] [datetime] NULL ,
	[MANAGER_QTY] [int] NULL ,
	[SERVICE_QTY] [int] NULL ,
	[OTHER_QTY] [int] NULL ,
	[CITY_LEVEL] [nvarchar] (50) COLLATE Chinese_PRC_CI_AS NULL ,
	[CATEGORY1] [nvarchar] (50) COLLATE Chinese_PRC_CI_AS NULL ,
	[CATEGORY2] [nvarchar] (50) COLLATE Chinese_PRC_CI_AS NULL ,
	[CATEGORY3] [nvarchar] (50) COLLATE Chinese_PRC_CI_AS NULL ,
	[CATEGORY4] [nvarchar] (50) COLLATE Chinese_PRC_CI_AS NULL ,
	[CATEGORY5] [nvarchar] (50) COLLATE Chinese_PRC_CI_AS NULL ,
	[CATEGORY6] [nvarchar] (50) COLLATE Chinese_PRC_CI_AS NULL ,
	[CATEGORY7] [nvarchar] (50) COLLATE Chinese_PRC_CI_AS NULL ,
	[CATEGORY8] [nvarchar] (50) COLLATE Chinese_PRC_CI_AS NULL ,
	[CATEGORY9] [nvarchar] (50) COLLATE Chinese_PRC_CI_AS NULL ,
	[CATEGORY10] [nvarchar] (50) COLLATE Chinese_PRC_CI_AS NULL ,
	[PROVINCE] [nvarchar] (10) COLLATE Chinese_PRC_CI_AS NULL ,
	[CATEGORY11] [nvarchar] (50) COLLATE Chinese_PRC_CI_AS NULL ,
	[CATEGORY12] [nvarchar] (50) COLLATE Chinese_PRC_CI_AS NULL ,
	[CATEGORY13] [nvarchar] (50) COLLATE Chinese_PRC_CI_AS NULL ,
	[CATEGORY14] [nvarchar] (50) COLLATE Chinese_PRC_CI_AS NULL ,
	[CATEGORY15] [nvarchar] (50) COLLATE Chinese_PRC_CI_AS NULL ,
	[CATEGORY16] [nvarchar] (50) COLLATE Chinese_PRC_CI_AS NULL ,
	[CATEGORY17] [nvarchar] (50) COLLATE Chinese_PRC_CI_AS NULL ,
	[CATEGORY18] [nvarchar] (50) COLLATE Chinese_PRC_CI_AS NULL ,
	[CATEGORY19] [nvarchar] (50) COLLATE Chinese_PRC_CI_AS NULL ,
	[CATEGORY20] [nvarchar] (50) COLLATE Chinese_PRC_CI_AS NULL ,
	[GROUP_ID] [varchar] (6) COLLATE Chinese_PRC_CI_AS NULL ,
	[SQL_STR] [nvarchar] (500) COLLATE Chinese_PRC_CI_AS NULL ,
	[DISCOUNT] [smallint] NULL ,
	[TRANSFER_STATUS] [nchar] (1) COLLATE Chinese_PRC_CI_AS NULL,
    [RATIO] [numeric] (18,4) NULL
) ON [PRIMARY]";
            b = DBSql.STRunSQL(shop00);
            if (MainForm.progressForm != null)
            {
                MainForm.progressForm.Add(1);
            }
                string vip00 = "CREATE TABLE [dbo].[VIP00] (            "
+ "    [VIP_ID] [nvarchar] (20) COLLATE Chinese_PRC_CI_AS NOT NULL ,     "
+"    [VIP_NAME] [nvarchar] (20) COLLATE Chinese_PRC_CI_AS NULL ,       "
+"    [VIP_CARD] [nvarchar] (20) COLLATE Chinese_PRC_CI_AS NULL ,       "
+"    [CARD_TYPE] [nvarchar] (20) COLLATE Chinese_PRC_CI_AS NULL ,      "
+"    [VIP_MEMO] [nvarchar] (100) COLLATE Chinese_PRC_CI_AS NULL ,      "
+"    [ADDRESS] [nvarchar] (50) COLLATE Chinese_PRC_CI_AS NULL ,        "
+"    [ZIP] [nvarchar] (10) COLLATE Chinese_PRC_CI_AS NULL ,            "
+"    [VIP_TELE] [nvarchar] (20) COLLATE Chinese_PRC_CI_AS NULL ,       "
+"    [DISCOUNT] [int] NULL ,                                           "
+"    [CONSUMED] [money] NULL ,                                         "
+"    [TOTAL_CONS] [money] NULL ,                                       "
+"    [TIMES] [int] NULL ,                                              "
+"    [TRANSFER_STATUS] [nchar] (2) COLLATE Chinese_PRC_CI_AS NOT NULL ,"
+"    [VIP_SEX] [nchar] (1) COLLATE Chinese_PRC_CI_AS NOT NULL ,        "
+"    [VIP_BIRTH] [datetime] NULL ,                                     "
+"    [AGE_SECTION] [nvarchar] (10) COLLATE Chinese_PRC_CI_AS NULL ,    "
+"    [APPLY_DATE] [datetime] NULL ,                                    "
+"    [SALARY] [money] NULL ,                                           "
+"    [EMAIL] [nvarchar] (50) COLLATE Chinese_PRC_CI_AS NULL ,          "
+"    [MOBILE] [nvarchar] (20) COLLATE Chinese_PRC_CI_AS NULL ,         "
+"    [OCCUPATION] [nvarchar] (20) COLLATE Chinese_PRC_CI_AS NULL ,     "
+"    [COMPANY] [nvarchar] (100) COLLATE Chinese_PRC_CI_AS NULL ,       "
+"    [MARRIAGE] [nchar] (1) COLLATE Chinese_PRC_CI_AS NULL ,           "
+"    [TOTAL_POINT] [numeric](18, 4) NULL ,                             "
+"    [TOTAL_REDEEM_POINT] [numeric](18, 4) NULL ,                      "
+"    [ID_CARD] [nvarchar] (50) COLLATE Chinese_PRC_CI_AS NULL ,        "
+"    [END_DATE] [datetime] NULL ,                                      "
+"    [COMPANY_ADDR] [nvarchar] (200) COLLATE Chinese_PRC_CI_AS NULL ,  "
+"    [STATUS] [nchar] (1) COLLATE Chinese_PRC_CI_AS NOT NULL ,         "
+"    [LAST_UPDATE] [datetime] NOT NULL ,                               "
+"    [SHOP_ID] [nvarchar] (20) COLLATE Chinese_PRC_CI_AS NULL ,        "
+"    [EXTENDS1] [nvarchar] (10) COLLATE Chinese_PRC_CI_AS NULL ,       "
+"    [EXTENDS2] [nvarchar] (10) COLLATE Chinese_PRC_CI_AS NULL ,       "
+"    [EXTENDS3] [nvarchar] (10) COLLATE Chinese_PRC_CI_AS NULL ,       "
+"    [EXTENDS4] [nvarchar] (10) COLLATE Chinese_PRC_CI_AS NULL ,       "
+"    [EXTENDS5] [nvarchar] (10) COLLATE Chinese_PRC_CI_AS NULL ,       "
+"    [INFORMATION] [bit] NULL ,                                        "
+"    [child] [nvarchar] (10) COLLATE Chinese_PRC_CI_AS NULL ,          "
+"    [child_birth] [datetime] NULL                                     "
+") ON [PRIMARY]"
                + " ALTER TABLE [dbo].[VIP00] WITH NOCHECK ADD "
                + "	CONSTRAINT [PK_VIP00] PRIMARY KEY  CLUSTERED "
                + "	("
                + "		[VIP_ID]"
                + "	)  ON [PRIMARY] ";
                b = DBSql.STRunSQL(vip00);
                if (MainForm.progressForm != null)
                {
                    MainForm.progressForm.Add(1);
                }
            string weather = "CREATE TABLE [dbo].[WEATHER] (            "
+ "    [SHOP_ID] [nvarchar] (14) COLLATE Chinese_PRC_CI_AS NOT NULL ,      "
+ "    [W_DATE] [datetime] NOT NULL ,                                      "
+"    [WEATHER] [nvarchar] (8) COLLATE Chinese_PRC_CI_AS NULL ,           "
+"    [LOW_TEMPER] [smallint] NULL ,                                      "
+"    [HIGHT_TEMPER] [smallint] NULL ,                                    "
+"    [LABOR_HOUR] [int] NOT NULL ,                                       "
+"    [LAST_UPDATE] [datetime] NOT NULL ,                                 "
+"    [Transfer_status] [nchar] (1) COLLATE Chinese_PRC_CI_AS NOT NULL ,  "
+"    [MEMO] [nvarchar] (100) COLLATE Chinese_PRC_CI_AS NULL ,            "
+"    [FORECAST_SALE] [money] NOT NULL                                    "
+") ON [PRIMARY]"
            + " ALTER TABLE [dbo].[WEATHER] WITH NOCHECK ADD "
            + "	CONSTRAINT [PK_WEATHER] PRIMARY KEY  CLUSTERED "
            + "	("
            + "		[SHOP_ID],"
            + "		[W_DATE]"
            + "	)  ON [PRIMARY] ";
            b = DBSql.STRunSQL(weather);
            if (MainForm.progressForm != null)
            {
                MainForm.progressForm.Add(1);
            }
            
                  string pos_transfer = "  CREATE TABLE [dbo].[POS_TRANSFER] ("
+ "	[Transfer_id] [int] NOT NULL ,                                            "
+"	[Transfer_mode] [nvarchar] (25) COLLATE Chinese_PRC_CI_AS NULL ,          "
+"	[Transfer_content] [nvarchar] (50) COLLATE Chinese_PRC_CI_AS NULL ,       "
+"	[Transfer_table] [nvarchar] (50) COLLATE Chinese_PRC_CI_AS NULL ,         "
+"	[Transfer_sql] [nvarchar] (255) COLLATE Chinese_PRC_CI_AS NULL ,          "
+"	[ENABLE] [bit] NOT NULL ,                                                 "
+"	[PRIORITY] [smallint] NOT NULL                                            "
+") ON [PRIMARY]"
                  + " ALTER TABLE [dbo].[POS_TRANSFER] WITH NOCHECK ADD "
                  + "	CONSTRAINT [PK_POS_TRANSFER] PRIMARY KEY  CLUSTERED "
                  + "	("
                  + "		[Transfer_id]"
                  + "	)  ON [PRIMARY] ";
            b = DBSql.STRunSQL(pos_transfer);

            if (MainForm.progressForm != null)
            {
                MainForm.progressForm.Add(1);
            }
           
            if (MainForm.progressForm != null)
            {
                MainForm.progressForm.Add(1);
            }
            MainForm.progressForm.Dispose();
            MainForm.progressForm = null;
            return b;
        
        }

       
    }
}
