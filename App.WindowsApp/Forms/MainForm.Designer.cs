namespace App.WindowsApp.Forms
{
    partial class MainForm
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null)) components.Dispose();
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            this.panelSide    = new System.Windows.Forms.Panel();
            this.lblLogo      = new System.Windows.Forms.Label();
            this.btnDashboard = new System.Windows.Forms.Button();
            this.btnProducts  = new System.Windows.Forms.Button();
            this.btnCustomers = new System.Windows.Forms.Button();
            this.btnOrders    = new System.Windows.Forms.Button();
            this.lblVersion   = new System.Windows.Forms.Label();
            this.panelContent = new System.Windows.Forms.Panel();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.lblUser      = new System.Windows.Forms.ToolStripStatusLabel();
            this.lblStatus    = new System.Windows.Forms.ToolStripStatusLabel();
            this.lblTime      = new System.Windows.Forms.ToolStripStatusLabel();

            this.panelSide.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            this.SuspendLayout();

            // ── panelSide ────────────────────────────────────────────────────
            this.panelSide.BackColor = System.Drawing.Color.FromArgb(31, 31, 46);
            this.panelSide.Controls.Add(this.lblLogo);
            this.panelSide.Controls.Add(this.btnDashboard);
            this.panelSide.Controls.Add(this.btnProducts);
            this.panelSide.Controls.Add(this.btnCustomers);
            this.panelSide.Controls.Add(this.btnOrders);
            this.panelSide.Controls.Add(this.lblVersion);
            this.panelSide.Dock     = System.Windows.Forms.DockStyle.Left;
            this.panelSide.Location = new System.Drawing.Point(0, 0);
            this.panelSide.Name     = "panelSide";
            this.panelSide.Size     = new System.Drawing.Size(200, 720);
            this.panelSide.TabIndex = 0;

            // ── lblLogo ──────────────────────────────────────────────────────
            this.lblLogo.BackColor  = System.Drawing.Color.FromArgb(22, 22, 35);
            this.lblLogo.Dock       = System.Windows.Forms.DockStyle.Top;
            this.lblLogo.Font       = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Bold);
            this.lblLogo.ForeColor  = System.Drawing.Color.White;
            this.lblLogo.Location   = new System.Drawing.Point(0, 0);
            this.lblLogo.Name       = "lblLogo";
            this.lblLogo.Size       = new System.Drawing.Size(200, 70);
            this.lblLogo.TabIndex   = 0;
            this.lblLogo.Text       = "☕ CafeShop";
            this.lblLogo.TextAlign  = System.Drawing.ContentAlignment.MiddleCenter;

            // ── btnDashboard ─────────────────────────────────────────────────
            this.btnDashboard.BackColor              = System.Drawing.Color.Transparent;
            this.btnDashboard.FlatStyle              = System.Windows.Forms.FlatStyle.Flat;
            this.btnDashboard.FlatAppearance.BorderSize = 0;
            this.btnDashboard.Font                   = new System.Drawing.Font("Segoe UI", 10F);
            this.btnDashboard.ForeColor              = System.Drawing.Color.FromArgb(190, 190, 220);
            this.btnDashboard.Location               = new System.Drawing.Point(0, 75);
            this.btnDashboard.Name                   = "btnDashboard";
            this.btnDashboard.Padding                = new System.Windows.Forms.Padding(20, 0, 0, 0);
            this.btnDashboard.Size                   = new System.Drawing.Size(200, 46);
            this.btnDashboard.TabIndex               = 1;
            this.btnDashboard.Text                   = "🏠  Dashboard";
            this.btnDashboard.TextAlign              = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnDashboard.UseVisualStyleBackColor = false;
            this.btnDashboard.Click                  += new System.EventHandler(this.btnDashboard_Click);
            this.btnDashboard.Cursor                 = System.Windows.Forms.Cursors.Hand;

            // ── btnProducts ──────────────────────────────────────────────────
            this.btnProducts.BackColor              = System.Drawing.Color.Transparent;
            this.btnProducts.FlatStyle              = System.Windows.Forms.FlatStyle.Flat;
            this.btnProducts.FlatAppearance.BorderSize = 0;
            this.btnProducts.Font                   = new System.Drawing.Font("Segoe UI", 10F);
            this.btnProducts.ForeColor              = System.Drawing.Color.FromArgb(190, 190, 220);
            this.btnProducts.Location               = new System.Drawing.Point(0, 125);
            this.btnProducts.Name                   = "btnProducts";
            this.btnProducts.Padding                = new System.Windows.Forms.Padding(20, 0, 0, 0);
            this.btnProducts.Size                   = new System.Drawing.Size(200, 46);
            this.btnProducts.TabIndex               = 2;
            this.btnProducts.Text                   = "📦  Products";
            this.btnProducts.TextAlign              = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnProducts.UseVisualStyleBackColor = false;
            this.btnProducts.Click                  += new System.EventHandler(this.btnProducts_Click);
            this.btnProducts.Cursor                 = System.Windows.Forms.Cursors.Hand;

            // ── btnCustomers ─────────────────────────────────────────────────
            this.btnCustomers.BackColor              = System.Drawing.Color.Transparent;
            this.btnCustomers.FlatStyle              = System.Windows.Forms.FlatStyle.Flat;
            this.btnCustomers.FlatAppearance.BorderSize = 0;
            this.btnCustomers.Font                   = new System.Drawing.Font("Segoe UI", 10F);
            this.btnCustomers.ForeColor              = System.Drawing.Color.FromArgb(190, 190, 220);
            this.btnCustomers.Location               = new System.Drawing.Point(0, 175);
            this.btnCustomers.Name                   = "btnCustomers";
            this.btnCustomers.Padding                = new System.Windows.Forms.Padding(20, 0, 0, 0);
            this.btnCustomers.Size                   = new System.Drawing.Size(200, 46);
            this.btnCustomers.TabIndex               = 3;
            this.btnCustomers.Text                   = "👥  Customers";
            this.btnCustomers.TextAlign              = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnCustomers.UseVisualStyleBackColor = false;
            this.btnCustomers.Click                  += new System.EventHandler(this.btnCustomers_Click);
            this.btnCustomers.Cursor                 = System.Windows.Forms.Cursors.Hand;

            // ── btnOrders ────────────────────────────────────────────────────
            this.btnOrders.BackColor              = System.Drawing.Color.Transparent;
            this.btnOrders.FlatStyle              = System.Windows.Forms.FlatStyle.Flat;
            this.btnOrders.FlatAppearance.BorderSize = 0;
            this.btnOrders.Font                   = new System.Drawing.Font("Segoe UI", 10F);
            this.btnOrders.ForeColor              = System.Drawing.Color.FromArgb(190, 190, 220);
            this.btnOrders.Location               = new System.Drawing.Point(0, 225);
            this.btnOrders.Name                   = "btnOrders";
            this.btnOrders.Padding                = new System.Windows.Forms.Padding(20, 0, 0, 0);
            this.btnOrders.Size                   = new System.Drawing.Size(200, 46);
            this.btnOrders.TabIndex               = 4;
            this.btnOrders.Text                   = "🧾  Orders";
            this.btnOrders.TextAlign              = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnOrders.UseVisualStyleBackColor = false;
            this.btnOrders.Click                  += new System.EventHandler(this.btnOrders_Click);
            this.btnOrders.Cursor                 = System.Windows.Forms.Cursors.Hand;

            // ── lblVersion ───────────────────────────────────────────────────
            this.lblVersion.Dock      = System.Windows.Forms.DockStyle.Bottom;
            this.lblVersion.Font      = new System.Drawing.Font("Segoe UI", 7.5F);
            this.lblVersion.ForeColor = System.Drawing.Color.FromArgb(100, 100, 130);
            this.lblVersion.Location  = new System.Drawing.Point(0, 690);
            this.lblVersion.Name      = "lblVersion";
            this.lblVersion.Size      = new System.Drawing.Size(200, 30);
            this.lblVersion.TabIndex  = 5;
            this.lblVersion.Text      = "v1.0  |  COSC-5136";
            this.lblVersion.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;

            // ── panelContent ─────────────────────────────────────────────────
            this.panelContent.BackColor = System.Drawing.Color.FromArgb(245, 245, 245);
            this.panelContent.Dock      = System.Windows.Forms.DockStyle.Fill;
            this.panelContent.Location  = new System.Drawing.Point(200, 0);
            this.panelContent.Name      = "panelContent";
            this.panelContent.Padding   = new System.Windows.Forms.Padding(10);
            this.panelContent.Size      = new System.Drawing.Size(984, 720);
            this.panelContent.TabIndex  = 1;

            // ── statusStrip1 ─────────────────────────────────────────────────
            this.statusStrip1.BackColor = System.Drawing.Color.FromArgb(31, 31, 46);
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
                this.lblUser, this.lblStatus, this.lblTime });
            this.statusStrip1.Location = new System.Drawing.Point(0, 720);
            this.statusStrip1.Name     = "statusStrip1";
            this.statusStrip1.Size     = new System.Drawing.Size(1184, 22);
            this.statusStrip1.TabIndex = 2;

            // ── lblUser ───────────────────────────────────────────────────────
            this.lblUser.Font      = new System.Drawing.Font("Segoe UI", 8.5F);
            this.lblUser.ForeColor = System.Drawing.Color.White;
            this.lblUser.Name      = "lblUser";
            this.lblUser.Size      = new System.Drawing.Size(80, 17);
            this.lblUser.Text      = "👤 Admin";

            // ── lblStatus ─────────────────────────────────────────────────────
            this.lblStatus.Font      = new System.Drawing.Font("Segoe UI", 8.5F);
            this.lblStatus.ForeColor = System.Drawing.Color.LightGreen;
            this.lblStatus.Name      = "lblStatus";
            this.lblStatus.Size      = new System.Drawing.Size(39, 17);
            this.lblStatus.Spring    = true;
            this.lblStatus.Text      = "Ready";
            this.lblStatus.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;

            // ── lblTime ───────────────────────────────────────────────────────
            this.lblTime.Font      = new System.Drawing.Font("Segoe UI", 8.5F);
            this.lblTime.ForeColor = System.Drawing.Color.White;
            this.lblTime.Name      = "lblTime";
            this.lblTime.Size      = new System.Drawing.Size(80, 17);
            this.lblTime.Text      = "12:00:00 AM";

            // ── MainForm ─────────────────────────────────────────────────────
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode       = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor           = System.Drawing.Color.FromArgb(245, 245, 245);
            this.ClientSize          = new System.Drawing.Size(1184, 742);
            this.Controls.Add(this.panelContent);
            this.Controls.Add(this.panelSide);
            this.Controls.Add(this.statusStrip1);
            this.Font            = new System.Drawing.Font("Segoe UI", 9F);
            this.MinimumSize     = new System.Drawing.Size(900, 600);
            this.Name            = "MainForm";
            this.StartPosition   = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text            = "☕ Cafe Shop Management System";

            this.panelSide.ResumeLayout(false);
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        #endregion

        // ── Control declarations ─────────────────────────────────────────────
        private System.Windows.Forms.Panel    panelSide;
        private System.Windows.Forms.Label    lblLogo;
        private System.Windows.Forms.Button   btnDashboard;
        private System.Windows.Forms.Button   btnProducts;
        private System.Windows.Forms.Button   btnCustomers;
        private System.Windows.Forms.Button   btnOrders;
        private System.Windows.Forms.Label    lblVersion;
        private System.Windows.Forms.Panel    panelContent;
        private System.Windows.Forms.StatusStrip          statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel lblUser;
        private System.Windows.Forms.ToolStripStatusLabel lblStatus;
        private System.Windows.Forms.ToolStripStatusLabel lblTime;
    }
}
