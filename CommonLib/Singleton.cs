namespace CommonLib
{
	public static class Singleton<T> where T : class, new()
	{
		private static class SingletonCreator
		{
			private static T _Instance;

			internal static T Instance
			{
				get { return _Instance ?? (_Instance = new T()); }
			}
		}

		public static T Instance
		{
			get { return SingletonCreator.Instance; }
		}
	}
}