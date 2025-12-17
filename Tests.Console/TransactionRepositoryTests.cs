using DAL;
using DAL.Repositories;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace Tests.Console
{
    public class TransactionRepositoryTests
    {
        [Fact]
        public void GetAllByUser_DoesNotThrow()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase(databaseName: "TestDb")
                .Options;

            using var context = new AppDbContext(options);
            var repo = new TransactionRepository(context);

            // Act
            var ex = Record.Exception(() =>
            {
                repo.GetAllByUser(1);
            });

            // Assert
            Assert.Null(ex);
        }
    }
}
