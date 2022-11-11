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
        //string connString = "";
        MySqlConnection cnn = null;


        public MovieCrud(string connString)
        {
            var connection = new MySqlConnection(connString);
            cnn = connection;


            cnn.Open();
        }
        public int GetActorId(Actor actor)
        {
            string sql = $"SELECT Id FROM Actor WHERE Name='{actor.Name}' AND Age='{actor.Age}'";

            var cmd = new MySqlCommand(sql, cnn);

            var reader = cmd.ExecuteScalar();

            actor.Id = int.Parse(reader.ToString());

            return actor.Id;
        }
        public int GetMovieId(Movie movie)
        {
            string sql = $"SELECT Id FROM Movie WHERE Title='{movie.Title}' AND Year='{movie.Year}'";

            var cmd = new MySqlCommand(sql, cnn);

            var reader = cmd.ExecuteScalar();

            movie.Id = int.Parse(reader.ToString());

            return movie.Id;
        }
        public void AddMovie(Movie movie)
        {
            // Kolla om filmen redan finns, uppdatera i så fall
            string CheckIfExist = $"SELECT Title FROM Movie WHERE Title = '{movie.Title}'";
            var dt = new DataTable();
            var adt = new MySqlDataAdapter(CheckIfExist, cnn);
            adt.Fill(dt);



            if (dt.Rows.Count > 0)
            {
                Console.WriteLine("Den filmen finns redan i databasen");             
            }
            else // Om inte, lägg till filmen i databasen
            {
                string sql = $"INSERT INTO `Movie`(`Title`, `Year`, `Genre`,`IMDB` ) VALUES('{movie.Title}','{movie.Year}','{movie.Genre}','{movie.IMDB}')";

                dt = new DataTable();
                adt = new MySqlDataAdapter(sql, cnn);
                adt.Fill(dt);

                //Actor actor = new() { Name = "Schulze Oscar", Age = 58, BornYear = 1963 };
                //AddActorToMovie(actor, movie);

            }
            string klarellerinte = "";
            while (klarellerinte != "KLAR")
            {
                Actor actor = new Actor();
                actor = actor.CreateActor();
                AddActor(actor);
                AddActorToMovie(actor, movie);

                Console.WriteLine("Vill du lägga till fler skådespelare till filmen? Skriv KLAR ifall du inte vill lägga till fler");
                klarellerinte = Console.ReadLine();
            }

            






            //AddActor(actor);
            // Lägg till skådespelarna i databasen
            //AddActorToMovie(actor, movie);
            // Lägg till relationen mellan filmen och skådespelarna i databasen

        }

        public void AddActor(Actor actor)
        {
            string CheckIfActorExist = $"SELECT * FROM Actor WHERE Name='{actor.Name}' AND Age='{actor.Age}'";

            var dt = new DataTable();
            var adt = new MySqlDataAdapter(CheckIfActorExist, cnn);
            adt.Fill(dt);

            if (dt.Rows.Count > 0)
            {
                Console.WriteLine("This actor already exist in the database");
            }
            else
            {
                string insertActorSql = $"INSERT INTO `Actor`(`Name`, `Age`, `BornYear`) VALUES ('{actor.Name}','{actor.Age}','{actor.BornYear}')";

                var cmd = new MySqlCommand(insertActorSql, cnn);

                cmd.ExecuteNonQuery();
            }



            // Kolla om skådespelaren finns i databasen
            // Uppdatera i så fall annars
            // Lägg till skådespelaren i databasen
        }

        public void AddActorToMovie(Actor actor, Movie movie)
        {

            // Kolla om skådespelaren redan finns
            string CheckIfExist = $"SELECT * FROM ConnectionTable WHERE ActorId='{GetActorId(actor)}' AND MovieId='{GetMovieId(movie)}'";
            var dt = new DataTable();
            var adt = new MySqlDataAdapter(CheckIfExist, cnn);
            adt.Fill(dt);

            if (dt.Rows.Count > 0)
            {

                Console.WriteLine("The Connection already exist");

            }
            else // Om inte, lägg till filmen i databasen
            {
                string sql = $"INSERT INTO `ConnectionTable`(`MovieId`,`ActorId`) VALUES ('{GetMovieId(movie)}','{GetActorId(actor)}')";
                var cmd = new MySqlCommand(sql, cnn);
                cmd.ExecuteNonQuery();


            }




            // Kolla om relationen finns i databasen, i så fall är du klar
            // Annars lägg till relationen mellan filmen och skådespelaren i databasen

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

        public void DeleteMove(int moveId)
        {
            // Ta bort filmen från databasen
            // Ta bort alla relationer mellan filmen och skådespelarna från databasen
        }
        */
    }
}
