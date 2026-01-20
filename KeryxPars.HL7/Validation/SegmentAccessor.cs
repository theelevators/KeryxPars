using System.Collections.Concurrent;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;
using KeryxPars.HL7.Contracts;

namespace KeryxPars.HL7.Validation;

/// <summary>
/// High-performance segment and field accessor using compiled expression trees.
/// Zero allocation, zero reflection at runtime.
/// </summary>
public sealed class SegmentAccessor
{
    // Cache compiled delegates for ultra-fast property access
    private static readonly ConcurrentDictionary<(Type, string), Func<object, ISegment?>> _segmentGetters = new();
    private static readonly ConcurrentDictionary<(Type, int), Func<ISegment, object?>> _fieldGetters = new();

    /// <summary>
    /// Gets a segment from a message with zero runtime reflection.
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ISegment? GetSegment(object message, string segmentId)
    {
        if (string.IsNullOrWhiteSpace(segmentId))
            return null;

        var messageType = message.GetType();
        var key = (messageType, segmentId);

        // Get or create compiled delegate
        var getter = _segmentGetters.GetOrAdd(key, CreateSegmentGetter);
        return getter(message);
    }

    /// <summary>
    /// Gets a field value from a segment with zero runtime reflection.
    /// Fields are 1-based indexed.
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static object? GetFieldValue(ISegment segment, int fieldIndex)
    {
        var segmentType = segment.GetType();
        var key = (segmentType, fieldIndex);

        // Get or create compiled delegate
        var getter = _fieldGetters.GetOrAdd(key, CreateFieldGetter);
        return getter(segment);
    }

    /// <summary>
    /// Checks if a message has a segment without retrieving it.
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool HasSegment(object message, string segmentId)
    {
        return GetSegment(message, segmentId) is not null;
    }

    /// <summary>
    /// Creates a compiled delegate for fast segment access.
    /// Uses expression trees - compiled once, runs fast forever.
    /// </summary>
    private static Func<object, ISegment?> CreateSegmentGetter((Type messageType, string segmentId) key)
    {
        var (messageType, segmentId) = key;
        
        // Find the property by name (case-insensitive)
        var property = messageType.GetProperty(segmentId, 
            System.Reflection.BindingFlags.Public | 
            System.Reflection.BindingFlags.Instance | 
            System.Reflection.BindingFlags.IgnoreCase);

        if (property == null)
        {
            // Return a delegate that always returns null
            return _ => null;
        }

        // Build expression tree: (object msg) => ((MessageType)msg).SegmentProperty as ISegment
        var parameter = Expression.Parameter(typeof(object), "msg");
        var convertedParameter = Expression.Convert(parameter, messageType);
        var propertyAccess = Expression.Property(convertedParameter, property);
        var convertToSegment = Expression.TypeAs(propertyAccess, typeof(ISegment));
        
        var lambda = Expression.Lambda<Func<object, ISegment?>>(convertToSegment, parameter);
        
        // Compile to native code - fast!
        return lambda.Compile();
    }

    /// <summary>
    /// Creates a compiled delegate for fast field access.
    /// Uses expression trees - compiled once, runs fast forever.
    /// </summary>
    private static Func<ISegment, object?> CreateFieldGetter((Type segmentType, int fieldIndex) key)
    {
        var (segmentType, fieldIndex) = key;
        
        // Get all public instance properties except SegmentId and SegmentType
        var properties = segmentType
            .GetProperties(System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Instance)
            .Where(p => p.CanRead && p.Name != "SegmentId" && p.Name != "SegmentType")
            .ToArray();

        // HL7 fields are 1-based
        if (fieldIndex < 1 || fieldIndex > properties.Length)
        {
            // Return a delegate that always returns null
            return _ => null;
        }

        var property = properties[fieldIndex - 1];

        // Build expression tree: (ISegment seg) => ((SegmentType)seg).FieldProperty
        var parameter = Expression.Parameter(typeof(ISegment), "seg");
        var convertedParameter = Expression.Convert(parameter, segmentType);
        var propertyAccess = Expression.Property(convertedParameter, property);
        var convertToObject = Expression.Convert(propertyAccess, typeof(object));
        
        var lambda = Expression.Lambda<Func<ISegment, object?>>(convertToObject, parameter);
        
        // Compile to native code - fast!
        return lambda.Compile();
    }

    /// <summary>
    /// Clears the accessor cache. Useful for testing or memory management.
    /// </summary>
    public static void ClearCache()
    {
        _segmentGetters.Clear();
        _fieldGetters.Clear();
    }

    /// <summary>
    /// Gets cache statistics for monitoring.
    /// </summary>
    public static (int SegmentAccessors, int FieldAccessors) GetCacheStats()
    {
        return (_segmentGetters.Count, _fieldGetters.Count);
    }
}
