using System;

namespace CommonLib.Options
{
	public class ExceptionOption<TValue, TException> : OptionBase<TValue>
		where TException : Exception
	{
		public TException Exception { get; protected set; }

		protected ExceptionOption(TValue value, TException exception)
			: base(value)
		{
			Exception = exception;
		}

		public override bool IsSuccess
		{
			get { return Exception == null; }
		}

		public bool Thrown { get { return Exception != null; } }

		public static implicit operator ExceptionOption<TValue, TException>(TValue value)
		{
			return new ExceptionOption<TValue, TException>(value, null);
		}

		public static implicit operator TValue(ExceptionOption<TValue, TException> value)
		{
			return value.Value;
		}

		public static implicit operator ExceptionOption<TValue, TException>(TException value)
		{
			return new ExceptionOption<TValue, TException>(default(TValue), value);
		}

		public static implicit operator TException(ExceptionOption<TValue, TException> value)
		{
			return value.Exception;
		}

		public static ExceptionOption<TValue, TException> Throw(TException exception)
		{
			return new ExceptionOption<TValue, TException>(default(TValue), exception);
		}
	}

	public class ExceptionOption<TValue> : ExceptionOption<TValue, Exception>
	{
		protected ExceptionOption(TValue value, Exception exception)
			: base(value, exception) { }

		public static implicit operator ExceptionOption<TValue>(TValue value)
		{
			return new ExceptionOption<TValue>(value, null);
		}

		public static implicit operator TValue(ExceptionOption<TValue> value)
		{
			if (value == null)
				throw new ArgumentNullException("value");
			return value.Value;
		}

		public static implicit operator ExceptionOption<TValue>(Exception value)
		{
			return new ExceptionOption<TValue>(default(TValue), value);
		}

		public static implicit operator Exception(ExceptionOption<TValue> value)
		{
			return value.Exception;
		}
	}
}