using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;

namespace HistoryCollection
{
    public interface IHistoryCollection<T> : IList<T>, IList, ICollection, IReadOnlyList<T>, IReadOnlyCollection<T>, INotifyCollectionChanged, INotifyPropertyChanged
    {
        IEnumerable<NotifyCollectionChangedAction> History { get; }

        int CurrentHistoryPosition { get; }

        bool CanUndo { get; }

        bool CanRedo { get; }

        bool Undo();

        bool UndoAll();

        bool Redo();

        bool RedoAll();

        void ClearHistory();
    }
}
