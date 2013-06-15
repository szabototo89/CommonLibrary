using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommonLib.Options;
using NUnit.Framework;

namespace CommonLib.Tests
{
	class OptionTest
	{
		[Test]
		public void IfTest()
		{
			Option<int> i = 10;

			Assert.DoesNotThrow(() =>
				i.IsSuccess(_ => Console.WriteLine("Hello World!"))
				 .Otherwise(() => Console.WriteLine("Sorryyy.")));
		}
	}
}
