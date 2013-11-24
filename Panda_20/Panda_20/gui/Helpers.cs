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
    /// <summary>
    ///     Hjælpeklasse til FB login browser.
    ///     Author: Frederik Olsen
    /// </summary>
    class BrowserHelper
    {
        public static Uri CurrentUri { get; set; }

        public static bool HasToken()
        {
            bool hasToken = false;

            if ((Service.ReadFromConfig("fb_token") != "") && (Service.ReadFromConfig("fb_token_expires_in") != ""))
            {
                long expiresInAsLong = Convert.ToInt64(Service.ReadFromConfig("fb_token_expires_in"));

                if (Misc.UnixTimeNow(43200) < expiresInAsLong || expiresInAsLong == 0)
                {
                    hasToken = true;
                }
            }

            return hasToken;
        }

        public static void InitBrowser(WebBrowser browser)
        {
//          CurrentUri = new Uri(Misc.ReadXmlElementFromAppValues("fbUrl"));

            CurrentUri = new Uri(Settings.Default.fbUrl);
            browser.Navigate(CurrentUri);
        }

        public static bool FetchToken()
        {
            string uriString = CurrentUri.ToString();
            Debug.WriteLine(uriString);
            bool hasToken = false;

            if (!uriString.Contains("error_reason=user_denied"))
            {
                if (uriString.Contains("access_token"))
                {
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
                TerminationAssistant.ShowErrorPopUp(null, "Panda did not receive the neccessary permissions from Facebook. Click OK to close the program.");
            }

            return hasToken;
        }
    }

  

    /**
     * Hjælpeklasse til diverse nedlukningsscenarier.
     * 
     * Author: Frederik Olsen
     */
    class TerminationAssistant
    {
        /**
         * Denne metode vil spawne en fejl-popup med en given meddelelse. Da "OK"-knappen terminerer programmet
         * og rydder ud i credentials skal denne selvfølgelig kun kaldes når lokummet virkelig brænder på og 
         * Panda ikke kan arbejde videre.
         */
        public static void ShowErrorPopUp(object sender, string msg)
        {
            // For at forhindre aktivitet i det bagvedliggende vindue.
            // Jeg har ikke haft held med andre former for låsning.
            // sender kan dog godt være null; det er ikke altid et vindue,
            // der beder om en fejl-popup.
            if (sender != null)
                ((Window)sender).Hide();

            string message = msg;
            const string caption = "Panda";
            const MessageBoxButton button = MessageBoxButton.OK;
            const MessageBoxImage image = MessageBoxImage.Error;
            MessageBoxResult result = MessageBox.Show(Application.Current.MainWindow, message, caption, button, image);

            if (result == MessageBoxResult.OK)
            {
                MainWindow.NotifyIcon.Dispose();
                Misc.DisposeWebClient();
                Service.WriteToConfig("fb_token", "");
                Service.WriteToConfig("fb_token_expires_in", "0");
                System.Environment.Exit(0);
            }
        }

        /*
         * Method for shutting down without dialog (extracted from ShowClosingPopUp)
         */
        public static void ShutItDown()
        {
            MainWindow.NotifyIcon.Dispose();
            Misc.DisposeWebClient();
            System.Environment.Exit(0);
        }
        
        /**
         * Denne metode spawner en pop-up til "pæn" nedlukning.
         */
        public static void ShowClosingPopUp(object sender, CancelEventArgs e)
        {
            // For at forhindre aktivitet i det bagvedliggende vindue.
            // Jeg har ikke haft held med andre former for låsning.
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