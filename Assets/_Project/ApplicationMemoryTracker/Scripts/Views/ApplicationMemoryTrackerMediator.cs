using _Project.ApplicationMemoryTracker.Scripts.Signals;
using strange.extensions.mediation.impl;

namespace _Project.ApplicationMemoryTracker.Scripts.Views
{
    public class ApplicationMemoryTrackerMediator : Mediator
    {
        [Inject] public ApplicationMemoryTrackerView View { get; set; }
        [Inject] public RequestApplicationMemoryCheckSignal RequestApplicationMemoryCheckSignal { get; set; }

        public override void OnRegister()
        {
            base.OnRegister();
            View.OnNextApplicationMemoryCheckSignal.AddListener(OnNextApplicationMemoryCheck);
        }

        public override void OnRemove()
        {
            View.OnNextApplicationMemoryCheckSignal.RemoveListener(OnNextApplicationMemoryCheck);
            base.OnRemove();
        }

        private void OnNextApplicationMemoryCheck()
        {
            RequestApplicationMemoryCheckSignal.Dispatch();
        }
    }
}