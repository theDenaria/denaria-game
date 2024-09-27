namespace _Project.ServerTimeStamp.Models
{
    public interface IServerTimeStampModel
    {
        int DifferenceInSeconds { get; set; }
        long CurrentEpochSeconds();
        long TargetEpochSeconds(int duration);
    }
}