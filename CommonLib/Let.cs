using System;
using System.Text;
using System.Threading.Tasks;

namespace CommonLib
{
	public static class Let
	{
		public static Let<TReturn> Be<TReturn>(Func<TReturn> func)
		{
			return new Let<TReturn>(func());
		}

		public static Let<TReturn> Be<TValue, TReturn>(TValue that, Func<TValue, TReturn> func)
		{
			return new Let<TValue>(that).Be(func);
		}

		public static Let<TValue> Default<TValue>(TValue that, TValue defaultValue)
		{
			return new Let<TValue>(that).Default(defaultValue);
		}

		#region Case features
		public static Case<TType> If<TType>(Func<bool> condition, Func<TType> value)
		{
			return new Case<TType>().If(condition, value);
		}

		public static Case<TType> If<TType>(Func<bool> condition, TType value)
		{
			return new Case<TType>().If(condition, () => value);
		}

		public static Case<TType> If<TType>(bool condition, Func<TType> value)
		{
			return If(() => condition, value);
		}

		public static Case<TType> If<TType>(bool condition, TType value)
		{
			return new Case<TType>().If(() => condition, () => value);
		}
		#endregion
	}

	public class Let<TValue>
	{
		public TValue Value { get; protected set; }

		internal Let(TValue that)
		{
			Value = that;
		}

		public Let<TReturn> Be<TReturn>(Func<TValue, TReturn> func)
		{
			return new Let<TReturn>(func(Value));
		}

		public Let<TValue> Default(TValue defaultValue)
		{
			return new Let<TValue>(Equals(default(TValue), Value) ? defaultValue : Value);
		}

		public static implicit operator TValue(Let<TValue> letValue)
		{
			if (letValue == null)
				throw new ArgumentNullException("letValue");

			return letValue.Value;
		}
	}
}
