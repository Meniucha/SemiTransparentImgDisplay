using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;

namespace SemiTransparentImgDisplay.Helpers.Converters
{
    public class WindowStateToHeaderConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value != null)
            {
                WindowState state = (WindowState)value;

                return state == WindowState.Maximized ? Properties.ImageWindowControlContent.ContextMenuMinimizeItem : Properties.ImageWindowControlContent.ContextMenuMaximizeItem;
            }

            return "Error in " + nameof(WindowStateToHeaderConverter.Convert);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
