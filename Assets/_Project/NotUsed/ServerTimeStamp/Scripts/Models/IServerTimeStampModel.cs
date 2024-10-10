namespace _Project.ServerTimeStamp.Scripts.Models
{
    public interface IServerTimeStampModel
    {
        int DifferenceInSeconds { get; set; }
        long CurrentEpochSeconds();
        long TargetEpochSeconds(int duration);
    }
}