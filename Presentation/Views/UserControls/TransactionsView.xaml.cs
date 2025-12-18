using DAL.Entities;
using DAL.Repositories;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Animation;

namespace Presentation.Views.UserControls
{
    public partial class TransactionsView : UserControl
    {
        private readonly CategoryRepository _catRepo = new();
        private readonly TransactionRepository _txRepo = new();

        private List<Category> _categories = new();
        private List<Transaction> _transactions = new();

        public TransactionsView()
        {
            InitializeComponent();

            TypeBox.SelectedIndex = 0;
            LoadCategories();
            LoadTransactions();
        }

        private void ShowToast(string text)
        {
            ToastText.Text = text;
            ((Storyboard)FindResource("ToastAnim")).Begin();
        }

        private void LoadCategories()
        {
            try
            {
                _categories = _catRepo.GetAll();
                CategoryBox.ItemsSource = _categories;
                if (_categories.Count > 0)
                    CategoryBox.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Failed to load categories:\n" + ex.Message);
            }
        }

        private void LoadTransactions()
        {
            try
            {
                _transactions = _txRepo.GetAllByUser(AppSession.CurrentUserId);
                GridTx.ItemsSource = _transactions;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Failed to load transactions:\n" + ex.Message);
            }
        }

        private void Add_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (CategoryBox.SelectedItem is not Category cat)
                {
                    MessageBox.Show("Select category");
                    return;
                }

                if (!decimal.TryParse(
                        AmountBox.Text.Replace(',', '.'),
                        NumberStyles.Any,
                        CultureInfo.InvariantCulture,
                        out var amount))
                {
                    MessageBox.Show("Amount must be a number");
                    return;
                }

                var tx = new Transaction
                {
                    UserId = AppSession.CurrentUserId,
                    CategoryId = cat.Id,
                    Amount = Math.Abs(amount),
                    Date = DateTime.Now,
                    Note = NoteBox.Text?.Trim() ?? ""
                };

                tx.Id = _txRepo.Add(tx);

                AmountBox.Text = "";
                NoteBox.Text = "";
                LoadTransactions();

                ShowToast("Transaction saved ✅");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Failed to add transaction:\n" + ex.Message);
            }
        }

        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (GridTx.SelectedItem is not Transaction tx)
                {
                    MessageBox.Show("Select transaction to delete");
                    return;
                }

                _txRepo.Delete(tx.Id);
                LoadTransactions();

                ShowToast("Transaction deleted 🗑️");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Failed to delete:\n" + ex.Message);
            }
        }
    }
}
