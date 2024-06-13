using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieAppTest.Converters
{
    public class AvatarConverter : IValueConverter
    {
        public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            string avatar = (string)value;
            if (string.IsNullOrEmpty(avatar))
            {
                avatar = "ic_nofoto.svg";
            }
            else
            {
                avatar = $"https://image.tmdb.org/t/p/original{avatar}";
            }
            return avatar;
        }

        public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            return value;
        }
    }
}
