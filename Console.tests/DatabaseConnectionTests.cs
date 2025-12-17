using Npgsql;
using Xunit;

public class DatabaseConnectionTests
{
    private const string ConnectionString =
        "Host=localhost;Port=5432;Username=postgres;Password=1111;Database=Money Manager";

    [Fact]
    public void CanOpenConnection()
    {
        using var conn = new NpgsqlConnection(ConnectionString);
        conn.Open();

        Assert.Equal(System.Data.ConnectionState.Open, conn.State);
    }

    [Fact]
    public void CanReadUsers()
    {
        using var conn = new NpgsqlConnection(ConnectionString);
        conn.Open();

        using var cmd = new NpgsqlCommand("SELECT COUNT(*) FROM users", conn);
        var count = (long)cmd.ExecuteScalar();

        Assert.True(count >= 0);
    }
}
