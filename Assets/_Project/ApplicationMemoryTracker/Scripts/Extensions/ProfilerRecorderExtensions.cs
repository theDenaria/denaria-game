using System.Text;
using Unity.Profiling;

namespace _Project.ApplicationMemoryTracker.Scripts.Extensions
{
    public static class ProfilerRecorderExtensions
    {
        public static string ToLogString(this ProfilerRecorder source, string statName, long divider)
        {
            if (!source.Valid) { return ""; }
            return GetLogFormatString(in source, statName, divider);;
        }

        public static void ToLogString(this ProfilerRecorder source, string statName, long divider, StringBuilder stringBuilder)
        {
            if (stringBuilder == null || !source.Valid)
            {
                return;
            }
            stringBuilder.Append(GetLogFormatString(in source, statName, divider));
        }


        private static string GetLogFormatString(in ProfilerRecorder source, string statName, long divider)
        {
            bool isValidDivider = divider > 1 || divider < 0;
            return string.Format("{0}: {1}\n",
                statName ?? "UnknownStat",
                isValidDivider ? source.LastValue / divider : source.LastValue
            );
        }
    }
}