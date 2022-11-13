using MovieDatabase;
using MovieDatabase_Template;

MovieCrud SQLHandler = new(@"Server=ns8.inleed.net;Database=s60127_DubaiOwls;Uid=s60127_Alexander;Pwd=fn3OcaNLnC9SEuBe");
Movie film = new() { Title = "Schulzes Revenge 80", Year = 2022, Genre = "Romantisk", IMDB = "Top1Ever" };


SQLHandler.AddMovie(film);


// Börja med att lägga till Nuget för MySQL
// Sen kan ni kolla på koden ;)

// Uppgift:
// Skapa en eller flera tabeller som ska
// innehålla information om filmer och skådespelare

