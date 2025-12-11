using FinanceManager.DAL.Entities;
using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Media;

namespace FinanceManager.Presentation.Converters
{
    public class AmountColorConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            // Якщо це сума (decimal) або тип транзакції, перевіряємо логіку
            // Тут припускаємо, що ми біндимося до самого об'єкта Transaction, щоб знати Type
            // Але простіше біндити колір до TransactionType

            if (value is TransactionType type)
            {
                return type == TransactionType.Income ? Brushes.Green : Brushes.Red;
            }
            return Brushes.Black;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}