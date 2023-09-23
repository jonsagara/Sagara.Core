#### [Sagara.Core](index.md 'index')
### [Sagara.Core.Validation](index.md#Sagara.Core.Validation 'Sagara.Core.Validation')

## ValidatableProperty<TValue> Struct

Used to populate values from MediatR models and pass to a validation service class. This allows the validation  
service to return a list of errors containing the HTML form element names that generated the errors so that  
we can highlight them on the form using JavaScript.

```csharp
public readonly struct ValidatableProperty<TValue> :
System.IEquatable<Sagara.Core.Validation.ValidatableProperty<TValue>>
```
#### Type parameters

<a name='Sagara.Core.Validation.ValidatableProperty_TValue_.TValue'></a>

`TValue`

The value of value to validate.

Implements [System.IEquatable&lt;](https://docs.microsoft.com/en-us/dotnet/api/System.IEquatable-1 'System.IEquatable`1')[Sagara.Core.Validation.ValidatableProperty&lt;](Sagara.Core.Validation.ValidatableProperty_TValue_.md 'Sagara.Core.Validation.ValidatableProperty<TValue>')[TValue](Sagara.Core.Validation.ValidatableProperty_TValue_.md#Sagara.Core.Validation.ValidatableProperty_TValue_.TValue 'Sagara.Core.Validation.ValidatableProperty<TValue>.TValue')[&gt;](Sagara.Core.Validation.ValidatableProperty_TValue_.md 'Sagara.Core.Validation.ValidatableProperty<TValue>')[&gt;](https://docs.microsoft.com/en-us/dotnet/api/System.IEquatable-1 'System.IEquatable`1')
### Constructors

<a name='Sagara.Core.Validation.ValidatableProperty_TValue_.ValidatableProperty(TValue,string,string)'></a>

## ValidatableProperty(TValue, string, string) Constructor

Used to populate values from MediatR models and pass to a validation service class. This allows the validation  
service to return a list of errors containing the HTML form element names that generated the errors so that  
we can highlight them on the form using JavaScript.

```csharp
public ValidatableProperty(TValue Value, string PropertyName, string? DisplayName);
```
#### Parameters

<a name='Sagara.Core.Validation.ValidatableProperty_TValue_.ValidatableProperty(TValue,string,string).Value'></a>

`Value` [TValue](Sagara.Core.Validation.ValidatableProperty_TValue_.md#Sagara.Core.Validation.ValidatableProperty_TValue_.TValue 'Sagara.Core.Validation.ValidatableProperty<TValue>.TValue')

The value to validate.

<a name='Sagara.Core.Validation.ValidatableProperty_TValue_.ValidatableProperty(TValue,string,string).PropertyName'></a>

`PropertyName` [System.String](https://docs.microsoft.com/en-us/dotnet/api/System.String 'System.String')

Required. The name of the property corresponding to the HTML form element.

<a name='Sagara.Core.Validation.ValidatableProperty_TValue_.ValidatableProperty(TValue,string,string).DisplayName'></a>

`DisplayName` [System.String](https://docs.microsoft.com/en-us/dotnet/api/System.String 'System.String')

Optional. User-friendly display name.
### Properties

<a name='Sagara.Core.Validation.ValidatableProperty_TValue_.DisplayName'></a>

## ValidatableProperty<TValue>.DisplayName Property

Optional. User-friendly display name.

```csharp
public string? DisplayName { get; set; }
```

#### Property Value
[System.String](https://docs.microsoft.com/en-us/dotnet/api/System.String 'System.String')

<a name='Sagara.Core.Validation.ValidatableProperty_TValue_.PropertyName'></a>

## ValidatableProperty<TValue>.PropertyName Property

Required. The name of the property corresponding to the HTML form element.

```csharp
public string PropertyName { get; set; }
```

#### Property Value
[System.String](https://docs.microsoft.com/en-us/dotnet/api/System.String 'System.String')

<a name='Sagara.Core.Validation.ValidatableProperty_TValue_.Value'></a>

## ValidatableProperty<TValue>.Value Property

The value to validate.

```csharp
public TValue Value { get; set; }
```

#### Property Value
[TValue](Sagara.Core.Validation.ValidatableProperty_TValue_.md#Sagara.Core.Validation.ValidatableProperty_TValue_.TValue 'Sagara.Core.Validation.ValidatableProperty<TValue>.TValue')