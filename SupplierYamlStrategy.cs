using System.Collections.Generic;
using System.IO;
using System.Linq;
using YamlDotNet.Serialization;
using YamlDotNet.Serialization.NamingConventions;
using Project;

namespace Program
{
     class SupplierYamlStrategy : SupplierStrategy
    {
       private readonly string yamlfilePath;

       public SupplierYamlStrategy(string yamlfilePath)
        {
            this.yamlfilePath = yamlfilePath;
        }

        // Чтение всех значений из файла
        public List<Supplier> ReadAllValues()
        {    
            try
            {
                var yaml = File.ReadAllText(_filePath);
                var deserializer = new DeserializerBuilder()
                    .WithNamingConvention(CamelCaseNamingConvention.Instance)
                    .Build();
                return deserializer.Deserialize<List<Supplier>>(yaml);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error reading YAML file: {ex.Message}");
                return new List<Supplier>(); // Возвращаем пустой список в случае ошибки
            }
        }

        // Запись всех значений в файл
        public void WriteAllValues(List<Supplier> data)
        {
            try
            {
                var serializer = new SerializerBuilder()
                    .WithNamingConvention(CamelCaseNamingConvention.Instance)
                    .Build();
                var yaml = serializer.Serialize(suppliers);
                File.WriteAllText(_filePath, yaml);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error writing YAML file: {ex.Message}");
            }
        }
    }
}
