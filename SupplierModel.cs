using Project;

namespace OOP_new
{
    public class SupplierModel : SupplierObservable
    {
        private ISupplierModel supplierRep;

        public SupplierModel(ISupplierModel supplierRep)
        {
            this.supplierRep = supplierRep;
        }

        public List<SupplierShort> GetSuppliers(int pageSize, int pageNum)
        {
            return supplierRep.Get_k_n_short_list(pageNum, pageSize);
        }

        public Supplier GetSupplierById(int supplierId)
        {
            return supplierRep.GetSupplierById(supplierId);
        }

        public void AddSupplier(string name, string address, string phoneNumber, string email, string inn, string ogrn)
        {
            Supplier newSupplier = new Supplier(null, name, address, phoneNumber, email, inn, ogrn);
            supplierRep.AddSupplier(newSupplier);
            NotifyObservers("add", newSupplier);
        }

        public void UpdateSupplier(int supplierId, string name, string address, string phoneNumber, string email, string inn, string ogrn)
        {
            Supplier updatedSupplier = new Supplier(supplierId, name, address, phoneNumber, email, inn, ogrn);

            bool success = supplierRep.ReplaceSupplierById(supplierId, updatedSupplier);
            if (!success)
            {
                throw new Exception($"Ошибка обновления данных поставщика с id: {supplierId}");
            }
            else
            {
                NotifyObservers("update", updatedSupplier);
            }
        }

        public void DeleteSupplier(int supplierId)
        {
            supplierRep.DeleteSupplierById(supplierId);
            NotifyObservers("delete", supplierId);
        }

        public int GetCount()
        {
            return supplierRep.GetCount();
        }
    }

}
