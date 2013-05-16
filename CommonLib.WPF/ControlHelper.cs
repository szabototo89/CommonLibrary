using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;

namespace CommonLib.WPF
{
	public class ControlHelper
	{
		public static IEnumerable<DependencyObject> GetVisualChildren(DependencyObject reference)
		{
			if (reference == null)
				throw new ArgumentNullException("reference");
			var count = VisualTreeHelper.GetChildrenCount(reference);

			for (var i = 0; i < count; i++)
				yield return VisualTreeHelper.GetChild(reference, i);
		}
	}
}