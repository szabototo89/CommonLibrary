using System;
using System.Globalization;

namespace CommonLib.WPF.Converters
{
	/// <summary>
	/// Value converter which uses lambda functions
	/// </summary>
	public class ValueConverter<TSource, TTarget> : ValueConverterBase<TSource, TTarget>
	{
		public Func<TSource, Type, object, CultureInfo, TTarget> ConvertToFunction { get; protected set; }
		public Func<TTarget, Type, object, CultureInfo, TSource> ConvertBackFunction { get; protected set; }

		public ValueConverter(Func<TSource, TTarget> convertTo)
			: this(convertTo, value => { throw new NotImplementedException(); })
		{
		}

		public ValueConverter(Func<TSource, TTarget> convertTo, Func<TTarget, TSource> convertBack)
		{
			if (convertTo == null)
				throw new ArgumentNullException("convertTo");
			if (convertBack == null)
				throw new ArgumentNullException("convertBack");

			ConvertToFunction = (value, targetType, parameter, culture) => convertTo(value);
			ConvertBackFunction = (value, targetType, parameter, culture) => convertBack(value);
		}

		public ValueConverter(Func<TSource, Type, object, CultureInfo, TTarget> convertTo)
			: this(convertTo, (value, targetType, parameter, culture) => { throw new NotImplementedException(); })
		{

		}

		public ValueConverter(Func<TSource, Type, object, CultureInfo, TTarget> convertTo, Func<TTarget, Type, object, CultureInfo, TSource> convertBack)
		{
			if (convertTo == null)
				throw new ArgumentNullException("convertTo");
			if (convertBack == null)
				throw new ArgumentNullException("convertBack");

			ConvertToFunction = convertTo;
			ConvertBackFunction = convertBack;
		}

		/// <summary>
		/// Converts a value. 
		/// </summary>
		/// <returns>
		/// A converted value. IsSuccess the method returns null, the valid null value is used.
		/// </returns>
		/// <param name="value">The value produced by the binding source.</param><param name="targetType">The type of the binding target property.</param><param name="parameter">The converter parameter to use.</param><param name="culture">The culture to use in the converter.</param>
		public override TTarget Convert(TSource value, Type targetType, object parameter, CultureInfo culture)
		{
			if (ConvertToFunction == null)
				throw new NullReferenceException("ConvertToFunction cannot be null!");

			return ConvertToFunction(value, targetType, parameter, culture);
		}

		/// <summary>
		/// Converts a value. 
		/// </summary>
		/// <returns>
		/// A converted value. IsSuccess the method returns null, the valid null value is used.
		/// </returns>
		/// <param name="value">The value that is produced by the binding target.</param><param name="targetType">The type to convert to.</param><param name="parameter">The converter parameter to use.</param><param name="culture">The culture to use in the converter.</param>
		public override TSource ConvertBack(TTarget value, Type targetType, object parameter, CultureInfo culture)
		{
			if (ConvertBackFunction == null)
				throw new NullReferenceException("ConvertBackFunction cannot be null!");

			return ConvertBackFunction(value, targetType, parameter, culture);
		}
	}
}