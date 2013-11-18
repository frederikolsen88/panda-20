using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;

namespace Panda_20.gui
{
    /// <summary>
    ///     Hjælpeklasse til FB login browser.
    ///     Author: Frederik Olsen
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
        public static void ShowClosingPopUp(object sender, CancelEventArgs e)
        {
            const string message = "Do you want to close Panda?";
            const string caption = "Panda";
            const MessageBoxButton buttons = MessageBoxButton.OKCancel;
            const MessageBoxImage image = MessageBoxImage.Question;
            MessageBoxResult result = MessageBox.Show(Application.Current.MainWindow, message, caption, buttons,
                    image);

                if (result == MessageBoxResult.OK)
                {
                   System.Environment.Exit(0);
                }

                if (result == MessageBoxResult.Cancel)
                {
                    e.Cancel = true;
                }
            }
            
        }
    }