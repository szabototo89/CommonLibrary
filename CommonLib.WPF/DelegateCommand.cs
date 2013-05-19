using System;
using System.Windows.Input;

namespace CommonLib.WPF
{
	/// <summary>
	/// Generic command
	/// </summary>
	public class DelegateCommand<T> : ICommand
	{
		private readonly Predicate<T> _canExecute;
		private readonly Action<T> _execute;

		void ICommand.Execute(object parameter)
		{
			Execute((T)parameter);
		}

		bool ICommand.CanExecute(object parameter)
		{
			return CanExecute((T)parameter);
		}

		public event EventHandler CanExecuteChanged;

		public DelegateCommand(Action<T> execute)
			: this(null, execute)
		{
		}

		public DelegateCommand(Predicate<T> canExecute, Action<T> execute)
		{
			_execute = execute;
			_canExecute = canExecute;
		}

		public bool CanExecute(T parameter)
		{
			if (_canExecute == null)
				return true;

			return _canExecute(parameter);
		}

		public void Execute(T parameter)
		{
			_execute(parameter);
		}

		public static DelegateCommand<T> Instance(Action<T> execute)
		{
			return Instance(null, execute);
		} 

		public static DelegateCommand<T> Instance(Predicate<T> canExecute, Action<T> execute)
		{
			return new DelegateCommand<T>(canExecute, execute);
		}

		protected void OnCanExecuteChanged()
		{
			if (CanExecuteChanged != null)
			{
				CanExecuteChanged(this, EventArgs.Empty);
			}
		}
	}
}