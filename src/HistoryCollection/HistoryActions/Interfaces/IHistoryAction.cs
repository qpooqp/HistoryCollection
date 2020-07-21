using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HistoryCollection.HistoryActions.Interfaces
{
    internal interface IHistoryAction<T>
    {
        NotifyCollectionChangedAction Action { get; }

        void Do(HistoryCollection<T> collection);

        void Undo(HistoryCollection<T> collection);
    }
}
