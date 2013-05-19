namespace CommonLib.Options
{
	public abstract class OptionBase<TValue>
	{
		protected OptionBase(TValue value)
		{
			Value = value;
		}

		public TValue Value { get; protected set; }
		public abstract bool IsSuccess { get; }
	}
}