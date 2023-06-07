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
            SetTitleLabel();
        }

        private void Sleep_Click(object sender, RoutedEventArgs e)
        {
            (DataContext as MainWindowViewModel).PreventSleepCommand.Execute(null);
            SetTitleLabel();
        }

        private void NormalMode_Click(object sender, RoutedEventArgs e)
        {
            (DataContext as MainWindowViewModel).SetNormalModeCommand.Execute(null);
            SetTitleLabel();
        }

        private void SetTitleLabel()
        {
            if ((string)statusLabel.Content != "ERROR" && (string)statusLabel.Content != "NORMAL")
            {
                titleLabel.Content = "It is ON!";
            }
        }
    }
}
