namespace _Project.SceneManagementUtilities
{
    public class NotifySceneChangeCommandData
    {
        public string TargetScene { get; set; }
        public string Request { get; set; }
        public string Result { get; set; }

        public NotifySceneChangeCommandData(string targetScene, string request, string result)
        {
            TargetScene = targetScene;
            Request = request;
            Result = result;
        }
    }
}