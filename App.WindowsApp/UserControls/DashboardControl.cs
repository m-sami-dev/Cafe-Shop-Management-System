using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using App.Core.Services;
using LiveChartsCore;
using LiveChartsCore.SkiaSharpView;
using LiveChartsCore.SkiaSharpView.WinForms;
using LiveChartsCore.SkiaSharpView.Painting;
using SkiaSharp;

namespace App.WindowsApp.UserControls
{
    public class DashboardControl : UserControl
    {
        private readonly OrderService   _orderSvc   = new OrderService();
        private readonly ProductService _productSvc = new ProductService();
        private readonly CustomerService _custSvc   = new CustomerService();
        private readonly Action<string> _updateStatus;

        public DashboardControl(Action<string> updateStatus)
        {
            _updateStatus = updateStatus;
            InitializeLayout();
            LoadData();
        }

        private Label _lblTotalOrders;
        private Label _lblRevenue;
        private Label _lblPending;
        private Label _lblCustomers;
        private CartesianChart _barChart;
        private PieChart _pieChart;

        private void InitializeLayout()
        {
            Dock      = DockStyle.Fill;
            BackColor = Color.FromArgb(245, 245, 245);

            // ── Page title ────────────────────────────────────────────────────
            var lblTitle = new Label
            {
                Text     = "Dashboard",
                Font     = new Font("Segoe UI", 18f, FontStyle.Bold),
                Location = new Point(10, 10),
                Size     = new Size(400, 40),
                ForeColor = Color.FromArgb(31, 31, 46)
            };
            Controls.Add(lblTitle);

            var lblSub = new Label
            {
                Text     = "Overview of your cafe performance",
                Font     = new Font("Segoe UI", 9f),
                Location = new Point(12, 50),
                Size     = new Size(400, 20),
                ForeColor = Color.Gray
            };
            Controls.Add(lblSub);

            // ── Stat cards ────────────────────────────────────────────────────
            int cardY = 85;
            _lblTotalOrders = AddStatCard("Total Orders",    "0",    Color.FromArgb(52, 152, 219),  10,  cardY);
            _lblRevenue     = AddStatCard("Total Revenue",   "Rs. 0",Color.FromArgb(46, 204, 113),  220, cardY);
            _lblPending     = AddStatCard("Pending Orders",  "0",    Color.FromArgb(231, 76, 60),   430, cardY);
            _lblCustomers   = AddStatCard("Customers",       "0",    Color.FromArgb(155, 89, 182),  640, cardY);

            // ── Charts ────────────────────────────────────────────────────────
            var lblBar = new Label
            {
                Text     = "📊  Revenue - Last 7 Days (Bar Chart)",
                Font     = new Font("Segoe UI", 10f, FontStyle.Bold),
                Location = new Point(10, cardY + 130),
                Size     = new Size(350, 25),
                ForeColor = Color.FromArgb(31, 31, 46)
            };
            Controls.Add(lblBar);

            _barChart = new CartesianChart
            {
                Location = new Point(10, cardY + 160),
                Size     = new Size(560, 240),
                BackColor = Color.White
            };
            Controls.Add(_barChart);

            var lblPie = new Label
            {
                Text     = "🥧  Sales by Category (Pie Chart)",
                Font     = new Font("Segoe UI", 10f, FontStyle.Bold),
                Location = new Point(590, cardY + 130),
                Size     = new Size(350, 25),
                ForeColor = Color.FromArgb(31, 31, 46)
            };
            Controls.Add(lblPie);

            _pieChart = new PieChart
            {
                Location = new Point(590, cardY + 160),
                Size     = new Size(540, 240),
                BackColor = Color.White
            };
            Controls.Add(_pieChart);
        }

        private Label AddStatCard(string title, string value, Color color, int x, int y)
        {
            var card = new Panel
            {
                Location  = new Point(x, y),
                Size      = new Size(195, 110),
                BackColor = color
            };

            var lblT = new Label
            {
                Text      = title,
                Font      = new Font("Segoe UI", 8.5f),
                ForeColor = Color.FromArgb(220, 220, 220),
                Location  = new Point(12, 12),
                Size      = new Size(170, 20)
            };

            var lblV = new Label
            {
                Text      = value,
                Font      = new Font("Segoe UI", 18f, FontStyle.Bold),
                ForeColor = Color.White,
                Location  = new Point(10, 38),
                Size      = new Size(175, 40),
                AutoSize  = false
            };

            card.Controls.Add(lblT);
            card.Controls.Add(lblV);
            Controls.Add(card);
            return lblV;
        }

        public void LoadData()
        {
            try
            {
                _updateStatus("Loading dashboard data...");

                // Stats
                _lblTotalOrders.Text = _orderSvc.GetTotalOrders().ToString();
                _lblRevenue.Text     = $"Rs. {_orderSvc.GetTotalRevenue():N0}";
                _lblPending.Text     = _orderSvc.GetPendingOrdersCount().ToString();
                _lblCustomers.Text   = _custSvc.GetAll().Count.ToString();

                // Bar chart — daily revenue
                var daily = _orderSvc.GetDailyRevenue(7);
                var barValues  = daily.Values.Select(v => (double)v).ToArray();
                var barLabels  = daily.Keys.ToArray();

                _barChart.Series = new ISeries[]
                {
                    new ColumnSeries<double>
                    {
                        Name   = "Revenue (Rs.)",
                        Values = barValues,
                        Fill   = new SolidColorPaint(SKColor.Parse("#3498db"))
                    }
                };
                _barChart.XAxes = new[]
                {
                    new Axis { Labels = barLabels, LabelsRotation = 15,
                               TextSize = 10 }
                };
                _barChart.YAxes = new[]
                {
                    new Axis { Labeler = v => $"Rs.{v:N0}", TextSize = 9 }
                };

                // Pie chart — sales by category
                var catSales = _orderSvc.GetSalesByCategory();
                var palette  = new[] { "#e74c3c","#3498db","#2ecc71","#9b59b6","#f39c12","#1abc9c" };
                int i = 0;
                var pieSeries = catSales.Select(kv => (ISeries)new PieSeries<double>
                {
                    Name   = kv.Key,
                    Values = new double[] { (double)kv.Value },
                    Fill   = new SolidColorPaint(SKColor.Parse(palette[i++ % palette.Length]))
                }).ToArray();

                _pieChart.Series = pieSeries;

                _updateStatus($"Dashboard refreshed — {DateTime.Now:hh:mm tt}");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading dashboard:\n{ex.Message}", "Error",
                                MessageBoxButtons.OK, MessageBoxIcon.Error);
                _updateStatus("Dashboard load error.");
            }
        }
    }
}
