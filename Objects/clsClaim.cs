using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace OnlineProducts
{
    public class clsClaim 
    {
        //test GitHub in p.nguyenm
        #region <PROPERTIES>
        public int claim_id { get; set; } //1
        public int claimant_id { get; set; } //2
        public int policy_id { get; set; } //3
        public int claim_status_id { get; set; } //4
        public int liability_sttus_id { get; set; } //5
        public int case_manager_id { get; set; } //6
        public int injury_nature_id { get; set; } //7
        public int work_status_id { get; set; } //8
        public string notification_number { get; set; } //9
        public string claim_number { get; set; } //10
        public DateTime injury_date { get; set; } //11
        public DateTime employer_notification_date { get; set; } //12
        public DateTime agent_notification_date { get; set; } //13
        public DateTime last_communication_date { get; set; } //14
        public Decimal outstanding_estimate { get; set; } //15
        public Decimal paid_to_date { get; set; } //16
        public Decimal net_incurred { get; set; } //17
        public DateTime expected_rtw_date { get; set; } //18
        public int med_cert_fitness_status_id { get; set; } //19
        public DateTime med_cert_fitness_date { get; set; } //20
        public DateTime med_cert_fitness_date_to { get; set; } //21
 

        #endregion 

        #region <CONSTRUCTORS>
        public clsClaim(DataRow row)
        {
            //1
            this.claim_id = Int32.Parse(row[0].ToString());
            //2
            this.claimant_id = Int32.Parse(row[1].ToString());
            //3


        }
        public clsClaim(XmlNode node)
        {
            this.claim_id = Int32.Parse(node.Attributes["claim_id"].Value);
            Console.WriteLine(this.claim_id);
            this.claimant_id = Int32.Parse(node.Attributes["claimant_id"].Value);
            this.policy_id = Int32.Parse(node.Attributes["policy_id"].Value);
        }
        #endregion
        #region <METHODS>
        public string CompareTo(clsClaim other)
        {
            string msg = string.Empty;
            //1
            if (this.claim_id != other.claim_id) { msg = "claim_id" + ","; }

            //2
            if (this.claim_number != other.claim_number) { msg = "claim_number" + ","; }

            //3

            //return result.
            return msg;
        }
        #endregion
    }
}
