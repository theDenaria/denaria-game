using _Project.SceneManagementUtilities.Utilities;
namespace _Project.SceneManagementUtilities.Scripts.Signals
{
    public class LoadingOptions
    {
        public bool WillReloadExistingScenes { get; set; }
        public bool WillUseLoadingScreen { get; set; }
        public SceneGroupType CustomLoadingScreen { get; set; }
        
        //SceneGroupType that consists customLoadingScreen should consist no other scenes.
        public LoadingOptions(SceneGroupType customLoadingScreen = SceneGroupType.None, bool willReloadExistingScenes = false)
        {
            CustomLoadingScreen = customLoadingScreen;
            WillReloadExistingScenes = willReloadExistingScenes;
        }
    }
}