# Sagara.Core — Agent Instructions

A collection of NuGet utility libraries targeting **net8.0**, **net9.0**, and **net10.0** simultaneously.
See [README.md](README.md) for a high-level description.

## Build & Test

```sh
dotnet build                # build all projects
dotnet test                 # run xunit v3 tests (Sagara.Core.Tests)
dotnet pack                 # produce .nupkg / .snupkg artifacts
make_docs.bat               # regenerate XML-doc → Markdown (requires Debug build first)
```

All projects multi-target; avoid singling out a TFM unless fixing a framework-specific issue.

## Solution Layout

| Project | Purpose |
|---------|---------|
| [Sagara.Core](src/Sagara.Core/) | Core utilities — guards, enums, extensions, I/O, JSON, time, validation |
| [Sagara.Core.AspNetCore](src/Sagara.Core.AspNetCore/) | ASP.NET Core filters, ModelState helpers, exception handling |
| [Sagara.Core.Caching](src/Sagara.Core.Caching/) | Redis cache abstractions (StackExchange.Redis) |
| [Sagara.Core.Data](src/Sagara.Core.Data/) | EF Core data-access patterns |
| [Sagara.Core.Logging.Serilog](src/Sagara.Core.Logging.Serilog/) | Serilog bootstrapping & enrichment |
| [Sagara.Core.Tests](src/Sagara.Core.Tests/) | xunit v3 unit tests for Sagara.Core |
| [Sagara.Core.Benchmarks](src/Sagara.Core.Benchmarks/) | BenchmarkDotNet harnesses (net10.0 only) |

## Language & SDK Conventions

Configured globally in [Directory.Build.props](Directory.Build.props):

- **SDK**: 10.0.203 (`global.json`); **C# 14**; **Implicit usings** enabled.
- **Nullable reference types**: enabled — always annotate parameters, returns, and fields.
- **Analysis**: `AllEnabledByDefault`. **CA1307, CA1309, CA1310** are treated as errors — always pass an explicit `StringComparison` or `CultureInfo` argument.
- **Warnings as errors**: keep the build clean; don't suppress without a comment explaining why.
- **SourceLink + deterministic builds** are configured — do not change `EmbedUntrackedSources` or symbol settings.

## Key Patterns

### Guard Clauses — `Check`
[`Check.cs`](src/Sagara.Core/Check.cs) provides static guard methods. Prefer these over raw `ArgumentNullException`/`ArgumentException`:

```csharp
Check.ThrowIfNull(value);
Check.ThrowIfNullOrWhiteSpace(name);
Check.ThrowIfEmptyGuid(id);
```

`[CallerArgumentExpression]` is used automatically — do **not** pass the argument name manually.

### Validation Pattern
[`Validation/`](src/Sagara.Core/Validation/) uses accumulator-style validation:

```csharp
var errors = new List<RequestError>();
ValidationHelper.CheckRequiredField(errors, new ValidatableProperty<string>(value, "Display Name"));
if (errors.Count > 0) { /* fail */ }
```

`RequestError` is a `record struct (string PropertyName, string ErrorMessage)`.

### Enum Utilities
- [`EnumTraits<TEnum>`](src/Sagara.Core/Enums/EnumTraits.cs) — typed enum helper (name lookup, parse, validate).
- `[InvalidEnumValue]` attribute — marks sentinel values (e.g., `Unknown = 0`) that must be rejected at boundaries.

### Sequential GUIDs
[`SequentialGuid.GenerateComb()`](src/Sagara.Core/SequentialGuid.cs) is `[Obsolete]` on .NET 9+; prefer `Guid.CreateVersion7()` there.

### Time
Use **NodaTime** types (`Instant`, `LocalDate`, etc.) via [`NodaTimeHelper`](src/Sagara.Core/Time/NodaTimeHelper.cs). Avoid `DateTime`/`DateTimeOffset` in new code unless required by external APIs.

### Redis Caching
[`RedisCache`](src/Sagara.Core.Caching/RedisCache.cs) and [`RedisAdminCache`](src/Sagara.Core.Caching/RedisAdminCache.cs) wrap StackExchange.Redis. Logging is via the injected `ILogger`; see [`RedisCacheLogger`](src/Sagara.Core.Caching/RedisCacheLogger.cs) for log-level definitions.

## Documentation

API docs live under `src/<Project>/docs/` and are generated from XML doc-comments via [DefaultDocumentation](DefaultDocumentation.json).  
Run `make_docs.bat` after a Debug build to regenerate them. Do not hand-edit the generated `.md` files.

## NuGet Feed

Only `nuget.org` is configured ([nuget.config](nuget.config)). Do not add custom feeds without updating that file.
