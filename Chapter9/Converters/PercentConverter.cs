using System;
using System.Globalization;
using System.Windows.Data;

namespace Chapter9.Converters
{
	public class PercentConverter : IValueConverter
	{
		public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
		{
			double dValue = System.Convert.ToDouble(value);
			double dParameter = System.Convert.ToDouble(parameter);
			return dValue * dParameter;
		}

		public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
		{
			throw new NotImplementedException();
		}
	}
}
