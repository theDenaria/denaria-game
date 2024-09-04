using UnityEngine;
using UnityEngine.UI;

public class SettingsMenuManager : MonoBehaviour
{
    public Button videoTabButton;
    public Button audioTabButton;
    public Button hotkeysTabButton;

    public GameObject videoTabPanel;
    public GameObject audioTabPanel;
    public GameObject hotkeysTabPanel;

    private void Start()
    {
        videoTabButton.onClick.AddListener(ShowVideoPanel);
        audioTabButton.onClick.AddListener(ShowAudioPanel);
        hotkeysTabButton.onClick.AddListener(ShowHotkeysPanel);

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
