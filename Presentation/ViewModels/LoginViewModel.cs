using BLL.Services;
using DAL.Repositories;
using Presentation.Commands;
using System.Windows;
using System.Windows.Input;

namespace Presentation.ViewModels
{
    public class LoginViewModel
    {
        private readonly UserService _service = new();

        public string Username { get; set; }
        public string Password { get; set; }

        public ICommand LoginCommand => new RelayCommand(_ => Login());

        private void Login()
        {
            var username = (Username ?? "").Trim();
            var password = Password ?? "";

            if (_service.Login(username, password))
            {
                // беремо user з БД, щоб дістати Id
                var repo = new UserRepository();
                var user = repo.GetByUsername(username);

                if (user == null)
                {
                    MessageBox.Show("User not found");
                    return;
                }

                AppSession.CurrentUserId = user.Id;
                AppSession.CurrentUsername = user.Username;

                new MainWindow().Show();
                Application.Current.Windows[0]?.Close();
            }
            else
            {
                MessageBox.Show("Wrong username or password");
            }
        }
    }
}
