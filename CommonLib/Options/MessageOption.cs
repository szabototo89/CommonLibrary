using System;

namespace CommonLib.Options
{
	public class MessageOption<TValue> : OptionBase<TValue>
	{
		public string Message { get; protected set; }

		public override bool IsSuccess
		{
			get { return !string.IsNullOrWhiteSpace(Message); }
		}

		protected MessageOption(TValue value, string message = null)
			: base(value)
		{
			Message = message;
		}

		public static implicit operator MessageOption<TValue>(TValue value)
		{
			return new MessageOption<TValue>(value);
		}

		public static implicit operator TValue(MessageOption<TValue> value)
		{
			if (value == null)
				throw new ArgumentNullException("value");
			return value.Value;
		}

		public static MessageOption<TValue> Throw(string message)
		{
			return new MessageOption<TValue>(default(TValue), message);
		}
	}
}