using System.Windows.Input;
using Presentation.Commands;

namespace Presentation.ViewModels
{
    public class MainViewModel : BaseViewModel
    {
        public ICommand OpenTransactionsCommand { get; }

        public MainViewModel()
        {
            OpenTransactionsCommand = new RelayCommand(_ => OpenTransactions());
        }

        private void OpenTransactions()
        {
            // Навігаційний use case
            // UI → ViewModel → відкриття функціонального вікна
        }
    }
}
