using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Windows.Data;

namespace HistoryCollection.Demo.Mvvm.Converters
{
    public class StringToObjectConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var items = value as IEnumerable;
            var output = new List<StringValueWrapper>();

            foreach (var item in items)
            {
                output.Add(new StringValueWrapper(item.ToString()));
            }

            return output;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotSupportedException("Cannot convert back");
        }

        public class StringValueWrapper
        {
            public StringValueWrapper(string value)
            {
                Value = value;
                TimeStamp = DateTimeOffset.Now;
            }

            public string Value { get; }

            public DateTimeOffset TimeStamp { get; }

            public override string ToString()
            {
                return Value;
            }
        }
    }
}
