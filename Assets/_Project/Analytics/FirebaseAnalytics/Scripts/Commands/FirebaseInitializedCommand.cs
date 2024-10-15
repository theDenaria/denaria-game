/*using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Firebase.Extensions;
using Firebase.RemoteConfig;
using strange.extensions.command.impl;

namespace _Project.Analytics.Commands
{
    public class FirebaseInitializedCommand : Command
    {
        public override void Execute()
        {
            Debug.Log("ABTEST_ FIREBASEINITIALIZEDCOMMAND");
            Firebase.Installations.FirebaseInstallations.DefaultInstance.GetTokenAsync(forceRefresh: true).ContinueWith(
                task => {
                    if (!(task.IsCanceled || task.IsFaulted) && task.IsCompleted) {
                        UnityEngine.Debug.Log(System.String.Format("ABTEST_ Installations token {0}", task.Result));
                    }
                });
            RemoteConfigSetup();
            Debug.Log("ABTEST_ FIREBASEINITIALIZEDCOMMAND FINISHED");
        }

        public void RemoteConfigSetup()
        {
            Dictionary<string, object> defaults = new Dictionary<string, object>();
            defaults.Add("test_parameter_activated", "true");

            try
            {
                Firebase.FirebaseApp.CheckAndFixDependenciesAsync()
                    .ContinueWith(task =>
                    {
                        FirebaseRemoteConfig.DefaultInstance.SetDefaultsAsync(defaults)
                            .ContinueWithOnMainThread(task => { Debug.Log("ABTEST_ defaults added"); });

                        Debug.Log("ABTEST_ Fetching data...");
                        FirebaseRemoteConfig.DefaultInstance.FetchAsync(
                            TimeSpan.Zero).ContinueWithOnMainThread(FetchComplete);
                    });
            }
            catch (Exception e)
            {
                Debug.Log("ABTEST_ " + e.Message);
            }
        }

        private void FetchComplete(Task fetchTask)
        {
            if (!fetchTask.IsCompleted) {
                Debug.LogError("ABTEST_ Retrieval hasn't finished.");
                return;
            }

            var remoteConfig = FirebaseRemoteConfig.DefaultInstance;
            var info = remoteConfig.Info;
            if(info.LastFetchStatus != LastFetchStatus.Success) {
                Debug.LogError($"ABTEST_ {nameof(FetchComplete)} was unsuccessful\n{nameof(info.LastFetchStatus)}: {info.LastFetchStatus}");
                return;
            }

            // Fetch successful. Parameter values must be activated to use.
            remoteConfig.ActivateAsync()
                .ContinueWithOnMainThread(
                    task => {
                        Debug.Log($"ABTEST_Remote data loaded and ready for use. Last fetch time {info.FetchTime}.");
                        ApplyABTestVariations();
                    });
        }

        void ApplyABTestVariations()
        {
            string buttonColor = FirebaseRemoteConfig.DefaultInstance.GetValue("test_parameter_activated").StringValue;

            if (buttonColor.Equals("true"))
            {
                Debug.Log("ABTEST_ baseline");
            }
            else if (buttonColor.Equals("false"))
            {
                Debug.Log("ABTEST_ A variant");
            }
        }
    }
}*/