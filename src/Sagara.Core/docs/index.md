#### [Sagara.Core](index.md 'index')

## Sagara.Core Assembly
### Namespaces

<a name='Sagara.Core'></a>

## Sagara.Core Namespace
- **[Check](Sagara.Core.Check.md 'Sagara.Core.Check')** `Class`   
    
  Argument checker so that we don't have to keep writing the same guard clauses all over the place.  
    
  This updated version uses the new [System.Runtime.CompilerServices.CallerArgumentExpressionAttribute](https://docs.microsoft.com/en-us/dotnet/api/System.Runtime.CompilerServices.CallerArgumentExpressionAttribute 'System.Runtime.CompilerServices.CallerArgumentExpressionAttribute') to get the   
              argument expression from the call site so that we don't have to manually pass that in.
  - **[HasNoEmpties(IReadOnlyCollection&lt;string&gt;, string, string, int, string)](Sagara.Core.Check.md#Sagara.Core.Check.HasNoEmpties(System.Collections.Generic.IReadOnlyCollection_string_,string,string,int,string) 'Sagara.Core.Check.HasNoEmpties(System.Collections.Generic.IReadOnlyCollection<string>, string, string, int, string)')** `Method` Throws an [System.ArgumentNullException](https://docs.microsoft.com/en-us/dotnet/api/System.ArgumentNullException 'System.ArgumentNullException') if the collection is null, or an [System.ArgumentException](https://docs.microsoft.com/en-us/dotnet/api/System.ArgumentException 'System.ArgumentException') if  
    the collection has one or more null or white space strings.
  - **[HasNoNulls&lt;T&gt;(IReadOnlyList&lt;T&gt;, string, string, int, string)](Sagara.Core.Check.md#Sagara.Core.Check.HasNoNulls_T_(System.Collections.Generic.IReadOnlyList_T_,string,string,int,string) 'Sagara.Core.Check.HasNoNulls<T>(System.Collections.Generic.IReadOnlyList<T>, string, string, int, string)')** `Method` Throws an [System.ArgumentNullException](https://docs.microsoft.com/en-us/dotnet/api/System.ArgumentNullException 'System.ArgumentNullException') if the collection is null, or an [System.ArgumentException](https://docs.microsoft.com/en-us/dotnet/api/System.ArgumentException 'System.ArgumentException') if  
    the collection has one or more null values.
  - **[NotEmpty(string, string, string, int, string)](Sagara.Core.Check.md#Sagara.Core.Check.NotEmpty(string,string,string,int,string) 'Sagara.Core.Check.NotEmpty(string, string, string, int, string)')** `Method` Throws an [System.ArgumentNullException](https://docs.microsoft.com/en-us/dotnet/api/System.ArgumentNullException 'System.ArgumentNullException') if the string is null, or an [System.ArgumentException](https://docs.microsoft.com/en-us/dotnet/api/System.ArgumentException 'System.ArgumentException') if the  
    string is null or white space.
  - **[NotEmpty(Guid, string, string, int, string)](Sagara.Core.Check.md#Sagara.Core.Check.NotEmpty(System.Guid,string,string,int,string) 'Sagara.Core.Check.NotEmpty(System.Guid, string, string, int, string)')** `Method` Throws an [System.ArgumentException](https://docs.microsoft.com/en-us/dotnet/api/System.ArgumentException 'System.ArgumentException') if the Guid equals Guid.Empty.
  - **[NotEmpty&lt;T&gt;(IReadOnlyCollection&lt;T&gt;, string, string, int, string)](Sagara.Core.Check.md#Sagara.Core.Check.NotEmpty_T_(System.Collections.Generic.IReadOnlyCollection_T_,string,string,int,string) 'Sagara.Core.Check.NotEmpty<T>(System.Collections.Generic.IReadOnlyCollection<T>, string, string, int, string)')** `Method` Throws an [System.ArgumentNullException](https://docs.microsoft.com/en-us/dotnet/api/System.ArgumentNullException 'System.ArgumentNullException') if the collection is null, or an [System.ArgumentException](https://docs.microsoft.com/en-us/dotnet/api/System.ArgumentException 'System.ArgumentException') if  
    the collection is empty.
  - **[NotNull&lt;T&gt;(T, string, string, int, string)](Sagara.Core.Check.md#Sagara.Core.Check.NotNull_T_(T,string,string,int,string) 'Sagara.Core.Check.NotNull<T>(T, string, string, int, string)')** `Method` Throws an [System.ArgumentNullException](https://docs.microsoft.com/en-us/dotnet/api/System.ArgumentNullException 'System.ArgumentNullException') if the value is null.
  - **[NotOutOfRange&lt;T&gt;(T, T, T, string, string, int, string)](Sagara.Core.Check.md#Sagara.Core.Check.NotOutOfRange_T_(T,T,T,string,string,int,string) 'Sagara.Core.Check.NotOutOfRange<T>(T, T, T, string, string, int, string)')** `Method` Throws an [System.ArgumentOutOfRangeException](https://docs.microsoft.com/en-us/dotnet/api/System.ArgumentOutOfRangeException 'System.ArgumentOutOfRangeException') if value is outside the range of   
    [rangeLo, rangeHi].
- **[IEnumerableExtensions](Sagara.Core.IEnumerableExtensions.md 'Sagara.Core.IEnumerableExtensions')** `Class`
  - **[Index&lt;TSource&gt;(this IEnumerable&lt;TSource&gt;)](Sagara.Core.IEnumerableExtensions.md#Sagara.Core.IEnumerableExtensions.Index_TSource_(thisSystem.Collections.Generic.IEnumerable_TSource_) 'Sagara.Core.IEnumerableExtensions.Index<TSource>(this System.Collections.Generic.IEnumerable<TSource>)')** `Method` Returns an enumerable that incorporates the element's index into a tuple.
- **[SequentialGuid](Sagara.Core.SequentialGuid.md 'Sagara.Core.SequentialGuid')** `Class` Generates Guids sequentially.
  - **[SequentialGuid()](Sagara.Core.SequentialGuid.md#Sagara.Core.SequentialGuid.SequentialGuid() 'Sagara.Core.SequentialGuid.SequentialGuid()')** `Constructor` Default .ctor. Initializes [CurrentGuid](Sagara.Core.SequentialGuid.md#Sagara.Core.SequentialGuid.CurrentGuid 'Sagara.Core.SequentialGuid.CurrentGuid') with a new value.
  - **[SequentialGuid(Guid)](Sagara.Core.SequentialGuid.md#Sagara.Core.SequentialGuid.SequentialGuid(System.Guid) 'Sagara.Core.SequentialGuid.SequentialGuid(System.Guid)')** `Constructor` Constructor that initializes [CurrentGuid](Sagara.Core.SequentialGuid.md#Sagara.Core.SequentialGuid.CurrentGuid 'Sagara.Core.SequentialGuid.CurrentGuid') with the provided Guid.
  - **[_epochTicks](Sagara.Core.SequentialGuid.md#Sagara.Core.SequentialGuid._epochTicks 'Sagara.Core.SequentialGuid._epochTicks')** `Field` Equivalent to 'SELECT CAST(0 AS DATETIME)'. Specify UTC since we're using UTC in GenerateComb, and   
    since all the servers we run on are set to UTC.
  - **[CurrentGuid](Sagara.Core.SequentialGuid.md#Sagara.Core.SequentialGuid.CurrentGuid 'Sagara.Core.SequentialGuid.CurrentGuid')** `Property` The current sequential Guid value.
  - **[GenerateComb()](Sagara.Core.SequentialGuid.md#Sagara.Core.SequentialGuid.GenerateComb() 'Sagara.Core.SequentialGuid.GenerateComb()')** `Method` Generate a sequential Guid.
  - **[Increment(SequentialGuid)](Sagara.Core.SequentialGuid.md#Sagara.Core.SequentialGuid.Increment(Sagara.Core.SequentialGuid) 'Sagara.Core.SequentialGuid.Increment(Sagara.Core.SequentialGuid)')** `Method` Increment one or more bytes of [CurrentGuid](Sagara.Core.SequentialGuid.md#Sagara.Core.SequentialGuid.CurrentGuid 'Sagara.Core.SequentialGuid.CurrentGuid') to get the next sequential Guid,  
    and then copy this new value back to [CurrentGuid](Sagara.Core.SequentialGuid.md#Sagara.Core.SequentialGuid.CurrentGuid 'Sagara.Core.SequentialGuid.CurrentGuid').
  - **[operator ++(SequentialGuid)](Sagara.Core.SequentialGuid.md#Sagara.Core.SequentialGuid.op_Increment(Sagara.Core.SequentialGuid) 'Sagara.Core.SequentialGuid.op_Increment(Sagara.Core.SequentialGuid)')** `Operator` Increment one or more bytes of [CurrentGuid](Sagara.Core.SequentialGuid.md#Sagara.Core.SequentialGuid.CurrentGuid 'Sagara.Core.SequentialGuid.CurrentGuid') to get the next sequential Guid,  
    and then copy this new value back to [CurrentGuid](Sagara.Core.SequentialGuid.md#Sagara.Core.SequentialGuid.CurrentGuid 'Sagara.Core.SequentialGuid.CurrentGuid').
- **[StringSplits](Sagara.Core.StringSplits.md 'Sagara.Core.StringSplits')** `Class` Static array references to that we don't have to allocate an array every time we call string.Split.

<a name='Sagara.Core.Collections'></a>

## Sagara.Core.Collections Namespace
- **[EmptyDictionary&lt;TKey,TValue&gt;](Sagara.Core.Collections.EmptyDictionary_TKey,TValue_.md 'Sagara.Core.Collections.EmptyDictionary<TKey,TValue>')** `Class` Provides a cached, read-only instance for the specified key and value types.
  - **[EmptyDictionary()](Sagara.Core.Collections.EmptyDictionary_TKey,TValue_.md#Sagara.Core.Collections.EmptyDictionary_TKey,TValue_.EmptyDictionary() 'Sagara.Core.Collections.EmptyDictionary<TKey,TValue>.EmptyDictionary()')** `Constructor` Don't let callers create their own instances.
  - **[Instance](Sagara.Core.Collections.EmptyDictionary_TKey,TValue_.md#Sagara.Core.Collections.EmptyDictionary_TKey,TValue_.Instance 'Sagara.Core.Collections.EmptyDictionary<TKey,TValue>.Instance')** `Field` The one and only instance of [EmptyDictionary&lt;TKey,TValue&gt;](Sagara.Core.Collections.EmptyDictionary_TKey,TValue_.md 'Sagara.Core.Collections.EmptyDictionary<TKey,TValue>') for this key/value type pair.
  - **[Count](Sagara.Core.Collections.EmptyDictionary_TKey,TValue_.md#Sagara.Core.Collections.EmptyDictionary_TKey,TValue_.Count 'Sagara.Core.Collections.EmptyDictionary<TKey,TValue>.Count')** `Property` Gets the number of elements in the collection.
  - **[Keys](Sagara.Core.Collections.EmptyDictionary_TKey,TValue_.md#Sagara.Core.Collections.EmptyDictionary_TKey,TValue_.Keys 'Sagara.Core.Collections.EmptyDictionary<TKey,TValue>.Keys')** `Property` Gets an enumerable collection that contains the keys in the read-only dictionary.
  - **[this[TKey]](Sagara.Core.Collections.EmptyDictionary_TKey,TValue_.md#Sagara.Core.Collections.EmptyDictionary_TKey,TValue_.this[TKey] 'Sagara.Core.Collections.EmptyDictionary<TKey,TValue>.this[TKey]')** `Property` Gets the element that has the specified key in the read-only dictionary.
  - **[Values](Sagara.Core.Collections.EmptyDictionary_TKey,TValue_.md#Sagara.Core.Collections.EmptyDictionary_TKey,TValue_.Values 'Sagara.Core.Collections.EmptyDictionary<TKey,TValue>.Values')** `Property` Gets an enumerable collection that contains the values in the read-only dictionary.
  - **[ContainsKey(TKey)](Sagara.Core.Collections.EmptyDictionary_TKey,TValue_.md#Sagara.Core.Collections.EmptyDictionary_TKey,TValue_.ContainsKey(TKey) 'Sagara.Core.Collections.EmptyDictionary<TKey,TValue>.ContainsKey(TKey)')** `Method` Determines whether the read-only dictionary contains an element that has the specified key.
  - **[GetEnumerator()](Sagara.Core.Collections.EmptyDictionary_TKey,TValue_.md#Sagara.Core.Collections.EmptyDictionary_TKey,TValue_.GetEnumerator() 'Sagara.Core.Collections.EmptyDictionary<TKey,TValue>.GetEnumerator()')** `Method` Returns an enumerator that iterates through the collection.
  - **[TryGetValue(TKey, TValue)](Sagara.Core.Collections.EmptyDictionary_TKey,TValue_.md#Sagara.Core.Collections.EmptyDictionary_TKey,TValue_.TryGetValue(TKey,TValue) 'Sagara.Core.Collections.EmptyDictionary<TKey,TValue>.TryGetValue(TKey, TValue)')** `Method` Gets the value that is associated with the specified key.
  - **[System.Collections.IEnumerable.GetEnumerator()](Sagara.Core.Collections.EmptyDictionary_TKey,TValue_.md#Sagara.Core.Collections.EmptyDictionary_TKey,TValue_.System.Collections.IEnumerable.GetEnumerator() 'Sagara.Core.Collections.EmptyDictionary<TKey,TValue>.System.Collections.IEnumerable.GetEnumerator()')** `Explicit Interface Implementation` Returns an enumerator that iterates through a collection.

<a name='Sagara.Core.Enums'></a>

## Sagara.Core.Enums Namespace
- **[EnumTraits&lt;TEnum&gt;](Sagara.Core.Enums.EnumTraits_TEnum_.md 'Sagara.Core.Enums.EnumTraits<TEnum>')** `Class`   
    
  Consolidated wrapper for enum values, valid values, and display names.  
    
  Inspired by: https://devblogs.microsoft.com/premier-developer/dissecting-new-generics-constraints-in-c-7-3/
  - **[AllValues](Sagara.Core.Enums.EnumTraits_TEnum_.md#Sagara.Core.Enums.EnumTraits_TEnum_.AllValues 'Sagara.Core.Enums.EnumTraits<TEnum>.AllValues')** `Property` Returns all defined enum values.
  - **[EnumType](Sagara.Core.Enums.EnumTraits_TEnum_.md#Sagara.Core.Enums.EnumTraits_TEnum_.EnumType 'Sagara.Core.Enums.EnumTraits<TEnum>.EnumType')** `Property` Returns the enum's Type information.
  - **[HasFlagsAttribute](Sagara.Core.Enums.EnumTraits_TEnum_.md#Sagara.Core.Enums.EnumTraits_TEnum_.HasFlagsAttribute 'Sagara.Core.Enums.EnumTraits<TEnum>.HasFlagsAttribute')** `Property` Returns true if the enum is decorated with a [System.FlagsAttribute](https://docs.microsoft.com/en-us/dotnet/api/System.FlagsAttribute 'System.FlagsAttribute'); false otherwise.
  - **[IsEmpty](Sagara.Core.Enums.EnumTraits_TEnum_.md#Sagara.Core.Enums.EnumTraits_TEnum_.IsEmpty 'Sagara.Core.Enums.EnumTraits<TEnum>.IsEmpty')** `Property` Returns true if the enum declaration is empty; false otherwise.
  - **[ValidValues](Sagara.Core.Enums.EnumTraits_TEnum_.md#Sagara.Core.Enums.EnumTraits_TEnum_.ValidValues 'Sagara.Core.Enums.EnumTraits<TEnum>.ValidValues')** `Property` Returns all enum values that are not marked with [InvalidEnumValueAttribute](Sagara.Core.Enums.InvalidEnumValueAttribute.md 'Sagara.Core.Enums.InvalidEnumValueAttribute').
  - **[EnsureNoDuplicateValues()](Sagara.Core.Enums.EnumTraits_TEnum_.md#Sagara.Core.Enums.EnumTraits_TEnum_.EnsureNoDuplicateValues() 'Sagara.Core.Enums.EnumTraits<TEnum>.EnsureNoDuplicateValues()')** `Method` Throws an [System.InvalidOperationException](https://docs.microsoft.com/en-us/dotnet/api/System.InvalidOperationException 'System.InvalidOperationException') if the enum has one or more duplicate underlying values defined.
  - **[GetDisplayName(TEnum)](Sagara.Core.Enums.EnumTraits_TEnum_.md#Sagara.Core.Enums.EnumTraits_TEnum_.GetDisplayName(TEnum) 'Sagara.Core.Enums.EnumTraits<TEnum>.GetDisplayName(TEnum)')** `Method` Return the display name for the enum value, which is either from the [Display] attribute   
    or the property name.
  - **[GetDisplayNameOrDefault(TEnum)](Sagara.Core.Enums.EnumTraits_TEnum_.md#Sagara.Core.Enums.EnumTraits_TEnum_.GetDisplayNameOrDefault(TEnum) 'Sagara.Core.Enums.EnumTraits<TEnum>.GetDisplayNameOrDefault(TEnum)')** `Method` Return the display name for the enum value, which is either from the [Display] attribute   
    or the property name. If not found, return null.
  - **[GetDisplayNameOrEnumValueName(TEnum)](Sagara.Core.Enums.EnumTraits_TEnum_.md#Sagara.Core.Enums.EnumTraits_TEnum_.GetDisplayNameOrEnumValueName(TEnum) 'Sagara.Core.Enums.EnumTraits<TEnum>.GetDisplayNameOrEnumValueName(TEnum)')** `Method` If an enum value is decorated with a Display attribute, get its Name. Otherwise, return  
    the enum value name as a string.
  - **[IsValid(TEnum)](Sagara.Core.Enums.EnumTraits_TEnum_.md#Sagara.Core.Enums.EnumTraits_TEnum_.IsValid(TEnum) 'Sagara.Core.Enums.EnumTraits<TEnum>.IsValid(TEnum)')** `Method` Returns true if the enum value is in the list of valid values; false otherwise.
- **[InvalidEnumValueAttribute](Sagara.Core.Enums.InvalidEnumValueAttribute.md 'Sagara.Core.Enums.InvalidEnumValueAttribute')** `Class` Denotes a sentinel value that we use to detect, e.g., uninitialized members ("Unknown"). Enum  
  values decorated with this attribute will not be considered valid by the EnumTraits helper class.
- **[InvalidEnumValueException](Sagara.Core.Enums.InvalidEnumValueException.md 'Sagara.Core.Enums.InvalidEnumValueException')** `Class` Thrown when an enum value is one of the enum's invalid values. Invalid values are indicated   
  explicitly by the presence of an [InvalidEnumValueAttribute](Sagara.Core.Enums.InvalidEnumValueAttribute.md 'Sagara.Core.Enums.InvalidEnumValueAttribute') on the enum value.
  - **[ThrowIfInvalid&lt;TEnum&gt;(TEnum)](Sagara.Core.Enums.InvalidEnumValueException.md#Sagara.Core.Enums.InvalidEnumValueException.ThrowIfInvalid_TEnum_(TEnum) 'Sagara.Core.Enums.InvalidEnumValueException.ThrowIfInvalid<TEnum>(TEnum)')** `Method` Throws an InvalidEnumValueException if the enum value is decorated with [InvalidEnumValueAttribute](Sagara.Core.Enums.InvalidEnumValueAttribute.md 'Sagara.Core.Enums.InvalidEnumValueAttribute').

<a name='Sagara.Core.IO'></a>

## Sagara.Core.IO Namespace
- **[RecyclableMemoryStreamManagerHelper](Sagara.Core.IO.RecyclableMemoryStreamManagerHelper.md 'Sagara.Core.IO.RecyclableMemoryStreamManagerHelper')** `Class` Creates a [Microsoft.IO.RecyclableMemoryStreamManager](https://docs.microsoft.com/en-us/dotnet/api/Microsoft.IO.RecyclableMemoryStreamManager 'Microsoft.IO.RecyclableMemoryStreamManager') that the caller can register as a singleton in   
  a DI framework.
  - **[Create()](Sagara.Core.IO.RecyclableMemoryStreamManagerHelper.md#Sagara.Core.IO.RecyclableMemoryStreamManagerHelper.Create() 'Sagara.Core.IO.RecyclableMemoryStreamManagerHelper.Create()')** `Method`   
      
    Create a new [Microsoft.IO.RecyclableMemoryStreamManager](https://docs.microsoft.com/en-us/dotnet/api/Microsoft.IO.RecyclableMemoryStreamManager 'Microsoft.IO.RecyclableMemoryStreamManager') instance that can be used by the DI framework.  
      
    Uses the same defaults (as of Microsoft.IO.RecyclableMemoryStream v2.3.2) as the [Microsoft.IO.RecyclableMemoryStreamManager](https://docs.microsoft.com/en-us/dotnet/api/Microsoft.IO.RecyclableMemoryStreamManager 'Microsoft.IO.RecyclableMemoryStreamManager') default ctor,  
                except we cap the max small and large pool free bytes at 12.5 MB and 512 MB, respectively. RMSM leaves them unbounded.

<a name='Sagara.Core.Json.SystemTextJson'></a>

## Sagara.Core.Json.SystemTextJson Namespace
- **[SystemTextJsonHelper](Sagara.Core.Json.SystemTextJson.SystemTextJsonHelper.md 'Sagara.Core.Json.SystemTextJson.SystemTextJsonHelper')** `Class` Helper methods for serializing JSON with System.Text.Json.
  - **[Deserialize&lt;T&gt;(string, bool)](Sagara.Core.Json.SystemTextJson.SystemTextJsonHelper.md#Sagara.Core.Json.SystemTextJson.SystemTextJsonHelper.Deserialize_T_(string,bool) 'Sagara.Core.Json.SystemTextJson.SystemTextJsonHelper.Deserialize<T>(string, bool)')** `Method` Parse the JSON string into into an instance of the type specified by a generic type parameter. Default  
    is to use case-insensitive property names.
  - **[Serialize&lt;TValue&gt;(TValue, bool, bool)](Sagara.Core.Json.SystemTextJson.SystemTextJsonHelper.md#Sagara.Core.Json.SystemTextJson.SystemTextJsonHelper.Serialize_TValue_(TValue,bool,bool) 'Sagara.Core.Json.SystemTextJson.SystemTextJsonHelper.Serialize<TValue>(TValue, bool, bool)')** `Method` Convert the value into a JSON string. Default is camelCase property names and minified output.

<a name='Sagara.Core.Time'></a>

## Sagara.Core.Time Namespace
- **[NodaTimeHelper](Sagara.Core.Time.NodaTimeHelper.md 'Sagara.Core.Time.NodaTimeHelper')** `Class` Wrappers around NodaTime helpers to convert between time zones.
  - **[ToLocal(this DateTime, string)](Sagara.Core.Time.NodaTimeHelper.md#Sagara.Core.Time.NodaTimeHelper.ToLocal(thisSystem.DateTime,string) 'Sagara.Core.Time.NodaTimeHelper.ToLocal(this System.DateTime, string)')** `Method` Convert the UTC date/time into the equivalent date/time of the given time zone specified by the   
    IANA time zone Id.
  - **[ToUtc(this DateTime, string)](Sagara.Core.Time.NodaTimeHelper.md#Sagara.Core.Time.NodaTimeHelper.ToUtc(thisSystem.DateTime,string) 'Sagara.Core.Time.NodaTimeHelper.ToUtc(this System.DateTime, string)')** `Method` Convert the local date/time (local to the time zone specified by the IANA time zone Id) into the  
    equivalent UTC date/time.

<a name='Sagara.Core.Validation'></a>

## Sagara.Core.Validation Namespace
- **[ValidationHelper](Sagara.Core.Validation.ValidationHelper.md 'Sagara.Core.Validation.ValidationHelper')** `Class` Contains common validation methods that work with [ValidatableProperty&lt;TValue&gt;](Sagara.Core.Validation.ValidatableProperty_TValue_.md 'Sagara.Core.Validation.ValidatableProperty<TValue>') and   
  [RequestError](Sagara.Core.Validation.RequestError.md 'Sagara.Core.Validation.RequestError').
  - **[CheckRequiredField(ValidatableProperty&lt;string&gt;, ICollection&lt;RequestError&gt;)](Sagara.Core.Validation.ValidationHelper.md#Sagara.Core.Validation.ValidationHelper.CheckRequiredField(Sagara.Core.Validation.ValidatableProperty_string_,System.Collections.Generic.ICollection_Sagara.Core.Validation.RequestError_) 'Sagara.Core.Validation.ValidationHelper.CheckRequiredField(Sagara.Core.Validation.ValidatableProperty<string>, System.Collections.Generic.ICollection<Sagara.Core.Validation.RequestError>)')** `Method` Add an error message if value is null or white space.
  - **[CheckRequiredField&lt;T&gt;(ValidatableProperty&lt;Nullable&lt;T&gt;&gt;, ICollection&lt;RequestError&gt;)](Sagara.Core.Validation.ValidationHelper.md#Sagara.Core.Validation.ValidationHelper.CheckRequiredField_T_(Sagara.Core.Validation.ValidatableProperty_System.Nullable_T__,System.Collections.Generic.ICollection_Sagara.Core.Validation.RequestError_) 'Sagara.Core.Validation.ValidationHelper.CheckRequiredField<T>(Sagara.Core.Validation.ValidatableProperty<System.Nullable<T>>, System.Collections.Generic.ICollection<Sagara.Core.Validation.RequestError>)')** `Method`   
      
    Add an error message if value is null.  
      
    Use this overload for nullable value types.
  - **[CheckRequiredField&lt;T&gt;(ValidatableProperty&lt;T&gt;, ICollection&lt;RequestError&gt;)](Sagara.Core.Validation.ValidationHelper.md#Sagara.Core.Validation.ValidationHelper.CheckRequiredField_T_(Sagara.Core.Validation.ValidatableProperty_T_,System.Collections.Generic.ICollection_Sagara.Core.Validation.RequestError_) 'Sagara.Core.Validation.ValidationHelper.CheckRequiredField<T>(Sagara.Core.Validation.ValidatableProperty<T>, System.Collections.Generic.ICollection<Sagara.Core.Validation.RequestError>)')** `Method`   
      
    Add an error message if value is null.  
      
    Use this overload for reference types.
  - **[CheckStringMaxLength(ValidatableProperty&lt;string&gt;, int, ICollection&lt;RequestError&gt;)](Sagara.Core.Validation.ValidationHelper.md#Sagara.Core.Validation.ValidationHelper.CheckStringMaxLength(Sagara.Core.Validation.ValidatableProperty_string_,int,System.Collections.Generic.ICollection_Sagara.Core.Validation.RequestError_) 'Sagara.Core.Validation.ValidationHelper.CheckStringMaxLength(Sagara.Core.Validation.ValidatableProperty<string>, int, System.Collections.Generic.ICollection<Sagara.Core.Validation.RequestError>)')** `Method` Add an error message if value's length exceeds maxLength.
  - **[CheckStringMinLength(ValidatableProperty&lt;string&gt;, int, ICollection&lt;RequestError&gt;)](Sagara.Core.Validation.ValidationHelper.md#Sagara.Core.Validation.ValidationHelper.CheckStringMinLength(Sagara.Core.Validation.ValidatableProperty_string_,int,System.Collections.Generic.ICollection_Sagara.Core.Validation.RequestError_) 'Sagara.Core.Validation.ValidationHelper.CheckStringMinLength(Sagara.Core.Validation.ValidatableProperty<string>, int, System.Collections.Generic.ICollection<Sagara.Core.Validation.RequestError>)')** `Method` Add an error message if value's length is less than minLength.
  - **[GetDisplayName&lt;T&gt;(this ValidatableProperty&lt;T&gt;)](Sagara.Core.Validation.ValidationHelper.md#Sagara.Core.Validation.ValidationHelper.GetDisplayName_T_(thisSagara.Core.Validation.ValidatableProperty_T_) 'Sagara.Core.Validation.ValidationHelper.GetDisplayName<T>(this Sagara.Core.Validation.ValidatableProperty<T>)')** `Method` Return displayName if it's not null; else, return propertyName.
- **[RequestError](Sagara.Core.Validation.RequestError.md 'Sagara.Core.Validation.RequestError')** `Struct` Used to pass validation error messages from validation logic back to the UI for display.
  - **[RequestError(string, string)](Sagara.Core.Validation.RequestError.md#Sagara.Core.Validation.RequestError.RequestError(string,string) 'Sagara.Core.Validation.RequestError.RequestError(string, string)')** `Constructor` Used to pass validation error messages from validation logic back to the UI for display.
  - **[ErrorMessage](Sagara.Core.Validation.RequestError.md#Sagara.Core.Validation.RequestError.ErrorMessage 'Sagara.Core.Validation.RequestError.ErrorMessage')** `Property` A message describing the error.
  - **[PropertyName](Sagara.Core.Validation.RequestError.md#Sagara.Core.Validation.RequestError.PropertyName 'Sagara.Core.Validation.RequestError.PropertyName')** `Property` The name of the property, if any, that generated the error. If there is no property,  
                use string.Empty.
- **[ValidatableProperty&lt;TValue&gt;](Sagara.Core.Validation.ValidatableProperty_TValue_.md 'Sagara.Core.Validation.ValidatableProperty<TValue>')** `Struct` Used to populate values from MediatR models and pass to a validation service class. This allows the validation  
  service to return a list of errors containing the HTML form element names that generated the errors so that  
  we can highlight them on the form using JavaScript.
  - **[ValidatableProperty(TValue, string, string)](Sagara.Core.Validation.ValidatableProperty_TValue_.md#Sagara.Core.Validation.ValidatableProperty_TValue_.ValidatableProperty(TValue,string,string) 'Sagara.Core.Validation.ValidatableProperty<TValue>.ValidatableProperty(TValue, string, string)')** `Constructor` Used to populate values from MediatR models and pass to a validation service class. This allows the validation  
    service to return a list of errors containing the HTML form element names that generated the errors so that  
    we can highlight them on the form using JavaScript.
  - **[DisplayName](Sagara.Core.Validation.ValidatableProperty_TValue_.md#Sagara.Core.Validation.ValidatableProperty_TValue_.DisplayName 'Sagara.Core.Validation.ValidatableProperty<TValue>.DisplayName')** `Property` Optional. User-friendly display name.
  - **[PropertyName](Sagara.Core.Validation.ValidatableProperty_TValue_.md#Sagara.Core.Validation.ValidatableProperty_TValue_.PropertyName 'Sagara.Core.Validation.ValidatableProperty<TValue>.PropertyName')** `Property` Required. The name of the property corresponding to the HTML form element.
  - **[Value](Sagara.Core.Validation.ValidatableProperty_TValue_.md#Sagara.Core.Validation.ValidatableProperty_TValue_.Value 'Sagara.Core.Validation.ValidatableProperty<TValue>.Value')** `Property` The value to validate.