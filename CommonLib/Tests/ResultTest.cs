using System;
using CommonLib.Results;
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
		public void ExceptionResult()
		{
			ExceptionResult<string, NullReferenceException> result = "Hello World!";

			Assert.IsTrue(result.IsSuccess);
			Assert.IsFalse(result.Thrown);

			Assert.IsTrue(result == "Hello World!");

			result = new NullReferenceException("Hello World!");
			Assert.IsFalse(result.IsSuccess);
			Assert.IsTrue(result.Thrown);

			Exception ex = result;

			Assert.IsTrue(ex != null && 
						  ex.Message == "Hello World!");
		}

		[Test]
		public void HelloTest()
		{
			Assert.IsTrue(Hello("Tomi") == "Tomi");
			Assert.IsTrue("Tomi" == Hello("Tomi"));
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