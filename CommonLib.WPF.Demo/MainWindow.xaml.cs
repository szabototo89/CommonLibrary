using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using CommonLib.WPF.Demo.ViewModels;
using IronPython.Hosting;
using Microsoft.Scripting.Hosting;

namespace CommonLib.WPF.Demo
{
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window
	{
		private ScriptEngine pythonEngine = Python.CreateEngine();

		public MainWindow()
		{
			InitializeComponent();
			DataContext = new MainWindowContext();
		}

		/*private void PART_CompileButton_Click(object sender, RoutedEventArgs e)
		{
			try {
				PART_ResultTextBlock.Background = new SolidColorBrush(Colors.Transparent);
				var result = pythonEngine.Execute(PART_SourceCode.Text);
				PART_ResultTextBlock.Text = result != null ? result.ToString() : "There is no result value!";
			}
			catch (Exception exception) {
				PART_ResultTextBlock.Background = new SolidColorBrush(Colors.Red);
				PART_ResultTextBlock.Text = exception.Message;
			}
		}

		private void Button_MouseLeftButtonDown_1(object sender, MouseButtonEventArgs e)
		{
			if (e.ClickCount == 2) {
				switch (WindowState) {
					case WindowState.Normal:
						WindowState = WindowState.Maximized;
						break;
					case WindowState.Maximized:
						WindowState = WindowState.Normal;
						break;
				}
			}
			else {
				if (WindowState == WindowState.Maximized)
					WindowState = WindowState.Normal;
				DragMove();
			}
		} */
	}
}
