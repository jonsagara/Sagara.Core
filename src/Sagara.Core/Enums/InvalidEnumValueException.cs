using System.Runtime.Serialization;

namespace Sagara.Core.Enums;

/// <summary>
/// Thrown when an enum value is one of the enum's invalid values. Invalid values are indicated 
/// explicitly by the presence of an <see cref="InvalidEnumValueAttribute"/> on the enum value.
/// </summary>
/// <remarks>
/// See: https://stackoverflow.com/a/100369
/// </remarks>
[Serializable]
public sealed class InvalidEnumValueException : Exception
{
    public InvalidEnumValueException()
    {
    }

    public InvalidEnumValueException(string message)
        : base(message)
    {
    }

    public InvalidEnumValueException(string message, Exception innerException)
        : base(message, innerException)
    {
    }

    private InvalidEnumValueException(SerializationInfo serializationInfo, StreamingContext streamingContext)
    {
        throw new NotImplementedException();
    }


    /// <summary>
    /// Throws an InvalidEnumValueException if the enum value is decorated with <see cref="InvalidEnumValueAttribute"/>.
    /// </summary>
    public static void ThrowIfInvalid<TEnum>(TEnum value)
        where TEnum : struct, Enum
    {
        if (!EnumTraits<TEnum>.IsValid(value))
        {
            throw new InvalidEnumValueException($"Invalid enum value '{value}' for enum type {typeof(TEnum).FullName}.");
        }
    }
}
