using System;
using System.Collections.Generic;
using System.Linq;
using Project;

namespace Program
{
    public abstract class Supplier_rep
    {
        protected readonly string filePath;

        protected Supplier_rep(string filePath)
        {
            this.filePath = filePath;
        }

        public abstract List<Supplier> ReadAllValues();
        public abstract void WriteAllValues(List<Supplier> suppliers);

        public Supplier GetSupplierById(int id)
        {
            var suppliers = ReadAllValues();
            return suppliers.FirstOrDefault(supplier => supplier.Id == id);
        }

        public List<SupplierShort> Get_k_n_short_list(int k, int n)
        {
            var suppliers = ReadAllValues();

            if (n < 0 || n >= suppliers.Count)
            {
                throw new ArgumentOutOfRangeException(nameof(n), "Индекс n выходит за пределы списка.");
            }

            var shortList = new List<SupplierShort>();

            for (int i = n; i < n + k && i < suppliers.Count; i++)
            {
                var supplier = suppliers[i];
                var supplierShort = new SupplierShort(supplier.Id, supplier.Name, supplier.PhoneNumber, supplier.Inn, supplier.Ogrn);

                shortList.Add(supplierShort);
            }

            return shortList;
        }

        public List<Supplier> SortByInn(List<Supplier> suppliers)
        {
            return suppliers.OrderBy(supplier => supplier.Inn).ToList();
        }

        public void AddSupplier(Supplier newSupplier)
        {
            var suppliers = ReadAllValues();
            int? maxId = suppliers.Count > 0 ? suppliers.Max(supplier => supplier.Id) : null;
            int newId = maxId.HasValue ? maxId.Value + 1 : 1;
            newSupplier.Id = newId;
            suppliers.Add(newSupplier);
            WriteAllValues(suppliers);
        }

        public void ReplaceSupplierById(int id, Supplier newSupplier)
        {
            var suppliers = ReadAllValues();
            var existingSupplier = suppliers.FirstOrDefault(supplier => supplier.Id == id);

            if (existingSupplier == null)
            {
                throw new ArgumentException($"Поставщик с ID {id} не найден.");
            }

            int index = suppliers.IndexOf(existingSupplier);
            suppliers[index] = newSupplier;
            newSupplier.Id = id;
            WriteAllValues(suppliers);
        }

        public void DeleteSupplierById(int id)
        {
            var suppliers = ReadAllValues();
            var supplierToDelete = suppliers.FirstOrDefault(supplier => supplier.Id == id);

            if (supplierToDelete == null)
            {
                throw new ArgumentException($"Поставщик с ID {id} не найден.");
            }

            suppliers.Remove(supplierToDelete);
            WriteAllValues(suppliers);
        }

        public int GetCount()
        {
            var suppliers = ReadAllValues();
            return suppliers.Count;
        }
    }
}
