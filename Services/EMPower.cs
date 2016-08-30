using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace OnlineProducts
{
    public class EMPower
    {
        private string main_node = "//Database[@name='" + INIT.EMPOWER + "']";
        private string sql_GetClaim;

        public List<clsClaim> Claims;
        public EMPower()
        {
            sql_GetClaim = CommonFuncs.getNodeValue<string>(INIT.SQL_path, main_node + @"/Script[@name='Get_Claims']");
        }
        public List<clsClaim> GetClaims()
        {
            //return null;
            //XmlReader xmlr = 
            XmlDocument xml = DBConnection.selectData2XML(CONST_DATABASE_CONFIG.db_Empower.ConnString, sql_GetClaim);
            //xml.Load(xmlr);
            XmlNodeList nodelist = xml.GetElementsByTagName("claim");
            List<clsClaim> lst = new List<clsClaim>();
            foreach (XmlNode node in nodelist)
            {
                clsClaim claim = new clsClaim(node);
                lst.Add(claim);
            }
            return lst;
          // return CommonFuncs.getObjectsListFromDB<clsClaim>(CONST_DATABASE_CONFIG.db_Empower.ConnString, sql_GetClaim);
        }
    }
}
