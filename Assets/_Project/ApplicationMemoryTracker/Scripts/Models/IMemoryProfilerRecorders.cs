using Unity.Profiling;

namespace _Project.ApplicationMemoryTracker.Scripts.Models
{
    public interface IMemoryProfilerRecorders
    {
        ProfilerRecorder SystemUsedMemory { get; set; }
        ProfilerRecorder SystemTotalUsedMemory { get; set; }
        ProfilerRecorder AppResidentMemory { get; set; }
        ProfilerRecorder AppCommittedMemory { get; set; }
        ProfilerRecorder TotalUsedMemory { get; set; }
        ProfilerRecorder TotalReservedMemory { get; set; }
        ProfilerRecorder GCUsedMemory { get; set; }
        ProfilerRecorder GCReservedMemory { get; set; }
        ProfilerRecorder TextureMemory { get; set; }
        ProfilerRecorder TextureCount { get; set; }

        void DisposeAllProfilerRecorders();
        string ToLogString();
    }
}