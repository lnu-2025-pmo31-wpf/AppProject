using System.Windows;
using System.Windows.Controls;
using Presentation.ViewModels;

namespace Presentation.Views.UserControls
{
    public partial class SettingsView : UserControl
    {
        public SettingsView()
        {
            InitializeComponent();
            DataContext = new SettingsViewModel();
        }

        private void ChangePassword_Click(object sender, RoutedEventArgs e)
        {
            var vm = (SettingsViewModel)DataContext;
            vm.OldPassword = OldPasswordBox.Password;
            vm.NewPassword = NewPasswordBox.Password;
            vm.ChangePasswordCommand.Execute(null);
        }
    }
}
