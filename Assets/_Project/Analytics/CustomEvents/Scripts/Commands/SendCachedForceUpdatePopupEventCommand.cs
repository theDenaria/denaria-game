using _Project.Analytics.Core.Scripts.Signals;
using _Project.Analytics.CustomEvents.Scripts.Models;
using _Project.LoggingAndDebugging;
using _Project.StrangeIOCUtility.Scripts.Utilities;
using _Project.Utilities;
using strange.extensions.command.impl;
using UnityEngine;

namespace _Project.Analytics.CustomEvents.Scripts.Commands
{
    public class SendCachedForceUpdatePopupEventCommand : Command
    {
        [Inject] public SendAnalyticsEventSignal SendAnalyticsEventSignal { get; set; }

        public override void Execute()
        {
            string sendForceUpdatePopupEventString =
                PlayerPrefs.GetString(Constants.FORCE_UPDATE_POPUP_GAME_LEFT_EVENT_PREF_KEY, Constants.NO_EVENT);

            DebugLoggerMuteable.Log("sendForceUpdatePopupEventString:" +sendForceUpdatePopupEventString);
            if (sendForceUpdatePopupEventString.Equals(Constants.NO_EVENT))
            {
                DebugLoggerMuteable.Log("sendForceUpdatePopupEventString is NO_EVENT");

                return;
            }

            DebugLoggerMuteable.Log("sendForceUpdatePopupEventString is not NO_EVENT");

            SendAnalyticsEventSignal.Dispatch(InjectedObjectFactory.GetInjectedInstance<ForceUpdatePopupFirebaseAnalyticsEvent>()
                .SetParametersAndReturn("quit_game", "success"));
        }
    }
}