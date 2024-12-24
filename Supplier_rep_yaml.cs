using System;
using System.Collections.Generic;
using System.IO;
using YamlDotNet.Serialization;
using YamlDotNet.Serialization.NamingConventions;
using Project;

namespace Program
{
    class Supplier_rep_yaml
    {
        private readonly string filePath;

        public Supplier_rep_yaml(string filePath)
        {
            this.filePath = filePath;
        }

        public List<Supplier> ReadAllValues()
        {
            if (!File.Exists(filePath))
            {
                throw new FileNotFoundException($"Файл {filePath} не найден.");
            }

            var deserializer = new DeserializerBuilder()
                .WithNamingConvention(CamelCaseNamingConvention.Instance) // Используем CamelCase для имен свойств
                .Build();

            using (var reader = new StreamReader(filePath))
            {
                var suppliers = deserializer.Deserialize<List<Supplier>>(reader);
                return suppliers;
            }
        }

        public void WriteAllValues(List<Supplier> suppliers)
        {
            var serializer = new SerializerBuilder()
                .WithNamingConvention(CamelCaseNamingConvention.Instance)
                .JsonCompatible()
                .Build();

            var yaml = serializer.Serialize(suppliers);
            File.WriteAllText(filePath, yaml);
        }
    }
}
