using System.Windows;
using System.Windows.Data;
using CommonLib.WPF.Converters;

namespace CommonLib.WPF.Bindings
{
	public class DebugBinding : Binding
	{
		private class DebugBindingConverter : DebugConverter
		{
			public string Path { get; private set; }

			public DebugBindingConverter(string path)
			{
				Path = path;
			}
		}

		public DebugBinding()
			: this("")
		{

		}

		public DebugBinding(string path)
			: base(path)
		{
			Converter = new DebugBindingConverter(path);
		}
	}
}