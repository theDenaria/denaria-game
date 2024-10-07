using _Project.Analytics.Enums;
using _Project.Analytics.Models;

namespace _Project.Analytics.Services
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