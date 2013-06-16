namespace CommonLib.WPF.Demo.ViewModels
{
	public class MainWindowContext : ViewModelBase
	{
		private string _Message;

		public string Message
		{
			get { return _Message; }
			set
			{
				_Message = value;
				OnPropertyChanged(() => Message);
			}
		}

		public DelegateCommand<string> ShowMessageCommand
		{
			get
			{
				return RegisterCommand<string>(() => ShowMessageCommand,
					msg => {
						Message = msg;
					});
			}
		}
	}
}