using System;

namespace CommonLib.Results
{
	public class ExceptionResult<TValue, TException> : ResultBase<TValue>
		where TException : Exception
	{
		public TException Exception { get; protected set; }

		protected ExceptionResult(TValue value, TException exception)
			: base(value)
		{
			Exception = exception;
		}

		public override bool IsSuccess
		{
			get { return Exception == null; }
		}

		public bool Thrown { get { return Exception != null; } }

		public static implicit operator ExceptionResult<TValue, TException>(TValue value)
		{
			return new ExceptionResult<TValue, TException>(value, null);
		}

		public static implicit operator TValue(ExceptionResult<TValue, TException> value)
		{
			return value.Value;
		}

		public static implicit operator ExceptionResult<TValue, TException>(TException value)
		{
			return new ExceptionResult<TValue, TException>(default(TValue), value);
		}

		public static implicit operator TException(ExceptionResult<TValue, TException> value)
		{
			return value.Exception;
		}

		public static ExceptionResult<TValue, TException> Throw(TException exception)
		{
			return new ExceptionResult<TValue, TException>(default(TValue), exception);
		}
	}

	public class ExceptionResult<TValue> : ExceptionResult<TValue, Exception>
	{
		protected ExceptionResult(TValue value, Exception exception)
			: base(value, exception) { }

		public static implicit operator ExceptionResult<TValue>(TValue value)
		{
			return new ExceptionResult<TValue>(value, null);
		}

		public static implicit operator TValue(ExceptionResult<TValue> value)
		{
			if (value == null)
				throw new ArgumentNullException("value");
			return value.Value;
		}

		public static implicit operator ExceptionResult<TValue>(Exception value)
		{
			return new ExceptionResult<TValue>(default(TValue), value);
		}

		public static implicit operator Exception(ExceptionResult<TValue> value)
		{
			return value.Exception;
		}
	}
}