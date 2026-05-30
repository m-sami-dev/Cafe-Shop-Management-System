using System;
using System.Windows.Forms;
using App.Core.Models;
using App.Core.Services;

namespace App.WindowsApp.Forms
{
    public partial class CustomerForm : Form
    {
        private readonly FormMode        _mode;
        private readonly Customer        _customer;
        private readonly CustomerService _svc = new CustomerService();

        public CustomerForm(FormMode mode, Customer customer)
        {
            InitializeComponent();
            _mode     = mode;
            _customer = customer ?? new Customer
            {
                CustomerId = "CUS-" + DateTime.Now.ToString("yyMMddHHmmss"),
                CreatedAt  = DateTime.Now
            };

            SetTitle();
            PopulateFields();
            ApplyMode();
        }

        private void SetTitle()
        {
            switch (_mode)
            {
                case FormMode.Add:  this.Text = "Add Customer";  lblFormTitle.Text = "👥  Add Customer";  break;
                case FormMode.Edit: this.Text = "Edit Customer"; lblFormTitle.Text = "👥  Edit Customer"; break;
                case FormMode.View: this.Text = "View Customer"; lblFormTitle.Text = "👥  View Customer"; break;
            }
        }

        private void PopulateFields()
        {
            txtCustomerId.Text  = _customer.CustomerId;
            txtFullName.Text    = _customer.FullName;
            txtPhone.Text       = _customer.Phone;
            txtEmail.Text       = _customer.Email;
            txtAddress.Text     = _customer.Address;
            numLoyalty.Value    = _customer.LoyaltyPts;
        }

        private void ApplyMode()
        {
            bool editable = (_mode != FormMode.View);
            txtCustomerId.ReadOnly = true;
            txtFullName.ReadOnly   = !editable;
            txtPhone.ReadOnly      = !editable;
            txtEmail.ReadOnly      = !editable;
            txtAddress.ReadOnly    = !editable;
            numLoyalty.Enabled     = editable;
            btnSave.Text           = (_mode == FormMode.View) ? "Close" : "Save";
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (_mode == FormMode.View) { DialogResult = DialogResult.Cancel; Close(); return; }

            if (string.IsNullOrWhiteSpace(txtFullName.Text))
            { ShowError("Full Name is required."); return; }

            if (string.IsNullOrWhiteSpace(txtPhone.Text))
            { ShowError("Phone number is required."); return; }

            _customer.FullName   = txtFullName.Text.Trim();
            _customer.Phone      = txtPhone.Text.Trim();
            _customer.Email      = txtEmail.Text.Trim();
            _customer.Address    = txtAddress.Text.Trim();
            _customer.LoyaltyPts = (int)numLoyalty.Value;

            bool ok = (_mode == FormMode.Add) ? _svc.Add(_customer) : _svc.Update(_customer);
            if (ok) { DialogResult = DialogResult.OK; Close(); }
            else ShowError("Database operation failed.");
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }

        private void ShowError(string msg) =>
            MessageBox.Show(msg, "Validation Error",
                            MessageBoxButtons.OK, MessageBoxIcon.Warning);
    }
}
