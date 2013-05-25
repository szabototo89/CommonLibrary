using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonLib
{
	public struct Validate<TValue>
	{
		private TValue _Value;
		private Func<TValue, bool> _Predicate;
		private readonly bool _IsThrowException;

		public Validate(TValue value)
			: this(value, _ => true)
		{

		}

		public Validate(TValue value, Func<TValue, bool> predicate, bool isThrowException = true)
		{
			_Value = value;
			_Predicate = predicate;
			_IsThrowException = isThrowException;
		}

		public Func<TValue, bool> Predicate
		{
			get { return _Predicate; }
			set { _Predicate = value; }
		}

		public TValue Value
		{
			get { return _Value; }
			set
			{
				if (Predicate(value))
					_Value = value;
				else if (IsThrowException)
					throw new ConstraintException(string.Format("Invalid value of Validate<{0}>!", typeof(TValue).FullName));
			}
		}

		public bool IsThrowException
		{
			get { return _IsThrowException; }
		}
	}
}
