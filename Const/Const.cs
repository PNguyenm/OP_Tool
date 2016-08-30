using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Configuration;
using System.IO;

namespace OnlineProducts
{
    public static class INIT
    {
        #region <Config Variables>
        public const string Version = "Version 1.0";
        public const string Name = "DATA MAPPING TEST TOOL";
        public static string Init_Folder = ConfigurationManager.AppSettings["Init_Folder"];
        public static string Init_path = Init_Folder + @"\" + ConfigurationManager.AppSettings["Init_File"];
        public static string PublicHolidays_path = Init_Folder + @"\" + ConfigurationManager.AppSettings["PublicHolidays_File"];
        public static string SQL_path = Init_Folder + @"\" + ConfigurationManager.AppSettings["SQLs_File"];
        public static string ENV = ConfigurationManager.AppSettings["Environment"];
        public static string EMICS = ConfigurationManager.AppSettings["Emics"];
        public static string TASK_MANAGER = ConfigurationManager.AppSettings["TaskManager"];
        public static string EMPOWER = ConfigurationManager.AppSettings["EMPOWER"];
        public static string PIVOTAL = ConfigurationManager.AppSettings["Pivotal"];

        #endregion
        public static bool init()
        {

            if ((!File.Exists(INIT.Init_path)) || (!File.Exists(INIT.PublicHolidays_path)))
            {
                INIT.Init_path = CommonFuncs.GetAssemblyDirectory() + @"\" + ConfigurationManager.AppSettings["Init_File"];
                INIT.PublicHolidays_path = CommonFuncs.GetAssemblyDirectory() + @"\" + ConfigurationManager.AppSettings["PublicHolidays_File"];
                INIT.SQL_path = CommonFuncs.GetAssemblyDirectory() + @"\" + ConfigurationManager.AppSettings["SQLs_File"];
            }
            if ((!File.Exists(INIT.Init_path)) || (!File.Exists(INIT.PublicHolidays_path)))
            {
                return false;
            }
            else
            {
                CONST_DATABASE_CONFIG.db_Emics_WCNSW = new clsDatabaseInfo(Init_path, INIT.ENV, CONST_BUSINESSUNIT.WCNSW, EMICS);
                CONST_DATABASE_CONFIG.db_Emics_HOSPEM = new clsDatabaseInfo(Init_path, INIT.ENV, CONST_BUSINESSUNIT.HOSPEM, EMICS);
                CONST_DATABASE_CONFIG.db_Emics_TMF = new clsDatabaseInfo(Init_path, INIT.ENV, CONST_BUSINESSUNIT.TMF, EMICS);

                CONST_DATABASE_CONFIG.db_TaskManager = new clsDatabaseInfo(Init_path, INIT.ENV, TASK_MANAGER, TASK_MANAGER);
                CONST_DATABASE_CONFIG.db_Pivotal = new clsDatabaseInfo(Init_path, INIT.ENV, PIVOTAL, PIVOTAL);
                CONST_DATABASE_CONFIG.db_Empower = new clsDatabaseInfo(Init_path, INIT.ENV, EMPOWER, EMPOWER); 

                return true;
            }
        }
       
    }

    public static class CONST_BUSINESSUNIT
    {
        public static string WCNSW = ConfigurationManager.AppSettings["WCNSW"];
        public static string HOSPEM = ConfigurationManager.AppSettings["HOSPEM"];
        public static string TMF = ConfigurationManager.AppSettings["TMF"];
        public static string[] lst_BusinessUnits = new string[] { WCNSW, HOSPEM, TMF };
    }

    public static class CONST_HOLIDAYS
    {       
        public static List<DateTime> lst_Public_Holidays;        
    }
    public static class CONST_DATABASE_CONFIG
    {
        public static clsDatabaseInfo db_Emics_WCNSW = new clsDatabaseInfo();
        public static clsDatabaseInfo db_Emics_HOSPEM = new clsDatabaseInfo();
        public static clsDatabaseInfo db_Emics_TMF = new clsDatabaseInfo();
        public static clsDatabaseInfo db_TaskManager = new clsDatabaseInfo();

        public static clsDatabaseInfo db_Pivotal = new clsDatabaseInfo();
        public static clsDatabaseInfo db_Empower = new clsDatabaseInfo();


        public const int Time_Out = 30;

        public const string TASK_MANAGER = "TM";
        public const string EMICS = "EMICS";
    }
    public static class CONST_ENVIRONMENT
    {
        public const string DEV = "Dev";
        public const string PRE_QA = "Pre-QA";
        public const string QA = "QA";
    }
    public static class CONST_ESCALTION
    {
        public const int TMF_ESCALATION_1 = 1;
        public const int TMF_ESCALATION_2 = 5;
        public const int WCN_ESCALATION_1 = 3;
        public const int WCN_ESCALATION_2 = 8;
        public const string ESCALATION_TL = "TEAM LEADER";
        public const string ESCALATION_GM = "GROUP MANAGER";
        public const int No_WorkingDays = 5;
    }
    public static class CONST_DATETYPE
    {
        public const string CALENDAR_DAY = "CALENDAR DAY";
        public const string BUSINESS_DAY = "BUSINESS DAY";
        public const string CALENDAR_MONTH = "CALENDAR MONTH";
        public const string WEEK = "WEEK";

        public const string ADD = "ADD";
        public const string SUBSTRACT = "SUBSTRACT";

        public const string DDMMYYYY = "dd/MM/yyyy";
        public const string YYYYMMDD = "yyyyMMdd";
        public const string DDMMMYYYY = "dd-MMM-yyyy";
        public const string YYYY_MM_DD = "yyyy-MM-dd";
    }
}
