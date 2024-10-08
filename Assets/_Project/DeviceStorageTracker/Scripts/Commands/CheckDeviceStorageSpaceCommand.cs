//using _Project.Analytics.Models;
//using _Project.Analytics.Signals;

using _Project.DeviceStorageTracker.Scripts.Models;
using _Project.DeviceStorageTracker.Scripts.Signals;
using SimpleDiskUtils;
using strange.extensions.command.impl;

namespace _Project.DeviceStorageTracker.Scripts.Commands
{
    public class CheckDeviceStorageSpaceCommand : Command
    {
        [Inject] public IDeviceStorageStatusModel DeviceStorageStatusModel { get; set; }
        [Inject] public DeviceStorageSpaceStatusChangedSignal DeviceStorageSpaceStatusChangedSignal { get; set; }
        //[Inject] public SendAnalyticsEventSignal SendAnalyticsEventSignal { get; set; }

        public override void Execute()
        {
            if (!DeviceStorageStatusModel.IsInitialized)
            {
                DeviceStorageStatusModel.TotalStorageSpace = DiskUtils.CheckTotalSpace();
                DeviceStorageStatusModel.IsInitialized = true;
            }

            bool wasInsufficientStorageSpace = DeviceStorageStatusModel.IsInsufficientStorageSpace;
            DeviceStorageStatusModel.AvailableStorageSpace = DiskUtils.CheckAvailableSpace();
            if (wasInsufficientStorageSpace != DeviceStorageStatusModel.IsInsufficientStorageSpace)
            {
                DeviceStorageSpaceStatusChangedSignal.Dispatch(DeviceStorageStatusModel);
                //SendAnalyticsEventSignal.Dispatch(InjectedObjectFactory.GetInjectedInstance<DeviceStorageStatusChangedFirebaseAnalyticsEvent>()
                    //.SetParametersAndReturn());
            }
        }

    }
}