namespace _Project.SettingsManager.Scripts.Models
{
    public class SettingsModel : ISettingsModel
    {
        public float ResolutionIndex { get; set; }
        public bool IsFullscreen { get; set; }
        public int QualityLevel { get; set; }
        public float MasterVolume { get; set; }
        public float GameSoundVolume { get; set; }
        public float MenuMusicVolume { get; set; }
        public string SoundDevice { get; set; }
        public string MoveForwardKey { get; set; }
        public string MoveBackwardKey { get; set; }
        public string MoveLeftKey { get; set; }
        public string MoveRightKey { get; set; }
        public string JumpKey { get; set; }
        public string CrouchKey { get; set; }
    }
}
