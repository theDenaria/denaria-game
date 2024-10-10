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
            View.ButtonPressedSignal.AddListener(HandleButtonPressed);
            View.init();
        }

        public override void OnRemove()
        {
            base.OnRemove();
            View.ButtonPressedSignal.RemoveListener(HandleButtonPressed);
        }

        private void HandleButtonPressed()
        {
            Debug.Log("xxx HandleButtonPressed entered");
            SendAnalyticsEventSignal.Dispatch(InjectedObjectFactory.GetInjectedInstance<TestAnalyticsEvent>()
                    .SetParametersAndReturn("dummyParameterString", 242, 0.5f, true));
            Debug.Log("xxx HandleButtonPressed exited");
        }

        private void Start()
        {
            Debug.Log("xxx Start entered");
            SendAnalyticsEvent();
            Debug.Log("xxx Start exited");
        }
        
        
        async void SendAnalyticsEvent()
        {
            Debug.Log("xxx SendAnalyticsEvent enter");

            try
            {
                await Task.Delay(10000); // 1 second delay
                SendAnalyticsEventSignal.Dispatch(InjectedObjectFactory.GetInjectedInstance<TestAnalyticsEvent>()
                    .SetParametersAndReturn("dummyParameterString", 242, 0.5f, true));

                Debug.Log("xxx SendAnalyticsEvent exit");
            }
            catch (Exception exception)
            {
            }
        }
    }
}