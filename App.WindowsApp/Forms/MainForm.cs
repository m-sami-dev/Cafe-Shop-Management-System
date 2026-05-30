using System;
using System.Configuration;
using System.Drawing;
using System.Windows.Forms;
using App.WindowsApp.UserControls;

namespace App.WindowsApp.Forms
{
    public partial class MainForm : Form
    {
        private readonly string _userName;
        private Button _activeBtn;

        public MainForm()
        {
            InitializeComponent();
            _userName = ConfigurationManager.AppSettings["UserName"] ?? "Admin";
            lblUser.Text = "👤 " + _userName;

            // clock
            var timer = new Timer { Interval = 1000 };
            timer.Tick += (s, e) => lblTime.Text = DateTime.Now.ToString("hh:mm:ss tt");
            timer.Start();

            NavigateTo("Dashboard");
            HighlightButton(btnDashboard);
        }

        // ── Navigation ────────────────────────────────────────────────────────
        public void NavigateTo(string view)
        {
            panelContent.Controls.Clear();

            UserControl uc = null;
            switch (view)
            {
                case "Dashboard": uc = new DashboardControl(UpdateStatus);  break;
                case "Products":  uc = new ProductsControl(UpdateStatus);   break;
                case "Customers": uc = new CustomersControl(UpdateStatus);  break;
                case "Orders":    uc = new OrdersControl(UpdateStatus);     break;
            }

            if (uc != null)
            {
                uc.Dock = DockStyle.Fill;
                panelContent.Controls.Add(uc);
            }
        }

        public void UpdateStatus(string msg)
        {
            lblStatus.Text = msg;
        }

        private void HighlightButton(Button btn)
        {
            if (_activeBtn != null)
            {
                _activeBtn.BackColor = Color.Transparent;
                _activeBtn.ForeColor = Color.FromArgb(190, 190, 220);
            }
            btn.BackColor = Color.FromArgb(55, 55, 80);
            btn.ForeColor = Color.White;
            _activeBtn = btn;
        }

        // ── Button click events ───────────────────────────────────────────────
        private void btnDashboard_Click(object sender, EventArgs e)
        {
            NavigateTo("Dashboard");
            HighlightButton(btnDashboard);
        }

        private void btnProducts_Click(object sender, EventArgs e)
        {
            NavigateTo("Products");
            HighlightButton(btnProducts);
        }

        private void btnCustomers_Click(object sender, EventArgs e)
        {
            NavigateTo("Customers");
            HighlightButton(btnCustomers);
        }

        private void btnOrders_Click(object sender, EventArgs e)
        {
            NavigateTo("Orders");
            HighlightButton(btnOrders);
        }
    }
}
