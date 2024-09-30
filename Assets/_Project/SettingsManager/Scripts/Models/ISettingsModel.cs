namespace _Project.SettingsManager.Scripts.Models
{
    public interface ISettingsModel
    {
        float ResolutionIndex { get; set; }
        bool IsFullscreen { get; set; }
        int QualityLevel { get; set; }
        float MasterVolume { get; set; }
        float GameSoundVolume { get; set; }
        float MenuMusicVolume { get; set; }
        string SoundDevice { get; set; }
        string MoveForwardKey { get; set; }
        string MoveBackwardKey { get; set; }
        string MoveLeftKey { get; set; }
        string MoveRightKey { get; set; }
        string JumpKey { get; set; }
        string CrouchKey { get; set; }
    }
}
