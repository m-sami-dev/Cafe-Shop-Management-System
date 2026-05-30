using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using App.Core.Models;
using App.Core.Services;

namespace App.WindowsApp.UserControls
{
    public class ProductsControl : UserControl
    {
        private readonly ProductService _svc;
        private readonly Action<string> _updateStatus;

        private DataGridView _grid;
        private BindingSource _bindingSource;
        private TextBox _txtSearch;
        private ComboBox _cmbCategory;
        private Button _btnAdd, _btnEdit, _btnView, _btnDelete, _btnRefresh;
        private Label _lblCount;

        public ProductsControl(Action<string> updateStatus)
        {
            _svc          = new ProductService();
            _updateStatus = updateStatus;
            _bindingSource = new BindingSource();
            InitializeLayout();
            LoadAsync();
        }

        private void InitializeLayout()
        {
            Dock = DockStyle.Fill;
            BackColor = Color.FromArgb(245, 245, 245);

            // ── Title ──────────────────────────────────────────────────────────
            var lblTitle = new Label
            {
                Text     = "📦  Products",
                Font     = new Font("Segoe UI", 16f, FontStyle.Bold),
                Location = new Point(10, 10), Size = new Size(300, 36),
                ForeColor = Color.FromArgb(31, 31, 46)
            };
            Controls.Add(lblTitle);

            //// ── Search bar ────────────────────────────────────────────────────
            //_txtSearch = new TextBox
            //{
            //    //PlaceholderText = "🔍 Search by name or description...",
            //    txtSearch.Text = "Search here...";
            //    Location = new Point(10, 56), Size = new Size(300, 28),
            //    Font = new Font("Segoe UI", 9.5f)
            //};
            // ── Search bar ────────────────────────────────────────────────────
            _txtSearch = new TextBox
            {
                Location = new Point(10, 56),
                Size = new Size(300, 28),
                Font = new Font("Segoe UI", 9.5f),
                Text = "🔍 Search by name or description..."
            };
            _txtSearch.TextChanged += (s, e) => FilterGrid();
            Controls.Add(_txtSearch);

            _cmbCategory = new ComboBox
            {
                Location     = new Point(320, 56), Size = new Size(150, 28),
                DropDownStyle = ComboBoxStyle.DropDownList,
                Font          = new Font("Segoe UI", 9.5f)
            };
            _cmbCategory.Items.Add("All Categories");
            _cmbCategory.SelectedIndex = 0;
            _cmbCategory.SelectedIndexChanged += (s, e) => FilterGrid();
            Controls.Add(_cmbCategory);

            // ── Toolbar ────────────────────────────────────────────────────────
            _btnAdd     = CreateToolButton("➕ Add",     Color.FromArgb(46,204,113));
            _btnEdit    = CreateToolButton("✏️ Edit",    Color.FromArgb(52,152,219));
            _btnView    = CreateToolButton("👁 View",    Color.FromArgb(155,89,182));
            _btnDelete  = CreateToolButton("🗑 Delete",  Color.FromArgb(231,76,60));
            _btnRefresh = CreateToolButton("🔄 Refresh", Color.FromArgb(100,100,120));

            var toolbar = new FlowLayoutPanel
            {
                Location  = new Point(480, 50),
                Size      = new Size(660, 40),
                FlowDirection = FlowDirection.LeftToRight
            };
            toolbar.Controls.AddRange(new Control[] { _btnAdd, _btnEdit, _btnView, _btnDelete, _btnRefresh });
            Controls.Add(toolbar);

            _btnAdd.Click     += OnAdd;
            _btnEdit.Click    += OnEdit;
            _btnView.Click    += OnView;
            _btnDelete.Click  += OnDelete;
            _btnRefresh.Click += (s, e) => LoadAsync();

            // ── Grid ───────────────────────────────────────────────────────────
            _grid = new DataGridView
            {
                Location          = new Point(10, 100),
                Size              = new Size(1130, 520),
                DataSource        = _bindingSource,
                AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill,
                SelectionMode     = DataGridViewSelectionMode.FullRowSelect,
                ReadOnly          = true,
                AllowUserToAddRows    = false,
                AllowUserToDeleteRows = false,
                BackgroundColor   = Color.White,
                BorderStyle       = BorderStyle.None,
                RowHeadersVisible = false,
                Font              = new Font("Segoe UI", 9f),
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
            _grid.ColumnHeaderMouseClick += OnColumnHeaderClick;
            _grid.CellDoubleClick        += (s, e) => OnView(s, e);
            Controls.Add(_grid);

            _lblCount = new Label
            {
                Location  = new Point(10, 625),
                Size      = new Size(300, 20),
                ForeColor = Color.Gray,
                Font      = new Font("Segoe UI", 8.5f)
            };
            Controls.Add(_lblCount);
        }

        private Button CreateToolButton(string text, Color color)
        {
            return new Button
            {
                Text      = text,
                Size      = new Size(100, 32),
                BackColor = color,
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Font      = new Font("Segoe UI", 9f),
                Margin    = new Padding(0, 0, 6, 0),
                Cursor    = Cursors.Hand,
                FlatAppearance = { BorderSize = 0 }
            };
        }

        // ── Async load ────────────────────────────────────────────────────────
        private async void LoadAsync()
        {
            _updateStatus("Loading products...");
            SetButtonsEnabled(false);

            var products = await _svc.GetAllAsync();

            // Populate category filter
            _cmbCategory.Items.Clear();
            _cmbCategory.Items.Add("All Categories");
            foreach (var cat in _svc.GetCategories())
                _cmbCategory.Items.Add(cat);
            _cmbCategory.SelectedIndex = 0;

            _bindingSource.DataSource = products;
            ConfigureColumns();
            UpdateCount();
            SetButtonsEnabled(true);
            _updateStatus($"Products loaded — {products.Count} records");
        }

        private void ConfigureColumns()
        {
            if (_grid.Columns.Count == 0) return;
            var hide = new[] { "Description", "CreatedAt" };
            foreach (DataGridViewColumn col in _grid.Columns)
                col.Visible = !Array.Exists(hide, h => h == col.Name);

            if (_grid.Columns["Price"] != null)
                _grid.Columns["Price"].DefaultCellStyle.Format = "N2";
            if (_grid.Columns["IsAvailable"] != null)
                _grid.Columns["IsAvailable"].HeaderText = "Available";
        }

        private void FilterGrid()
        {
            var keyword = _txtSearch.Text.Trim();
            var category = _cmbCategory.SelectedItem?.ToString();
            if (category == "All Categories") category = "";

            List<Product> results;
            if (string.IsNullOrWhiteSpace(keyword))
                results = _svc.GetAll();
            else
                results = _svc.Search(keyword, category);

            if (!string.IsNullOrWhiteSpace(category) && string.IsNullOrWhiteSpace(keyword))
                results = _svc.Search("", category);

            _bindingSource.DataSource = results;
            ConfigureColumns();
            UpdateCount();
        }

        private void UpdateCount()
        {
            _lblCount.Text = $"Showing {_bindingSource.Count} product(s)";
        }

        private void SetButtonsEnabled(bool enabled)
        {
            _btnAdd.Enabled = _btnEdit.Enabled = _btnView.Enabled =
            _btnDelete.Enabled = _btnRefresh.Enabled = enabled;
        }

        // ── Column sort ───────────────────────────────────────────────────────
        private void OnColumnHeaderClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            _grid.Sort(_grid.Columns[e.ColumnIndex],
                       _grid.SortOrder == SortOrder.Ascending
                           ? System.ComponentModel.ListSortDirection.Descending
                           : System.ComponentModel.ListSortDirection.Ascending);
        }

        // ── CRUD handlers ─────────────────────────────────────────────────────
        private void OnAdd(object sender, EventArgs e)
        {
            using (var f = new Forms.ProductForm(FormMode.Add, null))
            {
                if (f.ShowDialog() == DialogResult.OK)
                {
                    LoadAsync();
                    _updateStatus("Product added successfully.");
                }
            }
        }

        private void OnEdit(object sender, EventArgs e)
        {
            var p = GetSelected();
            if (p == null) return;
            using (var f = new Forms.ProductForm(FormMode.Edit, p))
            {
                if (f.ShowDialog() == DialogResult.OK)
                {
                    LoadAsync();
                    _updateStatus($"Product '{p.Name}' updated.");
                }
            }
        }

        private void OnView(object sender, EventArgs e)
        {
            var p = GetSelected();
            if (p == null) return;
            using (var f = new Forms.ProductForm(FormMode.View, p))
                f.ShowDialog();
        }

        private void OnDelete(object sender, EventArgs e)
        {
            var p = GetSelected();
            if (p == null) return;

            var confirm = MessageBox.Show(
                $"Are you sure you want to delete '{p.Name}'?\nThis action cannot be undone.",
                "Confirm Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

            if (confirm == DialogResult.Yes)
            {
                if (_svc.Delete(p.ProductId))
                {
                    LoadAsync();
                    _updateStatus($"Product '{p.Name}' deleted.");
                }
                else
                {
                    MessageBox.Show("Delete failed. The product may be used in existing orders.",
                                   "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private Product GetSelected()
        {
            if (_bindingSource.Current == null)
            {
                MessageBox.Show("Please select a product first.", "No Selection",
                                MessageBoxButtons.OK, MessageBoxIcon.Information);
                return null;
            }
            return _bindingSource.Current as Product;
        }
    }
}
