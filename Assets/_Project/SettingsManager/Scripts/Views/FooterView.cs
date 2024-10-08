using _Project.StrangeIOCUtility;
using _Project.UIZeitnot.ButtonZeitnot.Scripts;
using strange.extensions.signal.impl;
using UnityEngine;

namespace _Project.SettingsManager.Scripts.Views
{
    public class FooterView : ViewZeitnot
    {
        [field: SerializeField] private ButtonZeitnot ApplyButton { get; set; }
        [field: SerializeField] private ButtonZeitnot RestoreButton { get; set; }
        [field: SerializeField] private ButtonZeitnot BackButton { get; set; }
        [field: SerializeField] public GameObject SettingsPanel { get; set; }
        [field: SerializeField] public GameObject MainMenuPanel { get; set; }

        internal Signal onApplyButtonClicked = new Signal();
        internal Signal onRestoreButtonClicked = new Signal();
        internal Signal onBackButtonClicked = new Signal();

        private void OnEnable()
        {
            ApplyButton.onClick.AddListener(ApplyButtonClicked);
            RestoreButton.onClick.AddListener(RestoreButtonClicked);
            BackButton.onClick.AddListener(BackButtonClicked);
        }

        private void OnDisable()
        {
            ApplyButton.onClick.RemoveListener(ApplyButtonClicked);
            RestoreButton.onClick.RemoveListener(RestoreButtonClicked);
            BackButton.onClick.RemoveListener(BackButtonClicked);
        }

        public void ApplyButtonClicked()
        {
            onApplyButtonClicked.Dispatch();
        }

        public void RestoreButtonClicked()
        {
            onRestoreButtonClicked.Dispatch();
        }

        public void BackButtonClicked()
        {
            Debug.Log("BackButtonClicked VIEW");
            onBackButtonClicked.Dispatch();
            Debug.Log("BackButtonClicked VIEW AFTER");
        }
    }

}
