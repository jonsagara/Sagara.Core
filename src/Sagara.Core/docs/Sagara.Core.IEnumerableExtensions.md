#### [Sagara.Core](index.md 'index')
### [Sagara.Core](index.md#Sagara.Core 'Sagara.Core')

## IEnumerableExtensions Class

```csharp
public static class IEnumerableExtensions
```

Inheritance [System.Object](https://docs.microsoft.com/en-us/dotnet/api/System.Object 'System.Object') &#129106; IEnumerableExtensions
### Methods

<a name='Sagara.Core.IEnumerableExtensions.Index_TSource_(thisSystem.Collections.Generic.IEnumerable_TSource_)'></a>

## IEnumerableExtensions.Index<TSource>(this IEnumerable<TSource>) Method

Returns an enumerable that incorporates the element's index into a tuple.

```csharp
public static System.Collections.Generic.IEnumerable<(int Index,TSource Item)> Index<TSource>(this System.Collections.Generic.IEnumerable<TSource> source);
```
#### Type parameters

<a name='Sagara.Core.IEnumerableExtensions.Index_TSource_(thisSystem.Collections.Generic.IEnumerable_TSource_).TSource'></a>

`TSource`

The type of the elements of [source](Sagara.Core.IEnumerableExtensions.md#Sagara.Core.IEnumerableExtensions.Index_TSource_(thisSystem.Collections.Generic.IEnumerable_TSource_).source 'Sagara.Core.IEnumerableExtensions.Index<TSource>(this System.Collections.Generic.IEnumerable<TSource>).source').
#### Parameters

<a name='Sagara.Core.IEnumerableExtensions.Index_TSource_(thisSystem.Collections.Generic.IEnumerable_TSource_).source'></a>

`source` [System.Collections.Generic.IEnumerable&lt;](https://docs.microsoft.com/en-us/dotnet/api/System.Collections.Generic.IEnumerable-1 'System.Collections.Generic.IEnumerable`1')[TSource](Sagara.Core.IEnumerableExtensions.md#Sagara.Core.IEnumerableExtensions.Index_TSource_(thisSystem.Collections.Generic.IEnumerable_TSource_).TSource 'Sagara.Core.IEnumerableExtensions.Index<TSource>(this System.Collections.Generic.IEnumerable<TSource>).TSource')[&gt;](https://docs.microsoft.com/en-us/dotnet/api/System.Collections.Generic.IEnumerable-1 'System.Collections.Generic.IEnumerable`1')

The source enumerable providing the elements.

#### Returns
[System.Collections.Generic.IEnumerable&lt;](https://docs.microsoft.com/en-us/dotnet/api/System.Collections.Generic.IEnumerable-1 'System.Collections.Generic.IEnumerable`1')[&lt;](https://docs.microsoft.com/en-us/dotnet/api/System.ValueTuple 'System.ValueTuple')[System.Int32](https://docs.microsoft.com/en-us/dotnet/api/System.Int32 'System.Int32')[,](https://docs.microsoft.com/en-us/dotnet/api/System.ValueTuple 'System.ValueTuple')[TSource](Sagara.Core.IEnumerableExtensions.md#Sagara.Core.IEnumerableExtensions.Index_TSource_(thisSystem.Collections.Generic.IEnumerable_TSource_).TSource 'Sagara.Core.IEnumerableExtensions.Index<TSource>(this System.Collections.Generic.IEnumerable<TSource>).TSource')[&gt;](https://docs.microsoft.com/en-us/dotnet/api/System.ValueTuple 'System.ValueTuple')[&gt;](https://docs.microsoft.com/en-us/dotnet/api/System.Collections.Generic.IEnumerable-1 'System.Collections.Generic.IEnumerable`1')

### Remarks
NOTE: This will be built into .NET 9 and above, so this method is for .NET 8.