using System;
using System.Collections.Generic;
using System.Linq;

namespace CommonLib
{
	public class Case<TType>
	{
		public IDictionary<Func<bool>, Func<TType>> Cases { get; protected set; }

		public TType Value
		{
			get { return Cases.FirstOrDefault(value => value.Key()).Value(); }
		}

		public IEnumerable<TType> Values
		{
			get { return Cases.Where(value => value.Key()).Select(value => value.Value()); }
		}

		internal Case()
		{
			Cases = new Dictionary<Func<bool>, Func<TType>>();
		}

		public static implicit operator TType(Case<TType> @case)
		{
			return @case.Value;
		}

		public static implicit operator TType[](Case<TType> @case)
		{
			return @case.Values.ToArray();
		}

		public Case<TType> If(Func<bool> condition, Func<TType> value)
		{
			Ensure
				.Is(() => condition).NotNull()
				.Is(() => value).NotNull();

			if (Cases.ContainsKey(condition))
				Cases[condition] = value;
			else
				Cases.Add(condition, value);

			return this;
		}

		public Case<TType> If(bool condition, Func<TType> value)
		{
			return If(() => condition, value);
		}

		public Case<TType> If(Func<bool> condition, TType value)
		{
			return If(condition, () => value);
		}

		public Case<TType> If(bool condition, TType value)
		{
			return If(() => condition, () => value);
		}

		public Case<TType> Else(Func<TType> value)
		{
			return If(() => true, value);
		}

		public Case<TType> Else(TType value)
		{
			return Else(() => value);
		}
	}
}