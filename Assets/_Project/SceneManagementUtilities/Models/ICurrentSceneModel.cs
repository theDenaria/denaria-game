namespace _Project.SceneManagementUtilities.Models
{
    public interface ICurrentSceneModel
    {
        string CurrentSceneId { get; set; }
        string PreviousSceneId { get; set; }
        long SceneOpenedEpochTime { get; set; }
        long SceneBackgroundSpendTime { get; set; }
        long SceneUnfocusEpochTime { get; set; }
    }
}