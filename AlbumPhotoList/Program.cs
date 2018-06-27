using System;
using System.Diagnostics;
using System.Threading.Tasks;
using AlbumServices;

namespace AlbumPhotoList
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Welcome to AlbumPhotoList App");
            PromptForInput();
        }


        private static void PromptForInput()
        {
            Console.WriteLine(AlbumServiceMethods.GetUserMessage("start"));
            string result = AlbumServiceMethods.EvaluateUserInput(Console.ReadLine());

            switch (result)
            {
                case "GoodBye!":
                    Console.WriteLine(result);
                    Environment.Exit(0);
                    break;
                default:
                    Console.WriteLine(result);
                    break;
            }

            PromptForInput();
        }
    }
}
