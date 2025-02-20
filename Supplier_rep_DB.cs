using Npgsql;
using OOP;

public class Supplier_rep_DB
{
    private DBConnection dbConnection;

    public Supplier_rep_DB(DBConnection dbConnection)
    {
        this.dbConnection = dbConnection;
    }

    private NpgsqlConnection GetConnection()
    {
        return dbConnection.GetConnection();
    }
    
    public Supplier GetSupplierById(int supplierId)
    {
        var query = "SELECT * FROM Suppliers WHERE Id = @Id;";
        using (var connection = GetConnection())
        {
            try
            {
                using (var command = new NpgsqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Id", supplierId);
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

    public List<SupplierShort> GetKNSuppliers(int k, int n)
    {
        var query = "SELECT * FROM Suppliers ORDER BY Id LIMIT @Limit OFFSET @Offset;";
        using (var connection = GetConnection())
        {
            try
            {
                using (var command = new NpgsqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Limit", k);
                    command.Parameters.AddWithValue("@Offset", n);
                    using (var reader = command.ExecuteReader())
                    {
                        var suppliers = new List<SupplierShort>();
                        while (reader.Read())
                        {
                            var supplier = new SupplierShort(
                                id: reader.GetInt32(reader.GetOrdinal("Id")),
                                name: reader.GetString(reader.GetOrdinal("Name")),
                                phoneNumber: reader.GetString(reader.GetOrdinal("PhoneNumber")),
                                inn: reader.GetString(reader.GetOrdinal("Inn")),
                                ogrn: reader.GetString(reader.GetOrdinal("Ogrn"))
                            );

                            suppliers.Add(supplier);
                        }
                        return suppliers;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка при выполнении запроса: {ex.Message}");
                return new List<SupplierShort>();
            }
        }
    }

    public Supplier AddSupplier(Supplier supplier)
    {    
        var query = @"
        INSERT INTO Suppliers (Name, Address, PhoneNumber, Email, INN, OGRN)
        VALUES (@Name, @Address, @PhoneNumber, @Email, @INN, @OGRN)
        RETURNING Id;";

        using (var connection = GetConnection())
        {
            try
            {
                using (var command = new NpgsqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Name", supplier.Name);
                    command.Parameters.AddWithValue("@Address", supplier.Address);
                    command.Parameters.AddWithValue("@PhoneNumber", supplier.PhoneNumber);
                    command.Parameters.AddWithValue("@Email", supplier.Email);
                    command.Parameters.AddWithValue("@Inn", supplier.Inn);
                    command.Parameters.AddWithValue("@Ogrn", supplier.Ogrn);
                    var newId = (int)command.ExecuteScalar();
                    supplier.Id = newId;
                    return supplier;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка при добавлении поставщика: {ex.Message}");
                return null;
            }
        }
    }

    public bool ReplaceSupplier(int id, Supplier newSupplier)
    {
        var query = @"
            UPDATE Suppliers
            SET Name = @Name,
                Address = @Address,
                PhoneNumber = @PhoneNumber,
                Email = @Email,
                Inn = @Inn,
                Ogrn = @Ogrn
            WHERE Id = @Id;";

        using (var connection = GetConnection())
        {
            try
            {
                using (var command = new NpgsqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Id", id);
                    command.Parameters.AddWithValue("@Name", newSupplier.Name);
                    command.Parameters.AddWithValue("@Address", newSupplier.Address);
                    command.Parameters.AddWithValue("@PhoneNumber", newSupplier.PhoneNumber);
                    command.Parameters.AddWithValue("@Email", newSupplier.Email);
                    command.Parameters.AddWithValue("@Inn", newSupplier.Inn);
                    command.Parameters.AddWithValue("@Ogrn", newSupplier.Ogrn);

                    int rowsAffected = command.ExecuteNonQuery();
                    return rowsAffected > 0;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка при замене поставщика: {ex.Message}");
                return false;
            }
        }
    }

    public bool DeleteSupplier(int id)
    {
        var query = "DELETE FROM Suppliers WHERE Id = @Id;";
        using (var connection = GetConnection())
        {
            try
            {
                using (var command = new NpgsqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Id", id);
                    int rowsAffected = command.ExecuteNonQuery();
                    return rowsAffected > 0;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка при удалении поставщика: {ex.Message}");
                return false;
            }
        }
    }

    public int GetCount()
    {
        var query = "SELECT COUNT(*) FROM Suppliers;";
        using (var connection = new NpgsqlConnection(connectionString))
        {
            try
            {
                using (var command = new NpgsqlCommand(query, connection))
                {
                    return Convert.ToInt32(command.ExecuteScalar());
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка при получении количества поставщиков: {ex.Message}");
                return -1;
            }
        }
    }
}
