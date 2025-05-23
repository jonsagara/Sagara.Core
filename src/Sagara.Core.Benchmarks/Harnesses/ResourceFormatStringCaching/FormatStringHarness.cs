﻿using System.Collections.Concurrent;
using System.Globalization;
using System.Resources;
using System.Text;
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Jobs;
using Sagara.Core.Validation;
using SR = Sagara.Core.Resources.Strings;

namespace Sagara.Core.Benchmarks.Harnesses.ResourceFormatStringCaching;

[SimpleJob(RuntimeMoniker.Net90)]
[MemoryDiagnoser]
public class FormatStringHarness
{

    /*
     * Findings:
     * 
     * The cached version that uses struct keys is faster by about 50 ns, but uses about 50 bytes more of memory per
     * invocation. I don't think the speed savings warrant the increased memory use.
     * 
     * Don't even consider the version that uses a record as the cache key: it's faster, but uses way more memory per
     * invocation.

// * Summary *

BenchmarkDotNet v0.14.0, Windows 11 (10.0.26100.3476)
Intel Core i7-1065G7 CPU 1.30GHz, 1 CPU, 8 logical and 4 physical cores
.NET SDK 9.0.202
  [Host]   : .NET 8.0.14 (8.0.1425.11118), X64 RyuJIT AVX-512F+CD+BW+DQ+VL+VBMI
  .NET 9.0 : .NET 9.0.3 (9.0.325.11113), X64 RyuJIT AVX-512F+CD+BW+DQ+VL+VBMI

Job=.NET 9.0  Runtime=.NET 9.0

| Method                                               | Mean     | Error   | StdDev  | Ratio | RatioSD | Gen0   | Allocated | Alloc Ratio |
|----------------------------------------------------- |---------:|--------:|--------:|------:|--------:|-------:|----------:|------------:|
| FormatRawResourceString                              | 367.4 ns | 5.20 ns | 4.87 ns |  1.00 |    0.02 | 0.0534 |     224 B |        1.00 |
| FormatCachedParsedResourceString                     | 337.1 ns | 5.41 ns | 5.06 ns |  0.92 |    0.02 | 0.0744 |     312 B |        1.39 |
| FormatCachedParsedResourceStringStructKey            | 316.1 ns | 6.16 ns | 7.33 ns |  0.86 |    0.02 | 0.0648 |     272 B |        1.21 |
| FormatCachedParsedResourceStringStructKeyTryGetValue | 309.1 ns | 3.72 ns | 3.48 ns |  0.84 |    0.01 | 0.0648 |     272 B |        1.21 |

    */

    private const string _propertyName = "Cost";
    private const double _value = 0.99;
    private const double _threshold = 1.99;

    [Benchmark(Baseline = true)]
    public string FormatRawResourceString()
    {
        return string.Format(CultureInfo.CurrentCulture, SR.GreaterThanOrEqual, _propertyName, _threshold, _value);
    }

    [Benchmark]
    public string FormatCachedParsedResourceString()
    {
        return SR.ResourceManager.Format(resourceKey: nameof(SR.GreaterThanOrEqual), _propertyName, _threshold, _value);
    }

    [Benchmark]
    public string FormatCachedParsedResourceStringStructKey()
    {
        return SR.ResourceManager.FormatStructKey(resourceKey: nameof(SR.GreaterThanOrEqual), _propertyName, _threshold, _value);
    }

    [Benchmark]
    public string FormatCachedParsedResourceStringStructKeyTryGetValue()
    {
        return SR.ResourceManager.FormatStructKeyTryGetValue(resourceKey: nameof(SR.GreaterThanOrEqual), _propertyName, _threshold, _value);
    }
}

public static class ResourceFormatStrings
{
    private static readonly ConcurrentDictionary<CompositeFormatCacheKey, CompositeFormat> _localizedFormatStrings = new();
    private static readonly ConcurrentDictionary<CompositeFormatCacheKeyStruct, CompositeFormat> _localizedFormatStringsStructKey = new();

    public static string Format(this ResourceManager resourceManager, string resourceKey, params object[] args)
    {
        ArgumentNullException.ThrowIfNull(resourceManager);
        ArgumentException.ThrowIfNullOrWhiteSpace(resourceKey);
        ArgumentNullException.ThrowIfNull(args);

        var cacheKey = new CompositeFormatCacheKey(
            ResourceKey: resourceKey,
            CurrentCultureName: CultureInfo.CurrentCulture.Name,
            CurrentUICultureName: CultureInfo.CurrentUICulture.Name
            );

        var compositeFormat = _localizedFormatStrings.GetOrAdd(
            key: cacheKey,
            valueFactory: (key, resMan) =>
            {
                var localizedFormatString = resMan.GetString(key.ResourceKey, CultureInfo.CurrentUICulture);
                if (string.IsNullOrWhiteSpace(localizedFormatString))
                {
                    throw new InvalidOperationException($"Resource '{key.ResourceKey}' not found in ResourceManager '{resMan.BaseName}' for CurrentCulture '{key.CurrentCultureName}' and CurrentUICulture '{key.CurrentUICultureName}'.");
                }

                return CompositeFormat.Parse(localizedFormatString);
            },
            factoryArgument: resourceManager
            );

        return string.Format(CultureInfo.CurrentCulture, compositeFormat, args);
    }

    public static string FormatStructKey(this ResourceManager resourceManager, string resourceKey, params object[] args)
    {
        ArgumentNullException.ThrowIfNull(resourceManager);
        ArgumentException.ThrowIfNullOrWhiteSpace(resourceKey);
        ArgumentNullException.ThrowIfNull(args);

        var cacheKey = new CompositeFormatCacheKeyStruct(
            ResourceKey: resourceKey,
            CurrentCultureName: CultureInfo.CurrentCulture.Name,
            CurrentUICultureName: CultureInfo.CurrentUICulture.Name
            );

        var compositeFormat = _localizedFormatStringsStructKey.GetOrAdd(
            key: cacheKey,
            valueFactory: (key, resMan) =>
            {
                var localizedFormatString = resMan.GetString(key.ResourceKey, CultureInfo.CurrentUICulture);
                if (string.IsNullOrWhiteSpace(localizedFormatString))
                {
                    throw new InvalidOperationException($"Resource '{key.ResourceKey}' not found in ResourceManager '{resMan.BaseName}' for CurrentCulture '{key.CurrentCultureName}' and CurrentUICulture '{key.CurrentUICultureName}'.");
                }

                return CompositeFormat.Parse(localizedFormatString);
            },
            factoryArgument: resourceManager
            );

        return string.Format(CultureInfo.CurrentCulture, compositeFormat, args);
    }

    public static string FormatStructKeyTryGetValue(this ResourceManager resourceManager, string resourceKey, params object[] args)
    {
        ArgumentNullException.ThrowIfNull(resourceManager);
        ArgumentException.ThrowIfNullOrWhiteSpace(resourceKey);
        ArgumentNullException.ThrowIfNull(args);

        var cacheKey = new CompositeFormatCacheKeyStruct(
            ResourceKey: resourceKey,
            CurrentCultureName: CultureInfo.CurrentCulture.Name,
            CurrentUICultureName: CultureInfo.CurrentUICulture.Name
            );


        CompositeFormat? compositeFormat = null;

        if (!_localizedFormatStringsStructKey.TryGetValue(key: cacheKey, out compositeFormat))
        {
            var localizedFormatString = resourceManager.GetString(resourceKey, CultureInfo.CurrentUICulture);
            if (string.IsNullOrWhiteSpace(localizedFormatString))
            {
                throw new InvalidOperationException($"Resource '{cacheKey.ResourceKey}' not found in ResourceManager '{resourceManager.BaseName}' for CurrentCulture '{cacheKey.CurrentCultureName}' and CurrentUICulture '{cacheKey.CurrentUICultureName}'.");
            }

            compositeFormat = CompositeFormat.Parse(localizedFormatString);
            _localizedFormatStringsStructKey.TryAdd(cacheKey, compositeFormat);
        }

        return string.Format(CultureInfo.CurrentCulture, compositeFormat, args);
    }


    //
    // Types
    //

    private record CompositeFormatCacheKey(
        string ResourceKey,
        string CurrentCultureName,
        string CurrentUICultureName
        );

    private readonly record struct CompositeFormatCacheKeyStruct(
        string ResourceKey,
        string CurrentCultureName,
        string CurrentUICultureName
        );
}
