using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Windows;
using Panda_20.Properties;
using Panda_20.service;
using Application = System.Windows.Application;
using MessageBox = System.Windows.MessageBox;
using WebBrowser = System.Windows.Controls.WebBrowser;

namespace Panda_20.gui
{
    /**
     * Helper for the WebBrowser control used for logging into Facebook.
     * 
     * Author: Frederik Olsen
     */

    class BrowserHelper
    {
        // The Uri the browser is currently showing.
        public static Uri CurrentUri { get; set; }

        // Check if there is already a valid set of Facebook credentials
        // in the user's settings.
        public static bool HasToken()
        {
            bool hasToken = false;

            if ((Service.ReadFromConfig("fb_token") != "") && (Service.ReadFromConfig("fb_token_expires_in") != ""))
            {
                long expiresInAsLong = Convert.ToInt64(Service.ReadFromConfig("fb_token_expires_in"));

                // 12 hour difference to ensure the user does not log in
                // automatically using a token that will expire immediately
                // thereafter.
                if (Misc.UnixTimeNow(43200) < expiresInAsLong || expiresInAsLong == 0)
                {
                    hasToken = true;
                }
            }

            return hasToken;
        }

        // Initialises a given WebBrowser and navigates to the Facebook URL stored
        // in the configuration file.
        public static void InitBrowser(WebBrowser browser)
        {
            CurrentUri = new Uri(Settings.Default.fbUrl);
            browser.Navigate(CurrentUri);
        }

        // Extract a new access token from the URL, the Facebook URL mentioned above
        // redirects the browser to. Returns a boolean indicating success or failure.
        public static bool FetchToken()
        {
            string uriString = CurrentUri.ToString();
            Debug.WriteLine(uriString);
            bool hasToken = false;

            // If the URL contains this substring, we can assume an error occured.
            if (!uriString.Contains("error_reason=user_denied"))
            {
                // If the URL contains this substring, there is a valid token in the URL.
                if (uriString.Contains("access_token"))
                {
                    // Extract an access token from the URL.
                    int tokenStart = uriString.IndexOf('#') + 1;
                    int expiresInStart = uriString.LastIndexOf('=') + 1;
                    hasToken = true;

                    string token = uriString.Substring(tokenStart, uriString.IndexOf('&') - tokenStart);
                    token = token.Substring(token.IndexOf('=') + 1);

                    string expiresIn = uriString.Substring(expiresInStart, uriString.Length - expiresInStart);

                    Service.WriteToConfig("fb_token", token);
                    Service.WriteToConfig("fb_token_expires_in", expiresIn);
                }
            }

            else
            {
                // If a token couldn't be extracted, we may kill the program.
                TerminationAssistant.ShowErrorPopUp(null, "Panda did not receive the neccessary permissions from Facebook. Click OK to close the program.");
            }

            return hasToken;
        }
    }

  

    /**
     * Helper for our most common shutdown scenarios.
     * 
     * Author: Frederik Olsen & Tobias Roland Rasmussen
     */
    class TerminationAssistant
    {
        
        // Spawns a MessageBox indicating an irrecoverable error has happened.
        public static void ShowErrorPopUp(object sender, string msg)
        {
            // To prevent user activity while the MessageBox is being shown.
            if (sender != null)
                ((Window)sender).Hide();

            string message = msg;
            const string caption = "Panda";
            const MessageBoxButton button = MessageBoxButton.OK;
            const MessageBoxImage image = MessageBoxImage.Error;
            MessageBoxResult result = MessageBox.Show(Application.Current.MainWindow, message, caption, button, image);

            // When the user clicks OK, the program is terminated.
            // The Facebook credentials stored in the config file are also wiped,
            // for good measure in case they've somehow been corrupted.
            if (result == MessageBoxResult.OK)
            {
                Service.WriteToConfig("fb_token", "");
                Service.WriteToConfig("fb_token_expires_in", "0");
                ShutItDown();
            }
        }

        // Method for shutting down without dialog.
        public static void ShutItDown()
        {
            MainWindow.NotifyIcon.Dispose();
            Misc.DisposeWebClient();
            Environment.Exit(0);
        }
        
        // Spawns a MessageBox that handles graceful exits.
        public static void ShowClosingPopUp(object sender, CancelEventArgs e)
        {
            // To prevent user activity while the MessageBox is being shown.
            ((Window)sender).Hide();

            const string message = "Do you want to close Panda?";
            const string caption = "Panda";
            const MessageBoxButton buttons = MessageBoxButton.OKCancel;
            const MessageBoxImage image = MessageBoxImage.Question;
            MessageBoxResult result = MessageBox.Show(Application.Current.MainWindow, message, caption, buttons,
                    image);

                if (result == MessageBoxResult.OK)
                {
                    ShutItDown();
                }

                if (result == MessageBoxResult.Cancel)
                {
                    e.Cancel = true;
                    ((Window)sender).Show();
                }
            }
            
        }     
    }