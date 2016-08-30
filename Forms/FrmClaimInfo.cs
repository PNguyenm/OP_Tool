using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OnlineProducts
{
    public partial class FrmClaimInfo : Form
    {
        public FrmClaimInfo()
        {
            InitializeComponent();
   
        }

        private void dgvClaims_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void FrmClaimInfo_Load(object sender, EventArgs e)
        {

        }

        public void showInfo(DataTable tbl)
        {
            //dgvClaims.DataSource = tbl;
            dgvClaims.ReadOnly = true;
            dgvClaims.Columns.Clear();
            dgvClaims.DataSource = tbl;
            dgvClaims.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.DisplayedCells;

        }
    }
}
