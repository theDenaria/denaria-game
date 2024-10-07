namespace _Project.Analytics.Models
{
    public class AdIncomeFirebaseAnalyticsEvent : FirebaseAnalyticsEvent
    {
        public AdIncomeFirebaseAnalyticsEvent(string ad_source_screen_id, string ad_type, double revenue, string revenue_precision)
        {
            EventName = "ad_income";
            
            EventParameters.Add("screen_source", new FirebaseAnalyticsEventParameter(ad_source_screen_id));
            EventParameters.Add(nameof(ad_type), new FirebaseAnalyticsEventParameter(ad_type));
            EventParameters.Add("ad_ecpm", new FirebaseAnalyticsEventParameter(revenue));
            EventParameters.Add(nameof(revenue_precision), new FirebaseAnalyticsEventParameter(revenue_precision));
        }
    }
}