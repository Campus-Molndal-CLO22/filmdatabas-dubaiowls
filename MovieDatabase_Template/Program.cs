using MovieDatabase;
using MovieDatabase_Template;
using Org.BouncyCastle.Bcpg;

Console.WriteLine("Vad heter din användarprofil på datorn? (exempel: 'C:\\Users\\Joe Johnson\\'  innebär att du skriver bara 'Joe Johnson'.)");  
    string Användare = Console.ReadLine();
try { 
    string[] filesTEST = Directory.GetFiles("C:\\Users\\" +Användare+ "\\Desktop\\",  //letar igenom Desktop & alla dess subfolders efter filen loginSQL123.txt, kunde inte ha högre upp i mappstrukturen även med admin-rättigheter då jag skulle vart tvungen att implementera try & catch för folders som är o-accessbara via visual studio.
    "loginSQL123.txt", SearchOption.AllDirectories);
    StreamReader loginSQLTEST = new StreamReader(path: filesTEST[0]);
}
catch
{
    Console.WriteLine("Error: Press enter to exit program.");
    Console.ReadLine();
    Environment.Exit(0);
}
    string[] files = Directory.GetFiles("C:\\Users\\" +Användare+ "\\Desktop\\",  //letar igenom Desktop & alla dess subfolders efter filen loginSQL123.txt, kunde inte ha högre upp i mappstrukturen även med admin-rättigheter då jag skulle vart tvungen att implementera try & catch för folders som är o-accessbara via visual studio.
    "loginSQL123.txt", SearchOption.AllDirectories);
    StreamReader loginSQL = new StreamReader(path: files[0]);
MovieCrud SQLHandler = new(@"Server=ns8.inleed.net;Database=s60127_DubaiOwls;" +loginSQL.ReadToEnd() + ";");


Console.WriteLine("Menuchoices: ");
Console.WriteLine("1. Add a Movie to the Database.\n2. Add an Actor to the Database. \n3. List all movies in the Database. \n4. List all Actors in the Database. \n5. Delete Movie.\n6. Delete Actor.");
int MenuChoice = 0;

while (MenuChoice == 0 && (!int.TryParse(Console.ReadLine(), out MenuChoice) || MenuChoice <= 0 || MenuChoice >= 7))
    {
    MenuChoice = 0;
    Console.WriteLine("You must select a number corresponding to 1 of the menu choices!");
    }

switch (MenuChoice)
{
    case 1:
        Console.Clear();       
        Console.WriteLine("What is the Movie Title? :"); var TitleInput = Console.ReadLine(); Console.WriteLine("What year did the movie release? :"); int YearInput = Convert.ToInt32(Console.ReadLine()); Console.WriteLine("What genre does the movie belong to? :"); var GenreInput = Console.ReadLine(); Console.WriteLine("What is the IMDB link? :"); var IMDBInput = Console.ReadLine();
        Console.WriteLine("What Actors have a role in the movie? :"); var ActorsInput = Console.ReadLine(); 
        Movie film = new() { Title = TitleInput, Year = YearInput, Genre = GenreInput, IMDB = IMDBInput, Actors = ActorsInput };
        SQLHandler.AddMovie(film);
        break;
        
    case 2:
        Console.Clear();
        //SQLHandler.AddActor(skådespelare);
        break;
            

    case 3:
        Console.Clear();
        //SQLHandler.GetMovie();
        break;

    case 4:
        Console.Clear();
      //  SQLHandler.GetActors();
        break;

    case 5:
        Console.Clear();
    //    SQLHandler.DeleteMovie();
        break;

    case 6:
        Console.Clear();
     //   SQLHandler.DeleteActor();
        break;

}    /* cnn.close(); */

// Börja med att lägga till Nuget för MySQL
// Sen kan ni kolla på koden ;)

// Uppgift:
// Skapa en eller flera tabeller som ska
// innehålla information om filmer och skådespelare

