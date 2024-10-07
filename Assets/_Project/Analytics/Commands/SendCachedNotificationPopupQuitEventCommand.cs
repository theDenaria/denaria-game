using _Project.Analytics.Models;
using _Project.Analytics.Signals;
using _Project.StrangeIOCUtility;
using _Project.Utilities;
using strange.extensions.command.impl;
using UnityEngine;

namespace _Project.Analytics.Commands
{
    public class SendCachedNotificationPopupQuitEventCommand : Command
    {
        [Inject] public SendAnalyticsEventSignal SendAnalyticsEventSignal { get; set; }
        public override void Execute()
        {
            string encodedString =
                PlayerPrefs.GetString(Constants.NOTIFICATION_POPUP_QUIT_EVENT_PREF_KEY, Constants.NO_EVENT);
            
            if (encodedString.Equals(Constants.NO_EVENT)) return;
            string[] parts = encodedString.Split(Constants.STRING_CONVERTER_ELEMENT_DIVIDER);
            
            string result = parts[4];
            string eventEpochTime = parts[5];
            
            SendAnalyticsEventSignal.Dispatch(InjectedObjectFactory
                .GetInjectedInstance<NotificationPopupFirebaseAnalyticsEvent>().SetParametersAndReturn(
                    result,
                    eventEpochTime
                    ));

        }
    }
}