namespace Sagara.Core.Validation;

/// <summary>
/// Used to pass validation error messages from validation logic back to the UI for display.
/// </summary>
/// <param name="PropertyName">The name of the property, if any, that generated the error. If there is no property,
/// use string.Empty.</param>
/// <param name="ErrorMessage">A message describing the error.</param>
public readonly record struct RequestError(string PropertyName, string ErrorMessage);
