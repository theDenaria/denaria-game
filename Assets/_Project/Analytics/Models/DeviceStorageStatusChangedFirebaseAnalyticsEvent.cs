using _Project.DeviceStorageTracker.Scripts.Models;

namespace _Project.Analytics.Models
{
    public class DeviceStorageStatusChangedFirebaseAnalyticsEvent : FirebaseAnalyticsEvent
    {
        [Inject] public IDeviceStorageStatusModel DeviceStorageStatusModel { get; set; }

        public DeviceStorageStatusChangedFirebaseAnalyticsEvent()
        { }

        public DeviceStorageStatusChangedFirebaseAnalyticsEvent SetParametersAndReturn()
        {
            EventName = "device_storage_tracking";
            EventParameters.Add("total_storage_space", new FirebaseAnalyticsEventParameter(DeviceStorageStatusModel.TotalStorageSpace));
            EventParameters.Add("available_storage_space", new FirebaseAnalyticsEventParameter(DeviceStorageStatusModel.AvailableStorageSpace));
            EventParameters.Add("is_insufficient_storage_space", new FirebaseAnalyticsEventParameter(GetStorageInsufficientStorageStatusAsString()));
            return this;
        }

        private string GetStorageInsufficientStorageStatusAsString()
        {
            return DeviceStorageStatusModel.IsInsufficientStorageSpace ? "true" : "false";
        }

    }
}