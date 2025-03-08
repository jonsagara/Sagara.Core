#### [Sagara.Core](index.md 'index')
### [Sagara.Core.Extensions](index.md#Sagara.Core.Extensions 'Sagara.Core.Extensions')

## NumberExtensions Class

```csharp
public static class NumberExtensions
```

Inheritance [System.Object](https://docs.microsoft.com/en-us/dotnet/api/System.Object 'System.Object') &#129106; NumberExtensions
### Methods

<a name='Sagara.Core.Extensions.NumberExtensions.ToNullIfZero_TNumber_(thisTNumber)'></a>

## NumberExtensions.ToNullIfZero<TNumber>(this TNumber) Method

If [value](Sagara.Core.Extensions.NumberExtensions.md#Sagara.Core.Extensions.NumberExtensions.ToNullIfZero_TNumber_(thisTNumber).value 'Sagara.Core.Extensions.NumberExtensions.ToNullIfZero<TNumber>(this TNumber).value') is `TNumber.Zero`, return `null`. Otherwise, return  
[value](Sagara.Core.Extensions.NumberExtensions.md#Sagara.Core.Extensions.NumberExtensions.ToNullIfZero_TNumber_(thisTNumber).value 'Sagara.Core.Extensions.NumberExtensions.ToNullIfZero<TNumber>(this TNumber).value') unchanged.

```csharp
public static System.Nullable<TNumber> ToNullIfZero<TNumber>(this TNumber value)
    where TNumber : struct, System.Numerics.INumber<TNumber>, System.ValueType, System.ValueType;
```
#### Type parameters

<a name='Sagara.Core.Extensions.NumberExtensions.ToNullIfZero_TNumber_(thisTNumber).TNumber'></a>

`TNumber`
#### Parameters

<a name='Sagara.Core.Extensions.NumberExtensions.ToNullIfZero_TNumber_(thisTNumber).value'></a>

`value` [TNumber](Sagara.Core.Extensions.NumberExtensions.md#Sagara.Core.Extensions.NumberExtensions.ToNullIfZero_TNumber_(thisTNumber).TNumber 'Sagara.Core.Extensions.NumberExtensions.ToNullIfZero<TNumber>(this TNumber).TNumber')

#### Returns
[System.Nullable&lt;](https://docs.microsoft.com/en-us/dotnet/api/System.Nullable-1 'System.Nullable`1')[TNumber](Sagara.Core.Extensions.NumberExtensions.md#Sagara.Core.Extensions.NumberExtensions.ToNullIfZero_TNumber_(thisTNumber).TNumber 'Sagara.Core.Extensions.NumberExtensions.ToNullIfZero<TNumber>(this TNumber).TNumber')[&gt;](https://docs.microsoft.com/en-us/dotnet/api/System.Nullable-1 'System.Nullable`1')