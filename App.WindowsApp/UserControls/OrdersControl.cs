using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using App.Core.Models;
using App.Core.Services;

namespace App.WindowsApp.UserControls
{
    public partial class OrdersControl : UserControl
    {
        private readonly OrderService   _svc;
        private readonly Action<string> _updateStatus;
        private BindingSource           _bindingSource;
        private List<Order>             _allOrders = new List<Order>();
        private bool                    _loading   = false;

        public OrdersControl(Action<string> updateStatus)
        {
            InitializeComponent();
            _svc           = new OrderService();
            _updateStatus  = updateStatus;
            _bindingSource = new BindingSource();
            gridOrders.DataSource = _bindingSource;
            LoadAsync();
        }

        private async void LoadAsync()
        {
            _loading = true;
            _updateStatus("Loading orders...");
            btnAdd.Enabled = btnEdit.Enabled = btnView.Enabled =
            btnDelete.Enabled = btnRefresh.Enabled = false;

            _allOrders = await _svc.GetAllAsync();
            cmbStatus.SelectedIndex = 0;
            _bindingSource.DataSource = _allOrders;
            HideColumns();
            lblCount.Text = $"Showing {_allOrders.Count} order(s)";

            btnAdd.Enabled = btnEdit.Enabled = btnView.Enabled =
            btnDelete.Enabled = btnRefresh.Enabled = true;
            _loading = false;
            _updateStatus($"Orders loaded — {_allOrders.Count} records");
        }

        private void HideColumns()
        {
            foreach (string col in new[] { "Notes", "CustomerId", "Items" })
                if (gridOrders.Columns[col] != null)
                    gridOrders.Columns[col].Visible = false;
            if (gridOrders.Columns["TotalAmount"] != null)
                gridOrders.Columns["TotalAmount"].DefaultCellStyle.Format = "N2";
        }

        private void FilterGrid()
        {
            if (_loading) return;
            var kw     = txtSearch.Text.Trim().ToLower();
            var status = cmbStatus.SelectedItem?.ToString();
            if (status == "All Statuses") status = "";

            var results = _allOrders.Where(o =>
            {
                bool matchKw = string.IsNullOrEmpty(kw)
                            || o.OrderId.ToLower().Contains(kw)
                            || o.CustomerName.ToLower().Contains(kw);
                bool matchStatus = string.IsNullOrEmpty(status)
                                || o.Status == status;
                return matchKw && matchStatus;
            }).ToList();

            _bindingSource.DataSource = results;
            HideColumns();
            ColorRows();
            lblCount.Text = $"Showing {results.Count} order(s)";
        }

        private void ColorRows()
        {
            foreach (DataGridViewRow row in gridOrders.Rows)
            {
                var status = row.Cells["Status"]?.Value?.ToString();
                row.DefaultCellStyle.ForeColor = status switch
                {
                    "Pending"   => Color.DarkOrange,
                    "Preparing" => Color.DodgerBlue,
                    "Completed" => Color.ForestGreen,
                    "Cancelled" => Color.Crimson,
                    _           => Color.Black
                };
            }
        }

        private void txtSearch_TextChanged(object sender, EventArgs e)          => FilterGrid();
        private void cmbStatus_SelectedIndexChanged(object sender, EventArgs e) => FilterGrid();
        private void btnRefresh_Click(object sender, EventArgs e)               => LoadAsync();
        private void gridOrders_CellDoubleClick(object sender, DataGridViewCellEventArgs e) => OnView();
        private void gridOrders_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e) => ColorRows();

        private void gridOrders_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            gridOrders.Sort(gridOrders.Columns[e.ColumnIndex],
                gridOrders.SortOrder == SortOrder.Ascending
                    ? System.ComponentModel.ListSortDirection.Descending
                    : System.ComponentModel.ListSortDirection.Ascending);
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            using (var f = new Forms.OrderForm(FormMode.Add, null))
                if (f.ShowDialog() == DialogResult.OK) { LoadAsync(); _updateStatus("Order added."); }
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            var o = GetSelected(); if (o == null) return;
            using (var f = new Forms.OrderForm(FormMode.Edit, o))
                if (f.ShowDialog() == DialogResult.OK) { LoadAsync(); _updateStatus($"Order '{o.OrderId}' updated."); }
        }

        private void btnView_Click(object sender, EventArgs e) => OnView();

        private void OnView()
        {
            var o = GetSelected(); if (o == null) return;
            using (var f = new Forms.OrderForm(FormMode.View, o)) f.ShowDialog();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            var o = GetSelected(); if (o == null) return;
            if (MessageBox.Show($"Delete order '{o.OrderId}'?", "Confirm Delete",
                MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
            {
                if (_svc.Delete(o.OrderId)) { LoadAsync(); _updateStatus($"Order '{o.OrderId}' deleted."); }
                else MessageBox.Show("Delete failed.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private Order GetSelected()
        {
            if (_bindingSource.Current == null)
            { MessageBox.Show("Please select an order.", "No Selection", MessageBoxButtons.OK, MessageBoxIcon.Information); return null; }
            return _bindingSource.Current as Order;
        }
    }
}
