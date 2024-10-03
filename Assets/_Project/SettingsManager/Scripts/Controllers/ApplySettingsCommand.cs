using strange.extensions.command.impl;
using _Project.SettingsManager.Scripts.Models;
using UnityEngine;

public class ApplySettingsCommand : Command
{
    [Inject] public ISettingsModel Model { get; set; }

    public override void Execute()
    {
        // Save Video Settings
        PlayerPrefs.SetFloat("Resolution", Model.ResolutionIndex);
        PlayerPrefs.SetInt("Fullscreen", Model.IsFullscreen ? 1 : 0);
        PlayerPrefs.SetInt("Quality", Model.QualityLevel);

        // Save Audio Settings
        PlayerPrefs.SetFloat("MasterVolume", Model.MasterVolume);
        PlayerPrefs.SetFloat("GameSoundVolume", Model.GameSoundVolume);
        PlayerPrefs.SetFloat("MenuMusicVolume", Model.MenuMusicVolume);
        PlayerPrefs.SetString("SoundDevice", Model.SoundDevice ?? "");

        // Save Hotkeys
        SaveHotkey("MoveForwardKey", Model.MoveForwardKey);
        SaveHotkey("MoveBackwardKey", Model.MoveBackwardKey);
        SaveHotkey("MoveLeftKey", Model.MoveLeftKey);
        SaveHotkey("MoveRightKey", Model.MoveRightKey);
        SaveHotkey("JumpKey", Model.JumpKey);
        SaveHotkey("CrouchKey", Model.CrouchKey);

        PlayerPrefs.Save();
        Debug.Log("Settings saved successfully");
    }

    private void SaveHotkey(string key, object value)
    {
        if (value != null)
        {
            PlayerPrefs.SetString(key, value.ToString());
        }
        else
        {
            Debug.LogWarning($"{key} is null, skipping save");
        }
    }
}

