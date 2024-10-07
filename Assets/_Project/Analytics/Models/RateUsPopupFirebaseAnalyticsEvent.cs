using _Project.SceneManagementUtilities.Models;
using _Project.Utilities;

namespace _Project.Analytics.Models
{
    public class RateUsPopupFirebaseAnalyticsEvent : FirebaseAnalyticsEvent
    {
        [Inject] public ICurrentSceneModel CurrentSceneModel { get; set; }
        public RateUsPopupFirebaseAnalyticsEvent()
        {
            
        }
        public RateUsPopupFirebaseAnalyticsEvent SetParametersAndReturn()
        {
            EventName = "rateus_popup";

            return this;
        }
    }
}