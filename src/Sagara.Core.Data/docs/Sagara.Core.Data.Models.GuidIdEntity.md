#### [Sagara.Core.Data](index.md 'index')
### [Sagara.Core.Data.Models](index.md#Sagara.Core.Data.Models 'Sagara.Core.Data.Models')

## GuidIdEntity Class

Abstract base class for all EF Core entities. It contains common audit properties and the   
timestamp property.

```csharp
public abstract class GuidIdEntity : Sagara.Core.Data.Models.Entity
```

Inheritance [System.Object](https://docs.microsoft.com/en-us/dotnet/api/System.Object 'System.Object') &#129106; [Entity](Sagara.Core.Data.Models.Entity.md 'Sagara.Core.Data.Models.Entity') &#129106; GuidIdEntity
### Constructors

<a name='Sagara.Core.Data.Models.GuidIdEntity.GuidIdEntity()'></a>

## GuidIdEntity() Constructor

Default .ctor. Initializes the Id and sets [CreateUtc](Sagara.Core.Data.Models.Entity.md#Sagara.Core.Data.Models.Entity.CreateUtc 'Sagara.Core.Data.Models.Entity.CreateUtc') and [UpdateUtc](Sagara.Core.Data.Models.Entity.md#Sagara.Core.Data.Models.Entity.UpdateUtc 'Sagara.Core.Data.Models.Entity.UpdateUtc')  
to the current UTC date/time.

```csharp
public GuidIdEntity();
```
### Properties

<a name='Sagara.Core.Data.Models.GuidIdEntity.Id'></a>

## GuidIdEntity.Id Property

The Entity's unique id (primary key). Must be assigned by the client.

```csharp
public System.Guid Id { get; set; }
```

#### Property Value
[System.Guid](https://docs.microsoft.com/en-us/dotnet/api/System.Guid 'System.Guid')