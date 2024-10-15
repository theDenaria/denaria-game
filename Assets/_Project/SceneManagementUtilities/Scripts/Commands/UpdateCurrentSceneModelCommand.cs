//using _Project.Analytics.Models;
//using _Project.Analytics.Signals;

using _Project.SceneManagementUtilities.Scripts.Models;
using strange.extensions.command.impl;

namespace _Project.SceneManagementUtilities.Scripts.Commands
{
    public class UpdateCurrentSceneModelCommand : Command
    {
        [Inject] public SceneChangedCommandData SceneChangedCommandData { get; set; }
        [Inject] public ICurrentSceneModel CurrentSceneModel { get; set; }
        //[Inject] public SendAnalyticsEventSignal SendAnalyticsEventSignal { get; set; }
        public override void Execute()
        {
            if (SceneChangedCommandData.TargetScene == CurrentSceneModel.CurrentSceneId) return;
            
            long currentEpochTime = DateUtility.GetCurrentEpochSeconds();

            string previousScreenId = CurrentSceneModel.CurrentSceneId;
            string targetScreenId = SceneChangedCommandData.TargetScene;
            long sceneOpenedTime = CurrentSceneModel.SceneOpenedEpochTime;
            long sceneBackgroundSpendTime = CurrentSceneModel.SceneBackgroundSpendTime;
            
            //TODO: Uncomment after adding ScreenTrackingFirebaseAnalyticsEvent. -14 August 2024
            /*SendAnalyticsEventSignal.Dispatch(new ScreenTrackingFirebaseAnalyticsEvent(
                previousScreenId, 
                targetScreenId, 
                (int)((currentEpochTime - sceneOpenedTime) - sceneBackgroundSpendTime), 
                NotifySceneChangeCommandData.Request, 
                NotifySceneChangeCommandData.Result)
            );*/
            
            if (SceneChangedCommandData.Result.Equals("success"))
            {
                UpdateCurrentSceneModel(currentEpochTime);
            }
        }

        private void UpdateCurrentSceneModel(long currentEpochTime)
        {
            CurrentSceneModel.PreviousSceneId = CurrentSceneModel.CurrentSceneId;
            CurrentSceneModel.CurrentSceneId = SceneChangedCommandData.TargetScene;
            CurrentSceneModel.SceneOpenedEpochTime = currentEpochTime;
            CurrentSceneModel.SceneBackgroundSpendTime = 0;
            CurrentSceneModel.SceneUnfocusEpochTime = 0;
        }
    }
}