using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Data;
using System.Windows.Controls;
using System.Globalization;
using System.Text.RegularExpressions;

namespace Vistas {
    public class ValidarNumero : ValidationRule {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo) {
            var validationResult = new ValidationResult(true, null);

            if (value != null) {
                if (!string.IsNullOrEmpty(value.ToString())) {
                    var regex = new Regex("[^0-9.-]+"); //regex that matches disallowed text
                    var parsingOk = !regex.IsMatch(value.ToString());
                    if (!parsingOk) {
                        validationResult = new ValidationResult(false, "Ingrese un número");
                    }
                }
            }

            return validationResult;
        }
    }
}
