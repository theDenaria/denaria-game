namespace _Project.SceneManagementUtilities.Scripts.Models
{
    public class CurrentSceneModel : ICurrentSceneModel
    {
        public string CurrentSceneId { get; set; }
        public string PreviousSceneId { get; set; }
        public long SceneOpenedEpochTime { get; set; }
        public long SceneBackgroundSpendTime { get; set; } = 0;
        public long SceneUnfocusEpochTime { get; set; } = 0;
    }
}