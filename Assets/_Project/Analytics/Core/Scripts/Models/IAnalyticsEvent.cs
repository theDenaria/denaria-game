namespace _Project.Analytics.Core.Scripts.Models
{
    //Mainly used for injection. Consider using BaseAnalyticsEvent instead of adding things to this interface.
    public interface IAnalyticsEvent
    {
        //Maybe in the future add a default name to all events with reflection. For Example return this.GetType().Name;
        public string EventName { get; set; }
    }
}