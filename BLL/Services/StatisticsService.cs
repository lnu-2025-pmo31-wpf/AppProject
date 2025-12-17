using System;
using System.Linq;
using BLL.Interfaces;
using DAL.Repositories;

namespace BLL.Services
{
    public class StatisticsService : IStatisticsService
    {
        private readonly TransactionRepository _transactionRepo;
        private readonly CategoryRepository _categoryRepo;

        public StatisticsService()
        {
            _transactionRepo = new TransactionRepository();
            _categoryRepo = new CategoryRepository();
        }

        public StatisticsResult GetStatistics(int userId, DateTime from, DateTime to)
        {
            var transactions = _transactionRepo
                .GetAllByUser(userId)
                .Where(t => t.Date >= from && t.Date <= to)
                .ToList();

            var categories = _categoryRepo.GetAll()
                .ToDictionary(c => c.Id, c => c.IsExpense);

            decimal income = 0;
            decimal expense = 0;

            foreach (var t in transactions)
            {
                if (!categories.ContainsKey(t.CategoryId))
                    continue;

                if (categories[t.CategoryId])
                    expense += t.Amount;
                else
                    income += t.Amount;
            }

            return new StatisticsResult
            {
                TotalIncome = income,
                TotalExpense = expense
            };
        }
    }
}
