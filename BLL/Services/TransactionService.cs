using DAL;
using DAL.Entities;
using BLL.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace BLL.Services
{
    public class TransactionService : ITransactionService
    {
        private readonly AppDbContext _context;

        public TransactionService(AppDbContext context)
        {
            _context = context;
        }

        public IEnumerable<Transaction> GetAll()
        {
            return _context.Transactions
                .Include(t => t.Category)
                .Include(t => t.User)
                .ToList();
        }

        public void Add(Transaction transaction)
        {
            _context.Transactions.Add(transaction);
            _context.SaveChanges();
        }

        public void Delete(int id)
        {
            var t = _context.Transactions.Find(id);
            if (t != null)
            {
                _context.Transactions.Remove(t);
                _context.SaveChanges();
            }
        }
    }
}
