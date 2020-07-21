using HistoryCollection.HistoryActions.Interfaces;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;

namespace HistoryCollection.HistoryActions
{
    internal class Reset<T> : IHistoryAction<T>
    {
        private readonly IEnumerable<T> _items;

        public Reset(IEnumerable<T> items)
        {
            _items = items.ToList();
        }

        public NotifyCollectionChangedAction Action { get => NotifyCollectionChangedAction.Reset; }

        public void Do(HistoryCollection<T> collection)
        {
            collection.Clear();
        }

        public void Undo(HistoryCollection<T> collection)
        {
            foreach (var item in _items)
            {
                collection.Add(item);
            }
        }
    }
}
