using HistoryCollection.Demo.Generators;
using HistoryCollection.Demo.Models;
using HistoryCollection.Demo.Mvvm;
using System.Linq;

namespace HistoryCollection.Demo.ViewModels
{
    public class MainViewModel : ViewModelBase
    {
        public MainViewModel()
        {
            //Handling of selection
            Persons.CollectionChanged += (s, e) =>
            {
                switch (e.Action)
                {
                    case System.Collections.Specialized.NotifyCollectionChangedAction.Add:
                        SelectedPerson = e.NewItems.Cast<Person>().First();
                        break;
                    case System.Collections.Specialized.NotifyCollectionChangedAction.Remove:
                        SelectedPerson = Persons.Count > e.OldStartingIndex ? Persons[e.OldStartingIndex] : Persons.LastOrDefault();
                        break;
                    case System.Collections.Specialized.NotifyCollectionChangedAction.Replace:
                        SelectedPerson = Persons[e.OldStartingIndex];
                        break;
                    case System.Collections.Specialized.NotifyCollectionChangedAction.Reset:
                        SelectedPerson = Persons.FirstOrDefault();
                        break;
                }
            };
        }

        public HistoryCollection<Person> Persons { get; } = new HistoryCollection<Person>(128);

        private Person _selectedPerson;
        public Person SelectedPerson
        {
            get { return _selectedPerson; }
            set { Set(ref _selectedPerson, value); }
        }

        //Add
        private RelayCommand _addCmnd;
        public RelayCommand AddCmnd { get { return _addCmnd ?? (_addCmnd = new RelayCommand(AddCmndHandler)); } }
        private void AddCmndHandler(object parameter)
        {
            Persons.Add(RandomPersonGenerator.Generate());
        }

        //Replace
        private RelayCommand _replaceCmnd;
        public RelayCommand ReplaceCmnd { get { return _replaceCmnd ?? (_replaceCmnd = new RelayCommand(ReplaceCmndHandler, ReplaceCmndCanExecute)); } }
        private void ReplaceCmndHandler(object parameter)
        {
            var personToReplaceIndex = Persons.IndexOf(SelectedPerson);
            Persons[personToReplaceIndex] = RandomPersonGenerator.Generate();
        }
        private bool ReplaceCmndCanExecute(object parameter)
        {
            return SelectedPerson != null;
        }

        //Move up
        private RelayCommand _moveUpCmnd;
        public RelayCommand MoveUpCmnd { get { return _moveUpCmnd ?? (_moveUpCmnd = new RelayCommand(MoveUpCmndHandler, MoveUpCmndCanExecute)); } }
        private void MoveUpCmndHandler(object parameter)
        {
            var personToMoveIndex = Persons.IndexOf(SelectedPerson);
            Persons.Move(personToMoveIndex, personToMoveIndex - 1);
        }
        private bool MoveUpCmndCanExecute(object parameter)
        {
            return SelectedPerson != null && Persons.IndexOf(SelectedPerson) > 0;
        }

        //Move down
        private RelayCommand _moveDownCmnd;
        public RelayCommand MoveDownCmnd { get { return _moveDownCmnd ?? (_moveDownCmnd = new RelayCommand(MoveDownCmndHandler, MoveDownCmndCanExecute)); } }
        private void MoveDownCmndHandler(object parameter)
        {
            var personToMoveIndex = Persons.IndexOf(SelectedPerson);
            Persons.Move(personToMoveIndex, personToMoveIndex + 1);
        }
        private bool MoveDownCmndCanExecute(object parameter)
        {
            return SelectedPerson != null && Persons.IndexOf(SelectedPerson) < Persons.Count - 1;
        }

        //Remove
        private RelayCommand _removeCmnd;
        public RelayCommand RemoveCmnd { get { return _removeCmnd ?? (_removeCmnd = new RelayCommand(RemoveCmndHandler, RemoveCmndCanExecute)); } }
        private void RemoveCmndHandler(object parameter)
        {
            Persons.Remove(SelectedPerson);
        }
        private bool RemoveCmndCanExecute(object parameter)
        {
            return SelectedPerson != null;
        }

        //Clear
        private RelayCommand _clearCmnd;
        public RelayCommand ClearCmnd { get { return _clearCmnd ?? (_clearCmnd = new RelayCommand(ClearCmndHandler, ClearCmndCanExecute)); } }
        private void ClearCmndHandler(object parameter)
        {
            Persons.Clear();
        }
        private bool ClearCmndCanExecute(object parameter)
        {
            return Persons.Any();
        }

        //Undo
        private RelayCommand _undoCmnd;
        public RelayCommand UndoCmnd { get { return _undoCmnd ?? (_undoCmnd = new RelayCommand(UndoCmndHandler, UndoCmndCanExecute)); } }
        private void UndoCmndHandler(object parameter)
        {
            Persons.Undo();
        }
        private bool UndoCmndCanExecute(object parameter)
        {
            return Persons.CanUndo;
        }

        //Undo all
        private RelayCommand _undoAllCmnd;
        public RelayCommand UndoAllCmnd { get { return _undoAllCmnd ?? (_undoAllCmnd = new RelayCommand(UndoAllCmndHandler, UndoAllCmndCanExecute)); } }
        private void UndoAllCmndHandler(object parameter)
        {
            Persons.UndoAll();
        }
        private bool UndoAllCmndCanExecute(object parameter)
        {
            return Persons.CanUndo;
        }

        //Redo
        private RelayCommand _redoCmnd;
        public RelayCommand RedoCmnd { get { return _redoCmnd ?? (_redoCmnd = new RelayCommand(RedoCmndHandler, RedoCmndCanExecute)); } }
        private void RedoCmndHandler(object parameter)
        {
            Persons.Redo();
        }
        private bool RedoCmndCanExecute(object parameter)
        {
            return Persons.CanRedo;
        }

        //Redo all
        private RelayCommand _redoAllCmnd;
        public RelayCommand RedoAllCmnd { get { return _redoAllCmnd ?? (_redoAllCmnd = new RelayCommand(RedoAllCmndHandler, RedoAllCmndCanExecute)); } }
        private void RedoAllCmndHandler(object parameter)
        {
            Persons.RedoAll();
        }
        private bool RedoAllCmndCanExecute(object parameter)
        {
            return Persons.CanRedo;
        }

        //ClearHistory
        private RelayCommand _clearHistoryCmnd;
        public RelayCommand ClearHistoryCmnd { get { return _clearHistoryCmnd ?? (_clearHistoryCmnd = new RelayCommand(ClearHistoryCmndHandler, ClearHistoryCmndCanExecute)); } }
        private void ClearHistoryCmndHandler(object parameter)
        {
            Persons.ClearHistory();
        }
        private bool ClearHistoryCmndCanExecute(object parameter)
        {
            return Persons.History.Any();
        }
    }
}
