using System;
using System.Collections.Generic;
using Project;

namespace Program
{
    public class SupplierRepAdapter
    {
        private readonly SupplierRep supplierRep;

        // Конструктор
        public SupplierRepAdapter(SupplierStrategy strategy)
        {
            this.supplierRep = new SupplierRep(strategy); // Создание нового экземпляра класса полного
        }

        // Методы

        // Получение объекта по ID
        public Supplier GetObjectById(int id)
        {
            return supplierRep.GetObjectById(id);
        }

        // Получение списка k по счету n объектов
        public List<SupplierShort> GetKthNList(int k, int n)
        {
            return supplierRep.GetKthNList(k, n);
        }

        // Добавление объекта
        public void AddSupplier(Supplier supplier)
        {
            try
            {
                supplierRep.AddSupplier(supplier);
                supplierRep.WriteAllValues();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка при добавлении поставщика: {ex.Message}");
                throw;
            }
        }

        // Замена объекта по ID
        public bool ReplaceSupplierById(int id, Supplier newSupplier)
        {
            try
            {
                supplierRep.ReplaceSupplierById(id, newSupplier);
                supplierRep.WriteAllValues();
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка при замене поставщика: {ex.Message}");
                return false;
            }
        }

        // Удаление объекта по ID
        public void DeleteSupplierById(int id)
        {
            supplierRep.DeleteSupplierById(id);
            supplierRep.WriteAllValues();
        }

        // Получение количества объектов
        public int GetCount()
        {
            return supplierRep.GetCount();
        }
    }
}
