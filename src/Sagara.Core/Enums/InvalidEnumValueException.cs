using System.Runtime.Serialization;

namespace Sagara.Core.Enums;

/// <summary>
/// Thrown when a provided value does not match one of the enum's defined values.
/// </summary>
/// <remarks>
/// See: https://stackoverflow.com/a/100369
/// </remarks>
[Serializable]
public sealed class InvalidEnumValueException<TEnum> : Exception
    where TEnum : struct, Enum
{
    /// <summary>
    /// The invalid enum value.
    /// </summary>
    public TEnum InvalidValue { get; private set; }

    /// <summary>
    /// .ctor that accepts the invalid enum value.
    /// </summary>
    /// <param name="invalidValue">The invalid enum value that is decorated with <see cref="InvalidEnumValueAttribute"/>.</param>
    public InvalidEnumValueException(TEnum invalidValue)
        : this(invalidValue, message: $"Invalid enum value '{invalidValue}' for enum type '{EnumTraits<TEnum>.EnumType.FullName}'.", innerException: null)
    {
    }

    /// <summary>
    /// .ctor that accepts the invalid enum value and an exception message.
    /// </summary>
    /// <param name="invalidValue">The invalid enum value that is decorated with <see cref="InvalidEnumValueAttribute"/>.</param>
    /// <param name="message">The message to add to the exception.</param>
    public InvalidEnumValueException(TEnum invalidValue, string? message)
        : this(invalidValue, message, innerException: null)
    {
    }

    /// <summary>
    /// .ctor that accepts the invalid enum value, an exception message, and an inner exception.
    /// </summary>
    /// <param name="invalidValue">The invalid enum value that is decorated with <see cref="InvalidEnumValueAttribute"/>.</param>
    /// <param name="message">The message to add to the exception.</param>
    /// <param name="innerException">The inner exception to pass to the exception.</param>
    public InvalidEnumValueException(TEnum invalidValue, string? message, Exception? innerException)
        : base(message, innerException)
    {
        InvalidValue = invalidValue;
    }

    private InvalidEnumValueException(SerializationInfo info, StreamingContext context)
        : base(info, context)
    {
        InvalidValue = (TEnum)info.GetValue(nameof(InvalidValue), typeof(TEnum))!;
    }

    /// <summary>
    /// Serializes the invalid enum value, then calls the base class method.
    /// </summary>
    /// <param name="info"><inheritdoc/></param>
    /// <param name="context"><inheritdoc/></param>
    public override void GetObjectData(SerializationInfo info, StreamingContext context)
    {
        ArgumentNullException.ThrowIfNull(info);

        info.AddValue(nameof(InvalidValue), InvalidValue);

        base.GetObjectData(info, context);
    }


    /// <summary>
    /// Throws an InvalidEnumValueException if value is marked as [InvalidEnumValue].
    /// </summary>
    public static void ThrowIfInvalid(TEnum value)
    {
        if (!EnumTraits<TEnum>.IsValid(value))
        {
            throw new InvalidEnumValueException<TEnum>(value);
        }
    }
}
