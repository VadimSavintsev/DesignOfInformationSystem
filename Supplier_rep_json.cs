using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.Json;
using System.Linq;
using Project;

namespace Program
{
    class Supplier_rep_json : Supplier_rep
    {
        public Supplier_rep_json(string filePath) : base(filePath) { }

        public override List<Supplier> ReadAllValues()
        {
            if (!File.Exists(filePath))
            {
                throw new FileNotFoundException($"Файл {filePath} не найден.");
            }

            string jsonSupplier = File.ReadAllText(filePath);
            var suppliers = JsonSerializer.Deserialize<List<Supplier>>(jsonSupplier);
            return suppliers;
        }

        public override void WriteAllValues(List<Supplier> suppliers)
        {
            string jsonSupplier = JsonSerializer.Serialize(suppliers, new JsonSerializerOptions
            {
                WriteIndented = true,
                Encoder = System.Text.Encodings.Web.JavaScriptEncoder.UnsafeRelaxedJsonEscaping
            });
            File.WriteAllText(filePath, jsonSupplier, Encoding.UTF8);
        }
    }
}
