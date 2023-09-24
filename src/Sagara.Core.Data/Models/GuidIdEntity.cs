using System.ComponentModel.DataAnnotations.Schema;

namespace Sagara.Core.Data.Models;

/// <summary>
/// Abstract base class for all EF Core entities. It contains common audit properties and the 
/// timestamp property.
/// </summary>
// How else am I supposed to initialize Id? How else am I supposed to allow EF Core to create instances?
#pragma warning disable CA1012 // Abstract types should not have public constructors
public abstract class GuidIdEntity : Entity
#pragma warning restore CA1012 // Abstract types should not have public constructors
{
    /// <summary>
    /// The Entity's unique id (primary key). Must be assigned by the client.
    /// </summary>
    [DatabaseGenerated(DatabaseGeneratedOption.None)]
    public Guid Id { get; set; }

    /// <summary>
    /// Default .ctor. Initializes the Id and sets <see cref="Entity.CreateUtc"/> and <see cref="Entity.UpdateUtc"/>
    /// to the current UTC date/time.
    /// </summary>
    public GuidIdEntity()
        : base()
    {
        Id = SequentialGuid.GenerateComb();
    }
}
