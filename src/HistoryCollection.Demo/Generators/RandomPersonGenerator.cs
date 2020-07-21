using HistoryCollection.Demo.Models;

namespace HistoryCollection.Demo.Generators
{
    public static class RandomPersonGenerator
    {
        private static int _nextId;

        public static Person Generate()
        {
            return new Person
            {
                Id = _nextId++,
                Name = RandomNameGenerator.Generate(),
                Profession = RandomProfessionGenerator.Generate()
            };
        }
    }
}
