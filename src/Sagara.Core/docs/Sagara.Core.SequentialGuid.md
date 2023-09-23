#### [Sagara.Core](index.md 'index')
### [Sagara.Core](index.md#Sagara.Core 'Sagara.Core')

## SequentialGuid Class

Generates Guids sequentially.

```csharp
public class SequentialGuid
```

Inheritance [System.Object](https://docs.microsoft.com/en-us/dotnet/api/System.Object 'System.Object') &#129106; SequentialGuid
### Constructors

<a name='Sagara.Core.SequentialGuid.SequentialGuid()'></a>

## SequentialGuid() Constructor

Default .ctor. Initializes [CurrentGuid](Sagara.Core.SequentialGuid.md#Sagara.Core.SequentialGuid.CurrentGuid 'Sagara.Core.SequentialGuid.CurrentGuid') with a new value.

```csharp
public SequentialGuid();
```

<a name='Sagara.Core.SequentialGuid.SequentialGuid(System.Guid)'></a>

## SequentialGuid(Guid) Constructor

Constructor that initializes [CurrentGuid](Sagara.Core.SequentialGuid.md#Sagara.Core.SequentialGuid.CurrentGuid 'Sagara.Core.SequentialGuid.CurrentGuid') with the provided Guid.

```csharp
public SequentialGuid(System.Guid previousGuid);
```
#### Parameters

<a name='Sagara.Core.SequentialGuid.SequentialGuid(System.Guid).previousGuid'></a>

`previousGuid` [System.Guid](https://docs.microsoft.com/en-us/dotnet/api/System.Guid 'System.Guid')

The guid to use to initialize this instance.
### Fields

<a name='Sagara.Core.SequentialGuid._epochTicks'></a>

## SequentialGuid._epochTicks Field

Equivalent to 'SELECT CAST(0 AS DATETIME)'. Specify UTC since we're using UTC in GenerateComb, and   
since all the servers we run on are set to UTC.

```csharp
private static readonly long _epochTicks;
```

#### Field Value
[System.Int64](https://docs.microsoft.com/en-us/dotnet/api/System.Int64 'System.Int64')
### Properties

<a name='Sagara.Core.SequentialGuid.CurrentGuid'></a>

## SequentialGuid.CurrentGuid Property

The current sequential Guid value.

```csharp
public System.Guid CurrentGuid { get; set; }
```

#### Property Value
[System.Guid](https://docs.microsoft.com/en-us/dotnet/api/System.Guid 'System.Guid')
### Methods

<a name='Sagara.Core.SequentialGuid.GenerateComb()'></a>

## SequentialGuid.GenerateComb() Method

Generate a sequential Guid.

```csharp
public static System.Guid GenerateComb();
```

#### Returns
[System.Guid](https://docs.microsoft.com/en-us/dotnet/api/System.Guid 'System.Guid')

<a name='Sagara.Core.SequentialGuid.Increment(Sagara.Core.SequentialGuid)'></a>

## SequentialGuid.Increment(SequentialGuid) Method

Increment one or more bytes of [CurrentGuid](Sagara.Core.SequentialGuid.md#Sagara.Core.SequentialGuid.CurrentGuid 'Sagara.Core.SequentialGuid.CurrentGuid') to get the next sequential Guid,  
and then copy this new value back to [CurrentGuid](Sagara.Core.SequentialGuid.md#Sagara.Core.SequentialGuid.CurrentGuid 'Sagara.Core.SequentialGuid.CurrentGuid').

```csharp
public static Sagara.Core.SequentialGuid Increment(Sagara.Core.SequentialGuid sequentialGuid);
```
#### Parameters

<a name='Sagara.Core.SequentialGuid.Increment(Sagara.Core.SequentialGuid).sequentialGuid'></a>

`sequentialGuid` [SequentialGuid](Sagara.Core.SequentialGuid.md 'Sagara.Core.SequentialGuid')

The current sequential Guid.

#### Returns
[SequentialGuid](Sagara.Core.SequentialGuid.md 'Sagara.Core.SequentialGuid')  
The incremented sequential Guid.
### Operators

<a name='Sagara.Core.SequentialGuid.op_Increment(Sagara.Core.SequentialGuid)'></a>

## SequentialGuid.operator ++(SequentialGuid) Operator

Increment one or more bytes of [CurrentGuid](Sagara.Core.SequentialGuid.md#Sagara.Core.SequentialGuid.CurrentGuid 'Sagara.Core.SequentialGuid.CurrentGuid') to get the next sequential Guid,  
and then copy this new value back to [CurrentGuid](Sagara.Core.SequentialGuid.md#Sagara.Core.SequentialGuid.CurrentGuid 'Sagara.Core.SequentialGuid.CurrentGuid').

```csharp
public static Sagara.Core.SequentialGuid operator ++(Sagara.Core.SequentialGuid sequentialGuid);
```
#### Parameters

<a name='Sagara.Core.SequentialGuid.op_Increment(Sagara.Core.SequentialGuid).sequentialGuid'></a>

`sequentialGuid` [SequentialGuid](Sagara.Core.SequentialGuid.md 'Sagara.Core.SequentialGuid')

The current sequential Guid.

#### Returns
[SequentialGuid](Sagara.Core.SequentialGuid.md 'Sagara.Core.SequentialGuid')  
The incremented sequential Guid.