using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MovieDatabase;
using MovieDatabase_Template;
using MySql.Data.MySqlClient;

namespace MovieDatabase_Template
{
    internal static class Menu
    {
        
        public static void RunProgram()
        {
            
            int choice = 0;
            int.TryParse(Console.ReadLine(), out choice);

            switch (choice)
            {
                case 1:
                    Movie movie = new Movie();
                    break;
                    
            }
        }



    }
}
