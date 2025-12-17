using DAL.Entities;
using Npgsql;
using System;
using System.Collections.Generic;

namespace DAL.Repositories
{
    public class TransactionRepository
    {
        private const string ConnStr =
            "Host=localhost;Port=5432;Database=money_manager;Username=postgres;Password=1111";

        // =======================
        // GET
        // =======================
        public List<Transaction> GetAllByUser(int userId)
        {
            var list = new List<Transaction>();

            using var conn = new NpgsqlConnection(ConnStr);
            conn.Open();

            using var cmd = new NpgsqlCommand(@"
                SELECT id, user_id, category_id, amount, date, note
                FROM transactions
                WHERE user_id=@u
                ORDER BY date DESC, id DESC", conn);

            cmd.Parameters.AddWithValue("u", userId);

            using var r = cmd.ExecuteReader();
            while (r.Read())
            {
                list.Add(new Transaction
                {
                    Id = r.GetInt32(0),
                    UserId = r.GetInt32(1),
                    CategoryId = r.GetInt32(2),
                    Amount = r.GetDecimal(3),
                    Date = r.GetDateTime(4),
                    Note = r.IsDBNull(5) ? "" : r.GetString(5)
                });
            }

            return list;
        }

        // =======================
        // ADD  ✅ (ПОВЕРНУЛИ)
        // =======================
        public int Add(Transaction t)
        {
            using var conn = new NpgsqlConnection(ConnStr);
            conn.Open();

            using var cmd = new NpgsqlCommand(@"
                INSERT INTO transactions(user_id, category_id, amount, date, note)
                VALUES (@u, @c, @a, @d, @n)
                RETURNING id", conn);

            cmd.Parameters.AddWithValue("u", t.UserId);
            cmd.Parameters.AddWithValue("c", t.CategoryId);
            cmd.Parameters.AddWithValue("a", t.Amount);
            cmd.Parameters.AddWithValue("d", t.Date);
            cmd.Parameters.AddWithValue("n", (object?)t.Note ?? DBNull.Value);

            return (int)cmd.ExecuteScalar()!;
        }

        // =======================
        // DELETE ONE  ✅ (ПОВЕРНУЛИ)
        // =======================
        public void Delete(int id)
        {
            using var conn = new NpgsqlConnection(ConnStr);
            conn.Open();

            using var cmd = new NpgsqlCommand(
                "DELETE FROM transactions WHERE id=@id", conn);

            cmd.Parameters.AddWithValue("id", id);
            cmd.ExecuteNonQuery();
        }

        // =======================
        // DELETE ALL (Settings)
        // =======================
        public void DeleteAllByUser(int userId)
        {
            using var conn = new NpgsqlConnection(ConnStr);
            conn.Open();

            using var cmd = new NpgsqlCommand(
                "DELETE FROM transactions WHERE user_id=@u", conn);

            cmd.Parameters.AddWithValue("u", userId);
            cmd.ExecuteNonQuery();
        }
    }
}
