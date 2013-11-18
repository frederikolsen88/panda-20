using System.Windows;
using System.Windows.Forms;
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
        private MenuItem menuItemExit;
        private MenuItem menuItemOptions;

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
            menuItemExit = new MenuItem();
            menuItemOptions = new MenuItem();
            

            // Initialize contextMenu
            this.contextMenu.MenuItems.AddRange(
                    new System.Windows.Forms.MenuItem[] { this.menuItemExit , this.menuItemOptions});

            // Initialize menuItemEixt 
            this.menuItemExit.Index = 0;
            this.menuItemExit.Text = "E&xit";
            this.menuItemExit.Click += new System.EventHandler(this.menuItemExit_Click);

            // Initialize menuItemEixt 
            this.menuItemOptions.Index = 1;
            this.menuItemOptions.Text = "O&ption";
            this.menuItemOptions.Click += new System.EventHandler(this.menuItemOption_Click);

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
            const string message = "Vil du afslutte Panda?";
            const string caption = "Panda";
            const MessageBoxButton button = MessageBoxButton.OKCancel;
            const MessageBoxImage image = MessageBoxImage.Question;

            MessageBoxResult result = MessageBox.Show(message, caption, button, image);

            if (result == MessageBoxResult.OK)
            {
                // Når brugeren lukker MessageBoxen, må vi godt lukke programmet.
                Application.Current.Shutdown();
            }

            if (result == MessageBoxResult.Cancel)
            {
                e.Cancel = true;
            }

        }
    }
}

