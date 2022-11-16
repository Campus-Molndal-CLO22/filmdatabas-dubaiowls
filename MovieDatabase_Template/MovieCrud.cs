namespace MovieDatabase_Template
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using MovieDatabase;
    using MySql.Data.MySqlClient;
    using System.Data;

    public class MovieCrud
    {
        string connString = "";
        MySqlConnection cnn = null;


        public MovieCrud(string connString)
        {
            var connection = new MySqlConnection(connString);
            cnn = connection;
            cnn.Open();
        }


        public void AddMovie(Movie movie)
        {
            // Kolla om filmen redan finns, uppdatera i så fall
            string CheckIfExist = $"SELECT Id FROM Movie WHERE Title ='{movie.Title}' AND Year ='{movie.Year}' AND Genre ='{movie.Genre}'";
            var dt = new DataTable();
            var adt = new MySqlDataAdapter(CheckIfExist, cnn);
            adt.Fill(dt);

            if (dt.Rows.Count > 0) // Om man får tillbaka något betyder det att filmen redan finns i databasen
            {
                Console.WriteLine($"The movie {movie.Title} already exist in the database");             
            }
            else // Om inte, lägg till filmen i databasen
            {
                string sql = $"INSERT INTO `Movie`(`Title`, `Year`, `Genre`,`IMDB` ) VALUES('{movie.Title}','{movie.Year}','{movie.Genre}','{movie.IMDB}')";

                dt = new DataTable();
                adt = new MySqlDataAdapter(sql, cnn);
                adt.Fill(dt);

                Console.WriteLine($"The movie {movie.Title} was added to the database");
            }

            Console.WriteLine("Now its time to add some actors to the movie!");
            string addMoreActorsOrStop = "";
            while (addMoreActorsOrStop != "STOP")
            {
                
                Actor actor = Actor.CreateActor();
                
                AddActor(actor); // Lägg till skådespelarna i databasen
                AddActorToMovie(actor, movie); // Lägg till relationen mellan filmen och skådespelarna i databasen

                Console.WriteLine("If you're finished adding actors to the movie, write STOP. Else press a key of your choice to continue adding actors!");
                addMoreActorsOrStop = Console.ReadLine();
            }


        }
        public void AddActor(Actor actor)
        {

            // Kolla om skådespelaren finns i databasen
            string CheckIfActorExist = $"SELECT * FROM Actor WHERE Name ='{actor.Name}' AND Age ='{actor.Age}'";

            var dt = new DataTable();
            var adt = new MySqlDataAdapter(CheckIfActorExist, cnn);
            adt.Fill(dt);

            if (dt.Rows.Count > 0) // Om skådespelaren redan finns i databasen
            {
                Console.WriteLine("This actor already exist in the database");
            }
            else // Lägg till skådespelaren i databasen om den inte redan finns
            {
                string insertActorSql = $"INSERT INTO `Actor`(`Name`, `Age`, `BirthYear`) VALUES ('{actor.Name}','{actor.Age}','{actor.BirthYear}')";

                var cmd = new MySqlCommand(insertActorSql, cnn);
                cmd.ExecuteNonQuery();
                Console.WriteLine($"The actor {actor.Name} was added to the database");
            }
         
        }
        public void AddActorToMovie(Actor actor, Movie movie)
        {

            // Kolla om relationen finns i databasen, i så fall görs ingenting
            string CheckIfExist = $"SELECT * FROM ConnectionTable WHERE ActorId='{GetActorId(actor)}' AND MovieId='{GetMovieId(movie)}'";
            var dt = new DataTable();
            var adt = new MySqlDataAdapter(CheckIfExist, cnn);
            adt.Fill(dt);

            if (dt.Rows.Count > 0)
            {

                Console.WriteLine("The actor is already listed on this movie");

            }
            else // Om inte, skapa relationen mellan skådespelaren och filmen
            {
                string sql = $"INSERT INTO `ConnectionTable`(`MovieId`,`ActorId`) VALUES ('{GetMovieId(movie)}','{GetActorId(actor)}')";
                var cmd = new MySqlCommand(sql, cnn);
                cmd.ExecuteNonQuery();
                Console.WriteLine($"The connection has been made!");
            }

        }
        public int GetActorId(Actor actor) // Hämtar en skådespelaren id ifrån databasen
        {
            string sql = $"SELECT Id FROM Actor WHERE Name='{actor.Name}' AND Age='{actor.Age}' AND BirthYear ='{actor.BirthYear}'";

            var cmd = new MySqlCommand(sql, cnn);

            var reader = cmd.ExecuteScalar();

            actor.Id = int.Parse(reader.ToString());

            return actor.Id;
        }
        public int GetMovieId(Movie movie) // Hämtar en films id ifrån databasen
        {
            string sql = $"SELECT Id FROM Movie WHERE Title='{movie.Title}' AND Year ='{movie.Year}'";

            var cmd = new MySqlCommand(sql, cnn);

            var reader = cmd.ExecuteScalar();

            
            movie.Id = int.Parse(reader.ToString());

            return movie.Id;
        }

        internal void SearchGenre()
        {
            Console.Write("Enter a Genre: ");
            string search = Console.ReadLine();
            string sql = $"SELECT ActorsInMovie.Title, Actors, Movie.Year, Genre, Imdb FROM ActorsInMovie, Movie " +
                         $"WHERE ActorsInMovie.M_Id = Movie.Id AND Movie.Genre LIKE '%{search}%' " +
                         $"ORDER BY Title";
            var dt = new DataTable();
            var adt = new MySqlDataAdapter(sql, cnn);
            adt.Fill(dt);
            if (dt.Rows.Count > 0)
            {
                Console.WriteLine($"************************************:**********************:****************:**************:**********************************************************");
                Console.WriteLine($"Movie Titles                        : Actors               : Release Year   : Genre        : Imdb");
                Console.WriteLine($"************************************:**********************:****************:**************:**********************************************************");
                foreach (DataRow row in dt.Rows)
                {
                    Console.WriteLine($"{row["Title"],-35} : {row["Actors"],-20} : {row["Year"],-14} : {row["Genre"],-12} : {row["Imdb"]}");
                    Console.WriteLine($"------------------------------------:----------------------:----------------:--------------:----------------------------------------------------------");
                }
            }
            else Console.WriteLine($"No movies could be found using {search}");
        }

        public void DisplayAllMovies()
        {
            string sql = $"SELECT ActorsInMovie.Title, Actors, Movie.Year, Genre, Imdb FROM ActorsInMovie, Movie " +
                         $"WHERE ActorsInMovie.M_Id = Movie.Id " +
                         $"ORDER BY Title";
            var dt = new DataTable();
            var adt = new MySqlDataAdapter(sql, cnn);
            adt.Fill(dt);
            if (dt.Rows.Count > 0)
            {
                Console.WriteLine($"************************************:**********************:****************:**************:**********************************************************");
                Console.WriteLine($"Movie Titles                        : Actors               : Release Year   : Genre        : Imdb");
                Console.WriteLine($"************************************:**********************:****************:**************:**********************************************************");
                foreach (DataRow row in dt.Rows)
                {
                    Console.WriteLine($"{row["Title"],-35} : {row["Actors"],-20} : {row["Year"],-14} : {row["Genre"],-12} : {row["Imdb"]}");
                    Console.WriteLine($"------------------------------------:----------------------:----------------:--------------:----------------------------------------------------------");
                }
            }
            else Console.WriteLine("There are no movies in the database yet!");
                    
                    
        }
        public void SearchSpecificMovie()
        {
            Console.Write("Enter the entire movie title or keywords to find a movie: ");
            string search = Console.ReadLine();
            string sql = $"SELECT ActorsInMovie.Title, Actors, Movie.Year, Genre, Imdb FROM ActorsInMovie, Movie " +
                         $"WHERE ActorsInMovie.M_Id = Movie.Id AND ActorsInMovie.Title LIKE '%{search}%' " +
                         $"ORDER BY Title";
            var dt = new DataTable();
            var adt = new MySqlDataAdapter(sql, cnn);
            adt.Fill(dt);
            if (dt.Rows.Count > 0)
            {
                Console.WriteLine($"************************************:**********************:****************:**************:**********************************************************");
                Console.WriteLine($"Movie Titles                        : Actors               : Release Year   : Genre        : Imdb");
                Console.WriteLine($"************************************:**********************:****************:**************:**********************************************************");
                foreach (DataRow row in dt.Rows)
                {
                    Console.WriteLine($"{row["Title"],-35} : {row["Actors"],-20} : {row["Year"],-14} : {row["Genre"],-12} : {row["Imdb"]}");
                    Console.WriteLine($"------------------------------------:----------------------:----------------:--------------:----------------------------------------------------------");
                }
            }
            else Console.WriteLine($"No movies could be found using {search}");
        }
        public void DisplayAllActors()
        {
            string sql = $"SELECT * FROM Actor " +
                         $"ORDER BY Name";
            var dt = new DataTable();
            var adt = new MySqlDataAdapter(sql, cnn);
            adt.Fill(dt);

            if (dt.Rows.Count > 0)
            {
                Console.WriteLine($"****************:*************:****************");
                Console.WriteLine($"Name            : Age         :Year of Birth");
                Console.WriteLine($"****************:*************:****************");
                foreach (DataRow row in dt.Rows)
                {
                    Console.WriteLine($"{row["Name"],-15} : {row["Age"],-11} : {row["BirthYear"]}");
                    Console.WriteLine($"----------------:-------------:----------------");
                }
                    
            }
            else Console.WriteLine("No actors existing in the database");
        }
        public void MovieSearchWithActor()
        {
            Console.Write("Enter the name of the actor: ");
            string search = Console.ReadLine();
            string sql = $"SELECT ActorsInMovie.Title, Actors, Movie.Year, Genre, Imdb FROM ActorsInMovie, Movie " +
                         $"WHERE ActorsInMovie.M_Id = Movie.Id AND ActorsInMovie.Actors Like '%{search}%' " +
                         $"ORDER BY Title";
            var dt = new DataTable();
            var adt = new MySqlDataAdapter(sql, cnn);
            adt.Fill(dt);
            if (dt.Rows.Count > 0)
            {
                Console.WriteLine($"************************************:**********************:****************:**************:**********************************************************");
                Console.WriteLine($"Movie Titles                        : Actors               : Release Year   : Genre        : Imdb");
                Console.WriteLine($"************************************:**********************:****************:**************:**********************************************************");
                foreach (DataRow row in dt.Rows)
                {
                    Console.WriteLine($"{row["Title"],-35} : {row["Actors"],-20} : {row["Year"],-14} : {row["Genre"],-12} : {row["Imdb"]}");
                    Console.WriteLine($"------------------------------------:----------------------:----------------:--------------:----------------------------------------------------------");
                }
            }
            else Console.WriteLine($"No movies could be found using {search}");
        }
        public void SearchActor()
        {
            Console.Write("Enter the full name of the actor, or enter firstname or lastname:  ");
            string actorName = Console.ReadLine();
            string sql = $"SELECT Name, Age, BirthYear FROM Actor " +
                         $"WHERE Name LIKE '%{actorName}%'";

            var dt = new DataTable();
            var adt = new MySqlDataAdapter(sql, cnn);
            adt.Fill(dt);

            if (dt.Rows.Count > 0)
            {
                Console.WriteLine($"****************:*************:****************");
                Console.WriteLine($"Name            : Age         :Year of Birth");
                Console.WriteLine($"****************:*************:****************");
                foreach (DataRow row in dt.Rows)
                {
                    Console.WriteLine($"{row["Name"],-15} : {row["Age"],-11} : {row["BirthYear"]}");
                    Console.WriteLine($"----------------:-------------:----------------");
                }

            }
            else Console.WriteLine($"No actors found using {actorName}");
        }
        
        public List<Movie> DisplayMovies()
        {
            string sql = "SELECT * FROM ActorsInMovie";
            var dt = new DataTable();
            var adt = new MySqlDataAdapter(sql, cnn);
            adt.Fill(dt);
            List<Movie> filmer = new();
            foreach (DataRow row in dt.Rows)
            {
                Movie movie = new Movie();
                movie.Title = row["Title"].ToString();
                movie.Actors.Add((string)row["Actors"]);
                
                
                
                filmer.Add(movie);
            }

            foreach (Movie movie in filmer)
            {
                Console.WriteLine(movie.Title);
            }

            return filmer;
        }
        
        
        public void DeleteActor(int actorId)
        {
            // Ta bort skådespelaren från databasen
            // Ta bort alla relationer mellan skådespelaren och filmerna från databasen
        }

        public void DeleteMovie(int moveId)
        {
            // Ta bort filmen från databasen
            // Ta bort alla relationer mellan filmen och skådespelarna från databasen
        }
        
    }
}
