﻿namespace SQuan.Helpers.Maui.Mvvm;

/// <summary>
/// Indicates that a field or property is observable, allowing changes to its value to be tracked and responded to.
/// </summary>
[AttributeUsage(AttributeTargets.Property, AllowMultiple = false, Inherited = false)]
public class ObservablePropertyAttribute : Attribute
{
}
