using MovieDatabase;
using MovieDatabase_Template;

MovieCrud SQLHandler = new(@"Server=ns8.inleed.net;Database=s60127_DubaiOwls;Uid=s60127_Alexander;Pwd=fn3OcaNLnC9SEuBe");
Movie movie = new() { Title = "Star Wars 3", Year = 1980, Genre = "Sci-Fi", IMDB = "ABC" };
Actor actor = new();

//Actor actor = new() { Name = "Brad Pitt", Age = 58, BornYear = 1963, Movies = "Fight Club\nThe Big Short"};

SQLHandler.AddMovie(movie);
SQLHandler.GetMovies();
//SQLHandler.AddActor(actor);

// Börja med att lägga till Nuget för MySQL
// Sen kan ni kolla på koden ;)

// Uppgift:
// Skapa en eller flera tabeller som ska
// innehålla information om filmer och skådespelare

