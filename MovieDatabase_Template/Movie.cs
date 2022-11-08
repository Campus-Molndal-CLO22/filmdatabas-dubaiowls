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

        // Lägg till fler properties
        public List<Actor> Actors { get; set; }
    }

}
