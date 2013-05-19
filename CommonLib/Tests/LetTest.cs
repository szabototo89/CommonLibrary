using System;
using NUnit.Framework;

namespace CommonLib.Tests
{
	class LetTest
	{
		private const string HELLO_WORLD_MESSAGE = "Hello World";

		[Test]
		public void BeTest()
		{
			string result = Let.Be(HELLO_WORLD_MESSAGE, value => (string)null).Default("Hi!");
			Assert.AreEqual(result, "Hi!");

			result = Let.Default(null, HELLO_WORLD_MESSAGE);
			Assert.AreEqual(result, HELLO_WORLD_MESSAGE);
		}

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

			string message = Let.If(result == 10, HELLO_WORLD_MESSAGE)
								.Else("Hello Other World!");

			Assert.IsTrue((result == 10 && message == HELLO_WORLD_MESSAGE) ||
						  (result == 20 && message == "Hello Other World!"));
		}
	}
}