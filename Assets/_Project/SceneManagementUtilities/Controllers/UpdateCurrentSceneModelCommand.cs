//using _Project.Analytics.Models;
//using _Project.Analytics.Signals;
using _Project.SceneManagementUtilities.Models;
using strange.extensions.command.impl;

namespace _Project.SceneManagementUtilities.Controllers
{
    public class UpdateCurrentSceneModelCommand : Command
    {
        [Inject] public NotifySceneChangeCommandData NotifySceneChangeCommandData { get; set; }
        [Inject] public ICurrentSceneModel CurrentSceneModel { get; set; }
        //[Inject] public SendAnalyticsEventSignal SendAnalyticsEventSignal { get; set; }
        public override void Execute()
        {
            if (NotifySceneChangeCommandData.TargetScene == CurrentSceneModel.CurrentSceneId) return;
            
            long currentEpochTime = DateUtility.GetCurrentEpochSeconds();

            string previousScreenId = CurrentSceneModel.CurrentSceneId;
            string targetScreenId = NotifySceneChangeCommandData.TargetScene;
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
            
            if (NotifySceneChangeCommandData.Result.Equals("success"))
            {
                UpdateCurrentSceneModel(currentEpochTime);
            }
        }

        private void UpdateCurrentSceneModel(long currentEpochTime)
        {
            CurrentSceneModel.PreviousSceneId = CurrentSceneModel.CurrentSceneId;
            CurrentSceneModel.CurrentSceneId = NotifySceneChangeCommandData.TargetScene;
            CurrentSceneModel.SceneOpenedEpochTime = currentEpochTime;
            CurrentSceneModel.SceneBackgroundSpendTime = 0;
            CurrentSceneModel.SceneUnfocusEpochTime = 0;
        }
    }
}