using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using App.Core.Models;
using App.Core.Services;

namespace App.WindowsApp.UserControls
{
    public partial class CustomersControl : UserControl
    {
        private readonly CustomerService _svc;
        private readonly Action<string>  _updateStatus;
        private BindingSource            _bindingSource;
        private List<Customer>           _allCustomers = new List<Customer>();

        public CustomersControl(Action<string> updateStatus)
        {
            InitializeComponent();
            _svc           = new CustomerService();
            _updateStatus  = updateStatus;
            _bindingSource = new BindingSource();
            gridCustomers.DataSource = _bindingSource;
            LoadAsync();
        }

        private async void LoadAsync()
        {
            _updateStatus("Loading customers...");
            btnAdd.Enabled = btnEdit.Enabled = btnView.Enabled =
            btnDelete.Enabled = btnRefresh.Enabled = false;

            _allCustomers = await _svc.GetAllAsync();
            _bindingSource.DataSource = _allCustomers;
            HideCol("CreatedAt");
            lblCount.Text = $"Showing {_allCustomers.Count} customer(s)";

            btnAdd.Enabled = btnEdit.Enabled = btnView.Enabled =
            btnDelete.Enabled = btnRefresh.Enabled = true;
            _updateStatus($"Customers loaded — {_allCustomers.Count} records");
        }

        private void HideCol(string name)
        { if (gridCustomers.Columns[name] != null) gridCustomers.Columns[name].Visible = false; }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            var kw = txtSearch.Text.Trim().ToLower();
            var results = string.IsNullOrEmpty(kw)
                ? _allCustomers
                : _allCustomers.Where(c =>
                    c.FullName.ToLower().Contains(kw) ||
                    c.Phone.ToLower().Contains(kw) ||
                    (c.Email ?? "").ToLower().Contains(kw)).ToList();

            _bindingSource.DataSource = results;
            HideCol("CreatedAt");
            lblCount.Text = $"Showing {results.Count} customer(s)";
        }

        private void btnRefresh_Click(object sender, EventArgs e) => LoadAsync();

        private void gridCustomers_CellDoubleClick(object sender, DataGridViewCellEventArgs e) => OnView();

        private void gridCustomers_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            gridCustomers.Sort(gridCustomers.Columns[e.ColumnIndex],
                gridCustomers.SortOrder == SortOrder.Ascending
                    ? System.ComponentModel.ListSortDirection.Descending
                    : System.ComponentModel.ListSortDirection.Ascending);
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            using (var f = new Forms.CustomerForm(FormMode.Add, null))
                if (f.ShowDialog() == DialogResult.OK) { LoadAsync(); _updateStatus("Customer added."); }
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            var c = GetSelected(); if (c == null) return;
            using (var f = new Forms.CustomerForm(FormMode.Edit, c))
                if (f.ShowDialog() == DialogResult.OK) { LoadAsync(); _updateStatus($"Customer '{c.FullName}' updated."); }
        }

        private void btnView_Click(object sender, EventArgs e) => OnView();

        private void OnView()
        {
            var c = GetSelected(); if (c == null) return;
            using (var f = new Forms.CustomerForm(FormMode.View, c)) f.ShowDialog();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            var c = GetSelected(); if (c == null) return;
            if (MessageBox.Show($"Delete '{c.FullName}'?", "Confirm Delete",
                MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
            {
                if (_svc.Delete(c.CustomerId)) { LoadAsync(); _updateStatus($"Deleted '{c.FullName}'."); }
                else MessageBox.Show("Delete failed.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private Customer GetSelected()
        {
            if (_bindingSource.Current == null)
            { MessageBox.Show("Please select a customer.", "No Selection", MessageBoxButtons.OK, MessageBoxIcon.Information); return null; }
            return _bindingSource.Current as Customer;
        }
    }
}
