using System.Linq;
using DAL;
using DAL.Entities;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace Tests.BLL
{
    public class CategoryServiceTests
    {
        [Fact]
        public void InMemory_Works()
        {
            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase(databaseName: "CategoryDb_Test1")
                .Options;

            using var ctx = new AppDbContext(options);
            ctx.Categories.Add(new Category { Name = "Food", IsExpense = true });
            ctx.SaveChanges();

            Assert.Equal(1, ctx.Categories.Count());
        }
    }
}
