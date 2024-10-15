namespace _Project.Analytics.Core.Scripts.Models
{
    public abstract class BaseAnalyticsEvent<T> : IAnalyticsEvent where T : IAnalyticsEventParameter
    {
        public EventParameters<T> EventParameters { get; set; } = 
            new EventParameters<T>();

        public string EventName { get; set; }

        //TODO: Add default event name if event constructor gets no name parameter.
    }
}