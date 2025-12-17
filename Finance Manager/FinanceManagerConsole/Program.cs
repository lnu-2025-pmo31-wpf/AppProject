// <copyright file="Program.cs" company="FinanceManager">
// Copyright (c) FinanceManager. All rights reserved.
// </copyright>

namespace FinanceManagerConsole
{
    using System;
    using Npgsql;

    /// <summary>
    /// Entry point of the application.
    /// </summary>
    internal static class Program
    {
        /// <summary>
        /// Application main method.
        /// </summary>
        private static void Main()
        {
            string connectionString =
                "Host=localhost;Port=5432;Username=postgres;Password=1111;Database=\"Money Manager\"";

            using var conn = new NpgsqlConnection(connectionString);
            conn.Open();
            Console.WriteLine("Connected to PostgreSQL!");

            // Генерація тестових даних
            // InsertTestData(conn);

            // Виведення таблиць
            PrintUsers(conn);
            PrintCategories(conn);
            PrintTransactions(conn);
        }

        // --------- METHODS ------------
        private static void PrintUsers(NpgsqlConnection conn)
        {
            Console.WriteLine("\n=== USERS ===");

            string sql = "SELECT id, name, email FROM users";
            using var cmd = new NpgsqlCommand(sql, conn);
            using var reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                Console.WriteLine($"{reader["id"]} | {reader["name"]} | {reader["email"]}");
            }
        }

        private static void PrintCategories(NpgsqlConnection conn)
        {
            Console.WriteLine("\n=== CATEGORIES ===");

            string sql = "SELECT id, name, is_expense FROM categories";
            using var cmd = new NpgsqlCommand(sql, conn);
            using var reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                Console.WriteLine(
                    $"{reader["id"]} | {reader["name"]} | Expense: {reader["is_expense"]}");
            }
        }

        private static void PrintTransactions(NpgsqlConnection conn)
        {
            Console.WriteLine("\n=== TRANSACTIONS ===");

            string sql = "SELECT id, user_id, category_id, amount, type, date FROM transactions";
            using var cmd = new NpgsqlCommand(sql, conn);
            using var reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                Console.WriteLine(
                    $"{reader["id"]} | User {reader["user_id"]} | Cat {reader["category_id"]} | {reader["amount"]}");
            }
        }

        private static void InsertTestData(NpgsqlConnection conn)
        {
            Random r = new Random();

            // === Users ===
            for (int i = 0; i < 10; i++)
            {
                using var cmd = new NpgsqlCommand(
                    "INSERT INTO users (name, email) VALUES (@n, @e)",
                    conn);

                cmd.Parameters.AddWithValue("@n", "User" + r.Next(1000));
                cmd.Parameters.AddWithValue("@e", $"user{r.Next(9999)}@mail.com");
                cmd.ExecuteNonQuery();
            }

            // === Categories ===
            string[] cats = { "Food", "Transport", "Salary", "Rent", "Gifts" };
            for (int i = 0; i < cats.Length; i++)
            {
                using var cmd = new NpgsqlCommand(
                    "INSERT INTO categories (name, is_expense) VALUES (@n, @e)",
                    conn);

                cmd.Parameters.AddWithValue("@n", cats[i]);
                cmd.Parameters.AddWithValue("@e", i < 3);
                cmd.ExecuteNonQuery();
            }

            // === Transactions ===
            for (int i = 0; i < 50; i++)
            {
                bool exp = r.Next(2) == 1;

                using var cmd = new NpgsqlCommand(
                    @"INSERT INTO transactions
                      (user_id, category_id, amount, type, date)
                      VALUES (@u, @c, @a, @t, @d)",
                    conn);

                cmd.Parameters.AddWithValue("@u", r.Next(1, 11));
                cmd.Parameters.AddWithValue("@c", r.Next(1, cats.Length + 1));
                cmd.Parameters.AddWithValue("@a", Math.Round(r.NextDouble() * 300, 2));
                cmd.Parameters.AddWithValue("@t", exp ? "expense" : "income");
                cmd.Parameters.AddWithValue("@d", DateTime.Now.AddDays(-r.Next(100)));

                cmd.ExecuteNonQuery();
            }

            Console.WriteLine("Test data added!");
        }
    }
}
