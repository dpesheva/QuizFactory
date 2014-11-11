namespace HelperExtentions
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public static class IEnumerableExtentions
    {
        public static void ForEach<T>(this IEnumerable<T> collection, Action<T> action)
        {
            foreach (var item in collection)
            {
                action(item);
            }
        }
    }
}