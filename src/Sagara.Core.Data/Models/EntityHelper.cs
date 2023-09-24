namespace Sagara.Core.Data.Models;

/// <summary>
/// Methods to help with <see cref="Entity"/> objects.
/// </summary>
internal static class EntityHelper
{
    /// <summary>
    /// Common method to initialize the CreateUtc and UpdateUtc properties on classes that
    /// implement <see cref="IEntity"/>.
    /// </summary>
    /// <param name="entity">The instance to update.</param>
    public static void InitializeDates(IEntity entity)
    {
        var utcNow = DateTime.UtcNow;

        entity.CreateUtc = utcNow;
        entity.UpdateUtc = utcNow;
    }
}
