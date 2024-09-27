using UnityEngine;

namespace _Project.SettingsManager.Scripts.Models
{
    public class SettingsModel
    {
        public int ResolutionIndex { get; set; }
        public bool IsFullscreen { get; set; }
        public int QualityLevel { get; set; }

        public float MasterVolume { get; set; }
        public float GameSoundVolume { get; set; }
        public float MenuMusicVolume { get; set; }

        public string MoveForwardKey { get; set; }
        public string MoveBackwardKey { get; set; }
        public string MoveLeftKey { get; set; }
        public string MoveRightKey { get; set; }
        public string JumpKey { get; set; }
        public string CrouchKey { get; set; }

        public SettingsModel()
        {
            // Load from PlayerPrefs or set defaults
            ResolutionIndex = PlayerPrefs.GetInt("ResolutionIndex", 0);
            IsFullscreen = PlayerPrefs.GetInt("Fullscreen", 1) == 1;
            QualityLevel = PlayerPrefs.GetInt("QualityLevel", 2);

            MasterVolume = PlayerPrefs.GetFloat("MasterVolume", 1f);
            GameSoundVolume = PlayerPrefs.GetFloat("GameSoundVolume", 1f);
            MenuMusicVolume = PlayerPrefs.GetFloat("MenuMusicVolume", 1f);

            MoveForwardKey = PlayerPrefs.GetString("MoveForwardKey", "W");
            MoveBackwardKey = PlayerPrefs.GetString("MoveBackwardKey", "S");
            MoveLeftKey = PlayerPrefs.GetString("MoveLeftKey", "A");
            MoveRightKey = PlayerPrefs.GetString("MoveRightKey", "D");
            JumpKey = PlayerPrefs.GetString("JumpKey", "Space");
            CrouchKey = PlayerPrefs.GetString("CrouchKey", "C");
        }

        public void Save()
        {
            PlayerPrefs.SetInt("ResolutionIndex", ResolutionIndex);
            PlayerPrefs.SetInt("Fullscreen", IsFullscreen ? 1 : 0);
            PlayerPrefs.SetInt("QualityLevel", QualityLevel);

            PlayerPrefs.SetFloat("MasterVolume", MasterVolume);
            PlayerPrefs.SetFloat("GameSoundVolume", GameSoundVolume);
            PlayerPrefs.SetFloat("MenuMusicVolume", MenuMusicVolume);

            PlayerPrefs.SetString("MoveForwardKey", MoveForwardKey);
            PlayerPrefs.SetString("MoveBackwardKey", MoveBackwardKey);
            PlayerPrefs.SetString("MoveLeftKey", MoveLeftKey);
            PlayerPrefs.SetString("MoveRightKey", MoveRightKey);
            PlayerPrefs.SetString("JumpKey", JumpKey);
            PlayerPrefs.SetString("CrouchKey", CrouchKey);

            PlayerPrefs.Save();
        }
    }
}
