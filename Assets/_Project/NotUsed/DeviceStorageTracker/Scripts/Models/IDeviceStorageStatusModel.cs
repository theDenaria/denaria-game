namespace _Project.DeviceStorageTracker.Scripts.Models
{
    public interface IDeviceStorageStatusModel
    {
        /// <summary>
        ///     Total device storage space in MB.
        /// </summary>
        int TotalStorageSpace { get; set; }

        /// <summary>
        /// Available device storage space in MB.
        /// </summary>
        int AvailableStorageSpace { get; set; }

        bool IsInitialized { get; set; }
        bool IsInsufficientStorageSpace { get; }
    }
}