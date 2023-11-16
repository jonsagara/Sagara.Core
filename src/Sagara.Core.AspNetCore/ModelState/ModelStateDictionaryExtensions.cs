using System.Net.Mime;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Sagara.Core.Json.SystemTextJson;
using Sagara.Core.Validation;

namespace Sagara.Core.AspNetCore.ModelState;

/// <summary>
/// Extensions to convert ModelStateDictionary errors into JSON responses.
/// </summary>
public static class ModelStateDictionaryExtensions
{
    /// <summary>
    /// Creates a 400 Bad Request ContentResult whose body is the JSON-serialized ModelStateDictionary.
    /// </summary>
    /// <param name="modelState">The ModelState dictionary.</param>
    public static ContentResult ToJsonErrorResult(this ModelStateDictionary modelState)
    {
        Check.NotNull(modelState);

        return CreateBadRequestJsonContentResult(modelState);
    }

    /// <summary>
    /// Creates a 400 Bad Request ContentResult whose body is the JSON-serialized ModelStateDictionary.
    /// </summary>
    /// <param name="modelState">The ModelState dictionary.</param>
    /// <param name="additionalErrors">A collection of property-specific error messages to add to model state.</param>
    public static ContentResult ToJsonErrorResult(this ModelStateDictionary modelState, IReadOnlyCollection<RequestError> additionalErrors)
    {
        Check.NotNull(modelState);
        Check.NotNull(additionalErrors);

        foreach (var additionalError in additionalErrors)
        {
            modelState.AddModelError(additionalError.PropertyName, additionalError.ErrorMessage);
        }

        return CreateBadRequestJsonContentResult(modelState);
    }

    /// <summary>
    /// Only keep the Errors property, and of the errors, only keep the ErrorMessage.
    /// </summary>
    public static Dictionary<string, SlimModelStateEntry> ToSlimModelStateDictionary(this ModelStateDictionary modelState)
    {
        Check.NotNull(modelState);

        return modelState
            .ToDictionary
            (
                kvp => kvp.Key,
                kvp =>
                {
                    var errors = kvp.Value!.Errors
                        .Select(e => new SlimError(ErrorMessage: e.ErrorMessage))
                        .ToArray();

                    return new SlimModelStateEntry(errors);
                }
            );
    }

    /// <summary>
    /// Add a collection of error messages to ModelState, each with a blank key.
    /// </summary>
    public static void AddModelErrors(this ModelStateDictionary modelState, IReadOnlyCollection<string> errors)
    {
        Check.NotNull(modelState);
        Check.NotNull(errors);

        foreach (var error in errors)
        {
            modelState.AddModelError(string.Empty, error);
        }
    }


    //
    // Private methods
    //

    private static ContentResult CreateBadRequestJsonContentResult(ModelStateDictionary modelState)
    {
        // Since the vast majority of forms on this site use Pascal casing to match variable names, serialize
        //   model state in Pascal case so that we can work with the client side variable names.
        var content = SystemTextJsonHelper.Serialize(modelState.ToSlimModelStateDictionary(), camelCase: false);

        return new ContentResult
        {
            Content = content,
            ContentType = MediaTypeNames.Application.Json,
            StatusCode = StatusCodes.Status400BadRequest,
        };
    }
}
