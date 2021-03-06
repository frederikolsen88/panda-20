﻿using System;
using System.Threading;
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
        private bool isRunning;
        public static bool OptionsShowing = false;

        public MainWindow()
        {
            if (InstanceService.IsSingleInstance)
            {
                InitiateMainWindow();
            }
            else
            {
                MessageBox.Show("The application is already running!");
                isRunning = true;
            }

        }

        /// <summary>
        /// What would usually be contained in the Main-class, extracted for easier editing
        /// </summary>
        private void InitiateMainWindow()
        {
            // TimesRunStartupRoutine to check if this is first time run and other fun stuff.
            TimesRunStartupRoutine();
            InitializeComponent();
            WindowState = System.Windows.WindowState.Minimized;
            
        }
        

        /// <summary>
        ///  Invokes the method to handle first time run, if it is in deed the first time this application has been run, and
        ///  increases the appwide setting telling how many times the application has been run.
        /// </summary>
        private void TimesRunStartupRoutine()
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
            String messageBoxText = "Would you like the application to start automatically when Windows starts?";
            String caption = "Panda";
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

        /// <summary>
        /// TODO Write documentation
        /// </summary>
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            Hide();
            _notifyIcon = new NotifyIcon();
            _notifyIcon.Icon = Properties.Resources.panda;
            if (isRunning)
            {
                TerminationAssistant.ShutItDown();
            }
            ContextMenuSetup();
            

            
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
        /// <summary>
        /// TODO Write documentation
        /// </summary>
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

