using System;
using System.Windows;
using System.Windows.Forms;
using Panda_20.gui;
using Panda_20.Properties;
using Panda_20.service;
using MessageBox = System.Windows.MessageBox;

namespace Panda_20
{
    /// <summary>
    /// Hoved- og konfigurationsvinduelogik.
    /// 
    /// Author: Frederik Olsen og Toke V. Albrechtsen
    /// </summary>
    
    public partial class MainWindow : Window
    {
        private static NotifyIcon _notifyIcon;

        public static NotifyIcon NotifyIcon
        {
            get
            {
                return _notifyIcon;
            }
        }
        private ContextMenu contextMenu;
        private MenuItem menuItemExit;
        private MenuItem menuItemOptions;
        private MenuItem menuItemClearAll;
        private OptionsWindow opWindow;
        public static bool OptionsShowing = false;

        public MainWindow()
        {
            // SetupRoutine to check if this is first time and other fun stuff. FIRST THING THAT SHOULD HAPPEN IS THIS
            SetupRoutine();
            
            
            InitializeComponent();

            WindowState = System.Windows.WindowState.Minimized;
        }

        /// <summary>
        ///  Invokes the method to handle first time run, if it is in deed the first time this application has been run, and
        ///  increases the appwide setting telling how many times the application has been run.
        /// </summary>
        private void SetupRoutine()
        {
            if (Settings.Default.NumberOfTimesRun == 0)
            {
                FirstTimeRun();
            }
            Settings.Default.NumberOfTimesRun++; 
            Settings.Default.Save();
        }


        /// <summary>
        /// Displays a dialog, prompting the user to choose whether or not the application should start with windows or not
        /// </summary>
        private void FirstTimeRun()
        {
            // Configuration of Message Box
            String messageBoxText = "This program can launch automatically as soon as Windows starts." + "\n\n" +
                                    "HINT: You can change the settings yourself in the Options menu of the application at a later time" + "\n\n" +
                                    "Would you like to set the application to start automatically?";
            String caption = "Panda: Start application automatically?";
            MessageBoxButton button = MessageBoxButton.YesNo;
            MessageBoxImage icon = MessageBoxImage.Question;

            // Display the message box
            MessageBoxResult result = MessageBox.Show(messageBoxText, caption, button, icon);

            if (result.Equals(MessageBoxResult.Yes))
            {
                // actual configuration of that happens here
                Service.ConfigureRegistryKeyForStartup(true);
            }
        }


        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            
            _notifyIcon = new NotifyIcon();
            _notifyIcon.Icon = Properties.Resources.panda;

            ContextMenuSetup();
            Hide();

            
            if (BrowserHelper.HasToken())
            {
                PageList pageList = new PageList();
            }
            else
            {
                BrowserWindow browserWindow = new BrowserWindow();
                browserWindow.Show();
            }
        }

        // Handles the Menu
        public void ContextMenuSetup()
        {
            contextMenu = new ContextMenu();
            menuItemExit = new MenuItem();
            menuItemOptions = new MenuItem();
            menuItemClearAll = new MenuItem();
            

            // Initialize contextMenu
            contextMenu.MenuItems.AddRange(
                    new System.Windows.Forms.MenuItem[] { this.menuItemClearAll, this.menuItemOptions, this.menuItemExit });

            // Initialize menuRemoveAll
            menuItemClearAll.Index = 0;
            menuItemClearAll.Text = "C&lear all";
            menuItemClearAll.Click += new System.EventHandler(this.menuItemRemoveAll_Click);

            // Initialize menuItemOptions 
            menuItemOptions.Index = 1;
            menuItemOptions.Text = "O&ptions";
            menuItemOptions.Click += new System.EventHandler(this.menuItemOption_Click);

            // Initialize menuItemEixt 
            menuItemExit.Index = 2;
            menuItemExit.Text = "E&xit";
            menuItemExit.Click += new System.EventHandler(this.menuItemExit_Click);

           

            // The ContextMenu property sets the menu that will 
            // appear when the systray icon is right clicked.
            _notifyIcon.ContextMenu = this.contextMenu;

            // The Text property sets the text that will be displayed, 
            // in a tooltip, when the mouse hovers over the systray icon.
            _notifyIcon.Text = "Panda";
            _notifyIcon.Visible = true;
        }
    
        private void menuItemExit_Click(object Sender, System.EventArgs e)
        {
            Close();
        }


        /// <summary>
        ///  Creates and shows a new options window
        /// </summary>
        private void menuItemOption_Click(object Sender, System.EventArgs e)
        {
            if (!OptionsShowing)
            {
                opWindow = new OptionsWindow();
                opWindow.Show();
                menuItemOptions.Enabled = false;
                OptionsShowing = true;
            }   
        }

        private void menuItemRemoveAll_Click(object Sender, System.EventArgs e)
        {
            Queue.RemoveDisplayedPopups();
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
           TerminationAssistant.ShowClosingPopUp(this, e);
        }
       
    }
}

