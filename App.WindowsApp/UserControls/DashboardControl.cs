using System;
using System.Collections.Generic;
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
    public partial class DashboardControl : UserControl
    {
        private readonly OrderService    _orderSvc   = new OrderService();
        private readonly CustomerService _custSvc    = new CustomerService();
        private readonly Action<string>  _updateStatus;

        private CartesianChart _barChart;
        private PieChart       _pieChart;

        public DashboardControl(Action<string> updateStatus)
        {
            InitializeComponent();
            _updateStatus = updateStatus;

            // Charts ko panels mein add karo runtime pe
            _barChart = new CartesianChart { Dock = DockStyle.Fill };
            _pieChart = new PieChart       { Dock = DockStyle.Fill };
            panelBarChart.Controls.Add(_barChart);
            panelPieChart.Controls.Add(_pieChart);

            LoadData();
        }

        public void LoadData()
        {
            try
            {
                _updateStatus("Loading dashboard...");

                lblTotalOrdersVal.Text = _orderSvc.GetTotalOrders().ToString();
                lblRevenueVal.Text     = "Rs. " + _orderSvc.GetTotalRevenue().ToString("N0");
                lblPendingVal.Text     = _orderSvc.GetPendingOrdersCount().ToString();
                lblCustomersVal.Text   = _custSvc.GetAll().Count.ToString();

                // Bar chart
                var daily     = _orderSvc.GetDailyRevenue(7);
                var barValues = daily.Values.Select(v => (double)v).ToArray();
                var barLabels = daily.Keys.ToArray();

                _barChart.Series = new ISeries[]
                {
                    new ColumnSeries<double>
                    {
                        Name   = "Revenue (Rs.)",
                        Values = barValues,
                        Fill   = new SolidColorPaint(SKColor.Parse("#3498db"))
                    }
                };
                _barChart.XAxes = new[] { new Axis { Labels = barLabels, LabelsRotation = 15, TextSize = 10 } };
                _barChart.YAxes = new[] { new Axis { Labeler = v => $"Rs.{v:N0}", TextSize = 9 } };

                // Pie chart
                var catSales = _orderSvc.GetSalesByCategory();
                var palette  = new[] { "#e74c3c","#3498db","#2ecc71","#9b59b6","#f39c12","#1abc9c" };
                int i = 0;
                _pieChart.Series = catSales.Select(kv => (ISeries)new PieSeries<double>
                {
                    Name   = kv.Key,
                    Values = new double[] { (double)kv.Value },
                    Fill   = new SolidColorPaint(SKColor.Parse(palette[i++ % palette.Length]))
                }).ToArray();

                _updateStatus($"Dashboard refreshed — {DateTime.Now:hh:mm tt}");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading dashboard:\n{ex.Message}", "Error",
                                MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnRefresh_Click(object sender, EventArgs e) => LoadData();
    }
}
