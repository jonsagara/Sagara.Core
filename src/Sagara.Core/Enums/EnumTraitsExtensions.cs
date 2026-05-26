#if NET10_0_OR_GREATER
namespace Sagara.Core.Enums;

/// <summary>
/// C# 14 extension members for <see cref="EnumTraits{TEnum}"/>. Provides instance-level and
/// type-level extension members so callers can invoke enum helpers directly on enum values or
/// enum types rather than through the static generic class.
/// </summary>
public static class EnumTraitsExtensions
{
#pragma warning disable CA1034

    /// <summary>
    /// Instance extension members — called on a <typeparamref name="TEnum"/> value.
    /// </summary>
    extension<TEnum>(TEnum value)
        where TEnum : struct, Enum
    {
        /// <summary>
        /// Returns true if the enum value is in the list of valid values; false otherwise.
        /// </summary>
        public bool IsValidValue()
            => EnumTraits<TEnum>.IsValidValue(value);

        /// <summary>
        /// Return the display name for the enum value, which is either from the [Display] attribute
        /// or the property name.
        /// </summary>
        public string GetDisplayName()
            => EnumTraits<TEnum>.GetDisplayName(value);

        /// <summary>
        /// Return the display name for the enum value, which is either from the [Display] attribute
        /// or the property name. If not found, return null.
        /// </summary>
        public string? GetDisplayNameOrDefault()
            => EnumTraits<TEnum>.GetDisplayNameOrDefault(value);
    }

    /// <summary>
    /// Instance extension members — called on a <typeparamref name="TEnum"/> value.
    /// </summary>
    extension<TEnum>(TEnum? value)
        where TEnum : struct, Enum
    {
        /// <summary>
        /// Return the display name for the enum value, which is either from the [Display] attribute
        /// or the property name. If not found, return null.
        /// </summary>
        public string? GetDisplayNameOrDefault()
            => EnumTraits<TEnum>.GetDisplayNameOrDefault(value);
    }

    /// <summary>
    /// Static extension members — called on the <typeparamref name="TEnum"/> type itself.
    /// </summary>
    extension<TEnum>(TEnum)
        where TEnum : struct, Enum
    {
        // Justification: These are deliberately methods rather than properties to avoid conflicts with
        //   enum value names. CA1000 is unavoidable on static members of a generic extension block.
#pragma warning disable CA1000 // Do not declare static members on generic types
#pragma warning disable CA1024 // Use properties where appropriate

        /// <summary>
        /// Returns true if the enum declaration is empty; false otherwise.
        /// </summary>
        public static bool IsEmpty()
            => EnumTraits<TEnum>.IsEmpty;

        /// <summary>
        /// Returns true if the enum is decorated with a <see cref="FlagsAttribute"/>; false otherwise.
        /// </summary>
        public static bool HasFlagsAttribute()
            => EnumTraits<TEnum>.HasFlagsAttribute;

        /// <summary>
        /// Returns the enum's Type information.
        /// </summary>
        public static Type GetEnumType()
            => EnumTraits<TEnum>.EnumType;

        /// <summary>
        /// Returns all defined enum values.
        /// </summary>
        public static IReadOnlyCollection<TEnum> GetAllValues()
            => EnumTraits<TEnum>.AllValues;

        /// <summary>
        /// Returns all enum values that are not marked with <see cref="InvalidEnumValueAttribute"/>.
        /// </summary>
        public static IReadOnlyCollection<TEnum> GetValidValues()
            => EnumTraits<TEnum>.ValidValues;

        /// <summary>
        /// Throws an <see cref="InvalidOperationException"/> if the enum has one or more duplicate
        /// underlying values defined.
        /// </summary>
        /// <exception cref="InvalidOperationException">Throws if the enum has one or more duplicate underlying values defined.</exception>
        public static void EnsureNoDuplicateValues()
            => EnumTraits<TEnum>.EnsureNoDuplicateValues();

#pragma warning restore CA1024
#pragma warning restore CA1000
    }

#pragma warning restore CA1034
}

#endif
