using BLL.Services;
using System;

namespace BLL.Interfaces
{
    public interface IStatisticsService
    {
        StatisticsResult GetStatistics(int userId, DateTime from, DateTime to);
    }
}
