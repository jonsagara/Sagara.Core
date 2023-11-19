namespace Sagara.Core.Data.Models;

/// <summary>
/// Implemented by <see cref="Entity"/>, from which all non-Identity entities derive, and also by
/// any IdentityUser&lt;T&gt;-derived classes.
/// </summary>
public interface IEntity
{
    // NOTE: there is no Id property here because, e.g., ASP.NET Core Identity models have their
    //   own Id property.

    /// <summary>
    /// UTC date/time when the entity was created.
    /// </summary>
    DateTime CreateUtc { get; set; }

    /// <summary>
    /// UserId of the person who created the entity.
    /// </summary>
    Guid CreateUserId { get; set; }

    /// <summary>
    /// UTC date/time when the entity was last updated.
    /// </summary>
    DateTime UpdateUtc { get; set; }

    /// <summary>
    /// UserId of the person who last updated the entity.
    /// </summary>
    Guid UpdateUserId { get; set; }

    /// <summary>
    /// SQL Server timestamp field, used for concurrency checking.
    /// </summary>
    // Justification: Required by EF Core for the SQL Server timestamp to function.
#pragma warning disable CA1819 // Properties should not return arrays
    byte[] Timestamp { get; set; }
#pragma warning restore CA1819 // Properties should not return arrays
}

