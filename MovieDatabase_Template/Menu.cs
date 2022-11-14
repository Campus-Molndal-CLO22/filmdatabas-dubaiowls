using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MovieDatabase;
using MovieDatabase_Template;
using MySql.Data.MySqlClient;

namespace MovieDatabase_Template
{
    public static class Menu
    {

        public static void RunProgram()
        {
            Console.SetWindowSize(150, 40);
            MovieCrud SQLHandler = new(@"Server=ns8.inleed.net;Database=s60127_DubaiOwls;Uid=s60127_Alexander;Pwd=fn3OcaNLnC9SEuBe");

            int choice = 0;
            while (true)
            {
                Console.Clear();
                DisplayMenu();
                Console.Write("Enter choice: ");
                int.TryParse(Console.ReadLine(), out choice);
                switch (choice)
                {
                    case 1:
                        Movie movie = Movie.CreateMovie();
                        SQLHandler.AddMovie(movie);
                        break;
                    case 2:
                        Actor actor = Actor.CreateActor();
                        SQLHandler.AddActor(actor);
                        break;
                    case 3:
                        SQLHandler.ListMoviesWithActors();
                        Console.ReadLine();
                        break;
                    case 4:
                        SQLHandler.SearchSpecificMovie();
                        Console.ReadLine();
                        break;
                    case 5: 
                        SQLHandler.MovieSearchWithActor();
                        Console.ReadLine();
                        break;
                    case 6:
                        SQLHandler.DisplayAllActors();
                        Console.ReadLine();
                        break;
                    case 7:
                        SQLHandler.SearchSpecificActor();
                        Console.ReadLine();
                        break;
                    case 8:
                        Environment.Exit(0);
                        break;
                    default:
                        Console.WriteLine("Invalid choice");
                        Console.ReadLine();
                        break;

                }

            }
        }

        static void DisplayMenu()
        {
            Console.WriteLine("1. Add movie to the database");
            Console.WriteLine("2. Add actor to the database");
            Console.WriteLine("3. Display all movies existing in the database");
            Console.WriteLine("4. Search for specific movie");
            Console.WriteLine("5. Search for a movie containing specific actor");
            Console.WriteLine("6. Display all actors in the database");
            Console.WriteLine("7. Search for a specific actor");
            Console.WriteLine("8. Exit program");
        }



    }
}
