using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Controls;
using System.Globalization;

namespace Vistas.Resources {
    class ValidacionGenericaTextBox : ValidationRule {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo) {
            if (((string)value).Length < 0) {
                return new ValidationResult(false, "Campo obligatorio.");
            } else {
                return new ValidationResult(true, null);
            }
        }
    }
}
