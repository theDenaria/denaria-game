using System;
//using _Project.Analytics.Enums;
//using _Project.Analytics.Models;
//using _Project.Analytics.Signals;
using _Project.ApplicationMemoryTracker.Scripts.Models;
using _Project.ApplicationMemoryTracker.Scripts.Signals;
using _Project.LoggingAndDebugging;
using _Project.StrangeIOCUtility;
using _Project.Utilities;
using Cysharp.Threading.Tasks;
using Unity.Profiling;
using UnityEngine;
using UnityEngine.Profiling;

namespace _Project.ApplicationMemoryTracker.Scripts.Services
{
    public class ApplicationMemoryService : IApplicationMemoryService, IDisposable
    {
        [Inject] public ApplicationMemoryCriticalSignal ApplicationMemoryCriticalSignal { get; set; }
        [Inject] public ApplicationMemoryCleanedSignal ApplicationMemoryCleanedSignal { get; set; }
        [Inject] public IMemoryProfilerRecorders MemoryProfilerRecorders { get; set; }  // Those records may possibly be used for extra memory data.
        //[Inject] public SendAnalyticsEventSignal SendAnalyticsEventSignal { get; set; }

        private long MemoryCheckCounter { get; set; } = 0;
        private bool WasMemoryCritical { get; set; } = false;
        public bool IsMemoryCleaningBusy { get; private set; } = false;
        private long NextAvailableMemoryCleaningTimestamp { get; set; } = 0;


        #region Initializers

        [PostConstruct]     // To let StrangeIoC call that initialize method just after injections complete.
        private void InitializeMemoryProfilerRecorders()
        {
            // System used memory
            MemoryProfilerRecorders.SystemUsedMemory = ProfilerRecorder.StartNew(ProfilerCategory.Memory, "System Used Memory");
            MemoryProfilerRecorders.SystemTotalUsedMemory = ProfilerRecorder.StartNew(ProfilerCategory.Memory, "System Total Used Memory");

            // App memory
            MemoryProfilerRecorders.AppResidentMemory = ProfilerRecorder.StartNew(ProfilerCategory.Memory, "App Resident Memory");
            MemoryProfilerRecorders.AppCommittedMemory = ProfilerRecorder.StartNew(ProfilerCategory.Memory, "App Committed Memory");

            // Total memory
            MemoryProfilerRecorders.TotalUsedMemory = ProfilerRecorder.StartNew(ProfilerCategory.Memory, "Total Used Memory");
            MemoryProfilerRecorders.TotalReservedMemory = ProfilerRecorder.StartNew(ProfilerCategory.Memory, "Total Reserved Memory");

            // GC memory
            MemoryProfilerRecorders.GCUsedMemory = ProfilerRecorder.StartNew(ProfilerCategory.Memory, "GC Used Memory");
            MemoryProfilerRecorders.GCReservedMemory = ProfilerRecorder.StartNew(ProfilerCategory.Memory, "GC Reserved Memory");

            // Texture memory
            MemoryProfilerRecorders.TextureMemory = ProfilerRecorder.StartNew(ProfilerCategory.Memory, "Texture Memory");
            MemoryProfilerRecorders.TextureCount = ProfilerRecorder.StartNew(ProfilerCategory.Memory, "Texture Count");
        }

        #endregion


        #region Finalizers

        public bool IsDisposed { get; private set; } = false;

        protected virtual void Dispose(bool isDisposing)
        {
            if (!IsDisposed) { return; }
            if (isDisposing)
            {
                DisposeMemoryProfilerRecorders();
            }
            IsDisposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        private void DisposeMemoryProfilerRecorders()
        {
            MemoryProfilerRecorders.DisposeAllProfilerRecorders();
        }

        #endregion


        #region Public Memory Control Methods

        public void RequestMemoryCheck()
        {
            //DebugLoggerMuteable.Log(">>> MemoryProfilerRecorders <<<\n " + MemoryProfilerRecorders.ToLogString());
            ++MemoryCheckCounter;
            if (MemoryCheckCounter == 2)    // Send an analytics event at the 2nd check (at the 1st check, MemoryProfilerRecorders may contain zero records yet)
            {
                //SendAnalyticsEventSignal.Dispatch(InjectedObjectFactory.GetInjectedInstance<ApplicationMemoryStatusFirebaseAnalyticsEvent>()
                    //.SetParametersAndReturn(ApplicationMemoryStatusAnalyticsEventFiringReasons.first_time_open)
                //);
            }

            if (!HasEnoughMemory())
            {
                WasMemoryCritical = true;
                ApplicationMemoryCriticalSignal.Dispatch();
                CleanAllUnusedAssets(false);
            }
            else if (WasMemoryCritical)
            {
                DebugLoggerMuteable.Log("Memory cleared!");
                WasMemoryCritical = false;
                ApplicationMemoryCleanedSignal.Dispatch();
            }
        }

        #endregion


        #region Utilities

        public bool HasEnoughMemory()
        {
            long totalAllocatedMemoryMB = GetTotalAllocatedMemoryMB();
            return totalAllocatedMemoryMB < Constants.APPLICATION_CRITICAL_MEMORY_THRESHOLD_MB;
        }

        public bool IsMemoryCleaningInCooldown()
        {
            return GetTimestampNow() < NextAvailableMemoryCleaningTimestamp;
        }

        private long GetTimestampNow()
        {
            return DateTime.Now.Ticks / 10000;  // There are 10000 ticks in a milisecond.
        }

        private long GetTotalAllocatedMemoryMB()
        {
            return Profiler.GetTotalAllocatedMemoryLong() / Constants.MEMORY_BYTE_TO_MB_DIVIDER;
        }

        #endregion


        #region Memory Cleaning

        public async void CleanAllUnusedAssets(bool force = false)
        {
            if (IsMemoryCleaningBusy) { return; }
            if (!force && IsMemoryCleaningInCooldown())
            {
                return;
            }

            IsMemoryCleaningBusy = true;
            await CleanUnityUnusedResources();
            await CleanUnusedAddressablesAssets();
            await CleanOtherUnusedAssets();
            CleanGC();
            NextAvailableMemoryCleaningTimestamp = GetTimestampNow() + Constants.APPLICATION_MEMORY_CLEANING_COOLDOWN_MSEC;
            IsMemoryCleaningBusy = false;
        }

        private async UniTask CleanUnityUnusedResources()
        {
            await Resources.UnloadUnusedAssets();
        }

        private async UniTask CleanUnusedAddressablesAssets()
        {
            // TODO: Try to unload unused addressables assets here (if possible)
        }

        private async UniTask CleanOtherUnusedAssets()
        {
            // TODO: If there is any other assets in memory which can be cleaned up, do it here.
        }

        private void CleanGC()
        {
            GC.Collect();
        }

        #endregion

    }
}