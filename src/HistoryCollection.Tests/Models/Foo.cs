using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HistoryCollection.Tests.Models
{
    public class Foo
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public static Foo Create(int id)
        {
            return new Foo
            {
                Id = id,
                Name = id.ToString()
            };
        }
    }
}
