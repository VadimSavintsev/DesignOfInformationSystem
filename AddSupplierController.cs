namespace OOP_new
{
    internal class AddSupplierController:SupplierController
    {
        private readonly SupplierModel model;

        public AddSupplierController(SupplierModel model)
        {
            this.model = model;
        }

        public void Handle(string name, string address, string phoneNumber, string email, string inn, string ogrn)
        {
            SupplierController.ValidateData(name, address, phoneNumber, email, inn, ogrn);

            model.AddSupplier(name, address, phoneNumber, email, inn, ogrn);
        }
    }
}
