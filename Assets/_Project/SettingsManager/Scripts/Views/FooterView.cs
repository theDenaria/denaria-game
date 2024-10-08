using _Project.StrangeIOCUtility.Scripts.Views;
using _Project.UIZeitnot.ButtonZeitnot.Scripts;
using strange.extensions.signal.impl;
using UnityEngine;

namespace _Project.SettingsManager.Scripts.Views
{
    public class FooterView : ViewZeitnot
    {
        [field: SerializeField] private ButtonZeitnot ApplyButton { get; set; }
        [field: SerializeField] private ButtonZeitnot RestoreButton { get; set; }

        internal Signal onApplyButtonClicked = new Signal();
        internal Signal onRestoreButtonClicked = new Signal();

        private void OnEnable()
        {
            ApplyButton.onClick.AddListener(ApplyButtonClicked);
            RestoreButton.onClick.AddListener(RestoreButtonClicked);
        }

        private void OnDisable()
        {
            ApplyButton.onClick.RemoveListener(ApplyButtonClicked);
            RestoreButton.onClick.RemoveListener(RestoreButtonClicked);
        }

        public void ApplyButtonClicked()
        {
            onApplyButtonClicked.Dispatch();
        }

        public void RestoreButtonClicked()
        {
            onRestoreButtonClicked.Dispatch();
        }
    }
}
