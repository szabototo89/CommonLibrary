using System;
using CommonLib.Options;
using NUnit.Framework;

namespace CommonLib.Tests
{
	class ResultTest
	{
		[Test]
		public void InitTest()
		{
			Option<int> option = 10;
			Assert.IsTrue(option.IsSuccess);
			Assert.IsTrue(option + 1 == 11);

			option = Option<int>.None;
			Assert.IsFalse(option.IsSuccess);
		}

		[Test]
		public void ExceptionResult()
		{
			ExceptionOption<string, NullReferenceException> option = "Hello World!";

			Assert.IsTrue(option.IsSuccess);
			Assert.IsFalse(option.Thrown);

			Assert.IsTrue(option == "Hello World!");

			option = new NullReferenceException("Hello World!");
			Assert.IsFalse(option.IsSuccess);
			Assert.IsTrue(option.Thrown);

			Exception ex = option;

			Assert.IsTrue(ex != null && 
						  ex.Message == "Hello World!");
		}

		[Test]
		public void HelloTest()
		{
			Assert.IsTrue(Hello("Tomi") == "Tomi");
			Assert.IsTrue("Tomi" == Hello("Tomi"));
			Assert.AreEqual(Hello(""), Option<string>.None);
		}

		private static Option<string> Hello(string name)
		{
			return Let.If(string.IsNullOrWhiteSpace(name), Option<string>.None)
					  .Else(name);
		}
	}
}