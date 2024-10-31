namespace Extensions;


/// <summary>
/// Provides extension methods for collections.
/// </summary>
public static class CollectionExtensions
{
    /// <summary>
    /// Removes all elements that match the specified predicate from the collection.
    /// </summary>
    /// <typeparam name="T">The type of elements in the collection.</typeparam>
    /// <param name="this">The collection to remove elements from.</param>
    /// <param name="predicate">A function to test each element for a condition.</param>
    /// <remarks>
    /// For List{T}, this method uses the built-in RemoveAll method for better performance.
    /// For other collection types, it creates a temporary list of items to remove to avoid
    /// modifying the collection during enumeration.
    /// </remarks>
    public static void RemoveAll<T>(this ICollection<T> @this, Func<T, bool> predicate)
    {
        if (@this is List<T> list)
        {
            list.RemoveAll(new Predicate<T>(predicate));
        }
        else
        {
            var itemsToDelete = @this
                .Where(predicate)
                .ToList();

            foreach (var item in itemsToDelete) 
                @this.Remove(item);
        }
    }
    
    /// <summary>
    /// Returns a specified number of contiguous elements from the end of a sequence.
    /// </summary>
    /// <typeparam name="T">The type of the elements in the sequence.</typeparam>
    /// <param name="source">The sequence to return elements from.</param>
    /// <param name="n">The number of elements to take from the end of the sequence.</param>
    /// <returns>An IEnumerable{T} that contains the specified number of elements from the end of the input sequence.</returns>
    /// <remarks>
    /// This method optimizes for both ICollection{T} and IReadOnlyCollection{T} to avoid multiple enumerations.
    /// For other sequence types, it buffers the sequence into a List{T} to ensure single enumeration.
    /// </remarks>
    /// <exception cref="ArgumentNullException">source is null.</exception>
    public static IEnumerable<T> TakeLast<T>(this IEnumerable<T> source, int n)
    {
        if (source == null) throw new ArgumentNullException(nameof(source));
        if (n <= 0) return Enumerable.Empty<T>();

        // Optimize for collections where we can get the count without enumeration
        if (source is ICollection<T> collection)
        {
            return collection.Skip(Math.Max(0, collection.Count - n));
        }
        
        if (source is IReadOnlyCollection<T> readOnlyCollection)
        {
            return readOnlyCollection.Skip(Math.Max(0, readOnlyCollection.Count - n));
        }

        // For other sequences, buffer to avoid multiple enumerations
        var sourceAsList = source.ToList();
        return sourceAsList.Skip(Math.Max(0, sourceAsList.Count - n));
    }
}