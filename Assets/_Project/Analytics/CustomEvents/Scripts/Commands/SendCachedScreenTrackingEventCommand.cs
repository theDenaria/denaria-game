using System;
using _Project.Analytics.Core.Scripts.Signals;
using _Project.Analytics.CustomEvents.Scripts.Models;
using _Project.Utilities;
using strange.extensions.command.impl;
using UnityEngine;

namespace _Project.Analytics.CustomEvents.Scripts.Commands
{
    public class SendCachedScreenTrackingEventCommand : Command
    {
        [Inject] public SendAnalyticsEventSignal SendAnalyticsEventSignal { get; set; }
        public override void Execute()
        {
            string sendScreenTrackingEventString =
                PlayerPrefs.GetString(Constants.SCREEN_TRACKING_EVENT_PREF_KEY, Constants.NO_EVENT);
                        
            if (sendScreenTrackingEventString.Equals(Constants.NO_EVENT)) return;
            
            string[] parts = sendScreenTrackingEventString.Split(Constants.STRING_CONVERTER_ELEMENT_DIVIDER);
            string previousSceneId = parts[0];
            string currentSceneId = String.Empty;
            int screenDuration = int.Parse(parts[1]);
            string request = "quit_game";
            string result = "success";
            string eventTimeStamp = parts[2];
            
            SendAnalyticsEventSignal.Dispatch(new ScreenTrackingFirebaseAnalyticsEvent(previousSceneId, currentSceneId,
                screenDuration, request, result, eventTimeStamp));
        }
    }
}