using HistoryCollection.Helpers;
using HistoryCollection.HistoryActions.Helpers;
using HistoryCollection.HistoryActions.Interfaces;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;

namespace HistoryCollection
{
    public class HistoryCollection<T> : ObservableCollection<T>, IHistoryCollection<T>
    {
        private const int DefaultHistoryLimit = int.MaxValue;

        private readonly List<IHistoryAction<T>> _history = new List<IHistoryAction<T>>();

        private readonly SimpleMonitor _historySuppressedMonitor = new SimpleMonitor();
        private readonly SimpleMonitor _blockedForChangeMonitor = new SimpleMonitor();

        public HistoryCollection() : this(Enumerable.Empty<T>(), DefaultHistoryLimit)
        {
        }

        public HistoryCollection(int historyLimit) : this(Enumerable.Empty<T>(), historyLimit)
        {
        }

        public HistoryCollection(IEnumerable<T> collection) : this(collection, DefaultHistoryLimit)
        {
        }

        public HistoryCollection(IEnumerable<T> collection, int historyLimit) : base(collection)
        {
            if (historyLimit <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(historyLimit), "History limit must be greater than zero.");
            }

            HistoryLimit = historyLimit;
        }

        public IEnumerable<NotifyCollectionChangedAction> History { get => _history.Select(h => h.Action); }

        public int HistoryLimit { get; }

        public int CurrentHistoryPosition { get; private set; }

        public bool CanUndo { get => CurrentHistoryPosition > 0; }

        public bool CanRedo { get => _history.Count > CurrentHistoryPosition; }

        public bool Undo()
        {
            if (!CanUndo)
            {
                return false;
            }

            CheckIfBlockedForChange();

            using (_historySuppressedMonitor.Set())
            {
                CurrentHistoryPosition--;
                _history[CurrentHistoryPosition].Undo(this);
            }

            return true;
        }

        public bool UndoAll()
        {
            var returnValue = CanUndo;

            while (CanUndo)
            {
                Undo();
            }

            return returnValue;
        }

        public bool Redo()
        {
            if (!CanRedo)
            {
                return false;
            }

            CheckIfBlockedForChange();

            using (_historySuppressedMonitor.Set())
            {
                CurrentHistoryPosition++;
                _history[CurrentHistoryPosition - 1].Do(this);
            }

            return true;
        }

        public bool RedoAll()
        {
            var returnValue = CanRedo;

            while (CanRedo)
            {
                Redo();
            }

            return returnValue;
        }

        public void ClearHistory()
        {
            _history.Clear();
            CurrentHistoryPosition = 0;
            RaiseHistoryPropertyChangedEvents();
        }

        protected override void InsertItem(int index, T item)
        {
            CheckIfBlockedForChange();
            var action = HistoryActionGenerator.GenerateAdd(item, index);
            HandleHistory(action);
            base.InsertItem(index, item);
        }

        protected override void SetItem(int index, T item)
        {
            CheckIfBlockedForChange();
            var oldItem = this[index];
            var action = HistoryActionGenerator.GenerateReplace(item, index, oldItem, index);
            HandleHistory(action);
            base.SetItem(index, item);
        }

        protected override void MoveItem(int oldIndex, int newIndex)
        {
            CheckIfBlockedForChange();
            var action = HistoryActionGenerator.GenerateMove<T>(newIndex, oldIndex);
            HandleHistory(action);
            base.MoveItem(oldIndex, newIndex);
        }

        protected override void RemoveItem(int index)
        {
            CheckIfBlockedForChange();
            var oldItem = this[index];
            var action = HistoryActionGenerator.GenerateRemove(oldItem, index);
            HandleHistory(action);
            base.RemoveItem(index);
        }

        protected override void ClearItems()
        {
            CheckIfBlockedForChange();
            var action = HistoryActionGenerator.GenerateReset(Items);
            HandleHistory(action);
            base.ClearItems();
        }

        private void HandleHistory(IHistoryAction<T> historyAction)
        {
            if (_historySuppressedMonitor.IsSet)
            {
                return;
            }

            if (_history.Count > CurrentHistoryPosition)
            {
                _history.RemoveRange(CurrentHistoryPosition, _history.Count - CurrentHistoryPosition);
            }

            if (_history.Count >= HistoryLimit)
            {
                _history.RemoveAt(0);
                CurrentHistoryPosition--;
            }

            _history.Add(historyAction);
            CurrentHistoryPosition++;
        }

        private void CheckIfBlockedForChange()
        {
            if (_blockedForChangeMonitor.IsSet)
            {
                throw new InvalidOperationException($"Cannot change {nameof(HistoryCollection<T>)} during {nameof(CollectionChanged)} or {nameof(PropertyChanged)} event.");
            }
        }

        //TODO raise events only when the property actually changed
        private void RaiseHistoryPropertyChangedEvents()
        {
            OnPropertyChanged(new PropertyChangedEventArgs(nameof(History)));
            OnPropertyChanged(new PropertyChangedEventArgs(nameof(CurrentHistoryPosition)));
            OnPropertyChanged(new PropertyChangedEventArgs(nameof(CanUndo)));
            OnPropertyChanged(new PropertyChangedEventArgs(nameof(CanRedo)));
        }

        protected override void OnCollectionChanged(NotifyCollectionChangedEventArgs e)
        {
            using (_blockedForChangeMonitor.Set())
            {
                RaiseHistoryPropertyChangedEvents();
                base.OnCollectionChanged(e);
            }
        }

        protected override void OnPropertyChanged(PropertyChangedEventArgs e)
        {
            using (_blockedForChangeMonitor.Set())
            {
                base.OnPropertyChanged(e);
            }
        }
    }
}
