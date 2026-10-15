using MySql.Data.MySqlClient;
using System.Data;

public class MySqlContext
{
    private readonly string _connectionString;

    public MySqlContext(string connectionString)
    {
        _connectionString = connectionString;
    }

    public DataTable ExecuteQuery(string query)
    {
        var dt = new DataTable();

        using var conn = new MySqlConnection(_connectionString);
        conn.Open();

        using var cmd = new MySqlCommand(query, conn);
        using var reader = cmd.ExecuteReader();

        dt.Load(reader);
        return dt;
    }

    public void ExecuteNonQuery(string query)
    {
        using var conn = new MySqlConnection(_connectionString);
        conn.Open();

        using var cmd = new MySqlCommand(query, conn);
        cmd.ExecuteNonQuery();
    }
}
