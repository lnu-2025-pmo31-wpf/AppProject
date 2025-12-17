using System.Windows;
using BLL.Services;

namespace Presentation.Views
{
    public partial class RegisterWindow : Window
    {
        private readonly UserService _service = new();

        public RegisterWindow()
        {
            InitializeComponent();
        }

        private void Register_Click(object sender, RoutedEventArgs e)
        {
            if (_service.Register(UsernameBox.Text, PasswordBox.Password))
            {
                MessageBox.Show("Account created");
                Close();
            }
            else
            {
                MessageBox.Show("User already exists");
            }
        }
    }
}
