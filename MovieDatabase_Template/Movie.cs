namespace MovieDatabase
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Numerics;
    using System.Text;
    using System.Threading.Tasks;

    public class Movie
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public int Year { get; set; }
        public string Genre { get; set; }

        public string IMDB { get; set; }

        // Lägg till fler properties
        public List<string> Actors { get; set; }

        public Movie()
        {

        }
        public static Movie CreateMovie()
        {
            string title = "";
            string genre = "";
            int year = 0;
            string imdb = "";
            while (true)
            {
                Console.Write("Enter the title of the movie: ");
                title = Console.ReadLine();
                Console.Write("Enter the release year of the movie: ");
                year = int.Parse(Console.ReadLine());
                Console.Write("Enter the genre of the movie: ");
                genre = Console.ReadLine();
                Console.Write("Enter the IMDB-link: ");
                imdb = Console.ReadLine();

                if (title.Length < 1 || genre.Length < 1 || year < 1 || imdb.Length < 1)
                    Console.WriteLine("Make sure all inputs are correct");
                else
                    break;

            }

            return new Movie { Title = title, Year = year, Genre = genre, IMDB = imdb };
        }
    }

}
