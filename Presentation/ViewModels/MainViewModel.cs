using Presentation.Commands;
using Presentation.Views.UserControls;
using System.Windows.Input;

namespace Presentation.ViewModels
{
    public class MainViewModel : BaseViewModel
    {
        private object _currentView;
        public object CurrentView
        {
            get => _currentView;
            set { _currentView = value; OnPropertyChanged(); }
        }

        public ICommand ShowTransactions { get; }
        public ICommand ShowCategories { get; }
        public ICommand ShowStatistics { get; }
        public ICommand ShowSettings { get; }

        public MainViewModel()
        {
            ShowTransactions = new RelayCommand(_ => CurrentView = new TransactionsView());
            ShowCategories = new RelayCommand(_ => CurrentView = new CategoriesView());
            ShowStatistics = new RelayCommand(_ => CurrentView = new StatisticsView());
            ShowSettings = new RelayCommand(_ => CurrentView = new SettingsView());

            CurrentView = new TransactionsView();
        }
    }
}
