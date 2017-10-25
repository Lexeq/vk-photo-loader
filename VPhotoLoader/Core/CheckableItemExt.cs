using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VPhotoLoader.Core
{
    public static class CheckableItemExt
    {
        public static IEnumerable<CheckableItem<T>> ToCheckable<T>(this IEnumerable<T> items, bool check)
        {
            foreach (var item in items)
            {
                yield return new CheckableItem<T>(item, check);
            }
        }

        public static IEnumerable<T> Checked<T>(this IEnumerable<CheckableItem<T>> citems)
        {
            return citems.Where(c => c.Check).Select(c => c.Item);
        }

        public static IEnumerable<ICheckable> Checked(this IEnumerable<ICheckable> citems)
        {
            return citems.Where(c => c.Check).Select(c => c);
        }
    }
}
