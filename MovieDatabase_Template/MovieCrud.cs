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
        MySqlConnection cnn = null;
        MySqlDataAdapter adt = new();
        DataTable dt = new ();
        string sql = "";

        public MovieCrud(string connString)
        {
            cnn = new MySqlConnection(connString);
            cnn.Open();
        }

        public void AddMovie(Movie movie)
        {

            dt = new DataTable();
            sql = $"SELECT Titel FROM Movie WHERE Titel = '{movie.Title}'";
            adt = new MySqlDataAdapter(sql, cnn);
            adt.Fill(dt);

            if (dt.Rows.Count > 0)      // Kolla om filmen redan finns, uppdatera i så fall
            {
                Console.WriteLine("Den filmen finns redan i databasen");
                foreach(DataRow row in dt.Rows)
                {
                    Console.WriteLine($"{row["\"Title\",\"Year\",\"Genre\",\"IMDB\""]}");
                }
            }
            else                        // Om inte, lägg till filmen i databasen
            {
            sql = $"INSERT INTO `Movie`(`Titel`, `Year`, `Genre`,`IMDB` ) " +
                  $"VALUES('{movie.Title}','{movie.Year}','{movie.Genre}','{movie.IMDB}')";

            var dt = new DataTable();

            sql = $"INSERT INTO " +
                         $"           `Movie`" +
                         $"                (`Titel`," +
                         $"                 `Year`, " +
                         $"                 `Genre`, " +
                         $"                 `Actors`," +
                         $"                 `IMDB` ) " +
                         $"VALUES(" +
                         $"       '{movie.Title}'," +
                         $"       '{movie.Year}'," +
                         $"       '{movie.Genre}'," +
                         $"       '{movie.Actors}', " +
                         $"       '{movie.IMDB}'))";

            var cmd = new MySqlCommand(sql, cnn);
            var adt = new MySqlDataAdapter(sql, cnn);
            

            adt.Fill(dt);


                dt = new DataTable();
                adt = new MySqlDataAdapter(sql, cnn);
                adt.Fill(dt);
            }
            
            
            //Actor actor = new() { Name = "Chris Pratt", Age = 58, BornYear = 1963, Movies = "Fight Club\nThe Big Short" };

            //AddActor(actor);
            // Lägg till skådespelarna i databasen
            //AddActorToMovie(actor, movie);
            // Lägg till relationen mellan filmen och skådespelarna i databasen
            
        }

        public void AddActor(Actor actor)
        {
            sql = $"INSERT INTO `Actor`(`Name`, `Age`, `BornYear`)" +
                  $"VALUES ('{actor.Name}','{actor.Age}', '{actor.BornYear}')";

            dt = new DataTable();
            adt = new MySqlDataAdapter(sql, cnn);
            adt.Fill(dt);
            // Kolla om skådespelaren finns i databasen
            // Uppdatera i så fall annars
            // Lägg till skådespelaren i databasen
        }

        public void AddActorToMovie(Actor actor, Movie movie)
        {
            string GetNameFromList(List<Actor> actors)
            {
                string names = "";

                foreach (Actor name in actors)
                {
                    names +=(name.Name) + "\n";
                }
                return names;
            }
            
            sql = $"UPDATE `Movie` SET `Actors` = '{GetNameFromList(movie.Actors)}' WHERE Movie.Titel ='{movie.Title}'";
            //UPDATE `Movie` SET `Id`= '[value-1]',`Titel`= '[value-2]',`Year`= '[value-3]',`Genre`= '[value-4]',`Actors`= '[value-5]',`IMDB`= '[value-6]

            var dt = new DataTable();
            var adt = new MySqlDataAdapter(sql, cnn);
            adt.Fill(dt);
            // Kolla om relationen finns i databasen, i så fall är du klar
            // Annars lägg till relationen mellan filmen och skådespelaren i databasen

        }
        public void GetMovies()
        {
            // Hämta alla filmer från databasen
            dt = new DataTable();
            sql = "SELECT * " +
                    "FROM Movie";
            adt = new MySqlDataAdapter(sql, cnn);
            adt.Fill(dt);

            foreach(DataRow row in dt.Rows)
            {
                Console.WriteLine($"{row["Title"]}, {row["Year"]}, {row["Genre"]}, {row["IMDB"]}");
            }
            // Hämta alla skådespelare från databasen
            // Hämta alla relationer mellan filmer och skådespelare från databasen
            // Skapa en lista med filmer
            // Lägg till skådespelarna till filmerna
            // Returnera listan med filmer
        }

        /*
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

        public void DeleteMove(int moveId)
        {
            // Ta bort filmen från databasen
            // Ta bort alla relationer mellan filmen och skådespelarna från databasen
        }
        */
    }
}
