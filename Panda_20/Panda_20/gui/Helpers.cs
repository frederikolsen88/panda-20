using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using Facebook;

namespace Panda_20.gui
{
    /// <summary>
    /// Hjælpeklasse til FB login browser.
    /// 
    /// Author: Frederik Olsen
    /// </summary>
    class BrowserHelper
    {

        public static Uri CurrentUri { get; set; }

        public static void InitBrowser(WebBrowser browser)
        {
            CurrentUri = new Uri(Service.Instance.GetXmlElement("fbUrl"));
            browser.Navigate(CurrentUri);
        }

        public static bool FetchToken()
        {
            string uriString = CurrentUri.ToString();
            Debug.WriteLine(uriString);
            bool hasToken = false;

            if (uriString.Contains("access_token"))
            {
                int tokenStart = uriString.IndexOf('#') + 1;
                int expiresInStart = uriString.LastIndexOf('=') + 1;
                hasToken = true;

                string token = uriString.Substring(tokenStart, uriString.IndexOf('&') - tokenStart);

                token = token.Substring(token.IndexOf('=') + 1);

                string expiresIn = uriString.Substring(expiresInStart, uriString.Length - expiresInStart);

                Service.Instance.TokenAndExpiresIn[0] = token;
                Service.Instance.TokenAndExpiresIn[1] = expiresIn;
            }

            return hasToken;
        }
    }

    /// <summary>
    /// Hjælpeklasse til ListBox med Facebook-sider.
    /// 
    /// Author: Frederik Olsen
    /// </summary>
    class PageListHelper
    {
        public static void InitPageList(ListBox pagesListBox)
        {
            Service.Instance.FetchPages();

            // TESTS
            // KeyValuePair<string, JsonObject> first = Service.Instance.Pages.First();
            // Service.Instance.Pages.Clear();
            // Service.Instance.Pages.Add(first.Key, first.Value);
            // Debug.WriteLine(Service.Instance.Pages.Count);


            // Hvis brugeren ikke administrerer nogen pages, viser vi en
            // popup om dette.
            if (Service.Instance.Pages.Count == 0)
            {
                const string message = "Panda kræver, at du administrerer mindst én Facebook-side. Klik OK for at lukke programmet.";
                const string caption = "Fejl";
                const MessageBoxButton button = MessageBoxButton.OK;
                const MessageBoxImage image = MessageBoxImage.Warning;

                MessageBoxResult result = MessageBox.Show(message, caption, button, image);

                if (result == MessageBoxResult.OK)
                {
                    // Når brugeren lukker DialogBoxen, må vi godt lukke programmet.
                    Application.Current.Shutdown();
                }
            }

            // Brugeren har kun én Facebook-side, som vælges by default.
            else if (Service.Instance.Pages.Count == 1)
            {
                string key = Service.Instance.Pages.Keys.First();
                Service.Instance.SelectedPage = Service.Instance.Pages[key];
                Service.Instance.SetPageFacebookClient((string)Service.Instance.SelectedPage["access_token"]);

            }

            else
            {
                pagesListBox.ItemsSource = Service.Instance.Pages.Keys;
            }
        }
    }
}
