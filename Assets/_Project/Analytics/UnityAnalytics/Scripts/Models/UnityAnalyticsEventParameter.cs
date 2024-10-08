using _Project.Analytics.Core.Scripts.Models;

namespace _Project.Analytics.UnityAnalytics.Scripts.Models
{
    //This class makes sure that only supported types get passed into respective Analytics Service.
    public class UnityAnalyticsEventParameter : IAnalyticsEventParameter
    {
        public object Value { get; set; }

        public UnityAnalyticsEventParameter(string stringParameter)
        {
            Value = stringParameter;
        }
        
        public UnityAnalyticsEventParameter(int integerParameter)
        {
            Value = integerParameter;
        }
        
        public UnityAnalyticsEventParameter(float floatParameter)
        {
            Value = floatParameter;
        }
        
    }
}