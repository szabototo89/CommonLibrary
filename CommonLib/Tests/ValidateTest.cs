using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace CommonLib.Tests
{
	class ValidateTest
	{
		[Test]
		public void InitValidate()
		{
			var number = new Validate<int>(10, i => i < 20, false);
			Assert.AreEqual(number.Value, 10);
			number.Value = 30;
			Assert.AreNotEqual(number.Value, 30);
			Assert.AreEqual(number.Value, 10);

			number = 140.ToValidate(x => x == 140);
			Assert.AreEqual(number.Value, 140);
		}
	}
}
