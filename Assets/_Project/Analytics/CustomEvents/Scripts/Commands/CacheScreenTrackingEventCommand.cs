using _Project.SceneManagementUtilities.Scripts.Models;
using _Project.Utilities;
using strange.extensions.command.impl;
using UnityEngine;

namespace _Project.Analytics.CustomEvents.Scripts.Commands
{
    public class CacheScreenTrackingEventCommand : Command
    {
        [Inject] public ICurrentSceneModel CurrentSceneModel { get; set; }
        public override void Execute()
        {
            string currentSceneId = CurrentSceneModel.CurrentSceneId;
            long eventTimeStamp = DateUtility.GetCurrentEpochSeconds();
            string screenDuration = ((eventTimeStamp - CurrentSceneModel.SceneOpenedEpochTime) - CurrentSceneModel.SceneBackgroundSpendTime).ToString();

            string encodedString = currentSceneId + Constants.STRING_CONVERTER_ELEMENT_DIVIDER +
                                   screenDuration + Constants.STRING_CONVERTER_ELEMENT_DIVIDER +
                                   eventTimeStamp.ToString();

            PlayerPrefs.SetString(Constants.SCREEN_TRACKING_EVENT_PREF_KEY, encodedString);
        }
    }
}