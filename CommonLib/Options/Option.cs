using System;

namespace CommonLib.Options
{
	public class Option<TValue> : OptionBase<TValue>
	{
		private static readonly Option<TValue> _None = new Option<TValue>(default(TValue));

		public static Option<TValue> None
		{
			get { return _None; }
		}

		public override bool IsSuccess
		{
			get { return !Equals(this, _None); }
		}

		protected Option(TValue value)
			: base(value) { }

		public static implicit operator Option<TValue>(TValue value)
		{
			return new Option<TValue>(value);
		}

		public static implicit operator TValue(Option<TValue> option)
		{
			if (option == null)
				throw new ArgumentNullException("option");
			return option.Value;
		}
	}
}
