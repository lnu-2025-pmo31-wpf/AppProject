using BLL.Services;
using DAL.Repositories;
using Presentation.Commands;
using Presentation.Views;
using System.Windows;
using System.Windows.Input;

namespace Presentation.ViewModels
{
    public class SettingsViewModel : BaseViewModel
    {
        private readonly UserService _userService = new();
        private readonly TransactionRepository _transactionRepo = new();

        public string Username => AppSession.CurrentUsername;

        public string OldPassword { get; set; }
        public string NewPassword { get; set; }

        public ICommand ChangePasswordCommand { get; }
        public ICommand ClearTransactionsCommand { get; }
        public ICommand LogoutCommand { get; }

        public SettingsViewModel()
        {
            ChangePasswordCommand = new RelayCommand(_ => ChangePassword());
            ClearTransactionsCommand = new RelayCommand(_ => ClearTransactions());
            LogoutCommand = new RelayCommand(_ => Logout());
        }

        private void ChangePassword()
        {
            if (_userService.ChangePassword(
                AppSession.CurrentUserId,
                OldPassword,
                NewPassword))
            {
                MessageBox.Show("Password changed");
                OldPassword = NewPassword = "";
            }
            else
            {
                MessageBox.Show("Wrong old password");
            }
        }

        private void ClearTransactions()
        {
            if (MessageBox.Show(
                "Delete ALL transactions?",
                "Confirm",
                MessageBoxButton.YesNo) != MessageBoxResult.Yes)
                return;

            _transactionRepo.DeleteAllByUser(AppSession.CurrentUserId);
            MessageBox.Show("Transactions cleared");
        }

        private void Logout()
        {
            AppSession.CurrentUserId = 0;
            AppSession.CurrentUsername = "";

            var main = Application.Current.MainWindow;
            new LoginWindow().Show();
            main?.Close();
        }
    }
}
