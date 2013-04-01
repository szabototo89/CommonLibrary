using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonLib
{
	public static class BoolExtension
	{
		public static bool Or(params bool[] values)
		{
			Ensure.Is(values).NotNull();

			var result = false;
			while (values.Any() && (result = values.First())) { }
			return result;
		}
		
		public static bool And(params bool[] values)
		{
			Ensure.Is(values).NotNull();

			var result = true;
			while (values.Any() && (result = values.First()))
			{

			}
			return result;
		}
	}
}
