using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonLib
{
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
	}
}
