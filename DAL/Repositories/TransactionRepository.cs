using System.Collections.Generic;
using System.Linq;
using FinanceManager.DAL.Entities;

namespace FinanceManager.DAL.Repositories
{
    public class TransactionRepository
    {
        private readonly FinanceDbContext _context;

        public TransactionRepository(FinanceDbContext context)
        {
            _context = context;
        }

        // Отримати всі транзакції
        public List<Transaction> GetAll()
        {
            return _context.Transactions.ToList();
        }

        // Додати транзакцію
        public void Add(Transaction transaction)
        {
            _context.Transactions.Add(transaction);
            _context.SaveChanges();
        }

        // Видалити транзакцію
        public void Delete(Transaction transaction)
        {
            _context.Transactions.Remove(transaction);
            _context.SaveChanges();
        }

        // За потреби можна додати оновлення
        public void Update(Transaction transaction)
        {
            _context.Transactions.Update(transaction);
            _context.SaveChanges();
        }
    }
}
