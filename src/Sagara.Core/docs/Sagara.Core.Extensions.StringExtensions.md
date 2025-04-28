#### [Sagara.Core](index.md 'index')
### [Sagara.Core.Extensions](index.md#Sagara.Core.Extensions 'Sagara.Core.Extensions')

## StringExtensions Class

```csharp
public static class StringExtensions
```

Inheritance [System.Object](https://docs.microsoft.com/en-us/dotnet/api/System.Object 'System.Object') &#129106; StringExtensions
### Methods

<a name='Sagara.Core.Extensions.StringExtensions.Left(thisstring,int)'></a>

## StringExtensions.Left(this string, int) Method

Take the left-most [length](Sagara.Core.Extensions.StringExtensions.md#Sagara.Core.Extensions.StringExtensions.Left(thisstring,int).length 'Sagara.Core.Extensions.StringExtensions.Left(this string, int).length') characters of the given string.

```csharp
public static string Left(this string value, int length);
```
#### Parameters

<a name='Sagara.Core.Extensions.StringExtensions.Left(thisstring,int).value'></a>

`value` [System.String](https://docs.microsoft.com/en-us/dotnet/api/System.String 'System.String')

The string value.

<a name='Sagara.Core.Extensions.StringExtensions.Left(thisstring,int).length'></a>

`length` [System.Int32](https://docs.microsoft.com/en-us/dotnet/api/System.Int32 'System.Int32')

The maximum string length of the returned string.

#### Returns
[System.String](https://docs.microsoft.com/en-us/dotnet/api/System.String 'System.String')  
  
If [value](Sagara.Core.Extensions.StringExtensions.md#Sagara.Core.Extensions.StringExtensions.Left(thisstring,int).value 'Sagara.Core.Extensions.StringExtensions.Left(this string, int).value').Length >= [length](Sagara.Core.Extensions.StringExtensions.md#Sagara.Core.Extensions.StringExtensions.Left(thisstring,int).length 'Sagara.Core.Extensions.StringExtensions.Left(this string, int).length'), return the left-most [length](Sagara.Core.Extensions.StringExtensions.md#Sagara.Core.Extensions.StringExtensions.Left(thisstring,int).length 'Sagara.Core.Extensions.StringExtensions.Left(this string, int).length') characters.  
  
If [value](Sagara.Core.Extensions.StringExtensions.md#Sagara.Core.Extensions.StringExtensions.Left(thisstring,int).value 'Sagara.Core.Extensions.StringExtensions.Left(this string, int).value').Length < length, return [value](Sagara.Core.Extensions.StringExtensions.md#Sagara.Core.Extensions.StringExtensions.Left(thisstring,int).value 'Sagara.Core.Extensions.StringExtensions.Left(this string, int).value') unchanged.

<a name='Sagara.Core.Extensions.StringExtensions.Right(thisstring,int)'></a>

## StringExtensions.Right(this string, int) Method

Take the right-most [length](Sagara.Core.Extensions.StringExtensions.md#Sagara.Core.Extensions.StringExtensions.Right(thisstring,int).length 'Sagara.Core.Extensions.StringExtensions.Right(this string, int).length') characters of the given string.

```csharp
public static string Right(this string value, int length);
```
#### Parameters

<a name='Sagara.Core.Extensions.StringExtensions.Right(thisstring,int).value'></a>

`value` [System.String](https://docs.microsoft.com/en-us/dotnet/api/System.String 'System.String')

The string value.

<a name='Sagara.Core.Extensions.StringExtensions.Right(thisstring,int).length'></a>

`length` [System.Int32](https://docs.microsoft.com/en-us/dotnet/api/System.Int32 'System.Int32')

The maximum string length of the returned string.

#### Returns
[System.String](https://docs.microsoft.com/en-us/dotnet/api/System.String 'System.String')  
  
If [value](Sagara.Core.Extensions.StringExtensions.md#Sagara.Core.Extensions.StringExtensions.Right(thisstring,int).value 'Sagara.Core.Extensions.StringExtensions.Right(this string, int).value').Length >= [length](Sagara.Core.Extensions.StringExtensions.md#Sagara.Core.Extensions.StringExtensions.Right(thisstring,int).length 'Sagara.Core.Extensions.StringExtensions.Right(this string, int).length'), return the right-most [length](Sagara.Core.Extensions.StringExtensions.md#Sagara.Core.Extensions.StringExtensions.Right(thisstring,int).length 'Sagara.Core.Extensions.StringExtensions.Right(this string, int).length') characters.  
  
If [value](Sagara.Core.Extensions.StringExtensions.md#Sagara.Core.Extensions.StringExtensions.Right(thisstring,int).value 'Sagara.Core.Extensions.StringExtensions.Right(this string, int).value').Length < length, return [value](Sagara.Core.Extensions.StringExtensions.md#Sagara.Core.Extensions.StringExtensions.Right(thisstring,int).value 'Sagara.Core.Extensions.StringExtensions.Right(this string, int).value') unchanged.