using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using Panda_20.Properties;
using Panda_20.service;

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
//            CurrentUri = new Uri(Misc.ReadXmlElementFromAppValues("fbUrl"));
            CurrentUri = new Uri(Settings.Default.fbUrl); 
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


                // ----------------- <TODO EXTRACT THIS SETTING OF STUFF TO A METHOD ON SERVICE> -----------
                Service.TokenAndExpiresIn[0] = token;
                Service.TokenAndExpiresIn[1] = expiresIn;

                //This is how to set properties - damn easy, yo! Use it instead of writing to XML
                Settings.Default.fb_token = Service.TokenAndExpiresIn[0];
                Settings.Default.fb_token_expires_in = Service.TokenAndExpiresIn[1];
                Settings.Default.Save(); // remember to save the changes!

                // ----------------- </TODO EXTRACT THIS SETTING OF STUFF TO A METHOD ON SERVICE> -----------
            }

            return hasToken;
        }
    }

    class TerminationAssistant
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
                   MainWindow.NotifyIcon.Dispose();
                   Misc.DisposeWebClient();
                   System.Environment.Exit(0);
                }

                if (result == MessageBoxResult.Cancel)
                {
                    e.Cancel = true;
                }
            }
            
        }
    }