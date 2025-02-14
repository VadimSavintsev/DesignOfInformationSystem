using OOP_new;

namespace Project
{
    public interface ISupplierModel
    {
        Supplier GetSupplierById(int id);

        List<SupplierShort> Get_k_n_short_list(int k, int n);

        void AddSupplier(Supplier supplier);

        bool ReplaceSupplierById(int id, Supplier newSupplier);

        void DeleteSupplierById(int id);

        int GetCount();
    }
}
