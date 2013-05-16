using System;
using System.Globalization;
using System.Windows.Data;

namespace CommonLib.WPF.Converters
{
	/// <summary>
	/// Generic value converter 
	/// </summary>
	public interface IValueConverter<TSource, TTarget> : IValueConverter
	{
		TTarget Convert(TSource value, Type targetType, object parameter, CultureInfo culture);
		TSource ConvertBack(TTarget value, Type targetType, object parameter, CultureInfo culture);
	}
}