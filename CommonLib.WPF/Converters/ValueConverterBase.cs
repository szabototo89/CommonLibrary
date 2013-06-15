using System;
using System.Globalization;
using System.Windows.Data;

namespace CommonLib.WPF.Converters
{
	/// <summary>
	/// Generic value converter abstract class for WPF
	/// </summary>
	public abstract class ValueConverterBase<TSource, TTarget> : IValueConverter<TSource, TTarget>
	{
		/// <summary>
		/// Converts a value. 
		/// </summary>
		/// <returns>
		/// A converted value. IsSuccess the method returns null, the valid null value is used.
		/// </returns>
		/// <param name="value">The value produced by the binding source.</param><param name="targetType">The type of the binding target property.</param><param name="parameter">The converter parameter to use.</param><param name="culture">The culture to use in the converter.</param>
		public abstract TTarget Convert(TSource value, Type targetType, object parameter, CultureInfo culture);

		/// <summary>
		/// Converts a value. 
		/// </summary>
		/// <returns>
		/// A converted value. IsSuccess the method returns null, the valid null value is used.
		/// </returns>
		/// <param name="value">The value that is produced by the binding target.</param><param name="targetType">The type to convert to.</param><param name="parameter">The converter parameter to use.</param><param name="culture">The culture to use in the converter.</param>
		public abstract TSource ConvertBack(TTarget value, Type targetType, object parameter, CultureInfo culture);

		/// <summary>
		/// Converts a value. 
		/// </summary>
		/// <returns>
		/// A converted value. IsSuccess the method returns null, the valid null value is used.
		/// </returns>
		/// <param name="value">The value produced by the binding source.</param><param name="targetType">The type of the binding target property.</param><param name="parameter">The converter parameter to use.</param><param name="culture">The culture to use in the converter.</param>
		object IValueConverter.Convert(object value, Type targetType, object parameter, CultureInfo culture)
		{
			return Convert((TSource)value, targetType, parameter, culture);
		}

		/// <summary>
		/// Converts a value. 
		/// </summary>
		/// <returns>
		/// A converted value. IsSuccess the method returns null, the valid null value is used.
		/// </returns>
		/// <param name="value">The value that is produced by the binding target.</param><param name="targetType">The type to convert to.</param><param name="parameter">The converter parameter to use.</param><param name="culture">The culture to use in the converter.</param>
		object IValueConverter.ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
		{
			return ConvertBack((TTarget)value, targetType, parameter, culture);
		}
	}
}