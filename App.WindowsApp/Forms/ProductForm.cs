using System;
using System.Windows.Forms;
using App.Core.Models;
using App.Core.Services;

namespace App.WindowsApp.Forms
{
    public partial class ProductForm : Form
    {
        private readonly FormMode       _mode;
        private readonly Product        _product;
        private readonly ProductService _svc = new ProductService();

        public ProductForm(FormMode mode, Product product)
        {
            InitializeComponent();
            _mode    = mode;
            _product = product ?? new Product
            {
                ProductId = "PRD-" + DateTime.Now.ToString("yyMMddHHmmss"),
                IsAvailable = true,
                CreatedAt   = DateTime.Now
            };

            SetTitle();
            PopulateCategories();
            PopulateFields();
            ApplyMode();
        }

        // ── Setup helpers ─────────────────────────────────────────────────────
        private void SetTitle()
        {
            switch (_mode)
            {
                case FormMode.Add:  this.Text = "Add Product";  break;
                case FormMode.Edit: this.Text = "Edit Product"; break;
                case FormMode.View: this.Text = "View Product"; break;
            }
        }

        private void PopulateCategories()
        {
            cmbCategory.Items.Clear();
            cmbCategory.Items.AddRange(new object[]
                { "Coffee", "Tea", "Bakery", "Food", "Beverages", "Other" });
        }

        private void PopulateFields()
        {
            txtProductId.Text  = _product.ProductId;
            txtName.Text       = _product.Name;
            txtPrice.Text      = _product.Price.ToString("N2");
            txtStock.Text      = _product.StockQty.ToString();
            txtDescription.Text = _product.Description;
            chkAvailable.Checked = _product.IsAvailable;

            int idx = cmbCategory.Items.IndexOf(_product.Category);
            cmbCategory.SelectedIndex = idx >= 0 ? idx : 0;
        }

        private void ApplyMode()
        {
            bool editable = (_mode != FormMode.View);

            txtProductId.ReadOnly    = true;        // always readonly
            txtName.ReadOnly         = !editable;
            txtPrice.ReadOnly        = !editable;
            txtStock.ReadOnly        = !editable;
            txtDescription.ReadOnly  = !editable;
            cmbCategory.Enabled      = editable;
            chkAvailable.Enabled     = editable;

            btnSave.Text = (_mode == FormMode.View) ? "Close" : "Save";
        }

        // ── Button events ─────────────────────────────────────────────────────
        private void btnSave_Click(object sender, EventArgs e)
        {
            if (_mode == FormMode.View)
            {
                this.DialogResult = DialogResult.Cancel;
                this.Close();
                return;
            }

            // Validation
            if (string.IsNullOrWhiteSpace(txtName.Text))
            { ShowError("Product Name is required."); return; }

            if (!decimal.TryParse(txtPrice.Text, out decimal price) || price < 0)
            { ShowError("Enter a valid Price (number >= 0)."); return; }

            if (!int.TryParse(txtStock.Text, out int stock) || stock < 0)
            { ShowError("Enter a valid Stock Quantity (whole number >= 0)."); return; }

            // Save to model
            _product.Name        = txtName.Text.Trim();
            _product.Category    = cmbCategory.SelectedItem?.ToString() ?? "Other";
            _product.Price       = price;
            _product.StockQty   = stock;
            _product.Description = txtDescription.Text.Trim();
            _product.IsAvailable = chkAvailable.Checked;

            bool ok = (_mode == FormMode.Add) ? _svc.Add(_product) : _svc.Update(_product);

            if (ok)
            {
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            else
            {
                ShowError("Database operation failed. Please try again.");
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void ShowError(string msg) =>
            MessageBox.Show(msg, "Validation Error",
                            MessageBoxButtons.OK, MessageBoxIcon.Warning);
    }
}
