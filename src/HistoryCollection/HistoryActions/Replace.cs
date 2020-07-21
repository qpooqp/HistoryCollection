using HistoryCollection.HistoryActions.Interfaces;
using System.Collections.Specialized;

namespace HistoryCollection.HistoryActions
{
    internal class Replace<T> : IHistoryAction<T>
    {
        private readonly T _newItem;
        private readonly int _newItemIndex;
        private readonly T _oldItem;
        private readonly int _oldItemIndex;

        public Replace(T newItem, int newItemIndex, T oldItem, int oldItemIndex)
        {
            _newItem = newItem;
            _newItemIndex = newItemIndex;
            _oldItem = oldItem;
            _oldItemIndex = oldItemIndex;
        }

        public NotifyCollectionChangedAction Action { get => NotifyCollectionChangedAction.Replace; }

        public void Do(HistoryCollection<T> collection)
        {
            collection[_newItemIndex] = _newItem;
        }

        public void Undo(HistoryCollection<T> collection)
        {
            collection[_oldItemIndex] = _oldItem;
        }
    }
}
