using System;
using AlbumServices;

namespace AlbumPhotoList
{
    class Program
    {
        static void Main(string[] args)
        {
            PromptForInput();
        }

        static void PromptForInput()
        {
            Console.WriteLine(AlbumServiceMethods.DisplayUserMessage("start"));
            string userInput = Console.ReadLine();
            if(AlbumServiceMethods.IsNumberInRange(userInput))
            {
                var albumPhotoList = AlbumServiceMethods.BuildPhotoList(AlbumServiceMethods.GetPhotoList(userInput).Result);
                Console.WriteLine(AlbumServiceMethods.DisplayUserMessage("photos", albumPhotoList));
            } else if(AlbumServiceMethods.IsAlphaQ(userInput))
            {
                Console.WriteLine(AlbumServiceMethods.DisplayUserMessage("bye"));
                Environment.Exit(0);
            } else
            {
                Console.WriteLine(AlbumServiceMethods.DisplayUserMessage("nope"));
            }
 
            PromptForInput();
        }
    }
}
