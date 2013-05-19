using System;

namespace CommonLib
{
	public enum UnionDirection { Left, Right }

	/// <summary>
	/// Generic union datastructure. 
	/// </summary>
	public struct Union<TLeft, TRight>
	{
		public UnionDirection Direction { get; private set; }
		public TLeft Left { get; private set; }
		public TRight Right { get; private set; }

		public Union(TLeft value)
			: this()
		{
			Left = value;
			Direction = UnionDirection.Left;
		}

		public Union(TRight value)
			: this()
		{
			Right = value;
			Direction = UnionDirection.Right;
		}

		public void GetValue(Action<TLeft> left, Action<TRight> right)
		{
			if (left == null)
				throw new ArgumentNullException("left");
			if (right == null)
				throw new ArgumentNullException("right");

			switch (Direction)
			{
				case UnionDirection.Left:
					left(Left);
					break;
				case UnionDirection.Right:
					right(Right);
					break;
			}
		}

		public void GetValue(Action<TLeft> left)
		{
			GetValue(left, _ => { });
		}

		public void GetValue(Action<TRight> right)
		{
			GetValue(_ => { }, right);
		}

		public static implicit operator Union<TLeft, TRight>(TLeft left)
		{
			return new Union<TLeft, TRight>(left);
		}

		public static implicit operator Union<TLeft, TRight>(TRight right)
		{
			return new Union<TLeft, TRight>(right);
		}
	}
}