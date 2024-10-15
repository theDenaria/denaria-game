using strange.extensions.command.impl;
using Unity.Services.Analytics;

namespace _Project.Analytics.UnityAnalytics.Scripts.Commands
{
    public class GiveConsentForCollectingDataCommand : Command
    {
        public override void Execute()
        {
            GiveConsent();
        }
        
        public void GiveConsent()
        {
            AnalyticsService.Instance.StartDataCollection();

            Debug.Log($"Consent has been provided. The SDK is now collecting data!");
        }
    }
}