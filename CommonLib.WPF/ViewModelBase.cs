using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace CommonLib.WPF
{
	/// <summary>
	/// Nézetmodellek absztrakt ősosztálya
	/// </summary>
	public abstract class ViewModelBase : INotifyPropertyChanged
	{
		#region Property Changed
		/// <summary>
		/// Tulajdonság értékének megváltozása esetén lefutó esemény
		/// </summary>
		public event PropertyChangedEventHandler PropertyChanged;

		/// <summary>
		/// Tulajdonság értékének megváltoztatását jelző metódus
		/// </summary>
		protected ViewModelBase OnPropertyChanged(params Expression<Func<object>>[] expressions)
		{
			foreach (var exp in expressions)
				OnPropertyChanged<object>(exp);
			return this;
		}

		/// <summary>
		/// Tulajdonság értékének megváltoztatását jelző metódus
		/// </summary>
		protected ViewModelBase OnPropertyChanged<TPropertyType>(Expression<Func<TPropertyType>> expression)
		{
			if (expression == null)
				throw new ArgumentNullException("expression");

			return OnPropertyChanged(GetMemberNameFromExpression(expression));
		}

		/// <summary>
		/// Tulajdonság értékének megváltoztatását jelző metódus
		/// </summary>
		protected ViewModelBase OnPropertyChanged(params string[] propertyNames)
		{
			foreach (var prop in propertyNames)
				OnPropertyChanged(prop);
			return this;
		}

		/// <summary>
		/// Tulajdonság értékének megváltoztatását jelző metódus
		/// </summary>									
		protected virtual ViewModelBase OnPropertyChanged(string propertyName)
		{
			PropertyChangedEventHandler handler = PropertyChanged;
			if (handler != null) handler(this, new PropertyChangedEventArgs(propertyName));
			return this;
		}

		/// <summary>
		/// Az osztályon belül lévő összes tulajdonság értékének frissítése
		/// </summary>
		public virtual void Refresh()
		{
			foreach (var propertyName in GetType().GetProperties().Select(prop => prop.Name))
				OnPropertyChanged(propertyName);
		}
		#endregion

		#region Private Helper Methods

		private string GetMemberNameFromExpression<T>(Expression<Func<T>> expression)
		{
			Ensure.Is(() => expression).NotNull();

			var body = expression.Body as MemberExpression;
			if (body == null)
				throw new ArgumentException("Not supported expression!", "expression");

			return body.Member.Name;
		}

		#endregion

		#region Register methods

		protected readonly Dictionary<string, ICommand> Commands = new Dictionary<string, ICommand>();

		protected TCommand RegisterCommand<TCommand>(Expression<Func<object>> name, Func<TCommand> commandInitializer)
			where TCommand : class, ICommand
		{
			Ensure.Is(() => name).NotNull()
				.Is(() => commandInitializer).NotNull();

			var memberName = GetMemberNameFromExpression(name);

			return
				Let.If(() => !Commands.ContainsKey(memberName), () => {
					                                                      TCommand command = commandInitializer();
					                                                      Commands.Add(memberName, command);
					                                                      return command;
				})
					.Else(() => Commands[memberName] as TCommand);

		}

		protected ActionCommand RegisterCommand(Expression<Func<object>> name, Predicate<object> canExecute, Action execute)
		{
			Ensure
				.Is(() => canExecute).NotNull()
				.Is(() => execute).NotNull();
			return RegisterCommand(name, () => new ActionCommand(canExecute, execute));
		}

		protected ActionCommand RegisterCommand(Expression<Func<object>> name, Action execute)
		{
			return RegisterCommand(name, _ => true, execute);
		}

		protected DelegateCommand<TObject> RegisterCommand<TObject>(Expression<Func<object>> name, Predicate<TObject> canExecute, Action<TObject> execute)
		{
			Ensure
				.Is(() => canExecute).NotNull()
				.Is(() => execute).NotNull();

			return RegisterCommand(name, () => new DelegateCommand<TObject>(canExecute, execute));
		}

		protected DelegateCommand<TObject> RegisterCommand<TObject>(Expression<Func<object>> name, Action<TObject> execute)
		{
			return RegisterCommand(name, _ => true, execute);
		}

		#endregion
	}
}
