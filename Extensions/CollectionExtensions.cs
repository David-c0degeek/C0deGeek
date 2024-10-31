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
}