using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommonLib.Options;

namespace CommonLib
{
	public delegate bool TryParseMethod<T>(string value, out T result);

	public class Try
	{
		public Delegate Method { get; protected set; }
		public Dictionary<Type, Delegate> ExceptionHandlers { get; protected set; }

		internal Try(Delegate method)
		{
			Method = method;
			ExceptionHandlers = new Dictionary<Type, Delegate>();
		}

		public Try Catch<TException>(Action<TException> handler)
		{
			if (handler == null)
				throw new ArgumentNullException("handler");

			var exceptionType = typeof(TException);

			if (!ExceptionHandlers.ContainsKey(exceptionType))
				ExceptionHandlers.Add(exceptionType, handler);

			return this;
		}

		public static Option<T> Parse<T>(TryParseMethod<T> method, string value, Action<T> func)
		{
			T result;
			if (method(value, out result)) {
				func(result);
				return result;
			}

			return Option<T>.None;
		}

		public static Option<int> Parse(string value, Action<int> func)
		{
			return Parse(int.TryParse, value, func);
		}

		public static Option<long> Parse(string value, Action<long> func)
		{
			return Parse(long.TryParse, value, func);
		}

		public static Option<short> Parse(string value, Action<short> func)
		{
			return Parse(short.TryParse, value, func);
		}

		public static Option<bool> Parse(string value, Action<bool> func)
		{
			return Parse(bool.TryParse, value, func);
		}

		public static Option<decimal> Parse(string value, Action<decimal> func)
		{
			return Parse(decimal.TryParse, value, func);
		}

		public static Option<float> Parse(string value, Action<float> func)
		{
			return Parse(float.TryParse, value, func);
		}

		public static Option<double> Parse(string value, Action<double> func)
		{
			return Parse(double.TryParse, value, func);
		}

		public static Option<uint> Parse(string value, Action<uint> func)
		{
			return Parse(uint.TryParse, value, func);
		}

		public static Option<ushort> Parse(string value, Action<ushort> func)
		{
			return Parse(ushort.TryParse, value, func);
		}

		public static Option<ulong> Parse(string value, Action<ulong> func)
		{
			return Parse(ulong.TryParse, value, func);
		}

	}
}
