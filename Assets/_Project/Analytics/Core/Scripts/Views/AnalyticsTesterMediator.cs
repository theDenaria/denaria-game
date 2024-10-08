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
            SendAnalyticsEventSignal.Dispatch(InjectedObjectFactory.GetInjectedInstance<TestAnalyticsEvent>()
                    .SetParametersAndReturn(0.5f, 14124214214124, "dummy TEST string"));
        }
    }
}