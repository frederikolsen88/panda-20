﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Windows.Forms;

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
        public MainWindow()
        {
            // THOR IS HERE!
            InitializeComponent();
            this.notifyIcon = new NotifyIcon()
            {
                Icon = Properties.Resources.pandaIcon,
                Visible = true,
            }; 
            this.Hide();
            this.WindowState = System.Windows.WindowState.Minimized;
            this.notifyIcon.ShowBalloonTip(5000, "Panda Status", "Panda is currently running", ToolTipIcon.Info);
            BrowserWindow browserWindow = new BrowserWindow();
            browserWindow.Show();
        }
    }
}
