using _Project.Analytics.Core.Scripts.Enums;
using _Project.Analytics.Core.Scripts.Models;

namespace _Project.Analytics.Core.Scripts.Services
{
    public interface IAnalyticsService
    {
        
        public abstract void SendEvent(IAnalyticsEvent analyticsEvent);
        public abstract void ValidateAnalyticsEventType(IAnalyticsEvent analyticsEvent);
        
        public abstract AnalyticsResultWrapper SendEventViaAPI(IAnalyticsEvent analyticsEvent);

        public abstract void LogSentAnalyticsEvent(BaseAnalyticsEvent<IAnalyticsEventParameter> AnalyticsEvent, AnalyticsResultWrapper analyticsResultWrapper);

        //public abstract void AnalyticsResultWrapper analyticsResultWrapper;
        
        
    }
}