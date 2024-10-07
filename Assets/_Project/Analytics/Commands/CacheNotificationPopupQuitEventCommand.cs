//using _Project.PushNotifications.Scripts.Models;
using _Project.Utilities;
using strange.extensions.command.impl;
using UnityEngine;

namespace _Project.Analytics.Commands
{
    public class CacheNotificationPopupQuitEventCommand : Command
    {
        //[Inject] public IPopupState PopupState { get; set; }
        public override void Execute()
        {
            //if (PopupState.IsEnabled)
            if (true)
            {
                string result = "quit";
                string currentEpochTime = DateUtility.GetCurrentEpochSeconds().ToString();

                string encodedString = result + Constants.STRING_CONVERTER_ELEMENT_DIVIDER +
                                       currentEpochTime;
                
                PlayerPrefs.SetString(Constants.NOTIFICATION_POPUP_QUIT_EVENT_PREF_KEY, encodedString);
            }
            else
            {
                PlayerPrefs.SetString(Constants.NOTIFICATION_POPUP_QUIT_EVENT_PREF_KEY, Constants.NO_EVENT);
            }
        }
    }
}