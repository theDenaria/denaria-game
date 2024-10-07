using _Project.SceneManagementUtilities.Models;
using _Project.Utilities;
using strange.extensions.command.impl;
using UnityEngine;

namespace _Project.Analytics.Commands
{
    public class CacheForceUpdatePopupCommand : Command
    {
        [Inject] public ICurrentSceneModel CurrentSceneModel { get; set; }

        public override void Execute()
        {
            if (CurrentSceneModel.CurrentSceneId.Equals(Constants.FORCE_UPDATE_SCENE))
            {
                string encodedString = "quit_game"; //Is not used at the moment, a blank string would also do the job.
                PlayerPrefs.SetString(Constants.FORCE_UPDATE_POPUP_GAME_LEFT_EVENT_PREF_KEY, encodedString);
            }
            else
            {
                PlayerPrefs.SetString(Constants.FORCE_UPDATE_POPUP_GAME_LEFT_EVENT_PREF_KEY, Constants.NO_EVENT);
            }
        }
    }
}