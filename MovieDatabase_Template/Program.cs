using MovieDatabase;
using MovieDatabase_Template;
using Org.BouncyCastle.Bcpg;

Console.WriteLine("Vad heter din användarprofil på datorn? (exempel: 'C:\\Users\\Adam Hasan\\'  innebär att du skriver bara 'Adam Hasan'.)");  
string Användare = Console.ReadLine();
try 
{
    string[] files = Directory.GetFiles("C:\\Users\\" +Användare+ "\\Desktop\\",  //letar igenom Desktop & alla dess subfolders efter filen loginSQL123.txt, kunde inte ha högre upp i mappstrukturen även med admin-rättigheter då jag skulle vart tvungen att implementera try & catch för folders som är o-accessbara via visual studio.
    "loginSQL123.txt", SearchOption.AllDirectories);
    StreamReader loginSQL = new StreamReader(path: files[0]);
    MovieCrud SQLHandler = new(@"Server=ns8.inleed.net;Database=s60127_DubaiOwls;" +loginSQL.ReadToEnd() + ";");
}
catch 
{
    Console.WriteLine("Error: Login to database failed, it could be due to one of these reasons: \n1. Your loginSQL123.txt file isn't located in C:\\Users\\(your user name)\\Desktop\\ or one of its subfolders. \n2. Your Uid and password in the textfile don't match any viable login. \n3. Your textfile name is misspelled, make sure its name is loginSQL123.txt !");
    Environment.Exit(0);
}


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

