namespace Sagara.Core.Validation;

/// <summary>
/// Used to populate values from MediatR models and pass to a validation service class. This allows the validation
/// service to return a list of errors containing the HTML form element names that generated the errors so that
/// we can highlight them on the form using JavaScript.
/// </summary>
/// <typeparam name="TValue">The value of value to validate.</typeparam>
/// <param name="Value">The value to validate.</param>
/// <param name="PropertyName">Required. The name of the property corresponding to the HTML form element.</param>
/// <param name="DisplayName">Optional. User-friendly display name.</param>
public readonly record struct ValidatableProperty<TValue>(TValue Value, string PropertyName, string? DisplayName);
