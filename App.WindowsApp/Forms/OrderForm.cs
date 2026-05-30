using System;
using System.Collections.Generic;
using System.Windows.Forms;
using App.Core.Models;
using App.Core.Services;

namespace App.WindowsApp.Forms
{
    public partial class OrderForm : Form
    {
        private readonly FormMode       _mode;
        private readonly Order          _order;
        private readonly OrderService   _orderSvc  = new OrderService();
        private readonly CustomerService _custSvc  = new CustomerService();
        private readonly ProductService  _prodSvc  = new ProductService();

        private List<OrderItem> _items = new List<OrderItem>();
        private readonly BindingSource _itemsSource = new BindingSource();

        public OrderForm(FormMode mode, Order order)
        {
            InitializeComponent();
            _mode  = mode;
            _order = order ?? new Order
            {
                OrderId   = "ORD-" + DateTime.Now.ToString("yyMMddHHmmss"),
                OrderDate = DateTime.Now,
                Status    = "Pending"
            };

            SetTitle();
            LoadCustomers();
            LoadProducts();
            LoadExistingItems();
            PopulateFields();
            ApplyMode();
        }

        // ── Setup ─────────────────────────────────────────────────────────────
        private void SetTitle()
        {
            switch (_mode)
            {
                case FormMode.Add:  Text = "New Order";   lblFormTitle.Text = "🧾  New Order";   break;
                case FormMode.Edit: Text = "Edit Order";  lblFormTitle.Text = "🧾  Edit Order";  break;
                case FormMode.View: Text = "View Order";  lblFormTitle.Text = "🧾  View Order";  break;
            }
        }

        private void LoadCustomers()
        {
            cmbCustomer.Items.Clear();
            foreach (var c in _custSvc.GetAll())
                cmbCustomer.Items.Add(c);
            cmbCustomer.DisplayMember = "FullName";
        }

        private void LoadProducts()
        {
            cmbProduct.Items.Clear();
            foreach (var p in _prodSvc.GetAll())
                if (p.IsAvailable) cmbProduct.Items.Add(p);
            cmbProduct.DisplayMember = "Name";
            if (cmbProduct.Items.Count > 0) cmbProduct.SelectedIndex = 0;
        }

        private void LoadExistingItems()
        {
            if (_mode != FormMode.Add && !string.IsNullOrEmpty(_order.OrderId))
            {
                var full = _orderSvc.GetById(_order.OrderId);
                if (full != null) _items = new List<OrderItem>(full.Items);
            }
        }

        private void PopulateFields()
        {
            txtOrderId.Text = _order.OrderId;

            // Select customer in combo
            for (int i = 0; i < cmbCustomer.Items.Count; i++)
            {
                if (((Customer)cmbCustomer.Items[i]).CustomerId == _order.CustomerId)
                { cmbCustomer.SelectedIndex = i; break; }
            }
            if (cmbCustomer.SelectedIndex < 0 && cmbCustomer.Items.Count > 0)
                cmbCustomer.SelectedIndex = 0;

            // Status
            int si = cmbStatus.Items.IndexOf(_order.Status);
            cmbStatus.SelectedIndex = si >= 0 ? si : 0;

            txtNotes.Text = _order.Notes;

            // Bind items grid
            gridItems.DataSource = _itemsSource;
            RefreshItemsGrid();
        }

        private void ApplyMode()
        {
            bool ed = (_mode != FormMode.View);
            cmbCustomer.Enabled = ed;
            cmbStatus.Enabled   = ed;
            txtNotes.ReadOnly   = !ed;
            panelAddItem.Visible = ed;
            btnRemoveItem.Visible = ed;
            btnSave.Text = (_mode == FormMode.View) ? "Close" : "Save Order";
        }

        private void RefreshItemsGrid()
        {
            _itemsSource.DataSource = null;
            _itemsSource.DataSource = new System.ComponentModel.BindingList<OrderItem>(_items);

            // Hide internal ID columns
            foreach (string col in new[] { "ItemId", "OrderId", "ProductId" })
                if (gridItems.Columns[col] != null)
                    gridItems.Columns[col].Visible = false;

            // Update total label
            decimal total = 0;
            foreach (var it in _items) total += it.SubTotal;
            lblTotal.Text = "Total:  Rs. " + total.ToString("N2");
        }

        // ── Events ────────────────────────────────────────────────────────────
        private void btnAddItem_Click(object sender, EventArgs e)
        {
            if (cmbProduct.SelectedItem == null) return;
            var p = (Product)cmbProduct.SelectedItem;

            var item = new OrderItem
            {
                ItemId      = "ITM-" + DateTime.Now.ToString("yyMMddHHmmssff"),
                OrderId     = _order.OrderId,
                ProductId   = p.ProductId,
                ProductName = p.Name,
                Quantity    = (int)numQty.Value,
                UnitPrice   = p.Price
            };
            _items.Add(item);
            RefreshItemsGrid();
        }

        private void btnRemoveItem_Click(object sender, EventArgs e)
        {
            if (_itemsSource.Current is OrderItem item)
            {
                _items.Remove(item);
                RefreshItemsGrid();
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (_mode == FormMode.View) { DialogResult = DialogResult.Cancel; Close(); return; }

            if (cmbCustomer.SelectedItem == null) { ShowError("Please select a customer."); return; }
            if (_items.Count == 0) { ShowError("Add at least one item to the order."); return; }

            var customer        = (Customer)cmbCustomer.SelectedItem;
            _order.CustomerId   = customer.CustomerId;
            _order.CustomerName = customer.FullName;
            _order.Status       = cmbStatus.SelectedItem?.ToString() ?? "Pending";
            _order.Notes        = txtNotes.Text.Trim();
            _order.Items        = _items;

            decimal total = 0;
            foreach (var it in _items) total += it.SubTotal;
            _order.TotalAmount = total;

            bool ok = (_mode == FormMode.Add) ? _orderSvc.Add(_order) : _orderSvc.Update(_order);
            if (ok) { DialogResult = DialogResult.OK; Close(); }
            else ShowError("Database operation failed. Please try again.");
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
