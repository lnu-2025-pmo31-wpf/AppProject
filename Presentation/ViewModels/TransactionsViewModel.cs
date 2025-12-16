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

        public decimal Amount { get; set; }
        public Category SelectedCategory { get; set; }

        public ICommand AddCommand { get; }
        public ICommand DeleteCommand { get; }

        public TransactionsViewModel(
            ITransactionService service,
            ICategoryService categoryService) // якщо нема — скажи
        {
            _service = service;

            Transactions = new ObservableCollection<Transaction>(_service.GetAll());
            Categories = new ObservableCollection<Category>(categoryService.GetAll());

            AddCommand = new RelayCommand(_ => Add());
            DeleteCommand = new RelayCommand(obj => Delete(obj));
        }

        private void Add()
        {
            if (SelectedCategory == null) return;

            var transaction = new Transaction
            {
                Amount = Amount,
                CategoryId = SelectedCategory.Id,
                Date = DateTime.Now
            };

            _service.Add(transaction);
            Transactions.Add(transaction);
        }

        private void Delete(object obj)
        {
            if (obj is Transaction t)
            {
                _service.Delete(t.Id);
                Transactions.Remove(t);
            }
        }
    }
}
