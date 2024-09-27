using _Project.DeviceStorageTracker.Scripts.Models;
using _Project.DeviceStorageTracker.Scripts.Signals;
using strange.extensions.command.impl;

namespace _Project.DeviceStorageTracker.Scripts.Controllers
{
    public class CleanStorageUpCommand : Command
    {
        [Inject] public CheckDeviceStorageStatusSignal CheckDeviceStorageStatusSignal { get; set; }
        [Inject] public IDeviceStorageStatusModel DeviceStorageStatusModel { get; set; }

        public override void Execute()
        {
            Retain();

            // TODO: Implement cleaning the storage up here
            // Possible cleaning strategies:
            // - cleaning old addressables downloaded content.

            CheckDeviceStorageStatusSignal.Dispatch();
            Release();
        }
    }
}