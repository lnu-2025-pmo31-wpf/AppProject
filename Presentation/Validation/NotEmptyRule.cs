using System.Globalization;
using System.Windows.Controls;

namespace Presentation.Validation
{
    public class NotEmptyRule : ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            var text = value?.ToString()?.Trim();
            if (string.IsNullOrWhiteSpace(text))
                return new ValidationResult(false, "Поле не може бути порожнім");

            return ValidationResult.ValidResult;
        }
    }
}
