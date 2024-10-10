using System;

namespace _Project.ServerTimeStamp.Scripts.Services
{
    public interface IRequestServerTimeStampService
    {
        void GetCurrentEpochSecondsFromServer(Action<long, bool> currentEpochSeconds);
    }
}