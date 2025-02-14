using System.Data;

namespace OOP_new
{
    public class MainView : Form, SupplierObserver
    {
        private MainController controller;
        private DataGridView supplierTable;
        private DataTable tableModel;
        private int currentPage = 1;
        private int pageSize = 5;

        public MainView(MainController controller)
        {
            this.controller = controller;

            tableModel = new DataTable();
            tableModel.Columns.Add("№");
            tableModel.Columns.Add("Название компании");
            tableModel.Columns.Add("Телефон");
            tableModel.Columns.Add("ИНН");
            tableModel.Columns.Add("ОГРН");

            Text = "Список поставщиков";
            Size = new System.Drawing.Size(800, 600);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            MaximizeBox = false;
            MinimizeBox = false;
            StartPosition = FormStartPosition.CenterScreen;

            supplierTable = new DataGridView
            {
                ReadOnly = true,
                AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill,
                DataSource = tableModel,
                AllowUserToAddRows = false,
                AllowUserToDeleteRows = false,
                AllowUserToResizeRows = false,
                RowHeadersVisible = false,
                Size = new System.Drawing.Size(400, 300)
            };

            Controls.Add(supplierTable);

            Panel buttonPanel = new Panel();
            buttonPanel.Dock = DockStyle.Right;
            Controls.Add(buttonPanel);

            Button addButton = new Button();
            addButton.Text = "Добавить";
            addButton.Click += (sender, e) => OpenAddSupplierWindow();
            addButton.Size = new System.Drawing.Size(100, 30);
            addButton.Location = new Point(0, 100);
            buttonPanel.Controls.Add(addButton);

            Button updateButton = new Button();
            updateButton.Text = "Редактировать";
            updateButton.Click += (sender, e) => ViewSupplierDetails();
            updateButton.Size = new System.Drawing.Size(100, 30);
            updateButton.Location = new Point(0, 150);
            buttonPanel.Controls.Add(updateButton);

            Button deleteButton = new Button();
            deleteButton.Text = "Удалить";
            deleteButton.Click += (sender, e) => DeleteSupplier();
            deleteButton.Size = new System.Drawing.Size(100, 30);
            deleteButton.Location = new Point(0, 200);
            buttonPanel.Controls.Add(deleteButton);

            Button prevButton = new Button();
            prevButton.Text = "Предыдущий";
            prevButton.Click += (sender, e) => PrevPage();
            prevButton.Size = new System.Drawing.Size(100, 30);
            prevButton.Location = new Point(0, 250);
            buttonPanel.Controls.Add(prevButton);

            Button nextButton = new Button();
            nextButton.Text = "Следующий";
            nextButton.Click += (sender, e) => NextPage();
            nextButton.Size = new System.Drawing.Size(100, 30);
            nextButton.Location = new Point(0, 300);
            buttonPanel.Controls.Add(nextButton);

            controller.GetModel().AddObserver(this);

            RefreshTable();
        }

        private void RefreshTable()
        {
            tableModel.Rows.Clear();

            List<SupplierShort> suppliers = controller.GetSuppliers(pageSize, currentPage);
            if (suppliers.Count == 0 && currentPage > 1)
            {
                currentPage--;
                RefreshTable();
                return;
            }

            int startIndex = currentPage;
            for (int i = 0; i < suppliers.Count; i++)
            {
                SupplierShort supplier = suppliers[i];
                tableModel.Rows.Add(
                [
                startIndex + i,
                supplier.Name,
                supplier.PhoneNumber,
                supplier.Inn,
                supplier.Ogrn
                ]);
            }
        }

        private void PrevPage()
        {
            if (currentPage > 1)
            {
                currentPage--;
                RefreshTable();
            }
        }

        private void NextPage()
        {
            currentPage++;
            RefreshTable();
        }

        private void OpenAddSupplierWindow()
        {
            this.Invoke((MethodInvoker)delegate
            {
                new AddUpdateSupplierView(new AddSupplierController(controller.GetModel()), "Добавить", null).Show();
                RefreshTable();
            });
        }

        private void ViewSupplierDetails()
        {        
            int selectedRow = supplierTable.SelectedRows[0].Index;
            if (selectedRow == -1)
            {
                MessageBox.Show("Выберите поставщика для редактирования", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            int supplierId = controller.GetSupplierIdByRowIndex((currentPage - 1) * pageSize + selectedRow);
            Supplier driver = controller.GetSupplierById(supplierId);

            if (driver != null)
            {
                this.Invoke((MethodInvoker)delegate
                {
                    new AddUpdateSupplierView(new UpdateSupplierController(controller.GetModel(), supplierId), "Редактировать", driver).Show();
                    RefreshTable();
                });
            }
            else
            {
                MessageBox.Show("Не удалось найти поставщика", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void DeleteSupplier()
        {
            int selectedRow = supplierTable.SelectedRows[0].Index;
            if (selectedRow == -1)
            {
                MessageBox.Show("Выберите водителя для удаления", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            int driverId = controller.GetSupplierIdByRowIndex((currentPage - 1) * pageSize + selectedRow);
            DialogResult confirm = MessageBox.Show("Вы уверены, что хотите удалить поставщика?", "Подтверждение", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (confirm == DialogResult.Yes)
            {
                try
                {
                    controller.DeleteSupplier(driverId);
                    MessageBox.Show("Поставщик успешно удален!", "Успех", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    RefreshTable();
                }
                catch (Exception e)
                {
                    MessageBox.Show("Ошибка при удалении поставщика: " + e.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
        public void Update(string action, object data)
        {
            Console.WriteLine($"MainView received action: {action} with data: {data}");
        }
    }
}
