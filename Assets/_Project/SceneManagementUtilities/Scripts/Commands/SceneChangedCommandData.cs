namespace _Project.SceneManagementUtilities.Scripts.Commands
{
    public class SceneChangedCommandData
    {
        public string TargetScene { get; set; }
        public string Request { get; set; }
        public string Result { get; set; }

        public SceneChangedCommandData(string targetScene, string request, string result)
        {
            TargetScene = targetScene;
            Request = request;
            Result = result;
        }
    }
}