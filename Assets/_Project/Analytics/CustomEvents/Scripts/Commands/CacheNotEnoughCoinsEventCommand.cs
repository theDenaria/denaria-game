using _Project.Analytics.CustomEvents.Scripts.Enums;
using _Project.SceneManagementUtilities.Scripts.Models;
using _Project.Utilities;
using strange.extensions.command.impl;
using UnityEngine;

namespace _Project.Analytics.CustomEvents.Scripts.Commands
{
    public class CacheNotEnoughCoinsEventCommand : Command
    {
        [Inject] public ICurrentSceneModel CurrentSceneModel { get; set; }
        //[Inject] public INotEnoughGemsCanvasLeftTracking NotEnoughGemsCanvasLeftTracking { get; set; }
        public override void Execute()
        {
            /*if (!NotEnoughGemsCanvasLeftTracking.TrackNotEnoughGems)
            {
                return;
            }*/
            string currentSceneId = CurrentSceneModel.CurrentSceneId;

            string encodedString = currentSceneId + Constants.STRING_CONVERTER_ELEMENT_DIVIDER +
                                   NotEnoughCoinsPopupRequestTypes.quit_game;
            
            PlayerPrefs.SetString(Constants.NOT_ENOUGH_COINS_CANVAS_LEFT_EVENT_PREF_KEY, encodedString);
        }
    }
}