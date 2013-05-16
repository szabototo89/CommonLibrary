using System.Diagnostics;

namespace CommonLib.WPF.Converters
{
	public class DebugConverter : ValueConverter<object, object>
	{
		public DebugConverter()
			: base(value => {
					   Debugger.Break();
					   return value;
				   },
				   value => {
					   Debugger.Break();
					   return value;
				   })
		{

		}
	}
}