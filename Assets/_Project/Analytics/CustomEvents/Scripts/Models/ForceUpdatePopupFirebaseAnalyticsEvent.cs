using _Project.Analytics.FirebaseAnalytics.Scripts.Models;
using _Project.SceneManagementUtilities.Scripts.Models;

namespace _Project.Analytics.CustomEvents.Scripts.Models
{
    public class ForceUpdatePopupFirebaseAnalyticsEvent : FirebaseAnalyticsEvent
    {
        [Inject] public ICurrentSceneModel CurrentSceneModel { get; set; }
        
        public ForceUpdatePopupFirebaseAnalyticsEvent()
        {
            
        }
        
        public ForceUpdatePopupFirebaseAnalyticsEvent SetParametersAndReturn(string request, string response)
        {
            EventName = "force_update_popup";

            EventParameters.Add(nameof(request), new FirebaseAnalyticsEventParameter(request));
            EventParameters.Add(nameof(response), new FirebaseAnalyticsEventParameter(response));
            
            return this;
        }
    }
}