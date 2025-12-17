using BLL.Interfaces;
using DAL.Entities;
using Presentation.Commands;
using System;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace Presentation.ViewModels
{
    public class TransactionsViewModel : BaseViewModel
    {
        private readonly ITransactionService _service;

        public ObservableCollection<Transaction> Transactions { get; }
        public ObservableCollection<Category> Categories { get; }

        private decimal _amount;
        public decimal Amount
        {
            get => _amount;
            set
            {
                _amount = value;
                OnPropertyChanged();
            }
        }

        private Category _selectedCategory;
        public Category SelectedCategory
        {
            get => _selectedCategory;
            set
            {
                _selectedCategory = value;
                OnPropertyChanged();
            }
        }

        public ICommand AddCommand { get; }
        public ICommand DeleteCommand { get; }

        public TransactionsViewModel(
            ITransactionService service,
            ICategoryService categoryService)
        {
            _service = service;

            Transactions = new ObservableCollection<Transaction>(_service.GetAll());
            Categories = new ObservableCollection<Category>(categoryService.GetAll());

            AddCommand = new RelayCommand(_ => Add(), _ => CanAdd());
            DeleteCommand = new RelayCommand(obj => Delete(obj));
        }

        private bool CanAdd()
        {
            return Amount > 0 && SelectedCategory != null;
        }

        private void Add()
        {
            var transaction = new Transaction
            {
                Amount = Amount,
                CategoryId = SelectedCategory.Id,
                Date = DateTime.Now
            };

            _service.Add(transaction);
            Transactions.Add(transaction);

            Amount = 0;
            SelectedCategory = null;
        }

        private void Delete(object obj)
        {
            if (obj is Transaction transaction)
            {
                _service.Delete(transaction.Id);
                Transactions.Remove(transaction);
            }
        }
    }
}
