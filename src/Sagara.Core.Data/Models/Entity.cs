using System.ComponentModel.DataAnnotations;

namespace Sagara.Core.Data.Models;

/// <summary>
/// <para>Abstract base class for all EF Core entities. It contains common audit properties and the 
/// timestamp property.</para>
/// <para>Derive from this class and add an Id property with the desired Id type.</para>
/// </summary>
// How else am I supposed to initialize Id? How else am I supposed to allow EF Core to create instances?
#pragma warning disable CA1012 // Abstract types should not have public constructors
public abstract class Entity : IEntity
#pragma warning restore CA1012 // Abstract types should not have public constructors
{
    /// <inheritdoc />
    public DateTime CreateUtc { get; set; }

    /// <inheritdoc />
    public Guid CreateUserId { get; set; }

    /// <inheritdoc />
    public DateTime UpdateUtc { get; set; }

    /// <inheritdoc />
    public Guid UpdateUserId { get; set; }

    /// <inheritdoc />
    [Timestamp]
    // Required by EF Core for SQL Server Timestamp functionality.
#pragma warning disable CA1819 // Properties should not return arrays
    public byte[]? Timestamp { get; set; }
#pragma warning restore CA1819 // Properties should not return arrays

    /// <summary>
    /// Default .ctor. Initializes the Id and sets <see cref="CreateUtc"/> and <see cref="UpdateUtc"/>
    /// to the current UTC date/time.
    /// </summary>
    public Entity()
    {
        EntityHelper.InitializeDates(this);
    }
}
