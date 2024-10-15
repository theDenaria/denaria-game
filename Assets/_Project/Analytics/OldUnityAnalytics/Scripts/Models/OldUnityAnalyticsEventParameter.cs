using _Project.Analytics.Core.Scripts.Models;

namespace _Project.Analytics.UnityAnalytics.Scripts.Models
{
    //This class makes sure that only supported types get passed into respective Analytics Service.
    public class OldUnityAnalyticsEventParameter : IAnalyticsEventParameter
    {
        public object Value { get; set; }

        public OldUnityAnalyticsEventParameter(string stringParameter)
        {
            Value = stringParameter;
        }
        
        public OldUnityAnalyticsEventParameter(int integerParameter)
        {
            Value = integerParameter;
        }
        
        public OldUnityAnalyticsEventParameter(float floatParameter)
        {
            Value = floatParameter;
        }        
        
        public OldUnityAnalyticsEventParameter(bool boolParameter)
        {
            Value = boolParameter;
        }
        
    }
}