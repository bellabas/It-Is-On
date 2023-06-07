using ItIsOn.ViewModel;
using System;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ItIsOn
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
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

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            (DataContext as MainWindowViewModel).SetNormalModeCommand.Execute(null);
        }

    }
}
