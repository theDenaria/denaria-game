using _Project.SettingsManager.Scripts.Models;
using strange.extensions.command.impl;
using UnityEngine;

namespace _Project.SettingsManager.Scripts.Controllers
{
    public class RestoreDefaultSettingsCommand : Command
    {
        [Inject] public ISettingsModel Model { get; set; }

        public override void Execute()
        {
            // Reset Video Settings to default
            Model.ResolutionIndex = 1920; // Default resolution
            Model.IsFullscreen = true; // Default to fullscreen
            Model.QualityLevel = 1;    // Default quality level

            // Reset Audio Settings
            Model.MasterVolume = 1.0f;
            Model.GameSoundVolume = 1.0f;
            Model.MenuMusicVolume = 1.0f;
            Model.SoundDevice = "Default";
            // Reset Hotkeys
            Model.MoveForwardKey = "W";
            Model.MoveBackwardKey = "S";
            Model.MoveLeftKey = "A";
            Model.MoveRightKey = "D";
            Model.JumpKey = "Space";
            Model.CrouchKey = "LeftControl";

            PlayerPrefs.SetFloat("Resolution", Model.ResolutionIndex);
            PlayerPrefs.SetInt("Fullscreen", Model.IsFullscreen ? 1 : 0);
            PlayerPrefs.SetFloat("Quality", Model.QualityLevel);

            PlayerPrefs.SetFloat("MasterVolume", Model.MasterVolume);
            PlayerPrefs.SetFloat("GameSound", Model.GameSoundVolume);
            PlayerPrefs.SetFloat("MenuMusic", Model.MenuMusicVolume);

            PlayerPrefs.SetString("ForwardKey", Model.MoveForwardKey.ToString());
            PlayerPrefs.SetString("BackwardKey", Model.MoveBackwardKey.ToString());
            PlayerPrefs.SetString("LeftKey", Model.MoveLeftKey.ToString());
            PlayerPrefs.SetString("RightKey", Model.MoveRightKey.ToString());
            PlayerPrefs.SetString("JumpKey", Model.JumpKey.ToString());
            PlayerPrefs.SetString("CrouchKey", Model.CrouchKey.ToString());

            PlayerPrefs.Save();
        }
    }
}
