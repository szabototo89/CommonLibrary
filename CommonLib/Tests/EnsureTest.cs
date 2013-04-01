using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace CommonLib.Tests
{
	internal class EnsureTest
	{
		[Test]
		public void NotNullTest()
		{
			Assert.DoesNotThrow(() => Ensure.Is("Hello World").NotNull());
			Assert.DoesNotThrow(() => Ensure.Are("Hello World", "Hi All").NotNull(new ArgumentNullException()));
			Assert.DoesNotThrow(() => Ensure.Are("Hello World", "Hi All", new object()).NotNull(new ArgumentNullException()));

			Assert.DoesNotThrow(() => Ensure.Is("Hello World!").NotBlank(new Exception()));
			Assert.DoesNotThrow(() => Ensure.Are("a", "b", "c").NotBlank(new Exception()));
		}

		[Test]
		public void IntervalTest()
		{
			Assert.DoesNotThrow(() => Ensure.Is(40).Interval(30, 130));
			Assert.Catch(() => Ensure.Is(40).Interval(0, 1)
									 .Is(540).Interval(0, 10));
			Assert.DoesNotThrow(() => Ensure.Is("c").Interval("a", "d"));
		}

		[Test(Description = "Interval test for range(0,1000)")]
		public void IntervalTest([Range(0, 1000)] int value)
		{
			Assert.DoesNotThrow(() => Ensure.Is(value).Interval(0, 1000));
			Assert.Catch(() => Ensure.Is(value).Interval(1001, 20000));
		}

		[Test]
		public void NotEmptyTest()
		{
			Assert.DoesNotThrow(() => Ensure.Is(new[] { 1, 2, 3 }).NotEmpty(new Exception()));
			Assert.Catch(() => Ensure.Is((IEnumerable<string>)new string[0]).NotEmpty(new Exception()));
		}

		[Test]
		public void AndTest()
		{
			Assert.DoesNotThrow(() => Ensure.Is("Hello World!").NotBlank()
											.And("Hi").NotNull(new Exception()));

			Assert.Catch(() => Ensure.Is<object>(null)
									 .And("Hello World").NotNull(new Exception()));
		}

		[Test]
		public void CatchTest()
		{
			Assert.DoesNotThrow(() => Ensure.Do(() => { throw new Exception(); })
										    .Catch((Exception ex) => Console.Write("Exception raised!"))
										    .Invoke());
		}
	}
}
