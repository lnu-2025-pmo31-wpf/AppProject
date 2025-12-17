using BLL.Services;
using DAL;
using DAL.Entities;
using Microsoft.EntityFrameworkCore;
using Xunit;

public class CategoryServiceTests
{
    [Fact]
    public void GetAll_ReturnsCategories()
    {
        var options = new DbContextOptionsBuilder<AppDbContext>()
            .UseInMemoryDatabase("CategoryDb")
            .Options;

        using var context = new AppDbContext(options);
        context.Categories.Add(new Category { Name = "Food", IsExpense = true });
        context.SaveChanges();

        var service = new CategoryService(context);

        var result = service.GetAll();

        Assert.Single(result);
    }
}
