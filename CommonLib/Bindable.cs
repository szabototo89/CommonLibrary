using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonLib
{
	public class Bindable<TValue>
	{
		private Func<TValue> _ValueGetter;
		private Action<TValue> _ValueSetter;

		public TValue Value
		{
			get { return _ValueGetter(); }
			set { _ValueSetter(value); }
		}

		public Bindable(Func<TValue> getter, Action<TValue> setter)
		{
			Bind(getter, setter);
		}

		public void Bind(Func<TValue> getter, Action<TValue> setter)
		{
			Ensure.Are(getter, setter)
				  .NotNull();

			_ValueGetter = getter;
			_ValueSetter = setter;
		}
	}

}
