using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Media;
using WPFExperiments.Enums;

namespace WPFExperiments.Converters
{
	public class ValueToColorConverter : IValueConverter
	{
		public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
		{
			// If the value is of type ExampleId
			if (value is ExampleId id)
			{
				// Explicitly set the color based on the value if possible
				if (id.Equals(ExampleId.REGULATED))
				{
					return new SolidColorBrush(Colors.Orange);
				}
				else if (id.Equals(ExampleId.NOT_REGULATED))
				{
					return new SolidColorBrush(Colors.Green);
				}
			}
			// If all else fails, return the transparent color
			return new SolidColorBrush(Colors.Transparent);
		}

		public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
		{
			throw new NotImplementedException();
		}
	}
}
