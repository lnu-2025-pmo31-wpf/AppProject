using System.Globalization;
using System.Windows.Controls;

namespace Presentation.Validation
{
    public class NotEmptyRule : ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            if (value == null || string.IsNullOrWhiteSpace(value.ToString()))
            {
                return new ValidationResult(false, "Поле не може бути пустим");
            }

            return ValidationResult.ValidResult;
        }
    }
}
