using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Forms;
using System.Windows.Media;
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
        //--------------------------------------------------
        // If we want the system to start with windows or not
        public bool IsStartWithWindowsSetToOn
        {
            get { return (bool)GetValue(IsStartWithWindowsSetToOnDependencyProperty); }
            set { SetValue(IsStartWithWindowsSetToOnDependencyProperty, value); }
        }

        // Using a DependencyProperty as the backing for isStartWithWindowsSetToOn. This enables binding.
        public static readonly DependencyProperty IsStartWithWindowsSetToOnDependencyProperty =
            DependencyProperty.Register("isStartWithWindowsSetToOn", typeof(bool), typeof(OptionsWindow), new UIPropertyMetadata(false));
        
        
        
        
        //--------------------------CONSTRUCTOR------------------------------------


        /// <summary>
        /// Constructor for the Options-window
        /// </summary>
        public OptionsWindow()
        {
            IsStartWithWindowsSetToOn = Settings.Default.start_with_windows;
            InitializeComponent();
        }



        //--------------------------OTHER METHODS -----------------------------------

        /// <summary>
        /// Handler for button-click on Revert to Defaults... does what it says.
        /// </summary>
        private void ButtonRevertToDefault_Click(object sender, RoutedEventArgs e)
        {
            BackToDefaults.DefaultSettings();

            // Note: Special case with this one
            IsStartWithWindowsSetToOn = Settings.Default.start_with_windows;

            Close();
            OptionsWindow newOpt = new OptionsWindow();
            newOpt.Show();
        }

        /// <summary>
        /// Handler for button-click on Clear Credentials. Will show the cofirmation-panel and disable the Clear Credentials button
        /// </summary>
        private void ButtonClearCredentials_Click(object sender, RoutedEventArgs e)
        {
            ConfirmClearCredentials.Visibility = Visibility.Visible;
            ButtonClearCredentials.IsEnabled = false;
        }

        /// <summary>
        ///  Handler for clicking "yes" to clear credentials. Will shut down application after it has done so.
        /// </summary>
        private void ButtonYesToClearCredentials(object sender, RoutedEventArgs e)
        {
            BackToDefaults.DefaultFacebookValues();
            TerminationAssistant.ShutItDown();
        }

        /// <summary>
        /// Handler for clicking "no" to clearing credentials. Sets the Clear Credentials-button to enabled and hides the confirmation-panel.
        /// </summary>
        private void ButtonNoToClearCredentials(object sender, RoutedEventArgs e)
        {
            ConfirmClearCredentials.Visibility = Visibility.Hidden;
            ButtonClearCredentials.IsEnabled = true;
        }

        /// <summary>
        /// Handler for clicking the "OK"-button. Saves any changes, and if necessary, calls a method to change the startup-conditions. Also clears the queue of popups //TODO is it supposed to?
        /// </summary>
        private void ButtonOK_Click(object sender, RoutedEventArgs e)
        {
            Settings.Default.Save();
            //  TODO This saves any changes made to the app-wide settings; if anything else is being done, that has to be handled as well.

            // compare the current options-setting to the Settings.Default-setting 
            if ( IsStartWithWindowsSetToOn != Settings.Default.start_with_windows)
            {
                Service.ConfigureRegistryKeyForStartup(IsStartWithWindowsSetToOn);
            }

            Queue.Qp.RepositionMe();
            Queue.RemoveDisplayedPopups();
            this.Close();
        }

        /// <summary>
        /// Handler for the button to show the "about" window.
        /// </summary>
        private void ButtonAbout_Click(object sender, RoutedEventArgs e)
        {
            AboutWindow aboutWindow = new AboutWindow();
            aboutWindow.Show();
        }

        private void OptionMetroWindow_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            MainWindow.OptionsShowing = false;
            MainWindow.NotifyIcon.ContextMenu.MenuItems[1].Enabled = true;
        }

        private void ColourSwitch_IsCheckedChanged(object sender, EventArgs e)
        {
            bool status = ColourSwitch.IsChecked.Value;
            ComboBoxTimeLimit.IsEnabled = status;

            if (!status)
            {
                TimeLimitLabel1.Foreground = new SolidColorBrush(Colors.DimGray);
                TimeLimitLabel2.Foreground = new SolidColorBrush(Colors.DimGray);
            }

            else
            {
                TimeLimitLabel1.Foreground = new SolidColorBrush(Colors.Black);
                TimeLimitLabel2.Foreground = new SolidColorBrush(Colors.Black);
            }
        }

        private void PostDisappearSwitch_IsCheckedChanged(object sender, EventArgs e)
        {
            bool status = PostDisappearSwitch.IsChecked.Value;
            ComboBoxPostDisappear.IsEnabled = status;

            if (!status)
            {
                PostDisappearLabel1.Foreground = new SolidColorBrush(Colors.DimGray);
                PostDisappearLabel2.Foreground = new SolidColorBrush(Colors.DimGray);
            }

            else
            {
                PostDisappearLabel1.Foreground = new SolidColorBrush(Colors.Black);
                PostDisappearLabel2.Foreground = new SolidColorBrush(Colors.Black);
            }
        }

        private void PmDisappearSwitch_IsCheckedChanged(object sender, EventArgs e)
        {
            bool status = PmDisappearSwitch.IsChecked.Value;
            ComboBoxPmDisappear.IsEnabled = status;

            if (!status)
            {
                PmDisappearLabel1.Foreground = new SolidColorBrush(Colors.DimGray);
                PmDisappearLabel2.Foreground = new SolidColorBrush(Colors.DimGray);
            }

            else
            {
                PmDisappearLabel1.Foreground = new SolidColorBrush(Colors.Black);
                PmDisappearLabel2.Foreground = new SolidColorBrush(Colors.Black);
            }
        }

        private void CommentDisappearSwitch_IsCheckedChanged(object sender, EventArgs e)
        {
            bool status = CommentDisappearSwitch.IsChecked.Value;
            ComboBoxCommentDisappear.IsEnabled = status;

            if (!status)
            {
                CommentDisappearLabel1.Foreground = new SolidColorBrush(Colors.DimGray);
                CommentDisappearLabel2.Foreground = new SolidColorBrush(Colors.DimGray);
            }

            else
            {
                CommentDisappearLabel1.Foreground = new SolidColorBrush(Colors.Black);
                CommentDisappearLabel2.Foreground = new SolidColorBrush(Colors.Black);
            }
        }

        private void OptionMetroWindow_Loaded(object sender, RoutedEventArgs e)
        {
            ColourSwitch_IsCheckedChanged(ColourSwitch, null);
            PostDisappearSwitch_IsCheckedChanged(PostDisappearSwitch, null);
            PmDisappearSwitch_IsCheckedChanged(PmDisappearSwitch, null);
            CommentDisappearSwitch_IsCheckedChanged(CommentDisappearSwitch, null);
        }
    }
}
