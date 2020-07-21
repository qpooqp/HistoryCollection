using HistoryCollection.HistoryActions.Interfaces;
using System.Collections.Specialized;

namespace HistoryCollection.HistoryActions
{
    internal class Move<T> : IHistoryAction<T>
    {
        private readonly int _newIndex;
        private readonly int _oldIndex;

        public Move(int newIndex, int oldIndex)
        {
            _newIndex = newIndex;
            _oldIndex = oldIndex;
        }

        public NotifyCollectionChangedAction Action { get => NotifyCollectionChangedAction.Move; }

        public void Do(HistoryCollection<T> collection)
        {
            collection.Move(_oldIndex, _newIndex);
        }

        public void Undo(HistoryCollection<T> collection)
        {
            collection.Move(_newIndex, _oldIndex);
        }
    }
}
