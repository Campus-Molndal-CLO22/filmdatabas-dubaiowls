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

            var dt = new DataTable();
            var adt = new MySqlDataAdapter(sql, cnn);
            adt.Fill(dt);


            sql = $"SELECT * FROM `Movie` WHERE `Title` = '{movie.Title}' AND `Year` = '{movie.Year}'";
            cmd = new MySqlCommand(sql, cnn);
            MySqlDataReader rdr = cmd.ExecuteReader();
            if (rdr.HasRows)
            {
                Console.WriteLine("Movie already exists in database");
                Console.ReadLine();
                cnn.Close();
            }
            else
            {
                rdr.Close();
                sql = $"INSERT INTO `Movie`(`Title`, `Year`, `Genre`, `Actors`,`IMDB` ) VALUES('{movie.Title}','{movie.Year}','{movie.Genre}','{movie.Actors}', '{movie.IMDB}')";
                cmd = new MySqlCommand(sql, cnn);
                cmd.ExecuteNonQuery();
                Console.WriteLine("Movie added to database");
                Console.ReadLine();
                cnn.Close();
            }

            
            movie.Id = int.Parse(reader.ToString());

            return movie.Id;
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
            string klarellerinte = "";
            while (klarellerinte != "KLAR")
            {
                Actor actor = Actor.CreateActor();
                
                AddActor(actor); // Lägg till skådespelarna i databasen
                AddActorToMovie(actor, movie); // Lägg till relationen mellan filmen och skådespelarna i databasen

                Console.WriteLine("Skriv KLAR ifall du inte vill lägga till fler skådespelare. Annars klicka på valfri knapp");
                klarellerinte = Console.ReadLine();
            }


        }

        public void AddActor(Actor actor)
        {

            cnn.Open();
            string sql = $"SELECT * FROM `Actor` WHERE `Name` = '{actor.Name}' AND `BornYear` = '{actor.BirthYear}'";
            MySqlCommand cmd = new MySqlCommand(sql, cnn);
            MySqlDataReader rdr = cmd.ExecuteReader();
            if (rdr.HasRows)
            {
                Console.WriteLine("Actor already exists in database");
                cnn.Close();
            }
            else
            {
                rdr.Close();
                sql = $"INSERT INTO `Actor`(`Name`, `Age`, `BornYear`, `Movies`) VALUES ('{actor.Name}','{actor.Age}', '{actor.BirthYear}','{actor.Movies}')";
                cmd = new MySqlCommand(sql, cnn);
                cmd.ExecuteNonQuery();
                Console.WriteLine("Actor added to database");
                cnn.Close();
            }


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

                cmd = new MySqlCommand(insertActorSql, cnn);
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
        public void ListMoviesWithActors()
        {
            string sql = $"SELECT ActorsInMovie.Title, Actors, Movie.Year, Genre, Imdb FROM ActorsInMovie, Movie " +
                         $"WHERE ActorsInMovie.M_Id = Movie.Id " +
                         $"ORDER BY Title";
            var dt = new DataTable();
            var adt = new MySqlDataAdapter(sql, cnn);
            adt.Fill(dt);
            if (dt.Rows.Count > 0)
            {
                Console.WriteLine($"Movie Titles                        : Actors               : Release Year   : Genre        : Imdb");
                Console.WriteLine($"************************************:**********************:****************:**************:******");
                foreach (DataRow row in dt.Rows)
                {
                    Console.WriteLine($"{row["Title"],-35} : {row["Actors"],-20} : {row["Year"],-14} : {row["Genre"],-12} : {row["Imdb"]}");
                    Console.WriteLine($"------------------------------------:----------------------:----------------:--------------:------");
                }
            }
            else Console.WriteLine("There are no movies in the database yet!");
                    
                    
        }
        /*
        public List<Movie> GetMovies()
        {
            // Hämta alla filmer från databasen
            // Hämta alla skådespelare från databasen
            // Hämta alla relationer mellan filmer och skådespelare från databasen
            // Skapa en lista med filmer
            // Lägg till skådespelarna till filmerna
            // Returnera listan med filmer
        }

        public List<Movie> GetMoviesContaining(string search)
        {
            // Hämta alla matchande filmer från databasen
            // Hämta alla relationer mellan filmer och skådespelare från databasen
            // Hämta alla relaterade skådespelare från databasen
            // Skapa en lista med filmer
            // Lägg till skådespelarna till filmerna
            // Returnera listan med filmer
        }

        public List<Movie> GetMoviesFromYear(int year)
        {
            // Hämta alla matchande filmer från databasen
            // Hämta alla relationer mellan filmer och skådespelare från databasen
            // Hämta alla relaterade skådespelare från databasen
            // Skapa en lista med filmer
            // Lägg till skådespelarna till filmerna
            // Returnera listan med filmer
        }

        public List<Movie> GetMovie(int Id)
        {
            // Hämta matchande film från databasen
            // Hämta alla relationer mellan filmer och skådespelare från databasen
            // Hämta alla relaterade skådespelare från databasen
            // Skapa en lista med filmer
            // Lägg till skådespelarna till filmerna
            // Returnera listan med filmer
        }

        public List<Movie> GetMovie(string name)
        {
            // Hämta matchande film från databasen
            // Hämta alla relationer mellan filmer och skådespelare från databasen
            // Hämta alla relaterade skådespelare från databasen
            // Skapa en lista med filmer
            // Lägg till skådespelarna till filmerna
            // Returnera listan med filmer
        }


        public List<Actor> GetActors()
        {
            // Hämta alla skådespelare från databasen
            // Hämta alla relationer mellan filmer och skådespelare från databasen
            // Hämta alla matchande filmer från databasen
            // Skapa en lista med skådespelare
            // Lägg till filmerna till skådespelarna
            // Returnera listan med skådespelare
        }

        public List<Actor> GetActorsInMovie(Movie movie)
        {
            // Hämta alla skådespelare från databasen
            // Hämta alla relationer mellan filmer och skådespelare från databasen
            // Hämta alla matchande filmer från databasen
            // Skapa en lista med skådespelare
            // Lägg till filmerna till skådespelarna
            // Returnera listan med skådespelare
        }

        public List<Movie> GetMoviesWithActor(Actor actor)
        {
            // Hämta alla skådespelare från databasen
            // Hämta alla relationer mellan filmer och skådespelare från databasen
            // Hämta alla matchande filmer från databasen
            // Skapa en lista med skådespelare
            // Lägg till filmerna till skådespelarna
            // Returnera listan med skådespelare
        }

        public List<Movie> GetMoviesWithActor(string actorName)
        {
            // Hämta alla skådespelare från databasen
            // Hämta alla relationer mellan filmer och skådespelare från databasen
            // Hämta alla matchande filmer från databasen
            // Skapa en lista med skådespelare
            // Lägg till filmerna till skådespelarna
            // Returnera listan med skådespelare
        }

        public List<Movie> GetMoviesWithActor(int actorId)
        {
            // Hämta alla skådespelare från databasen
            // Hämta alla relationer mellan filmer och skådespelare från databasen
            // Hämta alla matchande filmer från databasen
            // Skapa en lista med skådespelare
            // Lägg till filmerna till skådespelarna
            // Returnera listan med skådespelare
        }

        public void DeleteActor(int actorId)
        {
            // Ta bort skådespelaren från databasen
            // Ta bort alla relationer mellan skådespelaren och filmerna från databasen
        }


        public void DeleteMovie(int movieId)

        public void DeleteMovie(int moveId)

        {
            // Ta bort filmen från databasen
            // Ta bort alla relationer mellan filmen och skådespelarna från databasen
        }
        */
    }
}
