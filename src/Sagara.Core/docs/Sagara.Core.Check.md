#### [Sagara.Core](index.md 'index')
### [Sagara.Core](index.md#Sagara.Core 'Sagara.Core')

## Check Class

  
Argument checker so that we don't have to keep writing the same guard clauses all over the place.  
  
This updated version uses the new [System.Runtime.CompilerServices.CallerArgumentExpressionAttribute](https://docs.microsoft.com/en-us/dotnet/api/System.Runtime.CompilerServices.CallerArgumentExpressionAttribute 'System.Runtime.CompilerServices.CallerArgumentExpressionAttribute') to get the   
            argument expression from the call site so that we don't have to manually pass that in.

```csharp
public static class Check
```

Inheritance [System.Object](https://docs.microsoft.com/en-us/dotnet/api/System.Object 'System.Object') &#129106; Check

### Remarks
  
NOTE: F# does not yet support [System.Runtime.CompilerServices.CallerArgumentExpressionAttribute](https://docs.microsoft.com/en-us/dotnet/api/System.Runtime.CompilerServices.CallerArgumentExpressionAttribute 'System.Runtime.CompilerServices.CallerArgumentExpressionAttribute'), so F# callers will need to pass in  
            the second valArgExpression argument. C# callers can omit this second argument.  
  
NOTE: Based on an older version of this utility class from EF Core: https://github.com/dotnet/efcore/blob/release/8.0/src/Shared/Check.cs
### Methods

<a name='Sagara.Core.Check.HasNoEmpties(System.Collections.Generic.IReadOnlyCollection_string_,string,string,int,string)'></a>

## Check.HasNoEmpties(IReadOnlyCollection<string>, string, string, int, string) Method

Throws an [System.ArgumentNullException](https://docs.microsoft.com/en-us/dotnet/api/System.ArgumentNullException 'System.ArgumentNullException') if the collection is null, or an [System.ArgumentException](https://docs.microsoft.com/en-us/dotnet/api/System.ArgumentException 'System.ArgumentException') if  
the collection has one or more null or white space strings.

```csharp
public static void HasNoEmpties(System.Collections.Generic.IReadOnlyCollection<string> value, string? valueArgExpression=null, string? memberName=null, int sourceLineNumber=0, string? sourceFilePath=null);
```
#### Parameters

<a name='Sagara.Core.Check.HasNoEmpties(System.Collections.Generic.IReadOnlyCollection_string_,string,string,int,string).value'></a>

`value` [System.Collections.Generic.IReadOnlyCollection&lt;](https://docs.microsoft.com/en-us/dotnet/api/System.Collections.Generic.IReadOnlyCollection-1 'System.Collections.Generic.IReadOnlyCollection`1')[System.String](https://docs.microsoft.com/en-us/dotnet/api/System.String 'System.String')[&gt;](https://docs.microsoft.com/en-us/dotnet/api/System.Collections.Generic.IReadOnlyCollection-1 'System.Collections.Generic.IReadOnlyCollection`1')

<a name='Sagara.Core.Check.HasNoEmpties(System.Collections.Generic.IReadOnlyCollection_string_,string,string,int,string).valueArgExpression'></a>

`valueArgExpression` [System.String](https://docs.microsoft.com/en-us/dotnet/api/System.String 'System.String')

<a name='Sagara.Core.Check.HasNoEmpties(System.Collections.Generic.IReadOnlyCollection_string_,string,string,int,string).memberName'></a>

`memberName` [System.String](https://docs.microsoft.com/en-us/dotnet/api/System.String 'System.String')

<a name='Sagara.Core.Check.HasNoEmpties(System.Collections.Generic.IReadOnlyCollection_string_,string,string,int,string).sourceLineNumber'></a>

`sourceLineNumber` [System.Int32](https://docs.microsoft.com/en-us/dotnet/api/System.Int32 'System.Int32')

<a name='Sagara.Core.Check.HasNoEmpties(System.Collections.Generic.IReadOnlyCollection_string_,string,string,int,string).sourceFilePath'></a>

`sourceFilePath` [System.String](https://docs.microsoft.com/en-us/dotnet/api/System.String 'System.String')

<a name='Sagara.Core.Check.HasNoNulls_T_(System.Collections.Generic.IReadOnlyList_T_,string,string,int,string)'></a>

## Check.HasNoNulls<T>(IReadOnlyList<T>, string, string, int, string) Method

Throws an [System.ArgumentNullException](https://docs.microsoft.com/en-us/dotnet/api/System.ArgumentNullException 'System.ArgumentNullException') if the collection is null, or an [System.ArgumentException](https://docs.microsoft.com/en-us/dotnet/api/System.ArgumentException 'System.ArgumentException') if  
the collection has one or more null values.

```csharp
public static void HasNoNulls<T>(System.Collections.Generic.IReadOnlyList<T> value, string? valueArgExpression=null, string? memberName=null, int sourceLineNumber=0, string? sourceFilePath=null);
```
#### Type parameters

<a name='Sagara.Core.Check.HasNoNulls_T_(System.Collections.Generic.IReadOnlyList_T_,string,string,int,string).T'></a>

`T`
#### Parameters

<a name='Sagara.Core.Check.HasNoNulls_T_(System.Collections.Generic.IReadOnlyList_T_,string,string,int,string).value'></a>

`value` [System.Collections.Generic.IReadOnlyList&lt;](https://docs.microsoft.com/en-us/dotnet/api/System.Collections.Generic.IReadOnlyList-1 'System.Collections.Generic.IReadOnlyList`1')[T](Sagara.Core.Check.md#Sagara.Core.Check.HasNoNulls_T_(System.Collections.Generic.IReadOnlyList_T_,string,string,int,string).T 'Sagara.Core.Check.HasNoNulls<T>(System.Collections.Generic.IReadOnlyList<T>, string, string, int, string).T')[&gt;](https://docs.microsoft.com/en-us/dotnet/api/System.Collections.Generic.IReadOnlyList-1 'System.Collections.Generic.IReadOnlyList`1')

<a name='Sagara.Core.Check.HasNoNulls_T_(System.Collections.Generic.IReadOnlyList_T_,string,string,int,string).valueArgExpression'></a>

`valueArgExpression` [System.String](https://docs.microsoft.com/en-us/dotnet/api/System.String 'System.String')

<a name='Sagara.Core.Check.HasNoNulls_T_(System.Collections.Generic.IReadOnlyList_T_,string,string,int,string).memberName'></a>

`memberName` [System.String](https://docs.microsoft.com/en-us/dotnet/api/System.String 'System.String')

<a name='Sagara.Core.Check.HasNoNulls_T_(System.Collections.Generic.IReadOnlyList_T_,string,string,int,string).sourceLineNumber'></a>

`sourceLineNumber` [System.Int32](https://docs.microsoft.com/en-us/dotnet/api/System.Int32 'System.Int32')

<a name='Sagara.Core.Check.HasNoNulls_T_(System.Collections.Generic.IReadOnlyList_T_,string,string,int,string).sourceFilePath'></a>

`sourceFilePath` [System.String](https://docs.microsoft.com/en-us/dotnet/api/System.String 'System.String')

<a name='Sagara.Core.Check.NotEmpty(string,string,string,int,string)'></a>

## Check.NotEmpty(string, string, string, int, string) Method

Throws an [System.ArgumentNullException](https://docs.microsoft.com/en-us/dotnet/api/System.ArgumentNullException 'System.ArgumentNullException') if the string is null, or an [System.ArgumentException](https://docs.microsoft.com/en-us/dotnet/api/System.ArgumentException 'System.ArgumentException') if the  
string is null or white space.

```csharp
public static void NotEmpty(string? value, string? valueArgExpression=null, string? memberName=null, int sourceLineNumber=0, string? sourceFilePath=null);
```
#### Parameters

<a name='Sagara.Core.Check.NotEmpty(string,string,string,int,string).value'></a>

`value` [System.String](https://docs.microsoft.com/en-us/dotnet/api/System.String 'System.String')

<a name='Sagara.Core.Check.NotEmpty(string,string,string,int,string).valueArgExpression'></a>

`valueArgExpression` [System.String](https://docs.microsoft.com/en-us/dotnet/api/System.String 'System.String')

<a name='Sagara.Core.Check.NotEmpty(string,string,string,int,string).memberName'></a>

`memberName` [System.String](https://docs.microsoft.com/en-us/dotnet/api/System.String 'System.String')

<a name='Sagara.Core.Check.NotEmpty(string,string,string,int,string).sourceLineNumber'></a>

`sourceLineNumber` [System.Int32](https://docs.microsoft.com/en-us/dotnet/api/System.Int32 'System.Int32')

<a name='Sagara.Core.Check.NotEmpty(string,string,string,int,string).sourceFilePath'></a>

`sourceFilePath` [System.String](https://docs.microsoft.com/en-us/dotnet/api/System.String 'System.String')

<a name='Sagara.Core.Check.NotEmpty(System.Guid,string,string,int,string)'></a>

## Check.NotEmpty(Guid, string, string, int, string) Method

Throws an [System.ArgumentException](https://docs.microsoft.com/en-us/dotnet/api/System.ArgumentException 'System.ArgumentException') if the Guid equals Guid.Empty.

```csharp
public static void NotEmpty(System.Guid value, string? valueArgExpression=null, string? memberName=null, int sourceLineNumber=0, string? sourceFilePath=null);
```
#### Parameters

<a name='Sagara.Core.Check.NotEmpty(System.Guid,string,string,int,string).value'></a>

`value` [System.Guid](https://docs.microsoft.com/en-us/dotnet/api/System.Guid 'System.Guid')

<a name='Sagara.Core.Check.NotEmpty(System.Guid,string,string,int,string).valueArgExpression'></a>

`valueArgExpression` [System.String](https://docs.microsoft.com/en-us/dotnet/api/System.String 'System.String')

<a name='Sagara.Core.Check.NotEmpty(System.Guid,string,string,int,string).memberName'></a>

`memberName` [System.String](https://docs.microsoft.com/en-us/dotnet/api/System.String 'System.String')

<a name='Sagara.Core.Check.NotEmpty(System.Guid,string,string,int,string).sourceLineNumber'></a>

`sourceLineNumber` [System.Int32](https://docs.microsoft.com/en-us/dotnet/api/System.Int32 'System.Int32')

<a name='Sagara.Core.Check.NotEmpty(System.Guid,string,string,int,string).sourceFilePath'></a>

`sourceFilePath` [System.String](https://docs.microsoft.com/en-us/dotnet/api/System.String 'System.String')

<a name='Sagara.Core.Check.NotEmpty_T_(System.Collections.Generic.IReadOnlyCollection_T_,string,string,int,string)'></a>

## Check.NotEmpty<T>(IReadOnlyCollection<T>, string, string, int, string) Method

Throws an [System.ArgumentNullException](https://docs.microsoft.com/en-us/dotnet/api/System.ArgumentNullException 'System.ArgumentNullException') if the collection is null, or an [System.ArgumentException](https://docs.microsoft.com/en-us/dotnet/api/System.ArgumentException 'System.ArgumentException') if  
the collection is empty.

```csharp
public static void NotEmpty<T>(System.Collections.Generic.IReadOnlyCollection<T> value, string? valueArgExpression=null, string? memberName=null, int sourceLineNumber=0, string? sourceFilePath=null);
```
#### Type parameters

<a name='Sagara.Core.Check.NotEmpty_T_(System.Collections.Generic.IReadOnlyCollection_T_,string,string,int,string).T'></a>

`T`
#### Parameters

<a name='Sagara.Core.Check.NotEmpty_T_(System.Collections.Generic.IReadOnlyCollection_T_,string,string,int,string).value'></a>

`value` [System.Collections.Generic.IReadOnlyCollection&lt;](https://docs.microsoft.com/en-us/dotnet/api/System.Collections.Generic.IReadOnlyCollection-1 'System.Collections.Generic.IReadOnlyCollection`1')[T](Sagara.Core.Check.md#Sagara.Core.Check.NotEmpty_T_(System.Collections.Generic.IReadOnlyCollection_T_,string,string,int,string).T 'Sagara.Core.Check.NotEmpty<T>(System.Collections.Generic.IReadOnlyCollection<T>, string, string, int, string).T')[&gt;](https://docs.microsoft.com/en-us/dotnet/api/System.Collections.Generic.IReadOnlyCollection-1 'System.Collections.Generic.IReadOnlyCollection`1')

<a name='Sagara.Core.Check.NotEmpty_T_(System.Collections.Generic.IReadOnlyCollection_T_,string,string,int,string).valueArgExpression'></a>

`valueArgExpression` [System.String](https://docs.microsoft.com/en-us/dotnet/api/System.String 'System.String')

<a name='Sagara.Core.Check.NotEmpty_T_(System.Collections.Generic.IReadOnlyCollection_T_,string,string,int,string).memberName'></a>

`memberName` [System.String](https://docs.microsoft.com/en-us/dotnet/api/System.String 'System.String')

<a name='Sagara.Core.Check.NotEmpty_T_(System.Collections.Generic.IReadOnlyCollection_T_,string,string,int,string).sourceLineNumber'></a>

`sourceLineNumber` [System.Int32](https://docs.microsoft.com/en-us/dotnet/api/System.Int32 'System.Int32')

<a name='Sagara.Core.Check.NotEmpty_T_(System.Collections.Generic.IReadOnlyCollection_T_,string,string,int,string).sourceFilePath'></a>

`sourceFilePath` [System.String](https://docs.microsoft.com/en-us/dotnet/api/System.String 'System.String')

<a name='Sagara.Core.Check.NotNull_T_(T,string,string,int,string)'></a>

## Check.NotNull<T>(T, string, string, int, string) Method

Throws an [System.ArgumentNullException](https://docs.microsoft.com/en-us/dotnet/api/System.ArgumentNullException 'System.ArgumentNullException') if the value is null.

```csharp
public static void NotNull<T>(T value, string? valueArgExpression=null, string? memberName=null, int sourceLineNumber=0, string? sourceFilePath=null);
```
#### Type parameters

<a name='Sagara.Core.Check.NotNull_T_(T,string,string,int,string).T'></a>

`T`
#### Parameters

<a name='Sagara.Core.Check.NotNull_T_(T,string,string,int,string).value'></a>

`value` [T](Sagara.Core.Check.md#Sagara.Core.Check.NotNull_T_(T,string,string,int,string).T 'Sagara.Core.Check.NotNull<T>(T, string, string, int, string).T')

<a name='Sagara.Core.Check.NotNull_T_(T,string,string,int,string).valueArgExpression'></a>

`valueArgExpression` [System.String](https://docs.microsoft.com/en-us/dotnet/api/System.String 'System.String')

<a name='Sagara.Core.Check.NotNull_T_(T,string,string,int,string).memberName'></a>

`memberName` [System.String](https://docs.microsoft.com/en-us/dotnet/api/System.String 'System.String')

<a name='Sagara.Core.Check.NotNull_T_(T,string,string,int,string).sourceLineNumber'></a>

`sourceLineNumber` [System.Int32](https://docs.microsoft.com/en-us/dotnet/api/System.Int32 'System.Int32')

<a name='Sagara.Core.Check.NotNull_T_(T,string,string,int,string).sourceFilePath'></a>

`sourceFilePath` [System.String](https://docs.microsoft.com/en-us/dotnet/api/System.String 'System.String')

<a name='Sagara.Core.Check.NotOutOfRange_T_(T,T,T,string,string,int,string)'></a>

## Check.NotOutOfRange<T>(T, T, T, string, string, int, string) Method

Throws an [System.ArgumentOutOfRangeException](https://docs.microsoft.com/en-us/dotnet/api/System.ArgumentOutOfRangeException 'System.ArgumentOutOfRangeException') if [value](Sagara.Core.Check.md#Sagara.Core.Check.NotOutOfRange_T_(T,T,T,string,string,int,string).value 'Sagara.Core.Check.NotOutOfRange<T>(T, T, T, string, string, int, string).value') is outside the range of   
[[rangeLo](Sagara.Core.Check.md#Sagara.Core.Check.NotOutOfRange_T_(T,T,T,string,string,int,string).rangeLo 'Sagara.Core.Check.NotOutOfRange<T>(T, T, T, string, string, int, string).rangeLo'), [rangeHi](Sagara.Core.Check.md#Sagara.Core.Check.NotOutOfRange_T_(T,T,T,string,string,int,string).rangeHi 'Sagara.Core.Check.NotOutOfRange<T>(T, T, T, string, string, int, string).rangeHi')].

```csharp
public static void NotOutOfRange<T>(T value, T rangeLo, T rangeHi, string? valueArgExpression=null, string? memberName=null, int sourceLineNumber=0, string? sourceFilePath=null)
    where T : System.IComparable, System.IComparable<T>;
```
#### Type parameters

<a name='Sagara.Core.Check.NotOutOfRange_T_(T,T,T,string,string,int,string).T'></a>

`T`
#### Parameters

<a name='Sagara.Core.Check.NotOutOfRange_T_(T,T,T,string,string,int,string).value'></a>

`value` [T](Sagara.Core.Check.md#Sagara.Core.Check.NotOutOfRange_T_(T,T,T,string,string,int,string).T 'Sagara.Core.Check.NotOutOfRange<T>(T, T, T, string, string, int, string).T')

<a name='Sagara.Core.Check.NotOutOfRange_T_(T,T,T,string,string,int,string).rangeLo'></a>

`rangeLo` [T](Sagara.Core.Check.md#Sagara.Core.Check.NotOutOfRange_T_(T,T,T,string,string,int,string).T 'Sagara.Core.Check.NotOutOfRange<T>(T, T, T, string, string, int, string).T')

<a name='Sagara.Core.Check.NotOutOfRange_T_(T,T,T,string,string,int,string).rangeHi'></a>

`rangeHi` [T](Sagara.Core.Check.md#Sagara.Core.Check.NotOutOfRange_T_(T,T,T,string,string,int,string).T 'Sagara.Core.Check.NotOutOfRange<T>(T, T, T, string, string, int, string).T')

<a name='Sagara.Core.Check.NotOutOfRange_T_(T,T,T,string,string,int,string).valueArgExpression'></a>

`valueArgExpression` [System.String](https://docs.microsoft.com/en-us/dotnet/api/System.String 'System.String')

<a name='Sagara.Core.Check.NotOutOfRange_T_(T,T,T,string,string,int,string).memberName'></a>

`memberName` [System.String](https://docs.microsoft.com/en-us/dotnet/api/System.String 'System.String')

<a name='Sagara.Core.Check.NotOutOfRange_T_(T,T,T,string,string,int,string).sourceLineNumber'></a>

`sourceLineNumber` [System.Int32](https://docs.microsoft.com/en-us/dotnet/api/System.Int32 'System.Int32')

<a name='Sagara.Core.Check.NotOutOfRange_T_(T,T,T,string,string,int,string).sourceFilePath'></a>

`sourceFilePath` [System.String](https://docs.microsoft.com/en-us/dotnet/api/System.String 'System.String')