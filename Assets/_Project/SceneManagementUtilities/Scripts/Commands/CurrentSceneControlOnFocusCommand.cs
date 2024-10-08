using _Project.SceneManagementUtilities.Scripts.Models;
using strange.extensions.command.impl;

namespace _Project.SceneManagementUtilities.Scripts.Commands
{
    public class CurrentSceneControlOnFocusCommand : Command
    {
        [Inject] public bool IsFocus { get; set; }
        [Inject] public ICurrentSceneModel CurrentSceneModel { get; set; }
        public override void Execute()
        {
            if (IsFocus)
            {
                if (CurrentSceneModel.SceneUnfocusEpochTime > 0)
                {
                    CurrentSceneModel.SceneBackgroundSpendTime =
                        (int)(DateUtility.GetCurrentEpochSeconds() - CurrentSceneModel.SceneUnfocusEpochTime) +
                        CurrentSceneModel.SceneBackgroundSpendTime;
                }
            }
            else
            {
                CurrentSceneModel.SceneUnfocusEpochTime = DateUtility.GetCurrentEpochSeconds();
            }
        }
    }
}