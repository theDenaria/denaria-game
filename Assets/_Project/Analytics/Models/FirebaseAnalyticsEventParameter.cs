namespace _Project.Analytics.Models
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