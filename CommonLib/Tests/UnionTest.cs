using System;
using NUnit.Framework;

namespace CommonLib.Tests
{
	class UnionTest
	{
		[Test]
		public void InitTest()
		{
			Union<int, string> union = "Hello World!";

			Assert.AreEqual(union.Right, "Hello World!");

			union = 10;
			Assert.AreEqual(union.Left, 10);
			Assert.AreNotEqual(union.Right, "Hello World!");

			var result = RandomIntOrString();

			if (result.Direction == UnionDirection.Left)
				Assert.IsTrue(result.Left >= 0 && result.Left <= 100);
			else
				Assert.IsTrue(result.Right == "Hello World!");
		}

		public Union<int, string> RandomIntOrString()
		{
			var random = new Random();

			if (random.NextDouble() >= 0.5)
				return random.Next(0, 100);

			return "Hello World!";
		}

	}
}