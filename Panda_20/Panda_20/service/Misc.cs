using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace Panda_20.service
{
    class Misc
    {
        // Accepts seconds to add or subtract
        public static long UnixTimeNow(long seconds)
        {
            TimeSpan _TimeSpan = (DateTime.UtcNow - new DateTime(1970, 1, 1, 0, 0, 0));
            return (long)_TimeSpan.TotalSeconds+seconds;
        }

        public static BitmapImage DownloadImage(string fullUrl)
        {
            Uri urlAsUri;
            bool urlIsValid = Uri.TryCreate(fullUrl, UriKind.Absolute, out urlAsUri);

            if (urlIsValid)
            {
                BitmapImage bitmap = new BitmapImage();
                bitmap.BeginInit();
                bitmap.UriSource = urlAsUri;
                bitmap.EndInit();

                return bitmap;
            }

            else
            {
                throw new UriFormatException(fullUrl + " could not be parsed as an Uri!");
            }
        }

        public static string GetPagePictureUrl(string req)
        {
            string pictureUrl = "";

            if (Service.WebClient == null) 
                Service.WebClient = new WebClient();

            try
            {
                string response = Service.WebClient.DownloadString(req);

                // Jeg har haft problemer med at arbejde med Json'en, der kommer tilbage.
                // Ergo denne workaround.

                pictureUrl = response.Split('"')[5].Replace(@"\", "");
            }

            catch (WebException)
            {
                pictureUrl = @"\resources\placeholder.gif";
            }

            return pictureUrl;
        }
    }
}