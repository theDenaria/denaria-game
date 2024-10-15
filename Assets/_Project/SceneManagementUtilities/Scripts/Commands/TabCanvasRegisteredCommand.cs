//using _Project.NavigationBar.Scripts.Commands;
//using _Project.NavigationBar.Scripts.Enums;

using _Project.SceneManagementUtilities.Scripts.Models;
using _Project.Utilities;
using strange.extensions.command.impl;

namespace _Project.SceneManagementUtilities.Scripts.Commands
{
    public class TabCanvasRegisteredCommand : Command
    {
        //TODO: Uncomment after adding OnNavigationButtonClickSignal. -14 August 2024
        //[Inject] public OnNavigationButtonClickSignal OnNavigationButtonClickSignal { get; set; }
        [Inject] public ICurrentSceneModel CurrentSceneModel { get; set; }
        public override void Execute()
        {
            if (CurrentSceneModel.CurrentSceneId.Equals(Constants.STORE_SCENE))
            {
                //OnNavigationButtonClickSignal.Dispatch(NavigationButtonType.InAppPurchase);
            }
        }
    }
}