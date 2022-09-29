using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Data;
using System.Globalization;
using System.Windows;
using System.Drawing;

namespace Vistas
{
    class ConversionStringToBrush : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            string color = "";
            if(value != null){
                switch (value.ToString())
                {
                    case "PENDIENTE":
                        color = "Red";
                        break;
                    case "PAGADA":
                        color = "Green";
                        break;
                    case "CONTABILIZADA":
                        color = "Blue";
                        break;
                    case "ANULADA":
                        color = "Gray";
                        break;
                }
            }
            return color;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
