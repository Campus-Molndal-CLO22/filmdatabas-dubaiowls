namespace MovieDatabase
{
    public class Actor
    {
        public int Id { get; set; }
        public string Name;
        public int Age { get; set; }
        public int BornYear { get; set; }
        public int MovieId { get; set; }

        public Actor()
        {
            Console.WriteLine("Enter the name of the actor: ");
            Name = Console.ReadLine();
            Console.WriteLine("Enter the age of the actor: ");
            Age = int.Parse(Console.ReadLine());
            Console.WriteLine();
        }
    }
}
