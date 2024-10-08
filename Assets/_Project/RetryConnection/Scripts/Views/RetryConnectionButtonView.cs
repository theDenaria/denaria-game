using _Project.StrangeIOCUtility;
using _Project.StrangeIOCUtility.Scripts.Views;
using _Project.UIZeitnot.ButtonZeitnot.Scripts;
using strange.extensions.signal.impl;

namespace _Project.RetryConnection.Scripts.Views
{
    public class RetryConnectionButtonView : ViewZeitnot
    {
        private ButtonZeitnot retryButton;
        
        internal Signal onRetryButtonClick = new Signal();

        internal void init()
        {
            retryButton = GetComponent<ButtonZeitnot>();
            retryButton.onClick.AddListener(OnRetryButtonClick);
        }

        private void OnDisable()
        {
            retryButton.onClick.RemoveListener(OnRetryButtonClick);
        }

        void OnRetryButtonClick()
        {
            onRetryButtonClick.Dispatch();
        }
        
    }
}