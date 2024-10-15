using _Project.Analytics.Core.Scripts.Enums;
using _Project.Analytics.Core.Scripts.Models;

namespace _Project.Analytics.Core.Scripts.Services
{
    public abstract class BaseAnalyticsService<T,S> : IAnalyticsService where T:IAnalyticsEvent where S:IAnalyticsEventParameter
    {
        protected BaseAnalyticsService()
        {
        }

        public void SendEvent(IAnalyticsEvent analyticsEvent)
        {
            ValidateAnalyticsEventType(analyticsEvent);

            AnalyticsResultWrapper analyticsResultWrapper = SendEventViaAPI(analyticsEvent);
            
            BaseAnalyticsEvent<IAnalyticsEventParameter> analyticsEventGeneric;
            //analyticsEventGeneric = (BaseAnalyticsEvent<IAnalyticsEventParameter>)analyticsEvent;

            //LogSentAnalyticsEvent(analyticsEventGeneric, (AnalyticsResultWrapper)analyticsResultWrapper);
        }

        public abstract void ValidateAnalyticsEventType(IAnalyticsEvent analyticsEvent);

        public abstract AnalyticsResultWrapper SendEventViaAPI(IAnalyticsEvent analyticsEvent);

        public abstract void LogSentAnalyticsEvent(BaseAnalyticsEvent<IAnalyticsEventParameter> analyticsEvent,
            AnalyticsResultWrapper analyticsResultWrapper);
    }
}