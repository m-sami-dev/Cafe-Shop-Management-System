using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using App.Core.Models;
using App.Core.Services;

namespace App.WindowsApp.UserControls
{
    public partial class ProductsControl : UserControl
    {
        private readonly ProductService _svc;
        private readonly Action<string> _updateStatus;
        private BindingSource _bindingSource;
        private List<Product> _allProducts = new List<Product>();
        private bool _loading = false;

        public ProductsControl(Action<string> updateStatus)
        {
            InitializeComponent();
            _svc           = new ProductService();
            _updateStatus  = updateStatus;
            _bindingSource = new BindingSource();
            gridProducts.DataSource = _bindingSource;
            LoadAsync();
        }

        // ── Load ─────────────────────────────────────────────────────────────
        private async void LoadAsync()
        {
            _loading = true;
            _updateStatus("Loading products...");
            btnAdd.Enabled = btnEdit.Enabled = btnView.Enabled =
            btnDelete.Enabled = btnRefresh.Enabled = false;

            _allProducts = await _svc.GetAllAsync();

            // Populate category dropdown
            cmbCategory.Items.Clear();
            cmbCategory.Items.Add("All Categories");
            foreach (var cat in _svc.GetCategories())
                cmbCategory.Items.Add(cat);
            cmbCategory.SelectedIndex = 0;

            _bindingSource.DataSource = _allProducts;
            ConfigureColumns();
            lblCount.Text = $"Showing {_allProducts.Count} product(s)";

            btnAdd.Enabled = btnEdit.Enabled = btnView.Enabled =
            btnDelete.Enabled = btnRefresh.Enabled = true;
            _loading = false;
            _updateStatus($"Products loaded — {_allProducts.Count} records");
        }

        private void ConfigureColumns()
        {
            if (gridProducts.Columns.Count == 0) return;
            foreach (string col in new[] { "Description", "CreatedAt" })
                if (gridProducts.Columns[col] != null)
                    gridProducts.Columns[col].Visible = false;
            if (gridProducts.Columns["Price"] != null)
                gridProducts.Columns["Price"].DefaultCellStyle.Format = "N2";
            if (gridProducts.Columns["IsAvailable"] != null)
                gridProducts.Columns["IsAvailable"].HeaderText = "Available";
        }

        // ── Filter ───────────────────────────────────────────────────────────
        private void FilterGrid()
        {
            if (_loading) return;

            var keyword  = txtSearch.Text.Trim().ToLower();
            var category = cmbCategory.SelectedItem?.ToString();
            if (category == "All Categories") category = "";

            var results = _allProducts.Where(p =>
            {
                bool matchKw  = string.IsNullOrEmpty(keyword)
                             || p.Name.ToLower().Contains(keyword)
                             || (p.Description ?? "").ToLower().Contains(keyword);
                bool matchCat = string.IsNullOrEmpty(category)
                             || p.Category == category;
                return matchKw && matchCat;
            }).ToList();

            _bindingSource.DataSource = results;
            ConfigureColumns();
            lblCount.Text = $"Showing {results.Count} product(s)";
        }

        // ── Events ───────────────────────────────────────────────────────────
        private void txtSearch_TextChanged(object sender, EventArgs e)       => FilterGrid();
        private void cmbCategory_SelectedIndexChanged(object sender, EventArgs e) => FilterGrid();
        private void btnRefresh_Click(object sender, EventArgs e)            => LoadAsync();
        private void gridProducts_CellDoubleClick(object sender, DataGridViewCellEventArgs e) => OnView();

        private void gridProducts_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            gridProducts.Sort(gridProducts.Columns[e.ColumnIndex],
                gridProducts.SortOrder == SortOrder.Ascending
                    ? System.ComponentModel.ListSortDirection.Descending
                    : System.ComponentModel.ListSortDirection.Ascending);
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            using (var f = new Forms.ProductForm(FormMode.Add, null))
                if (f.ShowDialog() == DialogResult.OK) { LoadAsync(); _updateStatus("Product added."); }
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            var p = GetSelected(); if (p == null) return;
            using (var f = new Forms.ProductForm(FormMode.Edit, p))
                if (f.ShowDialog() == DialogResult.OK) { LoadAsync(); _updateStatus($"Product '{p.Name}' updated."); }
        }

        private void btnView_Click(object sender, EventArgs e) => OnView();

        private void OnView()
        {
            var p = GetSelected(); if (p == null) return;
            using (var f = new Forms.ProductForm(FormMode.View, p)) f.ShowDialog();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            var p = GetSelected(); if (p == null) return;
            if (MessageBox.Show($"Delete '{p.Name}'?", "Confirm Delete",
                MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
            {
                if (_svc.Delete(p.ProductId)) { LoadAsync(); _updateStatus($"Deleted '{p.Name}'."); }
                else MessageBox.Show("Delete failed.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private Product GetSelected()
        {
            if (_bindingSource.Current == null)
            { MessageBox.Show("Please select a product.", "No Selection", MessageBoxButtons.OK, MessageBoxIcon.Information); return null; }
            return _bindingSource.Current as Product;
        }
    }
}
