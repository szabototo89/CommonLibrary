using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonLib
{
	public static class ObjectExtension
	{
		public static Let<TReturn> Be<TValue, TReturn>(this TValue that, Func<TValue, TReturn> func)
		{
			return that.Let().Be(func);
		}

		public static Let<TObject> Let<TObject>(this TObject that)
		{
			return new Let<TObject>(that);
		}

		public static bool IsNull<TValue>(this TValue that) where TValue : class
		{
			return that == null;
		}

		public static TConvert As<TValue, TConvert>(this TValue that)
			where TConvert : class
			where TValue : class
		{
			return that as TConvert;
		}

		public static TConvert As<TValue, TConvert>(this TValue that, Func<TValue, TConvert> converter)
		{
			Ensure.Is(converter).NotNull();
			return converter(that);
		}

		public static bool Is<TType>(this object that)
		{
			return that is TType;
		}

		public static object Is<TType>(this object that, Action<TType> method)
		{
			if (that is TType)
				method((TType)that);
			return that;
		}

		public static bool Is<TValue>(this TValue that, Type type)
		{
			if (type == null)
				throw new ArgumentNullException("type");
			return that.GetType() == type;
		}

		public static TConvert To<TConvert>(this object that)
		{
			return (TConvert)that;
		}

		public static Validate<TValue> ToValidate<TValue>(this TValue that, Func<TValue, bool> condition, bool throwException = true)
		{
			Ensure.Is(condition).NotNull("condition");
			return new Validate<TValue>(that, condition, throwException);
		}
	}
}
