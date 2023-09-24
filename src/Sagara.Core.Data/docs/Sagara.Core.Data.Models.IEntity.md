#### [Sagara.Core.Data](index.md 'index')
### [Sagara.Core.Data.Models](index.md#Sagara.Core.Data.Models 'Sagara.Core.Data.Models')

## IEntity Interface

Implemented by [Entity](Sagara.Core.Data.Models.Entity.md 'Sagara.Core.Data.Models.Entity'), from which all non-Identity entities derive, and also by  
any IdentityUser<T>-derived classes.

```csharp
public interface IEntity
```

Derived  
&#8627; [Entity](Sagara.Core.Data.Models.Entity.md 'Sagara.Core.Data.Models.Entity')
### Properties

<a name='Sagara.Core.Data.Models.IEntity.CreateUserId'></a>

## IEntity.CreateUserId Property

UserId of the person who created the entity.

```csharp
System.Guid CreateUserId { get; set; }
```

#### Property Value
[System.Guid](https://docs.microsoft.com/en-us/dotnet/api/System.Guid 'System.Guid')

<a name='Sagara.Core.Data.Models.IEntity.CreateUtc'></a>

## IEntity.CreateUtc Property

UTC date/time when the entity was created.

```csharp
System.DateTime CreateUtc { get; set; }
```

#### Property Value
[System.DateTime](https://docs.microsoft.com/en-us/dotnet/api/System.DateTime 'System.DateTime')

<a name='Sagara.Core.Data.Models.IEntity.Timestamp'></a>

## IEntity.Timestamp Property

SQL Server timestamp field, used for concurrency checking.

```csharp
byte[] Timestamp { get; set; }
```

#### Property Value
[System.Byte](https://docs.microsoft.com/en-us/dotnet/api/System.Byte 'System.Byte')[[]](https://docs.microsoft.com/en-us/dotnet/api/System.Array 'System.Array')

<a name='Sagara.Core.Data.Models.IEntity.UpdateUserId'></a>

## IEntity.UpdateUserId Property

UserId of the person who last updated the entity.

```csharp
System.Guid UpdateUserId { get; set; }
```

#### Property Value
[System.Guid](https://docs.microsoft.com/en-us/dotnet/api/System.Guid 'System.Guid')

<a name='Sagara.Core.Data.Models.IEntity.UpdateUtc'></a>

## IEntity.UpdateUtc Property

UTC date/time when the entity was last updated.

```csharp
System.DateTime UpdateUtc { get; set; }
```

#### Property Value
[System.DateTime](https://docs.microsoft.com/en-us/dotnet/api/System.DateTime 'System.DateTime')