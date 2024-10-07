using _Project.Analytics.Models;
using _Project.Analytics.Signals;
using _Project.LoggingAndDebugging;
using _Project.StrangeIOCUtility;
using _Project.Utilities;
using strange.extensions.command.impl;
using UnityEngine;

namespace _Project.Analytics.Commands
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