using System.Globalization;
using System.Numerics;
using VHR = Sagara.Core.Validation.ValidationHelperResources;

namespace Sagara.Core.Validation;

/// <summary>
/// Contains common validation methods that work with <see cref="ValidatableProperty{TValue}"/> and 
/// <see cref="RequestError"/>.
/// </summary>
public static class ValidationHelper
{
    /// <summary>
    /// Add an error message if property's value is null or white space.
    /// </summary>
    public static void CheckRequiredField(ValidatableProperty<string?> property, ICollection<RequestError> errors)
    {
        Check.NotNull(errors);

        if (string.IsNullOrWhiteSpace(property.Value))
        {
            // Justification: can't use composite format; we're loading a localized string from a resource file.
#pragma warning disable CA1863 // Use 'CompositeFormat'
            errors.Add(new RequestError(property.PropertyName, string.Format(CultureInfo.CurrentCulture, VHR.RequiredField, property.GetDisplayName())));
#pragma warning restore CA1863 // Use 'CompositeFormat'
        }
    }

    /// <summary>
    /// <para>Add an error message if property's value is null.</para>
    /// <para>Use this overload for reference types.</para>
    /// </summary>
    public static void CheckRequiredField<T>(ValidatableProperty<T> property, ICollection<RequestError> errors)
        where T : class?
    {
        Check.NotNull(errors);

        if (property.Value is null)
        {
            // Justification: can't use composite format; we're loading a localized string from a resource file.
#pragma warning disable CA1863 // Use 'CompositeFormat'
            errors.Add(new RequestError(property.PropertyName, string.Format(CultureInfo.CurrentCulture, VHR.RequiredField, property.GetDisplayName())));
#pragma warning restore CA1863 // Use 'CompositeFormat'
        }
    }

    /// <summary>
    /// <para>Add an error message if property's value is null.</para>
    /// <para>Use this overload for nullable value types.</para>
    /// </summary>
    public static void CheckRequiredField<T>(ValidatableProperty<T?> property, ICollection<RequestError> errors)
        where T : struct
    {
        Check.NotNull(errors);

        if (property.Value is null)
        {
            // Justification: can't use composite format; we're loading a localized string from a resource file.
#pragma warning disable CA1863 // Use 'CompositeFormat'
            errors.Add(new RequestError(property.PropertyName, string.Format(CultureInfo.CurrentCulture, VHR.RequiredField, property.GetDisplayName())));
#pragma warning restore CA1863 // Use 'CompositeFormat'
        }
    }

    /// <summary>
    /// Add an error message if property's value length exceeds maxLength.
    /// </summary>
    public static void CheckStringMaxLength(ValidatableProperty<string?> property, int maxLength, ICollection<RequestError> errors)
    {
        Check.NotNull(errors);

        if (property.Value?.Length > maxLength)
        {
            // Justification: can't use composite format; we're loading a localized string from a resource file.
#pragma warning disable CA1863 // Use 'CompositeFormat'
            errors.Add(new RequestError(property.PropertyName, string.Format(CultureInfo.CurrentCulture, VHR.StringMaxLength, property.GetDisplayName(), maxLength)));
#pragma warning restore CA1863 // Use 'CompositeFormat'
        }
    }

    /// <summary>
    /// Add an error message if property's value length is less than minLength.
    /// </summary>
    public static void CheckStringMinLength(ValidatableProperty<string?> property, int minLength, ICollection<RequestError> errors)
    {
        Check.NotNull(errors);

        if (property.Value?.Length < minLength)
        {
            // Justification: can't use composite format; we're loading a localized string from a resource file.
#pragma warning disable CA1863 // Use 'CompositeFormat'
            errors.Add(new RequestError(property.PropertyName, string.Format(CultureInfo.CurrentCulture, VHR.StringMinLength, property.GetDisplayName(), minLength)));
#pragma warning restore CA1863 // Use 'CompositeFormat'
        }
    }

    /// <summary>
    /// Return displayName if it's not null; else, return propertyName.
    /// </summary>
    public static string GetDisplayName<T>(this ValidatableProperty<T> property)
        => property.DisplayName ?? property.PropertyName;

    /// <summary>
    /// Add an error message if property's value is less than or equal to <paramref name="threshold"/>.
    /// </summary>
    public static void CheckGreaterThan<TNumber>(ValidatableProperty<TNumber?> property, TNumber threshold, ICollection<RequestError> errors)
        where TNumber : struct, INumber<TNumber>
    {
        Check.NotNull(errors);

        if (property.Value is null)
        {
            // This is note a required field validator.
            return;
        }

        if (property.Value.Value <= threshold)
        {
            // Justification: can't use composite format; we're loading a localized string from a resource file.
#pragma warning disable CA1863 // Use 'CompositeFormat'
            errors.Add(new RequestError(property.PropertyName, string.Format(CultureInfo.CurrentCulture, VHR.GreaterThan, property.GetDisplayName(), threshold, property.Value)));
#pragma warning restore CA1863 // Use 'CompositeFormat'
        }
    }

    /// <summary>
    /// Add an error message if property's value is less than <paramref name="threshold"/>.
    /// </summary>
    public static void CheckGreaterThanOrEqual<TNumber>(ValidatableProperty<TNumber?> property, TNumber threshold, ICollection<RequestError> errors)
        where TNumber : struct, INumber<TNumber>
    {
        Check.NotNull(errors);

        if (property.Value is null)
        {
            // This is note a required field validator.
            return;
        }

        if (property.Value.Value < threshold)
        {
            // Justification: can't use composite format; we're loading a localized string from a resource file.
#pragma warning disable CA1863 // Use 'CompositeFormat'
            errors.Add(new RequestError(property.PropertyName, string.Format(CultureInfo.CurrentCulture, VHR.GreaterThanOrEqual, property.GetDisplayName(), threshold, property.Value)));
#pragma warning restore CA1863 // Use 'CompositeFormat'
        }
    }

    /// <summary>
    /// Add an error message if property's value is greater than or equal to <paramref name="threshold"/>.
    /// </summary>
    public static void CheckLessThan<TNumber>(ValidatableProperty<TNumber?> property, TNumber threshold, ICollection<RequestError> errors)
        where TNumber : struct, INumber<TNumber>
    {
        Check.NotNull(errors);

        if (property.Value is null)
        {
            // This is note a required field validator.
            return;
        }

        if (property.Value.Value >= threshold)
        {
            // Justification: can't use composite format; we're loading a localized string from a resource file.
#pragma warning disable CA1863 // Use 'CompositeFormat'
            errors.Add(new RequestError(property.PropertyName, string.Format(CultureInfo.CurrentCulture, VHR.LessThan, property.GetDisplayName(), threshold, property.Value)));
#pragma warning restore CA1863 // Use 'CompositeFormat'
        }
    }

    /// <summary>
    /// Add an error message if property's value is greater than <paramref name="threshold"/>.
    /// </summary>
    public static void CheckLessThanOrEqual<TNumber>(ValidatableProperty<TNumber?> property, TNumber threshold, ICollection<RequestError> errors)
        where TNumber : struct, INumber<TNumber>
    {
        Check.NotNull(errors);

        if (property.Value is null)
        {
            // This is note a required field validator.
            return;
        }

        if (property.Value.Value > threshold)
        {
            // Justification: can't use composite format; we're loading a localized string from a resource file.
#pragma warning disable CA1863 // Use 'CompositeFormat'
            errors.Add(new RequestError(property.PropertyName, string.Format(CultureInfo.CurrentCulture, VHR.LessThanOrEqual, property.GetDisplayName(), threshold, property.Value)));
#pragma warning restore CA1863 // Use 'CompositeFormat'
        }
    }
}

