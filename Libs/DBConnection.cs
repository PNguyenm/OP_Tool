using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;
using System.Windows.Forms;
using System.Xml;

namespace OnlineProducts
{
    public static class DBConnection
    {
        #region <Connection>
        public static string getConnectionString(string pServer, string pDatabase, string pUser, string pPassword, int pTimeOut)
        {
            string connstring = @"user id =" + pUser + ";"
                                + @"password=" + pPassword + ";"
                                + @"server=" + pServer + ";"
                                + @"database=" + pDatabase + ";"
                                + @"MultipleActiveResultSets=true;"
                                + @"connection timeout=" + pTimeOut.ToString() +";";
            return connstring;
        }
        #endregion
        public static int update(string pConnString, string sql)
        {
            using (SqlConnection conn = new SqlConnection(pConnString))
            {
                try
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand(sql, conn))
                    {
                        return cmd.ExecuteNonQuery();
                    }
                }
                catch (Exception)
                {
                    MessageBox.Show("Cannot connection to database. Please check configuration info.");
                    return 0;
                }
                finally
                {
                    if (conn != null)
                        ((IDisposable)conn).Dispose();
                }
            }
        }
        public static int delete(string pConnString, string sql)
        {
            using (SqlConnection conn = new SqlConnection(pConnString))
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    return cmd.ExecuteNonQuery();
                }
            }
        }
        public static IEnumerable<IDataRecord> select(string pConnString, string sql)
        {
            using (SqlConnection conn = new SqlConnection(pConnString))
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            yield return reader;
                        }
                    }
                }             
            }
        }
        public static DataTable selectDataTable(string pConnString, string sql)
        {
            using (SqlConnection conn = new SqlConnection(pConnString))
            {
                try
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand(sql, conn))                      
                    {
                        cmd.CommandTimeout = 0;
                        using (var reader = cmd.ExecuteReader())
                        {
                            var tbl = new DataTable();
                            tbl.Load(reader);
                            return tbl;
                        }
                    }
                }
                catch (Exception e)
                {
                    MessageBox.Show("Cannot connection to database. Please check configuration info." + e.Message + e.StackTrace);
                    return null;
                }
               
            }
        }

        public static XmlDocument selectData2XML(string pConnString, string sqlXML)
        {
            using (SqlConnection conn = new SqlConnection(pConnString))
            {
                try
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand(sqlXML, conn))
                    {
                        cmd.CommandTimeout = 0;
                        using (XmlReader xmlreader = cmd.ExecuteXmlReader())
                        {
                            XmlDocument xml = new XmlDocument();
                            xml.Load(xmlreader);
                            return xml;
                        }
                    }
                }
                catch (Exception e)
                {
                    MessageBox.Show("Cannot connection to database. Please check configuration info." + e.Message + e.StackTrace);
                    return null;
                }

            }

        }
    }
}
