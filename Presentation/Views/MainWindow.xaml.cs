using Presentation.Views.UserControls;
using System.Windows;

namespace Presentation
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            MainContent.Content = new TransactionsView();
        }

        private void Transactions_Click(object sender, RoutedEventArgs e)
            => MainContent.Content = new TransactionsView();

        private void Categories_Click(object sender, RoutedEventArgs e)
            => MainContent.Content = new CategoriesView();

        private void Statistics_Click(object sender, RoutedEventArgs e)
            => MainContent.Content = new StatisticsView();

        private void Settings_Click(object sender, RoutedEventArgs e)
            => MainContent.Content = new SettingsView();
    }
}
