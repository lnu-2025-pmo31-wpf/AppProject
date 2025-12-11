namespace FinanceManager.DAL.Entities
{
    public enum TransactionType
    {
        Income,  // дохід
        Expense  // витрата
    }

    public class Transaction
    {
        public int Id { get; set; }                // унікальний ідентифікатор
        public string Description { get; set; }    // опис транзакції
        public decimal Amount { get; set; }        // сума
        public TransactionType Type { get; set; }  // тип транзакції
        public DateTime Date { get; set; }         // дата транзакції
    }
}
