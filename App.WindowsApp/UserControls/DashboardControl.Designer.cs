namespace App.WindowsApp.UserControls
{
    partial class DashboardControl
    {
        private System.ComponentModel.IContainer components = null;
        protected override void Dispose(bool disposing)
        { if (disposing && (components != null)) components.Dispose(); base.Dispose(disposing); }

        #region Component Designer generated code
        private void InitializeComponent()
        {
            this.panelTop          = new System.Windows.Forms.Panel();
            this.lblTitle          = new System.Windows.Forms.Label();
            this.lblSubtitle       = new System.Windows.Forms.Label();
            this.btnRefresh        = new System.Windows.Forms.Button();
            this.panelCard1        = new System.Windows.Forms.Panel();
            this.lblCard1Title     = new System.Windows.Forms.Label();
            this.lblTotalOrdersVal = new System.Windows.Forms.Label();
            this.panelCard2        = new System.Windows.Forms.Panel();
            this.lblCard2Title     = new System.Windows.Forms.Label();
            this.lblRevenueVal     = new System.Windows.Forms.Label();
            this.panelCard3        = new System.Windows.Forms.Panel();
            this.lblCard3Title     = new System.Windows.Forms.Label();
            this.lblPendingVal     = new System.Windows.Forms.Label();
            this.panelCard4        = new System.Windows.Forms.Panel();
            this.lblCard4Title     = new System.Windows.Forms.Label();
            this.lblCustomersVal   = new System.Windows.Forms.Label();
            this.lblBarTitle       = new System.Windows.Forms.Label();
            this.lblPieTitle       = new System.Windows.Forms.Label();
            this.panelBarChart     = new System.Windows.Forms.Panel();
            this.panelPieChart     = new System.Windows.Forms.Panel();

            this.panelTop.SuspendLayout();
            this.panelCard1.SuspendLayout();
            this.panelCard2.SuspendLayout();
            this.panelCard3.SuspendLayout();
            this.panelCard4.SuspendLayout();
            this.SuspendLayout();

            // panelTop
            this.panelTop.BackColor = System.Drawing.Color.FromArgb(31, 31, 46);
            this.panelTop.Controls.Add(this.btnRefresh);
            this.panelTop.Controls.Add(this.lblSubtitle);
            this.panelTop.Controls.Add(this.lblTitle);
            this.panelTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelTop.Name = "panelTop";
            this.panelTop.Size = new System.Drawing.Size(1150, 65);
            this.panelTop.TabIndex = 0;

            this.lblTitle.Font      = new System.Drawing.Font("Segoe UI", 16F, System.Drawing.FontStyle.Bold);
            this.lblTitle.ForeColor = System.Drawing.Color.White;
            this.lblTitle.Location  = new System.Drawing.Point(15, 10);
            this.lblTitle.Name = "lblTitle"; this.lblTitle.Size = new System.Drawing.Size(300, 30);
            this.lblTitle.TabIndex = 0; this.lblTitle.Text = "🏠  Dashboard";

            this.lblSubtitle.Font      = new System.Drawing.Font("Segoe UI", 9F);
            this.lblSubtitle.ForeColor = System.Drawing.Color.FromArgb(180, 180, 210);
            this.lblSubtitle.Location  = new System.Drawing.Point(18, 42);
            this.lblSubtitle.Name = "lblSubtitle"; this.lblSubtitle.Size = new System.Drawing.Size(400, 18);
            this.lblSubtitle.TabIndex = 1; this.lblSubtitle.Text = "Overview of your cafe performance";

            this.btnRefresh.BackColor = System.Drawing.Color.FromArgb(52, 152, 219);
            this.btnRefresh.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnRefresh.FlatAppearance.BorderSize = 0;
            this.btnRefresh.Font      = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.btnRefresh.ForeColor = System.Drawing.Color.White;
            this.btnRefresh.Location  = new System.Drawing.Point(1020, 17);
            this.btnRefresh.Name = "btnRefresh"; this.btnRefresh.Size = new System.Drawing.Size(110, 30);
            this.btnRefresh.TabIndex = 2; this.btnRefresh.Text = "🔄 Refresh";
            this.btnRefresh.UseVisualStyleBackColor = false;
            this.btnRefresh.Click += new System.EventHandler(this.btnRefresh_Click);

            // Card 1
            this.panelCard1.BackColor = System.Drawing.Color.FromArgb(52, 152, 219);
            this.panelCard1.Controls.Add(this.lblCard1Title); this.panelCard1.Controls.Add(this.lblTotalOrdersVal);
            this.panelCard1.Location = new System.Drawing.Point(15, 80); this.panelCard1.Name = "panelCard1";
            this.panelCard1.Size = new System.Drawing.Size(200, 100); this.panelCard1.TabIndex = 1;
            this.lblCard1Title.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblCard1Title.ForeColor = System.Drawing.Color.FromArgb(220, 235, 255);
            this.lblCard1Title.Location = new System.Drawing.Point(12, 12); this.lblCard1Title.Name = "lblCard1Title";
            this.lblCard1Title.Size = new System.Drawing.Size(176, 18); this.lblCard1Title.TabIndex = 0;
            this.lblCard1Title.Text = "Total Orders";
            this.lblTotalOrdersVal.Font = new System.Drawing.Font("Segoe UI", 22F, System.Drawing.FontStyle.Bold);
            this.lblTotalOrdersVal.ForeColor = System.Drawing.Color.White;
            this.lblTotalOrdersVal.Location = new System.Drawing.Point(10, 38); this.lblTotalOrdersVal.Name = "lblTotalOrdersVal";
            this.lblTotalOrdersVal.Size = new System.Drawing.Size(180, 48); this.lblTotalOrdersVal.TabIndex = 1;
            this.lblTotalOrdersVal.Text = "0";

            // Card 2
            this.panelCard2.BackColor = System.Drawing.Color.FromArgb(46, 204, 113);
            this.panelCard2.Controls.Add(this.lblCard2Title); this.panelCard2.Controls.Add(this.lblRevenueVal);
            this.panelCard2.Location = new System.Drawing.Point(230, 80); this.panelCard2.Name = "panelCard2";
            this.panelCard2.Size = new System.Drawing.Size(200, 100); this.panelCard2.TabIndex = 2;
            this.lblCard2Title.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblCard2Title.ForeColor = System.Drawing.Color.FromArgb(210, 255, 230);
            this.lblCard2Title.Location = new System.Drawing.Point(12, 12); this.lblCard2Title.Name = "lblCard2Title";
            this.lblCard2Title.Size = new System.Drawing.Size(176, 18); this.lblCard2Title.TabIndex = 0;
            this.lblCard2Title.Text = "Total Revenue";
            this.lblRevenueVal.Font = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Bold);
            this.lblRevenueVal.ForeColor = System.Drawing.Color.White;
            this.lblRevenueVal.Location = new System.Drawing.Point(10, 38); this.lblRevenueVal.Name = "lblRevenueVal";
            this.lblRevenueVal.Size = new System.Drawing.Size(180, 48); this.lblRevenueVal.TabIndex = 1;
            this.lblRevenueVal.Text = "Rs. 0";

            // Card 3
            this.panelCard3.BackColor = System.Drawing.Color.FromArgb(231, 76, 60);
            this.panelCard3.Controls.Add(this.lblCard3Title); this.panelCard3.Controls.Add(this.lblPendingVal);
            this.panelCard3.Location = new System.Drawing.Point(445, 80); this.panelCard3.Name = "panelCard3";
            this.panelCard3.Size = new System.Drawing.Size(200, 100); this.panelCard3.TabIndex = 3;
            this.lblCard3Title.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblCard3Title.ForeColor = System.Drawing.Color.FromArgb(255, 220, 215);
            this.lblCard3Title.Location = new System.Drawing.Point(12, 12); this.lblCard3Title.Name = "lblCard3Title";
            this.lblCard3Title.Size = new System.Drawing.Size(176, 18); this.lblCard3Title.TabIndex = 0;
            this.lblCard3Title.Text = "Pending Orders";
            this.lblPendingVal.Font = new System.Drawing.Font("Segoe UI", 22F, System.Drawing.FontStyle.Bold);
            this.lblPendingVal.ForeColor = System.Drawing.Color.White;
            this.lblPendingVal.Location = new System.Drawing.Point(10, 38); this.lblPendingVal.Name = "lblPendingVal";
            this.lblPendingVal.Size = new System.Drawing.Size(180, 48); this.lblPendingVal.TabIndex = 1;
            this.lblPendingVal.Text = "0";

            // Card 4
            this.panelCard4.BackColor = System.Drawing.Color.FromArgb(155, 89, 182);
            this.panelCard4.Controls.Add(this.lblCard4Title); this.panelCard4.Controls.Add(this.lblCustomersVal);
            this.panelCard4.Location = new System.Drawing.Point(660, 80); this.panelCard4.Name = "panelCard4";
            this.panelCard4.Size = new System.Drawing.Size(200, 100); this.panelCard4.TabIndex = 4;
            this.lblCard4Title.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblCard4Title.ForeColor = System.Drawing.Color.FromArgb(235, 215, 255);
            this.lblCard4Title.Location = new System.Drawing.Point(12, 12); this.lblCard4Title.Name = "lblCard4Title";
            this.lblCard4Title.Size = new System.Drawing.Size(176, 18); this.lblCard4Title.TabIndex = 0;
            this.lblCard4Title.Text = "Customers";
            this.lblCustomersVal.Font = new System.Drawing.Font("Segoe UI", 22F, System.Drawing.FontStyle.Bold);
            this.lblCustomersVal.ForeColor = System.Drawing.Color.White;
            this.lblCustomersVal.Location = new System.Drawing.Point(10, 38); this.lblCustomersVal.Name = "lblCustomersVal";
            this.lblCustomersVal.Size = new System.Drawing.Size(180, 48); this.lblCustomersVal.TabIndex = 1;
            this.lblCustomersVal.Text = "0";

            // Chart labels
            this.lblBarTitle.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.lblBarTitle.ForeColor = System.Drawing.Color.FromArgb(31, 31, 46);
            this.lblBarTitle.Location = new System.Drawing.Point(15, 195); this.lblBarTitle.Name = "lblBarTitle";
            this.lblBarTitle.Size = new System.Drawing.Size(400, 22); this.lblBarTitle.TabIndex = 5;
            this.lblBarTitle.Text = "📊  Revenue — Last 7 Days";

            this.lblPieTitle.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.lblPieTitle.ForeColor = System.Drawing.Color.FromArgb(31, 31, 46);
            this.lblPieTitle.Location = new System.Drawing.Point(590, 195); this.lblPieTitle.Name = "lblPieTitle";
            this.lblPieTitle.Size = new System.Drawing.Size(400, 22); this.lblPieTitle.TabIndex = 6;
            this.lblPieTitle.Text = "🥧  Sales by Category";

            // panelBarChart
            this.panelBarChart.BackColor   = System.Drawing.Color.White;
            this.panelBarChart.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panelBarChart.Location    = new System.Drawing.Point(15, 220);
            this.panelBarChart.Name        = "panelBarChart";
            this.panelBarChart.Size        = new System.Drawing.Size(555, 260);
            this.panelBarChart.TabIndex    = 7;

            // panelPieChart
            this.panelPieChart.BackColor   = System.Drawing.Color.White;
            this.panelPieChart.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panelPieChart.Location    = new System.Drawing.Point(590, 220);
            this.panelPieChart.Name        = "panelPieChart";
            this.panelPieChart.Size        = new System.Drawing.Size(545, 260);
            this.panelPieChart.TabIndex    = 8;

            // DashboardControl
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode       = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor           = System.Drawing.Color.FromArgb(245, 245, 245);
            this.Controls.Add(this.panelTop);
            this.Controls.Add(this.panelCard1); this.Controls.Add(this.panelCard2);
            this.Controls.Add(this.panelCard3); this.Controls.Add(this.panelCard4);
            this.Controls.Add(this.lblBarTitle); this.Controls.Add(this.lblPieTitle);
            this.Controls.Add(this.panelBarChart); this.Controls.Add(this.panelPieChart);
            this.Name = "DashboardControl";
            this.Size = new System.Drawing.Size(1150, 690);

            this.panelTop.ResumeLayout(false);
            this.panelCard1.ResumeLayout(false); this.panelCard2.ResumeLayout(false);
            this.panelCard3.ResumeLayout(false); this.panelCard4.ResumeLayout(false);
            this.ResumeLayout(false);
        }
        #endregion

        private System.Windows.Forms.Panel  panelTop;
        private System.Windows.Forms.Label  lblTitle;
        private System.Windows.Forms.Label  lblSubtitle;
        private System.Windows.Forms.Button btnRefresh;
        private System.Windows.Forms.Panel  panelCard1;
        private System.Windows.Forms.Label  lblCard1Title;
        private System.Windows.Forms.Label  lblTotalOrdersVal;
        private System.Windows.Forms.Panel  panelCard2;
        private System.Windows.Forms.Label  lblCard2Title;
        private System.Windows.Forms.Label  lblRevenueVal;
        private System.Windows.Forms.Panel  panelCard3;
        private System.Windows.Forms.Label  lblCard3Title;
        private System.Windows.Forms.Label  lblPendingVal;
        private System.Windows.Forms.Panel  panelCard4;
        private System.Windows.Forms.Label  lblCard4Title;
        private System.Windows.Forms.Label  lblCustomersVal;
        private System.Windows.Forms.Label  lblBarTitle;
        private System.Windows.Forms.Label  lblPieTitle;
        private System.Windows.Forms.Panel  panelBarChart;
        private System.Windows.Forms.Panel  panelPieChart;
    }
}
