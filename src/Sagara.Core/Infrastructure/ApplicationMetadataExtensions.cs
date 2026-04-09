using Microsoft.Extensions.DependencyInjection;

namespace Sagara.Core.Infrastructure;

public static class ApplicationMetadataExtensions
{
    /// <summary>
    /// Adds an <see cref="ApplicationMetadata"/> singleton to the specified <see cref="IServiceCollection"/>.
    /// </summary>
    /// <remarks>
    /// When using this in a non-<c>Microsoft.NET.Sdk.Web</c> project, you must explicitly tell <c>dotnet publish</c> to copy the <c>.buildinfo.json</c> 
    /// file to the output directory, or else you won't get the GitHub Actions build information and git branch.
    /// <code>
    /// <![CDATA[
    /// <ItemGroup>
    ///     <Content Include=".buildinfo.json" Condition="Exists('.buildinfo.json')">
    ///         <CopyToOutputDirectory>Always</CopyToOutputDirectory>
    ///     </Content>
    /// </ItemGroup>
    /// ]]>
    /// </code>
    /// </remarks>
    /// <typeparam name="TApplicationAssemblyType"></typeparam>
    /// <param name="services">The <see cref="IServiceCollection"/> to add the <see cref="ApplicationMetadata"/> to.</param>
    /// <param name="displayTimeZone">The IANA time zone identifier to use for the <see cref="ApplicationMetadata.BuiltLocal"/> property.
    /// If null, defaults to "America/Los_Angeles".</param>
    /// <returns>The updated <see cref="IServiceCollection" />.</returns>
    public static IServiceCollection AddApplicationMetadata<TApplicationAssemblyType>(this IServiceCollection services, string? displayTimeZone = null)
        => services.AddSingleton(sp => new ApplicationMetadata(typeof(TApplicationAssemblyType), displayTimeZone));
}
