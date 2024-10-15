using _Project.DeviceStorageTracker.Scripts.Signals;
using strange.extensions.mediation.impl;

namespace _Project.DeviceStorageTracker.Scripts.Views
{
    public class DeviceStorageTrackerMediator : Mediator
    {
        [Inject] public DeviceStorageTrackerView View { get; set; }
        [Inject] public CheckDeviceStorageStatusSignal CheckDeviceStorageStatusSignal { get; set; }

        public override void OnRegister()
        {
            base.OnRegister();
            View.onCheckStorageStatusSignal.AddListener(OnCheckStorageStatus);
        }

        public override void OnRemove()
        {
            View.onCheckStorageStatusSignal.RemoveListener(OnCheckStorageStatus);
            base.OnRemove();
        }

        private void OnCheckStorageStatus()
        {
            CheckDeviceStorageStatusSignal.Dispatch();
        }
    }
}