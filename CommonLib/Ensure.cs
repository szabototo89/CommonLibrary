﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using CommonLib.Extensions;

namespace CommonLib
{
	public static class Ensure
	{
		public static Ensure<TValue> Is<TValue>(TValue that)
		{
			return new Ensure<TValue>(that);
		}

		public static EnsureExpression<TValue> Is<TValue>(Expression<Func<TValue>> that)
		{
			if (that == null)
				throw new ArgumentNullException("that");
			return new EnsureExpression<TValue>(that);
		}

		public static IEnumerable<Ensure<TValue>> Are<TValue>(params TValue[] values)
		{
			if (values == null) throw new ArgumentNullException("values");
			return values.Select(value => new Ensure<TValue>(value));
		}

		public static IEnumerable<Ensure<object>> Are(params object[] values)
		{
			if (values == null) throw new ArgumentNullException("values");
			return values.Select(value => new Ensure<object>(value));
		}

		public static IEnumerable<EnsureExpression<TValue>> Are<TValue>(params Expression<Func<TValue>>[] values)
		{
			if (values == null)
				throw new ArgumentNullException("values");

			return values.Select(value => new EnsureExpression<TValue>(value));
		}

		public static Ensure<Action> Do(Action method)
		{
			return new Ensure<Action>(method);
		}
	}

	public class Ensure<TValue>
	{
		public TValue Value { get; protected set; }

		internal Ensure() { }

		internal Ensure(TValue value)
		{
			Value = value;
		}
	}

	public class EnsureExpression<TValue> : Ensure<TValue>
	{
		public MemberExpression ValueExpression { get; protected set; }

		public string MemberName { get { return ValueExpression.Member.Name; } }

		internal EnsureExpression(Expression<Func<TValue>> valueExpression)
		{
			if (valueExpression == null) 
				throw new ArgumentNullException("valueExpression");

			ValueExpression = valueExpression.Body as MemberExpression;
			if (ValueExpression == null)
				throw new InvalidConstraintException("Type of 'ValueExpression' must be a MemberExpression!");

			Value = valueExpression.Compile()();
		}
	}

	public static class EnsureExtension
	{
		private static IEnumerable<Ensure<TValue>> ApplyToArray<TValue>(this IEnumerable<Ensure<TValue>> values, Action<Ensure<TValue>> action)
		{
			if (values == null)
				throw new ArgumentNullException("values");
			if (action == null) 
				throw new ArgumentNullException("action");

			var _values = (values as Ensure<TValue>[]) != null ? values : values.ToArray();

			foreach (var value in _values)
				action(value);

			return _values;
		}

		public static EnsureExpression<TReturns> Is<TValue, TReturns>(this EnsureExpression<TValue> that, Expression<Func<TReturns>> value)
		{
			return Ensure.Is(value);
		}

		public static Ensure<TReturns> Is<TValue, TReturns>(this Ensure<TValue> that, TReturns value)
		{
			return Ensure.Is(value);
		}

		public static Ensure<TReturns> Is<TValue, TReturns>(this IEnumerable<Ensure<TValue>> that, TReturns value)
		{
			return Ensure.Is(value);
		}

		public static IEnumerable<Ensure<TReturns>> Are<TValue, TReturns>(this Ensure<TValue> that, params TReturns[] value)
		{
			return Ensure.Are(value);
		}

		public static IEnumerable<Ensure<TReturns>> Are<TValue, TReturns>(this IEnumerable<Ensure<TValue>> that, params TReturns[] value)
		{
			return Ensure.Are(value);
		}

		public static IEnumerable<Ensure<TValue>> And<TValue>(this Ensure<TValue> that, TValue value)
		{
			if (that.Value is IEnumerable<TValue>)
				return Ensure.Are((that.Value as IEnumerable<TValue>).Concat(new[] { value }).ToArray());
			return Ensure.Are(that.Value, value);
		}

		/*public static IEnumerable<Ensure<object>> And<TValue, TReturns>(this Ensure<TValue> that, TReturns value)
		{
			if (that.Value is IEnumerable<TValue>)
				return Ensure.Are((that.Value as IEnumerable<TValue>).Cast<object>().Concat(new[] { (object)value }).ToArray());
			return Ensure.Are(that.Value, value);
		}*/

		public static EnsureExpression<TValue> NotNull<TValue>(this EnsureExpression<TValue> that, Exception exception)
			where TValue : class
		{
			if (that == null)
				throw new ArgumentNullException("that");
			if (exception == null)
				throw new ArgumentNullException("exception");

			if (that.Value == null)
				throw exception;

			return that;
		}

		public static EnsureExpression<TValue> NotNull<TValue>(this EnsureExpression<TValue> that)
			where TValue : class
		{
			if (that == null)
				throw new ArgumentNullException("that");

			return NotNull(that, new ArgumentNullException(that.MemberName));
		}

		public static Ensure<TValue> NotNull<TValue>(this Ensure<TValue> that, string parameterName = "") where TValue : class
		{
			return NotNull(that, new ArgumentNullException(string.Format("{0} cannot be null!", parameterName)));
		}

		public static Ensure<TValue> NotNull<TValue>(this Ensure<TValue> that, Exception exception) where TValue : class
		{
			if (exception == null)
				throw new ArgumentNullException("exception");

			if (that != null && that.Value == null)
				throw exception;
			return that;
		}

		public static IEnumerable<Ensure<TValue>> NotNull<TValue>(this IEnumerable<Ensure<TValue>> values) where TValue : class
		{
			return values.ApplyToArray(value => value.NotNull());
		}

		public static IEnumerable<Ensure<TValue>> NotNull<TValue>(this IEnumerable<Ensure<TValue>> values, Exception exception) where TValue : class
		{
			return values.ApplyToArray(value => value.NotNull(exception));
		}

		public static EnsureExpression<string> NotBlank(this EnsureExpression<string> that)
		{
			return null;
		}

		public static Ensure<string> NotBlank(this Ensure<string> that, string parameterName = "")
		{
			return that.NotBlank(new ArgumentException(string.Format("{0} cannot be blank!", parameterName)));
		}

		public static Ensure<string> NotBlank(this Ensure<string> that, Exception exception)
		{
			if (that != null && string.IsNullOrWhiteSpace(that.Value))
				throw exception;

			return that;
		}

		public static IEnumerable<Ensure<string>> NotBlank(this IEnumerable<Ensure<string>> values)
		{
			return values.ApplyToArray(value => value.NotBlank());
		}

		public static IEnumerable<Ensure<string>> NotBlank(this IEnumerable<Ensure<string>> values, Exception exception)
		{
			return values.ApplyToArray(value => value.NotBlank(exception));
		}

		public static Ensure<string> Match(this Ensure<string> that, string regularExpression)
		{
			Ensure.Is(that).NotNull()
				  .Is(regularExpression).NotBlank();

			if (!Regex.IsMatch(that.Value, regularExpression))
				throw new Exception(string.Format("This '{0}' string does not match with {1}!", that.Value, regularExpression));

			return that;
		}

		public static Ensure<TValue> Requires<TValue>(this Ensure<TValue> that, Predicate<TValue> predicate, Exception exception)
		{
			Ensure.Is(predicate).NotNull("predicate");

			if (that != null && !predicate(that.Value))
				throw exception;

			return that;
		}

		public static Ensure<TValue> Requires<TValue>(this Ensure<TValue> that, Predicate<TValue> predicate, string parameterName = "")
		{
			return that.Requires(predicate, new Exception(string.Format("Invalid value of {0}!", parameterName)));
		}

		public static EnsureExpression<TValue> Requires<TValue>(this EnsureExpression<TValue> that, Predicate<TValue> predicate)
		{
			return that.Requires(predicate, new Exception(string.Format("Invalid value of {0}!", that.MemberName)));
		}

		public static EnsureExpression<TValue> Requires<TValue>(this EnsureExpression<TValue> that, Predicate<TValue> predicate, Exception exception)
		{
			Ensure.Is(() => predicate).NotNull()
				  .Is(() => that).NotNull();

			if (!predicate(that.Value))
				throw exception;

			return that;
		}

		public static IEnumerable<Ensure<TValue>> Require<TValue>(this IEnumerable<Ensure<TValue>> values, Predicate<TValue> predicate, Exception exception)
		{
			return values.ApplyToArray(value => value.Requires(predicate, exception));
		}

		public static IEnumerable<Ensure<TValue>> Require<TValue>(this IEnumerable<Ensure<TValue>> values, Predicate<TValue> predicate)
		{
			return values.ApplyToArray(value => value.Requires(predicate));
		}

		public static IEnumerable<Ensure<TValue>> Require<TValue>(this IEnumerable<Ensure<TValue>> values, Func<TValue, TValue, bool> predicate, Exception exception)
		{
			var _values = values as Ensure<TValue>[] ?? values.ToArray();

			if (_values.Count() < 2 || predicate == null)
				throw new Exception();

			if ((bool)predicate.DynamicInvoke(_values.Take(2).Select(value => (object)value.Value).ToArray()) == false)
				throw exception;

			return _values;
		}

		public static IEnumerable<Ensure<TValue>> Require<TValue>(this IEnumerable<Ensure<TValue>> values, Func<TValue, TValue, bool> predicate)
		{
			return values.Require(predicate, new Exception());
		}

		public static Ensure<TValue> Interval<TValue>(this Ensure<TValue> that, TValue minimum, TValue maximum)
			where TValue : IComparable
		{
			Ensure.Are(minimum, maximum)
				//.NotNull(new ArgumentException("Minimum and Maximum cannot be null!"))
				  .Require((min, max) => min.CompareTo(max) <= 0);

			if (that.IsNull())
				throw new ArgumentNullException("that");

			if (minimum.CompareTo(that.Value) > 0 || that.Value.CompareTo(maximum) > 0)
				throw new Exception("Value must be between minimum and maximum!");

			return that;
		}

		public static Ensure<IEnumerable<TValue>> NotEmpty<TValue>(this Ensure<IEnumerable<TValue>> that, Exception exception)
		{
			if (that != null && that.Value != null && !that.Value.Any())
				throw exception;

			return that;
		}

		public static Ensure<IEnumerable<TValue>> NotEmpty<TValue>(this Ensure<IEnumerable<TValue>> that, string parameterName = "")
		{
			return that.NotEmpty(new Exception(string.Format("{0} enumerable cannot be empty!", parameterName)));
		}

		public static Ensure<TValue[]> NotEmpty<TValue>(this Ensure<TValue[]> that, Exception exception)
		{
			if (that != null && that.Value != null && !that.Value.Any())
				throw exception;

			return that;
		}

		public static Ensure<TValue[]> NotEmpty<TValue>(this Ensure<TValue[]> that, string parameterName = "")
		{
			return that.NotEmpty(new Exception(string.Format("{0} enumerable cannot be empty!", arg0: parameterName)));
		}

		public static Ensure<Action> Catch<TException>(this Ensure<Action> that, Action<TException> exceptionHandler)
			where TException : Exception
		{
			if (that == null || that.Value == null)
				throw new ArgumentNullException("that");

			return new Ensure<Action>(() => {
				try {
					that.Value();
				}
				catch (TException ex) {
					if (exceptionHandler != null)
						exceptionHandler(ex);
				}
			});
		}

		public static Ensure<Action> Invoke(this Ensure<Action> that)
		{
			if (that == null || that.Value == null)
				throw new ArgumentNullException("that");

			that.Value();
			return that;
		}
	}
}
