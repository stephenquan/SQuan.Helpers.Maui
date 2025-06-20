﻿namespace SQuan.Helpers.Maui.Localization;

/// <summary>
/// A markup extension that provides localized strings based on a specified key.
/// </summary>
[ContentProperty(nameof(Key))]
[AcceptEmptyServiceProvider]
public partial class LocalizeExtension : BindableObject, IMarkupExtension<BindingBase>
{
	/// <summary>
	/// Bindable proeprty for <see cref="Key"/>."/>
	/// </summary>
	public static readonly BindableProperty KeyProperty = BindableProperty.Create(nameof(Key), typeof(string), typeof(LocalizeExtension), string.Empty,
		propertyChanged: (b, o, n) => ((LocalizeExtension)b).OnTranslatedValueChanged());

	/// <summary>
	/// Gets or sets the localization key for the string to be translated.
	/// </summary>
	public string Key
	{
		get => (string)GetValue(KeyProperty);
		set => SetValue(KeyProperty, value);
	}

	/// <summary>
	/// Gets the localized string for the specified key.
	/// </summary>
	public string TranslatedValue => LocalizationManager.Current.GetString(Key);

	void OnTranslatedValueChanged() => OnPropertyChanged(nameof(TranslatedValue));

	/// <summary>
	/// Initializes a new instance of the <see cref="LocalizeExtension"/> class.
	/// </summary>
	public LocalizeExtension()
	{
		LocalizationManager.Current.CurrentUICultureChanged += (s, e) => OnTranslatedValueChanged();
	}

	/// <summary>
	/// Provides a binding object based on the specified service provider.
	/// </summary>
	/// <param name="serviceProvider">An object that provides services for the binding. This parameter is typically used to resolve services or context
	/// information required to create the binding.</param>
	/// <returns>A <see cref="BindingBase"/> instance that represents the binding to be used. The specific type and configuration of
	/// the binding depend on the implementation.</returns>
	BindingBase IMarkupExtension<BindingBase>.ProvideValue(IServiceProvider serviceProvider)
		=> BindingBase.Create(static (LocalizeExtension ctx) => ctx.TranslatedValue, BindingMode.OneWay, source: this);

	object IMarkupExtension.ProvideValue(IServiceProvider serviceProvider)
		=> (this as IMarkupExtension<BindingBase>).ProvideValue(serviceProvider);
}
