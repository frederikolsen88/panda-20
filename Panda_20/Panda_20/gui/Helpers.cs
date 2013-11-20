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
                TerminationAssistant.NoPermissionsErrorPopUp();
            }

            return hasToken;
        }
    }

    class TerminationAssistant
    {

        public static void NoPermissionsErrorPopUp()
        {
            const string message = "Panda did not get the required permissions. Click OK to close.";
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