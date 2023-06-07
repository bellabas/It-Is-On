using ItIsOn.Logic;
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
        public string Status { get; private set; }

        public ICommand PreventScreensaverAndSleepCommand { get; set; }
        public ICommand PreventSleepCommand { get; set; }
        public ICommand SetNormalModeCommand { get; set; }

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
                threadExecutionStateLogic.SetMode(ModeOptions.PreventScreensaverAndSleep);
            }
            );


            PreventSleepCommand = new RelayCommand(
            () =>
            {
                threadExecutionStateLogic.SetMode(ModeOptions.PreventSleep);
            }
            );

            SetNormalModeCommand = new RelayCommand(
            () =>
            {
                threadExecutionStateLogic.SetMode(ModeOptions.Normal);
            }
            );

            Messenger.Register<MainWindowViewModel, string, string>(this, "ThreadExecutionStateStatus", (recipient, msg) =>
            {
                Status = msg;
                OnPropertyChanged(nameof(Status));
            });

            Status = "NORMAL";
        }



    }
}
