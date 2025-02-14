using System.Data;

namespace OOP_new
{
    public class MainView : Form
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

            Button prevButton = new Button();
            prevButton.Text = "Предыдущий";
            prevButton.Click += (sender, e) => PrevPage();
            prevButton.Size = new System.Drawing.Size(100, 30);
            prevButton.Location = new Point(0, 100);
            buttonPanel.Controls.Add(prevButton);

            Button nextButton = new Button();
            nextButton.Text = "Следующий";
            nextButton.Click += (sender, e) => NextPage();
            nextButton.Size = new System.Drawing.Size(100, 30);
            nextButton.Location = new Point(0, 150);
            buttonPanel.Controls.Add(nextButton);

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

            int startIndex = (currentPage - 1) * pageSize + 1;
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
    }
}
