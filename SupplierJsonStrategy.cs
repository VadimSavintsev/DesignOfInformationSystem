using System;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;
using Project;

namespace Program
{
    class SupplierJsonStrategy : SupplierStrategy
    {
        private readonly string jsonfilePath;

        public SupplierJsonStrategy(string jsonfilePath)
        {
            this.jsonfilePath = jsonfilePath;
        }

        public List<Supplier> ReadAllValues()
        {
            try
            {
                var json = File.ReadAllText(jsonfilePath);
                return JsonConvert.DeserializeObject<List<Supplier>>(json);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error reading JSON file: {ex.Message}");
                return new List<Supplier>(); // Возвращаем пустой список в случае ошибки
            }
        }

        public void WriteAllValues(List<Supplier> data)
        {
            try
            {
                var json = JsonConvert.SerializeObject(suppliers, Formatting.Indented);
                File.WriteAllText(jsonfilePath, json);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error writing JSON file: {ex.Message}");
            }
        }
    }
}
