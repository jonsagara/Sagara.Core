#### [Sagara.Core.Data](index.md 'index')
### [Sagara.Core.Data.Models](index.md#Sagara.Core.Data.Models 'Sagara.Core.Data.Models')

## Entity Class

  
Abstract base class for all EF Core entities. It contains common audit properties and the   
            timestamp property.  
  
Derive from this class and add an Id property with the desired Id type.

```csharp
public abstract class Entity :
Sagara.Core.Data.Models.IEntity
```

Inheritance [System.Object](https://docs.microsoft.com/en-us/dotnet/api/System.Object 'System.Object') &#129106; Entity

Derived  
&#8627; [GuidIdEntity](Sagara.Core.Data.Models.GuidIdEntity.md 'Sagara.Core.Data.Models.GuidIdEntity')

Implements [IEntity](Sagara.Core.Data.Models.IEntity.md 'Sagara.Core.Data.Models.IEntity')
### Constructors

<a name='Sagara.Core.Data.Models.Entity.Entity()'></a>

## Entity() Constructor

Default .ctor. Initializes the Id and sets [CreateUtc](Sagara.Core.Data.Models.Entity.md#Sagara.Core.Data.Models.Entity.CreateUtc 'Sagara.Core.Data.Models.Entity.CreateUtc') and [UpdateUtc](Sagara.Core.Data.Models.Entity.md#Sagara.Core.Data.Models.Entity.UpdateUtc 'Sagara.Core.Data.Models.Entity.UpdateUtc')  
to the current UTC date/time.

```csharp
public Entity();
```
### Properties

<a name='Sagara.Core.Data.Models.Entity.CreateUserId'></a>

## Entity.CreateUserId Property

UserId of the person who created the entity.

```csharp
public System.Guid CreateUserId { get; set; }
```

Implements [CreateUserId](Sagara.Core.Data.Models.IEntity.md#Sagara.Core.Data.Models.IEntity.CreateUserId 'Sagara.Core.Data.Models.IEntity.CreateUserId')

#### Property Value
[System.Guid](https://docs.microsoft.com/en-us/dotnet/api/System.Guid 'System.Guid')

<a name='Sagara.Core.Data.Models.Entity.CreateUtc'></a>

## Entity.CreateUtc Property

UTC date/time when the entity was created.

```csharp
public System.DateTime CreateUtc { get; set; }
```

Implements [CreateUtc](Sagara.Core.Data.Models.IEntity.md#Sagara.Core.Data.Models.IEntity.CreateUtc 'Sagara.Core.Data.Models.IEntity.CreateUtc')

#### Property Value
[System.DateTime](https://docs.microsoft.com/en-us/dotnet/api/System.DateTime 'System.DateTime')

<a name='Sagara.Core.Data.Models.Entity.Timestamp'></a>

## Entity.Timestamp Property

SQL Server timestamp field, used for concurrency checking.

```csharp
public byte[] Timestamp { get; set; }
```

Implements [Timestamp](Sagara.Core.Data.Models.IEntity.md#Sagara.Core.Data.Models.IEntity.Timestamp 'Sagara.Core.Data.Models.IEntity.Timestamp')

#### Property Value
[System.Byte](https://docs.microsoft.com/en-us/dotnet/api/System.Byte 'System.Byte')[[]](https://docs.microsoft.com/en-us/dotnet/api/System.Array 'System.Array')

<a name='Sagara.Core.Data.Models.Entity.UpdateUserId'></a>

## Entity.UpdateUserId Property

UserId of the person who last updated the entity.

```csharp
public System.Guid UpdateUserId { get; set; }
```

Implements [UpdateUserId](Sagara.Core.Data.Models.IEntity.md#Sagara.Core.Data.Models.IEntity.UpdateUserId 'Sagara.Core.Data.Models.IEntity.UpdateUserId')

#### Property Value
[System.Guid](https://docs.microsoft.com/en-us/dotnet/api/System.Guid 'System.Guid')

<a name='Sagara.Core.Data.Models.Entity.UpdateUtc'></a>

## Entity.UpdateUtc Property

UTC date/time when the entity was last updated.

```csharp
public System.DateTime UpdateUtc { get; set; }
```

Implements [UpdateUtc](Sagara.Core.Data.Models.IEntity.md#Sagara.Core.Data.Models.IEntity.UpdateUtc 'Sagara.Core.Data.Models.IEntity.UpdateUtc')

#### Property Value
[System.DateTime](https://docs.microsoft.com/en-us/dotnet/api/System.DateTime 'System.DateTime')