using System.Text;
using _Project.ApplicationMemoryTracker.Scripts.Extensions;
using Unity.Profiling;
using _Project.Utilities;

namespace _Project.ApplicationMemoryTracker.Scripts.Models
{
    public class MemoryProfilerRecorders : IMemoryProfilerRecorders
    {
        public ProfilerRecorder SystemUsedMemory { get; set; }
        public ProfilerRecorder SystemTotalUsedMemory { get; set; }
        public ProfilerRecorder AppResidentMemory { get; set; }
        public ProfilerRecorder AppCommittedMemory { get; set; }
        public ProfilerRecorder TotalUsedMemory { get; set; }
        public ProfilerRecorder TotalReservedMemory { get; set; }
        public ProfilerRecorder GCUsedMemory { get; set; }
        public ProfilerRecorder GCReservedMemory { get; set; }
        public ProfilerRecorder TextureMemory { get; set; }
        public ProfilerRecorder TextureCount { get; set; }


        public void DisposeAllProfilerRecorders()
        {
            if (SystemUsedMemory.Valid) { SystemUsedMemory.Dispose(); }
            if (SystemTotalUsedMemory.Valid) { SystemTotalUsedMemory.Dispose(); }
            if (AppResidentMemory.Valid) { AppResidentMemory.Dispose(); }
            if (AppCommittedMemory.Valid) { AppCommittedMemory.Dispose(); }
            if (TotalUsedMemory.Valid) { TotalUsedMemory.Dispose(); }
            if (TotalReservedMemory.Valid) { TotalReservedMemory.Dispose(); }
            if (GCUsedMemory.Valid) { GCUsedMemory.Dispose(); }
            if (GCReservedMemory.Valid) { GCReservedMemory.Dispose(); }
            if (TextureMemory.Valid) { TextureMemory.Dispose(); }
            if (TextureCount.Valid) { TextureCount.Dispose(); }
        }

        public string ToLogString()
        {
            StringBuilder strBuilder = new StringBuilder();
            SystemUsedMemory.ToLogString("SystemUsedMemory", Constants.MEMORY_BYTE_TO_MB_DIVIDER, strBuilder);
            SystemTotalUsedMemory.ToLogString("SystemTotalUsedMemory", Constants.MEMORY_BYTE_TO_MB_DIVIDER, strBuilder);
            AppResidentMemory.ToLogString("AppResidentMemory", Constants.MEMORY_BYTE_TO_MB_DIVIDER, strBuilder);
            AppCommittedMemory.ToLogString("AppCommittedMemory", Constants.MEMORY_BYTE_TO_MB_DIVIDER, strBuilder);
            TotalUsedMemory.ToLogString("TotalUsedMemory", Constants.MEMORY_BYTE_TO_MB_DIVIDER, strBuilder);
            TotalReservedMemory.ToLogString("TotalReservedMemory", Constants.MEMORY_BYTE_TO_MB_DIVIDER, strBuilder);
            GCUsedMemory.ToLogString("GCUsedMemory", Constants.MEMORY_BYTE_TO_MB_DIVIDER, strBuilder);
            GCReservedMemory.ToLogString("GCReservedMemory", Constants.MEMORY_BYTE_TO_MB_DIVIDER, strBuilder);
            TextureMemory.ToLogString("TextureMemory", Constants.MEMORY_BYTE_TO_MB_DIVIDER, strBuilder);
            TextureCount.ToLogString("TextureCount", 1, strBuilder);
            return strBuilder.ToString();
        }
    }
}