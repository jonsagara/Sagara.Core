using System.Globalization;
using VHR = Sagara.Core.Validation.ValidationHelperResources;

namespace Sagara.Core.Validation;

/// <summary>
/// Contains common validation methods that work with <see cref="ValidatableProperty{TValue}"/> and 
/// <see cref="RequestError"/>.
/// </summary>
public static class ValidationHelper
{
    /// <summary>
    /// Add an error message if value is null or white space.
    /// </summary>
    public static void CheckRequiredField(ValidatableProperty<string?> property, ICollection<RequestError> errors)
    {
        Check.NotNull(errors);

        if (string.IsNullOrWhiteSpace(property.Value))
        {
            errors.Add(new RequestError(property.PropertyName, string.Format(CultureInfo.CurrentCulture, VHR.RequiredField, property.GetDisplayName())));
        }
    }

    /// <summary>
    /// <para>Add an error message if value is null.</para>
    /// <para>Use this overload for reference types.</para>
    /// </summary>
    public static void CheckRequiredField<T>(ValidatableProperty<T> property, ICollection<RequestError> errors)
        where T : class?
    {
        Check.NotNull(errors);

        if (property.Value is null)
        {
            errors.Add(new RequestError(property.PropertyName, string.Format(CultureInfo.CurrentCulture, VHR.RequiredField, property.GetDisplayName())));
        }
    }

    /// <summary>
    /// <para>Add an error message if value is null.</para>
    /// <para>Use this overload for nullable value types.</para>
    /// </summary>
    public static void CheckRequiredField<T>(ValidatableProperty<T?> property, ICollection<RequestError> errors)
        where T : struct
    {
        Check.NotNull(errors);

        if (property.Value is null)
        {
            errors.Add(new RequestError(property.PropertyName, string.Format(CultureInfo.CurrentCulture, VHR.RequiredField, property.GetDisplayName())));
        }
    }

    /// <summary>
    /// Add an error message if value's length exceeds maxLength.
    /// </summary>
    public static void CheckStringMaxLength(ValidatableProperty<string?> property, int maxLength, ICollection<RequestError> errors)
    {
        Check.NotNull(errors);

        if (property.Value?.Length > maxLength)
        {
            errors.Add(new RequestError(property.PropertyName, string.Format(CultureInfo.CurrentCulture, VHR.StringMaxLength, property.GetDisplayName(), maxLength)));
        }
    }

    /// <summary>
    /// Add an error message if value's length is less than minLength.
    /// </summary>
    public static void CheckStringMinLength(ValidatableProperty<string?> property, int minLength, ICollection<RequestError> errors)
    {
        Check.NotNull(errors);

        if (property.Value?.Length < minLength)
        {
            errors.Add(new RequestError(property.PropertyName, string.Format(CultureInfo.CurrentCulture, VHR.StringMinLength, property.GetDisplayName(), minLength)));
        }
    }

    /// <summary>
    /// Return displayName if it's not null; else, return propertyName.
    /// </summary>
    public static string GetDisplayName<T>(this ValidatableProperty<T> property)
        => property.DisplayName ?? property.PropertyName;
}

