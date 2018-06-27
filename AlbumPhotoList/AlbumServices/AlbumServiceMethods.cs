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
        internal static List<AlbumPhoto> PhotoList = new List<AlbumPhoto>();
        internal static StringBuilder msgSB = new StringBuilder();

        public static string GetUserMessage(string msgType)
        {
            switch (msgType)
            {
                case "nope":
                    msgSB.AppendLine("User Input is not recognized");
                    break;
                case "bye":
                    msgSB.Append("GoodBye!");
                    break;
                case "photos":
                    msgSB = BuildPhotoList();
                    break;
                default:
                    msgSB.Clear();
                    msgSB.AppendLine("");
                    msgSB.AppendLine("++++++++++++++++++++++++++++++++++");
                    msgSB.AppendLine("Type a number between 0 and 100 + [Enter] to see album contents.");
                    msgSB.AppendLine("Type Q + [Enter] to exit.");
                    msgSB.AppendLine("++++++++++++++++++++++++++++++++++");
                    break;
            }

            return msgSB.ToString();
        }


        public static bool IsNumberInRange(string userInput)
        {
            var evalResult = Regex.IsMatch(userInput, @"^[0-9]{1,2}?$|^100$");
            return evalResult;
        }

        public static bool IsAlphaQ(string userInput)
        {
            var evalResult = Regex.IsMatch(userInput, @"^[Q]$");
            return evalResult;
        }

        public static WebResponse GetPhotoWebResponse(string userInput)
        {
            try
            {
                var photoUrl = "https://jsonplaceholder.typicode.com/photos?albumId=" + userInput;
                HttpWebRequest myReq = (HttpWebRequest)WebRequest.Create(photoUrl);
                return myReq.GetResponse();
            }
            catch (WebException ex)
            {
                return ex.Response;
            }
        }


        public static void GetPhotoList(string userInput)
        { 
          using (WebResponse response = GetPhotoWebResponse(userInput)) {
                using (Stream photoStream = response.GetResponseStream())
                {
                    var photoSerializer = new DataContractJsonSerializer(typeof(List<AlbumPhoto>));
                    PhotoList = photoSerializer.ReadObject(photoStream) as List<AlbumPhoto>;
                }
          }
        }

        public static StringBuilder BuildPhotoList()
        {
            StringBuilder photoSB = new StringBuilder();
            string albumID = "";

            photoSB.AppendLine("----------------------------------");
            photoSB.AppendLine("*albumID*");
            photoSB.AppendLine("----------------------------------");
            foreach (var photo in PhotoList)
            {
                albumID = photo.albumId;
                photoSB.AppendFormat("[{0}]  {1}", photo.id, photo.title);
                photoSB.AppendLine();
            }
            if (PhotoList.Count == 0)
            {
                albumID = " cannot be located.";
            }
            photoSB.Replace("*albumID*", "Photo Album " + albumID);
            photoSB.AppendLine("----------------------------------");
            return photoSB;

        }

        public static string EvaluateUserInput(string userInput)
        {
            msgSB.Clear();
            PhotoList.Clear();

            if (IsNumberInRange(userInput))
            {
                GetPhotoList(userInput);
                return GetUserMessage("photos");
            }
            else if (IsAlphaQ(userInput))
            {
                return GetUserMessage("bye");
            }
            else
            {
                return GetUserMessage("nope");
            }
        }



    }

}
