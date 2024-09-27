using System;

namespace _Project.ServerTimeStamp.Services
{
    public interface IRequestServerTimeStampService
    {
        void GetCurrentEpochSecondsFromServer(Action<long, bool> currentEpochSeconds);
    }
}