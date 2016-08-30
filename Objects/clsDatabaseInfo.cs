using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace OnlineProducts
{
    public class clsDatabaseInfo
    {
        public string Server { get; set; }
        public string Database { get; set; }
        public string User { get; set; }
        public string Password { get; set; }
        public int TimeOut { get; set; }
        public string ConnString { get; set; }

        #region <Constructors>
        public clsDatabaseInfo() { }
        public clsDatabaseInfo(string Server, string Database, string User, string Password, int TimeOut)
        {
            this.Server = Server;
            this.Database = Database;
            this.User = User;
            this.Password = Password;
            this.TimeOut = TimeOut;
        }
        public clsDatabaseInfo(XmlDocument xml, string pEnv, string pBusinessUnit, string pApplication)
        {
            string node = "//Environments_List/Environment[@Name='" + pEnv + "']";
            //XmlDocument xml = new XmlDocument();
            //xml.Load(pPath);
            string Emics_node = node + @"/" + pApplication;
            this.Server = xml.SelectSingleNode(Emics_node + @"/Server").InnerText;
            this.Database = xml.SelectSingleNode(Emics_node + @"/Database[@Name='" + pBusinessUnit + "']").InnerText;
            this.User = xml.SelectSingleNode(Emics_node + @"/User").InnerText;
            this.Password = xml.SelectSingleNode(Emics_node + @"/Password").InnerText;
            this.TimeOut = Int32.Parse(xml.SelectSingleNode(Emics_node + @"/TimeOut").InnerText);
            this.ConnString = getConnString();

        }
        
        #endregion
        public string getConnString()
        {
            ConnString = @"user id =" + User + ";"
                        + @"password=" + Password + ";"
                        + @"server=" + Server + ";"
                        + @"database=" + Database + ";"
                        //+ @"MultipleActiveResultSets=true;"
                        + @"connection timeout=" + TimeOut + ";";
            return ConnString;
        }
        public void setConnString()
        {
            ConnString = @"user id =" + User + ";"
                        + @"password=" + Password + ";"
                        + @"server=" + Server + ";"
                        + @"database=" + Database + ";"
                //+ @"MultipleActiveResultSets=true;"
                        + @"connection timeout=" + TimeOut + ";";
        }
        public clsDatabaseInfo(string pPath, string pEnv, string pBusinessUnit, string pSource)
        {
            string node = "//Environments_List/Environment[@Name='" + pEnv + "']";
            XmlDocument xml = new XmlDocument();
            xml.Load(pPath);
            string Emics_node = node + @"/" + pSource;
            this.Server = xml.SelectSingleNode(Emics_node + @"/Server").InnerText;
            this.Database = xml.SelectSingleNode(Emics_node + @"/Database[@Name='" + pBusinessUnit + "']").InnerText;
            this.User = xml.SelectSingleNode(Emics_node + @"/User").InnerText;
            this.Password = xml.SelectSingleNode(Emics_node + @"/Password").InnerText;
            this.TimeOut = Int32.Parse(xml.SelectSingleNode(Emics_node + @"/TimeOut").InnerText);
            setConnString();
        }
    }
}
