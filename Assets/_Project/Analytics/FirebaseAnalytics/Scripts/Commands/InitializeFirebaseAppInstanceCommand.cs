/*using System;
using System.Threading.Tasks;
using _Project.Analytics.Services;
using _Project.Analytics.Signals;
using Firebase;
using Firebase.Crashlytics;
using strange.extensions.command.impl;
using _Project.Utilities;
using Cysharp.Threading.Tasks;
using Firebase.Extensions;

namespace _Project.Analytics.Commands
{
    //Following code is taken and modified from https://firebase.google.com/docs/unity/setup
    public class InitializeFirebaseAppInstanceCommand : Command
    {
        [Inject] public IAnalyticsService AnalyticsService { get; set; }
        [Inject] public PreLoginInitializerSignal PreLoginInitializerSignal { get; set; }
        public Task<DependencyStatus> FirebaseInitializeResult { get; set; }

        private DependencyStatus DependencyStatus { get; set; } = DependencyStatus.UnavailableOther;

        private bool IsResultCompleted()
        {
            return FirebaseInitializeResult.IsCompleted;
        }
        
        public override async void Execute()
        {
            Retain();
            Debug.Log("Firebase App initializing.");//Muteable Debug Log is not used because this is one of earlier commands. 
            await WaitFirebaseCheckAndFixDependenciesAsync();
            PreLoginInitializerSignal.Dispatch();
        }
        
        private async Task WaitFirebaseCheckAndFixDependenciesAsync()
        {
            Debug.Log("Firebase App initializing second step, WaitFirebaseCheckAndFixDependenciesAsync.");
            int retryCount = 0;
            while (retryCount < Constants.FirebaseInitialization.MAXIMUM_RETRY_ATTEMPTS && DependencyStatus != DependencyStatus.Available)
            {
                //ContinueWithOnMainThread is used because of https://forum.unity.com/threads/problem-with-task-continuewith-using-firebase.1211901/
                //This ensures the code will execute in Update stage of Unity, before Coroutines.
                await FirebaseApp.CheckAndFixDependenciesAsync().ContinueWithOnMainThread(HandleCheckAndFixDependenciesAsyncResult);

                retryCount++;
                Debug.Log("Firebase App initializing, retryCount is:" + retryCount);
                
                if (DependencyStatus != DependencyStatus.Available)
                {
                    await Task.Delay(Constants.FirebaseInitialization.DELAY_BETWEEN_ATTEMPTS_IN_MS);
                }
            }
        }

        private void HandleCheckAndFixDependenciesAsyncResult(Task<DependencyStatus> task)
        {
            Debug.Log("Firebase App initializing third step 1");
            DependencyStatus = task.Result;
            Debug.Log("Firebase App initializing third step 2");
            
            if (DependencyStatus == Firebase.DependencyStatus.Available) { 
                Debug.Log("Firebase App initializing third step 3");
                
                // Create and hold a reference to your FirebaseApp,
                // where app is a Firebase.FirebaseApp property of your application class.
                if (AnalyticsService is FirebaseAnalyticsService)
                {
                    InitializeFirebase();
                    FirebaseInitializeResult = task;
                }
                else
                {
                    ExplainAnalyticsServiceProblem(DependencyStatus);
                }
                // Set a flag here to indicate whether Firebase is ready to use by your app.
            } else { 
                ExplainDependencyStatusProblem(DependencyStatus);
            }
            Release();
        }
        
        private void InitializeFirebase()
        {
            Debug.Log("Firebase App initializing A4");
                        
            FirebaseAnalyticsService firebaseAnalyticsService = (FirebaseAnalyticsService)AnalyticsService;
                        
            Debug.Log("Firebase App initializing A5");

            firebaseAnalyticsService.FirebaseApp = FirebaseApp.DefaultInstance;

            // When this property is set to true, Crashlytics will report all
            // uncaught exceptions as fatal events. This is the recommended behavior.
            Crashlytics.ReportUncaughtExceptionsAsFatal = true;
            
            Debug.Log("Firebase App initialized.");
        }
        
        private void ExplainAnalyticsServiceProblem(DependencyStatus dependencyStatus)
        {
            Debug.Log("Firebase App initializing B1");

            UnityEngine.Debug.LogError(String.Format(
                "AnalyticsService was not FirebaseAnalyticsService. Dependency status: {0}", dependencyStatus));
            Debug.Log("Firebase App initializing B2");
            // Firebase Unity SDK is not safe to use here.
        }
        
        private void ExplainDependencyStatusProblem(DependencyStatus dependencyStatus)
        {
            Debug.Log("Firebase App initializing C1");

            UnityEngine.Debug.LogError(String.Format(
                "Could not resolve all Firebase dependencies: {0}", dependencyStatus));
            Debug.Log("Firebase App initializing C2");
            // Firebase Unity SDK is not safe to use here.
        }
    }
}*/