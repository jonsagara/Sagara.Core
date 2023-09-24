namespace Sagara.Core.AspNetCore.ModelState;

/// <summary>
/// For sending model state errors back to the browser without all the extra, unnecessary fluff.
/// </summary>
public record SlimModelStateEntry
{
    /// <summary>
    /// The collection of model state errors for the request.
    /// </summary>
    public IReadOnlyCollection<SlimError> Errors { get; init; }

    /// <summary>
    /// .ctor
    /// </summary>
    /// <param name="errors"></param>
    public SlimModelStateEntry(IReadOnlyCollection<SlimError> errors)
    {
        Check.NotNull(errors);

        Errors = errors;
    }
}

/// <summary>
/// Used to reduce the content of a model state dictionary that's sent back to the browser.
/// </summary>
/// <param name="ErrorMessage">The error message for the request.</param>
public readonly record struct SlimError(string ErrorMessage);
