using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace HistoryCollection.Helpers
{
    /// <summary>
    /// Class used to help take track of state. Can be used with using pattern.
    /// </summary>
    [DebuggerDisplay("IsSet = {IsSet}")]
    internal class SimpleMonitor : IDisposable
    {
        public int Counter { get; private set; }

        public bool IsSet { get => Counter > 0; }

        public IDisposable Set()
        {
            Counter++;
            return this;
        }

        public void Unset()
        {
            if (Counter != 0)
            {
                Counter--;
            }
        }

        public void Dispose()
        {
            Unset();
        }
    }
}
