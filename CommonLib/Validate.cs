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
		private TValue _value;
		private Func<TValue, bool> _predicate;
		private readonly bool _isThrowException;

		public Validate(TValue value)
			: this(value, _ => true)
		{

		}

		public Validate(TValue value, Func<TValue, bool> predicate, bool isThrowException = true)
		{
			_value = value;
			_predicate = predicate;
			_isThrowException = isThrowException;
		}

		public Func<TValue, bool> Predicate
		{
			get { return _predicate; }
			set { _predicate = value; }
		}

		public TValue Value
		{
			get { return _value; }
			set
			{
				if (Predicate(value))
					_value = value;
				else if (IsThrowException)
					throw new ConstraintException(string.Format("Invalid value of Validate<{0}>!", typeof(TValue).FullName));
			}
		}

		public bool IsThrowException
		{
			get { return _isThrowException; }
		}
	}
}
