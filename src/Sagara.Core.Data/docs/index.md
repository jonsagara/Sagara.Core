#### [Sagara.Core.Data](index.md 'index')

## Sagara.Core.Data Assembly
### Namespaces

<a name='Sagara.Core.Data.Models'></a>

## Sagara.Core.Data.Models Namespace
- **[Entity](Sagara.Core.Data.Models.Entity.md 'Sagara.Core.Data.Models.Entity')** `Class`   
    
  Abstract base class for all EF Core entities. It contains common audit properties and the   
              timestamp property.  
    
  Derive from this class and add an Id property with the desired Id type.
  - **[Entity()](Sagara.Core.Data.Models.Entity.md#Sagara.Core.Data.Models.Entity.Entity() 'Sagara.Core.Data.Models.Entity.Entity()')** `Constructor` Default .ctor. Initializes the Id and sets [CreateUtc](Sagara.Core.Data.Models.Entity.md#Sagara.Core.Data.Models.Entity.CreateUtc 'Sagara.Core.Data.Models.Entity.CreateUtc') and [UpdateUtc](Sagara.Core.Data.Models.Entity.md#Sagara.Core.Data.Models.Entity.UpdateUtc 'Sagara.Core.Data.Models.Entity.UpdateUtc')  
    to the current UTC date/time.
  - **[CreateUserId](Sagara.Core.Data.Models.Entity.md#Sagara.Core.Data.Models.Entity.CreateUserId 'Sagara.Core.Data.Models.Entity.CreateUserId')** `Property` UserId of the person who created the entity.
  - **[CreateUtc](Sagara.Core.Data.Models.Entity.md#Sagara.Core.Data.Models.Entity.CreateUtc 'Sagara.Core.Data.Models.Entity.CreateUtc')** `Property` UTC date/time when the entity was created.
  - **[Timestamp](Sagara.Core.Data.Models.Entity.md#Sagara.Core.Data.Models.Entity.Timestamp 'Sagara.Core.Data.Models.Entity.Timestamp')** `Property` SQL Server timestamp field, used for concurrency checking.
  - **[UpdateUserId](Sagara.Core.Data.Models.Entity.md#Sagara.Core.Data.Models.Entity.UpdateUserId 'Sagara.Core.Data.Models.Entity.UpdateUserId')** `Property` UserId of the person who last updated the entity.
  - **[UpdateUtc](Sagara.Core.Data.Models.Entity.md#Sagara.Core.Data.Models.Entity.UpdateUtc 'Sagara.Core.Data.Models.Entity.UpdateUtc')** `Property` UTC date/time when the entity was last updated.
- **[EntityHelper](Sagara.Core.Data.Models.EntityHelper.md 'Sagara.Core.Data.Models.EntityHelper')** `Class` Methods to help with [Entity](Sagara.Core.Data.Models.Entity.md 'Sagara.Core.Data.Models.Entity') objects.
  - **[InitializeDates(IEntity)](Sagara.Core.Data.Models.EntityHelper.md#Sagara.Core.Data.Models.EntityHelper.InitializeDates(Sagara.Core.Data.Models.IEntity) 'Sagara.Core.Data.Models.EntityHelper.InitializeDates(Sagara.Core.Data.Models.IEntity)')** `Method` Common method to initialize the CreateUtc and UpdateUtc properties on classes that  
    implement [IEntity](Sagara.Core.Data.Models.IEntity.md 'Sagara.Core.Data.Models.IEntity').
- **[GuidIdEntity](Sagara.Core.Data.Models.GuidIdEntity.md 'Sagara.Core.Data.Models.GuidIdEntity')** `Class` Abstract base class for all EF Core entities. It contains common audit properties and the   
  timestamp property.
  - **[GuidIdEntity()](Sagara.Core.Data.Models.GuidIdEntity.md#Sagara.Core.Data.Models.GuidIdEntity.GuidIdEntity() 'Sagara.Core.Data.Models.GuidIdEntity.GuidIdEntity()')** `Constructor` Default .ctor. Initializes the Id and sets [CreateUtc](Sagara.Core.Data.Models.Entity.md#Sagara.Core.Data.Models.Entity.CreateUtc 'Sagara.Core.Data.Models.Entity.CreateUtc') and [UpdateUtc](Sagara.Core.Data.Models.Entity.md#Sagara.Core.Data.Models.Entity.UpdateUtc 'Sagara.Core.Data.Models.Entity.UpdateUtc')  
    to the current UTC date/time.
  - **[Id](Sagara.Core.Data.Models.GuidIdEntity.md#Sagara.Core.Data.Models.GuidIdEntity.Id 'Sagara.Core.Data.Models.GuidIdEntity.Id')** `Property` The Entity's unique id (primary key). Must be assigned by the client.
- **[IEntity](Sagara.Core.Data.Models.IEntity.md 'Sagara.Core.Data.Models.IEntity')** `Interface` Implemented by [Entity](Sagara.Core.Data.Models.Entity.md 'Sagara.Core.Data.Models.Entity'), from which all non-Identity entities derive, and also by  
  any IdentityUser<T>-derived classes.
  - **[CreateUserId](Sagara.Core.Data.Models.IEntity.md#Sagara.Core.Data.Models.IEntity.CreateUserId 'Sagara.Core.Data.Models.IEntity.CreateUserId')** `Property` UserId of the person who created the entity.
  - **[CreateUtc](Sagara.Core.Data.Models.IEntity.md#Sagara.Core.Data.Models.IEntity.CreateUtc 'Sagara.Core.Data.Models.IEntity.CreateUtc')** `Property` UTC date/time when the entity was created.
  - **[Timestamp](Sagara.Core.Data.Models.IEntity.md#Sagara.Core.Data.Models.IEntity.Timestamp 'Sagara.Core.Data.Models.IEntity.Timestamp')** `Property` SQL Server timestamp field, used for concurrency checking.
  - **[UpdateUserId](Sagara.Core.Data.Models.IEntity.md#Sagara.Core.Data.Models.IEntity.UpdateUserId 'Sagara.Core.Data.Models.IEntity.UpdateUserId')** `Property` UserId of the person who last updated the entity.
  - **[UpdateUtc](Sagara.Core.Data.Models.IEntity.md#Sagara.Core.Data.Models.IEntity.UpdateUtc 'Sagara.Core.Data.Models.IEntity.UpdateUtc')** `Property` UTC date/time when the entity was last updated.