using System.Windows;
using BLL.Services;
using DAL.Repositories;

namespace Presentation.Views
{
    public partial class LoginWindow : Window
    {
        private readonly UserService _service = new();

        public LoginWindow()
        {
            InitializeComponent();
        }

        private void Login_Click(object sender, RoutedEventArgs e)
        {
            var username = UsernameBox.Text.Trim();
            var password = PasswordBox.Password;

            if (_service.Login(username, password))
            {
                // ✅ беремо Id користувача з БД
                var repo = new UserRepository();
                var user = repo.GetByUsername(username);

                AppSession.CurrentUserId = user!.Id;
                AppSession.CurrentUsername = user.Username;

                new MainWindow().Show();
                Close();
            }
            else
            {
                MessageBox.Show("Wrong username or password");
            }
        }

        private void Register_Click(object sender, RoutedEventArgs e)
        {
            new RegisterWindow().ShowDialog();
        }
    }
}
