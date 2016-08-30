using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace OnlineProducts
{
    public class Pivotal
    {
        private string main_node = "//Database[@name='" + INIT.PIVOTAL + "']";
        private string sql_GetClaim; 

        public List<clsClaim> Claims;
        public Pivotal()
        {
            sql_GetClaim = CommonFuncs.getNodeValue<string>(INIT.SQL_path, main_node + @"/Script[@name='Get_Claims']");
        }

        public List<clsClaim> GetClaims()
        {
            return CommonFuncs.getObjectsListFromDB<clsClaim>(CONST_DATABASE_CONFIG.db_Pivotal.ConnString, sql_GetClaim);
            //return null;
        }
    }
}
