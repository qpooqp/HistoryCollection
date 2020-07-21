using HistoryCollection.HistoryActions.Interfaces;
using System.Collections.Specialized;

namespace HistoryCollection.HistoryActions
{
    internal class Remove<T> : IHistoryAction<T>
    {
        private readonly T _item;
        private readonly int _index;

        public Remove(T item, int index)
        {
            _item = item;
            _index = index;
        }

        public NotifyCollectionChangedAction Action { get => NotifyCollectionChangedAction.Remove; }

        public void Do(HistoryCollection<T> collection)
        {
            collection.Remove(_item);
        }

        public void Undo(HistoryCollection<T> collection)
        {
            collection.Insert(_index, _item);
        }
    }
}
