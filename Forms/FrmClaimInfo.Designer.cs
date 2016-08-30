namespace OnlineProducts
{
    partial class FrmClaimInfo
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.dgvClaims = new System.Windows.Forms.DataGridView();
            ((System.ComponentModel.ISupportInitialize)(this.dgvClaims)).BeginInit();
            this.SuspendLayout();
            // 
            // dgvClaims
            // 
            this.dgvClaims.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvClaims.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvClaims.Location = new System.Drawing.Point(0, 0);
            this.dgvClaims.Name = "dgvClaims";
            this.dgvClaims.Size = new System.Drawing.Size(625, 432);
            this.dgvClaims.TabIndex = 0;
            this.dgvClaims.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvClaims_CellContentClick);
            // 
            // FrmClaimInfo
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(625, 432);
            this.Controls.Add(this.dgvClaims);
            this.Name = "FrmClaimInfo";
            this.Text = "FrmClaimInfo";
            this.Load += new System.EventHandler(this.FrmClaimInfo_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvClaims)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dgvClaims;
    }
}