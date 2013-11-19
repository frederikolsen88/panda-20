using System.ComponentModel;
using System.Windows;
using System.Windows.Forms;
using Panda_20.gui;
using Application = System.Windows.Application;
using MessageBox = System.Windows.MessageBox;

namespace Panda_20
{
    /// <summary>
    /// Hoved- og konfigurationsvinduelogik.
    /// 
    /// Author: Frederik Olsen (nej, ikke Thor)
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

        public MainWindow()
        {
            // THOR IS HERE!
            InitializeComponent();
            _notifyIcon = new NotifyIcon();
            _notifyIcon.Icon =  Properties.Resources.pandaIcon;

            contextMenuSetup();
            Hide();

            WindowState = System.Windows.WindowState.Minimized;
            _notifyIcon.ShowBalloonTip(5000, "Panda Status", "Panda is currently running", ToolTipIcon.Info);
            BrowserWindow browserWindow = new BrowserWindow();
            browserWindow.Show();
            NotificationPopup np = new NotificationPopup();
            np.Show();
        }

        // Handles the Menu
        public void contextMenuSetup()
        {
            contextMenu = new ContextMenu();
            menuItemExit = new MenuItem();
            menuItemOptions = new MenuItem();
            

            // Initialize contextMenu
            contextMenu.MenuItems.AddRange(
                    new System.Windows.Forms.MenuItem[] { this.menuItemOptions, this.menuItemExit });

            // Initialize menuItemEixt 
            menuItemExit.Index = 1;
            menuItemExit.Text = "E&xit";
            menuItemExit.Click += new System.EventHandler(this.menuItemExit_Click);

            // Initialize menuItemOptions 
            menuItemOptions.Index = 0;
            menuItemOptions.Text = "O&ptions";
            menuItemOptions.Click += new System.EventHandler(this.menuItemOption_Click);

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
            // Close the form, which closes the application. 
            Close();
        }
        private void menuItemOption_Click(object Sender, System.EventArgs e)
        {
            // Option Logic ToDo
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
           MiscHelper.ShowClosingPopUp(this, e);
        }
    }
}

