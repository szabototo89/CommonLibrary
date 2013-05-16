using System;

namespace CommonLib.Results
{
	public class Result<TValue> : ResultBase<TValue>
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

		public override bool IsSuccess
		{
			get { return !Equals(this, _None); }
		}

		public virtual bool IsError
		{
			get { return Equals(this, _Error); }
		}

		protected Result(TValue value)
			: base(value) { }

		public static implicit operator Result<TValue>(TValue value)
		{
			return new Result<TValue>(value);
		}

		public static implicit operator TValue(Result<TValue> result)
		{
			if (result == null)
				throw new ArgumentNullException("result");
			return result.Value;
		}
	}
}
