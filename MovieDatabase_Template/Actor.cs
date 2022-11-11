namespace MovieDatabase
{
    public class Actor
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Age { get; set; }
        public int BornYear { get; set; }
        public int MovieId { get; set; }
        public List<Movie> Movies { get; set; }
       
        public Actor()
        {
            
        }

        public Actor CreateActor()
        {
            string name = "";
            int age, bornyear;
            Console.Write("Enter the name of the actor: ");
            name = Console.ReadLine();
            Console.Write("Enter the age of the actor: ");
            age = int.Parse(Console.ReadLine());
            Console.Write("Enter the birth year of the actor: ");
            bornyear = int.Parse(Console.ReadLine());

            return new Actor { Name = name, Age = age, BornYear = bornyear };
            
        }
    }
}
