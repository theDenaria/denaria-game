using JetBrains.Annotations;

namespace _Project.Analytics.Models
{
    public class ExampleConcreteUnityAnalyticsEvent : UnityAnalyticsEvent
    {
        public ExampleConcreteUnityAnalyticsEvent(int totalScore, [CanBeNull] string eventName = null)
        {
            EventParameters.Add(nameof(totalScore), new UnityAnalyticsEventParameter(totalScore));
        }
    }
}