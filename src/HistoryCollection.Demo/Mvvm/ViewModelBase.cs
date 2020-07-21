using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace HistoryCollection.Demo.Mvvm
{
    public class ViewModelBase : ObservableObject
    {
        protected void Set<T>(ref T field, T value, [CallerMemberName] string propertName = "")
        {
            if (EqualityComparer<T>.Default.Equals(field, value))
            {
                return;
            }

            field = value;
            NotifyPropertyChanged(propertName);
        }
    }
}
