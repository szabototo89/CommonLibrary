using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace CommonLib.WPF.Extensions
{
	public static class PointExtension
	{
		public static Point Multiply(this Point point, double scalar)
		{
			return new Point(point.X * scalar, point.Y * scalar);
		}

		public static Point Multiply(this Point point, Point value)
		{
			return new Point(point.X * value.Y, point.Y * value.Y);
		}
	}
}
