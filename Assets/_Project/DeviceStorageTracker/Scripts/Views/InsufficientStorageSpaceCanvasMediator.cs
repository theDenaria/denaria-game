using _Project.DeviceStorageTracker.Scripts.Models;
using _Project.DeviceStorageTracker.Scripts.Signals;
using _Project.DeviceUtility.Scripts.Signals;
using strange.extensions.mediation.impl;

namespace _Project.DeviceStorageTracker.Scripts.Views
{
    public class InsufficientStorageSpaceCanvasMediator : Mediator
    {
        [Inject] public InsufficientStorageSpaceCanvasView View { get; set; }
        [Inject] public CleanStorageUpSignal CleanStorageUpSignal { get; set; }
        [Inject] public ForceQuitApplicationSignal ForceQuitApplicationSignal { get; set; }
        [Inject] public DeviceStorageSpaceStatusChangedSignal DeviceStorageSpaceStatusChangedSignal { get; set; }

        public override void OnRegister()
        {
            base.OnRegister();
            View.onCleanStorageButtonClickedSignal.AddListener(OnCleanStorageButtonClickedSignal);
            View.onQuitApplicationButtonClickedSignal.AddListener(OnQuitApplicationButtonClickedSignal);
            DeviceStorageSpaceStatusChangedSignal.AddListener(OnDeviceStorageStatusChangedEvent);
        }

        public override void OnRemove()
        {
            View.onCleanStorageButtonClickedSignal.RemoveListener(OnCleanStorageButtonClickedSignal);
            View.onQuitApplicationButtonClickedSignal.RemoveListener(OnQuitApplicationButtonClickedSignal);
            DeviceStorageSpaceStatusChangedSignal.RemoveListener(OnDeviceStorageStatusChangedEvent);
            base.OnRemove();
        }

        private void OnCleanStorageButtonClickedSignal()
        {
            CleanStorageUpSignal.Dispatch();
        }

        private void OnQuitApplicationButtonClickedSignal()
        {
            ForceQuitApplicationSignal.Dispatch();
        }

        private void OnDeviceStorageStatusChangedEvent(IDeviceStorageStatusModel deviceStorageStatus)
        {
            if (deviceStorageStatus.IsInsufficientStorageSpace)
            {
                View.Show();
            }
            else
            {
                View.Close();
            }
        }

    }
}