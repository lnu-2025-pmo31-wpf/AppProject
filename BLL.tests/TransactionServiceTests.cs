using BLL.Services;
using DAL;
using DAL.Entities;
using Microsoft.EntityFrameworkCore;
using Xunit;

public class TransactionServiceTests
{
    private AppDbContext GetDbContext()
    {
        var options = new DbContextOptionsBuilder<AppDbContext>()
            .UseInMemoryDatabase(databaseName: "TestDb")
            .Options;

        return new AppDbContext(options);
    }

    [Fact]
    public void Add_ShouldAddTransaction()
    {
        // Arrange
        var context = GetDbContext();
        var service = new TransactionService(context);

        var transaction = new Transaction
        {
            UserId = 1,
            CategoryId = 1,
            Amount = 100,
            Type = "expense",
            Date = DateTime.Now
        };

        // Act
        service.Add(transaction);

        // Assert
        Assert.Single(context.Transactions);
    }

    [Fact]
    public void GetAll_ShouldReturnTransactions()
    {
        var context = GetDbContext();
        context.Transactions.Add(new Transaction
        {
            UserId = 1,
            CategoryId = 1,
            Amount = 50,
            Type = "income",
            Date = DateTime.Now
        });
        context.SaveChanges();

        var service = new TransactionService(context);

        var result = service.GetAll();

        Assert.Single(result);
    }

    [Fact]
    public void Delete_ShouldRemoveTransaction()
    {
        var context = GetDbContext();
        var t = new Transaction
        {
            UserId = 1,
            CategoryId = 1,
            Amount = 30,
            Type = "expense",
            Date = DateTime.Now
        };
        context.Transactions.Add(t);
        context.SaveChanges();

        var service = new TransactionService(context);

        service.Delete(t.Id);

        Assert.Empty(context.Transactions);
    }
}
