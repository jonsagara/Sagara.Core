using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Reflection;

namespace Sagara.Core.Enums;

/// <summary>
/// <para>Consolidated wrapper for enum values, valid values, and display names.</para>
/// <para>Inspired by: https://devblogs.microsoft.com/premier-developer/dissecting-new-generics-constraints-in-c-7-3/ </para>
/// </summary>
/// <typeparam name="TEnum">The type of enum.</typeparam>
public static class EnumTraits<TEnum>
    where TEnum : struct, Enum
{
    private static readonly Type _enumType;
    private static readonly TEnum[] _allValues;
    private static readonly long[] _duplicateNumericValues;
    private static readonly HashSet<TEnum> _validValues;
    private static readonly Dictionary<TEnum, string> _validValueDisplayNames;


    // Justification: I can't think of a way to implement this without switching to an instance based class.
#pragma warning disable CA1000 // Do not declare static members on generic types

    /// <summary>
    /// Returns true if the enum declaration is empty; false otherwise.
    /// </summary>
    public static bool IsEmpty { get; }

    /// <summary>
    /// Returns true if the enum is decorated with a <see cref="FlagsAttribute"/>; false otherwise.
    /// </summary>
    public static bool HasFlagsAttribute { get; }

    /// <summary>
    /// Returns the enum's Type information.
    /// </summary>
    public static Type EnumType
        => _enumType;

    /// <summary>
    /// Returns all defined enum values.
    /// </summary>
    public static IReadOnlyCollection<TEnum> AllValues
        => _allValues;

    /// <summary>
    /// Returns all enum values that are not marked with <see cref="InvalidEnumValueAttribute" />.
    /// </summary>
    public static IReadOnlyCollection<TEnum> ValidValues
        => _validValues;


    // Justification: The static fields must be initialized in a specific order.
#pragma warning disable CA1810 // Initialize reference type static fields inline
    static EnumTraits()
#pragma warning restore CA1810 // Initialize reference type static fields inline
    {
        _enumType = typeof(TEnum);

        HasFlagsAttribute = _enumType.GetCustomAttributes<FlagsAttribute>().Any();


        //
        // Construct a list of all defined values.
        //

        _allValues = Enum.GetValues<TEnum>();
        IsEmpty = _allValues.Length == 0;


        // 
        // Ensure there are no duplicate underlying numeric values.
        //

        _duplicateNumericValues = GetDuplicateNumericValues(_enumType, _allValues);

        //
        // Construct a HashSet of valid values by filtering out any values decorated with an InvalidEnumValueAttribute.
        //

        _validValues = FilterOutInvalidValues(_enumType, _allValues);

        //
        // Construct a dictionary of valid values and their corresponding display names.
        //

        _validValueDisplayNames = _validValues
            .ToDictionary(t => t, t => GetDisplayNameOrEnumValueName(t));
    }

    /// <summary>
    /// Returns true if the enum value is in the list of valid values; false otherwise.
    /// </summary>
    public static bool IsValid(TEnum value)
        => _validValues.Contains(value);

    /// <summary>
    /// Throws an <see cref="InvalidOperationException"/> if the enum has one or more duplicate underlying values defined.
    /// </summary>
    /// <exception cref="InvalidOperationException">Throws if the enum has one or more duplicate underlying values defined.</exception>
    public static void EnsureNoDuplicateValues()
    {
        if (_duplicateNumericValues.Length > 0)
        {
            throw new InvalidOperationException($"{_enumType.FullName} has one or more duplicate numeric values: {string.Join(", ", _duplicateNumericValues.Select(dnv => dnv.ToString(CultureInfo.InvariantCulture)))}");
        }
    }

    /// <summary>
    /// Return the display name for the enum value, which is either from the [Display] attribute 
    /// or the property name.
    /// </summary>
    public static string GetDisplayName(TEnum value)
    {
        if (!IsValid(value))
        {
            throw new ArgumentException($"Invalid {_enumType.FullName} enum value '{value}'.", nameof(value));
        }

        return _validValueDisplayNames[value];
    }

    /// <summary>
    /// Return the display name for the enum value, which is either from the [Display] attribute 
    /// or the property name. If not found, return null.
    /// </summary>
    public static string? GetDisplayNameOrDefault(TEnum value)
    {
        return _validValueDisplayNames.TryGetValue(value, out string? displayName)
            ? displayName
            : null;
    }


    //
    // Private methods
    //

    private static HashSet<TEnum> FilterOutInvalidValues(Type enumType, IReadOnlyCollection<TEnum> allValues)
    {
        return allValues
            .Where(v =>
            {
                var enumValueName = v.ToString();

                // Use the enum value name to get its MemberInfo.
                var enumValueMemberInfo = enumType
                    .GetMember(enumValueName)
                    .SingleOrDefault();

                // We're getting the enum values straight from the enum itself, and not from outside code, so there
                //   should be exactly one matching enum value. This will give a better, more descriptive error 
                //   message vs calling .Single() if something goes haywire.
                if (enumValueMemberInfo is null)
                {
                    throw new InvalidOperationException($"{enumType.FullName} does not contain enum value {enumValueName}");
                }

                // If the enum value does not have the [InvalidEnumValue] attribute, it's valid. Otherwise, it's 
                //   an invalid value, and we should exclude it.
                return enumValueMemberInfo.GetCustomAttribute<InvalidEnumValueAttribute>() is null;
            })
            .ToHashSet();
    }

    private static long[] GetDuplicateNumericValues(Type enumType, IReadOnlyCollection<TEnum> allValues)
    {
        // Convert from the underlying type to the biggest underlying type possible (Int64).
        var underlyingType = Enum.GetUnderlyingType(enumType);

        var longValues = allValues
            .Select(v => Convert.ChangeType(v, underlyingType, CultureInfo.InvariantCulture))
            .Select(v => Convert.ToInt64(v, CultureInfo.InvariantCulture))
            .ToArray();

        // Find any values that occur more than once, and return them as a distinct array.
        return longValues
            .GroupBy(v => v)
            .Where(grp => grp.Count() > 1)
            .Select(grp => grp.Key)
            .OrderBy(v => v)
            .ToArray();
    }

    /// <summary>
    /// If an enum value is decorated with a Display attribute, get its Name. Otherwise, return
    /// the enum value name as a string.
    /// </summary>
    private static string GetDisplayNameOrEnumValueName(TEnum value)
    {
        var enumType = value.GetType();
        var enumValueName = value.ToString();

        var enumValMemberInfo = enumType
            .GetMember(enumValueName)
            .FirstOrDefault();

        if (enumValMemberInfo is null)
        {
            throw new InvalidOperationException($"{enumType.FullName} does not contain enum value {enumValueName}");
        }

        // DisplayAttribute is defined with AllowMultiple == false.
        var displayAttribute = enumValMemberInfo.GetCustomAttribute<DisplayAttribute>();

        // Use DisplayAttribute.GetName() if there's an attribute, otherwise fall back to the enum value 
        //   name as a string.
        return displayAttribute?.GetName() ?? enumValueName;
    }

#pragma warning restore CA1000 // Do not declare static members on generic types
}
