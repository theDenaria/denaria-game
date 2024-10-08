namespace _Project.SceneManagementUtilities.Scripts.Signals
{
    public class LoadingOptions
    {
        public bool WillReloadExistingScenes { get; set; }
        public bool WillUseLoadingScreen { get; set; }

        public LoadingOptions(bool willReloadExistingScenes = false, bool willUseLoadingScreen = false)
        {
            WillReloadExistingScenes = willReloadExistingScenes;
            WillUseLoadingScreen = willUseLoadingScreen;
        }
    }
}