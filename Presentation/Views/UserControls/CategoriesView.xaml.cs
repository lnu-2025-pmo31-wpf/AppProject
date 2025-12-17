using DAL.Entities;
using DAL.Repositories;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;

namespace Presentation.Views.UserControls
{
    public partial class CategoriesView : UserControl
    {
        private readonly CategoryRepository _repo = new();
        private List<Category> _categories = new();

        public CategoriesView()
        {
            InitializeComponent();
            TypeBox.SelectedIndex = 0;
            Load();
        }

        private void Load()
        {
            _categories = _repo.GetAll();
            GridCats.ItemsSource = _categories;
        }

        private void Add_Click(object sender, RoutedEventArgs e)
        {
            var name = NameBox.Text.Trim();
            if (name.Length == 0)
            {
                MessageBox.Show("Enter category name");
                return;
            }

            var isExpense =
                (TypeBox.SelectedItem as ComboBoxItem)?.Content?.ToString() == "Expense";

            _repo.Add(new Category
            {
                Name = name,
                IsExpense = isExpense
            });

            NameBox.Text = "";
            Load();
        }
    }
}
