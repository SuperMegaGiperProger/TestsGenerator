using System.Collections.Generic;
using System.Linq;

namespace TestsGenerator.Extensions
{
    public static class IEnumerableExtensions
    {
        public static IEnumerable<string> Compact(this IEnumerable<string> collection)
        {
            return collection.Where(str => !string.IsNullOrEmpty(str));
        }
    }
}