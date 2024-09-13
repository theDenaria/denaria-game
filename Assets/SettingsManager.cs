using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SettingsManager : MonoBehaviour
{
    // Existing fields for resolution, fullscreen, and quality settings
    public TMP_Dropdown resolutionDropdown;
    public Toggle fullscreenToggle;
    public Slider qualitySlider;

    // Fields for Hotkey InputFields
    public TMP_InputField moveForwardInputField;
    public TMP_InputField moveBackwardInputField;
    public TMP_InputField moveLeftInputField;
    public TMP_InputField moveRightInputField;
    public TMP_InputField jumpInputField;
    public TMP_InputField crouchInputField;

    // Audio Settings UI elements
    public Slider masterVolumeSlider;
    public Slider gameSoundSlider;
    public Slider menuMusicSlider;
    public TMP_Dropdown soundDeviceDropdown;

    Resolution[] resolutions;

    private void Start()
    {
        // Initialize Video Settings
        InitializeVideoSettings();

        // Initialize Audio Settings
        InitializeAudioSettings();

        // Initialize Hotkeys
        InitializeHotkeys();

        // Add listeners for video settings changes
        resolutionDropdown.onValueChanged.AddListener(SetResolution);
        fullscreenToggle.onValueChanged.AddListener(SetFullscreen);
        qualitySlider.onValueChanged.AddListener(SetQuality);
    }

    // Initialize Video Settings
    private void InitializeVideoSettings()
    {
        resolutions = Screen.resolutions;
        resolutionDropdown.ClearOptions();

        int currentResolutionIndex = 0;
        for (int i = 0; i < resolutions.Length; i++)
        {
            string option = resolutions[i].width + " x " + resolutions[i].height;
            resolutionDropdown.options.Add(new TMP_Dropdown.OptionData(option));

            if (resolutions[i].width == Screen.currentResolution.width &&
                resolutions[i].height == Screen.currentResolution.height)
            {
                currentResolutionIndex = i;
            }
        }

        resolutionDropdown.value = PlayerPrefs.GetInt("ResolutionIndex", currentResolutionIndex);
        fullscreenToggle.isOn = PlayerPrefs.GetInt("Fullscreen", Screen.fullScreen ? 1 : 0) == 1;
        qualitySlider.value = PlayerPrefs.GetInt("QualityLevel", QualitySettings.GetQualityLevel());
    }

    // Save and apply resolution
    public void SetResolution(int resolutionIndex)
    {
        Resolution resolution = resolutions[resolutionIndex];
        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
        PlayerPrefs.SetInt("ResolutionIndex", resolutionIndex);
        PlayerPrefs.Save();
    }

    // Save and apply fullscreen setting
    public void SetFullscreen(bool isFullscreen)
    {
        Screen.fullScreen = isFullscreen;
        PlayerPrefs.SetInt("Fullscreen", isFullscreen ? 1 : 0);
        PlayerPrefs.Save();
    }

    // Save and apply quality settings
    public void SetQuality(float qualityIndex)
    {
        // Set the quality level based on the slider value (cast float to int)
        int qualityLevel = Mathf.RoundToInt(qualityIndex);

        // Apply the selected quality level
        QualitySettings.SetQualityLevel(qualityLevel);

        // Save the selected quality level to PlayerPrefs
        PlayerPrefs.SetInt("RenderQualityLevel", qualityLevel);
        PlayerPrefs.Save();
    }

    // Initialize Audio Settings
    private void InitializeAudioSettings()
    {
        // Load previously saved audio settings or default to max volume
        masterVolumeSlider.value = PlayerPrefs.GetFloat("MasterVolume", 1f); // Default is max volume
        gameSoundSlider.value = PlayerPrefs.GetFloat("GameSoundVolume", 1f);
        menuMusicSlider.value = PlayerPrefs.GetFloat("MenuMusicVolume", 1f);

        // You can populate the sound device dropdown here if needed
        // Let's assume it's already populated with devices in the UI
        soundDeviceDropdown.value = PlayerPrefs.GetInt("SoundDevice", 0); // Default to the first device

        // Add listeners to detect changes
        masterVolumeSlider.onValueChanged.AddListener(SetMasterVolume);
        gameSoundSlider.onValueChanged.AddListener(SetGameSoundVolume);
        menuMusicSlider.onValueChanged.AddListener(SetMenuMusicVolume);
        soundDeviceDropdown.onValueChanged.AddListener(SetSoundDevice);
    }

    // Apply Master Volume
    public void SetMasterVolume(float volume)
    {
        // Set master volume here (if you have an audio manager, you'd use it here)
        AudioListener.volume = volume;

        // Save the value
        PlayerPrefs.SetFloat("MasterVolume", volume);
        PlayerPrefs.Save();
    }

    // Apply Game Sound Volume
    public void SetGameSoundVolume(float volume)
    {
        // Apply this value to game sounds specifically (if you have a mixer or separate audio manager)
        PlayerPrefs.SetFloat("GameSoundVolume", volume);
        PlayerPrefs.Save();
    }

    // Apply Menu Music Volume
    public void SetMenuMusicVolume(float volume)
    {
        // Apply this value to menu music specifically (if you have a mixer or separate audio manager)
        PlayerPrefs.SetFloat("MenuMusicVolume", volume);
        PlayerPrefs.Save();
    }

    // Apply Sound Device Selection
    public void SetSoundDevice(int deviceIndex)
    {
        // Logic for selecting sound devices may vary based on your game setup
        PlayerPrefs.SetInt("SoundDevice", deviceIndex);
        PlayerPrefs.Save();
    }

    // Apply all changes (Apply Settings button)
    public void ApplyAudioSettings()
    {
        // This method can save any further settings if necessary and apply all audio changes
        PlayerPrefs.Save();
        Debug.Log("Audio settings applied");
    }

    // Restore default audio settings (Restore to Default button)
    public void RestoreDefaultAudioSettings()
    {
        // Reset sliders to default values
        SetMasterVolume(1f); // Default max volume
        SetGameSoundVolume(1f); // Default max game sound
        SetMenuMusicVolume(1f); // Default max menu music

        // Reset the dropdown to the first device
        SetSoundDevice(0);

        // Update the UI
        masterVolumeSlider.value = 1f;
        gameSoundSlider.value = 1f;
        menuMusicSlider.value = 1f;
        soundDeviceDropdown.value = 0;

        Debug.Log("Audio settings restored to default");
    }

    // Initialize Hotkeys
    private void InitializeHotkeys()
    {
        // Load saved keybindings or use default values
        moveForwardInputField.text = PlayerPrefs.GetString("MoveForwardKey", "W");
        moveBackwardInputField.text = PlayerPrefs.GetString("MoveBackwardKey", "S");
        moveLeftInputField.text = PlayerPrefs.GetString("MoveLeftKey", "A");
        moveRightInputField.text = PlayerPrefs.GetString("MoveRightKey", "D");
        jumpInputField.text = PlayerPrefs.GetString("JumpKey", "Space");
        crouchInputField.text = PlayerPrefs.GetString("CrouchKey", "C");

        // Add listeners for keybinding changes
        moveForwardInputField.onEndEdit.AddListener(delegate { SetKeybinding("MoveForwardKey", moveForwardInputField.text); });
        moveBackwardInputField.onEndEdit.AddListener(delegate { SetKeybinding("MoveBackwardKey", moveBackwardInputField.text); });
        moveLeftInputField.onEndEdit.AddListener(delegate { SetKeybinding("MoveLeftKey", moveLeftInputField.text); });
        moveRightInputField.onEndEdit.AddListener(delegate { SetKeybinding("MoveRightKey", moveRightInputField.text); });
        jumpInputField.onEndEdit.AddListener(delegate { SetKeybinding("JumpKey", jumpInputField.text); });
        crouchInputField.onEndEdit.AddListener(delegate { SetKeybinding("CrouchKey", crouchInputField.text); });
    }

    // Save keybindings
    public void SetKeybinding(string keyName, string newKey)
    {
        PlayerPrefs.SetString(keyName, newKey);
        PlayerPrefs.Save();
    }

    // Apply Settings Method (For Apply Button)
    public void ApplySettings()
    {
        PlayerPrefs.Save(); // Save all settings
        Debug.Log("Settings Applied");
    }

    // Restore Default Settings (For Restore Defaults Button)
    public void RestoreDefaultSettings()
    {
        // Restore default video settings (you can set your default values)
        SetResolution(0);  // Default resolution
        SetFullscreen(true); // Default fullscreen setting
        SetQuality(2);  // Default quality

        // Restore default hotkeys
        SetKeybinding("MoveForwardKey", "W");
        SetKeybinding("MoveBackwardKey", "S");
        SetKeybinding("MoveLeftKey", "A");
        SetKeybinding("MoveRightKey", "D");
        SetKeybinding("JumpKey", "Space");
        SetKeybinding("CrouchKey", "C");

        // Update the UI with default values
        moveForwardInputField.text = "W";
        moveBackwardInputField.text = "S";
        moveLeftInputField.text = "A";
        moveRightInputField.text = "D";
        jumpInputField.text = "Space";
        crouchInputField.text = "C";

        Debug.Log("Settings Restored to Default");
    }
}
