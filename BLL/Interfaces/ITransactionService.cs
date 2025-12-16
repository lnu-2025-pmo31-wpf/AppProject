using DAL.Entities;

namespace BLL.Interfaces
{
    public interface ITransactionService
    {
        IEnumerable<Transaction> GetAll();
        void Add(Transaction transaction);
        void Delete(int id);
    }
}
