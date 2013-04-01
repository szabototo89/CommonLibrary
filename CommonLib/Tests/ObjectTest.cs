using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace CommonLib.Tests
{
	class ObjectTest
	{
		private class A { }
		private class B : A { }
		private class C : A { }
		private class D : B { }

		[Test]
		public void IsTest()
		{
			var a = new D();
			var result = string.Empty;

			a.Is<B>(_ => result += "B")
		     .Is<A>(_ => result += "A")
		     .Is<string>(_ => result += "S")
		     .Is<C>(_ => result += "C");

			Assert.AreEqual(result, "BA");
		}
	}
}
