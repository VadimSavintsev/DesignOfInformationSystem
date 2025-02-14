namespace OOP_new
{
    public class MainController
    {
        private SupplierModel model;

        public MainController(SupplierModel model)
        {
            this.model = model;
        }

        public SupplierModel GetModel()
        {
            return model;
        }

        public List<SupplierShort> GetSuppliers(int pageSize, int pageNum)
        {
            return model.GetSuppliers(pageSize, pageNum);
        }

        public Supplier GetSupplierById(int driverId)
        {
            return model.GetSupplierById(driverId);
        }

        public void AddSupplier(string name, string address, string phoneNumber, string email, string inn, string ogrn)
        {
            model.AddSupplier(name, address, phoneNumber, email, inn, ogrn);
        }

        public void UpdateSupplier(int supplierId, string name, string address, string phoneNumber, string email, string inn, string ogrn)
        {
            model.UpdateSupplier(supplierId, name, address, phoneNumber, email, inn, ogrn);
        }

        public void DeleteSupplier(int driverId)
        {
            model.DeleteSupplier(driverId);
        }

        public int GetSupplierCount()
        {
            return model.GetCount();
        }
    }
}
