namespace MovieDatabase
{
    public class Actor
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Age { get; set; }
        public int BirthYear { get; set; }
        public int MovieId { get; set; }
        public List<Movie> Movies { get; set; }

        public Actor()
        {

        }

        public static Actor CreateActor()
        {
            string name = "";
            int age = 1;
            int birthYear = 1;
            while (true)
            {
                Console.Write("Enter the name of the actor: ");
                name = Console.ReadLine();
                Console.Write("Enter the age of the actor: ");
                int.TryParse(Console.ReadLine(), out age);
                Console.Write("Enter the birth year of the actor: ");
                int.TryParse(Console.ReadLine(), out birthYear);

                if (name.Length < 1 || age < 1 || birthYear < 1)
                    Console.WriteLine("Make sure all inputs are correct");
                else break;
            }

            return new Actor { Name = name, Age = age, BirthYear = birthYear };

        }
    }
}
