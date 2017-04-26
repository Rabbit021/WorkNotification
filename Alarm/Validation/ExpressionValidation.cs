using System.Globalization;
using System.Windows.Controls;
using Quartz;

namespace Alarm.Validation
{
    public class ExpressionValidation : ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            var str = value + "";
            if (CronExpression.IsValidExpression(str))
                return new ValidationResult(true, null);
            return new ValidationResult(false, "验证表达式");
        }
    }
}