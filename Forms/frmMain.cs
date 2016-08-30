using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OnlineProducts
{
    public partial class frmMain : Form
    {
        private EMPower empower = new EMPower();
        private Pivotal pivotal = new Pivotal();
        private AutoResetEvent _workerCompleted = new AutoResetEvent(false);
        private AutoResetEvent _workerCompleted_Empower = new AutoResetEvent(false);
        public frmMain()
        {
            this.Name = INIT.Name + INIT.Version;
            this.Text = INIT.Name + " " + INIT.Version;
            InitializeComponent();
        }

        private void btnExecute_Click(object sender, EventArgs e)
        {
            //1. Get list of claims in Pivotal view           
            
            //Thread t1 = new Thread();
            BackgroundWorker bgPivotal = new BackgroundWorker();
            bgPivotal.DoWork += new DoWorkEventHandler(bgPivotal_DoWork);
            bgPivotal.RunWorkerCompleted += new RunWorkerCompletedEventHandler(bgPivotal_RunWorkerCompleted);
           // bgPivotal.RunWorkerAsync();
            //loadingForm = new loadingForm();
            
            
            //2. Get Online Product claims
            BackgroundWorker bgEmpower = new BackgroundWorker();
            bgEmpower.DoWork += new DoWorkEventHandler(bgEmpower_DoWork);
            bgEmpower.RunWorkerCompleted += new RunWorkerCompletedEventHandler(bgEmpower_RunWorkerCompleted);
            bgEmpower.RunWorkerAsync();
            empower.Claims = empower.GetClaims();
            

            //3. Compare
            //Create DataTable to store results
            DataTable tblResults = new DataTable();
            tblResults.Columns.Add("#", typeof(int));
            tblResults.Columns.Add("Claim Number", typeof(string));
            tblResults.Columns.Add("Results", typeof(string));

            //wait for job finish
            _workerCompleted_Empower.WaitOne();
           // _workerCompleted.WaitOne();
            for (int i = 0; i < empower.Claims.Count; i++)
            {
                DataRow row = tblResults.NewRow();
                row["#"] = i + 1;
                row["Claim Number"] = empower.Claims[i].claim_number;
               // row["Results"] =  pivotal.Claims[i].CompareTo(empower.Claims[i]);
                tblResults.Rows.Add(row);
            }

            //4. Show results
            FrmClaimInfo frm = new FrmClaimInfo();
           
            frm.showInfo(tblResults);
            frm.Show();





        }

        private void bgPivotal_DoWork(object sender, DoWorkEventArgs e)
        {
            BackgroundWorker worker = sender as BackgroundWorker;
            pivotal.Claims = pivotal.GetClaims();
            Console.WriteLine("Pivotal Job Complete.");
            _workerCompleted.Set();
        }
        private void bgPivotal_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            object result = e.Result;
            _workerCompleted.Set();
        }
        private void bgEmpower_DoWork(object sender, DoWorkEventArgs e)
        {
            BackgroundWorker worker = sender as BackgroundWorker;
            empower.Claims = pivotal.GetClaims();
            Console.WriteLine("Empower Job Complete.");
            _workerCompleted_Empower.Set();
        }
        private void bgEmpower_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            object result = e.Result;
            _workerCompleted_Empower.Set();
        }
        private void frmMain_Load(object sender, EventArgs e)
        {
            INIT.init();
        }
    }
}
