using _Project.DeviceStorageTracker.Scripts.Models;
using strange.extensions.signal.impl;

namespace _Project.DeviceStorageTracker.Scripts.Signals
{
    public class DeviceStorageSpaceStatusChangedSignal : Signal<IDeviceStorageStatusModel>
    { }
}