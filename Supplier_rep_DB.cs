using Npgsql;
using OOP;

public class Supplier_rep_DB
{
    private readonly string connectionString;

    public Supplier_rep_DB(string connectionString)
    {
        connectionString = connectionString;
    }
    public Supplier GetSupplierById(int supplierId)
    {
        var query = "SELECT * FROM Suppliers WHERE SupplierID = @SupplierID;";

        using (var connection = new NpgsqlConnection(connectionString))
        {
            try
            {
                connection.Open();

                using (var command = new NpgsqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@SupplierID", supplierId);

                    using (var reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            var supplier = new Supplier(
                                id: reader.GetInt32(reader.GetOrdinal("Id")),
                                name: reader.GetString(reader.GetOrdinal("Name")),
                                address: reader.GetString(reader.GetOrdinal("Address")),
                                phoneNumber: reader.GetString(reader.GetOrdinal("PhoneNumber")),
                                email: reader.GetString(reader.GetOrdinal("Email")),
                                inn: reader.GetString(reader.GetOrdinal("Inn")),
                                ogrn: reader.GetString(reader.GetOrdinal("Ogrn"))
                            );

                            return supplier;
                        }
                        else
                        {
                            return null;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка при выполнении запроса: {ex.Message}");
                return null;
            }
        }
    }
}
