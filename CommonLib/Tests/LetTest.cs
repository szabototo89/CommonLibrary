using System;
using NUnit.Framework;

namespace CommonLib.Tests
{
	public class LetTest
	{
		[Test]
		[Repeat(1000)]
		public void IfTest()
		{
			var random = new Random().NextDouble();

			int result = Let.If(random < 0.5, 10)
							.If(random >= 0.5, 20)
							.Else(1000);

			Assert.IsTrue((random < 0.5 && result == 10) ||
						  (random >= 0.5 && result == 20));

			string message = Let.If(result == 10, "Hello World!")
								.Else("Hello Other World!");

			Assert.IsTrue((result == 10 && message == "Hello World!") ||
						  (result == 20 && message == "Hello Other World!"));
		}
	}
}