using UnityEngine;
using UnityEngine.UI;

public class SettingsMenuManager : MonoBehaviour
{
    public GameObject videoTabPanel;   // Reference to the Video settings panel
    public GameObject audioTabPanel;   // Reference to the Audio settings panel
    public GameObject hotkeysTabPanel; // Reference to the Hotkeys settings panel

    public Button videoTabButton;      // Reference to the Video tab button
    public Button audioTabButton;      // Reference to the Audio tab button
    public Button hotkeysTabButton;    // Reference to the Hotkeys tab button

    private void Start()
    {
        // Add listeners to the buttons
        videoTabButton.onClick.AddListener(ShowVideoPanel);
        audioTabButton.onClick.AddListener(ShowAudioPanel);
        hotkeysTabButton.onClick.AddListener(ShowHotkeysPanel);

        // Show the default panel (e.g., Video Panel at the start)
        ShowVideoPanel();
    }

    private void ShowVideoPanel()
    {
        videoTabPanel.SetActive(true);
        audioTabPanel.SetActive(false);
        hotkeysTabPanel.SetActive(false);
    }

    private void ShowAudioPanel()
    {
        videoTabPanel.SetActive(false);
        audioTabPanel.SetActive(true);
        hotkeysTabPanel.SetActive(false);
    }

    private void ShowHotkeysPanel()
    {
        videoTabPanel.SetActive(false);
        audioTabPanel.SetActive(false);
        hotkeysTabPanel.SetActive(true);
    }
}
