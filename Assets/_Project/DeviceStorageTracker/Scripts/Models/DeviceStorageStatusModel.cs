using _Project.Utilities;

namespace _Project.DeviceStorageTracker.Scripts.Models
{
    public class DeviceStorageStatusModel : IDeviceStorageStatusModel
    {
        public int TotalStorageSpace { get; set; } = int.MaxValue;
        public int AvailableStorageSpace { get; set; } = int.MaxValue;

        public bool IsInitialized { get; set; } = false;
        public bool IsInsufficientStorageSpace
        {
            get { return AvailableStorageSpace <= Constants.INSUFFICIENT_STORAGE_SPACE_ALERT_THRESHOLD_MB; }
        }
    }
}