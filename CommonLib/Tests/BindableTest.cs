using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace CommonLib.Tests
{
	class BindableTest
	{
		[Test]
		public void InitTest()
		{
			var str = "Hello World!";
			Bindable<int> bindable;
			{
				var number = 10;
				bindable = new Bindable<int>(() => number, value => number = value);
				number++;
			}

			str = "Not hello!";
		}
	}
}
