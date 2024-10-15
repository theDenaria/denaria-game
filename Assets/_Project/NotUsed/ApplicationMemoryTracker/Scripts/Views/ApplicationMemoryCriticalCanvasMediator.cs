//using _Project.Analytics.Models;
//using _Project.Analytics.Signals;
using _Project.ApplicationMemoryTracker.Scripts.Signals;
using _Project.DeviceUtility.Scripts.Signals;
using _Project.StrangeIOCUtility;
using strange.extensions.mediation.impl;

namespace _Project.ApplicationMemoryTracker.Scripts.Views
{
    public class ApplicationMemoryCriticalCanvasMediator : Mediator
    {
        [Inject] public ApplicationMemoryCriticalCanvasView View { get; set; }
        [Inject] public ApplicationMemoryCriticalSignal ApplicationMemoryCriticalSignal { get; set; }
        [Inject] public ApplicationMemoryCleanedSignal ApplicationMemoryCleanedSignal { get; set; }
        [Inject] public ForceQuitApplicationSignal ForceQuitApplicationSignal { get; set; }
        //[Inject] public SendAnalyticsEventSignal SendAnalyticsEventSignal { get; set; }

        public override void OnRegister()
        {
            base.OnRegister();
            View.OnViewShownSignal.AddListener(OnViewShown);
            //View.OnClearMemoryButtonClickedSignal.AddListener(OnClearMemoryButtonClicked);
            View.OnQuitApplicationButtonClickedSignal.AddListener(OnQuitApplicationButtonClicked);
            ApplicationMemoryCriticalSignal.AddListener(OnApplicationMemoryCritical);
            ApplicationMemoryCleanedSignal.AddListener(OnApplicationMemoryCleaned);
        }

        public override void OnRemove()
        {
            View.OnViewShownSignal.RemoveListener(OnViewShown);
            //View.OnClearMemoryButtonClickedSignal.RemoveListener(OnClearMemoryButtonClicked);
            View.OnQuitApplicationButtonClickedSignal.RemoveListener(OnQuitApplicationButtonClicked);
            ApplicationMemoryCriticalSignal.RemoveListener(OnApplicationMemoryCritical);
            ApplicationMemoryCleanedSignal.RemoveListener(OnApplicationMemoryCleaned);
            base.OnRemove();
        }

        private void OnViewShown()
        {
            //SendAnalyticsEventSignal.Dispatch(InjectedObjectFactory.GetInjectedInstance<ApplicationMemoryStatusFirebaseAnalyticsEvent>()
             //   .SetParametersAndReturn(ApplicationMemoryStatusAnalyticsEventFiringReasons.popup_created)
            //;
        }

        /*
        public void OnClearMemoryButtonClicked()
        {

        }
        */

        public void OnQuitApplicationButtonClicked()
        {
            ForceQuitApplicationSignal.Dispatch();
        }

        private void OnApplicationMemoryCritical()
        {
            View.Show();
        }

        private void OnApplicationMemoryCleaned()
        {
            Debug.LogWarning(">>> Memory cleaned - ApplicationMemoryCirticalCanvas will be closed.");
            View.Close();
        }

    }
}