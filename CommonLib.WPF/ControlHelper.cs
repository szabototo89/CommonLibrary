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
		public static IEnumerable<DependencyObject> GetVisualChildren(DependencyObject reference, bool isRecursive = false)
		{
			if (reference == null)
				throw new ArgumentNullException("reference");
			var count = VisualTreeHelper.GetChildrenCount(reference);

			if (!isRecursive)
			{
				for (var i = 0; i < count; i++)
					yield return VisualTreeHelper.GetChild(reference, i);
			}
			else
			{
				if (count == 0)
					yield return reference;
				for (var i = 0; i < count; i++)
				{
					foreach (var child in GetVisualChildren(VisualTreeHelper.GetChild(reference, i), true))
						yield return child;
				}
			}
		}
	}
}