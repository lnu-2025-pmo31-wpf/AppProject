using System;
using System.Linq;
using BLL.Interfaces;
using Common.Logging;
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

            Log.Info("StatisticsService created");
        }

        public StatisticsResult GetStatistics(int userId, DateTime from, DateTime to)
        {
            Log.Info($"GetStatistics userId={userId} from={from:d} to={to:d}");

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
                {
                    Log.Warn($"Transaction without category: txId={t.Id}");
                    continue;
                }

                if (categories[t.CategoryId])
                    expense += t.Amount;
                else
                    income += t.Amount;
            }

            Log.Info($"Statistics result: income={income}, expense={expense}");

            return new StatisticsResult
            {
                TotalIncome = income,
                TotalExpense = expense
            };
        }
    }
}
