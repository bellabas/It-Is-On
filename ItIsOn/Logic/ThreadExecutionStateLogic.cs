using Microsoft.Toolkit.Mvvm.Messaging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace ItIsOn.Logic
{
    public enum ModeOptions
    {
        PreventScreensaverAndSleep = 1,
        PreventSleep = 2,
        Normal = 3
    }

    public class ThreadExecutionStateLogic : IThreadExecutionStateLogic
    {
        [Flags]
        private enum EXECUTION_STATE : uint
        {
            ES_AWAYMODE_REQUIRED = 0x00000040,
            ES_CONTINUOUS = 0x80000000,
            ES_DISPLAY_REQUIRED = 0x00000002,
            ES_SYSTEM_REQUIRED = 0x00000001
        }

        IMessenger messenger;
        private EXECUTION_STATE? CurrentState { get; set; }
        private string statusMsg;

        [DllImport("kernel32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern EXECUTION_STATE SetThreadExecutionState(EXECUTION_STATE esFlags);

        public ThreadExecutionStateLogic(IMessenger messenger)
        {
            this.messenger = messenger;
            this.CurrentState = null;
            this.statusMsg = "NORMAL";
        }

        public void SetMode(ModeOptions mode)
        {
            try
            {
                if (mode == ModeOptions.PreventScreensaverAndSleep)
                {
                    CurrentState = SetThreadExecutionState(EXECUTION_STATE.ES_CONTINUOUS | EXECUTION_STATE.ES_SYSTEM_REQUIRED | EXECUTION_STATE.ES_DISPLAY_REQUIRED);
                    statusMsg = "SCREENSAVER and SLEEP";
                }
                else if (mode == ModeOptions.PreventSleep)
                {
                    CurrentState = SetThreadExecutionState(EXECUTION_STATE.ES_CONTINUOUS | EXECUTION_STATE.ES_SYSTEM_REQUIRED);
                    statusMsg = "SLEEP";
                }
                else
                {
                    do
                    {
                        CurrentState = SetThreadExecutionState(EXECUTION_STATE.ES_CONTINUOUS);
                        statusMsg = "NORMAL";
                    } while (CurrentState == null);
                }

                if (CurrentState != null && CurrentState.GetType() == typeof(EXECUTION_STATE))
                {
                    messenger.Send(statusMsg, "ThreadExecutionStateStatus");
                }
                else
                {
                    throw new Exception();
                }
            }
            catch (Exception e)
            {
                messenger.Send("ERROR", "ThreadExecutionStateStatus");
            }
        }

    }
}
