using System;
using System.Threading.Tasks;
using strange.extensions.command.impl;
using Unity.Services.Analytics;
using Unity.Services.Core;
using Unity.Services.Core.Environments;

namespace _Project.Analytics.UnityAnalytics.Scripts.Commands
{
    public class InitializeUnityServicesCommand : Command
    {
        
        public string environment = "production";

        public override void Execute()
        {
            Retain();
            InitializeUnityServices();
        }
        
        async void InitializeUnityServices()
        {
            Debug.Log("xxx InitializeUnityServicesCommand enter");

            try
            {
                var options = new InitializationOptions()
                    .SetEnvironmentName(environment);

                await UnityServices.InitializeAsync(options);
                Debug.Log($"Started UGS Analytics Sample with user ID: {AnalyticsService.Instance.GetAnalyticsUserID()}");

                await Task.Delay(5000); // 1 second delay

                GiveConsent();
                Debug.Log("xxx InitializeUnityServicesCommand finising");
                Release();
            }
            catch (Exception exception)
            {
                Fail();
            }
        }
        
        
        public void GiveConsent()
        {
            AnalyticsService.Instance.StartDataCollection();

            Debug.Log($"Consent has been provided. The SDK is now collecting data!");
        }
    }
}