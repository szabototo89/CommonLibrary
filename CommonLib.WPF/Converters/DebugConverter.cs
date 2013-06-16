using System.Diagnostics;

namespace CommonLib.WPF.Converters
{
	public class DebugConverter : ValueConverter<object, object>
	{
		public DebugConverter()
			: base(value => {
				Debugger.Log(0, "[Info]", string.Format("[Convert to: {0}]\n", value ?? "null"));
				Debugger.Break();
				return value;
			}, value => {
				Debugger.Log(0, "[Info]", string.Format("[Convert back: {0}]\n", value ?? "null"));
				Debugger.Break();
				return value;
			})
		{ }
	}
}