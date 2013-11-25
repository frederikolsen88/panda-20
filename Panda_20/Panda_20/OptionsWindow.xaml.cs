using System;
using System.Windows;
using MahApps.Metro.Controls;
using Panda_20.gui;
using Panda_20.Properties;
using Panda_20.service;

namespace Panda_20
{
    /// <summary>
    /// Interaction logic for OptionsWindow.xaml
    /// </summary>
    public partial class OptionsWindow : MetroWindow
    {
        private readonly bool _startupConditionsBeforeUserFiddlesWithThem = Settings.Default.start_with_windows;

        public OptionsWindow()
        {
            InitializeComponent();
        }

        private void ButtonRevertToDefault(object sender, RoutedEventArgs e)
        {
            BackToDefaults.DefaultSettings();
        }

        private void ButtonClearCredentials_Click(object sender, RoutedEventArgs e)
        {
            ConfirmClearCredentials.Visibility = Visibility.Visible;
            ButtonClearCredentials.IsEnabled = false;
        }

       

        private void ButtonYesClear(object sender, RoutedEventArgs e)
        {
            BackToDefaults.DefaultFacebookValues();
            TerminationAssistant.ShutItDown();
        }

        private void ButtonNoClear(object sender, RoutedEventArgs e)
        {
            ConfirmClearCredentials.Visibility = Visibility.Hidden;
            ButtonClearCredentials.IsEnabled = true;
        }



        private void ButtonOK_Click(object sender, RoutedEventArgs e)
        {
            Settings.Default.Save();
            //  TODO This saves any changes made to the app-wide settings; if anything else is being done, that has to be handled as well.

            
            bool startupConditionsChanged = _startupConditionsBeforeUserFiddlesWithThem !=
                                            Settings.Default.start_with_windows;

            if (startupConditionsChanged)
            {
                Service.ConfigureRegistryKeyForStartup(Settings.Default.start_with_windows);
            }

            Queue.Qp.RepositionMe();
            Queue.RemoveDisplayedPopups();
            this.Close();
        }

        private void ButtonAbout_Click(object sender, RoutedEventArgs e)
        {
            AboutWindow aboutWindow = new AboutWindow();
            aboutWindow.Show();
        }

    }
}
