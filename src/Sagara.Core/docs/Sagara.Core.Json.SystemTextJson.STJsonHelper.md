#### [Sagara.Core](index.md 'index')
### [Sagara.Core.Json.SystemTextJson](index.md#Sagara.Core.Json.SystemTextJson 'Sagara.Core.Json.SystemTextJson')

## STJsonHelper Class

Helper methods for serializing JSON with System.Text.Json.

```csharp
public static class STJsonHelper
```

Inheritance [System.Object](https://docs.microsoft.com/en-us/dotnet/api/System.Object 'System.Object') &#129106; STJsonHelper
### Methods

<a name='Sagara.Core.Json.SystemTextJson.STJsonHelper.Deserialize_T_(string,bool)'></a>

## STJsonHelper.Deserialize<T>(string, bool) Method

Parse the JSON string into into an instance of the type specified by a generic type parameter. Default  
is to use case-insensitive property names.

```csharp
public static T? Deserialize<T>(string json, bool caseInsensitivePropertyNames=true);
```
#### Type parameters

<a name='Sagara.Core.Json.SystemTextJson.STJsonHelper.Deserialize_T_(string,bool).T'></a>

`T`
#### Parameters

<a name='Sagara.Core.Json.SystemTextJson.STJsonHelper.Deserialize_T_(string,bool).json'></a>

`json` [System.String](https://docs.microsoft.com/en-us/dotnet/api/System.String 'System.String')

The JSON string to parse.

<a name='Sagara.Core.Json.SystemTextJson.STJsonHelper.Deserialize_T_(string,bool).caseInsensitivePropertyNames'></a>

`caseInsensitivePropertyNames` [System.Boolean](https://docs.microsoft.com/en-us/dotnet/api/System.Boolean 'System.Boolean')

True to use case-insensitive property names; false to use  
            case-sensitive property names. Default is true.

#### Returns
[T](Sagara.Core.Json.SystemTextJson.STJsonHelper.md#Sagara.Core.Json.SystemTextJson.STJsonHelper.Deserialize_T_(string,bool).T 'Sagara.Core.Json.SystemTextJson.STJsonHelper.Deserialize<T>(string, bool).T')  
The deserialized object of type T.

<a name='Sagara.Core.Json.SystemTextJson.STJsonHelper.Serialize_TValue_(TValue,bool,bool)'></a>

## STJsonHelper.Serialize<TValue>(TValue, bool, bool) Method

Convert the value into a JSON string. Default is camelCase property names and minified output.

```csharp
public static string Serialize<TValue>(TValue value, bool camelCase=true, bool prettyPrint=false);
```
#### Type parameters

<a name='Sagara.Core.Json.SystemTextJson.STJsonHelper.Serialize_TValue_(TValue,bool,bool).TValue'></a>

`TValue`
#### Parameters

<a name='Sagara.Core.Json.SystemTextJson.STJsonHelper.Serialize_TValue_(TValue,bool,bool).value'></a>

`value` [TValue](Sagara.Core.Json.SystemTextJson.STJsonHelper.md#Sagara.Core.Json.SystemTextJson.STJsonHelper.Serialize_TValue_(TValue,bool,bool).TValue 'Sagara.Core.Json.SystemTextJson.STJsonHelper.Serialize<TValue>(TValue, bool, bool).TValue')

The value to convert to JSON.

<a name='Sagara.Core.Json.SystemTextJson.STJsonHelper.Serialize_TValue_(TValue,bool,bool).camelCase'></a>

`camelCase` [System.Boolean](https://docs.microsoft.com/en-us/dotnet/api/System.Boolean 'System.Boolean')

True to use camelCase property naming; false to use PascalCase. Default is true.

<a name='Sagara.Core.Json.SystemTextJson.STJsonHelper.Serialize_TValue_(TValue,bool,bool).prettyPrint'></a>

`prettyPrint` [System.Boolean](https://docs.microsoft.com/en-us/dotnet/api/System.Boolean 'System.Boolean')

True to pretty print the JSON; false to minify it. Default is false.

#### Returns
[System.String](https://docs.microsoft.com/en-us/dotnet/api/System.String 'System.String')  
The serialized JSON string.