using System;
using NUnit.Framework;

namespace CommonLib.Tests
{
	class LoggerObjectTest
	{
		[Test]
		public void InitTest()
		{
			dynamic v = new LoggerObject();
			var result = v + 10;
		}
	}
}