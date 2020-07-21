using FluentAssertions;
using HistoryCollection.Helpers;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HistoryCollection.Tests.Helpers
{
    [TestClass]
    public class SimpleMonitorTests
    {
        [TestMethod]
        public void Constructor_should_initialize_object_with_zeroed_counter()
        {
            var monitor = new SimpleMonitor();

            monitor.Counter.Should().Be(0);
        }

        [TestMethod]
        public void Set_should_increment_counter()
        {
            var monitor = new SimpleMonitor();

            monitor.Set();

            monitor.Counter.Should().Be(1);
        }

        [TestMethod]
        public void Unset_should_decrement_counter()
        {
            var monitor = new SimpleMonitor();

            monitor.Set();

            monitor.Unset();

            monitor.Counter.Should().Be(0);
        }

        [TestMethod]
        public void Unset_should_not_decrement_counter_if_counter_is_zeroed()
        {
            var monitor = new SimpleMonitor();

            monitor.Unset();

            monitor.Counter.Should().Be(0);
        }

        [TestMethod]
        public void Dispose_shoud_decrement_counter()
        {
            var monitor = new SimpleMonitor();

            monitor.Set();

            monitor.Dispose();

            monitor.Counter.Should().Be(0);
        }

        [TestMethod]
        public void Dispose_should_not_decrement_counter_if_counter_is_zeroed()
        {
            var monitor = new SimpleMonitor();

            monitor.Dispose();

            monitor.Counter.Should().Be(0);
        }

        [TestMethod]
        public void IsSet_should_return_true_if_counter_is_more_than_zero()
        {
            var monitor = new SimpleMonitor();

            monitor.Set();

            monitor.IsSet.Should().BeTrue();
        }

        [TestMethod]
        public void IsSet_should_return_false_if_counter_is_zero()
        {
            var monitor = new SimpleMonitor();

            monitor.IsSet.Should().BeFalse();
        }
    }
}
