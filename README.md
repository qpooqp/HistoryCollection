# HistoryCollection
Simple generic collection with ability to undo and redo collection change actions. 

It is meant to be used in WPF/UWP frontend development because it implements `INotifyCollectionChanged` and `INotifyPropertyChanged` and thus can be easily used together with data binding.

## What can it do
It tracks history of changes done on itself and it can undo or redo the changes. You can specify how many changes it can keep track of.

List of collection changes
- Insert
- Move
- Remove
- Replace
- Clear

## TODO
- Tests