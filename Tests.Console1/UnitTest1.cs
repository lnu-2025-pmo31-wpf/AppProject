using DAL.Repositories;
using Xunit;

namespace Tests.Console
{
    public class TransactionRepositoryTests
    {
        [Fact]
        public void GetAllByUser_DoesNotThrow()
        {
            var repo = new TransactionRepository();

            var exception = Record.Exception(() =>
            {
                repo.GetAllByUser(1);
            });

            Assert.Null(exception);
        }
    }
}
