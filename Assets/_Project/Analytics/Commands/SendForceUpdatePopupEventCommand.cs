using _Project.Analytics.Models;
using _Project.Analytics.Signals;
using _Project.StrangeIOCUtility;
using strange.extensions.command.impl;

namespace _Project.Analytics.Commands
{
    public class SendForceUpdatePopupEventCommand : Command
    {
        [Inject] public SendAnalyticsEventSignal SendAnalyticsEventSignal { get; set; }

        public override void Execute()
        {
            //return;
            Debug.Log("SendForceUpdatePopupEventCommand go_to_store");
            SendAnalyticsEventSignal.Dispatch(InjectedObjectFactory.GetInjectedInstance<ForceUpdatePopupFirebaseAnalyticsEvent>()
                .SetParametersAndReturn("go_to_store", "success"));
        }
    }
}