using _Project.Analytics.FirebaseAnalytics.Scripts.Models;
using _Project.SceneManagementUtilities.Scripts.Models;

namespace _Project.Analytics.CustomEvents.Scripts.Models
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