using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace Control
{
    public partial class DataGV_DS_Phong : UserControl
    {
        private List<Item> items = new List<Item>();
        private int currentPage = 1;
        private const int itemsPerPage = 10;
        private DataGridView dataGridView1;
        private Button btnAdd, btnPrevious, btnNext;

        public DataGV_DS_Phong()
        {
            InitializeComponent();
            InitializeDataGridView();
            InitializeButtons();
            LoadItems();
            UpdateDataGridView();
        }

        private void InitializeDataGridView()
        {
            // Khởi tạo DataGridView
            dataGridView1 = new DataGridView
            {
                Location = new Point(10, 10), // Vị trí
                Size = new Size(600, 300), // Kích thước lớn hơn
                AllowUserToAddRows = false,
                ReadOnly = true
            };

            // Thiết lập các cột
            dataGridView1.Columns.Add("Id", "ID");
            dataGridView1.Columns.Add("Name", "Name");
            dataGridView1.Columns.Add("Edit", "Edit");
            dataGridView1.Columns.Add("Delete", "Delete");

            // Đăng ký sự kiện click
            dataGridView1.CellClick += DataGridView1_CellClick;

            // Thêm DataGridView vào UserControl
            this.Controls.Add(dataGridView1);
        }

        private void InitializeButtons()
        {
            // Khởi tạo nút thêm
            btnAdd = new Button
            {
                Location = new Point(620, 10),
                Size = new Size(100, 30),
               // Image = Properties.Resources.add_icon, // Sử dụng biểu tượng thêm
                ImageAlign = ContentAlignment.MiddleLeft,
                Text = "Add",
                TextImageRelation = TextImageRelation.ImageBeforeText
            };
            btnAdd.Click += btnAdd_Click;

            // Khởi tạo nút trước
            btnPrevious = new Button
            {
                Location = new Point(10, 320), // Di chuyển xuống dưới
                Size = new Size(100, 30),
               // Image = Properties.Resources.previous_icon, // Sử dụng biểu tượng trước
                ImageAlign = ContentAlignment.MiddleLeft,
                Text = "Previous",
                TextImageRelation = TextImageRelation.ImageBeforeText
            };
            btnPrevious.Click += btnPrevious_Click;

            // Khởi tạo nút tiếp theo
            btnNext = new Button
            {
                Location = new Point(120, 320), // Di chuyển xuống dưới
                Size = new Size(100, 30),
               // Image = Properties.Resources.next_icon, // Sử dụng biểu tượng tiếp theo
                ImageAlign = ContentAlignment.MiddleLeft,
                Text = "Next",
                TextImageRelation = TextImageRelation.ImageBeforeText
            };
            btnNext.Click += btnNext_Click;

            // Thêm nút vào UserControl
            this.Controls.Add(btnAdd);
            this.Controls.Add(btnPrevious);
            this.Controls.Add(btnNext);
        }


        private void LoadItems()
        {
            // Tạo dữ liệu mẫu với 30 hàng
            for (int i = 1; i <= 30; i++)
            {
                items.Add(new Item { Id = i, Name = "Item " + i });
            }
        }

        private void UpdateDataGridView()
        {
            dataGridView1.Rows.Clear();
            int startIndex = (currentPage - 1) * itemsPerPage;
            int endIndex = Math.Min(startIndex + itemsPerPage, items.Count);

            for (int i = startIndex; i < endIndex; i++)
            {
                var row = new DataGridViewRow();
                row.CreateCells(dataGridView1, items[i].Id, items[i].Name, "✏️", "🗑️"); // Sử dụng icon
                dataGridView1.Rows.Add(row);
            }

            btnPrevious.Enabled = currentPage > 1;
            btnNext.Enabled = currentPage * itemsPerPage < items.Count;
        }

        private void DataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;

            // Nếu nhấn vào cột Edit
            if (e.ColumnIndex == 2)
            {
                EditItem(e.RowIndex);
            }
            // Nếu nhấn vào cột Delete
            else if (e.ColumnIndex == 3)
            {
                DeleteItem(e.RowIndex);
            }
        }

        private void EditItem(int rowIndex)
        {
            // Cập nhật tên item (hoặc mở hộp thoại để sửa)
            items[rowIndex].Name = "Edited " + items[rowIndex].Name; // Ví dụ chỉnh sửa
            UpdateDataGridView();
        }

        private void DeleteItem(int rowIndex)
        {
            items.RemoveAt(rowIndex);
            UpdateDataGridView();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            int newId = items.Count + 1;
            items.Add(new Item { Id = newId, Name = "Item " + newId });
            UpdateDataGridView();
        }

        private void btnPrevious_Click(object sender, EventArgs e)
        {
            if (currentPage > 1)
            {
                currentPage--;
                UpdateDataGridView();
            }
        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            if (currentPage * itemsPerPage < items.Count)
            {
                currentPage++;
                UpdateDataGridView();
            }
        }

        private class Item
        {
            public int Id { get; set; }
            public string Name { get; set; }
        }
    }
}
