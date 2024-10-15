using _Project.Analytics.Core.Scripts.Signals;
using _Project.Analytics.CustomEvents.Scripts.Enums;
using _Project.Analytics.CustomEvents.Scripts.Models;
using _Project.StrangeIOCUtility.Scripts.Utilities;
using _Project.Utilities;
using strange.extensions.command.impl;
using UnityEngine;

namespace _Project.Analytics.CustomEvents.Scripts.Commands
{
    public class SendCachedNotEnoughCoinsEventCommand : Command
    {
        [Inject] public SendAnalyticsEventSignal SendAnalyticsEventSignal { get; set; }
        public override void Execute()
        {
            string notEnoughCoinsCanvasLeftEventString =
                PlayerPrefs.GetString(Constants.NOT_ENOUGH_COINS_CANVAS_LEFT_EVENT_PREF_KEY, Constants.NO_EVENT);
            Debug.Log(notEnoughCoinsCanvasLeftEventString);
            if (notEnoughCoinsCanvasLeftEventString.Equals(Constants.NO_EVENT)) return;

            PlayerPrefs.SetString(Constants.NOT_ENOUGH_COINS_CANVAS_LEFT_EVENT_PREF_KEY, Constants.NO_EVENT); //Might not be needed

            string[] parts = notEnoughCoinsCanvasLeftEventString.Split(Constants.STRING_CONVERTER_ELEMENT_DIVIDER);
            string source = parts[0];
            string request = parts[1];

            SendAnalyticsEventSignal.Dispatch(InjectedObjectFactory
                .GetInjectedInstance<NotEnoughCoinsCanvasLeftFirebaseAnalyticsEvent>()
                .SetParametersAndReturn(source, NotEnoughCoinsPopupRequestTypes.quit_game, NotEnoughCoinsPopupPurchaseResultTypes.none));
        }
    }
}