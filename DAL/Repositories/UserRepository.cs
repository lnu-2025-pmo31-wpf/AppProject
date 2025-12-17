using DAL.Entities;
using Npgsql;

namespace DAL.Repositories
{
    public class UserRepository
    {
        private const string ConnStr =
            "Host=localhost;Port=5432;Database=money_manager;Username=postgres;Password=1111";

        public User? GetByUsername(string username)
        {
            using var conn = new NpgsqlConnection(ConnStr);
            conn.Open();

            using var cmd = new NpgsqlCommand(
                "SELECT id, username, password_hash FROM users WHERE username=@u", conn);
            cmd.Parameters.AddWithValue("u", username);

            using var r = cmd.ExecuteReader();
            if (!r.Read()) return null;

            return new User
            {
                Id = r.GetInt32(0),
                Username = r.GetString(1),
                PasswordHash = r.GetString(2)
            };
        }

        public User? GetById(int id)
        {
            using var conn = new NpgsqlConnection(ConnStr);
            conn.Open();

            using var cmd = new NpgsqlCommand(
                "SELECT id, username, password_hash FROM users WHERE id=@id", conn);
            cmd.Parameters.AddWithValue("id", id);

            using var r = cmd.ExecuteReader();
            if (!r.Read()) return null;

            return new User
            {
                Id = r.GetInt32(0),
                Username = r.GetString(1),
                PasswordHash = r.GetString(2)
            };
        }

        public bool Exists(string username)
        {
            using var conn = new NpgsqlConnection(ConnStr);
            conn.Open();

            using var cmd = new NpgsqlCommand(
                "SELECT 1 FROM users WHERE username=@u", conn);
            cmd.Parameters.AddWithValue("u", username);

            return cmd.ExecuteScalar() != null;
        }

        public void Add(User user)
        {
            using var conn = new NpgsqlConnection(ConnStr);
            conn.Open();

            using var cmd = new NpgsqlCommand(
                "INSERT INTO users(username, password_hash) VALUES (@u, @p)", conn);
            cmd.Parameters.AddWithValue("u", user.Username);
            cmd.Parameters.AddWithValue("p", user.PasswordHash);
            cmd.ExecuteNonQuery();
        }

        public void UpdatePassword(int userId, string hash)
        {
            using var conn = new NpgsqlConnection(ConnStr);
            conn.Open();

            using var cmd = new NpgsqlCommand(
                "UPDATE users SET password_hash=@p WHERE id=@id", conn);
            cmd.Parameters.AddWithValue("id", userId);
            cmd.Parameters.AddWithValue("p", hash);
            cmd.ExecuteNonQuery();
        }
    }
}
