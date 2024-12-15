using System.Collections.Generic;
using System.IO;
using System.Linq;
using Newtonsoft.Json;
using Project;

namespace Program
{
    class SupplierRepJson
    {
        private readonly string _filePath;

        public SupplierRepJson(string filePath)
        {
            _filePath = filePath;
            if (!File.Exists(_filePath))
            {
                File.WriteAllText(_filePath, "[]"); // Создаем пустой JSON-файл, если его нет
            }
        }

        // Чтение всех значений из файла
        public List<Supplier> ReadAll()
        {
            var json = File.ReadAllText(_filePath);
            return JsonConvert.DeserializeObject<List<Supplier>>(json);
        }

        // Запись всех значений в файл
        public void WriteAll(List<Supplier> data)
        {
            var json = JsonConvert.SerializeObject(data, Formatting.Indented);
            File.WriteAllText(_filePath, json);
        }

        // Получить объект по ID
        public Supplier GetById(int id)
        {
            var data = ReadAll();
            return data.FirstOrDefault(supplier => supplier.GetId() == id);
        }

        // Получить список k по счету n объектов
        public List<SupplierShort> GetKNShortList(int k, int n)
        {
            var data = ReadAll(); // Читаем все данные из файла
            return data.Skip(k * n).Take(n) // Пропускаем k * n элементов и берем следующие n элементов
                        .Select(supplier => new SupplierShort(
                            supplier.GetId(),
                            supplier.GetName(),
                            supplier.GetPhoneNumber(),
                            supplier.GetInn(),
                            supplier.GetOgrn()
                        ))
                        .ToList(); // Преобразуем в список SupplierShort
        }

        // Сортировать элементы по ИНН
        public List<Supplier> SortByInn()
        {
            var data = ReadAll();
            return data.OrderBy(supplier => supplier.GetInn()).ToList();
        }

        // Добавить объект в список (сформировать новый ID)
        public void AddItem(Supplier supplier)
        {
            var data = ReadAll();
            var newId = data.Any() ? data.Max(x => x.GetId()) + 1 : 1;
            supplier.SetId(newId);
            data.Add(supplier);
            WriteAll(data);
        }

        // Заменить элемент списка по ID
        public void ReplaceById(int id, Supplier newSupplier)
        {
            var data = ReadAll();
            var index = data.FindIndex( supplier=> supplier.GetId() == id);
            if (index != -1)
            {
                newSupplier.SetId(id); // Убедимся, что ID остается неизменным
                data[index] = newSupplier;
                WriteAll(data);
            }
        }

        // Удалить элемент списка по ID
        public void DeleteById(int id)
        {
            var data = ReadAll();
            var index = data.FindIndex(supplier => supplier.GetId() == id);
            if (index != -1)
            {
                data.RemoveAt(index);
                WriteAll(data);
            }
        }

        // Получить количество элементов
        public int GetCount()
        {
            return ReadAll().Count;
        }
    }
}
