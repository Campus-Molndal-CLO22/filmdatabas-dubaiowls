namespace MovieDatabase
{
    public class Actor
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Age { get; set; }
        public int BornYear { get; set; }
        public int MovieId { get; set; }
        public List<Movie> Movies { get; set; }
       
        public Actor()
        {

        }
    }
}
