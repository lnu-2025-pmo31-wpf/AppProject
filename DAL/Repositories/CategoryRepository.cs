using DAL.Entities;
using Npgsql;
using System.Collections.Generic;

namespace DAL.Repositories
{
    public class CategoryRepository
    {
        private const string ConnStr =
            "Host=localhost;Port=5432;Database=money_manager;Username=postgres;Password=1111";

        public List<Category> GetAll()
        {
            var list = new List<Category>();

            using var conn = new NpgsqlConnection(ConnStr);
            conn.Open();

            using var cmd = new NpgsqlCommand(
                "SELECT id, name, is_expense FROM categories ORDER BY id", conn);

            using var r = cmd.ExecuteReader();
            while (r.Read())
            {
                list.Add(new Category
                {
                    Id = r.GetInt32(0),
                    Name = r.GetString(1),
                    IsExpense = r.GetBoolean(2)
                });
            }

            return list;
        }

        public void Add(Category c)
        {
            using var conn = new NpgsqlConnection(ConnStr);
            conn.Open();

            using var cmd = new NpgsqlCommand(
                "INSERT INTO categories(name, is_expense) VALUES (@n, @e)", conn);

            cmd.Parameters.AddWithValue("n", c.Name);
            cmd.Parameters.AddWithValue("e", c.IsExpense);

            cmd.ExecuteNonQuery();
        }
    }
}
