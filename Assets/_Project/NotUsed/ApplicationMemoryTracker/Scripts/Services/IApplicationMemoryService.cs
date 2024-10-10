using _Project.ApplicationMemoryTracker.Scripts.Signals;

namespace _Project.ApplicationMemoryTracker.Scripts.Services
{
    public interface IApplicationMemoryService
    {
        ApplicationMemoryCriticalSignal ApplicationMemoryCriticalSignal { get; set; }
        ApplicationMemoryCleanedSignal ApplicationMemoryCleanedSignal { get; set; }

        bool IsMemoryCleaningBusy { get; }

        void RequestMemoryCheck();
        bool HasEnoughMemory();
        bool IsMemoryCleaningInCooldown();

        /// <summary>
        ///     Cleans all unused assets.Warning: That is a heavy process.
        /// </summary>
        /// <param name="force">Should force to start cleaning memory by bypassing memory cleaning cooldown or any active memory cleaning operation?</param>
        void CleanAllUnusedAssets(bool force = false);
    }
}