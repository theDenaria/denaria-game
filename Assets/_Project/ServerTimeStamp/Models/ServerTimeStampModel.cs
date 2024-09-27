namespace _Project.ServerTimeStamp.Models
{
    public class ServerTimeStampModel : IServerTimeStampModel
    {
        public int DifferenceInSeconds { get; set; }
        public long CurrentEpochSeconds()
        {
            return DateUtility.GetCurrentEpochSeconds() - DifferenceInSeconds;
        }
        
        public long TargetEpochSeconds(int duration)
        {
            return CurrentEpochSeconds() + duration;
        }
    }
}