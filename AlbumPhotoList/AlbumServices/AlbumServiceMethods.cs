using System.Text;
using System.Text.RegularExpressions;
using System.Net;
using System.Threading.Tasks;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;
using System.Collections.Generic;
using System.Net.Http;
using System.IO;

namespace AlbumServices
{
    public class AlbumServiceMethods
    {

        public static string DisplayUserMessage(string msgType, StringBuilder photolist = null )
        {
            StringBuilder msgSB = new StringBuilder();

            switch (msgType)
            {
                case "nope":
                    msgSB.AppendLine("User Input is not recognized");
                    break;
                case "bye":
                    msgSB.AppendLine("GoodBye!");
                    break;
                case "photos":
                    msgSB = photolist;
                    break;
                default:
                    msgSB.AppendLine("");
                    msgSB.AppendLine("++++++++++++++++++++++++++++++++++");
                    msgSB.AppendLine("Welcome to AlbumPhotoList.");
                    msgSB.AppendLine("Type a number between 1 and 100 + [Enter] to see album contents.");
                    msgSB.AppendLine("Type Q + [Enter] to exit.");
                    msgSB.AppendLine("++++++++++++++++++++++++++++++++++");
                    break;
            }

            return msgSB.ToString();
        }


        public static bool IsNumberInRange(string userInput)
        {
            var evalResult = Regex.IsMatch(userInput, @"[1-9][0-9]?$|^100");
            return evalResult;
        }

        public static bool IsAlphaQ(string userInput)
        {
                var evalResult = Regex.IsMatch(userInput, @"[Q]");
                return evalResult;
        }

        public static async Task<Stream> GetPhotoList(string userInput)
        {
            HttpClient client = new HttpClient();
            var photoUrl = "https://jsonplaceholder.typicode.com/photos?albumId=" + userInput;
            var photoStream = await client.GetStreamAsync(photoUrl);
           
            return photoStream;
        }

        public static StringBuilder BuildPhotoList(Stream albumPhotoStream)
        {
            StringBuilder photoSB = new StringBuilder();
            var photoSerializer = new DataContractJsonSerializer(typeof(List<AlbumPhoto>));
            var photos = photoSerializer.ReadObject(albumPhotoStream) as List<AlbumPhoto>;
            string albumID= "";

            photoSB.AppendLine("----------------------------------");
            photoSB.AppendLine("*albumID*");
            photoSB.AppendLine("----------------------------------");
            foreach (var photo in photos)
            {
                albumID = photo.albumId;
                photoSB.AppendFormat("[{0}]  {1}", photo.id, photo.title );
                photoSB.AppendLine();
            }
            if(photos.Count == 0)
            {
                albumID = " cannot be located.";
            }
            photoSB.Replace("*albumID*", "Photo Album " + albumID);
            photoSB.AppendLine("----------------------------------");
            return photoSB;

        }



    }

}
