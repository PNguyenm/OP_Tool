using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;

namespace OnlineProducts
{
    public static class CommonFuncs
    {
        public static DateTime AddBusinessDays(DateTime aDate, int Number)
        {
            int i = 0;
            int j = (Number > 0) ? 1 : -1;
            Number = Math.Abs(Number);
            while (i < Number)
            {
                aDate = aDate.AddDays(j);
                if (checkBusinessDay(aDate)) { i++; }
            }
            return aDate;
        }
        public static bool checkBusinessDay(DateTime aDate)
        {
            CONST_HOLIDAYS.lst_Public_Holidays = getPublicHolidaysList(INIT.PublicHolidays_path);
            //check for weekend
            DayOfWeek day = aDate.DayOfWeek;
            if ((day >= DayOfWeek.Monday) && (day <= DayOfWeek.Friday))
            {
                //check if public holidays
                DateTime pDay;
                for (int i = 0; i < CONST_HOLIDAYS.lst_Public_Holidays.Count(); i++)
                {
                    pDay = CONST_HOLIDAYS.lst_Public_Holidays[i];
                    if (aDate.Date == pDay.Date) { return false; }
                }
            }
            else { return false; }
            return true;
        }
        public static DateTime ConvertStringToDateTime(string value, string pattern)
        {
            DateTime aDate = DateTime.ParseExact(value, pattern, CultureInfo.InvariantCulture, DateTimeStyles.None);
            return aDate;
        }
        /// <summary>
        /// convert string get from sql datetime to C# datatime
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        /// Created By: Phuong Nguyen, Created Date: 07/09/2015
        public static DateTime ConvertSQLDateFormatToDateTime(string value)
        {
            string[] patterns = new string[] { "d/MM/yyyy hh:mm:ss tt", "dd/MM/yyyy hh:mm:ss tt" };
            string pattern = patterns.Single(c => c.Length == value.Length);
            DateTime aDate = CommonFuncs.ConvertStringToDateTime(value, pattern);
            return aDate;
        }
        /// <summary>
        /// Return list of objects that has data row contructs with data get from xml
        /// </summary>
        /// <typeparam name="T">Object Type</typeparam>
        /// <param name="path"></param>
        /// <returns>List of Object with T type</returns>
        /// Created By: Phuong Nguyen, Created Date: 10/09/2015
        public static List<T> GetObjectsListFromXML<T>(string path)
        {
            try
            {
                using (DataSet ds = new DataSet())
                {
                    List<T> lst = new List<T>();
                    ds.ReadXml(path);
                    var table = ds.Tables[0];
                    foreach (DataRow row in table.Rows)
                    {
                        T t = (T)Activator.CreateInstance(typeof(T), new object[] { row });
                        lst.Add(t);
                    }
                    return lst;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.StackTrace);
                return null;
            }

        }
        public static List<T> getObjectsListFromDB<T>(string connstring, string sql)
        {
            try
            {
                List<T> lst = new List<T>();
                DataTable tbl = DBConnection.selectDataTable(connstring, sql);
                foreach (DataRow row in tbl.Rows)
                {
                    T t = (T)Activator.CreateInstance(typeof(T), new object[] { row });
                    lst.Add(t);
                }
                return lst;
            }
            catch(Exception e)
            {
                MessageBox.Show("Error: " + e.Message + e.StackTrace);
                return null;
            }
        }

        public static List<DateTime> getPublicHolidaysList(string path)
        {
            try
            {
                XmlDocument xml = new XmlDocument();
                {
                    List<DateTime> lst = new List<DateTime>();
                    xml.Load(path);
                    XmlNodeList eList = xml.GetElementsByTagName("Holiday");
                    for (int i = 0; i < eList.Count; i++)
                    {
                        int year = Int32.Parse(eList[i].Attributes["year"].Value);
                        int month = Int32.Parse(eList[i].Attributes["month"].Value);
                        int day = Int32.Parse(eList[i].Attributes["day"].Value);
                        lst.Add(new DateTime(year, month, day));
                    }
                    return lst;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Cannot Load Public Holidays List. Error: " + ex.StackTrace);
                return null;
            }
        }
        public static List<string> getReportsList(string path, string pReportType)
        {
            try
            {
                XmlDocument xml = new XmlDocument();
                {
                    List<string> lst = new List<string>();
                    xml.Load(path);
                    string node = @"//REPORTS_LIST/Group[@Name='" + pReportType + @"']";
                    XmlNodeList eList = xml.SelectNodes(node + @"/Report");
                    foreach(XmlNode item in eList)
                    {
                        lst.Add(item.InnerText);
                    }
                    return lst;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Cannot Load Reports List. Error: " + ex.StackTrace);
                return null;
            }
        }
        
        public static DataTable getPublicHolidays(string path)
        {
            XmlDocument xml = new XmlDocument();
            try
            {
                
                {
                    DataTable tbl = new DataTable();
                    tbl.Columns.Add("#", typeof(int));
                    tbl.Columns.Add("Date", typeof(string));
                    tbl.Columns.Add("Year", typeof(int));
                    tbl.Columns.Add("Month", typeof(int));
                    tbl.Columns.Add("Day", typeof(int));
                   
                    
                    xml.Load(path);
                    XmlNodeList eList = xml.GetElementsByTagName("Holiday");
                    for (int i = 0; i < eList.Count; i++)
                    {
                        int year = Int32.Parse(eList[i].Attributes["year"].Value);
                        int month = Int32.Parse(eList[i].Attributes["month"].Value);
                        int day = Int32.Parse(eList[i].Attributes["day"].Value);
                        DateTime date = new DateTime(year, month, day);
                        tbl.Rows.Add(i + 1, date.ToString("MMM-dd-yyyy"), year, month, day );
                    }
                    xml = null;
                    return tbl;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Cannot Load Public Holidays List. Error: " + ex.StackTrace);
                xml = null;
                GC.Collect();
                return null;
            }
        }
        public static string GetAssemblyDirectory()
        {
            string codeBase = Assembly.GetExecutingAssembly().CodeBase;
            UriBuilder uri = new UriBuilder(codeBase);
            string path = Uri.UnescapeDataString(uri.Path);
            return Path.GetDirectoryName(path);

        }
        public static T getNodeValue<T>(string XMLpath, string xpath)
        {
            XmlDocument xml = new XmlDocument();
            string value = string.Empty;
            try
            {
                
                xml.Load(XMLpath);
                value = xml.SelectSingleNode(xpath).InnerText;
                
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message + e.StackTrace);
            }
            xml = null;
            return (T)Convert.ChangeType(value, typeof(T));
            
        }
    }
}
