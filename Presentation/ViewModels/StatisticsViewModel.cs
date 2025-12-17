using System;
using System.Windows.Input;
using BLL.Interfaces;
using BLL.Services;
using Presentation.Commands;

namespace Presentation.ViewModels
{
    public class StatisticsViewModel : BaseViewModel
    {
        private readonly IStatisticsService _statisticsService;

        private decimal _totalIncome;
        private decimal _totalExpense;
        private decimal _balance;

        public decimal TotalIncome
        {
            get => _totalIncome;
            set { _totalIncome = value; OnPropertyChanged(); }
        }

        public decimal TotalExpense
        {
            get => _totalExpense;
            set { _totalExpense = value; OnPropertyChanged(); }
        }

        public decimal Balance
        {
            get => _balance;
            set { _balance = value; OnPropertyChanged(); }
        }

        public ICommand LoadWeekCommand { get; }
        public ICommand LoadMonthCommand { get; }
        public ICommand LoadYearCommand { get; }

        public StatisticsViewModel()
        {
            _statisticsService = new StatisticsService();

            LoadWeekCommand = new RelayCommand(_ => LoadWeek());
            LoadMonthCommand = new RelayCommand(_ => LoadMonth());
            LoadYearCommand = new RelayCommand(_ => LoadYear());

            LoadMonth();
        }

        private void LoadWeek()
        {
            var to = DateTime.Now;
            Load(to.AddDays(-7), to);
        }

        private void LoadMonth()
        {
            var now = DateTime.Now;
            Load(new DateTime(now.Year, now.Month, 1), now);
        }

        private void LoadYear()
        {
            var now = DateTime.Now;
            Load(new DateTime(now.Year, 1, 1), now);
        }

        private void Load(DateTime from, DateTime to)
        {
            var stats = _statisticsService.GetStatistics(
                AppSession.CurrentUserId,
                from,
                to
            );

            TotalIncome = stats.TotalIncome;
            TotalExpense = stats.TotalExpense;
            Balance = TotalIncome - TotalExpense;
        }
    }
}
