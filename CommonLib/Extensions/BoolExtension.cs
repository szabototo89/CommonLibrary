using System.Linq;

namespace CommonLib.Extensions
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
