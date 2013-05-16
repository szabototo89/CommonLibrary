using NUnit.Framework;

namespace CommonLib.Tests
{
	class ResultTest
	{
		[Test]
		public void InitTest()
		{
			Result<int> result = 10;
			Assert.IsTrue(result.IsSuccess);
			Assert.IsTrue(result + 1 == 11);

			result = Result<int>.None;
			Assert.IsFalse(result.IsSuccess);

			result = Result<int>.Error;
			Assert.IsTrue(result == Result<int>.Error);
			Assert.IsTrue(result.IsError);
		}

		[Test]
		public void HelloTest()
		{
			Assert.IsTrue(Hello("Tomi") == "Tomi");
			Assert.AreEqual(Hello(""), Result<string>.None);
			Assert.AreEqual(Hello(null), Result<string>.Error);
		}

		private static Result<string> Hello(string name)
		{
			return Let.If(name == null, Result<string>.Error)
					  .If(string.IsNullOrWhiteSpace(name), Result<string>.None)
					  .Else(name);
		}
	}
}