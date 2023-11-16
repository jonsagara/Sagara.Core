#### [Sagara.Core.Data](index.md 'index')
### [Sagara.Core.Data.Models](index.md#Sagara.Core.Data.Models 'Sagara.Core.Data.Models')

## EntityHelper Class

Methods to help with [Entity](Sagara.Core.Data.Models.Entity.md 'Sagara.Core.Data.Models.Entity') objects.

```csharp
public static class EntityHelper
```

Inheritance [System.Object](https://docs.microsoft.com/en-us/dotnet/api/System.Object 'System.Object') &#129106; EntityHelper
### Methods

<a name='Sagara.Core.Data.Models.EntityHelper.InitializeDates(Sagara.Core.Data.Models.IEntity)'></a>

## EntityHelper.InitializeDates(IEntity) Method

Common method to initialize the CreateUtc and UpdateUtc properties on classes that  
implement [IEntity](Sagara.Core.Data.Models.IEntity.md 'Sagara.Core.Data.Models.IEntity').

```csharp
public static void InitializeDates(Sagara.Core.Data.Models.IEntity entity);
```
#### Parameters

<a name='Sagara.Core.Data.Models.EntityHelper.InitializeDates(Sagara.Core.Data.Models.IEntity).entity'></a>

`entity` [IEntity](Sagara.Core.Data.Models.IEntity.md 'Sagara.Core.Data.Models.IEntity')

The instance to update.