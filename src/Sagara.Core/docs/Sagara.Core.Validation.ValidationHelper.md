#### [Sagara.Core](index.md 'index')
### [Sagara.Core.Validation](index.md#Sagara.Core.Validation 'Sagara.Core.Validation')

## ValidationHelper Class

Contains common validation methods that work with [ValidatableProperty&lt;TValue&gt;](Sagara.Core.Validation.ValidatableProperty_TValue_.md 'Sagara.Core.Validation.ValidatableProperty<TValue>') and   
[RequestError](Sagara.Core.Validation.RequestError.md 'Sagara.Core.Validation.RequestError').

```csharp
public static class ValidationHelper
```

Inheritance [System.Object](https://docs.microsoft.com/en-us/dotnet/api/System.Object 'System.Object') &#129106; ValidationHelper
### Methods

<a name='Sagara.Core.Validation.ValidationHelper.CheckGreaterThan_TNumber_(Sagara.Core.Validation.ValidatableProperty_System.Nullable_TNumber__,TNumber,System.Collections.Generic.ICollection_Sagara.Core.Validation.RequestError_)'></a>

## ValidationHelper.CheckGreaterThan<TNumber>(ValidatableProperty<Nullable<TNumber>>, TNumber, ICollection<RequestError>) Method

Add an error message if property's value is less than or equal to [threshold](Sagara.Core.Validation.ValidationHelper.md#Sagara.Core.Validation.ValidationHelper.CheckGreaterThan_TNumber_(Sagara.Core.Validation.ValidatableProperty_System.Nullable_TNumber__,TNumber,System.Collections.Generic.ICollection_Sagara.Core.Validation.RequestError_).threshold 'Sagara.Core.Validation.ValidationHelper.CheckGreaterThan<TNumber>(Sagara.Core.Validation.ValidatableProperty<System.Nullable<TNumber>>, TNumber, System.Collections.Generic.ICollection<Sagara.Core.Validation.RequestError>).threshold').

```csharp
public static void CheckGreaterThan<TNumber>(Sagara.Core.Validation.ValidatableProperty<System.Nullable<TNumber>> property, TNumber threshold, System.Collections.Generic.ICollection<Sagara.Core.Validation.RequestError> errors)
    where TNumber : struct, System.Numerics.INumber<TNumber>, System.ValueType, System.ValueType;
```
#### Type parameters

<a name='Sagara.Core.Validation.ValidationHelper.CheckGreaterThan_TNumber_(Sagara.Core.Validation.ValidatableProperty_System.Nullable_TNumber__,TNumber,System.Collections.Generic.ICollection_Sagara.Core.Validation.RequestError_).TNumber'></a>

`TNumber`
#### Parameters

<a name='Sagara.Core.Validation.ValidationHelper.CheckGreaterThan_TNumber_(Sagara.Core.Validation.ValidatableProperty_System.Nullable_TNumber__,TNumber,System.Collections.Generic.ICollection_Sagara.Core.Validation.RequestError_).property'></a>

`property` [Sagara.Core.Validation.ValidatableProperty&lt;](Sagara.Core.Validation.ValidatableProperty_TValue_.md 'Sagara.Core.Validation.ValidatableProperty<TValue>')[System.Nullable&lt;](https://docs.microsoft.com/en-us/dotnet/api/System.Nullable-1 'System.Nullable`1')[TNumber](Sagara.Core.Validation.ValidationHelper.md#Sagara.Core.Validation.ValidationHelper.CheckGreaterThan_TNumber_(Sagara.Core.Validation.ValidatableProperty_System.Nullable_TNumber__,TNumber,System.Collections.Generic.ICollection_Sagara.Core.Validation.RequestError_).TNumber 'Sagara.Core.Validation.ValidationHelper.CheckGreaterThan<TNumber>(Sagara.Core.Validation.ValidatableProperty<System.Nullable<TNumber>>, TNumber, System.Collections.Generic.ICollection<Sagara.Core.Validation.RequestError>).TNumber')[&gt;](https://docs.microsoft.com/en-us/dotnet/api/System.Nullable-1 'System.Nullable`1')[&gt;](Sagara.Core.Validation.ValidatableProperty_TValue_.md 'Sagara.Core.Validation.ValidatableProperty<TValue>')

<a name='Sagara.Core.Validation.ValidationHelper.CheckGreaterThan_TNumber_(Sagara.Core.Validation.ValidatableProperty_System.Nullable_TNumber__,TNumber,System.Collections.Generic.ICollection_Sagara.Core.Validation.RequestError_).threshold'></a>

`threshold` [TNumber](Sagara.Core.Validation.ValidationHelper.md#Sagara.Core.Validation.ValidationHelper.CheckGreaterThan_TNumber_(Sagara.Core.Validation.ValidatableProperty_System.Nullable_TNumber__,TNumber,System.Collections.Generic.ICollection_Sagara.Core.Validation.RequestError_).TNumber 'Sagara.Core.Validation.ValidationHelper.CheckGreaterThan<TNumber>(Sagara.Core.Validation.ValidatableProperty<System.Nullable<TNumber>>, TNumber, System.Collections.Generic.ICollection<Sagara.Core.Validation.RequestError>).TNumber')

<a name='Sagara.Core.Validation.ValidationHelper.CheckGreaterThan_TNumber_(Sagara.Core.Validation.ValidatableProperty_System.Nullable_TNumber__,TNumber,System.Collections.Generic.ICollection_Sagara.Core.Validation.RequestError_).errors'></a>

`errors` [System.Collections.Generic.ICollection&lt;](https://docs.microsoft.com/en-us/dotnet/api/System.Collections.Generic.ICollection-1 'System.Collections.Generic.ICollection`1')[RequestError](Sagara.Core.Validation.RequestError.md 'Sagara.Core.Validation.RequestError')[&gt;](https://docs.microsoft.com/en-us/dotnet/api/System.Collections.Generic.ICollection-1 'System.Collections.Generic.ICollection`1')

<a name='Sagara.Core.Validation.ValidationHelper.CheckGreaterThanOrEqual_TNumber_(Sagara.Core.Validation.ValidatableProperty_System.Nullable_TNumber__,TNumber,System.Collections.Generic.ICollection_Sagara.Core.Validation.RequestError_)'></a>

## ValidationHelper.CheckGreaterThanOrEqual<TNumber>(ValidatableProperty<Nullable<TNumber>>, TNumber, ICollection<RequestError>) Method

Add an error message if property's value is less than [threshold](Sagara.Core.Validation.ValidationHelper.md#Sagara.Core.Validation.ValidationHelper.CheckGreaterThanOrEqual_TNumber_(Sagara.Core.Validation.ValidatableProperty_System.Nullable_TNumber__,TNumber,System.Collections.Generic.ICollection_Sagara.Core.Validation.RequestError_).threshold 'Sagara.Core.Validation.ValidationHelper.CheckGreaterThanOrEqual<TNumber>(Sagara.Core.Validation.ValidatableProperty<System.Nullable<TNumber>>, TNumber, System.Collections.Generic.ICollection<Sagara.Core.Validation.RequestError>).threshold').

```csharp
public static void CheckGreaterThanOrEqual<TNumber>(Sagara.Core.Validation.ValidatableProperty<System.Nullable<TNumber>> property, TNumber threshold, System.Collections.Generic.ICollection<Sagara.Core.Validation.RequestError> errors)
    where TNumber : struct, System.Numerics.INumber<TNumber>, System.ValueType, System.ValueType;
```
#### Type parameters

<a name='Sagara.Core.Validation.ValidationHelper.CheckGreaterThanOrEqual_TNumber_(Sagara.Core.Validation.ValidatableProperty_System.Nullable_TNumber__,TNumber,System.Collections.Generic.ICollection_Sagara.Core.Validation.RequestError_).TNumber'></a>

`TNumber`
#### Parameters

<a name='Sagara.Core.Validation.ValidationHelper.CheckGreaterThanOrEqual_TNumber_(Sagara.Core.Validation.ValidatableProperty_System.Nullable_TNumber__,TNumber,System.Collections.Generic.ICollection_Sagara.Core.Validation.RequestError_).property'></a>

`property` [Sagara.Core.Validation.ValidatableProperty&lt;](Sagara.Core.Validation.ValidatableProperty_TValue_.md 'Sagara.Core.Validation.ValidatableProperty<TValue>')[System.Nullable&lt;](https://docs.microsoft.com/en-us/dotnet/api/System.Nullable-1 'System.Nullable`1')[TNumber](Sagara.Core.Validation.ValidationHelper.md#Sagara.Core.Validation.ValidationHelper.CheckGreaterThanOrEqual_TNumber_(Sagara.Core.Validation.ValidatableProperty_System.Nullable_TNumber__,TNumber,System.Collections.Generic.ICollection_Sagara.Core.Validation.RequestError_).TNumber 'Sagara.Core.Validation.ValidationHelper.CheckGreaterThanOrEqual<TNumber>(Sagara.Core.Validation.ValidatableProperty<System.Nullable<TNumber>>, TNumber, System.Collections.Generic.ICollection<Sagara.Core.Validation.RequestError>).TNumber')[&gt;](https://docs.microsoft.com/en-us/dotnet/api/System.Nullable-1 'System.Nullable`1')[&gt;](Sagara.Core.Validation.ValidatableProperty_TValue_.md 'Sagara.Core.Validation.ValidatableProperty<TValue>')

<a name='Sagara.Core.Validation.ValidationHelper.CheckGreaterThanOrEqual_TNumber_(Sagara.Core.Validation.ValidatableProperty_System.Nullable_TNumber__,TNumber,System.Collections.Generic.ICollection_Sagara.Core.Validation.RequestError_).threshold'></a>

`threshold` [TNumber](Sagara.Core.Validation.ValidationHelper.md#Sagara.Core.Validation.ValidationHelper.CheckGreaterThanOrEqual_TNumber_(Sagara.Core.Validation.ValidatableProperty_System.Nullable_TNumber__,TNumber,System.Collections.Generic.ICollection_Sagara.Core.Validation.RequestError_).TNumber 'Sagara.Core.Validation.ValidationHelper.CheckGreaterThanOrEqual<TNumber>(Sagara.Core.Validation.ValidatableProperty<System.Nullable<TNumber>>, TNumber, System.Collections.Generic.ICollection<Sagara.Core.Validation.RequestError>).TNumber')

<a name='Sagara.Core.Validation.ValidationHelper.CheckGreaterThanOrEqual_TNumber_(Sagara.Core.Validation.ValidatableProperty_System.Nullable_TNumber__,TNumber,System.Collections.Generic.ICollection_Sagara.Core.Validation.RequestError_).errors'></a>

`errors` [System.Collections.Generic.ICollection&lt;](https://docs.microsoft.com/en-us/dotnet/api/System.Collections.Generic.ICollection-1 'System.Collections.Generic.ICollection`1')[RequestError](Sagara.Core.Validation.RequestError.md 'Sagara.Core.Validation.RequestError')[&gt;](https://docs.microsoft.com/en-us/dotnet/api/System.Collections.Generic.ICollection-1 'System.Collections.Generic.ICollection`1')

<a name='Sagara.Core.Validation.ValidationHelper.CheckLessThan_TNumber_(Sagara.Core.Validation.ValidatableProperty_System.Nullable_TNumber__,TNumber,System.Collections.Generic.ICollection_Sagara.Core.Validation.RequestError_)'></a>

## ValidationHelper.CheckLessThan<TNumber>(ValidatableProperty<Nullable<TNumber>>, TNumber, ICollection<RequestError>) Method

Add an error message if property's value is greater than or equal to [threshold](Sagara.Core.Validation.ValidationHelper.md#Sagara.Core.Validation.ValidationHelper.CheckLessThan_TNumber_(Sagara.Core.Validation.ValidatableProperty_System.Nullable_TNumber__,TNumber,System.Collections.Generic.ICollection_Sagara.Core.Validation.RequestError_).threshold 'Sagara.Core.Validation.ValidationHelper.CheckLessThan<TNumber>(Sagara.Core.Validation.ValidatableProperty<System.Nullable<TNumber>>, TNumber, System.Collections.Generic.ICollection<Sagara.Core.Validation.RequestError>).threshold').

```csharp
public static void CheckLessThan<TNumber>(Sagara.Core.Validation.ValidatableProperty<System.Nullable<TNumber>> property, TNumber threshold, System.Collections.Generic.ICollection<Sagara.Core.Validation.RequestError> errors)
    where TNumber : struct, System.Numerics.INumber<TNumber>, System.ValueType, System.ValueType;
```
#### Type parameters

<a name='Sagara.Core.Validation.ValidationHelper.CheckLessThan_TNumber_(Sagara.Core.Validation.ValidatableProperty_System.Nullable_TNumber__,TNumber,System.Collections.Generic.ICollection_Sagara.Core.Validation.RequestError_).TNumber'></a>

`TNumber`
#### Parameters

<a name='Sagara.Core.Validation.ValidationHelper.CheckLessThan_TNumber_(Sagara.Core.Validation.ValidatableProperty_System.Nullable_TNumber__,TNumber,System.Collections.Generic.ICollection_Sagara.Core.Validation.RequestError_).property'></a>

`property` [Sagara.Core.Validation.ValidatableProperty&lt;](Sagara.Core.Validation.ValidatableProperty_TValue_.md 'Sagara.Core.Validation.ValidatableProperty<TValue>')[System.Nullable&lt;](https://docs.microsoft.com/en-us/dotnet/api/System.Nullable-1 'System.Nullable`1')[TNumber](Sagara.Core.Validation.ValidationHelper.md#Sagara.Core.Validation.ValidationHelper.CheckLessThan_TNumber_(Sagara.Core.Validation.ValidatableProperty_System.Nullable_TNumber__,TNumber,System.Collections.Generic.ICollection_Sagara.Core.Validation.RequestError_).TNumber 'Sagara.Core.Validation.ValidationHelper.CheckLessThan<TNumber>(Sagara.Core.Validation.ValidatableProperty<System.Nullable<TNumber>>, TNumber, System.Collections.Generic.ICollection<Sagara.Core.Validation.RequestError>).TNumber')[&gt;](https://docs.microsoft.com/en-us/dotnet/api/System.Nullable-1 'System.Nullable`1')[&gt;](Sagara.Core.Validation.ValidatableProperty_TValue_.md 'Sagara.Core.Validation.ValidatableProperty<TValue>')

<a name='Sagara.Core.Validation.ValidationHelper.CheckLessThan_TNumber_(Sagara.Core.Validation.ValidatableProperty_System.Nullable_TNumber__,TNumber,System.Collections.Generic.ICollection_Sagara.Core.Validation.RequestError_).threshold'></a>

`threshold` [TNumber](Sagara.Core.Validation.ValidationHelper.md#Sagara.Core.Validation.ValidationHelper.CheckLessThan_TNumber_(Sagara.Core.Validation.ValidatableProperty_System.Nullable_TNumber__,TNumber,System.Collections.Generic.ICollection_Sagara.Core.Validation.RequestError_).TNumber 'Sagara.Core.Validation.ValidationHelper.CheckLessThan<TNumber>(Sagara.Core.Validation.ValidatableProperty<System.Nullable<TNumber>>, TNumber, System.Collections.Generic.ICollection<Sagara.Core.Validation.RequestError>).TNumber')

<a name='Sagara.Core.Validation.ValidationHelper.CheckLessThan_TNumber_(Sagara.Core.Validation.ValidatableProperty_System.Nullable_TNumber__,TNumber,System.Collections.Generic.ICollection_Sagara.Core.Validation.RequestError_).errors'></a>

`errors` [System.Collections.Generic.ICollection&lt;](https://docs.microsoft.com/en-us/dotnet/api/System.Collections.Generic.ICollection-1 'System.Collections.Generic.ICollection`1')[RequestError](Sagara.Core.Validation.RequestError.md 'Sagara.Core.Validation.RequestError')[&gt;](https://docs.microsoft.com/en-us/dotnet/api/System.Collections.Generic.ICollection-1 'System.Collections.Generic.ICollection`1')

<a name='Sagara.Core.Validation.ValidationHelper.CheckLessThanOrEqual_TNumber_(Sagara.Core.Validation.ValidatableProperty_System.Nullable_TNumber__,TNumber,System.Collections.Generic.ICollection_Sagara.Core.Validation.RequestError_)'></a>

## ValidationHelper.CheckLessThanOrEqual<TNumber>(ValidatableProperty<Nullable<TNumber>>, TNumber, ICollection<RequestError>) Method

Add an error message if property's value is greater than [threshold](Sagara.Core.Validation.ValidationHelper.md#Sagara.Core.Validation.ValidationHelper.CheckLessThanOrEqual_TNumber_(Sagara.Core.Validation.ValidatableProperty_System.Nullable_TNumber__,TNumber,System.Collections.Generic.ICollection_Sagara.Core.Validation.RequestError_).threshold 'Sagara.Core.Validation.ValidationHelper.CheckLessThanOrEqual<TNumber>(Sagara.Core.Validation.ValidatableProperty<System.Nullable<TNumber>>, TNumber, System.Collections.Generic.ICollection<Sagara.Core.Validation.RequestError>).threshold').

```csharp
public static void CheckLessThanOrEqual<TNumber>(Sagara.Core.Validation.ValidatableProperty<System.Nullable<TNumber>> property, TNumber threshold, System.Collections.Generic.ICollection<Sagara.Core.Validation.RequestError> errors)
    where TNumber : struct, System.Numerics.INumber<TNumber>, System.ValueType, System.ValueType;
```
#### Type parameters

<a name='Sagara.Core.Validation.ValidationHelper.CheckLessThanOrEqual_TNumber_(Sagara.Core.Validation.ValidatableProperty_System.Nullable_TNumber__,TNumber,System.Collections.Generic.ICollection_Sagara.Core.Validation.RequestError_).TNumber'></a>

`TNumber`
#### Parameters

<a name='Sagara.Core.Validation.ValidationHelper.CheckLessThanOrEqual_TNumber_(Sagara.Core.Validation.ValidatableProperty_System.Nullable_TNumber__,TNumber,System.Collections.Generic.ICollection_Sagara.Core.Validation.RequestError_).property'></a>

`property` [Sagara.Core.Validation.ValidatableProperty&lt;](Sagara.Core.Validation.ValidatableProperty_TValue_.md 'Sagara.Core.Validation.ValidatableProperty<TValue>')[System.Nullable&lt;](https://docs.microsoft.com/en-us/dotnet/api/System.Nullable-1 'System.Nullable`1')[TNumber](Sagara.Core.Validation.ValidationHelper.md#Sagara.Core.Validation.ValidationHelper.CheckLessThanOrEqual_TNumber_(Sagara.Core.Validation.ValidatableProperty_System.Nullable_TNumber__,TNumber,System.Collections.Generic.ICollection_Sagara.Core.Validation.RequestError_).TNumber 'Sagara.Core.Validation.ValidationHelper.CheckLessThanOrEqual<TNumber>(Sagara.Core.Validation.ValidatableProperty<System.Nullable<TNumber>>, TNumber, System.Collections.Generic.ICollection<Sagara.Core.Validation.RequestError>).TNumber')[&gt;](https://docs.microsoft.com/en-us/dotnet/api/System.Nullable-1 'System.Nullable`1')[&gt;](Sagara.Core.Validation.ValidatableProperty_TValue_.md 'Sagara.Core.Validation.ValidatableProperty<TValue>')

<a name='Sagara.Core.Validation.ValidationHelper.CheckLessThanOrEqual_TNumber_(Sagara.Core.Validation.ValidatableProperty_System.Nullable_TNumber__,TNumber,System.Collections.Generic.ICollection_Sagara.Core.Validation.RequestError_).threshold'></a>

`threshold` [TNumber](Sagara.Core.Validation.ValidationHelper.md#Sagara.Core.Validation.ValidationHelper.CheckLessThanOrEqual_TNumber_(Sagara.Core.Validation.ValidatableProperty_System.Nullable_TNumber__,TNumber,System.Collections.Generic.ICollection_Sagara.Core.Validation.RequestError_).TNumber 'Sagara.Core.Validation.ValidationHelper.CheckLessThanOrEqual<TNumber>(Sagara.Core.Validation.ValidatableProperty<System.Nullable<TNumber>>, TNumber, System.Collections.Generic.ICollection<Sagara.Core.Validation.RequestError>).TNumber')

<a name='Sagara.Core.Validation.ValidationHelper.CheckLessThanOrEqual_TNumber_(Sagara.Core.Validation.ValidatableProperty_System.Nullable_TNumber__,TNumber,System.Collections.Generic.ICollection_Sagara.Core.Validation.RequestError_).errors'></a>

`errors` [System.Collections.Generic.ICollection&lt;](https://docs.microsoft.com/en-us/dotnet/api/System.Collections.Generic.ICollection-1 'System.Collections.Generic.ICollection`1')[RequestError](Sagara.Core.Validation.RequestError.md 'Sagara.Core.Validation.RequestError')[&gt;](https://docs.microsoft.com/en-us/dotnet/api/System.Collections.Generic.ICollection-1 'System.Collections.Generic.ICollection`1')

<a name='Sagara.Core.Validation.ValidationHelper.CheckRequiredField(Sagara.Core.Validation.ValidatableProperty_string_,System.Collections.Generic.ICollection_Sagara.Core.Validation.RequestError_)'></a>

## ValidationHelper.CheckRequiredField(ValidatableProperty<string>, ICollection<RequestError>) Method

Add an error message if property's value is null or white space.

```csharp
public static void CheckRequiredField(Sagara.Core.Validation.ValidatableProperty<string?> property, System.Collections.Generic.ICollection<Sagara.Core.Validation.RequestError> errors);
```
#### Parameters

<a name='Sagara.Core.Validation.ValidationHelper.CheckRequiredField(Sagara.Core.Validation.ValidatableProperty_string_,System.Collections.Generic.ICollection_Sagara.Core.Validation.RequestError_).property'></a>

`property` [Sagara.Core.Validation.ValidatableProperty&lt;](Sagara.Core.Validation.ValidatableProperty_TValue_.md 'Sagara.Core.Validation.ValidatableProperty<TValue>')[System.String](https://docs.microsoft.com/en-us/dotnet/api/System.String 'System.String')[&gt;](Sagara.Core.Validation.ValidatableProperty_TValue_.md 'Sagara.Core.Validation.ValidatableProperty<TValue>')

<a name='Sagara.Core.Validation.ValidationHelper.CheckRequiredField(Sagara.Core.Validation.ValidatableProperty_string_,System.Collections.Generic.ICollection_Sagara.Core.Validation.RequestError_).errors'></a>

`errors` [System.Collections.Generic.ICollection&lt;](https://docs.microsoft.com/en-us/dotnet/api/System.Collections.Generic.ICollection-1 'System.Collections.Generic.ICollection`1')[RequestError](Sagara.Core.Validation.RequestError.md 'Sagara.Core.Validation.RequestError')[&gt;](https://docs.microsoft.com/en-us/dotnet/api/System.Collections.Generic.ICollection-1 'System.Collections.Generic.ICollection`1')

<a name='Sagara.Core.Validation.ValidationHelper.CheckRequiredField_T_(Sagara.Core.Validation.ValidatableProperty_System.Nullable_T__,System.Collections.Generic.ICollection_Sagara.Core.Validation.RequestError_)'></a>

## ValidationHelper.CheckRequiredField<T>(ValidatableProperty<Nullable<T>>, ICollection<RequestError>) Method

  
Add an error message if property's value is null.  
  
Use this overload for nullable value types.

```csharp
public static void CheckRequiredField<T>(Sagara.Core.Validation.ValidatableProperty<System.Nullable<T>> property, System.Collections.Generic.ICollection<Sagara.Core.Validation.RequestError> errors)
    where T : struct, System.ValueType, System.ValueType;
```
#### Type parameters

<a name='Sagara.Core.Validation.ValidationHelper.CheckRequiredField_T_(Sagara.Core.Validation.ValidatableProperty_System.Nullable_T__,System.Collections.Generic.ICollection_Sagara.Core.Validation.RequestError_).T'></a>

`T`
#### Parameters

<a name='Sagara.Core.Validation.ValidationHelper.CheckRequiredField_T_(Sagara.Core.Validation.ValidatableProperty_System.Nullable_T__,System.Collections.Generic.ICollection_Sagara.Core.Validation.RequestError_).property'></a>

`property` [Sagara.Core.Validation.ValidatableProperty&lt;](Sagara.Core.Validation.ValidatableProperty_TValue_.md 'Sagara.Core.Validation.ValidatableProperty<TValue>')[System.Nullable&lt;](https://docs.microsoft.com/en-us/dotnet/api/System.Nullable-1 'System.Nullable`1')[T](Sagara.Core.Validation.ValidationHelper.md#Sagara.Core.Validation.ValidationHelper.CheckRequiredField_T_(Sagara.Core.Validation.ValidatableProperty_System.Nullable_T__,System.Collections.Generic.ICollection_Sagara.Core.Validation.RequestError_).T 'Sagara.Core.Validation.ValidationHelper.CheckRequiredField<T>(Sagara.Core.Validation.ValidatableProperty<System.Nullable<T>>, System.Collections.Generic.ICollection<Sagara.Core.Validation.RequestError>).T')[&gt;](https://docs.microsoft.com/en-us/dotnet/api/System.Nullable-1 'System.Nullable`1')[&gt;](Sagara.Core.Validation.ValidatableProperty_TValue_.md 'Sagara.Core.Validation.ValidatableProperty<TValue>')

<a name='Sagara.Core.Validation.ValidationHelper.CheckRequiredField_T_(Sagara.Core.Validation.ValidatableProperty_System.Nullable_T__,System.Collections.Generic.ICollection_Sagara.Core.Validation.RequestError_).errors'></a>

`errors` [System.Collections.Generic.ICollection&lt;](https://docs.microsoft.com/en-us/dotnet/api/System.Collections.Generic.ICollection-1 'System.Collections.Generic.ICollection`1')[RequestError](Sagara.Core.Validation.RequestError.md 'Sagara.Core.Validation.RequestError')[&gt;](https://docs.microsoft.com/en-us/dotnet/api/System.Collections.Generic.ICollection-1 'System.Collections.Generic.ICollection`1')

<a name='Sagara.Core.Validation.ValidationHelper.CheckRequiredField_T_(Sagara.Core.Validation.ValidatableProperty_T_,System.Collections.Generic.ICollection_Sagara.Core.Validation.RequestError_)'></a>

## ValidationHelper.CheckRequiredField<T>(ValidatableProperty<T>, ICollection<RequestError>) Method

  
Add an error message if property's value is null.  
  
Use this overload for reference types.

```csharp
public static void CheckRequiredField<T>(Sagara.Core.Validation.ValidatableProperty<T> property, System.Collections.Generic.ICollection<Sagara.Core.Validation.RequestError> errors)
    where T : class?;
```
#### Type parameters

<a name='Sagara.Core.Validation.ValidationHelper.CheckRequiredField_T_(Sagara.Core.Validation.ValidatableProperty_T_,System.Collections.Generic.ICollection_Sagara.Core.Validation.RequestError_).T'></a>

`T`
#### Parameters

<a name='Sagara.Core.Validation.ValidationHelper.CheckRequiredField_T_(Sagara.Core.Validation.ValidatableProperty_T_,System.Collections.Generic.ICollection_Sagara.Core.Validation.RequestError_).property'></a>

`property` [Sagara.Core.Validation.ValidatableProperty&lt;](Sagara.Core.Validation.ValidatableProperty_TValue_.md 'Sagara.Core.Validation.ValidatableProperty<TValue>')[T](Sagara.Core.Validation.ValidationHelper.md#Sagara.Core.Validation.ValidationHelper.CheckRequiredField_T_(Sagara.Core.Validation.ValidatableProperty_T_,System.Collections.Generic.ICollection_Sagara.Core.Validation.RequestError_).T 'Sagara.Core.Validation.ValidationHelper.CheckRequiredField<T>(Sagara.Core.Validation.ValidatableProperty<T>, System.Collections.Generic.ICollection<Sagara.Core.Validation.RequestError>).T')[&gt;](Sagara.Core.Validation.ValidatableProperty_TValue_.md 'Sagara.Core.Validation.ValidatableProperty<TValue>')

<a name='Sagara.Core.Validation.ValidationHelper.CheckRequiredField_T_(Sagara.Core.Validation.ValidatableProperty_T_,System.Collections.Generic.ICollection_Sagara.Core.Validation.RequestError_).errors'></a>

`errors` [System.Collections.Generic.ICollection&lt;](https://docs.microsoft.com/en-us/dotnet/api/System.Collections.Generic.ICollection-1 'System.Collections.Generic.ICollection`1')[RequestError](Sagara.Core.Validation.RequestError.md 'Sagara.Core.Validation.RequestError')[&gt;](https://docs.microsoft.com/en-us/dotnet/api/System.Collections.Generic.ICollection-1 'System.Collections.Generic.ICollection`1')

<a name='Sagara.Core.Validation.ValidationHelper.CheckStringMaxLength(Sagara.Core.Validation.ValidatableProperty_string_,int,System.Collections.Generic.ICollection_Sagara.Core.Validation.RequestError_)'></a>

## ValidationHelper.CheckStringMaxLength(ValidatableProperty<string>, int, ICollection<RequestError>) Method

Add an error message if property's value length exceeds maxLength.

```csharp
public static void CheckStringMaxLength(Sagara.Core.Validation.ValidatableProperty<string?> property, int maxLength, System.Collections.Generic.ICollection<Sagara.Core.Validation.RequestError> errors);
```
#### Parameters

<a name='Sagara.Core.Validation.ValidationHelper.CheckStringMaxLength(Sagara.Core.Validation.ValidatableProperty_string_,int,System.Collections.Generic.ICollection_Sagara.Core.Validation.RequestError_).property'></a>

`property` [Sagara.Core.Validation.ValidatableProperty&lt;](Sagara.Core.Validation.ValidatableProperty_TValue_.md 'Sagara.Core.Validation.ValidatableProperty<TValue>')[System.String](https://docs.microsoft.com/en-us/dotnet/api/System.String 'System.String')[&gt;](Sagara.Core.Validation.ValidatableProperty_TValue_.md 'Sagara.Core.Validation.ValidatableProperty<TValue>')

<a name='Sagara.Core.Validation.ValidationHelper.CheckStringMaxLength(Sagara.Core.Validation.ValidatableProperty_string_,int,System.Collections.Generic.ICollection_Sagara.Core.Validation.RequestError_).maxLength'></a>

`maxLength` [System.Int32](https://docs.microsoft.com/en-us/dotnet/api/System.Int32 'System.Int32')

<a name='Sagara.Core.Validation.ValidationHelper.CheckStringMaxLength(Sagara.Core.Validation.ValidatableProperty_string_,int,System.Collections.Generic.ICollection_Sagara.Core.Validation.RequestError_).errors'></a>

`errors` [System.Collections.Generic.ICollection&lt;](https://docs.microsoft.com/en-us/dotnet/api/System.Collections.Generic.ICollection-1 'System.Collections.Generic.ICollection`1')[RequestError](Sagara.Core.Validation.RequestError.md 'Sagara.Core.Validation.RequestError')[&gt;](https://docs.microsoft.com/en-us/dotnet/api/System.Collections.Generic.ICollection-1 'System.Collections.Generic.ICollection`1')

<a name='Sagara.Core.Validation.ValidationHelper.CheckStringMinLength(Sagara.Core.Validation.ValidatableProperty_string_,int,System.Collections.Generic.ICollection_Sagara.Core.Validation.RequestError_)'></a>

## ValidationHelper.CheckStringMinLength(ValidatableProperty<string>, int, ICollection<RequestError>) Method

Add an error message if property's value length is less than minLength.

```csharp
public static void CheckStringMinLength(Sagara.Core.Validation.ValidatableProperty<string?> property, int minLength, System.Collections.Generic.ICollection<Sagara.Core.Validation.RequestError> errors);
```
#### Parameters

<a name='Sagara.Core.Validation.ValidationHelper.CheckStringMinLength(Sagara.Core.Validation.ValidatableProperty_string_,int,System.Collections.Generic.ICollection_Sagara.Core.Validation.RequestError_).property'></a>

`property` [Sagara.Core.Validation.ValidatableProperty&lt;](Sagara.Core.Validation.ValidatableProperty_TValue_.md 'Sagara.Core.Validation.ValidatableProperty<TValue>')[System.String](https://docs.microsoft.com/en-us/dotnet/api/System.String 'System.String')[&gt;](Sagara.Core.Validation.ValidatableProperty_TValue_.md 'Sagara.Core.Validation.ValidatableProperty<TValue>')

<a name='Sagara.Core.Validation.ValidationHelper.CheckStringMinLength(Sagara.Core.Validation.ValidatableProperty_string_,int,System.Collections.Generic.ICollection_Sagara.Core.Validation.RequestError_).minLength'></a>

`minLength` [System.Int32](https://docs.microsoft.com/en-us/dotnet/api/System.Int32 'System.Int32')

<a name='Sagara.Core.Validation.ValidationHelper.CheckStringMinLength(Sagara.Core.Validation.ValidatableProperty_string_,int,System.Collections.Generic.ICollection_Sagara.Core.Validation.RequestError_).errors'></a>

`errors` [System.Collections.Generic.ICollection&lt;](https://docs.microsoft.com/en-us/dotnet/api/System.Collections.Generic.ICollection-1 'System.Collections.Generic.ICollection`1')[RequestError](Sagara.Core.Validation.RequestError.md 'Sagara.Core.Validation.RequestError')[&gt;](https://docs.microsoft.com/en-us/dotnet/api/System.Collections.Generic.ICollection-1 'System.Collections.Generic.ICollection`1')

<a name='Sagara.Core.Validation.ValidationHelper.GetDisplayName_T_(thisSagara.Core.Validation.ValidatableProperty_T_)'></a>

## ValidationHelper.GetDisplayName<T>(this ValidatableProperty<T>) Method

Return displayName if it's not null; else, return propertyName.

```csharp
public static string GetDisplayName<T>(this Sagara.Core.Validation.ValidatableProperty<T> property);
```
#### Type parameters

<a name='Sagara.Core.Validation.ValidationHelper.GetDisplayName_T_(thisSagara.Core.Validation.ValidatableProperty_T_).T'></a>

`T`
#### Parameters

<a name='Sagara.Core.Validation.ValidationHelper.GetDisplayName_T_(thisSagara.Core.Validation.ValidatableProperty_T_).property'></a>

`property` [Sagara.Core.Validation.ValidatableProperty&lt;](Sagara.Core.Validation.ValidatableProperty_TValue_.md 'Sagara.Core.Validation.ValidatableProperty<TValue>')[T](Sagara.Core.Validation.ValidationHelper.md#Sagara.Core.Validation.ValidationHelper.GetDisplayName_T_(thisSagara.Core.Validation.ValidatableProperty_T_).T 'Sagara.Core.Validation.ValidationHelper.GetDisplayName<T>(this Sagara.Core.Validation.ValidatableProperty<T>).T')[&gt;](Sagara.Core.Validation.ValidatableProperty_TValue_.md 'Sagara.Core.Validation.ValidatableProperty<TValue>')

#### Returns
[System.String](https://docs.microsoft.com/en-us/dotnet/api/System.String 'System.String')