using System.Windows;
using Presentation.ViewModels;

namespace Presentation.Views
{
    public partial class TransactionsWindow : Window
    {
        public TransactionsWindow(TransactionsViewModel vm)
        {
            InitializeComponent();
            DataContext = vm;
        }
    }
}
