using _Project.StrangeIOCUtility;
using _Project.UIZeitnot.ButtonZeitnot.Scripts;
using strange.extensions.signal.impl;
using UnityEngine;

namespace _Project.SettingsManager.Scripts.Views
{
    public class HeaderView : ViewZeitnot
    {
        [field: SerializeField] private ButtonZeitnot VideoButton { get; set; }
        [field: SerializeField] private ButtonZeitnot AudioButton { get; set; }
        [field: SerializeField] private ButtonZeitnot HotkeysButton { get; set; }

        [field: SerializeField] private GameObject VideoTab { get; set; }
        [field: SerializeField] private GameObject AudioTab { get; set; }
        [field: SerializeField] private GameObject HotkeysTab { get; set; }

        internal Signal onVideoButtonClicked = new Signal();
        internal Signal onAudioButtonClicked = new Signal();
        internal Signal onHotkeysButtonClicked = new Signal();

        private void OnEnable()
        {
            VideoButton.onClick.AddListener(VideoButtonClicked);
            AudioButton.onClick.AddListener(AudioButtonClicked);
            HotkeysButton.onClick.AddListener(HotkeysButtonClicked);
        }

        private void OnDisable()
        {
            VideoButton.onClick.RemoveListener(VideoButtonClicked);
            AudioButton.onClick.RemoveListener(AudioButtonClicked);
            HotkeysButton.onClick.RemoveListener(HotkeysButtonClicked);
        }

        public void VideoButtonClicked()
        {
            VideoTab.SetActive(true);
            AudioTab.SetActive(false);
            HotkeysTab.SetActive(false);
        }

        public void AudioButtonClicked()
        {
            VideoTab.SetActive(false);
            AudioTab.SetActive(true);
            HotkeysTab.SetActive(false);
        }

        public void HotkeysButtonClicked()
        {
            VideoTab.SetActive(false);
            AudioTab.SetActive(false);
            HotkeysTab.SetActive(true);
        }
    }
}
