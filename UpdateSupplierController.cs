namespace OOP_new
{
    internal class UpdateSupplierController : SupplierController
    {
        private readonly SupplierModel model;
        private readonly int supplierId;

        public UpdateSupplierController(SupplierModel model, int supplierId)
        {
            this.model = model;
            this.supplierId = supplierId;
        }

        public void Handle(string name, string address, string phoneNumber, string email, string inn, string ogrn)
        {
            SupplierController.ValidateData(name, address, phoneNumber, email, inn, ogrn);

            model.UpdateSupplier(supplierId, name, address, phoneNumber, email, inn, ogrn);
        }
    }
}
