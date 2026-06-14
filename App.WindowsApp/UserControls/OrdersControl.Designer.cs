namespace App.WindowsApp.UserControls
{
    partial class OrdersControl
    {
        private System.ComponentModel.IContainer components = null;
        protected override void Dispose(bool disposing)
        { if (disposing && (components != null)) components.Dispose(); base.Dispose(disposing); }

        #region Component Designer generated code
        private void InitializeComponent()
        {
            this.panelTop     = new System.Windows.Forms.Panel();
            this.lblTitle     = new System.Windows.Forms.Label();
            this.lblSubtitle  = new System.Windows.Forms.Label();
            this.panelSearch  = new System.Windows.Forms.Panel();
            this.txtSearch    = new System.Windows.Forms.TextBox();
            this.cmbStatus    = new System.Windows.Forms.ComboBox();
            this.panelToolbar = new System.Windows.Forms.Panel();
            this.btnAdd       = new System.Windows.Forms.Button();
            this.btnEdit      = new System.Windows.Forms.Button();
            this.btnView      = new System.Windows.Forms.Button();
            this.btnDelete    = new System.Windows.Forms.Button();
            this.btnRefresh   = new System.Windows.Forms.Button();
            this.gridOrders   = new System.Windows.Forms.DataGridView();
            this.lblCount     = new System.Windows.Forms.Label();
            this.panelTop.SuspendLayout();
            this.panelSearch.SuspendLayout();
            this.panelToolbar.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridOrders)).BeginInit();
            this.SuspendLayout();

            // panelTop
            this.panelTop.BackColor = System.Drawing.Color.FromArgb(31, 31, 46);
            this.panelTop.Controls.Add(this.lblSubtitle);
            this.panelTop.Controls.Add(this.lblTitle);
            this.panelTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelTop.Name = "panelTop";
            this.panelTop.Size = new System.Drawing.Size(1150, 65);
            this.panelTop.TabIndex = 0;

            // lblTitle
            this.lblTitle.Font      = new System.Drawing.Font("Segoe UI", 16F, System.Drawing.FontStyle.Bold);
            this.lblTitle.ForeColor = System.Drawing.Color.White;
            this.lblTitle.Location  = new System.Drawing.Point(15, 10);
            this.lblTitle.Name      = "lblTitle";
            this.lblTitle.Size      = new System.Drawing.Size(300, 30);
            this.lblTitle.TabIndex  = 0;
            this.lblTitle.Text      = "🧾  Orders";

            // lblSubtitle
            this.lblSubtitle.Font      = new System.Drawing.Font("Segoe UI", 9F);
            this.lblSubtitle.ForeColor = System.Drawing.Color.FromArgb(180, 180, 210);
            this.lblSubtitle.Location  = new System.Drawing.Point(18, 42);
            this.lblSubtitle.Name      = "lblSubtitle";
            this.lblSubtitle.Size      = new System.Drawing.Size(400, 18);
            this.lblSubtitle.TabIndex  = 1;
            this.lblSubtitle.Text      = "Manage and track customer orders";

            // panelSearch
            this.panelSearch.BackColor = System.Drawing.Color.FromArgb(245, 245, 245);
            this.panelSearch.Controls.Add(this.cmbStatus);
            this.panelSearch.Controls.Add(this.txtSearch);
            this.panelSearch.Dock     = System.Windows.Forms.DockStyle.Top;
            this.panelSearch.Location = new System.Drawing.Point(0, 65);
            this.panelSearch.Name     = "panelSearch";
            this.panelSearch.Size     = new System.Drawing.Size(1150, 45);
            this.panelSearch.TabIndex = 1;

            // txtSearch
            this.txtSearch.Font     = new System.Drawing.Font("Segoe UI", 9.5F);
            this.txtSearch.Location = new System.Drawing.Point(10, 10);
            this.txtSearch.Name     = "txtSearch";
            this.txtSearch.Size     = new System.Drawing.Size(280, 24);
            this.txtSearch.TabIndex = 0;
            this.txtSearch.TextChanged += new System.EventHandler(this.txtSearch_TextChanged);

            // cmbStatus
            this.cmbStatus.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbStatus.Font          = new System.Drawing.Font("Segoe UI", 9.5F);
            this.cmbStatus.Items.AddRange(new object[] { "All Statuses","Pending","Preparing","Completed","Cancelled" });
            this.cmbStatus.Location      = new System.Drawing.Point(300, 10);
            this.cmbStatus.Name          = "cmbStatus";
            this.cmbStatus.Size          = new System.Drawing.Size(140, 25);
            this.cmbStatus.TabIndex      = 1;
            this.cmbStatus.SelectedIndexChanged += new System.EventHandler(this.cmbStatus_SelectedIndexChanged);

            // panelToolbar
            this.panelToolbar.BackColor   = System.Drawing.Color.White;
            this.panelToolbar.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panelToolbar.Controls.Add(this.btnAdd);
            this.panelToolbar.Controls.Add(this.btnEdit);
            this.panelToolbar.Controls.Add(this.btnView);
            this.panelToolbar.Controls.Add(this.btnDelete);
            this.panelToolbar.Controls.Add(this.btnRefresh);
            this.panelToolbar.Dock     = System.Windows.Forms.DockStyle.Top;
            this.panelToolbar.Location = new System.Drawing.Point(0, 110);
            this.panelToolbar.Name     = "panelToolbar";
            this.panelToolbar.Size     = new System.Drawing.Size(1150, 46);
            this.panelToolbar.TabIndex = 2;

            // btnAdd
            this.btnAdd.BackColor = System.Drawing.Color.FromArgb(46, 204, 113);
            this.btnAdd.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAdd.FlatAppearance.BorderSize = 0;
            this.btnAdd.Font      = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.btnAdd.ForeColor = System.Drawing.Color.White;
            this.btnAdd.Location  = new System.Drawing.Point(8, 7);
            this.btnAdd.Name      = "btnAdd";
            this.btnAdd.Size      = new System.Drawing.Size(100, 30);
            this.btnAdd.TabIndex  = 0;
            this.btnAdd.Text      = "➕ Add";
            this.btnAdd.UseVisualStyleBackColor = false;
            this.btnAdd.Click    += new System.EventHandler(this.btnAdd_Click);

            // btnEdit
            this.btnEdit.BackColor = System.Drawing.Color.FromArgb(52, 152, 219);
            this.btnEdit.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnEdit.FlatAppearance.BorderSize = 0;
            this.btnEdit.Font      = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.btnEdit.ForeColor = System.Drawing.Color.White;
            this.btnEdit.Location  = new System.Drawing.Point(116, 7);
            this.btnEdit.Name      = "btnEdit";
            this.btnEdit.Size      = new System.Drawing.Size(100, 30);
            this.btnEdit.TabIndex  = 1;
            this.btnEdit.Text      = "✏️ Edit";
            this.btnEdit.UseVisualStyleBackColor = false;
            this.btnEdit.Click    += new System.EventHandler(this.btnEdit_Click);

            // btnView
            this.btnView.BackColor = System.Drawing.Color.FromArgb(155, 89, 182);
            this.btnView.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnView.FlatAppearance.BorderSize = 0;
            this.btnView.Font      = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.btnView.ForeColor = System.Drawing.Color.White;
            this.btnView.Location  = new System.Drawing.Point(224, 7);
            this.btnView.Name      = "btnView";
            this.btnView.Size      = new System.Drawing.Size(100, 30);
            this.btnView.TabIndex  = 2;
            this.btnView.Text      = "👁 View";
            this.btnView.UseVisualStyleBackColor = false;
            this.btnView.Click    += new System.EventHandler(this.btnView_Click);

            // btnDelete
            this.btnDelete.BackColor = System.Drawing.Color.FromArgb(231, 76, 60);
            this.btnDelete.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnDelete.FlatAppearance.BorderSize = 0;
            this.btnDelete.Font      = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.btnDelete.ForeColor = System.Drawing.Color.White;
            this.btnDelete.Location  = new System.Drawing.Point(332, 7);
            this.btnDelete.Name      = "btnDelete";
            this.btnDelete.Size      = new System.Drawing.Size(100, 30);
            this.btnDelete.TabIndex  = 3;
            this.btnDelete.Text      = "🗑 Delete";
            this.btnDelete.UseVisualStyleBackColor = false;
            this.btnDelete.Click    += new System.EventHandler(this.btnDelete_Click);

            // btnRefresh
            this.btnRefresh.BackColor = System.Drawing.Color.FromArgb(100, 100, 120);
            this.btnRefresh.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnRefresh.FlatAppearance.BorderSize = 0;
            this.btnRefresh.Font      = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.btnRefresh.ForeColor = System.Drawing.Color.White;
            this.btnRefresh.Location  = new System.Drawing.Point(440, 7);
            this.btnRefresh.Name      = "btnRefresh";
            this.btnRefresh.Size      = new System.Drawing.Size(100, 30);
            this.btnRefresh.TabIndex  = 4;
            this.btnRefresh.Text      = "🔄 Refresh";
            this.btnRefresh.UseVisualStyleBackColor = false;
            this.btnRefresh.Click    += new System.EventHandler(this.btnRefresh_Click);

            // gridOrders
            this.gridOrders.AllowUserToAddRows    = false;
            this.gridOrders.AllowUserToDeleteRows = false;
            this.gridOrders.AutoSizeColumnsMode   = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.gridOrders.BackgroundColor       = System.Drawing.Color.White;
            this.gridOrders.BorderStyle           = System.Windows.Forms.BorderStyle.None;
            this.gridOrders.ColumnHeadersDefaultCellStyle.BackColor = System.Drawing.Color.FromArgb(31, 31, 46);
            this.gridOrders.ColumnHeadersDefaultCellStyle.ForeColor = System.Drawing.Color.White;
            this.gridOrders.ColumnHeadersDefaultCellStyle.Font      = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.gridOrders.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gridOrders.Font             = new System.Drawing.Font("Segoe UI", 9F);
            this.gridOrders.Location         = new System.Drawing.Point(0, 156);
            this.gridOrders.Name             = "gridOrders";
            this.gridOrders.ReadOnly         = true;
            this.gridOrders.RowHeadersVisible = false;
            this.gridOrders.SelectionMode    = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.gridOrders.Size             = new System.Drawing.Size(1150, 500);
            this.gridOrders.TabIndex         = 3;
            this.gridOrders.AlternatingRowsDefaultCellStyle.BackColor = System.Drawing.Color.FromArgb(240, 242, 250);
            this.gridOrders.CellDoubleClick        += new System.Windows.Forms.DataGridViewCellEventHandler(this.gridOrders_CellDoubleClick);
            this.gridOrders.ColumnHeaderMouseClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.gridOrders_ColumnHeaderMouseClick);
            this.gridOrders.DataBindingComplete    += new System.Windows.Forms.DataGridViewBindingCompleteEventHandler(this.gridOrders_DataBindingComplete);

            // lblCount
            this.lblCount.Font      = new System.Drawing.Font("Segoe UI", 8.5F);
            this.lblCount.ForeColor = System.Drawing.Color.Gray;
            this.lblCount.Location  = new System.Drawing.Point(10, 660);
            this.lblCount.Name      = "lblCount";
            this.lblCount.Size      = new System.Drawing.Size(300, 20);
            this.lblCount.TabIndex  = 4;
            this.lblCount.Text      = "Showing 0 order(s)";

            // OrdersControl
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode       = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor           = System.Drawing.Color.FromArgb(245, 245, 245);
            this.Controls.Add(this.lblCount);
            this.Controls.Add(this.gridOrders);
            this.Controls.Add(this.panelToolbar);
            this.Controls.Add(this.panelSearch);
            this.Controls.Add(this.panelTop);
            this.Name = "OrdersControl";
            this.Size = new System.Drawing.Size(1150, 690);

            this.panelTop.ResumeLayout(false);
            this.panelSearch.ResumeLayout(false);
            this.panelSearch.PerformLayout();
            this.panelToolbar.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gridOrders)).EndInit();
            this.ResumeLayout(false);
        }
        #endregion

        private System.Windows.Forms.Panel          panelTop;
        private System.Windows.Forms.Label          lblTitle;
        private System.Windows.Forms.Label          lblSubtitle;
        private System.Windows.Forms.Panel          panelSearch;
        private System.Windows.Forms.TextBox        txtSearch;
        private System.Windows.Forms.ComboBox       cmbStatus;
        private System.Windows.Forms.Panel          panelToolbar;
        private System.Windows.Forms.Button         btnAdd;
        private System.Windows.Forms.Button         btnEdit;
        private System.Windows.Forms.Button         btnView;
        private System.Windows.Forms.Button         btnDelete;
        private System.Windows.Forms.Button         btnRefresh;
        private System.Windows.Forms.DataGridView   gridOrders;
        private System.Windows.Forms.Label          lblCount;
    }
}
