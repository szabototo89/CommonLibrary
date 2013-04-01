using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonLib
{
	public class Let
	{
		public Let<TReturn> Be<TReturn>(Func<TReturn> func)
		{
			return new Let<TReturn>(func());
		}

		public Let<TReturn> Be<TValue, TReturn>(TValue that, Func<TValue, TReturn> func)
		{
			return new Let<TValue>(that).Be(func);
		}
	}

	public class Let<TValue>
	{
		public TValue Value { get; protected set; }

		public Let(TValue that)
		{
			Value = that;
		}

		public Let<TReturn> Be<TReturn>(Func<TValue, TReturn> func)
		{
			return new Let<TReturn>(func(Value));
		}
	}
}
