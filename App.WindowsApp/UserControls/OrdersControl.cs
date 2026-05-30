using System;
using System.Drawing;
using System.Windows.Forms;
using App.Core.Models;
using App.Core.Services;

namespace App.WindowsApp.UserControls
{
    public class OrdersControl : UserControl
    {
        private readonly OrderService   _svc;
        private readonly Action<string> _updateStatus;
        private BindingSource _bindingSource;
        private DataGridView  _grid;
        private TextBox       _txtSearch;
        private ComboBox      _cmbStatus;
        private Label         _lblCount;
        private Button        _btnAdd, _btnEdit, _btnView, _btnDelete, _btnRefresh;

        public OrdersControl(Action<string> updateStatus)
        {
            _svc           = new OrderService();
            _updateStatus  = updateStatus;
            _bindingSource = new BindingSource();
            InitializeLayout();
            LoadAsync();
        }

        private void InitializeLayout()
        {
            Dock = DockStyle.Fill;
            BackColor = Color.FromArgb(245, 245, 245);

            var lblTitle = new Label
            {
                Text = "🧾  Orders",
                Font = new Font("Segoe UI", 16f, FontStyle.Bold),
                Location = new Point(10, 10), Size = new Size(300, 36),
                ForeColor = Color.FromArgb(31, 31, 46)
            };
            Controls.Add(lblTitle);

            //_txtSearch = new TextBox
            //{
            //    //PlaceholderText = "🔍 Search by order ID or customer...",
            //    txtSearch.Text = "Search here...";
            //    Location = new Point(10, 56), Size = new Size(280, 28),
            //    Font = new Font("Segoe UI", 9.5f)
            //};
            _txtSearch = new TextBox
            {
                Location = new Point(10, 56),
                Size = new Size(280, 28),
                Font = new Font("Segoe UI", 9.5f),
                Text = "🔍 Search by order ID or customer..."
            };

            _txtSearch.TextChanged += (s, e) => FilterGrid();
            Controls.Add(_txtSearch);

            _cmbStatus = new ComboBox
            {
                Location = new Point(300, 56), Size = new Size(130, 28),
                DropDownStyle = ComboBoxStyle.DropDownList,
                Font = new Font("Segoe UI", 9.5f)
            };
            _cmbStatus.Items.AddRange(new[] { "All Statuses","Pending","Preparing","Completed","Cancelled" });
            _cmbStatus.SelectedIndex = 0;
            _cmbStatus.SelectedIndexChanged += (s, e) => FilterGrid();
            Controls.Add(_cmbStatus);

            // Toolbar
            _btnAdd     = Btn("➕ Add",     Color.FromArgb(46,204,113));
            _btnEdit    = Btn("✏️ Edit",    Color.FromArgb(52,152,219));
            _btnView    = Btn("👁 View",    Color.FromArgb(155,89,182));
            _btnDelete  = Btn("🗑 Delete",  Color.FromArgb(231,76,60));
            _btnRefresh = Btn("🔄 Refresh", Color.FromArgb(100,100,120));

            var toolbar = new FlowLayoutPanel { Location = new Point(445, 50), Size = new Size(700, 40) };
            toolbar.Controls.AddRange(new Control[] { _btnAdd, _btnEdit, _btnView, _btnDelete, _btnRefresh });
            Controls.Add(toolbar);

            _btnAdd.Click     += (s, e) => ShowForm(FormMode.Add,  null);
            _btnEdit.Click    += (s, e) => ShowForm(FormMode.Edit,  GetSelected());
            _btnView.Click    += (s, e) => ShowForm(FormMode.View,  GetSelected());
            _btnDelete.Click  += OnDelete;
            _btnRefresh.Click += (s, e) => LoadAsync();

            _grid = new DataGridView
            {
                Location              = new Point(10, 100),
                Size                  = new Size(1130, 520),
                DataSource            = _bindingSource,
                AutoSizeColumnsMode   = DataGridViewAutoSizeColumnsMode.Fill,
                SelectionMode         = DataGridViewSelectionMode.FullRowSelect,
                ReadOnly              = true,
                AllowUserToAddRows    = false,
                AllowUserToDeleteRows = false,
                BackgroundColor       = Color.White,
                BorderStyle           = BorderStyle.None,
                RowHeadersVisible     = false,
                Font                  = new Font("Segoe UI", 9f),
                ColumnHeadersDefaultCellStyle = new DataGridViewCellStyle
                {
                    BackColor = Color.FromArgb(31,31,46),
                    ForeColor = Color.White,
                    Font      = new Font("Segoe UI", 9f, FontStyle.Bold)
                },
                AlternatingRowsDefaultCellStyle = new DataGridViewCellStyle
                {
                    BackColor = Color.FromArgb(240,242,250)
                }
            };
            _grid.ColumnHeaderMouseClick += (s, e) =>
                _grid.Sort(_grid.Columns[e.ColumnIndex],
                    _grid.SortOrder == SortOrder.Ascending
                        ? System.ComponentModel.ListSortDirection.Descending
                        : System.ComponentModel.ListSortDirection.Ascending);
            _grid.CellDoubleClick += (s, e) => ShowForm(FormMode.View, GetSelected());
            _grid.DataBindingComplete += ColorStatusRows;
            Controls.Add(_grid);

            _lblCount = new Label
            {
                Location = new Point(10, 625), Size = new Size(400, 20),
                ForeColor = Color.Gray, Font = new Font("Segoe UI", 8.5f)
            };
            Controls.Add(_lblCount);
        }

        private Button Btn(string text, Color color) => new Button
        {
            Text = text, Size = new Size(105, 32),
            BackColor = color, ForeColor = Color.White,
            FlatStyle = FlatStyle.Flat, Margin = new Padding(0,0,6,0),
            Cursor = Cursors.Hand, FlatAppearance = { BorderSize = 0 }
        };

        private async void LoadAsync()
        {
            _updateStatus("Loading orders...");
            var list = await _svc.GetAllAsync();
            _bindingSource.DataSource = list;
            HideCol("Notes"); HideCol("CustomerId"); HideCol("Items");
            if (_grid.Columns["TotalAmount"] != null)
                _grid.Columns["TotalAmount"].DefaultCellStyle.Format = "N2";
            _lblCount.Text = $"Showing {list.Count} order(s)";
            _updateStatus($"Orders loaded — {list.Count} records");
        }

        private void FilterGrid()
        {
            var kw     = _txtSearch.Text.Trim();
            var status = _cmbStatus.SelectedItem?.ToString();
            if (status == "All Statuses") status = "";

            var list = string.IsNullOrWhiteSpace(kw)
                ? _svc.Search("", status)
                : _svc.Search(kw, status);

            _bindingSource.DataSource = list;
            HideCol("Notes"); HideCol("CustomerId"); HideCol("Items");
            _lblCount.Text = $"Showing {list.Count} order(s)";
        }

        private void HideCol(string name)
        {
            if (_grid.Columns[name] != null) _grid.Columns[name].Visible = false;
        }

        // Color-code rows by status
        private void ColorStatusRows(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            foreach (DataGridViewRow row in _grid.Rows)
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

        private void ShowForm(FormMode mode, Order o)
        {
            if (mode != FormMode.Add && o == null) return;
            using (var f = new Forms.OrderForm(mode, o))
            {
                if (f.ShowDialog() == DialogResult.OK)
                    LoadAsync();
            }
        }

        private void OnDelete(object sender, EventArgs e)
        {
            var o = GetSelected();
            if (o == null) return;
            var r = MessageBox.Show($"Delete order '{o.OrderId}' for '{o.CustomerName}'?",
                "Confirm Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (r == DialogResult.Yes)
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
