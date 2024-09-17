using _Project.StrangeIOCUtility;
using _Project.UIZeitnot.ButtonZeitnot.Scripts;
using strange.extensions.mediation.impl;
using strange.extensions.signal.impl;
using UnityEngine;

namespace _Project.ForceUpdate.Scripts.Views
{
    public class ForceUpdatePopupView : View
    {
        [field: SerializeField] private ButtonZeitnot OpenStoreButton { get; set; }

        private Canvas canvas;

        internal Signal onOpenStoreButtonClick = new Signal();
        internal Signal onViewRegistered = new Signal();

        internal void Init()
        {
            OpenStoreButton.onClick.AddListener(OnOpenStoreButtonClick);

            canvas = GetComponent<Canvas>();
            onViewRegistered.Dispatch();
        }

        private void OnDisable()
        {
            OpenStoreButton.onClick.RemoveListener(OnOpenStoreButtonClick);
        }

        public void EnableCanvas()
        {
            UnityEngine.Debug.Log("Canvas will be enabled in ForceUpdatePopupView because of low version");
            canvas.enabled = true;
        }

        private void OnOpenStoreButtonClick()
        {
            onOpenStoreButtonClick.Dispatch();
        }
    }
}