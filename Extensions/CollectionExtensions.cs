namespace Extensions;

public static class CollectionExtensions
{
    public static void RemoveAll<T>(this ICollection<T> @this, Func<T, bool> predicate)
    {
        if (@this is List<T> list)
        {
            list.RemoveAll(new Predicate<T>(predicate));
        }
        else
        {
            List<T> itemsToDelete = @this
                .Where(predicate)
                .ToList();

            foreach (var item in itemsToDelete)
            {
                @this.Remove(item);
            }
        }
    }
}