using JetBrains.Annotations;

namespace _Project.Analytics.UnityAnalytics.Scripts.Models
{
    public class ExampleConcreteOldUnityAnalyticsEvent : OldUnityAnalyticsEvent
    {
        public ExampleConcreteOldUnityAnalyticsEvent(int totalScore, [CanBeNull] string eventName = null)
        {
            EventParameters.Add(nameof(totalScore), new OldUnityAnalyticsEventParameter(totalScore));
        }
    }
}