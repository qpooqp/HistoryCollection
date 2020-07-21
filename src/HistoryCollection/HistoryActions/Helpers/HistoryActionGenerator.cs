using HistoryCollection.HistoryActions.Interfaces;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HistoryCollection.HistoryActions.Helpers
{
    internal static class HistoryActionGenerator
    {
        public static IHistoryAction<T> GenerateAdd<T>(T item, int index)
        {
            return new Add<T>(item, index);
        }

        public static IHistoryAction<T> GenerateMove<T>(int newIndex, int oldIndex)
        {
            return new Move<T>(newIndex, oldIndex);
        }

        public static IHistoryAction<T> GenerateRemove<T>(T item, int index)
        {
            return new Remove<T>(item, index);
        }

        public static IHistoryAction<T> GenerateReplace<T>(T newItem, int newItemIndex, T oldItem, int oldItemIndex)
        {
            return new Replace<T>(newItem, newItemIndex, oldItem, oldItemIndex);
        }

        public static IHistoryAction<T> GenerateReset<T>(IEnumerable<T> items)
        {
            return new Reset<T>(items);
        }
    }
}
