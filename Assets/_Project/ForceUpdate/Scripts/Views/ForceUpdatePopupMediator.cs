using _Project.ForceUpdate.Scripts.Signals;
using strange.extensions.mediation.impl;

namespace _Project.ForceUpdate.Scripts.Views
{
    public class ForceUpdatePopupMediator : Mediator
    {
        [Inject] public ForceUpdatePopupView View { get; set; }
        [Inject] public OpenStorePageButtonClickSignal OpenStorePageButtonClickSignal { get; set; }
        [Inject] public CheckForceUpdateNeededSignal CheckForceUpdateNeededSignal { get; set; }

        public override void OnRegister()
        {
            base.OnRegister();
            //View.onViewRegistered.AddListener(ManuallyCheckForceUpdateNeeded);
            View.onOpenStoreButtonClick.AddListener(HandleOnOpenStoreButtonClicked);
            View.Init();
        }

        public override void OnRemove()
        {
            base.OnRemove();

            //View.onViewRegistered.RemoveListener(ManuallyCheckForceUpdateNeeded);
            View.onOpenStoreButtonClick.RemoveListener(HandleOnOpenStoreButtonClicked);
        }

        private void HandleOnOpenStoreButtonClicked()
        {
            OpenStorePageButtonClickSignal.Dispatch();
        }

        [ListensTo(typeof(OpenForceUpdatePopupSignal))]
        public void OpenForceUpdatePopUp()
        {
            UnityEngine.Debug.Log("Enabling OpenForceUpdatePopup");
            View.EnableCanvas();
        }
        
        private void ManuallyCheckForceUpdateNeeded()
        {
            CheckForceUpdateNeededSignal.Dispatch();
        }
    }
}