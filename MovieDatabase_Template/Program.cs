using MovieDatabase;
using MovieDatabase_Template;


MovieCrud SQLHandler = new(@"Server=ns8.inleed.net;Database=s60127_DubaiOwls;Uid=s60127_Alexander;Pwd=fn3OcaNLnC9SEuBe");
Actor actor = new() { Name = "Brad Pitt", Age = 58, BornYear = 1963, Movies = "Fight Club\nThe Big Short"};


Console.WriteLine("Menuchoices: ");
Console.WriteLine("1. Add Movie, Actor & connection.\n2. List all movies in the Database. \n3. List all Actors in the Database. \n4. Delete Movie.\n5. Delete Actor.");
int MenuChoice = 0;

while (MenuChoice == 0 && (!int.TryParse(Console.ReadLine(), out MenuChoice) || MenuChoice <= 0 || MenuChoice >= 6))
    {
    MenuChoice = 0;
    Console.WriteLine("You must select a number corresponding to 1 of the menu choices!");
    }

switch (MenuChoice)
{
    case 1:
        Console.Clear();       
        Console.WriteLine("What is the Movie Title? :"); var TitleInput = Console.ReadLine(); Console.WriteLine("What year did the movie release? :"); int YearInput = Convert.ToInt32(Console.ReadLine()); Console.WriteLine("What genre does the movie belong to? :"); var GenreInput = Console.ReadLine(); Console.WriteLine("What Actors have a role in the movie? :"); var ActorsInput = Console.ReadLine(); Console.WriteLine("What is the IMDB link? :"); var IMDBInput = Console.ReadLine();
        Movie film = new() {Title = TitleInput, Year = YearInput, Genre = GenreInput, Actors = ActorsInput, IMDB = IMDBInput };
        SQLHandler.AddMovie(film);
        break;

    case 2:
        Console.Clear();
        //SQLHandler.GetMovie();
        break;

    case 3:
        Console.Clear();
      //  SQLHandler.GetActors();
        break;

    case 4:
        Console.Clear();
    //    SQLHandler.DeleteMovie();
        break;

    case 5:
        Console.Clear();
     //   SQLHandler.DeleteActor();
        break;

}    /* cnn.close(); */

// Börja med att lägga till Nuget för MySQL
// Sen kan ni kolla på koden ;)

// Uppgift:
// Skapa en eller flera tabeller som ska
// innehålla information om filmer och skådespelare

