using ItIsOn.ViewModel;
using System;
using System.Collections.Generic;
using System.ComponentModel;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ItIsOn
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private System.Windows.Forms.NotifyIcon myNotifyIcon;
        private WindowState myStoredWindowState;
        public MainWindow()
        {
            InitializeComponent();

            myStoredWindowState = WindowState.Normal;

            myNotifyIcon = new System.Windows.Forms.NotifyIcon();
            myNotifyIcon.BalloonTipTitle = "Minimized";
            myNotifyIcon.BalloonTipText = "Click the tray icon to show!";
            myNotifyIcon.Text = "It Is ON! App";
            myNotifyIcon.Icon = new System.Drawing.Icon("appicon.ico");
            myNotifyIcon.Click += new EventHandler(MyNotifyIcon_Click);
        }

        private void ScreensaverAndSleep_Click(object sender, RoutedEventArgs e)
        {
            (DataContext as MainWindowViewModel).PreventScreensaverAndSleepCommand.Execute(null);
        }

        private void Sleep_Click(object sender, RoutedEventArgs e)
        {
            (DataContext as MainWindowViewModel).PreventSleepCommand.Execute(null);
        }

        private void NormalMode_Click(object sender, RoutedEventArgs e)
        {
            (DataContext as MainWindowViewModel).SetNormalModeCommand.Execute(null);
        }

        private void StatusLabel_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            if ((string)statusLabel.Content == "ERROR" || (string)statusLabel.Content == "NORMAL")
            {
                titleLabel.Content = "It is OFF!";
                titleLabel.Foreground = Brushes.Tomato;
            }
            else
            {
                titleLabel.Content = "It is ON!";
                titleLabel.Foreground = Brushes.ForestGreen;
            }

            if ((string)statusLabel.Content == "NORMAL")
            {
                statusLabel.Foreground = Brushes.Orange;
            }
            else if ((string)statusLabel.Content == "ERROR")
            {
                statusLabel.Foreground = Brushes.Red;
            }
            else
            {
                statusLabel.Foreground = Brushes.ForestGreen;
            }
        }

        private void Window_Closing(object sender, CancelEventArgs e)
        {
            (DataContext as MainWindowViewModel).SetNormalModeCommand.Execute(null);

            myNotifyIcon.Dispose();
            myNotifyIcon = null;
        }

        private void OnStateChanged(object sender, EventArgs args)
        {
            if (WindowState == WindowState.Minimized)
            {
                Hide();
                if (myNotifyIcon != null)
                {
                    myNotifyIcon.ShowBalloonTip(2000);
                }
            }
            else
            {
                myStoredWindowState = WindowState;
            }
        }
        private void OnIsVisibleChanged(object sender, DependencyPropertyChangedEventArgs args)
        {
            CheckTrayIcon();
        }

        private void MyNotifyIcon_Click(object sender, EventArgs e)
        {
            Show();
            WindowState = myStoredWindowState;
        }
        private void CheckTrayIcon()
        {
            ShowTrayIcon(!IsVisible);
        }

        private void ShowTrayIcon(bool show)
        {
            if (myNotifyIcon != null)
            {
                myNotifyIcon.Visible = show;
            }
        }

    }
}
