#### [Sagara.Core](index.md 'index')
### [Sagara.Core.Enums](index.md#Sagara.Core.Enums 'Sagara.Core.Enums')

## InvalidEnumValueException Class

Thrown when an enum value is one of the enum's invalid values. Invalid values are indicated   
explicitly by the presence of an [InvalidEnumValueAttribute](Sagara.Core.Enums.InvalidEnumValueAttribute.md 'Sagara.Core.Enums.InvalidEnumValueAttribute') on the enum value.

```csharp
public sealed class InvalidEnumValueException : System.Exception
```

Inheritance [System.Object](https://docs.microsoft.com/en-us/dotnet/api/System.Object 'System.Object') &#129106; [System.Exception](https://docs.microsoft.com/en-us/dotnet/api/System.Exception 'System.Exception') &#129106; InvalidEnumValueException

### Remarks
See: https://stackoverflow.com/a/100369
### Methods

<a name='Sagara.Core.Enums.InvalidEnumValueException.ThrowIfInvalid_TEnum_(TEnum)'></a>

## InvalidEnumValueException.ThrowIfInvalid<TEnum>(TEnum) Method

Throws an InvalidEnumValueException if the enum value is decorated with [InvalidEnumValueAttribute](Sagara.Core.Enums.InvalidEnumValueAttribute.md 'Sagara.Core.Enums.InvalidEnumValueAttribute').

```csharp
public static void ThrowIfInvalid<TEnum>(TEnum value)
    where TEnum : struct, System.Enum, System.ValueType, System.ValueType;
```
#### Type parameters

<a name='Sagara.Core.Enums.InvalidEnumValueException.ThrowIfInvalid_TEnum_(TEnum).TEnum'></a>

`TEnum`
#### Parameters

<a name='Sagara.Core.Enums.InvalidEnumValueException.ThrowIfInvalid_TEnum_(TEnum).value'></a>

`value` [TEnum](Sagara.Core.Enums.InvalidEnumValueException.md#Sagara.Core.Enums.InvalidEnumValueException.ThrowIfInvalid_TEnum_(TEnum).TEnum 'Sagara.Core.Enums.InvalidEnumValueException.ThrowIfInvalid<TEnum>(TEnum).TEnum')