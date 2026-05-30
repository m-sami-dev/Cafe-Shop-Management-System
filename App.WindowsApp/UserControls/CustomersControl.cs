using System;
using System.Drawing;
using System.Windows.Forms;
using App.Core.Models;
using App.Core.Services;

namespace App.WindowsApp.UserControls
{
    public class CustomersControl : UserControl
    {
        private readonly CustomerService _svc;
        private readonly Action<string>  _updateStatus;
        private BindingSource _bindingSource;
        private DataGridView  _grid;
        private TextBox       _txtSearch;
        private Label         _lblCount;
        private Button        _btnAdd, _btnEdit, _btnView, _btnDelete, _btnRefresh;

        public CustomersControl(Action<string> updateStatus)
        {
            _svc           = new CustomerService();
            _updateStatus  = updateStatus;
            _bindingSource = new BindingSource();
            InitializeLayout();
            LoadAsync();
        }

        private void InitializeLayout()
        {
            Dock      = DockStyle.Fill;
            BackColor = Color.FromArgb(245, 245, 245);

            var lblTitle = new Label
            {
                Text     = "👥  Customers",
                Font     = new Font("Segoe UI", 16f, FontStyle.Bold),
                Location = new Point(10, 10), Size = new Size(300, 36),
                ForeColor = Color.FromArgb(31, 31, 46)
            };
            Controls.Add(lblTitle);

            //_txtSearch = new TextBox
            //{
            //    //PlaceholderText = "🔍 Search by name, phone or email...",
            //    txtSearch.Text = "Search here...";
            //    Location = new Point(10, 56), Size = new Size(350, 28),
            //    Font = new Font("Segoe UI", 9.5f)
            //};
            //_txtSearch.TextChanged += (s, e) => FilterGrid();
            //Controls.Add(_txtSearch);
            _txtSearch = new TextBox
            {
                Location = new Point(10, 56),
                Size = new Size(350, 28),
                Font = new Font("Segoe UI", 9.5f),
                Text = "🔍 Search by name, phone or email..." // Bracket ke andar sahi tarika yeh hai
            };
            _txtSearch.TextChanged += (s, e) => FilterGrid();
            Controls.Add(_txtSearch);

            // Toolbar
            _btnAdd     = ToolBtn("➕ Add",     Color.FromArgb(46,204,113));
            _btnEdit    = ToolBtn("✏️ Edit",    Color.FromArgb(52,152,219));
            _btnView    = ToolBtn("👁 View",    Color.FromArgb(155,89,182));
            _btnDelete  = ToolBtn("🗑 Delete",  Color.FromArgb(231,76,60));
            _btnRefresh = ToolBtn("🔄 Refresh", Color.FromArgb(100,100,120));

            var toolbar = new FlowLayoutPanel { Location = new Point(380, 50), Size = new Size(660, 40) };
            toolbar.Controls.AddRange(new Control[] { _btnAdd, _btnEdit, _btnView, _btnDelete, _btnRefresh });
            Controls.Add(toolbar);

            _btnAdd.Click     += (s, e) => ShowForm(FormMode.Add, null);
            _btnEdit.Click    += (s, e) => ShowForm(FormMode.Edit, GetSelected());
            _btnView.Click    += (s, e) => ShowForm(FormMode.View, GetSelected());
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
            Controls.Add(_grid);

            _lblCount = new Label
            {
                Location = new Point(10, 625), Size = new Size(300, 20),
                ForeColor = Color.Gray, Font = new Font("Segoe UI", 8.5f)
            };
            Controls.Add(_lblCount);
        }

        private Button ToolBtn(string text, Color color) => new Button
        {
            Text = text, Size = new Size(100, 32),
            BackColor = color, ForeColor = Color.White,
            FlatStyle = FlatStyle.Flat, Margin = new Padding(0,0,6,0),
            Cursor = Cursors.Hand,
            FlatAppearance = { BorderSize = 0 }
        };

        private async void LoadAsync()
        {
            _updateStatus("Loading customers...");
            var list = await _svc.GetAllAsync();
            _bindingSource.DataSource = list;
            HideColumn("CreatedAt");
            _lblCount.Text = $"Showing {list.Count} customer(s)";
            _updateStatus($"Customers loaded — {list.Count} records");
        }

        private void FilterGrid()
        {
            var kw = _txtSearch.Text.Trim();
            var list = string.IsNullOrWhiteSpace(kw) ? _svc.GetAll() : _svc.Search(kw);
            _bindingSource.DataSource = list;
            HideColumn("CreatedAt");
            _lblCount.Text = $"Showing {list.Count} customer(s)";
        }

        private void HideColumn(string name)
        {
            if (_grid.Columns[name] != null) _grid.Columns[name].Visible = false;
        }

        private void ShowForm(FormMode mode, Customer c)
        {
            if (mode != FormMode.Add && c == null) return;
            using (var f = new Forms.CustomerForm(mode, c))
            {
                if (f.ShowDialog() == DialogResult.OK)
                    LoadAsync();
            }
        }

        private void OnDelete(object sender, EventArgs e)
        {
            var c = GetSelected();
            if (c == null) return;
            var r = MessageBox.Show($"Delete customer '{c.FullName}'?",
                "Confirm Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (r == DialogResult.Yes)
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
