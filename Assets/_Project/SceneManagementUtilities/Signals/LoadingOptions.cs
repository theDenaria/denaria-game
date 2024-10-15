using _Project.SceneManagementUtilities.Utilities;

namespace _Project.SceneManagementUtilities.Signals
{
    public class LoadingOptions
    {
        public bool WillReloadExistingScenes { get; set; }
        public bool WillUseLoadingScreen { get; set; }
        public SceneGroupType CustomLoadingScreen { get; set; }

        /*public LoadingOptions(bool willReloadExistingScenes = false, bool willUseLoadingScreen = false)
        {
            WillReloadExistingScenes = willReloadExistingScenes;
            WillUseLoadingScreen = willUseLoadingScreen;
        }*/
        
        //SceneGroupType that consists customLoadingScreen should consist no other scenes.
        public LoadingOptions(SceneGroupType customLoadingScreen = SceneGroupType.None, bool willReloadExistingScenes = false)
        {
            CustomLoadingScreen = customLoadingScreen;
            WillReloadExistingScenes = willReloadExistingScenes;
        }
    }
}