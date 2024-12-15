using System;
using System.Data;
using Npgsql;

namespace Program
{
    class DBConnection
    {
        private static DBConnection instance; // Один экземпляр класса
        private NpgsqlConnection connection;

        private const string TableName = "Suppliers";

        private string DbName;
        private string User;
        private string Password;
        private string Host;
        private string Port;

        // Конструктор
        private DBConnection(string dbName, string user, string password, string host, string port)
        {
            DbName = dbName;
            User = user;
            Password = password;
            Host = host;
            Port = port;
            Connect();
            CreateTableIfNotExists();
        }

        // Метод для создания таблицы, если она не существует
        private void CreateTableIfNotExists()
        {
            string createTableSQL = $@"
                CREATE TABLE IF NOT EXISTS {TableName} (
                    Id SERIAL PRIMARY KEY,
                    Name VARCHAR(100) NOT NULL,
                    Address VARCHAR(255) NOT NULL,
                    PhoneNumber VARCHAR(20) NOT NULL,
                    Email VARCHAR(100) NOT NULL,
                    Inn VARCHAR(12) NOT NULL,
                    Ogrn VARCHAR(15) NOT NULL
                )";

            try
            {
                using (var conn = GetConnection())
                using (var cmd = new NpgsqlCommand(createTableSQL, conn))
                {
                    cmd.ExecuteNonQuery();
                    Console.WriteLine($"Таблица \"{TableName}\" проверена/создана.");
                }
            }
            catch (NpgsqlException e)
            {
                Console.WriteLine($"Ошибка при создании таблицы: {TableName}");
                Console.WriteLine(e.Message);
                throw;
            }
        }

        // Подключение к базе данных
        private void Connect()
        {
            try
            {
                string connectionString = $"Host={Host};Port={Port};Database={DbName};Username={User};Password={Password}";
                connection = new NpgsqlConnection(connectionString);
                connection.Open();
                Console.WriteLine("Соединение с базой данных установлено.");
            }
            catch (NpgsqlException e)
            {
                Console.WriteLine("Ошибка подключения к базе данных");
                Console.WriteLine(e.Message);
                throw;
            }
        }

        // Одиночка
        public static DBConnection GetInstance(string dbName, string user, string password, string host, string port)
        {
            if (instance == null)
            {
                instance = new DBConnection(dbName, user, password, host, port);
            }
            return instance;
        }

        // Получение соединения
        public NpgsqlConnection GetConnection()
        {
            if (connection == null || connection.State != ConnectionState.Open)
            {
                Console.WriteLine("Соединение закрыто или отсутствует. Повторное подключение...");
                Connect();
            }
            return connection;
        }
    }
}
