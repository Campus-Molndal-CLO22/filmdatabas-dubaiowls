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
            MovieCrud SQLHandler = new(ConnectionString());

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
        static string ConnectionString()
        {
            string connection = "";
            Console.WriteLine("Vad heter din användarprofil på datorn? (exempel: 'C:\\Users\\Bosse Bossesson\\'  innebär att du skriver bara 'Bosse Bossesson'.)");
            string Användare = Console.ReadLine();
            try
            {
                string[] filesTEST = Directory.GetFiles(@"C:\Users\" + Användare + @"\Desktop\",  
                "loginSQL123.txt", SearchOption.AllDirectories);
                StreamReader loginSQLTEST = new StreamReader(path: filesTEST[0]);
            }
            catch
            {
                Console.WriteLine("Error: Press enter to exit program.");              
                Console.ReadLine();
                Environment.Exit(0);
            }
            string[] files = Directory.GetFiles(@"C:\Users\" + Användare + @"\Desktop\",  //letar igenom Desktop & alla dess subfolders efter filen loginSQL123.txt, kunde inte ha högre upp i mappstrukturen även med admin-rättigheter då jag skulle vart tvungen att implementera try & catch för folders som är o-accessbara via visual studio.

            "loginSQL123.txt", SearchOption.AllDirectories);
            StreamReader loginSQL = new StreamReader(path: files[0]);

            connection = @"Server=ns8.inleed.net;Database=s60127_DubaiOwls;" + loginSQL.ReadToEnd();
            return connection;
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
