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
            Brush oBrush = null;
            switch (value.ToString())
            {
                case "PENDIENTE":
                    oBrush = Brushes.Red;
                    break;
                case "PAGADA":
                    oBrush = Brushes.Green;
                    break;
                case "CONTABILIZADA":
                    oBrush = Brushes.Blue;
                    break;
                case "ANULADA":
                    oBrush = Brushes.Gray;
                    break;
            }
            return oBrush;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
