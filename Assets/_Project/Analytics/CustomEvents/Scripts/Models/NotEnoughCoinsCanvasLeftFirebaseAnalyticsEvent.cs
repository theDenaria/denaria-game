using _Project.Analytics.CustomEvents.Scripts.Enums;
using _Project.Analytics.FirebaseAnalytics.Scripts.Models;
using _Project.SceneManagementUtilities.Scripts.Models;

namespace _Project.Analytics.CustomEvents.Scripts.Models
{
    public class NotEnoughCoinsCanvasLeftFirebaseAnalyticsEvent : FirebaseAnalyticsEvent
    {
        [Inject] public ICurrentSceneModel CurrentSceneModel { get; set; }

        public NotEnoughCoinsCanvasLeftFirebaseAnalyticsEvent()
        {
        }
        
        public NotEnoughCoinsCanvasLeftFirebaseAnalyticsEvent SetParametersAndReturn(
            NotEnoughCoinsPopupRequestTypes requestType,
            NotEnoughCoinsPopupPurchaseResultTypes purchaseButtonResultType
        )
        {
            EventName = "not_enough_gem_popup";

            string source = GetSourceScene(CurrentSceneModel);
            string request = GetRequestTypeString(requestType);
            
            //EventParameters.Add("event_timestamp", new FirebaseAnalyticsEventParameter(DateUtility.GetCurrentEpochSeconds().ToString()));
            EventParameters.Add(nameof(source), new FirebaseAnalyticsEventParameter(source));
            EventParameters.Add(nameof(request), new FirebaseAnalyticsEventParameter(request));
            EventParameters.Add("purchase_result", new FirebaseAnalyticsEventParameter(GetPurchaseResultTypeString(purchaseButtonResultType)));

            return this;
        }
        
        public NotEnoughCoinsCanvasLeftFirebaseAnalyticsEvent SetParametersAndReturn(
            string source,
            NotEnoughCoinsPopupRequestTypes requestType,
            NotEnoughCoinsPopupPurchaseResultTypes purchaseButtonResultType
        )
        {
            EventName = "not_enough_gem_popup";

            string request = GetRequestTypeString(requestType);
            
            //EventParameters.Add("event_timestamp", new FirebaseAnalyticsEventParameter(DateUtility.GetCurrentEpochSeconds().ToString()));
            EventParameters.Add(nameof(source), new FirebaseAnalyticsEventParameter(source));
            EventParameters.Add(nameof(request), new FirebaseAnalyticsEventParameter(request));
            EventParameters.Add("purchase_result", new FirebaseAnalyticsEventParameter(GetPurchaseResultTypeString(purchaseButtonResultType)));

            return this;
        }
        
        private string GetSourceScene(ICurrentSceneModel currentSceneModel)
        {
            Debug.Log("GetSourceScene is:" +  currentSceneModel.CurrentSceneId);
            return currentSceneModel.CurrentSceneId;
        }
        
        private string GetRequestTypeString(NotEnoughCoinsPopupRequestTypes requestType)
        {
            Debug.Log("request_type is:" + requestType.ToString());
            return requestType.ToString();
        }

        private string GetPurchaseResultTypeString(NotEnoughCoinsPopupPurchaseResultTypes purchaseResultType)
        {
            if (purchaseResultType == NotEnoughCoinsPopupPurchaseResultTypes.none)
            {
                return null;
            }
            return purchaseResultType.ToString();
        }

    }
}