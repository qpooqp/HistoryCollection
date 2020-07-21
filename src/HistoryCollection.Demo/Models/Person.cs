namespace HistoryCollection.Demo.Models
{
    public class Person
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Profession { get; set; }

        public override string ToString()
        {
            return $"{Id,3}: {Name} - {Profession}";
        }
    }
}
