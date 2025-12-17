using System.Windows.Controls;
using Presentation.ViewModels;

namespace Presentation.Views.UserControls
{
    public partial class StatisticsView : UserControl
    {
        public StatisticsView()
        {
            InitializeComponent();
            DataContext = new StatisticsViewModel();
        }
    }
}
