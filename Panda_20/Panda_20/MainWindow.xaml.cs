using System;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Windows.Forms;
using Microsoft.Win32;
using Panda_20.gui;
using Panda_20.service;
using Application = System.Windows.Application;
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

        public MainWindow()
        {
            
            InitializeComponent();

            WindowState = System.Windows.WindowState.Minimized;
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

        private void menuItemOption_Click(object Sender, System.EventArgs e)
        {
            // TODO Option Logic 
            // ROLAND BE WORKING ON THIS!
            // More Like done?
            OptionsWindow opWindow = new OptionsWindow();
            opWindow.Show();
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

