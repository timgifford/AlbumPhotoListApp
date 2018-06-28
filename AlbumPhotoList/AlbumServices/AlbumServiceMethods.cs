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
        // internal static List<AlbumPhoto> PhotoList = new List<AlbumPhoto>();
        // TG: Avoid static fields. They are like global variables and multiple static functions can 
        // change the value of the stringbuilder.

        // internal static StringBuilder msgSB = new StringBuilder();

        public static string GetUserMessage(string msgType)
        {
                    string msgSB = "++++++++++++++++++++++++++++++++++" + System.Environment.NewLine;
                    msgSB += "Type a number between 0 and 100 + [Enter] to see album contents."+ System.Environment.NewLine;
                    msgSB += "Type Q + [Enter] to exit."+ System.Environment.NewLine;
                    msgSB += "++++++++++++++++++++++++++++++++++" + System.Environment.NewLine;
                    return msgSB;
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


        public static List<AlbumPhoto> GetPhotoList(string albumId)
        { 
          using (WebResponse response = GetPhotoWebResponse(albumId)) {
                using (Stream photoStream = response.GetResponseStream())
                {
                    var photoSerializer = new DataContractJsonSerializer(typeof(List<AlbumPhoto>));
                    return photoSerializer.ReadObject(photoStream) as List<AlbumPhoto>;
                }
          }
        }

        // TG: Here is an example of a function that takes in the albumId and a list of photos. From that
        // the function has everything it needs to return a string. It doesn't make any network calls or have any
        // external dependencies. This makes it much easier to test. Given this input...you get this output.
        public static string BuildConsoleOutput(string albumId, List<AlbumPhoto> photos){
            
            if(photos.Count == 0 ){
                return "Photo Album "+ albumId + " cannot be located.";
            }
            StringBuilder photoSB = new StringBuilder();
            foreach (var photo in photos)
            {
                photoSB.AppendFormat("[{0}]  {1}", photo.id, photo.title);
                photoSB.AppendLine();
            }

            return photoSB.ToString();
        }
        public static StringBuilder BuildPhotoList(string albumID)
        {
            StringBuilder photoSB = new StringBuilder();

            photoSB.AppendLine("----------------------------------");
            photoSB.AppendLine("*albumID*");
            photoSB.AppendLine("----------------------------------");
            var PhotoList = GetPhotoList(albumID);
            foreach (var photo in PhotoList)
            {
                photoSB.AppendFormat("[{0}]  {1}", photo.id, photo.title);
                photoSB.AppendLine();
            }
            // TG: This string parsing is really confusing. 
            // The logic to get the Photos is intermingled with the string generation for the message.
            // Each function should return the same output when given the same inputs. However, the
            // static variables cause the values to change based on the order in which you call the 
            // function. All the functions are coupled. If I change one function, it could impact other methods that
            // I didn't modify.

            // It's better practice to design the functions so that they don't interfere with one another.
            if (PhotoList.Count == 0)
            {
                albumID = "Photo Album "+ albumID + " cannot be located.";
            }
            photoSB.Replace("*albumID*", "Photo Album " + albumID);
            photoSB.AppendLine("----------------------------------");
            return photoSB;

        }

        public static string EvaluateUserInput(string userInput)
        {
           
            if (IsNumberInRange(userInput))
            {
                var photos = GetPhotoList(userInput);
                return BuildConsoleOutput(userInput, photos);
                // return BuildPhotoList().ToString();
                // return GetUserMessage("photos");
            }
            else if (IsAlphaQ(userInput))
            {
                return "GoodBye!";
            }
            else
            {
                return "User Input is not recognized";
            }
        }



    }

}
