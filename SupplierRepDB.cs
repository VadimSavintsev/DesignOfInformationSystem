using System;
using System.Collections.Generic;
using Npgsql;
using Project;

namespace Program
{
    class SupplierRepDB
    {
        private DBConnection dbConnection; // Делегация для БД
        private const string TableName = "Suppliers";

        public SupplierRepDB(DBConnection dbConnection)
        {
            this.dbConnection = dbConnection;
        }

        private NpgsqlConnection GetConnection()
        {
            return dbConnection.GetConnection(); // Делегация к DBConnection
        }

        // Методы для исправления повтора кода
        // Заполнение PreparedStatement
        private void FillSupplierStatement(NpgsqlCommand cmd, Supplier supplier)
        {
            cmd.Parameters.AddWithValue("Name", supplier.GetName());
            cmd.Parameters.AddWithValue("Address", supplier.GetAddress());
            cmd.Parameters.AddWithValue("PhoneNumber", supplier.GetPhoneNumber());
            cmd.Parameters.AddWithValue("Email", supplier.GetEmail());
            cmd.Parameters.AddWithValue("Inn", supplier.GetInn());
            cmd.Parameters.AddWithValue("Ogrn", supplier.GetOgrn());
        }

        // Запись результата в объект
        private Supplier MapSupplier(NpgsqlDataReader reader)
        {
            return new Supplier(
                reader.GetInt32(reader.GetOrdinal("Id")),
                reader.GetString(reader.GetOrdinal("Name")),
                reader.GetString(reader.GetOrdinal("Address")),
                reader.GetString(reader.GetOrdinal("PhoneNumber")),
                reader.GetString(reader.GetOrdinal("Email")),
                reader.GetString(reader.GetOrdinal("Inn")),
                reader.GetString(reader.GetOrdinal("Ogrn"))
            );
        }

        private SupplierShort MapSupplierShort(NpgsqlDataReader reader)
        {
            return new SupplierShort(
                reader.GetInt32(reader.GetOrdinal("Id")),
                reader.GetString(reader.GetOrdinal("Name")),
                reader.GetString(reader.GetOrdinal("PhoneNumber")),
                reader.GetString(reader.GetOrdinal("Inn")),
                reader.GetString(reader.GetOrdinal("Ogrn"))
            );
        }

        // Проверка данных на уникальность
        private bool IsUnique(string inn, string ogrn)
        {
            string sql = $"SELECT COUNT(*) FROM {TableName} WHERE Inn = @Inn OR Ogrn = @Ogrn";
            using (var conn = GetConnection())
            using (var cmd = new NpgsqlCommand(sql, conn))
            {
                cmd.Parameters.AddWithValue("Inn", inn);
                cmd.Parameters.AddWithValue("Ogrn", ogrn);
                var count = Convert.ToInt32(cmd.ExecuteScalar());
                return count == 0; // Уникально, если результат = 0
            }
        }

        // Получение объекта по ID
        public Supplier GetObjectById(int id)
        {
            string sql = $"SELECT * FROM {TableName} WHERE Id = @Id";
            using (var conn = GetConnection())
            using (var cmd = new NpgsqlCommand(sql, conn))
            {
                cmd.Parameters.AddWithValue("Id", id);
                using (var reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        return MapSupplier(reader);
                    }
                }
            }
            return null;
        }

        // Получение списка k по счету n объектов
        public List<SupplierShort> GetKthNList(int k, int n)
        {
            var suppliersShort = new List<SupplierShort>();
            string sql = $"SELECT Id, Name, PhoneNumber, Inn FROM {TableName} ORDER BY Name OFFSET @Offset LIMIT @Limit";

            using (var conn = GetConnection())
            using (var cmd = new NpgsqlCommand(sql, conn))
            {
                cmd.Parameters.AddWithValue("Offset", (k - 1) * n);
                cmd.Parameters.AddWithValue("Limit", n);
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        suppliersShort.Add(MapSupplierShort(reader));
                    }
                }
            }

            return suppliersShort;
        }

        // Добавление объекта с автоматическим назначением ID
        public void AddSupplier(Supplier supplier)
        {
            if (!IsUnique(supplier.GetInn(),supplier.GetOgrn()))
            {
                throw new Exception("Поставщик с таким ИНН или ОГРН уже существует!");
            }

            string sql = $"INSERT INTO {TableName} (Name, Address, PhoneNumber, Email, Inn, Ogrn) VALUES (@Name, @Address, @PhoneNumber, @Email, @Inn, @Ogrn) RETURNING Id";
            using (var conn = GetConnection())
            using (var cmd = new NpgsqlCommand(sql, conn))
            {
                FillSupplierStatement(cmd, supplier);
                var generatedId = cmd.ExecuteScalar();
                if (generatedId != null)
                {
                    supplier.SetId(Convert.ToInt32(generatedId));
                    Console.WriteLine("Поставщик добавлен с ID: " + generatedId);
                }
            }
        }

        // Замена элемента по ID
        public bool ReplaceSupplierById(int id, Supplier newSupplier)
        {
            if (!IsUnique(newSupplier.GetInn(),newSupplier.GetOgrn()))
            {
                throw new Exception("Нельзя заменить поставщика: поставщик с таким ИНН или ОГРН уже существует!");
            }

            string sql = $"UPDATE {TableName} SET Name = @Name, Address = @Address, PhoneNumber = @PhoneNumber, Email = @Email, Inn = @Inn, Ogrn = @Ogrn WHERE Id = @Id";
            using (var conn = GetConnection())
            using (var cmd = new NpgsqlCommand(sql, conn))
            {
                FillSupplierStatement(cmd, newSupplier);
                cmd.Parameters.AddWithValue("Id", id);
                cmd.ExecuteNonQuery();
                return true;
            }
        }

        // Удаление элемента по ID
        public void DeleteSupplierById(int id)
        {
            string sql = $"DELETE FROM {TableName} WHERE Id = @Id";
            using (var conn = GetConnection())
            using (var cmd = new NpgsqlCommand(sql, conn))
            {
                cmd.Parameters.AddWithValue("Id", id);
                cmd.ExecuteNonQuery();
            }
        }

        // Получение количества элементов
        public int GetCount()
        {
            string sql = $"SELECT COUNT(*) FROM {TableName}";
            using (var conn = GetConnection())
            using (var cmd = new NpgsqlCommand(sql, conn))
            {
                return Convert.ToInt32(cmd.ExecuteScalar());
            }
        }
    }
}
