using NUnit.Framework;

namespace CommonLib.Tests
{
	class SingletonTest
	{
		class Person
		{
			public string Name { get; set; }
			public int Age { get; set; }
		}

		[Test]
	 	public void InitTest()
		{
			Person p = Singleton<Person>.Instance;
			Person p2 = Singleton<Person>.Instance;

			Assert.AreEqual(p, p2);
			Assert.AreNotEqual(p, new Person());
		}
	}
}