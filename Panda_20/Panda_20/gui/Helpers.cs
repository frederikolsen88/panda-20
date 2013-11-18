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
            CurrentUri = new Uri(Service.GetXmlElement("fbUrl"));
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

                Service.TokenAndExpiresIn[0] = token;
                Service.TokenAndExpiresIn[1] = expiresIn;
            }

            return hasToken;
        }
    }

    class MiscHelper
    {

        private static bool _closing = false;

        public static void Close(object sender, System.ComponentModel.CancelEventArgs e)
        {

            if (!_closing)
            {
                _closing = true;
                const string message = "Vil du afslutte Panda?";
                const string caption = "Panda";
                const MessageBoxButton buttons = MessageBoxButton.OKCancel;
                const MessageBoxImage image = MessageBoxImage.Question;

                MessageBoxResult result = MessageBox.Show(Application.Current.MainWindow, message, caption, buttons,
                    image);

                if (result == MessageBoxResult.OK)
                {
                    // Når brugeren lukker MessageBoxen, må vi godt lukke programmet.
                    Application.Current.Shutdown();
                }

                if (result == MessageBoxResult.Cancel)
                {
                    e.Cancel = true;
                }
            }

            else
            {
                Application.Current.Shutdown();
            }
        }
    }
}
