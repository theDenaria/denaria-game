using _Project.SceneManagementUtilities.Models;
using _Project.Utilities;

namespace _Project.Analytics.Models
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