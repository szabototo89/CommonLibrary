namespace CommonLib.Results
{
	public abstract class ResultBase<TValue>
	{
		protected ResultBase(TValue value)
		{
			Value = value;
		}

		public TValue Value { get; protected set; }
		public abstract bool IsSuccess { get; }
	}
}