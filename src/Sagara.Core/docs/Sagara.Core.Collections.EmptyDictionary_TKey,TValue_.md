#### [Sagara.Core](index.md 'index')
### [Sagara.Core.Collections](index.md#Sagara.Core.Collections 'Sagara.Core.Collections')

## EmptyDictionary<TKey,TValue> Class

Provides a cached, read-only instance for the specified key and value types.

```csharp
public sealed class EmptyDictionary<TKey,TValue> :
System.Collections.Generic.IReadOnlyDictionary<TKey, TValue>,
System.Collections.Generic.IEnumerable<System.Collections.Generic.KeyValuePair<TKey, TValue>>,
System.Collections.IEnumerable,
System.Collections.Generic.IReadOnlyCollection<System.Collections.Generic.KeyValuePair<TKey, TValue>>
    where TKey : notnull
```
#### Type parameters

<a name='Sagara.Core.Collections.EmptyDictionary_TKey,TValue_.TKey'></a>

`TKey`

The type of keys in the read-only dictionary.

<a name='Sagara.Core.Collections.EmptyDictionary_TKey,TValue_.TValue'></a>

`TValue`

The type of values in the read-only dictionary.

Inheritance [System.Object](https://docs.microsoft.com/en-us/dotnet/api/System.Object 'System.Object') &#129106; EmptyDictionary<TKey,TValue>

Implements [System.Collections.Generic.IReadOnlyDictionary&lt;](https://docs.microsoft.com/en-us/dotnet/api/System.Collections.Generic.IReadOnlyDictionary-2 'System.Collections.Generic.IReadOnlyDictionary`2')[TKey](Sagara.Core.Collections.EmptyDictionary_TKey,TValue_.md#Sagara.Core.Collections.EmptyDictionary_TKey,TValue_.TKey 'Sagara.Core.Collections.EmptyDictionary<TKey,TValue>.TKey')[,](https://docs.microsoft.com/en-us/dotnet/api/System.Collections.Generic.IReadOnlyDictionary-2 'System.Collections.Generic.IReadOnlyDictionary`2')[TValue](Sagara.Core.Collections.EmptyDictionary_TKey,TValue_.md#Sagara.Core.Collections.EmptyDictionary_TKey,TValue_.TValue 'Sagara.Core.Collections.EmptyDictionary<TKey,TValue>.TValue')[&gt;](https://docs.microsoft.com/en-us/dotnet/api/System.Collections.Generic.IReadOnlyDictionary-2 'System.Collections.Generic.IReadOnlyDictionary`2'), [System.Collections.Generic.IEnumerable&lt;](https://docs.microsoft.com/en-us/dotnet/api/System.Collections.Generic.IEnumerable-1 'System.Collections.Generic.IEnumerable`1')[System.Collections.Generic.KeyValuePair&lt;](https://docs.microsoft.com/en-us/dotnet/api/System.Collections.Generic.KeyValuePair-2 'System.Collections.Generic.KeyValuePair`2')[TKey](Sagara.Core.Collections.EmptyDictionary_TKey,TValue_.md#Sagara.Core.Collections.EmptyDictionary_TKey,TValue_.TKey 'Sagara.Core.Collections.EmptyDictionary<TKey,TValue>.TKey')[,](https://docs.microsoft.com/en-us/dotnet/api/System.Collections.Generic.KeyValuePair-2 'System.Collections.Generic.KeyValuePair`2')[TValue](Sagara.Core.Collections.EmptyDictionary_TKey,TValue_.md#Sagara.Core.Collections.EmptyDictionary_TKey,TValue_.TValue 'Sagara.Core.Collections.EmptyDictionary<TKey,TValue>.TValue')[&gt;](https://docs.microsoft.com/en-us/dotnet/api/System.Collections.Generic.KeyValuePair-2 'System.Collections.Generic.KeyValuePair`2')[&gt;](https://docs.microsoft.com/en-us/dotnet/api/System.Collections.Generic.IEnumerable-1 'System.Collections.Generic.IEnumerable`1'), [System.Collections.IEnumerable](https://docs.microsoft.com/en-us/dotnet/api/System.Collections.IEnumerable 'System.Collections.IEnumerable'), [System.Collections.Generic.IReadOnlyCollection&lt;](https://docs.microsoft.com/en-us/dotnet/api/System.Collections.Generic.IReadOnlyCollection-1 'System.Collections.Generic.IReadOnlyCollection`1')[System.Collections.Generic.KeyValuePair&lt;](https://docs.microsoft.com/en-us/dotnet/api/System.Collections.Generic.KeyValuePair-2 'System.Collections.Generic.KeyValuePair`2')[TKey](Sagara.Core.Collections.EmptyDictionary_TKey,TValue_.md#Sagara.Core.Collections.EmptyDictionary_TKey,TValue_.TKey 'Sagara.Core.Collections.EmptyDictionary<TKey,TValue>.TKey')[,](https://docs.microsoft.com/en-us/dotnet/api/System.Collections.Generic.KeyValuePair-2 'System.Collections.Generic.KeyValuePair`2')[TValue](Sagara.Core.Collections.EmptyDictionary_TKey,TValue_.md#Sagara.Core.Collections.EmptyDictionary_TKey,TValue_.TValue 'Sagara.Core.Collections.EmptyDictionary<TKey,TValue>.TValue')[&gt;](https://docs.microsoft.com/en-us/dotnet/api/System.Collections.Generic.KeyValuePair-2 'System.Collections.Generic.KeyValuePair`2')[&gt;](https://docs.microsoft.com/en-us/dotnet/api/System.Collections.Generic.IReadOnlyCollection-1 'System.Collections.Generic.IReadOnlyCollection`1')

### Remarks
  
We're implementing [System.Collections.Generic.IReadOnlyDictionary&lt;&gt;](https://docs.microsoft.com/en-us/dotnet/api/System.Collections.Generic.IReadOnlyDictionary-2 'System.Collections.Generic.IReadOnlyDictionary`2') to get a true read-only dictionary without  
            risking various extension methods leading to runtime exceptions. For example, [System.Collections.ObjectModel.ReadOnlyDictionary&lt;&gt;](https://docs.microsoft.com/en-us/dotnet/api/System.Collections.ObjectModel.ReadOnlyDictionary-2 'System.Collections.ObjectModel.ReadOnlyDictionary`2')  
            implements [System.Collections.Generic.IDictionary&lt;&gt;](https://docs.microsoft.com/en-us/dotnet/api/System.Collections.Generic.IDictionary-2 'System.Collections.Generic.IDictionary`2'), which in turn lights up the [System.Collections.Generic.CollectionExtensions.TryAdd&lt;&gt;.Collections.Generic.IDictionary{&lt;&gt;](https://docs.microsoft.com/en-us/dotnet/api/System.Collections.Generic.CollectionExtensions.TryAdd--2#System_Collections_Generic_CollectionExtensions_TryAdd__2_System_Collections_Generic_IDictionary{__0,__1},__0,__1_ 'System.Collections.Generic.CollectionExtensions.TryAdd``2(System.Collections.Generic.IDictionary{``0,``1},``0,``1)')  
            extension method that will throw at runtime if called on a [System.Collections.ObjectModel.ReadOnlyDictionary&lt;&gt;](https://docs.microsoft.com/en-us/dotnet/api/System.Collections.ObjectModel.ReadOnlyDictionary-2 'System.Collections.ObjectModel.ReadOnlyDictionary`2').
### Constructors

<a name='Sagara.Core.Collections.EmptyDictionary_TKey,TValue_.EmptyDictionary()'></a>

## EmptyDictionary() Constructor

Don't let callers create their own instances.

```csharp
private EmptyDictionary();
```
### Fields

<a name='Sagara.Core.Collections.EmptyDictionary_TKey,TValue_.Instance'></a>

## EmptyDictionary<TKey,TValue>.Instance Field

The one and only instance of [EmptyDictionary&lt;TKey,TValue&gt;](Sagara.Core.Collections.EmptyDictionary_TKey,TValue_.md 'Sagara.Core.Collections.EmptyDictionary<TKey,TValue>') for this key/value type pair.

```csharp
public static readonly IReadOnlyDictionary<TKey,TValue> Instance;
```

#### Field Value
[System.Collections.Generic.IReadOnlyDictionary&lt;](https://docs.microsoft.com/en-us/dotnet/api/System.Collections.Generic.IReadOnlyDictionary-2 'System.Collections.Generic.IReadOnlyDictionary`2')[TKey](Sagara.Core.Collections.EmptyDictionary_TKey,TValue_.md#Sagara.Core.Collections.EmptyDictionary_TKey,TValue_.TKey 'Sagara.Core.Collections.EmptyDictionary<TKey,TValue>.TKey')[,](https://docs.microsoft.com/en-us/dotnet/api/System.Collections.Generic.IReadOnlyDictionary-2 'System.Collections.Generic.IReadOnlyDictionary`2')[TValue](Sagara.Core.Collections.EmptyDictionary_TKey,TValue_.md#Sagara.Core.Collections.EmptyDictionary_TKey,TValue_.TValue 'Sagara.Core.Collections.EmptyDictionary<TKey,TValue>.TValue')[&gt;](https://docs.microsoft.com/en-us/dotnet/api/System.Collections.Generic.IReadOnlyDictionary-2 'System.Collections.Generic.IReadOnlyDictionary`2')
### Properties

<a name='Sagara.Core.Collections.EmptyDictionary_TKey,TValue_.Count'></a>

## EmptyDictionary<TKey,TValue>.Count Property

Gets the number of elements in the collection.

```csharp
public int Count { get; }
```

Implements [Count](https://docs.microsoft.com/en-us/dotnet/api/System.Collections.Generic.IReadOnlyCollection-1.Count 'System.Collections.Generic.IReadOnlyCollection`1.Count')

#### Property Value
[System.Int32](https://docs.microsoft.com/en-us/dotnet/api/System.Int32 'System.Int32')

<a name='Sagara.Core.Collections.EmptyDictionary_TKey,TValue_.Keys'></a>

## EmptyDictionary<TKey,TValue>.Keys Property

Gets an enumerable collection that contains the keys in the read-only dictionary.

```csharp
public System.Collections.Generic.IEnumerable<TKey> Keys { get; }
```

Implements [Keys](https://docs.microsoft.com/en-us/dotnet/api/System.Collections.Generic.IReadOnlyDictionary-2.Keys 'System.Collections.Generic.IReadOnlyDictionary`2.Keys')

#### Property Value
[System.Collections.Generic.IEnumerable&lt;](https://docs.microsoft.com/en-us/dotnet/api/System.Collections.Generic.IEnumerable-1 'System.Collections.Generic.IEnumerable`1')[TKey](Sagara.Core.Collections.EmptyDictionary_TKey,TValue_.md#Sagara.Core.Collections.EmptyDictionary_TKey,TValue_.TKey 'Sagara.Core.Collections.EmptyDictionary<TKey,TValue>.TKey')[&gt;](https://docs.microsoft.com/en-us/dotnet/api/System.Collections.Generic.IEnumerable-1 'System.Collections.Generic.IEnumerable`1')

<a name='Sagara.Core.Collections.EmptyDictionary_TKey,TValue_.this[TKey]'></a>

## EmptyDictionary<TKey,TValue>.this[TKey] Property

Gets the element that has the specified key in the read-only dictionary.

```csharp
public TValue this[TKey key] { get; }
```
#### Parameters

<a name='Sagara.Core.Collections.EmptyDictionary_TKey,TValue_.this[TKey].key'></a>

`key` [TKey](Sagara.Core.Collections.EmptyDictionary_TKey,TValue_.md#Sagara.Core.Collections.EmptyDictionary_TKey,TValue_.TKey 'Sagara.Core.Collections.EmptyDictionary<TKey,TValue>.TKey')

The key to locate.

Implements [this[TKey]](https://docs.microsoft.com/en-us/dotnet/api/System.Collections.Generic.IReadOnlyDictionary-2.Item#System_Collections_Generic_IReadOnlyDictionary_2_Item__0_ 'System.Collections.Generic.IReadOnlyDictionary`2.Item(`0)')

#### Property Value
[TValue](Sagara.Core.Collections.EmptyDictionary_TKey,TValue_.md#Sagara.Core.Collections.EmptyDictionary_TKey,TValue_.TValue 'Sagara.Core.Collections.EmptyDictionary<TKey,TValue>.TValue')

#### Exceptions

[System.ArgumentNullException](https://docs.microsoft.com/en-us/dotnet/api/System.ArgumentNullException 'System.ArgumentNullException')  
[key](Sagara.Core.Collections.EmptyDictionary_TKey,TValue_.md#Sagara.Core.Collections.EmptyDictionary_TKey,TValue_.this[TKey].key 'Sagara.Core.Collections.EmptyDictionary<TKey,TValue>.this[TKey].key') is [null](https://docs.microsoft.com/en-us/dotnet/csharp/language-reference/keywords/null 'https://docs.microsoft.com/en-us/dotnet/csharp/language-reference/keywords/null').

[System.Collections.Generic.KeyNotFoundException](https://docs.microsoft.com/en-us/dotnet/api/System.Collections.Generic.KeyNotFoundException 'System.Collections.Generic.KeyNotFoundException')  
The property is retrieved and [key](Sagara.Core.Collections.EmptyDictionary_TKey,TValue_.md#Sagara.Core.Collections.EmptyDictionary_TKey,TValue_.this[TKey].key 'Sagara.Core.Collections.EmptyDictionary<TKey,TValue>.this[TKey].key') is not found.

<a name='Sagara.Core.Collections.EmptyDictionary_TKey,TValue_.Values'></a>

## EmptyDictionary<TKey,TValue>.Values Property

Gets an enumerable collection that contains the values in the read-only dictionary.

```csharp
public System.Collections.Generic.IEnumerable<TValue> Values { get; }
```

Implements [Values](https://docs.microsoft.com/en-us/dotnet/api/System.Collections.Generic.IReadOnlyDictionary-2.Values 'System.Collections.Generic.IReadOnlyDictionary`2.Values')

#### Property Value
[System.Collections.Generic.IEnumerable&lt;](https://docs.microsoft.com/en-us/dotnet/api/System.Collections.Generic.IEnumerable-1 'System.Collections.Generic.IEnumerable`1')[TValue](Sagara.Core.Collections.EmptyDictionary_TKey,TValue_.md#Sagara.Core.Collections.EmptyDictionary_TKey,TValue_.TValue 'Sagara.Core.Collections.EmptyDictionary<TKey,TValue>.TValue')[&gt;](https://docs.microsoft.com/en-us/dotnet/api/System.Collections.Generic.IEnumerable-1 'System.Collections.Generic.IEnumerable`1')
### Methods

<a name='Sagara.Core.Collections.EmptyDictionary_TKey,TValue_.ContainsKey(TKey)'></a>

## EmptyDictionary<TKey,TValue>.ContainsKey(TKey) Method

Determines whether the read-only dictionary contains an element that has the specified key.

```csharp
public bool ContainsKey(TKey key);
```
#### Parameters

<a name='Sagara.Core.Collections.EmptyDictionary_TKey,TValue_.ContainsKey(TKey).key'></a>

`key` [TKey](Sagara.Core.Collections.EmptyDictionary_TKey,TValue_.md#Sagara.Core.Collections.EmptyDictionary_TKey,TValue_.TKey 'Sagara.Core.Collections.EmptyDictionary<TKey,TValue>.TKey')

The key to locate.

Implements [ContainsKey(TKey)](https://docs.microsoft.com/en-us/dotnet/api/System.Collections.Generic.IReadOnlyDictionary-2.ContainsKey#System_Collections_Generic_IReadOnlyDictionary_2_ContainsKey__0_ 'System.Collections.Generic.IReadOnlyDictionary`2.ContainsKey(`0)')

#### Returns
[System.Boolean](https://docs.microsoft.com/en-us/dotnet/api/System.Boolean 'System.Boolean')  
[true](https://docs.microsoft.com/en-us/dotnet/csharp/language-reference/builtin-types/bool 'https://docs.microsoft.com/en-us/dotnet/csharp/language-reference/builtin-types/bool') if the read-only dictionary contains an element that has the specified key; otherwise, [false](https://docs.microsoft.com/en-us/dotnet/csharp/language-reference/builtin-types/bool 'https://docs.microsoft.com/en-us/dotnet/csharp/language-reference/builtin-types/bool').

#### Exceptions

[System.ArgumentNullException](https://docs.microsoft.com/en-us/dotnet/api/System.ArgumentNullException 'System.ArgumentNullException')  
[key](Sagara.Core.Collections.EmptyDictionary_TKey,TValue_.md#Sagara.Core.Collections.EmptyDictionary_TKey,TValue_.ContainsKey(TKey).key 'Sagara.Core.Collections.EmptyDictionary<TKey,TValue>.ContainsKey(TKey).key') is [null](https://docs.microsoft.com/en-us/dotnet/csharp/language-reference/keywords/null 'https://docs.microsoft.com/en-us/dotnet/csharp/language-reference/keywords/null').

<a name='Sagara.Core.Collections.EmptyDictionary_TKey,TValue_.GetEnumerator()'></a>

## EmptyDictionary<TKey,TValue>.GetEnumerator() Method

Returns an enumerator that iterates through the collection.

```csharp
public System.Collections.Generic.IEnumerator<System.Collections.Generic.KeyValuePair<TKey,TValue>> GetEnumerator();
```

Implements [GetEnumerator()](https://docs.microsoft.com/en-us/dotnet/api/System.Collections.Generic.IEnumerable-1.GetEnumerator 'System.Collections.Generic.IEnumerable`1.GetEnumerator'), [GetEnumerator()](https://docs.microsoft.com/en-us/dotnet/api/System.Collections.IEnumerable.GetEnumerator 'System.Collections.IEnumerable.GetEnumerator')

#### Returns
[System.Collections.Generic.IEnumerator&lt;](https://docs.microsoft.com/en-us/dotnet/api/System.Collections.Generic.IEnumerator-1 'System.Collections.Generic.IEnumerator`1')[System.Collections.Generic.KeyValuePair&lt;](https://docs.microsoft.com/en-us/dotnet/api/System.Collections.Generic.KeyValuePair-2 'System.Collections.Generic.KeyValuePair`2')[TKey](Sagara.Core.Collections.EmptyDictionary_TKey,TValue_.md#Sagara.Core.Collections.EmptyDictionary_TKey,TValue_.TKey 'Sagara.Core.Collections.EmptyDictionary<TKey,TValue>.TKey')[,](https://docs.microsoft.com/en-us/dotnet/api/System.Collections.Generic.KeyValuePair-2 'System.Collections.Generic.KeyValuePair`2')[TValue](Sagara.Core.Collections.EmptyDictionary_TKey,TValue_.md#Sagara.Core.Collections.EmptyDictionary_TKey,TValue_.TValue 'Sagara.Core.Collections.EmptyDictionary<TKey,TValue>.TValue')[&gt;](https://docs.microsoft.com/en-us/dotnet/api/System.Collections.Generic.KeyValuePair-2 'System.Collections.Generic.KeyValuePair`2')[&gt;](https://docs.microsoft.com/en-us/dotnet/api/System.Collections.Generic.IEnumerator-1 'System.Collections.Generic.IEnumerator`1')  
An enumerator that can be used to iterate through the collection.

<a name='Sagara.Core.Collections.EmptyDictionary_TKey,TValue_.TryGetValue(TKey,TValue)'></a>

## EmptyDictionary<TKey,TValue>.TryGetValue(TKey, TValue) Method

Gets the value that is associated with the specified key.

```csharp
public bool TryGetValue(TKey key, out TValue value);
```
#### Parameters

<a name='Sagara.Core.Collections.EmptyDictionary_TKey,TValue_.TryGetValue(TKey,TValue).key'></a>

`key` [TKey](Sagara.Core.Collections.EmptyDictionary_TKey,TValue_.md#Sagara.Core.Collections.EmptyDictionary_TKey,TValue_.TKey 'Sagara.Core.Collections.EmptyDictionary<TKey,TValue>.TKey')

The key to locate.

<a name='Sagara.Core.Collections.EmptyDictionary_TKey,TValue_.TryGetValue(TKey,TValue).value'></a>

`value` [TValue](Sagara.Core.Collections.EmptyDictionary_TKey,TValue_.md#Sagara.Core.Collections.EmptyDictionary_TKey,TValue_.TValue 'Sagara.Core.Collections.EmptyDictionary<TKey,TValue>.TValue')

When this method returns, the value associated with the specified key, if the key is found; otherwise, the default value for the type of the [value](Sagara.Core.Collections.EmptyDictionary_TKey,TValue_.md#Sagara.Core.Collections.EmptyDictionary_TKey,TValue_.TryGetValue(TKey,TValue).value 'Sagara.Core.Collections.EmptyDictionary<TKey,TValue>.TryGetValue(TKey, TValue).value') parameter. This parameter is passed uninitialized.

Implements [TryGetValue(TKey, TValue)](https://docs.microsoft.com/en-us/dotnet/api/System.Collections.Generic.IReadOnlyDictionary-2.TryGetValue#System_Collections_Generic_IReadOnlyDictionary_2_TryGetValue__0,_1@_ 'System.Collections.Generic.IReadOnlyDictionary`2.TryGetValue(`0,`1@)')

#### Returns
[System.Boolean](https://docs.microsoft.com/en-us/dotnet/api/System.Boolean 'System.Boolean')  
[true](https://docs.microsoft.com/en-us/dotnet/csharp/language-reference/builtin-types/bool 'https://docs.microsoft.com/en-us/dotnet/csharp/language-reference/builtin-types/bool') if the object that implements the [System.Collections.Generic.IReadOnlyDictionary&lt;&gt;](https://docs.microsoft.com/en-us/dotnet/api/System.Collections.Generic.IReadOnlyDictionary-2 'System.Collections.Generic.IReadOnlyDictionary`2') interface contains an element that has the specified key; otherwise, [false](https://docs.microsoft.com/en-us/dotnet/csharp/language-reference/builtin-types/bool 'https://docs.microsoft.com/en-us/dotnet/csharp/language-reference/builtin-types/bool').

#### Exceptions

[System.ArgumentNullException](https://docs.microsoft.com/en-us/dotnet/api/System.ArgumentNullException 'System.ArgumentNullException')  
[key](Sagara.Core.Collections.EmptyDictionary_TKey,TValue_.md#Sagara.Core.Collections.EmptyDictionary_TKey,TValue_.TryGetValue(TKey,TValue).key 'Sagara.Core.Collections.EmptyDictionary<TKey,TValue>.TryGetValue(TKey, TValue).key') is [null](https://docs.microsoft.com/en-us/dotnet/csharp/language-reference/keywords/null 'https://docs.microsoft.com/en-us/dotnet/csharp/language-reference/keywords/null').
### Explicit Interface Implementations

<a name='Sagara.Core.Collections.EmptyDictionary_TKey,TValue_.System.Collections.IEnumerable.GetEnumerator()'></a>

## EmptyDictionary<TKey,TValue>.System.Collections.IEnumerable.GetEnumerator() Method

Returns an enumerator that iterates through a collection.

```csharp
System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator();
```

Implements [GetEnumerator()](https://docs.microsoft.com/en-us/dotnet/api/System.Collections.IEnumerable.GetEnumerator 'System.Collections.IEnumerable.GetEnumerator')