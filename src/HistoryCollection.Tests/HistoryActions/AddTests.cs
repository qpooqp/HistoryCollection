using FluentAssertions;
using HistoryCollection.HistoryActions;
using HistoryCollection.Tests.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HistoryCollection.Tests.HistoryActions
{
    [TestClass]
    public class AddTests
    {
        [TestMethod]
        public void Action_should_return_NotifyCollectionChangedAction_Add()
        {
            var action = new Add<Foo>(new Foo(), 0);

            action.Action.Should().Be(NotifyCollectionChangedAction.Add);
        }

        [TestMethod]
        public void Do_should_add_item_to_provided_history_collection()
        {
            var collection = new HistoryCollection<Foo>();

            var item = Foo.Create(1);
            var action = new Add<Foo>(item, 0);

            action.Do(collection);

            collection.Count.Should().Be(1);
            collection[0].Should().Be(item);
        }

        [TestMethod]
        public void Undo_should_remove_previously_added_item_from_provided_history_collection()
        {
            var collection = new HistoryCollection<Foo>();

            var item = Foo.Create(1);
            var action = new Add<Foo>(item, 0);

            action.Do(collection);
            action.Undo(collection);

            collection.Count.Should().Be(0);
        }
    }
}
