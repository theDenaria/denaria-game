using System;
using System.Threading.Tasks;
using _Project.Analytics.Core.Scripts.Models;
using _Project.Analytics.Core.Scripts.Signals;
using _Project.StrangeIOCUtility.Scripts.Utilities;
using strange.extensions.mediation.impl;

namespace _Project.Analytics.Core.Scripts.Views
{
    public class AnalyticsTesterMediator : Mediator
    {
        [Inject] public AnalyticsTesterView View { get; set; }

        [Inject] public SendAnalyticsEventSignal SendAnalyticsEventSignal { get; set; }

        public override void OnRegister()
        {
            base.OnRegister();
            View.buttonPressedSignal.AddListener(HandleButtonPressed);
            View.init();
        }

        public override void OnRemove()
        {
            base.OnRemove();
            View.buttonPressedSignal.RemoveListener(HandleButtonPressed);
        }
        
        private void Start()
        {
            Debug.Log("xxx Start entered");
            WaitAndSendAnalyticsEvent();
            Debug.Log("xxx Start exited");
        }

        private void HandleButtonPressed()
        {
            Debug.Log("xxx HandleButtonPressed entered");
            SendDummyEvent();
            Debug.Log("xxx HandleButtonPressed exited");
        }
        
        async void WaitAndSendAnalyticsEvent()
        {
            Debug.Log("xxx SendAnalyticsEvent enter");

            try
            {
                await Task.Delay(10000); //1 second delay to ensure analytics system is initialized and permission given.
                SendDummyEvent();
                Debug.Log("xxx SendAnalyticsEvent exit");
            }
            catch (Exception exception)
            {
            }
        }

        private void SendDummyEvent()
        {
            SendAnalyticsEventSignal.Dispatch(InjectedObjectFactory.GetInjectedInstance<UnityTestEvent>()
                .SetParametersAndReturn("dummyParameterString", 242, 0.5f, true));
        }
    }
}