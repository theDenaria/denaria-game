using strange.extensions.mediation.impl;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace _Project.SettingsManager.Scripts.Views
{
    public class SettingsView : MonoBehaviour
    {
        public TMP_Dropdown resolutionDropdown;
        public Toggle fullscreenToggle;
        public Slider qualitySlider;

        public TMP_InputField moveForwardInputField;
        public TMP_InputField moveBackwardInputField;
        public TMP_InputField moveLeftInputField;
        public TMP_InputField moveRightInputField;
        public TMP_InputField jumpInputField;
        public TMP_InputField crouchInputField;

        public Slider masterVolumeSlider;
        public Slider gameSoundSlider;
        public Slider menuMusicSlider;
        public TMP_Dropdown soundDeviceDropdown;


        public Button applyButton;
        public Button restoreDefaultsButton;
    }
}
