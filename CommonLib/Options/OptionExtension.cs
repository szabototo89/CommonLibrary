using System;

namespace CommonLib.Options
{
	public static class OptionExtension
	{
		public static Option<TValue> If<TValue>(this Option<TValue> option, Action func)
		{
			return If(option, _ => func());
		}

		public static Option<TValue> If<TValue>(this Option<TValue> option, Action<TValue> func)
		{
			Ensure.Is(() => option).NotNull()
				  .Is(() => func).NotNull();

			if (option.IsSuccess)
				func(option.Value);
			return option;
		}

		public static Option<TValue> Otherwise<TValue>(this Option<TValue> option, Action func)
		{
			Ensure
				.Is(() => option).NotNull()
				.Is(() => func).NotNull();

			if (!option.IsSuccess)
				func();

			return option;
		}

		public static ExceptionOption<TValue> Throw<TValue>(this ExceptionOption<TValue> option)
		{
			Ensure.Is(() => option).NotNull();

			if (option.Thrown)
				throw option.Exception;
			return option;
		}
	}
}