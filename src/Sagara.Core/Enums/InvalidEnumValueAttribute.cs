namespace Sagara.Core.Enums;

/// <summary>
/// Denotes a sentinel value that we use to detect, e.g., uninitialized members (&quot;Unknown&quot;). Enum
/// values decorated with this attribute will not be considered valid by the EnumTraits helper class.
/// </summary>
[AttributeUsage(AttributeTargets.Field, AllowMultiple = false, Inherited = false)]
public sealed class InvalidEnumValueAttribute : Attribute
{
}
