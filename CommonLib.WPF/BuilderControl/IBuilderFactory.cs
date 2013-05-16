using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace CommonLib.WPF.BuilderControl
{
	public interface IBuilderFactory<out TResult>
	{
		IBuilderFactory<TResult> Bind<TType, TElement>(Func<TType, TElement> elementBuilder);

		TResult Build();
	}

	public class BuilderFactory : IBuilderFactory<DependencyObject>
	{
		protected readonly Dictionary<Type, Func<object, DependencyObject>> _TypeBindingDictionary;
		protected readonly Dictionary<string, Func<object, DependencyObject>> _NameBindingDictionary;

		public Type Model { get; set; }

		public BuilderFactory()
		{
			_TypeBindingDictionary = new Dictionary<Type, Func<object, DependencyObject>>();
			_NameBindingDictionary = new Dictionary<string, Func<object, DependencyObject>>();
		}

		IBuilderFactory<DependencyObject> IBuilderFactory<DependencyObject>.Bind<TType, TElement>(Func<TType, TElement> elementBuilder)
		{
			throw new NotImplementedException();
		}

		DependencyObject IBuilderFactory<DependencyObject>.Build()
		{
			return Build();
		}

		public DependencyObject Build()
		{
			var properties = Model.GetProperties(BindingFlags.Public);
			var stackPanel = new StackPanel();

			foreach (var property in properties)
			{
				var name = property.Name;
				var type = property.PropertyType;
			}

			return stackPanel;
		}

		public BuilderFactory Bind<TType, TElement>(Func<TType, TElement> elementBuilder)
			where TElement : DependencyObject
		{
			Ensure.Is(elementBuilder).NotNull("elementBuilder")
				  .Requires(_ => !_TypeBindingDictionary.ContainsKey(typeof(TType)));

			_TypeBindingDictionary.Add(typeof(TType), value => elementBuilder((TType)value));
			return this;
		}

		public BuilderFactory Bind<TType, TElement>(string propertyName, Func<TType, TElement> elementBuilder)
			where TElement : DependencyObject
		{
			Ensure.Is(propertyName)
				.NotBlank("propertyName")
				.Requires(_ => !_NameBindingDictionary.ContainsKey(propertyName))
				.Is(elementBuilder)
				.NotNull("elementBuilder");

			_NameBindingDictionary.Add(propertyName, value => elementBuilder((TType)value));
			return this;
		}
	}
}
