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
        private NotifyIcon notifyIcon;
        private ContextMenu contextMenu;
        private MenuItem menuItemOptions;
        private MenuItem menuItemExit;

        public MainWindow()
        {
            // THOR IS HERE!
            InitializeComponent();
            notifyIcon = new NotifyIcon();
            notifyIcon.Icon =  Properties.Resources.pandaIcon;

            contextMenuSetup();
            Hide();

            WindowState = System.Windows.WindowState.Minimized;
            notifyIcon.ShowBalloonTip(5000, "Panda Status", "Panda is currently running", ToolTipIcon.Info);
            BrowserWindow browserWindow = new BrowserWindow();
            browserWindow.Show();
        }

        // Handles the Menu
        public void contextMenuSetup()
        {
            contextMenu = new ContextMenu();
            menuItemOptions = new MenuItem();
            menuItemExit = new MenuItem();
            

            // Initialize contextMenu


            this.contextMenu.MenuItems.Add(1,this.menuItemExit);
            this.contextMenu.MenuItems.Add(0,this.menuItemOptions);


            // Initialize menuItemOption 
            this.menuItemOptions.Index = 0;
            this.menuItemOptions.Text = "O&ption";
            this.menuItemOptions.Click += new System.EventHandler(this.menuItemOption_Click);

            // Initialize menuItemEixt 
            this.menuItemExit.Index = 1;
            this.menuItemExit.Text = "E&xit";
            this.menuItemExit.Click += new System.EventHandler(this.menuItemExit_Click);

            

            // The ContextMenu property sets the menu that will 
            // appear when the systray icon is right clicked.
            notifyIcon.ContextMenu = this.contextMenu;

            // The Text property sets the text that will be displayed, 
            // in a tooltip, when the mouse hovers over the systray icon.
            notifyIcon.Text = "Panda";
            notifyIcon.Visible = true;
        }
    
        private void menuItemExit_Click(object Sender, System.EventArgs e)
        {
            // Close the form, which closes the application. 
            this.Close();
        }
        private void menuItemOption_Click(object Sender, System.EventArgs e)
        {
            // Option Logic ToDo
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            MiscHelper.Close(sender, e);
        }
    }
}

