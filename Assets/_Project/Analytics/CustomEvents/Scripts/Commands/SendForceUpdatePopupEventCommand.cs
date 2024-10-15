using _Project.Analytics.Core.Scripts.Signals;
using _Project.Analytics.CustomEvents.Scripts.Models;
using _Project.StrangeIOCUtility.Scripts.Utilities;
using strange.extensions.command.impl;

namespace _Project.Analytics.CustomEvents.Scripts.Commands
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