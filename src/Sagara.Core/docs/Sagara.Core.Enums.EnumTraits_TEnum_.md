#### [Sagara.Core](index.md 'index')
### [Sagara.Core.Enums](index.md#Sagara.Core.Enums 'Sagara.Core.Enums')

## EnumTraits<TEnum> Class

  
Consolidated wrapper for enum values, valid values, and display names.  
  
Inspired by: https://devblogs.microsoft.com/premier-developer/dissecting-new-generics-constraints-in-c-7-3/

```csharp
public static class EnumTraits<TEnum>
    where TEnum : struct, System.Enum, System.ValueType, System.ValueType
```
#### Type parameters

<a name='Sagara.Core.Enums.EnumTraits_TEnum_.TEnum'></a>

`TEnum`

The type of enum.

Inheritance [System.Object](https://docs.microsoft.com/en-us/dotnet/api/System.Object 'System.Object') &#129106; EnumTraits<TEnum>
### Properties

<a name='Sagara.Core.Enums.EnumTraits_TEnum_.AllValues'></a>

## EnumTraits<TEnum>.AllValues Property

Returns all defined enum values.

```csharp
public static System.Collections.Generic.IReadOnlyCollection<TEnum> AllValues { get; }
```

#### Property Value
[System.Collections.Generic.IReadOnlyCollection&lt;](https://docs.microsoft.com/en-us/dotnet/api/System.Collections.Generic.IReadOnlyCollection-1 'System.Collections.Generic.IReadOnlyCollection`1')[TEnum](Sagara.Core.Enums.EnumTraits_TEnum_.md#Sagara.Core.Enums.EnumTraits_TEnum_.TEnum 'Sagara.Core.Enums.EnumTraits<TEnum>.TEnum')[&gt;](https://docs.microsoft.com/en-us/dotnet/api/System.Collections.Generic.IReadOnlyCollection-1 'System.Collections.Generic.IReadOnlyCollection`1')

<a name='Sagara.Core.Enums.EnumTraits_TEnum_.EnumType'></a>

## EnumTraits<TEnum>.EnumType Property

Returns the enum's Type information.

```csharp
public static System.Type EnumType { get; }
```

#### Property Value
[System.Type](https://docs.microsoft.com/en-us/dotnet/api/System.Type 'System.Type')

<a name='Sagara.Core.Enums.EnumTraits_TEnum_.HasFlagsAttribute'></a>

## EnumTraits<TEnum>.HasFlagsAttribute Property

Returns true if the enum is decorated with a [System.FlagsAttribute](https://docs.microsoft.com/en-us/dotnet/api/System.FlagsAttribute 'System.FlagsAttribute'); false otherwise.

```csharp
public static bool HasFlagsAttribute { get; }
```

#### Property Value
[System.Boolean](https://docs.microsoft.com/en-us/dotnet/api/System.Boolean 'System.Boolean')

<a name='Sagara.Core.Enums.EnumTraits_TEnum_.IsEmpty'></a>

## EnumTraits<TEnum>.IsEmpty Property

Returns true if the enum declaration is empty; false otherwise.

```csharp
public static bool IsEmpty { get; }
```

#### Property Value
[System.Boolean](https://docs.microsoft.com/en-us/dotnet/api/System.Boolean 'System.Boolean')

<a name='Sagara.Core.Enums.EnumTraits_TEnum_.ValidValues'></a>

## EnumTraits<TEnum>.ValidValues Property

Returns all enum values that are not marked with [InvalidEnumValueAttribute](Sagara.Core.Enums.InvalidEnumValueAttribute.md 'Sagara.Core.Enums.InvalidEnumValueAttribute').

```csharp
public static System.Collections.Generic.IReadOnlyCollection<TEnum> ValidValues { get; }
```

#### Property Value
[System.Collections.Generic.IReadOnlyCollection&lt;](https://docs.microsoft.com/en-us/dotnet/api/System.Collections.Generic.IReadOnlyCollection-1 'System.Collections.Generic.IReadOnlyCollection`1')[TEnum](Sagara.Core.Enums.EnumTraits_TEnum_.md#Sagara.Core.Enums.EnumTraits_TEnum_.TEnum 'Sagara.Core.Enums.EnumTraits<TEnum>.TEnum')[&gt;](https://docs.microsoft.com/en-us/dotnet/api/System.Collections.Generic.IReadOnlyCollection-1 'System.Collections.Generic.IReadOnlyCollection`1')
### Methods

<a name='Sagara.Core.Enums.EnumTraits_TEnum_.EnsureNoDuplicateValues()'></a>

## EnumTraits<TEnum>.EnsureNoDuplicateValues() Method

Throws an [System.InvalidOperationException](https://docs.microsoft.com/en-us/dotnet/api/System.InvalidOperationException 'System.InvalidOperationException') if the enum has one or more duplicate underlying values defined.

```csharp
public static void EnsureNoDuplicateValues();
```

#### Exceptions

[System.InvalidOperationException](https://docs.microsoft.com/en-us/dotnet/api/System.InvalidOperationException 'System.InvalidOperationException')  
Throws if the enum has one or more duplicate underlying values defined.

<a name='Sagara.Core.Enums.EnumTraits_TEnum_.GetDisplayName(TEnum)'></a>

## EnumTraits<TEnum>.GetDisplayName(TEnum) Method

Return the display name for the enum value, which is either from the [Display] attribute   
or the property name.

```csharp
public static string GetDisplayName(TEnum value);
```
#### Parameters

<a name='Sagara.Core.Enums.EnumTraits_TEnum_.GetDisplayName(TEnum).value'></a>

`value` [TEnum](Sagara.Core.Enums.EnumTraits_TEnum_.md#Sagara.Core.Enums.EnumTraits_TEnum_.TEnum 'Sagara.Core.Enums.EnumTraits<TEnum>.TEnum')

#### Returns
[System.String](https://docs.microsoft.com/en-us/dotnet/api/System.String 'System.String')

<a name='Sagara.Core.Enums.EnumTraits_TEnum_.GetDisplayNameOrDefault(TEnum)'></a>

## EnumTraits<TEnum>.GetDisplayNameOrDefault(TEnum) Method

Return the display name for the enum value, which is either from the [Display] attribute   
or the property name. If not found, return null.

```csharp
public static string? GetDisplayNameOrDefault(TEnum value);
```
#### Parameters

<a name='Sagara.Core.Enums.EnumTraits_TEnum_.GetDisplayNameOrDefault(TEnum).value'></a>

`value` [TEnum](Sagara.Core.Enums.EnumTraits_TEnum_.md#Sagara.Core.Enums.EnumTraits_TEnum_.TEnum 'Sagara.Core.Enums.EnumTraits<TEnum>.TEnum')

#### Returns
[System.String](https://docs.microsoft.com/en-us/dotnet/api/System.String 'System.String')

<a name='Sagara.Core.Enums.EnumTraits_TEnum_.GetDisplayNameOrEnumValueName(TEnum)'></a>

## EnumTraits<TEnum>.GetDisplayNameOrEnumValueName(TEnum) Method

If an enum value is decorated with a Display attribute, get its Name. Otherwise, return  
the enum value name as a string.

```csharp
private static string GetDisplayNameOrEnumValueName(TEnum value);
```
#### Parameters

<a name='Sagara.Core.Enums.EnumTraits_TEnum_.GetDisplayNameOrEnumValueName(TEnum).value'></a>

`value` [TEnum](Sagara.Core.Enums.EnumTraits_TEnum_.md#Sagara.Core.Enums.EnumTraits_TEnum_.TEnum 'Sagara.Core.Enums.EnumTraits<TEnum>.TEnum')

#### Returns
[System.String](https://docs.microsoft.com/en-us/dotnet/api/System.String 'System.String')

<a name='Sagara.Core.Enums.EnumTraits_TEnum_.IsValid(TEnum)'></a>

## EnumTraits<TEnum>.IsValid(TEnum) Method

Returns true if the enum value is in the list of valid values; false otherwise.

```csharp
public static bool IsValid(TEnum value);
```
#### Parameters

<a name='Sagara.Core.Enums.EnumTraits_TEnum_.IsValid(TEnum).value'></a>

`value` [TEnum](Sagara.Core.Enums.EnumTraits_TEnum_.md#Sagara.Core.Enums.EnumTraits_TEnum_.TEnum 'Sagara.Core.Enums.EnumTraits<TEnum>.TEnum')

#### Returns
[System.Boolean](https://docs.microsoft.com/en-us/dotnet/api/System.Boolean 'System.Boolean')