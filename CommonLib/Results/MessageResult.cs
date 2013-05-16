using System;

namespace CommonLib.Results
{
	public class MessageResult<TValue> : ResultBase<TValue>
	{
		public string Message { get; protected set; }

		public override bool IsSuccess
		{
			get { return !string.IsNullOrWhiteSpace(Message); }
		}

		protected MessageResult(TValue value, string message = null)
			: base(value)
		{
			Message = message;
		}

		public static implicit operator MessageResult<TValue>(TValue value)
		{
			return new MessageResult<TValue>(value);
		}

		public static implicit operator TValue(MessageResult<TValue> value)
		{
			if (value == null)
				throw new ArgumentNullException("value");
			return value.Value;
		}

		public static MessageResult<TValue> Throw(string message)
		{
			return new MessageResult<TValue>(default(TValue), message);
		}
	}
}