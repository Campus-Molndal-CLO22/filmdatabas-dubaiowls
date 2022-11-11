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
        public string Actors { get; set; }

        public Movie()
        {

        }
    }

    public class Actor
    {
        public int Id { get; set; }
        public string Name;
        public int BornYear { get; set; }
    }
}
