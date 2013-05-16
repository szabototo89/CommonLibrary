using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonLib
{
	public class Result<TValue>
	{
		private static readonly Result<TValue> _None = new Result<TValue>(default(TValue));
		private static readonly Result<TValue> _Error = new Result<TValue>(default(TValue)); 

		public static Result<TValue> None
		{
			get { return _None; }
		}

		public static Result<TValue> Error
		{
			get { return _Error; }
		}

		public TValue Value { get; protected set; }

		public bool IsSuccess
		{
			get { return !Equals(this, _None); }
		}

		public bool IsError
		{
			get { return Equals(this, _Error); }
		}

		internal Result(TValue value)
		{
			Value = value;
		}

		public static implicit operator Result<TValue>(TValue value)
		{
			return new Result<TValue>(value);
		}

		public static implicit operator TValue(Result<TValue> result)
		{
			return result.Value;
		}
	}
}
