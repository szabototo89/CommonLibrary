using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonLib
{
	public class Bindable<TValue>
	{
		private Func<TValue> _valueGetter;
		private Action<TValue> _valueSetter;

		public TValue Value
		{
			get { return _valueGetter(); }
			set { _valueSetter(value); }
		}

		public Bindable(Func<TValue> getter, Action<TValue> setter)
		{
			Bind(getter, setter);
		}

		public void Bind(Func<TValue> getter, Action<TValue> setter)
		{
			Ensure.Are(getter, setter)
				  .NotNull();

			_valueGetter = getter;
			_valueSetter = setter;
		}
	}

}
