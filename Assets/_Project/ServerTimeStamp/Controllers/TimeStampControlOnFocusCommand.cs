using System;
using _Project.LoggingAndDebugging;
using _Project.ServerTimeStamp.Signals;
using strange.extensions.command.impl;

namespace _Project.ServerTimeStamp.Controllers
{
    public class TimeStampControlOnFocusCommand : Command
    {
        [Inject] public bool IsFocus { get; set; }
        [Inject] public SetTimeStampDifferenceSignal SetTimeStampDifferenceSignal { get; set; }
        public override void Execute()
        {
            if (IsFocus)
            {
                SetTimeStampDifferenceCommandData commandData =
                    new SetTimeStampDifferenceCommandData(OnTimeStampDifference, false);
                try
                {
                    SetTimeStampDifferenceSignal.Dispatch(commandData);
                }
                catch (Exception ex)
                {
                    DebugLoggerMuteable.Log("Application First Start, Playfab is not initialized yet probably");
                }
            }
        }
        private void OnTimeStampDifference(bool succeed)
        {
            if (!succeed) return; 
        }
    }
}