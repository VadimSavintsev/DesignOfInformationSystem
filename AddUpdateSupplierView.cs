namespace OOP_new
{
    public class AddUpdateSupplierView : Form
    {
        private SupplierController controller;

        private TextBox nameField;
        private TextBox phoneNumberField;
        private TextBox innField;
        private TextBox ogrnField;
        private TextBox addressField;
        private TextBox emailField;

        public AddUpdateSupplierView(SupplierController controller, string title, Supplier supplierData)
        {
            this.controller = controller;

            Text = title;
            Size = new System.Drawing.Size(400, 350);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            MinimizeBox = false;
            StartPosition = FormStartPosition.CenterScreen;
            FormClosing += (s, e) => this.Dispose();

            TableLayoutPanel tableLayout = new TableLayoutPanel();
            tableLayout.Dock = DockStyle.Fill;
            tableLayout.ColumnCount = 2;
            tableLayout.RowCount = 7;
            tableLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 40F));
            tableLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 60F));
            tableLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 30F));
            tableLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 30F));
            tableLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 30F));
            tableLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 30F));
            tableLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 30F));
            tableLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 30F));
            tableLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 30F));

            tableLayout.Controls.Add(new Label { Text = "Название:", AutoSize = true }, 0, 0);
            nameField = new TextBox();
            tableLayout.Controls.Add(nameField, 1, 0);

            tableLayout.Controls.Add(new Label { Text = "Телефон:", AutoSize = true }, 0, 1);
            phoneNumberField = new TextBox();
            tableLayout.Controls.Add(phoneNumberField, 1, 1);

            tableLayout.Controls.Add(new Label { Text = "ИНН:", AutoSize = true }, 0, 2);
            innField = new TextBox();
            tableLayout.Controls.Add(innField, 1, 2);

            tableLayout.Controls.Add(new Label { Text = "ОГРН:", AutoSize = true }, 0, 3);
            ogrnField = new TextBox();
            tableLayout.Controls.Add(ogrnField, 1, 3);

            tableLayout.Controls.Add(new Label { Text = "Адрес:", AutoSize = true }, 0, 4);
            addressField = new TextBox();
            tableLayout.Controls.Add(addressField, 1, 4);

            tableLayout.Controls.Add(new Label { Text = "Email:", AutoSize = true }, 0, 5);
            emailField = new TextBox();
            tableLayout.Controls.Add(emailField, 1, 5);

            if (supplierData != null)
            {
                nameField.Text = supplierData.Name;
                phoneNumberField.Text = supplierData.PhoneNumber;
                innField.Text = supplierData.Inn;
                ogrnField.Text = supplierData.Ogrn;
                addressField.Text = supplierData.Address;
                emailField.Text = supplierData.Email;
            }

            Button actionButton = new Button { Text = title };
            actionButton.Click += (s, e) => HandleAction();
            actionButton.Size = new System.Drawing.Size(100, 30);
            tableLayout.Controls.Add(actionButton, 1, 6);
            tableLayout.SetColumnSpan(actionButton, 2);
            Controls.Add(tableLayout);
        }

        private void HandleAction()
        {
            try
            {
                string name = nameField.Text.Trim();
                string phoneNumber = phoneNumberField.Text.Trim();
                string inn = innField.Text.Trim();
                string ogrn = ogrnField.Text.Trim();
                string address = addressField.Text.Trim();
                string email = emailField.Text.Trim();

                controller.Handle(name, address, phoneNumber, email, inn, ogrn);

                MessageBox.Show("Операция выполнена успешно!", "Успех", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.Close();
            }
            catch (ArgumentException ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Произошла ошибка: " + ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
