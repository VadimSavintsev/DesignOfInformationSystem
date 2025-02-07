using Npgsql;

public class DBConnection
{
    private static DBConnection instance;
    private NpgsqlConnection connection;

    private const string TABLE_NAME = "Suppliers";

    private string dbName;
    private string user;
    private string password;
    private string host;
    private string port;

    private DBConnection(string dbName, string user, string password, string host, string port)
    {
        this.dbName = dbName;
        this.user = user;
        this.password = password;
        this.host = host;
        this.port = port;
        Connect();
        CreateTableIfNotExists();
    }

    private void CreateTableIfNotExists()
    {
        string createTableSQL = $@"
            CREATE TABLE IF NOT EXISTS {TABLE_NAME} (
                Id SERIAL PRIMARY KEY,
                Name VARCHAR(100) NOT NULL,
                Address VARCHAR(200) NOT NULL,
                PhoneNumber VARCHAR(20) NOT NULL,
                Email VARCHAR(100) NOT NULL,
                Inn VARCHAR(12) NOT NULL,
                Ogrn VARCHAR(13) NOT NULL
            )";

        using (var conn = GetConnection())
        using (var cmd = new NpgsqlCommand(createTableSQL, conn))
        {
            try
            {
                cmd.ExecuteNonQuery();
                Console.WriteLine($"Таблица \"{TABLE_NAME}\" проверена/создана.");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                throw new Exception($"Ошибка при создании таблицы: {TABLE_NAME}");
            }
        }
    }

    private void Connect()
    {
        try
        {
            string connectionString = $"Host={host};Port={port};Database={dbName};Username={user};Password={password}";
            connection = new NpgsqlConnection(connectionString);
            connection.Open();
            Console.WriteLine("Соединение с базой данных установлено.");
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.ToString());
            throw new Exception("Ошибка подключения к базе данных");
        }
    }

    public static DBConnection GetInstance(string dbName, string user, string password, string host, string port)
    {
        if (instance == null)
        {
            lock (typeof(DBConnection))
            {
                if (instance == null)
                {
                    instance = new DBConnection(dbName, user, password, host, port);
                }
            }
        }
        return instance;
    }

    public NpgsqlConnection GetConnection()
    {
        try
        {
            if (connection == null || connection.State != System.Data.ConnectionState.Open)
            {
                Console.WriteLine("Соединение закрыто или отсутствует. Повторное подключение...");
                Connect();
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.ToString());
        }
        return connection;
    }
}
