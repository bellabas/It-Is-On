using Microsoft.Toolkit.Mvvm.ComponentModel;
using Microsoft.Toolkit.Mvvm.DependencyInjection;
using Microsoft.Toolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace ItIsOn.ViewModel
{
    public class MainWindowViewModel : ObservableRecipient
    {
        public string SelectedMode { get; private set; }
        public string Status { get; private set; }

        public ICommand PreventScreensaverAndSleepCommand { get; set; }
        public ICommand PreventSleepCommand { get; set; }


        public static bool IsInDesignMode
        {
            get
            {
                var prop = DesignerProperties.IsInDesignModeProperty;
                return (bool)DependencyPropertyDescriptor.FromProperty(prop, typeof(FrameworkElement)).Metadata.DefaultValue;
            }
        }


        public MainWindowViewModel()
            : this(IsInDesignMode ? null : Ioc.Default.GetService<IThreadExecutionStateLogic>())
        {

        }

        public MainWindowViewModel(IThreadExecutionStateLogic threadExecutionStateLogic)
        {
            PreventScreensaverAndSleepCommand = new RelayCommand(
            () =>
            {
                threadExecutionStateLogic.PreventScreensaverAndSleep();
                SelectedMode = "prevent SCREENSAVER and SLEEP";
                OnPropertyChanged(nameof(SelectedMode));
            }
            );


            PreventSleepCommand = new RelayCommand(
            () =>
            {
                threadExecutionStateLogic.PreventSleep();
                SelectedMode = "prevent SLEEP";
                OnPropertyChanged(nameof(SelectedMode));
            }
            );

            Messenger.Register<MainWindowViewModel, string, string>(this, "ThreadExecutionStateStatus", (recipient, msg) =>
            {
                Status = msg;
                OnPropertyChanged(nameof(Status));
            });

        }



    }
}
