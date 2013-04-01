using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonLib
{
	public static class IntegerExtension
	{
		public static void Times(this int value, Action action)
		{
			Ensure.Is(action).NotNull();

			Times(value, i => action());
		}

		public static void Times(this int value, Action<int> action)
		{
			for (var i = 0; i < value; i++)
				action(i);
		}

		public static IEnumerable<TReturn> Times<TReturn>(this int value, Func<TReturn> func)
		{
			return Times(value, i => func());
		}

		public static IEnumerable<TReturn> Times<TReturn>(this int value, Func<int, TReturn> func)
		{
			var values = new TReturn[value];
			for (var i = 0; i < value; i++)
				values[i] = func(value);
			return values;
		}
	}
}
