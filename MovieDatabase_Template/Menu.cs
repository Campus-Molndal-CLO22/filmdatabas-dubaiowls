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

            
            while (true)
            {
                Console.Clear();
                DisplayMenu();
                
                switch (Choice(1,6))
                {
                    case 1:                
                        Movie movie = Movie.CreateMovie();
                        SQLHandler.AddMovie(movie);
                        break;
                    case 2:
                        Actor actor = Actor.CreateActor();
                        SQLHandler.AddActor(actor);
                        Console.ReadLine();
                        break;
                    case 3:
                        SQLHandler.DisplayAllMovies();
                        Console.ReadLine();
                        break;                                     
                    case 4:
                        SQLHandler.DisplayAllActors();
                        Console.ReadLine();
                        break;
                    case 5:
                        SearchFunctions(SQLHandler);
                        Console.ReadLine();
                        break;
                    case 6:
                        Environment.Exit(0);
                        break;
                    

                }

            }
        }
        static string ConnectionString()
        {
            
            string Användare = "";
            bool Correct = false;
            while (!Correct)
            {
                
                Console.WriteLine("Vad heter din användarprofil på datorn? (exempel: 'C:\\Users\\Bosse Bossesson\\'  innebär att du skriver bara 'Bosse Bossesson'.)");
                Användare = Console.ReadLine();
                try
                {
                    string[] filesTEST = Directory.GetFiles(@"C:\Users\" + Användare + @"\Desktop\",
                    "loginSQL123.txt", SearchOption.AllDirectories);
                    StreamReader loginSQLTEST = new StreamReader(path: filesTEST[0]);
                    Correct = true;
                }
                catch
                {
                    Console.WriteLine("Kontrollera att du skrev in rätt information");
                    
                    //Console.WriteLine("Error: Press enter to exit program.");              
                    // Console.ReadLine();
                    //Environment.Exit(0);
                }
            }
            
            string[] files = Directory.GetFiles(@"C:\Users\" + Användare + @"\Desktop\",  //letar igenom Desktop & alla dess subfolders efter filen loginSQL123.txt, kunde inte ha högre upp i mappstrukturen även med admin-rättigheter då jag skulle vart tvungen att implementera try & catch för folders som är o-accessbara via visual studio.

            "loginSQL123.txt", SearchOption.AllDirectories);
            StreamReader loginSQL = new StreamReader(path: files[0]);

            string connection = @"Server=ns8.inleed.net;Database=s60127_DubaiOwls;" + loginSQL.ReadToEnd();
            return connection;
        }
        static void DisplayMenu()
        {
            Console.WriteLine("1. Add movie to the database");
            Console.WriteLine("2. Add actor to the database");
            Console.WriteLine("3. Display all movies existing in the database");
            Console.WriteLine("4. Display all actors in the database");
            Console.WriteLine("5. Search functions");
            Console.WriteLine("6. Exit program");
            
            
        }

        static void SearchFunctions(MovieCrud SqlHandler)
        {
            
            Console.WriteLine("1. Search movie by title");
            Console.WriteLine("2. Search for a movie with a specific actor");
            Console.WriteLine("3. Search for an actor in the database");
            Console.WriteLine("4. Search for a specific Genre");
            Console.WriteLine("5. Back to main menu");

            switch (Choice(1, 5))
            {
                case 1:
                    SqlHandler.SearchSpecificMovie();
                    break;
                case 2: 
                    SqlHandler.MovieSearchWithActor();
                    break;
                case 3:
                    SqlHandler.SearchActor();
                    break;
                case 4: SqlHandler.SearchGenre();
                    break;
                case 5:
                    break;
                    
            }
        }

        static int Choice(int min, int max)
        {
            int choice;

            while (true)
            {
                Console.Write("Enter choice: ");
                string input = Console.ReadLine();


                int.TryParse(input, out choice);

                if (choice < min || choice > max)
                {
                    Console.WriteLine("Invalid choice");
                }
                else
                {
                    return choice;
                }
            }
        }

    }
}
