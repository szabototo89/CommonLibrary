using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace CommonLib.WPF
{
	public class ActionCommand : ICommand
	{
		private readonly Predicate<object> _CanExecute;
		private readonly Action _Execute;

		/// <summary>
		/// ActionCommand konstruktor
		/// </summary>
		public ActionCommand(Action execute)
			: this(null, execute)
		{

		}

		/// <summary>
		/// ActionCommand konstruktor
		/// </summary>
		public ActionCommand(Predicate<object> canExecute, Action execute)
		{
			_Execute = execute;
			_CanExecute = canExecute;
		}

		public void Execute(object parameter)
		{
			_Execute();
		}

		public bool CanExecute(object parameter)
		{
			if (_CanExecute == null)
				return true;
			return _CanExecute(parameter);
		}

		public event EventHandler CanExecuteChanged;

		protected void OnCanExecuteChanged()
		{
			if (CanExecuteChanged != null)
			{
				CanExecuteChanged(this, EventArgs.Empty);
			}
		}
	}
}
