#### [Sagara.Core](index.md 'index')
### [Sagara.Core.Time](index.md#Sagara.Core.Time 'Sagara.Core.Time')

## NodaTimeHelper Class

Wrappers around NodaTime helpers to convert between time zones.

```csharp
public static class NodaTimeHelper
```

Inheritance [System.Object](https://docs.microsoft.com/en-us/dotnet/api/System.Object 'System.Object') &#129106; NodaTimeHelper
### Methods

<a name='Sagara.Core.Time.NodaTimeHelper.ToLocal(thisSystem.DateTime,string)'></a>

## NodaTimeHelper.ToLocal(this DateTime, string) Method

Convert the UTC date/time into the equivalent date/time of the given time zone specified by the   
IANA time zone Id.

```csharp
public static System.DateTimeOffset ToLocal(this System.DateTime utc, string ianaTimeZoneId);
```
#### Parameters

<a name='Sagara.Core.Time.NodaTimeHelper.ToLocal(thisSystem.DateTime,string).utc'></a>

`utc` [System.DateTime](https://docs.microsoft.com/en-us/dotnet/api/System.DateTime 'System.DateTime')

<a name='Sagara.Core.Time.NodaTimeHelper.ToLocal(thisSystem.DateTime,string).ianaTimeZoneId'></a>

`ianaTimeZoneId` [System.String](https://docs.microsoft.com/en-us/dotnet/api/System.String 'System.String')

#### Returns
[System.DateTimeOffset](https://docs.microsoft.com/en-us/dotnet/api/System.DateTimeOffset 'System.DateTimeOffset')

<a name='Sagara.Core.Time.NodaTimeHelper.ToUtc(thisSystem.DateTime,string)'></a>

## NodaTimeHelper.ToUtc(this DateTime, string) Method

Convert the local date/time (local to the time zone specified by the IANA time zone Id) into the  
equivalent UTC date/time.

```csharp
public static System.DateTime ToUtc(this System.DateTime local, string ianaTimeZoneId);
```
#### Parameters

<a name='Sagara.Core.Time.NodaTimeHelper.ToUtc(thisSystem.DateTime,string).local'></a>

`local` [System.DateTime](https://docs.microsoft.com/en-us/dotnet/api/System.DateTime 'System.DateTime')

<a name='Sagara.Core.Time.NodaTimeHelper.ToUtc(thisSystem.DateTime,string).ianaTimeZoneId'></a>

`ianaTimeZoneId` [System.String](https://docs.microsoft.com/en-us/dotnet/api/System.String 'System.String')

#### Returns
[System.DateTime](https://docs.microsoft.com/en-us/dotnet/api/System.DateTime 'System.DateTime')