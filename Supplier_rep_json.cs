using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.Json;
using System.Linq;
using Project;

namespace Program
{
    class Supplier_rep_json
    {
        private readonly string filePath;

        public Supplier_rep_json(string filePath)
        {
            this.filePath = filePath;
        }

        public List<Supplier> ReadAllValues()
        {
            if (!File.Exists(filePath))
            {
                throw new FileNotFoundException($"Файл {filePath} не найден.");
            }

            string jsonSupplier = File.ReadAllText(filePath);
            var suppliers = JsonSerializer.Deserialize<List<Supplier>>(jsonSupplier);
            return suppliers;
        }

        public void WriteAllValues(List<Supplier> suppliers)
        {
            string jsonSupplier = JsonSerializer.Serialize(suppliers, new JsonSerializerOptions
            {
                WriteIndented = true,
                Encoder = System.Text.Encodings.Web.JavaScriptEncoder.UnsafeRelaxedJsonEscaping
            });
            File.WriteAllText(filePath, jsonSupplier, Encoding.UTF8);
        }

        public Supplier GetSupplierById(int? id)
        {
            var suppliers = ReadAllValues();
            return suppliers.FirstOrDefault(supplier => supplier.GetId() == id.Value);
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
                var supplierShort = new SupplierShort(supplier.GetId(),supplier.GetName(),supplier.GetPhoneNumber(),supplier.GetInn(),supplier.GetOgrn());

                shortList.Add(supplierShort);
            }

            return shortList;
        }

        public List<Supplier> SortByInn(List<Supplier> suppliers)
        {
            return suppliers.OrderBy(supplier => supplier.GetInn()).ToList();
        }

        public void AddSupplier(Supplier newSupplier)
        {
            var suppliers = ReadAllValues();
            int? maxId = suppliers.Count > 0 ? suppliers.Max(supplier => supplier.GetId()) : null;
            int newId = maxId.HasValue ? maxId.Value + 1 : 1;
            newSupplier.SetId(newId);
            suppliers.Add(newSupplier);
            WriteAllValues(suppliers);
        }

        public void ReplaceSupplierById(int id, Supplier newSupplier)
        {
            var suppliers = ReadAllValues();
            var existingSupplier = suppliers.FirstOrDefault(supplier => supplier.GetId() == id);

            if (existingSupplier == null)
            {
                throw new ArgumentException($"Поставщик с ID {id} не найден.");
            }

            int index = suppliers.IndexOf(existingSupplier);
            suppliers[index] = newSupplier;
            newSupplier.SetId(id);
            WriteAllValues(suppliers);
        }

        public void DeleteSupplierById(int id)
        {
            var suppliers = ReadAllValues();
            var supplierToDelete = suppliers.FirstOrDefault(supplier => supplier.GetId() == id);

            if (supplierToDelete == null)
            {
                throw new ArgumentException($"Поставщик с ID {id} не найден.");
            }

            suppliers.Remove(supplierToDelete);
            WriteAllValues(suppliers);
        }
    }
}
