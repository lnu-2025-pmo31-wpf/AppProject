using DAL.Entities;
using Npgsql;
using System;
using System.Collections.Generic;
using Common.Logging;

namespace DAL.Repositories
{
    public class TransactionRepository
    {
        private const string ConnStr =
            "Host=localhost;Port=5432;Database=money_manager;Username=postgres;Password=1111";

        public List<Transaction> GetAllByUser(int userId)
        {
            Log.Info($"DB GetAllByUser userId={userId}");
            var list = new List<Transaction>();

            try
            {
                using var conn = new NpgsqlConnection(ConnStr);
                conn.Open();

                using var cmd = new NpgsqlCommand(
                    "SELECT id, user_id, category_id, amount, date, note FROM transactions WHERE user_id=@u",
                    conn);

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
            }
            catch (Exception ex)
            {
                Log.Error("DB error GetAllByUser", ex);
                throw;
            }

            return list;
        }

        public int Add(Transaction t)
        {
            Log.Info($"DB Add transaction amount={t.Amount}");

            try
            {
                using var conn = new NpgsqlConnection(ConnStr);
                conn.Open();

                using var cmd = new NpgsqlCommand(
                    "INSERT INTO transactions(user_id, category_id, amount, date, note) VALUES (@u,@c,@a,@d,@n) RETURNING id",
                    conn);

                cmd.Parameters.AddWithValue("u", t.UserId);
                cmd.Parameters.AddWithValue("c", t.CategoryId);
                cmd.Parameters.AddWithValue("a", t.Amount);
                cmd.Parameters.AddWithValue("d", t.Date);
                cmd.Parameters.AddWithValue("n", (object?)t.Note ?? DBNull.Value);

                return (int)cmd.ExecuteScalar();
            }
            catch (Exception ex)
            {
                Log.Error("DB error Add transaction", ex);
                throw;
            }
        }

        public void Delete(int id)
        {
            Log.Warn($"DB Delete transaction id={id}");

            try
            {
                using var conn = new NpgsqlConnection(ConnStr);
                conn.Open();

                using var cmd = new NpgsqlCommand(
                    "DELETE FROM transactions WHERE id=@id", conn);

                cmd.Parameters.AddWithValue("id", id);
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                Log.Error("DB error Delete transaction", ex);
                throw;
            }
        }

        
        public void DeleteAllByUser(int userId)
        {
            Log.Warn($"DB Delete ALL transactions userId={userId}");

            try
            {
                using var conn = new NpgsqlConnection(ConnStr);
                conn.Open();

                using var cmd = new NpgsqlCommand(
                    "DELETE FROM transactions WHERE user_id=@u", conn);

                cmd.Parameters.AddWithValue("u", userId);
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                Log.Error("DB error DeleteAllByUser", ex);
                throw;
            }
        }
    }
}
