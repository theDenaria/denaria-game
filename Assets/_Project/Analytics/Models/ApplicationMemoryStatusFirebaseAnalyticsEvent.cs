using _Project.Analytics.Enums;
using _Project.ApplicationMemoryTracker.Scripts.Models;

namespace _Project.Analytics.Models
{
    public class ApplicationMemoryStatusFirebaseAnalyticsEvent : FirebaseAnalyticsEvent
    {
        [Inject] public IMemoryProfilerRecorders MemoryProfilerRecorders { get; set; }

        public ApplicationMemoryStatusFirebaseAnalyticsEvent()
        { }

        public ApplicationMemoryStatusFirebaseAnalyticsEvent SetParametersAndReturn(ApplicationMemoryStatusAnalyticsEventFiringReasons eventFiringReason)
        {
            EventName = "memory_tracking";

            EventParameters.Add("system_used_memory", new FirebaseAnalyticsEventParameter(MemoryProfilerRecorders.SystemUsedMemory.LastValue));
            EventParameters.Add("system_total_used_memory", new FirebaseAnalyticsEventParameter(MemoryProfilerRecorders.SystemTotalUsedMemory.LastValue));

            EventParameters.Add("app_resident_memory", new FirebaseAnalyticsEventParameter(MemoryProfilerRecorders.AppResidentMemory.LastValue));
            EventParameters.Add("app_committed_memory", new FirebaseAnalyticsEventParameter(MemoryProfilerRecorders.AppCommittedMemory.LastValue));

            EventParameters.Add("total_used_memory", new FirebaseAnalyticsEventParameter(MemoryProfilerRecorders.TotalUsedMemory.LastValue));
            EventParameters.Add("total_reserved_memory", new FirebaseAnalyticsEventParameter(MemoryProfilerRecorders.TotalReservedMemory.LastValue));

            EventParameters.Add("gc_used_memory", new FirebaseAnalyticsEventParameter(MemoryProfilerRecorders.GCUsedMemory.LastValue));
            EventParameters.Add("gc_reserved_memory", new FirebaseAnalyticsEventParameter(MemoryProfilerRecorders.GCReservedMemory.LastValue));

            EventParameters.Add("status", new FirebaseAnalyticsEventParameter(eventFiringReason.ToString()));

            return this;
        }
    }
}