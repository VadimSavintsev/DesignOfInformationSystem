using Project;

namespace Program
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class SupplierRep
    {
        private SupplierStrategy strategy;
        private List<Supplier> suppliers; // Поле для хранения данных в памяти

        public SupplierRep(SupplierStrategy strategy)
        {
            this.SetStrategy(strategy);
            this.suppliers = ReadAllValues(); // Инициализация списка данными из файла
        }

        public void SetStrategy(SupplierStrategy strategy)
        {
            this.strategy = strategy;
        }

        // Чтение всех значений из файла
        public List<Supplier> ReadAllValues()
        {
            if (strategy != null)
            {
                return strategy.ReadAllValues();
            }
            return new List<Supplier>();
        }

        // Запись всех значений в файл
        public void WriteAllValues()
        {
            if (strategy != null)
            {
                strategy.WriteAllValues(suppliers);
            }
        }

        // Получить объект по ID
        public Supplier GetObjectById(int id)
        {
            return suppliers.FirstOrDefault(supplier => supplier.GetId() == id);
        }

        // Получить список k по счету n объектов
        public List<SupplierShort> GetKthNList(int k, int n)
        {
            return suppliers.Skip(k * n).Take(n)
                             .Select(supplier => new SupplierShort(
                                 supplier.GetId(),
                                 supplier.GetName(),
                                 supplier.GetPhoneNumber(),
                                 supplier.GetInn(),
                                 supplier.GetOgrn()
                             ))
                             .ToList();
        }

        // Сортировать элементы по ИНН
        public List<Supplier> SortByInn()
        {
            return suppliers.OrderBy(supplier => supplier.GetInn()).ToList();
        }

        private bool IsUnique(Supplier supplier)
        {
            return suppliers.All(existingSupplier => !existingSupplier.Equals(supplier));
        }

        // Добавить объект в список (сформировать новый ID)
        public void AddSupplier(Supplier supplier)
        {
            var newId = suppliers.Any() ? suppliers.Max(x => x.GetId()) + 1 : 1;
            supplier.SetId(newId);
            if(!IsUnique(supplier))
            {
                throw new Exception("Поставщик с таким ИНН или ОГРН уже существует");
            }
            suppliers.Add(supplier);
            WriteAllValues(); // Сохраняем изменения в файл
        }

        // Заменить элемент списка по ID
        public void ReplaceSupplierById(int id, Supplier newSupplier)
        {
            var index = suppliers.FindIndex(supplier => supplier.GetId() == id);
            if (index != -1)
            {
                newSupplier.SetId(id);
                if(!IsUnique(newSupplier))
                {
                    throw new Exception("Нельзя заменить поставщика: Поставщие с таким ИНН или ОГРН уже существует");
                }
                suppliers[index] = newSupplier;
                WriteAllValues(); // Сохраняем изменения в файл
            }
        }

        // Удалить элемент списка по ID
        public void DeleteSupplierById(int id)
        {
            var index = suppliers.FindIndex(supplier => supplier.GetId() == id);
            if (index != -1)
            {
                suppliers.RemoveAt(index);
                WriteAllValues(); // Сохраняем изменения в файл
            }
        }

        // Получить количество элементов
        public int GetCount()
        {
            return suppliers.Count;
        }
    }
}
