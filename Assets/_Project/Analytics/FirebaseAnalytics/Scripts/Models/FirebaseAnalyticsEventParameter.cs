using _Project.Analytics.Core.Scripts.Models;

namespace _Project.Analytics.FirebaseAnalytics.Scripts.Models
{
    public class FirebaseAnalyticsEventParameter : IAnalyticsEventParameter //TODO: Check documentation for supported types
    {
        public object Value { get; set; }

        public FirebaseAnalyticsEventParameter(string stringParameter)
        {
            Value = stringParameter;
        }
        
        public FirebaseAnalyticsEventParameter(double doubleParameter)
        {
            Value = doubleParameter;
        }
        
        public FirebaseAnalyticsEventParameter(long longParameter)
        {
            Value = longParameter;
        }
        
    }
}