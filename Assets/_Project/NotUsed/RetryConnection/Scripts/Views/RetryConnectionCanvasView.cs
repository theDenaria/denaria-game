using _Project.StrangeIOCUtility;
using _Project.StrangeIOCUtility.Scripts.Views;
using _Project.UIZeitnot.ButtonZeitnot.Scripts;
using strange.extensions.signal.impl;
using UnityEngine;
using UnityEngine.UI;

namespace _Project.RetryConnection.Scripts.Views
{
    [RequireComponent(typeof(Canvas), typeof(CanvasGroup), typeof(GraphicRaycaster))]
    public class RetryConnectionCanvasView : ViewZeitnot
    {
        [field: SerializeField] private Canvas ViewCanvas { get; set; }
        [field: SerializeField] private CanvasGroup ViewCanvasGroup { get; set; }
        [SerializeField] private ButtonZeitnot retryConnectionButtoCBS;
        [SerializeField] private ButtonZeitnot retryConnectionButtonNetworkLoss;

        public bool IsShown { get; private set; } = false;

        internal Signal<bool> onToggleCanvas = new Signal<bool>();

        internal void init()
        {
            onToggleCanvas.AddListener(OnToggleRetryConnectionCanvas);
        }

        private void OnDisable()
        {
            onToggleCanvas.RemoveListener(OnToggleRetryConnectionCanvas);
        }

        private void OnToggleRetryConnectionCanvas(bool toggle)
        {
            retryConnectionButtoCBS.gameObject.SetActive(true);
            retryConnectionButtonNetworkLoss.gameObject.SetActive(false);

            if (toggle)
            {
                Show();
            }
            else
            {
                Close();
            }
        }

        public void OnNetworkConnectionSuccess()
        {
            Close();
        }

        public void OnNetworkConnectionLost()
        {
            retryConnectionButtoCBS.gameObject.SetActive(false);
            retryConnectionButtonNetworkLoss.gameObject.SetActive(true);
            Show();
        }


        private void Show()
        {
            // TODO: We can implement an animation here, but when the animation ends, we must call OnShown method to make sure about the Canvas setup is valid after the animation.
            OnShown();
        }

        private void Close()
        {
            IsShown = false;
            // TODO: We can implement an animation here, but when the animation ends, we must call OnShown method to make sure about the Canvas setup is valid after the animation.
            OnClosed();
        }

        private void OnShown()
        {
            ViewCanvasGroup.blocksRaycasts = true;
            ViewCanvasGroup.interactable = true;
            ViewCanvasGroup.alpha = 1f;
            ViewCanvas.enabled = true;
            IsShown = true;
        }

        private void OnClosed()
        {
            ViewCanvasGroup.blocksRaycasts = false;
            ViewCanvasGroup.alpha = 0f;
            ViewCanvasGroup.interactable = false;
            ViewCanvas.enabled = false;
        }

    }
}