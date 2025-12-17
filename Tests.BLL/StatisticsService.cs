using System;
using BLL.Services;
using Xunit;

namespace Tests.BLL
{
    public class StatisticsServiceTests
    {
        [Fact]
        public void GetStatistics_ReturnsValidResult()
        {
            var service = new StatisticsService();

            var from = DateTime.Now.AddMonths(-1);
            var to = DateTime.Now;

            var result = service.GetStatistics(1, from, to);

            Assert.True(result.TotalIncome >= 0);
            Assert.True(result.TotalExpense >= 0);
        }
    }
}
