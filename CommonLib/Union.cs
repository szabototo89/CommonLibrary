using System;
using CommonLib.Results;

namespace CommonLib
{
	public enum UnionDirection { Left, Right }

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